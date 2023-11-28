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
        private static Random random = new Random();

        public DeviceAgent(int taskSize, double valuation)
        {
            TaskSize = taskSize;
            Valuation = valuation;
        }

        public DeviceAgent() { }

        public override void Setup()
        {
            TaskSize = random.Next(100, 1100);  // 100Mb to 1099Mb
            Valuation = random.Next(70, 521);  // £70 to £520
            Send("marketplace", $"Bid {TaskSize} Mb at £ {Valuation}");
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