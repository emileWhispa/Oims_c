<%@ page title="Orion|ExpenseGroups" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="ExpenseGroups, App_Web_z1fnw4ln" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <fieldset>
        <legend>Expense Groups</legend>
        <table>
               <tr><td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style1">
                    <asp:Label ID="lblMessage" runat="server" BackColor="White" Font-Bold="True" Font-Italic="False" Font-Size="Medium" ForeColor="#99FF99"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;ExpenseGroup Name&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtexpGrpName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtexpGrpName" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    <asp:TextBox ID="txtexpgrpId" runat="server" Visible="False"></asp:TextBox>
                </td>
                <td class="auto-style1">
                    </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
            </tr>
            <tr>
            <td>&nbsp;</td>
                <td> 
                   
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" CausesValidation="false" OnClick="btnCancel_Click" />
                    
                        <br />
                    
                </td>
               
            </tr>
            <tr>
            <td>&nbsp;</td>
                <td> 
                   
                        &nbsp;</td>
               
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Expense Groups List</legend>
        
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="ExpenseGroupName" HeaderText="Expense Group Name" />
                               <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfKey" runat="server" EnableViewState="True" Value='<%# Bind("id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField HeaderText="" Text="Edit" CommandName="edt" ItemStyle-HorizontalAlign="Center" />
                <asp:ButtonField HeaderText="" Text="Delete" CommandName="del" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfKey0" runat="server" EnableViewState="True" Value='<%# Bind("Id") %>' />
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

