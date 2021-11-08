<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="ReceiveSpecialOrder, App_Web_z1fnw4ln" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                Width="100%"
                OnRowCommand="gvData_RowCommand" CssClass="grid">
        <Columns>
            <asp:BoundField DataField="customername" HeaderText="Customer" />
            <asp:BoundField DataField="Descr" HeaderText="Item Description" />
            <asp:BoundField DataField="orderdate" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="ExpectedDeliveryDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Collection Date" />
            <asp:BoundField DataField="Quantity" HeaderText="Units" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />
            <asp:BoundField DataField="OriginPOS" HeaderText="Origin POS" />
            <asp:BoundField DataField="CollectionPOSName" HeaderText="Collection POS" />
            <asp:BoundField DataField="sample" HeaderText="Sample?" />
            <asp:ButtonField HeaderText="" Text="Receive" CommandName="rec" ItemStyle-HorizontalAlign="Center">
<ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:ButtonField>
            <asp:TemplateField Visible="false">
                <ItemTemplate>
                    <asp:HiddenField ID="hfKey" runat="server" EnableViewState="True" Value='<%# Bind("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                    BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                    VerticalAlign="Middle" Width="100%" />
        <HeaderStyle CssClass="firstcol" />
        <SelectedRowStyle BackColor="#E3EEFC" />
    </asp:GridView>
</asp:Content>

