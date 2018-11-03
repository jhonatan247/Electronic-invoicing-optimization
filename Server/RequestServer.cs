using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralBusiness;
using Data.CRUD;
using System.Threading;

namespace Server
{
    public class RequestServer
    {

        static ThreadStart Validator1Delegate = new ThreadStart(validator1);
        static ThreadStart Validator2Delegate = new ThreadStart(validator2);
        static ThreadStart Validator3Delegate = new ThreadStart(validator3);

        Thread Validator1Thread = new Thread(Validator1Delegate);
        Thread Validator2Thread = new Thread(Validator2Delegate);
        Thread Validator3Thread = new Thread(Validator3Delegate);
        public RequestServer()
        {
            Validator1Thread.Start();
            Validator2Thread.Start();
            Validator3Thread.Start();
        }
        public void addRequests(List<InvoiceModel> newRequests)
        {
            foreach (InvoiceModel request in newRequests)
            {
                if (request.Type == RequestType.PDF)
                    Request.Insert("PDF", request.pathPdfSource);
                else
                    Request.Insert("XML", request.sourceXmlText);
                Complete();
            }
            
        }
        public void Complete() {
            if (Validator1Thread.ThreadState != ThreadState.Running && Validator1Thread.ThreadState != ThreadState.WaitSleepJoin)
            {
                Validator1Thread = new Thread(Validator1Delegate);
                Validator1Thread.Start();
            }
            if (Validator2Thread.ThreadState != ThreadState.Running && Validator2Thread.ThreadState != ThreadState.WaitSleepJoin)
            {
                Validator2Thread = new Thread(Validator2Delegate);
                Validator2Thread.Start();
            }
            if (Validator3Thread.ThreadState != ThreadState.Running && Validator3Thread.ThreadState != ThreadState.WaitSleepJoin)
            {
                Validator3Thread = new Thread(Validator3Delegate);
                Validator3Thread.Start();
            }
        }
        static void validator1()
        {
            Validator1Server vs = new Validator1Server();
            vs.Start();
        }
        static void validator2()
        {
            Validator2Server vs = new Validator2Server();
            vs.Start();
        }
        static void validator3()
        {
            Validator3Server vs = new Validator3Server();
            vs.Start();
        }
    }
}
