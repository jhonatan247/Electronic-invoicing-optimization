using System;
using GeneralBusiness;
using Data.CRUD;
using System.Data;
using System.Threading;

namespace Server
{
    public class Validator1Server
    {
        public static ThreadStart SignInvoiceDelegate = new ThreadStart(SignInvoice);
        public static Thread SignInvoiceThread = new Thread(SignInvoiceDelegate);

        DataTable dtRequests;
        public Validator1Server() {
            dtRequests = Request.SelectValidator(1);
            if (SignInvoiceThread.ThreadState != ThreadState.Running && SignInvoiceThread.ThreadState != ThreadState.WaitSleepJoin)
            {
                SignInvoiceThread = new Thread(SignInvoiceDelegate);
                SignInvoiceThread.Start();
            }
        }

        public void Start() {
            InvoiceModel currentRequest;
            ResponseModel currentResponse;
            for (int i = 0; i < dtRequests.Rows.Count; i++)
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + (dtRequests.Rows.Count - i) + " requests are in step 1, in the validator 1");
                Thread.Sleep(500);
                String type = dtRequests.Rows[i][2].ToString();
                if (type == "PDF")
                {
                    currentRequest = new InvoiceModel(Int64.Parse(dtRequests.Rows[i][0].ToString()), RequestType.PDF, dtRequests.Rows[i][7].ToString());
                    currentResponse = Software1Validator.Process.AttachPDF(currentRequest);
                }
                else
                {
                    currentRequest = new InvoiceModel(Int64.Parse(dtRequests.Rows[i][0].ToString()), RequestType.PDF, dtRequests.Rows[i][8].ToString());
                    currentResponse = Software1Validator.Process.ProcesInvoice(currentRequest);
                }
                if (currentResponse.success)
                {
                    Request.Update_Estado(currentRequest.Id, (int)InvoiceState.saved);
                    if (SignInvoiceThread.ThreadState != ThreadState.Running && SignInvoiceThread.ThreadState != ThreadState.WaitSleepJoin)
                    {
                        SignInvoiceThread = new Thread(SignInvoiceDelegate);
                        try
                        {
                            SignInvoiceThread.Start();
                        }
                        catch { }
                    }
                }
                else
                {
                    if (currentResponse.error == Error.validate)
                        Request.Update_Estado(currentRequest.Id, (int)InvoiceState.validationError);
                    else
                        Request.Update_Estado(currentRequest.Id, (int)InvoiceState.saveError);

                }
            }
            Console.WriteLine(DateTime.Now.ToLongTimeString() + " The validator 1 has no requests");
            dtRequests = Request.SelectValidator(1);
            if (dtRequests.Rows.Count > 0)
                Start();
        }
        static void SignInvoice()
        {
            StepBServer step = new StepBServer();
            step.Start();
        }

    }
}
