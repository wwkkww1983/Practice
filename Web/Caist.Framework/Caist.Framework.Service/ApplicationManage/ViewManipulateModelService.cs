using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Model.Param.OrganizationManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Microsoft.EntityFrameworkCore.Internal;
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


        public async Task<SystemDataEntity> GetPublishList(SystemDataParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilterPublish(param, strSql);
            var list = await this.BaseRepository().FindList<SystemDataEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList().FirstOrDefault();
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

        public async Task<object> ExistViewFunctionId(ViewManipulateModelEntity param)
        {
            StringBuilder strSql = new StringBuilder();
            var parameter = new List<DbParameter>();
            strSql.Append("select count(1) from mk_view_manipulate_model where mk_view_manipulate_model.view_function_id=@ViewFunctionId");
            parameter.Add(DbParameterExtension.CreateDbParameter("@ViewFunctionId", param.ViewFunctionId));
            object count = await this.BaseRepository().FindObject(strSql.ToString(), parameter.ToArray());
            return count;
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
                                    b.system_setting_id as SystemId, 
                                    a.manipulate_model_name as ManipulateModelName,
                                    a.manipulate_model_sort as ManipulateModelSort,
                                    a.manipulate_model_stutas as ManipulateModelStutas,
                                    a.manipulate_model_show_home as ManipulateModelShowHome,
                                    a.manipulate_model_mark as ManipulateModelMark,
                                    a.manipulate_model_unit as ManipulateModelUnit,
                                    a.Instruct_view as InstructView,
                                    b.view_name as ViewName
                                     ");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM mk_view_manipulate_model a left join mk_view_function b on a.view_function_id = b.id WHERE a.base_is_delete = 0 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (param.InstructView.HasValue)
                {
                    strSql.Append(" AND a.Instruct_view = @InstructView");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@InstructView", param.InstructView));
                }
                if (!string.IsNullOrEmpty(param.ManipulateModelName))
                {
                    strSql.Append(" AND a.manipulate_model_name like @ManipulateModelName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ManipulateModelName", "%" + param.ManipulateModelName + "%"));
                }
                if (param.ManipulateModelStutas > 0)
                {
                    strSql.Append(" AND a.manipulate_model_stutas = @ManipulateModelStutas");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ManipulateModelStutas", param.ManipulateModelStutas));
                }
                if (!string.IsNullOrEmpty(param.ViewFunctionId.ToString()))
                {
                    strSql.Append(" AND a.view_function_id = @ViewFunctionId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ViewFunctionId", param.ViewFunctionId));
                }
                //系统模块ID
                if (param.SystemId > 0)
                {
                    strSql.Append(" AND b.system_setting_id = @SystemId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemId", param.SystemId));
                }
            }
            return parameter;
        }

        private List<DbParameter> ListFilterPublish(SystemDataParam param, StringBuilder strSql)
        {
            strSql.Append(@"SELECT top(1) dict_Id as DictName,
                                dict_Value as DictValue,
                                create_Time as CreateTime ");
        
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.TabName))
                {
                    strSql.Append(string.Format(" from {0} ", param.TabName));
                }
                if (!string.IsNullOrEmpty(param.DictId))
                {
                    strSql.Append(" where dict_Id = @DictId order by create_Time desc");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@DictId", param.DictId));
                }
            }
            return parameter;
        }
        #endregion
    }
}
