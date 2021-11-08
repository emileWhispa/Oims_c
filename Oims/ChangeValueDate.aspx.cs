using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using bMobile.Shared;
using Bmat.Tools;
using System.Data;

public partial class ChangeValueDate : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        txtOldDate.Text = getPOSValueDate(conn, Session["POSId"].ToString());
        txtPOSName.Text = Session["POSName"].ToString();
    }

    private string getPOSValueDate(string con, string POSId)
    {
        string valueDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
        DataTable dtValue = Toolkit.dataDisplay(con, "SELECT convert(varchar(25), dateadd(hh,15,getdate()),102)");//We are adding +10 hours//"Select convert(varchar(25),ValueDate,102) from ValueDates where POSId=" + POSId);
        //SELECT convert(varchar(25), dateadd(hh,10,getdate()),102)
        if (dtValue != null && dtValue.Rows.Count == 1)
            valueDate = dtValue.Rows[0].ItemArray.GetValue(0).ToString().Replace(".", "-");
        return valueDate;
    }

    protected void btnChangeDate_Click(object sender, EventArgs e)
    {
        Toolkit.RunSqlCommand("update ValueDates set ValueDate='" + txtNewDate.Text + "', UserId="+Session["UserId"].ToString()+" where POSId=" + Session["POSId"].ToString(), conn);
        txtOldDate.Text = getPOSValueDate(conn, Session["POSId"].ToString());
    }
}