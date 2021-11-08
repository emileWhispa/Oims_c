﻿<%@ Page Title="Oims|POS Orders" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeFile="POSorders.aspx.cs" Inherits="orion.ims.POSorders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <fieldset>
        <legend>POS Orders</legend>
        <table>
            <tr>
                <td>
                    Pos Centre:
                </td>
                <td>
                    <asp:DropDownList ID="ddlPos" runat="server">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*"
                        CssClass="validation-error" ControlToValidate="ddlPos" />
                </td>
            </tr>
            <tr>
                <td>
                    Order Date
                </td>
                <td>
                    <asp:TextBox runat="server" CssClass="textbox" ID="txtorderdate" Width="100px"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" Format="dd MMM yyyy" runat="server"
                        TargetControlID="txtorderdate">
                    </asp:CalendarExtender>
                    
                    </td>
            </tr>
            
            <tr>
                <td>
                    Remarks:
                </td>
                <td>
                    <asp:TextBox ID="txtdescr" runat="server" TextMode="SingleLine" Width="216px"></asp:TextBox>
                     
                </td>
            </tr>
             <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:TextBox ID="hfId" runat="server" Width="67px" Visible="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                </td>
                <td>
                    <div class="btn-group">
                        <asp:Button ID="btnnew" CausesValidation="false" runat="server" Text="Add New" CssClass="btn btn-primary btn-sm"
                            OnClick="btnnew_Click" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                            CausesValidation="false" OnClick="btnCancel_Click" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset>
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="Pose.posName" HeaderText="POS Centre" />
                <asp:BoundField DataField="orderdate" HeaderText="Order Date" />
                <asp:BoundField DataField="OrderNo" HeaderText="Order No" />
                <asp:BoundField DataField="addinfo" HeaderText="Remarks" />
                 <asp:HyperLinkField DataNavigateUrlFields="Orderno" Text="Items" DataNavigateUrlFormatString="~/PosOderItems.aspx?id={0}"
                    HeaderText="View Items" />
                <asp:ButtonField HeaderText="" Text="Edit" CommandName="sel" ItemStyle-HorizontalAlign="Center" />
                <asp:ButtonField HeaderText="" Text="Delete" CommandName="del" ItemStyle-HorizontalAlign="Center" />
                 <asp:ButtonField HeaderText="" Text="Submit" CommandName="sub" ItemStyle-HorizontalAlign="Center" />
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
