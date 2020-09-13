using NPOI.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Caist.Framework.Entity.PeopleManage
{
    [Serializable]
    [Table("mk_fiber_mark")]
    public class FiberMark : BaseExtensionEntity
    {
        public string i { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }
}
