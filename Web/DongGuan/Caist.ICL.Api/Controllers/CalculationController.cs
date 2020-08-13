/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/5/21 17:58:42
*说明：			计算结果 - Calculation 
*****************************************************************************************************/
using Caist.ICL.Api.Models;
using Caist.ICL.Models;
using Caist.ICL.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Caist.ICL.Api.Controllers
{
    /// <summary>
    /// 计算结果 - Calculation 
    /// </summary>
    public class CalculationController : ApiController
    {
        CalculationService service;
        public CalculationController(CalculationService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 保存计算结果
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> Save([FromBody] Calculation entity)
        {
            return API("保存计算结果", () =>
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
        /// 删除计算结果
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<string> Delete(string id)
        {
            return API("删除计算结果", () =>
            {
                service.Delete(id);
                return id;
            });
        }

        /// <summary>
        /// 获取计算结果
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<Calculation> Get(string id)
        {
            return API("获取计算结果", () =>
            {
                var data = service.Get(id);
                return data;
            });
        }

        /// <summary>
        /// 获取计算结果下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<object> Items([FromBody] ApiArgs<object> args)
        {
            return API<object>("查找计算结果", () =>
            {
                var q = args.Query();
                var data = service.GetItem(q.OrderBy, q.Sql, q.Args);
                return data;
            });
        }

        /// <summary>
        /// 分页查找计算结果
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> Page([FromBody] ApiArgs<object> args)
        {
            return API("查找计算结果", () =>
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
