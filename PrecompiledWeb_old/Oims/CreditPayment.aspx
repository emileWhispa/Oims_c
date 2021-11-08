<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="CreditPayment, App_Web_e2euaq1s" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <fieldset>
        <legend>Credit Payment</legend>
        <table class="auto-style1">
            <tr>
                <td>Customer:</td>
                <td>
                    <asp:TextBox ID="txtSearchCustomer" runat="server" AutoPostBack="True" OnTextChanged="txtSearchCustomer_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlCustomer" runat="server" Height="22px" Width="371px" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="ddlCustomer" />
                </td>
            </tr>
            <tr>
                <td>Total Credit Balance:</td>
                <td>
                    <asp:TextBox ID="txtBalance" runat="server" AutoPostBack="True"></asp:TextBox>
                    </td>
            </tr>
            </table>
    </fieldset>
    <br />
        <fieldset>
        <legend>Credit History</legend>
        <table class="auto-style1">
            <tr>
                <td>
                        <asp:GridView ID="gvCredit" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                            Width="100%" CssClass="grid" OnSelectedIndexChanged="gvCredit_SelectedIndexChanged" DataKeyNames="CreditId">
                            <Columns>
                                <asp:BoundField DataField="CreditDate" HeaderText="Credit Date" />
                                <asp:BoundField DataField="OpeningBalance" HeaderText="Opening Balance" />
                                <asp:BoundField DataField="Amount" HeaderText="Total Amount" />
                                <asp:BoundField DataField="PaidAmount" HeaderText="Paid Amount" />
                                <asp:BoundField DataField="Balance" HeaderText="Balance Due" />
                                <asp:BoundField DataField="FullNames" HeaderText="User" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:BoundField DataField="CreditId" HeaderText="CreditId" Visible="False" />
                                <asp:CommandField ShowSelectButton="True" />
                                <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSalesVal" runat="server" EnableViewState="True" Value='<%# Eval("SalesId") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            </Columns>
                            <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                                BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" Width="100%" />
                            <HeaderStyle CssClass="firstcol" />
                            <SelectedRowStyle BackColor="#E3EEFC" />
                        </asp:GridView>
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
                    <td class="auto-style2">Total Invoice Amount:</td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtInvoiceAmount" CssClass="textbox" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Amount To Pay:
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtamount" CssClass="textbox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            CssClass="validation-error" ControlToValidate="txtamount" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Payment Mode:
                    </td>
                    <td class="auto-style4">
                        <asp:DropDownList ID="ddlpaymentmode" runat="server" Height="24px" Width="127px" AutoPostBack="True" OnSelectedIndexChanged="ddlpaymentmode_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Document No:
                    </td>
                    <td class="auto-style4">
                        <asp:TextBox ID="txtdocno" CssClass="textbox" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqdDocumentNo" runat="server" ControlToValidate="txtdocno" CssClass="validation-error" Enabled="False" ErrorMessage="*" />
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <asp:TextBox ID="txtCreditId" runat="server" CssClass="textbox" Enabled="False" Height="16px" ReadOnly="True" Width="16px" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtSalesId" runat="server" CssClass="textbox" Enabled="False" Height="16px" ReadOnly="True" Width="16px" Visible="False"></asp:TextBox>
                    </td>
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
                </td>
            </tr>
            </table>
    </fieldset>
</asp:Content>

