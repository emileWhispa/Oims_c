    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using bMobile.Shared;
using Bmat.Tools;
using System.Data;

public partial class POSTransferAuth : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {

        lblError.Text = "";
        try
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                loaddata();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }

    }
    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "approve")
            {
                loaddata();
                int rowId = Convert.ToInt32(e.CommandArgument);
                var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                hfId.Text = hf.Value;
                int id = int.Parse(hfId.Text);
                var c = bl.getPOSItemTransfer(id);
                if (c!=null && c.Status == 2)
                {
                    lblError.Text = "Already Approved";
                    return;
                }
                c.Status = 2;
                //Updating stores (RequestPOSId and SupplingPOSId)
                var supPOS = bl.getPOS(c.SupplyingPOSId);
                var reqPOS = bl.getPOS(c.RequestPOSId);
                if (supPOS != null && reqPOS != null)
                {
                    DataTable dt = Toolkit.dataDisplay(conn, "exec Sp_UpdateStock " + c.SupplyingPOSId.ToString() + ",0," + c.Quantity.ToString() + "," + c.ProductId.ToString() + ",'Transfer to " + reqPOS.POSName + "'," + Session["UserId"].ToString());//Decrease
                    int res = 0;
                    if (dt != null && dt.Rows.Count == 1)
                        int.TryParse(dt.Rows[0].ItemArray.GetValue(0).ToString(), out res);
                    if (res == 1)
                        Toolkit.RunSqlCommand("exec Sp_UpdateStock " + c.RequestPOSId.ToString() + ",1," + c.Quantity.ToString() + "," + c.ProductId.ToString() + ",'Transfer from " + supPOS.POSName + "'," + Session["UserId"].ToString(), conn);//Increase
                    if (res <= 0)
                        lblError.Text = "The system has failed to adjust stock quantities for Requesting and Supplying POSes. Contact Administrator";
                    else
                        bl.savePOSItemTransfer(c);
                    loaddata();
                }
                else
                {
                    lblError.Text = "Unable to determine the POSes";
                }
            }
            else if (e.CommandName == "decline")
            {
                loaddata();
                int rowId = Convert.ToInt32(e.CommandArgument);
                var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                hfId.Text = hf.Value;
                int id = int.Parse(hfId.Text);
                var c = bl.getPOSItemTransfer(id);
                if (c != null && c.Status == 2)
                {
                    lblError.Text = "Already Approved";
                    return;
                }
                c.Status = -1;
                bl.savePOSItemTransfer(c);
                loaddata();
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
            var usr = bl.getUser(int.Parse(Session["UserId"].ToString()));
            gvData.DataSource = Toolkit.dataDisplay(conn, "Select a.TransferId as Id,b.POSName, c.FullNames as Requester, a.RequestDate, d.ProductName,a.Quantity,case a.status when 0 then 'Pending' when 1 then 'Submitted' when 2 then 'Approved' when -1 then 'Cancelled' end 'Status',a.Remarks from POSItemTransfers a, POSes b, Users c, Products d where a.SupplyingPOSId=b.POSId" +
                " and a.MakerId=c.UserId and a.ProductId=d.ProductId and a.SupplyingPOSId=" + usr.POSID + " and a.status<>0 order by a.TransferId desc");
            gvData.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
}