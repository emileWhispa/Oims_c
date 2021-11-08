<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Quotation, App_Web_z1fnw4ln" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .auto-style2 {
            width: 165px;
            text-align: right;
        }

        .auto-style3 {
            width: 165px;
        }

        .auto-style4 {
            width: 100%;
        }

        .auto-style5 {
            width: 180px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
            <fieldset>
                <legend>Sales Details</legend>
                <table style="width: 100%">
                    <tr>
                        <td class="auto-style2">Customer Name:</td>
                        <td>
                            <asp:TextBox ID="txtSearchCustomer" runat="server" AutoPostBack="True" OnTextChanged="txtSearchCustomer_TextChanged"></asp:TextBox>
                            <asp:DropDownList ID="ddlCustomer" runat="server" Height="22px" Width="400px" AutoPostBack="True" OnSelectedIndexChanged="ddlCustomer_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="ddlCustomer" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <asp:CheckBox ID="chkDiscountedCustomer" runat="server" OnCheckedChanged="chkDiscountedCustomer_CheckedChanged" Text="Discounted Customer:" AutoPostBack="True" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchCustomer1" runat="server" AutoPostBack="True" OnTextChanged="txtSearchCustomer1_TextChanged"></asp:TextBox>
                            <asp:DropDownList ID="ddlDiscountedCustomer" runat="server" Height="22px" Width="400px" AutoPostBack="True" Enabled="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">Order Date:</td>
                        <td>
                            <asp:TextBox ID="dteOrderDate" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="dteOrderDate_CalendarExtender" runat="server" Enabled="True" Format="yyyy-MM-dd" TargetControlID="dteOrderDate">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="dteOrderDate" ValidationGroup="Sale" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">Deposit:</td>
                        <td>
                            <asp:TextBox ID="txtDepositAmount" runat="server">0</asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtDepositAmount" ValidationGroup="Sale" />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">Remarks:</td>
                        <td>
                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="415px" Height="40px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">
                            <asp:TextBox ID="txtSalesId" runat="server" Visible="False"></asp:TextBox>
                        </td>
                        <td>&nbsp;<asp:Button ID="btnSaveSale" runat="server" Text="Save" CssClass="btn btn-default btn-sm" OnClick="btnSaveSale_Click" ValidationGroup="Sale" />
                            <asp:Button ID="btnCancelSale" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" CausesValidation="False" ValidationGroup="Sale" OnClick="btnCancelSale_Click" />
                            <asp:Label ID="lblError0" runat="server" ForeColor="#FF3300"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <hr />
                            <table class="auto-style4">
                                <tr>
                                    <td class="auto-style5">Search Sales by Order No: </td>
                                    <td>
                                        <asp:TextBox ID="txtSearchKey" runat="server" Height="22px" Width="80px"></asp:TextBox>
                                        <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Go" Width="31px" ValidationGroup="srch" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5">Search by Customer Name:</td>
                                    <td>
                                        <asp:TextBox ID="txtSearchByName" runat="server" Height="22px" Width="200px"></asp:TextBox>
                                        <asp:Button ID="btnSearchByName" runat="server" OnClick="btnSearchByName_Click" Text="Go" Width="31px" ValidationGroup="srch" Height="27px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5">Search by Invoice No:</td>
                                    <td>
                                        <asp:TextBox ID="txtSearchByInvoiceNo" runat="server" Height="22px" Width="200px"></asp:TextBox>
                                        <asp:Button ID="btnSearchByInvoiceNo" runat="server" OnClick="btnSearchByInvoiceNo_Click" Text="Go" ValidationGroup="srch" Width="31px" />
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                                Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
                                OnRowCommand="gvData_RowCommand" CssClass="grid" DataKeyNames="Id" PageSize="15">
                                <Columns>
                                    <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                    <asp:BoundField DataField="QuoteDate" HeaderText="Quote Date" DataFormatString="{0:yyyy-MM-dd}" />
                                    <asp:BoundField DataField="SalesRef" HeaderText="Order No" />
                                    <asp:BoundField HeaderText="Deposit" DataField="Deposit" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                                    <asp:BoundField DataField="Type" HeaderText="Type" />
                                    <asp:HyperLinkField DataNavigateUrlFields="Id" Text="Items" DataNavigateUrlFormatString="~/QuotationItems.aspx?id={0}" />
                                    <asp:ButtonField HeaderText="" Text="Edit" CommandName="sel" ItemStyle-HorizontalAlign="Center">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:ButtonField HeaderText="" Text="Delete" CommandName="del" ItemStyle-HorizontalAlign="Center" Visible="False">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:ButtonField>
                                    <asp:HyperLinkField DataNavigateUrlFields="Id" Text="Print" DataNavigateUrlFormatString="~/Reports.aspx?report=quote&id={0}" />
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hfKey" runat="server" EnableViewState="True" Value='<%# Bind("Id") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Id" HeaderText="SalesId" Visible="False" />
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
            </fieldset>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="IMGDIV" runat="server" style="position: absolute; left: 50%; top: 50%; visibility: visible;
                vertical-align: middle;">
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ajax-loader-1.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>


