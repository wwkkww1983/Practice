using Caist.Framework.Entity.SystemManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.ApplicationManage
{
    public class VideoBLL
    {
        private readonly VideoService videoService = new VideoService();

        #region 获取数据
        public async Task<TData<List<VideoEntity>>> GetList(VideoListParam param)
        {
            TData<List<VideoEntity>> obj = new TData<List<VideoEntity>>();
            obj.Result = await videoService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<VideoEntity>>> GetPageList(VideoListParam param, Pagination pagination)
        {
            TData<List<VideoEntity>> obj = new TData<List<VideoEntity>>();
            obj.Result = await videoService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<VideoEntity>>> GetPageContentList(VideoListParam param, Pagination pagination)
        {
            TData<List<VideoEntity>> obj = new TData<List<VideoEntity>>();
            obj.Result = await videoService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<VideoEntity>> GetEntity(long id)
        {
            TData<VideoEntity> obj = new TData<VideoEntity>();
            obj.Result = await videoService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await videoService.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(VideoEntity entity)
        {
            TData<string> obj = new TData<string>();
            await videoService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await videoService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
