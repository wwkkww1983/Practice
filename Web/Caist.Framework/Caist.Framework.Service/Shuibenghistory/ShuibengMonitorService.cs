using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.Shuibenghistory;
using Caist.Framework.Model.Param.Shuibenghistory;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.Shuibenghistory
{
   public class ShuibengMonitorService :RepositoryFactory
    {
        #region 获取数据
        public async Task<List<ShuibengMonitorEntity>> GetSecurityInfoList(ShuibengMonitorParam param)
        {
            var expression = ListFilter(param);
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append(@"select top(1000) 
            //dict_Id as dictId,dict_Id as dictId,dict_Value as dictValue,create_Time as createTime
            //from [dbo].[mk_plc_shuibeng_values]");
            //strSql.Append("where  DATEDIFF(HOUR,getdate(),[create_Time]) <=1");
            var list = await this.BaseRepository().FindList<ShuibengMonitorEntity>(expression);
            return list.OrderBy(p => p.createTime).Take(1000).ToList();
        }

        #endregion

        #region 私有方法
        private Expression<Func<ShuibengMonitorEntity, bool>> ListFilter(ShuibengMonitorParam param)
        {
            var expression = LinqExtensions.True<ShuibengMonitorEntity>();
            if (param != null)
            {
                if (param.StartDate.HasValue())
                {
                    expression = expression.And(t => t.createTime >= param.StartDate);
                }
                if (param.EndDate.HasValue())
                {
                    expression = expression.And(t => t.createTime <= param.EndDate);
                }
            }
            //if (param != null)
            //{
            //}
            return expression;
        }
        #endregion
    }
}

