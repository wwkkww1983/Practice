using System;

internal class StrTobyte
{
    ///<param >将字符串转化为byte数组</param>
    ///<summary >空格分隔字符串、转换16进制</summary>
    public static byte[] StrToArraybyte(string str)//字符串转换数组
	{
		string[] array = str.Split(new char[]{' '});//以空格分割字符串 并生成数组
		int num = array.Length;
		byte[] array2 = new byte[num];
		for (int i = 0; i < num; i++)
		{
			array2[i] = Convert.ToByte(FuckProtect.DataFrom(0) + array[i], 16); //8 位无符号、16进制转换
        }
		return array2;
	}

}
