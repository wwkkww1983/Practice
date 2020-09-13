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
    public class AlarmSettingService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<AlarmSettingEntity>> GetList(AlarmSettingListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<AlarmSettingEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<AlarmSettingEntity>> GetPageList(AlarmSettingListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<AlarmSettingEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<AlarmSettingEntity>> GetPageContentList(AlarmSettingListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<AlarmSettingEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<AlarmSettingEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<AlarmSettingEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(AlarmSettingEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<AlarmSettingEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<AlarmSettingEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<AlarmSettingEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(AlarmSettingListParam param, StringBuilder strSql, bool bSystemSettingContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.view_manipulate_id as ViewManipulateId,
                                    b.manipulate_model_name as ManipulateModelName,
                                    a.system_models as SystemModels,
                                    a.min_value as MinValue,
                                    a.max_value as MaxValue,
                                    a.broadcast_content as BroadcastContent,
                                    a.broadcast_count as BroadcastCount ");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM    mk_alarm_settings a left join mk_view_manipulate_model b on a.view_manipulate_id = b.id
                             left join mk_system_setting c on a.system_models = c.id WHERE   a.base_is_delete = 0");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ViewManipulateId.ToString()))
                {
                    strSql.Append(" AND a.id like @Id");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@Id", "%" + param.ViewManipulateId + "%"));
                }
                if (!string.IsNullOrEmpty(param.ManipulateModelName))
                {
                    strSql.Append(" AND a.system_name like @SystemName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemName", "%" + param.ManipulateModelName + "%"));
                }
                if (!string.IsNullOrEmpty(param.SystemModels.ToString()))
                {
                    strSql.Append(" AND a.system_models like @SystemModels");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemModels", "%" + param.SystemModels + "%"));
                }
            }
            return parameter;
        }
        #endregion
    }
}
