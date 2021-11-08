<%@ page title="Orion|Discounts" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="orion.ims.Discounts, App_Web_e2euaq1s" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1
        {
            width: 131px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
    <fieldset>
        <legend>Discounting Customers</legend>
        <table>
            <tr>
                <td class="auto-style1">

                    Customer Name :</td>
                <td>

                    <asp:TextBox ID="txtSearchCustomer" runat="server" AutoPostBack="True" OnTextChanged="txtSearchCustomer_TextChanged"></asp:TextBox>
                    <asp:DropDownList ID="ddlcustomer" runat="server" Height="22px" Width="371px" AutoPostBack="True" OnSelectedIndexChanged="ddlcustomer_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="ddlCustomer" />

                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    Discount Code :</td>
                <td>

                    <asp:TextBox ID="txtdiscountCode" runat="server" Height="19px" Width="140px" ReadOnly="True"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    Discount Rate :</td>
                <td>

                    <asp:TextBox ID="txtdiscrate" runat="server" Height="19px" Width="58px"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtdiscrate" />

                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    Comments :</td>
                <td>

                    <asp:TextBox ID="txtcomments" runat="server" Height="32px" TextMode="MultiLine" Width="256px"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    <asp:TextBox ID="txtdiscountId" runat="server" Height="19px" Width="58px" Visible="False"></asp:TextBox>

                </td>
                <td>

                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" CausesValidation="False" OnClick="btnCancel_Click" />
                    
                        </td>
            </tr>
            </table>
        <br />
    </fieldset>
    <fieldset>
        <legend>Discounting List</legend>
         <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="customer.CustomerName" HeaderText="Customer Name" />
                <asp:BoundField DataField="DiscountCode" HeaderText="Discount Code" />
                <asp:BoundField DataField="DiscountRate" HeaderText="Discount Rate" />
                <asp:BoundField DataField="DiscountDate" HeaderText="Discount Date" />
                <asp:BoundField DataField="Comments" HeaderText="Comments" />
                <asp:BoundField DataField="user.UserName" HeaderText="Created by" />
                <asp:ButtonField HeaderText="" Text="Edit" CommandName="edt" ItemStyle-HorizontalAlign="Center" />
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
</asp:Content>

