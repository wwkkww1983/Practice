using Caist.Framework.Entity;
using Caist.Framework.DataAccess;
using System.Text;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Caist.Framework.Business.ApplicationManage
{
    public class MqThemeBLL
    {
        public static async Task<List<MqThemeEntity>> GetList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"SELECT * FROM dbo.mk_mq_theme");

            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable =await conn.GetDataTableAsync(builder.ToString());
                return DataConvert.DataTableToList<MqThemeEntity>(dataTable).ToList();
            }
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<int> UpdateEntity(MqThemeEntity entity)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(@"update dbo.mk_mq_theme set last_update_time='{0}' where id={1}", entity.LastUpdateTime, entity.Id);

            using (var conn = Connect.GetConn("SQLServer"))
            {
                return await conn.ExcuteSQLAsync(builder.ToString());
            }
        }
    }
}
