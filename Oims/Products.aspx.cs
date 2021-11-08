using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using System.Diagnostics;
using Bmat.Tools;

namespace orion.ims
{
    public partial class Products : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                var pcat = (from b in bl.getProductCategories().ToList() select new ListItem { Text = b.CategoryName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlprodcategory, pcat,true);
                Util.LogAudit(this.Page, "Accessed Page", this.Title);
                loadProducts();
                if (Request.QueryString.HasKeys())
                {
                    txtproductId.Text = Request.QueryString["id"];
                    int id = int.Parse(txtproductId.Text);
                    var x = bl.getProduct(id);
                    txtprodname.Text = x.ProductName;
                    txtdescr.Text = x.Description;
                    txtreorder.Text = x.ReorderLevel.ToString();
                    txtpackingunit.Text = x.PackagingUnit;
                    txtsellingprice.Text = x.SellingPrice.ToString();
                    ddlprodcategory.SelectedIndex = ddlprodcategory.Items.IndexOf(ddlprodcategory.Items.FindByValue(x.ProductCategoryId.ToString()));
                }
            }
        }
        private void loadProducts()
        {
            var data = bl.getProducts().OrderByDescending(x=>x.Id).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtproductId.Text != "")
            {
                var product = bl.getProduct(int.Parse(txtproductId.Text));
                product.ProductName = txtprodname.Text;
                product.Description = txtdescr.Text;
                product.ReorderLevel = Convert.ToDecimal(txtreorder.Text);
                product.PackagingUnit = txtpackingunit.Text;
                product.SellingPrice = Convert.ToDecimal(txtsellingprice.Text);
                product.ProductCategoryId = Convert.ToInt16(ddlprodcategory.SelectedValue);

                bl.saveProduct(product);
                lblMessage.Text = "Product updated successfuly";
            }
            else
            {
                orion.ims.DAL.Product pd = new DAL.Product
                {
                    ProductName = txtprodname.Text,
                    Description = txtdescr.Text,
                    ReorderLevel = Convert.ToDecimal(txtreorder.Text),
                    PackagingUnit = txtpackingunit.Text,
                    SellingPrice = Convert.ToDecimal(txtsellingprice.Text),
                    ProductCategoryId = Convert.ToInt16(ddlprodcategory.SelectedValue)

                };
                bl.saveProduct(pd);
                lblMessage.Text = "Product updated successfuly";

            }
            btnCancel_Click(null, null);
            loadProducts();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtproductId.Text = "";
            txtprodname.Text = "";
            txtdescr.Text = "";
            txtreorder.Text = "";
            txtpackingunit.Text = "";
            txtsellingprice.Text = "";
            ddlprodcategory.SelectedIndex = -1;
        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loadProducts();
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {
                    loadProducts();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtproductId.Text = hf.Value;
                    int id = int.Parse(txtproductId.Text);
                    var x = bl.getProduct(id);
                    txtprodname.Text = x.ProductName;
                    txtdescr.Text = x.Description;
                    txtreorder.Text = x.ReorderLevel.ToString();
                    txtpackingunit.Text = x.PackagingUnit;
                    txtsellingprice.Text = x.SellingPrice.ToString();
                    ddlprodcategory.SelectedIndex = ddlprodcategory.Items.IndexOf(ddlprodcategory.Items.FindByValue(x.ProductCategoryId.ToString()));
                    loadProducts();
                }
                else if (e.CommandName == "del")
                {
                    loadProducts();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getProduct(id);

                    Bmat.Tools.BTSecurity ln = new BTSecurity();
                    string slt = ln.GenerateSalt(9);
                    if (c != null)
                    {
                        bl.deleteProduct(id);
                        lblMessage.Text = "Deleted product " + txtprodname.Text;
                        Util.LogAudit(this.Page, "Deleted product " + txtprodname.Text, this.Title);
                        loadProducts();
                    }

                }
            }
            catch (Exception ex)
            {

                string fx, caller;
                Util.ErrorFunctions(new StackTrace(), new StackFrame(), out fx, out caller);
                Util.SysErrLog(ex.Message, fx, caller);
            }
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            if (txtSearchItem.Text.Trim() != "")
            {
                var data = bl.getProducts().Where(y => y.ProductName.Contains(txtSearchItem.Text.Trim())).OrderByDescending(x => x.Id).ToList();
                gvData.DataSource = data;
                gvData.DataBind();
            }
            else
            {
                loadProducts();
            }
        }
        protected void gvData_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rowId = gvData.SelectedIndex;
            var hf = (HiddenField)(gvData.Rows[rowId].FindControl("hfKey"));
            txtproductId.Text = hf.Value;
            int id = int.Parse(txtproductId.Text);
            var x = bl.getProduct(id);
            txtprodname.Text = x.ProductName;
            txtdescr.Text = x.Description;
            txtreorder.Text = x.ReorderLevel.ToString();
            txtpackingunit.Text = x.PackagingUnit;
            txtsellingprice.Text = x.SellingPrice.ToString();
            ddlprodcategory.SelectedIndex = ddlprodcategory.Items.IndexOf(ddlprodcategory.Items.FindByValue(x.ProductCategoryId.ToString()));
            loadProducts();
        }
}
}