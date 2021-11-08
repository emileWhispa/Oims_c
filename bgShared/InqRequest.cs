using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace bMobile.Shared
{
    public class InqRequest 
    {
        [Required(ErrorMessage = "NO USER KEY SUPPLIED!", AllowEmptyStrings = false)]
        public SecurityKey AppUser { get; set; }

        [Required(ErrorMessage = "ACCOUNT NO EMPTY!", AllowEmptyStrings = false)]
        public string AccountNo { get; set; }

        [Required(ErrorMessage = "ACCOUNT BRANCH EMPTY!", AllowEmptyStrings = false)]
        public string Branch { get; set; }

        [Required(ErrorMessage = "INQUIRY TYPE MISSING!", AllowEmptyStrings = false)]
        public InquiryTypes InquiryType { get; set; }

        public string MsgId { get; set; }

        public string Dets1 { get; set; }
        public string Dets2 { get; set; }
        public string Dets3 { get; set; }
        public string Dets4 { get; set; }

    }

    public enum InquiryTypes
    {
        AccountDetails = 0,
        Balance = 1,
        ForexRate = 4,
        ATMStatus = 5,
        ChequeBookStatus = 6,
        ChequeTransactionStatus = 7,
        GeneralInquiry = 8,
        Others = 100,
    }
}
