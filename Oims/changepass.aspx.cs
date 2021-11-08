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


namespace orion.ims
{
    public partial class changepass : System.Web.UI.Page
    {
        Bl bl;
      string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);

            //string usernm = SessionData.UserName;
            //txtUserName.Text = usernm;
            if (!IsPostBack)
            {
            if (Request.QueryString.HasKeys())
                {
                    txtUserName.Text = Request.QueryString["username"];
                   var u = bl.getUser(txtUserName.Text);
                   if (u.StatusId == Convert.ToInt16(Statusid.Expired))
                   {
                        lblmsg.Text = "Password Expired";
                    }
                   else
                       lblmsg.Text = "Change Password";
                }
            Util.LogAudit(this.Page, "Accessed page", this.Title);
            }
        }
            
        protected void btnchange_Click(object sender, EventArgs e)
        {
            string response = "";
            User user = null;
            if (txtnewpass.Text.Trim() != txtconfirm.Text.Trim())
            {
                lblstatus.Text = "New Password does not Match";
                return;
            }
            Bmat.Tools.BTSecurity lgn = new BTSecurity();
             var u = bl.getUser(txtUserName.Text);
            if (u != null)
            {
                if (u.Password != lgn.HashPassword(txtPassword.Text.Trim(), u.Salt))
                {
                    lblstatus.Text = "Invalid Password";
                    return;
                }
                var h = bl.getpassbyUser(u.Id).ToList();//  
                foreach (var k in h)
                {
                    if (k.Password == lgn.HashPassword(txtnewpass.Text.Trim(), u.Salt))
                    {
                     lblstatus.Text="Password has already been Used previously";
                        return;
                    }
                }
                u.Password = lgn.HashPassword(txtnewpass.Text.Trim(), u.Salt);
                u.PassChangeDate = DateTime.Today.AddDays(30);
                u.ChangePassword = false;
                u.StatusId = Convert.ToInt16(Statusid.Active);
                bl.saveUser(u);
                Pass_Hist p = new Pass_Hist
                {

                    PassDate = DateTime.Today,
                    Password =u.Password,
                    UserId = u.Id
                };
                bl.savePassHist(p);
                Util.LogAudit(this.Page, "logged In to the systems", this.Title);
                Util.ShowMsgBox(this, "Password Changed Successfully", "Password Change", MsgBoxType.Info);
                Util.LogAudit(this.Page, "Changed password for user " + txtUserName.Text, this.Title);
               Response.Redirect("~/login.aspx");
            }
            else
            {
                lblstatus.Text = "Invalid User or Password";
                return;
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {

        }
    }
}