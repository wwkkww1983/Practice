using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.PointManage;
using Caist.Framework.Entity.PointManage;
using Caist.Framework.Model.Param.PointManage;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.PointManage.Controllers
{
    [Area("PointManage")]
    public class DistributionController : BaseController
    {
        private DistributionBLL deviceBLL = new DistributionBLL();
        #region 视图功能
        [AuthorizeFilter("distribution:distribution:view")]
        public IActionResult distribution()
        {
            return View();
        }
        public async Task<IActionResult> distributionForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("distribution:distribution:search")]
        public async Task<IActionResult> GetListJson(DistributionParam param)
        {
            TData<List<DistributionEntity>> obj = await deviceBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("distribution:distribution:search")]
        public async Task<IActionResult> GetPageListJson(DistributionParam param, Pagination pagination)
        {
            TData<List<DistributionEntity>> obj = await deviceBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<DistributionEntity> obj = await deviceBLL.GetEntity(id);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetMaxSortJson()
        {
            TData<int> obj = await deviceBLL.GetMaxSort();
            return Json(obj);
        }
        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("distribution:distribution:add,distribution:distribution:edit")]
        public async Task<IActionResult> SaveFormJson(DistributionEntity entity)
        {
            TData<string> obj = await deviceBLL.SaveForm(entity);
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("distribution:distribution:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await deviceBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}