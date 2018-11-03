using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;
using System.Threading;

namespace CXsdValidator
{
    public class XsdValidatorProcess
    {

        static int CONSTSeed = 100;
        static int CONSTSleepValue = 2000;
        static double CONSTErrorProb = 0.1;

        public static ResponseModel ValidateInvoice(InvoiceModel invoice)
        {
            Random random = new Random(CONSTSeed);
            ResponseModel responseModel = new ResponseModel();
            Thread.Sleep(CONSTSleepValue);
            if (random.NextDouble() <= CONSTErrorProb)
            {
                // [TODO] Implementar actualizacion de estado de factura asumiendo falla de verificacion y notificaicon de fallo al administrador
                throw new NotImplementedException();
                responseModel.success = false;
            }
            else
            {
                // [TODO] Implementar cambio de estado de factura
                throw new NotImplementedException();
                responseModel.success = true;
            }
            return responseModel;
        }
    }
}
