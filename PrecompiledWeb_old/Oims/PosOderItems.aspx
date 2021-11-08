<%@ page title="Oims|Pos Order Items" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" inherits="orion.ims.PosOderItems, App_Web_e2euaq1s" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1
        {
            width: 77px;
        }
        .auto-style2 {
            width: 77px;
            height: 22px;
        }
        .auto-style3 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <fieldset>
        <legend>Pos Oder Items</legend>
        <table style="width:100%">
            <tr>
                <td class="auto-style1">
                    <asp:HyperLink ID="HyperLink1" NavigateUrl="~/Posorders.aspx" runat="server">Back</asp:HyperLink>
                </td>
            </tr>
            <td class="auto-style1">
                Order No:
            </td>
            <td>
                <asp:TextBox ID="txtorderNo" ReadOnly="true" runat="server" ValidationGroup="Entry"></asp:TextBox>
            </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    Product:
                </td>
                <td class="auto-style3">
                        <asp:TextBox ID="txtSearchItem" runat="server" AutoPostBack="True" OnTextChanged="txtSearchItem_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" Height="22px" Width="371px">
                        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlProduct" ValidationGroup="Entry" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    Quantity:
                </td>
                <td>
                    <asp:TextBox ID="txtquantity" runat="server" ValidationGroup="Entry"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtquantity" ValidationGroup="Entry" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="hfId" runat="server" Width="67px" Visible="False" ValidationGroup="Entry"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                </td>
                <td>
                    <%--<div class="btn-group">--%>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm"
                            OnClick="btnSave_Click" ValidationGroup="Entry" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                            CausesValidation="false" OnClick="btnCancel_Click" ValidationGroup="Entry" />
                    <%--</div>--%>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="Product.ProductName" HeaderText="Product" />
                <asp:BoundField DataField="QtyOrdered" HeaderText="Quantity" />
                <asp:ButtonField HeaderText="" Text="Edit" CommandName="sel" ItemStyle-HorizontalAlign="Center" />
                <asp:ButtonField HeaderText="" Text="Delete" CommandName="del" ItemStyle-HorizontalAlign="Center" />
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
    <br />
    <fieldset>
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit Order" CssClass="btn btn-primary btn-sm" OnClick="btnSubmit_Click"  />
                    <asp:ConfirmButtonExtender ID="btnSubmit_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure you want to submit this order?" Enabled="True" TargetControlID="btnSubmit">
                    </asp:ConfirmButtonExtender>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
