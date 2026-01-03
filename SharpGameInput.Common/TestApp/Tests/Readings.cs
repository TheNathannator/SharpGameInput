using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SharpGameInput.TestApp.Display;
using SharpGameInput.TestApp.Utility;

namespace SharpGameInput.TestApp.Tests
{
    internal class ReadingsTest
    {
        public static void Run(IGameInput gameInput)
        {
            ConsoleMenu.WriteMenuHeader("Readings");

            (string name, Action<IGameInput> func)[] subTests =
            [
                ("Polling", Polling),
                ("Polling (Per-Device)", PollingPerDevice),
                ("Callback", Callback),
            ];

            int choice = ConsoleMenu.PromptChoice("Select a sub-test", "Exit", subTests.Select((i) => i.name));
            if (choice >= 0)
            {
                subTests[choice].func(gameInput);
            }
        }

        public static GameInputKind PromptInputKind()
        {
            var kinds = ((GameInputKind[]) Enum.GetValues(typeof(GameInputKind)))
                .Except([GameInputKind.Unknown, GameInputKind.AnyKind])
                .ToArray();

            Console.WriteLine();
            int choice = ConsoleMenu.PromptChoice("Select an input kind", "AnyKind", kinds.Select((i) => i.ToString()));
            if (choice >= 0)
            {
                return kinds[choice];
            }

            return GameInputKind.AnyKind;
        }

        public static void Polling(IGameInput gameInput)
        {
            var inputKind = PromptInputKind();

            Console.WriteLine("Press Enter to stop this test and return to the main menu.");

            var lastReport = new ByteBuffer();
            for (; !Console.KeyAvailable || Console.ReadKey(intercept: true).Key != ConsoleKey.Enter; Thread.Sleep(1))
            {
                PollAndPrintReport(gameInput, inputKind, null, lastReport);
            }
        }

        public static void PollingPerDevice(IGameInput gameInput)
        {
            var inputKind = PromptInputKind();

            var deviceThreads = new Dictionary<IGameInputDevice, (Thread thread, EventWaitHandle stopHandle)>();

            if (!gameInput.RegisterDeviceCallback(
                null,
                inputKind,
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
                                while (!stopHandle.WaitOne(0) && PollAndPrintReport(gameInput, inputKind, device, lastReport));
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
                ConsoleMenu.WaitForKey("Press any key to return to the main menu...");
                return;
            }

            ConsoleMenu.WaitForKey("Press any key to stop this test and return to the main menu.");

            foreach (var (thread, stopHandle) in deviceThreads.Values)
            {
                stopHandle.Set();
                thread.Join();
            }

            deviceThreads.Clear();
        }

        public static void Callback(IGameInput gameInput)
        {
            var inputKind = PromptInputKind();

            if (!gameInput.RegisterReadingCallback(
                null,
                inputKind,
#if GAMEINPUT_v0
                0,
#endif
                null,
#if GAMEINPUT_v0
                (callbackToken, context, reading, hasOverrunOccurred) =>
#else
                (callbackToken, context, reading) =>
#endif
                {
                    using (reading)
                    {
                        ConsolePrinting.Print(reading, null);
                    }
                },
                out var token,
                out int result
            ))
            {
                ConsolePrinting.PrintPInvokeError("Failed to register reading callback", result);
                ConsoleMenu.WaitForKey("Press any key to return to the main menu...");
                return;
            }

            using (token)
            {
                ConsoleMenu.WaitForKey("Press any key to stop this test and return to the main menu.");
            }
        }

        private static bool PollAndPrintReport(IGameInput gameInput, GameInputKind reportKind, IGameInputDevice? device, ByteBuffer lastReport)
        {
            int result = gameInput.GetCurrentReading(reportKind, device, out var reading);
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
                ConsolePrinting.Print(reading, lastReport);
            }

            return true;
        }
    }
}