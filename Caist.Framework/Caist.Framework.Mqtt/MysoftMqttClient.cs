using Caist.Framework.Mqtt.Message;
using System;
using System.Threading.Tasks;

namespace Caist.Framework.Mqtt
{
    /// <summary>
    /// MQTT 客户端
    /// </summary>
    public abstract class MysoftMqttClient : IMysoftMqttClient
    {
        public abstract bool IsConnected { get; }

        /// <summary>
        /// 收到消息事件
        /// </summary>
        public abstract event EventHandler<MqttMessage> MessageReceived;

        /// <summary>
        /// 回收接口
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// 清空
        /// </summary>
        public abstract void Flush();

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="message"></param>
        public abstract void Publish(MqttMessage message);

        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract Task PublishAsync(MqttMessage message);

        /// <summary>
        /// 订阅主题
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="qos"></param>
        protected internal abstract void Subscribe(string topic, MqttQosLevel qos = MqttQosLevel.AtMostOnce);

        /// <summary>
        /// 异步订阅
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="qos"></param>
        /// <returns></returns>
        protected internal abstract Task SubscribeAsync(string topic, MqttQosLevel qos = MqttQosLevel.AtMostOnce);
    }
}
