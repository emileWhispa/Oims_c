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
    /// Summary description for Post
    /// </summary>
    [WebService(Namespace = "http://www.bmatsolutions.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class Post : System.Web.Services.WebService
    {
        CBSInterface cbsInt;
        public Post()
        {
            cbsInt = new CBSInterface(Util.DbConnectionString(),Util.ExtDbConnectionString(),Util.CBSType);
        }

        [WebMethod]
        public PostResponse PostTransaction(PostRequest req)
        {
            return cbsInt.Post(req);
        }
    }
}
