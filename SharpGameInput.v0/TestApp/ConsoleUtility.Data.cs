using System;
using System.Diagnostics;

namespace SharpGameInput.v0.TestApp
{
    internal static partial class ConsoleUtility
    {
        public static bool PrintRawReport(LightIGameInputReading reading, ref byte[]? lastReport)
        {
            if (!reading.GetRawReport(out var rawReport))
            {
                Console.WriteLine("Could not get raw report!");
                return false;
            }

            uint reportId = rawReport.GetReportInfo().id;
            UIntPtr size = rawReport.GetRawDataSize();

            using (rawReport)
            {
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
                    if (buffer.SequenceEqual(lastReport))
                    {
                        return true;
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

                Program.PrintTimestamp(reading.GetTimestamp());
                Console.Write(": Report ID: ");
                Console.Write(reportId);
                Console.Write(", size: ");
                Console.Write(size);
                Console.Write(", ");
                ConsoleUtility.WriteLine(buffer);
            }

            return true;
        }
    }
}