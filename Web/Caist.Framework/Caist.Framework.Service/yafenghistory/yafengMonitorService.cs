using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.yafenghistory;
using Caist.Framework.Model.yafenghistory;
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

namespace Caist.Framework.Service.yafenghistory
{
  public class yafengMonitorService: RepositoryFactory
    {
        #region 获取数据
        public async Task<List<yafengMonitorEntity>> GetSecurityInfoList(yafengMonitorParam param)
        {
            var strSql = new StringBuilder();
            List<DbParameter> filter = ListFilter(param, strSql);
            var list = await this.BaseRepository().FindList<yafengMonitorEntity>(strSql.ToString(), filter.ToArray());
            return list.ToList();
        }
        #endregion

        #region 私有方法

        private List<DbParameter> ListFilter(yafengMonitorParam param, StringBuilder strSql)
        {
            strSql.Append(@"SELECT top(1000) a.[dict_Id ] as dictId,a.dict_Value as dictValue, a.create_Time as CreateTime");
            strSql.Append(@" from [dbo].[mk_plc_yafeng_values] a  where 1=1");
            var parameter = new List<DbParameter>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.StartDate.ParseToString()))
                {
                    //开始
                    strSql.Append(" AND a.create_Time >= @StartDate");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@StartDate", param.StartDate));
                }
                if (!string.IsNullOrEmpty(param.EndDate.ParseToString()))
                {
                    //结束
                    param.EndDate = (param.EndDate.Value.ToString("yyyy-MM-dd") + " 23:59:59").ParseToDateTime();
                    strSql.Append(" AND a.create_Time <= @EndTime");
                    parameter.Add(DbParameterExtension.CreateDbParameter("@EndTime", param.EndDate));
                }
                if (!string.IsNullOrEmpty(param.DictId.ParseToString()))
                {
                    //标识符ID
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
