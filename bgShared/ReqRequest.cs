using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace bMobile.Shared
{
    public class ReqRequest
    {
        [Required(ErrorMessage = "NO USER KEY SUPPLIED!", AllowEmptyStrings = false)]
        public SecurityKey AppUser { get; set; }

        [Required(ErrorMessage = "ACCOUNT NUMBER EMPTY!", AllowEmptyStrings = false)]
        public string AccountNo { get; set; }

        [Required(ErrorMessage = "ACCOUNT BRANCH EMPTY!", AllowEmptyStrings = false)]
        public string Branch { get; set; }

        [Required(ErrorMessage = "REQUEST TYPE MISSING!", AllowEmptyStrings = false)]
        public RequestTypes RequestType { get; set; }

        public string MsgId { get; set; }

        public string Dets1 { get; set; }
        public string Dets2 { get; set; }
        public string Dets3 { get; set; }
        public string Dets4 { get; set; }
    }

    public enum RequestTypes
    {
        MiniStatement = 0,
        FullStatement = 1,
        ATMCard = 2,
        ChequeBook = 3,
        Others = 100
    }
}
