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
    public class ViewControlModelService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<ViewControlModelEntity>> GetList(ViewControlModelListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<ViewControlModelEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<ViewControlModelEntity>> GetPageList(ViewControlModelListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<ViewControlModelEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<ViewControlModelEntity>> GetPageContentList(ViewControlModelListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<ViewControlModelEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<ViewControlModelEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<ViewControlModelEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(control_sort) FROM mk_view_control_model");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(ViewControlModelEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<ViewControlModelEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<ViewControlModelEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<ViewControlModelEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(ViewControlModelListParam param, StringBuilder strSql, bool bSystemSettingContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.view_function_id as ViewFunctionId,
                                    a.control_name as ControlName,
                                    a.control_stutas as ControlStutas,
                                    a.control_sort as ControlSort,
                                    b.view_name as ViewName");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM mk_view_control_model a left join mk_view_function b on a.view_function_id = b.id
                            WHERE   1 = 1");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ControlName))
                {
                    strSql.Append(" AND a.control_name like @ControlName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ControlName", '%' + param.ControlName + '%'));
                }
                if (param.ControlStutas > 0)
                {
                    strSql.Append(" AND a.control_stutas = @ControlStutas");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@ControlStutas", param.ControlStutas));
                }
            }
            return parameter;
        }
        #endregion
    }
}
