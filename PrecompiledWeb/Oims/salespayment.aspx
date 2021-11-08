<%@ page title="Oims|Sales Payment" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" inherits="orion.ims.SalesPayment, App_Web_z1fnw4ln" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 139px;
        }

        .auto-style2 {
            text-align: right;
        }

        .auto-style4 {
            width: 286px;
        }
        .auto-style5 {
            width: 319px;
        }
        .auto-style6 {
            text-align: right;
            width: 138px;
        }
        .auto-style7 {
            width: 138px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <fieldset>
        <legend>Sales Payment</legend>
        <table>
            <tr>
                <td class="auto-style1">
                    Payment Type:
                </td>
                <td class="auto-style5">
                    <asp:DropDownList ID="ddlPaymentType" runat="server" Height="21px" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentType_SelectedIndexChanged"
                        Width="201px">
                        <asp:ListItem>Invoice</asp:ListItem>
                        <asp:ListItem>Deposit</asp:ListItem>
                        <asp:ListItem Selected="True"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlPaymentType" ValidationGroup="search" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblPaymentDescription" runat="server">Select:</asp:Label>
                </td>
                <td class="auto-style5">
                    <asp:DropDownList ID="ddlinvoice" runat="server" Height="21px" AutoPostBack="true" OnSelectedIndexChanged="ddlinvoice_SelectedIndexChanged"
                        Width="201px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlinvoice" />
                                <asp:TextBox ID="txtSalesID" runat="server" Height="16px" Visible="False" Width="16px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:Label ID="lblSearchKey" runat="server" Text="Search Key:"></asp:Label>
                </td>
                <td class="auto-style5">
                                <asp:TextBox ID="txtSearchKey" runat="server" Height="22px"></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Go" ValidationGroup="search" />
                </td>
            </tr>
        </table>
        <fieldset>
            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
                OnRowCommand="gvData_RowCommand" CssClass="grid">
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
        <asp:Panel ID="pnlDetails" runat="server" Visible="False">
            <table>
                <%-- <tr>
                <td>
                    Total Amount:
                </td>
                <td>
                    <asp:TextBox ID="txttotal" CssClass="textbox" ReadOnly="true" runat="server"></asp:TextBox>
                </td>
            </tr>--%>
                <tr>
                    <td class="auto-style6">Total Invoice Amount:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtInvoiceAmount" CssClass="textbox" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6">Deposit:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtDepositAmount" CssClass="textbox" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6">Amount Already Paid:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtAmountPaid" CssClass="textbox" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6">Total Balance Due:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtBalance" CssClass="textbox" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6">Amount To Pay:
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtamount" CssClass="textbox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            CssClass="validation-error" ControlToValidate="txtamount" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6">Payment Mode:
                    </td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="ddlpaymentmode" runat="server" Height="24px" Width="127px" AutoPostBack="True" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style6">Document No:
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtdocno" CssClass="textbox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqdDocumentNo" runat="server" ControlToValidate="txtdocno" CssClass="validation-error" Enabled="False" ErrorMessage="*" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style7"></td>
                    <td class="auto-style4">
                        &nbsp;<asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm"
                            OnClick="btnSave_Click" />
                        <asp:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure you want to save this payment?" Enabled="True" TargetControlID="btnSave">
                        </asp:ConfirmButtonExtender>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                            CausesValidation="false" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>
            <fieldset>
            <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                Width="100%" CssClass="grid">
                <Columns>
                    <asp:BoundField DataField="PaymentDate" HeaderText="Payment Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="PaymentName" HeaderText="Payment Mode" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                    <asp:BoundField DataField="DocumentNo" HeaderText="Document No." />
                    <asp:BoundField DataField="FullNames" HeaderText="User" />
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfKey" runat="server" EnableViewState="True" Value='<%# Bind("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="Reports.aspx?report=PaymentNote&amp;id={0}" Text="Print" />
                </Columns>
                <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                    BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                    VerticalAlign="Middle" Width="100%" />
                <HeaderStyle CssClass="firstcol" />
                <SelectedRowStyle BackColor="#E3EEFC" />
            </asp:GridView>
        </fieldset>
        </asp:Panel>
    </fieldset>
</asp:Content>
