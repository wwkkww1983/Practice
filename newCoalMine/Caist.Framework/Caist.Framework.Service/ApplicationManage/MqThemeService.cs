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
    public class MqThemeService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<MqThemeEntity>> GetList(MqThemeListParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<MqThemeEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<MqThemeEntity>> GetPageList(MqThemeListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<MqThemeEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<MqThemeEntity>> GetPageContentList(MqThemeListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<MqThemeEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<MqThemeEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<MqThemeEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(mq_stutas) FROM mk_mq_theme");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(MqThemeEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<MqThemeEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<MqThemeEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<MqThemeEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(MqThemeListParam param, StringBuilder strSql, bool bMqThemeContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.mq_host as MqHost,
                                    a.mq_port as MqPort,
                                    a.mq_clientid as MqClientid,
                                    a.mq_encryption as MqEncryption,
                                    a.mq_user as MqUser,
                                    a.mq_password as MqPassword,
                                    a.mq_ctl as MqCtl,
                                    a.mq_code as MqCode,
                                    a.mq_name as MqName,
                                    a.mq_colliery_name as MqCollieryName,
                                    a.mq_colliery_code as MqCollieryCode,
                                    a.mq_stutas as MqStutas");
            if (bMqThemeContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM    mk_mq_theme a WHERE   1 = 1");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.MqHost))
                {
                    strSql.Append(" AND a.mq_host like @MqHost");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@MqHost", '%' + param.MqHost + '%'));
                }
                if (!string.IsNullOrEmpty(param.MqUser))
                {
                    strSql.Append(" AND a.mq_user = @MqUser");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@MqUser", param.MqUser));
                }
            }
            return parameter;
        }
        #endregion
    }
}
