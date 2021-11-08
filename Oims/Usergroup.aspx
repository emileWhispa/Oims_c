<%@ Page Title="bMobile|User Groups" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Usergroup.aspx.cs" Inherits=" orion.ims.Usergroup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1
        {
            width: 158px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <a href="Index.aspx">Home</a>
     <hr />
    <fieldset>
        <legend>User Group Details</legend>
        <table>
            <tr>
                <td>Group Name:</td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtgroupname" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtgroupname"/>
                </td>
            </tr>
            <tr>
                <td>Description:</td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtdescription" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtdescription"/>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style1">
                    &nbsp;</td>
            </tr>
           <tr>
            <td><asp:HiddenField ID="hfId" runat="server" Value="0" /></td>
                 <td class="auto-style1">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" CausesValidation="false" OnClick="btnCancel_Click" />

                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>User Group Lists</legend>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True"  OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="Groupname" HeaderText="Group Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:ButtonField HeaderText="" Text="Edit" CommandName="edt" ItemStyle-HorizontalAlign="Center" />
                <asp:ButtonField HeaderText="" Text="Delete" CommandName="del" ItemStyle-HorizontalAlign="Center" />
                  <asp:ButtonField HeaderText="" Text="Access Rights" CommandName="rights" ItemStyle-HorizontalAlign="Center" />
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
