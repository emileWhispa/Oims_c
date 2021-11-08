<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="QuotationItems, App_Web_z1fnw4ln" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 18px;
            text-align: left;
        }

        .auto-style3 {
            width: 130px;
        }

        .auto-style4 {
            height: 18px;
            text-align: left;
            width: 130px;
        }

        .auto-style5 {
            width: 59%;
        }

        .auto-style6 {
            width: 286px;
        }

        .auto-style7 {
            width: 109px;
        }

        .auto-style8 {
            width: 147px;
        }

        .auto-style9 {
            width: 63px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:Panel runat="server" ID="pnlDetails">
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        <fieldset>
            <legend>Sales Items</legend>
            <table style="width: 100%">
                <tr>
                    <td class="auto-style4">Quotation No.:</td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtQuotationNo" runat="server" ReadOnly="True"></asp:TextBox>
                        <asp:Label ID="lblText" runat="server" ForeColor="#0000CC"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Customer Name:</td>
                    <td>
                        <asp:TextBox ID="txtCustomerName" runat="server" Height="19px" ReadOnly="True" Width="357px"></asp:TextBox>
                        <asp:TextBox ID="txtPhone" runat="server" Height="19px" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Product Name:</td>
                    <td>
                        <asp:TextBox ID="txtSearchItem" runat="server" AutoPostBack="True" OnTextChanged="txtSearchItem_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" Height="22px" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" Width="371px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlProduct" CssClass="validation-error" ErrorMessage="*" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Quantity:</td>
                    <td>
                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtQuantity" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Unit Price:</td>
                    <td>
                        <asp:TextBox ID="txtUnitPrice" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtUnitPrice" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Discount Rate:</td>
                    <td>
                        <asp:TextBox ID="txtDiscountRate" runat="server" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtDiscountRate" />
                        <asp:CheckBox ID="chkNoDiscount" runat="server" Text="No Discount" AutoPostBack="True" OnCheckedChanged="chkNoDiscount_CheckedChanged" />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td>
                        <asp:RadioButton ID="rdioVatable" runat="server" GroupName="VAT" Text="Vatable" AutoPostBack="True" Checked="True" OnCheckedChanged="rdioVatable_CheckedChanged" />
                        &nbsp;
                        <asp:RadioButton ID="rdioNotVatable" runat="server" GroupName="VAT" Text="Not Vatable" AutoPostBack="True" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">VAT Rate:</td>
                    <td class="auto-style6">
                        <asp:TextBox ID="txtVATRate" runat="server">18.00</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtVATRate" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Manual Description:</td>
                    <td>
                        <asp:TextBox ID="txtManualDescr" runat="server" Height="29px" TextMode="MultiLine" Width="415px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:TextBox ID="txtSaleDetailId" runat="server" Visible="False" Width="16px" Height="19px"></asp:TextBox>
                        <asp:TextBox ID="txtSaleId" runat="server" Height="19px" Visible="False" Width="16px"></asp:TextBox>
                    </td>
                    <td>&nbsp;<asp:Button ID="btnSaveItem" runat="server" Text="Save" CssClass="btn btn-default btn-sm" OnClick="btnSaveItem_Click" />
                        <asp:Button ID="btnCancelSaleDetails" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" CausesValidation="False" OnClick="btnCancelSaleDetails_Click" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4"></td>
                    <td></td>
                </tr>
            </table>
        </fieldset>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                        Width="100%"
                        CssClass="grid" OnRowCommand="gvData_RowCommand" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging">
                        <Columns>
                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                            <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" />
                            <asp:BoundField DataField="Discounts" HeaderText="Disc. Rate" />
                            <asp:BoundField DataField="vatrate" HeaderText="VAT Rate" />
                            <asp:BoundField DataField="ManualDescr" HeaderText="Manual Description" />
                            <asp:ButtonField Text="Edit" CommandName="sel">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:ButtonField>
                            <asp:ButtonField Text="Delete" CommandName="del">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:ButtonField>
                            <asp:TemplateField Visible="False">
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
                </td>
            </tr>
            <tr><td>
                <asp:Panel runat="server" ID="pnlSDC" Visible="false">
            <table class="auto-style5">
                <tr>
                    <td class="auto-style7">SDC Receipt No:</td>
                    <td class="auto-style8">
                        <asp:TextBox ID="txtSDCReceiptNo" runat="server"></asp:TextBox>
                    </td>
                    <td class="auto-style9">SDC No:</td>
                    <td>
                        <asp:TextBox ID="txtSDCNo" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table></asp:Panel>
            </td>
            </tr>
            <tr>
                <td>&nbsp;<br />
                    &nbsp;&nbsp;<asp:Button ID="btnApprove" runat="server" CausesValidation="False" CssClass="btn btn-primary btn-sm" OnClick="btnApprove_Click" Text="Confirm Order" ValidationGroup="Sale" Width="127px" />
                    <asp:ConfirmButtonExtender ID="btnApprove_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure you want to perform this action?" Enabled="True" TargetControlID="btnApprove">
                    </asp:ConfirmButtonExtender>
                    <asp:Button ID="btnPrint" runat="server" CausesValidation="False" CssClass="btn btn-primary btn-sm" OnClick="btnPrint_Click" Text="Print" ValidationGroup="Sale" Width="101px" />
                    <br />
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

