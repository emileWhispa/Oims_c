<%@ page title="Orion|Customers" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="orion.ims.Customers, App_Web_e2euaq1s" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style5
        {
            text-align: left;
            width: 168px;
            height: 19px;
        }
        .auto-style6
        {
            width: 168px;
        }
        .auto-style7
        {
            width: 168px;
            height: 18px;
        }
        .auto-style14
        {
            width: 314px;
        }
        .auto-style15
        {
            width: 314px;
            height: 18px;
        }
        .auto-style16 {
            width: 314px;
            height: 19px;
        }
        .auto-style17 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:Panel ID="Panel1" runat="server" Height="381px">
             <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>

         <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" Width="100%">
             <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Data Entry">
                 <ContentTemplate>
           <table>
                <tr><td class="auto-style1">
                    &nbsp;</td>
                <td class="auto-style14">
                    <asp:Label ID="lblMessage" runat="server" BackColor="White" Font-Bold="True" Font-Italic="False" Font-Size="Medium" ForeColor="#99FF99"></asp:Label>
                </td>
            <tr>
                <td class="auto-style6">Customer Name:</td>
                <td class="auto-style14">
                    <asp:TextBox ID="txtcustname" runat="server" Height="22px" Width="264px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtcustname" />
                </td>
            </tr>
            <tr>
                <td class="auto-style6">Credit Limit:</td>
                <td class="auto-style14">
                    <asp:TextBox ID="txtcrdlimit" runat="server" Height="22px" Width="100px" TabIndex="1">0</asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtcrdlimit" />
                </td>
            </tr>
            <tr>
                <td class="auto-style7">Phone No:</td>
                <td class="auto-style15">
                    <asp:TextBox ID="txtphoneNo" runat="server" Height="22px" TabIndex="2" Width="133px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtphoneNo" CssClass="validation-error" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
                    <tr>
                        <td class="auto-style7">Additional Information:</td>
                        <td class="auto-style15">
                            <asp:TextBox ID="txtaddinfo" runat="server" Height="40px" TabIndex="3" TextMode="MultiLine" Width="291px"></asp:TextBox>
                        </td>
                    </tr>
            <tr>
                <td class="auto-style7">Address:</td>
                <td class="auto-style15">
                    <asp:TextBox ID="txtaddress" runat="server" Width="240px" Height="22px" TabIndex="4"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Distributor :</td>
                <td class="auto-style16">
                    <asp:CheckBoxList ID="chkDistributo" runat="server" Height="17px" RepeatDirection="Horizontal" Width="117px" ValidationGroup="check" TabIndex="5">
                        <asp:ListItem>Yes</asp:ListItem>
                    </asp:CheckBoxList>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:TextBox ID="txtcustomerId" runat="server" Visible="False"></asp:TextBox>
                </td>
                <td class="auto-style14">
                    </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    &nbsp;</td>
                <td class="auto-style14">
                    &nbsp;</td>
            </tr>
            <tr>
            <td class="auto-style6">&nbsp;</td>
                <td class="auto-style14"> 
                   
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-default btn-sm" OnClick="btnSave_Click" TabIndex="6" />
                        <ajaxToolkit:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server" ConfirmText="Do you want to save this customer?" Enabled="True" TargetControlID="btnSave">
                        </ajaxToolkit:ConfirmButtonExtender>
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-primary btn-sm" CausesValidation="False" OnClick="btnCancel_Click" />
                    
                        <br />
                    
                </td>
               
            </tr>
            <tr>
            <td class="auto-style6">&nbsp;</td>
                <td class="auto-style14"> 
                   
                        &nbsp;</td>
               
            </tr>
        </table>
    </ContentTemplate>
             </ajaxToolkit:TabPanel>
             <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Data View">
                 <ContentTemplate>
                      <table class="auto-style17">
                          <tr>
                              <td>Search by Name:</td>
                              <td>
                                  <asp:TextBox ID="txtSearchItem" runat="server" Height="23px" Width="344px"></asp:TextBox>
                                  <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Go" ValidationGroup="search" />
                              </td>
                          </tr>
                      </table>
                      <asp:GridView ID="gvData" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="grid" EmptyDataText="No data to display" OnPageIndexChanging="gvData_PageIndexChanging" OnRowCommand="gvData_RowCommand" PageSize="20" Width="100%">
                          <Columns>
                              <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                              <asp:BoundField DataField="CreditLimit" HeaderText="Credit Limit" />
                              <asp:BoundField DataField="AddInfo" HeaderText="Additional Information" />
                              <asp:BoundField DataField="Address" HeaderText="Address" />
                              <asp:BoundField DataField="PhoneNo" HeaderText="Phone No" />
                              <asp:BoundField DataField="Balance" HeaderText="Balance" />
                              <asp:BoundField DataField="Distributor" HeaderText="Distributor" />
                              <asp:TemplateField Visible="False">
                                  <ItemTemplate>
                                      <asp:HiddenField ID="hfKey" runat="server" EnableViewState="True" Value='<%# Bind("id") %>' />
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:ButtonField CommandName="edt" Text="Edit">
                              <ItemStyle HorizontalAlign="Center" />
                              </asp:ButtonField>
                              <asp:ButtonField CommandName="del" Text="Delete">
                              <ItemStyle HorizontalAlign="Center" />
                              </asp:ButtonField>
                              <asp:TemplateField Visible="False">
                                  <ItemTemplate>
                                      <asp:HiddenField ID="hfKey0" runat="server" EnableViewState="True" Value='<%# Bind("Id") %>' />
                                  </ItemTemplate>
                              </asp:TemplateField>
                          </Columns>
                          <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" Width="100%" />
                          <HeaderStyle CssClass="firstcol" />
                          <SelectedRowStyle BackColor="#E3EEFC" />
                      </asp:GridView>

                 </ContentTemplate>
             </ajaxToolkit:TabPanel>
         </ajaxToolkit:TabContainer>
     </asp:Panel>
     <br />
     <br />
   
</asp:Content>

