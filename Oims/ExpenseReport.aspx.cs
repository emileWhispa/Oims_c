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

public partial class ExpenseReport : System.Web.UI.Page
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
            var pos = (from b in bl.getUsers().ToList() select new ListItem { Text = b.FullNames, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlUser, pos, true);
        }
    }
    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        string cmd = "Select * from Vw_expenses where ExpenseDate between '"+txtstartdate.Text+"' and '"+txtendDate.Text+"'";
        if (!chkbxAll.Checked)
        {
            if (ddlUser.SelectedIndex == -1)
                lblError.Text = "Please select a POS or tick All";
            else
                cmd += " and UserId=" + ddlUser.SelectedValue;
        }

        DataTable data = new DataTable();
        data = Toolkit.dataDisplay(conn, cmd);
        gvData.DataSource = data;
        gvData.DataBind();
        if (gvData.Rows.Count > 1)
            btnPrint.Visible = true;
        else
            btnPrint.Visible = false;
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        string cmd = "Select '"+txtstartdate.Text+"' as StartDate, '"+txtendDate.Text+"' as EndDate, * from Vw_expenses where ExpenseDate between '" + txtstartdate.Text + "' and '" + txtendDate.Text + "'";
        if (!chkbxAll.Checked)
        {
            if (ddlUser.SelectedIndex == -1)
                lblError.Text = "Please select a POS or tick All";
            else
                cmd += " and UserId=" + ddlUser.SelectedValue;
        }
        DataTable data = new DataTable();
        data = Toolkit.dataDisplay(conn, cmd);
        ReportDocument rpt = new ReportDocument();
        rpt.FileName = Server.MapPath("~/Reports/rptExpense.rpt");
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
}