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
        static int CONSTSleepValue = 200;
        static double CONSTErrorProb = 0.2;
        static double CONSTErrorPdfProb = 0.1;

        public static ResponseModel ProcesInvoice(InvoiceModel request)
        {
            Random random = new Random();
            ResponseModel responseModel = new ResponseModel();
            Thread.Sleep(CONSTSleepValue);
            if (random.NextDouble() <= CONSTErrorProb)
            {
                responseModel.success = false;
                responseModel.error = Error.validate;
            }
            else
            {
                responseModel.CUFE = (new Guid()).ToString();
                responseModel.success = SaveInvoiceProcess.Save(request);
                if (!responseModel.success)
                    responseModel.error = Error.save;
            }
            return responseModel;
        }

        public static ResponseModel AttachPDF(InvoiceModel request)
        {
            Random random = new Random();
            ResponseModel responseModel = new ResponseModel();
            if (random.NextDouble() <= CONSTErrorPdfProb)
            {
                responseModel.success = false;
                responseModel.error = Error.validate;
            }
            else
            {
                responseModel.CUFE = (new Guid()).ToString();
                responseModel.success = SaveInvoiceProcess.AttachPDF(request);
                if (!responseModel.success)
                    responseModel.error = Error.save;
            }
            return responseModel;
        }
    }
}
