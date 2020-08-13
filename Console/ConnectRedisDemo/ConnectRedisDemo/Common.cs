using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace ConnectRedisDemo
{
    public static class CommonExtends
    {
        public static string ToHash(this string rawText)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawText));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static string GetConfigValue(this string key)
        {
            var str = ConfigurationManager.AppSettings[key];
            return string.IsNullOrWhiteSpace(str) ? string.Empty : str;
        }
        public static bool HasValue(this DataTable dt)
        {
            return dt != null && dt.Rows.Count > 0;
        }
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
        public static string GetFileName(this string str)
        {
            var p = Path.GetFileName(str);
            return p.Substring(0, p.Length - 4);
        }
    }

    public class Common
    {
        public static List<string> ReadText(string path)
        {
            StreamReader sr = null;
            List<string> list = new List<string>();
            try
            {
                sr = new StreamReader(path, Encoding.UTF8);
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sr.Close();
            }
            return list;
        }
    }
}
