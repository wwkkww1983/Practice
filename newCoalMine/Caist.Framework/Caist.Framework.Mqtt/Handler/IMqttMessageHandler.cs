using Caist.Framework.Mqtt.Message;
using System.Threading.Tasks;

namespace Caist.Framework.Mqtt.Handler
{
    /// <summary>
    /// mqtt消息处理接口
    /// </summary>
    public interface IMqttMessageHandler
    {
        string[] Topic { get; }

        Task Execute(MqttMessage message);
    }
}
