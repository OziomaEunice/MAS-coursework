using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * This class represents an offer.
 * An offer is sent by an edge server agent to a device agent, via the marketplace.
 * It contains the edge server agent ID, the quantity of data to be processed, and the price the edge server agent is willing to accept.
 */

namespace MAS
{
    public class Offer
    {
        public string EdgeServerAgentID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}