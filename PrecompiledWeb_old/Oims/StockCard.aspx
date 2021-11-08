<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="StockCard, App_Web_e2euaq1s" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxtoolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
        }
        .auto-style3 {
            width: 79px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">POS:</td>
            <td>
                                <asp:DropDownList runat="server" ID="cmbPOS" Height="24px" Width="249px" AutoPostBack="True"></asp:DropDownList>

                                </td>
        </tr>
        <tr>
            <td class="auto-style3">Product:</td>
            <td>
                        <asp:TextBox ID="txtSearchItem" runat="server" AutoPostBack="True" OnTextChanged="txtSearchItem_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" Height="22px" Width="371px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlProduct" CssClass="validation-error" ErrorMessage="*" />
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
                <asp:BoundField DataField="TranDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="OpeningQuantity" HeaderText="Opening Qty" />
                <asp:BoundField DataField="QuantityIn" HeaderText="Quantity In" />
                <asp:BoundField DataField="QuantityOut" HeaderText="Quantity Out" />
                <asp:BoundField DataField="Balance" HeaderText="Balance" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="FullNames" HeaderText="User" />
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
                    
                        <asp:Button ID="btnPrint" runat="server" Text="Print Report" CssClass="btn btn-default btn-sm" Visible="False" OnClick="btnPrint_Click" />
                        &nbsp;<asp:Button ID="btnExcel" runat="server" Text="Export To Excel" CssClass="btn btn-default btn-sm"
                            OnClick="btnSave_Click" Visible="False" />
                    
                        </td>
        </tr>
    </table>
</asp:Content>

