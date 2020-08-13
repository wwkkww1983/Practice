using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.Cewenhistory;
using Caist.Framework.Model.Param.Cewenhistory;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
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
            strSql.Append(@"select top(1000) 
            area_Name as areaName,max_value as maxValue,min_Value as minValue,create_Time as createTime
            from [dbo].[mk_plc_cewen_values]");
            strSql.Append("where  DATEDIFF(HOUR,getdate(),[create_Time]) <=1");
            var list = await this.BaseRepository().FindList<CewenMonitorEntity>(strSql.ToString());
            return list.OrderBy(p => p.createTime).ToList();
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
