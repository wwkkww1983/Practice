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
    public class InstructController : BaseController
    {
        private InstructBLL instructBLL = new InstructBLL();

        #region 视图功能
        [AuthorizeFilter("instruct:instruct:view")]
        public IActionResult InstructIndex()
        {
            return View();
        }

        public async Task<IActionResult> InstructForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("instruct:instruct:search")]
        public async Task<IActionResult> GetListJson(InstructListParam param)
        {
            TData<List<InstructEntity>> obj = await instructBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("instruct:instruct:search")]
        public async Task<IActionResult> GetPageListJson(InstructListParam param, Pagination pagination)
        {//InstructReturnEntity
            TData<List <InstructReturnEntity>> obj = await instructBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<InstructEntity> obj = await instructBLL.GetEntity(id);
            return Json(obj);
        }

        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("instruct:instruct:add,instruct:instruct:edit")]
        public async Task<IActionResult> SaveFormJson(InstructEntity entity)
        {
            TData<string> obj = await instructBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("instruct:instruct:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await instructBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}