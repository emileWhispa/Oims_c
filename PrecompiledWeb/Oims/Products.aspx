<%@ page title="Orion|Products" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="orion.ims.Products, App_Web_z1fnw4ln" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            text-align: right;
        }
        .auto-style2 {
            height: 14px;
        }
        .auto-style3 {
            text-align: right;
            height: 30px;
        }
        .auto-style4 {
            height: 30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    <fieldset>
        <legend>Product Details</legend>
         <table style="width:100%">
               <tr><td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblMessage" runat="server" BackColor="White" Font-Bold="False" Font-Italic="False" Font-Size="Small" ForeColor="#000099"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Product Name</td>
                <td>
                    <asp:TextBox ID="txtprodname" runat="server" Height="20px" Width="180px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtprodname" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Description:</td>
                <td>
                    <asp:TextBox ID="txtdescr" runat="server" Height="20px" Width="454px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtdescr" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">ReOrder Level:</td>
                <td>
                    <asp:TextBox ID="txtreorder" runat="server" Height="20px" Width="114px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtreorder" />
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Packing Unit:</td>
                <td class="auto-style4">
                    <asp:TextBox ID="txtpackingunit" runat="server" Height="20px" Width="116px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtpackingunit" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Selling Price</td>
                <td>
                    <asp:TextBox ID="txtsellingprice" runat="server" Height="20px" Width="96px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtsellingprice" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Product Category:</td>
                <td>
                    <asp:DropDownList ID="ddlprodcategory" runat="server">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtproductId" runat="server" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
            <td class="auto-style2"></td>
                <td class="auto-style2"> 
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm" OnClick="btnSave_Click" />
                        <asp:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server" ConfirmText="Do you want to save this product?" Enabled="True" TargetControlID="btnSave">
                    </asp:ConfirmButtonExtender>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" CausesValidation="False" OnClick="btnCancel_Click" />
                    
                        <br />
                    
                </td>
               
            </tr>
            </table>
        
    </fieldset>
        <fieldset>
            <legend>List of Products</legend>
            <table>
            <tr>
                <td>Search by Product:</td>
                <td>
                        <asp:TextBox ID="txtSearchItem" runat="server" Height="24px" ValidationGroup="search"></asp:TextBox>
                        <asp:Button ID="btnGo" runat="server" OnClick="btnGo_Click" Text="Go" ValidationGroup="search" />
                        </td>
            </tr>
        </table>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid" PageSize="20" OnSelectedIndexChanged="gvData_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                <asp:BoundField DataField="Description" HeaderText="Description" />
                <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder Level" />
                <asp:BoundField DataField="PackagingUnit" HeaderText="Packing Unit" />
                <asp:BoundField DataField="SellingPrice" HeaderText="Selling Price" />
                <asp:BoundField DataField="ProductCategory.CategoryName" HeaderText=" Category Name" />
                <asp:ButtonField HeaderText="" Text="Delete" CommandName="del" ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfKey" runat="server" EnableViewState="True" Value='<%# Bind("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:HyperLinkField DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/Products.aspx?id={0}" Text="Edit" />
            </Columns>
            <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                VerticalAlign="Middle" Width="100%" />
            <HeaderStyle CssClass="firstcol" />
            <SelectedRowStyle BackColor="#E3EEFC" />
        </asp:GridView>
            <br />
             <br />
            <br />
    </fieldset>
</asp:Content>

