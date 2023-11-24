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

            // create the auctioneer agent


            // create the edge server agents


            // create the device agents and add them to the environment
            var device = new DeviceAgent(); env.Add(device, "device");


            DeviceAgent device1 = new DeviceAgent(1, 20);
            DeviceAgent device2 = new DeviceAgent(2, 20);

            


            // start the environment
            env.Start();

            Console.ReadLine(); //wait for the user to press enter
        }
    }
}