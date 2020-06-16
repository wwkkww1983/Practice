using Caist.Framework.DataAccess;
using Caist.Framework.Entity;
using Caist.Framework.Entity.Entity;
using Caist.Framework.IdGenerator;
using Caist.Framework.ThreadPool;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
                builder.Clear();
                return DataConvert.DataTableToList<FiberEntity>(dataTable).ToList();
            }
        }

        /// <summary>
        /// 获取人员定位的数据
        /// </summary>
        /// <returns></returns>
        public static async Task<List<PepolePostionEntity>> GetPepolePostionData()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(@"select count(people_id)as Nums,Current_Station, Station_Address 
                         from
                         (
                         select  people_id, position_id as Current_Station, [position_desc] as Station_Address from mk_people_position p
                        where convert(varchar(10),report_time,23) = '{0}'
                         group by position_id,position_desc,people_id,people_name
                         )res
                         group by Current_Station, Station_Address order by Current_Station;", DateTime.Now.ToString("yyyy-MM-dd"));

            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = await conn.GetDataTableAsync(builder.ToString());
                builder.Clear();
                return DataConvert.DataTableToList<PepolePostionEntity>(dataTable).ToList();
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
                builder.Clear();
                return DataConvert.DataTableToList<SubStationEntity>(dataTable).ToList();
            }
        }


        /// <summary>
        /// 获取供电站的数据
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SaveHistoryData(HistoryEntity history)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"INSERT INTO [dbo].{history.TabName} ([dict_Id],[dict_value])VALUES('{history.DictId}','{history.DictValue}')");

            using (var conn = Connect.GetConn("SQLServer"))
            {
                return await conn.ExcuteSQLAsync(builder.ToString()) > 0;
            }
        }
    }
}
