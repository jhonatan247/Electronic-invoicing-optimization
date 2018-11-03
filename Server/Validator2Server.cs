using System;
using GeneralBusiness;
using Data.CRUD;
using System.Data;
using System.Threading;

namespace Server
{
    public class Validator2Server
    {

        DataTable dtRequests;
        public Validator2Server()
        {
            dtRequests = Request.SelectValidator(2);
            if (Validator1Server.SignInvoiceThread.ThreadState != ThreadState.Running && Validator1Server.SignInvoiceThread.ThreadState != ThreadState.WaitSleepJoin)
            {
                Validator1Server.SignInvoiceThread = new Thread(Validator1Server.SignInvoiceDelegate);
                Validator1Server.SignInvoiceThread.Start();
            }
        }

        public void Start()
        {
            InvoiceModel currentRequest;
            ResponseModel currentResponse;
            for (int i = 0; i < dtRequests.Rows.Count; i++)
            {
                Console.WriteLine(DateTime.Now.ToLongTimeString() + " " + (dtRequests.Rows.Count - i) + " requests are in step 1, in the validator 2");
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
                    if (Validator1Server.SignInvoiceThread.ThreadState != ThreadState.Running && Validator1Server.SignInvoiceThread.ThreadState != ThreadState.WaitSleepJoin)
                    {
                        Validator1Server.SignInvoiceThread = new Thread(Validator1Server.SignInvoiceDelegate);
                        try
                        {
                            Validator1Server.SignInvoiceThread.Start();
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
            Console.WriteLine(DateTime.Now.ToLongTimeString() + " The validator 2 has no requests");
        }

    }
}