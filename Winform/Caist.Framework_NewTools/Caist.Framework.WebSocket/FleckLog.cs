using System;

namespace Caist.Framework.WebSocket
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error
    }

    public class WebSocketLog
    {
        public static LogLevel Level = LogLevel.Info;

        public static Action<LogLevel, string, Exception> LogAction = (level, message, ex) =>
        {
            if (level >= Level)
                Console.WriteLine("{0} [{1}] {2} {3}", DateTime.Now, level, message, ex);
        };

        public static void Warn(string message, Exception ex = null)
        {
            LogAction(LogLevel.Warn, message, ex);
        }

        public static void Error(string message, Exception ex = null)
        {
            LogAction(LogLevel.Error, message, ex);
        }

        public static void Debug(string message, Exception ex = null)
        {
            LogAction(LogLevel.Debug, message, ex);
        }

        public static void Info(string message, Exception ex = null)
        {
            LogAction(LogLevel.Info, message, ex);
        }

    }
}
