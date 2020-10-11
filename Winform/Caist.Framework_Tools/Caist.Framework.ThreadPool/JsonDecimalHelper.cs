using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Caist.Framework.ThreadPool
{
    public class JsonDecimalHelper : JsonConverter
    {

        private void dumpNumArray<T>(JsonWriter writer, T n)
        {

            var s = n.ToString();
            //if (s.EndsWith(".0"))
            //    writer.WriteRawValue(s.Substring(0, s.Length - 2));
            //else if (s.Contains("."))
            //    writer.WriteRawValue(s.TrimEnd('0').TrimEnd('.'));
            //else
            //    writer.WriteRawValue(s);
            writer.WriteRawValue(s); //避免数字类型在转换json的时候会改变小数位数。直接返回原有的字符串格式即可
        }

        public override void WriteJson(JsonWriter writer, object value,
           JsonSerializer serializer)
        {
            Type t = value.GetType();
            if (t == dblArrayType)
                dumpNumArray<double>(writer, (double)value);
            else if (t == decArrayType)
                dumpNumArray<decimal>(writer, (decimal)value);
            else if (t == flArrayType)
                dumpNumArray<float>(writer, (float)value);
            else
                throw new NotImplementedException();
        }
        private Type flArrayType = typeof(float);
        private Type dblArrayType = typeof(double);
        private Type decArrayType = typeof(decimal);

        public override bool CanConvert(Type objectType)
        {
            if (objectType == dblArrayType || objectType == decArrayType || objectType == flArrayType)
                return true;
            return false;
        }

        public override bool CanRead
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType,
            object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }


    }
}
