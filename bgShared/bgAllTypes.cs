using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bMobile.Shared
{

    public enum TransactionTypes
    {
        //----- Cheques
        OutCheques = 100,
        OutUnpaidCheques = 107,
        InCheques = 110,
        InUnpaidCheques = 117,
        //----- Direct Debits
        OutDirectDebits = 200,
        OutUnpaidDirectDebits = 207,
        InDirectDebits = 210,
        InUnpaidDirectDebits = 217,
        //----- Direct Credits
        OutDirectCredits = 300,
        OutUnpaidDirectCredits = 307,
        InDirectCredits = 310,
        InUnpaidDirectCredits = 317,
        //----- RTGS
        OutRTGS = 400,
        OutUnpaidRTGS = 407,
        InRTGS = 410,
        InUnpaidRTGS = 417,
    }

    public enum SocketMessageType { Post = 1, Enquiry = 2 }

    public enum InquiryMessageType { Balance = 1, Enquiry = 2 }
}
