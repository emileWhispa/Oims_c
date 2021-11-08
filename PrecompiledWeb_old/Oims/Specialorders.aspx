<%@ page title="Oims|Specialorders" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" inherits="orion.ims.Specialorders, App_Web_e2euaq1s" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }

        .auto-style2 {
            width: 117px;
        }

        .auto-style3 {
            height: 26px;
            width: 117px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <asp:Panel ID="Panel1" runat="server">
        <fieldset>
            <legend>Special Orders</legend>
            <table>
                <tr>
                    <td class="auto-style2">Customer:
                    </td>
                    <td>
                        <asp:TextBox ID="txtSearchCustomer" runat="server" AutoPostBack="True" OnTextChanged="txtSearchCustomer_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddlcustomers" runat="server" Height="22px" Width="371px" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlcustomers" CssClass="validation-error" ErrorMessage="*" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Order Date
                    </td>
                    <td>
                        <asp:TextBox runat="server" CssClass="textbox" ID="txtorderdate" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
                            TargetControlID="txtorderdate">
                        </asp:CalendarExtender>

                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Product Category:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlcategory" runat="server" Height="22px" Width="350px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                            CssClass="validation-error" ControlToValidate="ddlcategory" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Units:
                    </td>
                    <td>
                        <asp:TextBox ID="txtunits" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                            CssClass="validation-error" ControlToValidate="txtunits" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Amount:
                    </td>
                    <td>
                        <asp:TextBox ID="txtamount" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                            CssClass="validation-error" ControlToValidate="txtamount" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Delivery Date
                    </td>
                    <td>
                        <asp:TextBox runat="server" CssClass="textbox" ID="txtdeliverydate" Width="100px"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" Format="dd MMM yyyy" runat="server"
                            TargetControlID="txtdeliverydate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Description:
                    </td>
                    <td>
                        <asp:TextBox ID="txtdescr" runat="server" TextMode="MultiLine" Width="216px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                            CssClass="validation-error" ControlToValidate="txtdescr" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">With Sample
                    </td>
                    <td class="auto-style1">
                        <asp:RadioButtonList runat="server" ID="rdosample" RepeatDirection="Horizontal"
                            Height="20px" Width="144px">
                            <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                            <asp:ListItem Value="0">No</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">Collection POS:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlcollectionpos" runat="server" Height="22px" Width="224px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                            CssClass="validation-error" ControlToValidate="ddlcollectionpos" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2">
                        <asp:TextBox ID="hfId" runat="server" Width="67px" Visible="False"></asp:TextBox>
                    </td>
                    <td>&nbsp;
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
                    <asp:BoundField DataField="customername" HeaderText="Customer" />
                    <asp:BoundField DataField="Descr" HeaderText="Item Description" />
                    <asp:BoundField DataField="orderdate" HeaderText="Order Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:BoundField DataField="ExpectedDeliveryDate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="Collection Date" />
                    <asp:BoundField DataField="Quantity" HeaderText="Units" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" />
                    <asp:BoundField DataField="OriginPOS" HeaderText="Origin POS" />
                    <asp:BoundField DataField="CollectionPOSName" HeaderText="Collection POS" />
                    <asp:BoundField DataField="sample" HeaderText="Sample?" />

                    <asp:ButtonField HeaderText="" Text="Edit" CommandName="sel" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonField>
                    <asp:ButtonField HeaderText="" Text="Delete" CommandName="del" ItemStyle-HorizontalAlign="Center">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:ButtonField>
                    <asp:ButtonField CommandName="sub" Text="Submit" />
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
    </asp:Panel>
</asp:Content>
