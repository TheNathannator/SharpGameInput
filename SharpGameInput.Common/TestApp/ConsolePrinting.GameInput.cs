using System;

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
    }
}