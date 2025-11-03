using System;
using System.Diagnostics;
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
            uint reportId = rawReport.GetReportInfo().id;
            UIntPtr size = rawReport.GetRawDataSize();

            Span<byte> buffer = stackalloc byte[(int)size];
            unsafe
            {
                fixed (byte* ptr = buffer)
                {
                    UIntPtr readSize = rawReport.GetRawData(size, ptr);
                    Debug.Assert(size == readSize);
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
            Console.Write(size);
            Console.Write(", ");
            PrintBuffer(buffer);
        }
    }
}