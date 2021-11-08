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
    public partial class Production : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                loadData();
                txtproductiondate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                Util.LogAudit(this.Page, "Accessed Page", this.Title);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (hfId.Text != "")
            {
                var prod = bl.getProduction(int.Parse(hfId.Text));
                prod.Prod_Date = Convert.ToDateTime(txtproductiondate.Text);
                prod.Remarks = txtremarks.Text;
                //prod.BatchNo = txtbatchno.Text;
                prod.UserId = int.Parse(Session["UserId"].ToString());
                

                bl.saveProduction(prod);
            }
            else
            {
                orion.ims.DAL.Production p = new DAL.Production
                 {
                     BatchNo = DateTime.Now.ToString("yyMMddHHmmss"),
                     Remarks = txtremarks.Text,
                     Prod_Date = Convert.ToDateTime(txtproductiondate.Text),
                     UserId = int.Parse(Session["UserId"].ToString())
                 };
                bl.saveProduction(p);
            }
            //Util.ShowMsgBox(this, "Production Created successfully", "Production", MsgBoxType.Info);
            btnCancel_Click(null, null);
            loadData();

        }

        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loadData();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtremarks.Text = "";
            txtbatchno.Text = "";
            txtbatchno.Visible = false;
            lbrBatchno.Visible = false;
           
        }

        private void loadData()
        {
            //gvData
            var data = bl.getProductions().Where(x => x.Authorized == 0).OrderByDescending(x => x.Id).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {
                    loadData();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hfId.Text = hf.Value;
                    int id = int.Parse(hfId.Text);
                    var x = bl.getProduction(id);
                    txtproductiondate.Text = x.Prod_Date.ToString();
                    txtremarks.Text = x.Remarks;
                    txtbatchno.Text = x.BatchNo;
                    txtbatchno.Visible = true;
                    lbrBatchno.Visible = true;
                    


                }
                else if (e.CommandName == "del")
                {
                    loadData();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    hf.Value = hf.Value;
                    int id = int.Parse(hf.Value);
                    var c = bl.getProduction(id);
                    if (c != null)
                    {
                        bl.deleteProduction(id);
                       // Util.ShowMsgBox(this, "Production Details Deleted Succesfully", "Production", MsgBoxType.Info);
                        loadData();
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
}
}