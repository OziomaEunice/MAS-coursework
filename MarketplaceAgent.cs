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
        private Dictionary<string, List<string>> resourcesType;

        public MarketplaceAgent()
        {
            edgeServerOffer = new List<Offer>();
            deviceBid = new List<Bid>();
            resourcesType = new Dictionary<string, List<string>>();
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
                        HandleOffer(message.Sender, Convert.ToInt32(parameters[0]), Convert.ToDouble(parameters[4]));
                        break;

                    case "Bid":
                        // when receive the bids from the device, add them to the list of bids
                        HandleBid(message.Sender, Convert.ToInt32(parameters[0]), Convert.ToDouble(parameters[4]));
                        break;

                    //case "Cost":
                        //HandleFee(message.Sender, Convert.ToInt32(parameters[8]));
                        //break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /*private void HandleFee(string device, int fee)
        {
            // Get the fee from the cloud server and submit it to the device agent
            //Bid bid = new Bid 
            {
                DeviceAgentID = device
            };

            //Send(device, $"The fee received from the cloud server is {fee}");
        }*/
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

            // check that the offer has been added to the list
            if (edgeServerOffer.Count > 0)
            {
                Send(edgeServer, $"Added offer at {offer.Quantity} Mb at £ {offer.Price} for each 10Mb to the list");
            }

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

            // check that the bid has been added to the list
            if (deviceBid.Count > 0)
            {
                Send(device, $"Added bid of {bid.Quantity} Mb at £ {bid.Price} to the list");
            }

            DoubleAuction();  // call the double auction method
        }

        private void DoubleAuction()
        {
            /* To allocate the resource, the auction protocol that will be used is the average mechanism in the Double Auction:
                1. Order the bids and offers by price
                2. Calculate the breakeven index (k) based on ordered bids and offers
                3. Find the price as the average of the kth values (price = (bk + ok)/2:
                4. Allocate the resource by letting first k sellers (edge server) sell offer to the first k buyers (device).
                5. If the resource cannot be allocated, send the task to the cloud server.
                6. Notify the buyer (device) and seller (edge server) of all the transactions, whether successful or not.
            */

            // Order the bids and offers by price
            var orderedBids = deviceBid.OrderBy(b => b.Price).ToList();
            var orderedOffers = edgeServerOffer.OrderBy(o => o.Price).ToList();

            // Calculate the breakeven index (k) based on ordered bids and offers
            int k = Math.Min(orderedBids.Count, orderedOffers.Count);

            // find the price as the average of the kth values => price = (bk + ok) / 2
            if (k > 0)
            {
                double price = (orderedBids[k-1].Price + orderedOffers[k-1].Price) / 2;

                // allocate the resource
                for (int i = 0; i < k; i++)
                {
                    NotifyDevice(orderedBids[i].DeviceAgentID, price);
                    NotifyEdgeServer(orderedOffers[i].EdgeServerAgentID, price);

                    // remove the offer and the bid
                    deviceBid.Remove(orderedBids[i]);
                    edgeServerOffer.Remove(orderedOffers[i]);
                }
            }

            // if the resource cannot be allocated, inform the device agent
            // and then send the task to the cloud server
            foreach (var bid in deviceBid)
            {
                Send(bid.DeviceAgentID, $"Sorry, no offer available. Task will be sent to the cloud server");
                Send("cloudServer", $"Process {bid.Quantity} Mb of {bid.DeviceAgentID}");
            }
        }

        private void NotifyDevice(string deviceAgentID, double price)
        {
            Bid bid = new Bid
            {
                DeviceAgentID = deviceAgentID,
                Price = price
            };

            Send(deviceAgentID, $"Your task has been allocated with a price at £ {price}");
        }

        private void NotifyEdgeServer(string edgeServerAgentID, double price)
        {
            Offer offer = new Offer
            {
                EdgeServerAgentID = edgeServerAgentID,
                Price = price
            };

            Send(edgeServerAgentID, $"You have been allocated with a task at £ {price}");
        }
    }
}