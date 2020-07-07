using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.ApplicationManage
{
    public class ViewParamenterService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<ViewParamenterEntity>> GetList(ViewParamenterListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<ViewParamenterEntity>(strSql.ToString(), filter.ToArray());
            return list.OrderBy(n=>n.ParamenterSort).ToList();
        }

        public async Task<List<ViewParamenterEntity>> GetPageList(ViewParamenterListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<ViewParamenterEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.OrderBy(n => n.ParamenterSort).ToList();
        }

        public async Task<List<ViewParamenterEntity>> GetPageContentList(ViewParamenterListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<ViewParamenterEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<ViewParamenterEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<ViewParamenterEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(paramenter_sort) FROM mk_view_paramenter");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(ViewParamenterEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<ViewParamenterEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<ViewParamenterEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<ViewParamenterEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(ViewParamenterListParam param, StringBuilder strSql, bool bSystemSettingContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.view_control_model_id as ViewControlModelId,
                                    a.paramenter_name as ParamenterName,
                                    a.paramenter_instruct as ParamenterInstruct,
                                    a.paramenter_instruct_start as ParamenterInstructStart,
                                    a.paramenter_instruct_end as ParamenterInstructEnd,
                                    a.paramenter_unit as ParamenterUnit,
                                    a.paramenter_status as ParamenterStatus,
                                    a.paramenter_sort as ParamenterSort,
                                    a.paramenter_ip as ParamenterIp,
                                    a.paramenter_port as ParamenterPort,
                                    b.control_name as ControlName,
                                    a.paramenter_value_type as ParamenterValueType,
                                    a.paramenter_value as ParamenterValue");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM mk_view_paramenter a left join mk_view_control_model b on a.view_control_model_id = b.id 
                            left join mk_view_function c on b.view_function_id = c.id left join mk_system_setting d on c.system_setting_id = d.id
                            WHERE   a.base_is_delete = 0 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ParamenterName))
                {
                    strSql.Append(" AND a.paramenter_name like @ParamenterName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ParamenterName", "%" + param.ParamenterName + "%"));
                }
                if (param.ParamenterStatus>0)
                {
                    strSql.Append(" AND a.paramenter_status = @ParamenterStatus");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ParamenterStatus", param.ParamenterStatus));
                }
                //系统模块ID
                if (param.SystemId > 0)
                {
                    strSql.Append(" AND d.id = @SystemId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemId", param.SystemId));
                }
            }
            return parameter;
        }
        #endregion
    }
}
