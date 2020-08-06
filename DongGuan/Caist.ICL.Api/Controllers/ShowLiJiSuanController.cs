using Caist.ICL.Api.Models;
using Caist.ICL.Core.Models;
using Caist.ICL.Library;
using Caist.ICL.Models;
using Caist.ICL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Caist.ICL.Api.Controllers
{
    /// <summary>
    /// 用于计算受力
    /// </summary>
    public class ShowLiJiSuanController : ApiController
    {
        T_ShouLiFenXiService _service;

        Basic_LocusService _LocusService;
        /// <summary>
        /// 注入服务方法
        /// </summary>
        public ShowLiJiSuanController(Services.T_ShouLiFenXiService service, Services.Basic_LocusService LocusService)
        {
            _service = service;
            _LocusService = LocusService;
        }
        /// <summary>
        /// 受力计算API
        /// 电缆沟
        /// T=T0+µWL
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<string> AddShouLiJiSuan()
        {
            //return API("Excel信息导入", () =>
            return API("受力计算", () =>
            {
                var data = _LocusService.GetItemJisuan("CreateTime", " GisPoint!='' ", "");

                if (data.Count > 0)
                {
                    var sql = "delete from ForceAnalysis";
                    UnitOfWork.Execute(sql, "");
                    List<ForceAnalysis> list = new List<ForceAnalysis>();
                    for (int i = 0; i < data.Count - 1; i++)
                    {
                        // new System.Collections.Generic.Mscorlib_DictionaryValueCollectionDebugView<string, object>(((NPoco.PocoExpando)(new System.Collections.Generic.Mscorlib_CollectionDebugView<object>(data).Items[9])).Values).Items[6]
                        // new System.Collections.Generic.Mscorlib_DictionaryValueCollectionDebugView<string, object>(((NPoco.PocoExpando)(new System.Collections.Generic.Mscorlib_CollectionDebugView<object>(data).Items[17])).Values).Items[11]
                        // List<Basic_Locus> values =new List<Basic_Locus>((NPoco.PocoExpando)data[i]).Values;
                        var values = ((NPoco.PocoExpando)data[i]).Values.ToList();
                        var dd = ((NPoco.PocoExpando)data[i + 1]).Values.ToList();
                        string startx = values[5].ToString();
                        string starty = values[6].ToString();
                        //if()
                        //string startz= values[7].ToString();
                        string endx = dd[5].ToString();
                        string endy = dd[6].ToString();
                        //string endz= dd[7].ToString();
                        //(values).Items[6]
                        //for (int j = 0; j < dd.Count - 1; j++)
                        //{

                        //    string endy = dd[j].ToString();
                        //    //string endz = data[i + 1]["Zvaules"].ToString();
                        //    //string startx = dr["Xvalues"].ToString();
                        //    //string starty = dr["Yvaules"].ToString();
                        //    //string startz = dr["Zvaules"].ToString();
                        //}
                        //ListItem dr = data[i];

                        list.Add(Tools.CalcStress(new StressEntity()
                        {
                            Index = i,
                            values = values,
                            dd = dd,
                            StartX = startx,
                            StartY = starty,
                            EndX = endx,
                            EndY = endy
                        }));
                        //myshouli.EndZ = endz;

                        ////vT2
                        //string section = dr["PointName"].ToString() + "-" + dr2["PointName"].ToString();
                        //string insertsql = "insert into ForceAnalysis values(newid()," + i.ToString() + ",'" + section + "','管沟'," + ifhege + ",'" + jianyi + "','" + dr["Materail"].ToString() + "'," + Convert.ToDecimal(lenghtss).ToString("0.00") + ",'" + startx + "','" + starty + "','" + startz + "','" + endx + "','" + endy + "','" + endz + "','" + Convert.ToDecimal(T1).ToString("0.00") + "','" + Convert.ToDecimal(totalqianyingli).ToString("0.00") + "',getdate())";
                        //DbHelperSQL.ExecuteSql(insertsql);
                    }
                    _service.Insert(list);
                    //MessageBox.Show("计算完成");
                    ////加载计算结果数据库表内容
                    //this.ShowResult();
                }
                return "计算完成";
            }
            );
        }


        /// <summary>
        /// 分页查找计算结果信息
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<PageData<object>> Page([FromBody] ApiArgs<object> args)
        {
            return API("查找计算结果信息", () =>
            {
                var q = args.Query();
                var data = _service.GetPage(args.Page, args.Rows, out int total, q.OrderBy, q.Sql, q.Args);

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
        /// 计算倾斜直线向下段公式
        /// </summary>
        /// <param name="line"></param>
        /// <param name="result_digit"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<double> CalcLineDown(string strType, Line line, int result_digit)
        {
            return API("受力计算", () =>
            {
                double T1 = line.prevPullPower;

                double W = line.weight;

                double L1 = line.length1;

                double μ = line.friction;
                double θ1 = line.angleDown;

                //Math.Cos(θ1);
                //Math.Sin(θ1);

                double angleValue = Math.Cos(θ1);

                if (angleValue == 0)
                {
                    //SystemLog.WriteLine("Cos(θ1)：" + angleValue + "，不是需要的结果！");
                }
                //else
                //{
                //    SystemLog.WriteLine("Cos(θ1)：" + angleValue);
                //}


                // SystemLog.WriteLine("");

                double T2 = T1 + W * L1 / angleValue * (μ * angleValue - Math.Sin(θ1));

                T2 = Convert.ToDouble(decimal.Round(Convert.ToDecimal(T2), result_digit));

                return T2;
            });
        }
        /// <summary>
        /// 计算倾斜直线向上段公式 张鹏20181206 add
        /// </summary>
        /// <param name="line"></param>
        /// <param name="result_digit"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<double> CalcLineUp(Line line, int result_digit)
        {
            return API("受力计算", () =>
            {
                double T1 = line.prevPullPower;

                double W = line.weight;

                double L1 = line.length1;

                double μ = line.friction;
                double θ2 = line.angleUp;

                //Math.Cos(θ1);
                //Math.Sin(θ1);

                double angleValue = Math.Cos(θ2);

                if (angleValue == 0)
                {
                    //SystemLog.WriteLine("Cos(θ2)：" + angleValue + "，不是需要的结果！");
                }
                //else
                //{
                //    SystemLog.WriteLine("Cos(θ1)：" + angleValue);
                //}


                //SystemLog.WriteLine("");

                double T2 = T1 + W * L1 / angleValue * (μ * angleValue + Math.Sin(θ2));

                T2 = Convert.ToDouble(decimal.Round(Convert.ToDecimal(T2), result_digit));

                //isSuc = true;

                return T2;
            });
        }
        /// <summary>
        /// 计算垂直凹面向下段公式
        /// </summary>
        /// <param name="line"></param>
        /// <param name="result_digit"></param>
        /// <param name="isSuc"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<double> LineConcaveDown(Line line, int result_digit)
        {
            return API("受力计算", () =>
            {
                double T1 = line.prevPullPower;

                double W = line.weight;

                double μ = line.friction;
                double θ1 = line.angleDown;

                double R = line.radius;

                //Math.Cos(θ1);
                //Math.Sin(θ1);


                //SystemLog.WriteLine("");

                double resultValue = MathNet.Numerics.Integration.DoubleExponentialTransformation.Integrate(θ => μ * Math.Cos(θ) - Math.Sin(θ), 0, θ1, 1e-8);

                double T2 = T1 + W * R * resultValue;

                T2 = Convert.ToDouble(decimal.Round(Convert.ToDecimal(T2), result_digit));

                //isSuc = true;

                return T2;
            });
        }
        /// <summary>
        /// 计算垂直凹面向上段公式
        /// </summary>
        /// <param name="line"></param>
        /// <param name="result_digit"></param>
        /// <param name="isSuc"></param>
        /// <returns></returns>
        [HttpPost]
        public ApiResult<double> LineConcaveUp(Line line, int result_digit)
        {
            return API("受力计算", () =>
            {


                double T1 = line.prevPullPower;

                double W = line.weight;

                double μ = line.friction;
                double θ2 = line.angleUp;

                double R = line.radius;

                //Math.Cos(θ1);
                //Math.Sin(θ1);


                // SystemLog.WriteLine("");

                double resultValue = MathNet.Numerics.Integration.DoubleExponentialTransformation.Integrate(θ => μ * Math.Cos(θ) + Math.Sin(θ), 0, θ2, 1e-8);

                double T2 = T1 + W * R * resultValue;

                T2 = Convert.ToDouble(decimal.Round(Convert.ToDecimal(T2), result_digit));

                //i/*s*/Suc = true;

                return T2;
            });
        }
    }
}