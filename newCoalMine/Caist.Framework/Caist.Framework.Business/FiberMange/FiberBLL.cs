using Caist.Framework.Entity.FiberManage;
using Caist.Framework.Entity.PeopleManage;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Model.PeopleManage;
using Caist.Framework.Service.FiberManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Business.FiberManage
{
    public class FiberBLL
    {
        private FiberService fiberService = new FiberService();

        //private MqThemeCache mqThemeCache = new MqThemeCache();

        #region 获取数据
        public async Task<TData<List<CableEntity>>> GetFiberInfoList(FiberParam param)
        {
            var obj = new TData<List<CableEntity>>();
            var list = await fiberService.GetFiberInfoList(param);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }

     
        #endregion

        #region 提交数据
        //public async Task<TData<string>> SaveForm(RegionEntity entity)
        //{
        //    TData<string> obj = new TData<string>();
        //    await fiberService.SaveForm(entity);
        //    obj.Result = entity.Id.ParseToString();
        //    obj.Tag = 1;
        //    return obj;
        //}

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await fiberService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        //private List<RegionEntity> ListFilter(RegionParam param, List<RegionEntity> list)
        //{
            //if (param != null)
            //{
            //    if (!string.IsNullOrEmpty(param.MqHost))
            //    {
            //        list = list.Where(p => p.MqHost.Contains(param.MqHost)).ToList();
            //    }
            //    if (!string.IsNullOrEmpty(param.MqUser))
            //    {
            //        list = list.Where(p => p.MqUser == param.MqUser).ToList();
            //    }
            //}
        //    return list;
        //}
        #endregion
    }
}
