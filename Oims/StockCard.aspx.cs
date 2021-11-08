using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using Bmat.Tools;
using System.Data.SqlClient;
using System.Data;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web;

public partial class StockCard : System.Web.UI.Page
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
                //var cust = (from b in bl.getCustomers().ToList() select new ListItem { Text = b.CustomerName, Value = b.Id.ToString() }).ToList();
                Util.LogAudit(this.Page, "Accessed Page", this.Title);
                var prod = (from c in bl.getProducts().OrderBy(y => y.ProductName).ToList() select new ListItem { Text = c.ProductName, Value = c.Id.ToString() }).ToList();
                Util.loadCombo(ddlProduct, prod, true);
                var pos = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(cmbPOS, pos, true);

            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void txtSearchItem_TextChanged(object sender, EventArgs e)
    {
        try
        {
            var prod = (from c in bl.getProducts().Where(x => x.ProductName.Contains(txtSearchItem.Text)).OrderBy(x => x.ProductName).ToList() select new ListItem { Text = c.ProductName, Value = c.Id.ToString() }).ToList();
            Util.loadCombo(ddlProduct, prod, true);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        loadData();
    }

    private void loadData()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Toolkit.dataDisplay(conn, "Select a.*,a.OpeningQuantity-a.QuantityOut+QuantityIn as Balance,b.FullNames from StockCard a, Users b where a.UserId=b.UserId and a.ProductId=" + ddlProduct.SelectedValue + " and a.POSId=" + cmbPOS.SelectedValue + " and TranDate>='" + txtStartDate.Text + "' and TranDate<dateadd(d,1,'" + txtEndDate.Text + "') order by StockCardId");
            gvData.DataSource = dt;
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
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        loadData();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Toolkit.dataDisplay(conn, "Select '" + txtStartDate.Text + "' as StartDate, '" + txtEndDate.Text + "' as EndDate,* from Vw_StockCard_Details where POSId=" + cmbPOS.SelectedValue + " and ProductId=" + ddlProduct.SelectedValue + " and TranDate>='" + txtStartDate.Text + "' and TranDate<dateadd(d,1,'" + txtEndDate.Text + "') order by StockCardId");
            ReportDocument rpt = new ReportDocument();
            rpt.FileName = Server.MapPath("~/Reports/rptStockCardDetails.rpt");
            rpt.SetDataSource(dt);
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
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}