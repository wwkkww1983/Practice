using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.yafenghistory;
using Caist.Framework.Model.yafenghistory;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.yafenghistory
{
  public class yafengMonitorService: RepositoryFactory
    {
        #region 获取数据
        public  async Task<List<yafengMonitorEntity>> GetSecurityInfoList(yafengMonitorParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList<yafengMonitorEntity>(expression);
            return list.OrderBy(p => p.createTime).Take(1000).ToList();
        }

        #endregion

        #region 私有方法
        private Expression<Func<yafengMonitorEntity, bool>> ListFilter(yafengMonitorParam param)
        {
            var expression = LinqExtensions.True<yafengMonitorEntity>();

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
