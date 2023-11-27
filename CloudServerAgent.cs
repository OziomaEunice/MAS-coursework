using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * This class represents a cloud server agent.
 * It is responsible for processing tasks of device agents, who are not able to find a suitable edge server.
 * It charges a fee for processing the task, which can be much higher than the value of task that the device agent bid for.
 * */

namespace MAS
{
    public class CloudServerAgent : Agent
    {
        private int fee;
        private static Random random = new Random();

        public CloudServerAgent()
        {
            //fee = random.Next(700, 1001); // generate a random fee between £700 and £1000
        }

        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}\n");
                message.Parse(out string action, out List<string> parameters);

                switch (action)
                {
                    case "process":
                        ProcessTask(message.Sender, parameters[0]);
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

        private void ProcessTask(string device, string resource)
        {
            // Send a message to the device agent (through the marketplace), informing the cost of processing the task.
            // The cost is the fee plus a random number between £700 and £1000
            int cost = fee + random.Next(700, 1001);

            Send(device, $"cost of processing task is £{cost}");
        }
    }
}
