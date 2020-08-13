/****************************************************************************************************
*创建人:			Lixz
*创建时间:		2019/5/21 17:58:42
*说明：			 - CL_SectionCable 
*****************************************************************************************************/
using Caist.ICL.Api.Models;
using Caist.ICL.Models;
using Caist.ICL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Caist.ICL.Api.Controllers
{
    /// <summary>
    ///  - CL_SectionCable 
    /// </summary>
    public class CL_SectionCableController : ApiController
    {
        CL_SectionCableService service;
        public CL_SectionCableController(CL_SectionCableService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 保存CL_SectionCable
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> Save([FromBody] CL_SectionCable entity)
        {
            return API("保存CL_SectionCable", () =>
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
        /// 删除CL_SectionCable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<string> Delete(string id)
        {
            return API("删除CL_SectionCable", () =>
            {
                service.Delete(id);
                return id;
            });
        }

        /// <summary>
        /// 获取CL_SectionCable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<CL_SectionCable> Get(string id)
        {
            return API("获取CL_SectionCable", () =>
            {
                var data = service.Get(id);
                return data;
            });
        }

        /// <summary>
        /// 获取CL_SectionCable下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<object> Items([FromBody] ApiArgs<object> args)
        {
            return API<object>("查找CL_SectionCable", () =>
            {
                var q = args.Query();
                var data = service.GetItem(q.OrderBy, q.Sql, q.Args);
                return data;
            });
        }

        /// <summary>
        /// 分页查找CL_SectionCable
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> Page([FromBody] ApiArgs<object> args)
        {
            return API("查找CL_SectionCable", () =>
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
