using System;
using System.Linq;
using SharpGameInput.v0;

namespace SharpGameInput.TestApp
{
    internal class CallbacksTest
    {
        public static void Run(IGameInput gameInput)
        {
            ConsoleMenu.WriteMenuHeader("Callbacks");

            (string name, Action<IGameInput> func)[] subTests =
            {
                ("Reading callback", ReadingCallbackTest),
                ("Device callback", DeviceCallbackTest),
                ("System button callback", SystemButtonCallbackTest),
                ("Keyboard layout callback", KeyboardLayoutCallbackTest),
            };

            int choice = ConsoleMenu.PromptChoice("Select a sub-test", subTests.Select((i) => i.name));
            if (choice < 0)
                return;

            subTests[choice].func(gameInput);
        }

        private static void ReadingCallbackTest(IGameInput gameInput)
        {
            if (!gameInput.RegisterReadingCallback(
                null, GameInputKind.AnyKind, 0,
                null, ReadingCallback,
                out var token, out int result
            ))
            {
                ConsolePrinting.PrintPInvokeError("Failed to register reading callback", result);
                ConsoleMenu.WaitForKey("Press any key to return to the main menu...");
                return;
            }

            ConsoleMenu.WaitForKey("Press any key to stop this test and return to the main menu.");
            token.Unregister(5000);
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

            ConsoleMenu.WaitForKey("Press any key to stop this test and return to the main menu.");
            token.Unregister(5000);
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

            ConsoleMenu.WaitForKey("Press any key to stop this test and return to the main menu.");
            token.Unregister(5000);
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

            ConsoleMenu.WaitForKey("Press any key to stop this test and return to the main menu.");
            token.Unregister(5000);
        }

        private static void ReadingCallback(
            LightGameInputCallbackToken callbackToken,
            object? context,
            LightIGameInputReading reading,
            bool hasOverrunOccurred
        )
        {
            using (reading)
            {
                // Check report type
                // We only read raw reports here for simplicity
                var kind = reading.GetInputKind();
                if ((kind & GameInputKind.RawDeviceReport) == 0)
                {
                    Console.WriteLine(kind.ToString());
                    return;
                }

                byte[]? dummy = null;
                ConsolePrinting.PrintRawReport(reading, ref dummy);
            }
        }

        private static unsafe void DeviceCallback(
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

            ConsolePrinting.WriteTimestamp(timestamp);
            Console.WriteLine(isConnected ? ": Device connected" : ": Device disconnected");

            ref readonly var info = ref device.GetDeviceInfo();
            Console.WriteLine($"- Name: {GameInputString.ToString(info.displayName)}");
            Console.WriteLine($"- Hardware IDs: VID_{info.vendorId:X4}&PID_{info.productId:X4}&REV_{info.revisionNumber:X4}");
            Console.WriteLine($"- Device ID:      {info.deviceId}");
            Console.WriteLine($"- Device root ID: {info.deviceRootId}");
            if (!isConnected)
                return;

            Console.WriteLine($"- Family: {info.deviceFamily}");
            Console.WriteLine($"- Capabilities: {info.capabilities}");
            Console.WriteLine($"- Supported inputs: {info.supportedInput}");
            Console.WriteLine($"- Hardware version: {info.hardwareVersion}");
            Console.WriteLine($"- Firmware version: {info.firmwareVersion}");
            Console.WriteLine($"- Interface number: {info.interfaceNumber}");
            Console.WriteLine($"- Collection number: {info.collectionNumber}");
            Console.WriteLine($"- Usage: {info.usage.page:X4}:{info.usage.id:X4}");

            Console.WriteLine($"- {info.deviceStringCount} strings");
            if (info.deviceStrings != null)
            {
                for (int i = 0; i < info.deviceStringCount; i++)
                {
                    Console.WriteLine($"  - {info.deviceStrings[i]}");
                }
            }

            Console.WriteLine($"- Descriptor data: {info.deviceDescriptorSize} bytes");
            if (info.deviceDescriptorData != null && info.deviceDescriptorSize > 0)
            {
                var descriptor = new ReadOnlySpan<byte>(info.deviceDescriptorData, (int)info.deviceDescriptorSize);
                ConsolePrinting.PrintBufferWrapped(descriptor, indentAmount: 2, wrapCount: 32);
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