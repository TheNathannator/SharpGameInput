using System;
using System.Linq;
using SharpGameInput.TestApp.Display;

namespace SharpGameInput.TestApp.Tests
{
    internal class CallbacksTest
    {
        public static void Run(IGameInput gameInput)
        {
            ConsoleMenu.WriteMenuHeader("Callbacks");

            (string name, Action<IGameInput> func)[] subTests =
            [
                ("Reading callback", ReadingsTest.Callback),
                ("Device callback", DeviceCallbackTest),
                ("System button callback", SystemButtonCallbackTest),
                ("Keyboard layout callback", KeyboardLayoutCallbackTest),
            ];

            int choice = ConsoleMenu.PromptChoice("Select a sub-test", "Exit", subTests.Select((i) => i.name));
            if (choice < 0)
                return;

            subTests[choice].func(gameInput);
        }

        private static void DeviceCallbackTest(IGameInput gameInput)
        {
            if (!gameInput.RegisterDeviceCallback(
                null, GameInputKind.AnyKind, GameInputDeviceStatus.AnyStatus, GameInputEnumerationKind.AsyncEnumeration,
                null, DeviceCallback,
                out var token, out int result
            ))
            {
                ConsolePrinting.PrintPInvokeError("Failed to register device callback", result);
                ConsoleMenu.WaitForKey("Press any key to return to the main menu...");
                return;
            }

            using (token)
            {
                ConsoleMenu.WaitForKey("Press any key to stop this test and return to the main menu.");
            }
        }

        private static void SystemButtonCallbackTest(IGameInput gameInput)
        {
            if (!gameInput.RegisterSystemButtonCallback(
                null, GameInputSystemButtons.Guide | GameInputSystemButtons.Share,
                null, SystemButtonCallback,
                out var token, out int result
            ))
            {
                ConsolePrinting.PrintPInvokeError("Failed to register guide button callback", result);
                ConsoleMenu.WaitForKey("Press any key to return to the main menu...");
                return;
            }

            using (token)
            {
                ConsoleMenu.WaitForKey("Press any key to stop this test and return to the main menu.");
            }
        }

        private static void KeyboardLayoutCallbackTest(IGameInput gameInput)
        {
            if (!gameInput.RegisterKeyboardLayoutCallback(
                null,
                null, KeyboardLayoutCallback,
                out var token, out int result
            ))
            {
                ConsolePrinting.PrintPInvokeError("Failed to register keyboard layout callback", result);
                ConsoleMenu.WaitForKey("Press any key to return to the main menu...");
                return;
            }

            using (token)
            {
                ConsoleMenu.WaitForKey("Press any key to stop this test and return to the main menu.");
            }
        }

        private static void DeviceCallback(
            LightGameInputCallbackToken callbackToken,
            object? context,
            LightIGameInputDevice device,
            ulong timestamp,
            GameInputDeviceStatus currentStatus,
            GameInputDeviceStatus previousStatus
        )
        {
            bool isConnected = (currentStatus & GameInputDeviceStatus.Connected) != 0;
            bool wasConnected = (previousStatus & GameInputDeviceStatus.Connected) != 0;
            if (isConnected == wasConnected)
                return;

            ref readonly var info = ref device.GetDeviceInfo();

            Console.WriteLine($"==================================================");
            ConsolePrinting.WriteTimestamp(timestamp);
            if (isConnected)
            {
                Console.WriteLine($": Device {info.deviceId} connected");
                ConsolePrinting.Print(info);
            }
            else
            {
                Console.WriteLine($": Device {info.deviceId} disconnected");
            }
        }

        private static void SystemButtonCallback(
            LightGameInputCallbackToken callbackToken,
            object? context,
            LightIGameInputDevice device,
            ulong timestamp,
            GameInputSystemButtons currentState,
            GameInputSystemButtons previousState
        )
        {
            ConsolePrinting.WriteTimestamp(timestamp);
            Console.WriteLine($": System buttons changed. Old: {previousState}, new: {currentState}");
        }

        private static void KeyboardLayoutCallback(
            LightGameInputCallbackToken callbackToken,
            object? context,
            LightIGameInputDevice device,
            ulong timestamp,
            uint currentLayout,
            uint previousLayout
        )
        {
            ConsolePrinting.WriteTimestamp(timestamp);
            Console.Write($": Keyboard layout changed. Old: 0x{previousLayout:X8}, new: 0x{currentLayout:X8}");
        }
    }
}