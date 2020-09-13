using NLog;
using System;
using System.Configuration;
using System.IO;

namespace SyncUtil
{
    public class Common
    {
        //NLog recommends using a static variable for the logger object
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public static void InitLog(string path)
        {
            var config = new NLog.Config.LoggingConfiguration();

            var fileName = Path.Combine(path, $"{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
            // Targets where to log to: File and Console
            var logfile = new NLog.Targets.FileTarget("logfile")
            {
                FileName = fileName
            };
            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Info, LogLevel.Fatal, logfile);

            // Apply config           
            NLog.LogManager.Configuration = config;
        }
        public static string GetConfigValue(string key)
        {
            string str = string.Empty;
            if (!string.IsNullOrWhiteSpace(key))
            {
                str = ConfigurationManager.AppSettings[key];
            }
            return str;
        }
        public static void LogError(string msg)
        {
            try
            {
                logger.Info(msg);
                //string path = Path.Combine(logPath, DateTime.Now.ToString("yyyyMMdd") + ".txt");
                //string str = $"{ex.Message}。\r\n{ex.StackTrace}";
                //FileOperation.Log(path, $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]：{str}");
            }
            catch{}
        }

        public static void LogError(Exception ex)
        {
            try
            {
                string str = $"{ex.Message}。\r\n{ex.StackTrace}";
                logger.Error(ex, str);
            }
            catch{}
        }
    }

}
