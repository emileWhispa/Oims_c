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

public partial class QuotationItems : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    static string defaultDiscountRate= "0";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            bl = new Bl(conn);
            //   |Do not allow caching of page
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    txtSaleId.Text = Request.QueryString["id"];
                    int id = int.Parse(txtSaleId.Text);
                    refreshHeader();
                    loadSaleItems();
                    //var cust = (from b in bl.getCustomers().ToList() select new ListItem { Text = b.CustomerName, Value = b.Id.ToString() }).ToList();
                    Util.LogAudit(this.Page, "Accessed Page", this.Title);
                    var prod = (from c in bl.getProducts().OrderBy(y => y.ProductName).ToList() select new ListItem { Text = c.ProductName, Value = c.Id.ToString() }).ToList();
                    Util.loadCombo(ddlProduct, prod, true);
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void refreshHeader()
    {
        DataTable dt = new DataTable();
        dt = Toolkit.dataDisplay(conn, "Select a.SalesRef, b.CustomerName, case a.PaymentStatus when 0 then 'Quotation' when 1 then 'Order' else 'Invoice' end [Type], a.DiscountCustomerId,b.PhoneNo from Sales a, Customers b where a.SalesId=" + txtSaleId.Text + " and a.CustomerID=b.CustomerID");
        if (dt != null && dt.Rows.Count == 1)
        {
            txtCustomerName.Text = dt.Rows[0].ItemArray.GetValue(1).ToString();
            txtPhone.Text = dt.Rows[0].ItemArray.GetValue(4).ToString();
            txtQuotationNo.Text = dt.Rows[0].ItemArray.GetValue(0).ToString();
            lblText.Text = dt.Rows[0].ItemArray.GetValue(2).ToString();
            btnApprove.Text = "Approve " + lblText.Text;
            if (lblText.Text == "Invoice")
                btnApprove.Enabled = false;
            else if (lblText.Text == "Order")
                pnlSDC.Visible = true;
            //Getting discount information
            string disc = "0";
            dt = Toolkit.dataDisplay(conn, "Select DiscountRate from Discounts where CustomerId=" + dt.Rows[0].ItemArray.GetValue(3).ToString());
            if (dt != null && dt.Rows.Count == 1)
            {
                if (dt.Rows[0].ItemArray.GetValue(0).ToString().Length > 0)
                    disc = dt.Rows[0].ItemArray.GetValue(0).ToString();
            }
            txtDiscountRate.Text = disc;
            defaultDiscountRate = disc;
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

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            string tranDate = getPOSValueDate(conn, Session["POSId"].ToString());
            if (lblText.Text == "Quotation")//confirm quotation
            {
                Toolkit.RunSqlCommand("Update Sales set OrderDate='"+tranDate+"',ApproverId=" + Session["UserId"].ToString() + ", PaymentStatus=1 where SalesId=" + txtSaleId.Text, conn);
                refreshHeader();
            }
            else if (lblText.Text == "Order")
            {
                //Check if SDC info is captured
                if (txtSDCNo.Text.Trim() == "" || txtSDCReceiptNo.Text.Trim() == "")
                {
                    lblError.Text = "You must enter EBM Information required";
                    return;
                }
                string invoiceNumber=GenerateInvoice();
                string balance="0";
                string status="";
                Toolkit.RunSqlCommand("Update Sales set InvoiceDate='" + tranDate + "',ApproverId=" + Session["UserId"].ToString() + ", InvoiceNumber='" + invoiceNumber + "', PaymentStatus=2, SDCNo='" + txtSDCNo.Text.Replace("'", "''").Trim() + "', SDCReceiptNo='"+txtSDCReceiptNo.Text.Replace("'","''").Trim()+"' where SalesId=" + txtSaleId.Text, conn);
                refreshHeader();
                //SMS Section
                string phone = "";
                if (txtPhone.Text.Trim().Length > 8 && txtPhone.Text.Trim().Length<11)
                {
                    phone = txtPhone.Text.Trim().Length == 9 ? "250" + txtPhone.Text.Trim() : "25" + txtPhone.Text.Trim();
                    SMSAPI.ksms x = new SMSAPI.ksms();
                    x.ksend("rwandafoam", "6a8k4x4k9b0e", "RWANDAFOAM", "Murakaza neza muri RWANDAFOAM. Inyemezabuguzi yanyu No. " + invoiceNumber + " imaze gukorwa. Tubashimiye icyizere mukomeje kutugirira", phone, out balance, out status);
                    Toolkit.RunSqlCommand("exec sp_SMS_Log '" + txtCustomerName.Text.Trim().Replace("'", "''") + "','" + invoiceNumber + "','" + status + "'," + balance, conn);
                }
          }
            //refreshHeader();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private string GenerateInvoice()
    {
        string ReceiptNo = "";
        string sSql = "Sp_GenerateInvoice " +Session["UserId"].ToString();
        var con = new SqlConnection(conn);
        var tcmd = new SqlCommand(sSql, con);
        tcmd.Connection.Open();
        var retval = tcmd.ExecuteScalar();
        if (retval != null)
            ReceiptNo = retval.ToString();
        return ReceiptNo; ;
    }

    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int prodId = 0;
            if (ddlProduct.SelectedIndex != -1)
            {
                prodId = int.Parse(ddlProduct.SelectedValue);
                var price = bl.getProduct(prodId);
                txtUnitPrice.Text = price.SellingPrice.ToString("#.##");
                txtManualDescr.Text = price.Description;
            }
        }
        catch (Exception ex)
        {
            Util.ShowMsgBox(this, ex.Message, "Error while saving", MsgBoxType.Error);
        }
    }
    protected void rdioVatable_CheckedChanged(object sender, EventArgs e)
    {
        if (rdioVatable.Checked)
            txtVATRate.Text = "18.00";
        else
            txtVATRate.Text = "0.00";
    }
    protected void btnSaveItem_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtSaleDetailId.Text.Trim() == "")//New record
            {
                orion.ims.DAL.SalesDetail p = new orion.ims.DAL.SalesDetail
                {
                    ProductId = int.Parse(ddlProduct.SelectedValue),
                    SalesId = int.Parse(txtSaleId.Text),
                    Quantity = decimal.Parse(txtQuantity.Text),
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    Discounts = (chkNoDiscount.Checked?0:decimal.Parse(txtDiscountRate.Text)),
                    Vatable = (rdioVatable.Checked ? 1 : 0),
                    vatrate = decimal.Parse(txtVATRate.Text),
                    ManualDescr = txtManualDescr.Text
                };
                bl.saveSalesDetail(p);
            }
            else//update
            {
                int id = int.Parse(txtSaleDetailId.Text);
                var items = bl.getSalesDetail(id);
                items.ProductId = int.Parse(ddlProduct.SelectedValue);
                items.SalesId = int.Parse(txtSaleId.Text);
                items.Quantity = decimal.Parse(txtQuantity.Text);
                items.UnitPrice = decimal.Parse(txtUnitPrice.Text);
                items.Discounts = (chkNoDiscount.Checked ? 0 : decimal.Parse(txtDiscountRate.Text));
                items.Vatable = (rdioVatable.Checked ? 1 : 0);
                items.vatrate = decimal.Parse(txtVATRate.Text);
                items.ManualDescr = txtManualDescr.Text;
                bl.saveSalesDetail(items);
            }
            clearSaleDetailsControls();
            loadSaleItems();
            //Util.ShowMsgBox(this, "Production Created successfully", "Production", MsgBoxType.Info);
            //btnCancel_Click(null, null);
            //loadData();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void clearSaleDetailsControls()
    {
        refreshHeader();
        ddlProduct.DataSource = null;
        ddlProduct.DataBind();
        txtSearchItem.Text = "";
        txtQuantity.Text = "";
        txtUnitPrice.Text = "";
        txtVATRate.Text = "18.00";
        rdioVatable.Checked = true;
        txtManualDescr.Text = "";
        chkNoDiscount.Checked = false;
    }

    private void loadSaleItems()
    {
        gvData.DataSource = Toolkit.dataDisplay(conn, "Select a.SalesDetailsId as Id, b.ProductName, a.Quantity, a.Discounts, a.vat_rate as vatrate,a.UnitPrice,a.Vatable,a.Manualdescr from SalesDetails a, Products b where a.ProductId=b.ProductId and a.SalesId=" + txtSaleId.Text);
        gvData.DataBind();
        if (gvData.Rows.Count > 0)
        {
            btnApprove.Visible = true;
            btnPrint.Visible = true;
        }
        else
        {
            btnApprove.Visible = false;
            btnPrint.Visible = false;
        }
    }
    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            if (e.CommandName == "sel")
            {
                loadSaleItems();
                int rowId = Convert.ToInt32(e.CommandArgument);
                var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                txtSaleDetailId.Text = hf.Value;
                int id = int.Parse(txtSaleDetailId.Text);
                var x = bl.getSalesDetail(id);
                var sale = bl.getSale(x.SalesId);
                if (sale.PaymentStatus > 1)
                {
                    lblError.Text="You cannot edit an approved invoice";
                    return;
                }
                var prod = (from c in bl.getProducts().OrderBy(y => y.ProductName).ToList() select new ListItem { Text = c.ProductName, Value = c.Id.ToString() }).ToList();
                Util.loadCombo(ddlProduct, prod, true);
                ddlProduct.SelectedIndex = ddlProduct.Items.IndexOf(ddlProduct.Items.FindByValue(x.ProductId.ToString()));
                txtQuantity.Text = x.Quantity.ToString("#.##");
                txtManualDescr.Text = x.ManualDescr;
                txtUnitPrice.Text = x.UnitPrice.ToString("#.##");
                txtDiscountRate.Text = x.Discounts.ToString("#.##");
                txtVATRate.Text = x.vatrate.ToString("#.##");
                rdioVatable.Checked = (x.Vatable == 1 ? true : false);
            }
            else if (e.CommandName == "del")
            {
                loadSaleItems();
                int rowId = Convert.ToInt32(e.CommandArgument);
                var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                txtSaleDetailId.Text = hf.Value;
                int id = int.Parse(txtSaleDetailId.Text);
                var c = bl.getSalesDetail(id);
                var sale = bl.getSale(c.SalesId);
                if (sale.PaymentStatus > 1)
                {
                    lblError.Text = "You cannot delete an approved invoice";
                    return;
                }
                if (c != null)
                {
                    bl.deleteSalesDetail(id);
                    Util.ShowMsgBox(this, "Sale Item Deleted Succesfully", "Sale Item Deletion", MsgBoxType.Info);
                }
                loadSaleItems();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            //Util.ShowMsgBox(this, ex.Message, "Error", MsgBoxType.Error);
        }
    }

    protected void btnCancelSaleDetails_Click(object sender, EventArgs e)
    {
        clearSaleDetailsControls();
    }
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        loadSaleItems();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (txtSaleId.Text.Trim() != "")
            Response.Redirect("Reports.aspx?report=quote&id=" + txtSaleId.Text.Trim());
    }
    protected void txtSearchItem_TextChanged(object sender, EventArgs e)
    {
        var prod = (from c in bl.getProducts().Where( x => x.ProductName.Contains(txtSearchItem.Text)).OrderBy(x => x.ProductName).ToList() select new ListItem { Text = c.ProductName, Value = c.Id.ToString() }).ToList();
        Util.loadCombo(ddlProduct, prod, true);
    }
    protected void chkNoDiscount_CheckedChanged(object sender, EventArgs e)
    {
        if (chkNoDiscount.Checked)
            txtDiscountRate.Text = "0";
        else
        {
            txtDiscountRate.Text = defaultDiscountRate;
        }
    }
}