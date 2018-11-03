using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;
using System.Threading;

namespace EConfirmState
{
    public class ConfirmStateProcess
    {
        static int CONSTSeed = 100;
        static int CONSTSleepValue = 2000;

        public static ResponseModel ConfirmState(InvoiceModel invoice)
        {
            Random random = new Random(CONSTSeed);
            ResponseModel responseModel = new ResponseModel();
            Thread.Sleep(CONSTSleepValue);
            try
            {
                var result = DianService.DianServiceController.Receive(invoice.ToString());
                if (result.Equals("ok"))
                {
                    // [TODO] Implementar cambio de estado por estado exitoso
                    throw new NotImplementedException();
                    responseModel.success = true;
                }
                else
                {
                    // [TODO] Implementar cambio de estado por estado fallido y notificar al obligado y al administrador
                    throw new NotImplementedException();
                    responseModel.success = false;
                }
            }
            catch (Exception)
            {
                // [TODO] Notificar al obligado y al administrador del error de consulta de estado
                throw new NotImplementedException();
                responseModel.success = false;
            }
            return responseModel;
        }
    }
}
