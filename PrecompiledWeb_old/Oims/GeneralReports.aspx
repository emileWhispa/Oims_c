<%@ page title="" language="C#" masterpagefile="~/Site.master" autoeventwireup="true" inherits="General_Reports, App_Web_e2euaq1s" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
        .auto-style1
        {
            width: 119px;
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <fieldset>
        <legend>REPORTS</legend>
        <br />
       <table>
           <tr>
               <td class="auto-style1">Products List</td>
               <td>
                   <asp:RadioButton ID="rdbtnproduct" runat="server" Height="20px" AutoPostBack="True" GroupName="rpt" />
               </td>
           </tr>
           <tr>
               <td class="auto-style1">Customers List</td>
               <td>
                   <asp:RadioButton ID="RdbtnCustomer" runat="server" AutoPostBack="True" GroupName="rpt" />
               </td>
           </tr>
           <tr>
               <td></td>
               <td></td>
           </tr>
           <tr>
               <td></td>
               <td></td>
           </tr>
           <tr><br />
               <td></td>
               <td>
                    <asp:Button ID="btnPrint" runat="server" CausesValidation="False" CssClass="btn btn-primary btn-sm" OnClick="btnPrint_Click" Text="Print PDF" ValidationGroup="Sale" Width="101px" />
                    </td>
           </tr>
       </table>
    </fieldset>

</asp:Content>

