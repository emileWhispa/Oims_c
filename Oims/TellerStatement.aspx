<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="TellerStatement.aspx.cs" Inherits="TellerStatement" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
        }
        .auto-style3 {
            width: 108px;
        }
        .auto-style4 {
            width: 108px;
            height: 18px;
        }
        .auto-style5 {
            height: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>

    <table class="auto-style1">
        <tr>
            <td class="auto-style3">Teller:</td>
            <td>
                                <asp:DropDownList runat="server" ID="cmbTeller" Height="24px" Width="249px" AutoPostBack="True" OnSelectedIndexChanged="cmbTeller_SelectedIndexChanged"></asp:DropDownList>

                                </td>
        </tr>
        <tr>
            <td class="auto-style3">Cash Balance:</td>
            <td>
                    <asp:TextBox ID="txtCashBalance" runat="server" Height="20px" Width="163px" Enabled="False"></asp:TextBox>
                    </td>
        </tr>
        <tr>
            <td class="auto-style3">Cheque Balance:</td>
            <td>
                    <asp:TextBox ID="txtChequeBalance" runat="server" Height="20px" Width="163px" Enabled="False"></asp:TextBox>
                    </td>
        </tr>
        <tr>
            <td class="auto-style3">Start Date:</td>
            <td>
                    <asp:TextBox ID="txtStartDate" runat="server" Height="20px" Width="163px"></asp:TextBox>
                    <ajaxtoolkit:calendarextender ID="txtStartDate_CalendarExtender" runat="server" BehaviorID="TextBox2_CalendarExtender" TargetControlID="txtStartDate" Format="yyyy-MM-dd" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtStartDate" />
                </td>
        </tr>
        <tr>
            <td class="auto-style3">End Date:</td>
            <td>
                    <asp:TextBox ID="txtEndDate" runat="server" Height="20px" Width="163px"></asp:TextBox>
                    <ajaxtoolkit:CalendarExtender ID="txtEndDate_CalendarExtender" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="txtEndDate">
                    </ajaxtoolkit:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtEndDate" />
                </td>
        </tr>
        <tr>
            <td class="auto-style3">&nbsp;</td>
            <td>
                    
                        <asp:Button ID="btnDisplay" runat="server" Text="Display" CssClass="btn btn-default btn-sm"
                            OnClick="btnSave_Click" />
                        </td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="2">
         <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging" CssClass="grid" PageSize="20">
            <Columns>
                <asp:BoundField DataField="TransactionDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="CashOpeningBal" HeaderText="Cash Opening Bal." />
                <asp:BoundField DataField="CashAmount" HeaderText="Cash Amount" />
                <asp:BoundField DataField="CashBal" HeaderText="Cash Balance" />
                <asp:BoundField DataField="ChequeOpeningBal" HeaderText="Cheque Opening Bal." />
                <asp:BoundField DataField="ChequeAmount" HeaderText="Cheque Amount" />
                <asp:BoundField DataField="ChequeBal" HeaderText="Cheque Balance" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
            </Columns>
            <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                VerticalAlign="Middle" Width="100%" />
            <HeaderStyle CssClass="firstcol" />
            <SelectedRowStyle BackColor="#E3EEFC" />
        </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style2" colspan="2">&nbsp;
                    
                        <asp:Button ID="btnPrint" runat="server" Text="Print Report" CssClass="btn btn-default btn-sm"
                            OnClick="btnPrint_Click" Visible="False" />
                        &nbsp;<asp:Button ID="btnExcel" runat="server" Text="Export To Excel" CssClass="btn btn-default btn-sm"
                            OnClick="btnSave_Click" Visible="False" />
                    
                        </td>
        </tr>
    </table>
</asp:Content>

