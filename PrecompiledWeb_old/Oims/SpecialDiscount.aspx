<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="SpecialDiscount, App_Web_e2euaq1s" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1
        {
            width: 136px;
        }
        .auto-style2
        {
            width: 141px;
        }
        .auto-style3
        {
            width: 135px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <fieldset>
        <legend>Special Discount</legend>
        <table>
            <tr>
                <td class="auto-style3" >Select Sales No:
                </td>
                <td>
                    <asp:DropDownList ID="ddlinvoice" runat="server" Height="19px" AutoPostBack="true" OnSelectedIndexChanged="ddlinvoice_SelectedIndexChanged"
                        Width="243px">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlinvoice" />
                </td>
            </tr>
         </table>
        <table>
            <tr>
                <td class="auto-style1">
                    <asp:CheckBox ID="ChkDiscount" runat="server" Text="Apply To All" AutoPostBack="True" OnCheckedChanged="ChkDiscount_CheckedChanged" />
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtdisrate" runat="server"></asp:TextBox>
                    <cc1:TextBoxWatermarkExtender ID="txtdisrate_TextBoxWatermarkExtender" runat="server" BehaviorID="txtdisrate_TextBoxWatermarkExtender" TargetControlID="txtdisrate" WatermarkText="Discount Rate">
                    </cc1:TextBoxWatermarkExtender>
                </td>
                <td>
                       <asp:Button ID="btnAction" runat="server" Text="Apply" CssClass="btn btn-primary btn-sm"  Width="99px" OnClick="btnAction_Click" />
                   </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <fieldset>
                        <asp:GridView ID="gvData" runat="server" AllowPaging="True" DataKeyNames="Id" AutoGenerateColumns="False" CssClass="grid" EmptyDataText="No data to display" Width="100%" OnRowEditing="gvData_RowEditing" OnRowUpdating="gvData_RowUpdating" OnRowCancelingEdit="gvData_RowCancelingEdit" OnPageIndexChanging="gvData_PageIndexChanging">
                    <Columns>
                        <asp:CommandField ShowEditButton="True" />
                     <asp:BoundField DataField="product.Productname" HeaderText="Product" ReadOnly="true" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="true" />
                    <asp:BoundField DataField="unitPrice" HeaderText="Unit Price" ReadOnly="true" />
                        <asp:TemplateField HeaderText="Discount">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtDiscounts" runat="Server" Text='<%# Eval("Discounts") %>'
                                    Width="40px" BorderColor="Black" BorderWidth="1px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="req3" runat="Server" Text="*" ControlToValidate="txtDiscounts"></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%# Eval("Discounts") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="Id" Visible="False" />
                    </Columns>
                    <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="100%" />
                    <HeaderStyle CssClass="firstcol" />
                    <SelectedRowStyle BackColor="#E3EEFC" />
                </asp:GridView>
                    </fieldset>
                </td>
            </tr>
        </table>
        </fieldset>
</asp:Content>

