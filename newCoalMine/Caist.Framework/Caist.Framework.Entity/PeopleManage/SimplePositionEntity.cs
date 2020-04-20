namespace Caist.Framework.Entity.PeopleManage
{
    public class SimplePositionEntity : BaseExtensionEntity
    {
        /// <summary>
        /// X坐标
        /// </summary>
        public string Position_x { get; set; }

        /// <summary>
        /// Y坐标
        /// </summary>
        public string Position_y { get; set; }

        /// <summary>
        /// Z坐标
        /// </summary>
        public string Position_z { get; set; }

        /// <summary>
        /// SIN值
        /// </summary>
        public string Position_sin { get; set; }

        /// <summary>
        /// COS值
        /// </summary>
        public string Position_cos { get; set; }

        /// <summary>
        /// VCOS值
        /// </summary>
        public string Position_vcos { get; set; }

        /// <summary>
        /// 位置描述
        /// </summary>
        public string Position_desc { get; set; }
    }
}
