<%@ Page Language="C#" AutoEventWireup="true" Inherits="Admin_Office_Timing_AdminOfficeTime"
    CodeBehind="AdminOfficeTime.aspx.cs" %>

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
    <title>Admin Console::OfficeTime</title>
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0.02)" />
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0.02)" />
    <link href="../style/default.css" rel="stylesheet" type="text/css" />
    <link href="../jscript48/calendar.css" rel="stylesheet" type="text/css" />
    <link href="../style/common.css" rel="stylesheet" type="text/css" />

    <script src="../scripts/AjaxScript.js" type="text/javascript"></script>

    <script src="../jscript48/Validator.js" type="text/javascript"></script>

    <script src="../jscript48/timerCode.js" type="text/javascript"></script>

    <script src="../jscript48/calendarcode.js" type="text/javascript"></script>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js"></script>

</head>

<script type="text/javascript" language="javascript">

    var Checkdiff;
 
    function checkAddvalidation() {
   
         var selectedText = $("#<%= ddlShift.ClientID%> option:selected").text();
        curDate = new Date();
        date = curDate.getMonth() + "/" + curDate.getDate() + "/" + curDate.getYear();

         startTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtStartTime").value);
        endTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtEndTime").value);
        recessFrom = new Date(date + " " + document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtRecessFrom").value);
        recessTo = new Date(date + " " + document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtRecessTo").value);
        LateExitTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtNLateEnd").value);
        EarlyEntryTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtEarlyETime").value);
        LateEntryTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtLateETime").value);
        ExtraLateExitTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtExLateExitTime").value);
        HalfDayExitTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtHalfETime").value);
        HalfDayLateExitTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtHalfLateETime").value);
        DateExceed = new Date(date + " " + "11:59 PM");

 
        if (!DropDownValidation('TabOfficeTime_TabCreateOfficeTime_ddlLocation', 'Location')) {
            return false;
        }
        if (!DropDownValidation('TabOfficeTime_TabCreateOfficeTime_ddlShift', 'Shift')) {
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtStartTime', 'Start Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabCreateOfficeTime_txtStartTime')) {
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtRecessFrom', 'Recess From Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabCreateOfficeTime_txtRecessFrom')) {
            return false;
        }

         if(selectedText.toLowerCase()!='night')
        {
            if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtStartTime','TabOfficeTime_TabCreateOfficeTime_txtRecessFrom')) {
                alert('RecessFrom time can not be smaller than start time');
                document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtRecessFrom").focus();
                return false;
            }
        }
        if (selectedText.toLowerCase() != 'night') {
            if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtRecessFrom', 'TabOfficeTime_TabCreateOfficeTime_txtEndTime')) {
                alert('End time can not be smaller than Recess From time');
                document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtRecessFrom").focus();
                return false;
            }
        }

        if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtRecessTo', 'Recess To Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabCreateOfficeTime_txtRecessTo')) {
            return false;
        }
         if(selectedText.toLowerCase()!='night')
        {
             if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtRecessFrom', 'TabOfficeTime_TabCreateOfficeTime_txtRecessTo')) {
                alert('RecessTo time can not be smaller than Recess From time.');
                document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtRecessTo").focus();
                return false;
            }
        }
        if (selectedText.toLowerCase() != 'night') {
            if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtRecessTo', 'TabOfficeTime_TabCreateOfficeTime_txtEndTime')) {
                alert('End time can not be smaller than Recess To time');
                document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtRecessTo").focus();
                return false;
            }
        }
        if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtEndTime', 'Exit Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabCreateOfficeTime_txtEndTime')) {
            return false;
        }
         if(selectedText.toLowerCase()!='night')
        {
            if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtStartTime', 'TabOfficeTime_TabCreateOfficeTime_txtEndTime')) {
                alert('Exit Time can not be smaller than Start Time');
                document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtEndTime").focus();
                return false;
            }
        }
        if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtNLateEnd', 'Late Exit Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabCreateOfficeTime_txtNLateEnd')) {
            return false;
        }
         if(selectedText.toLowerCase()!='night')
        {
            if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtEndTime', 'TabOfficeTime_TabCreateOfficeTime_txtNLateEnd')) {
                alert('Late ExitTime can not be smaller than Exit Time');
                document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtNLateEnd").focus();
                return false;
            }
        }
        if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtEarlyETime', 'Early Entry Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabCreateOfficeTime_txtEarlyETime')) {
            return false;
        }
         if(selectedText.toLowerCase()!='night')
        {
            if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtEarlyETime', 'TabOfficeTime_TabCreateOfficeTime_txtStartTime')) {
                alert('EarlyEntry Time should be less than Start Time');
                document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtEarlyETime").focus();
                return false;
            }
        }
        if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtLateETime', 'Late Entry Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabCreateOfficeTime_txtLateETime')) {
            return false;
        }
         if(selectedText.toLowerCase()!='night')
        {
            if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtStartTime', 'TabOfficeTime_TabCreateOfficeTime_txtLateETime')) {
                alert('Late Entry Time can not be less than Start Time');
                document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtLateETime").focus();
                return false;
            }
        }
        if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtExLateExitTime', 'Extra Late Exit Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabCreateOfficeTime_txtExLateExitTime')) {
            return false;
        }
         if(selectedText.toLowerCase()!='night')
        {
            if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtNLateEnd', 'TabOfficeTime_TabCreateOfficeTime_txtExLateExitTime')) {
                alert('ExtraLateExit Time can not ne less than Late Exit Time');
                document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtExLateExitTime").focus();
                return false;
            }
        }
         if(selectedText.toLowerCase()!='night')
        {
            if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtExLateExitTime','TimeExceed')) {
                alert('ExtraLateExit time should be less than 11:59 PM');
                document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtExLateExitTime").focus();
                return false;
            }
        }
        if (document.getElementById("TabOfficeTime_TabCreateOfficeTime_cbWeekHalf1").checked == true) {
            
            if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtHfStartTime', 'Half Start Time')) {
                return false;
            }
            if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtHfEETime', 'Half Early Entry Time')) {
                return false;
            }
            if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtHfEETime', 'TabOfficeTime_TabCreateOfficeTime_txtHfStartTime')) {
                alert('Half day Early Entry Time can not be greater than Half Day Start Time');
                document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtHfEETime").focus();
                return false;
            }
            if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtHfLETime', 'Half Late Entry Time')) {
                return false;
            }
            if(selectedText.toLowerCase()!='night')
            {
                 if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtHfStartTime', 'TabOfficeTime_TabCreateOfficeTime_txtHfLETime')) {
                    alert('Half day Late Entry Time can not be less than Half Day Start Time');
                    document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtHfLETime").focus();
                    return false;
                }
            }
            if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtHalfETime', 'Half Exit Time')) {
                return false;
            }
            if (!TimeValidation('TabOfficeTime_TabCreateOfficeTime_txtHalfETime')) {
                return false;
            }
             if(selectedText.toLowerCase()!='night')
            {
                if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtHfStartTime', 'TabOfficeTime_TabCreateOfficeTime_txtHalfETime')) {
                    alert('Half day ExitTime Time can not be less than Start Time');
                    document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtHalfETime").focus();
                    return false;
                }
            }
            if (!blankFieldValidation('TabOfficeTime_TabCreateOfficeTime_txtHalfLateETime', 'Half Late Exit Time')) {
                return false;
            }           
            if (!TimeValidation('TabOfficeTime_TabCreateOfficeTime_txtHalfLateETime')) {
                return false;
            }
            if(selectedText.toLowerCase()!='night')
            {
                if (!checkDifference('TabOfficeTime_TabCreateOfficeTime_txtHalfETime', 'TabOfficeTime_TabCreateOfficeTime_txtHalfLateETime')) {
                    alert('Half day Late ExitTime can not be less than HalfDay ExitTime');
                    document.getElementById("TabOfficeTime_TabCreateOfficeTime_txtHalfLateETime").focus();
                    return false;
                }
            }
            
        }
    }
    function ShowHide1() {
        if (document.getElementById("TabOfficeTime_TabCreateOfficeTime_cbWeekHalf1").checked == true) {
            document.getElementById("TabOfficeTime_TabCreateOfficeTime_tr11").style.display = "";
            document.getElementById("TabOfficeTime_TabCreateOfficeTime_tr12").style.display = "";
            document.getElementById("TabOfficeTime_TabCreateOfficeTime_tr13").style.display = "";
            document.getElementById("TabOfficeTime_TabCreateOfficeTime_tr14").style.display = "";
            document.getElementById("TabOfficeTime_TabCreateOfficeTime_tr15").style.display = "";
        }
        else {
            document.getElementById("TabOfficeTime_TabCreateOfficeTime_tr11").style.display = "none";
            document.getElementById("TabOfficeTime_TabCreateOfficeTime_tr12").style.display = "none";
            document.getElementById("TabOfficeTime_TabCreateOfficeTime_tr13").style.display = "none";
            document.getElementById("TabOfficeTime_TabCreateOfficeTime_tr14").style.display = "none";
            document.getElementById("TabOfficeTime_TabCreateOfficeTime_tr15").style.display = "none";
        }
    }


    function CheckSelect() {
       
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
   
    function conformation(btnId) {
    debugger
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
    debugger;
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

    function checkExpAddvalidation() {
    debugger;
        curDate = new Date();
        date = curDate.getMonth() + "/" + curDate.getDate() + "/" + curDate.getYear();
        startTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExStartTime").value);
        endTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExEndTime").value);
        recessFrom = new Date(date + " " + document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExRecessFrom").value);
        recessTo = new Date(date + " " + document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExRecessTo").value);
        LateExitTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExNLateEnd").value);
        EarlyEntryTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExEarlyETime").value);
        LateEntryTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExLateETime").value);
        ExtraLateExitTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExpExLateExitTime").value);
        HalfDayExitTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExHalfETime").value);
        HalfDayLateExitTime = new Date(date + " " + document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExHalfLateETime").value);
        DateExceed = new Date(date + " " + "11:59 PM");

        if (!DropDownValidation('TabOfficeTime_TabExceptionalOffice_ddlExplocation', 'Location')) {
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtDateFrom', 'Date From')) {
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtDateTo', 'Date To')) {
            return false;
        }
        var toDate = convertDate(document.getElementById("TabOfficeTime_TabExceptionalOffice_txtDateTo").value);
        var dateFrom = convertDate(document.getElementById("TabOfficeTime_TabExceptionalOffice_txtDateFrom").value);

        if ('<%=ViewState["Exp"]%>' != null) {
            if ('<%=ViewState["Exp"]%>' == "Add") {
                if ('<%=DtContrl%>' != null) {
                    var PreDate = convertDate('<%=DtContrl%>');
                    if (dateFrom <= PreDate) {
                        alert("From Date should be greater than" + " " + '<%=DtContrl%>');
                        return false;
                    }
                }
            }
            if ('<%=ViewState["Exp"]%>' == "Update") {
                var preDateFrom = '<%=strPreFromdate%>';
                var preDateTo = '<%=strPreTodate%>';
                var NextDateFrom = '<%=strNextFromdate%>';
                var NextDateTo = '<%=strNextTodate%>';

                if (NextDateFrom != "01-Jan-0001") {

                    if (dateFrom >= convertDate(NextDateFrom)) {
                        alert("From Date should be less than" + " " + NextDateFrom);
                        return false;
                    }
                    if (toDate >= convertDate(NextDateFrom)) {
                        alert("To Date should be less than" + " " + NextDateFrom);
                        return false;
                    }
                }

                if (preDateTo != '01-Jan-0001') {
                    if (dateFrom <= convertDate(preDateTo)) {
                        alert("From Date should be Greater than" + " " + preDateFrom);
                        return false;
                    }

                    if (toDate <= convertDate(preDateTo)) {
                        alert("To Date should be less than" + " " + preDateFrom);
                        return false;
                    }
                }
            }

        }

        if (dateFrom > toDate) {
            alert("From Date cannot be greater than To date");
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtExStartTime', 'Start Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabExceptionalOffice_txtExStartTime')) {
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtExRecessFrom', 'Recess From Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabExceptionalOffice_txtExRecessFrom')) {
            return false;
        }
        if (!checkDifference('TabOfficeTime_TabExceptionalOffice_txtExStartTime', 'TabOfficeTime_TabExceptionalOffice_txtExRecessFrom')) {
            alert('RecessFrom Time Can not be smaller than Start Time');
            document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExRecessFrom").focus();
            return false;
        }
        if (!checkDifference( 'TabOfficeTime_TabExceptionalOffice_txtExRecessFrom','TabOfficeTime_TabExceptionalOffice_txtExEndTime')) {
            alert('End Time Can not be smaller than RecessFrom Time');
            document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExRecessFrom").focus();
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtExRecessTo', 'Recess To Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabExceptionalOffice_txtExRecessTo')) {
            return false;
        }
        if (!checkDifference('TabOfficeTime_TabExceptionalOffice_txtExRecessFrom', 'TabOfficeTime_TabExceptionalOffice_txtExRecessTo')) {
            alert('RecessTo Time Can not be smaller than RecessFrom');
            document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExRecessTo").focus();
            return false;
        }
        if (!checkDifference('TabOfficeTime_TabExceptionalOffice_txtExRecessTo', 'TabOfficeTime_TabExceptionalOffice_txtExEndTime')) {
            alert('End Time Can not be smaller than RecessTo Time');
            document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExRecessTo").focus();
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtExEndTime', 'Exit Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabExceptionalOffice_txtExEndTime')) {
            return false;
        }
        if (!checkDifference('TabOfficeTime_TabExceptionalOffice_txtExStartTime', 'TabOfficeTime_TabExceptionalOffice_txtExEndTime')) {
            alert('ExitTime Can not be smaller than StartTime');
            document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExEndTime").focus();
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtExNLateEnd', 'Late Exit Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabExceptionalOffice_txtExNLateEnd')) {
            return false;
        }
        if (!checkDifference('TabOfficeTime_TabExceptionalOffice_txtExEndTime', 'TabOfficeTime_TabExceptionalOffice_txtExNLateEnd')) {
            alert('Late ExitTime Time Can not be smaller than Exit Time');
            document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExNLateEnd").focus();
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtExEarlyETime', 'Early Entry Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabExceptionalOffice_txtExEarlyETime')) {
            return false;
        }
        if (!checkDifference('TabOfficeTime_TabExceptionalOffice_txtExEarlyETime', 'TabOfficeTime_TabExceptionalOffice_txtExStartTime')) {
            alert('EarlyEntry Time should be less than Start Time');
            document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExEarlyETime").focus();
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtExLateETime', 'Late Entry Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabExceptionalOffice_txtExLateETime')) {
            return false;
        }
        if (!checkDifference('TabOfficeTime_TabExceptionalOffice_txtExStartTime', 'TabOfficeTime_TabExceptionalOffice_txtExLateETime')) {
            alert('Late Entry Time can not be less than Start Time');
            document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExLateETime").focus();
            return false;
        }
        if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtExpExLateExitTime', 'Extra Late Exit Time')) {
            return false;
        }
        if (!TimeValidation('TabOfficeTime_TabExceptionalOffice_txtExpExLateExitTime')) {
            return false;
        }
        if (!checkDifference('TabOfficeTime_TabExceptionalOffice_txtExEndTime', 'TabOfficeTime_TabExceptionalOffice_txtExpExLateExitTime')) {
            alert('Extra Late Exit time can not be less than late exit Time');
            document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExpExLateExitTime").focus();
            return false;
        }
        if (!checkDifference('TabOfficeTime_TabExceptionalOffice_txtExpExLateExitTime', 'TimeExceed')) {
            alert('ExtraLateExit Time should be less than 11:59 PM');
            document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExpExLateExitTime").focus();
            return false;
        }
        if (document.getElementById("TabOfficeTime_TabExceptionalOffice_cbWeekHalf2").checked == true) {             
            if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtExHalfETime', 'Half Exit Time')) {
                return false;
            }
            if (!TimeValidation('TabOfficeTime_TabExceptionalOffice_txtExHalfETime')) {
                return false;
            }
           
            if (!checkDifference('TabOfficeTime_TabExceptionalOffice_txtExStartTime', 'TabOfficeTime_TabExceptionalOffice_txtExHalfETime')) {
                alert('HalfDay ExitTime Time can not be less than Start Time');
                document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExHalfETime").focus();
                return false;
            }
            if (!blankFieldValidation('TabOfficeTime_TabExceptionalOffice_txtExHalfLateETime', 'Half Late Exit Time')) {
                return false;
            }
            if (!TimeValidation('TabOfficeTime_TabExceptionalOffice_txtExHalfLateETime')) {
                return false;
            }
            
            if (!checkDifference(HalfDayLateExitTime, HalfDayExitTime)) {
                alert('HalfDay LateExitTime Time can not be less than Half day Exit Time');
                document.getElementById("TabOfficeTime_TabExceptionalOffice_txtExHalfLateETime").focus();
                return false;
            }
        }
    }

    function ShowHide2() {
        if (document.getElementById("TabOfficeTime_TabExceptionalOffice_cbWeekHalf2").checked == true) {
            document.getElementById("TabOfficeTime_TabExceptionalOffice_tr21").style.display = "";
            document.getElementById("TabOfficeTime_TabExceptionalOffice_tr22").style.display = "";
        }
        else {
            document.getElementById("TabOfficeTime_TabExceptionalOffice_tr21").style.display = "none";
            document.getElementById("TabOfficeTime_TabExceptionalOffice_tr22").style.display = "none";
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
    function Expconformation() {
    debugger;
        var Exbtntext = '<%=ViewState["Exp"]%>';
        if (checkExpAddvalidation() != "") {
            if (Exbtntext == "Add") {
                if (confirm("Are you sure to Save ?")) {
                    return true;
                }
                else
                    return false;
            } 

            if (Exbtntext == "Update") {
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

    function CallReset() {
        if (document.getElementById("TabOfficeTime_TabCreateOfficeTime_btnReset").value == "Reset") {
            var strCtrls = "TabOfficeTime_TabCreateOfficeTime_ddlLocation,TabOfficeTime_TabCreateOfficeTime_ddlShift,ddlLocation,TabOfficeTime_txtStartTime,";
            var objArr = strCtrls.split(",");

            for (var i = 0; i < objArr.length; i++) {
                ResetValue(objArr[i]);
            }

        } else
            history.back();
        return false;
    }
</script>

<body>
    <div id="popTimer" style="position: absolute; visibility: hidden">
    </div>
    <div id="popupcalendar" style="position: absolute; visibility: hidden;">
    </div>
    <form id="form1" runat="server" enctype="multipart/form-data">
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
                                    <cc1:TabContainer ID="TabOfficeTime" runat="server" AutoPostBack="true" OnActiveTabChanged="TabOfficeTime_ActiveTabChanged"
                                        Width="100%" ActiveTabIndex="1" CssClass="ajax__tab_yuitabview-theme">
                                        <div class="Menubar">
                                            <cc1:TabPanel runat="server" HeaderText="CREATE" ID="TabCreateOfficeTime" TabIndex="0">
                                                <ContentTemplate>
                                                    <div class="mandatory">
                                                        (* indicates mandatory fields)
                                                    </div>
                                                    <div class="nodata">
                                                        <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="addTable">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td valign="middle">
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="FormBorder">
                                                                        <tr>
                                                                            <td width="150">
                                                                                Select Location
                                                                            </td>
                                                                            <td width="5">
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlLocation" runat="server" Width="150px" AutoPostBack="true"
                                                                                    OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Select Shift
                                                                            </td>
                                                                            <td width="5px">
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlShift" runat="server" Width="150px">
                                                                                    <asp:ListItem Value="0" Text="-Select-"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Start Time
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtStartTime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" id="btnTimeHET1" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtStartTime','btnTimeHET1');" />
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
                                                                            <td>
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
                                                                            <td>
                                                                                <asp:TextBox ID="txtRecessFrom" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    height="12" id="btnTime2" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtRecessFrom','btnTime2');"
                                                                                    style="width: 11px">&nbsp; <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Recess To
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtRecessTo" runat="server"></asp:TextBox>
                                                                                &nbsp;&nbsp;<img src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtRecessTo','btnTimeRT');"
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
                                                                            <td>
                                                                                <asp:TextBox ID="txtEndTime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtEndTime','btnTimeET');"
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
                                                                            <td>
                                                                                <asp:TextBox ID="txtNLateEnd" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtNLateEnd','btnTimeNLE');"
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
                                                                            <td>
                                                                                <asp:TextBox ID="txtEarlyETime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtEarlyETime','btnTimeEET');"
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
                                                                            <td>
                                                                                <asp:TextBox ID="txtLateETime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtLateETime','btnTimeELT');"
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
                                                                            <td>
                                                                                <asp:TextBox ID="txtExLateExitTime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img
                                                                                    src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtExLateExitTime','btnTimeELET');"
                                                                                    id="btnTimeELET">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                <asp:CheckBox ID="cbWeekHalf1" runat="server" OnClick="ShowHide1();" Text="Time for Weekly HalfDay"
                                                                                    Font-Bold="true" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr13" runat="server" style="display: none;">
                                                                            <td>
                                                                                Start Time
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtHfStartTime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtHfStartTime','imgHfStartTime');"
                                                                                    id="imgHfStartTime">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr14" runat="server" style="display: none;">
                                                                            <td>
                                                                                Early Entry Time
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtHfEETime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtHfEETime','imgHfEETime');"
                                                                                    id="imgHfEETime">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr15" runat="server" style="display: none;">
                                                                            <td>
                                                                                Late Entry Time
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtHfLETime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtHfLETime','imgHfLETime');"
                                                                                    id="imgHfLETime">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr11" runat="server" style="display: none;">
                                                                            <td>
                                                                                Exit Time
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtHalfETime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtHalfETime','imgHalfETime');"
                                                                                    id="imgHalfETime">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr12" runat="server" style="display: none;">
                                                                            <td>
                                                                                Late Exit Time
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtHalfLateETime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img
                                                                                    src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabCreateOfficeTime_txtHalfLateETime','btnTimeHELT');"
                                                                                    id="btnTimeHELT">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <span>
                                                                                    <asp:Button ID="btnAdd" OnClientClick="return conformation(this);" Width="70px" runat="server"
                                                                                        Text="Add" OnClick="btnAdd_Click" />
                                                                                    <asp:Button ID="btnReset" OnClick="btnReset_Click" runat="server" Width="70px" Text="Reset" />
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel runat="server" HeaderText="EDIT" ID="TabEditoffice" TabIndex="1">
                                                <ContentTemplate>
                                                    <div class="nodata" align="center">
                                                        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#C00000"></asp:Label>
                                                    </div>
                                                    <div class="addTable">
                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="FormBorder">
                                                            <tr>
                                                                <td width="110px">
                                                                    <strong>Location Name</strong>
                                                                </td>
                                                                <td width="5px">
                                                                    <strong>:</strong>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlLoc" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlLoc_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="viewTable">
                                                        <asp:GridView ID="GVOfficeTime" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                            CellSpacing="0" HeaderStyle-Font-Bold="True" ItemStyle-VerticalAlign="Top" PagerStyle-HorizontalAlign="Right"
                                                            PagerStyle-Mode="NumericPages" PagerStyle-PageButtonCount="10" PageSize="10"
                                                            DataKeyNames="OfficeTimeID" OnRowDataBound="GVOfficeTime_RowDataBound">
                                                            <EmptyDataTemplate>
                                                                <div id="divCentr" style="text-align: center;">
                                                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="LightGray"
                                                                        Text="No Office Time Record Found ..."></asp:Label>
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="25px">
                                                                    <HeaderTemplate>
                                                                        <input type="checkbox" name="cbAll" value="cbAll" onclick="SelectAll(cbAll,'GVOfficeTime','form1');" /></HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll,'GVOfficeTime','form1');" /></ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="SL. No" ItemStyle-Width="40px"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Location" DataField="LocationName" />
                                                                <asp:BoundField HeaderText="Shift" DataField="ShiftName" />
                                                                <asp:BoundField HeaderText="Start Time" DataField="StartTime" />
                                                                <asp:BoundField HeaderText="End Time" DataField="EndTime" />
                                                                <asp:BoundField HeaderText="Recess From" DataField="RecessFrom" />
                                                                <asp:BoundField HeaderText="Recess To" DataField="RecessTo" />
                                                                <asp:BoundField HeaderText="Grace Time" DataField="GraceTime" />
                                                                <asp:TemplateField ItemStyle-Width="40px">
                                                                    <HeaderTemplate>
                                                                        Edit
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href="AdminOfficeTime.aspx?OID=<%#DataBinder.Eval(Container.DataItem,"OfficeTimeID") %>">
                                                                            <img alt="" src="../images/editIcon.gif" border="0" align="absmiddle" />
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="deletebtn" style="padding-left: 10px;">
                                                        <asp:Button ID="btndelete" runat="server" Text="Delete" OnClick="btndelete_Click"
                                                            OnClientClick="return CheckSelect();" />
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel runat="server" HeaderText="Add Exceptional Office Time" ID="TabExceptionalOffice"
                                                TabIndex="2">
                                                <ContentTemplate>
                                                    <div class="mandatory">
                                                        (* indicates mandatory fields)
                                                    </div>
                                                    <div class="nodata">
                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                    </div>
                                                    <div class="addTable">
                                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                                <td valign="middle">
                                                                    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="FormBorder">
                                                                        <tr>
                                                                            <td width="150">
                                                                                Select Location
                                                                            </td>
                                                                            <td width="5px">
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlExplocation" runat="server" Width="150px" OnSelectedIndexChanged="ddlExplocation_SelectedIndexChanged"
                                                                                    AutoPostBack="true">
                                                                                </asp:DropDownList>
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Date From :
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                                                                                <font color="#FF0000">*</font><a class="so-BtnLink" href="javascript:calClick();return false;"
                                                                                    onclick="calSwapImg('BTN_date1', 'img_Date_DOWN');showCalendar('form1','TabOfficeTime_TabExceptionalOffice_txtDateFrom','BTN_date1');return false;"
                                                                                    onmouseout="calSwapImg('BTN_date1', 'img_Date_UP',true);" onmouseover="calSwapImg('BTN_date1', 'img_Date_OVER',true);"><img
                                                                                        src="../jscript48/btn_date_up.gif" name="BTN_date1" width="20" height="14" border="0"
                                                                                        align="middle" id="BTN_date1" /></a>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Date To :
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>
                                                                                <font color="#FF0000">*</font><a class="so-BtnLink" href="javascript:calClick();return false;"
                                                                                    onclick="calSwapImg('BTN_date2', 'img_Date_DOWN');showCalendar('form1','TabOfficeTime_TabExceptionalOffice_txtDateTo','BTN_date2');return false;"
                                                                                    onmouseout="calSwapImg('BTN_date2', 'img_Date_UP',true);" onmouseover="calSwapImg('BTN_date2', 'img_Date_OVER',true);"><img
                                                                                        src="../jscript48/btn_date_up.gif" name="BTN_date2" width="20" height="14" border="0"
                                                                                        align="middle" id="BTN_date2" /></a>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Start Time :
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExStartTime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" id="btnExTimeHET1" onclick="showTimer('form1','TabOfficeTime_TabExceptionalOffice_txtExStartTime','btnExTimeHET1');" />
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
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlExgracetime" runat="server" Width="50px">
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
                                                                                Recess From :
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExRecessFrom" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    height="12" id="btnExTime2" onclick="showTimer('form1','TabOfficeTime_TabExceptionalOffice_txtExRecessFrom','btnExTime2');"
                                                                                    style="width: 11px">&nbsp; <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Recess To :
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExRecessTo" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabExceptionalOffice_txtExRecessTo','btnExTimeRT');"
                                                                                    id="btnExTimeRT">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Exit Time :
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExEndTime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabExceptionalOffice_txtExEndTime','btnExTimeET');"
                                                                                    id="btnExTimeET">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Late Exit Time:
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExNLateEnd" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabExceptionalOffice_txtExNLateEnd','btnExTimeNLE');"
                                                                                    id="btnExTimeNLE">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Early Entry Time :
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExEarlyETime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabExceptionalOffice_txtExEarlyETime','btnExTimeEET');"
                                                                                    id="btnExTimeEET">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Late Entry Time :
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExLateETime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabExceptionalOffice_txtExLateETime','btnExTimeELT');"
                                                                                    id="btnExTimeELT">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Extra Late Exit Time :
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExpExLateExitTime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img
                                                                                    src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabExceptionalOffice_txtExpExLateExitTime','btnExTimeELET');"
                                                                                    id="btnExTimeELET">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                Religion
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlReligion" runat="server" Width="135px">
                                                                                    <asp:ListItem Value="0" Text="Hindu"></asp:ListItem>
                                                                                    <asp:ListItem Value="1" Text="Muslim"></asp:ListItem>
                                                                                    <asp:ListItem Value="2" Text="Christian"></asp:ListItem>
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="3">
                                                                                <asp:CheckBox ID="cbWeekHalf2" runat="server" OnClick="ShowHide2();" Text="Time for Weekly HalfDay"
                                                                                    Font-Bold="true" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr21" runat="server" style="display: none;">
                                                                            <td>
                                                                                Exit Time:
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExHalfETime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img src="../images/clock_icon.gif"
                                                                                    width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabExceptionalOffice_txtExHalfETime','btnExTimeHET');"
                                                                                    id="btnExTimeHET">
                                                                                <font color="#FF0000">*</font>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="tr22" runat="server" style="display: none;">
                                                                            <td>
                                                                                Late Exit Time:
                                                                            </td>
                                                                            <td>
                                                                                :
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtExHalfLateETime" runat="server"></asp:TextBox>&nbsp;&nbsp;<img
                                                                                    src="../images/clock_icon.gif" width="11" height="12" onclick="showTimer('form1','TabOfficeTime_TabExceptionalOffice_txtExHalfLateETime','btnExTimeHELT');"
                                                                                    id="btnExTimeHELT">
                                                                                <font color="#FF0000">*</font>
                                                                                <asp:HiddenField ID="hidExpval" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                &nbsp;
                                                                            </td>
                                                                            <td>
                                                                                <span>&nbsp;<asp:Button ID="BtnExAdd" runat="server" Text="Add" OnClick="BtnExAdd_Click" />
                                                                                    &nbsp;&nbsp;
                                                                                    <asp:Button ID="btnExReset" OnClick="btnExReset_Click" runat="server" Text="Reset" />
                                                                                </span>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ContentTemplate>
                                            </cc1:TabPanel>
                                            <cc1:TabPanel runat="server" HeaderText="View Exceptional Office Time" ID="TabExceptionalView"
                                                TabIndex="3">
                                                <ContentTemplate>
                                                    <div class="nodata" align="center">
                                                        <asp:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="#C00000"></asp:Label>
                                                    </div>
                                                    <div class="addTable">
                                                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" class="FormBorder">
                                                            <tr>
                                                                <td width="150">
                                                                    Location Name
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlViewExplocation" runat="server" Width="150px" AutoPostBack="true"
                                                                        OnSelectedIndexChanged="ddlViewExplocation_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                    <font color="#FF0000">*</font>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div class="viewTable">
                                                        <asp:GridView ID="GvExpOffView" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                            CellSpacing="0" HeaderStyle-Font-Bold="True" ItemStyle-VerticalAlign="Top" PagerStyle-HorizontalAlign="Right"
                                                            PagerStyle-Mode="NumericPages" PagerStyle-PageButtonCount="10" PageSize="10"
                                                            DataKeyNames="OfficeTimeID" EmptyDataText="No Records" OnRowDataBound="GvExpOffView_RowDataBound">
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-Width="25px">
                                                                    <HeaderTemplate>
                                                                        <input type="checkbox" name="cbAll" value="cbAll" onclick="SelectAll(cbAll,'GvExpOffView','form1');" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="cbItem" runat="server" onclick="return deSelectHeader(cbAll,'GvExpOffView','form1');" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="SL. No" ItemStyle-Width="40px"></asp:BoundField>
                                                                <asp:BoundField HeaderText="Location" DataField="LocationName" />
                                                                <asp:BoundField HeaderText="From Date" DataField="DateFrom" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField HeaderText="To Date" DataField="DateTo" DataFormatString="{0:dd-MMM-yyyy}" />
                                                                <asp:BoundField HeaderText="Start Time" DataField="StartTime" />
                                                                <asp:BoundField HeaderText="End Time" DataField="EndTime" />
                                                                <asp:BoundField HeaderText="Recess From" DataField="RecessFrom" />
                                                                <asp:BoundField HeaderText="Recess To" DataField="RecessTo" />
                                                                <asp:TemplateField ItemStyle-Width="40px">
                                                                    <HeaderTemplate>
                                                                        Edit
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <a href="AdminOfficeTime.aspx?EID=<%#DataBinder.Eval(Container.DataItem,"OfficeTimeID") %>">
                                                                            <img alt="" src="../images/editIcon.gif" border="0" align="absmiddle" />
                                                                        </a>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <div class="deletebtn" style="margin-left: 10px;">
                                                        <asp:Button ID="btnVDelete" runat="server" Text="Delete" OnClientClick="return CheckSelect();"
                                                            OnClick="btnVDelete_Click" />
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
