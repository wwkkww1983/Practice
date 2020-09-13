using System;

namespace Caist.Framework.PLC.Siemens.CheckType
{
	internal class StrTobyte
    {
		/// <summary>
		/// 空格分隔字符串、转换16进制
		/// </summary>
		/// <param name="str">将字符串转化为byte数组</param>
		/// <returns></returns>
		public static byte[] StrToArraybyte(string str)//字符串转换数组
		{
			string[] StringArray = str.Split(new char[] { ' ' });//以空格分割字符串 并生成数组
			int num = StringArray.Length;
			byte[] ByteArray = new byte[num];
			for (int i = 0; i < num; i++)
			{

                if (Convert.ToInt32(StringArray[i],16) > 255)
                {
					var Area = (byte)(Convert.ToInt32(StringArray[i], 16) % 256);
					ByteArray[i] = Area;

					ByteArray[i + 1] = (byte)(Area == 0x84 ? (ushort)(Convert.ToInt32(StringArray[i], 16) / 256) : (ushort)0);
					i++;
                }
                else
                {
					ByteArray[i] = Convert.ToByte(FuckProtect.DataFrom(0) + StringArray[i], 16); //8 位无符号、16进制转换
				}

			
			}
			return ByteArray;
		}
	}
}
