using Caist.ICL.Core;
using Caist.ICL.Core.Dao;
using Caist.ICL.Core.Entitys;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Api
{
    /// <summary>
    /// API控制器基类
    /// ljl@2018
    /// </summary>
    [Route("api/[controller]/[action]")]
    public class ApiController : Controller
    {
        System.Text.RegularExpressions.Regex _rxBearer = new System.Text.RegularExpressions.Regex("^Bearer ", System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            if (token != null)
            {
                token = _rxBearer.Replace(token, "");
                var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                User = ((JObject)jwt.Payload["user"]).ToObject<CurrentUser>();
            }
            base.OnActionExecuting(context);
        }

        protected new CurrentUser User { get; private set; }

        protected string NewGuid()
        {
            return Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 获取一个流水号
        /// </summary>
        /// <param name="perfix">前缀</param>
        /// <param name="len">最大长度</param>
        /// <returns></returns>
        //protected string GetSerialNumber(string perfix, int len, string dateFmt = null)
        //{
        //    var obj = UnitOfWork.Get<System_BillNo>(w => w.Perfix == perfix);
        //    int num = 1;
        //    if (obj == null)
        //    {
        //        obj = new System_BillNo();
        //        obj.Id = NewGuid();
        //        obj.Perfix += perfix;
        //        obj.MaxValue = Convert.ToInt32("".PadLeft(len, '9'));
        //        obj.CurValue = 2;
        //        UnitOfWork.Insert(obj);
        //    }
        //    else
        //    {
        //        num = obj.CurValue.Value;
        //        obj.CurValue = obj.CurValue + 1;
        //        if (obj.MaxValue <= obj.CurValue)
        //        {
        //            obj.CurValue = 1;
        //        }
        //        UnitOfWork.Update(obj);
        //    }

        //    string billno = perfix;
        //    if (dateFmt != null)
        //    {
        //        billno += DateTime.Now.ToString(dateFmt);
        //    }
        //    billno += num.ToString().PadLeft(len, '0');
        //    return billno;
        //    /*
        //    //通过序列获取流水号，如果序列不存在则创建序列

        //    string name = "seq_" + perfix;
        //    var num = UnitOfWork.GetValue<object>($"if exists(select object_id from sys.sequences where name='{name}')select next value for {name}");
        //    if (num == null)
        //    {
        //        num = "1";
        //        UnitOfWork.Execute($"CREATE SEQUENCE [dbo].[{name}] AS[INT] START WITH 2 INCREMENT BY 1 MINVALUE 1 MAXVALUE {"".PadLeft(len, '9')} CYCLE CACHE ");
        //    }
        //    return num.ToString().PadLeft(len, '0');
        //    */
        //}

        /// <summary>
        /// 从容器中获取类型实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GetService<T>() => (T)HttpContext.RequestServices.GetService(typeof(T));

        /// <summary>
        /// 工作单元
        /// </summary>
        protected IUnitOfWork UnitOfWork => GetService<IUnitOfWork>();

        /// <summary>
        /// 事务包裹提交
        /// </summary>
        /// <param name="action"></param>
        protected void CommitTransaction(Action action)
        {
            UnitOfWork.CommitTransaction(action);
        }

        /// <summary>
        /// 创建一个API结果
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getter"></param>
        /// <returns></returns>
        protected ApiResult<T> API<T>(string title, Func<T> getter)
        {
            try
            {
                var data = getter();

                return new ApiResult<T>
                {
                    Code = 0,
                    Message = title + "成功！",
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<T>
                {
                    Code = 1,
                    Message = title + "失败！" + ex.Message
                };
            }
        }

        protected ApiResult<object> API(string title, Action action)
        {
            try
            {
                action();

                return new ApiResult<object>
                {
                    Code = 0,
                    Message = title + "成功！"
                };
            }
            catch (Exception ex)
            {
                return new ApiResult<object>
                {
                    Code = 1,
                    Message = title + "失败！" + ex.Message
                };
            }
        }
    }
}
