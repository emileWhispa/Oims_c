using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bMobile.Shared
{
    public class clrPostRequest
    {
        public TransactionTypes TransType { get; set; }
        public List<PostTransactionReq> Transactions { get; set; }
        public clrPostRequest()
        {
            this.Transactions = new List<PostTransactionReq>();
        }
    }

    public class PostTransactionReq
    {
        public string BatchNo { get; set; }
        public string TransRef { get; set; }
        public decimal Amount { get; set; }
        public string SerialNo { get; set; }
        public string AccountNo { get; set; }
        public string Narration { get; set; }
        public string BankCode { get; set; }
        public string BranchCode { get; set; }
        public string TransCode { get; set; }
        public DateTime ValueDate { get; set; }
        public string UserName { get; set; }
        public string Dets1 { get; set; }
        public string Dets2 { get; set; }
        public string Dets3 { get; set; }
        public string Dets4 { get; set; }
        public string Dets5 { get; set; }
    }
}
