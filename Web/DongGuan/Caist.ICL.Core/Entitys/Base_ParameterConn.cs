/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 14:21:52
*说明:			指标关联表 - Base_ParameterConn 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    /// 指标关联表 - Base_ParameterConn
    /// </summary>
    public class Base_ParameterConn : BaseEntity
    {
        /// <summary>
        /// 指标ID - IndexId
        /// </summary>
        public string IndexId { get; set; }
        /// <summary>
        /// 引用的指标ID - ConnectIndexId
        /// </summary>
        public string ConnectIndexId { get; set; }
    }
}

