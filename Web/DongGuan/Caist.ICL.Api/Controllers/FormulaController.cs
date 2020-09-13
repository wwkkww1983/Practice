/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/5/21 17:58:42
*说明：			力学公式 - Formula 
*****************************************************************************************************/
using Caist.ICL.Api.Models;
using Caist.ICL.Models;
using Caist.ICL.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Caist.ICL.Api.Controllers
{
    /// <summary>
    /// 力学公式 - Formula 
    /// </summary>
    public class FormulaController : ApiController
    {
        FormulaService service;
        public FormulaController(FormulaService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 保存力学公式
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> Save([FromBody] Formula entity)
        {
            return API("保存力学公式", () =>
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
        /// 删除力学公式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<string> Delete(string id)
        {
            return API("删除力学公式", () =>
            {
                service.Delete(id);
                return id;
            });
        }

        /// <summary>
        /// 获取力学公式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<Formula> Get(string id)
        {
            return API("获取力学公式", () =>
            {
                var data = service.Get(id);
                return data;
            });
        }

        /// <summary>
        /// 获取力学公式下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<object> Items([FromBody] ApiArgs<object> args)
        {
            return API<object>("查找力学公式", () =>
            {
                var q = args.Query();
                var data = service.GetItem(q.OrderBy, q.Sql, q.Args);
                return data;
            });
        }

        /// <summary>
        /// 分页查找力学公式
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> Page([FromBody] ApiArgs<object> args)
        {
            return API("查找力学公式", () =>
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
