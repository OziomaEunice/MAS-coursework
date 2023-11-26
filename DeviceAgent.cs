using ActressMas;
using System;
using System.Collections.Generic;

namespace MAS
{
    public class DeviceAgent : Agent
    {
        private ServiceType _type;
        private int _operationParameter1, _operationParameter2;
        private static Random _rand = new Random();

        public DeviceAgent(ServiceType serviceType)
        {
            _type = serviceType;
        }

        public override void Setup()
        {
            Send("marketplace", $"search {_type}");
        }

        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}");
                message.Parse(out string action, out List<string> parameters);

                switch (action)
                {
                    case "providers":
                        HandleProviders(parameters);
                        break;

                    case "response":
                        HandleResponse(parameters[0]);
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

        private void HandleProviders(List<string> providers)
        {
            string selected = providers[_rand.Next(providers.Count)];
            _operationParameter1 = _rand.Next(100);
            _operationParameter2 = _rand.Next(100);
            Send(selected, $"request {_operationParameter1} {_operationParameter2}");
        }

        private void HandleResponse(string result)
        {
            Console.WriteLine($"[{Name}]: {_type}({_operationParameter1}, {_operationParameter2}) = {result}");
        }

        public override void ActDefault()
        {
            // if the client has not received a response yet,
            //then randomly pick a service type and then
            //send a message

            var v = Enum.GetValues(typeof(ServiceType));
            var t = v.GetValue(_rand.Next(v.Length));

            //Continue the process....
        }
    }
}