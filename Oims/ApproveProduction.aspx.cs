using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using System.Data.SqlClient;
using System.Data;

using Bmat.Tools;

namespace orion.ims
{
    public partial class ApproveProduction : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            if (!IsPostBack)
            {
                loadData();
                 Util.LogAudit(this.Page, "Accessed Page", this.Title);
            }
        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "sel")
                {
                    loadData();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                     var x = bl.getProduction(int.Parse(hf.Value));
                     hfId.Text = hf.Value;
                     x.Authorized = 2;
                     bl.saveProduction(x);
                     string sSql = "Sp_UpdateProductionStock " + x.Id + "";
                     var conn = new SqlConnection(SessionData.DBConString);
                     var tcmd = new SqlCommand(sSql, conn);
                     tcmd.Connection.Open();
                     tcmd.ExecuteNonQuery();
                     loadData();
                     Util.ShowMsgBox(this, "Production Approved Succesfully", "Production", MsgBoxType.Info);
                }
                else if(e.CommandName == "view")
                {
                    loadData();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtId.Text = hf.Value.ToString();
                    loadItems(int.Parse(hf.Value));
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
            loadData();
        }
        protected void gvitems_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvitems.PageIndex = e.NewPageIndex;
            loadItems(int.Parse(hfId.Text));
        }
        private void loadItems(int Id)
        {
            //gvData
            var data = bl.getProductionItems(Id).ToList();
            gvitems.DataSource = data;
            gvitems.DataBind();
            if (gvitems.Rows.Count > 0)
            {
                btnApprove.Visible = true;
                btnDecline.Visible = true;
                txtcomments.Visible = true;
            }
            else
            {
                btnApprove.Visible = false;
                btnDecline.Visible = false;
                txtcomments.Visible = false;
                txtcomments.Text = "";
            }
        }

        private void loadData()
        {
            //gvData
            var data = bl.getProductions().Where(x=>x.Authorized==1).OrderByDescending(x=>x.Id).ToList();
            gvData.DataSource = data;
            gvData.DataBind();
            gvitems.DataSource = null;
            gvitems.DataBind();
            btnApprove.Visible = false;
            btnDecline.Visible = false;
            txtcomments.Visible = false;
            txtcomments.Text = "";
           
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {

                string Id = txtId.Text;
                Toolkit.RunSqlCommand("update Productions set Authorized= 2 where ProductionId =" + txtId.Text,conn);

                //var x = bl.getProduction(int.Parse(txtId.Text));

                string sSql = "Sp_UpdateProductionStock " + txtId.Text + ","+Session["UserId"].ToString();
                var con = new SqlConnection(SessionData.DBConString);
                var tcmd = new SqlCommand(sSql, con);
                tcmd.Connection.Open();
                tcmd.ExecuteNonQuery();
                loadData();
               // Util.ShowMsgBox(this, "Production Approved Succesfully", "Production", MsgBoxType.Info);

                //Response.Redirect("Production.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnDecline_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            try
            {
                Toolkit.RunSqlCommand("update Productions set Authorized= 0, Remarks = Remarks +' | Comments: " + txtcomments.Text + "' where ProductionId =" + txtId.Text, conn);
                loadData();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
}
}