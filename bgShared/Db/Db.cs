using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace bMobile.Shared
{
    public class Db
    {
              
        private string _connString;
        private DatabaseType _dbType;
        private bool _connected = false;
        private string _connError = "";
        private IDb myDb;

        public bool Connected
        {
            get { return _connected; }
        }
        public string ConnError
        {
            get { return _connError; }
        }

        public Db(string conString, DatabaseType dbType)
        {
            _connString = conString;
            _dbType = dbType;
            if (dbType == DatabaseType.MsSql)
                myDb = new sqlDb(conString);
            else
                myDb = new oraDb(conString);

            this._connected = myDb.Connected;
            this._connError = myDb.ConnError;
        }

        public Db(DbConnDetails dbConn)
        {
            _connString = dbConn.ConnString;
            _dbType = dbConn.dbType;
            if (_dbType == DatabaseType.MsSql)
                myDb = new sqlDb(_connString);
            else
                myDb = new oraDb(_connString);

            this._connected = myDb.Connected;
            this._connError = myDb.ConnError;
        }

        public int ExecuteQuery(string query)
        {
            return myDb.ExecQuery(query); 
        }

        public int ExecuteQuery(string query, List<CommandParam> Params)
        {
            return myDb.ExecQuery(query,Params);
        }

        public List<CommandParam> ExecPackage(string packageName, List<CommandParam> packageParams)
        {
            return myDb.ExecPackage(packageName, packageParams);
        }
       

        public DataTable QueryData(string query)
        {
            return myDb.QueryData(query);
        }

        public DataTable QueryData(string query, List<CommandParam> Params)
        {
            return myDb.QueryData(query, Params);
        }

        public DbTransaction CreateTransaction()
        {
            return myDb.CreateTransaction();
        }
    }
}
