using System.ComponentModel.DataAnnotations.Schema;

namespace Caist.Framework.Entity.SettingValueManage
{
    [Table("mk_setting_value")]
    public class SettingValueEntity : BaseExtensionEntity
    {
        public string ParameterName { get; set; }
        public string ParameterIp { get; set; }
        public string ParameterPort { get; set; }
        public string ParameterPlcType { get; set; }
        public string ParameterDataType { get; set; }
        public string ParameterInstructions { get; set; }
        public string ParameterValue { get; set; }
        public string ParameterMinInstructions { get; set; }
        public string ParameterMinValue { get; set; }
        public string ParameterMaxInstructions { get; set; }
        public string ParameterMaxValue { get; set; }
        public string ParameterUnit { get; set; }
        public string ParameterSettingType { get; set; }
        public int? ParameterSort { get; set; }
        public string ParameterModels { get; set; }
        public string ParameterControls { get; set; }
        public string ParameterType { get; set; }
    }
}
