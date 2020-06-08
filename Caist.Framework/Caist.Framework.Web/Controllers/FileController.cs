using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Caist.Framework.Util;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Caist.Framework.Entity.SystemManage;
using Caist.Framework.Business.SystemManage;
using Caist.Framework.Model.Param.SystemManage;
using Newtonsoft.Json;

namespace Caist.Framework.Web.Controllers
{
    public class FileController : BaseController
    {
        readonly FileBLL fileBLL = new FileBLL();
        #region 上传单个文件
        [HttpPost]
        public async Task<IActionResult> UploadFile(int fileModule, IFormCollection files)
        {
            TData<string> obj = await FileHelper.UploadFile(fileModule, files.Files);
            return Json(obj);
        }
        #endregion

        #region 删除单个文件
        [HttpPost]
        public IActionResult DeleteFile(int fileModule, string path)
        {
            TData<string> obj = FileHelper.DeleteFile(fileModule, path);
            return Json(obj);
        }
        #endregion

        #region 下载文件
        [HttpGet]
        public void DownloadFile(string fileName, int delete = 1)
        {
            fileName = fileName.ParseToString();
            string filePath = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                throw new FileNotFoundException("文件不存在：" + filePath);
            }
            fileName = Path.GetFileName(fileName);
            FileHelper.DownLoadFile(HttpContext, fileName, filePath);
            if (delete == 1)
            {
                System.IO.File.Delete(filePath);
            }
        }
        #endregion

        #region 查询文件表
        [HttpGet]
        public async Task<IActionResult> GetFileList(FileListParam fileListParam)
        {
            TData<List<FileEntity>> obj = await fileBLL.GetList(fileListParam);
            return Json(obj);
        }
        #endregion

        #region 提交文件数据
        [HttpPost]
        public async Task<string> SaveFormJson(List<FileEntity> entitys)
        {
            TData<string> obj = await fileBLL.SaveForm(entitys);
            return JsonConvert.SerializeObject(obj);
        }
        #endregion
    }
}