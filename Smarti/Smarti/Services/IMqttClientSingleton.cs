using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;

namespace Smarti.Services
{
    public interface IMqttAppClientSingleton
    {
        void Publish(string topic, string message);
    }
}
