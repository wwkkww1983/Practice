using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caist.Framework.Business.Baojinghistory;
using Caist.Framework.Entity.Baojinghistory;
using Caist.Framework.Model.Param.Baojinghistory;
using Caist.Framework.Util.Extension;
using Caist.Framework.Util.Model;
using Microsoft.AspNetCore.Mvc;

namespace Caist.Framework.Web.Areas.BojingMonitor
{
    [Area("BojingMonitor")]
    public class BojingController : Controller
    {
        private BojingMonitorBLL bojingMonitorBLL = new BojingMonitorBLL();
        public IActionResult Bojing()
        {
            return View();
        }
        [HttpPost]
        public async Task<string> GetBojingTempLists([FromBody]BojingMonitorParam param)
        {
            var obj = await bojingMonitorBLL.GetSecurityInfoList(param);
            BaoJingName(obj);

            return obj.RemoveNullValue().ToLower();
        }
        /// <summary>
        /// 报警图表名称转换 
        /// TODO:临时处理办法，后续可优化
        /// </summary>
        /// <param name="obj"></param>
        private static void BaoJingName(TData<List<BojingMonitorEntity>> obj)
        {
            foreach (var item in obj.Result)
            {
                if (item.Name.Contains("通风"))
                {
                    obj.Result[obj.Result.IndexOf(item)].Name = "通风机";
                }
                else if (item.Name.Contains("压风"))
                {
                    obj.Result[obj.Result.IndexOf(item)].Name = "压风机";
                }
                else if (item.Name.Contains("皮带"))
                {
                    obj.Result[obj.Result.IndexOf(item)].Name = "皮带";
                }
                else if (item.Name.Contains("水泵"))
                {
                    obj.Result[obj.Result.IndexOf(item)].Name = "水泵";
                }
                else if (item.Name.Contains("局部"))
                {
                    obj.Result[obj.Result.IndexOf(item)].Name = "局部风机";
                }
                else if (item.Name.Contains("光纤"))
                {
                    obj.Result[obj.Result.IndexOf(item)].Name = "光纤测温";
                }
                else if (item.Name.Contains("安全"))
                {
                    obj.Result[obj.Result.IndexOf(item)].Name = "安全监测";
                }
                else if (item.Name.Contains("人员"))
                {
                    obj.Result[obj.Result.IndexOf(item)].Name = "人员定位";
                }
                else if (item.Name.Contains("供配电"))
                {
                    obj.Result[obj.Result.IndexOf(item)].Name = "供配电";
                }
                else if (item.Name.Contains("视频"))
                {
                    obj.Result[obj.Result.IndexOf(item)].Name = "视频监控";
                }
            }
        }
    }
}
