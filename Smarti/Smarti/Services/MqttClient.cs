using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace Smarti.Services
{
    public class MqttAppClient : IMqttAppClient
    {
        private MqttClient client;

        public MqttClient Client
        {
            get
            {
                return client;
            }
        }

        public MqttAppClient()
        {
            client = new MqttClient("127.0.0.1");
            byte code = client.Connect(Guid.NewGuid().ToString());
        }

        public void Publish(string topic, string message)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(message));
        }

        public void SubscribeToMany(string[] topics)
        {
            byte[] qoses = new byte[topics.Length];

            for (int i = 0; i < topics.Length; i++)
            {
                qoses[i] = MqttMsgBase.QOS_LEVEL_AT_MOST_ONCE;
            }

            client.Subscribe(topics, qoses);
        }
    }
}
