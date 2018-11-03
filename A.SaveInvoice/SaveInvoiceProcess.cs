using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;
using Data.CRUD;

namespace ASaveInvoice
{
    public class SaveInvoiceProcess
    {
        static string CONSTSavedFolder = "./Invoices/";
        
        static double CONSTErrorProb = 0.3;

        public static bool Save(InvoiceModel request)
        {
            Request.Update_PDF(request.Id);
            Random random = new Random();
            if (random.NextDouble() <= CONSTErrorProb)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool AttachPDF(InvoiceModel request)
        {
            Request.Update_XML(request.Id);
            Random random = new Random();
            if (random.NextDouble() <= CONSTErrorProb)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
