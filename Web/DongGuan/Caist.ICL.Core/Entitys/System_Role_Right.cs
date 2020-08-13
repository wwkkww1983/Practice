/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 17:12:33
*说明:			角色功能表 - System_Role_Right 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    /// 角色功能表 - System_Role_Right
    /// </summary>
    public class System_Role_Right : BaseEntity
    {
    
        /// <summary>
        /// 功能ID - RightId
        /// </summary>
        public string RightId { get; set; }
        /// <summary>
        /// 功能码ID - ObjectCode
        /// </summary>
        public string ObjectCode { get; set; }
        /// <summary>
        /// 角色Id - RoleId
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名称 - RoleName
        /// </summary>
        public string RoleName { get; set; }
    }
}

