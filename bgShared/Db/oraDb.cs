using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace bMobile.Shared
{

    public class oraDb : IDb
    {
        public OracleTransaction oTransaction;
        private OracleConnection oConn;
        private string err = "";
        private bool connected = false;
        private bool IsTransaction = false;

        public bool Connected
        {
            get { return connected; }
        }
        public string ConnError
        {
            get { return err; }
        }

        public oraDb(string conString)
        {
            try
            {
                oConn = new OracleConnection(conString);
                oConn.Open();
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
                return -1;
            }
            try
            {
                OracleCommand cmd = new OracleCommand(query, oConn);
                if (IsTransaction)
                    cmd.Transaction = oTransaction;
                return cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                this.err = ex.Message;
                return -1;
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
                OracleDataAdapter oda = new OracleDataAdapter(query, oConn);
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
                return -1;
            }
            try
            {
                OracleCommand cmd = new OracleCommand(query, oConn);
                AddParams(ref cmd, Params);
                if (IsTransaction)
                    cmd.Transaction = oTransaction;
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                this.err = ex.Message;
                return -1;
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
                OracleCommand cmd = new OracleCommand(query, oConn);
                AddParams(ref cmd, Params);
                OracleDataAdapter oda = new OracleDataAdapter(cmd);
                oda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                this.err = ex.Message;
                return null;
            }
        }

        public List<CommandParam> ExecPackage(string packageName, List<CommandParam> packageParams)
        {
            if (!connected)
            {
                this.err = "Connection closed!";
                return null;
            }
            try
            {
                //----- Create Command
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = oConn;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = packageName;// "cbklprod.ifpks_atm_accounting.fn_post_entry"
                //-----Create and add paramemters
                AddPackageParams(ref cmd, packageParams);
                cmd.Parameters.Add(new OracleParameter("p_trn_ref",OracleType.VarChar,30)).Direction=ParameterDirection.Output;
                 cmd.Parameters.Add(new OracleParameter("p_errcode",OracleType.VarChar,30)).Direction=ParameterDirection.Output;
                 cmd.Parameters.Add(new OracleParameter("p_errparam",OracleType.VarChar,30)).Direction=ParameterDirection.Output;
               
                if (IsTransaction)
                    cmd.Transaction = oTransaction;
                //-----Execute Command
                cmd.ExecuteNonQuery();
                //----Get parameters
                var returnParams = cmd.Parameters;
                //-----Return parameters
                return ReturnParams(returnParams);
            }
            catch (Exception ex)
            {
                this.err = ex.Message;
                return null;
            }
        }

        private void AddParams(ref OracleCommand cmd, List<CommandParam> Params)
        {
            foreach (var p in Params)
            {
                OracleParameter op = new OracleParameter()
                {
                    ParameterName = p.ParamName,
                    Value = p.ParamValue,
                    DbType = p.DataType,
                    Size = p.Size,
                    Direction = p.Direction
                };
                cmd.Parameters.Add(op);
            }
        }

        private void AddPackageParams(ref OracleCommand cmd, List<CommandParam> Params)
        {
            foreach (var p in Params)
            {
                OracleParameter op = new OracleParameter()
                {
                    ParameterName = p.ParamName,
                    Value = p.ParamValue,
                    DbType = p.DataType
                };
                cmd.Parameters.Add(op);
            }
        }

        private List<CommandParam> ReturnParams(OracleParameterCollection ps)
        {
            List<CommandParam> rp = new List<CommandParam>();
            foreach (var para in ps)
            {
                var p = (OracleParameter)para;
                rp.Add(new CommandParam
                {
                    DataType=p.DbType,
                    Direction=p.Direction,
                    ParamName=p.ParameterName,
                    ParamValue=p.Value,
                    Size=p.Size
                });
            }
            return rp;
        }

        public DbTransaction CreateTransaction()
        {
            oTransaction = oConn.BeginTransaction(IsolationLevel.ReadCommitted);
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
