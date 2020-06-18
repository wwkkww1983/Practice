using Caist.Framework.IdGenerator;
using SyncModel;
using System.Data;
using System.Linq;
using System.Text;

namespace SyncUtil
{
    public static class CommonExtends
    {
        public static bool HasValue(this string value)
        {
            return value != null && !string.IsNullOrEmpty(value);
        }
        public static bool HasData(this DataTable dt)
        {
            return dt != null && dt.Rows.Count > 0;
        }

        //public static string ToSelectSql(this DataBaseModel model)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append($"select (placeholder)");
        //    if (model.TableFields != null && model.TableFields.Length > 0)
        //    {
        //        string tempStr = "," + string.Join(",", model.TableFields.Select(p => p.Split(',')).ToDictionary(k => k[0], v => v[1]).Keys);
        //        sb.Append(tempStr);
        //    }
        //    switch (model.SourceDBType)
        //    {
        //        case DataEmun.SQLServer:
        //            sb.Append($" from {model.SourceDB}.dbo.{model.SourceTable} ");
        //            break;
        //        case DataEmun.Oracle://TODO:oracle 可能还得测试下
        //        case DataEmun.MySQL:
        //            sb.Append($" from {model.SourceDB}.{model.SourceTable} ");
        //            break;
        //        default:
        //            break;
        //    }
        //    return sb.ToString();
        //}

        //public static string ToInsertSql(this DataBaseModel model)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    switch (model.TargetDBType)
        //    {
        //        case DataEmun.SQLServer:
        //            sb.Append($"insert into {model.TargetDB}.dbo.{model.TargetTable}( (placeholder)");
        //            break;
        //        case DataEmun.Oracle://TODO:oracle 可能还得测试下
        //        case DataEmun.MySQL:
        //            sb.Append($"insert into {model.TargetDB}.{model.TargetTable}((placeholder)");
        //            break;
        //        default:
        //            break;
        //    }

        //    if (model.TableFields != null && model.TableFields.Length > 0)
        //    {
        //        string tempStr = "," + string.Join(",", model.TableFields.Select(p => p.Split(',')).ToDictionary(k => k[0], v => v[1]).Values);
        //        sb.Append(tempStr);
        //    }
        //    sb.Append(@") values({0})");
        //    return sb.ToString();
        //}
    }
}
