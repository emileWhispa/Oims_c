<%@ page title="Orion|POS" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="orion.ims.POS, App_Web_z1fnw4ln" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1
        {
            height: 22px;
            width: 156px;
        }
        .auto-style2
        {
            height: 22px;
            width: 239px;
        }
        .auto-style3
        {
            width: 239px;
        }
        .auto-style4
        {
            width: 156px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <fieldset>
        <legend> POINT OF SALE</legend>
        <table>
               <tr><td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style2">
                    <asp:Label ID="lblMessage" runat="server" BackColor="White" Font-Bold="True" Font-Italic="False" Font-Size="Medium" ForeColor="#99FF99"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    POS Name:
                </td>
                <td class="auto-style3">
                    
                    <asp:TextBox ID="txtposname" runat="server" Height="19px" Width="205px"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtposname" />
                    
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    POS CODE :</td>
                <td class="auto-style3">
                    
                    <asp:TextBox ID="txtposcode" runat="server"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtposname" />
                    
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    POS Address:
                </td>
                <td class="auto-style3">

                    <asp:TextBox ID="txtposaddress" runat="server"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtposaddress" />

                </td>
            </tr>
            <tr>
                <td class="auto-style4">

                    MainStore:</td>
                <td class="auto-style3">
                    <asp:CheckBoxList ID="chkmainstore" runat="server" RepeatDirection="Horizontal" Width="147px">
                        <asp:ListItem>Yes</asp:ListItem>
                        <asp:ListItem Selected="True">No</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    <asp:TextBox ID="txtPosId" runat="server" Visible="False"></asp:TextBox>

                </td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style3"> 
                   
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" CausesValidation="False" OnClick="btnCancel_Click" />
                    
                        <br />
                    
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
          <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="POSName" HeaderText="POS Name" />
                <asp:BoundField DataField="POSCode" HeaderText="POS Code" />
                <asp:BoundField DataField="POSAddress" HeaderText="POS Address" />
                <asp:BoundField DataField="MainStore" HeaderText="Is MainStore" />
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

