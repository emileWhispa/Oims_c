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

    public partial class Customers : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                loadCustomers();
            }
        }
        private void loadCustomers()
        {
            try
            {
                if (txtSearchItem.Text.Trim() != "")
                {
                    var data = bl.getCustomers().Where(x => x.CustomerName.Contains(txtSearchItem.Text.Trim())).OrderByDescending(x => x.Id).ToList();
                    gvData.DataSource = data;
                    gvData.DataBind();

                }
                else
                {
                    var data = bl.getCustomers().OrderByDescending(x => x.Id).ToList();
                    gvData.DataSource = data;
                    gvData.DataBind();
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
                if (txtcustomerId.Text != "")
                {
                    var customer = bl.getCustomer(int.Parse(txtcustomerId.Text));
                    customer.CustomerName = txtcustname.Text;
                    customer.CreditLimit = Convert.ToDecimal(txtcrdlimit.Text);
                    customer.AddInfo = txtaddinfo.Text;
                    customer.Address = txtaddress.Text;
                    customer.PhoneNo = txtphoneNo.Text;
                    customer.Distributor = chkDistributo.Items[0].Selected ? true : false;

                    bl.saveCustomer(customer);
                    //lblMessage.Text = "Customer " + txtcustname.Text + " Updated successfully!.";
                }
                else
                {
                    orion.ims.DAL.Customer cust = new DAL.Customer
                    {
                        CustomerName = txtcustname.Text,
                        CreditLimit = Convert.ToDecimal(txtcrdlimit.Text),
                        AddInfo = txtaddinfo.Text,
                        Address = txtaddress.Text,
                        PhoneNo = txtphoneNo.Text,
                        Balance = 0,
                        Distributor = Convert.ToBoolean(chkDistributo.Items[0].Selected ? true : false),
                    };

                    bl.saveCustomer(cust);
                    //lblMessage.Text = "Customer Saved successfully!.";

                }
                TabContainer1.ActiveTabIndex = 1;
                btnCancel_Click(null, null);
                loadCustomers();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtcustomerId.Text = "";
            txtcustname.Text = "";
            txtcrdlimit.Text = "0";
            txtaddinfo.Text = "";
            txtaddress.Text = "";
            txtphoneNo.Text = "";
            chkDistributo.Items[0].Selected = false;          
        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loadCustomers();
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {

                    searchCustomer();
                    TabContainer1.ActiveTabIndex = 0;
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtcustomerId.Text = hf.Value;
                    int id = int.Parse(txtcustomerId.Text);
                    var x = bl.getCustomer(id);

                    txtcustname.Text = x.CustomerName;
                    txtcrdlimit.Text =  x.CreditLimit.ToString();
                    txtaddinfo.Text = x.AddInfo;
                    txtaddress.Text = x.Address;
                    txtphoneNo.Text = x.PhoneNo;
                    chkDistributo.Items[0].Selected = false;
                    //chkDistributo.Items[1].Selected = false;
                    if (x.Distributor == true)
                    {
                        chkDistributo.Items[0].Selected = true;
                    }
                    else
                        chkDistributo.Items[0].Selected = false;
                   


                    loadCustomers();
                }
                else if (e.CommandName == "del")
                {
                    searchCustomer();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getCustomer(id);

                    Bmat.Tools.BTSecurity ln = new BTSecurity();
                    string slt = ln.GenerateSalt(9);
                    if (c != null)
                    {
                        bl.deleteCustomer(id);
                        Util.LogAudit(this.Page, "Deleted Customer " + txtcustname.Text, this.Title);
                        loadCustomers();
                    }

                }
            }
            catch (Exception ex)
            {

                string fx, caller;
                Util.ErrorFunctions(new StackTrace(), new StackFrame(), out fx, out caller);
                Util.SysErrLog(ex.Message, fx, caller);
                lblError.Text = ex.Message;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            searchCustomer();
        }

        private void searchCustomer()
        {
            try
            {
                if (txtSearchItem.Text.Trim() != "")
                {
                    var data = bl.getCustomers().Where(x => x.CustomerName.Contains(txtSearchItem.Text.Trim())).OrderByDescending(x => x.Id).ToList();
                    gvData.DataSource = data;
                    gvData.DataBind();

                }
                else
                {
                    var data = bl.getCustomers().OrderByDescending(x => x.Id).ToList();
                    gvData.DataSource = data;
                    gvData.DataBind();
                }

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
}
}