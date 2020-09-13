using System;

namespace Caist.Framework.Entity.FiberManage
{
    [Serializable]
    public class SectionInfoEntity : BaseExtensionEntity
    {
        /// <summary>
        /// 分区ID
        /// </summary>
        public string ID
        {
            get; set;
        }
       
        /// <summary>
        /// 起始光纤
        /// </summary>
        public string fiberstart
        {
            get; set;
        }

        /// <summary>
        /// 终止光纤
        /// </summary>
        public string fiberend
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
        /// 是否可用
        /// </summary>
        public string effect
        {
            get; set;
        }
        
        /// <summary>
        /// 起始位置
        /// </summary>
        public string startposition
        {
            get; set;
        }

        /// <summary>
        /// 终止位置
        /// </summary>
        public string endposition
        {
            get; set;
        }

        /// <summary>
        /// 是否工作
        /// </summary>
        public string isworking
        {
            get; set;
        }

        /// <summary>
        /// 工作面
        /// </summary>
        public string workingface
        {
            get; set;
        }

        /// <summary>
        /// 警告1
        /// </summary>
        public string warn1
        {
            get; set;
        }

        /// <summary>
        /// 警告2
        /// </summary>
        public string warn2
        {
            get; set;
        }

        /// <summary>
        /// 警告C
        /// </summary>
        public string warnC
        {
            get; set;
        }

        /// <summary>
        /// P_X
        /// </summary>
        public string P_X
        {
            get; set;
        }

        /// <summary>
        /// P_Y
        /// </summary>
        public string P_Y
        {
            get; set;
        }

        /// <summary>
        /// P_Z
        /// </summary>
        public string P_Z
        {
            get; set;
        }

        /// <summary>
        /// S_X
        /// </summary>
        public string S_X
        {
            get; set;
        }

        /// <summary>
        /// S_Y
        /// </summary>
        public string S_Y
        {
            get; set;
        }

        /// <summary>
        /// S_Z
        /// </summary>
        public string S_Z
        {
            get; set;
        }
    }
}
