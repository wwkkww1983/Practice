using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Caist.Framework.Plc
{
	internal class FuckProtect
    {
		//此程序块为加密程序所在、现在已全部阉割
		private static Assembly assembly; //表示一个程序集，它是一个可重用
		private static int[] int_0;
		private static IntPtr intptr_0;
		internal static Hashtable hashtable_0;
		private static byte[] bytes;
		private static IntPtr intptr_1;
		private static IntPtr intptr_2;
		private static SortedList sortedList;
		private static string[] string_0;
		static FuckProtect()//构造函数
		{
			assembly = typeof(FuckProtect).Assembly;
			bytes = new byte[0];
			intptr_1 = IntPtr.Zero;
			intptr_2 = IntPtr.Zero;
			string_0 = new string[0];
			int_0 = new int[0];
			sortedList = new SortedList(); //键/值对 集合
			FuckProtect.intptr_0 = IntPtr.Zero;
			FuckProtect.hashtable_0 = new Hashtable();
			try
			{
				RSACryptoServiceProvider.UseMachineKeyStore = true;
			}
			catch
			{
			}
		}
		internal static string DataFrom(int Startindexs)//读取数据文件
		{
			if (FuckProtect.bytes.Length == 0)
			{
				BinaryReader binaryReader = new BinaryReader(assembly.GetManifestResourceStream("PLCComHelperProj.db"));
				binaryReader.BaseStream.Position = 0L; // 流文件初始位置
				byte[] array = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
				binaryReader.Close();

				FuckProtect.bytes = array;
			}
			int count = BitConverter.ToInt32(FuckProtect.bytes, Startindexs);//由四个字节构成、从 startIndex 开始的 32 位有符号整数
			try
			{
				return Encoding.Unicode.GetString(FuckProtect.bytes, Startindexs + 4, count);//indexs 第一个 count 数量
			}
			catch
			{
			}
			return "";
		}
	}
}
