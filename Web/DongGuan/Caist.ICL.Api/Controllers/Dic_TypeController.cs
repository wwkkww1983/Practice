/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/5/21 17:58:42
*说明：			字典类型表 - BaseInfo_Dictype 
*****************************************************************************************************/
using Caist.ICL.Api.Models;
using Caist.ICL.Models;
using Caist.ICL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Caist.ICL.Api.Controllers
{
    /// <summary>
    /// 字典类型表 - Dic_Type 
    /// </summary>
    public class Dic_TypeController : ApiController
    {
        Dic_TypeService service;
        public Dic_TypeController(Dic_TypeService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 保存字典类型表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> Save([FromBody] Dic_Type entity)
        {
            return API("保存字典类型表", () =>
            {
                entity.UpdateId = User.UserID;
                entity.UpdateUser = User.UserName;
                entity.UpdateTime = DateTime.Now;
                if (!string.IsNullOrEmpty(entity.Id))
                {
                    var old = service.Get(entity.Id);
                    if (old != null)
                    {
                        service.Update(entity, old);
                        return entity.Id;
                    }
                }
                entity.Id = NewGuid();
                entity.CreateId = User.UserID;
                entity.CreateUser = User.UserName;
                entity.CreateTime = DateTime.Now;
                service.Insert(entity);

                return entity.Id;
            });
        }

        /// <summary>
        /// 删除字典类型表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<string> Delete(string id)
        {
            return API("删除字典类型表", () =>
            {
                if (id.Contains(','))
                {
                    string[] ids = id.Split(',');
                    foreach (string strid in ids)
                    {
                        service.Delete(strid);
                    }
                }
                else
                { 
                    service.Delete(id);
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
        public ApiResult<object> DeleteBase([FromBody]BaseEntity[] sysId)
        {
            return API("删除字典类型表", () =>
            {
                service.DeleteBase(sysId);
            });
        }
        /// <summary>
        /// 获取字典类型表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<Dic_Type> Get(string id)
        {
            return API("获取字典类型表", () =>
            {
                var data = service.Get(id);
                return data;
            });
        }

        /// <summary>
        /// 获取字典类型表下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<object> Items([FromBody] ApiArgs<object> args)
        {
            return API<object>("查找字典类型表", () =>
            {
                var q = args.Query();
                var data = service.GetItem(q.OrderBy, q.Sql, q.Args);
                return data;
            });
        }

        /// <summary>
        /// 分页查找字典类型表
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> Page([FromBody] ApiArgs<object> args)
        {
            return API("查找字典类型表", () =>
            {
                var q = args.Query();
                var data = service.GetPage(args.Page, args.Rows, out int total, q.OrderBy, q.Sql, q.Args);

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
