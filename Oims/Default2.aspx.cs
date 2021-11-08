using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SMSAPI.ksms x = new SMSAPI.ksms();
        string bal="";
        string status="";
        x.ksend("jecjank", "5t9b2f", "TEST", "TEST MESSAGE", "25775902228", out bal, out status);
        Label1.Text = "Status: " + status + " - Balance: " + bal;
    }
}