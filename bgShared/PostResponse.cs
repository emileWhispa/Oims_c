using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bMobile.Shared
{
    public class PostResponse
    {
        public PostRespHeader Header { get; set; }
        public List<PostRespTrans> Transactions { get; set; }
        public PostResponse()
        {
            this.Header = new PostRespHeader();
            this.Transactions = new List<PostRespTrans>();
        }
    }

    public class PostRespHeader
    {
        public string BatchNo { get; set; }
        public bool Success { get; set; }
        public int ErrorNo { get; set; }
        public string ErrMsg { get; set; }
        public PostRespHeader()
        {
            this.ErrMsg = "";
        }
    }

    public class PostRespTrans
    {
        public bool Success { get; set; }
        public string ErrMsg { get; set; }
        public string ExternalRef { get; set; }
        public PostReqTrans Transaction { get; set; }
    }

    public class TransactionDet
    {
        public string AccountCr { get; set; }
        public string AccountDr { get; set; }
        public string CrBr { get; set; }
        public string DrBr { get; set; }
        public string CrDr { get; set; }
        public Decimal Amount { get; set; }
        public string TransRef { get; set; }
        public string Addtxt { get; set; }
        public DateTime TransDate { get; set; }
        public string Txncode { get; set; }
        public string Currency { get; set; }
    }

    public class AccountStatus
    {
        public string Accountstatus { get; set; }
        public string AccountBranch { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public Boolean isvalid { get; set; }
    }
    public class acctst
    {
        public string Acc { get; set; }
        public string Br { get; set; }
    }

    public class ministatement
    {
        public string Acc { get; set; }
        public string Br { get; set; }
        public int Txncnt { get; set; }
   }
}
