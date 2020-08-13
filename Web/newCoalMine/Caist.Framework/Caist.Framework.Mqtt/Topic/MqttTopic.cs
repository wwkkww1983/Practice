namespace Caist.Framework.Mqtt.Topic
{
    public class MqttTopic : IMqttTopic
    {
        private readonly string _topic;

        public MqttTopic(string topic)
        {
            _topic = topic;
        }

        public string Build()
        {
            return _topic;
        }
    }
}
