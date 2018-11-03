using Data.CRUD;
using System;
using System.Data;
using GeneralBusiness;
using System.Threading;

namespace Server
{
    public class StepDServer
    {
        public static ThreadStart ConfirmStateDelegate = new ThreadStart(ConfirmState);
        public static Thread ConfirmStateThread = new Thread(ConfirmStateDelegate);

        DataTable dtRequests;
        public StepDServer()
        {
            dtRequests = Request.SelectStatus((int)InvoiceState.xsdValidated, (int)InvoiceState.connectionError);
        }

        public void Start()
        {
            InvoiceModel currentRequest;
            ResponseModel currentResponse;
            for (int i = 0; i < dtRequests.Rows.Count; i++)
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + (dtRequests.Rows.Count - i) + " requests are in step 4");
                Thread.Sleep(500);
                String type = dtRequests.Rows[i][2].ToString();
                currentRequest = new InvoiceModel(Int64.Parse(dtRequests.Rows[i][0].ToString()), RequestType.PDF, dtRequests.Rows[i][10].ToString());
                currentResponse = DSendInvoice.SendServiceProcess.SendInvoice(currentRequest);
                if (currentResponse.success)
                {
                    Request.Update_Estado(currentRequest.Id, (int)InvoiceState.sended);
                    if (ConfirmStateThread.ThreadState != ThreadState.Running && ConfirmStateThread.ThreadState != ThreadState.WaitSleepJoin)
                    {
                        ConfirmStateThread = new Thread(ConfirmStateDelegate);
                        ConfirmStateThread.Start();
                    }
                }
                else
                {
                    if (currentResponse.error == Error.send)
                    {
                        Request.Update_Estado(currentRequest.Id, (int)InvoiceState.sendError);
                    }
                    else
                    {
                        Request.Update_Estado(currentRequest.Id, (int)InvoiceState.connectionError);
                    }
                }
            }
            Console.WriteLine(DateTime.Now.ToLongTimeString() + " There are no requests in step 4");
            dtRequests = Request.SelectStatus((int)InvoiceState.xsdValidated, (int)InvoiceState.connectionError);
            if (dtRequests.Rows.Count > 0)
                Start();
        }
        public static void ConfirmState()
        {
            StepEServer step = new StepEServer();
            step.Start();
        }
    }
}