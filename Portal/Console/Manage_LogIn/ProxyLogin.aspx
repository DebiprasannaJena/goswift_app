<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProxyLogin.aspx.cs" Inherits="AdminApp_UI.Console.Manage_LogIn.ProxyLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
      <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />
   <script src="../scripts/1.4.2_jquery.min.js" type="text/javascript"></script>

    <script src="../scripts/1.8.1_jquery-ui.min.js" type="text/javascript"></script>

    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <link href="../style/default.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/ajax.js" type="text/javascript"></script>

    <script src="../scripts/PopulateHiearchyajax.js" type="text/javascript"></script>

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        function GetUserList() {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ProxyLogin.aspx/BindUserList",
                        data: "{'strUserName':'" + document.getElementById('TabadminConfigMaster_TabCreateConfig_txtOrg').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.FullName,
                                    val: item.GetID
                                }
                            }));
                            document.getElementById('TabadminConfigMaster_TabCreateConfig_txtOrg').focus();
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                select: function (event, ui) {
                    document.getElementById('TabadminConfigMaster_TabCreateConfig_txtOrg').value = ui.item.value;
                    document.getElementById("TabadminConfigMaster_TabCreateConfig_hdnOrg").value = ui.item.val
                }
            });
        }
        function GetUserList2() {
            $(".autosuggest").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "ProxyLogin.aspx/BindUserList",
                        data: "{'strUserName':'" + document.getElementById('TabadminConfigMaster_TabCreateConfig_txtProxy').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.FullName,
                                    val: item.GetID
                                }
                            }));
                            document.getElementById('TabadminConfigMaster_TabCreateConfig_txtProxy').focus();
                        },
                        error: function (result) {
                            alert("Error");
                        }
                    });
                },
                select: function (event, ui) {
                    document.getElementById('TabadminConfigMaster_TabCreateConfig_txtProxy').value = ui.item.value;
                    document.getElementById("TabadminConfigMaster_TabCreateConfig_hdnProxy").value = ui.item.val
                }
            });
        }
        function checkvalidation() {
            if (!blankFieldValidation('TabadminConfigMaster_TabCreateConfig_txtOrg', 'Original User')) {

                return false;
            }
            if (document.getElementById('TabadminConfigMaster_TabCreateConfig_hdnOrg').value == '') {
                alert('Please Enter Valid User !');
                document.getElementById('TabadminConfigMaster_TabCreateConfig_txtOrg').focus();
                return false;
            }
            if (!blankFieldValidation('TabadminConfigMaster_TabCreateConfig_txtProxy', 'Proxy User')) {

                return false;
            }
            if (document.getElementById('TabadminConfigMaster_TabCreateConfig_hdnProxy').value == '') {
                alert('Please Enter Valid User !');
                document.getElementById('TabadminConfigMaster_TabCreateConfig_txtProxy').focus();
                return false;
            }
            if (parseInt(document.getElementById('TabadminConfigMaster_TabCreateConfig_hdnOrg').value) == parseInt(document.getElementById('TabadminConfigMaster_TabCreateConfig_hdnProxy').value)) {
                alert('Original User cannot be same as Proxy User');
                return false;

            }
            if (!blankFieldValidation('TabadminConfigMaster_TabCreateConfig_txtPass', 'Password')) {
                return false;
            }
            if (!blankFieldValidation('TabadminConfigMaster_TabCreateConfig_txtDTMFrom', 'From Date')) {
                return false;
            }
            if (!blankFieldValidation('TabadminConfigMaster_TabCreateConfig_txtDTMTo', 'To Date')) {
                return false;
            }
            if (!DateValidation()) {
                return false;
            }
            return true;
        }
        function DateValidation() {
            var monthsArr = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
            var objDOB = document.getElementById("TabadminConfigMaster_TabCreateConfig_txtDTMFrom").value;
            var dtDOB = objDOB.split('-');
            for (var i = 0; i < monthsArr.length; i++) {
                if (dtDOB[1] == monthsArr[i]) {
                    dtDOB[1] = i + 1;
                    break;
                }
            }
            objDOB = dtDOB[1] + '/' + dtDOB[0] + '/' + dtDOB[2];
            var objDOJ = document.getElementById("TabadminConfigMaster_TabCreateConfig_txtDTMTo").value;
            var dtDOJ = objDOJ.split('-');
            for (var i = 0; i < monthsArr.length; i++) {
                if (dtDOJ[1] == monthsArr[i]) {
                    dtDOJ[1] = i + 1;
                    break;
                }
            }
            objDOJ = dtDOJ[1] + '/' + dtDOJ[0] + '/' + dtDOJ[2];
            var dateDob = new Date(objDOB);
            var dateDoj = new Date(objDOJ);
            var currentDate = new Date();
            currentDate.setDate(currentDate.getDate() - 1);
            if (dateDob <= currentDate) {
                alert("From Date should be greater than or equals CurrentDate");
                return false;
            }
            if (dateDob > dateDoj) {
                alert("From Date should be less than To Date");
                return false;
            }
            return true;

        }
        function dispConfm(btnId) {
            if (btnId.value == "Submit") {
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
        function checkSelect() {
            if (!ConfirmCheck('form1')) {

                return false;
            }
            for (var i = 0; i < document.forms[0].elements.length; i++) {
                if (document.forms[0].elements[i].checked == true) {
                    return confirm('Are You Sure To Delete?');

                }
            }

            return false;
        }
    </script>
  
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
                                                                Original User
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtOrg" runat="server" Width="175px" class="autosuggest" 
                                                                    Font-Bold="True" runat="server" onkeyup="GetUserList();"></asp:TextBox> &nbsp;<font color="#FF0000">*</font>
                                                                <asp:HiddenField ID="hdnOrg" runat="server" />
                                                            </td>
                                                        </tr>
                                                          <tr>
                                                            <td>
                                                              Proxy User
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                               <asp:TextBox ID="txtProxy" runat="server" Width="175px" class="autosuggest" 
                                                                    Font-Bold="True" runat="server" onkeyup="GetUserList2();"></asp:TextBox> &nbsp; <font color="#FF0000">*</font>
                                                                <asp:HiddenField ID="hdnProxy" runat="server" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Password
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtPass" runat="server" Width="175px" Font-Bold="True"></asp:TextBox>&nbsp;<font color="#FF0000">&nbsp; 
                                                                *</font>
                                                                
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               From Date
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                              <asp:TextBox ID="txtDTMFrom" runat="server"  class="inputCalendar" Width="175px"></asp:TextBox>
                                                                <cc1:CalendarExtender
                                                                    ID="txtDTMFrom_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy"
                                                                    TargetControlID="txtDTMFrom" CssClass="cal_Theme1">
                                                                </cc1:CalendarExtender> &nbsp; <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                               To Date
                                                            </td>
                                                            <td>
                                                                :
                                                            </td>
                                                            <td>
                                                              <asp:TextBox ID="txtDTMTo" runat="server" Width="175px" class="inputCalendar"></asp:TextBox>
                                                                <cc1:CalendarExtender
                                                                    ID="txtDTMTo_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy"
                                                                    TargetControlID="txtDTMTo" CssClass="cal_Theme1">
                                                                </cc1:CalendarExtender> &nbsp; <font color="#FF0000">*</font>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>                                                               
                                                             <asp:Button runat="server" Text="Generate" ID="btnGen" OnClick="btnGen_Click"></asp:Button>&nbsp;&nbsp;
                                                                <asp:Button ID="btnsave" runat="server" OnClick="btnsave_Click" OnClientClick="return dispConfm(this);"
                                                                    Text="Submit" />
                                                                &nbsp;&nbsp;
                                                                <asp:Button ID="btncancel" runat="server" OnClientClick="return dispConfm(this);"
                                                                    OnClick="btncancel_Click" Text="Reset" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ContentTemplate>
                                        </cc1:TabPanel>
                                        <cc1:TabPanel runat="server" HeaderText="VIEW" ID="TabViewProxy" TabIndex="1">
                                            <HeaderTemplate>
                                                VIEW
                                            </HeaderTemplate>
                                            <ContentTemplate>
                                                <div style="text-align: right; height: 20px">
                                                    <table border="0" align="right">
                                                        <tr>
                                                            <td>
                                                                <asp:LinkButton ID="LnkbtnAllin" runat="server" OnClick="LnkbtnAllin_Click1" Text="All"
                                                                    Visible="False"></asp:LinkButton>
                                                            </td>
                                                            <td>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblpage" runat="server" Visible="False"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="viewTable">
                                                    <asp:GridView ID="GridDesignation" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                        CellPadding="0" DataKeyNames="GetID" EmptyDataText="No Records Found"
                                                        ItemStyle-VerticalAlign="Top" OnPageIndexChanging="GridDesignation_PageIndexChanging"
                                                        OnRowDataBound="GridDesignation_RowDataBound" PagerStyle-Mode="NumericPages"
                                                        PagerStyle-PageButtonCount="10">
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <input name="cbAll" onclick="SelectAll(cbAll,'GridDesignation','form1')" type="checkbox"
                                                                        value="cbAll" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll,'GridDesignation','form1')" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="25px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Sl No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSlno" runat="server" Text=""></asp:Label>
                                                                
                                                                </ItemTemplate>
                                                                <ItemStyle Width="40px" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField HeaderText="Original User" DataField="UserID" />
                                                            <asp:BoundField HeaderText="Proxy User" DataField="UserName" />
                                                            <asp:BoundField HeaderText="From Date" DataField="DivName"  />
                                                            <asp:BoundField HeaderText="To Date" DataField="DivisionName"  />
                                                        </Columns>
                                                        <HeaderStyle Font-Bold="True" />
                                                        <PagerStyle CssClass="paging" />
                                                    </asp:GridView>
                                                </div>
                                                <div class="deletebtn" style="padding-left: 10px">
                                                    <asp:Button ID="btninDel" runat="server" Text="Delete" Width="65px" OnClick="btninDel_Click"
                                                        OnClientClick="return checkSelect();" />
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
