using Caist.Framework.Business.Cewenhistory;
using Caist.Framework.Business.FiberManage;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Model.Param.Cewenhistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.FiberDemo.Controllers
{
    [Area("FiberDemo")]
    public class FiberController : Controller
    {
        private CewenMonitorBLL fiberBLL = new CewenMonitorBLL();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GetFiberTempLists([FromBody]CewenMonitorParam param)
        {
            var obj = await fiberBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
        public async Task<string> GetFiberTempListsr([FromBody]CewenMonitorParam param)
        {
            var obj = await fiberBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }

        private IHostingEnvironment _hostingEnvironment;

        public FiberController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// excel导出功能
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// excel导出功能
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Export(CewenMonitorParam tfj)
        {
            //100天以前数据
            //tfj.StartDate = DateTime.Now.AddDays(-100);
            //tfj.EndDate = DateTime.Now;
            var dataFromDb = fiberBLL.GetSecurityInfoList(tfj);
            //
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = $"{Guid.NewGuid()}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));  //Path.Combine把多个字符串组成一个路径
            using (ExcelPackage package = new ExcelPackage(file))   //ExcelPackage 操作excel的主要对象
            {
                //如果之前有同名的文件先删除然后重新创建 
                var count = package.Workbook.Worksheets.Count; if (count > 0)
                {
                    for (var i = 0; i < count; i++)
                    {
                        package.Workbook.Worksheets.Delete(i + 1);
                    }
                    package.File.Delete();
                }
                // 添加worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("光纤Excel");
                //添加头
                worksheet.Cells[1, 1].Value = "名称";
                worksheet.Cells[1, 2].Value = "最大值";
                worksheet.Cells[1, 3].Value = "最小值";
                var result = dataFromDb.Result.Result;
                if (result != null)
                {
                    //添加值
                    for (var i = 0; i < result.Count; i++)
                    {
                        worksheet.Cells[$"A{i + 2}"].Value = result[i].areaName;
                        worksheet.Cells[$"B{i + 2}"].Value = result[i].maxValue;
                        worksheet.Cells[$"C{i + 2}"].Value = result[i].minValue;
                        worksheet.Cells[$"C3"].Style.Font.Bold = true;
                }
                }

                package.Save();
            }
            return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}