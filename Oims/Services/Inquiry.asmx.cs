using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using bMobile.Shared;
using bMobile.BL;


namespace bMobile
{
    /// <summary>
    /// Summary description for Inquiry
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Inquiry : System.Web.Services.WebService
    {
        CBSInterface cbsInt;
        public Inquiry()
        {
            cbsInt = new CBSInterface(Util.DbConnectionString(),Util.ExtDbConnectionString(),Util.CBSType);
        }

        [WebMethod]
        public string TestInquiry(string testString)
        {
            return "bMobile: Test Inquiry = OK";
        }

        [WebMethod]
        public InqResponse Inquire(InqRequest request)
        {
            return cbsInt.Inquiry(request);
        }
        
    }
}
