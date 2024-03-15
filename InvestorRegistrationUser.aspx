<%--'*******************************************************************************************************************
' File Name         : InvestorRegistrationUser.aspx
' Description       : Registration of Investor
' Created by        : AMit Sahoo
' Created On        : 18 July 2017
' Modification History:

' <CR no.>              <Date>                <Modified by>        <Modification Summary>                                         <Instructed By>                                                     
    1                 25-Aug-2018             Sushant Jena         PAN based registration with parent child unit creation.        Smruti Ranjan Nayak
    2                 30-Aug-2019             Sushant Jena         Changes in Investment Level due to AIM Integration             Rama Rao Teki
'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvestorRegistrationUser.aspx.cs"
    Inherits="InvestorRegistrationUser" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/includes/webdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/webheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/simplefooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc2" %>



<!DOCTYPE html>
<html>
<head id="Head1" runat="server">

    <title></title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="css/custom.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function inputLimiter(e, allow) {
            var AllowableCharacters = '';

            if (allow == 'NameCharacters') {
                AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'NameCharactersAndNumbers') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
            }
            if (allow == 'Numbers') {
                AllowableCharacters = '1234567890';
            }
            if (allow == 'Decimal') {
                AllowableCharacters = '1234567890.';
            }
            if (allow == 'Email') {
                AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._';
            }
            if (allow == 'Address') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
            }
            if (allow == 'DateFormat') {
                AllowableCharacters = '1234567890/-';
            }
            if (allow == 'NumbersSSN') {
                AllowableCharacters = '1234567890-';
            }
            if (allow == 'RawMetrial') {
                AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
            }
            var k;
            k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
            if (k != 13 && k != 8 && k != 0) {
                if ((e.ctrlKey == false) && (e.altKey == false)) {
                    return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
                }
                else {
                    return true;
                }
            }
            else {
                return true;
            }
        }

        /*--------------------------------------------------------*/

        function pageLoad(sender, args) {

            $('#Txt_Email_Id').change(function () {
                var email = $('#Txt_Email_Id').val();
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (filter.test(email) == false) {
                    jAlert('<strong>Invalid email format !</strong>', projname);
                    $("#Txt_Email_Id").focus();
                    $('#Txt_Email_Id').val('');
                    return false;
                }
            });


            //$('#Txt_Email_Id').change(function () {
            //    checkUserNameAvail();
            //});

            //function checkUserNameAvail() {
            //    //debugger;
            //    $("#divAvailImg").html("");
            //    var email = $('#Txt_Email_Id').val();
            //    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
            //    if (filter.test(email) == false) {
            //        jAlert('<strong>Invalid email address !</strong>', projname);
            //        $("#Txt_Email_Id").focus();
            //        $('#Txt_Email_Id').val('');
            //        return false;
            //    }

            //    $.ajax({
            //        type: "POST",
            //        url: "InvestorRegistrationUser.aspx/checkMailAvail", //It calls our web method  
            //        contentType: "application/json; charset=utf-8",
            //        data: "{'strEmailId':'" + email + "'}",
            //        dataType: "json",
            //        success: function (msg) {
            //            var response = JSON.stringify(msg);
            //            //  alert(msg.d);
            //            if (msg.d == '1') {
            //                //alert('This email is already exists !!');
            //                $("#Txt_Email_Id").val('');
            //                $("#Txt_Email_Id").focus();
            //                $("#divAvailImg").html("<img src='images/cancel-square.png' />");
            //                //$("#divAvailImg").html("<img src='images/cancel-square.png' /> <span style='color:red;'>That email is taken.Try another.</span>");
            //            }
            //            else {
            //                $("#divAvailImg").html("<img src='images/active.png' />");
            //                //$("#divAvailImg").html("<img src='images/active.png' /> <span style='color:green;'>Available</span>");
            //            }
            //        },
            //        error: function (d) {
            //        }
            //    });
            //}
        }

    </script>
    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        /*--------------------------------------------------------------*/

        function ValidateAtCheckMailBtn() {
            if (blankFieldValidation('Txt_PAN', 'PAN', projname) == false) {
                return false;
            }
            if ($('#Txt_PAN').val().length != 10) {
                jAlert('<strong>PAN should be 10 digits.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_PAN").focus(); });
                return false;
            }
            var txt_pan = $('#Txt_PAN').val().toUpperCase();
            var checkval = /^[A-Z]{5}\d{4}[A-Z]{1}$/;
            if (checkval.test(txt_pan) == false) {
                jAlert('<strong>Invalid PAN card number !</strong>', projname);
                $('#Txt_PAN').val('');
                $("#popup_ok").click(function () { $("#Txt_PAN").focus(); });
                return false;
            }

            if (blankFieldValidation('Txt_Panname', 'PAN holder name', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidation1st('Txt_Panname', 'PAN holder name', projname) == false) {
                return false;
            }
            if (blankFieldValidation('Txt_dob', 'Date Of Birth', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidation1st('Txt_dob', 'Date Of Birth', projname) == false) {
                return false;
            }
        }

        /*--------------------------------------------------------------*/

        function Validate() {

            if (blankFieldValidation('Txt_PAN', 'PAN', projname) == false) {
                return false;
            }
            
            if ($('#Txt_PAN').val().length != 10) {
                jAlert('<strong>PAN should be 10 digits.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_PAN").focus(); });
                return false;
            }
            var txt_pan = $('#Txt_PAN').val().toUpperCase();
            var checkval = /^[A-Z]{5}\d{4}[A-Z]{1}$/;
            if (checkval.test(txt_pan) == false) {
                jAlert('<strong>Invalid PAN card number !</strong>', projname);
                $('#Txt_PAN').val('');
                $("#popup_ok").click(function () { $("#Txt_PAN").focus(); });
                return false;
            }

            if (blankFieldValidation('Txt_Panname', 'PAN holder name', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidation1st('Txt_Panname', 'PAN holder name', projname) == false) {
                return false;
            }

            if (blankFieldValidation('Txt_dob', 'Date Of Birth', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidation1st('Txt_dob', 'Date Of Birth', projname) == false) {
                return false;
            }
            if ($('#Txt_User_Id').val() == "") {
                jAlert('<strong>Please click on Check Availability button to validate PAN !</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_PAN").focus(); });
                return false;
            }
            if (DropDownValidation('DrpDwn_Salutation', '0', 'prefix for name of applicant', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_Salutation").focus(); });
                return false;
            }
            if (blankFieldValidation('Txt_First_Name', 'First name', projname) == false) {
                return false;
            }
            //if (blankFieldValidation('Txt_Last_Name', 'Last name', projname) == false) {
            //    return false;
            //}
            if (blankFieldValidation('Txt_Address', 'Address', projname) == false) {
                return false;
            }
            if (blankFieldValidation('Txt_Mobile_No', 'Mobile number', projname) == false) {
                return false;
            }
            if ($('#Txt_Mobile_No').val().substring(0, 1) == '0') {
                jAlert('<strong>Mobile number should not be start with zero !</strong>', projname);
                $('#Txt_Mobile_No').val('');
                $('#Txt_Mobile_No').focus();
                return false;
            }
            if ($('#Txt_Mobile_No').val().length != 10) {
                jAlert('<strong>Mobile number should be 10 digits !</strong>', projname);
                $("#Txt_Mobile_No").focus();
                return false;
            }
            if (WhiteSpaceValidation1st('Txt_Mobile_No', 'Mobile number', projname) == false) {
                return false;
            }
            if (blankFieldValidation('Txt_Email_Id', 'Email id', projname) == false) {
                return false;
            }
            if ($('#Txt_Email_Id').val() != "") {
                var email = $('#Txt_Email_Id').val();
                var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
                if (filter.test(email) == false) {
                    jAlert('<strong>Invalid email address !</strong>', projname);
                    $("#Txt_Email_Id").focus();
                    return false;
                }
            }

            if (blankFieldValidation('Txt_Unit_Name', 'Unit name', projname) == false) {
                return false;
            }
            if (DropDownValidation('DrpDwn_Invest_Level', '0', 'Investment level', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_Invest_Level").focus(); });
                return false;
            }
            if (blankFieldValidation('Txt_Site_Loc', 'Proposed site location', projname) == false) {
                return false;
            }

            if ($('#DrpDwn_Invest_Level').val() == "1") {

                if (DropDownValidation('DrpDwn_License_Type', '0', 'IEM/Production Certificate', projname) == false) {
                    $("#popup_ok").click(function () { $("#DrpDwn_License_Type").focus(); });
                    return false;
                }
                if (blankFieldValidation('Txt_EIN_IEM', 'IEM/Production Certificate number', projname) == false) {
                    return false;
                }
                if ($('#FileUpload_Licence_Doc').val() == "") {
                    jAlert('<strong>Please upload document in support of IEM/Production Certificate !</strong>');
                    $("#FileUpload_Licence_Doc").focus();
                    return false;
                }
            }

            if ($('#DrpDwn_Invest_Level').val() == "2") {

                if (DropDownValidation('DrpDwn_License_Type', '0', 'EIN/Production Certificate/Udyog Aadhaar/Udyam Registration', projname) == false) {
                    $("#popup_ok").click(function () { $("#DrpDwn_License_Type").focus(); });
                    return false;
                }
                if (blankFieldValidation('Txt_EIN_IEM', 'EIN/Production Certificate/Udyog Aadhaar/Udyam Registration number', projname) == false) {
                    return false;
                }
                if ($('#FileUpload_Licence_Doc').val() == "") {
                    jAlert('<strong>Please upload document in support of EIN/Production Certificate/Udyog Aadhaar/Udyam Registration !</strong>');
                    $("#FileUpload_Licence_Doc").focus();
                    return false;
                }
            }

            //            if ($('#DrpDwn_Invest_Level').val() == "1") {

            //                if (DropDownValidation('DrpDwn_License_Type', '0', 'IEM/Production Certificate', projname) == false) {
            //                    $("#popup_ok").click(function () { $("#DrpDwn_License_Type").focus(); });
            //                    return false;
            //                }
            //                if (blankFieldValidation('Txt_EIN_IEM', 'IEM/Production Certificate number', projname) == false) {
            //                    return false;
            //                }
            //                if ($('#FileUpload_Licence_Doc').val() == "") {
            //                    jAlert('<strong>Please upload document in support of IEM/Production Certificate !</strong>');
            //                    $("#FileUpload_Licence_Doc").focus();
            //                    return false;
            //                }
            //            }

            //            if ($('#DrpDwn_License_Type').val() != "0") {

            //                if (blankFieldValidation('Txt_EIN_IEM', 'EIN/PC/Udyog Aadhaar/Udyam Registration number', projname) == false) {
            //                    return false;
            //                }

            //                if ($('#FileUpload_Licence_Doc').val() == "") {
            //                    jAlert('<strong>Please upload document in support of EIN/PC/Udyog Aadhaar/Udyam Registration !</strong>');
            //                    $("#FileUpload_Licence_Doc").focus();
            //                    return false;
            //                }
            //            }

            if (DropDownValidation('DrpDwn_District', '0', 'district', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_District").focus(); });
                return false;
            }
            if (DropDownValidation('DrpDwn_Block', '0', 'block', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_Block").focus(); });
                return false;
            }
            if (DropDownValidation('DrpDwn_Sector', '0', 'sector', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_Sector").focus(); });
                return false;
            }
            if (DropDownValidation('DrpDwn_Sub_Sector', '0', 'sub sector', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_Sub_Sector").focus(); });
                return false;
            }

            if (WhiteSpaceValidation1st('Txt_GSTIN', 'GSTIN', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_GSTIN', 'GSTIN', projname) == false) {
                return false;
            }
            if ($('#Txt_GSTIN').val() != "") {
                if ($("#Txt_GSTIN").val().length < 15) {
                    jAlert('<strong>GST identification no. can not be less then 15 characters !</strong>');
                    $("#Txt_GSTIN").focus();
                    return false;
                }
            }

            //  var GSTN = /^([0-9]){2}([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}([0-9]){1}([a-zA-Z]){1}([0-9]){1}?$/;
            //  if (document.getElementById("txtGSTIN").value.search(GSTN) == -1) {
            //      jAlert('<strong>Invalid GST Identification No.</strong>', projname);
            //      $(document.getElementById("txtGSTIN")).val('');
            //      $(document.getElementById("txtGSTIN")).focus();
            //      return false;
            //  }



            

            
           


            
            

           



            if (blankFieldValidation('Txt_User_Id', 'User ID', projname) == false) {
                return false;
            }
            if (blankFieldValidation('Txt_Pwd', 'Password', projname) == false) {
                return false;
            }
            if (!checkPassword()) {
                return false;
            }
            if (blankFieldValidation('Txt_Conf_Pwd', 'Confirm password', projname) == false) {
                return false;
            }
            if (document.getElementById("Txt_Pwd").value != document.getElementById("Txt_Conf_Pwd").value) {
                document.getElementById("Txt_Conf_Pwd").focus();
                jAlert("<strong>Confirm password should be same as password !</strong>", projname);
                return false;
            }
            if (DropDownValidation('DrpDwn_Question', '0', 'security question', projname) == false) {
                $("#popup_ok").click(function () { $("#DrpDwn_Question").focus(); });
                return false;
            }
            if (blankFieldValidation('Txt_Answer', 'Answer', projname) == false) {
                return false;
            }
            //            if (blankFieldValidation('Txt_Captcha', 'Captcha', projname) == false) {
            //                return false;
            //            }



        }

        /*--------------------------------------------------------------*/

        function checkPassword() {
            var txtNewPsw = document.getElementById("Txt_Pwd");
            var pwdVal = txtNewPsw.value;
            var illegalChars = /[\W_]g/;
            var re = /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,14}$/
            if (!re.test(pwdVal)) {
                jAlert("<strong>Password must contain atleast one uppercase letter,one lowercase letter,one number and one special character and length must be between 8-14 characters !</strong>", projname);
                txtNewPsw.focus();
                return false;
            }
            else {
                return true;
            }
        }

        /*--------------------------------------------------------------*/
        ////// FileUpload validation

        function validateFile(e) {
            var ids = e.id;
            var fileExtension = ['pdf'];
            if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                jAlert('<strong>Only .pdf format is allowed.</strong>', projname);
                $("#" + ids).val(null);
                return false;
            }
            else {
                if ((e.files[0].size > parseInt(4 * 1024 * 1024)) && ($("#" + ids).val() != '')) {

                    jAlert('<strong>File size must be less then 4 MB ! </strong>', projname);
                    $("#" + ids).val(null);
                    //e.preventDefault();
                    return false;
                }
            }
        }

        /*--------------------------------------------------------------*/

        function showDocName() {

            var docName = $('#DrpDwn_License_Type').val();

            if (docName != '0') {

                $('#Lbl_Doc_Name').html("Upload " + docName + " Document");
$("#Txt_EIN_IEM").attr('title', "Enter " + docName + " Number Here.");

                if (docName == "EIN") {
                    $('#EINNo').show();
                    $('#IEMNo').hide();
                    $('#UAadhaarNo').hide();
                    $('#UdyamReg').hide();
                }
                else if (docName == "IEM") {
                    $('#EINNo').hide();
                    $('#IEMNo').show();
                    $('#UAadhaarNo').hide();
                    $('#UdyamReg').hide();
                }
                else if (docName == "Udyog Aadhaar") {
                    $('#EINNo').hide();
                    $('#IEMNo').hide();
                    $('#UAadhaarNo').show();
                    $('#UdyamReg').hide();
                }
                else if (docName == "Production Certificate") {
                    $('#EINNo').hide();
                    $('#IEMNo').hide();
                    $('#UAadhaarNo').hide();
                    $('#UdyamReg').hide();
                }
                else if (docName == "Udyam Registration") {
                    $('#EINNo').hide();
                    $('#IEMNo').hide();
                    $('#UAadhaarNo').hide();
                    $('#UdyamReg').show();
                }
                else {
                    $('#EINNo').show();
                    $('#IEMNo').show();
                    $('#UAadhaarNo').show();
                    $('#UdyamReg').show();
                }
            }
            else {
                $('#Lbl_Doc_Name').html("Upload EIN/IEM/Udyog Aadhaar/Production Certificate/Udyam Registration Document");
                $("#Txt_EIN_IEM").attr('title', "Enter EIN/IEM/Udyog Aadhaar/Production Certificate/Udyam Registration Number Here.");
                $('#EINNo').show();
                $('#IEMNo').show();
                $('#UAadhaarNo').show();
                $('#UdyamReg').show();
            }
        }

        /*--------------------------------------------------------------*/

        function limitText(limitField, limitCount, limitNum) {
            if (limitField.value.length > limitNum) {
                limitField.value = limitField.value.substring(0, limitNum);
            } else {
                limitCount.value = limitNum - limitField.value.length;
            }
        }

        /*--------------------------------------------------------------*/

        


        /*--------------------------------------------------------------*/

    </script>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $('.fieldinfo').attr('tabindex', '-1')

            //            $("#DrpDwn_Invest_Level").change(function () {
            //                debugger;
            //                var invLev = $('#DrpDwn_Invest_Level').val();
            //                if (invLev == 2) {
            //                    $('#spanMandatory1').hide();
            //                    $('#spanMandatory2').hide();
            //                    $('#spanMandatory3').hide();
            //                }
            //                else {
            //                    $('#spanMandatory1').show();
            //                    $('#spanMandatory2').show();
            //                    $('#spanMandatory3').show();
            //                }
            //            });



            //$('.datePicker').datepicker({
            //    format: "dd-M-yyyy",
            //    changeMonth: true,
            //    changeYear: true,
            //    autoclose: true
            //});
            //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            //function EndRequestHandler(sender, args) {
            //    $('.datePicker').datepicker({
            //        format: 'dd-M-yyyy',
            //        autoclose: true
            //    });
            //}



            $('.datePicker').datepicker({
                format: "dd-M-yyyy",
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
            

           
        });


    </script>


    
    <script type="text/javascript">
        $(function () {
            $("#DrpDwn_Entity_Type").change(function () {

                if ($('#DrpDwn_Entity_Type').val() !== "1") {

                    $('#spnrequird').css('display', 'none');

                }
                else {
                    $('#spnrequird').show();
                }
                

            });

            $("#Txt_Pwd").bind("keyup", function () {

                //TextBox left blank.
                if ($(this).val().length == 0) {
                    $("#password_strength").html("");
                    return;
                }

                //Regular Expressions.
                var regex = new Array();
                regex.push("[A-Z]"); //Uppercase Alphabet.
                regex.push("[a-z]"); //Lowercase Alphabet.
                regex.push("[0-9]"); //Digit.
                regex.push("[$@$!%*#?&]"); //Special Character.

                var passed = 0;

                //Validate for each Regular Expression.
                for (var i = 0; i < regex.length; i++) {
                    if (new RegExp(regex[i]).test($(this).val())) {
                        passed++;
                    }
                }


                //Validate for length of Password.
                if (passed > 2 && $(this).val().length > 8) {
                    passed++;

                }

                //Display status.
                var color = "";
                var strength = "";
                switch (passed) {
                    case 0:
                    case 1:
                    case 2:
                        strength = "Weak";
                        color = "red";
                        break;
                    case 3:
                    case 4:
                        strength = "Good";
                        color = "blue";
                        break;
                    case 5:
                    case 6:
                        strength = "Strong";
                        color = "green";
                        break;
                    case 7:
                        strength = "Very Strong";
                        color = "darkgreen";
                        break;
                }
                $("#password_strength").html(strength);
                $("#password_strength").css("color", color);
            });


        });
    </script>
    <style type="text/css">
        .footer-top {
            display: none;
        }

        .upper-case {
            text-transform: uppercase;
        }
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 900px;
            height: 550px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <uc2:header ID="header" runat="server" />
        <div class="container wrapper">
            <div class="registration-div">
                <div id="exTab1" class="">
                    <div runat="server" visible="true" id="divScrollingText" style="color: #0d3fd1; font-size: 14px; font-family: Verdana; font-style:italic; font-weight: 600; padding-top: 5px;">
                     </div>
                     <%--<marquee style=" overflow: hidden; position: relative; background: #fefefe;
                    color: Red; font-style:italic; ">Due to maintenance activity, Online PAN Verification services will not be available to PAN verification users between 1AM to 4AM on December 10, 2022.</marquee>--%>
                    <div class="wizard">
                        <h2 class="pageTittle">
                            <div class="ring-container">
                                <div class="ringring"></div>
                                <div class="circle"></div>
                            </div>

                            &nbsp;Industrial User Registration <span class="mandatoryspan pull-right">( * ) Marked fields are mandatory</span></h2>
                       

                        <div class="wizard-inner">
                            <div class="connecting-line">
                            </div>
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation" class="active"><a href="#step2" data-toggle="tab" aria-controls="Profile Creation"
                                    role="tab" title="Profile Creation"><span class="round-tab"><i class="glyphicon glyphicon-pencil"></i></span><small>Profile Creation</small> </a></li>
                                <li role="presentation" class="disabled"><a href="#step3" data-toggle="tab" aria-controls="OTP Confirmation"
                                    role="tab" title="OTP Confirmation"><span class="round-tab"><i class="glyphicon glyphicon-picture"></i></span><small>OTP Confirmation</small> </a></li>
                                <li role="presentation" class="disabled"><a href="#complete" data-toggle="tab" aria-controls="Success"
                                    role="tab" title="Complete"><span class="round-tab"><i class="glyphicon glyphicon-ok"></i></span><small>Success</small> </a></li>
                            </ul>
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="step2">
                                <div class="tab-pane active" role="tabpanel" id="Div1">
                                    <div class="form-sec">
                                        <div class="form-header">
                                            <h2>PAN Details</h2>
                                        </div>
                                        <div class="form-body">
                                            <div class="formbodycontent">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="email" class="col-sm-3 col-md-2">
                                                            Enter Company PAN
                                                        </label>
                                                        <div class="col-sm-6 col-md-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="Txt_PAN" CssClass="form-control" runat="server" MaxLength="10" ToolTip="Enter Company PAN Number Here."></asp:TextBox>
                                                            <span class="mandetory">*</span>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Please enter the Company PAN number !">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </div>
                                                        <%--<div class="col-sm-3 col-md-6">
                                                            <asp:Button ID="Btn_PAN_Validate" runat="server" Text="Validate & Check Availability"
                                                                OnClientClick="return ValidateAtCheckMailBtn();" CssClass=" btn btn-success"
                                                                OnClick="Btn_PAN_Validate_Click" ToolTip="Click here to validate PAN." />&nbsp;&nbsp;
                                                        <asp:Label ID="Lbl_Msg" runat="server"></asp:Label>
                                                            <asp:Image ID="Img_Success" runat="server" ImageUrl="~/images/successfulTick.png" />
                                                        </div>--%>

                                                        <label for="email" class="col-sm-3 col-md-2">
                                                            Enter the PAN Holder Name
                                                        </label>
                                                        <div class="col-sm-6 col-md-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="Txt_Panname" CssClass="form-control" runat="server" ToolTip="Enter PAN Holder Name Here." Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                                            <span class="mandetory">*</span>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Please enter PAN Holder Name !">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </div>

                                                    </div>

                                                    <div class="row">
                                                        <label for="email" class="col-sm-3 col-md-2">
                                                            Enter the DOB (dd/mm/yyyy)
                                                        </label>

                                                        <div class="col-sm-6 col-md-4">
                                                            <span class="colon">:</span>
                                                            <div class="input-group date datePicker">
                                                                <asp:TextBox ID="Txt_dob" CssClass="form-control" runat="server" TabIndex="1" AutoComplete="Off"></asp:TextBox>

                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                            </div>
                                                        </div>


                                                        <div class="col-sm-3 col-md-6">
                                                            <asp:Button ID="Btn_PAN_Validate" runat="server" Text="Validate & Check Availability"
                                                                OnClientClick="return ValidateAtCheckMailBtn();" CssClass=" btn btn-success"
                                                                OnClick="Btn_PAN_Validate_Click" ToolTip="Click here to validate PAN." />&nbsp;&nbsp;
                                                        <asp:Label ID="Lbl_Msg" runat="server"></asp:Label>
                                                            <asp:Image ID="Img_Success" runat="server" ImageUrl="~/images/successfulTick.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-sec">
                                        <div class="form-header">
                                            <h2>Investor Details</h2>
                                        </div>
                                        <div class="form-body">
                                            <div class="formbodycontent">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <div class="col-sm-12">
                                                            <div class="row">
                                                                <label for="Contact" class="col-sm-3 col-md-2">
                                                                    Name of Applicant
                                                                </label>
                                                                <div class="col-sm-1  padding-right-0">
                                                                    <span class="colon">:</span>
                                                                    <asp:DropDownList ID="DrpDwn_Salutation" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="Mr" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Ms" Value="2"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <span class="mandetory">*</span>
                                                                </div>
                                                                <div class="col-sm-9">
                                                                    <asp:TextBox ID="Txt_First_Name" autocomplete="off" CssClass="form-control" runat="server"
                                                                        placeholder="Full name of the applicant" MaxLength="100" ToolTip="Enter full name of the applicant here."></asp:TextBox>
                                                                    <span class="mandetory">*</span>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                                        TargetControlID="Txt_First_Name" FilterMode="ValidChars" FilterType="Custom,LowercaseLetters,UppercaseLetters"
                                                                        ValidChars="a-zA-Z .">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Mobile" class="col-sm-3 col-md-2">
                                                            Address
                                                        </label>
                                                        <div class="col-sm-9 col-md-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="Txt_Address" autocomplete="off" CssClass="form-control height95"
                                                                TextMode="MultiLine" onKeyUp="limitText(this,this.form.count,250);" onKeyDown="limitText(this,this.form.count,250);"
                                                                MaxLength="250" runat="server" ToolTip="Enter applicant's address here."></asp:TextBox>
                                                            <span class="mandetory">*</span> <span class="mandatory" style="font-size: 14px; color: red"><small>Maximum
                                                                <input name="count" class="inputCss" readonly="readonly" style="font-weight: bold; color: red; width: 26px;"
                                                                    type="text" value="250" tabindex="-1" />
                                                                Characters Left</small></span>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                                TargetControlID="Txt_Address" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                InvalidChars="&quot;'<>&;">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="col-md-6 col-sm-12">
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Mobile" class="col-sm-3 col-md-4">
                                                                        Mobile Number
                                                                    </label>
                                                                    <div class="col-sm-9 col-md-8">
                                                                        <span class="colon">:</span>
                                                                        <asp:TextBox ID="Txt_Mobile_No" CssClass="form-control" runat="server" MaxLength="10"
                                                                            autocomplete="off" ToolTip="Enter applicant's mobile number here."></asp:TextBox>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender_txtMobileNo" runat="server"
                                                                            Enabled="True" FilterType="Numbers" TargetControlID="Txt_Mobile_No" />
                                                                        <a data-toggle="tooltip" class="fieldinfo" title="This number will be used for all future official communication via SMS ,don't prefix with +91 or 0!">
                                                                            <i class="fa fa-question-circle" aria-hidden="true"></i></a><span class="mandetory">*</span>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                                            TargetControlID="Txt_Mobile_No" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                            InvalidChars="&quot;'<>&;">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Email" class="col-sm-3 col-md-2">
                                                            Email Id
                                                        </label>
                                                        <div class="col-sm-9 col-md-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="Txt_Email_Id" autocomplete="off" CssClass="form-control height95"
                                                                MaxLength="250" runat="server" ToolTip="Enter applicant's email id here."></asp:TextBox>
                                                            <span class="mandetory">*</span>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                                TargetControlID="Txt_Email_Id" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                InvalidChars="&quot;'<>&;">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>
                                                        <div class="col-sm-9 col-md-4" id="divAvailImg">
                                                        </div>
                                                        <div class="col-md-6 col-sm-12" id="proprietor" runat="server" visible="false">
                                                            <div class="form-group">
                                                                <div class="row">
                                                                    <label for="Contact" class="col-sm-3 col-md-4">
                                                                        Proprietorship Name
                                                                    </label>
                                                                    <div class="col-sm-9 col-md-8">
                                                                        <span class="colon">:</span>
                                                                        <asp:TextBox ID="Txt_Proprietorship_Name" autocomplete="off" CssClass="form-control"
                                                                            runat="server" placeholder="Proprietorship Name" MaxLength="50" Enabled="false"></asp:TextBox><span
                                                                                class="mandetory">*</span>
                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                            TargetControlID="Txt_Proprietorship_Name" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                            InvalidChars="&quot;'<>&;/\|{}[]">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                        <span class="mandetory">*</span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-sec">
                                        <div class="form-header">
                                            <h2>Unit Details</h2>
                                        </div>
                                        <div class="form-body">
                                            <div class="formbodycontent">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="IName" class="col-sm-3 col-md-2">
                                                            Unit Name
                                                        </label>
                                                        <div class="col-md-1 col-sm-3  padding-right-0">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="DrpDwn_Unit_Prefix" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="M/s" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="mandetory">*</span>
                                                        </div>
                                                        <div class="col-sm-9">
                                                            <asp:TextBox ID="Txt_Unit_Name" autocomplete="off" CssClass="form-control" runat="server"
                                                                MaxLength="50" placeholder="Name of the Unit" ToolTip="Enter name of the unit here."></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="Fill Unit Name (Please don't add the prefix M/S to the Unit Name)">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                TargetControlID="Txt_Unit_Name" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                InvalidChars="&quot;<>;">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <span class="mandetory">*</span>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Category" class="col-sm-3 col-md-2">
                                                            Investment Level
                                                        </label>
                                                        <div class="col-sm-9 col-md-4">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="DrpDwn_Invest_Level" runat="server" CssClass="form-control"
                                                                AutoPostBack="true" OnSelectedIndexChanged="DrpDwn_Invest_Level_SelectedIndexChanged" ToolTip="Select investment level here.">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="Project Cost >= 50 crore" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="Project cost upto < 50 crore" Value="2"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="mandetory">*</span>
                                                        </div>

                                                        <label for="District" class="col-sm-3 col-md-2">
                                                            District
                                                        </label>
                                                        <div class="col-sm-9 col-md-4">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="DrpDwn_District" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                OnSelectedIndexChanged="DrpDwn_District_SelectedIndexChanged" ToolTip="Select district name here.">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="mandetory">*</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="SiteLocation" class="col-sm-3 col-md-2">
                                                            Proposed Site Location
                                                        </label>
                                                        <div class="col-sm-9 col-md-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="Txt_Site_Loc" autocomplete="off" CssClass="form-control height95"
                                                                TextMode="MultiLine" onKeyUp="limitText(this,this.form.count1,250);" onKeyDown="limitText(this,this.form.count1,250);"
                                                                MaxLength="250" runat="server" ToolTip="Enter the proposed location of the unit here."></asp:TextBox>
                                                            <span class="mandetory">*</span> <span class="mandatory" style="font-size: 14px; color: red"><small>Maximum
                                                                <input name="count1" class="inputCss" readonly="readonly" style="font-weight: bold; color: red; width: 26px;"
                                                                    type="text" value="250" tabindex="-1" />
                                                                Characters Left</small></span>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                                TargetControlID="Txt_Site_Loc" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                InvalidChars="&quot;'<>&;">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </div>

                                                        <label for="Block" class="col-sm-3 col-md-2">
                                                            Block
                                                        </label>
                                                        <div class="col-sm-9 col-md-4">
                                                            <span class="colon">:</span>
                                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="DrpDwn_Block" runat="server" CssClass="form-control" ToolTip="Select block name here.">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="DrpDwn_District" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            <span class="mandetory">*</span>
                                                        </div>
                                                        <label for="Sector" class="col-sm-3 col-md-2 margin-top10">
                                                            Sector
                                                        </label>
                                                        <div class="col-sm-9 col-md-4 margin-top10">
                                                            <span class="colon">:</span>
                                                            <asp:DropDownList ID="DrpDwn_Sector" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                OnSelectedIndexChanged="DrpDwn_Sector_SelectedIndexChanged" ToolTip="Select sector name here.">
                                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                            </asp:DropDownList>
                                                            <span class="mandetory">*</span>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="EINIEM" class="col-sm-3 col-md-2">
                                                            EIN / IEM / Udyog Aadhaar / Production Certificate / Udyam Registration
                                                        </label>
                                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                            <ContentTemplate>
                                                                <div class="col-md-1 col-sm-2 padding-right-0">
                                                                    <span class="colon">:</span>
                                                                    <asp:DropDownList ID="DrpDwn_License_Type" runat="server" CssClass="form-control"
                                                                        onchange="return showDocName();" ToolTip="Select licence type here.">
                                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                        <asp:ListItem>EIN</asp:ListItem>
                                                                        <asp:ListItem>IEM</asp:ListItem>
                                                                        <asp:ListItem>Production Certificate</asp:ListItem>
                                                                        <asp:ListItem>Udyog Aadhaar</asp:ListItem>
                                                                        <asp:ListItem>Udyam Registration</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <span class="mandetory" id="spanMandatory1" runat="server">*</span>
                                                                </div>
                                                                <div class="col-sm-3 col-md-3">
                                                                    <asp:TextBox ID="Txt_EIN_IEM" CssClass="form-control" runat="server" autocomplete="off"
                                                                        MaxLength="50" ToolTip="Enter EIN/IEM/Udyog Aadhaar/Production Certificate/Udyam Registration Number Here."></asp:TextBox>
                                                                    <span class="mandetory" id="spanMandatory2" runat="server">*</span>
                                                                </div>
                                                            </ContentTemplate>
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="DrpDwn_Invest_Level" />
                                                            </Triggers>
                                                        </asp:UpdatePanel>

                                                        <label for="Sector" class="col-sm-3 col-md-2">
                                                            Sub Sector
                                                        </label>
                                                        <div class="col-sm-9 col-md-4">
                                                            <span class="colon">:</span>
                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="DrpDwn_Sub_Sector" runat="server" CssClass="form-control" ToolTip="Select sub-sector name here.">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="DrpDwn_Sector" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                            <span class="mandetory">*</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="EINIEM" class="col-sm-3 col-md-2">
                                                        </label>
                                                        <div class="col-sm-9 col-md-4" style="display: block;">
                                                            <span id="EINNo" style="display: block; font-family: Verdana; font-size: 11px;"><a
                                                                href="https://odishamsme.nic.in/user/MSME_Entrepreneur_Login.aspx" target="_blank">Click here
                                                            to apply for EIN number.</a></span> <span id="IEMNo" style="display: block; font-family: Verdana; font-size: 11px;"><a href=" https://services.dpiit.gov.in/lms/login" target="_blank">Click here to apply for IEM number.</a></span> <span id="UAadhaarNo" style="display: block; font-family: Verdana; font-size: 11px;"><a href="https://udyamregistration.gov.in/Government-India/Ministry-MSME-registration.htm"
                                                                target="_blank">Click here to apply for Udyog Aadhaar number.</a></span>
                                                            <span id="UdyamReg" style="display: block; font-family: Verdana; font-size: 11px;"><a
                                                                href="https://udyamregistration.gov.in/" target="_blank">Click here to apply for
                                                            Udyam Registration.</a></span>
                                                        </div>

                                                        <label for="GSTIN" class="col-sm-3 col-md-2">
                                                            GSTIN
                                                        </label>
                                                        <div class="col-sm-3 col-md-4">
                                                            <span class="colon">:</span>
                                                            <asp:TextBox ID="Txt_GSTIN" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')"
                                                                autocomplete="off" MaxLength="15" CssClass="form-control" runat="server" AutoCompleteType="None" ToolTip="Enter GSTIN number here."></asp:TextBox>
                                                            <a data-toggle="tooltip" class="fieldinfo" title="It accepts only alphabet and number.The length of the PAN number should be 10!">
                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="EINIEM" class="col-sm-3 col-md-2">
                                                            <asp:Label ID="Lbl_Doc_Name" runat="server" Text="Upload EIN / IEM / Udyog Aadhaar / Production Certificate / Udyam Registration Document"></asp:Label>
                                                        </label>
                                                        <div class="col-sm-9 col-md-4">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="FileUpload_Licence_Doc" CssClass="form-control" runat="server"
                                                                onchange="return validateFile(this);" ToolTip="Browse File to Upload !!" />
                                                            <span class="mandetory" id="spanMandatory3" runat="server">*</span> <small class="text-danger">(.pdf file only and Max file size 4 MB)</small>
                                                        </div>
                                                    </div>
                                                </div>

                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-sec">
                                        <div class="form-header">
                                            <h2>Login Details</h2>
                                        </div>
                                        <div class="form-body">
                                            <div class="formbodycontent">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="email" class="col-sm-3 col-md-2">
                                                                    User ID
                                                                </label>
                                                                <div class="col-sm-6 col-md-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="Txt_User_Id" CssClass="form-control" runat="server" autocomplete="off"
                                                                        Enabled="false" MaxLength="50"></asp:TextBox>
                                                                    <a data-toggle="tooltip" class="fieldinfo" title="This is the auto generated user id and will be used for login to the system !">
                                                                        <i class="fa fa-question-circle" aria-hidden="true"></i></a><span class="mandetory">*</span> <small class="text-red" style="margin-left: 0px;">The above id will be used
                                                                            as the user id when logged into the system.</small>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                                        TargetControlID="Txt_User_Id" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                        InvalidChars="&quot;'<>&;">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Password" class="col-sm-3 col-md-2">
                                                                    Password
                                                                </label>
                                                                <div class="col-sm-9 col-md-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="Txt_Pwd" CssClass="form-control" runat="server" TextMode="Password"
                                                                        autocomplete="off" MaxLength="14" ToolTip="Enter your password here."></asp:TextBox>
                                                                    <a data-toggle="tooltip" class="fieldinfo" title="Provide Password as per Password Policy!">
                                                                        <i class="fa fa-question-circle" aria-hidden="true"></i></a><span class="mandetory">*</span><small class="text-red" style="margin-left: 0px;">Password Policy: It should
                                                                            be between 8-14 characters,should contain atleast one uppercase,one lowercase,one
                                                                            number and one special character(!@#$&*).</small>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                                        TargetControlID="Txt_Pwd" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                        InvalidChars="&quot;'<>;">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </div>
                                                                <span id="password_strength"></span>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Re-Password" class="col-sm-3 col-md-2">
                                                                    Confirm Password
                                                                </label>
                                                                <div class="col-sm-9 col-md-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="Txt_Conf_Pwd" CssClass="form-control" runat="server" TextMode="Password"
                                                                        autocomplete="off" MaxLength="14" ToolTip="Confirm your password here."></asp:TextBox>
                                                                    <span class="mandetory">*</span>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                                        TargetControlID="Txt_Conf_Pwd" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                        InvalidChars="&quot;'<>;">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="SecurityQues" class="col-sm-3 col-md-2">
                                                                    Select Security Question
                                                                </label>
                                                                <div class="col-sm-9 col-md-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:DropDownList ID="DrpDwn_Question" runat="server" CssClass="form-control" ToolTip="Select the security question here.">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="What was the name of your  primary school?" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="What is your youngest brother's birthday?" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="Who was your childhood hero? " Value="3"></asp:ListItem>
                                                                        <asp:ListItem Text=" Where were you New Year's 2000?" Value="4"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <span class="mandetory">*</span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Answer" class="col-sm-3 col-md-2">
                                                                    Answer
                                                                </label>
                                                                <div class="col-sm-9 col-md-4">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="Txt_Answer" CssClass="form-control" runat="server" TextMode="Password"
                                                                        autocomplete="off" MaxLength="50" ToolTip="Enter secutity answer here."></asp:TextBox>
                                                                    <span class="mandetory">*</span>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                                                        TargetControlID="Txt_Answer" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                        InvalidChars="&quot;'<>&;">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" style="display: none;">
                                                            <div class="row">
                                                                <div class="col-sm-9 col-md-4 col-sm-offset-2">
                                                                    <asp:CheckBox ID="chkBoxEmail" runat="server" Text="Send status updates via email"></asp:CheckBox>
                                                                    <br />
                                                                    <asp:CheckBox ID="chkBoxSMS" runat="server" Text="Send status updates via SMS"></asp:CheckBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group" style="display: none;">
                                                            <div class="row">
                                                                <label for="Answer" class="col-sm-3 col-md-2">
                                                                    Enter Captcha
                                                                </label>
                                                                <div class="col-sm-9 col-md-4">
                                                                    <span class="colon">:</span>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td width="55%">
                                                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                    <ContentTemplate>
                                                                                        <cc2:CaptchaControl ID="Captcha1" runat="server" CaptchaBackgroundNoise="Low" CaptchaLength="5"
                                                                                            CaptchaMinTimeout="5" CaptchaMaxTimeout="240" CssClass="captchaimg" NoiseColor="#B1B1B1" />
                                                                                        <div class="refresh">
                                                                                            <asp:LinkButton ID="ImageButton1" runat="server" CausesValidation="false">  <span class="fa fa-refresh refreshbtn" style="cursor: pointer;" onclick="return RefreshCaptcha();" aria-hidden="true"></span> </asp:LinkButton>
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </td>
                                                                            <td width="45%">
                                                                                <asp:TextBox ID="Txt_Captcha" runat="server" CssClass="form-control upper-case" MaxLength="6"
                                                                                    autocomplete="off"></asp:TextBox>
                                                                                <a data-toggle="tooltip" class="fieldinfo" title="Enter the Characters shown in Image!">
                                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                                                    TargetControlID="Txt_Captcha" FilterMode="InvalidChars" FilterType="Custom,Numbers,LowercaseLetters,UppercaseLetters"
                                                                                    InvalidChars="&quot;'<>&;">
                                                                                </cc1:FilteredTextBoxExtender>
                                                                                <span class="mandetory">*</span>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                                </div>
                                                                <div class="clear20">
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-footer">
                                        <div class="row">
                                            <div class="col-sm-12 text-center">
                                                <asp:Button ID="Btn_Submit" runat="server" Text="Next" CssClass=" btn btn-success"
                                                    OnClick="Btn_Submit_Click" OnClientClick="return Validate();" />
                                                <asp:Button ID="Btn_Reset" runat="server" Text="Reset" CssClass=" btn btn-danger"
                                                    OnClick="Btn_Reset_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="Hid_Pop" runat="server" />
            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
                TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="LnkBtn_Close">
            </cc1:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: none;">
                <div id="undertakingipr2015">
                    <div class="modal-dialog modal-lg">
                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header bg-purpul">
                                <div class="form-group">
                                    <div class="col-sm-11">
                                        <h4 class="modal-title">List of registered units under PAN&nbsp;
                                        <asp:Label ID="Lbl_PAN" runat="server"></asp:Label></h4>
                                    </div>
                                    <div class="col-sm-1" style="text-align: right;">
                                        <asp:LinkButton ID="LnkBtn_Close" runat="server" ForeColor="White" ToolTip="Click here to close the popup">X</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-body">
                                <p>
                                    The PAN provided by you already exists for the following unit(s):
                                </p>
                                <%--     <div class="form-group">
                                <div class="row">
                                    <div class="col-sm-12">--%>
                                <div class="listdiv">
                                    <asp:GridView ID="GrdUserList" runat="server" AutoGenerateColumns="false" class="table    table-bordered table-hover">
                                        <Columns>
                                            <asp:BoundField DataField="VCH_INV_NAME" HeaderText="Unit Name" />
                                            <asp:BoundField DataField="VCH_SITELOCATION" HeaderText="Site Location" />
                                            <asp:BoundField DataField="VCH_EMAIL" HeaderText="Email Id" />
                                            <asp:BoundField DataField="VCH_OFF_MOBILE" HeaderText="Mobile No" />
                                            <asp:BoundField DataField="VCH_USER_TYPE" HeaderText="Unit Type" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                <%--  </div>
                                </div>
                            </div>--%>
                                <div>
                                    <span style="color: Red; font-family: Verdana;">Do you want to register as a subsidiary
                                    unit? ?</span>
                                    <br />
                                </div>
                            </div>
                            <div class="modal-footer">
                                <div class="row">
                                    <div class="col-sm-8" style="text-align: left;">
                                        <strong>Click Yes to proceed or Cancel</strong>
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="Btn_Yes" runat="server" Text="Yes" OnClick="Btn_Yes_Click" class="btn btn-success"
                                            ToolTip="Click Here to Proceed" />
                                        <asp:Button ID="Btn_No" runat="server" Text="Cancel" OnClick="Btn_No_Click" class="btn btn-danger"
                                            ToolTip="Click Here to Close Window" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
            </asp:Panel>
        </div>
        <uc3:footer ID="footer" runat="server" />
    </form>
</body>
  
</html>
