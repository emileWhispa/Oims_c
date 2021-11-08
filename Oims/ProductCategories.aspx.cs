using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.BL;
using orion.ims.DAL;
using Bmat.Tools;
using System.Diagnostics;
namespace orion.ims
{

    public partial class ProductCategories : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            loadProductCategories();

        }
        private void loadProductCategories()
        {
            var data = bl.getProductCategories().OrderByDescending(x=>x.Id).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtProductCategoryId.Text != "")
            {
                var prct = bl.getProductCategory(int.Parse(txtProductCategoryId.Text));
                prct.CategoryName = txtcategryName.Text;

                bl.saveProductCategory(prct);
                lblMessage.Text = "Product Category updated successfully!.";
            }
            else
            {
                orion.ims.DAL.ProductCategory prdcat = new DAL.ProductCategory
                {
                    CategoryName = txtcategryName.Text
                };
                bl.saveProductCategory(prdcat);
                lblMessage.Text = "Product Category updated successfully!.";

            }
            btnCancel_Click(null,null);
            loadProductCategories();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtcategryName.Text = "";
            txtProductCategoryId.Text = "";
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {
                    loadProductCategories();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtProductCategoryId.Text = hf.Value;
                    int id = int.Parse(txtProductCategoryId.Text);
                    var x = bl.getProductCategory(id);
                    txtcategryName.Text = x.CategoryName;

                }
                else if (e.CommandName == "del")
                {
                    loadProductCategories();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getProductCategory(id);
                    if (c != null)
                    {
                        bl.deleteProductCategory(id);
                        lblMessage.Text="Product Category deleted successfuly.";
                        //Util.ShowMsgBox(this, "Bank Details Deleted Suceesfully", "User", MsgBoxType.Info);
                        loadProductCategories();
                    }
                }
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
            loadProductCategories();
        }
}
}