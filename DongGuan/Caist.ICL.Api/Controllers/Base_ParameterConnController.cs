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
    public class Base_ParameterConnController : ApiController
    {
        Base_ParameterConnService service;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public Base_ParameterConnController(Base_ParameterConnService service)
        {
            this.service = service;
        }
        /// <summary>
        /// 保存指标关联表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> Save([FromBody] Base_ParameterConn entity)
        {
            return API("保存指标关联表", () =>
            {

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
                service.Insert(entity);

                return entity.Id;
            });
        }
        /// <summary>
        /// 删除字典条目表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<string> Delete(string id)
        {
            return API("删除指标关联", () =>
            {
                service.Delete(id);
                return id;
            });
        }
        /// <summary>
        /// 获取指标关联
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<Base_ParameterConn> Get(string id)
        {
            return API("获取指标关联", () =>
            {
                var data = service.Get(id);
                return data;
            });
        }
        /// <summary>
        /// 分页查找字典条目表
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> Page([FromBody] ApiArgs<object> args)
        {
            return API("查找字典条目表", () =>
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