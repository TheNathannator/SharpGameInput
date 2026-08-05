using System;

namespace SharpGameInput.TestApp.Display
{
    internal static partial class ConsolePrinting
    {
        public static unsafe void Print(in GameInputDeviceInfo info)
        {
            Console.WriteLine($"- Name: {GameInputString.ToString(info.displayName)}");
            Console.WriteLine($"- Interface number: {info.interfaceNumber}");
            Console.WriteLine($"- Collection number: {info.collectionNumber}");
            Console.WriteLine($"-----");
            Console.WriteLine($"- Hardware IDs: VID_{info.vendorId:X4}&PID_{info.productId:X4}&REV_{info.revisionNumber:X4}");
            Console.WriteLine($"- Usage: {info.usage.page:X4}:{info.usage.id:X4}");
            Console.WriteLine($"- Hardware version: {info.hardwareVersion}");
            Console.WriteLine($"- Firmware version: {info.firmwareVersion}");
            Console.WriteLine($"-----");
            Console.WriteLine($"- Device ID:      {info.deviceId}");
            Console.WriteLine($"- Device root ID: {info.deviceRootId}");
            Console.WriteLine($"-----");
            Console.WriteLine($"- Family: {info.deviceFamily}");
            Console.WriteLine($"- Capabilities: {info.capabilities}");
            Console.WriteLine($"- Supported inputs: {info.supportedInput}");
            Console.WriteLine($"- Supported rumble: {info.supportedRumbleMotors}");
            Console.WriteLine($"-----");
            Console.WriteLine($"- {info.deviceStringCount} strings");
            if (info.deviceStrings != null)
            {
                for (int i = 0; i < info.deviceStringCount; i++)
                {
                    Console.WriteLine($"  - {info.deviceStrings[i]}");
                }
            }
            Console.WriteLine($"-----");
            Console.WriteLine($"- Descriptor data: {info.deviceDescriptorSize} bytes");
            if (info.deviceDescriptorData != null && info.deviceDescriptorSize > 0)
            {
                var descriptor = new ReadOnlySpan<byte>(info.deviceDescriptorData, (int)info.deviceDescriptorSize);
                PrintBufferWrapped(descriptor, indentAmount: 2, wrapCount: 32);
            }
        }
    }
}
