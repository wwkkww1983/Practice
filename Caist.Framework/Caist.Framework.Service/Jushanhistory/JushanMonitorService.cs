using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.Jushanhistory;
using Caist.Framework.Model.Param.Jushanhistory;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.Jushanhistory
{
   public class JushanMonitorService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<JushanMonitorEntity>> GetSecurityInfoList(JushanMonitorParam param)
        {
            var expression = ListFilter(param);
         
            var list = await this.BaseRepository().FindList<JushanMonitorEntity>(expression);

            return list.OrderBy(p => p.createTime).Take(1000).ToList();
        }

        #endregion

        #region 私有方法
        private Expression<Func<JushanMonitorEntity, bool>> ListFilter(JushanMonitorParam param)
        {
            var expression = LinqExtensions.True<JushanMonitorEntity>();
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

