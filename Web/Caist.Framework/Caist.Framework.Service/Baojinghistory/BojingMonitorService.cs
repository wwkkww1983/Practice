using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity;
using Caist.Framework.Entity.AlarmManage;
using Caist.Framework.Entity.Baojinghistory;
using Caist.Framework.Enum;
using Caist.Framework.Model.Param.Baojinghistory;
using Caist.Framework.Model.Param.SystemManage;
using Caist.Framework.Service.SystemManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.Baojinghistory
{
    public class BojingMonitorService : RepositoryFactory
    {
        #region service
        private FileService fileService = new FileService();
        #endregion
        #region 获取数据
        public async Task<List<BojingMonitorEntity>> GetSecurityInfoList(BojingMonitorParam param)
        {
            var expression = ListFilter(param);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select count(tongji.system_name) as Value,tongji.system_name as Name from 
           (select mk_alarm_record.alarm_id,mk_alarm_record.alarm_reason,mk_alarm_settings.system_models,
           mk_alarm_settings.broadcast_content,mk_system_setting.system_name from mk_alarm_record left join mk_alarm_settings
           on mk_alarm_record.alarm_id = mk_alarm_settings.id 
           left join  mk_system_setting on mk_alarm_settings.system_models =  mk_system_setting.id) tongji group by tongji.system_name");

            var list = await this.BaseRepository().FindList<BojingMonitorEntity>(strSql.ToString());

            return list.OrderBy(p => p.Value).ToList();
        }

        /// <summary>
        /// 报警预案详情
        /// </summary>
        /// <param name="AlarmField">mk_alarm_plan.alarm_field</param>
        /// <returns></returns>
        public async Task<AlarmPlanInfoEntity> GetAlarmPlanInfo(long? AlarmField)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select mk_alarm_plan.id,mk_alarm_plan.alarm_name as AlarmName,mk_alarm_plan.sys_id as SysId,
                mk_alarm_plan.remark as Remark,mk_system_setting.system_nick_name as SystemNickName,
                mk_alarm_plan.alarm_field as AlarmField,mk_alarm_plan.enable as Enable,mk_files.file_name as FileName,mk_files.file_path as SystemImage
                from mk_alarm_plan left join mk_system_setting on mk_system_setting.id = mk_alarm_plan.sys_id
                left join mk_files on mk_alarm_plan.id = mk_files.module_id ");
            var parameter = new List<DbParameter>();
            AlarmPlanInfoEntity Entity = null;
            if (AlarmField.HasValue)
            {

                strSql.Append(" where mk_alarm_plan.alarm_field = @AlarmField and mk_files.module_type=@ModuleType");
                strSql.Append(" order by mk_alarm_plan.base_create_time desc");
                parameter.Add(DbParameterExtension.CreateDbParameter("@AlarmField", AlarmField.Value));
                parameter.Add(DbParameterExtension.CreateDbParameter("@ModuleType", UploadFileType.RoadMap.ToString()));

                var list = await this.BaseRepository().FindList<AlarmPlanInfoEntity>(strSql.ToString(), parameter.ToArray());
                Entity = list.FirstOrDefault(); //有可能存在多个相同预案，取最新一个
                if (Entity != null)
                {
                    Entity.SystemImage = GlobalContext.SystemConfig.WebUrI + Entity.SystemImage;
                    //获取文件列表
                    FileListParam fileListParam = new FileListParam();
                    fileListParam.ModuleId = Entity.Id;
                    fileListParam.ModuleType = "File"; // TODO: UploadFileType 枚举对象没有File 只有Files 但是数据库中存的是File如果前端上传有修改这里可以优化。
                    var Files = await fileService.GetList(fileListParam);
                    Files.ForEach(i =>
                    {
                        i.FilePath = GlobalContext.SystemConfig.WebUrI + i.FilePath;
                    });
                    Entity.FileList = Files;
                }
            }
            return Entity;
        }


        /// <summary>
        /// 获取有报警预案的报警记录条数
        /// </summary>
        /// <returns></returns>
        public async Task<int> GetAlarmCount()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select count(1)
                    from mk_alarm_record a 
                    left join mk_alarm_settings  b on a.alarm_id = b.id 
                    inner join  mk_alarm_plan c on c.alarm_field = b.id ");
            object result = await this.BaseRepository().FindObject(strSql.ToString());
            int count = result.ParseToInt();
            return count;
        }

        /// <summary>
        /// 区域告警环境数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<AreaAlarmEntity>> GetAreaAlarmInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select *,CASE WHEN AlarmNum < 1 then '优' when AlarmNum < = 2  then '一般'  else '差' end as State from (
select mk_system_setting.id,mk_system_setting.system_name as SystemName,count(mk_system_setting.id) as AlarmNum
 from mk_alarm_record  left join mk_alarm_settings on mk_alarm_record.alarm_id = mk_alarm_settings.id 
left join  mk_system_setting on mk_alarm_settings.system_models = mk_system_setting.id
group by mk_system_setting.id,mk_system_setting.system_name) as tb ");

            var list = await this.BaseRepository().FindList<AreaAlarmEntity>(strSql.ToString());
            return list.ToList();
        }



        #endregion

        #region 私有方法
        private Expression<Func<BojingMonitorEntity, bool>> ListFilter(BojingMonitorParam param)
        {
            var expression = LinqExtensions.True<BojingMonitorEntity>();
            return expression;
        }
        #endregion
    }
}
