using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using bMobile.Shared;
namespace orion.ims
{
    public partial class Usergroup : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            loadData();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserGroup c = new UserGroup
            {
              GroupName = txtgroupname.Text,
                Description = txtdescription.Text
               
            };
            c = bl.saveUserGroup(c);

            btnCancel_Click(null, null);
            loadData();
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {
                    loadData();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hfId.Value = hf.Value;
                    int id = int.Parse(hfId.Value);
                    var x = bl.getUserGroup(id);
                    txtgroupname.Text = x.GroupName;
                    txtdescription.Text = x.Description;
                 }
                else if (e.CommandName == "del")
                {
                    loadData();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getUserGroup(id);
                    if (c != null)
                    {
                        bl.deleteUserGroup(id);
                        Util.ShowMsgBox(this, "User Group Details Deleted Suceesfully", "User", MsgBoxType.Info);
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
            loadData();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtdescription.Text = "";
            txtgroupname.Text = "";
        }

        private void loadData()
        {
            var c = bl.getUserGroups().ToList();
            gvData.DataSource = c;
            gvData.DataBind();
        }
    }
}