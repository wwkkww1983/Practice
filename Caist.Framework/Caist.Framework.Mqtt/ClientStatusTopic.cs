using Caist.Framework.Mqtt.Topic;

namespace Caist.Framework.Mqtt
{
    internal class ClientStatusTopic : IMqttTopic
    {
        private readonly string clientId;

        public ClientStatusTopic(string clientId)
        {
            this.clientId = clientId;
        }

        public string Build()
        {
            return $"clients/{clientId}/status";
        }
    }
}
