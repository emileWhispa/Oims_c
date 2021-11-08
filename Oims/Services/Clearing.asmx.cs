using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using bMobile.Shared;

    /// <summary>
    /// Summary description for Clearing
    /// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class Clearing : System.Web.Services.WebService
{

    [WebMethod]
    public clrPostResponse TestPost()
    {
        return new clrPostResponse()
        {
            ResponseType = ResposeTypes.OK,
            Message = "",
            TransResponses = new List<PostTransactionResp>()
                {
                    new PostTransactionResp{
                        ResponseType = ResposeTypes.OK,
                        ExternalRef ="FT0986509",
                        Message="",
                        TransRef="6767543009"
                    }
                }
        };
    }

    [WebMethod]
    public clrPostResponse PostTransactions(clrPostRequest transactions)
    {
        return new clrPostResponse();
    }
}

