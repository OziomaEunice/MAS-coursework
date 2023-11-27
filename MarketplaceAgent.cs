using ActressMas;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;

/**
 * This class represents a marketplace agent.
 * It is responsible for managing the marketplace. It manages the list of bids and offers of both
 * buyers (device) and sellers (edge server). It also manages the list of transactions that have been made.
 * If a transaction is made, it will notify the buyer and seller.
 * If a bid is unsuccessful, it will notify the buyer and will remove the bid from the list. Also, it 
 * will notify the cloud server agent, which will take care of the device's bid.
 */

namespace MAS
{
    public class MarketplaceAgent : Agent
    {
        private Dictionary<string, List<string>> serviceProviders;

        public MarketplaceAgent()
        {
            serviceProviders = new Dictionary<string, List<string>>();
        }

        public override void Setup()
        {
            Console.WriteLine("==========================\n ****AUCTION SYSTEM**** \n==========================");
        }

        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}\n");
                message.Parse(out string action, out List<string> parameters);

                switch (action)
                {
                    case "register":
                        ManageRegistration(message.Sender, parameters[0]);
                        break;

                    case "bid":
                        ManageBid(message.Sender, parameters[0]);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ManageRegistration(string edgeServer, string resource)
        {
            // if the service provider contains the resource provided by the edge server
            // then add it to the list of service providers
            if (serviceProviders.ContainsKey(resource))
            {
                List<string> edgeServers = serviceProviders[resource]; // get the list of edge servers that provide the resource

                if (!edgeServers.Contains(edgeServer)) // if the edge server is not in the list
                {
                    edgeServers.Add(edgeServer); // add it to the list
                    serviceProviders[resource] = edgeServers; // update the list of edge servers that provide the resource
                }
            }
            else
            {
                List<string> edgeServers = new List<string> { edgeServer}; // create a new list of edge servers that provide the resource
                serviceProviders.Add(resource, edgeServers); // add the resource and the list of edge servers to the dictionary
            }

            Console.WriteLine($"\t\n[[Registered {edgeServer} as a service provider for {resource}]]\n\n");
        }

        private void ManageBid(string device, string resource)
        {
            Random random = new Random();

            // if the marketplace finds the resource requested by the device, 
            // then allocate the resource to the device. Otherwise, notify it that there are no resources available.
            if (serviceProviders.ContainsKey(resource))
            {
                List<string> edgeServers = serviceProviders[resource]; // get the list of edge servers that provide the resource

                // randomly select an edge server from the list
                int randomIndex = random.Next(edgeServers.Count);
                string edgeServer = edgeServers[randomIndex];

                // send a message to the edge server to process the task
                Send(edgeServer, $"process {device} {resource}");

                Console.WriteLine($"\t\n[[Allocated {resource} to {device}]]\n\n");
            }
            else
            {
                // send a message to the device that there are no resources available. Also, notify the cloud server agent.
                Send(device, $"no resources available");
                Send("cloudServer", $"process {device} {resource}");
            }   
        }
    }
}