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
    }
}
