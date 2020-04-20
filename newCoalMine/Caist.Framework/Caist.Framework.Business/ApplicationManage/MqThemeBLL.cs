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
    public class MqThemeBLL
    {
        private MqThemeService mqThemeService = new MqThemeService();

        private MqThemeCache mqThemeCache = new MqThemeCache();

        #region 获取数据
        public async Task<TData<List<MqThemeEntity>>> GetList(MqThemeListParam param)
        {
            var obj = new TData<List<MqThemeEntity>>();
            List<MqThemeEntity> list = await mqThemeCache.GetList();
            list = ListFilter(param, list);
            obj.Result = list;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<MqThemeEntity>>> GetPageList(MqThemeListParam param, Pagination pagination)
        {
            TData<List<MqThemeEntity>> obj = new TData<List<MqThemeEntity>>();
            obj.Result = await mqThemeService.GetPageList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<List<MqThemeEntity>>> GetPageContentList(MqThemeListParam param, Pagination pagination)
        {
            TData<List<MqThemeEntity>> obj = new TData<List<MqThemeEntity>>();
            obj.Result = await mqThemeService.GetPageContentList(param, pagination);
            obj.TotalCount = pagination.TotalCount;
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<MqThemeEntity>> GetEntity(long id)
        {
            TData<MqThemeEntity> obj = new TData<MqThemeEntity>();
            obj.Result = await mqThemeService.GetEntity(id);
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData<int>> GetMaxSort()
        {
            TData<int> obj = new TData<int>();
            obj.Result = await mqThemeService.GetMaxSort();
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        public async Task<TData<string>> SaveForm(MqThemeEntity entity)
        {
            TData<string> obj = new TData<string>();
            await mqThemeService.SaveForm(entity);
            obj.Result = entity.Id.ParseToString();
            obj.Tag = 1;
            return obj;
        }

        public async Task<TData> DeleteForm(string ids)
        {
            TData obj = new TData();
            await mqThemeService.DeleteForm(ids);
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 私有方法
        private List<MqThemeEntity> ListFilter(MqThemeListParam param, List<MqThemeEntity> list)
        {
            if (param != null)
            {
                if (!string.IsNullOrEmpty(param.MqHost))
                {
                    list = list.Where(p => p.MqHost.Contains(param.MqHost)).ToList();
                }
                if (!string.IsNullOrEmpty(param.MqUser))
                {
                    list = list.Where(p => p.MqUser == param.MqUser).ToList();
                }
            }
            return list;
        }
        #endregion
    }
}
