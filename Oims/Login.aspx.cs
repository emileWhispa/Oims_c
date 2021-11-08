using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Configuration;
using orion.ims.DAL;
using orion.ims.BL;
using Bmat.Tools;
using System.Data;


namespace orion.ims
{
    public partial class Login : System.Web.UI.Page
    {
        Bl bl;

        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
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

        protected void btnLogin_Click(object sender, EventArgs e)
        {
         
            //License validation
            string expiryDate = "2021-12-31";
            string posValueDate = getPOSValueDate(conn, "1");
            if (DateTime.Parse(posValueDate) >= DateTime.Parse(expiryDate))
            {
                lblstatus.Text = "License expired! Contact the vendor";
                return;
                //Response.Redirect("~/Login.aspx");
            }
            //string response = "";
            //User user = null;
          
            var u = bl.getUser(txtUserName.Text);
            if (u != null)
            {
                if (CheckLogin(u) == false)
                {
                    return;
                }
                else
                {
                    //SessionData.UserId = u.Id;
                    Session["UserName"] = u.UserName;
                    Session["POSId"] = u.POSID;
                    //SessionData.POSName = u.Pos.POSName;
                    Session["UserGroupId"] = u.UserGroupId;
                    Session["Logged_in"] = "Yes";
                    Session["UserId"] = u.Id;
                    Session["POSName"] = u.Pos.POSName;
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                lblstatus.Text = "Invalid User or Password";
                return;
            }

          
        }

        private Boolean CheckLogin(User u)
        {
            Bmat.Tools.BTSecurity lgn = new BTSecurity();
            Boolean stat = false;
            var pass = bl.getSettingParam("PASS_EXPIRY_DAYS");
            var param = bl.getSettingParam("LOG_ATTEMPTS");

            if (u.StatusId == Convert.ToInt16(Statusid.Locked))
            {
                lblstatus.Text = "Account Locked.Contact Administrator";
                return stat = false;
            }
            if (u.PassChangeDate.AddDays(Convert.ToInt16(pass.ParamValue)) < DateTime.Today)
            {
                u.StatusId = Convert.ToInt16(Statusid.Expired);
                bl.saveUser(u);
            }
            if (u.InvalidAttempts > Convert.ToInt16(param.ParamValue))
            {
                u.StatusId = Convert.ToInt16(Statusid.Locked);
                bl.saveUser(u);
            }
           
            if (u.Password != lgn.HashPassword(txtpassword.Text.Trim(), u.Salt))
            {
                lblstatus.Text = "Invalid User or Password";
                u.InvalidAttempts = u.InvalidAttempts + 1;
                bl.saveUser(u);
                Util.LogAudit(this.Page, "Entered wrong login details", this.Title,u.Id);
                return stat = false;
            }
            else
            {
                if (u.ChangePassword == true || u.StatusId == Convert.ToInt16(Statusid.Expired))
                {
                    Response.Redirect("~/changepass.aspx?username=" + u.UserName + "");
                    stat = false;
                }
                else
                    u.InvalidAttempts = 0;
                    u.StatusId = Convert.ToInt16(Statusid.Active);
                    stat = true;
                    bl.saveUser(u);
                    SessionData.sessionduration = DateTime.Now;
                    Util.LogAudit(this.Page, "logged In to the systems", this.Title);
            }
            return stat;

        }
      protected void btncancel_Click(object sender, EventArgs e)
        {
            txtpassword.Text = "";
            txtUserName.Text = "";
        }
    }
}