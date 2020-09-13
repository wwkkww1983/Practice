using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.Security;
using Caist.Framework.Model.Security;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Caist.Framework.Service.Security
{
    public class SecurityMonitorService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<SecurityMonitorEntity>> GetSecurityInfoList(SecurityMonitorParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        #endregion

        #region 私有方法
        private Expression<Func<SecurityMonitorEntity, bool>> ListFilter(SecurityMonitorParam param)
        {
            var expression = LinqExtensions.True<SecurityMonitorEntity>();
            //if (param != null)
            //{
            //}
            return expression;
        }
        #endregion
    }
}
