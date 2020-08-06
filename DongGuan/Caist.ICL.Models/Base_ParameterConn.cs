/****************************************************************************************************
*项目:			ICL 
*创建人:			zmq
*创建时间:		2019/8/4 9:10:52
*说明:			指标关联表 - Base_ParameterConn 
*****************************************************************************************************/

namespace Caist.ICL.Models
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

