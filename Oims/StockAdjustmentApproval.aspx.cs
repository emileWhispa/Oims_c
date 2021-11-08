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

public partial class StockAdjustmentApproval : System.Web.UI.Page
{
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
    Bl bl;

    protected void Page_Load(object sender, EventArgs e)
    {
        lblError.Text = "";
        bl = new Bl(conn);

        if (!IsPostBack)
        {
            var pos = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlPOS, pos, true);
        }
    }

    private void refreshAdjustments()
    {
        gvData.DataSource = Toolkit.dataDisplay(conn, "Select * from Vw_StockAdjustment where authorized='NO' and PosID=" + ddlPOS.SelectedValue + " and Authorized='No' order by AdjustmentDate desc");
        gvData.DataBind();
    }
    protected void ddlPOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        refreshAdjustments();
    }
    protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string id = gvData.Rows[gvData.SelectedIndex].Cells[0].Text;
            DataTable dt = new DataTable();
            dt = Toolkit.dataDisplay(conn, "Select * from StockAdjustment where StockAdjustmentID=" + id);
            if (dt == null || dt.Rows.Count == 0)
            {
                lblError.Text = "Unable to retrieve adjustment details";
                return;
            }
            string action = dt.Rows[0].ItemArray.GetValue(3).ToString();
            string qty = dt.Rows[0].ItemArray.GetValue(4).ToString();
            string prodId = dt.Rows[0].ItemArray.GetValue(2).ToString();
            //string posID=dt.Rows[0].ItemArray.GetValue(1).ToString();
            string comments = dt.Rows[0].ItemArray.GetValue(5).ToString();
            long res=Toolkit.RunSqlCommand("exec Sp_UpdateStock " + ddlPOS.SelectedValue + "," + action + "," + qty + "," + prodId + ",'" + comments + "'," + Session["UserId"].ToString(), conn);
            Toolkit.RunSqlCommand("update stockadjustment set authorized=1 where StockAdjustmentID=" + id, conn);
            refreshAdjustments();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}