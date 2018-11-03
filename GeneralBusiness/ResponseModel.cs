using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralBusiness
{
    public enum Error {
        validate,
        save,
        sign,
        validateWithXSD,
        send,
        connection, 
        decline,
        none
    }
    public class ResponseModel
    {
        public bool success;
        public string CUFE;
        public Error error;
        
        public ResponseModel() {
            success = false;
            CUFE = "";
            error = Error.none;
        }

    }
}
