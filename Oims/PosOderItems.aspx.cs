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

namespace orion.ims
{
    public partial class PosOderItems : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bl = new Bl(conn);
                if (!IsPostBack)
                {
                    if (Request.QueryString.HasKeys())
                    {
                        txtorderNo.Text = Request.QueryString["id"];
                        loaddata();
                        var prd = (from b in bl.getProducts().ToList() select new ListItem { Text = b.ProductName, Value = b.Id.ToString() }).ToList();
                        Util.loadCombo(ddlProduct, prd, true);
                        Util.LogAudit(this.Page, "Accessed Page", this.Title);
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var order = bl.getPOSOrder(txtorderNo.Text.Trim());
                if (hfId.Text != "")
                {
                    var sp = bl.getOrderItem(int.Parse(hfId.Text));
                    sp.OrderId = order.Id;
                    sp.ProductId = int.Parse(ddlProduct.SelectedValue);
                    sp.QtyOrdered = decimal.Parse(txtquantity.Text);
                    bl.saveOrderItem(sp);
                }
                else
                {
                    orion.ims.DAL.OrderItem p = new DAL.OrderItem
                    {
                        Product = bl.getProduct(int.Parse(ddlProduct.SelectedValue)),
                        ProductId = int.Parse(ddlProduct.SelectedValue),
                        QtyOrdered = decimal.Parse(txtquantity.Text),
                        OrderId = order.Id,
                    };
                    bl.saveOrderItem(p);
                }
                // Util.ShowMsgBox(this, "Order Item saved Successfully", "Production", MsgBoxType.Info);
                btnCancel_Click(null, null);
                loaddata();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "sel")
                {
                    loaddata();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hfId.Text = hf.Value;
                    int id = int.Parse(hfId.Text);
                    var x = bl.getOrderItem(id);
                    var prd = (from b in bl.getProducts().ToList() select new ListItem { Text = b.ProductName, Value = b.Id.ToString() }).ToList();
                    Util.loadCombo(ddlProduct, prd, true);
                    ddlProduct.SelectedIndex = ddlProduct.Items.IndexOf(ddlProduct.Items.FindByValue(x.ProductId.ToString()));
                    txtquantity.Text = x.QtyOrdered.ToString();

                }
                else if (e.CommandName == "del")
                {
                    loaddata();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getOrderItem(id);
                    if (c != null)
                    {
                        bl.deleteOrderItem(id);
                        Util.ShowMsgBox(this, "Order Item Deleted Suceesfully", "Production", MsgBoxType.Info);
                    }
                }
                loaddata();
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
            txtquantity.Text = "";
            txtSearchItem.Text = "";
        }

        private void loaddata()
        {
            try
            {
                var ord = bl.getPOSOrder(txtorderNo.Text);
                var data = bl.getOrderItems(ord.Id).OrderByDescending(x => x.Id).ToList();
                gvData.DataSource = data;
                gvData.DataBind();
                if (gvData.Rows.Count > 0)
                {
                    btnSubmit.Visible = true;
                }
                else
                {
                    btnSubmit.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        protected void btnnew_Click(object sender, EventArgs e)
        {
            hfId.Text = "";
            txtquantity.Text = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Toolkit.RunSqlCommand("update POSOrders set OrderStatus = 1 where OrderNo = " + txtorderNo.Text, conn);
                Response.Redirect("~/POSOrders.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        protected void txtSearchItem_TextChanged(object sender, EventArgs e)
        {
            var prod = (from c in bl.getProducts().Where(x => x.ProductName.Contains(txtSearchItem.Text)).OrderBy(x => x.ProductName).ToList() select new ListItem { Text = c.ProductName, Value = c.Id.ToString() }).ToList();
            Util.loadCombo(ddlProduct, prod, true);
        }
    }
}