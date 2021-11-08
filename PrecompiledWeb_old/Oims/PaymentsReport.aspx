<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="PaymentsReport, App_Web_e2euaq1s" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1
        {
            width: 152px;
        }
        .auto-style2
        {
            width: 152px;
            height: 27px;
        }
        .auto-style3
        {
            height: 27px;
        }
        .auto-style4
        {
            width: 152px;
            height: 18px;
        }
        .auto-style5
        {
            height: 18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
      <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
    <fieldset>
        <legend>Periodical Payments</legend>
        <table>
            <tr>
                <td class="auto-style2">
                    POS Name:</td>
                <td class="auto-style3">

                    <asp:DropDownList ID="ddlPOS" runat="server" Height="22px" Width="149px">
                    </asp:DropDownList>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="All"></asp:Label>
&nbsp;<asp:CheckBox ID="chkbxAll" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="auto-style4">

                    Start Date :</td>
                <td class="auto-style5">

                    <asp:TextBox ID="txtstartdate" runat="server" Height="20px" Width="140px"></asp:TextBox>

                    <cc1:CalendarExtender ID="txtstartdate_CalendarExtender" runat="server" BehaviorID="txtstartdate_CalendarExtender" TargetControlID="txtstartdate" Format="yyyy-MM-dd">
                    </cc1:CalendarExtender>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtstartdate" />

                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    End Date :</td>
                <td>

                    <asp:TextBox ID="txtendDate" runat="server" Height="20px" Width="140px"></asp:TextBox>

                    <cc1:CalendarExtender ID="txtendDate_CalendarExtender" runat="server" BehaviorID="txtendDate_CalendarExtender" TargetControlID="txtendDate" Format="yyyy-MM-dd">
                    </cc1:CalendarExtender>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtendDate" />

                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    Payment Mode:</td>
                <td>

                    <asp:DropDownList ID="ddlpaymode" runat="server"  Height="22px" Width="107px">
                    </asp:DropDownList>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; All
                    <asp:CheckBox ID="chkpymnt" runat="server" />

                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    Customer Name:</td>
                <td>

                                        <asp:TextBox ID="txtSearchByName" runat="server" Height="22px" Width="200px"></asp:TextBox>

                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    &nbsp;</td>
                <td>

                    <asp:CheckBox ID="chkDeposit" runat="server" Text="Deposits Only" />

                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    &nbsp;</td>
                <td>

                        <asp:Button ID="btnDisplay" runat="server" Text="Display" CssClass="btn btn-primary btn-sm" CausesValidation="False" OnClick="btnDisplay_Click"  />
                    
                        </td>
            </tr>
            </table>
        <br />
    </fieldset>
    <fieldset>
         <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging"
            OnRowCommand="gvData_RowCommand" CssClass="grid">
            <Columns>
                <asp:BoundField DataField="PaymentDate" HeaderText="Date" />
                <asp:BoundField DataField="PaymentName" HeaderText="Payment Mode" />
                <asp:BoundField DataField="PaymentType" HeaderText="Payment Type" />
                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                <asp:BoundField DataField="Document No" HeaderText="Document No" />
                <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                <asp:BoundField DataField="POSName" HeaderText="POS NAME" />
        
            </Columns>
            <EmptyDataRowStyle BackColor="#0099CC" BorderColor="#FF3300" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Italic="True" ForeColor="White" HorizontalAlign="Center"
                VerticalAlign="Middle" Width="100%" />
            <HeaderStyle CssClass="firstcol" />
            <SelectedRowStyle BackColor="#E3EEFC" />
        </asp:GridView>
    </fieldset>
    <table>
        <tr>
            <td>
                        <asp:Button ID="btnAction" runat="server" Text="PDF Report" CssClass="btn btn-primary btn-sm" CausesValidation="False" OnClick="btnAction_Click"  />

            </td>
        </tr>
    </table>
</asp:Content>

