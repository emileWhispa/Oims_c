<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        // Code that runs on application startup
    }

    //void Application_BeginRequest(object sender, EventArgs e)
    //{
    //    if (!Request.IsSecureConnection)
    //    {
    //        string securePath = string.Format("https{0}", Request.Url.AbsoluteUri.Substring(4));
    //        Response.Redirect(securePath);
    //    }
    //}
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        //---- Read config settings
       string conn = System.Configuration.ConfigurationManager.ConnectionStrings["OIMS"].ToString();
        Session["constring"] = conn;
        Session["cbs"] = System.Configuration.ConfigurationManager.AppSettings["CBS_TYPE"];

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
