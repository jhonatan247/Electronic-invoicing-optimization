using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;
using System.Threading;

namespace BSignInvoice
{
    public class SignInvoiceProcess
    {
        static int CONSTSleepValue = 500;
        static double CONSTErrorProb = 0.1;
        
        public static ResponseModel SignInvoice(InvoiceModel invoice)
        {
            Random random = new Random();
            ResponseModel responseModel = new ResponseModel();
            Thread.Sleep(CONSTSleepValue);
            if (random.NextDouble() <= CONSTErrorProb)
            {
                responseModel.success = false;
                responseModel.error = Error.sign;
            }
            else
            {
                responseModel.success = true;
            }
            return responseModel;
        }
    }
}
