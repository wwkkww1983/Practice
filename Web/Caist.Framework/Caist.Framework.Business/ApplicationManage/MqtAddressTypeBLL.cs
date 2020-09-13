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
    public class MqtAddressTypeBLL
    {
        private MqtAddressTypeService Service = new MqtAddressTypeService();


        #region 获取数据
        public async Task<TData<List<MqtAddressTypeEntity>>> GetList(MqtAddressTypeParam param)
        {
            var obj = new TData<List<MqtAddressTypeEntity>>();
            List<MqtAddressTypeEntity> list = await Service.GetList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<MqtAddressTypeEntity>>> GetPageList(MqtAddressTypeParam param, Pagination pagination)
        {
            TData<List<MqtAddressTypeEntity>> obj = new TData<List<MqtAddressTypeEntity>>();
            obj.Result = await Service.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<MqtAddressTypeEntity>>> GetPageContentList(MqtAddressTypeParam param, Pagination pagination)
        {
            TData<List<MqtAddressTypeEntity>> obj = new TData<List<MqtAddressTypeEntity>>();
            obj.Result = await Service.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<MqtAddressTypeEntity>> GetEntity(long id)
        {
            TData<MqtAddressTypeEntity> obj = new TData<MqtAddressTypeEntity>();
            obj.Result = await Service.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

      
        #endregion
    }
}
