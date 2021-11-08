<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="StockAdjustmentApproval, App_Web_z1fnw4ln" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 18px;
        }
        .auto-style3 {
            height: 18px;
            width: 109px;
        }
        .auto-style4 {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">POS:</td>
            <td class="auto-style2">

                    <asp:DropDownList ID="ddlPOS" runat="server" Height="22px" Width="149px" AutoPostBack="True" OnSelectedIndexChanged="ddlPOS_SelectedIndexChanged">
                    </asp:DropDownList>

                    </td>
        </tr>
        <tr>
            <td class="auto-style4" colspan="2">
                <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                                Width="100%" AllowPaging="True" CssClass="grid" PageSize="15" OnSelectedIndexChanged="gvData_SelectedIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="StockAdjustmentID" HeaderText="ID">
                        <HeaderStyle Width="30px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="AdjustmentDate" HeaderText="Date" DataFormatString="{0:yyyy-MM-dd}" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="POSName" HeaderText="POS" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ProductName" HeaderText="Product" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Type" DataField="AdjustmentType" >
                        <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QuantityAdjusted" HeaderText="Qty" >
                        <HeaderStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Authorized" HeaderText="Authorized" />
                        <asp:BoundField DataField="Comments" HeaderText="Comments" />
                        <asp:CommandField SelectText="Approve" ShowSelectButton="True" />
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
            <td class="auto-style4" colspan="2">
                            <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
                        </td>
        </tr>
    </table>
</asp:Content>

