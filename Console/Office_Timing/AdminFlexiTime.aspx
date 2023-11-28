<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminFlexiTime.aspx.cs"
    Inherits="Admin_Office_Timing_AdminFlexiTime" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Console/FillUserHierarchy.ascx" TagName="getusers" TagPrefix="uc2" %>
<%@ Register Src="../Menu_Manage/AdminConsoleNavigation.ascx" TagName="Navigation"
    TagPrefix="uc4" %>
<%@ Register Src="~/Console/Includes/Admin_Console_Header.ascx" TagName="Header"
    TagPrefix="hdr" %>
<%@ Register Src="~/Console/Includes/AdminConsoleLeftMenu.ascx" TagName="LeftMenu"
    TagPrefix="lm" %>
<!DOCTYPE html PUBLIC 
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Console::FlexiTime</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.02)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.02)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../jscript48/calendar.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    
    <script src="../scripts/ajax.js" type="text/javascript"></script>

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

    <script src="../jscript48/timerCode.js" type="text/javascript"></script>

    <script src="../jscript48/calendarcode.js" type="text/javascript"></script>

</head>

<script type="text/javascript" language="javascript">
     
    function DeleteConfrom() {
        if (CheckSelect() != "") {
            return confirm('Are You Sure To Delete?');
        }
        else {
            return false;
        }
    }
   
    function SetPhysicalLocation() {
        var phyloc = '<%=hidVal.Value%>';
        alert(phyloc);
        document.getElementById("TabFlexiTime_TabCreateFlexiTime_lblPhylocation").innerHTML = phyloc;
    }
     
    function checkAddvalidation() {
    debugger;
        curDate = new Date();
        date = curDate.getMonth() + "/" + curDate.getDate() + "/" + curDate.getYear();
        start = new Date();
        end = new Date();
        recessTo = new Date();
        recessFrom = new Date();
        diff1 = new Date();
        diff2 = new Date();
        startTime = new Date(date + " " + document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtStartTime").value);
        endTime = new Date(date + " " + document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtEndTime").value);
        recessFrom = new Date(date + " " + document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtRecessFrom").value);
        recessTo = new Date(date + " " + document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtRecessTo").value);
         lateExitTime = new Date(date + " " + document.getElementById('<% = txtNLateEnd.ClientID%>').value);
        earlyEntryTime = new Date(date + " " + document.getElementById('<%= txtEarlyETime.ClientID%>').value);
        lateEntryTime = new Date(date + " " + document.getElementById('<%= txtLateETime.ClientID%>').value);
        extralateExitTime = new Date(date + " " + document.getElementById('<%= txtExLateExitTime.ClientID%>').value);
         if (!DropDownValidation('TabFlexiTime_TabCreateFlexiTime_getuser1_sdrplayers0', 'Location')) {
            return false;
        }

        if (!DropDownValidation('TabFlexiTime_TabCreateFlexiTime_ddlUser', 'User')) {
            return false;
        }
         if (parseInt(document.getElementById("TabFlexiTime_TabCreateFlexiTime_ddlUser").value) > 0) {
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_hiduserid").value = document.getElementById("TabFlexiTime_TabCreateFlexiTime_ddlUser").value;
         }

        if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtDateFrom', 'Date From')) {
            return false;

        }
        if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtDateTo', 'Date To')) {
            return false;
        }
         var toDate = convertDate(document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtDateTo").value);
        var dateFrom = convertDate(document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtDateFrom").value);

        if (dateFrom > toDate) {
            alert("From Date cannot be greater than To date");
            return false;
        }
         if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtStartTime', 'Start Time')) {
            return false;
        }
        if (!TimeValidation('TabFlexiTime_TabCreateFlexiTime_txtStartTime')) {
            return false;
        }
         if (!DropDownValidation('TabFlexiTime_TabCreateFlexiTime_ddlgracetime', 'Grace time')) {
            return false;
        } 
        if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtRecessFrom', 'Recess From Time')) {
            return false;
        }
        if (!TimeValidation('TabFlexiTime_TabCreateFlexiTime_txtRecessFrom')) {
            return false;
        }
         if (checkDifference('TabFlexiTime_TabCreateFlexiTime_txtRecessFrom', 'TabFlexiTime_TabCreateFlexiTime_txtStartTime')) {
            alert('RecessFrom Time Can not be smaller than  or equal to  Start Time');
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtRecessFrom").focus();
            return false;
        }
 
        if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtRecessTo', 'Recess To Time')) {
            return false;
        }
        if (!TimeValidation('TabFlexiTime_TabCreateFlexiTime_txtRecessTo')) {
            return false;
        }
         if (checkDifference('TabFlexiTime_TabCreateFlexiTime_txtRecessTo', 'TabFlexiTime_TabCreateFlexiTime_txtRecessFrom')) {
            alert('Recess To  cannot be smaller than or equal to RecessFrom time');
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtRecessTo").focus();
            return false;
        }
 
        if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtEndTime', 'Exit Time')) {
            return false;
        }
        if (!TimeValidation('TabFlexiTime_TabCreateFlexiTime_txtEndTime')) {
            return false;
        }
        

         if (checkDifference('TabFlexiTime_TabCreateFlexiTime_txtEndTime', 'TabFlexiTime_TabCreateFlexiTime_txtRecessTo')) {
            alert('Exit Time Can not be smaller than or equal to Recess To Time');
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtEndTime").focus();
            return false;
        }
         diff1.setTime(endTime.getTime() - startTime.getTime());
        timediff1 = new Number(diff1.getTime());

        if (timediff1 < 0) {
            alert("start time cannot be greater than exit time");
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtStartTime").focus();
            return false;
        }
        if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtNLateEnd', 'Late Exit Time')) {
            return false;
        }
        if (!TimeValidation('TabFlexiTime_TabCreateFlexiTime_txtNLateEnd')) {
            return false;
        }
         if (checkDifference('TabFlexiTime_TabCreateFlexiTime_txtNLateEnd', 'TabFlexiTime_TabCreateFlexiTime_txtEndTime')) {
             alert('Late Exit Time  Can not be smaller than or equal to Exit Time');
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtNLateEnd").focus();
            return false;
        }
 
        if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtEarlyETime', 'Early Entry Time')) {
            return false;
        }
        if (!TimeValidation('TabFlexiTime_TabCreateFlexiTime_txtEarlyETime')) {
            return false;
        }
         if (checkDifference('TabFlexiTime_TabCreateFlexiTime_txtStartTime', 'TabFlexiTime_TabCreateFlexiTime_txtEarlyETime')) {
            alert('Early entry time should always smaller than Start Time');
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtEarlyETime").focus();
            return false;
        }
 
        if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtLateETime', 'Late Entry Time')) {
            return false;
        }
        if (!TimeValidation('TabFlexiTime_TabCreateFlexiTime_txtLateETime')) {
            return false;
        }
         if (checkDifference('TabFlexiTime_TabCreateFlexiTime_txtLateETime', 'TabFlexiTime_TabCreateFlexiTime_txtStartTime')) {
            alert('Late Entry Time should always greater than  Start Time');
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtLateETime").focus();
            return false;
        }
         if (checkDifference('TabFlexiTime_TabCreateFlexiTime_txtRecessFrom', 'TabFlexiTime_TabCreateFlexiTime_txtLateETime')) {
            alert('Late Entry Time should always smaller than Recess From  Time');
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtLateETime").focus();
            return false;
        }
 
        if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtExLateExitTime', 'Extra Late Exit Time')) {
            return false;
        }
        if (!TimeValidation('TabFlexiTime_TabCreateFlexiTime_txtExLateExitTime')) {
            return false;
        }
         if (checkDifference('TabFlexiTime_TabCreateFlexiTime_txtExLateExitTime', 'TabFlexiTime_TabCreateFlexiTime_txtLateETime')) {
            alert('Extra Late Exit time should always greater than  Late Exit Time');
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtExLateExitTime").focus();
            return false;
        }
         if (document.getElementById("TabFlexiTime_TabCreateFlexiTime_cbWeekHalf").checked == true) {            
            
             if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtHfStartTime', 'Half Start Time')) {
                return false;
            }
            if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtHfEETime', 'Half Early Entry Time')) {
                return false;
            }
            if (!checkDifference('TabFlexiTime_TabCreateFlexiTime_txtHfEETime', 'TabFlexiTime_TabCreateFlexiTime_txtHfStartTime')) {
                alert('Half day Early Entry Time can not be greater than Half Day Start Time');
                document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtHfEETime").focus();
                return false;
            }
            if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtHfLETime', 'Half Late Entry Time')) {
                return false;
            }
            //if(selectedText.toLowerCase()!='night')
            //{
                 if (!checkDifference('TabFlexiTime_TabCreateFlexiTime_txtHfStartTime', 'TabFlexiTime_TabCreateFlexiTime_txtHfLETime')) {
                    alert('Half day Late Entry Time can not be less than Half Day Start Time');
                    document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtHfLETime").focus();
                    return false;
                }
            //}
            if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtHalfETime', 'Half Exit Time')) {
                return false;
            }
            if (!TimeValidation('TabFlexiTime_TabCreateFlexiTime_txtHalfETime')) {
                return false;
            }
            //if(selectedText.toLowerCase()!='night')
            //{
                if (!checkDifference('TabFlexiTime_TabCreateFlexiTime_txtHfStartTime', 'TabFlexiTime_TabCreateFlexiTime_txtHalfETime')) {
                    alert('Half day ExitTime Time can not be less than Start Time');
                    document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtHalfETime").focus();
                    return false;
                }
            //}
            if (!blankFieldValidation('TabFlexiTime_TabCreateFlexiTime_txtHalfLateETime', 'Half Late Exit Time')) {
                return false;
            }           
            if (!TimeValidation('TabFlexiTime_TabCreateFlexiTime_txtHalfLateETime')) {
                return false;
            }
            //if(selectedText.toLowerCase()!='night')
            //{
                if (!checkDifference('TabFlexiTime_TabCreateFlexiTime_txtHalfETime', 'TabFlexiTime_TabCreateFlexiTime_txtHalfLateETime')) {
                    alert('Half day Late ExitTime can not be less than HalfDay ExitTime');
                    document.getElementById("TabFlexiTime_TabCreateFlexiTime_txtHalfLateETime").focus();
                    return false;
                }
           // }
         }
    }
    function ShowHide() {
        if (document.getElementById("TabFlexiTime_TabCreateFlexiTime_cbWeekHalf").checked == true) {
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_tr1").style.display = "";
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_tr2").style.display = "";
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_tr3").style.display = "";
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_tr4").style.display = "";
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_tr5").style.display = "";
        }
        else {
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_tr1").style.display = "none";
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_tr2").style.display = "none";
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_tr3").style.display = "none";
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_tr4").style.display = "none";
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_tr5").style.display = "none";
        }
    }
    
     function convertDate(dt) {
        var strTemp = "";
        var strChar;
        var date1 = new Array(3);
        var j = 0;
        var strDateTo = dt;
        var todatelen = strDateTo.length;

        for (var i = 0; i <= todatelen; i++) {
            strChar = strDateTo.charAt(i);

            if (strChar == '-' || strChar == '') {

                date1[j] = strTemp;

                strTemp = "";
                j = j + 1;

            }
            else {
                strTemp = strTemp + strChar;
            }
        }

        if (date1[1] == 'Jan') {


            date1[1] = 01;
        }
        else if (date1[1] == 'Feb') {

            date1[1] = 02;
        }
        else if (date1[1] == 'Mar') {
            date1[1] = 03;
        }
        else if (date1[1] == 'Apr') {
            date1[1] = 04;
        }
        else if (date1[1] == 'May') {
            date1[1] = 05;
        }
        else if (date1[1] == 'Jun') {
            date1[1] = 06;
        }
        else if (date1[1] == 'Jul') {
            date1[1] = 07;
        }
        else if (date1[1] == 'Aug') {
            date1[1] = 08;
        }
        else if (date1[1] == 'Sep') {
            date1[1] = 09;
        }
        else if (date1[1] == 'Oct') {
            date1[1] = 10;
        }
        else if (date1[1] == 'Nov') {
            date1[1] = 11;
        }
        else if (date1[1] == 'Dec') {

            date1[1] = 12;
        }
        var conDate = new Date(date1[1] + "/" + date1[0] + "/" + date1[2]);
        return conDate;
    }
     function CheckSelect() {
        if (!ConfirmCheck('form1')) {

            return false;
        }
    }
     function conformation(btnId) {
 debugger;
        var btntext = '<%=strbtntext%>';
        if (checkAddvalidation() != "") {
            if (btnId.value == "Add") {
                if (confirm("Are you sure to Save ?")) {
                    return true;
                }
                else
                    return false;
            }  

            if (btnId.value == "Update") {
                if (confirm("Are You Sure To Update?")) {
                    return true;
                }
                else
                    return false;
            }  
        }
        else
            return false;
    }
     function checkDifference(txtTime1, txtTime2) {
         var Todaydate = new Date();
        var Formatteddate = Todaydate.format("MM/dd/yyyy"); 
        
        var start = document.getElementById(txtTime1).value;
        if(txtTime2!='TimeExceed'){    
            var end = document.getElementById(txtTime2).value;
        }
        var FormattedTIme2 ;
        var FormattedTime1 = Formatteddate + " " + start;
         if(txtTime2!='TimeExceed'){ 
              FormattedTIme2 = Formatteddate + " " + end;
        }
        else{
              FormattedTIme2 = Formatteddate + " " + '11:59 PM';
        }
                

        var  Time1 = new Date(FormattedTime1);
        var  Time2 = new Date(FormattedTIme2);
        
        var TimeDiff = Time2.getTime() - Time1.getTime();
        if(TimeDiff <= 0){
            return false;
        }
        else{
            return true;
        }                            
    }
     
   function fillPhysicalLoc() {
    var UserID=document.getElementById("TabFlexiTime_TabCreateFlexiTime_ddlUser").value;
     $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        url: "AdminFlexiTime.aspx/GetUserPhysicalLoc",
        data: '{"strUserId":"'+UserID+'"}',
        dataType: "json",
        success: function (msg) {
            document.getElementById("TabFlexiTime_TabCreateFlexiTime_lblPhylocation").innerHTML = msg.d

        },
        error: function(msg) {
            AjaxFailed;
        }
    });

}
</script>

<body>
    <div id="popTimer" style="position: absolute; visibility: hidden">
    </div>
    <div id="popupcalendar" style="position: absolute; visibility: hidden">
    </div>
    <form id="form1" runat="server" enctype="multipart/form-data">
    <asp:HiddenField ID="hidVal" runat="server" />
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
                                    <cc1:TabContainer ID="TabFlexiTime" runat="server" Width="100%" ActiveTabIndex="1"
                                        CssClass="ajax__tab_yuitabview-theme" OnActiveTabChanged="TabFlexiTime_ActiveTabChanged"
                                        AutoPostBack="true">
                                        <div class="Menubar">
                                            <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateFlexiTime" TabIndex="0">
                                                <ContentTemplate>
                                                    <div class="mandatory">
                                                        (* indicates mandatory fields)
                                                    </div>
                                                    <div class="nodata">
                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="addTable">
                                                        <uc2:getusers ID="getuser1" runat="server" />
                                                        <asp:HiddenField ID="hiduserid" runat="server" />
                                                        <table border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td width="150px">
                                                                    User
                                                                </td>
                                                                <td width="5px">
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:DropDownList ID="ddlUser" onchange="fillPhysicalLoc();" runat="server" Width="185px">
                                                                        <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr id="trPhyLoc" runat="server">
                                                                <td width="150px">
                                                                    Physical Location
                                                                </td>
                                                                <td width="5px">
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:Label ID="lblPhylocation" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Date From
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtDateFrom" runat="server" Width="175px"></asp:TextBox>
                                                                    <font color="#FF0000">*</font><a class="so-BtnLink" href="javascript:calClick();return false;"
                                                                        onclick="calSwapImg('BTN_date1', 'img_Date_DOWN');showCalendar('form1','TabFlexiTime_TabCreateFlexiTime_txtDateFrom','BTN_date1');return false;"
                                                                        onmouseout="calSwapImg('BTN_date1', 'img_Date_UP',true);" onmouseover="calSwapImg('BTN_date1', 'img_Date_OVER',true);"><img
                                                                            src="../jscript48/btn_date_up.gif" name="BTN_date1" width="20" height="14" border="0"
                                                                            align="middle" id="BTN_date1" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Date To
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtDateTo" runat="server" Width="175px"></asp:TextBox>
                                                                    <font color="#FF0000">*</font><a class="so-BtnLink" href="javascript:calClick();return false;"
                                                                        onclick="calSwapImg('BTN_date2', 'img_Date_DOWN');showCalendar('form1','TabFlexiTime_TabCreateFlexiTime_txtDateTo','BTN_date2');return false;"
                                                                        onmouseout="calSwapImg('BTN_date2', 'img_Date_UP',true);" onmouseover="calSwapImg('BTN_date2', 'img_Date_OVER',true);"><img
                                                                            src="../jscript48/btn_date_up.gif" name="BTN_date2" width="20" height="14" border="0"
                                                                            align="middle" id="BTN_date2" /></a>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Start Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtStartTime" runat="server" Width="175px"></asp:TextBox>
                                                                    &nbsp;&nbsp;<img src="../images/clock_icon.gif" width="11" height="12" id="btnTimeHET1"
                                                                        onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtStartTime','btnTimeHET1');" />
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Grace Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:DropDownList ID="ddlgracetime" runat="server" Width="50px">
                                                                        <asp:ListItem Value="0" Text="0"></asp:ListItem>
                                                                        <asp:ListItem Value="1" Text="5"></asp:ListItem>
                                                                        <asp:ListItem Value="2" Text="10"></asp:ListItem>
                                                                        <asp:ListItem Value="3" Text="15"></asp:ListItem>
                                                                        <asp:ListItem Value="4" Text="20"></asp:ListItem>
                                                                        <asp:ListItem Value="5" Text="25"></asp:ListItem>
                                                                        <asp:ListItem Value="6" Text="30"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Recess From
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtRecessFrom" runat="server" Width="175px"></asp:TextBox>
                                                                    &nbsp;&nbsp;<img src="../images/clock_icon.gif" height="12" id="btnTime2" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtRecessFrom','btnTime2');"
                                                                        style="width: 11px">
                                                                    &nbsp;<font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Recess To
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtRecessTo" runat="server" Width="175px"></asp:TextBox>
                                                                    &nbsp;&nbsp;<img src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtRecessTo','btnTimeRT');"
                                                                        id="btnTimeRT">
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Exit Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtEndTime" runat="server" Width="175px"></asp:TextBox>
                                                                    &nbsp;&nbsp;<img src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtEndTime','btnTimeET');"
                                                                        id="btnTimeET">
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Late Exit Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtNLateEnd" runat="server" Width="175px"></asp:TextBox>
                                                                    &nbsp;&nbsp;<img src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtNLateEnd','btnTimeNLE');"
                                                                        id="btnTimeNLE">
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Early Entry Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtEarlyETime" runat="server" Width="175px"></asp:TextBox>
                                                                    &nbsp;&nbsp;<img src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtEarlyETime','btnTimeEET');"
                                                                        id="btnTimeEET">
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Late Entry Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtLateETime" runat="server" Width="175px"></asp:TextBox>
                                                                    &nbsp;&nbsp;<img src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtLateETime','btnTimeELT');"
                                                                        id="btnTimeELT">
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Extra Late Exit Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtExLateExitTime" runat="server" Width="175px"></asp:TextBox>
                                                                    &nbsp;&nbsp;<img src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtExLateExitTime','btnTimeELET');"
                                                                        id="btnTimeELET">
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align="left">
                                                                    <asp:CheckBox ID="cbWeekHalf" runat="server" OnClick="ShowHide();" Text="Time for Weekly HalfDay"
                                                                        Font-Bold="true" />
                                                                </td>
                                                            </tr>
                                                            <tr id="tr3" runat="server" style="display: none;">
                                                                <td>
                                                                    Start Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtHfStartTime" Width="175px" runat="server"></asp:TextBox>&nbsp;&nbsp;<img
                                                                        src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtHfStartTime','imgHfStartTime');"
                                                                        id="imgHfStartTime">
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr4" runat="server" style="display: none;">
                                                                <td>
                                                                    Early Entry Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtHfEETime" Width="175px" runat="server"></asp:TextBox>&nbsp;&nbsp;<img
                                                                        src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtHfEETime','imgHfEETime');"
                                                                        id="imgHfEETime">
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr5" runat="server" style="display: none;">
                                                                <td>
                                                                    Late Entry Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtHfLETime" Width="175px" runat="server"></asp:TextBox>&nbsp;&nbsp;<img
                                                                        src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtHfLETime','imgHfLETime');"
                                                                        id="imgHfLETime">
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr1" runat="server" style="display: none;">
                                                                <td>
                                                                    Exit Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtHalfETime" runat="server" Width="175px"></asp:TextBox>
                                                                    &nbsp;&nbsp;<img src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtHalfETime','imgHalfETime');"
                                                                        id="imgHalfETime">
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                            <tr id="tr2" runat="server" style="display: none;">
                                                                <td>
                                                                    Late Exit Time
                                                                </td>
                                                                <td>
                                                                    :
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:TextBox ID="txtHalfLateETime" runat="server" Width="175px"></asp:TextBox>
                                                                    &nbsp;&nbsp;<img src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabFlexiTime_TabCreateFlexiTime_txtHalfLateETime','imgHalfLateETime');"
                                                                        id="imgHalfLateETime"><font color="#FF0000"> *</font>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td style="padding-left: 6px;">
                                                                    <asp:Button ID="BtnAdd" runat="server" Text="Save" OnClick="BtnAdd_Click" OnClientClick="return conformation(this);" />
                                                                    <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel runat="server" HeaderText="EDIT" ID="TabEditFlexiTime" TabIndex="1">
                                                <ContentTemplate>
                                                    <div style="margin-right: 7px; height: 20px">
                                                        <table border="0" align="right">
                                                            <tr>
                                                                <td>
                                                                    <asp:LinkButton ID="LnkbtnAllin" Visible="False" runat="server" Text="All" OnClick="LnkbtnAllin_Click"></asp:LinkButton>
                                                                </td>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblpage" runat="server" Visible="False"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="nodata" align="center">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#C00000"></asp:Label>
                                                    </div>
                                                    <div class="viewTable">
                                                        <asp:GridView ID="GVFlexiTime" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                            CellSpacing="0" HeaderStyle-Font-Bold="True" ItemStyle-VerticalAlign="Top" PagerStyle-HorizontalAlign="Right"
                                                            PagerStyle-Mode="NumericPages" PagerStyle-PageButtonCount="10" PageSize="20"
                                                            DataKeyNames="OffTimeID" OnRowDataBound="GVFlexiTime_RowDataBound" OnPageIndexChanging="GVFlexiTime_PageIndexChanging"
                                                            OnRowCreated="GVFlexiTime_RowCreated">
                                                            <EmptyDataTemplate>
                                                                <div id="divCentr" style="text-align: center;">
                                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="LightGray"
                                                                        Text="No Flexi Time Record Found ..."></asp:Label>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <HeaderTemplate>
                                                                        <input type="checkbox" name="cbAll" value="cbAll" onclick="SelectAll(cbAll,'GVFlexiTime','form1')" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll,'GVFlexiTime','form1')" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Sl No" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Empl. Name" HeaderStyle-HorizontalAlign="Left" DataField="FullName" />
                                                                <asp:BoundField HeaderText="Designation" DataField="Designation" HeaderStyle-HorizontalAlign="Left" />
                                                                <asp:BoundField HeaderText="From" DataField="DateFrom" ItemStyle-HorizontalAlign="Center"
                                                                    DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField HeaderText="To" DataField="DateTo" ItemStyle-HorizontalAlign="Center"
                                                                    DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField HeaderText="In" DataField="StartTime" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:BoundField HeaderText="Out" DataField="EndTime" ItemStyle-HorizontalAlign="Center" />
                                                                <asp:TemplateField>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <HeaderTemplate>
                                                                        Edit
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href="AdminFlexiTime.aspx?FID=<%#DataBinder.Eval(Container.DataItem,"OffTimeID")%>">
                                                                            <img alt="" src="../images/editIcon.gif" border="0" align="absmiddle" />
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="deletebtn" style="padding-left: 10px;">
                                                        <asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btndelete_Click"
                                                            OnClientClick="return DeleteConfrom();" />
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                        </div>
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
