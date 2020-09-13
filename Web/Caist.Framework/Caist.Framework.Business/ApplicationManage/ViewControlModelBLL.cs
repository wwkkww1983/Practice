using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.ApplicationManage
{
    public class ViewControlModelBLL
    {
        private ViewControlModelService viewControlModelService = new ViewControlModelService();

        #region 获取数据
        public async Task<TData<List<ViewControlModelEntity>>> GetList(ViewControlModelListParam param)
        {
            TData<List<ViewControlModelEntity>> obj = new TData<List<ViewControlModelEntity>>();
            obj.Result = await viewControlModelService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ViewControlModelEntity>>> GetPageList(ViewControlModelListParam param, Pagination pagination)
        {
            TData<List<ViewControlModelEntity>> obj = new TData<List<ViewControlModelEntity>>();
            obj.Result = await viewControlModelService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ViewControlModelEntity>>> GetPageContentList(ViewControlModelListParam param, Pagination pagination)
        {
            TData<List<ViewControlModelEntity>> obj = new TData<List<ViewControlModelEntity>>();
            obj.Result = await viewControlModelService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ViewControlModelEntity>> GetEntity(long id)
        {
            TData<ViewControlModelEntity> obj = new TData<ViewControlModelEntity>();
            obj.Result = await viewControlModelService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await viewControlModelService.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(ViewControlModelEntity entity)
        {
            TData<string> obj = new TData<string>();
            await viewControlModelService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await viewControlModelService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
