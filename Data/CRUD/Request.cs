
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Data.DataConnection;

namespace Data.CRUD
{
    public class Request
    {
        private Connection ED = new Connection();
        
        SqlCommand Comando { get; set; }

        public long Id { get; set; }
        public int Status { get; set; }
        public string Type { get; set; }
        public string Prefix { get; set; }
        public string Consecutive { get; set; }
        public string UBL { get; set; }
        public string PathPdfSource { get; set; }
        public string DownloadLink { get; set; }
        public string SourceXmlText { get; set; }
        public string XmlText { get; set; }
        public DateTime CreatedTime { get; set; }
        
        public Request()
        {
        }

        public Request(long id, int status, string type, string prefix, string consecutive, string uBL, string pathPdfSource, string downloadLink, string sourceXmlText, string xmlText, DateTime createdTime)
        {
            Id = id;
            Status = status;
            Type = type;
            Prefix = prefix;
            Consecutive = consecutive;
            UBL = uBL;
            PathPdfSource = pathPdfSource;
            DownloadLink = downloadLink;
            SourceXmlText = sourceXmlText;
            XmlText = xmlText;
            CreatedTime = createdTime;
        }
        public static  long Insert(string type, string value)
        {
            Connection ED = new Connection();
            SqlCommand Comando = new SqlCommand();
            Comando.CommandText = "insert_request";

            SqlParameter Type = new SqlParameter();
            Type.ParameterName = "@type";
            Type.Value = type;
            Type.Direction = ParameterDirection.Input;
            Type.SqlDbType = SqlDbType.VarChar;
            Type.Size = 30;
            Comando.Parameters.Add(Type);

            SqlParameter Value = new SqlParameter();
            Value.ParameterName = "@value";
            Value.Value = value;
            Value.Direction = ParameterDirection.Input;
            Value.SqlDbType = SqlDbType.VarChar;
            Value.Size = 200;
            Comando.Parameters.Add(Value);

            return ED.ProcedureExecuteNonQuery(Comando);
        }
        public static long Update_Estado(long id, int status)
        {
            Connection ED = new Connection();
            SqlCommand Comando = new SqlCommand();
            Comando = new SqlCommand();
            Comando.CommandText = "update_request_state";

            SqlParameter Id = new SqlParameter();
            Id.ParameterName = "@Id";
            Id.Value = id;
            Id.Direction = ParameterDirection.Input;
            Id.SqlDbType = SqlDbType.BigInt;
            Comando.Parameters.Add(Id);

            SqlParameter Status = new SqlParameter();
            Status.ParameterName = "@status";
            Status.Value = status;
            Status.Direction = ParameterDirection.Input;
            Status.SqlDbType = SqlDbType.Int;
            Comando.Parameters.Add(Status);

            return ED.ProcedureExecuteNonQuery(Comando);
        }
        public static long Update_XML(long id)
        {
            Connection ED = new Connection();
            SqlCommand Comando = new SqlCommand();
            Comando = new SqlCommand();
            Comando.CommandText = "update_request_xml";

            SqlParameter Id = new SqlParameter();
            Id.ParameterName = "@Id";
            Id.Value = id;
            Id.Direction = ParameterDirection.Input;
            Id.SqlDbType = SqlDbType.BigInt;
            Comando.Parameters.Add(Id);

            return ED.ProcedureExecuteNonQuery(Comando);
        }
        public static long Update_PDF(long id)
        {
            Connection ED = new Connection();
            SqlCommand Comando = new SqlCommand();
            Comando = new SqlCommand();
            Comando.CommandText = "update_request_pdf";

            SqlParameter Id = new SqlParameter();
            Id.ParameterName = "@Id";
            Id.Value = id;
            Id.Direction = ParameterDirection.Input;
            Id.SqlDbType = SqlDbType.BigInt;
            Comando.Parameters.Add(Id);

            return ED.ProcedureExecuteNonQuery(Comando);
        }
        public static DataTable Select()
        {
            Connection ED = new Connection();
            SqlCommand Comando = new SqlCommand();
            Comando.CommandText = "select_request";

            return ED.ProcedureExecuteReader(Comando);
        }
        public static DataTable SelectInterface()
        {
            Connection ED = new Connection();
            SqlCommand Comando = new SqlCommand();
            Comando.CommandText = "select_request_interface";

            return ED.ProcedureExecuteReader(Comando);
        }
        public static DataTable ClearData()
        {
            Connection ED = new Connection();
            SqlCommand Comando = new SqlCommand();
            Comando.CommandText = "clear_data";

            return ED.ProcedureExecuteReader(Comando);
        }
        public static DataTable SelectValidator(int validator)
        {
            Connection ED = new Connection();
            SqlCommand Comando = new SqlCommand();
            Comando.CommandText = "select_request_validator";

            SqlParameter Validator = new SqlParameter();
            Validator.ParameterName = "@validator";
            Validator.Value = validator;
            Validator.Direction = ParameterDirection.Input;
            Validator.SqlDbType = SqlDbType.BigInt;
            Comando.Parameters.Add(Validator);

            return ED.ProcedureExecuteReader(Comando);
        }
        public static DataTable SelectStatus(int status)
        {
            Connection ED = new Connection();
            SqlCommand Comando = new SqlCommand();
            Comando.CommandText = "select_request_status";

            SqlParameter Status = new SqlParameter();
            Status.ParameterName = "@status";
            Status.Value = status;
            Status.Direction = ParameterDirection.Input;
            Status.SqlDbType = SqlDbType.BigInt;
            Comando.Parameters.Add(Status);

            return ED.ProcedureExecuteReader(Comando);
        }
        public static DataTable SelectStatus(int status1, int status2)
        {
            Connection ED = new Connection();
            SqlCommand Comando = new SqlCommand();
            Comando.CommandText = "select_request_two_status";

            SqlParameter Status1 = new SqlParameter();
            Status1.ParameterName = "@status1";
            Status1.Value = status1;
            Status1.Direction = ParameterDirection.Input;
            Status1.SqlDbType = SqlDbType.BigInt;
            Comando.Parameters.Add(Status1);

            SqlParameter Status2 = new SqlParameter();
            Status2.ParameterName = "@status2";
            Status2.Value = status2;
            Status2.Direction = ParameterDirection.Input;
            Status2.SqlDbType = SqlDbType.BigInt;
            Comando.Parameters.Add(Status2);

            return ED.ProcedureExecuteReader(Comando);
        }
    }
}