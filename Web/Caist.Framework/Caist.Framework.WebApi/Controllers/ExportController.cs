using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Caist.Framework.Business;
using Caist.Framework.Business.AlarmManage;
using Caist.Framework.Business.EventRecordManage;
using Caist.Framework.Entity.AlarmManage;
using Caist.Framework.Entity.EventRecordManage;
using Caist.Framework.Enum.SystemManage;
using Caist.Framework.Model.Param.AlarmManage;
using Caist.Framework.Model.Param.EventRecordManage;
using Caist.Framework.Model.Param.OrganizationManage;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ExportController : ControllerBase
    {

        private EventRecordBLL eventRecordBLL = new EventRecordBLL();
        private AlarmReccordBLL alarm = new AlarmReccordBLL();
        private SystemDataService systemDataService = new SystemDataService();
        /// <summary>
        /// 导出操作记录到报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ExportEventRecord([FromQuery] EventRecordListParam param)
        {
            //定义导出后的文件名称
            string fileName = $"操作记录-" + DateTime.Now.ToString("yyy-MM-dd") + ".xlsx";
            //初始化导出文件流   默认为空的流
            MemoryStream stream = new MemoryStream();

            //查询出数据
            TData<List<EventRecordEntity>> obj = await eventRecordBLL.GetList(param);
            if (obj.Tag == 1 && obj.Result != null)
            {
                using (ExcelPackage package = new ExcelPackage(stream))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("操作记录");
                    //设置title
                    worksheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
                    worksheet.Row(1).CustomHeight = true;//自动调整行高
                    worksheet.Cells[1, 1, 1, 6].Merge = true; //合并 1-6列
                    worksheet.Cells[1, 1].Value = "操作记录";
                    worksheet.Cells[1, 1].Style.Font.Name = "微软雅黑";//字体
                    worksheet.Cells[1, 1].Style.Font.Size = 16;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //设置列title
                    worksheet.Cells[2, 1].Value = "序号";
                    worksheet.Cells[2, 2].Value = "系统名称";
                    worksheet.Cells[2, 3].Value = "操作人";
                    worksheet.Cells[2, 4].Value = "控制模式";
                    worksheet.Cells[2, 5].Value = "操作时间";
                    worksheet.Cells[2, 6].Value = "操作内容";


                    worksheet.Column(2).Width = 17;//设置列宽
                    worksheet.Column(3).Width = 17;//设置列宽
                    worksheet.Column(4).Width = 15;//设置列宽
                    worksheet.Column(5).Width = 24;//设置列宽
                    worksheet.Column(6).Width = 70;//设置列宽
                    //输出列值
                    for (var i = 0; i < obj.Result.Count; i++)
                    {
                        worksheet.Cells[$"A{i + 3}"].Value = i + 1;
                        worksheet.Cells[$"B{i + 3}"].Value = obj.Result[i].SystemName;
                        worksheet.Cells[$"C{i + 3}"].Value = obj.Result[i].RealName;
                        worksheet.Cells[$"D{i + 3}"].Value = obj.Result[i].OperationModel.HasValue ? (obj.Result[i].OperationModel.Value == 0 ? "远程" : "就地 ") : "远程";
                        worksheet.Cells[$"E{i + 3}"].Value = obj.Result[i].OperatorTime.ToString("yyyy-MM-dd HH:mm:ss");
                        worksheet.Cells[$"F{i + 3}"].Value = "操作了" + obj.Result[i].SystemName + ":" + obj.Result[i].InstructName;
                        //样式
                        worksheet.Cells[$"A{i + 3}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    }
                    package.Save();
                }
                stream.Position = 0;
            }
            //通过file控制器对象输出指定格式（excel）的文件，并且制定了文件名称
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }



        /// <summary>
        /// 导出操作记录到报表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ExportAlarmRecord([FromQuery] ALarmReccordListParam param)
        {
            //定义导出后的文件名称
            string fileName = $"报警记录-" + DateTime.Now.ToString("yyy-MM-dd") + ".xlsx";
            //初始化导出文件流   默认为空的流
            MemoryStream stream = new MemoryStream();

            //查询出数据
            TData<List<AlarmRecordEntity>> obj = await alarm.GetList(param);
            if (obj.Tag == 1 && obj.Result != null)
            {
                using (ExcelPackage package = new ExcelPackage(stream))
                {

                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("报警记录");
                    //设置title
                    worksheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
                    worksheet.Row(1).CustomHeight = true;//自动调整行高
                    worksheet.Cells[1, 1, 1, 6].Merge = true; //合并 1-6列
                    worksheet.Cells[1, 1].Value = "报警记录";
                    worksheet.Cells[1, 1].Style.Font.Name = "微软雅黑";//字体
                    worksheet.Cells[1, 1].Style.Font.Size = 16;
                    worksheet.Cells[1, 1].Style.Font.Bold = true;
                    worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    //设置列title
                    worksheet.Cells[2, 1].Value = "序号";
                    worksheet.Cells[2, 2].Value = "报警时间";
                    worksheet.Cells[2, 3].Value = "系统名称";
                    worksheet.Cells[2, 4].Value = "报警内容";
                    worksheet.Cells[2, 5].Value = "报警等级";

                    worksheet.Cells[2, 6].Value = "报警值";


                    worksheet.Column(2).Width = 17;//设置列宽
                    worksheet.Column(3).Width = 24;//设置列宽
                    worksheet.Column(4).Width = 70;//设置列宽
                    worksheet.Column(5).Width = 15;//设置列宽

                    worksheet.Column(6).Width = 17;//设置列宽

                    //输出列值
                    for (var i = 0; i < obj.Result.Count; i++)
                    {
                        worksheet.Cells[$"A{i + 3}"].Value = i + 1;

                        worksheet.Cells[$"B{i + 3}"].Value = obj.Result[i].AlarmTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
                        worksheet.Cells[$"C{i + 3}"].Value = obj.Result[i].SystemName;
                        worksheet.Cells[$"D{i + 3}"].Value = obj.Result[i].ViewName + obj.Result[i].BroadcastContent + "：" + obj.Result[i].AlarmReason;
                        worksheet.Cells[$"E{i + 3}"].Value = obj.Result[i].BroadcastCount + 1;
                        worksheet.Cells[$"F{i + 3}"].Value = obj.Result[i].AlarmValue;

                        //样式
                        worksheet.Cells[$"A{i + 3}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        worksheet.Cells[$"E{i + 3}"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    package.Save();
                }
                stream.Position = 0;
            }
            //通过file控制器对象输出指定格式（excel）的文件，并且制定了文件名称
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }

        /// <summary>
        /// 导出系统数据
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ExportSystemData([FromQuery] SystemDataParam param)
        {
            //定义导出后的文件名称
            string fileName = "系统数据-{0}.xlsx";
            //根据系统ID判断获取数据的表名
            var obj = await systemDataService.GetSystemTableNameList(param);
            List<SystemDataEntity> systemData = new List<SystemDataEntity>();
            //初始化导出文件流   默认为空的流
            MemoryStream stream = new MemoryStream();
            if (obj != null && obj.Result != null && obj.Result.Count > 0)
            {
                //获取表名
                param.TabName = obj.Result[0].TabName;
                //根据时间参数获取指定时间段内数据
                var data = await systemDataService.GetSystemDataList(param);
                systemData = data.Result;
                if (systemData != null && systemData.Count > 0)
                {
                    switch (param.Cycle)
                    {
                        case CycleEnum.Day:
                            fileName = string.Format(fileName,CycleEnum.Day.GetDescription()+"报表");
                            ExportExcel(systemData, param, param.Cycle, 23, "{0}点", out stream);
                            break;
                        case CycleEnum.Week:
                            fileName = string.Format(fileName, CycleEnum.Week.GetDescription() + "报表");
                            ExportExcel(systemData, param, param.Cycle, 7, "第{0}天", out stream);
                            break;
                        case CycleEnum.Month:
                            fileName = string.Format(fileName, CycleEnum.Month.GetDescription() + "报表");
                            TimeSpan ts = param.StartDate.Value.Subtract(param.EndDate.Value);
                            ExportExcel(systemData, param, param.Cycle, ts.Days, "{0}号", out stream);
                            break;
                        case CycleEnum.Season:
                            fileName = string.Format(fileName, CycleEnum.Season.GetDescription() + "报表");
                            ExportExcel(systemData, param, param.Cycle, 3, "第{0}月", out stream);
                            break;
                        case CycleEnum.Year:
                            fileName = string.Format(fileName, CycleEnum.Year.GetDescription() + "报表");
                            ExportExcel(systemData, param, param.Cycle, 12, "{0}月", out stream);
                            break;
                        default:
                            stream = null;
                            break;
                    }
                }

            }
            //通过file控制器对象输出指定格式（excel）的文件，并且制定了文件名称
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
        private void ExportExcel(List<SystemDataEntity> systemData, SystemDataParam param, CycleEnum Cycle, int ColumnNum, string Unit, out MemoryStream stream)
        {

            stream = new MemoryStream();
            using (ExcelPackage package = new ExcelPackage(stream))
            {

                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("系统数据" + Cycle.GetDescription() + "报表");
                //设置title
                worksheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
                worksheet.Row(1).CustomHeight = true;//自动调整行高
                worksheet.Cells[1, 1, 1, ColumnNum + 2].Merge = true; //合并生成title
                worksheet.Cells[1, 1].Value = "系统数据" + Cycle.GetDescription() + "报表";
                worksheet.Cells[1, 1].Style.Font.Name = "微软雅黑";//字体
                worksheet.Cells[1, 1].Style.Font.Size = 16;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //设置列title
                worksheet.Cells[2, 1].Value = "序号";
                worksheet.Cells[2, 2].Value = "设别名称";
                for (int j = 0; j < ColumnNum; j++)
                {
                    if (Cycle==CycleEnum.Season)
                    {
                        worksheet.Cells[2, 3 + j].Value = string.Format(Unit, (param.StartDate.Value.Month + j).NumberToChinese());
                    }
                    else if (Cycle == CycleEnum.Day)
                    {
                        
                        worksheet.Cells[2, 3 + j].Value = string.Format(Unit, (j + 1));  
                    }
                    else
                    {
                        worksheet.Cells[2, 3 + j].Value = string.Format(Unit, (j + 1).NumberToChinese());
                    }
                    worksheet.Cells[2, 3 + j].Style.ShrinkToFit = true;//单元格自动适应大小
                }
                worksheet.Column(2).Width = 24;//设置列宽

                var data = systemData.GroupBy(n => n.DictName).ToList();

                //分组输出
                for (var i = 0; i < data.Count; i++)
                {
                    //序号
                    worksheet.Cells[i + 3, 1].Value = i + 1;
                    worksheet.Cells[i + 3, 2].Value = data[i].Key;

                    for (int j = 0; j < ColumnNum; j++)
                    {
                        float value = 0f;
                        IEnumerable<SystemDataEntity> list;
                        var startTime = Convert.ToDateTime(param.StartDate.Value.ToString("yyy-MM-dd"));
                        var endTime = Convert.ToDateTime(param.StartDate.Value.ToString("yyy-MM-dd") + " 23:59:59");
                        //获取所有相关数据
                        //根据周期筛选出平均值
                        switch (Cycle)
                        {
                            case CycleEnum.Day:
                                list = systemData.Where(n => n.DictName == data[i].Key && n.CreateTime.Hour == j + 1);
                                value = (list != null && list.Count()>0)  ? list.Average(t => t.DictValue.ParseToFloat()) : 0;
                                break;
                            case CycleEnum.Week:
                              
                                list = systemData.Where(n => n.DictName == data[i].Key && n.CreateTime >= startTime.AddDays(j) && n.CreateTime<= endTime.AddDays(j));
                                value = (list != null && list.Count() > 0) ? list.Average(t => t.DictValue.ParseToFloat()) : 0;
                                break;
                            case CycleEnum.Month:
                                list = systemData.Where(n => n.DictName == data[i].Key && n.CreateTime >= startTime.AddDays(j) && n.CreateTime <= endTime.AddDays(j));
                                value = (list != null && list.Count() > 0) ? list.Average(t => t.DictValue.ParseToFloat()) : 0;
                                break;
                            case CycleEnum.Season:
                                list = systemData.Where(n => n.DictName == data[i].Key && n.CreateTime >= startTime.AddMonths(j) && n.CreateTime <= endTime.AddMonths(j));
                                value = (list != null && list.Count() > 0) ? list.Average(t => t.DictValue.ParseToFloat()) : 0;
                                break;
                            case CycleEnum.Year:
                                list = systemData.Where(n => n.DictName == data[i].Key && n.CreateTime >= startTime.AddMonths(j) && n.CreateTime <= endTime.AddMonths(j));
                                value = (list != null && list.Count() > 0) ? list.Average(t => t.DictValue.ParseToFloat()) : 0;
                                break;
                            default:
                                break;
                        }
                        worksheet.Cells[i + 3, j + 3].Value = value.ToString("f2");
                        worksheet.Cells[i + 3, j + 3].Style.ShrinkToFit = true;//单元格自动适应大小
                    }

                }
                package.Save();
            }
            stream.Position = 0;
        }
    }
}