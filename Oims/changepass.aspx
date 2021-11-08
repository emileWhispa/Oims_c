<%@ Page Language="C#" AutoEventWireup="true" CodeFile="changepass.aspx.cs" Inherits="orion.ims.changepass" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Orion|Change Password</title>
   <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/bMobile.css" rel="stylesheet" />
    <link href="css/jquery.ui.all.css" rel="stylesheet" type="text/css" />
     <script src="Scripts/jquery-2.0.3.min.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 134px;
            color:#808080;
            text-align :left;
        }
        .style3
        {
            width: 14px;
            color:#808080;
            text-align :left;
        }
        #form1
        {
            margin-top: 104px;
            height: 481px;
            width: 1222px;
        }
        .style8
        {
            width: 144px;
              text-align :justify;
        }
       
        .style9
        {
            width: 134px;
            color: #808080;
            text-align : left;
            height: 23px;
        }
        .style10
        {
            width: 14px;
            color: #808080;
            text-align : left;
            height: 23px;
        }
       
    </style>
</head>
<body style="height: 673px; width: 1184px;">
    <form id="form1" runat="server">
    <div style="height: 498px">
        <div class="changepass">
            <div class="loginheader">
               <asp:Label ID="lblmsg" runat="server"></asp:Label>
                <table>              
                  <tr>
                       <td class="style9">User Name:</td>
                          <td class="style9"><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                        </td>
                        <td class="style10">

                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                            Old Password:</td>
                        <td class="style1">
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td class="style3">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ErrorMessage="*" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                     <tr>
                        <td class="style9">
                            New Password:</td>
                        <td class="style9">
                            <asp:TextBox ID="txtnewpass" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td class="style10">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ErrorMessage="*" ControlToValidate="txtnewpass"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                     <tr>
                        <td class="style1">
                            Confirm:</td>
                        <td class="style1">
                            <asp:TextBox ID="txtconfirm" runat="server" TextMode="Password"></asp:TextBox>
                        </td>
                        <td class="style3">

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ErrorMessage="*" ControlToValidate="txtconfirm"></asp:RequiredFieldValidator>

                        </td>
                    </tr>
                    <tr>
                        <td class="style8" colspan="2">
                              <asp:Label ID="lblstatus" runat="server" ForeColor="#CC0000"></asp:Label></td>
                        <td class="style3">

                        </td>
                    </tr>
                    <tr>
                          <td>
                          <asp:Button ID="btnchange" runat="server" Text="Change" 
                                CssClass="btn btn-primary btn-sm" onclick="btnchange_Click" Width="65px" />
                                </td>
                                <td>
                            <asp:Button ID="btncancel" runat="server" Text="Cancel" 
                                CssClass="btn btn-primary btn-sm" onclick="btncancel_Click" Width="58px" />
                       
                        &nbsp;
                       
                        </td>
                       
                        <td class="style3">
                            
                        </td>
                    </tr>
                </table>
            </div>
            </div>
    </div>
    </form>
</body>
</html>
