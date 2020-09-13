using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.AlarmManage;
using Caist.Framework.Model.Param.AlarmManage;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.AlarmManage
{
    public class AlarmReccordService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<AlarmRecordEntity>> GetList(ALarmReccordListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<AlarmRecordEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<AlarmRecordEntity>> GetPageList(ALarmReccordListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<AlarmRecordEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<AlarmRecordEntity>> GetPageContentList(ALarmReccordListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<AlarmRecordEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }


        /// <summary>
        /// 以小时为单位获取系统报警数据量
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<List<AlarmCount>> GetAlarmCureList(ALarmReccordListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> parameter = new List<DbParameter>();
            strSql.Append(@"select DATEADD(HOUR, DATEDIFF(HOUR,0,alarm_time), 0) AlarmTime,count([mk_alarm_record].id) Count
                                 FROM [mk_alarm_record] left join [mk_alarm_settings] b on [mk_alarm_record].alarm_id = b.id ");

            if (param != null)
            {
                strSql.Append(" where 1=1 ");
                if (!string.IsNullOrEmpty(param.SystemId))
                {
                    strSql.Append(" and b.system_models=@SystemId ");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemId", param.SystemId));
                }
                if (!string.IsNullOrEmpty(param.StartDate.ParseToString()) && !string.IsNullOrEmpty(param.EndDate.ParseToString()))
                {
                    strSql.Append(" and alarm_time between @StartDate and @EndDate ");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@StartDate", param.StartDate));
                    parameter.Add(DbParameterExtension.CreateDbParameter("@EndDate", param.EndDate));
                }
            }
            strSql.Append(" group by DATEADD(HOUR, DATEDIFF(HOUR,0,alarm_time), 0) ");
            var list = await this.BaseRepository().FindList<AlarmCount>(strSql.ToString(), parameter.ToArray());
            return list.ToList();
        }

        /// <summary>
        /// 获取有报警的系统名称
        /// </summary>
        /// <returns></returns>
        public async Task<List<AlarmCure>> GetAlarmSystemNameList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select c.system_nick_name as SystemName,c.id as SystemId  from [mk_alarm_record] a
                        left join [mk_alarm_settings] b on a.alarm_id = b.id
                        left join mk_system_setting c on b.system_models = c.id
                        group by c.system_nick_name,c.id");
            var list = await this.BaseRepository().FindList<AlarmCure>(strSql.ToString());
            return list.ToList();
        }

        #endregion

        #region 提交数据
        public async Task SaveForm(AlarmRecordEditEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<AlarmRecordEditEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<AlarmRecordEditEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<AlarmRecordEditEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(ALarmReccordListParam param, StringBuilder strSql, bool bSystemSettingContent = false)
        {
            strSql.Append(@"select d.id as Id,d.alarm_id as AlarmId,d.alarm_time as AlarmTime,d.alarm_time_length as AlarmTimeLength,
                            d.alarm_reason as AlarmReason,e.view_manipulate_id as ViewManipulateId,e.manipulate_model_name as ManipulateModelName,
                            e.broadcast_content as BroadcastContent,e.system_models as SystemId,e.system_name as SystemName,e.broadcast_count as BroadcastCount,
                            e.manipulate_model_mark as ManipulateModelMark,e.view_name as ViewName,d.alarm_value as AlarmValue
							from [mk_alarm_record] d left join 
                            (select a.id,a.view_manipulate_id,c.manipulate_model_name ,a.broadcast_content,a.system_models,b.system_name,
                                    a.broadcast_count,c.manipulate_model_mark,f.view_name  
                            from [mk_alarm_settings] a left join [mk_system_setting] b on a.system_models = b.id
                            left join [mk_view_manipulate_model] c on a.view_manipulate_id = c.id 
                            left join mk_view_function f on c.view_function_id = f.id) e 
                            on d.alarm_id = e.id ");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                strSql.Append("where 1=1 ");
                if (!string.IsNullOrEmpty(param.SystemId))
                {
                    strSql.Append(" and e.system_models=@SystemId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemId", param.SystemId));
                }
                if (param.BroadcastCount.HasValue)
                {
                    strSql.Append(" and e.broadcast_count=@BroadcastCount");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@BroadcastCount", param.BroadcastCount.Value));
                }
                if (!string.IsNullOrEmpty(param.StartDate.ParseToString()) && !string.IsNullOrEmpty(param.EndDate.ParseToString()))
                {
                    strSql.Append(" and d.alarm_time between @StartDate and @EndDate ");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@StartDate", param.StartDate));
                    parameter.Add(DbParameterExtension.CreateDbParameter("@EndDate", param.EndDate));
                }
            }
            return parameter;
        }
        #endregion
    }
}
