<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="ItemDelivery, App_Web_z1fnw4ln" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 171px;
            text-align: left;
        }

        .auto-style2 {
            width: 599px;
        }

        .auto-style3 {
            width: 171px;
            text-align: left;
            height: 28px;
        }

        .auto-style4 {
            width: 599px;
            height: 28px;
        }

        .auto-style5 {
            width: 171px;
            text-align: left;
            height: 25px;
        }

        .auto-style6 {
            width: 599px;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <fieldset>
        <legend>Item Delivery</legend>
        <table>
            <tr>
                <td class="auto-style1">Select Invoice:
                </td>
                <td class="auto-style2">
                    <asp:DropDownList ID="ddlinvoice" runat="server" Height="23px" AutoPostBack="true" OnSelectedIndexChanged="ddlinvoice_SelectedIndexChanged"
                        Width="201px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlinvoice" />
                    <asp:TextBox runat="server" Height="22px" Width="172px" ID="txtInvoiceNo" AutoPostBack="True" OnTextChanged="txtInvoiceNo_TextChanged"></asp:TextBox>

                    <asp:TextBoxWatermarkExtender ID="txtInvoiceNo_TextBoxWatermarkExtender" runat="server" Enabled="True" TargetControlID="txtInvoiceNo" WatermarkText="Or type Invoice No.">
                    </asp:TextBoxWatermarkExtender>
                    <asp:TextBox runat="server" Height="22px" Width="100px" ID="txtBatchNo" Visible="False"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="auto-style5">Item:
                </td>
                <td class="auto-style6">
                    <asp:DropDownList ID="ddlItem" runat="server" Height="23px" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged"
                        Width="469px">
                    </asp:DropDownList>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlItem" CssClass="validation-error" ErrorMessage="*" />

                </td>
            </tr>
            <tr>
                <td class="auto-style1">Quantity Purchased:</td>
                <td class="auto-style2">
                    <asp:TextBox runat="server" Height="21px" Width="60px" ID="txtQtyPurchased" ReadOnly="True"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtQtyPurchased" CssClass="validation-error" ErrorMessage="*" />

                </td>
            </tr>
            <tr>
                <td class="auto-style1">Quantity Delivered:</td>
                <td class="auto-style2">
                    <asp:TextBox runat="server" Height="21px" Width="60px" ID="txtQtyDelivered" ReadOnly="True"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtQtyDelivered" CssClass="validation-error" ErrorMessage="*" />

                </td>
            </tr>
            <tr>
                <td class="auto-style1">Quantity Remaining:</td>
                <td class="auto-style2">
                    <asp:TextBox runat="server" Height="21px" Width="60px" ID="txtQtyRemaining" ReadOnly="True"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtQtyRemaining" CssClass="validation-error" ErrorMessage="*" />

                </td>
            </tr>
            <tr>
                <td class="auto-style3">Quantity Available:</td>
                <td class="auto-style4">
                    <asp:TextBox runat="server" Height="21px" Width="60px" ID="txtQtyAvailable" ReadOnly="True" BackColor="#00FFCC"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtQtyAvailable" CssClass="validation-error" ErrorMessage="*" />

                </td>
            </tr>
            <tr>
                <td class="auto-style3">Quantity to be delivered:</td>
                <td class="auto-style4">
                    <asp:TextBox runat="server" Height="21px" Width="60px" ID="txtQuantityToDeliver" BackColor="#FFFFCC"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtQuantityToDeliver" CssClass="validation-error" ErrorMessage="*" />

                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td class="auto-style2">&nbsp;<asp:Button runat="server" CausesValidation="False" Text="Deliver" CssClass="btn btn-primary btn-sm" ID="btnDeliver" OnClick="btnDeliver_Click"></asp:Button>


                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Current Deliveries</legend>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" CssClass="grid" OnRowCommand="gvData_RowCommand" DataKeyNames="DeliveryId">
            <Columns>
                <asp:BoundField DataField="Productname" HeaderText="Product" ReadOnly="true" />
                <asp:BoundField DataField="Quantity" HeaderText="Quantity Purchased" ReadOnly="true" />
                <asp:BoundField DataField="QuantityDelivered" HeaderText="Qty Delivered" ReadOnly="true" />
                <asp:BoundField DataField="QuantityRemaining" HeaderText="Qty Remaining" ReadOnly="true" />
                <asp:BoundField DataField="QuantityPicked" HeaderText="Qty to Deliver" />
                <asp:BoundField DataField="Balance" HeaderText="New Balance" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:ButtonField CommandName="reverse" Text="Cancel" />
                <asp:BoundField DataField="DeliveryId" Visible="False" />
            </Columns>
            <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                VerticalAlign="Middle" Width="100%" />
            <HeaderStyle CssClass="firstcol" />
            <SelectedRowStyle BackColor="#E3EEFC" />
        </asp:GridView>
        &nbsp;&nbsp;
        <br />
&nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" CausesValidation="False" Text="Delivery Note" CssClass="btn btn-primary btn-sm" ID="btnPrint" Visible="False" OnClick="btnPrint_Click"></asp:Button>
        <br />
        <br />
        <fieldset>
            <legend>Previous Deliveries</legend>
            <table style="width: 100%">
                <tr>
                    <td style="width: 100%">
                        <asp:GridView ID="gvPreviousDelivery" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                            Width="100%" AllowPaging="True" CssClass="grid">
                            <Columns>
                                <asp:BoundField DataField="DeliveryDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Delivery Date" />
                                <asp:BoundField DataField="Productname" HeaderText="Product" ReadOnly="true" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity Purchased" ReadOnly="true" />
                                <asp:BoundField DataField="QuantityDelivered" HeaderText="Qty Delivered" ReadOnly="true" />
                                <asp:BoundField DataField="QuantityRemaining" HeaderText="Qty Remaining" ReadOnly="true" />
                                <asp:BoundField DataField="QuantityPicked" HeaderText="Qty to Deliver" />
                                <asp:BoundField DataField="Status" HeaderText="Status" />
                                <asp:HyperLinkField DataNavigateUrlFields="BatchNo" DataNavigateUrlFormatString="~/Reports.aspx?report=customerdeliverynote&amp;id={0}" Text="Print" />
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

    </fieldset>
</asp:Content>
