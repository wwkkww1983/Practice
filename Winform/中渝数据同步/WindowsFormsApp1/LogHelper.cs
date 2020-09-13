using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WindowsFormsApp1
{
    public  class LogHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errMsg">内容</param>
        /// <param name="logFileName">名称</param>
        public static void AddLog(string errMsg, string logFileName)
        {
            string logPath = "E:\\MineSys\\Logs\\";
            System.DateTime d = System.DateTime.Now;
            string filename = logPath + logFileName + d.Date.ToShortDateString().Replace("/","-") + ".txt";

            try
            {
                StreamWriter din;
                if (File.Exists(filename))
                    din = new StreamWriter(File.Open(filename, FileMode.Append), Encoding.GetEncoding(936));
                else
                    din = new StreamWriter(File.Open(filename, FileMode.Create), Encoding.GetEncoding(936));

                din.Write(errMsg + "\r\n");
                din.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(System.DateTime.Now.ToString() + "  " + e.Message + e.StackTrace + e.Source);
            }

        }
    }
}
