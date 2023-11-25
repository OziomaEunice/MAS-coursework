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
        private string[] resources = { "CPU", "RAM", "Storage", "Network bandwidth" }; // the resources that the edge server agent offers



        // create the edge server agent's constructor
        public EdgeServerAgent() 
        {
            // randomly generate the capacity and cost per unit
            Random rnd = new Random();
            capacity = rnd.Next(55, 250); // generate a random capacity between 55Mb and 250Mb
            costPerUnit = rnd.Next(1, 11); // generate a random cost per 10Mb between 1 penny and 10 pence
        }


        // create the edge server agent's setup method
        public override void Setup()
        {
            Random randomOffer = new Random();
            int randomIndex = randomOffer.Next(resources.Length); // generate a random index
            string selectResource = resources[randomIndex]; // get the resource at the random index
            
            // send the edge server agent's offer to the marketplace agent
            Send("marketplace", $"Offer {selectResource} with capacity of {capacity}Mb per 0.{costPerUnit}p");
        }


        // create the edge server agent's act method
        public override void Act(Message message)
        {
            // receive the bid from the device agent through the marketplace,
            // process the bid and send the auction result to the marketplace agent.
        }

        private int ExtractBidValueFromMessage(Message message)
        {
            throw new NotImplementedException();  // remove this and implement logic
        }
    }
}