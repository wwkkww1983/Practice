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
    public class SensorService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<SensorEntity>> GetList(SensorParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<SensorEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<SensorEntity>> GetPageList(SensorParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<SensorEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<SensorEntity>> GetPageContentList(SensorParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<SensorEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<SensorEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<SensorEntity>(id);
        }

        #endregion

        #region 提交数据
        public async Task SaveForm(SensorEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<SensorEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<SensorEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<SensorEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(SensorParam param, StringBuilder strSql, bool bMqThemeContent = false)
        {
            strSql.Append(@"select id as Id,
                            system_code as SystemCode,
                            name as Name,
                            code as Code,
                            code_type as CodeType,
                            value_length as ValueLength,
                            decimal_places as  DecimalPlaces ");
            if (bMqThemeContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM mk_mqtt_sensor_setting  WHERE   base_is_delete = 0 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.SystemCode))
                {
                    strSql.Append(" AND system_code = @SystemCode");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@SystemCode", param.SystemCode));
                }
            }
            return parameter;
        }
        #endregion
    }
}
