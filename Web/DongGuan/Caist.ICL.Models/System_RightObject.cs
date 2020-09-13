 /****************************************************************************************************
*项目:			ICL 
*创建人:			zmq
*创建时间:		2019/8/4  17:11:09
*说明:			功能对象 - System_RightObject 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Models
{
    /// <summary>
    /// 功能对象 - System_RightObject
    /// </summary>
    public class System_RightObject : BaseEntity
    {
    
        /// <summary>
        /// 父Id - ParentId
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 编码 - RightNo
        /// </summary>
        public string RightNo { get; set; }
        /// <summary>
        /// 名称 - Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 内容 - RightText
        /// </summary>
        public string RightText { get; set; }
        /// <summary>
        /// 是否可用 - IsVisible
        /// </summary>
        public bool? IsVisible { get; set; }
        /// <summary>
        /// 0=菜单 1=功能 - MenumOrPower
        /// </summary>
        public int? MenumOrPower { get; set; }


        /// <summary>
        /// 图标元素 - Ico
        /// </summary>
        public byte[] Ico { get; set; }
        /// <summary>
        /// 排序号 - SortNumber
        /// </summary>
        public int? SortNumber { get; set; }
        /// <summary>
        ///  - Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        ///  - CreateId
        /// </summary>
        public string CreateId { get; set; }
        /// <summary>
        /// 创建人姓名 - CreateUser
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        ///  - CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        ///  - ChangedUser
        /// </summary>
        public string ChangedUser { get; set; }
        /// <summary>
        /// 更新人姓名 - UpdateUser
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        ///  - UpdateTime
        /// </summary>
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        ///  - DeleteFlag
        /// </summary>
        public bool? DeleteFlag { get; set; }
    }
}

