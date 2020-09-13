using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Models
{
    public class System_MenuGrant:BaseEntity
    {
        /// <summary>
        /// RoleId - 权限ID
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// Menu_Id - 菜单ID
        /// </summary>
        public string Menu_Id { get; set; }
        /// <summary>
        /// grant_query - 查询权限
        /// </summary>
        public int grant_query { get; set; }
        /// <summary>
        /// grant_create - 新增权限
        /// </summary>
        public int grant_create { get; set; }
        /// <summary>
        /// grant_edit 编辑权限
        public int grant_edit { get; set; }
        /// <summary>
        /// grant_delete 删除权限
        public int grant_delete { get; set; }
        /// <summary>
        /// grant_print 打印权限
        /// </summary>
        public int grant_print { get; set; }
        /// 创建人姓名 - CreateUser
        /// </summary>
        public string CreateUser { get; set; }
        /// <summary>
        ///  - CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// 更新人 - UpdateUser
        /// </summary>
        public string UpdateUser { get; set; }
        /// <summary>
        ///  - UpdateTime
        /// </summary>
        public DateTime? UpdateTime { get; set; }
    }
}
