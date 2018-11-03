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

        public RequestModel(RequestType type, int id, String value) {
            this.type = type;
            this.id = id;
            this.value = value;
            invoice = new InvoiceModel();

        }
    }
}
