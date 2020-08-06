using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Models
{
    public class System_Menu: BaseEntity
    {

        /// <summary>
        /// Menu_Name - 菜单名称
        /// </summary>
        public string Menu_Name { get; set; }
        /// <summary>
        /// Menu_Url - 链接地址
        /// </summary>
        public string Menu_Url { get; set; }
        /// <summary>
        /// Menu_Pid
        /// </summary>
        public string Menu_Pid { get; set; }
        public string Menu_Icon { get; set; }
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
