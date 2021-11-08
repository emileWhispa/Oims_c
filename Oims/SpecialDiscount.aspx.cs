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

public partial class SpecialDiscount : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {

        bl = new Bl(conn);
        if (!IsPostBack)
        {
            //DataTable dt = new DataTable();
            var dt = Toolkit.dataDisplay(conn, "Select SalesRef,SalesID from Sales where PaymentStatus in (0,1) and POSId="+Session["POSId"].ToString());
            ddlinvoice.DataSource = dt;
            ddlinvoice.DataValueField = "SalesID";
            ddlinvoice.DataTextField = "SalesRef";
            ddlinvoice.DataBind();
            ddlinvoice.Items.Add("Select Sales Number");
            ddlinvoice.SelectedIndex = ddlinvoice.Items.Count - 1;
        }
        txtdisrate.Enabled = false;
        btnAction.Enabled = false;

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

            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void ddlinvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvData.DataSource = null;
        gvData.DataBind();
        if (ddlinvoice.SelectedIndex >= 0 && ddlinvoice.SelectedItem.Text != "Select Invoice")
            loaddata();
        ChkDiscount.Checked = false;
    }
    protected void gvData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvData.EditIndex = -1;
        loaddata();
    }
    protected void gvData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvData.EditIndex = e.NewEditIndex;
        loaddata();
    }
    protected void gvData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            int rowId = e.RowIndex;
            string salesId = gvData.DataKeys[rowId].Value.ToString();
            GridViewRow row = (GridViewRow)gvData.Rows[e.RowIndex];
            TextBox txtdisc = (TextBox)row.FindControl("txtDiscounts");
            string cmd = "update SalesDetails set Discounts=" + txtdisc.Text + " where SalesDetailsId=" + salesId;
            Toolkit.RunSqlCommand(cmd, conn);
            gvData.EditIndex = -1;
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
    protected void ChkDiscount_CheckedChanged(object sender, EventArgs e)
    {
        if (ChkDiscount.Checked == false)
        {
            txtdisrate.Enabled = false;
            btnAction.Enabled = false;
        }
        if (ChkDiscount.Checked == true)
        {
            txtdisrate.Enabled = true;
            btnAction.Enabled = true;
        }
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        try
        {

            if (gvData.Rows.Count <= 0)
            {
                lblError.Text = "You can not apply discount without Data";
                return;
            }
            else
            {
                string cmd = "update SalesDetails set Discounts=" + txtdisrate.Text + " where SalesId=" + ddlinvoice.SelectedValue;
                Toolkit.RunSqlCommand(cmd, conn);
                txtdisrate.Text = "";
                gvData.EditIndex = -1;
                loaddata();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}