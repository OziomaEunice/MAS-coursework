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
            EdgeServerAgent edgeServerAgent = new EdgeServerAgent(ResourceType.CPU); env.Add(edgeServerAgent, "edge server 1");
            EdgeServerAgent edgeServerAgent2 = new EdgeServerAgent(ResourceType.RAM); env.Add(edgeServerAgent2, "edge server 2");
            EdgeServerAgent edgeServerAgent3 = new EdgeServerAgent(ResourceType.Storage); env.Add(edgeServerAgent3, "edge server 3");
            EdgeServerAgent edgeServerAgent4 = new EdgeServerAgent(ResourceType.Bandwidth); env.Add(edgeServerAgent4, "edge server 4");
            EdgeServerAgent edgeServerAgent5 = new EdgeServerAgent(ResourceType.Bandwidth); env.Add(edgeServerAgent5, "edge server 5");

            // create device agent and add it to the environment
            DeviceAgent deviceAgent = new DeviceAgent(ResourceType.CPU); env.Add(deviceAgent, "mobile device 1");
            DeviceAgent deviceAgent2 = new DeviceAgent(ResourceType.RAM); env.Add(deviceAgent2, "mobile device 2");
            DeviceAgent deviceAgent3 = new DeviceAgent(ResourceType.Storage); env.Add(deviceAgent3, "mobile device 3");
            DeviceAgent deviceAgent4 = new DeviceAgent(ResourceType.RAM); env.Add(deviceAgent4, "mobile device 4");
            DeviceAgent deviceAgent5 = new DeviceAgent(ResourceType.CPU); env.Add(deviceAgent5, "mobile device 5");
            DeviceAgent deviceAgent6 = new DeviceAgent(ResourceType.Storage); env.Add(deviceAgent6, "mobile device 6");
            DeviceAgent deviceAgent7 = new DeviceAgent(ResourceType.Bandwidth); env.Add(deviceAgent7, "mobile device 7");

            env.Start();

            System.Threading.Thread.Sleep(10);

            Console.ReadLine();
        }
    }
}
