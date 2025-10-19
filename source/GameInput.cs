using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.Registry;

namespace SharpGameInput
{
    using static PInvoke;

    using unsafe GameInputCreate = delegate* unmanaged[Stdcall]<
        out IntPtr, // gameInput
        int // <return>
    >;

    using unsafe GameInputInitialize = delegate* unmanaged[Stdcall]<
        in Guid, // riid
        out IntPtr, // ppv
        int // <return>
    >;

    public static partial class GameInput
    {
        public const int HResultFacility = 0x38A;

        public const ulong CurrentCallbackToken = 0xFFFFFFFFFFFFFFFF;
        public const ulong InvalidCallbackToken = 0x0000000000000000;

        private static readonly Guid IID_IGameInput_v0 = new(0x11be2a7e, 0x4254, 0x445a, 0x9c, 0x09, 0xff, 0xc4, 0x0f, 0x00, 0x69, 0x18);
        private static readonly Guid IID_IGameInput_v1 = new(0x40ffb7e4, 0x6150, 0x407a, 0xb4, 0x39, 0x13, 0x2b, 0xad, 0xc0, 0x8d, 0x2d);
        private static readonly Guid IID_IGameInput_v2 = new(0xbbaa66d2, 0x837a, 0x40f7, 0xa3, 0x03, 0x91, 0x7d, 0x50, 0x09, 0x55, 0xf4);
        private static readonly Guid IID_IGameInput_v3 = new(0x20efc1c7, 0x5d9a, 0x43ba, 0xb2, 0x6f, 0xb8, 0x07, 0xfa, 0x48, 0x60, 0x9c);

        private static SafeHandle _gameInputDll = null!;
        private static unsafe GameInputCreate _gameInputCreate;
        private static unsafe GameInputInitialize _gameInputInitialize;

        public static bool Create([NotNullWhen(true)] out IGameInput? gameInput)
        {
            return Create(out gameInput, out _);
        }

        public static unsafe bool Create([NotNullWhen(true)] out IGameInput? gameInput, out int result)
        {
            if (_gameInputDll == null)
            {
                var loadResult = LoadGameInputDll();
                if (loadResult.Failed)
                {
                    gameInput = null;
                    result = loadResult;
                    return false;
                }
            }

            IntPtr handle;
            if (_gameInputInitialize != null)
            {
                result = _gameInputInitialize(IID_IGameInput_v0, out handle);
            }
            else if (_gameInputCreate != null)
            {
                result = _gameInputCreate(out handle);
            }
            else
            {
                throw new Exception("!!UNREACHABLE!! No GameInput creation methods got loaded! This is a bug in the module loading code.");
            }

            bool success = result >= 0 && handle != IntPtr.Zero;
            gameInput = success ? new(handle, ownsHandle: true) : null;
            return success;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////
        // The following code is ported from the GameInput.cpp file provided in the GameInput NuGet package. //
        ///////////////////////////////////////////////////////////////////////////////////////////////////////

        private static HRESULT LoadGameInputDll()
        {
            string system32 = Environment.GetFolderPath(Environment.SpecialFolder.System);
            string inboxPath = Path.Combine(system32, "GameInput.dll");
            string redistPath = Path.Combine(system32, "GameInputRedist.dll");

            if (!File.Exists(redistPath))
            {
                // GameInputRedist.dll can be found in both System32 and in a separate
                // redistributable install location, so we need to check both locations
                Debug.WriteLine("[SharpGameInput] Redist not found in System32, checking registry.");

                var redistDir = GetRedistDirectory();
                if (redistDir != null)
                {
                    Debug.WriteLine("[SharpGameInput] Redist found through registry.");
                    redistPath = Path.Combine(redistDir, "GameInputRedist.dll");
                }
                else
                {
                    Debug.WriteLine("[SharpGameInput] Redist registry not found.");
                }
            }

            var (inboxVersion, inboxVersionStr) = GetDllVersion(inboxPath);
            var (redistVersion, redistVersionStr) = GetDllVersion(redistPath);

            Debug.WriteLine(inboxVersionStr != null
                ? $"[SharpGameInput] Inbox module: '{inboxPath}' v{inboxVersionStr} ({inboxVersion})"
                : $"[SharpGameInput] Inbox module not found at '{inboxPath}'"
            );
            Debug.WriteLine(redistVersionStr != null
                ? $"[SharpGameInput] Redist module: '{redistPath}' v{redistVersionStr} ({redistVersion})"
                : $"[SharpGameInput] Redist module not found at '{redistPath}'"
            );

            // Take whichever has the later version,
            // preferring the redistributable if versions match
            // Note that file existence checks are done by GetDllVersion,
            // so this will also implicitly handle the case where only one exists
            var (finalPath, finalVersionStr) = inboxVersion > redistVersion
                ? (inboxPath, inboxVersionStr)
                : (redistPath, redistVersionStr);

            if (!File.Exists(finalPath))
            {
                Debug.WriteLine($"[SharpGameInput] No versions of GameInput found!");
                return HRESULT_FROM_WIN32(WIN32_ERROR.ERROR_DLL_NOT_FOUND);
            }

            Debug.WriteLine($"[SharpGameInput] Loading GameInput {finalVersionStr} from {finalPath}");
            var module = LoadLibrary(finalPath);
            if (module == null || module.IsInvalid)
            {
                var error = (WIN32_ERROR)Marshal.GetLastWin32Error();
                Debug.WriteLine($"[SharpGameInput] Failed to load GameInput: {error}");
                return HRESULT_FROM_WIN32(error);
            }

            unsafe
            {
                if (GetProcAddress(module, "GameInputCreate") is { IsNull: false } create)
                {
                    _gameInputCreate = (GameInputCreate)create.Value;
                }

                if (GetProcAddress(module, "GameInputInitialize") is { IsNull: false } initialize)
                {
                    _gameInputInitialize = (GameInputInitialize)initialize.Value;
                }

                if (_gameInputCreate == null && _gameInputInitialize == null)
                {
                    module.Dispose();
                    Debug.WriteLine($"[SharpGameInput] Could not find init procedure in GameInput module!");
                    return HRESULT_FROM_WIN32(WIN32_ERROR.ERROR_PROC_NOT_FOUND);
                }
            }

            _gameInputDll = module;
            return HRESULT.S_OK;
        }

        private static unsafe string? GetRedistDirectory()
        {
            // `fixed` needed to use the overload of RegGetValue
            // that takes an HKEY instead of a SafeHandle
            fixed (char* redistDirKey = "SOFTWARE\\Microsoft\\GameInput")
            fixed (char* redistDirValue = "RedistDir")
            {
                uint valueByteSize = 0;
                var result = RegGetValue(
                    HKEY.HKEY_LOCAL_MACHINE,
                    redistDirKey,
                    redistDirValue,
                    REG_ROUTINE_FLAGS.RRF_RT_REG_SZ | REG_ROUTINE_FLAGS.RRF_SUBKEY_WOW6432KEY,
                    null,
                    null,
                    &valueByteSize
                );
                if (result != WIN32_ERROR.ERROR_SUCCESS)
                {
                    Debug.WriteLine($"[SharpGameInput] Failed to get size of redist path from registry: {result} (0x{(int)result:X4})");
                    return null;
                }

                char* valueStr = stackalloc char[(int)(valueByteSize / sizeof(char))];
                result = RegGetValue(
                    HKEY.HKEY_LOCAL_MACHINE,
                    redistDirKey,
                    redistDirValue,
                    REG_ROUTINE_FLAGS.RRF_RT_REG_SZ | REG_ROUTINE_FLAGS.RRF_SUBKEY_WOW6432KEY,
                    null,
                    valueStr,
                    &valueByteSize
                );
                if (result != WIN32_ERROR.ERROR_SUCCESS)
                {
                    Debug.WriteLine($"[SharpGameInput] Failed to get redist path from registry: {result} (0x{(int)result:X4})");
                    return null;
                }

                return new(valueStr);
            }
        }

        private static (long, string?) GetDllVersion(string path)
        {
            if (!File.Exists(path))
            {
                return (0, null);
            }

            var version = FileVersionInfo.GetVersionInfo(path);
            long versionNum = (version.FileMajorPart << 48) |
                (version.FileMinorPart << 32) |
                (version.FileBuildPart << 16) |
                version.FilePrivatePart;
            return (versionNum, version.FileVersion);
        }
    }
}
