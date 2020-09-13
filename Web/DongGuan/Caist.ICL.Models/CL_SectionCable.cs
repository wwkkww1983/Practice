/****************************************************************************************************
*项目:			ICL 
*创建人:			zmq
*创建时间:		2019/8/4  14:21:53
*说明:			 - CL_SectionCable 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Models
{
    /// <summary>
    ///  - CL_SectionCable
    /// </summary>
    public class CL_SectionCable : BaseEntity
    {
    
        /// <summary>
        /// 电缆编号 - CableId
        /// </summary>
        public string CableId { get; set; }
        /// <summary>
        /// 分段编号 - SectionId
        /// </summary>
        public string SectionId { get; set; }
    }
}

