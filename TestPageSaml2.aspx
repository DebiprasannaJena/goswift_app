<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestPageSaml2.aspx.cs" Inherits="TestPageSaml2" %>

<!DOCTYPE html>

<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container wrapper">
        <div class="panel-body">
            <div class="row">
                 <table width="100%">
                     <tr>
                         <td colspan="2">
                             <h1 style="color: Green;"> SAML pre-check API</h1>
                             <hr />
                         </td>
                     </tr>
                     <tr>
                         <td><label style="color: orange;">PAN :</label></td><td><asp:TextBox runat="server" ID="txtpan"></asp:TextBox></td>
                     </tr>
                     <tr>
                         <td><label style="color: orange;">CIN :</label></td><td><asp:TextBox runat="server" ID="txtcin"></asp:TextBox></td>
                     </tr>
                     <tr>
                         <td><label style="color: orange;">Entity Type :</label></td><td>
                             <asp:DropDownList runat= "server" id="seltype">
                                 <asp:ListItem  Value="">select</asp:ListItem>
                                 <asp:ListItem  Value="1">Incorporated Company</asp:ListItem>
                                 <asp:ListItem  Value="2">Limited Liability Partnership</asp:ListItem>
                                 <asp:ListItem  Value="3">Sole Proprietor</asp:ListItem>
                                 <asp:ListItem  Value="5">Trust</asp:ListItem>
                                 <asp:ListItem  Value="6">Cooperative Society</asp:ListItem>
                                 <asp:ListItem  Value="7">Hindu Undivided Family (HUF)</asp:ListItem>
                                 <asp:ListItem  Value="8">Joint Venture</asp:ListItem>
                                  <asp:ListItem  Value="9">Partnership Firm</asp:ListItem>
                                  <asp:ListItem  Value="10">Foreign Investor</asp:ListItem>
                                 <asp:ListItem  Value="11">Others</asp:ListItem>
                             </asp:DropDownList>
                                                               </td>
                     </tr>
                     <tr>
                         <td><label style="color: orange;">Email :</label></td><td><asp:TextBox runat="server" ID="txtemail"></asp:TextBox></td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             <asp:Button ID="btnsaml" runat="server" Text="SAML Integration" style="color:blue" OnClick="btnsaml_Click"/>
                            
                         </td>
                         
                         
                     </tr>
                     <tr>
                         <td colspan="2">
                             Status: <asp:Label ID="Lblstatus" runat="server"  style="color: orangered;" ></asp:Label>
                         </td>
                     </tr>
                     <tr>
                         <td colspan="2">
                             Content Result: <asp:Label ID="lblcontent" runat="server" style="color: orangered;"></asp:Label>
                         </td>
                     </tr>
                 </table>
            </div>
        </div>
    </div>


        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
            <asp:ListItem>Yes</asp:ListItem>
            <asp:ListItem>No</asp:ListItem>
        </asp:RadioButtonList>

         <asp:Button ID="Button2" runat="server" Text="Precheck API " style="color:blue" OnClick="Button2_Click"/>
        <asp:Label ID="Label2" runat="server"  style="color: orangered;" ></asp:Label>
    </form>
</body>
</html>
