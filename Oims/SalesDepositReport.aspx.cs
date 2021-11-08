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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web;

public partial class SalesDepositReport : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    static DataTable data = new DataTable();

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
    protected void ddlPOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPOS.SelectedIndex != -1 && ddlPOS.Text != "")
        {
            int posId = int.Parse(ddlPOS.SelectedValue);
            var usr = (from b in bl.getUsers().Where(t => t.POSID == posId).OrderBy(x => x.FullNames).ToList() select new ListItem { Text = b.FullNames, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(cmbTeller, usr, true);
        }
    }
    protected void chkbxAll_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbxAllPOS.Checked)
        {
            ddlPOS.SelectedIndex = -1;
            ddlPOS.Enabled = false;
        }
        else
        {
            ddlPOS.Enabled = true;
        }
    }
    protected void chkbxAllTeller_CheckedChanged(object sender, EventArgs e)
    {
        if (chkbxAllTeller.Checked)
        {
            cmbTeller.SelectedIndex = -1;
            cmbTeller.Enabled = false;
        }
        else
        {
            cmbTeller.Enabled = true;
        }
    }
    protected void btnDisplay_Click(object sender, EventArgs e)
    {
        loaddata();
        if (gvData.Rows.Count > 0)
        {
            btnAction.Visible = true;
        }
        else
            btnAction.Visible=false;
    }

    private void loaddata()
    {
        if (chkbxAllPOS.Checked == true)
        {
            if (!chkbxAllTeller.Checked)
            {
                if (cmbTeller.SelectedIndex == -1)
                {
                    lblError.Text = "Select first the Teller or Tick [All Teller] Option";
                    return;
                }
                data = Toolkit.dataDisplay(conn, "select '" + txtstartdate.Text + "' as StartDate, '" + txtendDate.Text + "' as EndDate,* from Vw_Sale_Deposit where QuoteDate between '" + txtstartdate.Text + "'" + " and '" + txtendDate.Text + "' and UserID=" + cmbTeller.SelectedValue + " order by QuoteDate desc");
            }
            else
                data = Toolkit.dataDisplay(conn, "select '" + txtstartdate.Text + "' as StartDate, '" + txtendDate.Text + "' as EndDate,* from Vw_Sale_Deposit where QuoteDate between '" + txtstartdate.Text + "'" + " and '" + txtendDate.Text + "' order by QuoteDate desc");
        }
        else
        {
            if (!chkbxAllTeller.Checked)
            {
                if (cmbTeller.SelectedIndex == -1)
                {
                    lblError.Text = "Select first the Teller or Tick [All Teller] Option";
                    return;
                }
                data = Toolkit.dataDisplay(conn, "select '" + txtstartdate.Text + "' as StartDate, '" + txtendDate.Text + "' as EndDate,* from Vw_Sale_Deposit where QuoteDate between '" + txtstartdate.Text + "'" + " and '" + txtendDate.Text + "'" + "and POSID ='" + ddlPOS.SelectedValue + "' and UserID=" + cmbTeller.SelectedValue + " order by QuoteDate desc");
            }
            else
                data = Toolkit.dataDisplay(conn, "select '" + txtstartdate.Text + "' as StartDate, '" + txtendDate.Text + "' as EndDate,* from Vw_Sale_Deposit where QuoteDate between '" + txtstartdate.Text + "'" + " and '" + txtendDate.Text + "'" + "and POSID ='" + ddlPOS.SelectedValue + "'" + " order by QuoteDate desc");
        }
        gvData.DataSource = data;
        gvData.DataBind();
    }
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void cmbTeller_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvData.DataSource = null;
        gvData.DataBind();
    }
    protected void btnAction_Click(object sender, EventArgs e)
    {
        ReportDocument rpt = new ReportDocument();
        rpt.FileName = Server.MapPath("~/Reports/rptSaleDeposit.rpt");
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