<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="POSTransferAuth.aspx.cs" Inherits="POSTransferAuth" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
                    <asp:TextBox ID="hfId" runat="server" Width="16px" Visible="False" Height="18px"></asp:TextBox>
    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
        Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
        OnRowCommand="gvData_RowCommand" CssClass="grid">
        <Columns>
            <asp:BoundField DataField="POSName" HeaderText="POS Centre" />
            <asp:BoundField DataField="Requester" HeaderText="Requester" />
            <asp:BoundField DataField="RequestDate" HeaderText="Request Date" />
            <asp:BoundField DataField="ProductName" HeaderText="Product" />
            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
            <asp:ButtonField HeaderText="" Text="Approve" CommandName="approve" ItemStyle-HorizontalAlign="Center">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
            </asp:ButtonField>
            <asp:ButtonField HeaderText="" Text="Decline" CommandName="decline" ItemStyle-HorizontalAlign="Center">
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

