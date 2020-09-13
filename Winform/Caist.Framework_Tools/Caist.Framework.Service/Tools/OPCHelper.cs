using Caist.Framework.Entity;
using Caist.Framework.ThreadPool;
using Opc.Ua;
using OpcUaHelper;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Caist.Framework.Service.Tools
{
    public class OpcHelper
    {
        public string IP { get; set; }
        public string Port { get; set; }
        public OpcUaClient _OpcUaClient { get; set; }
        public List<string> ListAddress { get; set; }
        public bool Init()
        {
            _OpcUaClient = new OpcUaClient();
            ListAddress = new List<string>();
            //PublicEntity.OPCPowerEntities.Where(p=>p.IP == this.IP).ToList().ForEach(v =>
            //{
            //    ListAddress.Add(v.Name + ":" + v.Address);
            //});
            return true;
        }

        public async void Connect()
        {
            if (!_OpcUaClient.Connected)
            {
                try
                {
                    _OpcUaClient.UserIdentity = new UserIdentity(new AnonymousIdentityToken());
                    var addr = $"opc.tcp://{this.IP}:{this.Port}/";
                    await _OpcUaClient.ConnectServer(addr);
                }
                catch (Exception ex)
                {
                    Common.LogError(ex);
                }
            }
        }
    }
}
