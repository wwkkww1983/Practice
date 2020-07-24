using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace DLClient
{
    public enum EffentNextType
    {
        /// <summary>
        /// 对其他语句无任何影响 
        /// </summary>
        None,
        /// <summary>
        /// 当前语句必须为"select count(1) from .."格式，如果存在则继续执行，不存在回滚事务
        /// </summary>
        WhenHaveContine,
        /// <summary>
        /// 当前语句必须为"select count(1) from .."格式，如果不存在则继续执行，存在回滚事务
        /// </summary>
        WhenNoHaveContine,
        /// <summary>
        /// 当前语句影响到的行数必须大于0，否则回滚事务
        /// </summary>
        ExcuteEffectRows,
        /// <summary>
        /// 引发事件-当前语句必须为"select count(1) from .."格式，如果不存在则继续执行，存在回滚事务
        /// </summary>
        SolicitationEvent
    }


    public class CommandInfo
    {
        public object ShareObject = null;
        public object OriginalData = null;
        event EventHandler _solicitationEvent;
        public event EventHandler SolicitationEvent
        {
            add
            {
                _solicitationEvent += value;
            }
            remove
            {
                _solicitationEvent -= value;
            }
        }
        public void OnSolicitationEvent()
        {
            if (_solicitationEvent != null)
            {
                _solicitationEvent(this, new EventArgs());
            }
        }
        public string CommandText;
        public System.Data.Common.DbParameter[] Parameters;
        public EffentNextType EffentNextType = EffentNextType.None;
        public CommandInfo()
        {

        }
        public CommandInfo(string sqlText, SqlParameter[] para)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
        }
        public CommandInfo(string sqlText, SqlParameter[] para, EffentNextType type)
        {
            this.CommandText = sqlText;
            this.Parameters = para;
            this.EffentNextType = type;
        }
    }


    public enum ClearMode
    {
        Part = 1,
        All = 2
    }
    public enum OperationMode
    {
        View = 1,
        Edit = 2
    }
    public class MapUtils
    {
        //private static double EARTH_RADIUS = 6378.137;
        private static double EARTH_RADIUS = 6371.393;

        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }

        /**
         * 计算两个经纬度之间的距离(单位/米)
         */
        public static double GetDistance(double lat1, double lng1, double lat2, double lng2)
        {
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);
            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
                    Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 1000);
            return s;
        }
    }

    public class PositionMark
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double? Lat { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double? Lng { get; set; }
        /// <summary>
        /// 当前地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 两点距离
        /// </summary>
        public double? Distance { get; set; }
        /// <summary>
        /// 缩放层级
        /// </summary>
        public double? Zoom { get; set; }
        public int? Type { get; set; }
    }

    public class MapControlModel
    {
        public string SectionCode { get; set; }
        public string SectionId { get; set; }

        public OperationMode operationMode { get; set; }

        public int Level{ get; set; }
    }

    public static class Common
    {
        public static string IgnoreNull<T>(this T listPosition)
        {
            return JsonConvert.SerializeObject(listPosition, new JsonSerializerSettings()
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
    }
}
