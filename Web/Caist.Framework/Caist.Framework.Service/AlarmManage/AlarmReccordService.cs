using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.AlarmManage;
using Caist.Framework.Model.Param.AlarmManage;
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
                            e.broadcast_content as BroadcastContent,e.system_models as SystemId,e.system_name as SystemName from [mk_alarm_record] d left join 
                            (select a.id,a.view_manipulate_id,c.manipulate_model_name ,a.broadcast_content,a.system_models,b.system_name 
                            from [mk_alarm_settings] a left join [mk_system_setting] b on a.system_models = b.id
                            left join [mk_view_manipulate_model] c on a.view_manipulate_id = c.id) e on d.alarm_id = e.id ");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.SystemId.ToString()))
                {
                    strSql.Append(" AND e.system_models like @Id");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@Id", "%" + param.SystemId + "%"));
                }
            }
            return parameter;
        }
        #endregion
    }
}
