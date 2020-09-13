using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
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
        private List<DbParameter> ListFilter(InformationPublishParam param, StringBuilder strSql)
        {
            strSql.Append(@"select a.id as Id,
                            a.Device_Uid as DeviceUid,
                            a.Link_Content as LinkContent,
                            a.base_modify_time as BaseModifyTime,
                            b.Device_Name as DeviceName");
            strSql.Append(@" FROM mk_information_publish a left join mk_led_device b on a.Device_Uid = b.Device_Uid ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.DeviceUid))
                {
                    strSql.Append(" AND a.Device_Uid=@DeviceUid");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@DeviceUid", param.DeviceUid));
                   
                }
                if (!string.IsNullOrEmpty(param.LinkContent))
                {
                    strSql.Append(" AND a.Link_Content like @LinkContent");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@LinkContent", "%" + param.LinkContent + "%"));
                }
            }
            return parameter;
        }
        #endregion
    }
}
