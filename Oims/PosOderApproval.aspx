<%@ Page Title="Oims|Pos Oder Approval" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="PosOderApproval.aspx.cs" Inherits="orion.ims.PosOderApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <table>
            <tr>
                <td>
                    Select Processing Status:
                </td>
                <td>
                    <asp:DropDownList ID="ddlorderstatus" runat="server">
                        <%--<asp:ListItem Value="-1">Select</asp:ListItem>--%>
                      <%--  <asp:ListItem Value="0">Pending</asp:ListItem>--%>
                        <asp:ListItem Value="1">Submitted</asp:ListItem>
                        <%--<asp:ListItem Value="2">Dispatched</asp:ListItem>
                        <asp:ListItem Value="3">Delivered</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnget" runat="server" Text="Load" CssClass="btn btn-primary btn-sm"
                            CausesValidation="true" OnClick="btnget_Click" />
                </td>
                <td>
                    <asp:TextBox ID="hfId" runat="server" Width="67px" Visible="False"></asp:TextBox>
                </td></td>
            </tr>
        </table>
        <legend>Pos Orders</legend>
    </fieldset>
    <fieldset>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"     OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="OrderNo" HeaderText="Order Number" />
                <asp:BoundField DataField="orderdate" HeaderText="Order Date" />
                <asp:BoundField DataField="Pose.posName" HeaderText="collection POS" />
                <asp:BoundField DataField="Addinfo" HeaderText="Remarks" />
                <asp:ButtonField HeaderText="" Text="Items" CommandName="sel" ItemStyle-HorizontalAlign="Center" />
                <asp:ButtonField HeaderText="" Text="Dispatch" CommandName="app" ItemStyle-HorizontalAlign="Center" />
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
    </fieldset>
   <table>
   <tr>
   </tr>
   <tr>
   <td><legend>Order Items</legend></td>
   </tr></table>
    <fieldset>
      
        <asp:MultiView ID="MultiView1" runat="server">
            <asp:View ID="items" runat="server">
                <asp:GridView ID="gvitems" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" CssClass="grid" EmptyDataText="No data to display" 
                    OnPageIndexChanging="gvitems_PageIndexChanging" Width="100%">
                    <Columns>
                        <asp:BoundField DataField="Product.ProductName" HeaderText="Product" />
                        <asp:BoundField DataField="QtyOrdered" HeaderText="Quantity" />
                    </Columns>
                    <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" 
                        BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Italic="True" 
                        ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" 
                        Width="100%" />
                    <HeaderStyle CssClass="firstcol" />
                    <SelectedRowStyle BackColor="#E3EEFC" />
                </asp:GridView>
            </asp:View>
            <asp:View ID="vwdispatch" runat="server">
             <fieldset>
        <asp:GridView ID="gvdispatch" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvdispatch_PageIndexChanging"
            OnRowCommand="gvdispatch_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="Product.ProductName" HeaderText="Product" />
                <asp:BoundField DataField="QtyOrdered" HeaderText="Quantity" />
                  <asp:BoundField DataField="QtyDelivered" HeaderText="Quantity Dispatched" />
                <asp:ButtonField HeaderText="" Text="Select" CommandName="sel" ItemStyle-HorizontalAlign="Center" />
                 <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfKey2" runat="server" EnableViewState="True" Value='<%# Bind("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                VerticalAlign="Middle" Width="100%" />
            <HeaderStyle CssClass="firstcol" />
            <SelectedRowStyle BackColor="#E3EEFC" />
        </asp:GridView>
    </fieldset>
    <fieldset>
    <table>
    <tr><td>Product:</td>
    <td><asp:Label runat="server" ID="lblproduct" CssClass="textbox"></asp:Label></td></tr>
    <tr><td>Quantity Dispatched:</td>
    <td>  <asp:TextBox ID="txtquantity" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtquantity" /></td></tr>
                        <tr>
                        <td><asp:Button ID="btnsave" runat="server" Text="Dispatch" CssClass="btn btn-primary btn-sm"
                            CausesValidation="true" OnClick="btnsave_Click" />
                        </td> <td>
                    <asp:TextBox ID="hfid2" runat="server" Width="67px" Visible="False"></asp:TextBox>
                </td></tr>
    </table>
    </fieldset>
            </asp:View>
            <br />
        </asp:MultiView>
    </fieldset>
</asp:Content>
