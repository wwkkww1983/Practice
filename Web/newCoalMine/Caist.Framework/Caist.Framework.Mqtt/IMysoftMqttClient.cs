using Caist.Framework.Mqtt.Message;
using System;
using System.Threading.Tasks;

namespace Caist.Framework.Mqtt
{
    public interface IMysoftMqttClient : IDisposable
    {
        event EventHandler<MqttMessage> MessageReceived;

        void Publish(MqttMessage message);

        Task PublishAsync(MqttMessage message);

        /// <summary>
        /// 清空订阅
        /// </summary>
        /// <returns></returns>
        void Flush();

        /// <summary>
        /// 是否连接
        /// </summary>
        bool IsConnected { get; }
    }
}
