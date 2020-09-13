using Caist.Framework.DataAccess.Cache;
using Caist.Framework.Entity;
using Caist.Framework.Entity.Entity;
using Caist.Framework.IdGenerator;
using Caist.Framework.ThreadPool;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caist.Framework.DataAccess
{
    public class DataServices
    {
        public static async Task<bool> SaveSecurityMonitorData(List<SecurityMonitorEntity> recordEntity)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("truncate table security_monitor_ingreal_time_data_realTime;");
            foreach (var item in recordEntity)
            {
                builder.Append("INSERT INTO [dbo].[security_monitor_ingreal_time_data]([point],[Address],[dw],[ssz],[Updat_Date_Time])");
                builder.Append("VALUES");
                builder.Append($"('{item.Point}','{item.Address}','{string.Empty}','{item.SSZ}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}');");
                builder.Append("INSERT INTO [dbo].[security_monitor_ingreal_time_data_realTime]([point],[Address],[dw],[ssz])");
                builder.Append("VALUES");
                builder.Append($"('{item.Point}','{item.Address}','{string.Empty}','{item.SSZ}');");
            }
            using (var conn = Connect.GetConn("SQLServer"))
            {
                return await conn.ExcuteSQLAsync(builder.ToString()) > 0;
            }
        }
        public static async Task<bool> SaveAlarmData(AlarmRecordEntity recordEntity)
        {
            var idGeneratorHelper = IdGeneratorHelper.Instance;
            var id = idGeneratorHelper.GetId();
            StringBuilder builder = new StringBuilder();
            builder.Append("INSERT INTO [dbo].[mk_alarm_record]([id],[alarm_id],[alarm_time],[alarm_time_length],[alarm_reason],[alarm_value],[base_is_delete],[base_create_time])");
            builder.Append("VALUES");
            builder.Append($"({id},{recordEntity.alarm_id},'{recordEntity.alarm_time}','{recordEntity.alarm_time_length}','{recordEntity.alarm_reason}','{recordEntity.alarm_value}',0,'{DateTime.Now}')");
            using (var conn = Connect.GetConn("SQLServer"))
            {
                return await conn.ExcuteSQLAsync(builder.ToString()) > 0;
            }
        }

        /// <summary>
        /// 获取光纤测温的数据
        /// </summary>
        /// <returns></returns>
        public static async Task<List<FiberEntity>> GetFiberData()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(SyncCache.GetConfigSQL("FiberSql"));

            var strs = "FiberSourceType".GetConfigrationStr();
            var connStr = ConfigurationManager.ConnectionStrings["FiberSource"].ToString();
            using (var conn = Connect.GetConn(strs, connStr))
            {
                DataTable dataTable = await conn.GetDataTableAsync(builder.ToString());
                //builder.Clear();
                return DataConvert.DataTableToList<FiberEntity>(dataTable).ToList();
            }
        }

        /// <summary>
        /// 数据插入光纤测温历史表
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> InsertMk_Cable_Thermometry(List<FiberEntity> list)
        {
            bool flag = false;
            StringBuilder builder = new StringBuilder();
            if (list.HasValue())
            {
                foreach (var item in list)
                {
                    builder.Append($"INSERT INTO [dbo].[mk_cable_thermometry]([area_name],[max_value],[min_value],[average_value],[create_time])VALUES('{item.AreaName}','{item.MaxValue}','{item.MinValue}','{item.AverageValue}','{DateTime.Now}');");
                }
                using (var conn = Connect.GetConn("SQLServer"))
                {
                    flag = await conn.ExcuteSQLAsync(builder.ToString()) > 0;
                }
            }
            return flag;
        }

        /// <summary>
        /// 获取人员定位的数据
        /// </summary>
        /// <returns></returns>
        public static async Task<List<PepolePositionEntity>> GetPepolePostionData()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(SyncCache.GetConfigSQL("PeoplePositionSql"));

            var strs = "PeoplePositionSourceType".GetConfigrationStr();
            var connStr = ConfigurationManager.ConnectionStrings["PeoplePositionSource"].ToString();
            using (var conn = Connect.GetConn(strs, connStr))
            {
                DataTable dataTable = await conn.GetDataTableAsync(builder.ToString());
                builder.Clear();
                return DataConvert.DataTableToList<PepolePositionEntity>(dataTable).ToList();
            }
        }

        /// <summary>
        /// 数据插入人员定位历史表
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> InsertMk_People_Position(List<PepolePositionEntity> list)
        {
            bool flag = false;
            StringBuilder builder = new StringBuilder();
            if (list.HasValue())
            {
                foreach (var item in list)
                {
                    builder.Append($"INSERT INTO [caist_mk_db].[dbo].[mk_people_position]([pepole_number],[people_name],[type_of_work_name],[current_station],[station_address],[report_time],[post],[duty],[in_mine_time])VALUES({item.PepoleNumber},'{item.PepoleName}','{item.TypeOfWorkName}','{item.CurrentStation}','{item.StationAddress}','{DateTime.Now}','{item.Post}','{item.Duty}','{item.InMineTime}');");
                }
                using (var conn = Connect.GetConn("SQLServer"))
                {
                    flag = await conn.ExcuteSQLAsync(builder.ToString()) > 0;
                }
            }
            return flag;
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
        /// 保存plc数据到相应的历史表
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> SaveHistoryData(HistoryEntity history)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"INSERT INTO [dbo].{history.TabName} ([dict_Id],[dict_value],[instruct_type])VALUES('{history.DictId}','{history.DictValue}',{history.InstructType})");

            using (var conn = Connect.GetConn("SQLServer"))
            {
                return await conn.ExcuteSQLAsync(builder.ToString()) > 0;
            }
        }


        #region 数据加载
        public static DataTable GetGroupInfo(string ip, string port, Tuple<string, string> instruct)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"select a.id as groupID,i.id as instructId from mk_device d inner join mk_instruct_group a on d.id=a.device_id inner join mk_instruct i on a.id = i.instruct_group_id where d.Device_Host='{ip}' and d.Device_Port='{port}' and a.name = '{instruct.Item1}' and i.name ='{instruct.Item2}'");
            using (var conn = Connect.GetConn("SQLServer"))
            {
                return conn.GetDataTable(builder.ToString());
            }
        }
        public static DataTable GetSwitcCommands(InstructModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(@"select m.control_name,m.control_stutas,p.id,p.paramenter_name,p.paramenter_instruct_end,p.paramenter_instruct_start,
                                    p.paramenter_value_type,p.paramenter_instruct,p.paramenter_unit,p.paramenter_ip,p.paramenter_port
                                    from [dbo].[mk_view_paramenter] p inner join 
                                    [dbo].[mk_view_control_model] m on p.view_control_model_id = m.id
                                    where p.base_is_delete=0 and exists(
                                    select id from mk_view_function v where v.id=m.view_function_id and  exists(
                                    select id from mk_system_setting s where id={0} and s.id=v.system_setting_id));", model.SystemId);
            using (var conn = Connect.GetConn("SQLServer"))
            {
                return conn.GetDataTable(builder.ToString());
            }
        }
        public static DataTable GetSingleCommandValue(InstructModel model)
        {
            //var addrs = model.Instruct.Split('.');
            StringBuilder builder = new StringBuilder();
            //builder.AppendFormat(@"select i.id as instructId,g.id as groupID from [dbo].[mk_instruct] i inner join [dbo].[mk_instruct_group] g on i.instruct_group_id=g.id where 
            //                    i.name='{0}' and g.name ='{1}' and exists
            //                    (select id from [dbo].[mk_device] d where d.system_id={2} and d.Device_Host='{3}' 
            //                    and d.Device_Port='{4}' and d.id=g.device_id);", addrs[1], addrs[0], model.SystemId, model.Ip, model.Port);
            builder.AppendFormat(@"select  [id]
      ,[base_is_delete]
      ,[base_create_time]
      ,[base_modify_time]
      ,[base_creator_id]
      ,[base_modifier_id]
      ,[base_version]
      ,[view_control_model_id]
      ,[paramenter_name]
      ,[paramenter_instruct]
      ,[paramenter_instruct_start]
      ,[paramenter_instruct_end]
      ,[paramenter_unit]
      ,[paramenter_status]
      ,[paramenter_sort]
      ,[paramenter_ip]
      ,[paramenter_port]
      ,[paramenter_value_type]
      ,[paramenter_value]
      ,[Animation]
      ,[control_models] from mk_view_paramenter v where v.paramenter_ip='{0}' and v.paramenter_port={1} and paramenter_instruct='{2}';", model.Ip, model.Port, model.Instruct);
            using (var conn = Connect.GetConn("SQLServer"))
            {
                return conn.GetDataTable(builder.ToString());
            }
        }

        #region 加载PLC配置数据
        public static void LoadDataDevice(TreeNodeCollection treeNode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"SELECT  a.tab_name,a.id as Id, a.Device_Name as Name, a.Device_Host as Host, a.Device_Port as Port, a.Slot_No as CPU_SlotNO, a.PLCType as PLCType,a.system_id,
                            a.Local as LocalTASP,  a.Remote as RemoteTASP, a.parent_id as ParentId,a.tab_name as TabName  FROM mk_device a WHERE a.base_is_delete = 0 and tab_name is not null and tab_name <> ''
                            ;");//and a.Device_Host in ('192.168.200.53')
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                PublicEntity.DeviceEntities = DataConvert.DataTableToList<DeviceEntity>(dataTable).ToList();
            }
            PublicEntity.DeviceEntities.ForEach(d =>
            {
                TreeNode tree = treeNode.Add(string.Format("{0}-[{1}:{2}]", d.Name, d.Host, d.Port.ToString()));
                tree.ImageIndex = 0;
            });
        }

        public static void LoadDataTagGroup(string id = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select a.id as Id,a.device_id as DeviceId,a.name as Name,a.modular_type as MMType,a.read_count as ReadCount,a.begin_address as BeginAddress,
                             a.begin_block as Block from mk_instruct_group a where a.base_is_delete = 0 ");
            if (!string.IsNullOrEmpty(id))
            {
                builder.Append(" and a.device_id = '" + id + "' ");
            }
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                PublicEntity.TagGroupsEntities = DataConvert.DataTableToList<TagGroupsEntity>(dataTable).ToList();
            }
        }

        public static void LoadDataTag(string id = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select a.id as Id,a.Name as Name,a.instruct_group_id as TagGroup,a.address as Address,a.data_type as DataType,a.output as Output,a.remark as [Desc] 
                             from mk_instruct a where a.base_is_delete = 0;");
            if (!string.IsNullOrEmpty(id))
            {
                builder.Append(" and a.device_id = '" + id + "' ");
            }
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                PublicEntity.TagEntities = DataConvert.DataTableToList<TagEntity>(dataTable).ToList();
            }
        }
        #endregion


        #region 加载供配电参数配置
        public static void LoadOpcDataTag()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"SELECT [num] as Id
      ,[Name]
      ,Area
      ,[Data_Type] as DataType
      ,Address
  FROM [dbo].[mk_opc_power] where base_is_delete = 0 ");
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                PublicEntity.OPCPowerEntities = DataConvert.DataTableToList<OpcPowerEntity>(dataTable).ToList();
            }
        }
        #endregion

        /// <summary>
        /// 获取报警标准数据（系统手动设置的）
        /// </summary>
        /// <param name="id"></param>
        public static void LoadAlarmData(string id = null)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select e.id SysId,s.id as ModuleId,s.min_value,s.max_value,s.broadcast_content,m.manipulate_model_mark,m.manipulate_model_name,e.system_name
								from [mk_alarm_settings] s 
								inner join mk_view_manipulate_model m on s.view_manipulate_id=m.id
								left join mk_view_function d  on m.view_function_id = d.id
								left join mk_system_setting e on d.system_setting_id = e.id
								where  s.base_is_delete=0;");
            //if (!string.IsNullOrEmpty(id))
            //{
            //    builder.Append(" and a.device_id = '" + id + "' ");
            //}
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                PublicEntity.AlarmEntities = DataConvert.DataTableToList<AlarmEntity>(dataTable).ToList();
            }
        }

        /// <summary>
        /// 获取告警命令数据（点表点位设置的）
        /// </summary>
        /// <param name="id"></param>
        public static void LoadAlertData()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(@"select m.id as itemId,s.system_name,s.system_nick_name,f.view_name,m.manipulate_model_name,m.manipulate_model_mark as command,f.id,s.id as systemId
                                from [dbo].[mk_view_manipulate_model] m 
                                inner join [dbo].[mk_view_function] f on m.view_function_id = f.id
                                inner join [dbo].[mk_system_setting] s on f.system_setting_id= s.id 
                                where m.base_is_delete=0 and m.Instruct_view=2;");

            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                PublicEntity.AlertEntities = DataConvert.DataTableToList<AlertEntity>(dataTable).ToList();
            }
        }
        /// <summary>
        /// 获取报警标准数据
        /// </summary>
        /// <param name="id"></param>
        public static void LoadAllAddress(string addr = "")
        {
            string str = string.Empty;
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(addr))
            {
                str = $" and addr = '{addr}'";
            }
            builder.AppendFormat(@"select Device_Name,Device_Host,Device_Port,addr from (
                            select d.Device_Name,d.Device_Host,d.Device_Port,g.name+'.'+i.name as addr  from  [dbo].[mk_device] d inner join 
                            [dbo].[mk_instruct_group] g  on d.id=g.device_id
                            inner join [dbo].[mk_instruct] i on g.id=i.instruct_group_id where i.base_is_delete=0
                            )res where 1=1 {0}
                            group by Device_Name,Device_Host,Device_Port,addr ", str);
            using (var conn = Connect.GetConn("SQLServer"))
            {
                DataTable dataTable = conn.GetDataTable(builder.ToString());
                PublicEntity.InstructAddrs = DataConvert.DataTableToList<InstructAddrs>(dataTable).ToList();
            }
        }
        #endregion

        public static bool ExcuteSql(string sql)
        {
            using (var conn = Connect.GetConn("SQLServer"))
            {
                return conn.ExcuteSQL(sql) > 0;
            }
        }

        public static bool SaveFiberData(List<FiberEntity> list)
        {
            bool flag = false;
            if (list.HasValue())
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("truncate table [dbo].[mk_cable_thermometry_realTime];");
                foreach (var item in list)
                {
                    sb.AppendLine($"INSERT INTO [dbo].[mk_cable_thermometry]([area_name],[max_value],[min_value],[average_value],[create_time])VALUES('{item.AreaName}','{item.MaxValue}','{item.MinValue}','{item.AverageValue}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}');");
                    sb.AppendLine($"INSERT INTO [dbo].[mk_cable_thermometry_realTime]([area_name],[max_value],[min_value],[average_value])VALUES('{item.AreaName}','{item.MaxValue}','{item.MinValue}','{item.AverageValue}');");
                }
                flag = ExcuteSql(sb.ToString());
            }
            return flag;
        }
    }
}
