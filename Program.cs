using ActressMas;
using System;

// this class belongs to the environment. It is responsible for creating the 
// agents and the environment.

namespace MAS
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // create a new environment
            var env = new EnvironmentMas(100);

            // create the marketplace agent and add it to the environment
            MarketplaceAgent marketplace = new MarketplaceAgent(); env.Add(marketplace, "marketplace");


            // create the edge server agents and add them to the environment
            EdgeServerAgent edgeServer1 = new EdgeServerAgent(); env.Add(edgeServer1, "Edge Server 1");
            EdgeServerAgent edgeServer2 = new EdgeServerAgent(); env.Add(edgeServer2, "Edge Server 2");
            EdgeServerAgent edgeServer3 = new EdgeServerAgent(); env.Add(edgeServer3, "Edge Server 3");
            EdgeServerAgent edgeServer4 = new EdgeServerAgent(); env.Add(edgeServer4, "Edge Server 4");
            EdgeServerAgent edgeServer5 = new EdgeServerAgent(); env.Add(edgeServer5, "Edge Server 5");


            // create the device agents and add them to the environment
            DeviceAgent device1 = new DeviceAgent(1, 20); env.Add(device1, "Mobile device");
            DeviceAgent device2 = new DeviceAgent(4, 20); env.Add(device2, "Desktop device");
            DeviceAgent device3 = new DeviceAgent(5, 20); env.Add(device3, "Tablet device");
            DeviceAgent device4 = new DeviceAgent(6, 20); env.Add(device4, "Smart TV device");

            


            // start the environment
            env.Start();

            System.Threading.Thread.Sleep(10); 

            Console.ReadLine(); //wait for the user to press enter
        }
    }
}