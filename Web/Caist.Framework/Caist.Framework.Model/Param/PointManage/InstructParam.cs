using Caist.Framework.Model.Param.SystemManage;

namespace Caist.Framework.Model.Param.PointManage
{
    public class InstructListParam : BaseAreaParam
    {
        public string Name { get; set; }
        public string SystemSettingId { get; set; }
        /// <summary>
        /// 寄存器分组ID
        /// </summary>
        public string GroupId { get; set; }
    }
}
