using Caist.Framework.Mqtt.Message;

namespace Caist.Framework.Mqtt
{
    /// <summary>
    /// 主体订阅
    /// </summary>
    public interface ITopicSubscriber
    {
        /// <summary>
        /// 主题
        /// </summary>
        string Topic { get; }

        /// <summary>
        /// 质量等级
        /// </summary>
        MqttQosLevel Qos { get; }

        /// <summary>
        /// 共享订阅
        /// </summary>
        bool Shared { get; }
    }
}
