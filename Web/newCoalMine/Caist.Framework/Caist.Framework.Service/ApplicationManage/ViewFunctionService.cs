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
    public class ViewFunctionService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<ViewFunctionEntity>> GetList(ViewFunctionListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<ViewFunctionEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<ViewFunctionEntity>> GetPageList(ViewFunctionListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<ViewFunctionEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<ViewFunctionEntity>> GetPageContentList(ViewFunctionListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<ViewFunctionEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<ViewFunctionEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<ViewFunctionEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(view_sort) FROM mk_view_function");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }

        public bool ExistMenuName(ViewFunctionEntity entity)
        {
            var expression = LinqExtensions.True<ViewFunctionEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.ViewName == entity.ViewName && t.ViewType == entity.ViewType);
            }
            else
            {
                expression = expression.And(t => t.ViewName == entity.ViewName && t.ViewType == entity.ViewType && t.Id != entity.Id);
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(ViewFunctionEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<ViewFunctionEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(ViewFunctionListParam param, StringBuilder strSql, bool bSystemSettingContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.system_setting_id as SystemSettingId,
                                    a.view_name as ViewName,
                                    a.view_sort as ViewSort,
                                    a.view_type as ViewType,
                                    a.view_button_event as ViewButtonEvent,
                                    a.view_button_id as ViewButtonId,
                                    a.view_button_alt as ViewButtonAlt,
                                    a.view_button_url as ViewButtonUrl,
                                    a.view_button_image as ViewButtonImage,
                                    a.view_status as ViewStatus,
                                    b.system_name as SystemSettingName ");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM mk_view_function a left join mk_system_setting b on a.system_setting_id = b.id WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ViewName))
                {
                    strSql.Append(" AND b.[system_nick_name] like @ViewName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ViewName", '%' + param.ViewName + '%'));
                }
                if (param.ViewStatus > 0)
                {
                    strSql.Append(" AND a.view_status = @ViewStatus");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ViewStatus", param.ViewStatus));
                }
            }
            return parameter;
        }
        #endregion
    }
}
