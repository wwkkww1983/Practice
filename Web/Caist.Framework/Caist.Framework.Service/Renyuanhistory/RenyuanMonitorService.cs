using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.Renyuanhistory;
using Caist.Framework.Model.Param.Renyuanhistory;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Service.Renyuanhistory
{
   public class RenyuanMonitorService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<RenyuanMonitorEntity>> GetSecurityInfoList(RenyuanMonitorParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList<RenyuanMonitorEntity>(@"select top(1000)* 
            from [dbo].[mk_plc_renyuan_values]
            where DATEDIFF(HOUR,getdate(),[create_Time]) <=1 order by [create_Time] desc");
            return list.OrderBy(p => p.createTime).ToList();
        }

        #endregion

        #region 私有方法
        private Expression<Func<RenyuanMonitorEntity, bool>> ListFilter(RenyuanMonitorParam param)
        {
            var expression = LinqExtensions.True<RenyuanMonitorEntity>();
            //if (param != null)
            //{
            //}
            return expression;
        }
        #endregion
    }
}




