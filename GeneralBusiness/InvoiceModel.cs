using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralBusiness
{
    public enum InvoiceState
    {
        created,
        received,
        signed,
        signError,
        validated,
        validationError,
        sended,
        sendError,
        confirmed,
        confirmError 
    }

    public enum CustomerSendState
    {
        noSended,
        sended,
        sendError
    }

    public enum CustomerAction
    {
        noAction,
        approved,
        rejected
    }

    public class InvoiceModel
    {
        public string prefix;
        public long consecutive;
        public string UBL;
        public byte[] pdfFile;
        public DateTime createddate;
        public InvoiceState state;
        public CustomerSendState customerSendState;
        public CustomerAction customerAction;

        public InvoiceModel()
        {
            state = InvoiceState.created;
            createddate = DateTime.Now;
            customerSendState = CustomerSendState.noSended;
            customerAction = CustomerAction.noAction;
        }

        public override string ToString()
        {
            return $"<invoice><prefix>{prefix}</prefix><consecutive>{consecutive }</consecutive><ubl>{UBL}</ubl><invoice>";
        }
    }
}
