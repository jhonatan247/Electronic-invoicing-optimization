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
        public RequestType Type { get; set; }
        public String Value { get; set; }
        public InvoiceModel Invoice { get; set; }
        public String Status {
            get {
                switch (Invoice.State) {
                    case InvoiceState.confirmed: return "Confirmed";
                    case InvoiceState.confirmError: return "Confirm error";
                    case InvoiceState.added: return "Added";
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
        
        public event ChangeStatusDelegate StatusChanged;

        public RequestModel(RequestType type, int requestIndex, int typeIndex, String value) {
            Type = type;
            RequestIndex = requestIndex;
            TypeIndex = typeIndex;
            Value = value;
            Invoice = new InvoiceModel();
            Invoice.StatusChanged += this.invoice_StatusChanged;
        }

        private void invoice_StatusChanged(object sender) {
            StatusChanged(this);
        }
    }
}
