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
        private List<string> edgeServerOffer;
        private List<string> deviceBid;
        private List<string> cloudServerOffer;

        public MarketplaceAgent()
        {
            edgeServerOffer = new List<string>();
            deviceBid = new List<string>();
            cloudServerOffer = new List<string>();
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

                switch (action)
                {
                    case "Offer":
                        Send("edgeServer1", $"Hey");
                        break;

                    case "Bid":
                        Send("Mobile device", $"Ciao");
                        break;

                    case "Cost":
                        Send("cloudServer", $"Hi");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
