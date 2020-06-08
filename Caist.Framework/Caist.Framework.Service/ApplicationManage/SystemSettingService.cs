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
    public class SystemSettingService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<SystemSettingEntity>> GetList(SystemSettingListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<SystemSettingEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<SystemSettingEntity>> GetPageList(SystemSettingListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<SystemSettingEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<SystemSettingEntity>> GetPageContentList(SystemSettingListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<SystemSettingEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<SystemSettingEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<SystemSettingEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(system_sort) FROM mk_system_setting");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(SystemSettingEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<SystemSettingEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<SystemSettingEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<SystemSettingEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(SystemSettingListParam param, StringBuilder strSql, bool bSystemSettingContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.system_name as SystemName,
                                    a.system_nick_name as SystemNickName,
                                    a.system_image as SystemImage,
                                    a.system_model as SystemModel,
                                    a.system_sort as SystemSort,
                                    a.system_url as SystemUrl");
            if (bSystemSettingContent)
            {
                strSql.Append("");
            }
            strSql.Append(@"         FROM    mk_system_setting a
                            WHERE  a.base_is_delete = 0 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.Id.ToString()))
                {
                    strSql.Append(" AND a.id = @Id");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@Id",  param.Id));
                }
                if (!string.IsNullOrEmpty(param.SystemName))
                {
                    strSql.Append(" AND a.system_name like @SystemName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemName", '%' + param.SystemName + '%'));
                }
                if (!string.IsNullOrEmpty(param.SystemNickName))
                {
                    strSql.Append(" AND a.system_nick_name = @SystemNickName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemNickName", param.SystemNickName));
                }
            }
            return parameter;
        }
        #endregion
    }
}
