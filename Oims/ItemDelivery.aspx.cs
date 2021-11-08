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

public partial class ItemDelivery : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            bl = new Bl(conn);
            lblError.Text = "";
            if (!IsPostBack)
            {
                //DataTable dt = new DataTable();
                var dt = Toolkit.dataDisplay(conn, "Select InvoiceNumber,SalesId from Sales where PaymentStatus in (3,4,5) and POSId="+Session["POSId"].ToString());//3:Partially Paid, 4: Fully Paid, 5: Partially Delivered, 6: Fully Delivered
                //var pos = (from b in bl.getSales().ToList().Where(x=>x.PaymentStatus==2 || x.PaymentStatus==3) select new ListItem { Text = b.InvoiceNumber, Value = b.Id.ToString() }).ToList();//status=2: Invoice, 3: Partially Paid
                //Util.loadCombo(ddlinvoice, dt, true);
                ddlinvoice.DataSource = dt;
                ddlinvoice.DataValueField = "SalesID";
                ddlinvoice.DataTextField = "InvoiceNumber";
                ddlinvoice.DataBind();
                ddlinvoice.Items.Add("Select Invoice");
                ddlinvoice.SelectedIndex = ddlinvoice.Items.Count - 1;
                Util.LogAudit(this.Page, "Accessed Page", this.Title);
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void ddlinvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtQtyPurchased.Text = "";
            txtQtyDelivered.Text = "";
            txtQtyRemaining.Text = "";
            txtQtyAvailable.Text = "";
            txtQuantityToDeliver.Text = "";
            btnPrint.Visible = false;
            gvData.DataSource = null;
            gvData.DataBind();
            gvPreviousDelivery.DataSource = null;
            gvPreviousDelivery.DataBind();
            if (ddlinvoice.SelectedIndex >= 0 && ddlinvoice.SelectedItem.Text != "Select Invoice")
            {
                var data = Toolkit.dataDisplay(conn, "Select SalesDetailsId, ProductName, Quantity, QuantityDelivered, Quantity-QuantityDelivered as QuantityRemaining, 0 as QuantityPicked from Vw_Item_Delivery where SalesId='" + ddlinvoice.SelectedValue.Replace("'", "''") + "'");
                //gvData.DataSource = data;
                //gvData.DataBind();
                ddlItem.DataSource = data;
                ddlItem.DataValueField = "SalesDetailsId";
                ddlItem.DataTextField = "ProductName";
                ddlItem.DataBind();
                ddlItem.Items.Add("Select Item");
                ddlItem.SelectedIndex = ddlItem.Items.Count - 1;
                //if (gvData.Rows.Count > 0)
                txtBatchNo.Text = DateTime.Now.ToFileTime().ToString();
                string cmd = @"SELECT dbo.ItemDelivery.DeliveryId, Case dbo.ItemDelivery.status when 0 then 'Delivered' else 'Cancelled' end as Status,   dbo.ItemDelivery.DeliveryDate, dbo.ItemDelivery.BatchNo, dbo.Products.ProductName,dbo.SalesDetails.SalesId, dbo.SalesDetails.Quantity, dbo.ItemDelivery.QuantityDelivered, dbo.ItemDelivery.QuantityRemaining, 
                      dbo.ItemDelivery.QuantityPicked,dbo.ItemDelivery.QuantityRemaining-dbo.ItemDelivery.QuantityPicked as Balance
                        FROM         dbo.SalesDetails INNER JOIN
                      dbo.Products ON dbo.SalesDetails.ProductId = dbo.Products.ProductId INNER JOIN dbo.Sales ON dbo.SalesDetails.SalesId=dbo.Sales.SalesId INNER JOIN 
                      dbo.ItemDelivery ON dbo.SalesDetails.SalesDetailsId = dbo.ItemDelivery.SalesDetailsId where dbo.Sales.SalesId='" + ddlinvoice.SelectedValue.Trim() + "' and BatchNo<>'" + txtBatchNo.Text + "' order by dbo.Products.ProductName";
                gvPreviousDelivery.DataSource = Toolkit.dataDisplay(conn, cmd);
                gvPreviousDelivery.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void loaddata()
    {
        try
        {
            if (ddlinvoice.SelectedIndex != -1)
            {
                //var p = bl.getSale(ddlinvoice.SelectedItem.Text);
                //int id = int.Parse(ddlinvoice.SelectedValue);
                var data = Toolkit.dataDisplay(conn, "Select SalesDetailsId, ProductName, Quantity, QuantityDelivered, Quantity-QuantityDelivered as QuantityRemaining, 0 as QuantityPicked from Vw_Item_Delivery where SalesId=" + ddlinvoice.SelectedValue);
                //gvData.DataSource = data;
                //gvData.DataBind();
                ddlItem.DataSource = data;
                ddlItem.DataValueField = "SalesDetailsId";
                ddlItem.DataTextField = "ProductName";
                ddlItem.DataBind();
                ddlItem.Items.Add("Select Item");
                ddlItem.SelectedIndex = ddlItem.Items.Count - 1;
                //if (gvData.Rows.Count > 0)
                txtBatchNo.Text = DateTime.Now.ToFileTime().ToString();
            }

        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void txtInvoiceNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtQtyPurchased.Text = "";
            txtQtyDelivered.Text = "";
            txtQtyRemaining.Text = "";
            txtQtyAvailable.Text = "";
            txtQuantityToDeliver.Text = "";
            btnPrint.Visible = false;
            gvData.DataSource = null;
            gvData.DataBind();
            gvPreviousDelivery.DataSource = null;
            gvPreviousDelivery.DataBind();
            if (txtInvoiceNo.Text.Trim() != "")
            {
                var data = Toolkit.dataDisplay(conn, "Select SalesDetailsId, ProductName, Quantity, QuantityDelivered, Quantity-QuantityDelivered as QuantityRemaining, 0 as QuantityPicked from Vw_Item_Delivery where InvoiceNumber='" + txtInvoiceNo.Text.Trim().Replace("'", "''") + "' and PaymentStatus in (3,4,5) and POSId=" + Session["POSId"].ToString());
                if (data == null || data.Rows.Count < 1)
                {
                    lblError.Text = "This Invoice is not found";
                    return;
                }
                ddlItem.DataSource = data;
                ddlItem.DataValueField = "SalesDetailsId";
                ddlItem.DataTextField = "ProductName";
                ddlItem.DataBind();
                ddlItem.Items.Add("Select Item");
                ddlItem.SelectedIndex = ddlItem.Items.Count - 1;
                //if (gvData.Rows.Count > 0)
                txtBatchNo.Text = DateTime.Now.ToFileTime().ToString();
                string cmd = @"SELECT dbo.ItemDelivery.DeliveryId, Case dbo.ItemDelivery.status when 0 then 'Delivered' else 'Cancelled' end as Status,   dbo.ItemDelivery.DeliveryDate, dbo.ItemDelivery.BatchNo, dbo.Products.ProductName,dbo.SalesDetails.SalesId, dbo.SalesDetails.Quantity, dbo.ItemDelivery.QuantityDelivered, dbo.ItemDelivery.QuantityRemaining, 
                      dbo.ItemDelivery.QuantityPicked,dbo.ItemDelivery.QuantityRemaining-dbo.ItemDelivery.QuantityPicked as Balance
                        FROM         dbo.SalesDetails INNER JOIN
                      dbo.Products ON dbo.SalesDetails.ProductId = dbo.Products.ProductId INNER JOIN dbo.Sales ON dbo.SalesDetails.SalesId=dbo.Sales.SalesId INNER JOIN 
                      dbo.ItemDelivery ON dbo.SalesDetails.SalesDetailsId = dbo.ItemDelivery.SalesDetailsId where dbo.Sales.InvoiceNumber='" + txtInvoiceNo.Text.Trim() + "' and BatchNo<>'" + txtBatchNo.Text + "' order by dbo.Products.ProductName";
                gvPreviousDelivery.DataSource = Toolkit.dataDisplay(conn, cmd);
                gvPreviousDelivery.DataBind();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtQtyPurchased.Text = "";
            txtQtyDelivered.Text = "";
            txtQtyRemaining.Text = "";
            txtQtyAvailable.Text = "";
            txtQuantityToDeliver.Text = "";
            if (ddlItem.SelectedIndex != -1 && ddlItem.SelectedItem.Text != "Select Item")
            {
                var data = Toolkit.dataDisplay(conn, "Select ProductId,Quantity, QuantityDelivered, Quantity-QuantityDelivered as QuantityRemaining, 0 as QuantityPicked from Vw_Item_Delivery where SalesDetailsId=" + ddlItem.SelectedValue);
                if (data != null)
                {
                    DataRow r = data.Rows[0];
                    txtQtyPurchased.Text = r["Quantity"].ToString();
                    txtQtyDelivered.Text = r["QuantityDelivered"].ToString();
                    txtQtyRemaining.Text = r["QuantityRemaining"].ToString();
                    //Getting available quantity
                    var q = Toolkit.dataDisplay(conn, "Select Quantity from StockItems where ProductId=" + r["ProductId"].ToString() + " and PosId=" + Session["POSId"].ToString());
                    if (q != null && q.Rows.Count == 1)
                        txtQtyAvailable.Text = q.Rows[0].ItemArray.GetValue(0).ToString();
                    else
                        txtQtyAvailable.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnDeliver_Click(object sender, EventArgs e)
    {
        try
        {
            //Checking if the available qty is enough
            float avail = 0;
            float pickedqty = 0;
            float remaiqyt = 0;
            float.TryParse(txtQtyRemaining.Text, out remaiqyt);
            float.TryParse(txtQtyAvailable.Text, out avail);
            float.TryParse(txtQuantityToDeliver.Text, out pickedqty);
            if (pickedqty > remaiqyt)
            {
                lblError.Text = "You cannot deliver more than the remaining quantity";
                return;
            }
            if (avail == 0)
            {
                lblError.Text = "This item is not available in the store";
                return;
            }
            if (avail < pickedqty)
            {
                lblError.Text = "Quantity to be delivered is greater than the available quantity";
                return;
            }
            string cmd = "exec sp_item_delivery '" + txtBatchNo.Text + "'," + ddlItem.SelectedValue + "," + txtQtyDelivered.Text + "," + txtQtyRemaining.Text + "," + txtQuantityToDeliver.Text + "," + Session["UserId"].ToString();
            long res = Toolkit.RunSqlCommand(cmd, conn);
            loadDeliveryDetails();
            if (res > 0)
            {
                txtQtyPurchased.Text = "";
                txtQtyDelivered.Text = "";
                txtQtyRemaining.Text = "";
                txtQtyAvailable.Text = "";
                txtQuantityToDeliver.Text = "";
                ddlItem.SelectedIndex = ddlItem.Items.Count - 1;//To display Select Item text
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void loadDeliveryDetails()
    {
        try
        {
            string cmd = @"SELECT dbo.ItemDelivery.DeliveryId,   Case dbo.ItemDelivery.status when 0 then 'Delivered' else 'Cancelled' end as Status,   dbo.ItemDelivery.BatchNo, dbo.Products.ProductName, dbo.SalesDetails.Quantity, dbo.ItemDelivery.QuantityDelivered, dbo.ItemDelivery.QuantityRemaining, 
                      dbo.ItemDelivery.QuantityPicked,dbo.ItemDelivery.QuantityRemaining-dbo.ItemDelivery.QuantityPicked as Balance
                        FROM         dbo.SalesDetails INNER JOIN
                      dbo.Products ON dbo.SalesDetails.ProductId = dbo.Products.ProductId INNER JOIN
                      dbo.ItemDelivery ON dbo.SalesDetails.SalesDetailsId = dbo.ItemDelivery.SalesDetailsId where BatchNo='" + txtBatchNo.Text + "'";
            gvData.DataSource = Toolkit.dataDisplay(conn, cmd);
            gvData.DataBind();
            if (gvData.Rows.Count > 0)
                btnPrint.Visible = true;
            else
                btnPrint.Visible = false;
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void loadPreviousDeliveries()
    {
        try
        {
            string cmd = @"SELECT dbo.ItemDelivery.DeliveryId, Case dbo.ItemDelivery.status when 0 then 'Delivered' else 'Cancelled' end as Status,   dbo.ItemDelivery.DeliveryDate, dbo.ItemDelivery.BatchNo, dbo.Products.ProductName,dbo.SalesDetails.SalesId, dbo.SalesDetails.Quantity, dbo.ItemDelivery.QuantityDelivered, dbo.ItemDelivery.QuantityRemaining, 
                      dbo.ItemDelivery.QuantityPicked,dbo.ItemDelivery.QuantityRemaining-dbo.ItemDelivery.QuantityPicked as Balance
                        FROM         dbo.SalesDetails INNER JOIN
                      dbo.Products ON dbo.SalesDetails.ProductId = dbo.Products.ProductId INNER JOIN
                      dbo.ItemDelivery ON dbo.SalesDetails.SalesDetailsId = dbo.ItemDelivery.SalesDetailsId where SalesId=" + ddlinvoice.SelectedValue + " and BatchNo<>'" + txtBatchNo.Text + "' order by dbo.Products.ProductName";
            gvPreviousDelivery.DataSource = Toolkit.dataDisplay(conn, cmd);
            gvPreviousDelivery.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (txtBatchNo.Text.Trim() != "")
            Response.Redirect("Reports.aspx?report=customerdeliverynote&id=" + txtBatchNo.Text.Trim());
    }
    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "reverse")
            {
                int rowId = Convert.ToInt32(e.CommandArgument);
                string DeliveryId = gvData.DataKeys[rowId].Value.ToString();
                var x = bl.getItemDelivery(int.Parse(DeliveryId));
                if (x.Status == -1)
                    return;
                long res = Toolkit.RunSqlCommand("exec Sp_UpdateStock " + Session["POSId"].ToString() + ",1," + x.QuantityPicked.ToString() + "," + x.saleDetail.ProductId.ToString() + ",'Cancellation - Deliver No. " + x.Id.ToString() + "'," + Session["UserId"].ToString(), conn);
                Toolkit.RunSqlCommand("Update ItemDelivery set Status= -1 where DeliveryId=" + x.Id.ToString(), conn);
                loadDeliveryDetails();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}