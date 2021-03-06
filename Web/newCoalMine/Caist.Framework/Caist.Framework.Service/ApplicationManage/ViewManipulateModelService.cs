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
    public class ViewManipulateModelService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<ViewManipulateModelEntity>> GetList(ViewManipulateModelListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<ViewManipulateModelEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<ViewManipulateModelEntity>> GetPageList(ViewManipulateModelListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<ViewManipulateModelEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<ViewManipulateModelEntity>> GetPageContentList(ViewManipulateModelListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<ViewManipulateModelEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<ViewManipulateModelEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<ViewManipulateModelEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(manipulate_model_sort) FROM mk_view_manipulate_model");
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
        public async Task SaveForm(ViewManipulateModelEntity entity)
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
            await this.BaseRepository().Delete<ViewManipulateModelEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(ViewManipulateModelListParam param, StringBuilder strSql, bool bSystemSettingContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.view_function_id as ViewFunctionId,
                                    a.manipulate_model_name as ManipulateModelName,
                                    a.manipulate_model_sort as ManipulateModelSort,
                                    a.manipulate_model_stutas as ManipulateModelStutas,
                                    a.manipulate_model_mark as ManipulateModelMark,
                                    a.manipulate_model_unit as ManipulateModelUnit,
                                    b.view_name as ViewName ");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM mk_view_manipulate_model a left join mk_view_function b on a.view_function_id = b.id WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ManipulateModelName))
                {
                    strSql.Append(" AND a.manipulate_model_name like @ManipulateModelName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ManipulateModelName", '%' + param.ManipulateModelName + '%'));
                }
                if (param.ManipulateModelStutas > 0)
                {
                    strSql.Append(" AND a.manipulate_model_stutas = @ManipulateModelStutas");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ManipulateModelStutas", param.ManipulateModelStutas));
                }
            }
            return parameter;
        }
        #endregion
    }
}
