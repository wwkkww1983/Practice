using System;
using System.ComponentModel;
using System.Reflection;

namespace Caist.Framework.Entity.Enum
{
    public static partial class Extensions
    {
        /// <summary>
        /// 获取枚举值对应的描述
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static string GetDescription(this System.Enum enumType)
        {
            FieldInfo EnumInfo = enumType.GetType().GetField(enumType.ToString());
            if (EnumInfo != null)
            {
                DescriptionAttribute[] EnumAttributes = (DescriptionAttribute[])EnumInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (EnumAttributes.Length > 0)
                {
                    return EnumAttributes[0].Description;
                }
            }
            return enumType.ToString();
        }

        /// <summary>
        /// 将object转换为int，若转换失败，则返回0。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ParseToInt(this object str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 转换数据类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ConvertDataEnum(int val)
        {
            string str = string.Empty;
            if (val == InstructEnum.BOOL.ParseToInt())
                str = InstructEnum.BOOL.GetDescription();
            else if (val == InstructEnum.BYTE.ParseToInt())
                str = InstructEnum.BYTE.GetDescription();
            else if (val == InstructEnum.SHORT.ParseToInt())
                str = InstructEnum.SHORT.GetDescription();
            else if (val == InstructEnum.INT.ParseToInt())
                str = InstructEnum.INT.GetDescription();
            else if (val == InstructEnum.FLOAT.ParseToInt())
                str = InstructEnum.FLOAT.GetDescription();
            return str;
        }

        /// <summary>
        /// 转换数据类型
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ConvertOutPutEnum(int val)
        {
            string str = string.Empty;
            if (val == InstructParamEnum.OUT.ParseToInt())
                str = InstructParamEnum.OUT.GetDescription();
            else
                str = InstructParamEnum.PUT.GetDescription();
            return str;
        }
    }
}