using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace Smarti.Services
{
    public class MqttAppClientSingleton : IMqttAppClientSingleton
    {
        private MqttClient client;

        public MqttAppClientSingleton()
        {
            client = new MqttClient("127.0.0.1");
            byte code = client.Connect(Guid.NewGuid().ToString());
        }

        public void Publish(string topic, string message)
        {
            client.Publish(topic, Encoding.UTF8.GetBytes(message));
        }
    }
}
