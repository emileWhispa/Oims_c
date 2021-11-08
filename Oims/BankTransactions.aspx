<%@ Page Title="Orion|BankTransactions" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="BankTransactions.aspx.cs" Inherits="BankTransactions" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style4
        {
            width: 234px;
        }
        .auto-style5
        {
            width: 135px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">

        <ajaxToolkit:TabContainer ID="TabContainer1" runat="server"  Height="430px" Width="100%" ScrollBars="Horizontal" VerticalStripWidth="20px" ActiveTabIndex="1">
            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="Data Entry">
                
                <ContentTemplate>
                    <table>
                        <tr>
                            <td class="auto-style5">Transaction Mode: </td>
                            <td class="auto-style4">
                                <asp:DropDownList ID="ddlTransatype" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlTransatype" CssClass="validation-error" ErrorMessage="*" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">Bank :</td>
                            <td class="auto-style4">
                                <asp:DropDownList ID="ddlbank" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbank_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">Bank Account :</td>
                            <td class="auto-style4">
                                <asp:DropDownList ID="ddlaccounts" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">Amount :</td>
                            <td class="auto-style4">
                                <asp:TextBox ID="txtamount" runat="server" Height="22px" Width="132px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtamount" CssClass="validation-error" ErrorMessage="*" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">Transaction Date :</td>
                            <td class="auto-style4">
                                <asp:TextBox ID="txtTransactionDate" runat="server" CssClass="textbox" Width="179px" Height="19px"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd MMM yyyy" TargetControlID ="txtTransactionDate" BehaviorID="_content_CalendarExtender2" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTransactionDate" CssClass="validation-error" ErrorMessage="*" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">Transaction Type :</td>
                            <td class="auto-style4">
                                <asp:DropDownList ID="ddlposting" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">Descrition:</td>
                            <td class="auto-style4">
                                <asp:TextBox ID="txtaddinfo" runat="server" TextMode="MultiLine" Height="36px" Width="207px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtaddinfo" CssClass="validation-error" ErrorMessage="*" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">Document No:</td>
                            <td class="auto-style4">
                                <asp:TextBox ID="txtdocno" runat="server" Height="21px" Width="126px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtdocno" CssClass="validation-error" ErrorMessage="*" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">Balance:</td>
                            <td class="auto-style4">
                                <asp:TextBox ID="txtbalance" runat="server" Height="22px" Width="97px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtbalance" CssClass="validation-error" ErrorMessage="*" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style5">
                                <asp:TextBox ID="txtbankTransId" runat="server" Visible="False" Width="67px"></asp:TextBox>
                            </td>
                            <td class="auto-style4">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style5"></td>
                            <td class="auto-style4">
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnSave_Click" Text="Save" />
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
            OnRowCommand="gvData_RowCommand" CssClass="grid" PageSize="20">
            <Columns>
                <asp:BoundField DataField="Account.AccountNo" HeaderText="Account No" />
                <asp:BoundField DataField="TransactionType.Description" HeaderText="Transaction Mode" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                <asp:BoundField DataField="TransactionDate" HeaderText="Transaction Date" />
                <asp:BoundField DataField="PostingType.Description" HeaderText="Transacton Type" />
                <asp:BoundField DataField="AdditionalInfo" HeaderText="Description" />
                <asp:BoundField DataField="DocRef" HeaderText="Document No" />
                <asp:BoundField DataField="Balance" HeaderText="Balance" />
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

