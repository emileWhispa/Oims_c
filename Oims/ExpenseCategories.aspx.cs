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
    public partial class ExpenseCategories : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                var l = (from b in bl.getExpenseGroups().ToList() select new ListItem { Text = b.ExpenseGroupName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlexpgroup, l,true);
                
                Util.LogAudit(this.Page, "Accessed Page", this.Title);
                loadExpenseCategories();

            }

        }
        private void loadExpenseCategories()
        {
            var data = bl.getExpenseCategories().OrderByDescending(x=>x.Id).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtexpcatId.Text != "")
            {
                var excat = bl.getExpenseCategory(int.Parse(txtexpcatId.Text));
                excat.ExpenseGroupId = Convert.ToInt16(ddlexpgroup.SelectedValue);
                excat.ExpenseName = txtexpname.Text;
                excat.SageLedgerNo = Convert.ToInt32(txtsageno.Text);
                bl.saveExpenseCategory(excat);
                lblMessage.Text = "Expense Categories updated Succesfully";
            }
            else
            {
                orion.ims.DAL.ExpenseCategory expcat = new DAL.ExpenseCategory
                {
                    ExpenseGroupId = Convert.ToInt16(ddlexpgroup.SelectedValue),
                    ExpenseName = txtexpname.Text,
                    SageLedgerNo = Convert.ToInt32(txtsageno.Text)
                };
                bl.saveExpenseCategory(expcat);
                lblMessage.Text= "Expense Categories saved Succesfully";
            }
            btnCancel_Click(null, null);
            loadExpenseCategories();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlexpgroup.SelectedIndex = -1;
            txtexpname.Text = "";
            txtsageno.Text = "";
           // lblMessage.Text = "";
        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loadExpenseCategories();
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {
                    loadExpenseCategories();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtexpcatId.Text = hf.Value;
                    int id = int.Parse(txtexpcatId.Text);
                    var x = bl.getExpenseCategory(id);

                    ddlexpgroup.SelectedIndex = ddlexpgroup.Items.IndexOf(ddlexpgroup.Items.FindByValue(x.ExpenseGroupId.ToString()));

                    txtexpname.Text = x.ExpenseName;
                    txtsageno.Text = x.SageLedgerNo.ToString();

                    loadExpenseCategories();
                }
                else if (e.CommandName == "del")
                {
                    loadExpenseCategories();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getBranch(id);

                    Bmat.Tools.BTSecurity ln = new BTSecurity();
                    string slt = ln.GenerateSalt(9);
                    if (c != null)
                    {
                        bl.deleteExpCategory(id);
                        //Util.LogAudit(this.Page,  " + txtexpname.Text, this.Title);
                        lblMessage.Text = "Deleted Expense Category "+ txtexpname.Text ;
                        loadExpenseCategories();
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
}
}