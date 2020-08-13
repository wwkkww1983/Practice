using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Caist.Framework.Util.Model;

namespace Caist.Framework.Business.AutoJob
{
    public interface IJobTask
    {
        Task<TData> Start();
    }
}
