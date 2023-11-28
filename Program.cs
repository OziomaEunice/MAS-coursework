using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS
{
    public class Program
    {
        static void Main(string[] args)
        {
            var env = new EnvironmentMas(100);


            // create marketplace agent and add it to the environment
            var marketplaceAgent = new MarketplaceAgent(); env.Add(marketplaceAgent, "marketplace");

            // create cloud server agent and add it to the environment
            CloudServerAgent cloudServerAgent = new CloudServerAgent(); env.Add(cloudServerAgent, "cloudServer");

            // create edge server agent and add it to the environment
            EdgeServerAgent edgeServerAgent1 = new EdgeServerAgent(); env.Add(edgeServerAgent1, "edgeServer1");
            EdgeServerAgent edgeServerAgent2 = new EdgeServerAgent(); env.Add(edgeServerAgent2, "edgeServer2");

            // create device agent and add it to the environment
            DeviceAgent deviceAgent1 = new DeviceAgent(); env.Add(deviceAgent1, "Mobile device");
            DeviceAgent deviceAgent2 = new DeviceAgent(); env.Add(deviceAgent2, "Laptop device");
            DeviceAgent deviceAgent3 = new DeviceAgent(); env.Add(deviceAgent3, "Desktop device");


            env.Start(); // start the environment

            Console.ReadLine();
        }
    }
}