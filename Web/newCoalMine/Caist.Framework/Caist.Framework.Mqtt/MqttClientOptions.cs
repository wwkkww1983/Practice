using Caist.Framework.Mqtt.Message;
using System;

namespace Caist.Framework.Mqtt
{
    public class MqttClientOptions : IMqttClientOptions
    {
        public MqttClientOptions()
        {
            AutoReconnectedDelay = TimeSpan.FromSeconds(5);
            Subscribers = new TopicSubscriber[] { };
        }

        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ClientId { get; set; }

        public bool CleanSession { get; set; }

        public TimeSpan KeepAlivePeriod { get; set; }

        public TimeSpan AutoReconnectedDelay { get; set; }

        public MqttMessage WillMessage { get; set; }

        public TopicSubscriber[] Subscribers { get; set; }

        public bool SharpLimit { get; set; }
    }
}
