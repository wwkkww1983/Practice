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

        #region 转换为float
        /// <summary>
        /// 将object转换为float，若转换失败，则返回0。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ParseToFloat(this object str)
        {
            try
            {
                return float.Parse(str.ToString());
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 将object转换为float，若转换失败，则返回指定值。不抛出异常。  
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ParseToFloat(this object str, float result)
        {
            try
            {
                return float.Parse(str.ToString());
            }
            catch
            {
                return result;
            }
        }
        public static double ParseTodouble(this object str)
        {
            try
            {
                return float.Parse(str.ToString());
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 科学计算类型转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double ChangeDataToD(this object str)
        {
            decimal dData = 0.0M;
            try
            {
                if (str.ToString().Contains("E") || str.ToString().Contains("e"))
                {
                    str = str.ToString().Substring(0, str.ToString().Length - 1).Trim();
                    dData = Convert.ToDecimal(Decimal.Parse(str.ToString(), System.Globalization.NumberStyles.Float));
                }
                else
                {
                    dData = decimal.Parse(str.ToString());
                }
            }
            catch (Exception)
            {
                dData = 0;
            }
            return double.Parse(dData.ToString());
        }
        #endregion

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