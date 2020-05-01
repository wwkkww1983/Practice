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
            sb.Append($"select ");
            sb.Append(string.Join(",", model.tableFields.Select(p => p.Split(',')).ToDictionary(k => k[0], v => v[1]).Keys));
            switch (model.sourceDBType)
            {
                case DataBaseType.SQLSERVER:
                    sb.Append($" from {model.sourceDB}.dbo.{model.sourceTable} ");
                    break;
                case DataBaseType.ORACLE://TODO:oracle 可能还得测试下
                case DataBaseType.MYSQL:
                    sb.Append($" from {model.sourceDB}.{model.sourceTable} ");
                    break;
                default:
                    break;
            }
            return sb.ToString();
        }

        public static string ToInsertSql(this DataBaseModel model)
        {
            StringBuilder sb = new StringBuilder();
            switch (model.targetDBType)
            {
                case DataBaseType.SQLSERVER:
                    sb.Append($"insert into {model.targetDB}.dbo.{model.targetTable}(");
                    break;
                case DataBaseType.ORACLE://TODO:oracle 可能还得测试下
                case DataBaseType.MYSQL:
                    sb.Append($"insert into {model.targetDB}.{model.targetTable}(");
                    break;
                default:
                    break;
            }
            sb.Append(string.Join(",", model.tableFields.Select(p => p.Split(',')).ToDictionary(k => k[0], v => v[1]).Values));
            sb.Append(@") values({0})");
            return sb.ToString();
        }
    }
}
