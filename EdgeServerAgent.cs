using ActressMas;
using System;
using System.Collections.Generic;

namespace MAS
{
    public enum ServiceType { Add, Sub, Mul, Div, Cos };

    public class EdgeServerAgent : Agent
    {
        private ServiceType _type;

        public EdgeServerAgent(ServiceType serviceType)
        {
            _type = serviceType;
        }

        public override void Setup()
        {
            Send("marketplace", $"register {_type}");
        }

        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}");
                message.Parse(out string action, out List<string> parameters);

                switch (action)
                {
                    case "force-unregister":
                        HandleForceUnregister();
                        break;

                    case "request":
                        HandleRequest(message, parameters);
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

        private void HandleForceUnregister()
        {
            Send("broker", $"unregister {_type}");
        }

        private void HandleRequest(Message message, List<string> parameters)
        {
            int p1 = Convert.ToInt32(parameters[0]);
            int p2 = Convert.ToInt32(parameters[1]);
            int result = 0;
            //int result = (_type == ServiceType.Add) ? (p1 + p2) : (p1 - p2);
            switch (_type)
            {
                case ServiceType.Add:
                    result = p1 + p2;
                    break;

                case ServiceType.Sub:
                    result = p1 - p2;
                    break;

                case ServiceType.Mul:
                    result = p1 * p2;
                    break;

                case ServiceType.Div:
                    result = p1 / p2;
                    break;

                default:
                    break;
            }

            Send(message.Sender, $"response {result}");
        }
    }
}