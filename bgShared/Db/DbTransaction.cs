using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace bMobile.Shared
{
    public class DbTransaction
    {
        IDb db;
        public DbTransaction(IDb db)
        {
            this.db = db;
            //this.db.BeginTransaction();
        }

       public bool Connected { get { return db.Connected; } }
       public string ConnError { get { return db.ConnError; } }

       public int ExecQuery(string query)
       {
           return db.ExecQuery(query);
       }      

        public int ExecQuery(string query, List<CommandParam> commandParams)
        {
            return db.ExecQuery(query,commandParams);
        }
        
        public bool Commit()
        {
            this.db.CommitTransaction();
            return true;
        }

        public bool Rollback()
        {
            this.db.RollbackTransaction();
            return true;
        }
    }
}
