using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;
using System.Threading;

namespace DSendInvoice
{
    public class SendServiceProcess
    {
        static int CONSTSleepValue = 500;
        static double CONSTErrorProb = 0.1;

        public static ResponseModel SendInvoice(InvoiceModel invoice)
        {
            Random random = new Random();
            ResponseModel responseModel = new ResponseModel();
            Thread.Sleep(CONSTSleepValue);
            try
            {
                var result = DianService.DianServiceController.Receive(invoice.ToString());
                if (result.Equals("ok"))
                {
                    responseModel.success = true;
                }
                else
                {
                    responseModel.error = Error.send;
                    responseModel.success = false;
                }
            }
            catch (Exception)
            {
                responseModel.error = Error.connection;
                responseModel.success = false;
            }            
            return responseModel;
        }
    }
}
