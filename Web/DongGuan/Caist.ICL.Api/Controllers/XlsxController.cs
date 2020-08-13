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
    public class XlsxController : ApiController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        Basic_LocusService service;
        public XlsxController(Basic_LocusService service, IHostingEnvironment hostingEnvironment)
        {
            this.service = service;
            _hostingEnvironment = hostingEnvironment;
        }
        /// <summary>
        /// 导入测绘数据
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
                string sFileName = $"{Guid.NewGuid()}.xlsx";
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

                        //int rowCount = worksheet.Dimension.Rows;
                        //int ColCount = worksheet.Dimension.Columns;
                        Basic_Locus entity = new Basic_Locus();
                        //string ProjectName = worksheet.Cells[1, 1].Value.ToString();
                        //var data = service.GetItem("", "", "").ToList();
                        //if (data.Count == 0)
                        //{
                        //    throw new Exception("请先添加需要导入的项目信息！");
                        //}
                        //else
                        //{
                        //var values = ((NPoco.PocoExpando)data[0]).Values.ToList();

                        //string ProjectID = values[0].ToString();
                        entity.ProjectId = prjId;
                        ExcelWorksheets worksheets = package.Workbook.Worksheets;
                        ExcelWorksheet myworksheet = worksheets[1];
                        //foreach (ExcelWorksheet myworksheet in worksheets)
                        //{
                        //    if (myworksheet.Index > 4)
                        //    {
                        int rowCount = myworksheet.Dimension.Rows;
                        int ColCount = myworksheet.Dimension.Columns;
                        entity.SectionId = secId;
                        for (int row = 5; row <= rowCount; row++)
                        {
                            entity.GisPoint = "";
                            if (myworksheet.Cells[row, 2].Value != null)
                            {
                                if (myworksheet.Cells[row, 2].Value.ToString() != "")
                                {
                                    if (myworksheet.Cells[row, 2].Value != null)
                                    {
                                        entity.GisPoint = myworksheet.Cells[row, 2].Value.ToString();//物探点号
                                    }
                                    if (myworksheet.Cells[row, 3].Value != null)
                                    {
                                        entity.LinkPoint = myworksheet.Cells[row, 3].Value.ToString();//连接点号
                                    }
                                    if (myworksheet.Cells[row, 4].Value != null)
                                    {
                                        entity.PointFeature = myworksheet.Cells[row, 4].Value.ToString();//点特征
                                    }
                                    if (myworksheet.Cells[row, 6].Value != null)
                                    {
                                        entity.Point_X = myworksheet.Cells[row, 6].Value.ToString();//X
                                    }
                                    else
                                    {
                                        break;
                                    }
                                    if (myworksheet.Cells[row, 7].Value != null)
                                    {
                                        entity.Point_Y = myworksheet.Cells[row, 7].Value.ToString();//Y
                                    }
                                    if (myworksheet.Cells[row, 10].Value != null)
                                    {
                                        entity.Point_Z = myworksheet.Cells[row, 10].Value.ToString();//埋深
                                    }
                                    if (myworksheet.Cells[row, 8].Value != null)
                                    {
                                        entity.GroundHeight = Convert.ToDecimal(myworksheet.Cells[row, 8].Value.ToString());//地面高程
                                    }
                                    if (myworksheet.Cells[row, 9].Value != null)
                                    {
                                        entity.LineHeight = Convert.ToDecimal(myworksheet.Cells[row, 9].Value.ToString());//管线高程
                                    }
                                    if (myworksheet.Cells[row, 11].Value != null)
                                    {
                                        entity.SectionSize = myworksheet.Cells[row, 11].Value.ToString();//截面尺寸
                                    }
                                    if (myworksheet.Cells[row, 13].Value != null)
                                    {
                                        entity.Material = myworksheet.Cells[row, 13].Value.ToString();//材质
                                    }
                                    if (myworksheet.Cells[row, 17].Value != null)
                                    {
                                        entity.BuryType = myworksheet.Cells[row, 17].Value.ToString();//埋设方式
                                    }
                                    entity.Id = NewGuid();
                                    //entity.CreateId = User.UserID;
                                    //entity.CreateUser = User.UserName;
                                    entity.CreateTime = DateTime.Now;
                                    service.Insert(entity);
                                }
                            }
                        }
                        //}
                        //}
                        //entity.ProjectId

                        return entity.Id;
                        //}

                    }
                }
                catch (Exception ex)
                {
                    return "导入失败：" + ex.Message;
                }
            });
        }
    }
}
