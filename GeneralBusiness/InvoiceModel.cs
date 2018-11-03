using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralBusiness
{
    public delegate void ChangeStatusDelegate(object sender);

    public enum InvoiceState
    {
        added = 1,
        validated = 2,
        validationError = 3,
        signed = 4,
        signError = 5,
        confirmed = 6,
        confirmError = 7,
        sended = 8,
        sendError = 9,
        received = 10
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
        private InvoiceState state;

        public string Prefix { get; set; }
        public long Consecutive { get; set; }
        public string UBL { get; set; }
        public byte[] PdfFile { get; set; }
        public DateTime CreatedDate { get; set; }
        public string DownloadLink { get; set; }
        public string XML { get; set; }
        public InvoiceState State {
            set {
                state = value;
                StatusChanged(this);
            }
            get {
                return state;
            }
        }
        public CustomerSendState CustomerSendState { get; set; }
        public CustomerAction CustomerAction { get; set; }

        public event ChangeStatusDelegate StatusChanged;

        public InvoiceModel()
        {
            state = InvoiceState.added;
            CreatedDate = DateTime.Now;
            CustomerSendState = CustomerSendState.noSended;
            CustomerAction = CustomerAction.noAction;
        }

        public override string ToString()
        {
            return $"<invoice><prefix>{Prefix}</prefix><consecutive>{Consecutive }</consecutive><ubl>{UBL}</ubl><invoice>";
        }
    }
}
