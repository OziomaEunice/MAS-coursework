using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * This class represents a marketplace agent.
 * It is responsible for managing the marketplace. It manages the list of bids and offers of both
 * buyers (device) and sellers (edge server). It also manages the list of transactions that have been made.
 * If a transaction is made, it will notify the buyer and seller.
 * If a bid is unsuccessful, it will notify the buyer and will remove the bid from the list. Also, it 
 * will notify the cloud server agent, which will take care of the device's bid.
 */

/*
 * It uses the double auction mechanism.
 */

namespace MAS
{
    public class MarketplaceAgent : Agent
    {
        private List<Offer> edgeServerOffer;
        private List<Bid> deviceBid;

        public MarketplaceAgent()
        {
            edgeServerOffer = new List<Offer>();
            deviceBid = new List<Bid>();
        }

        public override void Setup()
        {
            Console.WriteLine("============================\n******AUCTION SYSTEM******\n============================");
        }

        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}\n");
                message.Parse(out string action, out List<string> parameters);

                /* // Debugging: Print parameters to check their content
                *  Console.WriteLine("Action: " + action);
                *  Console.WriteLine("Parameters:");
                *   foreach (var param in parameters)
                    {
                        Console.WriteLine(param);
                    }
                */

                switch (action)
                {
                    case "Offer":
                        // when receive the offers from the edge server handle add them to the list of offers
                        HandleOffer(message.Sender, Convert.ToInt32(parameters[0]), Convert.ToDouble(parameters[3]));
                        break;

                    case "Bid":
                        // when receive the bids from the device, add them to the list of bids
                        HandleBid(message.Sender, Convert.ToInt32(parameters[0]), Convert.ToDouble(parameters[4]));
                        break;

                    case "Cost":
                        Send("cloudServer", $"Hi");
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

        private void HandleOffer(string edgeServer, int capacity, double price)
        {
            // create an offer object from the received parameters and add it to the list of the edgeServerOffer list
            Offer offer = new Offer
            {
                EdgeServerAgentID = edgeServer,
                Quantity = capacity,
                Price = price
            }; 
            
            edgeServerOffer.Add(offer);

            DoubleAuction();  // call the double auction method
        }

        private void HandleBid(string device, int size, double price)
        {
            // create a bid object from the received parameters and add it to the list of the deviceBid list
            Bid bid = new Bid
            {
                DeviceAgentID = device,
                Quantity = size,
                Price = price
            };

            deviceBid.Add(bid);

            DoubleAuction();  // call the double auction method
        }

        private void DoubleAuction()
        {
            // Implement double auction using average mechanism:
            // Order the bids and offers by price
            // Find the average price and determine:
            // transaction is possible => if the average price is between the highest bid and the lowest offer
            // transaction is not possible => if the average price is higher than the highest bid or lower than the lowest offer
            // Notify the buyer (device) and seller (edge server)
        }
    }
}
