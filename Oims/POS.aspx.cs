using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Bmat.Tools;
using orion.ims.DAL;
using orion.ims.BL;
using System.Diagnostics;

namespace orion.ims
{

    public partial class POS : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            loadPOSes();
        }
        private void loadPOSes()
        {
            var data = bl.getPOSes().OrderByDescending(x=>x.Id).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPosId.Text != "")
            {
                var p = bl.getPOS(int.Parse(txtPosId.Text));
                p.POSName = txtposname.Text;
                p.POSCode = txtposcode.Text;
                p.POSAddress = txtposaddress.Text;
                p.Mainstore = chkmainstore.Items[0].Selected ? true : false;

                bl.savePOS(p);
                lblMessage.Text = "POS updated successfully!.";
            }
            else
            {
                orion.ims.DAL.POSe pos = new DAL.POSe
                {
                    POSName = txtposname.Text,
                    POSCode = txtposcode.Text,
                    POSAddress = txtposaddress.Text,
                    Mainstore = chkmainstore.Items[0].Selected ? true : false
                };
                bl.savePOS(pos);
                lblMessage.Text = "POS saved successfully!.";

            }
            btnCancel_Click(null, null);
            loadPOSes();

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtposaddress.Text = "";
            txtposcode.Text="";
            txtPosId.Text = "";
            txtposname.Text = "";
        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loadPOSes();
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {
                    loadPOSes();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtPosId.Text = hf.Value;
                    int id = int.Parse(txtPosId.Text);
                    var x = bl.getPOS(id);
                    txtposname.Text = x.POSName;
                    txtposaddress.Text = x.POSAddress;

                    chkmainstore.Items[0].Selected = false;
                    chkmainstore.Items[1].Selected = false;

                    if (x.Mainstore == true)
                    { chkmainstore.Items[0].Selected = true; }
                    else
                        chkmainstore.Items[1].Selected = true;

                    loadPOSes();
                }
                else if (e.CommandName == "del")
                {
                    loadPOSes();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getPOS(id);

                    Bmat.Tools.BTSecurity ln = new BTSecurity();
                    string slt = ln.GenerateSalt(9);
                    if (c != null)
                    {
                        bl.deletePOS(id);
                        lblMessage.Text = "Deleted POS " + txtposname.Text;
                        Util.LogAudit(this.Page, "Deleted POS " + txtposname.Text, this.Title);
                        loadPOSes();
                    }

                }
            }
            catch (Exception ex)
            {

                string fx, caller;
                Util.ErrorFunctions(new StackTrace(), new StackFrame(), out fx, out caller);
                Util.SysErrLog(ex.Message, fx, caller);
            }

        }
        protected void chkMainstore_CheckedChanged(object sender, EventArgs e)
        {

        }
}
}