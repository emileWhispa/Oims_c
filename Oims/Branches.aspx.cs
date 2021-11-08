using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using Bmat.Tools;
using System.Diagnostics;
namespace orion.ims
{
    public partial class Branches : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                var l = (from b in bl.getBanks().ToList() select new ListItem { Text = b.BankName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlBankNew, l,true);
                Util.loadCombo(ddlBanks, l, true);
                var rg = (from b in bl.getRegions().ToList() select new ListItem { Text = b.RegionName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlRegion, rg,true);
                Util.LogAudit(this.Page, "Accessed Page", this.Title);
                
            }
          // lbldisplay.Text = "";
        }

        protected void ddlBanks_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranchbyBank();
        }

        private void loadBranches()
        {
            int bid = Convert.ToInt32(ddlBanks.SelectedValue);
            var bs = bl.getBranches();
            gvData.DataSource = bs.ToList();
            gvData.DataBind();
        }

        private void loadBranchbyBank()
        {
            int bid = Convert.ToInt32(ddlBanks.SelectedValue);
            var bs = bl.getBankBranches(bid).OrderByDescending(x=>x.Id);
            gvData.DataSource = bs.ToList();
            gvData.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtBranchId.Text != "")
            {
                var branch = bl.getBranch(int.Parse(txtBranchId.Text));
                branch.BankId = Convert.ToInt16(ddlBankNew.SelectedValue);
                branch.RegionId = Convert.ToInt16(ddlRegion.SelectedValue);
                branch.BranchName = txtBranchName.Text;
                bl.saveBranch(branch);
                lblMessage.Text = "Branch updated successfully!.";
            }
            else
            {
                orion.ims.DAL.Branch br = new DAL.Branch
                {

                    BankId = Convert.ToInt16(ddlBankNew.SelectedValue),
                    RegionId = Convert.ToInt16(ddlRegion.SelectedValue),
                    BranchName = txtBranchName.Text
                };
                bl.saveBranch(br);
                lblMessage.Text = "Branch saved successfully!.";

            }
            btnCancel_Click(null, null);
            loadBranches();
            
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {
                    loadBranches();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtBranchId.Text = hf.Value;
                    int id = int.Parse(txtBranchId.Text);
                    var x = bl.getBranch(id);
                    txtBranchName.Text = x.BranchName;
                    ddlBankNew.SelectedIndex = ddlBankNew.Items.IndexOf(ddlBankNew.Items.FindByValue(x.BankId.ToString()));
                    ddlRegion.SelectedIndex = ddlRegion.Items.IndexOf(ddlRegion.Items.FindByValue(x.RegionId.ToString()));
                    loadBranches();
                }
                else if (e.CommandName == "del")
                {
                    loadBranches();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getBranch(id);

                    Bmat.Tools.BTSecurity ln = new BTSecurity();
                    string slt = ln.GenerateSalt(9);
                    if (c != null)
                    {
                        bl.deleteBranch(id);
                        lblMessage.Text = "Deleted Branch " + txtBranchName.Text;
                        Util.LogAudit(this.Page, "Deleted Branch " + txtBranchName.Text, this.Title);
                        loadBranches();
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
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loadBranches();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtBranchId.Text = "";
            txtBranchName.Text = "";
            ddlBankNew.SelectedIndex = -1;
            ddlRegion.SelectedIndex = -1;


        }
       
}
}