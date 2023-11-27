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

                    //case "Cost":
                        //Send("cloudServer", $"Hi");
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
                Send(edgeServer, $"{offer.Quantity} Mb at £ {offer.Price * 10} for each 10Mb added");
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
                Send(device, $"{bid.Quantity} Mb and £ {bid.Price} added");
            }

            DoubleAuction();  // call the double auction method
        }

        private void DoubleAuction()
        {
            /* Implement double auction using average mechanism:
                1. Order the bids and offers by price and quantity
                2. Calculate the breakeven index (k) based on ordered bids and offers
                3. Find the price as the average of the kth values (price = (bk + ok)/2:
                4. Facilitate the transaction by letting first k sellers (edge server) sell offer to the first k buyers (device)
                5. Notify the buyer (device) and seller (edge server)
            */

            // check that offers and bids are in the list before ordering them
            if (!edgeServerOffer.Any() || !deviceBid.Any())
            {
                return;
            }
            else
            {
                // begin to order the prices of the bids and offers
                var orderedBids = deviceBid.OrderBy(b => b.Price).ToList();
                var orderedOffers = edgeServerOffer.OrderBy(o => o.Price).ToList();

                // calculate the breakeven index (k)
                int k = 0;

                for (int i = 0; i < orderedBids.Count; i++)
                {
                    // if the price of the bid is greater than or equal to the price of the offer,
                    // then set k = i
                    if (orderedBids[i].Price >= orderedOffers[i].Price)
                    {
                        k = i;
                        break;
                    } 
                }

                // calculate the price as the average of the kth values
                double price = (orderedBids[k].Price + orderedOffers[k].Price) / 2;


                // To avoid getting the error "Index was out of range. Must be non-negative and less than the size of the collection."
                // get the minimum value between the number of bids and offers
                int min = Math.Min(orderedBids.Count, orderedOffers.Count);
                // facilitate the transaction by letting first k sellers (edge server) sell offer to the first k buyers (device)
                for (int i = 0; i < k; i++)
                {
                    if (orderedBids[i].Price >= orderedOffers[i].Price) 
                    {
                        // notify the buyer (device) and seller (edge server)
                        NotifyDevice(orderedBids[i].DeviceAgentID, price);
                        NotifyEdgeServer(orderedOffers[i].EdgeServerAgentID, price);
                    }
                }
            }
        }

        private void NotifyDevice(string deviceAgentID, double price)
        {
            throw new NotImplementedException();
        }

        private void NotifyEdgeServer(string edgeServerAgentID, double price)
        {
            throw new NotImplementedException();
        }
    }
}