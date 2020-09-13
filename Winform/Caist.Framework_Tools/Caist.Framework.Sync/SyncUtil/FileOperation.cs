using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SyncUtil
{
    public class FileOperation
    {
        public static string ReadText(string path)
        {
            string str = string.Empty;
            StreamReader sr = null;
            try
            {
                if (File.Exists(path))
                {
                    sr = new StreamReader(path, Encoding.UTF8);
                    str = sr.ReadToEnd();
                }

            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
            return str;
        }
        public static List<string> ReadTextList(string path)
        {
            List<string> listStr = new List<string>();
            StreamReader sr = null;
            try
            {
                if (File.Exists(path))
                {
                    sr = new StreamReader(path, Encoding.UTF8);
                    while (!sr.EndOfStream)
                    {
                        listStr.Add(sr.ReadLine());
                    }
                }

            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
            return listStr;
        }

        /// <summary>
        /// 写入文本文档
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        /// <param name="append">是否追加（默认是）</param>
        /// <param name="wrap">是否增加换行（默认否）</param>
        /// <returns></returns>
        public static void WriteText(string path, string text, bool append = true, bool wrap = false)
        {
            StreamWriter sw = null;
            try
            {
                if (path.IndexOf(":") > -1)
                {
                    var p = path.Substring(0, path.LastIndexOf("\\"));
                    if (!Directory.Exists(p))
                    {
                        Directory.CreateDirectory(p);
                    }
                }
                if (File.Exists(path))
                {
                    sw = new StreamWriter(path, append, Encoding.UTF8);
                }
                else
                {
                    var direc = path.Substring(0, path.LastIndexOf("\\"));
                    if (!Directory.Exists(direc))
                    {
                        Directory.CreateDirectory(direc);
                    }
                    sw = new StreamWriter(File.Create(path));
                }
                sw.Write(text);
                sw.Flush();
                if (wrap)
                {
                    sw.WriteLine("\r\n");
                }

            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
            //return str;
        }


        /// <summary>
        /// 写入文本文档
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        /// <param name="append">是否追加（默认是）</param>
        /// <param name="wrap">是否增加换行（默认否）</param>
        /// <returns></returns>
        public static void Log(string path, string msg)
        {
            StreamWriter sw = null;
            try
            {
                if (path.IndexOf(":") > -1)
                {
                    var p = path.Substring(0, path.LastIndexOf("\\"));
                    if (!Directory.Exists(p))
                    {
                        Directory.CreateDirectory(p);
                    }
                }
                if (File.Exists(path))
                {
                    sw = new StreamWriter(path, true, Encoding.UTF8);
                }
                else
                {
                    var direc = path.Substring(0, path.LastIndexOf("\\"));
                    if (!Directory.Exists(direc))
                    {
                        Directory.CreateDirectory(direc);
                    }
                    sw = new StreamWriter(File.Create(path));
                }
                sw.Write(msg);
                sw.Flush();
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }
    }
}
