using Caist.ICL.Api.Models;
using Caist.ICL.Models;
using Caist.ICL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Caist.ICL.Api.Controllers
{
    public class GisInfoController : ApiController
    {
        GisInfoService service;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public GisInfoController(GisInfoService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 保存地理信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> Save([FromBody] GisCoordinateSystem entity)
        {
            return API("保存地理信息", () =>
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
        /// 删除地理信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<string> Delete(string id)
        {
            return API("删除地理信息", () =>
            {
                service.Delete(id);
                return id;
            });
        }

        /// <summary>
        /// 获取地理信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<GisCoordinateSystem> Get(string id)
        {
            return API("获取地理信息", () =>
            {
                var data = service.Get(id);
                return data;
            });
        }

        /// <summary>
        /// 分页查找地理信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> Page([FromBody] ApiArgs<object> args)
        {
            return API("查找地理信息", () =>
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