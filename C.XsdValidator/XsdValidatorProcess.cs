using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;
using System.Threading;

namespace CXsdValidator
{
    public class XsdValidatorProcess
    {
        static int CONSTSleepValue = 500;
        static double CONSTErrorProb = 0.1;

        public static ResponseModel ValidateInvoice(InvoiceModel invoice)
        {
            Random random = new Random();
            ResponseModel responseModel = new ResponseModel();
            Thread.Sleep(CONSTSleepValue);
            if (random.NextDouble() <= CONSTErrorProb)
            {
                responseModel.success = false;
                responseModel.error = Error.validateWithXSD;
            }
            else
            {
                responseModel.success = true;
            }
            return responseModel;
        }
    }
}
