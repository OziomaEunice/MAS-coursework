using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// this class belongs to the device agents. It is responsible for bidding
// the resources that edge servers have. 
namespace MAS
{
    internal class DeviceAgent : Agent
    {
        //create the device agent's properties: task and valuation
        private int task;
        private int valuation;

        //create the device agent's constructor
        public DeviceAgent(int task, int valuation)
        {
            this.task = task;
            this.valuation = valuation;
        }

        //create the device agent's default constructor
        public DeviceAgent() {}


        //create the device agent's setup method
        public override void Setup()
        {
            //create the device agent's behaviour
            Send("marketplace", $"search £{valuation}"); //send the valuation to the auctioneer
        }


        //create the device agent's act method
        public override void Act(Message messages)
        {
            // create the device agent's behaviour to handle bidding for resources
            // from the edge servers.
            if (messages.Content.StartsWith("Auction Result"))
            {
                // process the auction result received from the marketplace
                // update behaviour to handle the auction result
            }
            else if (messages.Content.StartsWith("Auction Request"))
            {
                // handle the auction request from the marketplace
                // prepare and send the bids to the marketplace
                int availableTask = 8;
                int bidValue = CalculateBidValue(); //calculate the bid value based on the task and valuation
                Send("marketplace", $"Bid {availableTask} £{bidValue}"); //send the bid to the auctioneer
            }
        }

        private int CalculateBidValue()
        {
            // do something...
        }

        //create the device agent's act dafult method
        public override void ActDefault()
        {
            // default behaviour of the device agent

        }
    }
}
