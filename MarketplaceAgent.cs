using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS
{
    public class MarketplaceAgent : Agent
    {
        private Dictionary<string, List<string>> edgeServerOffer;
        private Dictionary<string, List<string>> deviceBid;
        private Dictionary<string, List<string>> cloudServerOffer;

        public MarketplaceAgent()
        {
            edgeServerOffer = new Dictionary<string, List<string>>();
            deviceBid = new Dictionary<string, List<string>>();
            cloudServerOffer = new Dictionary<string, List<string>>();
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
