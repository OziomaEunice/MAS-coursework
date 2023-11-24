using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// this class belongs to the edge server agents. They offer resources that
// devices agents need, however, they only have a limited amount available to
// distribute.
namespace MAS
{
    internal class EdgeServerAgent : Agent
    {
        // create the edge server agent's properties: capacity and cost per unit
        private int capacity;       // capacity in Mb
        private int costPerUnit;   // cost per 10Mb of data processed



        // create the edge server agent's constructor
        public EdgeServerAgent() 
        {
            // randomly generate the capacity and cost per unit
            Random rnd = new Random();
            capacity = rnd.Next(55, 250); // generate a random capacity between 55Mb and 250Mb
            costPerUnit = rnd.Next(1, 10); // generate a random cost per 10Mb between 1 penny and 10 pence
        }


        // create the edge server agent's setup method
        public override void Setup()
        {
            // create the edge server agent's behaviour to handle bidding for
            // resources from the devices.
        }


        // create the edge server agent's act method
        public override void Act(Message message)
        {
            // receive the bid from the device agent through the marketplace,
            // process the bid and send the auction result to the marketplace agent.
            if (message.Content.StartsWith("Bid"))
            {
                int capacityToProcess = 10;
                int bidValue = ExtractBidValueFromMessage(message);

                if (bidValue > /*cost criteria*/)
                {
                    Send("marketplace", $"Auction Result: bid accepted");
                }
                else
                {
                    Send("marketplace", $"Auction Result: bid rejected");
                }
            }
            
        }

        private int ExtractBidValueFromMessage(Message message)
        {
            throw new NotImplementedException();  // remove this and implement logic
        }
    }
}
