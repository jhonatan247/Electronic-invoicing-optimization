using System;
using System.Collections.Generic;
using System.Text;
using GeneralBusiness;

using ASaveInvoice;
using System.Threading;

namespace Software1Validator
{
    public class Process
    {
        static int CONSTSeed = 10;
        static int CONSTSleepValue = 300;
        static double CONSTErrorProb = 0.3;
        static double CONSTErrorPdfProb = 0.1;

        public static ResponseModel ProcesInvoice(RequestModel request)
        {
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
                responseModel.success = SaveInvoiceProcess.Save(request);
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
