using System;
using System.Collections.Generic;

namespace Caist.Siemens.Types
{
    /// <summary>
    /// 包含在之间转换的方法，请参见 <see cref="T:System.DateTime"/> 和日期时间值的S7表示。
    /// </summary>
    public static class DateTime
    {
        public static readonly System.DateTime SpecMinimumDateTime = new System.DateTime(1990, 1, 1);

        public static readonly System.DateTime SpecMaximumDateTime = new System.DateTime(2089, 12, 31, 23, 59, 59, 999);

        /// <summary>
        /// 解析 <see cref="T:System.DateTime"/> 到字节值.
        /// </summary>
        /// <param name="bytes">从PLC读取输入字节.</param>
        public static System.DateTime FromByteArray(byte[] bytes)
        {
            return FromByteArrayImpl(bytes);
        }

        /// <summary>
        /// 解析bytes数组 <see cref="T:System.DateTime"/> 到字节值.
        /// </summary>
        /// <param name="bytes">从PLC读取输入字节.</param>
        public static System.DateTime[] ToArray(byte[] bytes)
        {
            if (bytes.Length % 8 != 0)
                throw new ArgumentOutOfRangeException(nameof(bytes), bytes.Length,
                    $"Parsing an array of DateTime requires a multiple of 8 bytes of input data, input data is '{bytes.Length}' long.");

            var cnt = bytes.Length / 8;
            var result = new System.DateTime[bytes.Length / 8];

            for (var i = 0; i < cnt; i++)
                result[i] = FromByteArrayImpl(new ArraySegment<byte>(bytes, i * 8, 8));

            return result;
        }

        private static System.DateTime FromByteArrayImpl(IList<byte> bytes)
        {
            if (bytes.Count != 8)
                throw new ArgumentOutOfRangeException(nameof(bytes), bytes.Count,
                    $"Parsing a DateTime requires exactly 8 bytes of input data, input data is {bytes.Count} bytes long.");

            int DecodeBcd(byte input) => 10 * (input >> 4) + (input & 0b00001111);

            int ByteToYear(byte bcdYear)
            {
                var input = DecodeBcd(bcdYear);
                if (input < 90) return input + 2000;
                if (input < 100) return input + 1900;

                throw new ArgumentOutOfRangeException(nameof(bcdYear), bcdYear,
                    $"Value '{input}' is higher than the maximum '99' of S7 date and time representation.");
            }

            int AssertRangeInclusive(int input, byte min, byte max, string field)
            {
                if (input < min)
                    throw new ArgumentOutOfRangeException(nameof(input), input,
                        $"Value '{input}' is lower than the minimum '{min}' allowed for {field}.");
                if (input > max)
                    throw new ArgumentOutOfRangeException(nameof(input), input,
                        $"Value '{input}' is higher than the maximum '{max}' allowed for {field}.");

                return input;
            }

            var year = ByteToYear(bytes[0]);
            var month = AssertRangeInclusive(DecodeBcd(bytes[1]), 1, 12, "month");
            var day = AssertRangeInclusive(DecodeBcd(bytes[2]), 1, 31, "day of month");
            var hour = AssertRangeInclusive(DecodeBcd(bytes[3]), 0, 23, "hour");
            var minute = AssertRangeInclusive(DecodeBcd(bytes[4]), 0, 59, "minute");
            var second = AssertRangeInclusive(DecodeBcd(bytes[5]), 0, 59, "second");
            var hsec = AssertRangeInclusive(DecodeBcd(bytes[6]), 0, 99, "first two millisecond digits");
            var msec = AssertRangeInclusive(bytes[7] >> 4, 0, 9, "third millisecond digit");
            var dayOfWeek = AssertRangeInclusive(bytes[7] & 0b00001111, 1, 7, "day of week");

            return new System.DateTime(year, month, day, hour, minute, second, hsec * 10 + msec);
        }

        /// <summary>
        /// 参考 <see cref="T:System.DateTime"/>值到字节数组.
        /// </summary>
        /// <param name="dateTime">要转换的日期时间值.</param>
        ///   or after <see cref="P:SpecMaximumDateTime"/>.</exception>
        public static byte[] ToByteArray(System.DateTime dateTime)
        {
            byte EncodeBcd(int value)
            {
                return (byte) ((value / 10 << 4) | value % 10);
            }

            if (dateTime < SpecMinimumDateTime)
                throw new ArgumentOutOfRangeException(nameof(dateTime), dateTime,
                    $"Date time '{dateTime}' is before the minimum '{SpecMinimumDateTime}' supported in S7 date time representation.");

            if (dateTime > SpecMaximumDateTime)
                throw new ArgumentOutOfRangeException(nameof(dateTime), dateTime,
                    $"Date time '{dateTime}' is after the maximum '{SpecMaximumDateTime}' supported in S7 date time representation.");

            byte MapYear(int year) => (byte) (year < 2000 ? year - 1900 : year - 2000);

            int DayOfWeekToInt(DayOfWeek dayOfWeek) => (int) dayOfWeek + 1;

            return new[]
            {
                EncodeBcd(MapYear(dateTime.Year)),
                EncodeBcd(dateTime.Month),
                EncodeBcd(dateTime.Day),
                EncodeBcd(dateTime.Hour),
                EncodeBcd(dateTime.Minute),
                EncodeBcd(dateTime.Second),
                EncodeBcd(dateTime.Millisecond / 10),
                (byte) (dateTime.Millisecond % 10 << 4 | DayOfWeekToInt(dateTime.DayOfWeek))
            };
        }

        /// <summary>
        /// 参考 <see cref="T:System.DateTime"/> 字节数组的值。.
        /// </summary>
        /// <param name="dateTimes">要转换的日期时间值.</param>
        ///   or after <see cref="P:SpecMaximumDateTime"/>.</exception>
        public static byte[] ToByteArray(System.DateTime[] dateTimes)
        {
            var bytes = new List<byte>(dateTimes.Length * 8);
            foreach (var dateTime in dateTimes) bytes.AddRange(ToByteArray(dateTime));

            return bytes.ToArray();
        }
    }
}