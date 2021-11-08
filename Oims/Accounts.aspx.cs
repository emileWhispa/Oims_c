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

    public partial class Accounts : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                var l = (from b in bl.getBanks().ToList() select new ListItem { Text = b.BankName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlbank, l,true);
                Util.loadCombo(ddlBanks, l, true);
                var c = (from b in bl.getCurrencies().ToList() select new ListItem { Text = b.CurrencyName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlcurrency, c,true);
                Util.LogAudit(this.Page, "Accessed Page", this.Title);
                loadAccounts();
            }

        }

        private void loadBranchesbyBank()
        {
            int bId = Convert.ToInt16(ddlbank.SelectedValue);
            var br = (from b in bl.getBankBranches(bId).ToList() select new ListItem { Text = b.BranchName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlbranch, br);

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtaccountId.Text != "")
            {
                var account = bl.getAccount(int.Parse(txtaccountId.Text));
                account.AccountNo = txtaccountNo.Text;
                account.AccountName = txtaccntnme.Text;
                account.BankId = Convert.ToInt16(ddlbank.SelectedValue);
                account.BranchId = Convert.ToInt16(ddlbranch.SelectedValue);
                account.CurrencyId = Convert.ToInt16(ddlcurrency.SelectedValue);


                bl.saveAccount(account);
                Util.ShowMsgBox(this.Page," Account updated successfully.");
                lblMessage.Text = "Account updated successfully!.";
            }
            else
            {
                orion.ims.DAL.Account ac = new orion.ims.DAL.Account
                  {
                      Bank = bl.getBank(int.Parse(ddlbank.SelectedValue)),
                      Branch = bl.getBranch(int.Parse(ddlbranch.SelectedValue)),
                      Currency = bl.getCurrency(int.Parse(ddlcurrency.SelectedValue)),

                      AccountNo = txtaccountNo.Text,
                      AccountName = txtaccntnme.Text,
                      BankId = Convert.ToInt16(ddlbank.SelectedValue),
                      BranchId = Convert.ToInt16(ddlbranch.SelectedValue),
                      CurrencyId = Convert.ToInt16(ddlcurrency.SelectedValue),
                      Balance = 0
                  };

                bl.saveAccount(ac);
                lblMessage.Text = "Account no " + txtaccountNo.Text + " Saved successfully!.";

            }
                btnCancel_Click(null, null);
                loadAccounts();
            
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtaccountNo.Text = "";
            txtaccntnme.Text = "";
            ddlbank.SelectedIndex = -1;
            ddlbranch.SelectedIndex = -1;
            ddlcurrency.SelectedIndex = -1;

        }
        private void loadAccounts()
        {
            var data = bl.getAccounts().OrderByDescending(x=>x.Id).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loadAccounts();
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {

                    loadAccounts();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtaccountId.Text = hf.Value;
                    int id = int.Parse(txtaccountId.Text);
                    var x = bl.getAccount(id);
                    txtaccountNo.Text = x.AccountNo;
                    txtaccntnme.Text = x.AccountName;
                    ddlbank.SelectedIndex = ddlbank.Items.IndexOf(ddlbank.Items.FindByValue(x.BankId.ToString()));
                    ddlbranch.SelectedIndex = ddlbranch.Items.IndexOf(ddlbranch.Items.FindByValue(x.BranchId.ToString()));
                    ddlcurrency.SelectedIndex = ddlcurrency.Items.IndexOf(ddlcurrency.Items.FindByValue(x.CurrencyId.ToString()));
                    //decimal balance = x.Balance;
                    loadAccounts();
                }
                else if (e.CommandName == "del")
                {
                    loadAccounts();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getAccount(id);

                    Bmat.Tools.BTSecurity ln = new BTSecurity();
                    string slt = ln.GenerateSalt(9);
                    if (c != null)
                    {
                        bl.deleteAccount(id);
                        lblMessage.Text = "Deleted Account " + txtaccntnme.Text;
                        Util.LogAudit(this.Page, "Deleted Account " + txtaccntnme.Text, this.Title);
                        loadAccounts();
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
        protected void ddlbank_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadBranchesbyBank();
        }
        private void loadAccountsbyBank()
        {
            int bid = Convert.ToInt32(ddlBanks.SelectedValue);
            var bs = bl.getBankAccounts(bid);
            gvData.DataSource = bs.ToList();
            gvData.DataBind();
        }
        protected void ddlBanks_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadAccountsbyBank();
        }
}

