using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace bMobile.Shared
{
    public class PostRequest
    {
        public PostReqHeader Header { get; set; }
        public List<PostReqTrans> Transactions { get; set; }
        public PostRequest()
        {
            this.Transactions = new List<PostReqTrans>();
        }
    }

    public class PostReqHeader
    {
        [Required(ErrorMessage = "HDR:NO USER KEY SUPPLIED!", AllowEmptyStrings = false)]
        public SecurityKey AppUser { get; set; }

        [Required(ErrorMessage = "HDR:BATCH NO EMPTY!", AllowEmptyStrings = false)]
        public string BatchNo { get; set; }

        [Required(ErrorMessage = "HDR:BRANCH CODE EMPTY!", AllowEmptyStrings = false)]
        public string BranchCode { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "HDR:TRANSACTION CODE EMPTY!", AllowEmptyStrings = false)]
        public string TransCode { get; set; }

        public DateTime PostDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "HDR:ITEM COUNT MUST BE GREATER THAN 1!")]
        public int ItemCount { get; set; }

        [Range(0.5, double.MaxValue, ErrorMessage = "HDR:SUM AMOUNT MUST BE GREATER THAN 0.5!")]
        public decimal ItemSum { get; set; }

        [Required(ErrorMessage = "HDR:TXN TYPE MISSING!", AllowEmptyStrings = false)]
        public TransactionTypes TransactionType { get; set; }

        public string Remarks { get; set; }

        public string MsgId { get; set; }
    }

    public class PostReqTrans
    {
        [Required(ErrorMessage = "TXN:TXN REF EMPTY!", AllowEmptyStrings = false)]
        public string TransRef { get; set; }

         [Range(0.5, double.MaxValue, ErrorMessage = "TXN:TXN AMOUNT MUST BE GREATER THAN 0.5!")]
        public decimal Amount { get; set; }

        public decimal FX_Rate { get; set; }

        public bool IsCredit { get; set; }

        public string SerialNo { get; set; }

        [Required(ErrorMessage = "TXN:ACCOUNT NO EMPTY!", AllowEmptyStrings = false)]
        public string DrAccountNo { get; set; }

        [Required(ErrorMessage = "TXN:CURRENCY EMPTY!", AllowEmptyStrings = false)]
        public string Currency { get; set; }

        [Required(ErrorMessage = "TXN:ACCOUNT NO EMPTY!", AllowEmptyStrings = false)]
        public string CrAccountNo { get; set; }

        public string Narration { get; set; }

        public string BankCode { get; set; }

        public string BranchCode { get; set; }

        [Required(ErrorMessage = "TXN:TXN CODE EMPTY!", AllowEmptyStrings = false)]
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
