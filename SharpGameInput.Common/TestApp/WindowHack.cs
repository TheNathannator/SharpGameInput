using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.Graphics.Gdi;
using Windows.Win32.UI.WindowsAndMessaging;

namespace SharpGameInput.TestApp
{
    using static PInvoke;

    public static class WindowHack
    {
        private static readonly EventWaitHandle _windowCreated = new(false, EventResetMode.ManualReset);

        public static bool StartWindow()
        {
            var windowThread = new Thread(WindowThread);
            windowThread.Start();

            _windowCreated.WaitOne();

            if (!windowThread.IsAlive)
            {
                ConsoleMenu.WaitForKey("Press any key to exit...");
                return false;
            }

            return true;
        }

        private static unsafe LRESULT WindowProc(HWND hwnd, uint msg, WPARAM wParam, LPARAM lParam)
        {
            switch (msg)
            {
                case WM_PAINT:
                {
                    PAINTSTRUCT ps;
                    HDC hdc = BeginPaint(hwnd, &ps);

                    FillRect(hdc, &ps.rcPaint, (HBRUSH)(IntPtr)(SYS_COLOR_INDEX.COLOR_WINDOW + 1));

                    const string message = "Focus me to get input";
                    fixed (char* ptr = message)
                    {
                        DrawText(hdc, ptr, message.Length, &ps.rcPaint, DRAW_TEXT_FORMAT.DT_CENTER);
                    }

                    EndPaint(hwnd, &ps);
                    return (LRESULT)0;
                }
            }

            return DefWindowProc(hwnd, msg, wParam, lParam);
        }

        private static unsafe void WindowThread()
        {
            const string ClassName = "SharpGameInput.TestApp";

            var classNameBytes = Encoding.Unicode.GetBytes(ClassName);
            var classNamePtr = Marshal.AllocHGlobal(classNameBytes.Length + 2);
            fixed (void* ptr = classNameBytes)
            {
                Buffer.MemoryCopy(ptr, (void*)classNamePtr, classNameBytes.Length + 2, classNameBytes.Length);
            }
            Marshal.WriteInt16(classNamePtr, classNameBytes.Length, 0);

            var windowClass = new WNDCLASSW()
            {
                lpfnWndProc = WindowProc,
                lpszClassName = (char*)classNamePtr,
            };
            RegisterClass(windowClass);

            var window = CreateWindowEx(
                0,
                ClassName,
                ClassName,
                WINDOW_STYLE.WS_OVERLAPPED,
                CW_USEDEFAULT,
                CW_USEDEFAULT,
                300,
                200,
                HWND.Null,
                null,
                null,
                null
            );

            _windowCreated.Set();
            if (window.IsNull)
            {
                ConsolePrinting.PrintPInvokeError("Failed to create window (necessary to receive input)");
                return;
            }

            ShowWindow(window, SHOW_WINDOW_CMD.SW_NORMAL);

            var msg = new MSG();
            while (GetMessage(out msg, HWND.Null, 0, 0) > 0)
            {
                TranslateMessage(msg);
                DispatchMessage(msg);
            }
        }
    }
}
