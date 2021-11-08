<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="POSOrderReception, App_Web_e2euaq1s" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 119px;
        }
        .auto-style3 {
            width: 179px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">
                POS Order Status:</td>
            <td class="auto-style3">
                    <asp:DropDownList ID="ddlorderstatus" runat="server" AutoPostBack="True" Height="22px" OnSelectedIndexChanged="ddlorderstatus_SelectedIndexChanged" Width="171px">
                        <%--<asp:ListItem Value="-1">Select</asp:ListItem>--%>
                      <%--  <asp:ListItem Value="0">Pending</asp:ListItem>--%>
                        <%--<asp:ListItem Value="2">Dispatched</asp:ListItem>
                        <asp:ListItem Value="3">Delivered</asp:ListItem>--%>
                        <asp:ListItem Value="2">Dispatched</asp:ListItem>
                        <asp:ListItem Value="3">Received</asp:ListItem>
                        <asp:ListItem Selected="True" Value="-1">Select Status</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnget" runat="server" Text="Load Data" CssClass="btn btn-primary btn-sm"
                            CausesValidation="true" OnClick="btnget_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <fieldset>
                    <legend>Pos Orders</legend>
                    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                        Width="100%" AllowPaging="True" DataKeyNames="Id" OnPageIndexChanging="gvData_PageIndexChanging" CssClass="grid" OnSelectedIndexChanged="gvData_SelectedIndexChanged">
                        <Columns>
                            <asp:CommandField ShowSelectButton="True" />
                            <asp:BoundField DataField="OrderNo" HeaderText="Order Number" />
                            <asp:BoundField DataField="orderdate" HeaderText="Order Date" />
                            <asp:BoundField DataField="Pose.posName" HeaderText="collection POS" />
                            <asp:BoundField DataField="Addinfo" HeaderText="Remarks" />
                            <asp:BoundField DataField="Id" Visible="False" />
                        </Columns>
                        <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                            BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                            VerticalAlign="Middle" Width="100%" />
                        <HeaderStyle CssClass="firstcol" />
                        <SelectedRowStyle BackColor="#E3EEFC" />
                    </asp:GridView>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <fieldset>
                    <legend> <asp:Label ID="lblPOSOrderNo" runat="server" Visible="False"></asp:Label> <asp:Label ID="lblOrderNo" runat="server"></asp:Label></legend></fieldset></td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:GridView ID="gvdispatch" runat="server" AllowPaging="True" DataKeyNames="Id" AutoGenerateColumns="False" CssClass="grid" EmptyDataText="No data to display" Width="100%" OnRowEditing="gvdispatch_RowEditing" OnRowUpdating="gvdispatch_RowUpdating" OnRowCancelingEdit="gvdispatch_RowCancelingEdit">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:BoundField DataField="Product.ProductName" HeaderText="Product" ReadOnly="true" />
                        <asp:BoundField DataField="QtyOrdered" HeaderText="QTY Requested" ReadOnly="True" />
                         <asp:BoundField DataField="QtyDelivered" HeaderText="QTY Dispatched"  ReadOnly="True" />
                        <asp:TemplateField HeaderText="QTY Received">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQtyReceived" runat="Server" Text='<%# Eval("QtyReceived") %>'
                                    Width="40px" BorderColor="Black" BorderWidth="1px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="req3" runat="Server" Text="*" ControlToValidate="txtQtyReceived"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%# Eval("QtyReceived") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" Visible="False" />
                    </Columns>
                    <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="100%" />
                    <HeaderStyle CssClass="firstcol" />
                    <SelectedRowStyle BackColor="#E3EEFC" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td colspan="3">&nbsp;&nbsp;
                    <asp:Button ID="btnAction" runat="server" Text="Confirm Reception" CssClass="btn btn-primary btn-sm" OnClick="btnAction_Click" Visible="False"  />
                <asp:ConfirmButtonExtender ID="btnAction_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure you want to confirm this Reception Note?" Enabled="True" TargetControlID="btnAction">
                </asp:ConfirmButtonExtender>
                </td>
        </tr>
    </table>
</asp:Content>

