#if GAMEINPUT_V0 || GAMEINPUT_V3_OR_LATER
#define HAS_RAW_REPORTS
#endif

using System;
using System.Buffers;
using System.Text;
using SharpGameInput.TestApp.Utility;

namespace SharpGameInput.TestApp.Display
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
#if HAS_RAW_REPORTS
                PrintRawReport(reading, lastReport) ||
#endif
                PrintKeyboardReading(reading, lastReport) ||
                PrintGamepadReading(reading, lastReport);

            if (!handled && (lastReport == null || lastReport.Write(reading.GetTimestamp())))
            {
                var inputs = reading.GetInputKind();
                WriteTimestamp(reading.GetTimestamp());
                Console.WriteLine($": {inputs} (0x{inputs:X})");
            }
        }

#if HAS_RAW_REPORTS
        public static bool PrintRawReport(LightIGameInputReading reading, ByteBuffer? lastReport)
        {
            if (!reading.GetRawReport(out var rawReport))
            {
                return false;
            }

            using (rawReport)
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

                    if (lastReport == null || lastReport.Write(buffer))
                    {
                        WriteTimestamp(reading.GetTimestamp());
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

            return true;
        }
#endif

        public static bool PrintKeyboardReading(LightIGameInputReading reading, ByteBuffer? lastReport)
        {
            const int maxStackSize = 16;

            int keyCount = (int)reading.GetKeyCount();
            if (keyCount < 1 && (reading.GetInputKind() & GameInputKind.Keyboard) == 0)
            {
                return false;
            }

            GameInputKeyState[]? poolBuffer = null;
            Span<GameInputKeyState> keyBuffer = keyCount > maxStackSize
                ? (poolBuffer = ArrayPool<GameInputKeyState>.Shared.Rent(keyCount))
                : stackalloc GameInputKeyState[maxStackSize];

            try
            {
                unsafe
                {
                    fixed (GameInputKeyState* ptr = keyBuffer)
                    {
                        int readKeys = (int)reading.GetKeyState((uint)keyBuffer.Length, ptr);
                        keyBuffer = keyBuffer.Slice(0, Math.Min(readKeys, keyBuffer.Length));
                    }
                }

                if (lastReport == null || lastReport.Write(keyBuffer))
                {
                    WriteTimestamp(reading.GetTimestamp());
                    if (keyBuffer.Length == 0)
                    {
                        Console.Write(": <none>");
                    }
                    else
                    {
                        Span<char> charBuffer = stackalloc char[4];

                        Console.Write(": ");
                        for (int i = 0; i < keyBuffer.Length; i++)
                        {
                            ref readonly var key = ref keyBuffer[i];
                            if (key.virtualKey == 0 && key.scanCode == 0)
                            {
                                break;
                            }

                            if (i != 0)
                            {
                                Console.Write(", ");
                            }

                            if (key.codePoint == 0 || !Rune.TryCreate(key.codePoint, out var rune))
                            {
                                Console.Write($"<{key.scanCode:X8}>");
                                continue;
                            }

                            int written = rune.EncodeToUtf16(charBuffer);
                            for (int c = 0; c < written; c++)
                            {
                                Console.Write(charBuffer[c]);
                            }
                        }
                    }

                    Console.WriteLine();
                }

                return true;
            }
            finally
            {
                if (poolBuffer != null)
                {
                    ArrayPool<GameInputKeyState>.Shared.Return(poolBuffer);
                }
            }
        }

        public static bool PrintGamepadReading(LightIGameInputReading reading, ByteBuffer? lastReport)
        {
            if (!reading.GetGamepadState(out var state))
            {
                return false;
            }

            if (lastReport == null || lastReport.Write(state))
            {
                WriteTimestamp(reading.GetTimestamp());
                Console.Write($": buttons {(int)state.buttons:8X}");
                Console.Write($"  LT {state.leftTrigger:F3} RT {state.rightTrigger:F3}");
                Console.Write($"  LX {state.leftThumbstickX:F3} LY {state.leftThumbstickY:F3}");
                Console.Write($"  RX {state.rightThumbstickX:F3} RY {state.rightThumbstickY:F3}");
                Console.WriteLine();
            }

            return true;
        }
    }
}