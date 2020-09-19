using Caist.Framework.Model.Param.SystemManage;

namespace Caist.Framework.Model.Param.ApplicationManage
{
    public class SystemSettingListParam
    {
        public long? Id { get; set; }
        public string SystemName { get; set; }
        public string SystemNickName { get; set; }

        /// <summary>
        /// 提供数据筛选业务逻辑
        /// </summary>
        public string DataType { get; set; }
    }
}
