using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.BL;
using orion.ims.DAL;
using Bmat.Tools;
using System.Data;

public partial class StockAdjustment : System.Web.UI.Page
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
                var pos = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlPOS, pos, true);
                Util.LogAudit(this.Page, "Accessed Page", this.Title);
            }
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
    protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int prodId = 0;
            int posId=0;
            int qty = 0;
            if (ddlProduct.SelectedIndex != -1)
            {
                prodId = int.Parse(ddlProduct.SelectedValue);
                posId=int.Parse(ddlPOS.SelectedValue);
                var it = bl.getStockItemByProductId(prodId,posId);
                if (it == null)
                    txtAvailableQty.Text = "0";
                else
                {
                    txtAvailableQty.Text = it.Quantity.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnSaveAdjustment_Click(object sender, EventArgs e)
    {
        try
        {
            Toolkit.RunSqlCommand("insert into StockAdjustment (PosID,ProductId,AdjustmentType,QuantityAdjusted, Comments) values (" + ddlPOS.SelectedValue + "," + ddlProduct.SelectedValue + "," + ddlAdjustment.SelectedValue + "," + txtQtyAdjusted.Text + ",'" + txtComments.Text.Replace("'", "''") + "')", conn);
            refreshAdjustments();
            clearControls();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }

    private void clearControls()
    {
        ddlPOS.SelectedIndex = -1;
        ddlProduct.SelectedIndex = -1;
        ddlAdjustment.SelectedIndex = -1;
        txtAvailableQty.Text = "";
        txtComments.Text = "";
        txtQtyAdjusted.Text = "";
        txtSearchItem.Text = "";
    }

    private void refreshAdjustments()
    {
        gvData.DataSource = Toolkit.dataDisplay(conn, "Select * from Vw_StockAdjustment where PosID=" + ddlPOS.SelectedValue + " order by AdjustmentDate desc");
        gvData.DataBind();
    }
    protected void gvData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (gvData.Rows[e.RowIndex].Cells[6].Text == "No")
        {
            Toolkit.RunSqlCommand("Delete from StockAdjustment where StockAdjustmentId=" + gvData.Rows[e.RowIndex].Cells[0].Text, conn);
            refreshAdjustments();
        }
        else
            lblError.Text = "You cannot delete an authorized Adjustment";
    }
    protected void ddlPOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        refreshAdjustments();
    }
}