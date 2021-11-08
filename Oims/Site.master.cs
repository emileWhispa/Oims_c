using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using orion.ims.BL;
using orion.ims.DAL;
using System.Web.UI.HtmlControls;

namespace orion.ims
{
    public partial class Site : System.Web.UI.MasterPage
    {
        Bl bl;
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
      protected void Page_Load(object sender, EventArgs e)
        {
            bl = new Bl(conn);
            try
            {
                if (!IsPostBack)
                {
                    SessionData.DBConString = conn;
                    if (Session != null)
                        lblCurrentUser.Text = Session["UserName"].ToString();
                }
                var param = bl.getSettingParam("CLIENT_NAME");
                if (param != null)
                {
                   lblClientName.Text = param.ParamValue;
                    LoadMenus();
                    // lblusername.Text = SessionData.UserName;
                    //----Check Page rights
                    string pageName = System.IO.Path.GetFileName(Request.Url.AbsolutePath);
                    string[] exempt = { "accessdenied.aspx", "default.aspx", "index.aspx" };
                    if (!exempt.Contains(pageName.ToLower()))
                    {
                        if (!bl.CheckMenuRights(pageName))
                            Response.Redirect("~/AccessDenied.aspx");
                    }
                    Console.WriteLine(pageName);
                }
                //
                if (SessionData.sessionduration == null)
                {
                    Timer1.Interval = 300000;
                    SessionData.sessionduration = DateTime.Now;
                }
                else
                {
                    DateTime MyTimeNow = DateTime.Now;
                    var diff = MyTimeNow.Subtract(SessionData.sessionduration);
                    if (diff.Minutes > 5)
                    {
                        SessionData.Logout = true;
                    }
                    else
                    {
                        SessionData.Logout = false;
                    }
                }

                //
            }
            catch { }
        }

        void LoadMenus()
        {
           
            //----Create ul
            HtmlGenericControl menu = new HtmlGenericControl("ul");
            menu.Attributes.Add("id", "menu-bar");

            HtmlGenericControl li;// = new HtmlGenericControl("li");
            HtmlGenericControl a;// = new HtmlGenericControl("a");
            HtmlGenericControl ul;
            HtmlGenericControl li1;
            //---- Get and load db menus
            var menus = bl.GetProfileMenus(int.Parse(Session["UserGroupId"].ToString()));
            if (menus != null)
            {
                //---- Main menus
                var mainMenus = menus.Where(x => x.MenuLevel == 0).ToList();
                foreach (var m in mainMenus)
                {
                    li = new HtmlGenericControl("li");
                    a = new HtmlGenericControl("a");
                    a.Attributes.Add("href", FormatUrl(m.Url));
                    a.InnerText = m.MenuName;
                    li.Controls.Add(a);
                    //------ Add sub menus
                 var subMenus = menus.Where(x => x.MenuLevel == 1 && x.ParentMenuId == m.Id && x.AllowAccess == true).ToList();
                    if (subMenus != null)
                    {
                        ul = new HtmlGenericControl("ul");
                        //--- Sub menus
                        foreach (var sm in subMenus)
                        {
                            li1 = new HtmlGenericControl("li");
                            a = new HtmlGenericControl("a");
                            a.Attributes.Add("href", FormatUrl(sm.Url));
                            a.InnerText = sm.MenuName;
                            li1.Controls.Add(a);
                            ul.Controls.Add(li1);
                        }
                        li.Controls.Add(ul);
                    }
                    menu.Controls.Add(li);
                }
            }
            //---- Add to the main div
            cssmenu.Controls.Clear();
            cssmenu.Controls.Add(menu);

        }

        public string FormatUrl(string url)
        {
            if (url.StartsWith("~/"))
                return url.Replace("~/", "");
            else
                return url;
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            if (SessionData.Logout == true)
            {
                Response.Redirect("~/login.aspx");
            }
        }
}
}