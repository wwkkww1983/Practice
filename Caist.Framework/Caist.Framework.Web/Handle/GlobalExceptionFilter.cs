﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Util.Model;
using Caist.Framework.Util.Extension;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Caist.Framework.Util;

namespace Caist.Framework.Web.Controllers
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            LogHelper.WriteWithTime(context.Exception);
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                TData obj = new TData();
                obj.Message = context.Exception.GetOriginalException().Message;
                if (string.IsNullOrEmpty(obj.Message))
                {
                    obj.Message = "抱歉，系统错误，请联系管理员！";
                }
                context.Result = new CustomJsonResult { Value = obj };
                context.ExceptionHandled = true;
            }
            else
            {
                string errorMessage = context.Exception.GetOriginalException().Message;
                context.Result = new RedirectResult("~/Home/Error?message=" + HttpUtility.UrlEncode(errorMessage));
                context.ExceptionHandled = true;
            }
        }

        public Task OnExceptionAsync(ExceptionContext context)
        {
            OnException(context);
            return Task.CompletedTask;
        }
    }
}