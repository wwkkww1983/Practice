using System;
using System.Linq;
using System.Collections.Generic;

namespace Caist.Framework.WebSocket
{
    public static class SubProtocolNegotiator
    {
        public static string Negotiate(IEnumerable<string> server, IEnumerable<string> client)
        {
            if (!server.Any() || !client.Any()) {
                return null;
            }

            var matches = client.Intersect(server);
            if (!matches.Any()) {
                throw new SubProtocolNegotiationFailureException("Unable to negotiate a subprotocol");
            }
            return matches.First();
        }
    }
}
