using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Caist.Framework.Business.Choucahistory;
using Caist.Framework.Business.FiberManage;
using Caist.Framework.Business.Jushanhistory;
using Caist.Framework.Business.Pidaihistory;
using Caist.Framework.Business.Shuibenghistory;
using Caist.Framework.Business.Tongfenghistory;
using Caist.Framework.Business.yafenghistory;
using Caist.Framework.Model.FiberManage;
using Caist.Framework.Model.Param.Choucahistory;
using Caist.Framework.Model.Param.Jushanhistory;
using Caist.Framework.Model.Param.Pidaihistory;
using Caist.Framework.Model.Param.Shuibenghistory;
using Caist.Framework.Model.Param.Tongfenghistory;
using Caist.Framework.Model.yafenghistory;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
namespace Caist.Framework.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExcelAPIController : ControllerBase
    {
        //根据bll调用方法
        private TongfengMonitorBLL tongfengMonitorBLL = new TongfengMonitorBLL();
        private yafengMonitorBLL yafengMonitorBLL = new yafengMonitorBLL();
        private PidaiMonitorBLL pidaiMonitorBLL = new PidaiMonitorBLL();
        private ShuibengMonitorBLL shuibengMonitorBLL = new ShuibengMonitorBLL();
        private JushanMonitorBLL jushanMonitorBLL = new JushanMonitorBLL();
        private FiberBLL fiberBLL = new FiberBLL();
        private ChoucaiMonitorBLL choucaiMonitorBLL = new ChoucaiMonitorBLL();

        /// <summary>
        /// 导出plc历史数据到excel
        /// 提供对外api
        /// </summary>
        /// <param name="parm">查询数据参数</param>
        /// <returns></returns>
        [HttpGet]
        [Route("export")]
        public async Task<IActionResult> Export([FromQuery]ExportParam parm)
        {
            //定义导出后的文件名称
            string fileName = "";
            //初始化导出文件流   默认为空的流
            MemoryStream stream = new MemoryStream();
            switch (parm.SystemId) //根据不同类型的参数查询出数据装在到输出流  也就是内存中
            {
                case 139006239412588544://通风机
                    tongfengExport(parm, out fileName, out stream);
                    break;
                case 139015008817254400://压风
                    yafengExport(parm, out fileName, out stream);
                    break;
                case 168646593724026880://水泵
                    shuibengExport(parm, out fileName, out stream);
                    break;
                case 168646431819698176://皮带
                    pidaiExport(parm, out fileName, out stream);
                    break;
                case 169374692635840512://局部
                    jubuExport(parm, out fileName, out stream);
                    break;
                case 192569225242480640://瓦斯
                    wasExport(parm, out fileName, out stream);
                    break;
                case 168647330583547904://光纤
                    gqExport(parm, out fileName, out stream);
                    break;
                default:
                    break;
            }
            //通过file控制器对象输出指定格式（excel）的文件，并且制定了文件名称
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        //每一个系统时间名称一样 所以用的都是yafengMonitorParam
        //通风机
        private void tongfengExport(ExportParam param, out string fileName, out MemoryStream stream)
        {
            TongfengMonitorParam tfjparam = new TongfengMonitorParam();
            if (param != null)
            {
                tfjparam.StartDate = param.StartDate;
                tfjparam.EndDate = param.EndDate;

            }
            var dataFromDb = tongfengMonitorBLL.GetSecurityInfoList(tfjparam);
            fileName = $"{Guid.NewGuid().ToString()}.xlsx";
            stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("通风曲线图");

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
                        worksheet.Cells[$"C{i + 2}"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        worksheet.Cells[$"C3"].Style.Font.Bold = true;
                    }
                }
                package.Save();
            }
            stream.Position = 0;
        }
        //压风
        private void yafengExport(ExportParam param, out string fileName, out MemoryStream stream)
        {
            yafengMonitorParam tfjparam = new yafengMonitorParam();
            if (param != null)
            {
                tfjparam.StartDate = param.StartDate;
                tfjparam.EndDate = param.EndDate;

            }
            var dataFromDb = yafengMonitorBLL.GetSecurityInfoList(tfjparam);
            fileName = $"{Guid.NewGuid().ToString()}.xlsx";
            stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("压风曲线图");

                worksheet.Cells[1, 1].Value = "内存地址";
                worksheet.Cells[1, 2].Value = "内存值";
                worksheet.Cells[1, 3].Value = "读取时间";
              //  string date = DateTime.FromOADate(Convert.ToInt32(sqlconsume_time)).ToString("yyyy-mm-dd")
                var result = dataFromDb.Result.Result;
                if (result != null)
                {
                    //添加值
                    for (var i = 0; i < result.Count; i++)
                    {
                        worksheet.Cells[$"A{i + 2}"].Value = result[i].dictId;
                        worksheet.Cells[$"B{i + 2}"].Value = result[i].dictValue;
                        worksheet.Cells[$"C{i + 2}"].Value = result[i].createTime;
                        worksheet.Cells[$"C{i + 2}"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        worksheet.Cells[$"C3"].Style.Font.Bold = true;
                    }
                }
                package.Save();
            }
            stream.Position = 0;
        }
        //水泵
        private void shuibengExport(ExportParam param, out string fileName, out MemoryStream stream)
        {

            ShuibengMonitorParam tfjparam = new ShuibengMonitorParam();
            if (param != null)
            {
                tfjparam.StartDate = param.StartDate;
                tfjparam.EndDate = param.EndDate;

            }
            var dataFromDb = shuibengMonitorBLL.GetSecurityInfoList(tfjparam);
            fileName = $"{Guid.NewGuid().ToString()}.xlsx";
            stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("水泵曲线图");

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
                        worksheet.Cells[$"C{i + 2}"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        worksheet.Cells[$"C3"].Style.Font.Bold = true;
                    }
                }
                package.Save();
            }
            stream.Position = 0;
        }
        //皮带
        private void pidaiExport(ExportParam param, out string fileName, out MemoryStream stream)
        {

            PidaiMonitorParam tfjparam = new PidaiMonitorParam();
            if (param != null)
            {
                tfjparam.StartDate = param.StartDate;
                tfjparam.EndDate = param.EndDate;

            }
            var dataFromDb = pidaiMonitorBLL.GetSecurityInfoList(tfjparam);
            fileName = $"{Guid.NewGuid().ToString()}.xlsx";
            stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("皮带曲线图");

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
                        worksheet.Cells[$"C{i + 2}"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        worksheet.Cells[$"C3"].Style.Font.Bold = true;
                    }
                }
                package.Save();
            }
            stream.Position = 0;
        }
        //局部
        private void jubuExport(ExportParam param, out string fileName, out MemoryStream stream)
        {

            JushanMonitorParam tfjparam = new JushanMonitorParam();
            if (param != null)
            {
                tfjparam.StartDate = param.StartDate;
                tfjparam.EndDate = param.EndDate;

            }
            var dataFromDb = jushanMonitorBLL.GetSecurityInfoList(tfjparam);
            fileName = $"{Guid.NewGuid().ToString()}.xlsx";
            stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("局部曲线图");

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
                        worksheet.Cells[$"C{i + 2}"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        worksheet.Cells[$"C3"].Style.Font.Bold = true;
                    }
                }
                package.Save();
            }
            stream.Position = 0;
        }
        //瓦斯
        private void wasExport(ExportParam param, out string fileName, out MemoryStream stream)
        {
            #region 请求数据参数类型转换，将参数转换为查询对象
            ChoucaiMonitorParam tfjparam = new ChoucaiMonitorParam();
            if (param != null)
            {
                tfjparam.StartDate = param.StartDate;
                tfjparam.EndDate = param.EndDate;

            }
            #endregion

            var dataFromDb = choucaiMonitorBLL.GetSecurityInfoList(tfjparam);
            fileName = $"{Guid.NewGuid().ToString()}.xlsx";
            stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("瓦斯曲线图");

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
                        worksheet.Cells[$"C{i + 2}"].Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        worksheet.Cells[$"C3"].Style.Font.Bold = true;
                    }
                }
                package.Save();
            }
            stream.Position = 0;
        }
        //光纤
        private void gqExport(ExportParam param, out string fileName, out MemoryStream stream)
        {

            #region 请求数据参数类型转换，将参数转换为查询对象
            FiberParam tfjparam = new FiberParam();
            if (param != null)
            {
                tfjparam.AreaName = param.AreaName;
            }
            #endregion
            
            //指定文件名称  随机生成guid
            fileName = $"{Guid.NewGuid().ToString()}.xlsx";
            //初始化文件流对象
            stream = new MemoryStream();
            //查询数据
            var dataFromDb = fiberBLL.GetFiberInfoList(tfjparam);
           //判断查询是否正确  并且  判断是否为空
            if (dataFromDb != null && dataFromDb.Result != null && dataFromDb.Result.Result != null)
            {
                //从异步查询数据返回对象中通过result属性获取泛型集合对象数据
                var result = dataFromDb.Result.Result;
                //开始组装excel数据格式
                using (ExcelPackage package = new ExcelPackage(stream))
                {
                    //添加excel sheets单元表格
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("光纤曲线图");
                    //添加首行列名
                    worksheet.Cells[1, 1].Value = "名称";
                    worksheet.Cells[1, 2].Value = "最大值";
                    worksheet.Cells[1, 3].Value = "最小值";
                    //将数据根据列的名称拼接到单元表格中
                    for (var i = 0; i < result.Count; i++)
                    {
                        worksheet.Cells[$"A{i + 2}"].Value = result[i].AreaName;
                        worksheet.Cells[$"B{i + 2}"].Value = result[i].MaxValue;
                        worksheet.Cells[$"C{i + 2}"].Value = result[i].MinValue;
                        worksheet.Cells[$"C3"].Style.Font.Bold = true;
                    }
                    //保存到内存
                    package.Save();
                }
                //输出流定位到0，避免文件流丢失
                stream.Position = 0;
            }

        }


    }
}