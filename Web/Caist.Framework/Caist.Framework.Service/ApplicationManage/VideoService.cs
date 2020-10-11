using Caist.Framework.Data;
using Caist.Framework.Data.Repository;
using Caist.Framework.Entity.SystemManage;
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
    public class VideoService : RepositoryFactory
    {
        #region 获取数据
        public async Task<List<VideoEntity>> GetList(VideoListParam param)
        {
            var expression = ListFilter(param);
            var list = await this.BaseRepository().FindList<VideoEntity>(expression);
            return list.OrderBy(p => p.SortOrder).ToList();
        }

        public async Task<List<VideoEntity>> GetPageList(VideoListParam param, Pagination pagination)
        {
            var strSql = new StringBuilder();
            var filter = ListFilter(param);
            var list = await this.BaseRepository().FindList<VideoEntity>(filter, pagination);

            return list.ToList();
        }

        public async Task<List<VideoEntity>> GetPageContentList(VideoListParam param, Pagination pagination)
        {
            var filter = ListFilter(param);
            var list = await this.BaseRepository().FindList<VideoEntity>(filter, pagination);
            return list.ToList();
        }

        public async Task<VideoEntity> GetEntity(long id)
        {
            return await this.BaseRepository().FindEntity<VideoEntity>(id);
        }

        public async Task<int> GetMaxSort()
        {
            object result = await this.BaseRepository().FindObject("SELECT MAX(SortOrder) FROM [caist_mk_db].dbo.mk_eb_video");
            int sort = result.ParseToInt();
            sort++;
            return sort;
        }
        #endregion

        #region 提交数据
        public async Task SaveForm(VideoEntity entity)
        {
            if (entity.Id.IsNullOrZero())
            {
                await entity.Create();
                await this.BaseRepository().Insert<VideoEntity>(entity);
            }
            else
            {
                await entity.Modify();
                await this.BaseRepository().Update<VideoEntity>(entity);
            }
        }

        public async Task DeleteForm(string ids)
        {
            long[] idArr = TextHelper.SplitToArray<long>(ids, ',');
            await this.BaseRepository().Delete<VideoEntity>(idArr);
        }
        #endregion

        #region 私有方法
        private Expression<Func<VideoEntity, bool>> ListFilter(VideoListParam param)
        {
            var expression = LinqExtensions.True<VideoEntity>();
            if (param != null)
            {
                if (param.Name.HasValue())
                {
                    expression = expression.And(t => t.Name == param.Name);
                }
                if (param.SystemId.HasValue())
                {
                    expression = expression.And(t => t.Area == param.SystemId);
                }
                if (param.Number.HasValue())
                {
                    expression = expression.And(t => t.Number == param.Number);
                }
            }
            return expression;
        }
        #endregion
    }
}
