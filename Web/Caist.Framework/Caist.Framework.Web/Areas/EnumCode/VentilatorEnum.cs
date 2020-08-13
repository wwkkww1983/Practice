using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.EnumCode
{
    public enum VentilatorEnum
    {
        /// <summary>
        ///一部皮带
        /// </summary>
        //电压
        [Description("192.168.20.112:DB16.DBD2")]
         Current,
        //电流
        [Description("192.168.20.112:DB16.DBD6")]
         Voltage,
        //电机温度
        [Description("192.168.20.112:DB16.DBD6")]
         Motortemperature,
        //速度
        [Description("192.168.20.112:DB16.DBD6")]
        speed,
        //滚筒温度
        [Description("192.168.20.112:DB16.DBD6")]
         Drumtemperature,
        /// <summary>
        ///二部皮带
        /// </summary>
        //电压
        [Description("192.168.20.112:DB16.DBD2")]
          Current1,
        //电流
        [Description("192.168.20.112:DB16.DBD6")]
         Voltage1,
        //电机温度
        [Description("192.168.20.112:DB16.DBD6")]
          Motortemperature1,
        //速度
        [Description("192.168.20.112:DB16.DBD6")]
          speed1,
        //滚筒温度
        [Description("192.168.20.112:DB16.DBD6")]
         Drumtemperature1,
        /// <summary>
        ///三部皮带
        /// </summary>
        //电压
        [Description("192.168.20.112:DB16.DBD2")]
          Current2,
        //电流
        [Description("192.168.20.112:DB16.DBD6")]
         Voltage2,
        //电机温度
        [Description("192.168.20.112:DB16.DBD6")]
          Motortemperature2,
        //速度
        [Description("192.168.20.112:DB16.DBD6")]
          speed2,
        //滚筒温度
        [Description("192.168.20.112:DB16.DBD6")]
         Drumtemperature2,
        
    }
}