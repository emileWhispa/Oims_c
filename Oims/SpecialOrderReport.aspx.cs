using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.BL;
using orion.ims.DAL;
using System.Data.SqlClient;
using System.Data;
using bMobile.Shared;
using Bmat.Tools;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web;

public partial class SpecialOrderReport : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    static DataTable dtData = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        bl = new Bl(conn);
        lblError.Text = "";
        if (!IsPostBack)
        {
            var pos = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlPOS, pos, true);
        }

    }
    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        string cmd = "";
        if (txtSearchByName.Text.Trim() != "")
            cmd = "Select '" + txtstartdate.Text + "' as StartDate, '" + txtendDate.Text + "' as EndDate, a.SpecialOrderId as id, case a.OrderStatus when 0 then 'Pending' when 1 then 'Submitted' when 2 then 'Processing' when 3 then 'Dispatched' when 4 then 'Received' when 5 then 'Collected' else 'Unknown' end as Status,a.*,c.CustomerName,d.CategoryName, b.POSName as [OriginPOS],(Select POSName from POSes where POSId=a.CollectionPOS) as [CollectionPOSName] from SpecialOrders a, Customers c, POSes b, ProductCategories d where a.POSId=b.POSId and a.ProductCategoryId = d.ProductCategoryId and a.CustomerId=c.CustomerId and c.CustomerName like '%"+txtSearchByName.Text.Trim()+"%' and a.OrderDate between '" + txtstartdate.Text + "' and '" + txtendDate.Text + "'";
        else
            cmd = "Select '" + txtstartdate.Text + "' as StartDate, '" + txtendDate.Text + "' as EndDate, a.SpecialOrderId as id, case a.OrderStatus when 0 then 'Pending' when 1 then 'Submitted' when 2 then 'Processing' when 3 then 'Dispatched' when 4 then 'Received' when 5 then 'Collected' else 'Unknown' end as Status,a.*,c.CustomerName,d.CategoryName, b.POSName as [OriginPOS],(Select POSName from POSes where POSId=a.CollectionPOS) as [CollectionPOSName] from SpecialOrders a, Customers c, POSes b, ProductCategories d where a.POSId=b.POSId and a.ProductCategoryId = d.ProductCategoryId and a.CustomerId=c.CustomerId and a.OrderDate between '" + txtstartdate.Text + "' and '" + txtendDate.Text + "'";
        if (!chkbxAll.Checked)
        {
            if (ddlPOS.SelectedIndex == -1)
                lblError.Text = "Please select a POS or tick All";
            else
                cmd += " and a.POSId=" + ddlPOS.SelectedValue;
        }
        if (ddlStatus.SelectedValue != "-1")
            cmd += " and a.OrderStatus=" + ddlStatus.SelectedValue;

        dtData = Toolkit.dataDisplay(conn, cmd+" order by a.OrderDate");
        gvData.DataSource = dtData;
        gvData.DataBind();
        if (dtData != null && dtData.Rows.Count > 0)
            btnPrint.Visible = true;
        else
            btnPrint.Visible = false;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ReportDocument rpt = new ReportDocument();
        rpt.FileName = Server.MapPath("~/Reports/rptSpecialOrders.rpt");
        rpt.SetDataSource(dtData);
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