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
using System.Data.SqlClient;

public partial class InvoiceCancellation : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
         bl = new Bl(conn);
         if (!IsPostBack)
         {
             //DataTable dt = new DataTable();
             var dt = Toolkit.dataDisplay(conn, "Select InvoiceNumber,SalesId from Sales where PaymentStatus=2 and POSId=" + Session["POSId"].ToString());
             //var pos = (from b in bl.getSales().ToList().Where(x=>x.PaymentStatus==2 || x.PaymentStatus==3) select new ListItem { Text = b.InvoiceNumber, Value = b.Id.ToString() }).ToList();//status=2: Invoice, 3: Partially Paid
             //Util.loadCombo(ddlinvoice, dt, true);
             ddlinvoice.DataSource = dt;
             ddlinvoice.DataValueField = "SalesID";
             ddlinvoice.DataTextField = "InvoiceNumber";
             ddlinvoice.DataBind();
             ddlinvoice.Items.Add("Select Invoice");
             ddlinvoice.SelectedIndex = ddlinvoice.Items.Count - 1;
         }
         btnAction.Visible = false;
         txtcomments.Visible = false;

    }
    private void loaddata()
    {
        try
        {
            if (ddlinvoice.SelectedIndex != -1)
            {
                //var p = bl.getSale(ddlinvoice.SelectedItem.Text);
                int id = int.Parse(ddlinvoice.SelectedValue);
                var data = bl.getSalesDetails().Where(x => x.SalesId == id).ToList();
                gvData.DataSource = data;
                gvData.DataBind();
                if (gvData.Rows.Count > 0)
                {
                   
                    btnAction.Visible = true;
                    txtcomments.Visible = true;
                }
                else
                {
                    btnAction.Visible = false;
                    txtcomments.Visible = false;
                    txtcomments.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    //private void loadPayments()
    //{
    //    //Loading previous payments
    //    var dtPayments = Toolkit.dataDisplay(conn, "Select a.SalesPaymentId as Id, a.PaymentDate, b.PaymentName,a.Amount, c.FullNames, a.DocumentNo from SalesPayments a, PaymentModes b, Users c where a.SalesId=" + ddlinvoice.SelectedValue + " and a.PaymentModeId=b.ModeId and a.UserId=c.UserId");
    //    gvPayment.DataSource = dtPayments;
    //    gvPayment.DataBind();
    //}

    protected void ddlinvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvData.DataSource = null;
        gvData.DataBind();
        btnAction.Visible = false;
        txtcomments.Visible = false;
        txtcomments.Text = "";
        if (ddlinvoice.SelectedIndex >= 0 && ddlinvoice.SelectedItem.Text != "Select Invoice")
        {
            txtInvoiceNo.Text = ddlinvoice.SelectedItem.Text;
            loaddata();
            //loadPreviousDeliveries();
            //loadPayments();
        }
    }

//    private void loadPreviousDeliveries()
//    {
//        try
//        {
//            string cmd = @"SELECT dbo.ItemDelivery.DeliveryId, Case dbo.ItemDelivery.status when 0 then 'Delivered' else 'Cancelled' end as Status,   dbo.ItemDelivery.DeliveryDate, dbo.ItemDelivery.BatchNo, dbo.Products.ProductName,dbo.SalesDetails.SalesId, dbo.SalesDetails.Quantity, dbo.ItemDelivery.QuantityDelivered, dbo.ItemDelivery.QuantityRemaining, 
//                      dbo.ItemDelivery.QuantityPicked,dbo.ItemDelivery.QuantityRemaining-dbo.ItemDelivery.QuantityPicked as Balance
//                        FROM         dbo.SalesDetails INNER JOIN
//                      dbo.Products ON dbo.SalesDetails.ProductId = dbo.Products.ProductId INNER JOIN
//                      dbo.ItemDelivery ON dbo.SalesDetails.SalesDetailsId = dbo.ItemDelivery.SalesDetailsId where SalesId=" + ddlinvoice.SelectedValue + " order by dbo.Products.ProductName";
//            gvPreviousDelivery.DataSource = Toolkit.dataDisplay(conn, cmd);
//            gvPreviousDelivery.DataBind();
//        }
//        catch (Exception ex)
//        {
//            lblError.Text = ex.Message;
//        }
//    }

    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        loaddata();
    }

    protected void btnAction_Click1(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        int salesid = int.Parse(ddlinvoice.SelectedValue);
        
        try
        {
            Toolkit.RunSqlCommand("update Sales set PaymentStatus= -1, Remarks = Remarks +' | Comments: " + txtcomments.Text + "' where SalesID =" + ddlinvoice.SelectedValue, conn);
//            string cmd = @"SELECT dbo.ItemDelivery.DeliveryId, Case dbo.ItemDelivery.status when 0 then 'Delivered' else 'Cancelled' end as Status,   dbo.ItemDelivery.DeliveryDate, dbo.ItemDelivery.BatchNo, dbo.Products.ProductName,dbo.SalesDetails.SalesId, dbo.SalesDetails.Quantity, dbo.ItemDelivery.QuantityDelivered, dbo.ItemDelivery.QuantityRemaining, 
//                      dbo.ItemDelivery.QuantityPicked,dbo.ItemDelivery.QuantityRemaining-dbo.ItemDelivery.QuantityPicked as Balance
//                        FROM  dbo.SalesDetails INNER JOIN
//                      dbo.Products ON dbo.SalesDetails.ProductId = dbo.Products.ProductId INNER JOIN
//                      dbo.ItemDelivery ON dbo.SalesDetails.SalesDetailsId = dbo.ItemDelivery.SalesDetailsId where SalesId=" + ddlinvoice.SelectedValue + " order by dbo.Products.ProductName";
//            //var d = Toolkit.dataDisplay(conn, cmd);
//            var d = bl.getSalesDetails().Where(x => x.SalesId ==salesid).ToList();
//            if (d != null)
//            {
//                for (int i = 0; i < d.Count(); i++)
//                {
//                    //@Posid int,@action int,@Qty int,@prd int, @description varchar(50),@userid int
//                    string sSql = "Sp_UpdateStock " + Session["POSId"].ToString() + ",1," + d[i].Quantity.ToString() + "," + d[i].ProductId.ToString() + ",'Invoice Cancellation: "+ddlinvoice.Text+"','"+Session["UserId"].ToString();
//                    var con = new SqlConnection(SessionData.DBConString);
//                    var tcmd = new SqlCommand(sSql, con);
//                    tcmd.Connection.Open();
//                    tcmd.ExecuteNonQuery();
//                    gvData.DataSource = null;
//                    gvData.DataBind();
//                    txtcomments.Text = "";
//                    btnAction.Visible = false;
//                    txtcomments.Visible = false;
//                    loaddata();
//                }
            Response.Redirect("~/InvoiceCancellation.aspx");

//            }
            //Cancelling the Previous Payments
            //Loading previous payments
            //var dtPayments = Toolkit.dataDisplay(conn, "Select a.SalesPaymentId as Id, a.PaymentDate, b.PaymentName,a.Amount, c.FullNames, a.DocumentNo from SalesPayments a, PaymentModes b, Users c where a.SalesId=" + ddlinvoice.SelectedValue + " and a.PaymentModeId=b.ModeId and a.UserId=c.UserId");
            //if (dtPayments != null)
            //{
            //    for (int k = 0; k < dtPayments.Rows.Count; k++)
            //    {

            //    }
            //}
          
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            //var sale = bl.getSale(txtSearchKey.Text.Trim().ToUpper());
            var sale = Toolkit.dataDisplay(conn, "Select InvoiceNumber from Sales a where a.InvoiceNumber='" + txtSearchKey.Text.Trim().ToUpper() + "' and PaymentStatus =2 and POSId=" + Session["POSId"].ToString());
            if (sale != null && sale.Rows.Count == 1)
            {
                DataRow r = sale.Rows[0];
                txtInvoiceNo.Text = r["InvoiceNumber"].ToString();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}