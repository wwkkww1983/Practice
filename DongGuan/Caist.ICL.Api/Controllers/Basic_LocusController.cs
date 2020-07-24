using Caist.ICL.Api.Models;
using Caist.ICL.Core.Entitys;
using Caist.ICL.Services;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.FormulaParsing.ExpressionGraph;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Caist.ICL.Api.Controllers
{
    /// <summary>
    /// 轨迹点表 - Basic_Locus
    /// </summary>
    public class Basic_LocusController : ApiController
    {
        Basic_LocusService service;
        public Basic_LocusController(Basic_LocusService service)
        {
            this.service = service;
        }
        /// <summary>
        /// 获取Basic_Locus
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<Basic_Locus> Get(string id)
        {
            return API("获取轨迹点表", () =>
            {
                var data = service.Get(id);
                return data;
            });
        }

        /// <summary>
        /// 获取轨迹点下拉列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<object> Items([FromBody] ApiArgs<object> args)
        {
            return API<object>("查找轨迹点表", () =>
            {
                var q = args.Query();
                var data = service.GetItem(q.OrderBy, q.Sql, q.Args);
                return data;
            });
        }

        /// <summary>
        /// 分页查找轨迹点
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> Page([FromBody] ApiArgs<object> args)
        {
            return API("查找轨迹点表", () =>
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
        /// <summary>
        /// 更据条件获取X，Y，Z的信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> GetLocusPint([FromBody] ApiArgs<object> args)
        {
            return API("", () =>
             {
                 var q = args.Query();
                 var data = service.GetPage(args.Page, 10, out int total, "createtime", q.Sql, q.Args);
                 string strPointX = "";
                 string strPointY = "";
                 string strPointZ = "";
                 string strPoint = "";
                 string strSheBei = "";

                 for (int i = 0; i < data.Count; i++)
                 {
                     // new System.Collections.Generic.Mscorlib_DictionaryValueCollectionDebugView<string, object>(((NPoco.PocoExpando)(new System.Collections.Generic.Mscorlib_CollectionDebugView<object>(data).Items[9])).Values).Items[6]
                     // new System.Collections.Generic.Mscorlib_DictionaryValueCollectionDebugView<string, object>(((NPoco.PocoExpando)(new System.Collections.Generic.Mscorlib_CollectionDebugView<object>(data).Items[17])).Values).Items[11]
                     // List<Basic_Locus> values =new List<Basic_Locus>((NPoco.PocoExpando)data[i]).Values;
                     var values = ((NPoco.PocoExpando)data[i]).Values.ToList();
                     string strX = values[5].ToString();
                     string strY = values[6].ToString();
                     string strIsSheBei = "无";
                     string strZ = values[7].ToString();
                     if (values[22] != null)
                     {
                         strIsSheBei = values[22].ToString();
                     }
                     //if()
                     //string startz= values[7].ToString();
                     strPointX += strX + ",";
                     strPointY += strY + ",";
                     strPointZ += strZ + ",";

                     strSheBei += strIsSheBei + ",";
                 }
                 strPoint = strPointX.TrimEnd(',') + ";" + strPointY.TrimEnd(',') + ";" + strPointZ.TrimEnd(',') + ";" + strSheBei;
                 return strPoint;
             }
            );
        }

        /// <summary>
        /// 根据位置获取点位相应的设备
        /// </summary>
        /// <param name="section"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<System.Collections.Generic.List<Basic_Locus>> getEquipment([FromBody] Basic_Locus basic_Locus)
        {
            return API("", () =>
            {
                //TODO:用表达式过滤，待完善
                System.Collections.Generic.List<Basic_Locus> data = service.GetList(t => t.Point_X == basic_Locus.Point_X && t.Point_Y == basic_Locus.Point_Y && t.SectionId == basic_Locus.SectionId);
                return data;
            }
            );
        }
        /// <summary>
        /// 分页查找轨迹点
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> pageDetail([FromBody] ApiArgs<object> args)
        {
            return API("查找轨迹点表", () =>
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
        /// <summary>
        /// 删除轨迹点表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}")]
        public ApiResult<string> Delete(string id)
        {
            return API("删除轨迹点表", () =>
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
        public ApiResult<object> DeleteBase([FromBody] Core.BaseEntity[] sysId)
        {
            return API("删除轨迹点表", () =>
            {
                service.DeleteBase(sysId);
            });
        }
        /// <summary>
        /// 保存项目信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> Save([FromBody] Basic_Locus entity)
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
    }
}