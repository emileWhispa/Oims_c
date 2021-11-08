using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace bMobile.Shared
{
    public interface IDb
    {
        bool Connected { get; }
        string ConnError { get; }

        int ExecQuery(string query);

        DataTable QueryData(string query);

        int ExecQuery(string query, List<CommandParam> commandParams);

        DataTable QueryData(string query, List<CommandParam> commandParams);

        DbTransaction CreateTransaction(); 

        void CommitTransaction();

        void RollbackTransaction();

        List<CommandParam>  ExecPackage(string packageName, List<CommandParam> commandParams);
    }
   
}
