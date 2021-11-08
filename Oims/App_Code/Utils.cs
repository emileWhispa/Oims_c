using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using orion.ims.DAL;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Text;
using bMobile.Shared;
using orion.ims.BL;
using System.Web.SessionState;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Util
/// </summary>

    public enum MsgBoxType
    {
        Alert,
        Error,
        Info
    }
    public enum Statusid
    {
            Active = 1,
        Edited = 2,
        Approved = 3,
        Expired = 4,
        Deleted = 5,
        Locked = 6,
        Blocked = 7
    }
    public class Util
    {
        public static void loadCombo(DropDownList ddl, List<ListItem> list, bool append = false)
        {
            ddl.DataTextField = "Text";
            ddl.DataValueField = "Value";
            ddl.DataSource = list;
            ddl.DataBind();
            if (append)
                ddl.Items.Insert(0, new ListItem { Text = "--- Select ----", Value = "-1" });
        }

        public static CBSType CBSType
        {
            get
            {
                HttpSessionState session = HttpContext.Current.Session;
                int typ = 0;// Convert.ToInt32(session["cbs"].ToString());
                return (CBSType)typ;
            }
        }

        public static string DbConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
        }

        public static string ExtDbConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["cbs"].ToString();
        }

        public static void ShowMsgBox(Page p, string content, string title = "bMobile", MsgBoxType msgBoxType = MsgBoxType.Info)
        {
            ScriptManager.RegisterStartupScript(p, p.GetType(), "LaunchServerSide", "$(function() { LoginFail(); });", true);
            //var sb = new StringBuilder();
            //sb.AppendLine("msgBoxImagePath = 'Images/';");
            //sb.AppendLine("$.msgBox({title: '" + title + "',content: '" + content + "',type: '" + msgBoxType.ToString().ToLower() + "', opacity: 0.7});");
            //ScriptManager.RegisterClientScriptBlock(p, typeof(Page), Guid.NewGuid().ToString(), sb.ToString(), true);
            //  ScriptManager.RegisterStartupScript(p, typeof(Page), Guid.NewGuid().ToString(), sb.ToString(), true);
        }
        public static void ErrorFunctions(StackTrace stackTrace, StackFrame stackFrame, out string fx, out string caller)
        {
            fx = stackFrame.GetMethod().Name;
            caller = stackTrace.GetFrame(1).GetMethod().Name;
        }
        public static void SysErrLog(string msg, string fx, string caller, string moreDetails = "")
        {
            try
            {
                HttpSessionState Session = HttpContext.Current.Session;
                if (!msg.Contains("Thread was being aborted."))
                {
                    string sPath = HttpContext.Current.Server.MapPath("~/logs");
                    ClearOldLogs(sPath, 10);
                    if (!Directory.Exists(sPath)) Directory.CreateDirectory(sPath);
                    sPath = Path.Combine(sPath, DateTime.Now.ToString("yyyyMMdd") + ".log");
                    var errMsg = DateTime.Now.ToString("dd-MMM-yyy HH:mm:ss") + ": Function: " + fx + ": Being Called By: " + caller + ": Error: " + msg + (moreDetails.Length == 0 ? "" : ": More Details: " + moreDetails);
                    if (!string.IsNullOrWhiteSpace(Session["UserName"].ToString()))
                        errMsg = "User: " + Session["UserName"].ToString() + " " + errMsg;

                    //Get the file for exclusive read/write access
                    using (FileStream fs = new FileStream(sPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        using (StreamWriter sw = new StreamWriter(fs)) { }
                    }

                    //Error Logging Starts
                    using (FileStream fs = new FileStream(sPath, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                            sw.WriteLine(errMsg);
                    }
                    ClearOldLogs(sPath, 3);
                }
            }
            catch
            {
            }
        }
        private static void ClearOldLogs(string path, int days)
        {
            try
            {
                var di = new DirectoryInfo(path);
                var itms = di.GetFiles().Where(p => p.LastWriteTime.AddDays(days) < DateTime.Now);
                foreach (var itm in itms)
                    File.Delete(itm.FullName);
            }
            catch
            {
            }
        }
        public static string[] RemoteMachine(Page p)
        {
            var d = new string[2];
            d[0] = GetIP4Address(p);
            d[1] = Dns.GetHostEntry(d[0]).HostName;
            return d;
        }

        public static string GetIP4Address(Page p)
        {
            try
            {
                string IP4Address = String.Empty;
                foreach (IPAddress IPA in Dns.GetHostAddresses(p.Request.ServerVariables["REMOTE_ADDR"].ToString()))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        IP4Address = IPA.ToString();
                        break;
                    }
                }

                if (IP4Address != String.Empty)
                    return IP4Address;

                foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
                {
                    if (IPA.AddressFamily.ToString() == "InterNetwork")
                    {
                        IP4Address = IPA.ToString();
                        break;
                    }
                }
                return IP4Address;
            }
            catch (Exception ex)
            {
                string fx, caller;
                ErrorFunctions(new StackTrace(), new StackFrame(), out fx, out caller);
                SysErrLog(ex.Message, fx, caller);
                return "localhost";
            }

        }

        public static void LogAudit(Page p, string action, string Modfunction, int userid = 1)
        {
            try
            {
                HttpSessionState Session = HttpContext.Current.Session;
                string conn = System.Configuration.ConfigurationManager.ConnectionStrings["oims"].ToString();
                Bl bl;
                bl = new Bl(conn);
                string module = p.Request.RawUrl;
                var ip = RemoteMachine(p);
                UserAudit a = new UserAudit
                {
                    UserId = userid == 1 ? int.Parse(Session["UserId"].ToString()) : userid,
                    ActionDescr = action,
                    AuditDate = DateTime.Now,
                    ClntIP = ip[0],
                    ModuleId = 1,
                    Mdl_Function = Modfunction,
                    Browser = p.Request.Browser.Type
                };

              //  bl.saveAudit(a);

            }
            catch (Exception ex)
            {
                string fx, caller;
                ErrorFunctions(new StackTrace(), new StackFrame(), out fx, out caller);

            }
        }

        public static void RedirectURl(string url, string target, string windowFeatures)
        {
            var context = HttpContext.Current;
            if ((string.IsNullOrEmpty(target) || target.Equals("_self", StringComparison.OrdinalIgnoreCase)) && string.IsNullOrEmpty(windowFeatures))
                context.Response.Redirect(url);
            else
            {
                var page = (Page)context.Handler;
                if (page == null)
                    throw new InvalidOperationException("Cannot redirect to new window outside Page context.");
                url = page.ResolveClientUrl(url);
                string script = null;
                if ((!string.IsNullOrEmpty(windowFeatures)))
                    script = "window.open(\"{0}\", \"{1}\", \"{2}\");";
                else
                    script = "window.open(\"{0}\", \"{1}\");";
                script = string.Format(script, url, target, windowFeatures);
                ScriptManager.RegisterStartupScript(page, typeof(Page), "Redirect", script, true);
            }
        }

       
        public void ExecSp(string Sp)
        {
            string sSql =Sp;
            var conn = new SqlConnection(Util.DbConnectionString());
            var tcmd = new SqlCommand(sSql, conn);
            tcmd.Connection.Open();
            tcmd.ExecuteNonQuery();
        }
    }
