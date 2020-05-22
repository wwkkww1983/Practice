using SyncCommon;
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

        public static string ToSelectSql(this DataBaseModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"select (placeholder)");
            sb.Append(string.Join(",", model.TableFields.Select(p => p.Split(',')).ToDictionary(k => k[0], v => v[1]).Keys));
            switch (model.SourceDBType)
            {
                case DataBaseType.SQLSERVER:
                    sb.Append($" from {model.SourceDB}.dbo.{model.SourceTable} ");
                    break;
                case DataBaseType.ORACLE://TODO:oracle 可能还得测试下
                case DataBaseType.MYSQL:
                    sb.Append($" from {model.SourceDB}.{model.SourceTable} ");
                    break;
                default:
                    break;
            }
            return sb.ToString();
        }

        public static string ToInsertSql(this DataBaseModel model)
        {
            StringBuilder sb = new StringBuilder();
            switch (model.TargetDBType)
            {
                case DataBaseType.SQLSERVER:
                    sb.Append($"insert into {model.TargetDB}.dbo.{model.TargetTable}( (placeholder)");
                    break;
                case DataBaseType.ORACLE://TODO:oracle 可能还得测试下
                case DataBaseType.MYSQL:
                    sb.Append($"insert into {model.TargetDB}.{model.TargetTable}((placeholder)");
                    break;
                default:
                    break;
            }
            sb.Append(string.Join(",", model.TableFields.Select(p => p.Split(',')).ToDictionary(k => k[0], v => v[1]).Values));
            sb.Append(@") values({0})");
            return sb.ToString();
        }
    }
}
