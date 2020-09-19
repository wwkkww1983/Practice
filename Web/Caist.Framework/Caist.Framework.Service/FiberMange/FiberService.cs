using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.FiberManage;
using Caist.Framework.Entity.PeopleManage;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.FiberManage
{
    public class FiberService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<CableEntity>> GetFiberInfoList(FiberParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<CableEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }
        /// <summary>
        /// 获取指定区域最新数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<CableInfo> GetFiberAreaData(string AreaName)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"select top(1) 
            area_Name as areaName,max_value as maxValue,min_Value as minValue,average_value as AverageValue,create_Time as createTime
            from [dbo].[mk_cable_thermometry] ");

            if (!string.IsNullOrEmpty(AreaName))
            {
                strSql.AppendFormat(@" where area_Name='{0}' ", AreaName);
            }
            strSql.Append(@" order by create_Time desc");
            return await this.BaseRepository().FindObject<CableInfo>(strSql.ToString());
          
        }

        #endregion

        #region 提交数据
        public async Task SaveForm(RegionEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<RegionEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<RegionEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<RegionEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(FiberParam param, StringBuilder strSql)
        {
            strSql.Append(@"select 
            area_Name as areaName,max_value as maxValue,min_Value as minValue,create_Time as createTime
            from [dbo].[mk_cable_thermometry]");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.AreaName))
                {
                    strSql.Append(" where area_Name=@AreaName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@AreaName", param.AreaName));
                }

            }
            return parameter;
        }
        #endregion
    }
}
