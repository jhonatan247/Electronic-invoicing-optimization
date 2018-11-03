using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralBusiness
{
    public enum RequestType {
        PDF,
        XML
    }
    public class RequestModel
    {
        public RequestType type;
        public int id;
        public String value;
        public InvoiceModel invoice;
        public String status {
            get {
                switch (invoice.state) {
                    case InvoiceState.confirmed: return "Confirmed";
                    case InvoiceState.confirmError: return "Confirm error";
                    case InvoiceState.created: return "Created";
                    case InvoiceState.received: return "Received";
                    case InvoiceState.sended: return "Sended";
                    case InvoiceState.sendError: return "Send error";
                    case InvoiceState.signed: return "Signed";
                    case InvoiceState.signError: return "Sign error";
                    case InvoiceState.validated: return "Validated";
                    case InvoiceState.validationError: return "Validation error";
                }
                return "";
            }
        }

        public RequestModel(RequestType type, int id, String value) {
            this.type = type;
            this.id = id;
            this.value = value;
            invoice = new InvoiceModel();

        }
    }
}
