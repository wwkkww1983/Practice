﻿using MQTTnet;
using MQTTnet.Protocol;
using MQTTnet.Server;
using ServiceStack;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace MqttNetServer
{
    public partial class FrmMqttServer : Form
    {

        private IMqttServer _mqttServer = null;

        private Action<string> _updateListBoxAction;
        public FrmMqttServer()
        {
            InitializeComponent();
        }

        private void FrmMqttServer_Load(object sender, EventArgs e)
        {
            var ips = Dns.GetHostAddressesAsync(Dns.GetHostName());

            foreach (var ip in ips.Result)
            {
                switch (ip.AddressFamily)
                {
                    case AddressFamily.InterNetwork:
                        TxbServer.Text = ip.ToString();
                        break;
                    case AddressFamily.InterNetworkV6:
                        break;
                }
            }

            _updateListBoxAction = new Action<string>((s) =>
            {
                listBox1.Items.Add(s);
                if (listBox1.Items.Count > 1000)
                {
                    listBox1.Items.RemoveAt(0);
                }
                var visibleItems = listBox1.ClientRectangle.Height / listBox1.ItemHeight;

                listBox1.TopIndex = listBox1.Items.Count - visibleItems + 1;
            });


            listBox1.KeyPress += (o, args) =>
            {
                if (args.KeyChar == 'c' || args.KeyChar == 'C')
                {
                    listBox1.Items.Clear();
                }
            };

            BtnStart.Enabled = true;
            BtnStop.Enabled = false;
            TxbServer.Enabled = true;
            TxbPort.Enabled = true;
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            MqttServer();
            BtnStart.Enabled = false;
            BtnStop.Enabled = true;
            TxbServer.Enabled = false;
            TxbPort.Enabled = false;
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            if (null != _mqttServer)
            {
                foreach (var clientSessionStatuse in _mqttServer.GetClientSessionsStatusAsync().Result)
                {
                    clientSessionStatuse.DisconnectAsync();
                }
                _mqttServer.StopAsync();
                _mqttServer = null;
            }
            BtnStart.Enabled = true;
            BtnStop.Enabled = false;
            TxbServer.Enabled = true;
            TxbPort.Enabled = true;
        }

        private async void MqttServer()
        {
            if (null != _mqttServer)
            {
                return;
            }

            var optionBuilder =
                new MqttServerOptionsBuilder().WithConnectionBacklog(1000).WithDefaultEndpointPort(Convert.ToInt32(TxbPort.Text));

            if (!TxbServer.Text.IsNullOrEmpty())
            {
                optionBuilder.WithDefaultEndpointBoundIPAddress(IPAddress.Parse(TxbServer.Text));
            }

            var options = optionBuilder.Build();

            (options as MqttServerOptions).ConnectionValidator += context =>
            {
                if (context.ClientId.Length < 10)
                {
                    context.ReturnCode = MqttConnectReturnCode.ConnectionRefusedIdentifierRejected;
                    return;
                }
                if (!context.Username.Equals("admin"))
                {
                    context.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                    return;
                }
                if (!context.Password.Equals("public"))
                {
                    context.ReturnCode = MqttConnectReturnCode.ConnectionRefusedBadUsernameOrPassword;
                    return;
                }
                context.ReturnCode = MqttConnectReturnCode.ConnectionAccepted;

            };

            _mqttServer = new MqttFactory().CreateMqttServer();
            _mqttServer.ClientConnected += (sender, args) =>
            {
                listBox1.BeginInvoke(_updateListBoxAction, $">客户端已连接:客户端号:{args.ClientId},协议版本:");

                var s = _mqttServer.GetClientSessionsStatusAsync();
                label3.BeginInvoke(new Action(() => { label3.Text = $"连接总数：{s.Result.Count}"; }));
            };

            _mqttServer.ClientDisconnected += (sender, args) =>
            {
                listBox1.BeginInvoke(_updateListBoxAction, $"<客户端断开连接:客户端号:{args.ClientId}");
                var s = _mqttServer.GetClientSessionsStatusAsync();
                label3.BeginInvoke(new Action(() => { label3.Text = $"连接总数：{s.Result.Count}"; }));
            };

            _mqttServer.ApplicationMessageReceived += (sender, args) =>
            {
                listBox1.BeginInvoke(_updateListBoxAction,
                    $"客户端号:{args.ClientId} 主题:{args.ApplicationMessage.Topic} 装载:{Encoding.UTF8.GetString(args.ApplicationMessage.Payload)} QoS:{args.ApplicationMessage.QualityOfServiceLevel}");

            };

            _mqttServer.ClientSubscribedTopic += (sender, args) =>
            {
                listBox1.BeginInvoke(_updateListBoxAction, $"@客户端订阅主题 客户端号:{args.ClientId} 主题:{args.TopicFilter.Topic} QoS:{args.TopicFilter.QualityOfServiceLevel}");
            };
            _mqttServer.ClientUnsubscribedTopic += (sender, args) =>
            {
                listBox1.BeginInvoke(_updateListBoxAction, $"%客户端未订阅主题 客户端号:{args.ClientId} Topic:{args.TopicFilter.Length}");
            };

            _mqttServer.Started += (sender, args) =>
            {
                listBox1.BeginInvoke(_updateListBoxAction, "Mqtt服务启动...");
            };

            _mqttServer.Stopped += (sender, args) =>
            {
                listBox1.BeginInvoke(_updateListBoxAction, "Mqtt服务停止...");

            };

            await _mqttServer.StartAsync(options);
        }
    }
}