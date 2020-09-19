using Caist.Framework.Entity;
using Caist.Framework.DataAccess;
using System.Text;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace Caist.Framework.Business.ApplicationManage
{
    public class MqttCodeSettingBLL
    {
        /// <summary>
        /// 获取要上传的所有点位配置信息
        /// </summary>
        /// <returns></returns>
        public static async Task<List<MqtSettingEntity>> GetList()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select DISTINCT(mk_mqtt_code_setting.id),mk_mqtt_code_setting.*,mk_device.tab_name 
from mk_mqtt_code_setting left join mk_device 
on mk_mqtt_code_setting.system_id = mk_device.system_id
                            ");
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                return DataConvert.DataTableToList<MqtSettingEntity>(dataTable).ToList();
            }
        }

        public static async Task<List<MqtPlcDataEntity>> GetUpLoadDataList(string TableName,DateTime lasTime)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select " + TableName + ".* from " + TableName+ " right join (SELECT dict_Id,MAX (id) id FROM " + TableName+
                " where create_time = '" + lasTime.ToString("yyy-MM-dd HH:mm") + "'" + //存储的是每分钟一组数据，所以只取分钟
                " and dict_Value<>'' " +
                " GROUP by dict_Id) a on a.id =" + TableName + ".id ");
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                return DataConvert.DataTableToList<MqtPlcDataEntity>(dataTable).ToList();
            }
        }
    }
}
