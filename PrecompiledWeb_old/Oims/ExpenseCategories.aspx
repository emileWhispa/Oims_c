<%@ page title="Orion|ExpenseCategories" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="orion.ims.ExpenseCategories, App_Web_e2euaq1s" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1
        {
            height: 25px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    <fieldset>
        <legend>Expense Categories Details</legend>
        <table>
            <tr>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style1">
                    <asp:Label ID="lblMessage" runat="server" BackColor="White" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#99FF99"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    Expense Group:</td>
                <td class="auto-style1">
                    <asp:DropDownList ID="ddlexpgroup" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
                 <tr>
                <td>
                    Expense Name:</td>
                <td>
                    <asp:TextBox ID="txtexpname" runat="server"></asp:TextBox>
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtexpname" />
                </td>
            </tr>
            <tr>
                <td>
                    Sage Ledger No :
                </td>
                <td>
                    <asp:TextBox ID="txtsageno" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtsageno" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtexpcatId" runat="server" Visible="False"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                            CausesValidation="false" OnClick="btnCancel_Click" />
                    
                        <br />
                        <br />
                    
                </td>
                <td>
                    
                        <br />
                        <br />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Expense Categories List
        </legend><asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="expensegroup.ExpenseGroupName" HeaderText="ExpenseGroup Name" />
                <asp:BoundField DataField="ExpenseName" HeaderText="Expense Name" />
                <asp:BoundField DataField="SageLedgerNo" HeaderText="Sage LedgerNo" />
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
