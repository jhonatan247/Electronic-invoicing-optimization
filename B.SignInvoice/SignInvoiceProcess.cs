using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;
using System.Threading;

namespace BSignInvoice
{
    public class SignInvoiceProcess
    {
        static int CONSTSeed = 100;
        static int CONSTSleepValue = 2000;
        static double CONSTErrorProb = 0.1;
        
        public static ResponseModel SignInvoice(InvoiceModel invoice)
        {
            Random random = new Random(CONSTSeed);
            ResponseModel responseModel = new ResponseModel();
            Thread.Sleep(CONSTSleepValue);
            if (random.NextDouble() <= CONSTErrorProb)
            {
                // [TODO] Implementar cambio de estado por fallo de firma y notificar al administrador del sistema
                throw new NotImplementedException();
                responseModel.success = false;
            }
            else
            {
                // [TODO] Implementar actualizacion de UBL de factura asumiendo firma exitosa y cambio de estado
                throw new NotImplementedException();
                responseModel.success = true;
            }
            return responseModel;
        }
    }
}
