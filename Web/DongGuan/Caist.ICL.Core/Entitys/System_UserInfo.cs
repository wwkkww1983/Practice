/****************************************************************************************************
*项目:			ICL 
*创建人:			Lixz
*创建时间:		2019/5/21 17:12:57
*说明:			用户信息表 - System_UserInfo 
*****************************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Core.Entitys
{
    /// <summary>
    /// 用户信息表 - System_UserInfo
    /// </summary>
    public class System_UserInfo : BaseEntity
    {
    
        /// <summary>
        /// 用户名 - UserName
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户类型 1=平台用户 2=业主 3=商家 4=社区管理员 - UserType
        /// </summary>
        public int? UserType { get; set; }
        /// <summary>
        /// 手机号 - Phone
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 邮箱 - Email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 密码 - Password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 邀请人 - Inviter
        /// </summary>
        public string Inviter { get; set; }
        /// <summary>
        /// 邀请码 - InviteCode
        /// </summary>
        public string InviteCode { get; set; }
        /// <summary>
        /// 头像 - PictureIco
        /// </summary>
        public byte[] PictureIco { get; set; }
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

