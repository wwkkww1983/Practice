using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.Cewenhistory;
using Caist.Framework.Model.Param.Cewenhistory;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.Cewenhistory
{
   public class CewenMonitorService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<CewenMonitorEntity>> GetSecurityInfoList(CewenMonitorParam param)
        {
            var expression = ListFilter(param);
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select 
            area_Name as areaName,max_value as maxValue,min_Value as minValue,create_Time as createTime
            from [dbo].[mk_cable_thermometry]");
            strSql.Append("where  DATEDIFF(HOUR,getdate(),[create_Time]) <=1");
            var list = await this.BaseRepository().FindList<CewenMonitorEntity>(strSql.ToString());
            return list.OrderBy(p => p.createTime).ToList();
        }

        /// <summary>
        /// 获取光前点位
        /// </summary>
        /// <returns></returns>
        public async Task<List<FiberCure>> GetPointNameList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select area_name as PointName from mk_cable_thermometry group by area_name");
            var list = await this.BaseRepository().FindList<FiberCure>(strSql.ToString());
            return list.ToList();
        }

        public async Task<List<FiberAverage>> GetPointAverageList(CewenMonitorParam param)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.average_value as AverageValue,create_time as CreateTime
                            from mk_cable_thermometry a 
                            where  
                            not exists(
                                select 1 from mk_cable_thermometry where  convert(varchar(13),create_time,120)=convert(varchar(13),a.create_time,120) and create_time>a.create_time
                            ) ");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (param.StartDate.HasValue)
                {
                    strSql.Append(" AND  create_time > @StartDate");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@StartDate", param.StartDate));

                }
                if (param.EndDate.HasValue)
                {
                    strSql.Append(" AND  create_time <= @EndDate");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@EndDate", param.EndDate));

                }
                if (param.DictId.HasValue())
                {
                    strSql.Append(" AND  area_name = @AreaName");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@AreaName", param.DictId));
                }
            }


            var list = await this.BaseRepository().FindList<FiberAverage>(strSql.ToString(),parameter.ToArray());
            return list.ToList();
        }
        #endregion

        #region 私有方法
        private Expression<Func<CewenMonitorEntity, bool>> ListFilter(CewenMonitorParam param)
        {
            var expression = LinqExtensions.True<CewenMonitorEntity>();
            //if (param != null)
            //{
            //}
            return expression;
        }
        #endregion
    }
}
