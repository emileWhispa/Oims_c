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
    public partial class Banks : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            loadBanks();
            Util.LogAudit(this.Page, "Accessed Page", this.Title);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtBankId.Text != "")
                {
                    var bk = bl.getBank(int.Parse(txtBankId.Text));
                    bk.BankName = txtBankName.Text;
                    bl.saveBank(bk);
                    lblMessage.Text = "Bank updated successfully!.";
                }
                else
                {
                    orion.ims.DAL.Bank B = new DAL.Bank
                    {
                        BankName = txtBankName.Text
                    };
                    bl.saveBank(B);
                    lblMessage.Text = "Bank saved successfully!.";

                }
                btnCancel_Click(null, null);
                loadBanks();
            }
            catch (Exception ex)
            {
                Util.ShowMsgBox(this,ex.Message, "Error while saving", MsgBoxType.Error);

            }

        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            try
            {
                if (e.CommandName == "edt")
                {
                    loadBanks();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtBankId.Text = hf.Value;
                    int id = int.Parse(txtBankId.Text);
                    var x = bl.getBank(id);
                    txtBankName.Text = x.BankName;
                   
                }
                else if (e.CommandName == "del")
                {
                    loadBanks();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getBank(id);
                    if (c != null)
                    {
                        bl.deleteBank(id);
                        lblMessage.Text = "Bank Details Deleted Suceesfully";
                        Util.ShowMsgBox(this, "Bank Details Deleted Suceesfully", "User", MsgBoxType.Info);
                        loadBanks();
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
            loadBanks();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtBankId.Text = "";
            txtBankName.Text = "";
            
        }

        private void loadBanks()
        {
            //gvData
            var data = bl.getBanks().OrderBy(X=>X.Id).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }
    }
}