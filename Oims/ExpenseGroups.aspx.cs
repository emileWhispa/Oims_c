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

public partial class ExpenseGroups : System.Web.UI.Page
{

    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        bl = new Bl(conn);
        loadExpenseGroups();
        
    }
    private void loadExpenseGroups()
    {
        var data = bl.getExpenseGroups().OrderByDescending(x=>x.Id).ToList();
        gvData.DataSource = data;
        gvData.DataBind();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtexpgrpId.Text != "")
        {
            var expgrp = bl.getExpenseGroup(int.Parse(txtexpgrpId.Text));
            expgrp.ExpenseGroupName = txtexpGrpName.Text;
            bl.saveExpenseGroup(expgrp);
            lblMessage.Text = "Expense Group updated successfully!.";
        }
        else
        {
            orion.ims.DAL.ExpenseGroup expg = new orion.ims.DAL.ExpenseGroup
            {
                ExpenseGroupName = txtexpGrpName.Text
            };
            bl.saveExpenseGroup(expg);
            lblMessage.Text = "Expense Group saved successfully!.";

        }
        btnCancel_Click(null, null);
        loadExpenseGroups();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtexpGrpName.Text = "";
        txtexpgrpId.Text = "";
    }
    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "edt")
            {

                loadExpenseGroups();
                int rowId = Convert.ToInt32(e.CommandArgument);
                var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                txtexpgrpId.Text = hf.Value;
                int id = int.Parse(txtexpgrpId.Text);
                var x = bl.getExpenseGroup(id);

                txtexpGrpName.Text = x.ExpenseGroupName;

                loadExpenseGroups();
            }
            else if (e.CommandName == "del")
            {
                loadExpenseGroups();
                int rowId = Convert.ToInt32(e.CommandArgument);
                var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                hf.Value = hf.Value;
                int id = int.Parse(hf.Value);
                var c = bl.getBankTransaction(id);

                Bmat.Tools.BTSecurity ln = new BTSecurity();
                string slt = ln.GenerateSalt(9);
                if (c != null)
                {
                    bl.deletexpenseGroup(id);
                    lblMessage.Text = "Expense Group has been deleted successfuly.";
                    Util.LogAudit(this.Page, "Deleted Transaction " + txtexpgrpId.Text, this.Title);
                    loadExpenseGroups();
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
        loadExpenseGroups();
    }
}