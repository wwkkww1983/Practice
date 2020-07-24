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
    public class InformationPublishService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<InformationPublishEntity>> GetList(InformationPublishParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<InformationPublishEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }

        public async Task<List<InformationPublishEntity>> GetPageList(InformationPublishParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<InformationPublishEntity>(strSql.ToString(), filter.ToArray(), pagination);
            return list.ToList();
        }

        public async Task<InformationPublishEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<InformationPublishEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(InformationPublishEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<InformationPublishEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<InformationPublishEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<InformationPublishEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(InformationPublishParam param, StringBuilder strSql, bool bMqThemeContent = false)
        {
            strSql.Append(@"SELECT  a.id as Id,
                                    a.base_modify_time as BaseModifyTime,
                                    a.base_modifier_id as BaseModifierId,
                                    a.Device_Uid as DeviceUid,
                                    a.Link_Content as LinkContent");
            if (bMqThemeContent)
            {
                strSql.Append("");
            }
            strSql.Append(@" FROM  mk_information_publish a WHERE   a.base_is_delete = 0 ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.DeviceUID))
                {
                    strSql.Append(" AND a.Device_UID=@DeviceUID");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@DeviceUID", param.DeviceUID));
                }
              
            }
            return parameter;
        }
        #endregion
    }
}
