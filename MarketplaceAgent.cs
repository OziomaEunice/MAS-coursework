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
        private Dictionary<string, List<string>> _serviceProviders;


        // create the marketplace agent's constructor
        public MarketplaceAgent()
        {
            _serviceProviders = new Dictionary<string, List<string>>();
        }


        // create the marketplace agent's act method
        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}");
                message.Parse(out string action, out List<string> parameters);

                switch (action)
                {
                    case "search":
                        // handle the search request from the device agent
                        // prepare and send the auction request to the edge servers
                        //[...]

                        Send("edge servers", $"Auction Request");
                        break;

                    case "Auction Result":
                        // process the auction result received from the edge servers
                        // update behaviour to handle the auction result
                        //[...]

                        Send("device", $"Auction Result");
                        break;

                    case "Bid":
                        // process the auction result received from the edge servers
                        // update behaviour to handle the auction result
                        //[...]

                        Send("device", $"Auction Result");
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
