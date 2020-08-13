using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
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

namespace Caist.Framework.Service.ApplicationManage
{
    public class InformationPublishService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<InformationPublishEntity>> GetList(InformationPublishParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression);
            return list.ToList();
        }

        public async Task<List<InformationPublishEntity>> GetPageList(InformationPublishParam param, Pagination pagination)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList(expression, pagination);
            return list.ToList();
        }

        public async Task<InformationPublishEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<InformationPublishEntity>(id);
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(InformationPublishEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<InformationPublishEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<InformationPublishEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<InformationPublishEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<InformationPublishEntity, bool>> ListFilter(InformationPublishParam param)
        {


            var expression = LinqExtensions.True<InformationPublishEntity>();
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.LinkContent))
                {
                    expression = expression.And(t => t.LinkContent.Contains(param.LinkContent));
                }
             
                if (!string.IsNullOrEmpty(param.StartTime.ParseToString()))
                {
                    expression = expression.And(t => t.BaseModifyTime >= param.StartTime);
                }
                if (!string.IsNullOrEmpty(param.EndTime.ParseToString()))
                {
                    param.EndTime = (param.EndTime.Value.ToString("yyyy-MM-dd") + " 23:59:59").ParseToDateTime();
                    expression = expression.And(t => t.BaseModifyTime <= param.EndTime);
                }
            }
            return expression;
        }
        #endregion
    }
}
