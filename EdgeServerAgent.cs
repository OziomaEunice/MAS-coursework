using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * This class represents an edge server agent.
 * It has capacity to process the tasks sent to it by the device agents. 
 * In addition, it provides the resources it has available
 * In processing a task, it has a cost (measured in pence) that it needs to pay per 10Mb of data processed.
 * It can accept or reject bids from device agents, depending on whether it has the capacity to process the task.
 * Whatever it decides, the message is sent via the marketplace to the device agent.
 */

namespace MAS
{
    public enum Resources { CPU, RAM, Bandwidth };
    public class EdgeServerAgent : Agent
    {
        public int Capacity { get; set; }
        public double CostPerUnit { get; set; }
        private Resources resources;
        private static Random random = new Random();

        public EdgeServerAgent(int capacity, double costPerUnit, Resources resource)
        {
            Capacity = capacity;
            CostPerUnit = costPerUnit;
            resources = resource;
        }

        public EdgeServerAgent() { }

        public override void Setup()
        {
            Capacity = random.Next(300, 1201);  // 300Mb to 1200Mb
            CostPerUnit = random.Next(50, 501);  // £50 to £500 per 10Mb


            // make the agent randomly have a resource listed in the Resources {}
            Array resourceItems = Enum.GetValues(typeof(Resources));
            int indexOfResources = random.Next(resourceItems.Length);
            Resources[] resourcesArray = (Resources[])resourceItems;
            resources = resourcesArray[indexOfResources];
            
            Send("marketplace", $"Offer {Capacity} Mb at £ {CostPerUnit} per 10Mb");
            Send("marketplace", $"Resource available: {resources}");
        }

        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}");
                message.Parse(out string action, out List<string> parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}