using Caist.Framework.Business.PointManage;
using Caist.Framework.Entity.PointManage;
using Caist.Framework.Model.Param.PointManage;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.PointManage.Controllers
{
    [Area("PointManage")]
    public class InstructGroupController : BaseController
    {
        private InstructGroupBLL instructGroupBLL = new InstructGroupBLL();

        #region 视图功能
        [AuthorizeFilter("instructgroup:instructgroup:view")]
        public IActionResult InstructGroupIndex()
        {
            return View();
        }

        public async Task<IActionResult> InstructGroupForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("instructgroup:instructgroup:search")]
        public async Task<IActionResult> GetListJson(InstructGroupListParam param)
        {
            TData<List<InstructGroupEntity>> obj = await instructGroupBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("instructgroup:instructgroup:search")]
        public async Task<IActionResult> GetPageListJson(InstructGroupListParam param, Pagination pagination)
        {
            TData<List<InstructGroupEntity>> obj = await instructGroupBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<InstructGroupEntity> obj = await instructGroupBLL.GetEntity(id);
            return Json(obj);
        }

        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("instructgroup:instructgroup:add,instructgroup:instructgroup:edit")]
        public async Task<IActionResult> SaveFormJson(InstructGroupEntity entity)
        {
            TData<string> obj = await instructGroupBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("instructgroup:instructgroup:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await instructGroupBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}