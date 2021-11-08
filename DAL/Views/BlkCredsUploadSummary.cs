using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace orion.ims.DAL
{
    public class BlkCredsUploadSummary
    {
        public int TxnCount { get; set; }
        public decimal Amount { get; set; }
        public string DrAccount { get; set; }
        public string BranchCode { get; set; }
        public string BankCode { get; set; }
        public string Currency { get; set; }
        public string Remarks { get; set; }
        public int InHouse { get; set; }
    }
}
