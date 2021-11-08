using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using System.Data.SqlClient;
using bMobile.Shared;
using Bmat.Tools;

public partial class POSOrderReception : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        bl = new Bl(conn);
        lblError.Text = "";
    }

    private void loaddata()
    {
        OrderStatus od = OrderStatus.Dispatched;
        if (ddlorderstatus.SelectedValue == "2")
        { od = OrderStatus.Dispatched; btnAction.Text = "Confirm Reception"; }
        else if (ddlorderstatus.SelectedValue == "3")
        { od = OrderStatus.Received; btnAction.Text = "Receipt Note"; }
        var data = bl.getPOSOrders().Where(x => x.OrderStatus == od).ToList();
        gvData.DataSource = data;
        gvData.DataBind();
        gvdispatch.DataSource = null;
        gvdispatch.DataBind();
        btnAction.Visible = false;
        lblOrderNo.Text = "";
        lblPOSOrderNo.Text = "";

    }
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        loaddata();
    }
    protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
    {
        int rowId = gvData.SelectedIndex;
        lblPOSOrderNo.Text = gvData.DataKeys[rowId].Value.ToString();
        lblOrderNo.Text = gvData.Rows[rowId].Cells[1].Text;
        loaddispatchItems(int.Parse(gvData.DataKeys[rowId].Value.ToString()));
    }

    private void  loaddispatchItems(int Id)
    {
        var data = bl.getOrderItems(Id).ToList();
        gvdispatch.DataSource = data;
        gvdispatch.DataBind();
        if (gvdispatch.Rows.Count > 0)
        {
            btnAction.Visible = true;
        }
        else
        {
            btnAction.Visible = false;
        }
        if (ddlorderstatus.SelectedValue == "2")
        {
            gvdispatch.Columns[4].Visible = true;
            gvdispatch.Columns[0].Visible = true;
        }
        else if (ddlorderstatus.SelectedValue == "3")
        {
            gvdispatch.Columns[4].Visible = true;
            gvdispatch.Columns[0].Visible = false;
        }
        //Enable/Disable Print link
        //DataTable dt = new DataTable();
        //dt = Toolkit.dataDisplay(strCon, "select Request_detail_code from Request_Details where request_code=" + gridPendingRequests.SelectedRow.Cells[1].Text + " and (item_description2 is null or item_description2 ='')");
        //if (dt.Rows.Count > 0)
        //    btnSubmit.Enabled = false;
        //else
        //    btnSubmit.Enabled = true;

    }
    protected void gvdispatch_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvdispatch.EditIndex = e.NewEditIndex;
        loaddispatchItems(int.Parse(lblPOSOrderNo.Text));
    }
    protected void gvdispatch_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int rowId = e.RowIndex;
            string orderItemId = gvdispatch.DataKeys[rowId].Value.ToString();
            GridViewRow row = (GridViewRow)gvdispatch.Rows[e.RowIndex];
            TextBox tReceivedQty = (TextBox)row.FindControl("txtQtyReceived");
            string cmd = "update OrderItems set QuantityReceived=" + tReceivedQty.Text + " where OrderItemId=" + orderItemId;
            Toolkit.RunSqlCommand(cmd, conn);
            gvdispatch.EditIndex = -1;
            loaddispatchItems(int.Parse(lblPOSOrderNo.Text));
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void gvdispatch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvdispatch.EditIndex = -1;
        loaddispatchItems(int.Parse(lblPOSOrderNo.Text));
    }

    protected void ddlorderstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvData.DataSource = null;
        gvData.DataBind();
        gvdispatch.DataSource = null;
        gvdispatch.DataBind();
        btnAction.Visible = false;
        lblOrderNo.Text = "";
        lblPOSOrderNo.Text = "";
        if (ddlorderstatus.SelectedValue != "-1")
            loaddata();
    }
    protected void btnget_Click(object sender, EventArgs e)
    {
        if (ddlorderstatus.SelectedValue == "-1")
        {
            lblError.Text = "Select a valid status";
            return;
        }
        loaddata();
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        string orderId = lblPOSOrderNo.Text;
        if (btnAction.Text == "Confirm Reception")
        {
            try
            {
                int OrderId = int.Parse(lblPOSOrderNo.Text);
                var O = bl.getPOSOrder(OrderId);
                if (O.OrderStatus == OrderStatus.Received)
                {
                    lblError.Text = "This order has been already confirmed as received";
                    return;
                }
                var d = bl.getOrderItems().Where(x => x.OrderId == OrderId).ToList();
                if (d != null)
                {
                    for (int i = 0; i < d.Count(); i++)
                    {

                        string sSql = "Sp_UpdateStock " + O.POSId.ToString() + ",1," + d[i].QtyReceived.ToString() + "," + d[i].ProductId.ToString() + ",'Replenishment No. "+O.OrderNo+"',"+Session["UserId"].ToString();
                        var conn = new SqlConnection(SessionData.DBConString);
                        var tcmd = new SqlCommand(sSql, conn);
                        tcmd.Connection.Open();
                        tcmd.ExecuteNonQuery();
                    }

                }
                O.OrderStatus = OrderStatus.Received;
                O.ReceiverId = int.Parse(Session["UserId"].ToString());
                O.ReceptionDate = DateTime.Now;
                bl.savePOSOrder(O);

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        //Print GRN
        Response.Redirect("Reports.aspx?report=ReceiptNote&id=" + orderId);

    }
}