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
using orion.ims.BL;
using orion.ims.DAL;
using Bmat.Tools;
using System.Data;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web;


public partial class UnpaidInvoices : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    static DataTable data = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        bl = new Bl(conn);
        lblError.Text = "";
        if (!IsPostBack)
        {
            var pos = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlPOS, pos, true);
        }
        btnAction.Visible = false;
    }
    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        loaddata();
        if (gvData.Rows.Count > 0)
        {
            btnAction.Visible = true;
        }
        else
            btnAction.Visible = false;
    }
    private void loaddata()
    {
        if (chkbxAll.Checked == true)
        {
            if(txtSearchByName.Text.Trim()!="")
                data = Toolkit.dataDisplay(conn, "select '" + txtstartdate.Text + "' as StartDate, '" + txtendDate.Text + "' as EndDate,* from Vw_DailyActivities_onSale where CustomerName like '%" + txtSearchByName.Text.Trim() + "%' and  Date between '" + txtstartdate.Text + "'" + " and '" + txtendDate.Text + "' and Status='Invoice' order by Date desc");
            else
            data = Toolkit.dataDisplay(conn, "select '" + txtstartdate.Text + "' as StartDate, '" + txtendDate.Text + "' as EndDate,* from Vw_DailyActivities_onSale where Date between '" + txtstartdate.Text + "'" + " and '" + txtendDate.Text + "' and Status='Invoice' order by Date desc");
            gvData.DataSource = data;
            gvData.DataBind();
        }
        else
        {
            if (txtSearchByName.Text.Trim() != "")
                data = Toolkit.dataDisplay(conn, "select '" + txtstartdate.Text + "' as StartDate, '" + txtendDate.Text + "' as EndDate,* from Vw_DailyActivities_onSale where  CustomerName like '%" + txtSearchByName.Text.Trim() + "%' and Date between '" + txtstartdate.Text + "'" + " and '" + txtendDate.Text + "'  and Status='Invoice' " + "and POSID ='" + ddlPOS.SelectedValue + "'" + " order by Date desc");
            else
            data = Toolkit.dataDisplay(conn, "select '" + txtstartdate.Text + "' as StartDate, '" + txtendDate.Text + "' as EndDate,* from Vw_DailyActivities_onSale where Date between '" + txtstartdate.Text + "'" + " and '" + txtendDate.Text + "'  and Status='Invoice' " + "and POSID ='" + ddlPOS.SelectedValue + "'" + " order by Date desc");
            gvData.DataSource = data;
            gvData.DataBind();
        }

    }
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        loaddata();
    }
    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        ReportDocument rpt = new ReportDocument();
        rpt.FileName = Server.MapPath("~/Reports/rptUnpaidInvoices.rpt");
        rpt.SetDataSource(data);
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
    protected void chkbxAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbxAll.Checked)
        {
            ddlPOS.SelectedIndex = -1;
            ddlPOS.Enabled = false;
        }
        else
        {
            ddlPOS.Enabled = true;
        }
    }

}