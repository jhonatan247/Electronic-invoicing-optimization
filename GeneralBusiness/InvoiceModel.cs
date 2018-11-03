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
        saved = 4,
        saveError = 5,
        signed = 6,
        signError = 7,
        xsdValidated = 8,
        xsdValidationError = 9,
        sended = 10,
        sendError = 11,
        connectionError = 12,
        connectionConfirmError = 13,
        notAccepted = 14,
        received = 15
    }
    public enum RequestType
    {
        PDF,
        XML
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

        public long Id { get; set; }
        public string Prefix { get; set; }
        public long Consecutive { get; set; }
        public string UBL { get; set; }
        public byte[] PdfFile { get; set; }
        public string xmlText { get; set; }
        public string DownloadLink { get; set; }
        public string pathPdfSource { get; set; }
        public string sourceXmlText { get; set; }
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
        public RequestType Type { get; set; }
        public DateTime CreatedDate { get; set; }

        public event ChangeStatusDelegate StatusChanged;

        public InvoiceModel(RequestType type, String value)
        {
            Type = type;
            if (type == RequestType.PDF)
                pathPdfSource = value;
            else
                sourceXmlText = value;

            state = InvoiceState.added;
            CreatedDate = DateTime.Now;
            CustomerSendState = CustomerSendState.noSended;
            CustomerAction = CustomerAction.noAction;
        }
        public InvoiceModel(long id, RequestType type, String value)
        {
            Id = id;
            Type = type;
            if (type == RequestType.PDF)
                pathPdfSource = value;
            else
                sourceXmlText = value;

            state = InvoiceState.added;
            CreatedDate = DateTime.Now;
            CustomerSendState = CustomerSendState.noSended;
            CustomerAction = CustomerAction.noAction;
        }
        public InvoiceModel(long id, String xml)
        {
            Id = id;
            xmlText = xml;
        }

        public override string ToString()
        {
            return $"<invoice><prefix>{Prefix}</prefix><consecutive>{Consecutive }</consecutive><ubl>{UBL}</ubl><invoice>";
        }
    }
}
