using ActressMas;
using System;
using System.Collections.Generic;

namespace MAS
{
    public class MarketplaceAgent : Agent
    {
        private Dictionary<string, List<string>> _serviceProviders;

        public MarketplaceAgent()
        {
            _serviceProviders = new Dictionary<string, List<string>>();
        }

        public override void Setup()
        {
            Console.WriteLine($"=============================\n*******AUCTION SYSTEM*******\n=============================");
        }

        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}\n");
                message.Parse(out string action, out List<string> parameters);

                switch (action)
                {
                    case "register":
                        HandleRegister(message.Sender, parameters[0]);
                        break;

                    case "unregister":
                        HandleUnregister(message.Sender, parameters[0]);
                        break;

                    case "search":
                        HandleSearch(message.Sender, parameters[0]);
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

        private void HandleRegister(string provider, string service)
        {
            if (_serviceProviders.ContainsKey(service))
            {
                List<string> sp = _serviceProviders[service];
                if (!sp.Contains(provider))
                    sp.Add(provider);
            }
            else
            {
                List<string> sp = new List<string> { provider };
                _serviceProviders.Add(service, sp);
            }
        }

        private void HandleUnregister(string provider, string service)
        {
            if (_serviceProviders.ContainsKey(service))
            {
                List<string> sp = _serviceProviders[service];
                if (sp.Contains(provider))
                    sp.Remove(provider);
            }
        }

        private void HandleSearch(string client, string service)
        {
            //If the broker finds the service requested by the client
            //allocated the service provider to the client,
            //else send a message to the client that it doesn't provide the service
            if (_serviceProviders.ContainsKey(service))
            {
                List<string> sp = _serviceProviders[service];
                string res = "";
                foreach (string p in sp)
                    res += $"{p} ";
                Send(client, $"providers {res.Trim()}");
            }
            else { Send(client, $"We don't provide this service"); }
        }
    }
}