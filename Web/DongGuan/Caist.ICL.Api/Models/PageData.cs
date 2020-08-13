using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Api.Models
{
    public class PageData<T>
    {
        public int total { get; set; }
        public int size { get; set; }
        public int index { get; set; }
        public IEnumerable<T> rows { get; set; }
    }
}
