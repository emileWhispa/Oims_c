using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using orion.ims.BL;
using System.Net.NetworkInformation;
using System.Text;
using System.IO;
using System.Web.UI;

/// <summary>
/// Summary description for DPage
/// </summary>
public class DPage
{
    public static string GetLastTenTxns()
    {
        Bl bl = new Bl(Util.DbConnectionString());
        //---- Get 10 last Txns

        //----- Create Table 
        HtmlGenericControl table = new HtmlGenericControl("table");
        HtmlGenericControl tr = new HtmlGenericControl("tr");
        HtmlGenericControl th = new HtmlGenericControl("th");
        HtmlGenericControl td = new HtmlGenericControl("td");

        table.Attributes.Add("id", "switchtxn");
        //----Create header
        th.InnerText = "Txn Id";
        tr.Controls.Add(th);

        th = new HtmlGenericControl("th");
        th.InnerText = "Txn Type";
        tr.Controls.Add(th);

        th = new HtmlGenericControl("th");
        th.InnerText = "Txn Amount";
        tr.Controls.Add(th);

        th = new HtmlGenericControl("th");
        th.InnerText = "Txn Time";
        tr.Controls.Add(th);

        th = new HtmlGenericControl("th");
        th.InnerText = "Txn Status";
        tr.Controls.Add(th);

        table.Controls.Add(tr);

        //------ Empty row
        tr = new HtmlGenericControl("tr");
        td = new HtmlGenericControl("td");
        td.Attributes.Add("colspan", "5");
        td.Attributes.Add("class", "emptyrowset");
        td.InnerText = "No data to display!";

        tr.Controls.Add(td);
        table.Controls.Add(tr);
        

        return GetHtmlText(table);
    }

    public static string SwitchConnStatus()
    {
        string ip = "129.0.0.2";
        Bl bl = new Bl(Util.DbConnectionString());
        //-----Get IP

        //--- Create Div
        string div = "";
        if (PingHost(ip))        
            div="<div class='ss-status ss-status-connected'>CONNECTED</div>";            
       
        else
            div = "<div class='ss-status ss-status-disconnected'>DIS-CONNECTED</div>";   
        
        return div;
    }

    public static string UploadMode()
    {
        Bl bl = new Bl(Util.DbConnectionString());
        //---- Get Status

        //----- Create Div
        string div = "";
        div = "<div class='ss-status ss-status-connected'>ONLINE</div>";    
        return div;
    }

    private static bool PingHost(string nameOrAddress)
    {
        bool pingable = false;
        Ping pinger = new Ping();
        try
        {
            PingReply reply = pinger.Send(nameOrAddress, 5000);
            pingable = reply.Status == IPStatus.Success;
        }
        catch (PingException)
        {
            // Discard PingExceptions and return false;
        }
        return pingable;
    }

    private static string GetHtmlText(HtmlGenericControl ctrl){
        StringBuilder generatedHtml = new StringBuilder();
        HtmlTextWriter htw = new HtmlTextWriter(new StringWriter(generatedHtml));
        ctrl.RenderControl(htw);

        return generatedHtml.ToString();
    }
}