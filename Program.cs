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
            EdgeServerAgent edgeServerAgent1 = new EdgeServerAgent(ServiceType.Add); env.Add(edgeServerAgent1, "edgeServer1");
            EdgeServerAgent edgeServerAgent2 = new EdgeServerAgent(ServiceType.Sub); env.Add(edgeServerAgent2, "edgeServer2");
            EdgeServerAgent edgeServerAgent3 = new EdgeServerAgent(ServiceType.Mul); env.Add(edgeServerAgent3, "edgeServer3");

            // create device agent and add it to the environment
            DeviceAgent deviceAgent1 = new DeviceAgent(ServiceType.Add); env.Add(deviceAgent1, "Mobile device 1");
            DeviceAgent deviceAgent2 = new DeviceAgent(ServiceType.Sub); env.Add(deviceAgent2, "Mobile device 2");
            DeviceAgent deviceAgent3 = new DeviceAgent(ServiceType.Add); env.Add(deviceAgent3, "Mobile device 3");
            DeviceAgent deviceAgent4 = new DeviceAgent(ServiceType.Mul); env.Add(deviceAgent4, "Mobile device 4");
            DeviceAgent deviceAgent5 = new DeviceAgent(ServiceType.Div); env.Add(deviceAgent5, "Mobile device 5");

            env.Start();

            edgeServerAgent1.Send("provider1", "force-unregister");

            EdgeServerAgent pa5 = new EdgeServerAgent(ServiceType.Sub); env.Add(pa5, "provider5");
            EdgeServerAgent pa6 = new EdgeServerAgent(ServiceType.Mul); env.Add(pa6, "provider6");

            env.Continue(100);

            DeviceAgent ca5 = new DeviceAgent(ServiceType.Add); env.Add(ca5, "client5");
            DeviceAgent ca6 = new DeviceAgent(ServiceType.Sub); env.Add(ca6, "client6");

            env.Continue(100);

            System.Threading.Thread.Sleep(10);

            Console.ReadLine();
        }
    }
}
