/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 17:12:00
*说明:			角色 - System_Role 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    /// 角色 - System_Role
    /// </summary>
    public class System_Role : BaseEntity
    {
    
        /// <summary>
        /// 编码 - RoleCode
        /// </summary>
        public string RoleCode { get; set; }
        /// <summary>
        /// 名称 - RoleName
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 备注 - Remark
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

