using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;

using ASaveInvoice;
using System.Threading;

namespace Software2Validator
{
    public class Process
    {
        static int CONSTSeed = 100;
        static int CONSTSleepValue = 200;
        static double CONSTErrorProb = 0.2;
        static double CONSTErrorPdfProb = 0.1;

        public static ResponseModel ProcesInvoice(string XML)
        {
            InvoiceModel Invoice = new InvoiceModel();
            Random random = new Random(CONSTSeed);
            ResponseModel responseModel = new ResponseModel();
            Thread.Sleep(CONSTSleepValue);
            if (random.NextDouble() <= CONSTErrorProb)
            {
                responseModel.success = false;
            }
            else
            {
                responseModel.CUFE = (new Guid()).ToString();
                responseModel.success = SaveInvoiceProcess.Save(Invoice);
            }
            return responseModel;
        }

        public static ResponseModel AttachPDF(string invoiceData, byte[] pdf)
        {
            InvoiceModel Invoice = new InvoiceModel();
            Random random = new Random(CONSTSeed);
            ResponseModel responseModel = new ResponseModel();
            if (random.NextDouble() <= CONSTErrorPdfProb)
            {
                responseModel.success = false;
            }
            else
            {
                responseModel.CUFE = (new Guid()).ToString();
                responseModel.success = SaveInvoiceProcess.AttachPDF(invoiceData, pdf);
            }
            return responseModel;
        }
    }
}
