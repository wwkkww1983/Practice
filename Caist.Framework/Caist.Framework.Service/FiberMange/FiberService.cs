using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.FiberManage;
using Caist.Framework.Entity.PeopleManage;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Caist.Framework.Service.FiberManage
{
    public class FiberService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<CableEntity>> GetFiberInfoList(FiberParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.OrderBy(p => p.AreaName).ToList();
        }

        //public async Task<int> GetMaxSort()
        //{
        //    object result = await this.BaseRepository().FindObject("SELECT MAX(mq_stutas) FROM mk_mq_theme");
        //    int sort = result.ParseToInt();
        //    sort++;
        //    return sort;
        //}
        #endregion

        #region 提交数据
        public async Task SaveForm(RegionEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<RegionEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<RegionEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<RegionEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<CableEntity, bool>> ListFilter(FiberParam param)
        {
            var expression = LinqExtensions.True<CableEntity>();
            if (param != null)
            {
                if (param.AreaName.HasValue())
                {
                    expression = expression.And(t => t.AreaName.Contains(param.AreaName));
                }
            }
            return expression;
        }
        #endregion
    }
}
