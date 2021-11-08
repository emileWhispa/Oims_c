<%@ Page Title="Orion|Branches" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="Branches.aspx.cs" Inherits="orion.ims.Branches" %>

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
        <legend>Branch Details</legend>
        <table>
             <tr><td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style1">
                    <asp:Label ID="lblMessage" runat="server" BackColor="White" Font-Bold="True" Font-Italic="false" Font-Size="Medium" ForeColor="#99FF99"></asp:Label>
                </td>
            <tr>
                <td class="auto-style1">
                    Bank:
                </td>
                <td class="auto-style1">
                    <asp:DropDownList ID="ddlBankNew" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlBankNew" />
                </td>
            </tr>
                 <tr>
                <td>
                    Region:</td>
                <td>
                    <asp:DropDownList ID="ddlRegion" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    Branch Name:
                </td>
                <td>
                    <asp:TextBox ID="txtBranchName" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtBranchName" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtBranchId" runat="server" Visible="False"></asp:TextBox>
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
                    
                </td>
                <td>
                    
                        &nbsp;</td>
            </tr>
        </table>
        <br>
    </fieldset>
    <fieldset>
        <legend>Branches Lists<br />
        </legend>&nbsp;&nbsp; Bank:
        <asp:DropDownList ID="ddlBanks" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBanks_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                <asp:BoundField DataField="Bank.BankName" HeaderText="Bank Name" />
                <asp:BoundField DataField="Region.RegionName" HeaderText="Region Name" />
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
