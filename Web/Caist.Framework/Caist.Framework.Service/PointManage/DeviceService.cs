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
    public class DeviceService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<DeviceEntity>> GetList(DeviceListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<DeviceEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<DeviceEntity>> GetPageList(DeviceListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<DeviceEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<DeviceEntity>> GetPageContentList(DeviceListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<DeviceEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<DeviceEntity> GetSysDataTableName(string dictId)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select top(1)
                                    c.id as Id,
                                    c.base_modify_time as BaseModifyTime,
                                    c.base_modifier_id as BaseModifierId,
                                    c.Device_Name as DeviceName,
                                    c.Device_Host as DeviceHost,
                                    c.Device_Port as DevicePort,
                                    c.PLCType as PLCType,
                                    c.Slot_No as SlotNo,
                                    c.Local as Local,
                                    c.Remote as Remote,
                                    c.parent_id as ParentId,
                                    c.system_id as SystemId,
                                    c.tab_name as TabName
            from  mk_view_manipulate_model a
            left join mk_view_function b on  a.view_function_id = b.id
            left join mk_device c on b.system_setting_id = c.system_id");
            List<DbParameter> param = new List<DbParameter>();
            if (!string.IsNullOrEmpty(dictId))
            {
                strSql.Append(" where a.manipulate_model_mark=@dictId");
                param.Add(DbParameterExtension.CreateDbParameter("@dictId", dictId));
            }
            var list = await this.BaseRepository().FindList<DeviceEntity>(strSql.ToString(), param.ToArray());
            return list.ToList().FirstOrDefault();
        }

        public async Task<DeviceEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<DeviceEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(view_sort) FROM GetMaxSort");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }

        public bool ExistMenuName(DeviceEntity entity)
        {
            var expression = LinqExtensions.True<DeviceEntity>();
            expression = expression.And(t => t.BaseIsDelete == 0);
            if (entity.Id.IsNullOrZero())
            {
                expression = expression.And(t => t.DeviceName == entity.DeviceName && t.DeviceHost == entity.DeviceHost);
            }
            else
            {
                expression = expression.And(t => t.DeviceName == entity.DeviceName && t.DeviceHost == entity.DeviceHost && t.Id != entity.Id);
            }
            return this.BaseRepository().IQueryable(expression).Count() > 0 ? true : false;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(DeviceEntity entity)
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
            await this.BaseRepository().Delete<DeviceEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(DeviceListParam param, StringBuilder strSql, bool bSystemSettingContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.Device_Name as DeviceName,
                                    a.Device_Host as DeviceHost,
                                    a.Device_Port as DevicePort,
                                    a.PLCType as PLCType,
                                    a.Slot_No as SlotNo,
                                    a.Local as Local,
                                    a.Remote as Remote,
                                    a.parent_id as ParentId,
                                    a.system_id as SystemId,
                                    a.tab_name as TabName");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM mk_device a WHERE a.base_is_delete = 0 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.DeviceName))
                {
                    strSql.Append(" AND a.Device_Name like @DeviceName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@DeviceName", "%" + param.DeviceName + "%"));
                }
                if (!string.IsNullOrEmpty(param.DeviceHost))
                {
                    strSql.Append(" AND a.Device_Host = @DeviceHost");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@DeviceHost", param.DeviceHost));
                }
            }
            return parameter;
        }
        #endregion
    }
}
