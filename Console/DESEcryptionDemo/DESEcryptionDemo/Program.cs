using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DESEcryptionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "hello world";
            string str = SecretUtilTools.AESEncrypt(s, "tsj0241");
            str = ModifyStr(str);
            Console.WriteLine(str);
            Console.ReadLine();
        }

        private static string ModifyStr(string str)
        {
            StringBuilder tempStr = new StringBuilder();
            if (str.Length > 8)
            {
                tempStr.Append(str.Substring(0, 8));
            }
            else
            {
                tempStr.Append(str);
                for (int i = 0; i < 8 - str.Length; i++)
                {
                    tempStr.Append("0");
                }
            }
            return tempStr.ToString();
        }
    }

    public class SecretUtilTools
    {
        /// <summary>  
        /// AES encrypt ( no IV)  
        /// </summary>  
        /// <param name="data">Raw data</param>  
        /// <param name="key">Key, requires 32 bits</param>  
        /// <returns>Encrypted string</returns>  
        public static string AESEncrypt(string data, string key)
        {

            using (MemoryStream Memory = new MemoryStream())
            {
                using (Aes aes = Aes.Create())
                {
                    byte[] plainBytes = Encoding.UTF8.GetBytes(data);
                    byte[] bKey = new byte[32];
                    Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);

                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.KeySize = 256;
                    aes.Key = bKey;

                    using (CryptoStream cryptoStream = new CryptoStream(Memory, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        try
                        {
                            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                            cryptoStream.FlushFinalBlock();
                            return ByteToHexStr(Memory.ToArray());
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }
                    }
                }
            }
        }

        /// <summary>  
        /// AES decrypt( no IV)  
        /// </summary>  
        /// <param name="data">Encrypted data</param>  
        /// <param name="key">Key, requires 32 bits</param>  
        /// <returns>Decrypted string</returns>  
        public static string AESDecrypt(string data, string key)
        {

            byte[] encryptedBytes = Convert.FromBase64String(data);
            byte[] bKey = new byte[32];
            Array.Copy(Encoding.UTF8.GetBytes(key.PadRight(bKey.Length)), bKey, bKey.Length);

            using (MemoryStream Memory = new MemoryStream(encryptedBytes))
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.PKCS7;
                    aes.KeySize = 256;
                    aes.Key = bKey;

                    using (CryptoStream cryptoStream = new CryptoStream(Memory, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        try
                        {
                            byte[] tmp = new byte[encryptedBytes.Length];
                            int len = cryptoStream.Read(tmp, 0, encryptedBytes.Length);
                            byte[] ret = new byte[len];
                            Array.Copy(tmp, 0, ret, 0, len);

                            return Encoding.UTF8.GetString(ret, 0, len);
                        }
                        catch (Exception ex)
                        {
                            return null;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHexStr(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("X2"));
                }
            }
            return sb.ToString();
        }

    }
}
