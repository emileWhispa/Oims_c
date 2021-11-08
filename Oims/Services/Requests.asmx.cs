using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using bMobile.Shared;
using bMobile.BL;

namespace bMobile.Services
{
    /// <summary>
    /// Summary description for Requests
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Requests : System.Web.Services.WebService
    {
        CBSInterface cbsInt;
        public Requests()
        {
            cbsInt = new CBSInterface(Util.DbConnectionString(), Util.ExtDbConnectionString(), Util.CBSType);
        }

        [WebMethod]
        public string TestRequest()
        {
            return "bMobile: Test Request = OK";
        }

        [WebMethod]
        public ReqResponse Request(ReqRequest request)
        {
            return cbsInt.Request(request);
        }
    }
}
