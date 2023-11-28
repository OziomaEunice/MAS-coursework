using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/*
 * This class represents a bid.
 * A bid is sent by a device agent to an edge server agent, via the marketplace.
 * It contains the device agent ID, the quantity of data to be processed, and the price the device agent is willing to pay.
 */

namespace MAS
{
    public class Bid
    {
        public string DeviceAgentID { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

}