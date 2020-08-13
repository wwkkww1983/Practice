using Caist.Framework.Model.Param.SystemManage;

namespace Caist.Framework.Model.Param.ApplicationManage
{
    public class InformationPublishParam: DateTimeParam
    {
        public string DeviceUid { get; set; }
        public string LinkContent { get; set; }
    }
}
