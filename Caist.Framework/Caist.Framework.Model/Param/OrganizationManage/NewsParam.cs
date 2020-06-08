using System;
using System.Collections.Generic;
using Caist.Framework.Model.Param.SystemManage;

namespace Caist.Framework.Model.Param.OrganizationManage
{
    public class NewsListParam : BaseAreaParam
    {
        public string NewsTitle { get; set; }
        public int? NewsType { get; set; }
        public string NewsTag { get; set; }
    }
}
