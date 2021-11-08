using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using bMobile.Shared;
using Bmat.Tools;
using System.Data;

namespace orion.ims
{
    public partial class SalesPayment : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            //   |Do not allow caching of page
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            lblError.Text = "";
            bl = new Bl(conn);
            if (!IsPostBack)
            {

                var py = (from b in bl.getPaymentModes().ToList() select new ListItem { Text = b.PaymentName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlpaymentmode, py, true);

                Util.LogAudit(this.Page, "Accessed Page", this.Title);

            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtSalesID.Text.Trim().Length == 0)
            {
                lblError.Text = "Unable to retrieve Sales ID. Contact the Vendor";
                return;
            }
            if (ddlPaymentType.SelectedItem.Text == "Deposit" && ddlpaymentmode.SelectedItem.Text == "Credit")
            {
                lblError.Text = "Deposit cannot be on credit";
                return;
            }
            if (ddlpaymentmode.SelectedValue == "-1" || ddlpaymentmode.SelectedIndex == -1)
            {
                lblError.Text = "Kindly select a valid Payment Mode";
                return;
            }
            try
            {
                int id = int.Parse(txtSalesID.Text);
                orion.ims.DAL.SalesPayment p = new DAL.SalesPayment
                    {
                        Amount = Convert.ToDecimal(txtamount.Text),
                        DocumentNo = txtdocno.Text,
                        PaymentDate = Convert.ToDateTime(getPOSValueDate(conn, Session["POSId"].ToString())),
                        PaymentModeId = Convert.ToInt16(ddlpaymentmode.SelectedValue),
                        SalesId = id,
                        UserId = int.Parse(Session["UserId"].ToString()),
                        PaymentType = ddlPaymentType.Text
                    };
                bl.saveSalespayment(p);
                if (ddlPaymentType.Text != "Deposit")
                {
                    if (ddlpaymentmode.SelectedItem.Text != "Credit")
                    {
                        //Flagging if the invoice is fully or partially paid
                        var q = bl.getSale(id);
                        if (q != null)
                        {
                            if (decimal.Parse(txtamount.Text) != decimal.Parse(txtBalance.Text))
                                q.PaymentStatus = 3;
                            else
                                q.PaymentStatus = 4;
                            bl.saveSales(q);
                        }
                    }
                    else
                    {
                        //Flagging if the invoice is fully or partially paid
                        var q = bl.getSale(id);
                        if (q != null)
                        {
                            q.PaymentStatus = 3;//Credit Payment is flagged as partially paid
                            bl.saveSales(q);
                        }
                    }
                }
                btnCancel_Click(null, null);

                if (ddlPaymentType.Text == "Deposit")
                {
                    txtInvoiceAmount.Text = "";
                    txtDepositAmount.Text = "";
                    txtAmountPaid.Text = "";
                    txtBalance.Text = "";
                    txtamount.Text = "";
                    //Loading previous payments
                    var dtPayments = Toolkit.dataDisplay(conn, "Select a.SalesPaymentId as Id, a.PaymentDate, b.PaymentName,a.Amount, c.FullNames, a.DocumentNo from SalesPayments a, PaymentModes b, Users c, Sales d where a.SalesId=d.SalesId and d.SalesRef='" + txtSearchKey.Text.Trim() + "' and a.PaymentModeId=b.ModeId and a.UserId=c.UserId");
                    gvPayment.DataSource = dtPayments;
                    gvPayment.DataBind();
                    ddlpaymentmode.SelectedIndex = -1;
                }
                else
                {
                    int sID = 0;
                    if (txtSearchKey.Text.Trim() != "")
                        searchBySalesRef();
                    else if (ddlinvoice.SelectedIndex != -1 && int.TryParse(ddlinvoice.SelectedValue, out sID))
                        loaddata();
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

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {


            }
            catch (Exception ex)
            {

                lblError.Text = ex.Message;
            }
        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loaddata();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtamount.Text = "";
            txtdocno.Text = "";
            ddlpaymentmode.SelectedIndex = -1;
        }

        private void loaddata()
        {
            try
            {
                int id = int.Parse(ddlinvoice.SelectedValue);
                txtSalesID.Text = ddlinvoice.SelectedValue;
                if (ddlPaymentType.Text == "Deposit")
                {
                    var sale = bl.getSale(id);
                    txtInvoiceAmount.Text = decimal.Parse(sale.Deposit.ToString()).ToString("#,###");
                    txtDepositAmount.Text = decimal.Parse(sale.Deposit.ToString()).ToString("#,###");
                    txtAmountPaid.Text = "0";
                    txtBalance.Text = decimal.Parse(sale.Deposit.ToString()).ToString("#,###");
                    txtamount.Text = decimal.Parse(sale.Deposit.ToString()).ToString("#,###");
                    if (txtBalance.Text == "")
                        txtBalance.Text = "0";
                    if (txtamount.Text == "")
                        txtamount.Text = "";
                    pnlDetails.Visible = true;

                }
                else
                {
                    if (ddlinvoice.SelectedIndex != -1)
                    {
                        //var p = bl.getSale(ddlinvoice.SelectedItem.Text);
                        var data = bl.getSalesDetails().Where(x => x.SalesId == id).ToList();
                        gvData.DataSource = data;
                        gvData.DataBind();
                        //Loading previous payments
                        var dtPayments = Toolkit.dataDisplay(conn, "Select a.SalesPaymentId as Id, a.PaymentDate, b.PaymentName,a.Amount, c.FullNames, a.DocumentNo from SalesPayments a, PaymentModes b, Users c where a.SalesId=" + ddlinvoice.SelectedValue + " and a.PaymentModeId=b.ModeId and a.UserId=c.UserId");
                        gvPayment.DataSource = dtPayments;
                        gvPayment.DataBind();
                        if (gvData.Rows.Count > 0)
                            pnlDetails.Visible = true;
                        else
                            pnlDetails.Visible = false;
                        //Loading Invoice Details
                        var salesInfo = Toolkit.dataDisplay(conn, "Select * from Vw_Sales_PaymentInfo where SalesId=" + ddlinvoice.SelectedValue);
                        if (salesInfo != null)
                        {
                            DataRow r = salesInfo.Rows[0];
                            txtInvoiceAmount.Text = decimal.Parse(r["AmountDue"].ToString()).ToString("#,###");
                            if (decimal.Parse(r["Deposit"].ToString()) == 0)
                                txtDepositAmount.Text = "0";
                            else
                                txtDepositAmount.Text = decimal.Parse(r["Deposit"].ToString()).ToString("#,###");
                            if (decimal.Parse(r["PaidAmount"].ToString()) == 0)
                                txtAmountPaid.Text = "0";
                            else
                                txtAmountPaid.Text = decimal.Parse(r["PaidAmount"].ToString()).ToString("#,###");
                            decimal bal = decimal.Parse(r["AmountDue"].ToString()) - decimal.Parse(r["PaidAmount"].ToString());
                            if (bal == 0)
                            {
                                txtBalance.Text = "0";
                                txtamount.Text = "0";
                            }
                            else
                            {
                                txtBalance.Text = Math.Round(bal, 0).ToString("#,###");
                                txtamount.Text = Math.Round(bal, 0).ToString("#,###");
                            }
                            if (txtBalance.Text == "")
                                txtBalance.Text = "0";
                            if (txtamount.Text == "")
                                txtamount.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void searchBySalesRef()
        {
            lblError.Text = "";
            try
            {
                if (ddlPaymentType.Text == "Deposit")
                {
                    //var sale = bl.getSale(txtSearchKey.Text.Trim().ToUpper());
                    var sale = Toolkit.dataDisplay(conn, "Select Deposit, SalesRef,SalesId from Sales a where a.SalesRef='" + txtSearchKey.Text.Trim().ToUpper() + "' and PaymentStatus =1 and POSId="+Session["POSId"].ToString()+" and SalesId not in (Select SalesId from SalesPayments where SalesId=a.SalesId)");
                    if (sale != null && sale.Rows.Count == 1)
                    {
                        DataRow r = sale.Rows[0];
                        txtInvoiceAmount.Text = decimal.Parse(r["Deposit"].ToString()).ToString("#,###");
                        txtDepositAmount.Text = decimal.Parse(r["Deposit"].ToString()).ToString("#,###");
                        txtAmountPaid.Text = "0";
                        txtBalance.Text = decimal.Parse(r["Deposit"].ToString()).ToString("#,###");
                        txtamount.Text = decimal.Parse(r["Deposit"].ToString()).ToString("#,###");
                        txtSalesID.Text = r["SalesId"].ToString();
                        if (txtBalance.Text == "")
                            txtBalance.Text = "0";
                        if (txtamount.Text == "")
                            txtamount.Text = "";
                        pnlDetails.Visible = true;

                    }
                    else
                    {
                        lblError.Text = "Unable to find this Order";
                        gvPayment.DataSource = null;
                        gvPayment.DataBind();
                    }
                }
                else
                {
                    //var p = bl.getSale(ddlinvoice.SelectedItem.Text);
                    var dt = Toolkit.dataDisplay(conn, "Select InvoiceNumber,SalesId from Sales where PaymentStatus in (2,3) and InvoiceNumber='"+txtSearchKey.Text+"' and POSId=" + Session["POSId"].ToString());
                    if (dt == null || dt.Rows.Count == 0)
                    {
                        lblError.Text = "Invoice either doesn't exist or is paid fully";
                        return;
                    }
                    var data = bl.getSalesDetails().Where(x => x.sale.InvoiceNumber == txtSearchKey.Text.Trim().ToUpper()).ToList();
                    gvData.DataSource = data;
                    gvData.DataBind();
                    //Loading previous payments
                    var dtPayments = Toolkit.dataDisplay(conn, "Select a.SalesPaymentId as Id, a.PaymentDate, b.PaymentName,a.Amount, c.FullNames, a.DocumentNo from SalesPayments a, PaymentModes b, Users c, Sales d where a.SalesId=d.SalesId and d.InvoiceNumber='" + txtSearchKey.Text.Trim() + "' and a.PaymentModeId=b.ModeId and a.UserId=c.UserId");
                    gvPayment.DataSource = dtPayments;
                    gvPayment.DataBind();
                    if (gvData.Rows.Count > 0)
                        pnlDetails.Visible = true;
                    else
                        pnlDetails.Visible = false;
                    //Loading Invoice Details
                    var salesInfo = Toolkit.dataDisplay(conn, "Select * from Vw_Sales_PaymentInfo where POSId="+Session["POSId"].ToString()+" and InvoiceNumber='" + txtSearchKey.Text.Trim() + "'");
                    if (salesInfo != null)
                    {
                        DataRow r = salesInfo.Rows[0];
                        txtInvoiceAmount.Text = decimal.Parse(r["AmountDue"].ToString()).ToString("#,###");
                        if (decimal.Parse(r["Deposit"].ToString()) == 0)
                            txtDepositAmount.Text = "0";
                        else
                            txtDepositAmount.Text = decimal.Parse(r["Deposit"].ToString()).ToString("#,###");
                        if (decimal.Parse(r["PaidAmount"].ToString()) == 0)
                            txtAmountPaid.Text = "0";
                        else
                            txtAmountPaid.Text = decimal.Parse(r["PaidAmount"].ToString()).ToString("#,###");
                        decimal bal = decimal.Parse(r["AmountDue"].ToString()) - decimal.Parse(r["PaidAmount"].ToString());
                        if (bal == 0)
                        {
                            txtBalance.Text = "0";
                            txtamount.Text = "0";
                        }
                        else
                        {
                            txtBalance.Text = Math.Round(bal, 0).ToString("#,###");
                            txtamount.Text = Math.Round(bal, 0).ToString("#,###");
                        }
                        if (txtBalance.Text == "")
                            txtBalance.Text = "0";
                        if (txtamount.Text == "")
                            txtamount.Text = "";
                        txtSalesID.Text = r["SalesId"].ToString();
                    }
                    else
                    {
                        lblError.Text = "Unable to find this Invoice";
                        gvPayment.DataSource = null;
                        gvPayment.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }



        protected void ddlinvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlinvoice.SelectedIndex > -1 && ddlinvoice.SelectedItem.Text != "Select Invoice")
                loaddata();
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
        protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentType.Text == "Deposit")
            {
                lblSearchKey.Text = "Search by Order No.:";
                lblPaymentDescription.Text = "Order Number: ";
                var dt = Toolkit.dataDisplay(conn, "Select SalesRef,SalesId from Sales a where POSId=" + Session["POSId"].ToString() + " and PaymentStatus in (1) and SalesId not in (Select SalesId from SalesPayments where SalesId=a.SalesId)");
                //var pos = (from b in bl.getSales().ToList().Where(x=>x.PaymentStatus==2 || x.PaymentStatus==3) select new ListItem { Text = b.InvoiceNumber, Value = b.Id.ToString() }).ToList();//status=2: Invoice, 3: Partially Paid
                //Util.loadCombo(ddlinvoice, dt, true);
                ddlinvoice.DataSource = dt;
                ddlinvoice.DataValueField = "SalesID";
                ddlinvoice.DataTextField = "SalesRef";
                ddlinvoice.DataBind();
                ddlinvoice.Items.Add("Select Order No.");
                ddlinvoice.SelectedIndex = ddlinvoice.Items.Count - 1;
            }
            else
            {
                lblSearchKey.Text = "Search by Invoice No.:";
                lblPaymentDescription.Text = "Invoice Number: ";
                var dt = Toolkit.dataDisplay(conn, "Select InvoiceNumber,SalesId from Sales where PaymentStatus in (2,3) and POSId=" + Session["POSId"].ToString());
                //var pos = (from b in bl.getSales().ToList().Where(x=>x.PaymentStatus==2 || x.PaymentStatus==3) select new ListItem { Text = b.InvoiceNumber, Value = b.Id.ToString() }).ToList();//status=2: Invoice, 3: Partially Paid
                //Util.loadCombo(ddlinvoice, dt, true);
                ddlinvoice.DataSource = dt;
                ddlinvoice.DataValueField = "SalesID";
                ddlinvoice.DataTextField = "InvoiceNumber";
                ddlinvoice.DataBind();
                ddlinvoice.Items.Add("Select Invoice");
                ddlinvoice.SelectedIndex = ddlinvoice.Items.Count - 1;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearchKey.Text.Trim() != "")
                searchBySalesRef();
        }
    }
}