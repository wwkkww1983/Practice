/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 17:13:27
*说明:			用户角色关联表 - System_UserOfRoles 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    /// 用户角色关联表 - System_UserOfRoles
    /// </summary>
    public class System_UserOfRoles : BaseEntity
    {
    
        /// <summary>
        ///  - UserId
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        ///  - RoleId
        /// </summary>
        public string RoleId { get; set; }
    }
}

