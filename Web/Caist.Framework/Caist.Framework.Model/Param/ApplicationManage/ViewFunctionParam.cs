namespace Caist.Framework.Model.Param.ApplicationManage
{
    public class ViewFunctionListParam
    {
        public string ViewName { get; set; }

        public int? ViewStatus { get; set; }

        public long? SystemSettingId { get; set; }
        public int? ViewType { get; set; }
        /// <summary>
        /// 是否显示首页
        /// </summary>
        public int? ViewFunctionShowHome { get; set; }
    }
}
