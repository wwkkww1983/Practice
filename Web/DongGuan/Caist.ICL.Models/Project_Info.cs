/****************************************************************************************************
*项目:			ICL 
*创建人:			zmq
*创建时间:		2019/8/4  16:41:34
*说明:			工程信息 - Project_Info 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Models
{
    /// <summary>
    /// 工程信息 - Project_Info
    /// </summary>
    public class Project_Info : BaseEntity
    {
    
        /// <summary>
        /// 工程编号 - ProjectCode
        /// </summary>
        public string ProjectCode { get; set; }
        /// <summary>
        /// 工程名称 - ProjectName
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 地址 - Location
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// 工程长度 - Distance
        /// </summary>
        public decimal? Distance { get; set; }
        /// <summary>
        /// 工程状态 - State
        /// </summary>
        public int? State { get; set; }
        /// <summary>
        /// 分段数 - SectionCount
        /// </summary>
        public int? SectionCount { get; set; }
        /// <summary>
        /// 回路数 - CircuitCount
        /// </summary>
        public int? CircuitCount { get; set; }
        /// <summary>
        /// 电流类型 - ClectricityType
        /// </summary>
        public string ClectricityType { get; set; }
        /// <summary>
        /// 敷设环境 - LayEnvironment
        /// </summary>
        public string LayEnvironment { get; set; }
        /// <summary>
        /// 温度 - Temperature
        /// </summary>
        public decimal? Temperature { get; set; }
        /// <summary>
        /// 湿度 - Humidity
        /// </summary>
        public decimal? Humidity { get; set; }
        /// <summary>
        /// 电压等级 - Voltage
        /// </summary>
        public string Voltage { get; set; }
        /// <summary>
        /// 截面尺寸 - CutSize
        /// </summary>
        public decimal? CutSize { get; set; }
        /// <summary>
        /// 建设单位 - BuildUnit
        /// </summary>
        public string BuildUnit { get; set; }
        /// <summary>
        /// 建设单位联系人 - BuildContacts
        /// </summary>
        public string BuildContacts { get; set; }
        /// <summary>
        /// 建设单位联系电话 - BuildTelephon
        /// </summary>
        public string BuildPhone { get; set; }
        /// <summary>
        /// 联系人 - Contacts
        /// </summary>
        public string DevContacts { get; set; }
        /// <summary>
        /// 施工单位联系电话 - Phone
        /// </summary>
        public string DevPhone { get; set; }
        /// <summary>
        /// 监理单位 - Supervision
        /// </summary>
        public string SuperUnit { get; set; }
        /// <summary>
        /// 监理单位联系人 - SuperContacts
        /// </summary>
        public string SuperContacts { get; set; }
        /// <summary>
        /// 监理单位联系电话 - SuperPhone
        /// </summary>
        public string SuperPhone { get; set; }
        /// <summary>
        ///  - NowProject
        /// </summary>
        public bool? NowProject { get; set; }
        /// <summary>
        /// 备注 - Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        ///  - CreateId
        /// </summary>
        public string CreateId { get; set; }
        /// <summary>
        ///  - CreateUser
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        ///  - CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        ///  - UpdateId
        /// </summary>
        public string UpdateId { get; set; }
        /// <summary>
        /// 修改人用户名 - UpdateUser
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        /// 修改时间 - UpdateTime
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// 是否删除 - Delteted
        /// </summary>
        public bool? Delteted { get; set; }

        /// <summary>
        /// 省 - provinceName
        /// </summary>
        public string provinceName { get; set; }
        /// <summary>
        /// 市 - cityName
        /// </summary>
        public string cityName { get; set; }
        /// <summary>
        /// 区 - districtName
        /// </summary>
        public string districtName { get; set; }
    }
}

