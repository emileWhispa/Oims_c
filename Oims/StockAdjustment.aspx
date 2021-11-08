<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="StockAdjustment.aspx.cs" Inherits="StockAdjustment" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: right;
        }
        .auto-style3 {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">POS:</td>
            <td>

                    <asp:DropDownList ID="ddlPOS" runat="server" Height="22px" Width="149px" AutoPostBack="True" OnSelectedIndexChanged="ddlPOS_SelectedIndexChanged">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPOS" CssClass="validation-error" ErrorMessage="*" />

                </td>
        </tr>
        <tr>
            <td class="auto-style3">Item Name:</td>
            <td>
                        <asp:TextBox ID="txtSearchItem" runat="server" AutoPostBack="True" OnTextChanged="txtSearchItem_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" Height="22px" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" Width="371px">
                        </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlProduct" CssClass="validation-error" ErrorMessage="*" />

                </td>
        </tr>
        <tr>
            <td class="auto-style3">Available Quantity:</td>
            <td>
                        <asp:TextBox ID="txtAvailableQty" runat="server" AutoPostBack="True" ReadOnly="True" Width="58px"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td class="auto-style3">Adjustment Type:</td>
            <td>
                    <asp:DropDownList ID="ddlAdjustment" runat="server" Height="23px" AutoPostBack="true"
                        Width="201px">
                        <asp:ListItem Value="1">Increase(+)</asp:ListItem>
                        <asp:ListItem Value="0">Decrease(-)</asp:ListItem>
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlAdjustment" CssClass="validation-error" ErrorMessage="*" />

                </td>
        </tr>
        <tr>
            <td class="auto-style3">Quantity Adjusted:</td>
            <td>
                    <asp:TextBox runat="server" Height="21px" Width="60px" ID="txtQtyAdjusted"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtQtyAdjusted" CssClass="validation-error" ErrorMessage="*" />

                </td>
        </tr>
        <tr>
            <td class="auto-style3">Comments:</td>
            <td>
                        <asp:TextBox ID="txtComments" runat="server" Width="487px"></asp:TextBox>
                        </td>
        </tr>
        <tr>
            <td class="auto-style2">&nbsp;</td>
            <td><asp:Button ID="btnSaveAdjustment" runat="server" Text="Save" CssClass="btn btn-default btn-sm" OnClick="btnSaveAdjustment_Click" ValidationGroup="Sale" />
                            <asp:ConfirmButtonExtender ID="btnSaveAdjustment_ConfirmButtonExtender" runat="server" ConfirmText="Do you want to submit this adjustment?" Enabled="True" TargetControlID="btnSaveAdjustment">
                </asp:ConfirmButtonExtender>
                            <asp:Button ID="btnCancelAdjustment" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" CausesValidation="False" ValidationGroup="Sale" />
                            <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
                        </td>
        </tr>
        <tr>
            <td colspan="2">
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                                Width="100%" AllowPaging="True" CssClass="grid" PageSize="15" OnRowDeleting="gvData_RowDeleting">
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
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                                <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                                    BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                                    VerticalAlign="Middle" Width="100%" />
                                <HeaderStyle CssClass="firstcol" />
                                <SelectedRowStyle BackColor="#E3EEFC" />
                            </asp:GridView>
                        </td>
        </tr>
    </table>
</asp:Content>

