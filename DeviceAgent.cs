using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        public int TaskSize { get; set; }
        public double Valuation { get; set; }
        private Resources resources;
        private static Random random = new Random();

        public DeviceAgent(int taskSize, double valuation, Resources resource)
        {
            TaskSize = taskSize;
            Valuation = valuation;
            resources = resource;
        }

        public DeviceAgent() { }

        public override void Setup()
        {
            TaskSize = random.Next(100, 1100);  // 100Mb to 1099Mb
            Valuation = random.Next(70, 521);  // £70 to £520

            // make the agent randomly have a resource listed in the Resources {}
            Array resourceItems = Enum.GetValues(typeof(Resources));
            int indexOfResources = random.Next(resourceItems.Length);
            Resources[] resourcesArray = (Resources[])resourceItems;
            resources = resourcesArray[indexOfResources];


            Send("marketplace", $"Bid {TaskSize} Mb at £ {Valuation}");
            Send("marketplace", $"Search for {resources}");
        }

        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}");
                message.Parse(out string action, out List<string> parameters);

                /*switch (action)
                {
                    case "Hey":
                        HandlePenaltyPayment(message.Sender, Convert.ToInt32(parameters[11]));
                        break;

                    default:
                        break;
                }*/
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /*private void HandlePenaltyPayment(string marketplace, int msg)
        {
            Send(marketplace, $"Alright. Payment of £ {msg} will be made.");
        }*/
    }
}