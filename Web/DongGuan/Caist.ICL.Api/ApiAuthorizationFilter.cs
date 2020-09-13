using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Caist.ICL.Library;
namespace Caist.ICL.Api
{
    public class ApiAuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //if (!context.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    if (context.Filters.OfType<NonAuthAttribute>().Any())
            //    {
            //        context.HttpContext.User.AddIdentity(new System.Security.Claims.ClaimsIdentity());
            //        return;
            //    }
            //    context.Result = new JsonResult(new ApiResult<object>
            //    {
            //        Code = 401,
            //        Message = "无授权，服务器已阻止访问！"
            //    });
            //}
            "".HasValue();

        }
    }
}
