using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using orion.ims.BL;
using orion.ims.DAL;
using System.Diagnostics;
using System.Web.UI.WebControls;
using Bmat.Tools;
using System.Data;
using System.Data.Sql;

namespace orion.ims
{
    public partial class Discounts : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                bl = new Bl(conn);
                if (!IsPostBack)
                {
                    loadDiscounts();
                    var l = (from b in bl.getCustomers().ToList() select new ListItem { Text = b.CustomerName, Value = b.Id.ToString() }).ToList();
                    Util.loadCombo(ddlcustomer, l, true);
                }
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
                if (txtdiscountId.Text != "")
                {
                    var disco = bl.getDiscount(int.Parse(txtdiscountId.Text));
                    disco.CustomerId = Convert.ToInt16(ddlcustomer.SelectedValue);
                    disco.DiscountCode = txtdiscountCode.Text;
                    disco.DiscountRate = Convert.ToDecimal(txtdiscrate.Text);
                    disco.DiscountDate = DateTime.Now;
                    disco.Comments = txtcomments.Text;
                    disco.Maker = int.Parse(Session["UserId"].ToString());

                    bl.saveDiscount(disco);

                }
                else
                {
                    orion.ims.DAL.Discount dsc = new DAL.Discount
                     {
                         customer = bl.getCustomer(int.Parse(ddlcustomer.SelectedValue)),
                         user = bl.getUser(int.Parse(Session["UserId"].ToString())),
                         CustomerId = Convert.ToInt16(ddlcustomer.SelectedValue),
                         DiscountCode = txtdiscountCode.Text,
                         DiscountRate = Convert.ToDecimal(txtdiscrate.Text),
                         DiscountDate = DateTime.Now,
                         Comments = txtcomments.Text,
                         Maker = int.Parse(Session["UserId"].ToString())
                     };
                    bl.saveDiscount(dsc);
                }
                loadDiscounts();
                btnCancel_Click(null, null);
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        private void loadDiscounts()
        {
            var data = bl.getDiscounts().OrderByDescending(x => x.Id).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtdiscountCode.Text = "";
            txtcomments.Text = "";
            txtdiscountId.Text = "";
            txtdiscrate.Text = "";
            ddlcustomer.SelectedIndex = -1;
            // txtdiscDate.Text = "";
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {

                    loadDiscounts();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtdiscountId.Text = hf.Value;
                    int id = int.Parse(txtdiscountId.Text);
                    var x = bl.getDiscount(id);
                    ddlcustomer.SelectedIndex = ddlcustomer.Items.IndexOf(ddlcustomer.Items.FindByValue(x.CustomerId.ToString()));
                    txtdiscountCode.Text = x.DiscountCode;
                    txtdiscrate.Text = x.DiscountRate.ToString();
                    txtcomments.Text = x.Comments;
                    loadDiscounts();
                }
                else if (e.CommandName == "del")
                {
                    loadDiscounts();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getDiscount(id);

                    Bmat.Tools.BTSecurity ln = new BTSecurity();
                    string slt = ln.GenerateSalt(9);
                    if (c != null)
                    {
                        bl.deleteDiscount(id);
                        //Util.LogAudit(this.Page, "Deleted Account " + txtaccntnme.Text, this.Title);
                        loadDiscounts();
                    }

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
            loadDiscounts();
        }
        protected void ddlcustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlcustomer.SelectedIndex == -1)
                    return;
                string code = ddlcustomer.SelectedItem.Text.Replace(" ", "").Substring(0, 3).ToUpper() + ddlcustomer.SelectedValue;
                txtdiscountCode.Text = code.ToUpper();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        protected void txtSearchCustomer_TextChanged(object sender, EventArgs e)
        {
            var cust = (from b in bl.getCustomers().Where(x => x.CustomerName.Contains(txtSearchCustomer.Text)).OrderBy(x => x.CustomerName).ToList() select new ListItem { Text = b.CustomerName, Value = b.Id.ToString() }).ToList();
            Util.loadCombo(ddlcustomer, cust, true);
        }
}
}