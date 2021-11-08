<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ChangeValueDate.aspx.cs" Inherits="ChangeValueDate" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 18px;
        }
        .auto-style3 {
            width: 125px;
        }
        .auto-style4 {
            height: 18px;
            width: 125px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">POS Name:</td>
            <td>
                        <asp:TextBox ID="txtPOSName" runat="server" Enabled="False" Width="200px"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td class="auto-style3">Old Value Date:</td>
            <td>
                        <asp:TextBox ID="txtOldDate" runat="server" Enabled="False"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td class="auto-style4">New Value Date:</td>
            <td class="auto-style2">
                        <asp:TextBox ID="txtNewDate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txtNewDate_CalendarExtender" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtNewDate">
                        </asp:CalendarExtender>
                        </td>
        </tr>
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td><asp:Button ID="btnChangeDate" runat="server" Text="Change" CssClass="btn btn-default btn-sm" Width="85px" OnClick="btnChangeDate_Click" />
                        <asp:ConfirmButtonExtender ID="btnChangeDate_ConfirmButtonExtender" runat="server" ConfirmText=" Are you sure you want to change this date?" Enabled="True" TargetControlID="btnChangeDate">
                </asp:ConfirmButtonExtender>
            </td>
        </tr>
    </table>
</asp:Content>

