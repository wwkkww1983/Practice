using NPOI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.PeopleManage
{
    [Serializable]
    [Table("mk_people_position_mark")]
    public class PeoplePositionMarkEntity :BaseExtensionEntity
    {
        public string i { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }
}
