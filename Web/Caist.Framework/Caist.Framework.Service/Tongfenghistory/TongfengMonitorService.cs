using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.Tongfenghistory;
using Caist.Framework.Model.Param.Tongfenghistory;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.Tongfenghistory
{
   public class TongfengMonitorService : RepositoryFactory
    {

        #region 获取数据
        public async Task<List<TongfengMonitorEntity>> GetSecurityInfoList(TongfengMonitorParam param)
        {
            var expression = ListFilter(param);
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append(@"select top(1000) 
            //dict_Id as dictId,dict_Id as dictId,dict_Value as dictValue,create_Time as createTime
            //from [dbo].[mk_plc_tongfeng_values]");
            //strSql.Append("where  DATEDIFF(HOUR,getdate(),[create_Time]) <=1");
            var list = await this.BaseRepository().FindList<TongfengMonitorEntity>(expression);
            return list.OrderBy(p => p.createTime).Take(1000).ToList();
        }

        #endregion
        #region 提交数据


        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<TongfengMonitorEntity>(idArr);
        }
        #endregion
        #region 私有方法
        private Expression<Func<TongfengMonitorEntity, bool>> ListFilter(TongfengMonitorParam param)
        {
            var expression = LinqExtensions.True<TongfengMonitorEntity>();
            if (param != null)
            {
                if (param.StartDate.HasValue())
                {
                    expression = expression.And(t => t.createTime >= param.StartDate);
                }
                if (param.EndDate.HasValue())
                {
                    expression = expression.And(t => t.createTime <=  param.EndDate);
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
