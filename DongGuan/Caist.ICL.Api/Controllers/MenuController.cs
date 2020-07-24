using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.ICL.Api.Models;
using Caist.ICL.Core.Entitys;
using Caist.ICL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Caist.ICL.Api.Controllers
{
    public class MenuController : ApiController
    {
        MenuService Service;
        public MenuController(MenuService Service) { this.Service = Service; }
        /// <summary>
        /// 保存菜单数据信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> Save([FromBody] System_Menu entity)
        {
            return API("保持菜单数据", () =>
             {
                 entity.UpdateUser = User.UserName;
                 entity.UpdateTime = DateTime.Now;
                 if (!String.IsNullOrEmpty(entity.Id))
                 {
                     var old = Service.Get(entity.Id);
                     if (old != null)
                     {
                         Service.Update(entity, old);
                         return entity.Id;
                     }
                 }
                 entity.Id = NewGuid();
                 entity.CreateUser = User.UserName;
                 entity.CreateTime = DateTime.Now;
                 Service.Insert(entity);
                 return entity.Id;
             });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<string> Delete(string id)
        {
            return API("删除菜单数据",()=>
            {
                if (id.Contains(','))
                {
                    string[] ids = id.Split(',');
                    foreach (string strid in ids)
                    {
                        Service.Delete(strid);
                    }
                }
                else
                {
                    Service.Delete(id);
                }
                return id;
            });
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sysId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public ApiResult<object> DeleteBase([FromBody]Core.BaseEntity[] sysId)
        {
            return API("删除菜单数据", () =>
            {
                Service.DeleteBase(sysId);
            });
        }
        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<System_Menu> Get(string id)
        {
            return API("获取菜单数据",()=>
            {
                    var data = Service.Get(id);
                    return data;
             });
        }
        /// <summary>
        /// 获取菜单下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<object> Items([FromBody] ApiArgs<object> args)
        {
            return API<object>("查找菜单数据", () =>
            {
                var q = args.Query();
                var data = Service.GetItem(q.OrderBy, q.Sql, q.Args);
                return data;
            });
        }
        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> Page([FromBody] ApiArgs<object> args)
        {
            return API("获取菜单数据", () =>
             {
                 var q = args.Query();
                 var data = Service.GetPage(args.Page, args.Rows, out int total, q.OrderBy, q.Sql, q.Args);
                 return new PageData<object>
                 {
                     total = total,
                     index = args.Page,
                     size = args.Rows,
                     rows = data
                 };
             });
        }
    }
}