<%@ page title="Orion|CashCollections" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="orion.ims.CashCollections, App_Web_e2euaq1s" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1
        {
            width: 100%;
            height: 196px;
        }
        .auto-style2
        {
            width: 154px;
        }
        .auto-style3
        {
            width: 154px;
            height: 18px;
        }
        .auto-style4
        {
            height: 18px;
        }
        .auto-style5
        {
            width: 154px;
            height: 26px;
        }
        .auto-style6
        {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <fieldset>
        <legend>Cash Collection </legend>

        <table class="auto-style1">
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">Collector Name :</td>
                <td>
                    <asp:TextBox ID="txtCollector" runat="server" Height="20px" Width="305px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtCollector" />
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Total Cash :</td>
                <td class="auto-style6">
                    <asp:TextBox ID="txtcash" runat="server" AutoPostBack="True" Height="19px" OnTextChanged="txtcash_TextChanged" Width="89px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtcash" />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Total Cheque :</td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtcheque" runat="server" AutoPostBack="True" Height="19px" OnTextChanged="txtcheque_TextChanged" Width="89px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtcheque" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Total Amount :</td>
                <td>
                    <asp:TextBox ID="txtTotalAmount" runat="server" Height="19px" ReadOnly="True" Width="106px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtTotalAmount" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    <asp:TextBox ID="txtcollectionId" runat="server" Visible="False"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
             <tr>
                <td class="auto-style2">
                    &nbsp;</td>
                <td>
                    
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm"
                            OnClick="btnSave_Click" />
                        <ajaxToolkit:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server" ConfirmText="Do you want to save this cash collection?" Enabled="True" TargetControlID="btnSave">
                        </ajaxToolkit:ConfirmButtonExtender>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                            CausesValidation="false" OnClick="btnCancel_Click" />
                    <br />
                    
                </td>
                <td>
                    
                        &nbsp;</td>
            </tr>
        </table>

    </fieldset>
    <fieldset><br />
         <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid" PageSize="20">
            <Columns>
                <asp:BoundField DataField="CollectorName" HeaderText="Collector Name" />
                <asp:BoundField DataField="CollectionDate" HeaderText="Collection Date" />
                <asp:BoundField DataField="TotalCash" HeaderText="Total Cash" />
                <asp:BoundField DataField="TotalCheque" HeaderText="Total Cheque" />
                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                <asp:CheckBoxField DataField="Cancelled" HeaderText="Cancelled" />
                <asp:ButtonField HeaderText="" Text="Reverse" CommandName="edt" ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfKey" runat="server" EnableViewState="True" Value='<%# Bind("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="id" DataNavigateUrlFormatString="~/Reports.aspx?report=CashCollection&amp;id={0}" Text="Print" />
            </Columns>
            <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                VerticalAlign="Middle" Width="100%" />
            <HeaderStyle CssClass="firstcol" />
            <SelectedRowStyle BackColor="#E3EEFC" />
        </asp:GridView>
    </fieldset>
</asp:Content>

