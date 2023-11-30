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
            var env = new EnvironmentMas(noTurns: 10);


            // create marketplace agent and add it to the environment
            var marketplaceAgent = new MarketplaceAgent(); env.Add(marketplaceAgent, "marketplace");

            // create cloud server agent and add it to the environment
            CloudServerAgent cloudServerAgent = new CloudServerAgent(); env.Add(cloudServerAgent, "cloudServer");

            // create edge server agent and add it to the environment
            EdgeServerAgent edgeServerAgent1 = new EdgeServerAgent(); env.Add(edgeServerAgent1, "edgeServer1");
            EdgeServerAgent edgeServerAgent2 = new EdgeServerAgent(); env.Add(edgeServerAgent2, "edgeServer2");
            /*EdgeServerAgent edgeServerAgent3 = new EdgeServerAgent(); env.Add(edgeServerAgent3, "edgeServer3");
            EdgeServerAgent edgeServerAgent4 = new EdgeServerAgent(); env.Add(edgeServerAgent4, "edgeServer4");
            EdgeServerAgent edgeServerAgent5 = new EdgeServerAgent(); env.Add(edgeServerAgent5, "edgeServer5");*/

            // create device agent and add it to the environment
            DeviceAgent deviceAgent1 = new DeviceAgent(); env.Add(deviceAgent1, "Mobile device");
            DeviceAgent deviceAgent2 = new DeviceAgent(); env.Add(deviceAgent2, "Laptop device");
            DeviceAgent deviceAgent3 = new DeviceAgent(); env.Add(deviceAgent3, "Desktop device");
            /*DeviceAgent deviceAgent4 = new DeviceAgent(); env.Add(deviceAgent4, "Smart TV device");
            DeviceAgent deviceAgent5 = new DeviceAgent(); env.Add(deviceAgent5, "Tablet device");*/


            env.Start(); // start the environment

            Console.ReadLine();
        }
    }
}