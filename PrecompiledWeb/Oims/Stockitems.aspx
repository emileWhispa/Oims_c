<%@ page title="Oims|Stock Items" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" inherits="orion.ims.Stockitems, App_Web_z1fnw4ln" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 134px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <fieldset>
        <legend>Stock Items</legend>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">POS: </td>
                <td>
                                <asp:DropDownList runat="server" ID="cmbPOS" Height="24px" Width="249px" AutoPostBack="True" OnSelectedIndexChanged="cmbPOS_SelectedIndexChanged"></asp:DropDownList>

                                </td>
            </tr>
            <tr>
                <td class="auto-style2">Search By Product:</td>
                <td>
                        <asp:TextBox ID="txtSearchItem" runat="server" Height="24px" ValidationGroup="search"></asp:TextBox>
                        <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" ValidationGroup="search" />

                                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging" CssClass="grid" PageSize="20">
            <Columns>
                <asp:BoundField DataField="Pos.posName" HeaderText="POS Centre" />
                <asp:BoundField DataField="Product.Productname" HeaderText="Product" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
            </Columns>
            <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                VerticalAlign="Middle" Width="100%" />
            <HeaderStyle CssClass="firstcol" />
            <SelectedRowStyle BackColor="#E3EEFC" />
        </asp:GridView>
        <br />
&nbsp;
                    
                        <asp:Button ID="btnPrint" runat="server" Text="Print Report" CssClass="btn btn-default btn-sm" Visible="False" OnClick="btnPrint_Click" />
    </fieldset>
</asp:Content>
