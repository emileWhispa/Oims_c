<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="InvoiceCancellation, App_Web_e2euaq1s" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 43%;
        }
        .auto-style3 {
            width: 586px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <fieldset>
        <legend>Invoice Cancellation</legend>
        <table>
            <tr>
                <td>Select Invoice:
                </td>
                <td>
                    <asp:DropDownList ID="ddlinvoice" runat="server" Height="21px" AutoPostBack="true" OnSelectedIndexChanged="ddlinvoice_SelectedIndexChanged"
                        Width="201px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlinvoice" />
                </td>
            </tr>
            <tr>
                <td>Or Type Invoice No.:
                </td>
                <td>
                                <asp:TextBox ID="txtSearchKey" runat="server" Height="22px"></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Go" ValidationGroup="search" />
                                <asp:TextBox ID="txtInvoiceNo" runat="server" Height="16px" Visible="False" Width="16px"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <fieldset>
                        <legend>Invoice Items</legend>
                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                            Width="100%" OnPageIndexChanging="gvData_PageIndexChanging" CssClass="grid">
                            <Columns>
                                <asp:BoundField DataField="product.Productname" HeaderText="Product" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField DataField="unitPrice" HeaderText="Unit Price" />
                                <asp:BoundField DataField="vatrate" HeaderText="VAT Rate" />
                                <asp:BoundField DataField="Discounts" HeaderText="Discount" />
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
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
        <table class="auto-style2">
            <tr>
                <td class="auto-style3">&nbsp;<asp:TextBox ID="txtcomments" runat="server" Height="28px" TextMode="MultiLine" Width="415px"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtcomments_TextBoxWatermarkExtender" runat="server" BehaviorID="txtcomments_TextBoxWatermarkExtender" TargetControlID="txtcomments" WatermarkText="Add Comments">
                    </cc1:TextBoxWatermarkExtender>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtcomments" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button ID="btnAction" runat="server" Text="Cancel Invoice" CssClass="btn btn-primary btn-sm" OnClick="btnAction_Click1" />
                    <cc1:ConfirmButtonExtender ID="btnAction_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure you want to cancel this Invoice?" Enabled="True" TargetControlID="btnAction">
                    </cc1:ConfirmButtonExtender>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

