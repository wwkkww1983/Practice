using Communication.Common;
using Communication.OPC.FBox;
using Opc.Ua;
using OpcUaHelper;
using SyncUtil;
using System;
using System.Collections.Generic;

namespace Caist.Framework.Service.Tools
{
    public class Opc_DAHelper
    {
        public static BaseMachine _opcMachine;

        


    public string IP { get; set; }
        public string Port { get; set; }
        public OpcUaClient _OpcUaClient { get; set; }
        public List<string> ListAddress { get; set; }
        public static bool Init()
        {
            if (_opcMachine == null)
            {
                //OPC连接
                _opcMachine = new FBoxOpcDaMachine("1", "OPC2IEC104", null, true);
            }
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
