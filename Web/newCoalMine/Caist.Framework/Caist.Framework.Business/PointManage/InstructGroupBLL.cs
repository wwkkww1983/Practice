using Caist.Framework.Entity.PointManage;
using Caist.Framework.Model.Param.PointManage;
using Caist.Framework.Service.PointManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.PointManage
{
    public class InstructGroupBLL
    {
        private InstructGroupService instructGroupService = new InstructGroupService();
        #region 获取数据
        public async Task<TData<List<InstructGroupEntity>>> GetList(InstructGroupListParam param)
        {
            TData<List<InstructGroupEntity>> obj = new TData<List<InstructGroupEntity>>();
            obj.Result = await instructGroupService.GetList(param);
            obj.TotalCount = obj.Result.Count;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<InstructGroupEntity>>> GetPageList(InstructGroupListParam param, Pagination pagination)
        {
            TData<List<InstructGroupEntity>> obj = new TData<List<InstructGroupEntity>>();
            obj.Result = await instructGroupService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<InstructGroupEntity>>> GetPageContentList(InstructGroupListParam param, Pagination pagination)
        {
            TData<List<InstructGroupEntity>> obj = new TData<List<InstructGroupEntity>>();
            obj.Result = await instructGroupService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<InstructGroupEntity>> GetEntity(long id)
        {
            TData<InstructGroupEntity> obj = new TData<InstructGroupEntity>();
            obj.Result = await instructGroupService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(InstructGroupEntity entity)
        {
            TData<string> obj = new TData<string>();
            await instructGroupService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await instructGroupService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        #endregion
    }
}