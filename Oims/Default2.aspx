<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <asp:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="1" Width="100%">
            <asp:TabPanel runat="server" HeaderText="TabPanel1" ID="TabPanel1">
            </asp:TabPanel>
            <asp:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2">
                <ContentTemplate>
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </ContentTemplate>
            </asp:TabPanel>
        </asp:TabContainer>
    
    </div>
    </form>
</body>
</html>
