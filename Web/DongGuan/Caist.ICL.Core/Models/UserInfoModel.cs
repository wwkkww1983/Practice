using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Core.Models
{
    public class UserInfoModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 用户名 - UserName
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 用户类型
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
        /// 头像
        /// </summary>
        public byte[] PictureIco { get; set; }
        /// <summary>
        /// 编号 - MemberCode
        /// </summary>
        public string MemberCode { get; set; }
        /// <summary>
        /// 姓名 - Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 出生年月 - BirthDay
        /// </summary>
        public DateTime? BirthDay { get; set; }
        /// <summary>
        /// 性别 - Sex
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 名族 - National
        /// </summary>
        public string National { get; set; }
        /// <summary>
        /// 籍贯 - JiGuan
        /// </summary>
        public string JiGuan { get; set; }
        /// <summary>
        /// 住址 - LiveIn
        /// </summary>
        public string LiveIn { get; set; }
        /// <summary>
        ///  - SpellLogn
        /// </summary>
        public string SpellLogn { get; set; }
        /// <summary>
        ///  - Phoneticize
        /// </summary>
        public string Phoneticize { get; set; }
        /// <summary>
        /// 专业 - Specialty
        /// </summary>
        public string Specialty { get; set; }
        /// <summary>
        /// 毕业院校 - Graduate
        /// </summary>
        public string Graduate { get; set; }
        /// <summary>
        /// 最高学历 - Education
        /// </summary>
        public string Education { get; set; }
        /// <summary>
        /// 行业 - Trade
        /// </summary>
        public string Trade { get; set; }
        /// <summary>
        /// 当前工作单位 - WorkAt
        /// </summary>
        public string WorkAt { get; set; }
        /// <summary>
        /// 当前工作岗位 - Positions
        /// </summary>
        public string Positions { get; set; }
        /// <summary>
        ///  - Grade
        /// </summary>
        public int? Grade { get; set; }
        /// <summary>
        /// 是否已婚 - IsMarry
        /// </summary>
        public bool? IsMarry { get; set; }
        /// <summary>
        /// 联系QQ - QQNo
        /// </summary>
        public string QQNo { get; set; }
        /// <summary>
        /// 联系微信 - WechatNo
        /// </summary>
        public string WechatNo { get; set; }
        /// <summary>
        /// 职称 - ProfessionalTitle
        /// </summary>
        public string ProfessionalTitle { get; set; }

        /// <summary>
        /// 照片
        /// </summary>
        public byte[] Picture { get; set; }


        /// <summary>
        /// 小区ID
        /// </summary>
        public string CommunityId { get; set; }

        /// <summary>
        /// 房号
        /// </summary>
        public string HouseId { get; set; }
        public string HouseNo { get; set; }

        public int subjectCount{ get; set; }
        public int replyCount { get; set; }

    }
}
