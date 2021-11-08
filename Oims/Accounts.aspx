<%@ Page Title="Orion|Accounts" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Accounts.aspx.cs" Inherits="Accounts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style2
        {
            width: 124px;
        }
        .auto-style3
        {
            width: 216px;
        }
        .auto-style4
        {
            width: 124px;
            height: 18px;
        }
        .auto-style5
        {
            width: 216px;
            height: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <fieldset>
        <legend>Accounts Details</legend>
        <table>
            <tr><td class="auto-style2"></td>
                <td class="auto-style3">
                    <asp:Label ID="lblMessage" runat="server" BackColor="White" Font-Bold="True" Font-Italic="False" Font-Size="Medium" ForeColor="#99FF99"></asp:Label>
                </td>
               </tr>
            <tr>
                <td class="auto-style2">
                    AccAccount No:
                </td>
                    <td class="auto-style3">
                    <asp:TextBox ID="txtaccountNo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtaccountNo" />
                </td>
            </tr>
              <tr>
                <td class="auto-style2">
                    Account Name:
                </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtaccntnme" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtaccntnme" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    Bank:
                <td class="auto-style3">
                    <asp:DropDownList ID="ddlbank" runat="server" OnSelectedIndexChanged="ddlbank_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlbank" />
                </td>
            </tr>
            <tr>
                <td class="auto-style2">
                    Branch</td>
                <td class="auto-style3">
                    <asp:DropDownList ID="ddlbranch" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
              <tr>
                <td class="auto-style2">
                    Currency:
                </td>
                <td class="auto-style3">
                    <asp:DropDownList ID="ddlcurrency" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlcurrency" />
                </td>
            </tr>

            <tr>
                <td class="auto-style2">
                    &nbsp;
                <asp:TextBox ID="txtaccountId" runat="server" Width="67px" Visible="False"></asp:TextBox>
                </td>
                <td class="auto-style3">
                    &nbsp;</td>
            
            <tr>
                <td class="auto-style2">
                    
                </td>
                <td class="auto-style3">
                   
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                                CausesValidation="false" OnClick="btnCancel_Click" />
                       
                    </td>
            </tr>
        </table>
     <br />
    </fieldset>
     <fieldset>
         <legend> Accounts List <br /></legend>
         </legend>&nbsp;&nbsp; Bank:
        <asp:DropDownList ID="ddlBanks" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBanks_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="AccountNo" HeaderText="Account No" />
                <asp:BoundField DataField="AccountName" HeaderText="Account Name" />
                <asp:BoundField DataField="Bank.BankName" HeaderText="Bank Name" />
                <asp:BoundField DataField="Branch.BranchName" HeaderText="Branch Name" />
                <asp:BoundField DataField="Currency.CurrencyName" HeaderText="Account No" />
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

