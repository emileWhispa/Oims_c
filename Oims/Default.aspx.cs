using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace orion.ims
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]




        protected void Button1_Click(object sender, EventArgs e)
        {
            System.IO.File.ReadAllText(@"d:\xx.txt");
        }

        [System.Web.Services.WebMethod]
        public static string GetCurrentTime(string name)
        {
            return "Hello " + name + Environment.NewLine + "The Current Time is: "
                + DateTime.Now.ToString();
        }

        [System.Web.Services.WebMethod]
        public static string GetConnStatus()
        {
            return DPage.SwitchConnStatus();
        }

        [System.Web.Services.WebMethod]
        public static string GetPostingMode()
        {
            return DPage.UploadMode();
        }

        [System.Web.Services.WebMethod]
        public static string GetTopTenTxns()
        {
            return DPage.GetLastTenTxns();
        }
    }
}