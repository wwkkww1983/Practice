using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Tongfenghistory;
using Caist.Framework.Entity.Tongfenghistory;
using Caist.Framework.Model.Param.Tongfenghistory;
using Caist.Framework.Service.Tongfenghistory;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;

namespace Caist.Framework.Web.Areas.TFJDemo.Controllers
{
    [Area("VentilatorManage")]
    public class TFJController : Controller
    {

        private TongfengMonitorBLL tongfengMonitorBLL = new TongfengMonitorBLL();
        
        //用来获取路径相关
        private IHostingEnvironment _hostingEnvironment;
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<string> GettongfengTempLists([FromBody]TongfengMonitorParam param)
        {
            var obj = await tongfengMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }
        public async Task<string> GettongfengTempListsr([FromBody]TongfengMonitorParam param)
        {
            var obj = await tongfengMonitorBLL.GetSecurityInfoList(param);
            return obj.RemoveNullValue();
        }



        public TFJController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        /// <summary>
        /// excel导出功能
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Export(TongfengMonitorParam tfj)
        {
            //100天以前数据
            //tfj.StartDate = DateTime.Now.AddDays(-100);
            //tfj.EndDate = DateTime.Now;
            var dataFromDb = tongfengMonitorBLL.GetSecurityInfoList(tfj);
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
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("通风Excel");
                //添加头
                worksheet.Cells[1, 1].Value = "内存地址";
                worksheet.Cells[1, 2].Value = "内存值";
                worksheet.Cells[1, 3].Value = "读取时间";
                var result = dataFromDb.Result.Result;
                if (result != null)
                {
                    //添加值
                    for (var i = 0; i < result.Count; i++)
                    {
                        worksheet.Cells[$"A{i + 2}"].Value = result[i].dictId;
                        worksheet.Cells[$"B{i + 2}"].Value = result[i].dictValue;
                        worksheet.Cells[$"C{i + 2}"].Value = result[i].createTime;
                        worksheet.Cells[$"C3"].Style.Font.Bold = true;
                    }
                }
               
                package.Save();
            }
            return File(sFileName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}