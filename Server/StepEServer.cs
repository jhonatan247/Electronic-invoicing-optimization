using Data.CRUD;
using System;
using System.Data;
using GeneralBusiness;
using System.Threading;
using DSendInvoice;

namespace Server
{
    public class StepEServer
    {

        DataTable dtRequests;
        public StepEServer()
        {
            dtRequests = Request.SelectStatus((int)InvoiceState.sended, (int)InvoiceState.connectionConfirmError);
        }

        public void Start()
        {
            InvoiceModel currentRequest;
            ResponseModel currentResponse;
            for (int i = 0; i < dtRequests.Rows.Count; i++)
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + (dtRequests.Rows.Count - i) + " requests are in step 5");
                Thread.Sleep(500);
                String type = dtRequests.Rows[i][2].ToString();
                currentRequest = new InvoiceModel(Int64.Parse(dtRequests.Rows[i][0].ToString()), RequestType.PDF, dtRequests.Rows[i][10].ToString());
                currentResponse = EConfirmState.ConfirmStateProcess.ConfirmState(currentRequest);
                if (currentResponse.success)
                {
                    Request.Update_Estado(currentRequest.Id, (int)InvoiceState.received);
                }
                else
                {
                    if (currentResponse.error == Error.decline)
                    {
                        Request.Update_Estado(currentRequest.Id, (int)InvoiceState.notAccepted);
                    }
                    else
                    {
                        Request.Update_Estado(currentRequest.Id, (int)InvoiceState.connectionConfirmError);
                    }
                }
            }
            Console.WriteLine(DateTime.Now.ToLongTimeString() + " There are no requests in step 5");
        }
    }
}