<%@ page title="Oims|Productionitems" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" inherits="orion.ims.Productionitems, App_Web_z1fnw4ln" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 90px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Label ID="lbrError" runat="server" ForeColor="Red"></asp:Label>
    <fieldset>
        <legend>Production Items</legend>
        <table style="width:100%">
            <tr>
                <td class="auto-style1">
                    Batch No:
                </td>
                <td>
                    <asp:TextBox ID="txtbatchno" ReadOnly="true" runat="server" ValidationGroup="Entry"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    Product:
                </td>
                <td>
                        <asp:TextBox ID="txtSearchItem" runat="server" AutoPostBack="True" OnTextChanged="txtSearchItem_TextChanged"></asp:TextBox>
                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" Height="22px" Width="371px">
                        </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlProduct" ValidationGroup="Entry" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    Units:
                </td>
                <td>
                    <asp:TextBox ID="txtunits" runat="server" ValidationGroup="Entry"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtunits" ValidationGroup="Entry" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    &nbsp;
                </td>
                <td>
                <asp:TextBox ID="hfId" runat="server" Width="67px" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    
                </td>
                <td>
                    <%--</div>--%>
 
                            <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm"
                                OnClick="btnSave_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                                CausesValidation="false" OnClick="btnCancel_Click" />
                        <%--</div>--%>
                    </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="Product.Productname" HeaderText="Product" />
                <asp:BoundField DataField="units" HeaderText="Units" />
                <asp:ButtonField HeaderText="" Text="Edit" CommandName="sel" ItemStyle-HorizontalAlign="Center" />
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
    <fieldset>
        <br />
        <table>
            <tr>
                <td>
        <asp:Button ID="btnSubmit"  runat="server" Text="Submit" CssClass="btn btn-primary btn-sm" OnClick="btnSubmit_Click" Visible="False"/>
                    </td>
                </tr>
        </table>
    </fieldset>
</asp:Content>
