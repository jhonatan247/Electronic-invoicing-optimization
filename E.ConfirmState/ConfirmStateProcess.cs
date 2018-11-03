using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;
using System.Threading;

namespace EConfirmState
{
    public class ConfirmStateProcess
    {
        static int CONSTSleepValue = 1000;

        public static ResponseModel ConfirmState(InvoiceModel invoice)
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

                    responseModel.error = Error.decline;
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
