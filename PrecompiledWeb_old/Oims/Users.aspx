<%@ page title="bMobile|Users" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" inherits="orion.ims.Users, App_Web_e2euaq1s" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
    .style1
    {
        width: 302px
    }
    .style2
    {
        height: 22px;
    }
    .style3
    {
        width: 302px;
        height: 22px;
    }
</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <a href="Index.aspx">Home</a>
    <h3 class="header">| Users</h3>
    <hr />
     <fieldset>
        <legend>User Details</legend>
        <table>
            <tr>
                <td>User Name:</td>
                <td class="style1">
                    <asp:TextBox ID="txtusername" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtusername"/>
                </td>
            </tr>
            <tr>
                <td>Full Name:</td>
                <td class="style1">
                    <asp:TextBox ID="txtfullname" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtfullname"/>
                </td>
            </tr>
             <tr>
                <td>Password:</td>
                <td class="style1">
                    <asp:TextBox ID="txtpassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtpassword"/>
                </td>
            </tr>
            <tr>
                <td>User Group:</td>
                <td class="style1">
                    <asp:DropDownList ID="ddlusergroup" runat="server"></asp:DropDownList>
                </td>
                 </tr>
                 <tr>
                 <td>User POS:</td>
                 <td class="style1">
                    <asp:DropDownList ID="ddlpos" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
            <td class="style2">Email:</td>
             <td class="style3">
                    <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
               </td>
            </tr>
            <tr>
            <td>Phone:</td>
             <td class="style1">
                    <asp:TextBox ID="txtphone" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtphone"/>
                </td>
            </tr>
            <tr>
            <td>&nbsp;</td>
             <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td><asp:HiddenField ID="hfId" runat="server" Value="0" /></td>
                <td class="style1">
                    <%--</div>--%>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm" OnClick="btnSave_Click" />
                        &nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" CausesValidation="false" OnClick="btnCancel_Click" />
                    <%--</div>--%>

                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Users List</legend>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" CssClass="grid" OnSelectedIndexChanged="gvData_SelectedIndexChanged" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand">
            <Columns>
                <asp:BoundField DataField="UserName" HeaderText="UserName" />
                <asp:BoundField DataField="fullnames" HeaderText="Full Names" />
                <asp:BoundField DataField="Pos.POSNAME" HeaderText="POS NAME" />
                <asp:BoundField DataField="UserGroup.GroupName" HeaderText="User Group" />
                 <asp:BoundField DataField="LastLogin" HeaderText="Last Login" />
                   <asp:BoundField DataField="email" HeaderText="Email" />
                     <asp:BoundField DataField="phone" HeaderText="Phone No" />
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
