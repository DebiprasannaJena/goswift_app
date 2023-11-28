<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Menu_Manage_AdminConsoleHeader"
    CodeBehind="AdminConsoleHeader.aspx.cs" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link type="text/css" rel="stylesheet" href="../style/default.css" />

    <script type="text/javascript" language="javascript">
         
        function aa()
        {
            top.location.href = '../../AdminConsoleLogin.aspx'
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="topMenuBgColor">
        <tr>
            <td height="60x" valign="top">
                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                    <tr>
                        <td width="406" height="40" valign="middle">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="10">
                                        &nbsp;
                                    </td>
                                    <td class="welcomeMsg">
                                        <span>Ipicol Portal</span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td valign="bottom">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td valign="top">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="20">
                                                    <div align="right">
                                                        <span class="blBoldText">WELCOME
                                                            <%=Session["adminstat"].ToString().ToUpper()%>
                                                            ADMINISTRATOR &nbsp;</span></div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td height="10">
                                                    <img src="../images/spacer.gif" width="1" height="1" alt="" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td width="10" valign="top">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <div id="tabs1">
                                            <ul>
                                                <%--<li><span><a id="A1" href="../../NewHome.aspx?arg=1" target="_top">Home</a> </span></li>--%>
                                                <li><span><a id="A1" href="../../Dashboard/Default.aspx" target="_top">Home</a> </span></li>
                                                    <li style="margin-right: 0px;"><span>
                                                        <asp:LinkButton ID="logout" runat="server" Text="Logout" OnClick="logout_Click" /></span></li>
                                            </ul>
                                        </div>
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td height="10" valign="bottom">
                                        <img src="../images/spacer.gif" width="1" height="1" alt="" />
                                    </td>
                                    <td valign="bottom">
                                        <img src="../images/spacer.gif" width="1" height="1" alt="" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="10" bgcolor="#f1932b">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
