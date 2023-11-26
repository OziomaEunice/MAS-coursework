﻿using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS
{
    public class CloudServerAgent : Agent
    {
        private int costOfService;
        private static Random random = new Random();

        public CloudServerAgent(int costOfService)
        {
            this.costOfService = costOfService;
        }

        public CloudServerAgent() { }

        public override void Setup()
        {
            costOfService = random.Next(900, 1200);  // £900 to £1199
            Send("marketplace", $"Cost of Service fee starts at £{costOfService}");
        }

        public override void Act(Message message)
        {
            try
            {
                Console.WriteLine($"\t{message.Format()}");
                message.Parse(out string action, out List<string> parameters);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}