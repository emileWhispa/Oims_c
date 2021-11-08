<%@ page title="OIMS|Production" language="C#" masterpagefile="~/Site.Master" autoeventwireup="true" inherits="orion.ims.Production, App_Web_z1fnw4ln" %>
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
        <legend>Production Details</legend>
        <table>
            <tr>
                <td class="style1">
                    Production Date:
                </td>
                <td>
                   <asp:TextBox runat="server" CssClass="textbox" ID="txtproductiondate" Width="100px"></asp:TextBox>
                      <asp:CalendarExtender ID="CalendarExtender2" Format="yyyy-MM-dd" runat="server" TargetControlID="txtproductiondate">
                   </asp:CalendarExtender>
                </td>
            </tr>
                 <tr>
                <td class="style1">
                    Remarks:
                </td>
                <td>
                    <asp:TextBox ID="txtremarks" runat="server" Height="31px" TextMode="MultiLine" Width="427px"></asp:TextBox>

                </td>
            </tr>
              <tr>
                <td>
                    <asp:Label ID="lbrBatchno" runat="server" Text="BatchNo" Visible="False"></asp:Label>
&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtbatchno" runat="server" Visible="False" ReadOnly="True"></asp:TextBox>

                </td>
                <td>
                    
                    &nbsp;</tr>
              <tr>
                <td>
                   <asp:TextBox ID="hfId" runat="server" Width="67px" Visible="False"></asp:TextBox></td>
                <td>
                </td>
                <td>
                    
            </tr>
            <tr>
            <td class="style1">
                <br />
                <br />
                </td>
            <td>
                <%--                    </div>--%>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm"
                            CausesValidation="false" OnClick="btnCancel_Click" />
<%--                    </div>--%>
                </td></tr>
        </table>
    </fieldset>
    <fieldset>
        <legend>Production Lists</legend>
        
        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="prod_date" HeaderText="Production Date" DataFormatString="{0:yyyy-MM-dd}" />
                <asp:BoundField DataField="Batchno" HeaderText="Batch No" />
                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                <%--<asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfKey" runat="server" EnableViewState="True" Value='<%# Bind("id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                  <asp:HyperLinkField DataNavigateUrlFields="Batchno" Text="Items" DataNavigateUrlFormatString="~/Productionitems.aspx?id={0}"
                    HeaderText="View Items" />
                <asp:ButtonField HeaderText="" Text="Edit" CommandName="edt" ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonField>
                <asp:ButtonField HeaderText="" Text="Delete" CommandName="del" ItemStyle-HorizontalAlign="Center" >
<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:ButtonField>
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
