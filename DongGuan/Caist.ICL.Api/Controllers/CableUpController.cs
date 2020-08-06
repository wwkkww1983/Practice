using Caist.ICL.Models;
using Caist.ICL.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Caist.ICL.Api.Controllers
{
    public class CableUpController : ApiController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        CableTypeService service;
        public CableUpController(CableTypeService service,IHostingEnvironment hostingEnvironment)
        {
            this.service = service;
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// 导入电缆型号数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> FileSave(IFormFile excelfile, string prjId, string secId)
        {
            return API("Excel信息导入", () =>
            {
                var date = Request;
                var files = Request.Form.Files;
                long size = files.Sum(f => f.Length);

                //entity.
                string sWebRootFolder = _hostingEnvironment.WebRootPath + "/excel/";
                string sFileName = $"{Guid.NewGuid()}电缆型号.xlsx";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
                try
                {
                    using (FileStream fs = new FileStream(file.ToString(), FileMode.Create))
                    {
                        files[0].CopyTo(fs);
                        fs.Flush();
                    }
                    using (ExcelPackage package = new ExcelPackage(file))
                    {
                        StringBuilder sb = new StringBuilder();
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1];

                        Cable_Type entity = new Cable_Type();
                        int rowCount = worksheet.Dimension.Rows;
                        int ColCount = worksheet.Dimension.Columns;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            string CableType = worksheet.Cells[row, 1].Value.ToString();
                            string IsBreak = worksheet.Cells[row, 10].Value.ToString();
                            var data = service.GetItem("", "CableType='" + CableType + "' and IsBreak='" + IsBreak + "'", "").ToList();

                            if (worksheet.Cells[row, 1].Value != null)
                            {
                                entity.CableType = worksheet.Cells[row, 1].Value.ToString();//电缆型号
                            }
                            if (worksheet.Cells[row, 2].Value != null)
                            {
                                entity.Voltage_Type = worksheet.Cells[row, 2].Value.ToString();//电压等级/kV
                            }
                            if (worksheet.Cells[row, 3].Value != null)
                            {
                                entity.Fsection = worksheet.Cells[row, 3].Value.ToString();//截面/mm2
                            }
                            if (worksheet.Cells[row, 4].Value != null)
                            {
                                entity.CableOd = worksheet.Cells[row, 4].Value.ToString();//电缆外径
                            }
                            if (worksheet.Cells[row, 5].Value != null)
                            {
                                entity.CableWeight = worksheet.Cells[row, 5].Value.ToString();//电缆重量
                            }
                            if (worksheet.Cells[row, 6].Value != null)
                            {
                                entity.CableRadius = worksheet.Cells[row, 6].Value.ToString();//转弯半径
                            }
                            if (worksheet.Cells[row, 7].Value != null)
                            {
                                entity.Max_Lateral_Pressure = worksheet.Cells[row, 7].Value.ToString();//允许最大侧压力
                            }
                            if (worksheet.Cells[row, 8].Value != null)
                            {
                                entity.Max_Traction = worksheet.Cells[row, 8].Value.ToString();//允许最大牵引力
                            }
                            if (worksheet.Cells[row, 9].Value != null)
                            {
                                entity.Factory = worksheet.Cells[row, 9].Value.ToString();//生产厂家
                            }
                            if (worksheet.Cells[row, 10].Value != null)
                            {
                                entity.IsBreak = worksheet.Cells[row, 10].Value.ToString();//分裂情况
                            }
                            if (data.Count > 0)
                            {
                                var Id = ((NPoco.PocoExpando)data[0]).Values.ToList();
                                entity.Id = Id[0].ToString();
                                service.Update(entity);
                            }
                            else
                            {
                                entity.Id = NewGuid();
                                service.Insert(entity);
                            }
                        }
                        return entity.Id;
                    }
                }
                catch (Exception ex)
                {
                    return "导入失败：" + ex.Message;
                }
            });
        }
        
        /// <summary>
        /// 下载EXCEL文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> Export()
        {
            return API("Excel信息导入", () =>
            {
                string filename = $"{Guid.NewGuid()}电缆型号.xlsx";
                string sWebRootFolder = _hostingEnvironment.WebRootPath+"/excel/";
                FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, filename));
                //var stream = new MemoryStream();
                using (ExcelPackage package = new ExcelPackage(file))
                {
                    //add worksheet
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("电缆数据");
                    //add head
                    worksheet.Cells[1, 1].Value = "电缆型号";
                    worksheet.Cells[1, 2].Value = "电压等级/kV";
                    worksheet.Cells[1, 3].Value = "截面/mm2";
                    worksheet.Cells[1, 4].Value = "电缆外径/mm";
                    worksheet.Cells[1, 5].Value = "电缆重量/kg/m";
                    worksheet.Cells[1, 6].Value = "转弯半径/m";
                    worksheet.Cells[1, 7].Value = "允许最大侧压力/kN/m";
                    worksheet.Cells[1, 8].Value = "允许最大牵引力/kN";
                    worksheet.Cells[1, 9].Value = "厂家";
                    worksheet.Cells[1, 10].Value = "分裂情况";
                    //add value
                    var rowNum = 2;
                    var data = service.GetItem("", "", "").ToList();
                    //var cableList = ((NPoco.PocoExpando)data[0]).Values.ToList();
                    for (int i = 0; i < data.Count; i++)
                    {
                        worksheet.Cells["A" + rowNum].Value = ((NPoco.PocoExpando)data[i]).Values.ToList()[1] == null ? null : ((NPoco.PocoExpando)data[i]).Values.ToList()[1].ToString();
                        worksheet.Cells["B" + rowNum].Value = ((NPoco.PocoExpando)data[i]).Values.ToList()[2] == null ? null : ((NPoco.PocoExpando)data[i]).Values.ToList()[2].ToString();
                        worksheet.Cells["C" + rowNum].Value = ((NPoco.PocoExpando)data[i]).Values.ToList()[4] == null ? null : ((NPoco.PocoExpando)data[i]).Values.ToList()[4].ToString();
                        worksheet.Cells["D" + rowNum].Value = ((NPoco.PocoExpando)data[i]).Values.ToList()[5] == null ? null : ((NPoco.PocoExpando)data[i]).Values.ToList()[5].ToString();
                        worksheet.Cells["E" + rowNum].Value = ((NPoco.PocoExpando)data[i]).Values.ToList()[6] == null ? null : ((NPoco.PocoExpando)data[i]).Values.ToList()[6].ToString();
                        worksheet.Cells["F" + rowNum].Value = ((NPoco.PocoExpando)data[i]).Values.ToList()[7] == null ? null : ((NPoco.PocoExpando)data[i]).Values.ToList()[7].ToString();
                        worksheet.Cells["G" + rowNum].Value = ((NPoco.PocoExpando)data[i]).Values.ToList()[8] == null ? null : ((NPoco.PocoExpando)data[i]).Values.ToList()[8].ToString();
                        worksheet.Cells["H" + rowNum].Value = ((NPoco.PocoExpando)data[i]).Values.ToList()[9] == null ? null : ((NPoco.PocoExpando)data[i]).Values.ToList()[9].ToString();
                        worksheet.Cells["I" + rowNum].Value = ((NPoco.PocoExpando)data[i]).Values.ToList()[10] == null ? null : ((NPoco.PocoExpando)data[i]).Values.ToList()[10].ToString();
                        worksheet.Cells["J" + rowNum].Value = ((NPoco.PocoExpando)data[i]).Values.ToList()[11] == null ? null : ((NPoco.PocoExpando)data[i]).Values.ToList()[11].ToString();
                        rowNum++;
                    }
                    package.Save();
                    return $"{"http://localhost:61851/excel"}/{filename}";
                }
            });
        }
    }
}