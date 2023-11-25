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
        private int penaltyCost; //the penalty cost for sending the task to the cloud server
        private static Random rnd = new Random(); // create a random object to generate random numbers

        //create the device agent's constructor
        public DeviceAgent(int task, int valuation)
        {
            this.task = task;
            this.valuation = valuation;
        }

        //create the device agent's default constructor
        public DeviceAgent() {}


        // create the device agent's setup method
        public override void Setup()
        {
            task = rnd.Next(50, 300); // generate a random task between 50Mb and 299Mb
            valuation = rnd.Next(50, 101); // generate a random valuation between £50 and £100
            // send the device agent's bid task and value to the marketplace agent
            Send("marketplace", $"Bid {task}Mb for £{valuation}");
        }


        //create the device agent's act method
        public override void Act(Message messages)
        {
            // create the device agent's behaviour to handle bidding for resources
            // from the edge servers.
        }


        private int CalculateBidValue()
        {
            throw new NotImplementedException(); // remove this and implement logic
        }

        private void SendTaskToCentralisedCloudServer()
        {
            throw new NotImplementedException(); // remove this and implement logic
        }

        private void PayPenaltyCost()
        {
            // Implement logic to deduct penalty cost from device's funds or resources
            // Deduct the cost from the device's valuation or available funds
            // For example:
            int remainingFunds = valuation - penaltyCost;
            Console.WriteLine($"Penalty cost paid. Remaining funds: {remainingFunds}");
        }


        //create the device agent's act dafult method
        public override void ActDefault()
        {
            // default behaviour of the device agent

        }
    }
}