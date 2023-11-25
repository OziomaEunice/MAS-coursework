﻿using ActressMas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAS
{
    public class EdgeServerAgent : Agent
    {
        private int capacity;
        private int costPerUnit;
        private static Random random = new Random();

        public EdgeServerAgent(int capacity, int costPerUnit)
        {
            this.capacity = capacity;
            this.costPerUnit = costPerUnit;
        }

        public EdgeServerAgent() { }

        public override void Setup()
        {
            capacity = random.Next(100, 1000);  // 100Mb to 999Mb
            costPerUnit = random.Next(10, 100);  // 0.10p to 0.99p
            Send("marketplace", $"Offer {capacity}Mb with cost per unit at 0.{costPerUnit}p");
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
