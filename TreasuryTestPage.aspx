<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TreasuryTestPage.aspx.cs" Inherits="TreasuryTestPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Button ID="btnSave" runat="server" Text="Check eService Payment Status" 
            onclick="btnSave_Click"/>
    <asp:Button ID="btnSave0" runat="server" Text="Check PEAL Payment Status" 
            onclick="btnPeal_Click"/>
        <table class="style1">
            <tr>
                <td width="150">
                    <strong>Service Status</strong></td>
                <td>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <strong>Peal Status</strong></td>
                <td>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
