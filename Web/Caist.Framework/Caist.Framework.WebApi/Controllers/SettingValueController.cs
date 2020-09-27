using Caist.Framework.Business.SettingValueManage;
using Caist.Framework.Entity.SettingValueManage;
using Caist.Framework.Model.Param.SettingValueManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Caist.Framework.WebApi.Handle;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [AuthorizeFilter]
    public class SettingValueController : ControllerBase
    {
        SettingValueBLL settingValueBLL = new SettingValueBLL();

        #region 获取数据
        [HttpGet]
        public async Task<TData<List<SettingValueEntity>>> GetListJson([FromQuery] SettingValueListParam param)
        {
            TData<List<SettingValueEntity>> obj = await settingValueBLL.GetList(param);
            return obj;
        }

        [HttpGet]
        public async Task<TData<List<SettingValueEntity>>> GetPageListJson([FromQuery] SettingValueListParam param, [FromQuery] Pagination pagination)
        {
            TData<List<SettingValueEntity>> obj = await settingValueBLL.GetPageList(param, pagination);


            return obj;
        }

        [HttpGet]
        public async Task<TData<SettingValueEntity>> GetFormJson([FromQuery] long id)
        {
            TData<SettingValueEntity> obj = await settingValueBLL.GetEntity(id);
            return obj;
        }

        /// <summary>
        /// 设置参数后 修改参数设置得值，只做更新，不做插入数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TData<string>> SaveForm([FromQuery] SettingValueEntity entity)
        {
            TData<string> obj = new TData<string>();
            obj = await settingValueBLL.UpdateEntity(entity);
            return obj;
        }

        #endregion
    }
}