using Caist.Framework.Entity.PointManage;
using Caist.Framework.Model.Param.PointManage;
using Caist.Framework.Service.PointManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.PointManage
{
    public class InstructBLL
    {
        private InstructService instructService = new InstructService();
        #region 获取数据
        public async Task<TData<List<InstructEntity>>> GetList(InstructListParam param)
        {
            TData<List<InstructEntity>> obj = new TData<List<InstructEntity>>();
            obj.Result = await instructService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<InstructEntity>>> GetPageList(InstructListParam param, Pagination pagination)
        {
            TData<List<InstructEntity>> obj = new TData<List<InstructEntity>>();
            obj.Result = await instructService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<InstructEntity>>> GetPageContentList(InstructListParam param, Pagination pagination)
        {
            TData<List<InstructEntity>> obj = new TData<List<InstructEntity>>();
            obj.Result = await instructService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<InstructEntity>> GetEntity(long id)
        {
            TData<InstructEntity> obj = new TData<InstructEntity>();
            obj.Result = await instructService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(InstructEntity entity)
        {
            TData<string> obj = new TData<string>();
            await instructService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await instructService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}
