using System;

namespace Caist.Siemens
{
    #if NET_FULL
    [Serializable]
    #endif
    public class InvalidDataException : Exception
    {
        public byte[] ReceivedData { get; }
        public int ErrorIndex { get; }
        public byte ExpectedValue { get; }

        public InvalidDataException(string message, byte[] receivedData, int errorIndex, byte expectedValue)
            : base(FormatMessage(message, receivedData, errorIndex, expectedValue))
        {
            ReceivedData = receivedData;
            ErrorIndex = errorIndex;
            ExpectedValue = expectedValue;
        }

        #if NET_FULL
        protected InvalidDataException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
            ReceivedData = (byte[]) info.GetValue(nameof(ReceivedData), typeof(byte[]));
            ErrorIndex = info.GetInt32(nameof(ErrorIndex));
            ExpectedValue = info.GetByte(nameof(ExpectedValue));
        }
        #endif

        private static string FormatMessage(string message, byte[] receivedData, int errorIndex, byte expectedValue)
        {
            if (errorIndex >= receivedData.Length)
                throw new ArgumentOutOfRangeException(nameof(errorIndex),
                    $"{nameof(errorIndex)}{errorIndex}在长度为{nameof(receivedData)}的范围之外 {receivedData.Length}.");

            return $"{message}收到的数据无效。在索引{errorIndex}处期望的'{expectedValue}', " +
                $"但收到{receivedData[errorIndex]}。参见{nameof(ReceivedData)}属性 " +
                "查看收到的完整消息.";
        }
    }
}