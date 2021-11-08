<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="ExpenseReport, App_Web_e2euaq1s" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
        .auto-style2 {
            height: 30px;
            text-align: right;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
          <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
    <table style="width:100%">
        <tr>
            <td class="auto-style2">User Name:</td>
            <td class="auto-style3">
                <asp:DropDownList ID="ddlUser" runat="server" Height="24px" Width="243px">
                </asp:DropDownList>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="All"></asp:Label>
&nbsp;<asp:CheckBox ID="chkbxAll" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">Start Date :</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtstartdate" runat="server" Height="20px" Width="140px"></asp:TextBox>
                <cc1:CalendarExtender ID="txtstartdate_CalendarExtender" runat="server" BehaviorID="txtstartdate_CalendarExtender" TargetControlID="txtstartdate" Format="yyyy-MM-dd">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtstartdate" />
            </td>
        </tr>
        <tr>
            <td class="auto-style2">End Date :</td>
            <td>
                <asp:TextBox ID="txtendDate" runat="server" Height="20px" Width="140px"></asp:TextBox>
                <cc1:CalendarExtender ID="txtendDate_CalendarExtender" runat="server" BehaviorID="txtendDate_CalendarExtender" TargetControlID="txtendDate" Format="yyyy-MM-dd">
                </cc1:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtendDate" />
            </td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>
                <asp:Button ID="btnDisplay" runat="server" Text="Display" CssClass="btn btn-primary btn-sm" CausesValidation="False" OnClick="btnDisplay_Click"  />
            </td>
        </tr>
        <tr>
            <td class="auto-style1">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style1" colspan="2">
    <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
                Width="100%" CssClass="grid">
        <Columns>
            <asp:BoundField DataField="ExpenseDate" HeaderText="Expense Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField DataField="SageLedgerNo" HeaderText="Sage Ledger" />
            <asp:BoundField DataField="ExpenseName" HeaderText="Ledger Name" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="Amount" HeaderText="Amount" />
            <asp:BoundField DataField="PostedBy" HeaderText="Posted By" />
        </Columns>
        <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                    BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                    VerticalAlign="Middle" Width="100%" />
        <HeaderStyle CssClass="firstcol" />
        <SelectedRowStyle BackColor="#E3EEFC" />
    </asp:GridView>
            </td>
        </tr>
        <tr>
            <td class="auto-style1" colspan="2">&nbsp;
                <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="btn btn-primary btn-sm" CausesValidation="False" OnClick="btnPrint_Click" Width="87px" Visible="False"  />
            </td>
        </tr>
    </table>
</asp:Content>

