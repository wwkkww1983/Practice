using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.Pidaihistory;
using Caist.Framework.Model.Param.Pidaihistory;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.Pidaihistory
{
   public class PidaiMonitorService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<PidaiMonitorEntity>> GetSecurityInfoList(PidaiMonitorParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<PidaiMonitorEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }
        #endregion

        #region 私有方法
        private List<DbParameter> ListFilter(PidaiMonitorParam param, StringBuilder strSql)
        {
            strSql.Append(@"SELECT top(1000) a.[dict_Id ] as dictId,a.dict_Value as dictValue, a.create_Time as CreateTime");
            strSql.Append(@" from [dbo].[mk_plc_pidai_values] a  where 1=1");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.StartDate.ParseToString()))
                {
                    strSql.Append(" AND a.create_Time >= @StartDate");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@StartDate", param.StartDate));
                }
                if (!string.IsNullOrEmpty(param.EndDate.ParseToString()))
                {
                    param.EndDate = (param.EndDate.Value.ToString("yyyy-MM-dd") + " 23:59:59").ParseToDateTime();
                    strSql.Append(" AND a.create_Time <= @EndTime");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@EndTime", param.EndDate));
                }
                if (!string.IsNullOrEmpty(param.DictId.ParseToString()))
                {
               
                    strSql.Append(" AND a.dict_Id <= @DictIda");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@DictIda", param.DictId));
                }
                if (!string.IsNullOrEmpty(param.Frequency.ParseToString()))
                {
                    string startDates = string.Empty;
                    string endDates = string.Empty;
                    if (param.Frequency == "早班")
                    {
                        startDates = "00:00:00";
                        endDates = "07:59:59";

                    }
                    else if (param.Frequency == "中班")
                    {
                        startDates = "08:00:00";
                        endDates = "15:59:59";
                    }
                    else if (param.Frequency == "晚班")
                    {
                        startDates = "16:00:00";
                        endDates = "23:59:59";
                    }
                    strSql.Append(" AND CONVERT(varchar(100), a.create_Time, 8)>=@StartDates ");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@StartDates", startDates));

                    strSql.Append("AND CONVERT(varchar(100), a.create_Time, 8)<=@EndDates ");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@EndDates", endDates));
                }
            }

            return parameter;
        }
        #endregion
    }
}