using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DianService
{
    public class DianServiceController
    {
        static int CONSTSleepValue = 100;
        static double CONSTErrorProb = 0.1;
        static double CONSTSystemDownProb = 0.1;

        public static string Receive(string invoice)
        {
            Random random = new Random();
            Thread.Sleep(CONSTSleepValue);
            if (random.NextDouble() <= CONSTSystemDownProb)
            {
                throw new Exception("Service no available");
            }
            if (random.NextDouble() <= CONSTErrorProb)
            {
                return "error";
            }
            else
            {
                return "ok";
            }            
        }

        public static string GetInvoiceState(string invoice)
        {
            Random random = new Random();
            Thread.Sleep(CONSTSleepValue);
            if (random.NextDouble() <= CONSTSystemDownProb)
            {
                throw new Exception("Service no available");
            }
            if (random.NextDouble() <= CONSTErrorProb)
            {
                return "error";
            }
            else
            {
                return "ok";
            }
        }
    }
}
