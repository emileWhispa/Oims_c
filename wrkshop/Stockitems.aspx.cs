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

namespace orion.ims
{
    public partial class POSorders : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                   loaddata();
                  var pos = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
                    Util.loadCombo(ddlPos, pos, true);
                    Util.LogAudit(this.Page, "Accessed Page", this.Title);
                   
            }
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (hfId.Text != "")
            {
                var sp = bl.getPOSOrder(int.Parse(hfId.Text));
                sp.POSId = Convert.ToInt16(ddlPos.SelectedValue);
                 sp.OrderDate = Convert.ToDateTime(txtorderdate.Text);
               sp.AddInfo = txtdescr.Text;
               bl.savePOSOrder(sp);
            }
            else
            {
                orion.ims.DAL.POSOrder p = new DAL.POSOrder
                {
                    AddInfo=txtdescr.Text,
                    POSId=Convert.ToInt16(ddlPos.SelectedValue),
                    Pose = bl.getPOS(int.Parse(ddlPos.SelectedValue)),
                    OrderNo= DateTime.Now.ToString("yyMMddHHmmss"),
                    OrderDate = Convert.ToDateTime(txtorderdate.Text),
                    UserId=SessionData.UserId,
                    OrderStatus=0
                 };
                bl.savePOSOrder(p);
            }
            Util.ShowMsgBox(this, "POS Order saved Successfully", "Orders", MsgBoxType.Info);
            btnCancel_Click(null, null);
            loaddata();

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
                    var x = bl.getPOSOrder(id);
                    ddlPos.SelectedIndex = ddlPos.Items.IndexOf(ddlPos.Items.FindByValue(x.POSId.ToString()));
                    txtdescr.Text = x.AddInfo;
                    txtorderdate.Text = x.OrderDate.ToString();
                }
                else if (e.CommandName == "del")
                {
                    loaddata();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getPOSOrder(id);
                    if (c != null)
                    {
                        bl.deletePOSOrder(id);
                        Util.ShowMsgBox(this, "POS Order Deleted Suceesfully", "Production", MsgBoxType.Info);
                    }
                }
                else if (e.CommandName == "sub")
                {
                    loaddata();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getPOSOrder(id);
                    if (c != null)
                    {
                        var itm = bl.getOrderItems(c.Id);
                        if (itm.Count() == 0)
                        { Util.ShowMsgBox(this, "POS Order has no Items and cannot be Submitted", "Pos Orders", MsgBoxType.Error); 
                        
                        }
                        else
                        {
                            c.OrderStatus = OrderStatus.Submitted;
                            bl.savePOSOrder(c);
                            Util.ShowMsgBox(this, "POS Order Submitted Succesfully", "Pos Orders", MsgBoxType.Info);
                        }
                    }
                }
                loaddata();
            }
            catch (Exception ex)
            {

                string fx, caller;
                //Common.Errorlog(new StackTrace(), new StackFrame(), out fx, out caller);
                //Common.LogError(ex.Message, fx, caller);
            }
        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loaddata();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtdescr.Text = "";
            
        }

        private void loaddata()
        {
            var data = bl.getPOSOrders().Where(x=>x.OrderStatus==0).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }
        protected void btnnew_Click(object sender, EventArgs e)
        {
            hfId.Text = "";
            txtdescr.Text = "";
        }
}
}