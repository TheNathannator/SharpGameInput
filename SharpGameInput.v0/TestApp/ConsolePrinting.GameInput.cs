using System;
using System.Buffers;
using SharpGameInput.v0;

namespace SharpGameInput.TestApp
{
    internal static partial class ConsolePrinting
    {
        public static bool PrintRawReport(LightIGameInputReading reading, ref byte[]? lastReport)
        {
            if (!reading.GetRawReport(out var rawReport))
            {
                Console.WriteLine("Could not get raw report!");
                return false;
            }

            using (rawReport)
            {
                Print(rawReport, reading.GetTimestamp(), ref lastReport);
            }

            return true;
        }

        public static void Print(LightIGameInputRawDeviceReport rawReport, ulong timestamp, ref byte[]? lastReport)
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

                if (lastReport != null)
                {
                    // Ignore unchanged reports
                    // GameInput does not update timestamps when only third-party-defined data changes,
                    // so we have to compare state memory manually to see when things actually change
                    if (buffer.SequenceEqual(lastReport))
                    {
                        return;
                    }

                    if (lastReport.Length != buffer.Length)
                    {
                        lastReport = buffer.ToArray();
                    }
                    else
                    {
                        buffer.CopyTo(lastReport);
                    }
                }

                WriteTimestamp(timestamp);
                Console.Write(": Report ID: ");
                Console.Write(reportId);
                Console.Write(", size: ");
                Console.Write(buffer.Length);
                Console.Write(", ");
                PrintBuffer(buffer);
            }
            finally
            {
                if (poolBuffer != null)
                {
                    ArrayPool<byte>.Shared.Return(poolBuffer);
                }
            }
        }
    }
}