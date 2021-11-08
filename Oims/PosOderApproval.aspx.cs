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

namespace orion.ims
{
    public partial class PosOderApproval : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {

            }

        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "sel")
                {
                    loaddata(int.Parse(ddlorderstatus.SelectedValue));
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hfId.Text = hf.Value.ToString();
                    loadOrderItems(int.Parse(hf.Value));
                    MultiView1.SetActiveView(items);
                }
                else if (e.CommandName == "app")
                {
                    MultiView1.SetActiveView(vwdispatch);

                    loaddata(int.Parse(ddlorderstatus.SelectedValue));
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    hfId.Text = hf.Value.ToString();
                    int id = int.Parse(hf.Value);
                    loaddispatchItems(int.Parse(hf.Value));
                    //var c = bl.getPOSOrder(id);
                    //int stat = int.Parse(ddlorderstatus.SelectedValue);
                    //OrderStatus od = OrderStatus.Submitted;
                    //if (stat == 1)
                    //{ od = OrderStatus.Dispatched; }
                    //else if (stat == 2)
                    //{ od = OrderStatus.Dispatched; }
                    //else if (stat == 3)
                    //{ od = OrderStatus.Delivered; }
                    //if (c != null)
                    //{
                    //    c.OrderStatus = OrderStatus.Submitted;
                    //    bl.savePOSOrder(c);
                    //}
                    //    Util.ShowMsgBox(this, "Special Order Approved Succesfully", "Production", MsgBoxType.Info);
                }
                loaddata(int.Parse(ddlorderstatus.SelectedValue));
            }
            catch (Exception ex)
            {

                string fx, caller;
                //Common.Errorlog(new StackTrace(), new StackFrame(), out fx, out caller);
                //Common.LogError(ex.Message, fx, caller);
            }
        }
        protected void gvdispatch_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "sel")
                {
                    loaddispatchItems(int.Parse(hfId.Text));
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey2");
                    var d = bl.getOrderItem(int.Parse(hf.Value));
                    hfid2.Text = hf.Value;
                    lblproduct.Text = d.Product.ProductName;
                }

            }
            catch (Exception ex)
            { }

        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loaddata(int.Parse(ddlorderstatus.SelectedValue));
        }
        protected void gvdispatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvdispatch.PageIndex = e.NewPageIndex;
            loaddispatchItems(int.Parse(hfId.Text));
        }
        protected void gvitems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvitems.PageIndex = e.NewPageIndex;
            loadOrderItems(int.Parse(hfId.Text));
        }
        private void loaddata(int stat)
        {
            OrderStatus od = OrderStatus.Pending;
            if (stat == 1)
            { od = OrderStatus.Submitted; }
            else if (stat == 2)
            { od = OrderStatus.Dispatched; }
            else if (stat == 3)
            { od = OrderStatus.Received; }
            var data = bl.getPOSOrders().Where(x => x.OrderStatus == od).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }

        private void loadOrderItems(int Id)
        {
            var data = bl.getOrderItems(Id).ToList();
            gvitems.DataSource = data;
            gvitems.DataBind();
        }
        private void loaddispatchItems(int Id)
        {
            var data = bl.getOrderItems(Id).ToList();
            gvdispatch.DataSource = data;
            gvdispatch.DataBind();
        }
        protected void btnget_Click(object sender, EventArgs e)
        {
            loaddata(int.Parse(ddlorderstatus.SelectedValue));
        }
        protected void btnsave_Click(object sender, EventArgs e)
        {
            try
            {
                var d = bl.getOrderItem(int.Parse(hfid2.Text));
                if (d != null)
                {
                    d.QtyDelivered = decimal.Parse(txtquantity.Text);
                    bl.saveOrderItem(d);
                    var O = bl.getPOSOrder(d.OrderId);
                    O.OrderStatus = OrderStatus.Dispatched;
                    bl.savePOSOrder(O);
                    string sSql = "Sp_UpdateStock " + O.POSId + ",1," + decimal.Parse(txtquantity.Text) + "," + d.ProductId + "";
                    var conn = new SqlConnection(SessionData.DBConString);
                    var tcmd = new SqlCommand(sSql, conn);
                    tcmd.Connection.Open();
                    tcmd.ExecuteNonQuery();
                    Util.ShowMsgBox(this, "POS Order Dispatched Succesfully", "Pos Orders", MsgBoxType.Info);
                    txtquantity.Text = "";
                    hfid2.Text = "";

                }
                loaddata(int.Parse(ddlorderstatus.SelectedValue));
                loaddispatchItems(int.Parse(hfId.Text));

            }
            catch (Exception ex)
            { }
        }
    }
}