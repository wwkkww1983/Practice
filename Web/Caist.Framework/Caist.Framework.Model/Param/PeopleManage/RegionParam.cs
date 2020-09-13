using System;

namespace Caist.Framework.Model.PeopleManage
{
    public class RegionParam
    {
        public string FormId { get; set; }
        public string SendId { get; set; }
        public string RegionId { get; set; }
        public string PositionId { get; set; }
        public string UserCardNo { get; set; }
        public string CurrentStation { get; set; }
        public string StationAddress { get; set; }
        public string TimeData { get; set; }
        public string PeopleName { get; set; }
        public string PeopleNumber { get; set; }
        public DateTime? MaxTime { get; set; }
        public DateTime? MinTime { get; set; }
        public string ViewFunctionId { get; set; }
    }
}
