<%@ page title="bMobile|Rights" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Rights, App_Web_e2euaq1s" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <a href="Index.aspx">Home</a>
    <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <hr />
    <fieldset>
        <legend>Filters</legend>
        <table>
            <tr>
                <td>
                    User Group&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="ddlUserGroups" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUserGroups_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ||&nbsp;&nbsp;
                    Menu Group&nbsp;&nbsp;&nbsp;
                </td>
                <td>
                    <asp:DropDownList ID="ddlMenuGroup" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlMenuGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <fieldset>
            <legend>Rights</legend>
            <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand"
                CssClass="grid" DataKeyNames="Id">
                <Columns>
                    <asp:BoundField DataField="MenuName" HeaderText="Menu Name" ItemStyle-Width="200px" />
                    <asp:TemplateField Visible="true" HeaderText="Allow" ItemStyle-Width="80px">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAllow" runat="server" Checked='<%# Bind("AllowAccess") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
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
            <div style="padding-top:10px;">
                <asp:Button ID="btnSave" runat="server" Text="Update" 
                    CssClass="btn btn-primary btn-sm" onclick="btnSave_Click" />
            </div>
        </fieldset>
    </fieldset>
</asp:Content>
