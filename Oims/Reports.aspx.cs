using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web;
using System.IO;
using System.Data;

public partial class Reports : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Reports Section
            SqlConnection con = new SqlConnection(conn);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dset = new DataSet();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            adapter.SelectCommand = cmd;
            string reportName = Request.Params["report"].ToString();
            ReportDocument rpt = new ReportDocument();
            if (reportName == "quote")
            {
                string id = Request.Params["id"].ToString();
                if (id == null)
                {
                    lblMessage.Text = "Unable to pass the Sales ID.";
                    return;
                }
                cmd.CommandText = "select * from Vw_Sale where SalesId=" + id;
                adapter.Fill(dset, "Vw_Sale");
                rpt.FileName = Server.MapPath("~/Reports/rptSale.rpt");
            }
            else if (reportName == "CashCollection")
            {
                string id = Request.Params["id"].ToString();
                if (id == null)
                {
                    lblMessage.Text = "Unable to pass the Cash Collection ID.";
                    return;
                }
                cmd.CommandText = "select * from Vw_Cash_Collection where CashCollectionId=" + id;
                adapter.Fill(dset, "Vw_Cash_Collection");
                rpt.FileName = Server.MapPath("~/Reports/rptCashCollection.rpt");
            }
            else if (reportName == "PaymentNote")
            {
                string id = Request.Params["id"].ToString();
                if (id == null)
                {
                    lblMessage.Text = "Unable to pass the Sale Payment ID.";
                    return;
                }
                cmd.CommandText = "select * from Vw_Sales_Payment where SalesPaymentId=" + id;
                adapter.Fill(dset, "Vw_Sales_Payment");
                rpt.FileName = Server.MapPath("~/Reports/rptPaymentNote.rpt");
            }
            else if (reportName == "DeliveryNote")
            {
                string id = Request.Params["id"].ToString();
                if (id == null)
                {
                    lblMessage.Text = "Unable to pass the Delivery Note ID.";
                    return;
                }
                cmd.CommandText = "select * from Vw_Delivery_Note where OrderId=" + id;
                adapter.Fill(dset, "Vw_Delivery_Note");
                rpt.FileName = Server.MapPath("~/Reports/rptDeliveryNote.rpt");
            }
            else if (reportName == "ReceiptNote")
            {
                string id = Request.Params["id"].ToString();
                if (id == null)
                {
                    lblMessage.Text = "Unable to pass the Receipt Note ID.";
                    return;
                }
                cmd.CommandText = "select * from Vw_Receipt_Note where OrderId=" + id;
                adapter.Fill(dset, "Vw_Receipt_Note");
                rpt.FileName = Server.MapPath("~/Reports/rptReceiptNote.rpt");
            }
            else if (reportName == "SalesActivities")
            {
                string startdate = Request.Params["startdate"].ToString();
                string enddate = Request.Params["enddate"].ToString();
                //string posid = Request.Params["POS"].ToString();
                if (startdate == null && enddate==null)
                {
                    lblMessage.Text = "Unable to pass empty data.";
                    return;
                }
                cmd.CommandText = "select '" + startdate + "' as StartDate, '" + enddate + "' as EndDate,* from Vw_DailyActivities_onSale where Date between '" + startdate + "'" + " and '" + enddate + "'" + " order by Date desc";
                adapter.Fill(dset, "Vw_DailyActivities_onSale");
                rpt.FileName = Server.MapPath("~/Reports/rptSalesActivities.rpt");
            }
            else if (reportName == "SalesByPOS")
            {
                string startdate = Request.Params["startdate"].ToString();
                string enddate = Request.Params["enddate"].ToString();
                string posid = Request.Params["POS"].ToString();
                if (startdate == null && enddate == null && posid==null)
                {
                    lblMessage.Text = "Unable to pass empty data.";
                    return;
                }
                cmd.CommandText = "select '" + startdate + "' as StartDate, '" + enddate + "' as EndDate,* from Vw_DailyActivities_onSale where Date between '" + startdate + "'" + " and '" + enddate + "'" + "and POSID ='" + posid + "'" + " order by Date desc";
                adapter.Fill(dset, "Vw_DailyActivities_onSale");
                rpt.FileName = Server.MapPath("~/Reports/rptSalesonPOS.rpt");
            }
            else if (reportName == "PaymentReport")
            {
                string startdate = Request.Params["startdate"].ToString();
                string enddate = Request.Params["enddate"].ToString();
                string depositFlag = Request.Params["depositFlag"].ToString();
                //string posid = Request.Params["POS"].ToString();
                if (startdate == null && enddate == null)
                {
                    lblMessage.Text = "Unable to pass empty data.";
                    return;
                }
                cmd.CommandText = "select 'GLOBAL PAYMENT REPORT' AS ReportTitle,'" + startdate + "' as StartDate, '" + enddate + "' as EndDate,* from Vw_PeriodicalPayment where PaymentDate between '" + startdate + "'" + " and '" + enddate + "'"+(depositFlag=="1"?" and PaymentType='Deposit' ":"")+" order by PaymentDate desc";
                adapter.Fill(dset, "Vw_PeriodicalPayment");
                rpt.FileName = Server.MapPath("~/Reports/rptPaymentReport.rpt");
            }
            //else if (reportName == "DepositPaymentReport")
            //{
            //    string startdate = Request.Params["startdate"].ToString();
            //    string enddate = Request.Params["enddate"].ToString();
            //    //string posid = Request.Params["POS"].ToString();
            //    if (startdate == null && enddate == null)
            //    {
            //        lblMessage.Text = "Unable to pass empty data.";
            //        return;
            //    }
            //    cmd.CommandText = "select 'DEPOSIT PAYMENT REPORT' AS ReportTitle," + startdate + "' as StartDate, '" + enddate + "' as EndDate,* from Vw_PeriodicalPayment where PaymentDate between '" + startdate + "'" + " and '" + enddate + "' and PaymentType='Deposit' " + " order by PaymentDate desc";
            //    adapter.Fill(dset, "Vw_PeriodicalPayment");
            //    rpt.FileName = Server.MapPath("~/Reports/rptPaymentReport.rpt");
            //}
            //else if (reportName == "CreditSaleReport")
            //{
            //    string startdate = Request.Params["startdate"].ToString();
            //    string enddate = Request.Params["enddate"].ToString();
            //    //string posid = Request.Params["POS"].ToString();
            //    if (startdate == null && enddate == null)
            //    {
            //        lblMessage.Text = "Unable to pass empty data.";
            //        return;
            //    }
            //    cmd.CommandText = "select 'SALES ON CREDIT REPORT' AS ReportTitle," + startdate + "' as StartDate, '" + enddate + "' as EndDate,* from Vw_PeriodicalPayment where PaymentDate between '" + startdate + "'" + " and '" + enddate + "' and PaymentType='Credit' " + " order by PaymentDate desc";
            //    adapter.Fill(dset, "Vw_PeriodicalPayment");
            //    rpt.FileName = Server.MapPath("~/Reports/rptPaymentReport.rpt");
            //}
            else if (reportName == "PaymentByPOS")
            {
                string startdate = Request.Params["startdate"].ToString();
                string enddate = Request.Params["enddate"].ToString();
                string posid = Request.Params["POS"].ToString();
                string depositFlag = Request.Params["depositFlag"].ToString();

                if (startdate == null && enddate == null)
                {
                    lblMessage.Text = "Unable to pass empty data.";
                    return;
                }
                cmd.CommandText = "select '" + startdate + "' as StartDate, '" + enddate + "' as EndDate,* from Vw_PeriodicalPayment where PaymentDate between '" + startdate + "'" + " and '" + enddate + "'" + "and POSID ='" + posid + "'" + (depositFlag == "1" ? " and PaymentType='Deposit' " : "") + " order by PaymentDate desc";
                adapter.Fill(dset, "Vw_PeriodicalPayment");
                rpt.FileName = Server.MapPath("~/Reports/rptPaymentByPOS.rpt");
            }

            else if (reportName == "PaymentByPOSandPmode")
            {
                string startdate = Request.Params["startdate"].ToString();
                string enddate = Request.Params["enddate"].ToString();
                string posid = Request.Params["POS"].ToString();
                string modid = Request.Params["Paymode"].ToString();
                string depositFlag = Request.Params["depositFlag"].ToString();

                if (startdate == null && enddate == null && posid == null && modid == null)
                {
                    lblMessage.Text = "Unable to pass empty data.";
                    return;
                }
                cmd.CommandText = "select '" + startdate + "' as StartDate, '" + enddate + "' as EndDate,* from Vw_PeriodicalPayment where PaymentDate between '" + startdate + "'" + " and '" + enddate + "'" + "and POSID ='" + posid + "'" + "and ModeId= " + modid + (depositFlag=="1"?" and PaymentType='Deposit' ":"")+" order by PaymentDate desc";
                adapter.Fill(dset, "Vw_PeriodicalPayment");
                rpt.FileName = Server.MapPath("~/Reports/rptPaymentByPOS.rpt");
            }
            else if (reportName == "PaymentBymode")
            {
                string startdate = Request.Params["startdate"].ToString();
                string enddate = Request.Params["enddate"].ToString();
                string modid = Request.Params["Paymode"].ToString();
                string depositFlag = Request.Params["depositFlag"].ToString();

                if (startdate == null && enddate == null && modid == null)
                {
                    lblMessage.Text = "Unable to pass empty data.";
                    return;
                }
                cmd.CommandText = "select '" + startdate + "' as StartDate, '" + enddate + "' as EndDate,* from Vw_PeriodicalPayment where PaymentDate between '" + startdate + "'" + " and '" + enddate + "'" + "and ModeId= " + modid + (depositFlag == "1" ? " and PaymentType='Deposit' " : "") + " order by PaymentDate desc";
                adapter.Fill(dset, "Vw_PeriodicalPayment");
                rpt.FileName = Server.MapPath("~/Reports/rptPaymentReport.rpt");
            }
            else if (reportName == "ProductsReport")
            {
                cmd.CommandText = "select * from Vw_Products  order by ProductName desc";
                adapter.Fill(dset, "Vw_Products");
                rpt.FileName = Server.MapPath("~/Reports/rptProducts.rpt");
            }
            else if (reportName == "CustomersReport")
            {
                cmd.CommandText = "select * from Vw_Customers  order by CustomerName asc";
                adapter.Fill(dset, "Vw_Customers");
                rpt.FileName = Server.MapPath("~/Reports/rptCustomers.rpt");
            }
            else if (reportName == "customerdeliverynote")
            {
                string id = Request.Params["id"].ToString();
                if (id == null)
                {
                    lblMessage.Text = "Unable to pass the Delivery Note ID.";
                    return;
                }
                cmd.CommandText = "select * from Vw_Item_Delivery_Report where BatchNo='" + id+"'";
                adapter.Fill(dset, "Vw_Item_Delivery_Report");
                rpt.FileName = Server.MapPath("~/Reports/rptCustomerDeliveryNote.rpt");
            }
            rpt.SetDataSource(dset);
            System.IO.Stream oStream = null;
            byte[] byteArray = null;
            oStream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            byteArray = new byte[oStream.Length];
            oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(byteArray);
            Response.Flush();
            Response.Close();
            rpt.Close();
            rpt.Dispose();
        }
    }
}