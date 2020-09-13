using Caist.Framework.Business.FiberManage;
using Caist.Framework.Model.FiberManage;
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
        private FiberBLL fiberBLL = new FiberBLL();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GetFiberTempLists([FromBody]FiberParam param)
        {
            var obj = await fiberBLL.GetFiberInfoList(param);
            return obj.RemoveNullValue();
        }
        public async Task<string> GetFiberTempListsr([FromBody]FiberParam param)
        {
            var obj = await fiberBLL.GetFiberInfoList(param);
            return obj.RemoveNullValue();
        }
        //用来获取路径相关
        private IHostingEnvironment _hostingEnvironment;

        public FiberController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// excel导出功能
        /// </summary>
        /// <returns></returns>
        public IActionResult Export()
        {
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = $"{Guid.NewGuid()}.xlsx";
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));  //Path.Combine把多个字符串组成一个路径
            using (ExcelPackage package = new ExcelPackage(file))   //ExcelPackage 操作excel的主要对象
            {
                // 添加worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("aspnetcore");
                //添加头
                worksheet.Cells[1, 1].Value = "序号";
                worksheet.Cells[1, 2].Value = "名称";
                worksheet.Cells[1, 3].Value = "地址";
                //添加值1
                worksheet.Cells["A2"].Value = 1000;
                worksheet.Cells["B2"].Value = "baidu";
                worksheet.Cells["C2"].Value = "https://www.baidu.com/";
                //添加值2
                worksheet.Cells["A3"].Value = 1001;
                worksheet.Cells["B3"].Value = "CSDN";
                worksheet.Cells["C3"].Value = "https://www.cnblogs.com/";
                worksheet.Cells["C3"].Style.Font.Bold = true;
                package.Save();
            }
            return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}