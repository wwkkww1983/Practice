using System.IO;

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
                    sr = new StreamReader(path);
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

        /// <summary>
        /// 写入文本文档
        /// </summary>
        /// <param name="path"></param>
        /// <param name="text"></param>
        /// <param name="falg">是否覆盖（默认是）</param>
        /// <returns></returns>
        public static string WriteText(string path, string text, bool flag = true)
        {
            string str = "保存失败！";
            StreamWriter sw = null;
            try
            {
                if (File.Exists(path))
                {
                    sw = new StreamWriter(path);
                }
                else
                {
                    sw = new StreamWriter(File.Create(path));
                }
                sw.Write(text);
                return "保存成功！";

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
    }
}
