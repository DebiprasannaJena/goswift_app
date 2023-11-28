<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="SetPermission.aspx.cs"
    Inherits="KwantifyPortalV3._2_App.Admin.Manage_User.SetPermission" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Console/FillUserHierarchy.ascx" TagName="getUsers" TagPrefix="uc2" %>
<%@ Register Src="~/Console/FillHierarchy.ascx" TagName="listUsers" TagPrefix="uc1" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc4" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="hdr" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Console::SetPermissiona</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.1)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.1)" />
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.1/themes/base/jquery-ui.css"
        rel="stylesheet" type="text/css" />

    <script src="" type="text/javascript"></script>

    <script src="../scripts/1.4.2_jquery.min.js" type="text/javascript"></script>

    <script src="../scripts/1.8.1_jquery-ui.min.js" type="text/javascript"></script>

    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/ProgressBar/jquery.blockUI.js" type="text/javascript"></script>

    <script src="../scripts/ProgressBar/common.js" type="text/javascript"></script>

    <link href="../style/default.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/ajax.js" type="text/javascript"></script>

    <script src="../scripts/PopulateHiearchyajax.js" type="text/javascript"></script>

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            jQuery(".divcontent").hide();
            jQuery(".divheading").click(function () {
                jQuery(".divcontent").slideToggle(300);
            });
        });

        function hideall() {
            for (var i = 0; i < document.all.length; i++) {

                if (document.all[i].id.substring(9) == '_gvwplink' || document.all[i].id.substring(8) == '_gvwplink') {
                    document.all[i].style.display = "None";
                }
            }
        }
        function show(ctlname, ctlname1) {
            //alert(ctlname);
            //alert(ctlname1);
            document.getElementById(ctlname).style.display = "";

            return false;
        }
        function CheckInnerGridCheckbox(ctlname, ctlname1) {
            debugger;
            var rcount = document.getElementById(ctlname).rows.length - 1;
            if (document.getElementById(ctlname + '_chkall') != null) {
                if (document.getElementById(ctlname1).checked == true) {
                    document.getElementById(ctlname + '_chkall').checked = true;
                }
                else {
                    document.getElementById(ctlname + '_chkall').checked = false;
                }

                if (document.getElementById(ctlname + '_chkall').checked == true) {
                    for (var i = 0; i <= rcount - 1; i++) {
                        document.getElementById(ctlname + '_chk_' + i).checked = true;
                        document.getElementById(ctlname + '_optview_' + i).checked = true;
                    }
                }
                else {
                    for (var i = 0; i <= rcount - 1; i++) {
                        document.getElementById(ctlname + '_chk_' + i).checked = false;
                        document.getElementById(ctlname + '_optview_' + i).checked = false;
                        document.getElementById(ctlname + '_optadd_' + i).checked = false;
                        document.getElementById(ctlname + '_optmanage_' + i).checked = false;
                    }
                }
            }
            else {
                if (document.getElementById(ctlname1).checked == true) {
                    document.getElementById(ctlname + '_ctl01_chkall').checked = true;
                }
                else {
                    document.getElementById(ctlname + '_ctl01_chkall').checked = false;
                }

                if (document.getElementById(ctlname + '_ctl01_chkall').checked == true) {
                    for (var i = 2; i <= rcount + 1; i++) {
                        if (i < 10) {
                            document.getElementById(ctlname + '_ctl0' + i + '_chk').checked = true;
                            document.getElementById(ctlname + '_ctl0' + i + '_optview').checked = true;
                        }
                        else {
                            document.getElementById(ctlname + '_ctl' + i + '_chk').checked = true;
                            document.getElementById(ctlname + '_ctl' + i + '_optview').checked = true;
                        }
                    }
                }
                else {
                    for (var i = 2; i <= rcount + 1; i++) {
                        if (i < 10) {
                            document.getElementById(ctlname + '_ctl0' + i + '_chk').checked = false;
                            document.getElementById(ctlname + '_ctl0' + i + '_optview').checked = false;
                            document.getElementById(ctlname + '_ctl0' + i + '_optadd').checked = false;
                            document.getElementById(ctlname + '_ctl0' + i + '_optmanage').checked = false;
                        }
                        else {
                            document.getElementById(ctlname + '_ctl' + i + '_chk').checked = false;
                            document.getElementById(ctlname + '_ctl' + i + '_optview').checked = false;
                            document.getElementById(ctlname + '_ctl' + i + '_optadd').checked = false;
                            document.getElementById(ctlname + '_ctl' + i + '_optmanage').checked = false;
                        }
                    }
                }
            }

        }
        function Check(ctlname) {
            document.getElementById(ctlname).checked == true;
            return false;
        }
        function hide(ctlname) {
            document.getElementById(ctlname).style.display = "None";
            return false;
        }
        function hideGrid(ctlname) {
            document.getElementById(ctlname).style.display = "None";
            return false;
        }
        function checkall(rcount, name, name1) {
            debugger;
            //rcount++;
            if (document.getElementById(name + '_chkall') != null) {
                var checkall = document.getElementById(name + '_chkall');
                var lnlChk = name.substring(0, 10)
                if (checkall.checked == true) {
                    document.getElementById(name1).checked = true;
                    for (var i = 0; i <= rcount - 1; i++) {

                        document.getElementById(name + '_chk_' + i).checked = true;
                        document.getElementById(name + '_optview_' + i).checked = true;
                    }
                }
                else {
                    document.getElementById(name1).checked = false;
                    for (var i = 0; i <= rcount - 1; i++) {
                        document.getElementById(name + '_chk_' + i).checked = false;
                        document.getElementById(name + '_optview_' + i).checked = false;
                        document.getElementById(name + '_optadd_' + i).checked = false;
                        document.getElementById(name + '_optmanage_' + i).checked = false;
                    }
                }
            }
            else {
                var checkall = document.getElementById(name + '_ctl01_chkall');
                var lnlChk = name.substring(0, 10)
                if (checkall.checked == true) {
                    document.getElementById(lnlChk + 'lnk').checked = true;
                    for (var i = 2; i <= rcount; i++) {
                        if (i < 10) {
                            document.getElementById(name + '_ctl0' + i + '_chk').checked = true;
                            document.getElementById(name + '_ctl0' + i + '_optview').checked = true;
                        }
                        else {
                            document.getElementById(name + '_ctl' + i + '_chk').checked = true;
                            document.getElementById(name + '_ctl' + i + '_optview').checked = true;
                        }
                    }
                }
                else {
                    document.getElementById(lnlChk + 'lnk').checked = false;
                    for (var i = 2; i <= rcount; i++) {
                        if (i < 10) {
                            document.getElementById(name + '_ctl0' + i + '_chk').checked = false;
                            document.getElementById(name + '_ctl0' + i + '_optview').checked = false;
                            document.getElementById(name + '_ctl0' + i + '_optadd').checked = false;
                            document.getElementById(name + '_ctl0' + i + '_optmanage').checked = false;
                        }
                        else {
                            document.getElementById(name + '_ctl' + i + '_chk').checked = false;
                            document.getElementById(name + '_ctl' + i + '_optview').checked = false;
                            document.getElementById(name + '_ctl' + i + '_optadd').checked = false;
                            document.getElementById(name + '_ctl' + i + '_optmanage').checked = false;
                        }
                    }
                }
            }

            //var lnlChk=name.substring(0, 10)

        }
        function uncheckheader(ctlname1) {
            debugger;
            var ctlname = ctlname1.substring(0, 14)
            var ctlname2 = ctlname1.substring(0, 22)
            var lnlChk = ctlname1.substring(0, 10)
            var id = ctlname1.substring(19, 20)
            var id2 = ctlname.substring(13, 14)
            if (id == '_') {
                ctlname = ctlname1.substring(0, 15)
                var id = ctlname1.substring(20, 21)
                id2 = ctlname.substring(13, 15)
            }
            if (document.getElementById(ctlname + '_chkall') != null) {
                if (document.getElementById(ctlname + '_chkall').checked == true)
                // document.getElementById(ctlname + '_chkall').checked = false;
                    if (document.getElementById(ctlname1).checked == false) {
                        document.getElementById(ctlname + '_optview_' + id).checked = false;
                        document.getElementById(ctlname + '_optadd_' + id).checked = false;
                        document.getElementById(ctlname + '_optmanage_' + id).checked = false;

                    }
                if (document.getElementById(ctlname1).checked == true) {
                    document.getElementById(ctlname + '_optview_' + id).checked = true;
                    document.getElementById(ctlname + '_chkall').checked = true;
                    document.getElementById("grd" + '_lnk_' + id2).checked = true;
                }
                else {
                    var flag = 0;
                    var rowCnt = document.getElementById(ctlname).rows.length - 1;


                    if (document.getElementById(ctlname + '_chk_' + id).checked == true) {
                        flag = 1;
                    }

                    if (flag == 0) {

                        document.getElementById(ctlname + '_chk_' + id).checked = false;
                        document.getElementById("grd" + '_lnk_' + id).checked = false;
                    }
                    else {
                        document.getElementById(ctlname + '_chkall').checked = true;
                        document.getElementById(ctlname + '_chk_' + id).checked = true;

                    }
                }
            }
            else {
                if (document.getElementById(ctlname2 + '01_chkall').checked == true)
                    document.getElementById(ctlname2 + '01_chkall').checked = false;
                if (document.getElementById(ctlname1).checked == false) {
                    document.getElementById(ctlname1.replace('_chk', '_optadd')).checked = false;
                    document.getElementById(ctlname1.replace('_chk', '_optview')).checked = false;
                    document.getElementById(ctlname1.replace('_chk', '_optmanage')).checked = false;
                }
                if (document.getElementById(ctlname1).checked == true) {
                    document.getElementById(ctlname1.replace('_chk', '_optview')).checked = true;
                    document.getElementById(ctlname2 + '01_chkall').checked = true;
                    document.getElementById(lnlChk + 'lnk').checked = true;
                }
                else {
                    var flag = 0;
                    var rowCnt = document.getElementById(innergrid).rows.length - 1;
                    for (var i = 2; i <= rowCnt + 1; i++) {
                        if (i < 10) {
                            if (document.getElementById(innergrid + '_ctl0' + i + '_chk').checked == true) {
                                flag = 1;
                            }
                        }
                        else {
                            if (document.getElementById(innergrid + '_ctl' + i + '_chk').checked == true) {
                                flag = 1;
                            }
                        }
                    }
                    if (flag == 0) {
                        document.getElementById(ctlname2 + '01_chkall').checked = false;
                        document.getElementById(lnlChk + 'lnk').checked = false;
                    }
                    else {
                        document.getElementById(ctlname2 + '01_chkall').checked = true;
                        document.getElementById(lnlChk + 'lnk').checked = true;
                    }
                }
            }

        }

        function makeCheck(rbtCtrlId, actionCode) {
            if (actionCode == "V") {
                document.getElementById(rbtCtrlId.replace('_optview', '_chk')).checked = true;
            }
            if (actionCode == "A") {
                document.getElementById(rbtCtrlId.replace('_optadd', '_chk')).checked = true;
            }
            if (actionCode == "M") {
                document.getElementById(rbtCtrlId.replace('_optmanage', '_chk')).checked = true;
            }
        }
       
    </script>

    <script language="javascript" type="text/javascript">
        function showChk() {
            var objfrm = document.frmuser;
            var Stat;
            if (document.getElementById("chkUser").checked == true) {
                document.getElementById("show").style.display = "";
            }
            else {
                document.getElementById("show").style.display = "none";
            }
        }
        function GetSelected(ctrlId) {
            var opt;
            document.getElementById("hidSelectedValue").value = "";
            for (i = 0; i < ctrlId.length; i++) {
                opt = ctrlId.options[i];
                if ((opt.selected) && (opt.value != 0)) {
                    var selected = opt.value;
                    document.getElementById("hidSelectedValue").value = document.getElementById("hidSelectedValue").value + ',' + selected;
                }
            }
        }
        function GetSelectedDdl(ddlId, hidId) {
            document.getElementById(hidId).value = "";
            document.getElementById(hidId).value = ddlId.value;
        }   
                         
    </script>

    <style type="text/css">
        .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
        .modalPopup
        {
            background-color: #ffffff;
            padding: 3px;
            width: 200px;
            height: auto;
        }
        .divheading
        {
        }
        .divcontent
        {
        }
        .AutoExtender
        {
            font-family: Verdana, Helvetica, sans-serif;
            font-size: small;
            font-weight: normal;
            border: solid 1px #006699;
            margin: 0px;
            padding: 1px;
            height: 70px;
            overflow: auto;
            background-color: #FFFFFF;
        }
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        .WaterMarkedTextBox
        {
            height: 16px;
            width: 200px;
            padding: 2px 2 2 2px;
            border: 1px solid #BEBEBE;
            background-color: #F0F8FF;
            font-family: Verdana;
            color: gray;
            font-style: italic;
        }
        .overlay
        {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px; /* background-color: #aaa;
            filter: alpha(opacity=50);
            opacity: 0.5;*/
        }
        .overlayContent
        {
            z-index: 99;
            margin: 250px auto;
            width: 90px;
            height: 90px;
        }
        .overlayContent h2
        {
            font-size: 18px;
            font-weight: bold;
            color: #000;
        }
    </style>
</head>
<body>
    <form id="frmuser" runat="server" enctype="multipart/form-data">
    <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="true" EnablePartialRendering="true"
        runat="server">
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
                            <uc4:Navigation ID="Navigation2" runat="server" />
                            <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" CssClass="ajax__tab_yuitabview-theme">
                                <cc1:TabPanel runat="server" HeaderText="Set Permission" ID="TabAddUser">
                                    <HeaderTemplate>
                                        Set Permission
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                    </ContentTemplate>
                                </cc1:TabPanel>
                            </cc1:TabContainer>
                            <div class="addTable">
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td valign="top">
                                                        <uc1:listUsers ID="getUsers2" runat="server" />
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="150px">
                                                                    User
                                                                </td>
                                                                <td>
                                                                    <strong>:</strong>
                                                                </td>
                                                                <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                                                                    <ContentTemplate>
                                                                        <td style="padding-left: 8px;">
                                                                            <asp:DropDownList ID="ddlUsers" onchange="GetSelectedDdl(this,'hidSelectedValue');"
                                                                                Width="185px" runat="server">
                                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <font style="color: red">*</font>
                                                                        </td>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td style="padding-left: 8px;">
                                                                    <asp:Button ID="btnShowUser" OnClientClick="return validateDdl('ddlUsers')" runat="server"
                                                                        OnClick="btnShowUser_Click" Text="Show" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td valign="top">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                              <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                                                                <ContentTemplate>
                                                            <tr>
                                                                <td width="70" rowspan="3" align="center" valign="top" style="font-size: 14px; font-weight: bold;">
                                                                    OR
                                                                </td>
                                                                <td width="100">
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSearch" class="autosuggest" Font-Bold="true" runat="server" onkeyup="GetUserList();"
                                                                        Width="200px" MaxLength="100"></asp:TextBox>
                                                                    <cc1:TextBoxWatermarkExtender ID="txtSearch_TextBoxWatermarkExtender" runat="server"
                                                                        Enabled="True" TargetControlID="txtSearch" WatermarkText="Type User Full Name"
                                                                        WatermarkCssClass="WaterMarkedTextBox">
                                                                    </cc1:TextBoxWatermarkExtender>
                                                                    <%--<cc1:AutoCompleteExtender ID="txtVehlcleNo_AutoCompleteExtender" runat="server" EnableCaching="true"
                                                                                Enabled="True" ServiceMethod="GetUsersName" TargetControlID="txtSearch" MinimumPrefixLength="1"
                                                                                CompletionSetCount="12" CompletionInterval="50" FirstRowSelected="true" CompletionListCssClass="AutoExtender"
                                                                                CompletionListItemCssClass="AutoExtenderList" OnClientItemSelected="BindPermissionGrid"
                                                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight">
                                                                            </cc1:AutoCompleteExtender>--%>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtSearch"
                                                                        FilterType="Custom,UppercaseLetters,LowercaseLetters,Numbers" FilterMode="ValidChars"
                                                                        ValidChars=" " Enabled="True">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <%--Commented By Dilip Tripathy on dated 12-Nov-2013
                                                                    <tr>
                                                                        <td>
                                                                            User List
                                                                        </td>
                                                                        <td>
                                                                            <strong>:</strong>&nbsp;<asp:DropDownList ID="drpUserSearch" onchange="GetSelectedDdl(this,'hidSelectedValue');"
                                                                                runat="server" AppendDataBoundItems="True" AutoPostBack="false" Width="200px">
                                                                                <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                            <font style="color: Red">*</font> &nbsp;
                                                                          
                                                                        </td>
                                                                    </tr>--%>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;<asp:Button ID="btnShowSearchUser" Style="display: none" runat="server"
                                                                        OnClick="btnShowSearchUser_Click" Text="Show" />
                                                                    <asp:HiddenField ID="hidUserId" runat="server" />
                                                                </td>
                                                            </tr>
                                                             </ContentTemplate>                                                                
                                                            </asp:UpdatePanel>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:CheckBox ID="chkUser" class="divheading" runat="server" />
                                            <b>Permission From Another User</b>
                                            <div class="divcontent">
                                                <uc2:getUsers ID="getUsers" runat="server" Width="138px" />
                                                <table border="0" cellpadding="0" cellspacing="0">
                                                    <tr>
                                                        <td width="150">
                                                            Users
                                                        </td>
                                                        <td>
                                                            <strong>:</strong>
                                                        </td>
                                                        <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                                            <ContentTemplate>
                                                                <td style="padding-left: 8px;">
                                                                    <asp:DropDownList ID="drpCopyUserList" onchange="GetSelectedDdl(this,'hidCopyUser');"
                                                                        runat="server" Width="185px" Height="22px">
                                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:HiddenField ID="hidCopyUser" runat="server" />
                                                                </td>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </tr>
                                                    <tr>
                                                        <td class="style11">
                                                            &nbsp;
                                                        </td>
                                                        <td class="style12">
                                                            &nbsp;
                                                        </td>
                                                        <td style="padding-left: 8px;">
                                                            <asp:Button ID="btnShowCopyUser" OnClientClick="return validateDdl('drpCopyUserList')"
                                                                OnClick="btnShowCopyUser_Click" runat="server" Text="Show" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                <ContentTemplate>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td>
                                                                <asp:GridView runat="server" ID="grd" DataKeyNames="GlinkId" AutoGenerateColumns="False"
                                                                    Width="100%" OnRowDataBound="grd_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <asp:Label runat="server" ID="lblheader"></asp:Label>
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                                                    <tr align="left">
                                                                                        <td align="left" style="width: 150px">
                                                                                            <asp:CheckBox runat="server" ID="lnk" Text='<%# DataBinder.Eval(Container.DataItem,"GLinkName") %>' />
                                                                                        </td>
                                                                                        <td style="width: 60px" align="left">
                                                                                            <asp:Button ID="btnshow" runat="server" Text="Show" CommandName="show" />
                                                                                        </td>
                                                                                        <td style="width: 60px" align="left">
                                                                                            <asp:Button ID="btnhide" runat="server" Text="Hide" CommandName="hide" Visible="true" />
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:Label ID="lblglink" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"GlinkId") %>'
                                                                                                Visible="false"></asp:Label>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <asp:GridView ID="gvwplink" Style="display: none;" runat="server" AutoGenerateColumns="False"
                                                                                    Width="592px" OnRowDataBound="gvwplink_RowDataBound" DataKeyNames="PlinkId" OnRowCreated="gvwplink_RowCreated"
                                                                                    CellPadding="0" CellSpacing="0">
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:CheckBox ID="chk" runat="server" />
                                                                                            </ItemTemplate>
                                                                                            <HeaderTemplate>
                                                                                                <asp:CheckBox ID="chkall" runat="server" />
                                                                                            </HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="blslno" Text='<%#slno%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <HeaderTemplate>
                                                                                                Sl No
                                                                                            </HeaderTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="PlinkId" HeaderText="Primary Link Id" />
                                                                                        <asp:BoundField DataField="PLinkName" HeaderText="Primary Link" />
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:RadioButton ID="optview" runat="server" GroupName="permission" Text=" View" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:RadioButton ID="optadd" runat="server" GroupName="permission" Text="Add" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:RadioButton ID="optmanage" runat="server" GroupName="permission" Text="Manage" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                                            <HeaderStyle HorizontalAlign="Right" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <EmptyDataTemplate>
                                                                        <asp:Label ID="Label1" runat="server" Font-Names="Arial Black" ForeColor="#FF8080"
                                                                            Text="NO DATA EXISTS"></asp:Label>
                                                                    </EmptyDataTemplate>
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Button ID="btnsubmit" runat="server" OnClientClick="return dispConfirm(this);"
                                                                    OnClick="btnsubmit_Click" Text="Save" Width="50px" />
                                                                <asp:Button ID="btnReset" OnClientClick="return dispConfirm(this);" runat="server"
                                                                    Text="Reset" OnClick="btnReset_Click" />
                                                                <asp:HiddenField ID="hidSelectedValue" runat="server" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="btnShowUser" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnShowCopyUser" EventName="Click" />
                                                    <asp:AsyncPostBackTrigger ControlID="btnShowSearchUser" EventName="Click" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:HiddenField ID="HiddenField4" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                                <div style="display: none" id="divBlock">
                                    <asp:Image runat="server" ID="Image1" ImageUrl="~/Console/images/Loading.gif" AlternateText="Loading.." />
                                </div>
                            </div>
                           
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <!--#include file="../Includes/footer.aspx" -->
    </div>
    </form>

    <script language="javascript" type="text/javascript">
        function BindPermissionGrid() {
            debugger;
            if (document.getElementById('txtSearch').value != '') {
            }
            else {
                alert('Please select user name.');
            }
        }       
    </script>

</body>
</html>

<script language="javascript" type="text/javascript">
    function assignsection() {
        if (document.getElementById("selSec").selectedIndex != 0) {
            document.getElementById("hiddensec").value = document.getElementById("selSec").selectedIndex;
        }
    }
    function validator() {
        objForm = document.form1;
        if (document.getElementById("txtSearch").value == "") {
            alert("Search can not be left blanck! ");
            document.getElementById("txtSearch").focus();
            return false;
        }
        if (!WhiteSpaceValidation1st('txtSearch')) {
            return false;
        }
        if (!chkSingleQuote('txtSearch')) {
            return false;
        }
        if (!SpecialCharcheck('txtSearch')) {
            return false;
        }
        if (!MaxlengthValidation('txtSearch', 'Subject', 100)) {
            return false;
        }
        if (!CharValidationWithSpace('txtSearch', 'Full Name')) {
            return false;
        }
    }
    function validation2() {

        if (document.getElementById("ddlUsers").selectedIndex > 0) {
            return true;
        }
        else {
            if (document.getElementById("hidUserId").value != "") {
                return true;
            }
            else {
                alert("Please  select a User");
                return false;
            }
        }
    }
    function dispConfirm(btnId) {
        if (btnId.value == "Save") {
            if (!validation2()) {
                return false;
            }
            else {
                return confirm('Do you want to set permission ?');
            }
        }
        else {
            return confirm('Do you want to reset ?');
        }
    }
    function validateDdl(ddlId) {
        if (document.getElementById(ddlId).selectedIndex > 0) {
            return true;
        }
        else {
            alert("Please  select a User");
            document.getElementById(ddlId).focus();
            return false;
        }
    }
    function validation() {
        objForm = document.form1;
        // if (document.getElementById("drpUserSearch").selectedIndex == 0) {
        if (document.getElementById("hidUserId").selectedIndex == "") {
            if (document.getElementById("drpCopyUserList").selectedIndex != 0) {
                if (document.getElementById("LBUser").selectedIndex == -1 || document.getElementById("LBUser").selectedIndex == 0) {
                    alert("Please  select a User");
                    document.getElementById("LBUser").focus();
                    return false;
                }
            }
            else if (document.getElementById("LBUser").options.length != 1) {
                if (document.getElementById("drpCopyUserList").selectedIndex == -1 || document.getElementById("drpCopyUserList").selectedIndex == 0) {
                    if (document.getElementById("chkUser").checked == true) {
                        alert("Please select a User to copy permission");
                        document.getElementById("drpCopyUserList").focus();
                        return false;
                    }
                    else {
                        alert("Please check 'Copy Permission from other user' to select a user");
                        document.getElementById("chkUser").focus();
                        return false;
                    }
                }
            }
            else {
                alert("Please select a User");
                document.getElementById("txtSearch").focus(); // Before it was drpUserSearch instead of txtSearch
                return false;
            }
        }
    }
    function searchvalidator() {
        if (document.getElementById("txtCopyUser").value == "") {
            alert("Search can not be left blanck! ");
            document.getElementById("txtCopyUser").focus();
            return false;
        }
        if (!WhiteSpaceValidation1st('txtCopyUser')) {
            return false;
        }
        if (!chkSingleQuote('txtCopyUser')) {
            return false;
        }
        if (!SpecialCharcheck('txtCopyUser')) {
            return false;
        }
        if (!MaxlengthValidation('txtCopyUser', 'Subject', 100)) {
            return false;
        }
        if (!CharValidationWithSpace('txtCopyUser', 'Full Name')) {
            return false;
        }
    }
    function getload() {
        if (document.getElementById("hidSelGrUser").value != "") {
            var hidenvalue = document.getElementById("hidSelGrUser").value;
            getNumber();
            var dropdown2 = document.forms(0).elements("DropDownList2");
            var rCount = dropdown2.options.length - 1;
            for (var i = 0; i <= rCount; i++) {
                if (i == document.getElementById("hidSelGrUser").value) {
                    dropdown2.focus();
                    document.getElementById("DropDownList2").selectedIndex = hidenvalue;
                }
            }
        }
    }
    function getsection() {
        if (document.getElementById("hiddensec").value != "") {
            var hidenvalue = document.getElementById("hiddensec").value;
            getSection();
            var dropdown2 = document.forms(0).elements("selSec");
            var rCount = dropdown2.options.length - 1;
            for (var i = 0; i <= rCount; i++) {
                if (i == document.getElementById("hiddensec").value) {
                    dropdown2.focus();
                    document.getElementById("selSec").selectedIndex = hidenvalue;
                }
            }
        }
    }
    function onLoadHide() {
        document.getElementById("LTR1").style.display = "";
        document.getElementById("LTR2").style.display = "";
    }
    function CallWMtoGetUsersIdName() {
        var txtVal = document.getElementById('<%=txtSearch.ClientID%>').value;
        if (txtVal != null || txtVal != '') {
            var userId = '<%=Session["UserId"]%>';
            var param = userId + "," + txtVal;
            PageMethods.GetUsers(param, GetUsersIdName);
        }
        else {
            PageMethods.GetUsers("0", GetUsersIdName);
        }
    }

    function GetUsersIdName(returnString) {
        try {
            // var ddlUser = Here assign the id of drpUserSearch  
            var userIdNm = returnString.split(',');
            while (ddlUser.options.length > 0) {
                ddlUser.options.remove(0);
            }
            for (var i = 0; i < userIdNm.length; i = i + 2) {
                var opt = document.createElement("option");
                opt.text = userIdNm[i];
                opt.value = userIdNm[i + 1];
                ddlUser.options.add(opt);
            }
        }
        catch (e) {
            alert(e);
        }
    }
    function GetUserList() {
        $(".autosuggest").autocomplete({
            source: function (request, response) {
                $.ajax({
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    url: "SetPermission.aspx/BindUserList",
                    data: "{'strUserName':'" + document.getElementById('txtSearch').value + "'}",
                    dataType: "json",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.FullName,
                                val: item.GetID
                            }
                        }));
                        document.getElementById('txtSearch').focus();
                    },
                    error: function (result) {
                        alert("Error");
                    }
                });
            },
            select: function (event, ui) {
                document.getElementById('txtSearch').value = ui.item.value;
                document.getElementById("hidUserId").value = ui.item.val
                document.getElementById("btnShowSearchUser").click();
                document.getElementById('txtSearch').focus();
            }
        });
    }
    function ClearSearchText() {
        debugger;
        document.getElementById('txtSearch').value = "";
    }    
</script>

