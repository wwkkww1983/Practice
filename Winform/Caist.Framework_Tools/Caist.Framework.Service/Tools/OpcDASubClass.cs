using Communication.OPC;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hylasoft.Opc.Common;

namespace Caist.Framework.Service.Tools
{
    class OpcDASubClass : OpcConnector
    {
        public OpcDASubClass(string host, bool isRegexOn) : base(host, isRegexOn)
        {

        }

        public async Task<Dictionary<string, IEnumerable<Node>>> SearchTags()
        {
            return _opcNodes;
        }

        public async Task<object> GetResultByTag(string tag)
        {
            return await GetValueByTag(tag);
        }
    }
}
