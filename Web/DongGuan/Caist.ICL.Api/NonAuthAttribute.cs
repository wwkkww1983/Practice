using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Api
{
    /// <summary>
    /// 标记无需授权
    /// ljl@2018
    /// </summary>
    public class NonAuthAttribute : Attribute, IFilterMetadata
    {

    }
}
