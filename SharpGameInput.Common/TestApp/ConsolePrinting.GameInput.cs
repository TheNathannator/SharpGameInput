using System;

#if GAMEINPUT_HAS_RAW_REPORTS
using System.Buffers;
#endif

namespace SharpGameInput.TestApp
{
    internal static partial class ConsolePrinting
    {
        private static ulong _timestampBase;

        public static void SetTimestampBase(ulong timestamp)
        {
            _timestampBase = timestamp;
        }

        public static TimeSpan TimestampToTimeSpan(ulong timestamp)
            => TimeSpan.FromSeconds(timestamp / 1000000.0);

        public static TimeSpan TimestampToTimeSpanFromStartup(ulong timestamp)
            => TimestampToTimeSpan(timestamp - _timestampBase);

        public static void WriteTimestamp(ulong timestamp)
        {
            var time = TimestampToTimeSpan(timestamp);
            var timeSinceStartup = TimestampToTimeSpanFromStartup(timestamp);
            Console.Write($"{time} ({timeSinceStartup})");
        }

        public static void Print(LightIGameInputReading reading, ByteBuffer? lastReport)
        {
            bool handled =
#if GAMEINPUT_HAS_RAW_REPORTS
                PrintRawReport(reading, lastReport) ||
#endif
                PrintGamepadReading(reading, lastReport);

            if (!handled)
            {
                var inputs = reading.GetInputKind();
                WriteTimestamp(reading.GetTimestamp());
                Console.WriteLine($": {inputs} (0x{inputs:X8})");
            }
        }

#if GAMEINPUT_HAS_RAW_REPORTS
        public static bool PrintRawReport(LightIGameInputReading reading, ByteBuffer? lastReport)
        {
            if (!reading.GetRawReport(out var rawReport))
            {
                using (rawReport)
                {
                    Print(rawReport, reading.GetTimestamp(), lastReport);
                }
                return true;
            }

            return false;
        }
#endif

        public static bool PrintGamepadReading(LightIGameInputReading reading, ByteBuffer? lastReport)
        {
            if (reading.GetGamepadState(out var state))
            {
                Print(state, reading.GetTimestamp(), lastReport);
                return true;
            }

            return false;
        }

#if GAMEINPUT_HAS_RAW_REPORTS
        public static void Print(LightIGameInputRawDeviceReport rawReport, ulong timestamp, ByteBuffer? lastReport)
        {
            const int maxStackSize = 64;

            uint reportId = rawReport.GetReportInfo().id;
            int reportSize = (int)rawReport.GetRawDataSize();

            byte[]? poolBuffer = null;
            Span<byte> buffer = reportSize > maxStackSize
                ? (poolBuffer = ArrayPool<byte>.Shared.Rent(reportSize))
                : stackalloc byte[maxStackSize];

            try
            {
                unsafe
                {
                    fixed (byte* ptr = buffer)
                    {
                        int readSize = (int)rawReport.GetRawData((UIntPtr)buffer.Length, ptr);
                        buffer = buffer.Slice(0, Math.Min(readSize, buffer.Length));
                    }
                }

                if (lastReport?.Write(buffer) ?? true)
                {
                    WriteTimestamp(timestamp);
                    Console.Write($": [{buffer.Length:D3}] {reportId:X2}: ");
                    WriteBuffer(buffer);
                    Console.WriteLine();
                }

            }
            finally
            {
                if (poolBuffer != null)
                {
                    ArrayPool<byte>.Shared.Return(poolBuffer);
                }
            }
        }
#endif

        public static void Print(in GameInputGamepadState state, ulong timestamp, ByteBuffer? lastReport)
        {
            if (lastReport?.Write(state) ?? true)
            {
                WriteTimestamp(timestamp);
                Console.Write($": buttons {(int)state.buttons:8X}");
                Console.Write($"  LT {state.leftTrigger:F3} RT {state.rightTrigger:F3}");
                Console.Write($"  LX {state.leftThumbstickX:3} LY {state.leftThumbstickY:3}");
                Console.Write($"  RX {state.rightThumbstickX:3} RY {state.rightThumbstickY:3}");
                Console.WriteLine();
            }
        }
    }
}