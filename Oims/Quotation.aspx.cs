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

public partial class Quotation : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    private static bool isNew = true;
    static string lastSearchKey = "All";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //   |Do not allow caching of page
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                lblError.Text = "";
                loadData("All");
                //var cust = (from b in bl.getCustomers().OrderBy(x => x.CustomerName).ToList() select new ListItem { Text = b.CustomerName + "-" + b.PhoneNo, Value = b.Id.ToString() }).ToList();
                //Util.loadCombo(ddlCustomer, cust, true);
                //Util.loadCombo(ddlDiscountedCustomer, cust, true);
                dteOrderDate.Text = getPOSValueDate(conn, Session["POSId"].ToString());
                Util.LogAudit(this.Page, "Accessed Page", this.Title);
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private string getPOSValueDate(string con, string POSId)
    {
        string valueDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
        DataTable dtValue = Toolkit.dataDisplay(con, "SELECT convert(varchar(25), dateadd(hh,15,getdate()),102)");//We are adding +10 hours//"Select convert(varchar(25),ValueDate,102) from ValueDates where POSId=" + POSId);
        //SELECT convert(varchar(25), dateadd(hh,10,getdate()),102)
        if (dtValue != null && dtValue.Rows.Count == 1)
            valueDate = dtValue.Rows[0].ItemArray.GetValue(0).ToString().Replace(".", "-");
        return valueDate;
    }

    protected void btnSaveSale_Click(object sender, EventArgs e)
    {
        try
        {
            lblError.Text = "";
            if (isNew || txtSalesId.Text.Trim()=="")
            {
                //lblError0.Text = "Date:" + dteOrderDate.Text + " Cust ID:" + ddlCustomer.SelectedValue + " Remarks:" + txtRemarks.Text + " Deposit:" + txtDepositAmount.Text + " User:" + Session["UserId"].ToString() + " Disc ID:" + ddlDiscountedCustomer.SelectedValue;
                OIMSDataContext dc = new OIMSDataContext(conn);
                int res = dc.usp_InsertSales(DateTime.Parse(dteOrderDate.Text), txtRemarks.Text, int.Parse(ddlCustomer.SelectedValue), decimal.Parse(txtDepositAmount.Text), GenerateReceipt(), int.Parse(Session["UserId"].ToString()), int.Parse(ddlDiscountedCustomer.SelectedValue));
                if (res > 0)
                {
                    txtSalesId.Text = res.ToString();
                }
            }
            else//Update
            {
                int id = int.Parse(txtSalesId.Text);
                var q = bl.getSale(id);
                q.QuoteDate = DateTime.Parse(dteOrderDate.Text);
                q.Remarks = txtRemarks.Text;
                q.CustomerId = int.Parse(ddlCustomer.SelectedValue);
                q.Deposit = decimal.Parse(txtDepositAmount.Text);
                q.DiscountCustomerId = int.Parse(ddlDiscountedCustomer.SelectedValue);
                bl.saveSales(q);
            }
            clearSaleControls();
            loadData("All");
        }
        catch (Exception ex)
        {
            //Util.ShowMsgBox(this, ex.Message, "Error while saving", MsgBoxType.Error);
            lblError.Text = ex.Message;
        }
    }

    private string GenerateReceipt()
    {
        string ReceiptNo = "";
        string sSql = "Sp_GenerateReceipt";
        var con = new SqlConnection(conn);
        var tcmd = new SqlCommand(sSql, con);
        tcmd.Connection.Open();
        var retval = tcmd.ExecuteScalar();
        if (retval != null)
            ReceiptNo = retval.ToString();
        return ReceiptNo; ;
    }

    protected void btnCancelSale_Click(object sender, EventArgs e)
    {
        clearSaleControls();
    }

    private void clearSaleControls()
    {
        ddlCustomer.SelectedIndex = -1;
        ddlDiscountedCustomer.SelectedIndex = -1;
        dteOrderDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        txtRemarks.Text = "";
        txtDepositAmount.Text = "0";
        txtSalesId.Text = "";
        lblError.Text = "";
        txtSearchByName.Text = "";
        txtSearchKey.Text = "";
        isNew = true;
    }

    private void loadData(string searchKey)
    {
        lastSearchKey = searchKey;
        DataTable dt = new DataTable();
        if (searchKey == "SalesRef")
        {
            if (txtSearchKey.Text.Trim() != "")
                dt = Toolkit.dataDisplay(conn, "Select a.SalesId as Id, a.Remarks, a.QuoteDate, a.CustomerId,b.CustomerName, a.Deposit, a.SalesRef,c.FullNames, case a.PaymentStatus when 0 then 'Quotation' when 1 then 'Order' when 2 then 'Invoice' when 3 then 'Partially Paid' when 4 then 'Fully Paid' when -1 then 'Cancelled' else 'Unknown' end as Type from Sales a, Customers b, Users c where a.CustomerID=b.CustomerID and a.SalesRef='" + txtSearchKey.Text.Trim() + "' and a.MakerId=c.UserID and a.POSId=" + Session["POSId"].ToString() + " order by a.SalesId desc");
            else
                dt = Toolkit.dataDisplay(conn, "Select top(30) a.SalesId as Id, a.Remarks, a.QuoteDate, a.CustomerId,b.CustomerName, a.Deposit, a.SalesRef,c.FullNames, case a.PaymentStatus when 0 then 'Quotation' when 1 then 'Order' when 2 then 'Invoice' when 3 then 'Partially Paid' when 4 then 'Fully Paid' when -1 then 'Cancelled' else 'Unknown' end as Type from Sales a, Customers b, Users c where a.CustomerID=b.CustomerID and a.MakerId=c.UserID and a.POSId=" + Session["POSId"].ToString() + " order by a.SalesId desc");
        }
        else if (searchKey == "CustomerName")
        {
            if (txtSearchByName.Text.Trim() != "")
                dt = Toolkit.dataDisplay(conn, "Select top(30) a.SalesId as Id, a.Remarks, a.QuoteDate, a.CustomerId,b.CustomerName, a.Deposit, a.SalesRef,c.FullNames, case a.PaymentStatus when 0 then 'Quotation' when 1 then 'Order' when 2 then 'Invoice' when 3 then 'Partially Paid' when 4 then 'Fully Paid' when -1 then 'Cancelled' else 'Unknown' end as Type from Sales a, Customers b, Users c where a.CustomerID=b.CustomerID and a.MakerId=c.UserID and b.CustomerName like '%" + txtSearchByName.Text.Trim() + "%' and a.POSId=" + Session["POSId"].ToString() + " order by a.SalesId desc");
            else
                dt = Toolkit.dataDisplay(conn, "Select top(30) a.SalesId as Id, a.Remarks, a.QuoteDate, a.CustomerId,b.CustomerName, a.Deposit, a.SalesRef,c.FullNames, case a.PaymentStatus when 0 then 'Quotation' when 1 then 'Order' when 2 then 'Invoice' when 3 then 'Partially Paid' when 4 then 'Fully Paid' when -1 then 'Cancelled' else 'Unknown' end as Type from Sales a, Customers b, Users c where a.CustomerID=b.CustomerID and a.MakerId=c.UserID and a.POSId=" + Session["POSId"].ToString() + " order by a.SalesId desc");
        }
        else if (searchKey == "InvoiceNo")
        {
            if (txtSearchByInvoiceNo.Text.Trim() != "")
                dt = Toolkit.dataDisplay(conn, "Select a.SalesId as Id, a.Remarks, a.QuoteDate, a.CustomerId,b.CustomerName, a.Deposit, a.SalesRef,c.FullNames, case a.PaymentStatus when 0 then 'Quotation' when 1 then 'Order' when 2 then 'Invoice' when 3 then 'Partially Paid' when 4 then 'Fully Paid' when -1 then 'Cancelled' else 'Unknown' end as Type from Sales a, Customers b, Users c where a.CustomerID=b.CustomerID and a.InvoiceNumber='" + txtSearchByInvoiceNo.Text.Trim() + "' and a.MakerId=c.UserID and a.POSId=" + Session["POSId"].ToString() + " order by a.SalesId desc");
            else
                dt = Toolkit.dataDisplay(conn, "Select top(30) a.SalesId as Id, a.Remarks, a.QuoteDate, a.CustomerId,b.CustomerName, a.Deposit, a.SalesRef,c.FullNames, case a.PaymentStatus when 0 then 'Quotation' when 1 then 'Order' when 2 then 'Invoice' when 3 then 'Partially Paid' when 4 then 'Fully Paid' when -1 then 'Cancelled' else 'Unknown' end as Type from Sales a, Customers b, Users c where a.CustomerID=b.CustomerID and a.MakerId=c.UserID and a.POSId=" + Session["POSId"].ToString() + " order by a.SalesId desc");
        }
        else
            dt = Toolkit.dataDisplay(conn, "Select top(30) a.SalesId as Id, a.Remarks, a.QuoteDate, a.CustomerId,b.CustomerName, a.Deposit, a.SalesRef,c.FullNames, case a.PaymentStatus when 0 then 'Quotation' when 1 then 'Order' when 2 then 'Invoice' when 3 then 'Partially Paid' when 4 then 'Fully Paid' when -1 then 'Cancelled' else 'Unknown' end as Type from Sales a, Customers b, Users c where a.CustomerID=b.CustomerID and a.MakerId=c.UserID and a.POSId=" + Session["POSId"].ToString() + " order by a.SalesId desc");
        gvData.DataSource = dt;
        gvData.DataBind();
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int rowId = Convert.ToInt32(e.CommandArgument);
            var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
            string SalesID = gvData.DataKeys[rowId].Value.ToString();
            DataTable dt = new DataTable();
            dt = Toolkit.dataDisplay(conn, "Select a.SalesId as Id, a.Remarks, a.QuoteDate, a.CustomerId,b.CustomerName, a.Deposit, a.SalesRef,c.FullNames, case a.PaymentStatus when 0 then 'Quotation' when 1 then 'Order' when 2 then 'Invoice' else 'Cancelled' end as Type, a.DiscountCustomerId from Sales a, Customers b, Users c where a.CustomerID=b.CustomerID and a.MakerId=c.UserID and SalesID=" + SalesID);
            if (dt != null && dt.Rows.Count == 1)
            {
                DataRow r = dt.NewRow();
                r = dt.Rows[0];
                if (e.CommandName == "sel")
                {
                    loadData(lastSearchKey);
                    txtSalesId.Text = SalesID;
                    int id = int.Parse(txtSalesId.Text);
                    var sl = bl.getSale(id);
                    if (sl.PaymentStatus >= 2)
                    {
                        lblError.Text = "You cannot edit an approved invoice";
                        return;
                    }
                    int idCust = int.Parse(r["CustomerId"].ToString());
                    var cust = (from b in bl.getCustomers().Where(y => y.Id == idCust).OrderBy(x => x.CustomerName).ToList() select new ListItem { Text = b.CustomerName + "-" + b.PhoneNo, Value = b.Id.ToString() }).ToList();
                    Util.loadCombo(ddlCustomer, cust, true);
                    Util.loadCombo(ddlDiscountedCustomer, cust, true);
                    ddlCustomer.SelectedIndex = ddlCustomer.Items.IndexOf(ddlCustomer.Items.FindByValue(r["CustomerId"].ToString()));
                    ddlDiscountedCustomer.SelectedIndex = ddlDiscountedCustomer.Items.IndexOf(ddlDiscountedCustomer.Items.FindByValue(r["DiscountCustomerId"].ToString()));
                    dteOrderDate.Text = DateTime.Parse(r["QuoteDate"].ToString()).ToString("yyyy-MM-dd");
                    txtDepositAmount.Text = r["Deposit"].ToString();
                    txtRemarks.Text = r["Remarks"].ToString();
                    isNew = false;
                }
                else if (e.CommandName == "del")
                {
                    loadData(lastSearchKey);
                    txtSalesId.Text = hf.Value;
                    int id = int.Parse(txtSalesId.Text);
                    var sl = bl.getSale(id);
                    if (sl.PaymentStatus >= 2)
                    {
                        lblError.Text = "You cannot delete an approved invoice";
                        return;
                    }
                    Toolkit.RunSqlCommand("delete from Sales where SalesId=" + hf.Value, conn);
                    loadData(lastSearchKey);
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            //Util.ShowMsgBox(this, ex.Message, "Error", MsgBoxType.Error);
        }
    }
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        loadData(lastSearchKey);
    }
    protected void chkDiscountedCustomer_CheckedChanged(object sender, EventArgs e)
    {
        if (chkDiscountedCustomer.Checked)
            ddlDiscountedCustomer.Enabled = true;
        else
        {
            ddlDiscountedCustomer.Enabled = false;
            ddlDiscountedCustomer.SelectedIndex = ddlCustomer.SelectedIndex;
        }
    }
    protected void ddlCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlDiscountedCustomer.SelectedIndex = ddlCustomer.SelectedIndex;
    }
    protected void txtSearchCustomer_TextChanged(object sender, EventArgs e)
    {
        var cust = (from b in bl.getCustomers().Where(x => x.CustomerName.Contains(txtSearchCustomer.Text)).OrderBy(x => x.CustomerName).ToList() select new ListItem { Text = b.CustomerName + "-" + b.PhoneNo, Value = b.Id.ToString() }).ToList();
        Util.loadCombo(ddlCustomer, cust, true);
        Util.loadCombo(ddlDiscountedCustomer, cust, true);
    }
    protected void txtSearchCustomer1_TextChanged(object sender, EventArgs e)
    {
        var cust = (from b in bl.getCustomers().Where(x => x.CustomerName.Contains(txtSearchCustomer1.Text)).OrderBy(x => x.CustomerName).ToList() select new ListItem { Text = b.CustomerName + "-" + b.PhoneNo, Value = b.Id.ToString() }).ToList();
        Util.loadCombo(ddlDiscountedCustomer, cust, true);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        loadData("SalesRef");
        txtSearchByName.Text = "";
        //txtSearchKey.Text = "";
    }
    protected void btnSearchByName_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        loadData("CustomerName");
        //txtSearchByName.Text = "";
        txtSearchKey.Text = "";
    }
    protected void btnSearchByInvoiceNo_Click(object sender, EventArgs e)
    {
        lblError.Text = "";
        loadData("InvoiceNo");
        //txtSearchByName.Text = "";
        txtSearchByInvoiceNo.Text = "";
    }
}