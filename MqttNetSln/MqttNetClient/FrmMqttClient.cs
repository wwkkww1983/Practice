﻿using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Diagnostics;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MqttNetClient
{
    public partial class FrmMqttClient : Form
    {
        private IMqttClient _mqttClient;

        private Action<string> _updateListBoxAction;

        private List<IManagedMqttClient> managedMqttClients = new List<IManagedMqttClient>();
        public FrmMqttClient()
        {
            InitializeComponent();

            MqttNetGlobalLogger.LogMessagePublished += (o, args) =>
            {
                var s = new StringBuilder();
                s.Append($"{args.TraceMessage.Timestamp} ");
                s.Append($"{args.TraceMessage.Level} ");
                s.Append($"{args.TraceMessage.Source} ");
                s.Append($"{args.TraceMessage.ThreadId} ");
                s.Append($"{args.TraceMessage.Message} ");
                s.Append($"{args.TraceMessage.Exception}");
                s.Append($"{args.TraceMessage.LogId} ");
            };
        }

        private void FrmMqttClient_Load(object sender, EventArgs e)
        {
            var ips = Dns.GetHostAddressesAsync(Dns.GetHostName());
            TxbServer.Text = ips.Result[1].ToString();
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

            foreach (var value in Enum.GetValues(typeof(MqttQualityOfServiceLevel)))
            {
                CmbPubMqttQuality.Items.Add((int)value);
                CmbSubMqttQuality.Items.Add((int)value);
            }
            CmbPubMqttQuality.SelectedItem = 0;
            CmbSubMqttQuality.SelectedIndex = 0;


            _updateListBoxAction = new Action<string>((s) =>
            {
                listBox1.Items.Add(s);
                if (listBox1.Items.Count > 100)
                {
                    listBox1.Items.RemoveAt(0);
                }
            });


        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            MqttClient();
        }

        private async void BtnDisConnect_Click(object sender, EventArgs e)
        {
            if (null != _mqttClient && _mqttClient.IsConnected)
            {
                await _mqttClient.DisconnectAsync();
                _mqttClient.Dispose();
                _mqttClient = null;
            }
        }

        private void BtnSubscribe_Click(object sender, EventArgs e)
        {
            try
            {
                Task.Factory.StartNew(async () =>
                {
                    await _mqttClient.SubscribeAsync(
                        new List<TopicFilter>
                        {
                            new TopicFilter(
                                txbSubscribe.Text,
                                (MqttQualityOfServiceLevel)
                                    Enum.Parse(typeof (MqttQualityOfServiceLevel), CmbSubMqttQuality.Text))
                        });
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BtnSend_Click(object sender, EventArgs e)
        {
            try
            {
                Task.Factory.StartNew(async () =>
                {
                    var msg = new MqttApplicationMessage()
                    {
                        Topic = TxbTopic.Text,
                        Payload = Encoding.UTF8.GetBytes(TxbPayload.Text),
                        QualityOfServiceLevel =
                            (MqttQualityOfServiceLevel)
                                Enum.Parse(typeof(MqttQualityOfServiceLevel), CmbPubMqttQuality.Text),
                        Retain = false
                    };
                    if (null != _mqttClient)
                    {
                        await _mqttClient.PublishAsync(msg);
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void BtnMultiConnect_Click(object sender, EventArgs e)
        {
            MqttMultiClient(Convert.ToInt32(TxbConnectCount.Text));
        }

        private void BtnMultiDisConnect_Click(object sender, EventArgs e)
        {
            Task.Factory.StartNew(async () =>
            {
                foreach (var client in managedMqttClients)
                {
                    await client.StopAsync();
                    client.Dispose();
                    Thread.Sleep(100);
                }
            });
        }

        private async void MqttClient()
        {
            try
            {
                var options = new MqttClientOptions() { ClientId = Guid.NewGuid().ToString("D") };
                options.ChannelOptions = new MqttClientTcpOptions()
                {
                    Server = TxbServer.Text,
                    Port = Convert.ToInt32(TxbPort.Text)
                };
                options.Credentials = new MqttClientCredentials()
                {
                    Username = "admin",
                    Password = "public"
                };

                options.CleanSession = true;
                options.KeepAlivePeriod = TimeSpan.FromSeconds(100.5);
                options.KeepAliveSendInterval = TimeSpan.FromSeconds(20000);

                if (null != _mqttClient)
                {
                    await _mqttClient.DisconnectAsync();
                    _mqttClient = null;
                }
                _mqttClient = new MqttFactory().CreateMqttClient();

                _mqttClient.ApplicationMessageReceived += (sender, args) =>
                {
                    listBox1.BeginInvoke(
                        _updateListBoxAction,
                        $"客户端号:{args.ClientId} | 主题:{args.ApplicationMessage.Topic} | 装载:{Encoding.UTF8.GetString(args.ApplicationMessage.Payload)} | QoS:{args.ApplicationMessage.QualityOfServiceLevel} | 保持:{args.ApplicationMessage.Retain}"
                        );
                };

                _mqttClient.Connected += (sender, args) =>
                {
                    listBox1.BeginInvoke(_updateListBoxAction,
                        $"客户端已连接:IsSessionPresent:{args.IsSessionPresent}");
                };

                _mqttClient.Disconnected += (sender, args) =>
                {
                    listBox1.BeginInvoke(_updateListBoxAction,
                        $"客户端已断开:ClientWasConnected:{args.ClientWasConnected}");
                };

                await _mqttClient.ConnectAsync(options);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void MqttMultiClient(int clientsCount)
        {
            await Task.Factory.StartNew(async () =>
             {
                 for (int i = 0; i < clientsCount; i++)
                 {
                     var options = new ManagedMqttClientOptionsBuilder()
                     .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                     .WithClientOptions(new MqttClientOptionsBuilder()
                         .WithClientId(Guid.NewGuid().ToString().Substring(0, 13))
                         .WithTcpServer(TxbServer.Text, Convert.ToInt32(TxbPort.Text))
                         .WithCredentials("admin", "public")
                         .Build()
                     )
                     .Build();

                     var c = new MqttFactory().CreateManagedMqttClient();
                     await c.SubscribeAsync(
                         new TopicFilterBuilder().WithTopic(txbSubscribe.Text)
                             .WithQualityOfServiceLevel(
                                 (MqttQualityOfServiceLevel)
                                     Enum.Parse(typeof(MqttQualityOfServiceLevel), CmbSubMqttQuality.Text)).Build());

                     await c.StartAsync(options);
                     managedMqttClients.Add(c);
                     Thread.Sleep(200);
                 }
             });
        }
    }
}
