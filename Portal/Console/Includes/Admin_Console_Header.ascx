<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin_Console_Header.ascx.cs"
    Inherits="Admin_Console_Header" %>
<link href="../style/default.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" language="javascript">
       
        function GoLgout()
        {
            top.location.href='../../login.aspx'
        }
</script>

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
                                    <span><asp:Label ID="lblDynamicHeader" runat="server" Text="Label"></asp:Label></span>
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
                                            <li><span> <asp:LinkButton ID="lnkHome" runat="server" Text="Home" OnClick="lnkHome_Click" /> </span>
                                                <li style="margin-right: 0px;"><span>
                                                    <asp:LinkButton ID="lnkLogOut" runat="server" Text="Logout" OnClick="lnkLogOut_Click" /></span></li>
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
