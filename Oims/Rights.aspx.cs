using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.BL;
using orion.ims.DAL;

public partial class Rights : System.Web.UI.Page
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
                //---- Load User Groups
                var l = (from b in bl.getUserGroups().ToList() select new ListItem { Text = b.GroupName, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlUserGroups, l, true);
                var mn = (from b in bl.GetMenus().ToList() select new ListItem { Text = b.Menu_Name, Value = b.Id.ToString() }).ToList();
                Util.loadCombo(ddlMenuGroup, mn, true);

                loadData();
            }
        }
        catch (Exception ex)
        {
            lblError.Text = ex.Message;
        }
    }
    protected void ddlUserGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadData();
    }
    protected void ddlMenuGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadData();
    }
    protected void gvData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvData.PageIndex = e.NewPageIndex;
        loadData();
    }
    protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        vw_ProfileMenus pm;
        CheckBox chk;
        var mns = Session["d"] as List<vw_ProfileMenus>;
        int id = 0;
        for (int k = 0; k < gvData.Rows.Count; k++)
        {
            string key = gvData.DataKeys[k].Value.ToString();
            if (!string.IsNullOrEmpty(key))
            {
                id = Convert.ToInt32(key);
                pm = mns.Where(x => x.Id == id).FirstOrDefault();
                chk = (CheckBox)gvData.Rows[k].FindControl("chkAllow");
                if (chk != null && pm != null)
                {
                    if (!pm.AllowAccess.Equals(chk.Checked))
                    {
                        pm.AllowAccess = chk.Checked;
                        pm.UserGroupId = Convert.ToInt32(ddlUserGroups.SelectedValue);
                        bl.SaveMenuRights(pm);
                    }
                }
            }
        }
        loadData();
    }

    public void loadData()
    {
        string ugId = ddlUserGroups.SelectedValue;
        string mgId = ddlMenuGroup.SelectedValue;
        if (string.IsNullOrEmpty(ugId))
            ugId = "0";
        if (string.IsNullOrEmpty(mgId))
            mgId = "0";
        int parent = Convert.ToInt32(mgId);
        var mns = bl.GetProfileMenus(Convert.ToInt32(ugId)).Where(x => x.ParentMenuId == parent).ToList();

        gvData.DataSource = mns;
        gvData.DataBind();
        Session["d"] = mns;

    }
}