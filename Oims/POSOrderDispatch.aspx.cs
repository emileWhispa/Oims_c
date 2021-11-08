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


public partial class POSOrderDispatch : System.Web.UI.Page
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
        OrderStatus od = OrderStatus.Submitted;
        if (ddlorderstatus.SelectedValue == "1")
        { od = OrderStatus.Submitted; btnAction.Text = "Dispatch"; }
        else if (ddlorderstatus.SelectedValue == "2")
        { od = OrderStatus.Dispatched; btnAction.Text = "Delivery Note"; }
        else if (ddlorderstatus.SelectedValue == "3")
        { od = OrderStatus.Received; btnAction.Text = "Reception Note"; }
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

    private void loaddispatchItems(int Id)
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
        if (ddlorderstatus.SelectedValue == "1")
            gvdispatch.Columns[4].Visible = false;
        else if (ddlorderstatus.SelectedValue == "2")
        {
            gvdispatch.Columns[4].Visible = false;
            gvdispatch.Columns[0].Visible = false;
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
            TextBox tDeliveredQty = (TextBox)row.FindControl("txtQtyDelivered");
            string cmd = "update OrderItems set QuantityDelivered=" + tDeliveredQty.Text + " where OrderItemId=" + orderItemId;
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
        try
        {

            if (btnAction.Text == "Dispatch")
            {
                int OrderId = int.Parse(lblPOSOrderNo.Text);
                //var O = bl.getPOSOrder(OrderId);
                //O.OrderStatus = OrderStatus.Dispatched;
                //O.DispatcherId = Session["UserId"].ToString();
                //O.DeliveryDate = DateTime.Now;
                //bl.savePOSOrder(O);
                //Getting the Main Store ID
                var stores = bl.getPOSes().Where(x => x.Mainstore == true).ToList();
                if (stores == null)
                {
                    lblError.Text = "There is no Main Store defined in the system";
                    return;
                }
                //var m_store = stores.FirstOrDefault();
                //var d = bl.getOrderItems().Where(x => x.OrderId == OrderId).ToList();
                //if (d != null)
                //{
                //for (int i = 0; i < d.Count(); i++)
                //{

                string sSql = "Sp_Item_Dispatch " + OrderId.ToString() + "," + Session["UserId"].ToString();
                var conn = new SqlConnection(SessionData.DBConString);
                var tcmd = new SqlCommand(sSql, conn);
                tcmd.Connection.Open();
                tcmd.ExecuteNonQuery();
                //}

                //}
                //loaddata();

            }
            Response.Redirect("Reports.aspx?report=DeliveryNote&id=" + lblPOSOrderNo.Text);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
        //Print Delivery Note
    }
    protected void gvdispatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvdispatch.PageIndex = e.NewPageIndex;
        loaddispatchItems(int.Parse(lblPOSOrderNo.Text));
    }
}