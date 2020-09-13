using Caist.Framework.Entity;
using Caist.Framework.DataAccess;
using System.Text;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Caist.Framework.Entity.Entity;

namespace Caist.Framework.Business.ApplicationManage
{
    public class VideoBLL
    {
        public static async Task<List<VideoEntity>> GetList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"SELECT * FROM dbo.mk_eb_video");

            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable =await conn.GetDataTableAsync(builder.ToString());
                return DataConvert.DataTableToList<VideoEntity>(dataTable).ToList();
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task<int> UpdateEntity(VideoEntity entity)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(@"update dbo.mk_eb_video set Url='{0}' where gid='{1}'", entity.Url, entity.Gid);

            using (var conn = Connect.GetConn("SQLServer"))
            {
                return await conn.ExcuteSQLAsync(builder.ToString());
            }
        }

        /// <summary>
        /// 根据设备编号获取数据
        /// </summary>
        /// <param name="Gid"></param>
        /// <returns></returns>
        public static async Task<VideoEntity> getEntity(string Gid)
        {

            StringBuilder builder = new StringBuilder();
            builder.Append($"SELECT top(1)* FROM dbo.mk_eb_video WHERE gid='{Gid}'");

            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = await conn.GetDataTableAsync(builder.ToString());
                return DataConvert.DataTableToList<VideoEntity>(dataTable).FirstOrDefault();
            }
        }
    }
}
