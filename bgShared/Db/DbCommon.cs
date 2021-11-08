using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace bMobile.Shared
{
    public enum CommandType { Query = 0, NonQuery = 1 }
    public enum DatabaseType { Oracle = 0, MsSql = 1 }

    public class CommandParam
    {
        public string ParamName { get; set; }
        public object ParamValue { get; set; }
        public DbType DataType { get; set; }
        public int Size { get; set; }
        public ParameterDirection Direction { get; set; }
    }

    public class dbCommamd
    {
        public string sqlStatement { get; set; }
        public CommandType CommandType { get; set; }
        public List<CommandParam> commandParams { get; set; }
        public Db Connection { get; set; }

        public dbCommamd(Db Db)
        {
            commandParams = new List<CommandParam>();
            CommandType = CommandType.Query;
            Connection = Db;
        }

        public DataTable QueryData()
        {
            return Connection.QueryData(this.sqlStatement, commandParams);
        }

        public int ExecuteStatement()
        {
            return Connection.ExecuteQuery(this.sqlStatement, commandParams);
        }

        
    }
    public class DbConnDetails
    {
        public string ConnString { get; set; }
        public string ServerName { get; set; }
        public string DbName { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public DatabaseType dbType { get; set; }
        public string Error { get; set; }

        public DbConnDetails()
        {
            this.ConnString = "";
            this.Error = "";
        }

        public bool CreateConnectionString()
        {
            this.Error = "";
            try
            {
                switch (this.dbType)
                {
                    case DatabaseType.Oracle:
                        ConnString = string.Format("data source ={0};User ID={1};Password ={2}", this.ServerName, this.UserId, this.Password);
                        break;
                    case DatabaseType.MsSql:
                        ConnString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};MultipleActiveResultSets=True", this.ServerName, this.DbName, this.UserId, this.Password);
                        break;
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Error = string.Format("Error! {0}", ex.Message);
                return false;
            }
        }
    }
}
