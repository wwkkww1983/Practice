using System;

namespace Caist.Framework.Entity.FiberManage
{
    [Serializable]
    public class SectionEntity: BaseExtensionEntity
    {
        /// <summary>
        /// 分区ID
        /// </summary>
        public string ID
        {
            get; set;
        }


        /// <summary>
        /// 分区名称
        /// </summary>
        public string SectionName
        {
            get; set;
        }

        /// <summary>
        /// 最大温度
        /// </summary>
        public string tempmax
        {
            get; set;
        }

        /// <summary>
        /// 最大温度位置
        /// </summary>
        public string maxposition
        {
            get; set;
        }

        /// <summary>
        /// 最小温度
        /// </summary>
        public string tempmin
        {
            get; set;
        }

        /// <summary>
        /// 最小温度位置
        /// </summary>
        public string minposition
        {
            get; set;
        }

        /// <summary>
        /// 平均温度
        /// </summary>
        public string tempavg
        {
            get; set;
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string recordtime
        {
            get; set;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public string alarmflag
        {
            get; set;
        }

        /// <summary>
        /// 定温
        /// </summary>
        public string upflag
        {
            get; set;
        }

        /// <summary>
        /// 差温
        /// </summary>
        public string cwflag
        {
            get; set;
        }
    }
}
