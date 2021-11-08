using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.BL;
using orion.ims.DAL;
using System.Data.SqlClient;
using System.Data;
using bMobile.Shared;
using Bmat.Tools;


public partial class PaymentsReport : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        bl = new Bl(conn);
        lblError.Text = "";
        if (!IsPostBack)
        {
            var pos = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlPOS, pos, true);

            var paymode = (from b in bl.getPaymentModes().ToList() select new ListItem { Text = b.PaymentName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlpaymode, paymode, true);


        }
        btnAction.Visible = false;
       
    }
    private void loaddata()
    {
        DataTable data = new DataTable();
        
        if (chkbxAll.Checked == true && chkpymnt.Checked == true)
        {
            if(txtSearchByName.Text.Trim()!="")
                data = Toolkit.dataDisplay(conn, "select * from Vw_PeriodicalPayment where CustomerName like '%"+txtSearchByName.Text.Trim()+"%' and Paymentdate between '" + txtstartdate.Text + "'" + " and '" + txtendDate.Text + "'" + (chkDeposit.Checked ? " and PaymentType='Deposit'" : "") + " order by PaymentDate desc");
            else
            data = Toolkit.dataDisplay(conn, "select * from Vw_PeriodicalPayment where Paymentdate between '" + txtstartdate.Text + "'" + " and '" + txtendDate.Text + "'" + (chkDeposit.Checked ? " and PaymentType='Deposit'" : "") + " order by PaymentDate desc");
            gvData.DataSource = data;
            gvData.DataBind();
        }
        else if (chkbxAll.Checked == true && chkpymnt.Checked == false)
        {
            if (txtSearchByName.Text.Trim() != "")
                data = Toolkit.dataDisplay(conn, "select * from Vw_PeriodicalPayment where  CustomerName like '%" + txtSearchByName.Text.Trim() + "%' and Paymentdate between '" + txtstartdate.Text + "'" + " and'" + txtendDate.Text + "'" + "and ModeId ='" + ddlpaymode.SelectedValue + "'" + (chkDeposit.Checked ? " and PaymentType='Deposit'" : "") + " order by PaymentDate desc");
            else
            data = Toolkit.dataDisplay(conn, "select * from Vw_PeriodicalPayment where Paymentdate between '" + txtstartdate.Text + "'" + " and'" + txtendDate.Text + "'" + "and ModeId ='" + ddlpaymode.SelectedValue + "'" + (chkDeposit.Checked?" and PaymentType='Deposit'":"") +" order by PaymentDate desc");
            gvData.DataSource = data;
            gvData.DataBind();
        }
        else if (chkbxAll.Checked == false && chkpymnt.Checked == true)
        {
            if (txtSearchByName.Text.Trim() != "")
                data = Toolkit.dataDisplay(conn, "select * from Vw_PeriodicalPayment where  CustomerName like '%" + txtSearchByName.Text.Trim() + "%' and  Paymentdate between '" + txtstartdate.Text + "'" + " and'" + txtendDate.Text + "'" + "and POSID ='" + ddlPOS.SelectedValue + "'" + (chkDeposit.Checked ? " and PaymentType='Deposit'" : "") + " order by PaymentDate desc");
            else
                data = Toolkit.dataDisplay(conn, "select * from Vw_PeriodicalPayment where Paymentdate between '" + txtstartdate.Text + "'" + " and'" + txtendDate.Text + "'" + "and POSID ='" + ddlPOS.SelectedValue + "'" + (chkDeposit.Checked ? " and PaymentType='Deposit'" : "") + " order by PaymentDate desc");
            gvData.DataSource = data;
            gvData.DataBind();
        }
        else
        {
            if (txtSearchByName.Text.Trim() != "")
                data = Toolkit.dataDisplay(conn, "select * from Vw_PeriodicalPayment where   CustomerName like '%" + txtSearchByName.Text.Trim() + "%' and  Paymentdate between '" + txtstartdate.Text + "'" + " and'" + txtendDate.Text + "'" + "and ModeId ='" + ddlpaymode.SelectedValue + "'" + "and POSID ='" + ddlPOS.SelectedValue + "'" + (chkDeposit.Checked ? " and PaymentType='Deposit'" : "") + " order by PaymentDate desc");
            else
                data = Toolkit.dataDisplay(conn, "select * from Vw_PeriodicalPayment where Paymentdate between '" + txtstartdate.Text + "'" + " and'" + txtendDate.Text + "'" + "and ModeId ='" + ddlpaymode.SelectedValue + "'" + "and POSID ='" + ddlPOS.SelectedValue + "'" + (chkDeposit.Checked ? " and PaymentType='Deposit'" : "") + " order by PaymentDate desc");
            gvData.DataSource = data;
            gvData.DataBind();
        }
    }
    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        loaddata();
        if (gvData.Rows.Count > 0)
        {
            btnAction.Visible = true;
        }
    }
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        loaddata();
        btnAction.Visible = true;
    }
    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        string depositFlag = "0";
        if (chkDeposit.Checked == true)
            depositFlag = "1";
        if (txtstartdate.Text != "" && txtendDate.Text != "" && chkbxAll.Checked == true && chkpymnt.Checked==true)
        {
            Response.Redirect("Reports.aspx?report=PaymentReport&depositFlag=" + depositFlag + "&startdate=" + txtstartdate.Text.Trim() + "&enddate=" + txtendDate.Text.Trim());
        }
         if (txtstartdate.Text != "" && txtendDate.Text != "" && chkbxAll.Checked == false && chkpymnt.Checked == true)
        {
            Response.Redirect("Reports.aspx?report=PaymentByPOS&depositFlag=" + depositFlag + "&startdate=" + txtstartdate.Text.Trim() + "&enddate=" + txtendDate.Text.Trim() + "&POS=" + ddlPOS.SelectedValue);

        }
         if (txtstartdate.Text != "" && txtendDate.Text != "" && chkbxAll.Checked == false && chkpymnt.Checked == false)
        {
            Response.Redirect("Reports.aspx?report=PaymentByPOSandPmode&depositFlag=" + depositFlag + "&startdate=" + txtstartdate.Text.Trim() + "&enddate=" + txtendDate.Text.Trim() + "&POS=" + ddlPOS.SelectedValue + "&Paymode=" + ddlpaymode.SelectedValue);

        }
         if (txtstartdate.Text != "" && txtendDate.Text != "" && chkbxAll.Checked == true && chkpymnt.Checked == false)
         {
             Response.Redirect("Reports.aspx?report=PaymentBymode&depositFlag=" + depositFlag + "&startdate=" + txtstartdate.Text.Trim() + "&enddate=" + txtendDate.Text.Trim() + "&Paymode=" + ddlpaymode.SelectedValue);

         }
    }
}