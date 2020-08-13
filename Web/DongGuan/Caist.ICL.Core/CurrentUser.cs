using System;
using System.Collections.Generic;
using System.Text;

namespace Caist.ICL.Core
{
    public class CurrentUser
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }

        /// <summary>
        /// 小区ID
        /// </summary>
        public string CommunityId { get; set; }
        /// <summary>
        /// 小区名
        /// </summary>
        public string CommunityName { get; set; }

        /// <summary>
        /// 房号
        /// </summary>
        public string HouseId { get; set; }
        public string HouseNo { get; set; }

        public string RoleId { get; set; }


    }
}
