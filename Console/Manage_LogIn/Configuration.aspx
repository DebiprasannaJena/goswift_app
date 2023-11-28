<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Configuration.aspx.cs" Inherits="AdminApp_UI_Console_Manage_LogIn" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc4" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="hdr" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Console::Configuration Master</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />
    <script src="../scripts/AjaxScript.js" type="text/javascript"></script>
    <script src="../jscript48/Validator.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function checkvalidation() {

            if (!blankFieldValidation('TabadminConfigMaster_TabCreateConfig_txtExp', 'Password Expiry Days')) {
                return false;
            }
            if (document.getElementById('TabadminConfigMaster_TabCreateConfig_txtExp').value == '0') {
                alert('Value Cannot be Zero');
                document.getElementById('TabadminConfigMaster_TabCreateConfig_txtExp').focus();
                return false;
             
            }
            if (!blankFieldValidation('TabadminConfigMaster_TabCreateConfig_txtAlert', 'Alert Period')) {
                return false;
            }
            if (document.getElementById('TabadminConfigMaster_TabCreateConfig_txtAlert').value == '0') {
                alert('Value Cannot be Zero');
                document.getElementById('TabadminConfigMaster_TabCreateConfig_txtAlert').focus();
                return false;

            }
            if (parseInt(document.getElementById('TabadminConfigMaster_TabCreateConfig_txtAlert').value) >= parseInt(document.getElementById('TabadminConfigMaster_TabCreateConfig_txtExp').value)) {
                alert('Alert Period should less than Password Expiry Days');
                document.getElementById('TabadminConfigMaster_TabCreateConfig_txtAlert').focus();
                return false;

            }
            if (!blankFieldValidation('TabadminConfigMaster_TabCreateConfig_txtUns', 'No of Unsuccessful Attempt')) {
                return false;
            }
            if (document.getElementById('TabadminConfigMaster_TabCreateConfig_txtUns').value == '0') {
                alert('Value Cannot be Zero');
                document.getElementById('TabadminConfigMaster_TabCreateConfig_txtUns').focus();
                return false;

            }
            if (!blankFieldValidation('TabadminConfigMaster_TabCreateConfig_txtLock', 'Permissible Time to Lock')) {
                return false;
            }
            if (document.getElementById('TabadminConfigMaster_TabCreateConfig_txtLock').value == '0') {
                alert('Value Cannot be Zero');
                document.getElementById('TabadminConfigMaster_TabCreateConfig_txtLock').focus();
                return false;

            }
            return true;
        }
       
        function dispConfm(btnId) {
            if (btnId.value == "Save") {
                if (!checkvalidation()) {
                    return false;
                }
                else {
                    return confirm("Are You Sure Want To Save ?")
                }
            }
            else if (btnId.value == "Reset") {
                return confirm("Are You Sure Want To Reset ?")
            }
        }
        
    </script>
    <script src="../scripts/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script src="../scripts/jquery.tablesorter.min.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" runat="server">
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
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <uc4:Navigation ID="Navigation1" runat="server" />
                                    <cc1:TabContainer runat="server" ID="TabadminConfigMaster" Width="100%" ActiveTabIndex="0"
                                        CssClass="ajax__tab_yuitabview-theme" OnActiveTabChanged="TabadminConfigMaster_ActiveTabChanged"
                                        AutoPostBack="true">
                                        <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateConfig" TabIndex="0">
                                            <HeaderTemplate>
                                                CREATE
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <div class="nodata" align="center">
                                                </div>
                                                <div class="mandatory">
                                                    (<font color="#FF0000">*</font> indicates mandatory field)</div>
                                                <div class="addTable">
                                                    <table border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                Password Expiry Days
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtExp" runat="server" MaxLength="2" Width="150px"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender1" ValidChars=".() " TargetControlID="txtExp"
                                                                    FilterType="Numbers" Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                                &nbsp; <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                            <td>
                                                                Alert Period For Expiry (In Days)</td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtAlert" runat="server" MaxLength="2" Width="150px"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender runat="server" ID="FilteredTextBoxExtender2" ValidChars=".() " TargetControlID="txtAlert"
                                                                    FilterType="Numbers" Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                                &nbsp; <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               No of Unsuccessful Attempt
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtUns" runat="server" MaxLength="2" Width="150px"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender runat="server" ID="designaiton" ValidChars=".() " TargetControlID="txtUns"
                                                                    FilterType="Numbers" Enabled="True">
                                                                </cc1:FilteredTextBoxExtender>
                                                                &nbsp; <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Permissible Time to Lock (In hr)
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLock" runat="server" MaxLength="2" Width="150px"></asp:TextBox>
                                                                &nbsp; <font color="#FF0000">*</font>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextAlias" runat="server" Enabled="True"
                                                                    FilterType="Numbers" TargetControlID="txtLock"
                                                                    ValidChars=". ">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                       
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" OnClientClick="return dispConfm(this);"
                                                                    Text="Save" />
                                                                &nbsp;&nbsp;
                                                                <asp:Button ID="btncancel" runat="server" OnClientClick="return dispConfm(this);"
                                                                    OnClick="btncancel_Click" Text="Reset" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        
                                    </cc1:TabContainer>
                                </ContentTemplate>
                            </asp:UpdatePanel>
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
