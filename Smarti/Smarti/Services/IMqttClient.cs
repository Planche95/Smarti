using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace Smarti.Services
{
    public interface IMqttAppClient
    {
        MqttClient Client { get; }

        void Publish(string topic, string message);

        void SubscribeToMany(string[] topics);
    }
}
