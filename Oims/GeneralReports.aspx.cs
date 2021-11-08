using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General_Reports : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        if (rdbtnproduct.Checked == true)
        {
            Response.Redirect("Reports.aspx?report=ProductsReport");

        }
        if (RdbtnCustomer.Checked == true)
        {
            Response.Redirect("Reports.aspx?report=CustomersReport");

        }
    }
}