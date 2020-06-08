using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity.SystemManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.ApplicationManage.Controllers
{
    [Area("ApplicationManage")]
    public class VideoController : BaseController
    {
        private readonly VideoBLL videoBLL = new VideoBLL();
        #region 视图功能
        [AuthorizeFilter("application:video:view")]
        public IActionResult VideoIndex()
        {
            return View();
        }

        public async Task<IActionResult> VideoForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("application:video:search")]
        public async Task<IActionResult> GetListJson(VideoListParam param)
        {
            TData<List<VideoEntity>> obj = await videoBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("application:video:search")]
        public async Task<IActionResult> GetPageListJson(VideoListParam param, Pagination pagination)
        {
            //Area参数和路由冲突导致每次都带有ApplicationManage  路由值进来，这里用不到Area参数屏蔽掉
            param.Area = string.Empty;
            TData<List<VideoEntity>> obj = await videoBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<VideoEntity> obj = await videoBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await videoBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("application:video:add,application:video:edit")]
        public async Task<IActionResult> SaveFormJson(VideoEntity entity)
        {
            TData<string> obj = await videoBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("application:video:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await videoBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion

    }
}