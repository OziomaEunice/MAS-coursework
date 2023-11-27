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
        private int taskSize;
        private int valuation;
        private static Random random = new Random();

        public DeviceAgent(int taskSize, int valuation)
        {
            this.taskSize = taskSize;
            this.valuation = valuation;
        }

        public DeviceAgent() { }

        public override void Setup()
        {
            taskSize = random.Next(100, 1100);  // 100Mb to 1099Mb
            valuation = random.Next(30, 250);  // £30 to £249
            Send("marketplace", $"Bid {taskSize}Mb with valuation at £{valuation}");
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
