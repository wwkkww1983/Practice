﻿using Caist.Framework.Model.Param.SystemManage;

namespace Caist.Framework.Model.Param.ApplicationManage
{
    public class ViewManipulateModelListParam 
    {
        public string ManipulateModelName { get; set; }

        public int? ManipulateModelStutas { get; set; }
        public int? ManipulateModelStutasr { get; set; }

        public long? ViewFunctionId { get; set; }
        
        public string ViewName { get; set; }

        public long? SystemId { get; set; }
    }
}
