using Caist.Framework.Mqtt.Message;
using Caist.Framework.Mqtt.Topic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.Framework.Mqtt.Handler
{
    /// <summary>
    /// mqtt消息处理基础类
    /// 所有需要处理的mqtt消息需继承自该类
    /// </summary>
    public abstract class MqttMessageHandler : IMqttMessageHandler
    {
        /// <summary>
        /// 主题
        /// </summary>
        public string[] Topic => MqttTopic?.Select(x => x.Build()).ToArray();

        /// <summary>
        /// 主题对象
        /// </summary>
        protected abstract IMqttTopic[] MqttTopic { get; }

        /// <summary>
        /// 消息执行
        /// </summary>
        /// <param name="message"></param>
        public abstract Task Execute(MqttMessage message);
    }
}
