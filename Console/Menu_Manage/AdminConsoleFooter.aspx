<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Menu_Manage_AdminConsoleFooter"
    CodeBehind="AdminConsoleFooter.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <script type="text/javascript">
function openNewWindows()
{
var url='<%=strUrl%>';
window.open(url);
}
    </script>

    <link type="text/css" rel="stylesheet" href="../style/default.css" />
</head>
<body>
    <table border="0" cellpadding="0" cellspacing="0" class="topMenuBgColor" width="100%"
        bgcolor="#004f86">
        <tr>
            <td align="right" class="whiteText" height="25px">
                Visit :<asp:HyperLink ID="hypVisit" onclick="openNewWindows()" Style="cursor: pointer;
                    text-decoration: underline;" runat="server" ForeColor="White">[hypVisit]</asp:HyperLink>
            </td>
        </tr>
    </table>
</body>
</html>
