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

public partial class DailyReport : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    static DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            bl = new Bl(conn);
            lblError.Text = "";
            if (!IsPostBack)
            {
                var pos = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(cmbPOS, pos, true);
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void cmbPOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int posID = int.Parse(cmbPOS.SelectedValue);
            var usr = (from b in bl.getUsers().Where(y => y.POSID == posID).OrderBy(x => x.FullNames).ToList() select new ListItem { Text = b.FullNames, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(cmbTeller, usr, true);
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (chkAll.Checked == false && cmbPOS.SelectedIndex == -1)
        {
            lblError.Text = "Select a user please";
            return;
        }
        loadData();
    }
    private void loadData()
    {
        try
        {
            DataTable dtDistinct = new DataTable();
            if (!chkAll.Checked)
                dt = Toolkit.dataDisplay(conn, "Select '"+txtStartDate.Text+"' as StartDate,a.* from Vw_Teller_Report a where UserId=" + cmbTeller.SelectedValue + " and TransactionDate>='" + txtStartDate.Text + "' and TransactionDate<dateadd(d,1,'" + txtStartDate.Text + "' ) order by TransactionDate");
            else
                dt = Toolkit.dataDisplay(conn, "Select '" + txtStartDate.Text + "' as StartDate,a.* from Vw_Teller_Report a where POSName='" + cmbPOS.SelectedItem.Text + "' and TransactionDate>='" + txtStartDate.Text + "' and TransactionDate<dateadd(d,1,'" + txtStartDate.Text + "') order by TransactionDate");
            string[] filter = { "TransactionDate", "CashOpeningBal", "CashAmount", "CashBal", "ChequeOpeningBal", "ChequeAmount", "ChequeBal", "FullNames", "UserId", "TransactionDetailsId", "Description", "TellerDescription","POSName"};
            dtDistinct = dt.DefaultView.ToTable(true,filter);
            gvData.DataSource = dtDistinct;
            gvData.DataBind();
            if (gvData.Rows.Count > 0)
            {
                btnPrint.Visible = true;
            }
            else
            {
                btnPrint.Visible = false;
            }
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
            ReportDocument rpt = new ReportDocument();
            rpt.FileName = Server.MapPath("~/Reports/rptDailyReport.rpt");
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