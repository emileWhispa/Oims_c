<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="POSItemTransfer, App_Web_z1fnw4ln" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 124px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>POS Item Transfer</legend>
        <table>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Transfer from POS:</td>
                <td>
                    <asp:DropDownList ID="ddlPos" runat="server" Height="25px" Width="301px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlPos" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Product:
                </td>
                <td>
                        <asp:TextBox ID="txtSearchItem" runat="server" AutoPostBack="True" OnTextChanged="txtSearchItem_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" Height="22px" Width="371px">
                        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlProduct" ValidationGroup="Entry" />

                </td>
            </tr>

            <tr>
                <td class="auto-style1">Quantity:</td>
                <td>
                    <asp:TextBox runat="server" CssClass="textbox" ID="txtQuantity" Width="100px"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtQuantity" />

                </td>
            </tr>

            <tr>
                <td class="auto-style1">Remarks:
                </td>
                <td>
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="419px"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:TextBox ID="hfId" runat="server" Width="67px" Visible="False"></asp:TextBox>
                </td>
                <td>

                    &nbsp;

                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                        CausesValidation="false" OnClick="btnCancel_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
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
                <asp:ButtonField HeaderText="" Text="Edit" CommandName="sel" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonField>
                <asp:ButtonField HeaderText="" Text="Delete" CommandName="del" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonField>
                <asp:ButtonField HeaderText="" Text="Submit" CommandName="sub" ItemStyle-HorizontalAlign="Center">
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
    </fieldset>
</asp:Content>

