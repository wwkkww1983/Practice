using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.PointManage;
using Caist.Framework.Model.Param.PointManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.PointManage
{
    public class InstructService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<InstructEntity>> GetList(InstructListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<InstructEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<InstructEntity>> GetPageList(InstructListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<InstructEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<InstructEntity>> GetPageContentList(InstructListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<InstructEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<InstructEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<InstructEntity>(id);
        }

        public bool ExistMenuName(InstructEntity entity)
        {
            var expression = LinqExtensions.True<InstructEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Name == entity.Name && t.Address == entity.Address);
            }
            else
            {
                expression = expression.And(t => t.Name == entity.Name && t.Address == entity.Address && t.Id != entity.Id);
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(InstructEntity entity)
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
            await this.BaseRepository().Delete<InstructEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(InstructListParam param, StringBuilder strSql, bool bSystemSettingContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.name as Name,
                                    a.address as Address,
                                    a.data_type as DataType,
                                    a.output as Output,
                                    a.remark as Remark,
                                    a.instruct_group_id as InstructGroupId,
                                    b.name as InstructGroupName ");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM [mk_instruct] a left join [mk_instruct_group] b on a.instruct_group_id = b.id WHERE a.base_is_delete = 0 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    strSql.Append(" AND a.Name like @Name");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@Name", '%' + param.Name + '%'));
                }
            }
            return parameter;
        }
        #endregion
    }
}
