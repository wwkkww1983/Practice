using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.SubStation;
using Caist.Framework.Model.SubStation;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Caist.Framework.Service.SubStation
{
    public class SubStationService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<SubStationEntity>> GetSubStationInfoList(SubStationParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.OrderBy(p => p.SysId).ToList();
        }

        #endregion

        #region 私有方法
        private Expression<Func<SubStationEntity, bool>> ListFilter(SubStationParam param)
        {
            var expression = LinqExtensions.True<SubStationEntity>();
            if (param != null)
            {
                if (param.Dianwei.HasValue())
                {
                    expression = expression.And(t => t.DianWei == param.Dianwei);
                }
            }
            return expression;
        }
        #endregion
    }
}
