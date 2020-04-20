using Caist.Framework.Cache;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.Framework.Business.ApplicationManage
{
    public class ViewFunctionBLL
    {
        private ViewFunctionService viewFunctionService = new ViewFunctionService();
        private ViewFunctionCache viewFunctionCache = new ViewFunctionCache();

        #region 获取数据
        public async Task<TData<List<ViewFunctionEntity>>> GetList(ViewFunctionListParam param)
        {
            var obj = new TData<List<ViewFunctionEntity>>();

            List<ViewFunctionEntity> list = await viewFunctionService.GetList(param);
            list = ListFilter(param, list);

            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<ViewFunctionEntity>>> GetPageContentList(ViewFunctionListParam param, Pagination pagination)
        {
            TData<List<ViewFunctionEntity>> obj = new TData<List<ViewFunctionEntity>>();
            obj.Result = await viewFunctionService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<ViewFunctionEntity>> GetEntity(long id)
        {
            TData<ViewFunctionEntity> obj = new TData<ViewFunctionEntity>();
            obj.Result = await viewFunctionService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await viewFunctionService.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(ViewFunctionEntity entity)
        {
            TData<string> obj = new TData<string>();
            if (viewFunctionService.ExistMenuName(entity))
            {
                obj.Message = "菜单名称已经存在！";
                return obj;
            }
            await viewFunctionService.SaveForm(entity);

            viewFunctionCache.Remove();

            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await viewFunctionService.DeleteForm(ids);

            viewFunctionCache.Remove();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        private List<ViewFunctionEntity> ListFilter(ViewFunctionListParam param, List<ViewFunctionEntity> list)
        {
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.ViewName))
                {
                    list = list.Where(p => p.ViewName.Contains(param.ViewName)).ToList();
                }
                if (param.ViewStatus > 0)
                {
                    list = list.Where(p => p.ViewStatus == param.ViewStatus).ToList();
                }
            }
            return list;
        }
        #endregion
    }
}
