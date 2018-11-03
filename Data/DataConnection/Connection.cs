using System;
using System.Data;
using System.Data.SqlClient;

namespace Data.DataConnection
{
    public class Connection
    {
        static string CONSTConnectionString = "Server=DESKTOP-HRFQUMI\\SQLEXPRESS;DataBase=siigo;Integrated Security=SSPI";

        public SqlConnection Connect { get; set; }
        public SqlCommand Command { get; set; }

        public Connection()
        {
            Connect = new SqlConnection();
            Command = new SqlCommand();
            Open();
        }
        private void Open()
        {
            if (Connect.State == ConnectionState.Open)
            {
                return;
            }
            Connect.ConnectionString = CONSTConnectionString;
            try
            {
                Connect.Open();
            }
            catch (Exception e)
            {
                throw;
            }
        }
        private void Close()
        {
            if (Connect.State == ConnectionState.Closed)
            {
                return;
            }
            try
            {
                Connect.Close();
            }
            catch
            {
                throw;
            }
        }
        public Int64 ProcedureExecuteNonQuery(SqlCommand pComando)
        {
            Int64 RowsAfected;
            Open();
            pComando.Connection = Connect;
            pComando.CommandType = CommandType.StoredProcedure;
            try
            {
                RowsAfected = pComando.ExecuteNonQuery();
                Close();
                return RowsAfected;
            }
            catch
            {
                Close();
                throw;
            }
        }
        public DataTable ProcedureExecuteReader(SqlCommand pComando)
        {
            pComando.Connection = Connect;
            pComando.CommandType = CommandType.StoredProcedure;
            try
            {
                DataTable ReturnTable = new DataTable();
                SqlDataAdapter DataAdapter = new SqlDataAdapter(pComando);
                DataAdapter.Fill(ReturnTable);
                Close();
                return ReturnTable;
            }
            catch
            {
                Close();
                throw;
            }
        }
    }
}