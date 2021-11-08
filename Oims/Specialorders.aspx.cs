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

namespace orion.ims
{
    public partial class Specialorders : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                loaddata();
                var l = (from b in bl.getCustomers().ToList() select new ListItem { Text = b.CustomerName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlcustomers, l, true);
                var p = (from b in bl.getProductCategories().ToList() select new ListItem { Text = b.CategoryName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlcategory, p, true);
                var pos = (from b in bl.getPOSes().ToList() select new ListItem { Text = b.POSName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlcollectionpos, pos, true);
                Util.LogAudit(this.Page, "Accessed Page", this.Title);
                txtorderdate.Text = getPOSValueDate(conn, Session["POSId"].ToString());
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //var usr = bl.getUser(int.Parse(Session["UserId"].ToString()));
            if (hfId.Text != "")
            {
                var sp = bl.getSpecialOrder(int.Parse(hfId.Text));
                sp.Quantity = int.Parse(txtunits.Text);
                sp.OrderDate = Convert.ToDateTime(txtorderdate.Text);
                sp.ExpectedDeliveryDate = Convert.ToDateTime(txtdeliverydate.Text);
                sp.Descr = txtdescr.Text;
                sp.Amount = Convert.ToDecimal(txtamount.Text);
                sp.CustomerId = Convert.ToInt16(ddlcustomers.SelectedValue);
                sp.ProductCategoryId = Convert.ToInt16(ddlcategory.SelectedValue);
                sp.Sample = rdosample.Items[0].Selected ? true : false;
                sp.CollectionPosId = Convert.ToInt16(ddlcollectionpos.SelectedValue);
                sp.UserId = int.Parse(Session["UserId"].ToString());
                sp.POSId = int.Parse(Session["POSId"].ToString());
                bl.saveSpecialOrder(sp);
            }
            else
            {
                orion.ims.DAL.SpecialOrder p = new DAL.SpecialOrder
                {
                    Customer = bl.getCustomer(int.Parse(ddlcustomers.SelectedValue)),
                    ProductCategory = bl.getProductCategory(int.Parse(ddlcategory.SelectedValue)),
                    Quantity = int.Parse(txtunits.Text),
                    OrderDate = Convert.ToDateTime(txtorderdate.Text),
                    ExpectedDeliveryDate = Convert.ToDateTime(txtdeliverydate.Text),
                    Descr = txtdescr.Text,
                    Amount = Convert.ToDecimal(txtamount.Text),
                    CustomerId = Convert.ToInt16(ddlcustomers.SelectedValue),
                    ProductCategoryId = Convert.ToInt16(ddlcategory.SelectedValue),
                    Sample = rdosample.Items[0].Selected ? true : false,
                    CollectionPosId = Convert.ToInt16(ddlcollectionpos.SelectedValue),
                    POSId=int.Parse(Session["POSId"].ToString()),
                    UserId=int.Parse(Session["UserId"].ToString())
                };
                bl.saveSpecialOrder(p);
            }
            Util.ShowMsgBox(this, "Special Order saved Successfully", "Production", MsgBoxType.Info);
            btnCancel_Click(null, null);
            loaddata();
            clearControls();
        }

        private string getPOSValueDate(string con, string POSId)
        {
            string valueDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            DataTable dtValue = Toolkit.dataDisplay(con, "SELECT convert(varchar(25), dateadd(hh,15,getdate()),102)");//We are adding +10 hours//"Select convert(varchar(25),ValueDate,102) from ValueDates where POSId=" + POSId);
            //SELECT convert(varchar(25), dateadd(hh,10,getdate()),102)
            if (dtValue != null && dtValue.Rows.Count == 1)
                valueDate = dtValue.Rows[0].ItemArray.GetValue(0).ToString().Replace(".", "-");
            return valueDate;
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
                    var x = bl.getSpecialOrder(id);
                    ddlcategory.SelectedIndex = ddlcategory.Items.IndexOf(ddlcategory.Items.FindByValue(x.ProductCategoryId.ToString()));
                    txtunits.Text = x.Quantity.ToString();
                    ddlcollectionpos.SelectedIndex = ddlcollectionpos.Items.IndexOf(ddlcollectionpos.Items.FindByValue(x.CollectionPosId.ToString()));
                    var l = (from b in bl.getCustomers().ToList() select new ListItem { Text = b.CustomerName, Value = b.Id.ToString() }).ToList();
                    Util.loadCombo(ddlcustomers, l, true);

                    ddlcustomers.SelectedIndex = ddlcustomers.Items.IndexOf(ddlcustomers.Items.FindByValue(x.CustomerId.ToString()));
                    txtamount.Text = x.Amount.ToString();
                    txtdeliverydate.Text = x.ExpectedDeliveryDate.ToString("yyyy-MM-dd");
                    txtorderdate.Text = x.OrderDate.ToString("yyyy-MM-dd");
                    txtdescr.Text = x.Descr;
                    rdosample.Items[0].Selected = false;
                    rdosample.Items[1].Selected = false;
                    if (x.Sample == true)
                    { rdosample.Items[0].Selected = true; }
                    else
                        rdosample.Items[1].Selected = true;
                }
                else if (e.CommandName == "del")
                {
                    loaddata();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getSpecialOrder(id);
                    if (c != null)
                    {
                        bl.deleteSpecialOrder(id);
                        Util.ShowMsgBox(this, "Special Order Deleted Suceesfully", "Production", MsgBoxType.Info);
                        loaddata();
                    }
                }
                else if (e.CommandName == "sub")
                {
                    loaddata();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hfId.Text = hf.Value;
                    int id = int.Parse(hfId.Text);
                    var x = bl.getSpecialOrder(id);
                    x.OrderStatus = 1;//Submitted
                    bl.saveSpecialOrder(x);
                    loaddata();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                //string fx, caller;
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
            txtunits.Text = "";

        }

        private void loaddata()
        {
            DataTable data = new DataTable();
            data = Toolkit.dataDisplay(conn, "Select a.SpecialOrderId as id, a.*,c.CustomerName,d.CategoryName, b.POSName as [OriginPOS],(Select POSName from POSes where POSId=a.CollectionPOS) as [CollectionPOSName] from SpecialOrders a, Customers c, POSes b, ProductCategories d where a.POSId=b.POSId  and a.POSId="+Session["POSId"].ToString()+" and a.ProductCategoryId = d.ProductCategoryId and a.CustomerId=c.CustomerId and a.OrderStatus=0");
            gvData.DataSource = data;
            gvData.DataBind();
        }

        private void clearControls()
        {
            hfId.Text = "";
            txtunits.Text = "";
            txtdescr.Text = "";
            txtamount.Text = "";
            ddlcategory.SelectedIndex = -1;
            ddlcustomers.SelectedIndex = -1;
            ddlcollectionpos.SelectedIndex = -1;
            //txtorderdate.Text = Toolkit.
            txtdeliverydate.Text = "";
        }
        protected void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            var cust = (from b in bl.getCustomers().Where(x => x.CustomerName.Contains(txtSearchCustomer.Text)).OrderBy(x => x.CustomerName).ToList() select new ListItem { Text = b.CustomerName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlcustomers, cust, true);
        }
}
}