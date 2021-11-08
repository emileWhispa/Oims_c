using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.DAL;
using orion.ims.BL;
using Bmat.Tools;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;

namespace orion.ims
{

    public partial class CashCollections : System.Web.UI.Page
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
            try
            {
                if (!IsPostBack)
                {
                    bl = new Bl(conn);
                    loadAmount();
                    loadCashCollection();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        private void loadAmount()
        {
            //Getting cash and cheque amount from TellerBalances
            DataTable dtBal = Toolkit.dataDisplay(conn, "Select TotalCash, TotalCheque from TellerBalances where UserID=" + Session["UserId"].ToString());
            if (dtBal != null && dtBal.Rows.Count == 1)
            {
                if (decimal.Parse(dtBal.Rows[0].ItemArray.GetValue(0).ToString()) == 0)
                    txtcash.Text = "0";
                else
                    txtcash.Text = decimal.Parse(dtBal.Rows[0].ItemArray.GetValue(0).ToString()).ToString("#,###");
                if (decimal.Parse(dtBal.Rows[0].ItemArray.GetValue(1).ToString()) == 0)
                    txtcheque.Text = "0";
                else
                    txtcheque.Text = decimal.Parse(dtBal.Rows[0].ItemArray.GetValue(1).ToString()).ToString("#,###");
                if (CalculateTotalAmount() > 0)
                    txtTotalAmount.Text = CalculateTotalAmount().ToString("#,###");
                else
                    txtTotalAmount.Text = "0";
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //var usr = bl.getUser(Session["UserId"].ToString());
                string cmd = "";
                if (txtcollectionId.Text == "")
                    cmd = "exec sp_insert_cash_collection '" + txtCollector.Text.Replace("'", "''") + "'," + txtcash.Text.Replace(",", "") + "," + txtcheque.Text.Replace(",", "") + "," + Session["UserId"].ToString() + "," + Session["POSId"].ToString();

                Toolkit.RunSqlCommand(cmd, conn);
                //btnCancel_Click(null, null);
                loadAmount();
                loadCashCollection();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtCollector.Text = "";
            txtcash.Text = "";
            txtcheque.Text = "";
            txtTotalAmount.Text = "";
            txtcollectionId.Text = "";
        }
        private void loadCashCollection()
        {
            try
            {
                DataTable dt = new DataTable();

                dt = Toolkit.dataDisplay(conn, "select b.Cancelled,b.CashCollectionId as Id,b.collectorname,b.collectiondate,b.totalcash,b.totalcheque,b.totalcash + b.totalcheque as TotalAmount ,a.username, c.posname from users a,cashcollections b , poses c where b.userid=a.userid and c.posid=a.posid and a.UserId=" + Session["UserId"].ToString() + " order by b.CashCollectionId desc ");
                gvData.DataSource = dt;
                gvData.DataBind();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvData.PageIndex = e.NewPageIndex;
            loadCashCollection();
        }
        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edt")
                {

                    loadCashCollection();
                    int rowId = Convert.ToInt32(e.CommandArgument);
                    var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
                    txtcollectionId.Text = hf.Value;
                    Toolkit.RunSqlCommand("exec sp_reverse_cash_collection " + txtcollectionId.Text + "," + Session["UserId"].ToString(), conn);
                    loadCashCollection();
                    loadAmount();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                string fx, caller;
                Util.ErrorFunctions(new StackTrace(), new StackFrame(), out fx, out caller);
                Util.SysErrLog(ex.Message, fx, caller);
            }

        }
        private decimal CalculateTotalAmount()
        {
            decimal cash = 0; decimal cheq = 0;
            decimal total = 0;

            try
            {
                decimal.TryParse(txtcash.Text.Replace(",", ""), out cash);
                decimal.TryParse(txtcheque.Text.Replace(",", ""), out cheq);
                //cash = Convert.ToDecimal(txtcash.Text.Replace(",", ""));
                //cheq = Convert.ToDecimal(txtcheque.Text.Replace(",", ""));

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
            return total = cash + cheq;

        }


        protected void txtcash_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtcheque.Text == "")
                {
                    return;
                }
                else
                {
                    decimal total = CalculateTotalAmount();
                    txtTotalAmount.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
        protected void txtcheque_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtcash.Text == "")
                {
                    return;
                }
                else
                {
                    decimal total = CalculateTotalAmount();
                    txtTotalAmount.Text = total.ToString();
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}