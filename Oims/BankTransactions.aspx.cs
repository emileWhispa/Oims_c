using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.BL;
using orion.ims.DAL;
using Bmat.Tools;
using System.Diagnostics;

public partial class BankTransactions : System.Web.UI.Page
{

    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        bl = new Bl(conn);
        if (!IsPostBack)
        {
            var l = (from b in bl.getTransactionTypes().ToList() select new ListItem { Text = b.Description, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlTransatype, l,true);

            var c = (from b in bl.getPostingTypes().ToList() select new ListItem { Text = b.Description, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlposting, c,true);

            var bnk = (from b in bl.getBanks().ToList() select new ListItem { Text = b.BankName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlbank, bnk,true);

            Util.LogAudit(this.Page, "Accessed Page", this.Title);

            loadTransactions();

        }
      
    }

    private void loadTransactions()
    {
        var data = bl.getBankTransactions().ToList();
        gvData.DataSource = data;
        gvData.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtbankTransId.Text != "")
        {
            var trans = bl.getBankTransaction(int.Parse(txtbankTransId.Text));
            trans.TransactionTypeId = Convert.ToInt16(ddlTransatype.SelectedValue);
            trans.AccountId = Convert.ToInt16(ddlaccounts.SelectedValue);
            trans.Amount = Convert.ToDecimal(txtamount.Text);
            trans.TransactionDate = Convert.ToDateTime(txtTransactionDate.Text);
            trans.PostingId = Convert.ToInt16(ddlposting.SelectedValue);
            trans.AdditionalInfo = txtaddinfo.Text;
            trans.DocRef = txtdocno.Text;
            trans.Balance = Convert.ToDecimal(txtbalance.Text);
            bl.saveBankTransaction(trans);

        }
        else
        {
            orion.ims.DAL.BankTransaction bt = new orion.ims.DAL.BankTransaction
            {
                TransactionTypeId = Convert.ToInt16(ddlTransatype.SelectedValue),
                AccountId = Convert.ToInt16(ddlaccounts.SelectedValue),
                Amount = Convert.ToDecimal(txtamount.Text),
                TransactionDate = Convert.ToDateTime(txtTransactionDate.Text),
                PostingId = Convert.ToInt16(ddlposting.SelectedValue),
                AdditionalInfo = txtaddinfo.Text,
                DocRef = txtdocno.Text,
                Balance = Convert.ToInt16(txtbalance.Text)
            };
            bl.saveBankTransaction(bt);
        }
        TabContainer1.ActiveTabIndex = 1;
        loadTransactions();
        btnCancel_Click(null, null);
                
            }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtbankTransId.Text = "";
        ddlTransatype.SelectedIndex = -1;
        ddlaccounts.SelectedIndex = -1;
        txtamount.Text = "";
        txtTransactionDate.Text = "";
        ddlposting.SelectedIndex = -1 ;
        txtaddinfo.Text = "";
        txtdocno.Text = "";
        txtbalance.Text = "";
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "edt")
            {
                TabContainer1.ActiveTabIndex = 0;
                loadTransactions();

                int rowId = Convert.ToInt32(e.CommandArgument);
                var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                txtbankTransId.Text = hf.Value;
                int id = int.Parse(txtbankTransId.Text);
                var x = bl.getBankTransaction(id);

                ddlTransatype.SelectedIndex = ddlTransatype.Items.IndexOf(ddlTransatype.Items.FindByValue(x.TransactionTypeId.ToString()));
                ddlaccounts.SelectedIndex = ddlaccounts.Items.IndexOf(ddlaccounts.Items.FindByValue(x.AccountId.ToString()));
                txtamount.Text = x.Amount.ToString();
                txtTransactionDate.Text = x.TransactionDate.ToString();

                ddlposting.SelectedIndex = ddlposting.Items.IndexOf(ddlposting.Items.FindByValue(x.PostingId.ToString()));
                txtaddinfo.Text = x.AdditionalInfo;
                txtbalance.Text = x.Balance.ToString();

                loadTransactions();
            }
            else if (e.CommandName == "del")
            {
                loadTransactions();
                int rowId = Convert.ToInt32(e.CommandArgument);
                var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                hf.Value = hf.Value;
                int id = int.Parse(hf.Value);
                var c = bl.getBankTransaction(id);

                Bmat.Tools.BTSecurity ln = new BTSecurity();
                string slt = ln.GenerateSalt(9);
                if (c != null)
                {
                    bl.deleteBankTransaction(id);
                    Util.LogAudit(this.Page, "Deleted Transaction " + txtbankTransId.Text, this.Title);
                    loadTransactions();
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
        loadTransactions();
    }

    private void loadAccountsByBank()
    {
        int bId = Convert.ToInt16(ddlbank.SelectedValue);
            var br = (from b in bl.getBankAccounts(bId).OrderByDescending(X=>X.Id).ToList() select new ListItem { Text = b.AccountNo, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlaccounts, br);
    }
    protected void ddlbank_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadAccountsByBank();
    }
   
}