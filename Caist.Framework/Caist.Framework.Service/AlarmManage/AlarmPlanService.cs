using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.AlarmManage;
using Caist.Framework.Model.Param.OrganizationManage;
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
    public class AlarmPlanService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<AlarmPlanEntity>> GetList(AlarmPlanListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<AlarmPlanEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<AlarmPlanReturnEntity>> GetPageList(AlarmPlanListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<AlarmPlanReturnEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<AlarmPlanEntity>> GetPageContentList(AlarmPlanListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<AlarmPlanEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<AlarmPlanEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<AlarmPlanEntity>(id);
        }

        #endregion

        #region 提交数据
        public async Task SaveForm(AlarmPlanEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<AlarmPlanEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<AlarmPlanEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<AlarmPlanEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(AlarmPlanListParam param, StringBuilder strSql)
        {
            strSql.Append(@"select p.id as Id,p.alarm_name as AlarmName,s.system_nick_name as SysName,t.broadcast_content as AlarmContent,p.enable
                            from mk_alarm_plan p inner join mk_system_setting s on p.sys_id=s.id
                            inner join mk_alarm_settings t on p.alarm_field = t.id where 1=1");
            
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (param.AlarmName.HasValue())
                {
                    strSql.Append(" AND a.alarm_name like @AlarmName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@AlarmName", '%' + param.AlarmName + '%'));
                }
            }
            return parameter;
        }
        #endregion
    }
}
