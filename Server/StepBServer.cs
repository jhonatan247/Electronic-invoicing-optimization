using Data.CRUD;
using System;
using System.Data;
using GeneralBusiness;
using System.Threading;

namespace Server
{
    public class StepBServer
    {
        public static ThreadStart XsdValidatorDelegate = new ThreadStart(XsdValidator);
        public static Thread XsdValidatorThread = new Thread(XsdValidatorDelegate);

        DataTable dtRequests;
        public StepBServer()
        {
            dtRequests = Request.SelectStatus((int)InvoiceState.saved);
        }

        public void Start()
        {
            InvoiceModel currentRequest;
            ResponseModel currentResponse;
            for (int i = 0; i < dtRequests.Rows.Count; i++)
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + (dtRequests.Rows.Count - i) + " requests are in step 2");
                Thread.Sleep(500);
                String type = dtRequests.Rows[i][2].ToString();
                currentRequest = new InvoiceModel(Int64.Parse(dtRequests.Rows[i][0].ToString()), RequestType.PDF, dtRequests.Rows[i][10].ToString());
                currentResponse = BSignInvoice.SignInvoiceProcess.SignInvoice(currentRequest);
                if (currentResponse.success)
                {
                    Request.Update_Estado(currentRequest.Id, (int)InvoiceState.signed);
                    if (XsdValidatorThread.ThreadState != ThreadState.Running && XsdValidatorThread.ThreadState != ThreadState.WaitSleepJoin)
                    {
                        XsdValidatorThread = new Thread(XsdValidatorDelegate);
                        XsdValidatorThread.Start();
                    }
                }
                else
                {
                    Request.Update_Estado(currentRequest.Id, (int)InvoiceState.signError);
                }
            }
            Console.WriteLine(DateTime.Now.ToLongTimeString() + " There are no requests in step 2");
            dtRequests = Request.SelectStatus((int)InvoiceState.saved);
            if (dtRequests.Rows.Count > 0)
                Start();
        }
        public static void XsdValidator() {
            StepCServer step = new StepCServer();
            step.Start();
        }
    }
}
