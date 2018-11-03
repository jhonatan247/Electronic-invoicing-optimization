using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;

namespace GeneralServices
{
    public class ThirdPartyService
    {
        public static void UpdateCustomerState(string invoiceData, CustomerAction oAction)
        {
            // [TODO] cambiar el estado de accion del cliente, en caso de rechazao notificar al obligado a facturar
            throw new NotImplementedException();
        }
    }
}
