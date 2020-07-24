/****************************************************************************************************
*创建人:			sty
*创建时间:		2019/5/28 15:40
*说明：			监测数据管理 - Monitoring 
*****************************************************************************************************/
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
    /// <summary>
    /// 监测数据管理 - Monitoring 
    /// </summary>
    public class Monitoring_DataController:ApiController
    {
        Monitoring_DataService service;
        /// <summary>
        /// 构造服务
        /// </summary>
        /// <param name="service"></param>
        public Monitoring_DataController(Monitoring_DataService service)
        {
            this.service = service;
        }
        /// <summary>
        /// 保存监测数据
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> Save([FromBody] Monitoring_Data entity)
        {
            return API("保存监测数据", () =>
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
        /// 删除监测数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<string> Delete(string id)
        {
            return API("删除监测数据", () =>
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
        public ApiResult<object> DeleteBase([FromBody]Core.BaseEntity[] sysId)
        {
            return API("删除监测数据", () =>
            {
                service.DeleteBase(sysId);
            });
        }
        /// <summary>
        /// 获取单个监测数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<Monitoring_Data> Get(string id)
        {
            return API("获取监测数据", () =>
            {
                var data = service.Get(id);
                return data;
            });
        }
        /// <summary>
        /// 获取监测数据下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<object> Items([FromBody] ApiArgs<object> args)
        {
            return API<object>("查找监测数据", () =>
            {
                var q = args.Query();
                var data = service.GetItem(q.OrderBy, q.Sql, q.Args);
                return data;
            });
        }
        /// <summary>
        /// 分页查找监测数据
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> Page([FromBody] ApiArgs<object> args)
        {
            return API("查找监测数据", () =>
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
