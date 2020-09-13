using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Caist.Framework.PLC.Siemens.CheckType
{
	internal class FuckProtect
    {
		private static Assembly _Assembly; //表示一个程序集，它是一个可重用
		private static int[] _IntArray;
		private static IntPtr _IntPtr;
		internal static Hashtable _Hashtable;
		private static byte[] _Bytes;
		private static IntPtr _IntPtrBefore;
		private static IntPtr _IntPtrAfter;
		private static SortedList _SortedList;
		private static string[] _StringArray;

		static FuckProtect()
		{
			_Assembly = typeof(FuckProtect).Assembly;
			_Bytes = new byte[0];
			_IntPtrBefore = IntPtr.Zero;
			_IntPtrAfter = IntPtr.Zero;
			_StringArray = new string[0];
			_IntArray = new int[0];
			_SortedList = new SortedList(); //键/值对 集合
			FuckProtect._IntPtr = IntPtr.Zero;
			FuckProtect._Hashtable = new Hashtable();
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
			if (FuckProtect._Bytes.Length == 0)
			{
				BinaryReader binaryReader = new BinaryReader(_Assembly.GetManifestResourceStream("Caist.Framework.PLC.db"));
				binaryReader.BaseStream.Position = 0L; // 流文件初始位置
				byte[] array = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
				binaryReader.Close();

				FuckProtect._Bytes = array;
			}
			int count = BitConverter.ToInt32(FuckProtect._Bytes, Startindexs);//由四个字节构成、从 startIndex 开始的 32 位有符号整数
			try
			{
				return Encoding.Unicode.GetString(FuckProtect._Bytes, Startindexs + 4, count);//indexs 第一个 count 数量
			}
			catch
			{
			}
			return "";
		}
	}
}
