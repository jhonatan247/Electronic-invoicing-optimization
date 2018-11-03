using System;
using System.Collections.Generic;
using System.Text;

namespace GeneralBusiness
{
    public class ResponseModel
    {
        public bool success;
        public string CUFE;
        public List<object> errorList;

        public ResponseModel() {
            success = false;
            CUFE = "";
            errorList = new List<object>();
        }

    }
}
