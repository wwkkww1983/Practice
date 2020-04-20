using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caist.Framework.WebSocket
{
    public static class SubProtocolNegotiator
    {
        public static string Negotiate(IEnumerable<string> server, IEnumerable<string> client)
        {
            if (!server.Any() || !client.Any())
            {
                return null;
            }

            var matches = client.Intersect(server);
            if (!matches.Any())
            {
                throw new SubProtocolNegotiationFailureException("无法访问的子目录");
            }
            return matches.First();
        }
    }
}
