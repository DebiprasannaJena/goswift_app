<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Manage_Hierarchy_AssignAdmin"
    EnableEventValidation="false" CodeBehind="AssignAdmin.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc4" %>
<%@ Register Src="~/Console/FillHierarchy.ascx" TagName="getUser1" TagPrefix="uc2" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="hdr" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Console::Assign Admin</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

   <script src="../scripts/ajax.js" type="text/javascript"></script>

    <script src="../scripts/PopulateHiearchyajax.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">
        function getlevelvalue() {
            document.getElementById("HdlhirLevel").value = document.getElementById("drpUserlist").value;
        }
        function checkvalidation() {
            if (!DropDownValidation('ddlLocationAdmin', 'Location')) {
                return false;
            }
            if (!DropDownValidation('getUsers2_sdrplayers0', 'Location')) {
                return false;
            }
            if (!DropDownValidation('drpUserlist', 'User')) {
                return false;
            }
        }
      
        function GetSelectedDdl(ddlId) {
            
            document.getElementById("hidSelectedValue").value = "";
            document.getElementById("hidSelectedValue").value = ddlId.value;
        }
    </script>

</head>
<body>
    <form id="frmuser" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server">
    </asp:ScriptManager>
    <div id="MainArea">
        <hdr:Header ID="header1" runat="server" />
        <div id="MidArea">
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                    <td valign="top" id="LeftPannel">
                        <lm:LeftMenu ID="lm1" runat="server" />
                    </td>
                    <td valign="top" class="midRightArea">
                        <div id="container">
                            <uc4:Navigation ID="Navigation1" runat="server" />
                            <div class="addTable" style="border-top: solid 1px #F0F0F0; margin-top: 5px">
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormBorder">
                                    <tr>
                                        <td align="center" style="height: 19px">
                                            <asp:Label ID="Label2" runat="server" Width="663px" Font-Bold="True" Font-Size="Small"
                                                ForeColor="#C00000"></asp:Label>
                                            <asp:HiddenField ID="HidShowVal" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <table border="0" cellpadding="0" cellspacing="0" class="FormBorder">
                                                        <tr>
                                                            <td width="150">
                                                                Administrator For
                                                            </td>
                                                            <td class="level">
                                                                :
                                                            </td>
                                                            <td class="levellight" style="padding-left: 8px;">
                                                                <asp:DropDownList ID="ddlLocationAdmin" AutoPostBack="true" runat="server" Width="185px"
                                                                    OnSelectedIndexChanged="ddlLocationAdmin_SelectedIndexChanged">
                                                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                </asp:DropDownList>
                                                                <font color="#FF0000">*</font>
                                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr id="trCurAdmin" runat="server" style="display: none">
                                                            <td>
                                                                Current Administrator
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td style="padding-left: 8px;">
                                                                <asp:Label runat="server" ID="lbladminuser" Font-Bold="true"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    <div id="tbluser" runat="server" visible="false">
                                                        <uc2:getUser1 ID="getUsers2" runat="server" />
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="150" class="level" id="Td2" style="padding-left: 2px;">
                                                                    Users
                                                                </td>
                                                                <td class="level" id="Td3">
                                                                    :
                                                                </td>
                                                                <td class="levellight" style="padding-left: 8px;">
                                                                    <asp:DropDownList ID="drpUserlist" runat="server" Width="185px">
                                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <font color="#FF0000">*</font>
                                                                    <asp:HiddenField ID="HdlhirLevel" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr id="show">
                                                                <td>
                                                                    <asp:HiddenField ID="hidSelectedValue" runat="server" />
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td class="levellight" style="padding-left: 8px;">
                                                                    <asp:Button runat="server" ID="btnSubmit" Text="Submit" OnClick="btnSubmit_Click"
                                                                        OnClientClick="return checkvalidation();" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--#include file="../Includes/footer.aspx" -->
    </div>
    </form>
</body>
</html>
