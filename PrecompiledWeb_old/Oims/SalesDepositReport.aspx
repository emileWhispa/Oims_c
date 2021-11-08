<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="SalesDepositReport, App_Web_e2euaq1s" %>
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
              width: 478px;
          }
        .auto-style4
        {
            width: 152px;
            height: 18px;
        }
        .auto-style5
        {
              height: 18px;
              width: 478px;
          }
          .auto-style6
          {
              width: 152px;
              height: 10px;
          }
          .auto-style7
          {
              height: 10px;
              width: 478px;
          }
          .auto-style8 {
              width: 478px;
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
     <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
    <fieldset>
        <legend>Sale Deposits Report</legend>
        <table>
            <tr>
                <td class="auto-style2">
                    POS Name:</td>
                <td class="auto-style3">

                    <asp:DropDownList ID="ddlPOS" runat="server" Height="22px" Width="149px" AutoPostBack="True" OnSelectedIndexChanged="ddlPOS_SelectedIndexChanged">
                    </asp:DropDownList>

                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label1" runat="server" Text="All"></asp:Label>
&nbsp;<asp:CheckBox ID="chkbxAllPOS" runat="server" AutoPostBack="True" OnCheckedChanged="chkbxAll_CheckedChanged" />

                </td>
            </tr>
            <tr>
                <td class="auto-style4">

                    Teller:</td>
                <td class="auto-style5">

                                <asp:DropDownList runat="server" ID="cmbTeller" Height="24px" Width="249px" OnSelectedIndexChanged="cmbTeller_SelectedIndexChanged"></asp:DropDownList>

                                &nbsp;&nbsp;
                    <asp:Label ID="Label2" runat="server" Text="All"></asp:Label>
&nbsp;<asp:CheckBox ID="chkbxAllTeller" runat="server" AutoPostBack="True" OnCheckedChanged="chkbxAllTeller_CheckedChanged" />

                </td>
            </tr>
            <tr>
                <td class="auto-style4">

                    Start Date :</td>
                <td class="auto-style5">

                    <asp:TextBox ID="txtstartdate" runat="server" Height="20px" Width="140px"></asp:TextBox>

                    <cc1:CalendarExtender ID="txtstartdate_CalendarExtender" runat="server" BehaviorID="txtstartdate_CalendarExtender" TargetControlID="txtstartdate" Format="yyyy-MM-dd">
                    </cc1:CalendarExtender>

                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    End Date :</td>
                <td class="auto-style8">

                    <asp:TextBox ID="txtendDate" runat="server" Height="20px" Width="140px"></asp:TextBox>

                    <cc1:CalendarExtender ID="txtendDate_CalendarExtender" runat="server" BehaviorID="txtendDate_CalendarExtender" TargetControlID="txtendDate" Format="yyyy-MM-dd">
                    </cc1:CalendarExtender>

                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" CssClass="validation-error" ControlToValidate="txtendDate" />

                </td>
            </tr>
            <tr>
                <td class="auto-style1">

                    &nbsp;</td>
                <td class="auto-style8">

                        <asp:Button ID="btnDisplay" runat="server" Text="Display" CssClass="btn btn-primary btn-sm" CausesValidation="False" OnClick="btnDisplay_Click"  />
                    
                        </td>
            </tr>
            </table>
        <br />
    </fieldset>
    <fieldset>
         <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="False" EmptyDataText="No data to display"
            Width="100%" AllowPaging="True" OnPageIndexChanging="gvData_PageIndexChanging" CssClass="grid" PageSize="20">
            <Columns>
                <asp:BoundField DataField="SalesRef" HeaderText="Sales REF" />
                <asp:BoundField DataField="QuoteDate" HeaderText="Date" />
                <asp:BoundField DataField="Deposit" HeaderText="Deposit Amount" />
                <asp:BoundField DataField="CustomerName" HeaderText="Customer" />
                <asp:BoundField DataField="POSName" HeaderText="POS NAME" />
                <asp:BoundField DataField="FullNames" HeaderText="Served by" />
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
                        <asp:Button ID="btnAction" runat="server" Text="PDF Report" CssClass="btn btn-primary btn-sm" CausesValidation="False" OnClick="btnAction_Click" Visible="False"  />

            </td>
        </tr>
    </table>
</asp:Content>

