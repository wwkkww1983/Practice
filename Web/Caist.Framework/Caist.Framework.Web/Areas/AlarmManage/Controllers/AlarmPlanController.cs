using Caist.Framework.Business.AlarmManage;
using Caist.Framework.Entity.AlarmManage;
using Caist.Framework.Model.Param.OrganizationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.AlarmManage.Controllers
{
    [Area("AlarmManage")]
    public class AlarmPlanController : BaseController
    {
        private readonly AlarmPlanBLL planBLL = new AlarmPlanBLL();
        private readonly FileController fileController = new FileController();

        #region 视图功能
        [AuthorizeFilter("alarmplan:alarmplan:view")]
        public IActionResult AlarmPlanIndex()
        {
            return View();
        }

        public async Task<IActionResult> AlarmPlanForm()
        {
            ViewBag.OperatorInfo = await Operator.Instance.Current();
            return View();
        }
        #endregion

        #region 获取数据
        [HttpGet]
        [AuthorizeFilter("alarmplan:alarmplan:search")]
        public async Task<IActionResult> GetListJson(AlarmPlanListParam param)
        {
            TData<List<AlarmPlanEntity>> obj = await planBLL.GetList(param);
            return Json(obj);
        }

        [HttpGet]
        [AuthorizeFilter("alarmplan:alarmplan:search")]
        public async Task<IActionResult> GetPageListJson(AlarmPlanListParam param, Pagination pagination)
        {
            TData<List<AlarmPlanReturnEntity>> obj = await planBLL.GetPageList(param, pagination);
            return Json(obj);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormJson(long id)
        {
            TData<AlarmPlanEntity> obj = await planBLL.GetEntity(id);
            return Json(obj);
        }

        #endregion

        #region 提交数据
        [HttpPost]
        [AuthorizeFilter("alarmplan:alarmplan:add,organization:alarmplan:edit")]
        public async Task<IActionResult> SaveFormJson(AlarmPlanParam entity)
        {
            TData<string> obj = await planBLL.SaveForm(new AlarmPlanEntity()
            {
                AlarmName = entity.AlarmName,
                SysId = entity.SysId,
                AlarmField = entity.AlarmField,
                Remark = entity.Remark,
                Enable = entity.Enable,
                Id = entity.Id
            });
            if (obj.Result.HasValue())
            {
                entity.Files.ForEach(p => p.ModuleId = long.Parse(obj.Result));
                //调用文件表保存接口
                await fileController.SaveFormJson(entity.Files);
            }
            return Json(obj);
        }

        [HttpPost]
        [AuthorizeFilter("alarmplan:alarmplan:delete")]
        public async Task<IActionResult> DeleteFormJson(string ids)
        {
            TData obj = await planBLL.DeleteForm(ids);
            return Json(obj);
        }
        #endregion
    }
}
