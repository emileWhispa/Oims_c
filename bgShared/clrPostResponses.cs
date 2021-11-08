using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bMobile.Shared
{

    public class clrPostResponse
    {
        public ResposeTypes ResponseType { get; set; }
        public string Message { get; set; }
        public List<PostTransactionResp> TransResponses { get; set; }
        public clrPostResponse()
        {
            this.TransResponses = new List<PostTransactionResp>();
        }
    }

    public class PostTransactionResp
    {
        public string TransRef { get; set; }
        public string ExternalRef { get; set; }
        public ResposeTypes ResponseType { get; set; }
        public string Message { get; set; }
    }
}
