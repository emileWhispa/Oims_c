<%@ page title="Oims|Transactions Report" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" inherits="orion.ims.TransReport, App_Web_e2euaq1s" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
    <style type="text/css">
        .style1
        {
            width: 52px;
        }
        .style2
        {
            width: 54px;
        }
        .style3
        {
            width: 97px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <h3 class="header">
        | Transactions Report</h3>
    <hr />
    <table>
        <tr>
            <td>
                <asp:ListBox ID="lbReports" runat="server" AutoPostBack="True" Rows="20" Width="325px"
                    CssClass="rounded_corners" Style="background-color: #fcfcfc; font-size: 13px;
                    padding: 5px;" ForeColor="#000066" OnSelectedIndexChanged="lbReports_SelectedIndexChanged">
                </asp:ListBox>
            </td>
            <td valign="top" style="padding-left: 10px;" class="style2">
                <fieldset>
                    <legend>Report Filters</legend>
                    <table class="style1">
                        <tr>
                            <td class="style3">
                                &nbsp;From:
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="textbox" ID="txtDateFrom" Width="100px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateFrom">
                                </asp:CalendarExtender>
                            </td>
                            <td class="style4">
                                To:
                            </td>
                            <td colspan="2" class="style5">
                                <asp:TextBox runat="server" CssClass="textbox" ID="txtDateTo" Width="100px"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateTo">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                         <tr>
                            <td class="style3">
                                Format:
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblReportType" runat="server" 
                                    RepeatDirection="Horizontal" Height="16px" Width="176px">
                                    <asp:ListItem Selected="True">PDF</asp:ListItem>
                                    <asp:ListItem>Excel</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <div>
                    </div>
                </fieldset>
                <div style="margin-top: 10px;">
                    <asp:Button ID="btnPrint" runat="server" Text="View Report" CssClass="btn btn-primary btn-sm"
                        OnClick="btnPrint_Click" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
