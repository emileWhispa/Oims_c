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

public partial class TellerStatement : System.Web.UI.Page
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
                var usr = (from b in bl.getUsers().OrderBy(x => x.FullNames).ToList() select new ListItem { Text = b.FullNames, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(cmbTeller, usr, true);
                Util.LogAudit(this.Page, "Accessed Page", this.Title);
            }
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
            dt = Toolkit.dataDisplay(conn, "Select * from Vw_Teller_Details where UserId=" + cmbTeller.SelectedValue + " and TransactionDate>='" + txtStartDate.Text + "' and TransactionDate<dateadd(d,1,'" + txtEndDate.Text + "')");
            gvData.DataSource = dt;
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
    private void loadAmount()
    {
        try
        {
            //Getting cash and cheque amount from TellerBalances
            DataTable dtBal = Toolkit.dataDisplay(conn, "Select TotalCash, TotalCheque from TellerBalances where UserID=" +cmbTeller.SelectedValue);
            if (dtBal != null && dtBal.Rows.Count == 1)
            {
                if (decimal.Parse(dtBal.Rows[0].ItemArray.GetValue(0).ToString()) == 0)
                    txtCashBalance.Text = "0";
                else
                    txtCashBalance.Text = decimal.Parse(dtBal.Rows[0].ItemArray.GetValue(0).ToString()).ToString("#,###");
                if (decimal.Parse(dtBal.Rows[0].ItemArray.GetValue(1).ToString()) == 0)
                    txtChequeBalance.Text = "0";
                else
                    txtChequeBalance.Text = decimal.Parse(dtBal.Rows[0].ItemArray.GetValue(1).ToString()).ToString("#,###");
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
    protected void cmbTeller_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cmbTeller.SelectedIndex != 1)
            loadAmount();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = Toolkit.dataDisplay(conn, "Select '" + txtStartDate.Text + "' as StartDate, '" + txtEndDate.Text + "' as EndDate,* from Vw_Teller_Details where UserId=" + cmbTeller.SelectedValue + " and TransactionDate>='" + txtStartDate.Text + "' and TransactionDate<dateadd(d,1,'" + txtEndDate.Text + "')");
            ReportDocument rpt = new ReportDocument();
            rpt.FileName = Server.MapPath("~/Reports/rptTellerStatement.rpt");
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