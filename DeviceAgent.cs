using ActressMas;
using System;
using System.Collections.Generic;


/**
 * This class represents a device agent.
 * A device agent has a task it desires the edge server to perform.
 * It sends a request to the edge server, through the marketplace, to perform a task, with the value of the task included.
 * This form of request is a bid, which will be accepted or rejected by the edge server. 
 */

namespace MAS
{
    public class DeviceAgent : Agent
    { 
        private ResourceType resourceType;
        private int task, value;
        private static Random random = new Random();

        public DeviceAgent(ResourceType resource)
        {
            resourceType = resource;
        }

        public override void Setup()
        {
            Send("marketplace", $"bid {resourceType}");
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
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}