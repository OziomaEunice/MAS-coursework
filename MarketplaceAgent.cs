using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// this class belongs to the marketplace agent. It is responsible for coordinating
// and facilitating the matching bids from device agents and resource offers from edge servers.

namespace MAS
{
    internal class MarketplaceAgent : Agent
    {
        // create the marketplace agent's properties to store bids and resource offers in
        // a dictionary lists
        private Dictionary<string, List<string>> edgeServerOffers;
        private Dictionary<string, List<string>> deviceBids;


        // create the marketplace agent's constructor
        public MarketplaceAgent()
        {
            edgeServerOffers = new Dictionary<string, List<string>>();
            deviceBids = new Dictionary<string, List<string>>();
        }


        // create the marketplace agent's act method
        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}");
                message.Parse(out string action, out List<string> parameters);

                // create the marketplace agent's behaviour to handle the bidding for resources
                // from the edge servers and the bidding for tasks from the device agents.
                switch (action)
                {
                    case "Offer":
                        // store the edge server's offer in the dictionary list
                        edgeServerOffers.Add(message.Sender, parameters);
                        break;
                    case "Bid":
                        // store the device agent's bid in the dictionary list
                        deviceBids.Add(message.Sender, parameters);
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