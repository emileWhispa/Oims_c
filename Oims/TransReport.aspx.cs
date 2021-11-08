using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using bMobile.Shared;

namespace orion.ims
{
    public partial class TransReport : System.Web.UI.Page
    {
        Bl bl;
        string conn = Util.DbConnectionString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                var l = bl.GetReports().ToList();
                lbReports.DataSource = l;
                lbReports.DataTextField = "Title";
                lbReports.DataValueField = "Id";
                lbReports.DataBind();
                txtDateFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtDateTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                List<ReportFilters> filters = new List<ReportFilters>();
                int id = 0;
                if (id!=0)// (!Int32.TryParse(hfRId.Value, out id))
                {
                    Util.ShowMsgBox(this, "Select report to print first!", "OIMS", MsgBoxType.Error);
                    return;
                }
                Report rep = bl.GetReport(id);

                var filter = new ReportFilters();
                string rptDir = MapPath("~/reports");
                ReportEngine re = new ReportEngine(rptDir);

                ReportingType typ;
                if (rblReportType.SelectedIndex == 0)
                    typ = ReportingType.PDF;
                else if (rblReportType.SelectedIndex == 1)
                    typ = ReportingType.Excel;
                else
                    typ = ReportingType.Html;

                ReportDetails rd = new ReportDetails
                {
                    Filters = filters,
                    ReportId = id,
                    //FboId = fboId,
                    //EventId = eId,
                //    RptCategory = int.Parse(hfCat.Value)
                };
                string rpt = re.ViewReport(rd, typ);
                if (rpt.Contains("No data to display!"))
                {
                    Util.ShowMsgBox(Page, "No Data to Display", "No Data", MsgBoxType.Alert);
                    return;
                }
                Console.WriteLine(rpt);
                Util.RedirectURl(rpt, "_blank", "");
            }
            catch (Exception ex)
            {
            }
        }

        protected void lbReports_SelectedIndexChanged(object sender, EventArgs e)
        {
            //hfRId.Value = lbReports.SelectedValue;
        }

        protected void ddlchannel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
}
}