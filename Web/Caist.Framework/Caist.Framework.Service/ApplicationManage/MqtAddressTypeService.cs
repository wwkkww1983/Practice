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
    public class MqtAddressTypeService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<MqtAddressTypeEntity>> GetList(MqtAddressTypeParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<MqtAddressTypeEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<MqtAddressTypeEntity>> GetPageList(MqtAddressTypeParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<MqtAddressTypeEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<List<MqtAddressTypeEntity>> GetPageContentList(MqtAddressTypeParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql, true);
            var list = await this.BaseRepository().FindList<MqtAddressTypeEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<MqtAddressTypeEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<MqtAddressTypeEntity>(id);
        }

     
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(MqtAddressTypeParam param, StringBuilder strSql, bool bMqThemeContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.system_code as SystemCode,
                                    a.name as Name,
                                    a.code as Code ");
            if (bMqThemeContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM    mk_mqtt_address_type a WHERE  1=1 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.SystemCode))
                {
                    if (param.SystemCode=="03")   //运输系统的编码
                    {
                        strSql.Append(" AND a.system_code=@SystemCode");
                        parameter.Add(DbParameterExtension.CreateDbParameter("@SystemCode", param.SystemCode));
                    }
                    else//其它系统的编码
                    {
                        strSql.Append(" AND a.system_code=@SystemCode");
                        parameter.Add(DbParameterExtension.CreateDbParameter("@SystemCode","01"));
                    }

                 
                }
               
            }
            return parameter;
        }
        #endregion
    }
}
