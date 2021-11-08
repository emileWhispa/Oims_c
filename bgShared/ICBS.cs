using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace bMobile.Shared
{
    public interface ICBS
    {
        #region CLEARING

        #endregion

        #region INQUIRY
        InqResponse Inquire(InqRequest inquiry);
        #endregion

        #region REQUESTS
        ReqResponse Request(ReqRequest request);
        #endregion

        #region POST
        PostResponse PostTransactions(PostRequest request);
        #endregion

      
    }
}
