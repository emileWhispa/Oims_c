using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
//using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web;
using System.Net.Mail;
using System.Net;
using System.Net.NetworkInformation;


    public static class Toolkit
    {
        private static string emailAccount;
        private static string emailPassword;
        private static string SMTPServer;
        private static int SMTPPort;
        private static bool useSSL;
        private static string oracleUser;
        private static string oraclePassword;
        private static string oracleHost;
        private static int oraclePort;
        private static string oracleSID;

        public static string returnEmailAccount
        {
            get { return emailAccount; }
            set { emailAccount = value; }
        }

        public static string returnEmailPassword
        {
            get { return emailPassword; }
            set { emailPassword = value; }
        }

        public static string returnSMTPServer
        {
            get { return SMTPServer; }
            set { SMTPServer = value; }
        }

        public static int returnSMTPPort
        {
            get { return SMTPPort; }
            set { SMTPPort = value; }
        }

        public static bool returnUseSSL
        {
            get { return useSSL; }
            set { useSSL = value; }
        }

        public static string returnOracleUser
        {
            get { return oracleUser; }
            set { oracleUser = value; }
        }

        public static string returnOraclePassword
        {
            get { return oraclePassword; }
            set { oraclePassword = value; }
        }

        public static string returnOracleHost
        {
            get { return oracleHost; }
            set { oracleHost = value; }
        }

        public static int returnOraclePort
        {
            get { return oraclePort; }
            set { oraclePort = value; }
        }


        public static string returnOracleSID
        {
            get { return oracleSID; }
            set { oracleSID = value; }
        }

        public static void dataSource(SqlConnection con, string selectCommand, DropDownList dl, string valueMember, string DisplayMember)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dl.DataSource = dt;
            dl.DataTextField = DisplayMember;
            dl.DataValueField = valueMember;
            dl.DataBind();
            dl.Items.Add("");
            dl.SelectedIndex = dl.Items.Count - 1;
        }

        public static DataTable dataDisplay(string strCon, string selectCommand)
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = strCon;
            SqlDataAdapter adapter = new SqlDataAdapter(selectCommand, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }

        public static void executeProcedure(string procedureName, string[] paramNames, string[] paramValues, SqlConnection cn)
        {
            //cn.ConnectionString = "Data Source=MEDIACOM-01060B;Initial Catalog=UMURENGE;Integrated Security=True";
            SqlCommand command = new SqlCommand();
            command.CommandText = procedureName;
            command.Connection = cn;
            command.CommandType = CommandType.StoredProcedure;
            if (paramNames.Length != paramValues.Length)
                return;
            for (int i = 0; i < paramValues.Length; i++)
            {
                command.Parameters.AddWithValue(paramNames[i], paramValues[i]);
            }
            cn.Open();
            command.ExecuteReader(CommandBehavior.CloseConnection);
            if (cn.State == ConnectionState.Open)
                cn.Close();
        }

        public static long RunSqlCommand(string strQuery, string conStr)
        {
            SqlConnection con = new SqlConnection(conStr);
            long RowsEffected = 0;
            try
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(strQuery, con);
                RowsEffected = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
            return RowsEffected;
        }

        public static void sendMail(string recipient, string subject, string content)
        {
            try
            {
                string sender = emailAccount;
                string senderPswd = emailPassword;
                string smtp = SMTPServer;
                string port = SMTPPort.ToString();
                //MailMessage objMail = new MailMessage(sender, recipient, subject, content);
                //NetworkCredential objNC = new NetworkCredential(sender, senderPswd, "mailer.esicia.rw");
                var smtpObj = new System.Net.Mail.SmtpClient();
                {
                    smtpObj.Host = smtp;
                    smtpObj.Port = SMTPPort;
                    if (useSSL)
                        smtpObj.EnableSsl = true;
                    smtpObj.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtpObj.Credentials = new NetworkCredential(sender, senderPswd);
                    smtpObj.Timeout = 20000;
                }
                smtpObj.Send(sender, recipient, subject, content);
                //smtpObj.SendAsync(objMail, res);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Boolean sendemail(String strTo, string strSubject, string strBody, bool IsBodyHTML)
        {
            SmtpClient smtp = new SmtpClient();
            try
            {
                string sender = emailAccount;
                string senderPswd = emailPassword;
                string smtpServer = SMTPServer;
                string port = SMTPPort.ToString();
                Array arrToArray;
                char[] splitter = { ';' };
                arrToArray = strTo.Split(splitter);
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(sender);
                mm.Subject = strSubject;
                mm.Body = strBody;
                mm.IsBodyHtml = IsBodyHTML;
                foreach (string s in arrToArray)
                {
                    if (s.Trim() != "")
                        mm.To.Add(new MailAddress(s));
                }
                smtp.Host = smtpServer;
                if (useSSL)
                    smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = sender;
                NetworkCred.Password = senderPswd;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(port);
                smtp.ServicePoint.MaxIdleTime = 1;
                smtp.Send(mm);
                mm = null;
                smtp = null;
                return true;
            }
            catch
            {
                smtp = null;
                return false;
            }
        }

        public static string sendemail(String strTo, string strSubject, string strBody)
        {
            SmtpClient smtp = new SmtpClient();
            try
            {
                string sender = emailAccount;
                string senderPswd = emailPassword;
                string smtpServer = SMTPServer;
                string port = SMTPPort.ToString();
                Array arrToArray;
                char[] splitter = { ';' };
                arrToArray = strTo.Split(splitter);
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(sender);
                mm.Subject = strSubject;
                mm.Body = strBody;
                mm.IsBodyHtml = true;
                foreach (string s in arrToArray)
                {
                    if (s.Trim() != "")
                        mm.To.Add(new MailAddress(s));
                }
                smtp.Host = smtpServer;
                if (useSSL)
                    smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = sender;
                NetworkCred.Password = senderPswd;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(port);
                smtp.ServicePoint.MaxIdleTime = 1;
                smtp.Send(mm);
                mm = null;
                smtp = null;
                return "OK";
            }
            catch (Exception ex)
            {
                smtp = null;
                return ex.Message + "--" + ex.InnerException.Message;
            }
        }

        public static string sendemail(String strTo, string strSubject, string strBody, bool IsBodyHTML, Attachment stmt)
        {
            SmtpClient smtp = new SmtpClient();
            try
            {
                string sender = emailAccount;
                string senderPswd = emailPassword;
                string smtpServer = SMTPServer;
                string port = SMTPPort.ToString();
                Array arrToArray;
                char[] splitter = { ';' };
                arrToArray = strTo.Split(splitter);
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress(sender);
                mm.Subject = strSubject;
                mm.Body = strBody;
                mm.IsBodyHtml = IsBodyHTML;
                mm.Attachments.Add(stmt);
                foreach (string s in arrToArray)
                {
                    if (s.Trim() != "")
                        mm.To.Add(new MailAddress(s));
                }
                smtp.Host = smtpServer;
                if (useSSL)
                    smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = sender;
                NetworkCred.Password = senderPswd;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.Port = int.Parse(port);
                smtp.ServicePoint.MaxIdleTime = 1;
                smtp.Send(mm);
                mm = null;
                smtp = null;
                return "E-mail sent";
            }
            catch (Exception ex)
            {
                smtp = null;
                return ex.Message;
            }
        }

        public static string GetMacAddress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    if (nic.GetPhysicalAddress().ToString().Length > 1)
                        macAddresses += nic.GetPhysicalAddress().ToString() + "|";
                }
            }
            if (macAddresses.Length > 0)
                return macAddresses.Substring(0, macAddresses.Length - 1);//to remove | at the end
            else
                return "";
        }
    }
