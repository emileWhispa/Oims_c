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

public partial class POSItemTransfer : System.Web.UI.Page
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
                var pos = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlPos, pos, true);
                var prod = (from c in bl.getProducts().ToList() select new ListItem { Text = c.ProductName, Value = c.Id.ToString() }).ToList();
                Util.loadCombo(ddlProduct, prod, true);
                Util.LogAudit(this.Page, "Accessed Page", this.Title);

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
            if (e.CommandName == "sel")
            {
                loaddata();
                int rowId = Convert.ToInt32(e.CommandArgument);
                var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                hfId.Text = hf.Value;
                int id = int.Parse(hfId.Text);
                var x = bl.getPOSItemTransfer(id);
                if (x.Status != 0)//Submitted
                {
                    lblError.Text = "You cannot edit the request due to its status (Submitted, Approved or Cancelled)";
                    return;
                }
                var prod = (from c in bl.getProducts().ToList() select new ListItem { Text = c.ProductName, Value = c.Id.ToString() }).ToList();
                Util.loadCombo(ddlProduct, prod, true);

                ddlProduct.SelectedIndex = ddlProduct.Items.IndexOf(ddlProduct.Items.FindByValue(x.ProductId.ToString()));
                ddlPos.SelectedIndex = ddlPos.Items.IndexOf(ddlPos.Items.FindByValue(x.SupplyingPOSId.ToString()));
                txtQuantity.Text = x.Quantity.ToString("#.##");
                txtRemarks.Text = x.Remarks;

            }
            else if (e.CommandName == "del")
            {
                loaddata();
                int rowId = Convert.ToInt32(e.CommandArgument);
                var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                hfId.Text = hf.Value;
                int id = int.Parse(hfId.Text);
                var c = bl.getPOSItemTransfer(id);
                
                if (c != null)
                {
                    if (c.Status > 0)//Submitted
                    {
                        lblError.Text = "Unable to delete a submitted request";
                        return;
                    }
                    bl.deletePOSItemTransfer(id);
                    lblError.Text = "POS Transfer Deleted Succesfully";
                    Util.ShowMsgBox(this, "POS Transfer Deleted Succesfully", "POS Transfer Item Deletion", MsgBoxType.Info);
                }
                loaddata();
            }
            else if (e.CommandName == "sub")
            {
                loaddata();
                int rowId = Convert.ToInt32(e.CommandArgument);
                var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                hfId.Text = hf.Value;
                int id = int.Parse(hfId.Text);
                var c = bl.getPOSItemTransfer(id);
                if (c != null)
                {
                    if (c.Status > 0)//Submitted
                    {
                        lblError.Text = "This request has been already submitted";
                        return;
                    }
                }
                c.Status = 1;
                bl.savePOSItemTransfer(c);
                loaddata();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
            //Util.ShowMsgBox(this, ex.Message, "Error", MsgBoxType.Error);
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
            gvData.DataSource = Toolkit.dataDisplay(conn, "Select a.TransferId as Id,b.POSName, c.FullNames as Requester, a.RequestDate, d.ProductName,a.Quantity, case a.status when 0 then 'Pending' when 1 then 'Submitted' when 2 then 'Approved' when -1 then 'Cancelled' end 'Status',a.Remarks from POSItemTransfers a, POSes b, Users c, Products d where a.RequestPOSId=b.POSId" +
                " and a.MakerId=c.UserId and a.ProductId=d.ProductId and a.RequestPOSId=" + usr.POSID + " order by a.TransferId desc");
            gvData.DataBind();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (hfId.Text != "")//Update
            {
                int id = int.Parse(hfId.Text);
                var postfr = bl.getPOSItemTransfer(id);
                postfr.ProductId = int.Parse(ddlProduct.SelectedValue);
                postfr.Quantity = int.Parse(txtQuantity.Text);
                postfr.Remarks = txtRemarks.Text;
                postfr.SupplyingPOSId = int.Parse(ddlPos.SelectedValue);
                bl.savePOSItemTransfer(postfr);
            }
            else
            {
                var usr = bl.getUser(int.Parse(Session["UserId"].ToString()));
                if (usr == null)
                {
                    lblError.Text = "Unable to retrieve the POS of the logged User";
                    return;
                }
                orion.ims.DAL.POSItemTransfer x = new orion.ims.DAL.POSItemTransfer
                {
                    SupplyingPOSId = int.Parse(ddlPos.SelectedValue),
                    ProductId = int.Parse(ddlProduct.SelectedValue),
                    Quantity = int.Parse(txtQuantity.Text),
                    Remarks = txtRemarks.Text,
                    MakerId=int.Parse(Session["UserId"].ToString()),
                    RequestDate=DateTime.Today,
                    TransferDate=DateTime.Today,
                    RequestPOSId=usr.POSID
                };
                bl.savePOSItemTransfer(x);
            }
            clearControls();
            loaddata();
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
        
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        clearControls();
    }

    private void clearControls()
    {
        ddlProduct.SelectedIndex = -1;
        ddlPos.SelectedIndex = -1;
        txtQuantity.Text = "";
        txtRemarks.Text = "";
        hfId.Text = "";
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static int GetNextValue(int current, string tag)
    {
        return default(int);
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static int GetPreviousValue(int current, string tag)
    {
        return default(int);
    }
    protected void txtSearchItem_TextChanged(object sender, EventArgs e)
    {
        var prod = (from c in bl.getProducts().Where(x => x.ProductName.Contains(txtSearchItem.Text)).OrderBy(x => x.ProductName).ToList() select new ListItem { Text = c.ProductName, Value = c.Id.ToString() }).ToList();
        Util.loadCombo(ddlProduct, prod, true);
    }
}