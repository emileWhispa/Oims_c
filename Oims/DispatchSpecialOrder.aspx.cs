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


public partial class DispatchSpecialOrder : System.Web.UI.Page
{
    Bl bl;
    string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        bl = new Bl(conn);
        if (!IsPostBack)
        {
            loaddata();
            Util.LogAudit(this.Page, "Accessed Page", this.Title);
        }
    }
    private void loaddata()
    {
        DataTable data = new DataTable();
        data = Toolkit.dataDisplay(conn, "Select a.SpecialOrderId as id, a.*,c.CustomerName,d.CategoryName, b.POSName as [OriginPOS],(Select POSName from POSes where POSId=a.CollectionPOS) as [CollectionPOSName] from SpecialOrders a, Customers c, POSes b, ProductCategories d where a.POSId=b.POSId and a.ProductCategoryId = d.ProductCategoryId and a.CustomerId=c.CustomerId and a.OrderStatus=2");
        gvData.DataSource = data;
        gvData.DataBind();
    }

    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "disp")
        {
            loaddata();
            int rowId = Convert.ToInt32(e.CommandArgument);
            var hf = (HiddenField)((GridView)e.CommandSource).Rows[rowId].FindControl("hfKey");
            int id = int.Parse(hf.Value.ToString());
            var x = bl.getSpecialOrder(id);
            x.OrderStatus = 3;//Dispatched
            bl.saveSpecialOrder(x);
            loaddata();
        }
    }
}