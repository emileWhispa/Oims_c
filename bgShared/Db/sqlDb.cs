using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace bMobile.Shared
{
    public class sqlDb : IDb
    {
        public SqlTransaction oTransaction;
        private SqlConnection sConn;
        private string err;
        private bool connected;
        private bool IsTransaction = false;

        public bool Connected
        {
            get { return connected; }
        }
        public string ConnError
        {
            get { return err; }
        }

        public sqlDb(string conString)
        {
            try
            {
                sConn = new SqlConnection(conString);
                sConn.Open();
                this.connected = true;
                this.err = "";
            }
            catch (Exception ex)
            {
                this.connected = false;
                this.err = ex.Message;
            }

        }

        public int ExecQuery(string query)
        {
            if (!connected)
            {
                this.err = "Connection closed!";
                return 0;
            }
            try
            {
                SqlCommand cmd = new SqlCommand(query, sConn);
                if (IsTransaction)
                    cmd.Transaction = oTransaction;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.err = ex.Message;
                return 0;
            }
        }

        public DataTable QueryData(string query)
        {
            if (!connected)
            {
                this.err = "Connection closed!";
                return null;
            }
            try
            {
                DataTable dt = new DataTable();
                SqlDataAdapter oda = new SqlDataAdapter(query, sConn);
                oda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                this.err = ex.Message;
                return null;
            }
        }

        public int ExecQuery(string query, List<CommandParam> Params)
        {
            if (!connected)
            {
                this.err = "Connection closed!";
                return 0;
            }
            try
            {
                SqlCommand cmd = new SqlCommand(query, sConn);
                AddParams(ref cmd, Params);
                if (IsTransaction)
                    cmd.Transaction = oTransaction;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.err = ex.Message;
                return 0;
            }
        }

        public DataTable QueryData(string query, List<CommandParam> Params)
        {
            if (!connected)
            {
                this.err = "Connection closed!";
                return null;
            }
            try
            {
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(query, sConn);
                AddParams(ref cmd, Params);
                SqlDataAdapter oda = new SqlDataAdapter(cmd);
                oda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                this.err = ex.Message;
                return null;
            }
        }

        public List<CommandParam> ExecPackage(string packageName, List<CommandParam> commandParams)
        {
            return null;
        }

        private void AddParams(ref SqlCommand cmd, List<CommandParam> Params)
        {
            foreach (var p in Params)
            {
                SqlParameter op = new SqlParameter()
                 {
                     ParameterName = p.ParamName,
                     Value = p.ParamValue,
                     DbType = p.DataType
                 };
                cmd.Parameters.Add(op);
            }
        }

        public DbTransaction CreateTransaction()
        {
            oTransaction = sConn.BeginTransaction(IsolationLevel.ReadCommitted);
            IsTransaction = true;
            return new DbTransaction(this);
        }

        public void CommitTransaction()
        {
            oTransaction.Commit();
        }

        public void RollbackTransaction()
        {
            oTransaction.Rollback();
        }

    }
}
