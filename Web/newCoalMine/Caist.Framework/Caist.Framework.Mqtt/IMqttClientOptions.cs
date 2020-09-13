using Caist.Framework.Mqtt.Message;
using System;

namespace Caist.Framework.Mqtt
{
    public interface IMqttClientOptions
    {
        string Host { get; set; }

        int Port { get; set; }

        string ClientId { get; }

        string UserName { get; }

        string Password { get; }

        bool CleanSession { get; }

        TimeSpan KeepAlivePeriod { get; }

        TimeSpan AutoReconnectedDelay { get; }

        MqttMessage WillMessage { get; }

        TopicSubscriber[] Subscribers { get; }

        bool SharpLimit { get; }
    }
}
