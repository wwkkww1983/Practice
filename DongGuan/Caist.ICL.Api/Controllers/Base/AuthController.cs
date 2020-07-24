using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Caist.ICL.Core;
using Caist.ICL.Core.Entitys;
using Caist.ICL.Services;
using Caist.ICL.Api.Models;
using System.Security.Cryptography;
using System.Text;

namespace Caist.ICL.Api.Controllers.Base
{
    public class AuthController : ApiController
    {
        readonly IConfiguration config;
        public AuthController(IConfiguration config)
        {
            this.config = config;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="userpwd"></param>
        /// <param name="communityid"></param>
        /// <returns></returns>
        [HttpGet]
        [NonAuth]
        public ApiResult<Models.SignInResult> Login(string username, string userpwd,string communityid)
        {
            return API("登录", () =>
            {
                //var user = new CurrentUser
                //{
                //    UserID = "1",
                //    UserName = "超级管理员"
                //};
                var model = UnitOfWork.Get<System_UserInfo>(w => w.UserName == username);
                var Rolemodel = UnitOfWork.Get<System_UserOfRoles>(w => w.UserId ==model.Id);
                if (model == null)
                    throw new Exception("用户名错误！");
                //string a = IL.Common.Helpers.SecurityHelper.StringToMD5Hash("1");
                if (model.Password != StringToMD5Hash(userpwd))
                    //if (model.Password != userpwd)
                        throw new Exception("密码错误！");

                var key = new SigningCredentials(new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(config["Settings:AuthKey"])), "HS256");
                var exp = DateTime.Now.AddSeconds(Convert.ToInt32(config["Settings:AuthExp"]));

                var jwt = new JwtSecurityToken(
                    expires: exp,
                    signingCredentials: key
                );
                var user = new CurrentUser
                {
                    UserID = model.Id,
                    UserName = model.UserName,
                    Phone = model.Phone,
                    RoleId=Rolemodel.RoleId,
                };
                jwt.Payload.Add("user", user);
                var token = new JwtSecurityTokenHandler().WriteToken(jwt);

                return new Models.SignInResult
                {
                    Exp = exp,
                    Token = token,
                    User = user,
                    Avatar = "xxxx",
                };
            });
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="OldPassword"></param>
        /// <param name="NewPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<Models.RePwd> RePwd(string UserName, string OldPassword, string NewPassword)
        {
            return API("重置密码", () =>
            {

                string strUserName = UserName;
                var model = UnitOfWork.Get<System_UserInfo>(w => w.UserName == strUserName);
                if (model == null)
                    throw new Exception("找不到对应的账户,请联系管理员！");

                string OldPwd = OldPassword;
                //if (model.Password != IL.Common.Helpers.SecurityHelper.StringToMD5Hash(OldPwd))
                    if (model.Password != StringToMD5Hash(OldPwd))
                        throw new Exception("原密码错误！");

                string NewPwd = NewPassword;
                model.Password = StringToMD5Hash(NewPwd);
                //model.Password = NewPwd;
                UnitOfWork.Update(model);
                return new Models.RePwd
                {
                    UserName = UserName,
                    OldPassword = OldPwd,
                    NewPassword = NewPwd,
                };
            });
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static string StringToMD5Hash(string inputString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(inputString));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 创建用户的数据库密码
        /// </summary>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public static string CreateDbPassword(string userPassword)
        {
            return StringToMD5Hash(userPassword);
        }
        /// <summary>
        /// 注册保存用户信息表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet]
        [NonAuth]
        public ApiResult<string> Save(string username,string password,string communityid)
        {
            return API("保存用户信息表", () =>
            {
                //System_UserInfoService userserver = new System_UserInfoService(unitOfWork);
                System_UserInfo entity = new System_UserInfo();
                //var un = HttpContext.Request.Form["username"].ToString();
                //var pwd = Request.Form["Password"].ToString();
                entity.UpdateTime = DateTime.Now;
                if (!string.IsNullOrEmpty(entity.Id))
                {
                    var old = UnitOfWork.Get<System_UserInfo>(entity.Id);
                    if (old != null)
                    {
                        UnitOfWork.Update<System_UserInfo>(entity, old);
                        return entity.Id;
                    }
                }
                entity.Id = NewGuid();
                entity.UserName = username;
                entity.Password = StringToMD5Hash(password); ;
                //entity.UserName = userentity.UserName;
                //entity.Password = userentity.Password;
                //entity.CreateId = User.UserID;
                //entity.CreateUser = User.UserName;
                entity.CreateTime = DateTime.Now;
                entity.DeleteFlag = false;
                UnitOfWork.Insert<System_UserInfo>(entity);


                //授予默认角色 ，主业角色
                //string roleid = '56bc1a2b-f88d-43c1-8652-a0deabac65c0';
                var roleobj = UnitOfWork.Get<System_Role>("56bc1a2b-f88d-43c1-8652-a0deabac65c0");
                if(roleobj!=null)
                {
                    System_UserOfRoles ur = new System_UserOfRoles();
                    ur.Id = NewGuid();
                    ur.RoleId = roleobj.Id;
                    ur.UserId = entity.Id;
                    UnitOfWork.Insert<System_UserOfRoles>(ur);
                }

                return entity.Id;
            });
        }
        /// <summary>
        /// 获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<CurrentUser> GetCurrentUser()
        {
            return API("当前用户", () =>
            {
                return User;
            });
        }

    }
}