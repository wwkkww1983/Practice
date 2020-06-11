using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Business.ApplicationManage
{
    public class ViewManipulateModelBLL
    {
        private ViewManipulateModelService viewManipulateModelService = new ViewManipulateModelService();

        #region 获取数据
        public async Task<TData<List<ViewManipulateModelEntity>>> GetList(ViewManipulateModelListParam param)
        {
            TData<List<ViewManipulateModelEntity>> obj = new TData<List<ViewManipulateModelEntity>>();
            obj.Result = await viewManipulateModelService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ViewManipulateModelEntity>>> GetPageList(ViewManipulateModelListParam param, Pagination pagination)
        {
            TData<List<ViewManipulateModelEntity>> obj = new TData<List<ViewManipulateModelEntity>>();
            obj.Result = await viewManipulateModelService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ViewManipulateModelEntity>>> GetPageContentList(ViewManipulateModelListParam param, Pagination pagination)
        {
            TData<List<ViewManipulateModelEntity>> obj = new TData<List<ViewManipulateModelEntity>>();
            obj.Result = await viewManipulateModelService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ViewManipulateModelEntity>> GetEntity(long id)
        {
            TData<ViewManipulateModelEntity> obj = new TData<ViewManipulateModelEntity>();
            obj.Result = await viewManipulateModelService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await viewManipulateModelService.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(ViewManipulateModelEntity entity)
        {
            TData<string> obj = new TData<string>();
            //添加新数据时，检查当前类别数据条数是否超限
            if (entity.Id.IsNullOrZero())
            {
                object Count = await viewManipulateModelService.ExistViewFunctionId(entity);
                if (Count.ParseToInt() >= Util.GlobalContext.SystemConfig.AddModelLimit)
                {
                    obj.Message = "当前类型数据模块超限制";
                    obj.Tag = -1;
                    return obj;
                }
            }
            await viewManipulateModelService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await viewManipulateModelService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
