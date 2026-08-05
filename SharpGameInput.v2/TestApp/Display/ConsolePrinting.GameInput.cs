using System;

namespace SharpGameInput.TestApp.Display
{
    internal static partial class ConsolePrinting
    {
        public static void Print(in GameInputDeviceInfo info)
        {
            Console.WriteLine($"- Name: {info.GetDisplayName()}");
            Console.WriteLine($"- Path: {info.GetPnpPath()}");
            Console.WriteLine($"- Container: {info.containerId}");
            Console.WriteLine($"-----");
            Console.WriteLine($"- Hardware IDs: VID_{info.vendorId:X4}&PID_{info.productId:X4}&REV_{info.revisionNumber:X4}");
            Console.WriteLine($"- Usage: {info.usage}");
            Console.WriteLine($"- Hardware version: {info.hardwareVersion}");
            Console.WriteLine($"- Firmware version: {info.firmwareVersion}");
            Console.WriteLine($"-----");
            Console.WriteLine($"- Device ID:      {info.deviceId}");
            Console.WriteLine($"- Device root ID: {info.deviceRootId}");
            Console.WriteLine($"-----");
            Console.WriteLine($"- Family: {info.deviceFamily}");
            Console.WriteLine($"- Supported inputs: {info.supportedInput}");
            Console.WriteLine($"- Supported rumble: {info.supportedRumbleMotors}");
            Console.WriteLine($"- Supported system buttons: {info.supportedSystemButtons}");
        }
    }
}
