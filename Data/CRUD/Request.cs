
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
        public long Insert(string type, string value)
        {
            Comando = new SqlCommand();
            Comando.CommandText = "Insert_Marca";

            SqlParameter Type = new SqlParameter();
            Type.ParameterName = "@type";
            Type.Value = type;
            Type.Direction = ParameterDirection.Input;
            Type.SqlDbType = SqlDbType.Int;
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
        public long Update_Estado(long id, int status)
        {
            Comando = new SqlCommand();
            Comando.CommandText = "Update_Estado_Marca";

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
        public DataRow Select(long pId)
        {
            Comando = new SqlCommand();
            Comando.CommandText = "Select_Marca";

            SqlParameter Id = new SqlParameter();
            Id.ParameterName = "@Id";
            Id.Value = pId;
            Id.Direction = ParameterDirection.Input;
            Id.SqlDbType = SqlDbType.BigInt;
            Comando.Parameters.Add(Id);

            return ED.ProcedureExecuteReader(Comando).Rows[0];
        }
    }
}