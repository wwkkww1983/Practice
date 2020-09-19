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
    public class InstructGroupService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<InstructGroupEntity>> GetList(InstructGroupListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<InstructGroupEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<InstructGroupEntity>> GetPageList(InstructGroupListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<InstructGroupEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<InstructGroupEntity>> GetPageContentList(InstructGroupListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<InstructGroupEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<InstructGroupEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<InstructGroupEntity>(id);
        }


        public bool ExistMenuName(InstructGroupEntity entity)
        {
            var expression = LinqExtensions.True<InstructGroupEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.Name == entity.Name && t.DeviceName == entity.DeviceName);
            }
            else
            {
                expression = expression.And(t => t.Name == entity.Name && t.DeviceName == entity.DeviceName && t.Id != entity.Id);
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }

        #endregion

        #region 提交数据
        public async Task SaveForm(InstructGroupEntity entity)
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
            await this.BaseRepository().Delete<InstructGroupEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(InstructGroupListParam param, StringBuilder strSql, bool bSystemSettingContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.name as Name,
                                    a.modular_type as ModularType,
                                    a.read_count as ReadCount,
                                    a.begin_address as BeginAddress,
                                    a.begin_block as BeginBlock,
                                    a.device_id as DeviceId,
                                    b.device_name as DeviceName,
                                    a.group_name as GroupName");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" from [mk_instruct_group] as a left join [mk_device] b on a.device_id = b.id WHERE a.base_is_delete = 0 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Name))
                {
                    strSql.Append(" AND a.Name like @Name");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@Name", "%" + param.Name + "%"));
                }
                if (!string.IsNullOrEmpty(param.DeviceId))
                {
                    strSql.Append(" AND a.device_id = @DeviceId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@DeviceId", param.DeviceId));
                }
                if (!string.IsNullOrEmpty(param.SystemId))
                {
                    strSql.Append(" AND b.system_id = @SystemId");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemId", param.SystemId));
                }
            }
            return parameter;
        }
        #endregion
    }
}
