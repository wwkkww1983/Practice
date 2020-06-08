using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.ApplicationManage
{
    public class ViewParamenterBLL
    {
        private ViewParamenterService viewParamenterService = new ViewParamenterService();

        #region 获取数据
        public async Task<TData<List<ViewParamenterEntity>>> GetList(ViewParamenterListParam param)
        {
            TData<List<ViewParamenterEntity>> obj = new TData<List<ViewParamenterEntity>>();
            obj.Result = await viewParamenterService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ViewParamenterEntity>>> GetPageList(ViewParamenterListParam param, Pagination pagination)
        {
            TData<List<ViewParamenterEntity>> obj = new TData<List<ViewParamenterEntity>>();
            obj.Result = await viewParamenterService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ViewParamenterEntity>>> GetPageContentList(ViewParamenterListParam param, Pagination pagination)
        {
            TData<List<ViewParamenterEntity>> obj = new TData<List<ViewParamenterEntity>>();
            obj.Result = await viewParamenterService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ViewParamenterEntity>> GetEntity(long id)
        {
            TData<ViewParamenterEntity> obj = new TData<ViewParamenterEntity>();
            obj.Result = await viewParamenterService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await viewParamenterService.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(ViewParamenterEntity entity)
        {
            TData<string> obj = new TData<string>();
            await viewParamenterService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await viewParamenterService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
