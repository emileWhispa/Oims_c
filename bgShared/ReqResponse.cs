using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace bMobile.Shared
{
    public class ReqResponse
    {
        public ReqRequest Request { get; set; }
        public ResposeTypes ResponseType { get; set; }
        public int ErrorNo { get; set; }
        public string Message { get; set; }
        public ACCStatement AccountStatement { get; set; }
    }

    public class ACCStatement
    {
        public string[] Data { get; set; }

        public string ToDataString()
        {
            if (Data == null)
                return "";
            //------ Format statement to defined format
            string data = "";
            foreach (var s in Data)
            {
                data += s + "~";
            }
            return data.Substring(0, data.Length - 1);
        }
    }
    public class PostResult
    {
        public bool Success { get; set; }
        public string Msg { get; set; }
        public string ExternalRef { get; set; }

    }
    //MiniStatement = 0,
    //FullStatement = 1,
    //ATMCard = 2,
    //ChequeBook = 3,
}
