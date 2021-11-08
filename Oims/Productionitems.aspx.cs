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
    public partial class Productionitems : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                if (Request.QueryString.HasKeys())
                {
                    txtbatchno.Text = Request.QueryString["id"];
                    loaddata();
                    var l = (from b in bl.getProducts().OrderBy(y=>y.ProductName).ToList() select new ListItem { Text = b.ProductName, Value = b.Id.ToString() }).ToList();
                    Util.loadCombo(ddlProduct, l, true);
                    Util.LogAudit(this.Page, "Accessed Page", this.Title);
                }
            }
           
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var b = bl.getProduction(txtbatchno.Text.Trim());
            if (hfId.Text != "")
            {
                var proditem = bl.getProductionItem(int.Parse(hfId.Text));
                proditem.ProductId = Convert.ToInt16(ddlProduct.SelectedValue);
                proditem.Units = int.Parse(txtunits.Text);
                proditem.ProductionId = b.Id;
                bl.saveProductionItem(proditem);
            }
            else
            {
                orion.ims.DAL.ProductionItem p = new DAL.ProductionItem
                {
                    ProductId = Convert.ToInt16(ddlProduct.SelectedValue),
                    Product = bl.getProduct(Convert.ToInt16(ddlProduct.SelectedValue)),
                    Units = int.Parse(txtunits.Text),
                    ProductionId = b.Id

                };
                bl.saveProductionItem(p);
            }
            //Util.ShowMsgBox(this, "Production Items saved Successfully", "Production", MsgBoxType.Info);
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
                    var x = bl.getProductionItem(id);
                    var l = (from b in bl.getProducts().OrderBy(y => y.ProductName).ToList() select new ListItem { Text = b.ProductName, Value = b.Id.ToString() }).ToList();
                    Util.loadCombo(ddlProduct, l, true);

                    ddlProduct.SelectedIndex = ddlProduct.Items.IndexOf(ddlProduct.Items.FindByValue(x.ProductId.ToString()));
                    txtunits.Text = x.Units.ToString();
                   
                }
                else if (e.CommandName == "del")
                {
                    loaddata();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getProductionItem(id);
                    if (c != null)
                    {
                        bl.deleteProductionItem(id);
                        //Util.ShowMsgBox(this, "Production Item Deleted Suceesfully", "Production", MsgBoxType.Info);
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
            txtunits.Text = "";
            hfId.Text = "";
            ddlProduct.SelectedIndex = -1;
            
        }

        private void loaddata()
        {
            var b = bl.getProduction(txtbatchno.Text.Trim());
            var data = bl.getProductionItems(b.Id).OrderByDescending(x=>x.Id).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
            if (gvData.Rows.Count > 0)
            {
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
            }
        }
        protected void btnnew_Click(object sender, EventArgs e)
        {
            hfId.Text = "";
            txtunits.Text = "";
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                Toolkit.RunSqlCommand("update Productions set Authorized=1 where BatchNo =" + txtbatchno.Text, conn);
                Response.Redirect("Production.aspx");
            }
            catch (Exception ex)
            {
                lbrError.Text = ex.Message;
            }


        }
        protected void txtSearchItem_TextChanged(object sender, EventArgs e)
        {
            var prod = (from c in bl.getProducts().Where(x => x.ProductName.Contains(txtSearchItem.Text)).OrderBy(x => x.ProductName).ToList() select new ListItem { Text = c.ProductName, Value = c.Id.ToString() }).ToList();
            Util.loadCombo(ddlProduct, prod, true);
        }
}
}