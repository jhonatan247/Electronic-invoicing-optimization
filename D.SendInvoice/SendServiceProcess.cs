using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;
using System.Threading;

namespace DSendInvoice
{
    public class SendServiceProcess
    {
        static int CONSTSeed = 100;
        static int CONSTSleepValue = 2000;
        static double CONSTErrorProb = 0.1;

        public static ResponseModel SendInvoice(InvoiceModel invoice)
        {
            Random random = new Random(CONSTSeed);
            ResponseModel responseModel = new ResponseModel();
            Thread.Sleep(CONSTSleepValue);
            try
            {
                var result = DianService.DianServiceController.Receive(invoice.ToString());
                if (result.Equals("ok"))
                {
                    // [TODO] Implementar cambio de estado por envio exitoso
                    throw new NotImplementedException();
                    responseModel.success = true;
                }
                else
                {
                    // [TODO] Implementar cambio de estado por envio fallido y notificar al obligado y al administrador
                    throw new NotImplementedException();
                    responseModel.success = false;
                }
            }
            catch (Exception)
            {
                // [TODO] Implementar cambio de estado por envio fallido y notificar al obligado y al administrador
                throw new NotImplementedException();
                responseModel.success = false;
            }            
            return responseModel;
        }
    }
}
