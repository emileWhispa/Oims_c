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

namespace orion.ims
{
    public partial class Stockitems : System.Web.UI.Page
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
                    var pos = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
                    Util.loadCombo(cmbPOS, pos, true);
                    Util.LogAudit(this.Page, "Accessed Page", this.Title);
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
            loaddata();
        }

        private void loaddata()
        {
            try
            {
                int posId=int.Parse(cmbPOS.SelectedValue);
                var data = bl.getStockItems().Where(x => x.posid ==posId).ToList();
                gvData.DataSource = data;
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

        protected void cmbPOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbPOS.SelectedIndex != -1)
            {
                loaddata();
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = Toolkit.dataDisplay(conn, "Select * from Vw_StockBalance where POSId=" + cmbPOS.SelectedValue+" order by ProductName");
                ReportDocument rpt = new ReportDocument();
                rpt.FileName = Server.MapPath("~/Reports/rptStockBalance.rpt");
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
        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSearchItem.Text.Trim() != "")
                {
                    int posId = int.Parse(cmbPOS.SelectedValue);
                    var data = bl.getStockItems().Where(x => x.posid == posId && x.Product.ProductName.Contains(txtSearchItem.Text.Trim())).ToList();
                    gvData.DataSource = data;
                    gvData.DataBind();
                    if (gvData.Rows.Count > 0)
                        btnPrint.Visible = true;
                    else
                        btnPrint.Visible = false;
                }
                else
                    loaddata();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
}
}