using Data.CRUD;
using System;
using System.Data;
using GeneralBusiness;
using System.Threading;

namespace Server
{
    public class StepCServer
    {
        public static ThreadStart SendInvoiceDelegate = new ThreadStart(SendInvoice);
        public static Thread SendInvoiceThread = new Thread(SendInvoiceDelegate);

        DataTable dtRequests;
        public StepCServer()
        {
            dtRequests = Request.SelectStatus((int)InvoiceState.signed);
        }

        public void Start()
        {
            InvoiceModel currentRequest;
            ResponseModel currentResponse;
            for (int i = 0; i < dtRequests.Rows.Count; i++)
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + (dtRequests.Rows.Count - i) + " requests are in step 3");
                Thread.Sleep(500);
                String type = dtRequests.Rows[i][2].ToString();
                currentRequest = new InvoiceModel(Int64.Parse(dtRequests.Rows[i][0].ToString()), RequestType.PDF, dtRequests.Rows[i][10].ToString());
                currentResponse = CXsdValidator.XsdValidatorProcess.ValidateInvoice(currentRequest);
                if (currentResponse.success)
                {
                    Request.Update_Estado(currentRequest.Id, (int)InvoiceState.xsdValidated);
                    if (SendInvoiceThread.ThreadState != ThreadState.Running && SendInvoiceThread.ThreadState != ThreadState.WaitSleepJoin)
                    {
                        SendInvoiceThread = new Thread(SendInvoiceDelegate);
                        SendInvoiceThread.Start();
                    }
                }
                else
                {
                    Request.Update_Estado(currentRequest.Id, (int)InvoiceState.xsdValidationError);
                }
            }
            Console.WriteLine(DateTime.Now.ToLongTimeString() + " There are no requests in step 3");

            dtRequests = Request.SelectStatus((int)InvoiceState.signed);
            if (dtRequests.Rows.Count > 0)
                Start();
        }
        public static void SendInvoice()
        {
            StepDServer step = new StepDServer();
            step.Start();
        }
    }
}

