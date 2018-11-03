using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;

namespace ASaveInvoice
{
    public class SaveInvoiceProcess
    {
        public static bool Save(InvoiceModel oInvoice)
        {
            /// [TODO] Codigo para almacenar las facturas
            throw new NotImplementedException();
        }

        public static bool AttachPDF(string invoiceData, byte[] pdf)
        {
            /// [TODO] Codigo para atachar el pdf a las facturas y notificar via Email al cliente de que se registro una factura
            throw new NotImplementedException();
        }
    }
}
