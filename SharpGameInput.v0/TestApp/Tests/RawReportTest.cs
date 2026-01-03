using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SharpGameInput.TestApp
{
    internal class RawReportTest
    {
        public static void Run(IGameInput gameInput)
        {
            ConsoleMenu.WriteMenuHeader("Read Raw Reports");

            (string name, Action<IGameInput> func)[] subTests =
            {
                ("Direct Polling", Direct),
                ("Polling with Device Callback", WithCallbacks),
            };

            int choice = ConsoleMenu.PromptChoice("Select a sub-test", subTests.Select((i) => i.name));
            if (choice < 0)
                return;

            subTests[choice].func(gameInput);
        }

        public static void Direct(IGameInput gameInput)
        {
            Console.WriteLine("Press any key to stop the test.");

            var lastReport = new ByteBuffer();
            for (; !Console.KeyAvailable; Thread.Sleep(1))
            {
                PollAndPrintReport(gameInput, null, lastReport);
            }

            // Consume keypress
            Console.ReadKey(intercept: true);
        }

        public static void WithCallbacks(IGameInput gameInput)
        {
            var deviceThreads = new Dictionary<IGameInputDevice, (Thread thread, EventWaitHandle stopHandle)>();

            if (!gameInput.RegisterDeviceCallback(
                null,
                GameInputKind.RawDeviceReport,
                GameInputDeviceStatus.Connected,
                GameInputEnumerationKind.AsyncEnumeration,
                null,
                (token, context, _device, timestamp, currentStatus, previousStatus) =>
                {
                    var device = _device.ToComPtr();
                    if ((currentStatus & GameInputDeviceStatus.Connected) != 0)
                    {
                        device = device.Duplicate();

                        ref readonly var info = ref device.GetDeviceInfo();
                        Console.WriteLine($"Device {info.deviceId} connected.");

                        var stopHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
                        var thread = new Thread(() =>
                        {
                            using (device)
                            {
                                var lastReport = new ByteBuffer();
                                while (!stopHandle.WaitOne(0) && PollAndPrintReport(gameInput, device, lastReport));
                            }
                        });
                        thread.Start();
                        deviceThreads.Add(device, (thread, stopHandle));
                    }
                    else
                    {
                        if (!deviceThreads.TryGetValue(device, out var read))
                            return;

                        deviceThreads.Remove(device);
                        read.stopHandle.Set();
                        read.thread.Join();
                    }
                },
                out var callbackToken,
                out int result
            ))
            {
                ConsolePrinting.PrintPInvokeError("Failed to register device callback", result);
                return;
            }

            ConsoleMenu.WaitForKey("Press any key to stop the test.");

            foreach (var (thread, stopHandle) in deviceThreads.Values)
            {
                stopHandle.Set();
                thread.Join();
            }

            deviceThreads.Clear();
        }

        private static bool PollAndPrintReport(IGameInput gameInput, IGameInputDevice? device, ByteBuffer lastReport)
        {
            int result = gameInput.GetCurrentReading(GameInputKind.RawDeviceReport, device, out var reading);
            if (result < 0)
            {
                if (result == (int)GameInputResult.ReadingNotFound)
                {
                    return true;
                }
                else if (device != null && result == (int)GameInputResult.DeviceDisconnected)
                {
                    Console.WriteLine($"Device {device.GetDeviceInfo().deviceId} disconnected.");
                    return false;
                }
                else
                {
                    if (device != null)
                    {
                        var deviceId = device.GetDeviceInfo().deviceId;
                        ConsolePrinting.PrintPInvokeError($"Failed to get current reading for device {deviceId}", result);
                    }
                    else
                    {
                        ConsolePrinting.PrintPInvokeError("Failed to get current reading", result);
                    }
                    return false;
                }
            }

            using (reading)
            {
                ConsolePrinting.PrintRawReport(reading, lastReport);
            }

            return true;
        }
    }
}