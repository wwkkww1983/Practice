using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Caist.Framework.Util
{
    /// <summary>
    /// 共享资源操作类
    /// </summary>
    public static class FileShareHelper
    {
        public static bool connectState()
        {
            return connectState(@""+GlobalContext.SystemConfig.FTPServer, GlobalContext.SystemConfig.FTPUser, GlobalContext.SystemConfig.FTPPwd);
        }

        public static bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = @"net use " + path + " /User:" + userName + " " + passWord + " /PERSISTENT:YES";
                if (string.IsNullOrEmpty(userName))  //如果共享磁盘路径不需要用户名密码
                {
                    dosLine = @"net use " + path + " /PERSISTENT:YES";
                }
                LogHelper.Write(dosLine);
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                    LogHelper.Write("链接成功");
                }
                else
                {
                    LogHelper.Write(errormsg);
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }


        //read file
        public static void ReadFiles(string path)
        {
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader(path))
                {
                    String line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);

                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }

        //write file
        public static bool WriteFiles(string filename,string Content)
        {
           
            try
            {
                // Create an instance of StreamWriter to write text to a file.
                // The using statement also closes the StreamWriter.
                using (StreamWriter sw = new StreamWriter(GlobalContext.SystemConfig.FTPServer + "\\"+ filename))
                {
                    // Add some text to the file.
                    sw.Write(Content);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.Write(ex);
                return false;
            }
        }



        /// <summary>
        /// 将本地文件上传到远程服务器共享目录
        /// </summary>
        /// <param name="src">本地文件的绝对路径，包含扩展名</param>
        /// <param name="dst">远程服务器共享文件路径，不包含文件扩展名</param>
        /// <param name="fileName">上传到远程服务器后的文件扩展名</param>
        public static void TransportLocalToRemote(string src, string dst, string fileName) 
        {
            FileStream inFileStream = new FileStream(src, FileMode.Open);    //此处假定本地文件存在，不然程序会报错   

            if (!Directory.Exists(dst))        //判断上传到的远程服务器路径是否存在
            {
                Directory.CreateDirectory(dst);
            }
            dst = dst + fileName;             //上传到远程服务器共享文件夹后文件的绝对路径

            FileStream outFileStream = new FileStream(dst, FileMode.OpenOrCreate);

            byte[] buf = new byte[inFileStream.Length];

            int byteCount;

            while ((byteCount = inFileStream.Read(buf, 0, buf.Length)) > 0)
            {

                outFileStream.Write(buf, 0, byteCount);

            }

            inFileStream.Flush();
            inFileStream.Close();
            outFileStream.Flush();
            outFileStream.Close();
        }
    }
}
