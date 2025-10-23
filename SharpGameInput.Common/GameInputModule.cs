using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.System.Registry;

namespace SharpGameInput.Common
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

    internal static class GameInputModule
    {
        private static readonly Guid IID_IGameInput_v0 = new("11BE2A7E-4254-445A-9C09-FFC40F006918");

        private static SafeHandle? _gameInputDll = null;
        private static unsafe GameInputCreate _gameInputCreate;
        private static unsafe GameInputInitialize _gameInputInitialize;

        public static bool Create(Guid iid, out IntPtr gameInput)
        {
            return Create(iid, out gameInput, out _);
        }

        public static unsafe bool Create(Guid iid, out IntPtr gameInput, out int result)
        {
            gameInput = IntPtr.Zero;

            if (_gameInputDll == null)
            {
                var loadResult = LoadGameInputDll();
                if (loadResult.Failed)
                {
                    result = loadResult;
                    return false;
                }
            }

            if (_gameInputInitialize != null)
            {
                result = _gameInputInitialize(iid, out gameInput);
            }
            else if (_gameInputCreate != null)
            {
                if (iid == IID_IGameInput_v0)
                {
                    result = _gameInputCreate(out gameInput);
                }
                else
                {
                    result = HRESULT.E_NOINTERFACE;
                }
            }
            else
            {
                Debug.WriteLine("(!!UNREACHABLE!!) No GameInput creation methods got loaded! This is a bug in the module loading code.");
                result = HRESULT.E_FAIL;
            }

            return result >= 0 && gameInput != IntPtr.Zero;
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
