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
    public class InformationPublishBLL
    {
        private InformationPublishService informationPublishService = new InformationPublishService();

        #region 获取数据
        public async Task<TData<List<InformationPublishEntity>>> GetList(InformationPublishParam param)
        {
            var obj = new TData<List<InformationPublishEntity>>();
            List<InformationPublishEntity> list = await informationPublishService.GetList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<InformationPublishEntity>>> GetPageList(InformationPublishParam param, Pagination pagination)
        {
            TData<List<InformationPublishEntity>> obj = new TData<List<InformationPublishEntity>>();
            obj.Result = await informationPublishService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

       
        public async Task<TData<InformationPublishEntity>> GetEntity(long id)
        {
            TData<InformationPublishEntity> obj = new TData<InformationPublishEntity>();
            obj.Result = await informationPublishService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(InformationPublishEntity entity)
        {
            TData<string> obj = new TData<string>();
            await informationPublishService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await informationPublishService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion
    }
}
