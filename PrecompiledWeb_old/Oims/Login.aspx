<%@ page language="C#" autoeventwireup="true" inherits="orion.ims.Login, App_Web_o2igng3j" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Oims | Login</title>
      <%--<link href="css/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="css/bmobile.css" rel="stylesheet" />
    <%--<link href="css/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="css/jquery.ui.all.css" rel="stylesheet" type="text/css" />
     <script src="Scripts/jquery-2.0.3.min.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            height: 34px;
            width: 148px;
        }
        #form1
        {
            height: 452px;
        }
        .style3
        {
            width: 89px;
        }
        .style4
        {
            height: 34px;
            width: 89px;
        }
        .style6
        {
            width: 148px;
        }
    </style>
  </head>
<body>
    <form id="form1" runat="server" method="post">
    <div id="login">
        <table style="padding-left: 1.5em; margin-left: -27px; margin-top: -35px; height: 132px; width: 273px;">
            <tr>
                <td class="style3">
                    User Name
                </td>
                <td class="style6">
                    <asp:TextBox ID="txtUserName" runat="server"  ToolTip ="UserName" Width="130px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    Password:
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtpassword" runat="server" TextMode="Password" Width="130px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style4">
                    &nbsp;</td>
                <td class="style1">
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" 
                        CssClass="btn btn-default btn-sm" Width="62px" />
                               <asp:Button ID="btncancel" runat="server" Text="Cancel" CausesValidation="false" OnClick="btncancel_Click" CssClass="btn btn-primary btn-sm" />
                  
                </td>
            </tr>
            <tr>
               <td colspan="2">
             <asp:Label ID="lblstatus" runat="server" ForeColor="Red"></asp:Label>
            </td>
            </tr>
            </table>
         </div>
    </form>
</body>
</html>
