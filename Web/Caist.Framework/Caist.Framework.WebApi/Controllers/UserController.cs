using Caist.Framework.Business.OrganizationManage;
using Caist.Framework.Entity.OrganizationManage;
using Caist.Framework.Enum;
using Caist.Framework.Model.Result.SystemManage;
using Caist.Framework.Util;
using Caist.Framework.Util.Model;
using Caist.Framework.Web.Code;
using Caist.Framework.WebApi.Handle;
using Microsoft.AspNetCore.Mvc;
using Senparc.Weixin.WxOpen.AdvancedAPIs.Sns;
using System.Threading.Tasks;

namespace Caist.Framework.WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    [AuthorizeFilter]
    public class UserController : ControllerBase
    {
        private UserBLL userBLL = new UserBLL();

        #region 获取数据
        /// <summary>
        /// 获取WxOpenId
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<TData<WeiXinInfo>> GetWxOpenId([FromQuery] string code)
        {
            TData<WeiXinInfo> obj = new TData<WeiXinInfo>();
            var result = await SnsApi.JsCode2JsonAsync(GlobalContext.SystemConfig.AppId, GlobalContext.SystemConfig.AppSecret, code);
            obj.Result = new WeiXinInfo { OpenId = result.openid, UnionId = result.unionid };
            obj.Tag = 1;
            return obj;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TData<OperatorInfo>> Login([FromQuery] string userName, [FromQuery] string password)
        {
            TData<OperatorInfo> obj = new TData<OperatorInfo>();
            TData<UserEntity> userObj = await userBLL.CheckLogin(userName, password, (int)PlatformEnum.WebApi);
            if (userObj.Tag == 1)
            {
                await new UserBLL().UpdateUser(userObj.Result);
                await Operator.Instance.AddCurrent(userObj.Result.ApiToken);
                obj.Result = await Operator.Instance.Current(userObj.Result.ApiToken);
                if (obj.Result!=null)
                    obj.Result.Portrait = GlobalContext.SystemConfig.WebUrI + obj.Result.Portrait;
                
            }
            obj.Tag = userObj.Tag;
            obj.Message = userObj.Message;
          
            return obj;
        }

        /// <summary>
        /// 用户退出登录
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public TData LoginOff([FromQuery] string token)
        {
            var obj = new TData();
            Operator.Instance.RemoveCurrent(token);
            obj.Message = "登出成功";
            return obj;
        }
        #endregion
    }
}