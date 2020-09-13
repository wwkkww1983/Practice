using Caist.ICL.Api.Models;
using Caist.ICL.Models;
using Caist.ICL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Caist.ICL.Api.Controllers
{
    public class ProjectInfoController:ApiController
    {
        ProjectInfoService service;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="service"></param>
        public ProjectInfoController(ProjectInfoService service)
        {
            this.service = service;
        }

        /// <summary>
        /// 保存项目信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> Save([FromBody] Project_Info entity)
        {
            return API("保存项目信息", () =>
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
        /// 删除保存项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<string> Delete(string id)
        {
            return API("删除项目信息", () =>
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
            return API("删除项目信息", () =>
            {
                service.DeleteBase(sysId);
            });
        }
        /// <summary>
        /// 获取项目信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<Project_Info> Get(string id)
        {
            return API("获取项目信息", () =>
            {
                var data = service.Get(id);
                return data;
            });
        }
        /// <summary>
        /// 获取项目信息表下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<object> Items([FromBody] ApiArgs<object> args)
        {
            return API<object>("查找项目信息", () =>
            {
                var q = args.Query();
                var data = service.GetItem(q.OrderBy, q.Sql, q.Args);
                return data;
            });
        }

        /// <summary>
        /// 分页查找项目信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> Page([FromBody] ApiArgs<object> args)
        {
            return API("查找项目信息", () =>
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
