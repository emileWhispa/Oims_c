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

    public partial class Expenses : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                var expgrp = (from b in bl.getExpenseGroups().ToList() select new ListItem { Text = b.ExpenseGroupName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlexpgrp, expgrp,true);

                var cur = (from b in bl.getCurrencies().ToList() select new ListItem { Text = b.CurrencyName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlcurrency, cur,true);

                Util.LogAudit(this.Page, "Accessed Page", this.Title);

                loadExpenses();

            }

        }

        private void loadExpenses()
        {
            var data = bl.getExpenses().OrderByDescending(x=>x.Id).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            decimal vrate;
            if (txtexpenseId.Text != "")
            {
                var expense = bl.getExpense(int.Parse(txtexpenseId.Text));
                expense.ExpenseCategoryId = Convert.ToInt16(ddlexpcat.SelectedValue);
                expense.Amount = Convert.ToDecimal(txtamount.Text);
                expense.CurrencyId = Convert.ToInt16(ddlcurrency.SelectedValue);
                expense.ExchangeRate = Convert.ToDecimal(txtexrate.Text);
                expense.Description = txtdescr.Text;
                expense.UserId = int.Parse(Session["UserId"].ToString());
                expense.vatrate = Convert.ToDecimal(txtvatrate.Text);

                bl.saveExpense(expense);
                lblMessage.Text = "Expense Updated Successfully!.";
            }
            else
            {
                if (!chkbxvatable.Checked)
                {
                     vrate = 0;
                }
                else
                {
                    vrate = Convert.ToDecimal(txtvatrate.Text);
                }
                orion.ims.DAL.Expense exp = new orion.ims.DAL.Expense
                {
                    ExpenseCategoryId = Convert.ToInt16(ddlexpcat.SelectedValue),
                    Amount = Convert.ToDecimal(txtamount.Text),
                    CurrencyId = Convert.ToInt16(ddlcurrency.SelectedValue),
                    ExchangeRate = Convert.ToDecimal(txtexrate.Text),
                    ExpenseDate = Convert.ToDateTime(txtexpDate.Text),
                    Description = txtdescr.Text,
                    UserId = int.Parse(Session["UserId"].ToString()),
                    vatrate = vrate

                };
                bl.saveExpense(exp);
                lblMessage.Text = "Expense Saved Successfully!.";
            }
            btnCancel_Click(null, null);
            TabContainer1.ActiveTabIndex = 1;
            loadExpenses();

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlexpgrp.SelectedIndex = -1;
            ddlexpcat.SelectedIndex = -1;
            txtamount.Text = "";
            ddlcurrency.SelectedIndex = -1;
            txtexrate.Text = "";
            txtexpDate.Text = "";
            txtdescr.Text = "";
            txtvatrate.Text = "";
            lbVatRate.Visible = false;
            txtvatrate.Visible = false;
            txtexpenseId.Text = "";
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {
                    TabContainer1.ActiveTabIndex = 0;
                    loadExpenses();
                    
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtexpenseId.Text = hf.Value;
                    int id = int.Parse(txtexpenseId.Text);
                    var x = bl.getExpense(id);

                    ddlexpcat.SelectedIndex = ddlexpcat.Items.IndexOf(ddlexpcat.Items.FindByValue(x.ExpenseCategoryId.ToString()));
                    txtamount.Text = x.Amount.ToString();
                    ddlcurrency.SelectedIndex = ddlcurrency.Items.IndexOf(ddlcurrency.Items.FindByValue(x.CurrencyId.ToString()));
                    txtexrate.Text = x.ExchangeRate.ToString();
                    txtexpDate.Text = x.ExpenseDate.ToString();
                    txtdescr.Text = x.Description;
                    lbVatRate.Visible = true;
                    txtvatrate.Visible = true;
                    txtvatrate.Text = x.vatrate.ToString();
                    loadExpenses();
                }
                else if (e.CommandName == "del")
                {
                    loadExpenses();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getExpense(id);

                    Bmat.Tools.BTSecurity ln = new BTSecurity();
                    string slt = ln.GenerateSalt(9);
                    if (c != null)
                    {
                        bl.deleteExpense(id);
                        lblMessage.Text = "Expense deleted successfully.";
                        Util.LogAudit(this.Page, "Deleted Expense " + txtdescr.Text, this.Title);
                        loadExpenses();
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
            loadExpenses();
        }
        private void loadExpenseCategoriesbyGroup()
        {
            int bId = Convert.ToInt16(ddlexpgrp.SelectedValue);
            var br = (from b in bl.getExpenseCategories(bId).ToList() select new ListItem { Text = b.ExpenseName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlexpcat, br);
        }
        protected void ddlexpgrp_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadExpenseCategoriesbyGroup();

        }
     
        protected void chkbxYes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkbxvatable.Checked)
            {
                lbVatRate.Visible = true;
                txtvatrate.Visible = true;
            }
            else { lbVatRate.Visible = false; txtvatrate.Visible = false; }
        }
}
