using ActressMas;
using System;
using System.Collections.Generic;


/**
 * This class represents an edge server agent.
 * It has capacity to process the tasks sent to it by the device agents. 
 * In processing a task, it has a cost (measured in pence) that it needs to pay per 10Mb of data processed.
 * It can accept or reject bids from device agents, depending on whether it has the capacity to process the task.
 * Whatever it decides, the message is sent via the marketplace to the device agent.
 */


namespace MAS
{
    public enum ResourceType { CPU, RAM, Storage, Bandwidth };
    public class EdgeServerAgent : Agent
    {
        private ResourceType resourceType;
        private int capacity;
        private int costOf10Mb;
        private static Random random = new Random();

        public EdgeServerAgent(ResourceType resource)
        {
            resourceType = resource;
        }

        public override void Setup()
        {
            Send("marketplace", $"register {resourceType}");
        }

        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}");
                message.Parse(out string action, out List<string> parameters);

                switch (action)
                {
                    case "":
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
    }
}