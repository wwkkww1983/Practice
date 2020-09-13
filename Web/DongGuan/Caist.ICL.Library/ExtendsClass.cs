using System;

namespace Caist.ICL.Library
{
    public static class ExtendsClass
    {
        public static double ToDouble(this string str)
        {
            double res = 0;
            if (str.HasValue())
            {
                double.TryParse(str, out res);
            }
            return res;
        }
        public static decimal ToDecimal(this string str)
        {
            decimal res = 0;
            if (str.HasValue())
            {
                decimal.TryParse(str, out res);
            }
            return res;
        }
        public static int ToInt(this string str)
        {
            int res = 0;
            if (str.HasValue())
            {
                int.TryParse(str, out res);
            }
            return res;
        }
        public static float ToFloat(this string str)
        {
            float res = 0;
            if (str.HasValue())
            {
                float.TryParse(str, out res);
            }
            return res;
        }
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
