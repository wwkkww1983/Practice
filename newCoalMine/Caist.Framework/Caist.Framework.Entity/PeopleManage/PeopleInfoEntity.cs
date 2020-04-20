using System;

namespace Caist.Framework.Entity.PeopleManage
{
    [Serializable]
    public class PeopleInfoEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 序号
        /// </summary>
        public string No { get; set; }

        /// <summary>
        /// 发射器编号
        /// </summary>
        public string Sender_id { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string People_num { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string People_name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Dept { get; set; }

        /// <summary>
        /// 工种
        /// </summary>
        public string Worktype { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Rank { get; set; }

        /// <summary>
        /// 进入区域时间
        /// </summary>
        public string First_report_time { get; set; }

        /// <summary>
        /// 停留区域时间
        /// </summary>
        public string Stay_report_time { get; set; }

        /// <summary>
        /// 当前位置编号
        /// </summary>
        public string Position_id { get; set; }

        /// <summary>
        /// 当前位置信息
        /// </summary>
        public string Position_name { get; set; }
    }
}
