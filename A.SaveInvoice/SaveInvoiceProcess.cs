using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;

namespace ASaveInvoice
{
    public class SaveInvoiceProcess
    {
        static string CONSTSavedFolder = "./Invoices/";
        public static bool Save(InvoiceModel oInvoice)
        {
            bool saved = true;
            //System.IO.File.WriteAllBytes("Archivo.pdf", oInvoice.pdfFile);
            
            return saved;
        }

        public static bool AttachPDF(string invoiceData, byte[] pdf)
        {
            /// [TODO] Codigo para atachar el pdf a las facturas y notificar via Email al cliente de que se registro una factura
            throw new NotImplementedException();
        }
    }
}
