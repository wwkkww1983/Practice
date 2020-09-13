using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.EnumCode
{
    public enum Tongfeng
    {
        #region 1#一级电参数
        [Description("192.168.20.112:DB16.DBX90.0")]
        Hori,
        //垂直振动
        [Description("192.168.20.112:DB16.DBX120.0")]
        Vert,
        //正压
        [Description("192.168.20.112:DB16.DBX60.0")]
        Barotropic,
        //负压
        [Description("192.168.20.112:DB16.DBX30.3")]
        under,
        #endregion

        #region 1#一级电参数
        //前轴温度
        [Description("192.168.20.112:DB16.DBX300.0")]
        Front,
        //后轴温度
        [Description("192.168.20.112:DB16.DBX330.0")]
        temperature,
        //A相温度
        [Description("192.168.20.112:DB16.DBX210.0")]
        Atemperature,
        //B相温度
        [Description("192.168.20.112:DB16.DBX240.0")]
        Btemperature,
        //C相温度
        [Description("192.168.20.112:DB16.DBX270.0")]
        Cuntemperatureder,
        #endregion

        #region 1#一级电参数
        //运行频率
        [Description("192.168.20.112:1")]
        ZH,
        //设定频率
        [Description("192.168.20.112:DB30.DBD4")]
        SDHZ,
        //母线电压
        [Description("192.168.20.112:DB30.DBD60")]
        busbarvoltage,
        //输出电压
        [Description("192.168.20.112:DB30.DBD28")]
        OperatingVoltage,
        //输出电流
        [Description("192.168.20.112:DB30.DBD20")]
        Outputpower,
        //输出功虑
        [Description("192.168.20.112:DB30.DBD52")]
        SCGL,
        //输出转矩
        [Description("192.168.20.112:DB30.DBD36")]
        SCZZ,
        //变频状态
        [Description("192.168.20.112:DB30.DBD12")]
        BPHZ,
        #endregion

        #region 1#二级电参数
        //水平振动
        [Description("192.168.20.112:1")]
        SPZH,
        //垂直振动
        [Description("192.168.20.112:1")]
        CZZH,
        //差压
        [Description("192.168.20.112:1")]
        CV,
        //风量
        [Description("192.168.20.112:1")]
        Air,
        //风速
        [Description("192.168.20.112:1")]
        velocityair,
        #endregion

        #region 1#二级电参数
        //前轴温度
        [Description("192.168.20.112:DB16.DBX300.0")]
        Front1,
        //后轴温度
        [Description("192.168.20.112:DB16.DBX330.0")]
        temperature1,
        //A相温度
        [Description("192.168.20.112:DB16.DBX210.0")]
        Atemperature1,
        //B相温度
        [Description("192.168.20.112:DB16.DBX240.0")]
        Btemperature1,
        //C相温度
        [Description("192.168.20.112:DB16.DBX270.0")]
        Cuntemperatureder1,
        #endregion

        #region 1#二级电参数
        //运行频率
        [Description("192.168.20.112:1")]
        ZH1,
        //设定频率
        [Description("192.168.20.112:DB30.DBD4")]
        SDHZ1,
        //母线电压
        [Description("192.168.20.112:DB30.DBD60")]
        busbarvoltage1,
        //输出电压
        [Description("192.168.20.112:DB30.DBD28")]
        OperatingVoltage1,
        //输出电流
        [Description("192.168.20.112:DB30.DBD20")]
        Outputpower1,
        //输出功虑
        [Description("192.168.20.112:DB30.DBD52")]
        SCGL1,
        //输出转矩
        [Description("192.168.20.112:DB30.DBD36")]
        SCZZ1,
        //变频状态
        [Description("192.168.20.112:DB30.DBD12")]
        BPHZ1,
        #endregion

        #region 1#通风机-蝶阀

        #endregion

        #region 2#一级电机参数
        //水平振动
        [Description("192.168.20.112:DB16.DBX90.1")]
        Hori2,
        //垂直振动
        [Description("192.168.20.112:DB16.DBX180.1")]
        Vert2,
        //正压
        [Description("192.168.20.112:DB16.DBX60.0")]
        Barotropic2,
        //负压
        [Description("192.168.20.112:DB16.DBX30.3")]
        under2,
        #endregion

        #region 2#一级电机参数
        //前轴温度
        [Description("192.168.20.112:DB16.DBX300.0")]
        Front2,
        //后轴温度
        [Description("192.168.20.112:DB16.DBX330.0")]
        temperature2,
        //A相温度
        [Description("192.168.20.112:DB16.DBX210.0")]
        Atemperature2,
        //B相温度
        [Description("192.168.20.112:DB16.DBX240.0")]
        Btemperature2,
        //C相温度
        [Description("192.168.20.112:DB16.DBX270.0")]
        Cuntemperatureder2,
        #endregion

        #region 2#一级电机参数
        //运行频率
        [Description("192.168.20.112:1")]
        ZH2,
        //设定频率
        [Description("192.168.20.112:DB30.DBD4")]
        SDHZ2,
        //母线电压
        [Description("192.168.20.112:DB30.DBD60")]
        busbarvoltage2,
        //输出电压
        [Description("192.168.20.112:DB30.DBD28")]
        OperatingVoltage2,
        //输出电流
        [Description("192.168.20.112:DB30.DBD20")]
        Outputpower2,
        //输出功虑
        [Description("192.168.20.112:DB30.DBD52")]
        SCGL2,
        //输出转矩
        [Description("192.168.20.112:DB30.DBD36")]
        SCZZ2,
        //变频状态
        [Description("192.168.20.112:DB30.DBD12")]
        BPHZ2,
        #endregion

        #region 2#二级电机参数
        //水平振动
        [Description("192.168.20.112:DB16.DBX150.0")]
        SPZH2,
        //垂直振动
        [Description("192.168.20.112:DB16.DBX180.0")]
        CZZH2,
        //差压
        [Description("192.168.20.112:DB16.DBX60.0")]
        CV2,
        //风量
        [Description("192.168.20.112:1")]
        Air2,
        //风速
        [Description("192.168.20.112:DB16.DBX0.0")]
        velocityair2,
        #endregion

        #region 2#二级电机参数
        //前轴温度
        [Description("192.168.20.112:DB16.DBX450.")]
        Fron,
        //后轴温度
        [Description("192.168.20.112:DB16.DBX480.0")]
        temperatur,
        //A相温度
        [Description("192.168.20.112:DB16.DBX360.0")]
        Atemperatur,
        //B相温度
        [Description("192.168.20.112:DB16.DBX390.0")]
        Btemperatur,
        //C相温度
        [Description("192.168.20.112:DB16.DBX420.0")]
        Cuntemperaturede,
        #endregion

        #region 2#二级电参数
        //运行频率
        [Description("192.168.20.112:1")]
        ZH11,
        //设定频率
        [Description("192.168.20.112:DB30.DBD4")]
        SDH,
        //母线电压
        [Description("192.168.20.112:DB31.DBD60")]
        busbarvoltag,
        //输出电压
        [Description("192.168.20.112:DB31.DBD28")]
        OperatingVoltag,
        //输出电流
        [Description("192.168.20.112:DB31.DBD20")]
        Outputpowe,
        //输出功虑
        [Description("192.168.20.112:DB31.DBD12")]
        SCG,
        //输出转矩
        [Description("192.168.20.112:DB31.DBD36")]
        SCZ,
        //变频状态
        [Description("192.168.20.112:DB31.DBD4")]
        BPH,
        #endregion

        #region 2#通风机-蝶阀

        #endregion
    }
}
