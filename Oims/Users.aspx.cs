using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.BL;
using bMobile.Shared;
using Bmat.Tools;
using orion.ims.DAL;
namespace orion.ims
{
    public partial class Users : System.Web.UI.Page
    {
        Bl bl;
       
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                var l = (from b in bl.getUserGroups().ToList() select new ListItem { Text = b.GroupName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlusergroup, l);
                l = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlpos, l);
                loadUsers();
                string brw = Request.Browser.Type;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Bmat.Tools.BTSecurity c = new BTSecurity();
            string str =c.GenerateSalt(txtusername.Text.Length);
            User usr = bl.getUser(txtusername.Text.Trim());
            if (usr != null)
            {

                usr.FullNames = txtfullname.Text.ToUpper();
                usr.UserGroupId = Convert.ToInt16(ddlusergroup.SelectedValue);
                usr.POSID = Convert.ToInt16(ddlpos.SelectedValue);
                usr.Email = txtemail.Text.Trim();
                usr.Phone = txtphone.Text.Trim();
                usr.Password = c.HashPassword(txtpassword.Text.Trim(), usr.Salt);
                usr.ChangePassword = true;
                usr.PassChangeDate = DateTime.Today;// DateTime.Today.AddDays(30);
                usr.StatusId = Convert.ToInt16(Statusid.Approved);
                usr.InvalidAttempts = 0;
                bl.saveUser(usr);
            }
            else
            {
                User u = new User
            {
                UserName = txtusername.Text.ToUpper(),
                FullNames = txtfullname.Text.ToUpper(),
                UserGroupId = Convert.ToInt16(ddlusergroup.SelectedValue),
                POSID = Convert.ToInt16(ddlpos.SelectedValue),
                Email = txtemail.Text.Trim(),
                Phone = txtphone.Text.Trim(),
                Salt = str,
                Password = c.HashPassword(txtpassword.Text.Trim(), str),
                ChangePassword = true,
                PassChangeDate = DateTime.Today,
                LastLogin = DateTime.Today,
                StatusId =Convert.ToInt16(Statusid.Active)
            };

                bl.saveUser(u);
            }
            loadUsers();
            btnCancel_Click(null, null);
            Util.ShowMsgBox(this, "User Details Saved Succesfully", "User", MsgBoxType.Info);
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {
                    loadUsers();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hfId.Value = hf.Value;
                    int id = int.Parse(hfId.Value);
                    var x = bl.getUser(id);
                    txtusername.Text = x.UserName;
                    txtphone.Text = x.Phone;
                    txtfullname.Text = x.FullNames;
                    txtpassword.Text = x.Password;
                    txtemail.Text = x.Email;
                    ddlpos.SelectedIndex = ddlpos.Items.IndexOf(ddlpos.Items.FindByValue(x.POSID.ToString()));
                    ddlusergroup.SelectedIndex = ddlusergroup.Items.IndexOf(ddlusergroup.Items.FindByValue(x.UserGroupId.ToString()));
                 }
                else if (e.CommandName == "del")
                {
                    loadUsers();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getUser(id);
                    if (c != null)
                    {
                        bl.deleteUser(id);
                        Util.ShowMsgBox(this, "User Details Deleted Suceesfully", "User", MsgBoxType.Info);
                    }
                }
            }
            catch (Exception ex)
            {

                string fx, caller;
                //Common.Errorlog(new StackTrace(), new StackFrame(), out fx, out caller);
                //Common.LogError(ex.Message, fx, caller);
            }
        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loadUsers();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtusername.Text = "";
            txtfullname.Text = "";
            txtemail.Text = "";
            txtpassword.Text = "";
            txtphone.Text = "";
            ddlpos.SelectedIndex = -1;
            ddlusergroup.SelectedIndex = -1;
        }
        private void loadUsers()
        {
            //gvData
            var data = bl.getUsers().ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }

        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}