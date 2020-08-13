using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.ApplicationManage;
using Caist.Framework.Entity.SystemManage;
using Caist.Framework.Model.Param.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Model;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Views.Video
{
    public class VideoController : Controller
    {
        private readonly VideoBLL videoBLL = new VideoBLL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Video()
        {
            return View();
        }

        public IActionResult SingleVideo()
        {
            return View();
        }

        /// <summary>
        /// 矿井视频信息
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetVideoList(string Key,string Number)
        {
            TData<List<VideoEntity>> obj = new TData<List<VideoEntity>>();
            try
            {
                if (Util.ValidatorHelper.VideoValid(Key, Request.Headers["Caller"].ToString()))
                {
                    var parm = new VideoListParam()
                    {
                        Number = Number
                    };
                    obj = await videoBLL.GetList(parm);

                    obj.Result.ForEach(i =>
                    {
                        i.Url = Util.UrlHelper.ReplaceDomain(GlobalContext.SystemConfig.VideoExtranetIP, i.Url);
                    });
                }
                else
                {
                    obj.Tag = 0;
                    obj.Message = "验证Caller或者Key失败，请登录！";
                }
            }
            catch (Exception ex)
            {
                obj.Tag = 0;
                obj.Message = ex.Message;
            }
            return Json(obj);
        }
    }
}
