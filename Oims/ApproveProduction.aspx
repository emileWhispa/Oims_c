<%@ Page Title="OIMS|Production" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeFile="ApproveProduction.aspx.cs" Inherits="orion.ims.ApproveProduction" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style1
        {
            width: 164px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
     <fieldset>
        <legend>Production Lists 
                   <asp:TextBox ID="hfId" runat="server" Width="67px" Visible="False"></asp:TextBox></td>
                <asp:TextBox ID="txtId" runat="server" Visible="False"></asp:TextBox><br />
            <asp:Label ID="lblError" runat ="server" ForeColor="Red"></asp:Label>
                </legend>
        
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="prod_date" HeaderText="Production Date" />
                <asp:BoundField DataField="Batchno" HeaderText="Batch No" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfKey" runat="server" EnableViewState="True" Value='<%# Bind("id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:ButtonField HeaderText="" Text="Items" CommandName="view" ItemStyle-HorizontalAlign="Center" />
            </Columns>
            <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                VerticalAlign="Middle" Width="100%" />
            <HeaderStyle CssClass="firstcol" />
            <SelectedRowStyle BackColor="#E3EEFC" />
        </asp:GridView>
    </fieldset>
    <fieldset><legend>Production Items</legend></fieldset>
    <fieldset>
        <asp:GridView ID="gvitems" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvitems_PageIndexChanging" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="Product.Productname" HeaderText="Product" />
                <asp:BoundField DataField="units" HeaderText="Units" />
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfKey1" runat="server" EnableViewState="True" Value='<%# Bind("Id") %>' />
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
                        <asp:Button ID="btnApprove"  runat="server" Text="Approve Production" CssClass="btn btn-primary btn-sm" OnClick="btnApprove_Click" Width="135px" />

                </td>
                <td>&nbsp;&nbsp;&nbsp;&nbsp; </td>
                <td>
                        <asp:Button ID="btnDecline"  runat="server" Text="Decline Production" CssClass="btn btn-primary btn-sm" OnClick="btnDecline_Click" Width="119px" ValidationGroup="decline" />

                </td>
                <td>

                    &nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtcomments" runat="server" Height="33px" TextMode="MultiLine" Width="342px" ValidationGroup="decline"></asp:TextBox>
                    <asp:TextBoxWatermarkExtender ID="txtcomments_TextBoxWatermarkExtender" runat="server" TargetControlID="txtcomments" ViewStateMode="Enabled" WatermarkText="Add Comments" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="txtcomments" ValidationGroup="decline" />

                </td>
                </tr>
        </table>
    </fieldset>
</asp:Content>
