<%@ page title="Orion|Expenses" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="Expenses, App_Web_e2euaq1s" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style5
        {
            width: 140px;
        }
        .auto-style6
        {
            width: 258px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server"  Height="430px" Width="100%" ScrollBars="Horizontal" VerticalStripWidth="20px" ActiveTabIndex="0">
            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Data Entry">
                
                <ContentTemplate>
           <table>
               <tr><td class="auto-style5">
                    &nbsp;</td>
                <td class="auto-style6">
                    <asp:Label ID="lblMessage" runat="server" BackColor="White" Font-Bold="True" Font-Italic="True" Font-Size="Medium" ForeColor="#99FF99"></asp:Label>
                </td>
               </tr>
                            <tr>
                                <td class="auto-style5">Expense Group : </td>
                                <td class="auto-style6">
                                    <asp:DropDownList ID="ddlexpgrp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlexpgrp_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Expense Category :</td>
                                <td class="auto-style6">
                                    <asp:DropDownList ID="ddlexpcat" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Amount :</td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtamount" runat="server"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtamount" CssClass="validation-error" ErrorMessage="*" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Currency :</td>
                                <td class="auto-style6">
                                    <asp:DropDownList ID="ddlcurrency" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Exchange Rate :</td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtexrate" runat="server" ></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtexrate" CssClass="validation-error" ErrorMessage="*" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">ExpenseDate :</td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtexpDate" runat="server" Height="20px" Width="202px" ></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender" runat="server" TargetControlID="txtexpDate" Enabled="True" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtexpDate" CssClass="validation-error" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Description :</td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtdescr" runat="server" TextMode="MultiLine" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">Vatable ?</td>
                                <td class="auto-style6">&nbsp;&nbsp;
                                    <asp:CheckBox ID="chkbxvatable" runat="server" AutoPostBack="True" OnCheckedChanged="chkbxYes_CheckedChanged" />
                                    &nbsp;</td>
               </tr>
                            <tr>
                                <td class="auto-style5">
                                    <asp:Label ID="lbVatRate" runat="server" style="text-align: right" Text="VAT Rate" Visible="False"></asp:Label>
                                </td>
                                <td class="auto-style6">
                                    <asp:TextBox ID="txtvatrate" runat="server" Visible="False" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style5">
                                    <asp:TextBox ID="txtexpenseId" runat="server"  Visible="False" ></asp:TextBox>
                                </td>
                                </tr>
                            <tr>
                                <td class="auto-style5">&nbsp;</td>
                                <td class="auto-style6">
                                    <br />
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnSave_Click" Text="Save" />
                                    <ajaxToolkit:ConfirmButtonExtender ID="btnSave_ConfirmButtonExtender" runat="server" ConfirmText="Do you want to save this expense?" Enabled="True" TargetControlID="btnSave">
                                    </ajaxToolkit:ConfirmButtonExtender>
                                    <asp:Button ID="btnCancel" runat="server" CausesValidation="False" CssClass="btn btn-primary btn-sm" OnClick="btnCancel_Click" Text="Cancel" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                </ContentTemplate>
             
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="Data View">
 <ContentTemplate>
      <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                 <asp:BoundField DataField="expensecategory.ExpenseName" HeaderText="Expense Category" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                <asp:BoundField DataField="currency.CurrencyName" HeaderText="Currency Name" />
                                <asp:BoundField DataField="ExchangeRate" HeaderText="Exchange Rate" />
                                <asp:BoundField DataField="ExpenseDate" HeaderText="Expense Date" />
                                <asp:BoundField DataField="Description" HeaderText=" Description" />
                                <asp:BoundField DataField="user.UserName" HeaderText=" Maker" />
                                <asp:BoundField DataField="vatrate" HeaderText=" VAT Rate" />
                               <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfKey" runat="server" EnableViewState="True" Value='<%# Bind("id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:ButtonField Text="Edit" CommandName="edt" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
                <asp:ButtonField Text="Delete" CommandName="del" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:ButtonField>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:HiddenField ID="hfKey0" runat="server" EnableViewState="True" Value='<%# Bind("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                VerticalAlign="Middle" Width="100%" />
            <HeaderStyle CssClass="firstcol" />
            <SelectedRowStyle BackColor="#E3EEFC" />
        </asp:GridView>

                 </ContentTemplate>
            </ajaxToolkit:TabPanel>
            
        </ajaxToolkit:TabContainer>
<%--        <ajaxToolkit:ResizableControlExtender ID="TabContainer1_ResizableControlExtender" runat="server" BehaviorID="TabContainer1_ResizableControlExtender" TargetControlID="TabContainer1" />--%>
    
</asp:Content>

