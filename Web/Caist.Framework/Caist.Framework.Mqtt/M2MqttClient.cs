using Caist.Framework.Mqtt.Extensions;
using Caist.Framework.Mqtt.Handler;
using Caist.Framework.Mqtt.Message;
using Caist.Framework.Mqtt.Topic;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Linq;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace Caist.Framework.Mqtt
{
    /// <summary>
    /// 对M2Mqtt客户端的封装
    /// </summary>
    public sealed class M2MqttClient : MysoftMqttClient
    {
        /// <summary>
        /// 客户端实例
        /// </summary>
        private MqttClient mqttClient;

        /// <summary>
        /// 客户端选项
        /// </summary>
        private readonly IMqttClientOptions options;

        /// <summary>
        /// 服务提供器
        /// </summary>
        private readonly IServiceProvider serviceProvider;

        /// <summary>
        /// 收到消息事件
        /// </summary>
        public override event EventHandler<MqttMessage> MessageReceived;

        private event EventHandler ClientConnected;

        /// <summary>
        /// 表示是否已经释放
        /// </summary>
        private bool IsDisposed;

        /// <summary>
        /// 标识客户端是否已连接
        /// </summary>
        public override bool IsConnected
        {
            get
            {
                if (mqttClient != null)
                {
                    return mqttClient.IsConnected;
                }

                return false;
            }
        }

        private bool IsFlushing { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="messageHandlers"></param>
        public M2MqttClient(IMqttClientOptions options, IServiceProvider serviceProvider)
        {
            this.options = options;
            this.serviceProvider = serviceProvider;

            Initializer();
        }

        /// <summary>
        /// 初始化器
        /// </summary>
        private void Initializer()
        {
            //初始化客户端
            mqttClient = new MqttClient(options.Host, options.Port, false, null, null, MqttSslProtocols.None);

            //事件定义
            this.mqttClient.ConnectionClosed += MqttClient_ConnectionClosed;
            this.mqttClient.MqttMsgPublishReceived += MqttClient_MqttMsgPublishReceived;
            this.mqttClient.MqttMsgSubscribed += MqttClient_MqttMsgSubscribed;
            this.mqttClient.MqttMsgPublished += MqttClient_MqttMsgPublished;
            this.mqttClient.MqttMsgUnsubscribed += MqttClient_MqttMsgUnsubscribed;

            //客户端连接事件
            this.ClientConnected += M2MqttClient_ClientConnected;

            //连接包装
            //自动重试
            ConnectWrap(options.CleanSession, () => Console.WriteLine("### CONNECTING SUCCEED ###"), () => Console.WriteLine("### CONNECTING FAILED ###"));
        }

        /// <summary>
        /// 客户端连接事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void M2MqttClient_ClientConnected(object sender, EventArgs e)
        {
            Console.WriteLine("### M2MqttClient_ClientConnected ###");

            Subscribe(new ClientStatusTopic("+").Build());

            foreach (var subscriber in options.Subscribers)
            {
                var topic = new MqttTopicBuilder()
                    .With(subscriber.Topic)
                    .Build(subscriber.Shared);

                Subscribe(topic, subscriber.Qos);
            }

            Publish(
                new MqttMessageBuilder()
                    .WithTopic(new ClientStatusTopic(options.ClientId))
                    .WithPayload(ClientStatusModel.Create(options.ClientId, true))
                    .WithRetainFlag(options.CleanSession == false)
                    .Build());
        }

        /// <summary>
        /// 连接关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_ConnectionClosed(object sender, EventArgs e)
        {
            Console.WriteLine("### DISCONNECTED FROM SERVER ###");

            if (IsConnected)
            {
                Console.WriteLine($"### IS CONNECTED ###{Environment.NewLine}### DO NOT CONNECT ###");
                return;
            }

            Console.WriteLine("### RECONNECTING ###");

            if (IsDisposed)
            {
                return;
            }

            if (IsFlushing)
            {
                Console.WriteLine("### Flushing ###");
                return;
            }

            Task.Factory.StartNew(() =>
            {
                ConnectWrap(options.CleanSession, () => Console.WriteLine("### RECONNECTING SUCCEED ###"), () => Console.WriteLine("### RECONNECTING FAILED ###"));
            });
        }

        /// <summary>
        /// 收到消息事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_MqttMsgPublishReceived(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishEventArgs e)
        {
            var message = new MqttMessage
            {
                Topic = e.Topic,
                Payload = e.Message,
                Qos = (MqttQosLevel)e.QosLevel,
                Retain = e.Retain
            };

            Task.Factory.StartNew(() =>
            {
                MessageReceived?.Invoke(this, message);
            });

            Task.Factory.StartNew(() =>
            {
                //消息处理器
                var messageHandlers = serviceProvider.GetServices<IMqttMessageHandler>();

                var handlers = messageHandlers?.Where(x => x.Topic.Any(t => t.Equals(e.Topic)));

                if (handlers != null)
                {
                    foreach (var handler in handlers)
                    {
                        handler?.Execute(message).GetAwaiter().GetResult();
                    }
                }
            });
        }

        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_MqttMsgUnsubscribed(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgUnsubscribedEventArgs e)
        {

        }

        /// <summary>
        /// 消息发布事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_MqttMsgPublished(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgPublishedEventArgs e)
        {

        }

        /// <summary>
        /// 主体订阅事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MqttClient_MqttMsgSubscribed(object sender, uPLibrary.Networking.M2Mqtt.Messages.MqttMsgSubscribedEventArgs e)
        {

        }

        /// <summary>
        /// 连接包装
        /// </summary>
        /// <param name="succeed">连接成功</param>
        /// <param name="failed">连接失败</param>
        /// <returns></returns>
        private byte ConnectWrap(bool cleanSession = true, Action succeed = null, Action failed = null)
        {
            if (IsDisposed)
            {
                return 0;
            }

            var result = Policy
                .HandleResult<byte>(x => IsConnected == false && x != 0)
                .WaitAndRetryForever(x => options.AutoReconnectedDelay)
                .Execute(() =>
                {
                    try
                    {
                        Console.WriteLine("### CONNECTING ###");

                        byte tmp;

                        tmp = mqttClient.Connect(
                            options.ClientId,
                            options.UserName,
                            options.Password,
                            options.CleanSession ? false : options.WillMessage?.Retain ?? false,
                            (byte)(options.WillMessage?.Qos ?? 0),
                            true,
                            options.WillMessage?.Topic,
                            options.WillMessage?.ConvertPayloadToString(),
                            cleanSession,
                            Convert.ToByte(options.KeepAlivePeriod.TotalSeconds));

                        Console.WriteLine($"Connection Result Code:{tmp}{Environment.NewLine}IsConnected:{IsConnected}");

                        succeed?.Invoke();

                        ClientConnected?.Invoke(mqttClient, null);

                        return 0;
                    }
                    catch (Exception ex)
                    {
                        failed?.Invoke();
                        return 255;
                        throw (ex);
                    }
                });

            return result;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public override void Dispose()
        {
            Console.WriteLine("### Dispose ###");

            IsDisposed = true;

            //try
            //{
            //    mqttClient?.Disconnect();
            //}
            //catch { }
        }

        public override void Publish(MqttMessage message)
        {
            mqttClient.Publish(message.Topic, message.Payload, (byte)message.Qos, message.Retain);
        }

        public override Task PublishAsync(MqttMessage message)
        {
            return Task.Factory.StartNew(() =>
            {
                Publish(message);
            });
        }

        protected internal override void Subscribe(string topic, MqttQosLevel qos = MqttQosLevel.AtMostOnce)
        {
            mqttClient.Subscribe(new string[] { topic }, new byte[] { (byte)qos });
        }

        protected internal override Task SubscribeAsync(string topic, MqttQosLevel qos = MqttQosLevel.AtMostOnce)
        {
            return Task.Factory.StartNew(() => Subscribe(topic, qos));
        }

        public override void Flush()
        {
            if (IsFlushing)
            {
                return;
            }

            Console.WriteLine($"### Flush ###");

            //清空中
            IsFlushing = true;

            Task.Factory.StartNew(() =>
            {
                Policy
               .HandleResult<bool>(x => x)
               .WaitAndRetryForever(x =>
               {
                   Console.WriteLine($"### 重试{x}次 ###");
                   return TimeSpan.FromSeconds(1);
               })
               .Execute(() => IsConnected == false);

                //断开客户端
                mqttClient.Disconnect();

                Policy
                    .HandleResult<bool>(x => x)
                    .WaitAndRetryForever(x =>
                    {
                        Console.WriteLine($"### 重试{x}次 ###");
                        return TimeSpan.FromSeconds(10);
                    })
                    .Execute(() => IsConnected);

                //以clean session方式连接broker
                ConnectWrap(true, () => Console.WriteLine("### RECONNECTING SUCCEED ###"), () => Console.WriteLine("### RECONNECTING FAILED ###"));

                Console.WriteLine($"### IsConnected:{IsConnected} ###");

                //清空中状态重置
                IsFlushing = false;

                //断开broker
                //断开事件中会自动连接
                mqttClient.Disconnect();
            });
        }
    }
}
