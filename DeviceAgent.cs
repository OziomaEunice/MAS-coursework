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
            Send("auctioneer", $"search{valuation}"); //send the valuation to the auctioneer
        }


        //create the device agent's act method
        public override void Act(Message messages)
        {
            //create the device agent's behaviour
            if (messages.Content.ToString().Contains("bid")) //if the message contains the word "bid"
            {
                string[] message = messages.Content.ToString().Split(' '); //split the message into an array of strings
                int bid = int.Parse(message[1]); //parse the second string in the array into an integer
                if (bid > valuation) //if the bid is greater than the valuation
                {
                    Send("auctioneer", $"accept{bid}"); //send the bid to the auctioneer
                }
            }
        }

        //create the device agent's act dafult method
        public override void ActDefault()
        {
            // default behaviour of the device agent

        }
    }
}
