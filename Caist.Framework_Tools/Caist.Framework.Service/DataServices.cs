using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.Entity.Entity;
using Caist.Framework.IdGenerator;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service
{
    public class DataServices
    {
        //public async Task<bool> SavePLCData()
        //{

        //}
        public static async Task<bool> SaveAlarmData(AlarmRecordEntity recordEntity)
        {
            var idGeneratorHelper = IdGeneratorHelper.Instance;
            var id = idGeneratorHelper.GetId();
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO [dbo].[mk_alarm_record]([id],[alarm_id],[alarm_time],[alarm_time_length],[alarm_reason])");
            builder.Append("VALUES");
            builder.Append($"({id},{recordEntity.alarm_id},'{recordEntity.alarm_time}','{recordEntity.alarm_time_length}','{recordEntity.alarm_reason}')");
            using (var conn = Connect.GetConn("SQLServer"))
            {
                return await conn.InsertAsync(builder.ToString()) > 0;
            }
        }

        /// <summary>
        /// 获取光纤测温的数据
        /// </summary>
        /// <returns></returns>
        public static async Task<List<FiberEntity>> GetFiberData()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"SELECT id
                              ,area_name
                              ,max_value
                              ,max_value_pos
                              ,min_value
                              ,min_value_pos
                              ,average_value
                          FROM dbo.mk_cable_thermometry");

            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = await conn.GetDataTableAsync(builder.ToString());
                return DataConvert.DataTableToList<FiberEntity>(dataTable).ToList();
            }
        }

        /// <summary>
        /// 获取供电站的数据
        /// </summary>
        /// <returns></returns>
        public static async Task<List<SubStationEntity>> GetSubStationData()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"SELECT Id ,Sys_Id,Dian_Wei,F,IA,P,Q,COS
                          FROM dbo.mk_substation");

            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = await conn.GetDataTableAsync(builder.ToString());
                return DataConvert.DataTableToList<SubStationEntity>(dataTable).ToList();
            }
        }
    }
}
