using System;

namespace SharpGameInput.TestApp
{
    internal class CallbacksTest
    {
        public static void Run(IGameInput gameInput)
        {
            ConsoleUtility.WriteMenuHeader("Callbacks");

            if (!gameInput.RegisterReadingCallback(
                null, GameInputKind.Unknown, 0, null, ReadingCallback,
                out var readingToken, out int result
            ))
            {
                ConsoleUtility.WritePInvokeError("Failed to register reading callback", result);
            }

            using var readingTokenDisposer = new CallbackTokenDisposer(readingToken, 5000);

            if (!gameInput.RegisterDeviceCallback(
                null, GameInputKind.AnyKind, GameInputDeviceStatus.AnyStatus, GameInputEnumerationKind.AsyncEnumeration,
                null, DeviceCallback,
                out var deviceToken, out result
            ))
            {
                ConsoleUtility.WritePInvokeError("Failed to register device callback", result);
            }

            using var deviceTokenDisposer = new CallbackTokenDisposer(deviceToken, 5000);

            if (!gameInput.RegisterSystemButtonCallback(
                null, GameInputSystemButtons.Guide | GameInputSystemButtons.Share, null, SystemButtonCallback,
                out var systemButtonToken, out result
            ))
            {
                ConsoleUtility.WritePInvokeError("Failed to register guide button callback", result);
            }

            using var systemButtonTokenDisposer = new CallbackTokenDisposer(systemButtonToken, 5000);

            if (!gameInput.RegisterKeyboardLayoutCallback(
                null, null, KeyboardLayoutCallback,
                out var keyboardLayoutToken, out result
            ))
            {
                ConsoleUtility.WritePInvokeError("Failed to register keyboard layout callback", result);
            }

            using var keyboardLayoutTokenDisposer = new CallbackTokenDisposer(keyboardLayoutToken, 5000);

            ConsoleUtility.WaitForKey("Press any key to stop this test and return to the main menu.");
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
                Program.PrintTimestamp(reading.GetTimestamp());
                Console.Write(": ");

                // Check report type
                // We only read raw reports here for simplicity
                var kind = reading.GetInputKind();
                if ((kind & GameInputKind.RawDeviceReport) != 0)
                {
                    Console.WriteLine(kind.ToString());
                    return;
                }

                RawReportTest.PrintRawReport(reading);
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

            Program.PrintTimestamp(timestamp);
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
                ConsoleUtility.WriteWrapped(descriptor, indentAmount: 2, wrapCount: 32);
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
            Program.PrintTimestamp(timestamp);
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
            Program.PrintTimestamp(timestamp);
            Console.Write($": Keyboard layout changed. Old: 0x{previousLayout:X8}, new: 0x{currentLayout:X8}");
        }
    }
}