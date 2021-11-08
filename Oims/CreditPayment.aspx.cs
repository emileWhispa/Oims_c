using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using Bmat.Tools;
using System.Data.SqlClient;
using System.Data;

public partial class CreditPayment : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    private static DataTable dtCredits = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        //   |Do not allow caching of page
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        bl = new Bl(conn);
        if (!IsPostBack)
        {
            var cust = (from b in bl.getCustomers().OrderBy(x => x.CustomerName).ToList() select new ListItem { Text = b.CustomerName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlCustomer, cust, true);
            var py = (from b in bl.getPaymentModes().Where(t=>t.Id!=4).ToList() select new ListItem { Text = b.PaymentName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlpaymentmode, py, true);

            Util.LogAudit(this.Page, "Accessed Page", this.Title);
        }
    }
    protected void txtSearchCustomer_TextChanged(object sender, EventArgs e)
    {
        var cust = (from b in bl.getCustomers().Where(x => x.CustomerName.Contains(txtSearchCustomer.Text)).OrderBy(x => x.CustomerName).ToList() select new ListItem { Text = b.CustomerName, Value = b.Id.ToString() }).ToList();
        Util.loadCombo(ddlCustomer, cust, true);
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlCustomer.SelectedIndex != -1)
            {
                var cust = bl.getCustomer(int.Parse(ddlCustomer.SelectedValue));
                if (cust != null)
                {
                    if (cust.Balance == 0)
                        txtBalance.Text = "0";
                    else
                        txtBalance.Text = decimal.Parse(cust.Balance.ToString()).ToString("#,###");
                }
                loadCredits();
                btnCancel_Click(sender, e);
                loaddata();
                //pnlDetails.Visible = false;
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void loadCredits()
    {
        try
        {
            string cmd = "Select d.SalesId,a.CreditId,d.InvoiceNumber, a.CreditDate, a.OpeningBalance, a.Amount, a.PaidAmount, (a.Amount-a.PaidAmount) as Balance,b.Fullnames, case a.PaymentStatus when 0 then 'Pending' when 1 then 'Partially Paid' end as Status from CreditSales a, Users b, SalesPayments c, Sales d where a.UserId=b.UserId and a.SalesPaymentId=c.SalesPaymentId and c.SalesId=d.SalesId and d.CustomerId=" + ddlCustomer.SelectedValue + " and a.Cancelled=0 and isnull(a.PaymentStatus,0)<2";
            dtCredits = Toolkit.dataDisplay(conn, cmd);
            gvCredit.DataSource = dtCredits;
            gvCredit.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void ddlpaymentmode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlpaymentmode.SelectedIndex != -1)
        {
            if (ddlpaymentmode.SelectedItem.Text.Contains("Bank") || ddlpaymentmode.SelectedItem.Text.Contains("Cheque"))
            {
                rqdDocumentNo.Enabled = true;
            }
            else
                rqdDocumentNo.Enabled = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string cmd = "exec sp_credit_payment " + txtCreditId.Text + "," + txtBalance.Text.Replace(",", "") + "," + txtSalesId.Text + "," + ddlpaymentmode.SelectedValue + "," + txtamount.Text.Replace(",", "") + "," + Session["UserId"].ToString() + ",'" + txtdocno.Text.Replace("'", "''") + "'," + ddlCustomer.SelectedValue;
            Toolkit.RunSqlCommand(cmd, conn);
            ddlCustomer_SelectedIndexChanged(sender, e);
            loaddata();
            btnCancel_Click(null, null);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtamount.Text = "";
        txtInvoiceAmount.Text = "";
        txtdocno.Text = "";
        ddlpaymentmode.SelectedIndex = -1;
        //gvPayment.DataSource = null;
        //gvPayment.DataBind();
        //pnlDetails.Visible = false;
    }

    private void loaddata()
    {
        if (txtSalesId.Text == "")
            return;
        try
        {
            var dtPayments = Toolkit.dataDisplay(conn, "Select a.SalesPaymentId as Id, a.PaymentDate, b.PaymentName,a.Amount, c.FullNames, a.DocumentNo from SalesPayments a, PaymentModes b, Users c where a.SalesId=" + txtSalesId.Text + " and a.PaymentModeId=b.ModeId and a.UserId=c.UserId order by a.SalesPaymentId desc ");
            gvPayment.DataSource = dtPayments;
            gvPayment.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void gvCredit_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (gvCredit.SelectedIndex != -1)
        {
            pnlDetails.Visible = true;
            int rowId = gvCredit.SelectedIndex;
            string CreditId = gvCredit.DataKeys[rowId].Value.ToString();
            txtCreditId.Text = CreditId;
            //var hf = (HiddenField)gvCredit.Rows[rowId].FindControl("hfId");
            //TextBox txtSalesValueId = (TextBox)gvCredit.Rows[rowId].FindControl("txtSalesVal");
            DataRow[] salesRows = dtCredits.Select("CreditId=" + CreditId);
            if(salesRows.Count()==1)
            txtSalesId.Text = salesRows[0].ItemArray.GetValue(0).ToString();
            txtInvoiceAmount.Text = decimal.Parse(gvCredit.Rows[rowId].Cells[4].Text.ToString()).ToString("#,###");
            txtamount.Text = txtInvoiceAmount.Text;
            loaddata();
        }
    }
}