<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ThrustPrioritysectorstatus.aspx.cs" Inherits="incentives_ThrustPrioritysectorstatus" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css">
    <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Basic_Details.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>>
<%--    <script language="javascript" type="text/javascript">

        var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });

            var $activePanelHeading = $('.panel-group .panel .panel-collapse.in').prev().addClass('active');  //add class="active" to panel-heading div above the "collapse in" (open) div
            $activePanelHeading.find('a').prepend('<span class="fa fa-minus"></span> ');  //put the minus-sign inside of the "a" tag
            $('.panel-group .panel-heading').not($activePanelHeading).find('a').prepend('<span class="fa fa-plus"></span> ');  //if it's not active, it will put a plus-sign inside of the "a" tag
            $('.panel-group').on('show.bs.collapse', function (e) {  //event fires when "show" instance is called
                //$('.panel-group .panel-heading.active').removeClass('active').find('.fa').toggleClass('fa-plus fa-minus'); - removed so multiple can be open and have minus sign
                $(e.target).prev().addClass('active').find('.fa').toggleClass('fa-plus fa-minus');
            });
            $('.panel-group').on('hide.bs.collapse', function (e) {  //event fires when "hide" method is called
                $(e.target).prev().removeClass('active').find('.fa').removeClass('fa-minus').addClass('fa-plus');
            });
        });

    </script>

    <script type="text/javascript" language="javascript">

        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    location.href = 'incentiveoffered.aspx?';
                    return true;
                }
                else {
                    return false;
                }
            });
        }

        /////////////////////////////////////////////////////////////////////////////

        function validateFile(e) {
            var ids = e.id;
            var fileExtension = ['pdf', 'zip'];
            if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                jAlert('<strong>Only .pdf or .zip formats are allowed.</strong>', projname);
                $("#" + ids).val(null);
                return false;
            }
            else {
                if ((e.files[0].size > parseInt(4 * 1024 * 1024)) && ($("#" + ids).val() != '')) {

                    jAlert('<strong>File size must be less then 4 MB !! </strong>', projname);
                    $("#" + ids).val(null);
                    //e.preventDefault();
                    return false;
                }
            }
        }

        /////////////////// jquery method for Industrial Unit////////////////////////////////////////

        function openpopup(flu) {
            var i = flu.id;
            $("#" + i).click();
            return false;
        }

        function SameAddressIndustry() {
            var cc = $('#Txt_Industry_Address').val();
            if ($("#ChkSameData").is(':checked')) {
                $('#Txt_Regd_Office_Address').val(cc);
            }
        }




        ///*-------------------------------------------------------------------------------------------------------------------------*/
        ///Add by Debiprasanna Jena on Dt-11-07-2023
        function validateThrustprioritysectorstatus() {


            //if (!blankFieldValidation('Txt_EnterPrise_Name', 'EnterPrise/Industrial Unit', projname)) {
            //    return false;
            //}
            //if (!DropDownValidation('DrpDwn_Unit_Cat', '0', 'Category of the Unit ', projname)) {
            //    $("#popup_ok").click(function () { $("#DrpDwn_Unit_Cat").focus(); });
            //    return false;
            //}           

            //if (!blankFieldValidation('Txt_Industry_Address', 'Address of Registered Office Unit ', projname)) {
            //    return false;
            //}
            if (!WhiteSpaceValidation1st('Txt_Industry_Address', 'Address of Registered Office Unit ', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Industry_Address").focus(); });
                return false;
            }

            var indAddLength = $('#Txt_Industry_Address').val().length;
            if (indAddLength > 500) {
                jAlert('<strong>Address of Registered Office Unit Should be Maximum 500 Characters  !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Industry_Address").focus(); });
                return false;
            }


            //if (!blankFieldValidation('Txt_Regd_Office_Address', 'Address of Correspondence ', projname)) {
            //    return false;
            //}
            if (!WhiteSpaceValidation1st('Txt_Regd_Office_Address', 'Address of Correspondence ', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Regd_Office_Address").focus(); });
                return false;
            }
            var offAddLength = $('#Txt_Regd_Office_Address').val().length;
            if (offAddLength > 500) {
                jAlert('<strong>Address of Correspondence  Should be Maximum 500 Characters  !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Regd_Office_Address").focus(); });
                return false;
            }
            //if (blankFieldValidation('Txt_Phone_no', 'Mobile number', projname) == false) {
            //    return false;
            //}
            if (WhiteSpaceValidation1st('Txt_Phone_no', 'Mobile number', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_Phone_no', 'Mobile number', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_Phone_no', 'Mobile number', projname) == false) {
                return false;
            }
            var Phoneno = $('#Txt_Phone_no').val().length;
            if (Phoneno < 10) {
                jAlert('<strong>The minimum length of the mobile number should be 10.  !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Phone_no").focus(); });
                return false;
            }

            //if (blankFieldValidation('Txt_Email', 'Email Address', projname) == false) {
            //    return false;
            //}
            if (WhiteSpaceValidation1st('Txt_Email', 'Email Address', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_Email', 'Email Address', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_Email', 'Email Address', projname) == false) {
                return false;
            }
            var Email = $("#Txt_Email").val();
            if (Email != '') {
                var InctMail = new RegExp(/^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i);
                if (!InctMail.test(Email)) {
                    jAlert('<strong>Invalid Email !!</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_Email").focus(); });
                    return false;
                }
            }


            //if (!DropDownValidation('DrpDwn_Org_Type', '0', 'Organization Type', projname)) {
            //    $("#popup_ok").click(function () { $("#DrpDwn_Org_Type").focus(); });
            //    return false;
            //}
            var orgName = $('#Lbl_Org_Name_Type').text();
            //if (!blankFieldValidation('Txt_Partner_Name', orgName, projname)) {
            //    return false;
            //}
            if (!WhiteSpaceValidation1st('Txt_Partner_Name', orgName, projname)) {
                $("#popup_ok").click(function () { $("#Txt_Partner_Name").focus(); });
                return false;
            }
            //if (blankFieldValidation('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname) == false) {
            //    return false;
            //}
            if (WhiteSpaceValidation1st('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname) == false) {
                return false;
            }


            var EINDt = $('#Txt_EIN_IL_Date').val()
            if (EINDt != '') {

                if (new Date(EINDt) > new Date()) {
                    jAlert('<strong>EIN/ PC/ IEM/PEAL approval Date issued by SLNA/DLNA Date should not be greater than Current Date.</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_EIN_IL_Date").focus(); });
                    return false;
                }
            }
            var DtCapitaInv = $('#Txt_Proposed_Date').val()
            if (DtCapitaInv != '') {

                if (new Date(DtCapitaInv) > new Date()) {
                    jAlert('<strong>Proposed Date/ Date of first fixed capital investment should not be greater than Current Date.</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_Proposed_Date").focus(); });
                    return false;
                }
            }
            var DtCommencProd = $('#Txt_Commence_production').val()
            if (DtCommencProd != '') {

                if (new Date(DtCommencProd) > new Date()) {
                    jAlert('<strong>Proposed Date/ Date of Commencement of production / Activity Date should not be greater than Current Date.</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_Commence_production").focus(); });
                    return false;
                }
            }
            if ($("input[name='Rad_production']:checked").val() == '1') {

                if (WhiteSpaceValidation1st('Txt_PC_EMI_No', 'Production certificate / EM-II No. ', projname) == false) {
                    return false;
                }
                else if (WhiteSpaceValidationLast('Txt_PC_EMI_No', 'Production certificate / EM-II No. ', projname) == false) {
                    return false;
                }
                var DtPC = $('#Txt_PC_EMI_Date').val()
                if (DtPC != '') {

                    if (new Date(DtPC) > new Date()) {
                        jAlert('<strong>Production certificate/EM-II Date should not be greater than Current Date.</strong>', projname);
                        $("#popup_ok").click(function () { $("#Txt_PC_EMI_Date").focus(); });
                        return false;
                    }
                }
            }
            if ($("input[name='Rad_production']:checked").val() == '2') {

                if (WhiteSpaceValidation1st('Txt_Uam_No', 'UAM no. for MSME ', projname) == false) {
                    return false;
                }
                else if (WhiteSpaceValidationLast('Txt_Uam_No', 'UAM no. for MSME', projname) == false) {
                    return false;
                }
                var DtUam = $('#Txt_Uam_Date').val()
                if (DtUam != '') {

                    if (new Date(DtUam) > new Date()) {
                        jAlert('<strong>UAM no. and Date for MSME should not be greater than Current Date.</strong>', projname);
                        $("#popup_ok").click(function () { $("#Txt_Uam_Date").focus(); });
                        return false;
                    }
                }
            }

            //if (blankFieldValidation('Txt_total_emp_Number', 'Total Employement Numbers', projname) == false) {
            //    return false;
            //}
            if (WhiteSpaceValidation1st('Txt_total_emp_Number', 'Total Employement Numbers', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_total_emp_Number', 'Total Employement Numbers', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_total_emp_Number', 'Total Employement Numbers', projname) == false) {
                return false;
            }



            //if ($('#Hid_PC_Status').val() == 'Y') {

            //    if ($('#Hid_Is_Exist_Before').val() == 'Y') {
            //        if (!blankFieldValidation('Txt_PC_No_Before', 'PC No.', projname)) {
            //            return false;
            //        }
            //        if (!blankFieldValidation('Txt_Prod_Comm_Date_Before', 'Date of Production Commencement', projname)) {
            //            return false;
            //        }
            //        if (new Date($('#Txt_Prod_Comm_Date_Before').val()) > new Date()) {
            //            jAlert('<strong>Date of Production Commencement should not be greater than Current Date.</strong>', projname);
            //            $("#popup_ok").click(function () { $("#Txt_Prod_Comm_Date_Before").focus(); });
            //            return false;
            //        }
            //        if (!blankFieldValidation('Txt_PC_Issue_Date_Before', 'PC Issuance Date', projname)) {
            //            return false;
            //        }
            //        if (new Date($('#Txt_PC_Issue_Date_Before').val()) > new Date()) {
            //            jAlert('<strong>PC Issuance Date should not be greater than Current Date.</strong>', projname);
            //            $("#popup_ok").click(function () { $("#Txt_PC_Issue_Date_Before").focus(); });
            //            return false;
            //        }
            //    }
            //    //if ($('#Hid_Is_Exist_After').val() == 'Y') {
            //    //    if (!blankFieldValidation('Txt_PC_No_After', 'PC No.', projname)) {
            //    //        return false;
            //    //    }
            //    //    if (!blankFieldValidation('Txt_Prod_Comm_Date_After', 'Date of Production Commencement', projname)) {
            //    //        return false;
            //    //    }
            //    //    if (new Date($('#Txt_Prod_Comm_Date_After').val()) > new Date()) {
            //    //        jAlert('<strong>Date of Production Commencement should not be greater than Current Date.</strong>', projname);
            //    //        $("#popup_ok").click(function () { $("#Txt_Prod_Comm_Date_After").focus(); });
            //    //        return false;
            //    //    }
            //    //    if (!blankFieldValidation('Txt_PC_Issue_Date_After', 'PC Issuance Date', projname)) {
            //    //        return false;
            //    //    }
            //    //    if (new Date($('#Txt_PC_Issue_Date_After').val()) > new Date()) {
            //    //        jAlert('<strong>PC Issuance Date should not be greater than Current Date.</strong>', projname);
            //    //        $("#popup_ok").click(function () { $("#Txt_PC_Issue_Date_After").focus(); });
            //    //        return false;
            //    //    }
            //    //}
            //}


            //if (($("input[name='Rad_Nature_Of_Activity']:checked").val() != '40') && ($("input[name='Rad_Nature_Of_Activity']:checked").val() != '41')) {
            //    jAlert('<strong>Please Select Nature of Activity !!</strong>', projname);
            //    return false;
            //}
            //if (($("input[name='Rad_Priority_User']:checked").val() != '1') && ($("input[name='Rad_Priority_User']:checked").val() != '2')) {
            //    jAlert('<strong>Please Select Whether in Priority IPR-2015 !!</strong>', projname);
            //    return false;
            //}

            //if (($("input[name='Rad_Is_Priority']:checked").val() != '1') && ($("input[name='Rad_Is_Priority']:checked").val() != '2') && ($("input[name='Rad_Is_Priority']:checked").val() != '3')) {
            //    jAlert('<strong>Please Select Priority Sector Status Granted !!</strong>', projname);
            //    return false;
            //}

            //if ($("input[name='Rad_Is_Priority']:checked").val() == '1') {

            //    if (($("input[name='Rad_Is_Pioneer']:checked").val() != '1') && ($("input[name='Rad_Is_Pioneer']:checked").val() != '2')) {
            //        jAlert('<strong>Please Select Pioneer Option !!</strong>', projname);
            //        return false;
            //    }
            //    if ($("input[name='Rad_Is_Pioneer']:checked").val() == '1') {
            //        if ($('#Hid_Pioneer_Doc_File_Name').val() == '') {
            //            jAlert('<strong>Please Upload Document in Support of Pioneer Unit !!</strong>', projname);
            //            $("#popup_ok").click(function () { $("#FU_Pioneer_Doc").focus(); });
            //            return false;
            //        }
            //    }
            //}


            //if ($('#Hid_Is_Exist_Before').val() == 'Y') {

            //    if ($("#Grd_Production_Before tr").length > 0) {
            //    }
            //    else {
            //        jAlert('<strong>Please Insert Atleast One Record for Items of Manufacture/Activity !!</strong>', projname);
            //        return false;
            //    }
            //    if (!blankFieldValidation('Txt_Direct_Emp_Before', 'Direct Empolyment in Numbers', projname)) {
            //        return false;
            //    }
            //    if (!blankFieldValidation('Txt_Contract_Emp_Before', 'Contractual Empolyment in Numbers', projname)) {
            //        return false;
            //    }
            //    //        if ($('#Hid_Direct_Emp_Before_File_Name').val() == '') {
            //    //            jAlert('<strong>Please Upload Document in Support of Number of Employes shown as directly employed !!</strong>', projname);
            //    //            $('#FU_Direct_Emp_Before').focus();
            //    //            return false;
            //    //        }

            //    if (!blankFieldValidation('Txt_Managarial_Before', 'Managerial Employee', projname)) {
            //        return false;
            //    }
            //    if (!blankFieldValidation('Txt_Supervisor_Before', 'Supervisor Employee', projname)) {
            //        return false;
            //    }
            //    if (!blankFieldValidation('Txt_Skilled_Before', 'Skilled Employee', projname)) {
            //        return false;
            //    }
            //    if (!blankFieldValidation('Txt_Semi_Skilled_Before', 'Semi Skilled Employee', projname)) {
            //        return false;
            //    }
            //    if (!blankFieldValidation('Txt_Unskilled_Before', 'Un Skilled Employee', projname)) {
            //        return false;
            //    }
            //    if (!blankFieldValidation('Txt_General_Before', 'General Employee', projname)) {
            //        return false;
            //    }
            //    if (!blankFieldValidation('Txt_SC_Before', 'SC Employee', projname)) {
            //        return false;
            //    }
            //    if (!blankFieldValidation('Txt_ST_Before', 'ST Employee', projname)) {
            //        return false;
            //    }
            //    if (!blankFieldValidation('Txt_Women_Before', 'Women Employee', projname)) {
            //        return false;
            //    }
            //    if (!blankFieldValidation('Txt_PHD_Before', 'Differently Abled Persons Employee', projname)) {
            //        return false;
            //    }

            //    var direct_emp_before = $('#Txt_Direct_Emp_Before').val();
            //    var contract_emp_before = $('#Txt_Contract_Emp_Before').val();

            //    var mngr_before = $('#Txt_Managarial_Before').val();
            //    var sup_before = $('#Txt_Supervisor_Before').val();
            //    var skilled_before = $('#Txt_Skilled_Before').val();
            //    var semiskilled_before = $('#Txt_Semi_Skilled_Before').val();
            //    var unskilled_before = $('#Txt_Unskilled_Before').val();
            //    var gen_before = $('#Txt_General_Before').val();
            //    var sc_before = $('#Txt_SC_Before').val();
            //    var st_before = $('#Txt_ST_Before').val();

            //    var women_before = $('#Txt_Women_Before').val();
            //    var phd_before = $('#Txt_PHD_Before').val();

            //    var totalDirContBefore = parseInt(direct_emp_before) + parseInt(contract_emp_before);
            //    var totalEmpBefore = parseInt(mngr_before) + parseInt(sup_before) + parseInt(skilled_before) + parseInt(semiskilled_before) + parseInt(unskilled_before);
            //    var totalCastEmpBefore = parseInt(sc_before) + parseInt(st_before) + parseInt(gen_before);

            //    if (totalDirContBefore != totalEmpBefore) {
            //        jAlert('<strong>Total Employees and Sum of Direct and Contractual Employees must be Same !!</strong>', projname);
            //        $("#popup_ok").click(function () { $("#Txt_Direct_Emp_Before").focus(); });
            //        return false;
            //    }
            //    if (totalEmpBefore != totalCastEmpBefore) {
            //        jAlert('<strong>Total Employees and Sum of General,SC and ST Employees must be Same !!</strong>', projname);
            //        return false;
            //    }
            //    if (women_before > totalEmpBefore) {
            //        jAlert('<strong>Total women employees must be less than or equal to total employees !!</strong>', projname);
            //        $("#popup_ok").click(function () { $("#Txt_Women_Before").focus(); });
            //        return false;
            //    }
            //    if (phd_before > totalEmpBefore) {
            //        jAlert('<strong>Total differently abled persons employees must be less than or equal to total employees !!</strong>', projname);
            //        $("#popup_ok").click(function () { $("#Txt_PHD_Before").focus(); });
            //        return false;
            //    }
            //}

            //if ($("#Grd_Production_After tr").length > 0) {
            //}
            //else {
            //    jAlert('<strong>Please Insert Atleast One Record for Items of Manufacture/Activity !!</strong>', projname);
            //    return false;
            //}
            //if (!blankFieldValidation('Txt_Direct_Emp_After', 'Direct Empolyment in Numbers', projname)) {
            //    return false;
            //}
            //if (!blankFieldValidation('Txt_Contract_Emp_After', 'Contractual Empolyment in Numbers', projname)) {
            //    return false;
            //}

            //    if ($('#Hid_Direct_Emp_After_File_Name').val() == '') {
            //        jAlert('<strong>Please upload document in support of number of employes shown as directly employed !!</strong>', projname);
            //        $('#FU_Direct_Emp_After').focus();
            //        return false;
            //    }



        }

        ///*--------------------------------------------------------------------------------------------------------------------------*/

        ///*--------------------------------------------------------------------------------------------------------------------------*/
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


///*-----------------------------------------------------------------------------------------------------------------------------*/

    </script>--%>
     <script language="javascript" type="text/javascript">

         var projname = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';

        $(document).ready(function () {

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });

            var $activePanelHeading = $('.panel-group .panel .panel-collapse.in').prev().addClass('active');  //add class="active" to panel-heading div above the "collapse in" (open) div
            $activePanelHeading.find('a').prepend('<span class="fa fa-minus"></span> ');  //put the minus-sign inside of the "a" tag
            $('.panel-group .panel-heading').not($activePanelHeading).find('a').prepend('<span class="fa fa-plus"></span> ');  //if it's not active, it will put a plus-sign inside of the "a" tag
            $('.panel-group').on('show.bs.collapse', function (e) {  //event fires when "show" instance is called
                //$('.panel-group .panel-heading.active').removeClass('active').find('.fa').toggleClass('fa-plus fa-minus'); - removed so multiple can be open and have minus sign
                $(e.target).prev().addClass('active').find('.fa').toggleClass('fa-plus fa-minus');
            });
            $('.panel-group').on('hide.bs.collapse', function (e) {  //event fires when "hide" method is called
                $(e.target).prev().removeClass('active').find('.fa').removeClass('fa-minus').addClass('fa-plus');
            });
        });

     </script>
    <script type="text/javascript" language="javascript">

        function alertredirect(msg) {
            jAlert(msg, projname, function (r) {
                if (r) {
                    location.href = 'incentiveoffered.aspx?';
                    return true;
                }
                else {
                    return false;
                }
            });
        }

        /////////////////////////////////////////////////////////////////////////////

        function validateFile(e) {
            var ids = e.id;
            var fileExtension = ['pdf', 'zip'];
            if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                jAlert('<strong>Only .pdf or .zip formats are allowed.</strong>', projname);
                $("#" + ids).val(null);
                return false;
            }
            else {
                if ((e.files[0].size > parseInt(4 * 1024 * 1024)) && ($("#" + ids).val() != '')) {

                    jAlert('<strong>File size must be less then 4 MB !! </strong>', projname);
                    $("#" + ids).val(null);
                    //e.preventDefault();
                    return false;
                }
            }
        }

        /////////////////// jquery method for Industrial Unit////////////////////////////////////////

        function openpopup(flu) {
            var i = flu.id;
            $("#" + i).click();
            return false;
        }

        function SameAddressIndustry() {
            var cc = $('#Txt_Industry_Address').val();
            if ($("#ChkSameData").is(':checked')) {
                $('#Txt_Regd_Office_Address').val(cc);
            }
        }

        ///*------------------------------------------------------------------------------------------------------------------------*/
        ///////// Term Loan (Add More)

        function validateTermLoanT() {
            if (!blankFieldValidation('Txt_TL_Financial_Institution', 'Name of Financial Institution', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_TL_Financial_Institution', 'Name of Financial Institution', projname)) {
                $("#popup_ok").click(function () { $("#Txt_TL_Financial_Institution").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_TL_State', 'State', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_TL_State', 'State', projname)) {
                $("#popup_ok").click(function () { $("#Txt_TL_State").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_TL_City', 'City', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_TL_City', 'City', projname)) {
                $("#popup_ok").click(function () { $("#Txt_TL_City").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_TL_Amount', 'Term Loan Amount', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_TL_Sanction_Date', 'Sanction Date', projname)) {
                return false;
            }
            if (new Date($('#Txt_TL_Sanction_Date').val()) > new Date()) {
                jAlert('<strong>Sanction Date should not be greater than Current Date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_TL_Sanction_Date").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_TL_Avail_Amount', 'Avail Amount', projname)) {
                return false;
            }
            var sanctionAmount = parseFloat($("#Txt_TL_Amount").val());
            var availedAmount = parseFloat($("#Txt_TL_Avail_Amount").val());
            if (availedAmount > sanctionAmount) {
                jAlert('<strong>Availed Amount cannot be greater than Term Loan Amount.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_TL_Avail_Amount").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_TL_Availed_Date', 'Avail Date', projname)) {
                return false;
            }
            //if (new Date($('#Txt_TL_Availed_Date').val()) > new Date()) {
            //    jAlert('<strong>Avail Date should not be greater than Current Date.</strong>', projname);
            //    $("#popup_ok").click(function () { $("#Txt_TL_Availed_Date").focus(); });
            //    return false;
            //}
            if (new Date($('#Txt_TL_Availed_Date').val()) < new Date($('#Txt_TL_Sanction_Date').val())) {
                jAlert('<strong>Availed Date cannot be less than sanction date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_TL_Availed_Date").focus(); });
                return false;
            }
        }

        ///*------------------------------------------------------------------------------------------------------------------------*/
        //////// Working Capital Loan (Add More)

        function validateWCLoanT() {
            if (!blankFieldValidation('Txt_WC_Financial_Institution', 'Name of Financial Institution', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_WC_Financial_Institution', 'Name of Financial Institution', projname)) {
                $("#popup_ok").click(function () { $("#Txt_WC_Financial_Institution").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_WC_State', 'State', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_WC_State', 'State', projname)) {
                $("#popup_ok").click(function () { $("#Txt_WC_State").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_WC_City', 'City', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_WC_City', 'City', projname)) {
                $("#popup_ok").click(function () { $("#Txt_WC_City").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_WC_Amount', 'Loan Amount', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_WC_Sanction_Date', 'Sanction Date', projname)) {
                return false;
            }
            if (new Date($('#Txt_WC_Sanction_Date').val()) > new Date()) {
                jAlert('<strong>Sanction Date should not be greater than Current Date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_WC_Sanction_Date").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_WC_Avail_Amount', 'Avail Amount', projname)) {
                return false;
            }
            var sanctionAmount = parseFloat($("#Txt_WC_Amount").val());
            var availedAmount = parseFloat($("#Txt_WC_Avail_Amount").val());
            if (availedAmount > sanctionAmount) {
                jAlert('<strong>Availed Amount cannot be greater than Loan Amount.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_WC_Avail_Amount").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_WC_Availed_Date', 'Avail Date', projname)) {
                return false;
            }
            //if (new Date($('#Txt_WC_Availed_Date').val()) > new Date()) {
            //    jAlert('<strong>Avail Date should not be greater than Current Date.</strong>', projname);
            //    $("#popup_ok").click(function () { $("#Txt_WC_Availed_Date").focus(); });
            //    return false;
            //}
            if (new Date($('#Txt_WC_Availed_Date').val()) < new Date($('#Txt_WC_Sanction_Date').val())) {
                jAlert('<strong>Availed Date cannot be less than sanction date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_WC_Availed_Date").focus(); });
                return false;
            }
        }



        /*------------------------------------------------------------------------------------------------------------------------*/
        ////// Total Investment Amount Calculation (Before)

        function funCalTotalInvestAmtcapitalinvestment() {
            var land_amt = 0;
            var building_amt = 0;
            var electrical_inst_amt = 0;
            var plant_mach_amt = 0;
            var other_amt = 0;
            var loadig_amt = 0;
            var margine_money_amt = 0;

            if ($('#Txt_Land_Details_before').val() != '') {
                land_amt = $('#Txt_Land_Details_before').val();
            }
            if ($('#Txt_Building_Before').val() != '') {
                building_amt = $('#Txt_Building_Before').val();
            }
            if ($('#Txt_Electrical_inst_Before').val() != '') {
                electrical_inst_amt = $('#Txt_Electrical_inst_Before').val();
            }
            if ($('#Txt_Plant_Mach_Before').val() != '') {
                plant_mach_amt = $('#Txt_Plant_Mach_Before').val();
            }
            if ($('#Txt_Other_Fixed_Asset_Before').val() != '') {
                other_amt = $('#Txt_Other_Fixed_Asset_Before').val();
            }
            if ($('#Txt_Loadig_Before').val() != '') {
                loadig_amt = $('#Txt_Loadig_Before').val();
            }
            if ($('#Txt_Margine_money').val() != '') {
                margine_money_amt = $('#Txt_Margine_money').val();
            }

            $('#Txt_Total_Capital_invst').val(parseFloat(land_amt) + parseFloat(building_amt) + parseFloat(electrical_inst_amt) + parseFloat(plant_mach_amt) + parseFloat(other_amt) + parseFloat(loadig_amt) + parseFloat(margine_money_amt));


        }

        /*---------------------------------------------------------------------------------------------------------------------------*/



        /*------------------------------------------------------------------------------------------------------------------------*/
        ////// Total Investment Amount Calculation (After)

        function funCalTotalInvestAmtcapitalinvestmentAfter() {
            var land_amt = 0;
            var building_amt = 0;
            var electrical_inst_amt = 0;
            var plant_mach_amt = 0;
            var other_amt = 0;
            var loadig_amt = 0;
            var margine_money_amt = 0;

            if ($('#Txt_Land_After').val() != '') {
                land_amt = $('#Txt_Land_After').val();
            }
            if ($('#Txt_Building_After').val() != '') {
                building_amt = $('#Txt_Building_After').val();
            }
            if ($('#Txt_Electrical_inst_After').val() != '') {
                electrical_inst_amt = $('#Txt_Electrical_inst_After').val();
            }
            if ($('#Txt_Plant_Mach_After').val() != '') {
                plant_mach_amt = $('#Txt_Plant_Mach_After').val();
            }
            if ($('#Txt_Other_Fixed_Asset_After').val() != '') {
                other_amt = $('#Txt_Other_Fixed_Asset_After').val();
            }
            if ($('#Txt_Loadig_After').val() != '') {
                loadig_amt = $('#Txt_Loadig_After').val();
            }
            if ($('#Txt_Margine_money_After').val() != '') {
                margine_money_amt = $('#Txt_Margine_money_After').val();
            }

            $('#Txt_Total_Capital_After').val(parseFloat(land_amt) + parseFloat(building_amt) + parseFloat(electrical_inst_amt) + parseFloat(plant_mach_amt) + parseFloat(other_amt) + parseFloat(loadig_amt) + parseFloat(margine_money_amt));


        }

        /*--------------------------------------------------------------------------------------------------------------------------------*/


        ///*-------------------------------------------------------------------------------------------------------------------------*/
        ///Add by Debiprasanna Jena on Dt-11-07-2023
        function validateThrustprioritysectorstatus() {


            //if (!blankFieldValidation('Txt_EnterPrise_Name', 'EnterPrise/Industrial Unit', projname)) {
            //    return false;
            //}
            //if (!DropDownValidation('DrpDwn_Unit_Cat', '0', 'Category of the Unit ', projname)) {
            //    $("#popup_ok").click(function () { $("#DrpDwn_Unit_Cat").focus(); });
            //    return false;
            //}           

            //if (!blankFieldValidation('Txt_Industry_Address', 'Address of Registered Office Unit ', projname)) {
            //    return false;
            //}
            if (!WhiteSpaceValidation1st('Txt_Industry_Address', 'Address of Registered Office Unit ', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Industry_Address").focus(); });
                return false;
            }

            var indAddLength = $('#Txt_Industry_Address').val().length;
            if (indAddLength > 500) {
                jAlert('<strong>Address of Registered Office Unit Should be Maximum 500 Characters  !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Industry_Address").focus(); });
                return false;
            }


            //if (!blankFieldValidation('Txt_Regd_Office_Address', 'Address of Correspondence ', projname)) {
            //    return false;
            //}
            if (!WhiteSpaceValidation1st('Txt_Regd_Office_Address', 'Address of Correspondence ', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Regd_Office_Address").focus(); });
                return false;
            }
            var offAddLength = $('#Txt_Regd_Office_Address').val().length;
            if (offAddLength > 500) {
                jAlert('<strong>Address of Correspondence  Should be Maximum 500 Characters  !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Regd_Office_Address").focus(); });
                return false;
            }
            //if (blankFieldValidation('Txt_Phone_no', 'Mobile number', projname) == false) {
            //    return false;
            //}
            if (WhiteSpaceValidation1st('Txt_Phone_no', 'Mobile number', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_Phone_no', 'Mobile number', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_Phone_no', 'Mobile number', projname) == false) {
                return false;
            }
            //var Phoneno = $('#Txt_Phone_no').val().length;
            //if (Phoneno < 10) {
            //    jAlert('<strong>The minimum length of the mobile number should be 10.  !!</strong>', projname);
            //    $("#popup_ok").click(function () { $("#Txt_Phone_no").focus(); });
            //    return false;
            //}
            var Phoneno = $('#Txt_Phone_no').val().length;

            if (Phoneno < 8 || Phoneno > 15) {
                jAlert('<strong>The minimum and maximum length of the mobile number should be 8 to 15 digit.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Phone_no").focus(); });
                return false;
            }


            //if (blankFieldValidation('Txt_Email', 'Email Address', projname) == false) {
            //    return false;
            //}
            if (WhiteSpaceValidation1st('Txt_Email', 'Email Address', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_Email', 'Email Address', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_Email', 'Email Address', projname) == false) {
                return false;
            }
            var Email = $("#Txt_Email").val();
            if (Email != '') {
                var InctMail = new RegExp(/^\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b$/i);
                if (!InctMail.test(Email)) {
                    jAlert('<strong>Invalid Email !!</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_Email").focus(); });
                    return false;
                }
            }


            //if (!DropDownValidation('DrpDwn_Org_Type', '0', 'Organization Type', projname)) {
            //    $("#popup_ok").click(function () { $("#DrpDwn_Org_Type").focus(); });
            //    return false;
            //}
            var orgName = $('#Lbl_Org_Name_Type').text();
            //if (!blankFieldValidation('Txt_Partner_Name', orgName, projname)) {
            //    return false;
            //}
            if (!WhiteSpaceValidation1st('Txt_Partner_Name', orgName, projname)) {
                $("#popup_ok").click(function () { $("#Txt_Partner_Name").focus(); });
                return false;
            }
            //if (blankFieldValidation('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname) == false) {
            //    return false;
            //}
            if (WhiteSpaceValidation1st('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname) == false) {
                return false;
            }


            var EINDt = $('#Txt_EIN_IL_Date').val()
            if (EINDt != '') {

                if (new Date(EINDt) > new Date()) {
                    jAlert('<strong>EIN/ PC/ IEM/PEAL approval Date issued by SLNA/DLNA Date should not be greater than Current Date.</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_EIN_IL_Date").focus(); });
                    return false;
                }
            }
            var DtCapitaInv = $('#Txt_Proposed_Date').val()
            if (DtCapitaInv != '') {

                if (new Date(DtCapitaInv) > new Date()) {
                    jAlert('<strong>Proposed Date/ Date of first fixed capital investment should not be greater than Current Date.</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_Proposed_Date").focus(); });
                    return false;
                }
            }

            if ($("input[name='Rad_production']:checked").val() == '1') {

                if (WhiteSpaceValidation1st('Txt_PC_EMI_No', 'Production certificate / EM-II No. ', projname) == false) {
                    return false;
                }
                else if (WhiteSpaceValidationLast('Txt_PC_EMI_No', 'Production certificate / EM-II No. ', projname) == false) {
                    return false;
                }
                var DtPC = $('#Txt_PC_EMI_Date').val()
                if (DtPC != '') {

                    if (new Date(DtPC) > new Date()) {
                        jAlert('<strong>Production certificate/EM-II Date should not be greater than Current Date.</strong>', projname);
                        $("#popup_ok").click(function () { $("#Txt_PC_EMI_Date").focus(); });
                        return false;
                    }
                }
            }
            if ($("input[name='Rad_production']:checked").val() == '2') {

                if (WhiteSpaceValidation1st('Txt_Uam_No', 'UAM no. for MSME ', projname) == false) {
                    return false;
                }
                else if (WhiteSpaceValidationLast('Txt_Uam_No', 'UAM no. for MSME', projname) == false) {
                    return false;
                }
                var DtUam = $('#Txt_Uam_Date').val()
                if (DtUam != '') {

                    if (new Date(DtUam) > new Date()) {
                        jAlert('<strong>UAM no. and Date for MSME should not be greater than Current Date.</strong>', projname);
                        $("#popup_ok").click(function () { $("#Txt_Uam_Date").focus(); });
                        return false;
                    }
                }
            }


            if (WhiteSpaceValidation1st('Txt_total_emp_Number', 'Total Employement Numbers', projname) == false) {
                return false;
            }
            if (WhiteSpaceValidationLast('Txt_total_emp_Number', 'Total Employement Numbers', projname) == false) {
                return false;
            }
            if (SpecialCharacter1st('Txt_total_emp_Number', 'Total Employement Numbers', projname) == false) {
                return false;
            }


            if (!blankFieldValidation('Txt_Managarial_After', 'Managerial Employee', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Supervisor_After', 'Supervisor Employee', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Skilled_After', 'Skilled Employee', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Semi_Skilled_After', 'Semi Skilled Employee', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Unskilled_After', 'Un Skilled Employee', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_General_After', 'General Employee', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_SC_After', 'SC Employee', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_ST_After', 'ST Employee', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Women_After', 'Women Employee', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_PHD_After', 'Differently Abled Persons Employee', projname)) {
                return false;
            }

            var direct_emp_after = $('#Txt_Direct_Emp_After').val();
            var contract_emp_after = $('#Txt_Contract_Emp_After').val();

            var mngr_after = $('#Txt_Managarial_After').val();
            var sup_after = $('#Txt_Supervisor_After').val();
            var skilled_after = $('#Txt_Skilled_After').val();
            var semiskilled_after = $('#Txt_Semi_Skilled_After').val();
            var unskilled_after = $('#Txt_Unskilled_After').val();
            var gen_after = $('#Txt_General_After').val();
            var sc_after = $('#Txt_SC_After').val();
            var st_after = $('#Txt_ST_After').val();

            var women_after = $('#Txt_Women_After').val();
            var phd_after = $('#Txt_PHD_After').val();

            var totalDirContAfter = parseInt(direct_emp_after) + parseInt(contract_emp_after);
            var totalEmpAfter = parseInt(mngr_after) + parseInt(sup_after) + parseInt(skilled_after) + parseInt(semiskilled_after) + parseInt(unskilled_after);
            var totalCastEmpAfter = parseInt(sc_after) + parseInt(st_after) + parseInt(gen_after);

            if (totalDirContAfter != totalEmpAfter) {
                jAlert('<strong>Total Employees and Sum of Direct and Contractual Employees must be Same !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Direct_Emp_After").focus(); });
                return false;
            }
            if (totalEmpAfter != totalCastEmpAfter) {
                jAlert('<strong>Total Employees and Sum of General,SC and ST Employees must be Same !!</strong>', projname);
                return false;
            }
            if (women_after > totalEmpAfter) {
                jAlert('<strong>Total women employees must be less than or equal to total employees !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Women_After").focus(); });
                return false;
            }
            if (phd_after > totalEmpAfter) {
                jAlert('<strong>Total differently abled persons employees must be less than or equal to total employees !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_PHD_After").focus(); });
                return false;
            }

            if ($('#Hid_Is_Exist_Before').val() == 'Y') {

                if (!blankFieldValidation('Txt_FFCI_Date_Before', 'Date of First Fixed Capital Investment', projname)) {
                    return false;
                }
                if (new Date($('#Txt_FFCI_Date_Before').val()) > new Date()) {
                    jAlert('<strong>Date of First Fixed Capital Investment should not be greater than Current Date</strong>', projname);
                    $("#popup_ok").click(function () { $("#Txt_FFCI_Date_Before").focus(); });
                    return false;
                }

                if ($('#Hid_FFCI_Before_File_Name').val() == '') {
                    jAlert('<strong>Please Upload Document in Support of FFCI !!</strong>', projname);
                    $("#popup_ok").click(function () { $("#FU_FFCI_Before").focus(); });
                    return false;
                }
                if (!blankFieldValidation('Txt_Land_Before', 'Land Investment', projname)) {
                    return false;
                }
                if (!blankFieldValidation('Txt_Building_Before', 'Building Investment', projname)) {
                    return false;
                }
                if (!blankFieldValidation('Txt_Plant_Mach_Before', 'Plant and Machinary Investment', projname)) {
                    return false;
                }
                if (!blankFieldValidation('Txt_Other_Fixed_Asset_Before', 'Other Fixed Asset', projname)) {
                    return false;
                }
                if ($('#Hid_Approved_DPR_Before_File_Name').val() == '') {
                    jAlert('<strong>Please Upload Document in Support of Approved DPR(Detail Project Report) !!</strong>', projname);
                    $("#popup_ok").click(function () { $("#FU_Approved_DPR_Before").focus(); });
                    return false;
                }
            }

            if (!blankFieldValidation('Txt_FFCI_Date_After', 'Date of First Fixed Capital Investment', projname)) {
                return false;
            }
            if (new Date($('#Txt_FFCI_Date_After').val()) > new Date()) {
                jAlert('<strong>Date of First Fixed Capital Investment should not be greater than Current Date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_FFCI_Date_After").focus(); });
                return false;
            }
            if ($('#Hid_FFCI_After_File_Name').val() == '') {
                jAlert('<strong>Please Upload Document in Support of FFCI !!</strong>', projname);
                $("#popup_ok").click(function () { $("#FU_FFCI_After").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_Land_After', 'Land Investment', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Building_After', 'Building Investment', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Plant_Mach_After', 'Plant and Machinary Investment', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Other_Fixed_Asset_After', 'Other Fixed Asset', projname)) {
                return false;
            }
            if ($('#Hid_Approved_DPR_After_File_Name').val() == '') {
                jAlert('<strong>Please Upload Document in Support of Approved DPR(Detail Project Report) !!</strong>', projname);
                $("#popup_ok").click(function () { $("#FU_Approved_DPR_After").focus(); });
                return false;
            }

            if (!blankFieldValidation('Txt_Equity_Amt', 'Equity Amount', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Loan_Bank_FI', 'Loan from Bank/FI Amount', projname)) {
                return false;
            }

            //    if ($("#Grd_TL tr").length > 0) {
            //    }
            //    else {
            //        jAlert('<strong>Please Insert Atleast One Record for Term Loan Details !!</strong>', projname);
            //        return false;
            //    }

            //    if ($("#Grd_WC tr").length > 0) {
            //    }
            //    else {
            //        jAlert('<strong>Please Insert Atleast One Record for Working Capital Loan Details !!</strong>', projname);
            //        return false;
            //    }

            //    if ($('#Hid_Term_Loan_File_Name').val() == '') {
            //        jAlert('<strong>Please Upload Document in Support of Term Loan Sanction Order !!</strong>', projname);
            //        $('#FU_Term_Loan').focus();
            //        return false;
            //    }
            if (!blankFieldValidation('Txt_FDI_Componet', 'FDI Componet Amount', projname)) {
                return false;
            }

            var fdi = parseFloat($('#Txt_FDI_Componet').val())
            var equity = parseFloat($('#Txt_Equity_Amt').val())

            if (fdi > equity) {
                jAlert('<strong>FDI Cannot be Greater than Equity !!</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_FDI_Componet").focus(); });
                return false;
            }


        }

        ///*--------------------------------------------------------------------------------------------------------------------------*/

        ///*--------------------------------------------------------------------------------------------------------------------------*/
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


///*-----------------------------------------------------------------------------------------------------------------------------*/

    </script>
    <style type="text/css">
        .fieldinfo-left
        {
            float: left;
            margin-right: 7px;
            left: 10px;
            font-size: 17px;
            margin-top: -23px;
            color: #337ab7;
            position: relative;
            z-index: 2;
        }
        .listdiv ol
        {
            margin-left: 20px;
        }
        .listdiv ol li
        {
            font-size: 13px;
            line-height: 22px;
        }
    </style>
    <style type="text/css">
        .unitdtl .groupmastreportlet2 .portletdivider
        {
            width: 20%;
        }
        .groupmastreportlet2 .portletdivider span
        {
            font-size: 20px;
            margin-left: 3px;
        }
        ul ol
        {
            margin-left: 35px !important;
        }
    </style>
    <style type="text/css">
        .overlayContent
        {
            z-index: 99;
            margin: 250px auto;
            width: 90px;
            height: 90px;
        }
        .overlay
        {
            position: fixed;
            z-index: 98;
            top: 0px;
            left: 0px;
            right: 0px;
            bottom: 0px;
            background-color: #aaa;
            filter: alpha(opacity=50);
            opacity: 0.6;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanelMain">
        <ProgressTemplate>
            <div class="overlay">
                <div class="overlayContent">
                    <img alt="" src="../images/basicloader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
     <asp:UpdatePanel runat="server" ID="UpdatePanelMain">
        <ContentTemplate>
            <div class="container">
                <uc2:header ID="header" runat="server" />
                <div class="registration-div investors-bg">
                    <div id="exTab1" class="">
                        <div class="investrs-tab">
                            <uc4:investoemenu ID="ineste" runat="server" />
                        </div>
                        <div class="tab-content clearfix">
                            <div class="tab-pane active" id="1a">
                                <div class="form-sec">
                                    <div class="innertabs  m-b-10">
                                        <ul class="nav nav-pills pull-right">
                                            <li><a href="incentiveoffered.aspx" title="Click Here to View Incentive Offered !!">
                                                Incentive Offered</a></li>
                                            <%--   <li class="active"><a href="Basic_Details.aspx" title="Click Here to Apply For Incentives !!">
                                                Apply For Incentive</a></li>--%>
                                            <li><a href="ViewApplicationStatus.aspx" title="Click Here to View Application Status !!">
                                                View Application Status</a></li>
                                        </ul>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-header">                                      
                                        <h2>
                                            BASIC UNIT DETAILS</h2>
                                    </div>
                                    <div class="incentivesec">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="details-section leftcolm">
                                                    <div class="panel-group p-t-20" id="accordion" role="tablist" aria-multiselectable="true">
                                                       
                                                            <div class="panel panel-default">
                                                                <div class="panel-heading" role="tab" id="headingOne">
                                                                    <h4 class="panel-title">
                                                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                                                            aria-expanded="true" aria-controls="collapseOne"><span class="text-red pull-right "
                                                                                style="margin-right: 20px;">All fields marked with an asterisk (*) are mandatory</span>Industrial
                                                                        Unit's Details </a>
                                                                    </h4>
                                                                </div>
                                                                <div id="collapseOne" class="panel-collapse collapse in" role="tabpanel" aria-labelledby="headingOne"
                                                                    runat="server">
                                                                    <div class="panel-body">
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <%--<label class="col-sm-4">
                                                                                 1.
                                                                                </label>--%>
                                                                                </div>
                                                                            <div class="row">
                                                                               <label for="Iname" class="col-sm-4">                                      
                                                                                   1.  Name of Enterprise/Industrial Unit &nbsp;</label>
                                                                                    
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span><asp:TextBox ID="Txt_EnterPrise_Name" CssClass="form-control"
                                                                                        runat="server" MaxLength="100" ToolTip="Enter Name of Enterprise/Industrial Unit Here !!"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender27" runat="server" TargetControlID="Txt_EnterPrise_Name"
                                                                                        FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" ValidChars=",-/. ">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </div>
                                                                                     
                                                                            </div>

                                                                                
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <%--<label class="col-sm-4">
                                                                                 2.
                                                                                </label>--%>
                                                                                </div>
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4 ">
                                                                                   2.Category of the  Unit  &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:DropDownList ID="DrpDwn_Unit_Cat" CssClass="form-control" runat="server" ToolTip="Select Category of the  Unit Here !!">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                               <%-- <label class="col-sm-4">
                                                                                 3.
                                                                                </label>--%>
                                                                                </div>
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4 ">
                                                                                    3.Address of Registered Office Unit &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_Industry_Address" CssClass="form-control" MaxLength="500" TextMode="MultiLine"
                                                                                        runat="server" ToolTip="Enter Address of Registered Office Unit Here !!"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTxtExt_Industry_Address" runat="server"
                                                                                        TargetControlID="Txt_Industry_Address" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom"
                                                                                        ValidChars=",-/. ">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                             <div class="row">
                                                                                <%--<label class="col-sm-4">
                                                                                4.
                                                                                </label>--%>
                                                                                </div>
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    4.Address of Correspondence &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:CheckBox ID="ChkSameData" runat="server" Text="Same as Address of Registered office Unit"
                                                                                        onclick="return SameAddressIndustry();" />
                                                                                    <asp:TextBox ID="Txt_Regd_Office_Address" MaxLength="500" CssClass="form-control"
                                                                                        TextMode="MultiLine" runat="server" ToolTip="Enter Address of Correspondence Here !!"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTxtExt_Regd_Office_Address" runat="server"
                                                                                        TargetControlID="Txt_Regd_Office_Address" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom"
                                                                                        ValidChars=",-/. ">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                             <div class="row">
                                                                               <%-- <label class="col-sm-4">
                                                                                 5.
                                                                                </label>--%>
                                                                                </div>
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                   5. Phone Number</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_Phone_no"  MaxLength="15" Onkeypress="return inputLimiter(event,'Numbers')" CssClass="form-control" runat="server"
                                                                                        ToolTip="Enter Phone Number Here !!"></asp:TextBox>
                                                                                   

                                                                                   <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="Txt_Phone_no"
                                                                                                            FilterType="Numbers,Custom" ValidChars=".">
                                                                                                        </cc1:FilteredTextBoxExtender>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                             <div class="row">
                                                                               <%-- <label class="col-sm-4">
                                                                                 6.
                                                                                </label>--%>
                                                                                </div>
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                   6. Email</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_Email" Onkeypress="return inputLimiter(event,'Email')" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter Email Here !!"></asp:TextBox>
                                                                                    
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                             <div class="row">
                                                                               <%-- <label class="col-sm-4">
                                                                                 7.
                                                                                </label>--%>
                                                                                </div>
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    7.Type of  Organization  &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:DropDownList ID="DrpDwn_Org_Type" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrpDwn_Org_Type_SelectedIndexChanged" ToolTip="Select Type of  Organization Here !!">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                             <div class="row">
                                                                               <%-- <label class="col-sm-4">
                                                                                8.
                                                                                </label>--%>
                                                                                </div>
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                   8. <asp:Label ID="Lbl_Org_Name_Type" runat="server" Text="Name of Managing Partner"></asp:Label>
                                                                                    &nbsp;
                                                                                </label>
                                                                                <div class="col-sm-1" style="padding-right: 0px">
                                                                                    <span class="colon">:</span><asp:DropDownList CssClass="form-control" ID="DrpDwn_Gender_Partner"
                                                                                        runat="server" ToolTip="Select Salutation Here !!">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                                <div class="col-sm-7">
                                                                                    <asp:TextBox ID="Txt_Partner_Name" MaxLength="100" CssClass="form-control" runat="server"
                                                                                        ToolTip="Enter Name Here !!"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender29" runat="server" TargetControlID="Txt_Partner_Name"
                                                                                        FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=",-/. ">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                             <div class="row">
                                                                               <%-- <label class="col-sm-4">
                                                                                 9.
                                                                                </label>--%>
                                                                                </div>
                                                                            <div class="row">
                                                                               

                                                                                <label for="Iname" class="col-sm-4">
                                                                                   9. EIN/ PC/ IEM/PEAL approval letter No.</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_EIN_IL_NO" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter EIN/ PC/ IEM/PEAL approval letter No. Here !!"></asp:TextBox>
                                                                                    <%-- <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender65" runat="server" TargetControlID="Txt_EIN_IL_NO"
                                                                                    FilterType="Numbers" ValidChars="0123456789">
                                                                                </cc1:FilteredTextBoxExtender>--%>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                             
                                                                            <div class="row">
                                                                                
                                                                                <label for="Iname" class="col-sm-4">
                                                                                 EIN/ PC/ IEM/PEAL approval Date issued by SLNA/DLNA</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group date datePicker" id="Div_Date_EIN" runat="server">
                                                                                        <asp:TextBox ID="Txt_EIN_IL_Date" CssClass="form-control" type="text" runat="server"
                                                                                            MaxLength="11" ToolTip="Enter EIN/PC/ IEM/ PEAL approval Date issued by SLNA/DLNA Date Here !!"></asp:TextBox>
                                                                                        <span id="Span_Date_EIN" runat="server" class="input-group-addon"><i class="fa fa-calendar"></i></span>

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>


                                                                    </div>
                                                                </div>
                                                        </div>
                                                        
                                                       <div class="panel panel-default">
                                                            <div class="panel-heading" role="tab" id="Div6">
                                                                <h4 class="panel-title">
                                                                    <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                                        href="#ProductionEmploymentDetails" aria-expanded="false" aria-controls="collapseThree">
                                                                        Production & Employment Details </a>
                                                                </h4>
                                                            </div>
                                                            <div id="ProductionEmploymentDetails" class="panel-collapse collapse " role="tabpanel"
                                                                aria-labelledby="headingThree">
                                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div class="panel-body">
                                                                            <p class="text-red text-right">
                                                                                All Amounts to be Entered in INR Lakhs</p>
                                                                            <div id="Div_Prod_Emp_Before" runat="server">
                                                                               
                                                                                <div class="form-group">
                                                                                     <div class="row">
                                                                                <%--<label class="col-sm-4">
                                                                                 10.
                                                                                </label>--%>
                                                                                </div>
                                                                                    <div class="row">
                                                                                        <label for="Iname" class="col-sm-12">
                                                                                           10. Proposed items or Items of manufacture / activities with proposed capacity / installed capacity&nbsp;
                                                                                        </label>
                                                                                        <div class="col-sm-12  margin-bottom10">
                                                                                            <table class="table table-bordered">
                                                                                                <tr>
                                                                                                    <th width="5%">
                                                                                                        SlNo
                                                                                                    </th>
                                                                                                    <th>
                                                                                                        Product/Items Name
                                                                                                    </th>
                                                                                                    <th width="15%">
                                                                                                        Quantity
                                                                                                    </th>
                                                                                                    <th width="20%">
                                                                                                        Units
                                                                                                    </th>
                                                                                                    <th width="20%">
                                                                                                        Value
                                                                                                    </th>
                                                                                                    <th width="5%">
                                                                                                        Action
                                                                                                    </th>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        &nbsp;
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="Txt_Product_Name_Before" runat="server" CssClass="form-control"
                                                                                                            MaxLength="100" ToolTip="Enter Product/Items Name Here !!"></asp:TextBox>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="Txt_Product_Name_Before"
                                                                                                            FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ">
                                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="Txt_Quantity_Before" runat="server" CssClass="form-control" MaxLength="10"
                                                                                                            onkeypress="return FloatOnly(event, this);" ToolTip="Enter Quantity Here !!"></asp:TextBox>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="Txt_Quantity_Before"
                                                                                                            FilterType="Numbers,Custom" ValidChars=".">
                                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:DropDownList ID="DrpDwn_Unit_Before" runat="server" CssClass="form-control"
                                                                                                            OnSelectedIndexChanged="DrpDwn_Unit_Before_SelectedIndexChanged" AutoPostBack="true"
                                                                                                            ToolTip="Select Unit Here !!">
                                                                                                        </asp:DropDownList>
                                                                                                        <asp:TextBox ID="Txt_Other_Unit_Before" runat="server" CssClass="form-control" placeholder="Enter Other Unit"
                                                                                                            MaxLength="100"></asp:TextBox>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender64" runat="server" TargetControlID="Txt_Other_Unit_Before"
                                                                                                            FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=".">
                                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="Txt_Value_Before" runat="server" CssClass="form-control" MaxLength="10"
                                                                                                            onkeypress="return FloatOnly(event, this);" ToolTip="Enter Value Here !!"></asp:TextBox>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="Txt_Value_Before"
                                                                                                            FilterType="Numbers,Custom" ValidChars=".">
                                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        <asp:LinkButton ID="LnkBtn_Add_Item_Before" CssClass="btn btn-success btn-sm" OnClick="LnkBtn_Add_Item_Before_Click" runat="server" OnClientClick="return validateItemAddBefore();"
                                                                                                            ToolTip="Click Here to Add Items !!"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <asp:GridView ID="Grd_Production_Before" runat="server" CssClass="table table-bordered"
                                                                                                DataKeyNames="vchProductName" AutoGenerateColumns="false" ShowHeader="false">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_Sl_No_Product_Before" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="5%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_Product_Name_Before" runat="server" Text='<%# Eval("vchProductName") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_Quantity_Before" runat="server" Text='<%# Eval("intQuantity") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="15%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_Unit_Before" runat="server" Text='<%# Eval("vchUnit") %>'></asp:Label>
                                                                                                            <asp:HiddenField ID="Hid_Unit_Before" runat="server" Value='<%# Eval("intUnit") %>' />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="10%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_Other_Unit_Before" runat="server" Text='<%# Eval("vchOtherUnit") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="10%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="Lbl_Value_Before" runat="server" Text='<%# Eval("decValue") %>'></asp:Label>
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="20%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField>
                                                                                                        <ItemTemplate>
                                                                                                            <asp:ImageButton ID="ImgBtn_Delete_Before" runat="server" OnClick="ImgBtn_Delete_Before_Click" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                                                                CommandArgument='<%# Container.DataItemIndex %>' 
                                                                                                                ToolTip="Click Here to Remove" />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle Width="5%"></ItemStyle>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                
                                                                       
                                                                                
                                                                            </div>
                                                                            <%--  <br />--%>
                                                                            <h4>
                                                                                <asp:Label ID="Lbl_Header_Prod_Emp" runat="server"></asp:Label></h4>

                                                                            <div class="form-group">
                                                                                <div class="row">
                                                                                <%--<label class="col-sm-4">
                                                                                  11.
                                                                                </label>--%>
                                                                                </div>
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                               11. Proposed Date/ Date of first fixed capital investment</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group date datePicker" id="Div7" runat="server">
                                                                                    <asp:TextBox ID="Txt_Proposed_Date" CssClass="form-control" type="text" runat="server"
                                                                                        MaxLength="11" ToolTip="Enter Proposed Date/ Date of first fixed capital investment Here !!"></asp:TextBox>
                                                                                    <span id="Span1" runat="server" class="input-group-addon"><i class="fa fa-calendar">
                                                                                    </i></span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                             <div class="form-group">
                                                                                  <div class="row">
                                                                               <%-- <label class="col-sm-4">
                                                                               12.
                                                                                </label>--%>
                                                                                </div>
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                               12. Proposed Date/ Date of Commencement of production / Activity</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group date datePicker" id="Div5" runat="server">
                                                                                    <asp:TextBox ID="Txt_Commence_production" CssClass="form-control" type="text" runat="server"
                                                                                        MaxLength="11" ToolTip="Enter  Proposed Date/ Date of Commencement of production / Activity Here !!"></asp:TextBox>
                                                                                    <span id="Span4" runat="server" class="input-group-addon"><i class="fa fa-calendar">
                                                                                    </i></span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>


                                                                            <div class="form-group">
                                                                                <div class="row">
                                                                               <%-- <label class="col-sm-4">
                                                                                 13.
                                                                                </label>--%>
                                                                                </div>
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                               13. Select EIM/UAM/Other
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:RadioButtonList ID="Rad_production" class="optradioPriority" runat="server"
                                                                                    CssClass="radio-inline" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="Rad_production_SelectedIndexChanged" ToolTip=" Select EIM/UAM/Other Here !!">
                                                                                    <asp:ListItem Value="1">EIM</asp:ListItem>
                                                                                    <asp:ListItem Value="2" >UAM</asp:ListItem>
                                                                                    <asp:ListItem Value="3" Selected="True">Other</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                             <div id="Div_EIM_No" runat="server">
                                                                            <div class="form-group">
                                                                                <div class="row">
                                                                                    <label for="Iname" class="col-sm-4">
                                                                                        Production certificate / EM-II No.</label>
                                                                                    <div class="col-sm-8">
                                                                                        <span class="colon">:</span>
                                                                                        <asp:TextBox ID="Txt_PC_EMI_No" CssClass="form-control" MaxLength="100" runat="server"
                                                                                            ToolTip="Enter Production certificate / EM-II No. Here !!"></asp:TextBox>

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                                 </div>
                                                                           
                                                                            <div id="Div_Eim_date" runat="server">
                                                                             <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                Production certificate/EM-II Date</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group date datePicker" id="Div3" runat="server">
                                                                                    <asp:TextBox ID="Txt_PC_EMI_Date" CssClass="form-control" type="text" runat="server"
                                                                                        MaxLength="11" ToolTip="Enter EIN/ IEM/ IL Date Here !!"></asp:TextBox>
                                                                                    <span id="Span2" runat="server" class="input-group-addon"><i class="fa fa-calendar">
                                                                                    </i></span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                                </div>


                                                                             <div id="Div_UAM_No" runat="server">
                                                                            <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">                                                                              
                                                                                UAM no. for MSME
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:TextBox ID="Txt_Uam_No" CssClass="form-control" MaxLength="100" runat="server"
                                                                                    ToolTip="Enter UAM no. for MSME Here !!"></asp:TextBox>
                                                                              
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                                 </div>

                                                                            <div id="Div_UAM_date" runat="server">
                                                                              <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                                UAM no. and Date for MSME</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group date datePicker" id="Div4" runat="server">
                                                                                    <asp:TextBox ID="Txt_Uam_Date" CssClass="form-control" type="text" runat="server"
                                                                                        MaxLength="11" ToolTip="Enter UAM no. and Date for MSME Here !!"></asp:TextBox>
                                                                                    <span id="Span3" runat="server" class="input-group-addon"><i class="fa fa-calendar">
                                                                                    </i></span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div

                                                                                 
                                                                            
                                                                            
                                                                            
                                                                            
                                                                        
                                                                            </div>
                                                                             </div>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>                                                    
                                                       <div class="panel panel-default">
                                                            <div class="panel-heading" role="tab" id="headingThree">
                                                                <h4 class="panel-title">
                                                                    <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                                        href="#IndustryDetails" aria-expanded="false" aria-controls="collapseThree">Investment
                                                                        Details </a>
                                                                </h4>
                                                            </div>
                                                            <div id="IndustryDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                                                <div class="panel-body">
                                                                    <p class="text-red text-right">
                                                                        All Amounts to be Entered in INR Lakhs</p>                                                                   
                                                                    <div id="Div_Investment_Before" runat="server">

                                                                         <div class="form-group">
                                                                             <div class="row">
                                                                                <%--<label class="col-sm-4">
                                                                                 14.
                                                                                </label>--%>
                                                                                </div>
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-2">
                                                                                   14. Total Employement Numbers</label>
                                                                            <div class="col-sm-4">
                                                                                <span class="colon">:</span>
                                                                                <asp:TextBox ID="Txt_total_emp_Number" Onkeypress="return inputLimiter(event,'Numbers')" CssClass="form-control text-right" MaxLength="50" runat="server"
                                                                                    ToolTip="Enter Total Employement Numbers Here !!"></asp:TextBox>
                                                                              
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                        
                                                                        
                                                                        <div class="form-group">
                                                                            <div class="row">
                                                                                <%--<label class="col-sm-4">
                                                                                 15.
                                                                                </label>--%>
                                                                                </div>
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-12 ">
                                                                                   15. Total Capital Investment</label>
                                                                                <div class="col-sm-12">
                                                                                    <table class="table table-bordered">
                                                                                        <tbody>
                                                                                            <tr>
                                                                                                <th>
                                                                                                    SlNo
                                                                                                </th>
                                                                                                <th>
                                                                                                    Investment Head
                                                                                                </th>
                                                                                                <th>
                                                                                                    As per DPR (INR Lakhs)
                                                                                                </th>
                                                                                                <th>
                                                                                                    Actual expenditure incurred(till date)(INR Lakhs)
                                                                                                </th>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    1
                                                                                                </td>
                                                                                                <td>
                                                                                                    Land and land development&nbsp;
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Land_Details_before" CssClass="form-control text-right" runat="server"
                                                                                                        Text="0"  onkeyup="return funCalTotalInvestAmtcapitalinvestment()" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter Land and Development Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Land_Details_before" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>

                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Land_After" CssClass="form-control text-right" runat="server"
                                                                                                        Text="0" onkeyup="return funCalTotalInvestAmtcapitalinvestmentAfter();" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter Land and Development Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Land_After" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    2
                                                                                                </td>
                                                                                                <td>
                                                                                                    Building and Civil construction&nbsp;
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Building_Before" CssClass="form-control text-right" runat="server" onkeyup="return funCalTotalInvestAmtcapitalinvestment();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter  Building and Civil construction Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Building_Before" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>

                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Building_After" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtcapitalinvestmentAfter();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter  Building and Civil construction Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Building_After" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    3
                                                                                                </td>
                                                                                                <td>
                                                                                                    Electrification and electrical installation&nbsp;
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Electrical_inst_Before" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtcapitalinvestment();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter Electrification and electrical installation Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Electrical_inst_Before" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>

                                                                                                 <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Electrical_inst_After" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtcapitalinvestmentAfter();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter Electrification and electrical installation Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Electrical_inst_After" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    4
                                                                                                </td>
                                                                                                <td>
                                                                                                  Plant and Machinery&nbsp;
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Plant_Mach_Before" CssClass="form-control text-right"
                                                                                                        runat="server" onkeyup="return funCalTotalInvestAmtcapitalinvestment();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter  Plant and Machinery Here !!"></asp:TextBox>
                                                                                                   
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Plant_Mach_Before" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>

                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Plant_Mach_After" CssClass="form-control text-right"
                                                                                                        runat="server" onkeyup="return funCalTotalInvestAmtcapitalinvestmentAfter();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter  Plant and Machinery Here !!"></asp:TextBox>

                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Plant_Mach_After" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>

                                                                                             <tr>
                                                                                                <td>
                                                                                                    5
                                                                                                </td>
                                                                                                <td>
                                                                                                    Other fixed assests of permanent nature&nbsp;
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Other_Fixed_Asset_Before" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtcapitalinvestment();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter Other fixea assests of permanent nature Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender13" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Other_Fixed_Asset_Before" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>

                                                                                                 <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Other_Fixed_Asset_After" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtcapitalinvestmentAfter();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter Other fixea assests of permanent nature Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender14" runat="server" Enabled="True"
                                                                                                          TargetControlID="Txt_Other_Fixed_Asset_After" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>

                                                                                             <tr>
                                                                                                <td>
                                                                                                    6
                                                                                                </td>
                                                                                                <td>
                                                                                                   Loading,unloading,transportation,tax and duties paid,erection expenses etc&nbsp;
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Loadig_Before" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtcapitalinvestment();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter Loading,unloading,transportation,tax and duties paid,erection expenses etc Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender15" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Loadig_Before" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>

                                                                                                 <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Loadig_After" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtcapitalinvestmentAfter();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter Loading,unloading,transportation,tax and duties paid,erection expenses etc Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender16" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Loadig_After" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>


                                                                                             <tr>
                                                                                                <td>
                                                                                                    7
                                                                                                </td>
                                                                                                <td>
                                                                                                    Margin money for Working Capital&nbsp;
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Margine_money" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtcapitalinvestment();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter Margin money for Working Capital Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender17" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Margine_money" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>

                                                                                                 <td class="text-right">
                                                                                                    <asp:TextBox ID="Txt_Margine_money_After" CssClass="form-control text-right" runat="server"
                                                                                                        onkeyup="return funCalTotalInvestAmtcapitalinvestmentAfter();" Text="0" onkeypress="return FloatOnly(event, this);"
                                                                                                        AutoCompleteType="None" autocomplete="off" MaxLength="10" ToolTip="Enter Margin money for Working Capital Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender18" runat="server" Enabled="True"
                                                                                                        TargetControlID="Txt_Margine_money_After" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                            </tr>

                                                                                            

                                                                                            <tr>
                                                                                                <td colspan="2">
                                                                                                    <strong>Total</strong>
                                                                                                </td>
                                                                                                <td class="text-right">
                                                                                                    <strong>
                                                                                                        <asp:TextBox ID="Txt_Total_Capital_invst" runat="server" CssClass="form-control text-right"
                                                                                                            Text="0" Enabled="false" onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender62" runat="server" Enabled="True"
                                                                                                            TargetControlID="Txt_Total_Capital_invst" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                                    </strong>
                                                                                                </td>
                                                                                               
                                                                                                <td class="text-right">
                                                                                                    <strong>
                                                                                                        <asp:TextBox ID="Txt_Total_Capital_After" runat="server" CssClass="form-control text-right"
                                                                                                            Text="0" Enabled="false" onkeypress="return FloatOnly(event, this);"></asp:TextBox>
                                                                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender12" runat="server" Enabled="True"
                                                                                                            TargetControlID="Txt_Total_Capital_After" FilterType="Numbers,Custom" ValidChars=".">
                                                                                                        </cc1:FilteredTextBoxExtender>
                                                                                                    </strong>
                                                                                                </td>
                                                                                            </tr>

                                                                                              
                                                                                        </tbody>
                                                                                    </table>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        
                                                                       
                                                                    </div>
                                                                    <div>
                                                                        <h4>
                                                                            <asp:Label ID="Lbl_Header_Investment" runat="server"></asp:Label>
                                                                        </h4>
                                                                       
                                                                    </div>
                                                                    <h4 class="h4-header">
                                                                        Means Of Finance
                                                                    </h4>
                                                                     <div class="row">
                                                                                <%--<label class="col-sm-4">
                                                                               16.
                                                                                </label>--%>
                                                                                </div>
                                                                    <div class="form-group row">
                                                                       
                                                                        <label class="col-sm-2">                                                                                                                                                             
                                                                            16.Internal sources(Lakhs) &nbsp; </label>
                                                                        <div class="col-sm-4">                                                                          
                                                                            <span class="colon">:</span>
                                                                            <asp:TextBox ID="Txt_Equity_Amt" CssClass="form-control text-right" runat="server" Text="0"
                                                                                onkeypress="return FloatOnly(event, this);" MaxLength="10" ToolTip="Enter Internal sources Here !!"></asp:TextBox>
                                                                           
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" TargetControlID="Txt_Equity_Amt"
                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                        <%--<label class="col-sm-2">
                                                                            Loan From Bank/FI</label>--%>
                                                                        <%--<label class="col-sm-4">
                                                                            <span class="colon">:</span> <span class="lablespan">Total Amount (Excluding Loan for
                                                                                Working Capital)</span></label>--%>
                                                                       <%-- <div class="col-sm-4">
                                                                            <span class="colon">:</span>
                                                                            <asp:TextBox ID="Txt_Loan_Bank_FI" CssClass="form-control" runat="server" Text="0"
                                                                                onkeypress="return FloatOnly(event, this);" MaxLength="10" ToolTip="Enter Loan Amount taken from Bank/Financial Institution Here!!"></asp:TextBox>
                                                                            <a href="#" data-toggle="tooltip" class="fieldinfo" title="The amount of loan borrowed from any financial institute/friend">
                                                                                <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server" TargetControlID="Txt_Loan_Bank_FI"
                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <small class="text-gray lablespan">Total Amount (Excluding Loan for Working Capital)</small>
                                                                        </div>--%>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-12 ">
                                                                                <strong>Term Loan Details</strong></label>
                                                                            <div class="col-sm-12">
                                                                                <table class="table table-bordered">
                                                                                    <tr>
                                                                                        <th rowspan="2" width="4%">
                                                                                            SlNo
                                                                                        </th>
                                                                                        <th rowspan="2">
                                                                                            Name of Financial Institution
                                                                                        </th>
                                                                                        <th colspan="2" >
                                                                                            Location
                                                                                        </th>
                                                                                        <th rowspan="2" width="10%">
                                                                                            Term Loan Amount
                                                                                        </th>
                                                                                        <th rowspan="2" width="12%">
                                                                                            Sanction Date
                                                                                        </th>
                                                                                        <th rowspan="2" width="10%">
                                                                                            Availed Amount
                                                                                        </th>
                                                                                        <th rowspan="2" width="12%">
                                                                                            Availed Date
                                                                                        </th>
                                                                                        <th rowspan="2" width="5%">
                                                                                            Add More
                                                                                        </th>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <th>
                                                                                            State
                                                                                        </th>
                                                                                        <th>
                                                                                            City
                                                                                        </th>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            -
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_TL_Financial_Institution" CssClass="form-control" runat="server"
                                                                                                MaxLength="100" ToolTip="Enter Name of the Financial Institution Here !!"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender55" runat="server" TargetControlID="Txt_TL_Financial_Institution"
                                                                                                FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" ValidChars=",-/. ">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_TL_State" CssClass="form-control" runat="server" MaxLength="50"
                                                                                                ToolTip="Enter State Here !!"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender56" runat="server" TargetControlID="Txt_TL_State"
                                                                                                FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_TL_City" CssClass="form-control" runat="server" MaxLength="50"
                                                                                                ToolTip="Enter City Here !!"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender57" runat="server" TargetControlID="Txt_TL_City"
                                                                                                FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=",-/. ">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_TL_Amount" CssClass="form-control text-right" runat="server" onkeypress="return FloatOnly(event, this);"
                                                                                                MaxLength="10" ToolTip="Enter Loan Amount Here !!"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender19" runat="server" Enabled="True"
                                                                                                TargetControlID="Txt_TL_Amount" FilterType="Numbers,Custom" ValidChars=".">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="input-group  date datePicker" id="Div15">
                                                                                                <asp:TextBox ID="Txt_TL_Sanction_Date" runat="server" class="form-control" MaxLength="11"
                                                                                                    ToolTip="Enter Sanction Date Here !!"></asp:TextBox>
                                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_TL_Avail_Amount" CssClass="form-control text-right" runat="server" onkeypress="return FloatOnly(event, this);"
                                                                                                MaxLength="10" ToolTip="Enter Avail Amount Here !!"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender20" runat="server" Enabled="True"
                                                                                                TargetControlID="Txt_TL_Avail_Amount" FilterType="Numbers,Custom" ValidChars=".">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="input-group  date datePicker" id="Div16">
                                                                                                <asp:TextBox ID="Txt_TL_Availed_Date" runat="server" class="form-control" MaxLength="11"
                                                                                                    ToolTip="Enter Avail Date Here !!"></asp:TextBox>
                                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="LnkBtn_TL_Add_More" OnClick="LnkBtn_TL_Add_More_Click" CssClass="btn btn-success btn-sm" runat="server"
                                                                                                 OnClientClick="return validateTermLoanT();"
                                                                                                ToolTip="Click Here to Add"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <asp:GridView ID="Grd_TL" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                                                    ShowHeader="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_TL_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="4%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_TL_Financial_Inst" runat="server" Text='<%# Eval("vchNameOfFinancialInst") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_TL_State" runat="server" Text='<%# Eval("vchState") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="13%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_TL_City" runat="server" Text='<%# Eval("vchCity") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="13%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_TL_Amount" runat="server" Text='<%# Eval("decLoanAmt") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_TL_Sanction_Date" runat="server" Text='<%# Eval("dtmSanctionDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="12%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_TL_Avail_Amt" runat="server" Text='<%# Eval("decAvailedAmt") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_TL_Avail_Date" runat="server" Text='<%# Eval("dtmAvailedDate" , "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="12%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="ImgBtn_Delete_TL" OnClick="ImgBtn_Delete_TL_Click" runat="server" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                                                                                    ToolTip="Click Here to Remove" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="5%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-12 ">
                                                                                <strong>Working Capital Loan Details</strong></label>
                                                                            <div class="col-sm-12">
                                                                                <table class="table table-bordered">
                                                                                    <tr>
                                                                                        <th rowspan="2" width="4%">
                                                                                            SlNo
                                                                                        </th>
                                                                                        <th rowspan="2">
                                                                                            Name of Financial Institution
                                                                                        </th>
                                                                                        <th colspan="2" width="26%" style="text-align: center;">
                                                                                            Location
                                                                                        </th>
                                                                                        <th rowspan="2" width="10%">
                                                                                            Loan Amount
                                                                                        </th>
                                                                                        <th rowspan="2" width="12%">
                                                                                            Sanction Date
                                                                                        </th>
                                                                                        <th rowspan="2" width="10%">
                                                                                            Availed Amount
                                                                                        </th>
                                                                                        <th rowspan="2" width="12%">
                                                                                            Availed Date
                                                                                        </th>
                                                                                        <th rowspan="2" width="5%">
                                                                                            Add More
                                                                                        </th>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <th>
                                                                                            State
                                                                                        </th>
                                                                                        <th>
                                                                                            City
                                                                                        </th>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>
                                                                                            -
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_WC_Financial_Institution" CssClass="form-control" runat="server"
                                                                                                MaxLength="100" ToolTip="Enter Name of the Financial Institution Here !!"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender58" runat="server" TargetControlID="Txt_WC_Financial_Institution"
                                                                                                FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom" ValidChars=",-/. ">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_WC_State" CssClass="form-control" runat="server" MaxLength="50"
                                                                                                ToolTip="Enter State Here !!"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender59" runat="server" TargetControlID="Txt_WC_State"
                                                                                                FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_WC_City" CssClass="form-control" runat="server" MaxLength="50"
                                                                                                ToolTip="Enter City Here !!"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender60" runat="server" TargetControlID="Txt_WC_City"
                                                                                                FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=",-/. ">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_WC_Amount" CssClass="form-control text-right" runat="server" onkeypress="return FloatOnly(event, this);"
                                                                                                MaxLength="10" ToolTip="Enter Loan Amount Here !!"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender21" runat="server" Enabled="True"
                                                                                                TargetControlID="Txt_WC_Amount" FilterType="Numbers,Custom" ValidChars=".">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="input-group  date datePicker" id="Div1">
                                                                                                <asp:TextBox ID="Txt_WC_Sanction_Date" runat="server" class="form-control" MaxLength="11"
                                                                                                    ToolTip="Enter Sanction Date Here !!"></asp:TextBox>
                                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:TextBox ID="Txt_WC_Avail_Amount" CssClass="form-control text-right" runat="server" onkeypress="return FloatOnly(event, this);"
                                                                                                MaxLength="10" ToolTip="Enter Avail Amount Here !!"></asp:TextBox>
                                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender22" runat="server" Enabled="True"
                                                                                                TargetControlID="Txt_WC_Avail_Amount" FilterType="Numbers,Custom" ValidChars=".">
                                                                                            </cc1:FilteredTextBoxExtender>
                                                                                        </td>
                                                                                        <td>
                                                                                            <div class="input-group  date datePicker" id="Div4">
                                                                                                <asp:TextBox ID="Txt_WC_Availed_Date" runat="server" class="form-control" MaxLength="11"
                                                                                                    ToolTip="Enter Avail Date Here !!"></asp:TextBox>
                                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                                            </div>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:LinkButton ID="LnkBtn_WC_Add_More" OnClick="LnkBtn_WC_Add_More_Click" CssClass="btn btn-success btn-sm" runat="server"
                                                                                                OnClientClick="return validateWCLoanT();" ToolTip="Click Here to Add"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                                <asp:GridView ID="Grd_WC" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                                                                    ShowHeader="false">
                                                                                    <Columns>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_WC_Sl_No" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="4%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_WC_Financial_Inst" runat="server" Text='<%# Eval("vchNameOfFinancialInst") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_WC_State" runat="server" Text='<%# Eval("vchState") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="13%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_WC_City" runat="server" Text='<%# Eval("vchCity") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="13%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_WC_Amount" runat="server" Text='<%# Eval("decLoanAmt") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_WC_Sanction_Date" runat="server" Text='<%# Eval("dtmSanctionDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="12%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_WC_Avail_Amt" runat="server" Text='<%# Eval("decAvailedAmt") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="10%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="Lbl_WC_Avail_Date" runat="server" Text='<%# Eval("dtmAvailedDate", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="12%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="ImgBtn_Delete_WC" OnClick="ImgBtn_Delete_WC_Click" runat="server" ImageUrl="~/Portal/images/deleteIcon.png"
                                                                                                    CommandArgument='<%# Container.DataItemIndex %>' 
                                                                                                    ToolTip="Click Here to Remove" />
                                                                                            </ItemTemplate>
                                                                                            <ItemStyle Width="5%"></ItemStyle>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                     <div class="form-group">
                                                                         <div class="row">
                                                                                <%--<label class="col-sm-4">
                                                                                  17.
                                                                                </label>--%>
                                                                                </div>
                                                                          
                                                                        <div class="row">
                                                                             
                                                                            <label for="Iname" class="col-sm-4">
                                                                               17. Whether the project needs clearance from Pollution Control Board
                                                                            </label>
                                                                            <div class="col-sm-2">
                                                                                <span class="colon">:</span>
                                                                                <asp:RadioButtonList ID="Rad_Clearnce_PCB" runat="server" CssClass="radio-inline"
                                                                                    RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                                    ToolTip="Choose Whether the project needs clearance from Pollution Control Board Here !!">
                                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                           
                                                                        </div>
                                                                    </div>

                                                                     <div class="form-group">
                                                                          <div class="row">
                                                                               <%-- <label class="col-sm-4">
                                                                                  18.
                                                                                </label>--%>
                                                                                </div>
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                               18. Whether granted with Provisional Priority/thrust Sector Status
                                                                            </label>
                                                                            <div class="col-sm-2">
                                                                                <span class="colon">:</span>
                                                                                <asp:RadioButtonList ID="Rad_PP_thrust_Status" runat="server" CssClass="radio-inline"
                                                                                    RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                                    ToolTip="Choose Whether granted with Provisional Priority/thrust Sector Status Here !!">
                                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                           
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                         <div class="row">
                                                                                <%--<label class="col-sm-4">
                                                                                  19.
                                                                                </label>--%>
                                                                                </div>
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                             19.  IPR Incentives availed
                                                                            </label>
                                                                            <div class="col-sm-2">
                                                                                <span class="colon">:</span>
                                                                                <asp:RadioButtonList ID="Rad_IPR_Incentive_avail" runat="server" CssClass="radio-inline"
                                                                                    RepeatLayout="Flow" RepeatDirection="Horizontal"
                                                                                    ToolTip="Choose IPR Incentives availed Here !!">
                                                                                    <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                    <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                           
                                                                        </div>
                                                                    </div>

                                                                    <div class="form-group">
                                                                        <div class="row">
                                                                            <%--<label class="col-sm-4">
                                                                                20.
                                                                            </label>--%>
                                                                        </div>
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                               20. Whether applied for statutory clearances /Clearances /Approvals under Single Windows Mechanism</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:TextBox ID="Txt_Swm_approve" CssClass="form-control" MaxLength="500" TextMode="MultiLine"
                                                                                    runat="server" ToolTip="Enter  Whether applied for statutory clearances /Clearances /Approvals under SINGLE Windows Mechanism Here !!"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server"
                                                                                    TargetControlID="Txt_Swm_approve" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom"
                                                                                    ValidChars=",-/. ">
                                                                                </cc1:FilteredTextBoxExtender>

                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                
                                                                    <%--
      <small class="text-danger">(.pdf/.zip file only and Max size file Size 2 MB)</small>--%>
                                                                   
                                                                </div>
                                                            </div>
                                                        </div>                                                       
                                                    </div>

                                                        <div class="panel panel-default">
                                                            <div class="panel-heading" role="tab" id="Div_other">
                                                                <%-- <h4 class="panel-title">
                                                <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                    href="#InterestSubsidyDetails" aria-expanded="false" aria-controls="collapseThree">
                                                    <i class="more-less fa  fa-plus"></i>Other Documents</a>
                                            </h4>--%>
                                                                <h4 class="panel-title">
                                                                    <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                                        href="#InterestSubsidyDetails" aria-expanded="false" aria-controls="collapseThree">Other Documents </a>

                                                                </h4>
                                                            </div>
                                                           
                                                          
                                        <div id="InterestSubsidyDetails" class="panel-collapse collapse" role="tabpanel"
                                            aria-labelledby="headingThree">
                                            <div class="panel-body">
                                                <div>
                                                    
                                                    <h2 style="color:blue;font-weight:bold;">Provisional Priority / Thrust Sector Status(Pre-Production)</h2>
                                                </div>

                                                <div class="form-group" id="div8" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            1. Power of Attorney / Board Resolution / Society Resolution as applicable while signing as Partner / Managing Director / Authorized person
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flPowerattpre" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnPowerattpre_code" runat="server" Value="D282" />
                                                                <asp:HiddenField ID="hdnPowerattpre_name" runat="server" />
                                                                <asp:LinkButton ID="lnkUPowerattpre" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDPowerattpre" OnClick="LnkBtn_Delete_Doc_Click"
                                                                    runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypPowerattpre" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblPowerattpre" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="div9" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            2. Certificate of registration under Indian Partnership Act-1932 / Societies Registration Act-1860 /Certificate of incorporation (Memorandum of Association & Article of Association) under Company Act-1956
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flcertofreg" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="certofreg_code" runat="server" Value="D283" />
                                                                 <asp:HiddenField ID="certofreg_name" runat="server" />
                                                                <asp:LinkButton ID="lnkUcertofreg" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDcertofreg" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwcertofreg" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblcertofreg" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group" id="div10" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            3. Approved DPR (Details Project Repot) As approved in DLSWCA/SLSWCA/HLCA
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flAppDPR" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnAppDPR_Code" runat="server" Value="D284" />
                                                                <asp:HiddenField ID="hdnAppDPR_Name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUAppDPR" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDAppDPR" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwAppDPR" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblAppDPR" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div12" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            4. EIN / IEM / PEAL approval letter
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flEIN" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnEIN_Code" runat="server" Value="D285" />
                                                                <asp:HiddenField ID="hdnEIN_Name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUEIN" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDEIN" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwEIN" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblEIN" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div13" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            5. Document in support of date of first investment in fixed capital i.e. first investmentin land / building / plant & machinery and balancing equipment 
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flPlantmachinery" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnPlantmachinery_Code" runat="server" Value="D286" />
                                                                <asp:HiddenField ID="hdnPlantmachinery_Name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUPlantmachinery" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDPlantmachinery" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hyVwPlantmachinery" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblPlantmachinery" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div14" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            6. Amount of Capital Investment made / to be made
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flCapitalInvst" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnCapitalInvst_Code" runat="server" Value="D287" />
                                                                <asp:HiddenField ID="hdnCapitalInvst_Name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUCapitalInvst" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDCapitalInvst" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwCapitalInvst" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblCapitalInvst" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div17" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            7. Investment in Plant & machinery made / to be made
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flInvplantmachinary" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnInvplantmachinary_code" runat="server" Value="D288" />
                                                                <asp:HiddenField ID="hdnInvplantmachinary_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUInvplantmachinary" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDInvplantmachinary" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwInvplantmachinary" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblInvplantmachinary" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div18" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            8. Abrief note on proposed production / manufacturing process or service to be provided
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flproposedprod" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnproposedprod_code" runat="server" Value="D289" />
                                                                <asp:HiddenField ID="hdnproposedprod_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUproposedprod" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDproposedprod" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwproposedprod" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblproposedprod" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div19" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            9.  Abrief note on the present stage of implementation
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flpresentstageimplemnt" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnpresentstageimplemnt_code" runat="server" Value="D290" />
                                                                <asp:HiddenField ID="hdnpresentstageimplemnt_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUpresentstageimplemnt" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDpresentstageimplemnt" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwpresentstageimplemnt" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblpresentstageimplemnt" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div20" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            10. Certificate on Migrated industrial unit treated as new industrial unit under Priority or Thrust Sector
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flmigrantindustrial" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnmigrantindustrial_code" runat="server" Value="D291" />
                                                                <asp:HiddenField ID="hdnmigrantindustrial_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUmigrantindustrial" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDmigrantindustrial" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwmigrantindustrial" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblmigrantindustrial" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div22" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            11. Undertaking in context of that Industrial units shall have to go in to production within three years for MSMEs and within five years for Large Industrial units from the date of starting first fixed capital investment
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flfixedcapitalinvst" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnfixedcapitalinvst_code" runat="server" Value="D292" />
                                                                <asp:HiddenField ID="hdnfixedcapitalinvst_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUfixedcapitalinvst" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDfixedcapitalinvst" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwfixedcapitalinvst" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblfixedcapitalinvst" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div21" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            12. Any other document / certificate in support of Catagory fal under Priority or Thrust Sector 
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flcatagoryfalpriority" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdncatagoryfalpriority_code" runat="server" Value="D293" />
                                                                <asp:HiddenField ID="hdncatagoryfalpriority_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUcatagoryfalpriority" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDcatagoryfalpriority" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwcatagoryfalpriority" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblcatagoryfalpriority" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div>

                                                    <h2 style="color: orange; font-weight: bold;">Priority or Thrust Sector Status(Post Production)</h2>
                                                </div>

                                                <div class="form-group" id="div11" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            1. Power of Attorney / Board Resolution / Spciety Resolution as applicable while signing as Partner / Managing Director / Authorized person
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flPowerattpost" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnPowerattpost_code" runat="server" Value="D294" />
                                                                <asp:HiddenField ID="Powerattpost_name" runat="server" />
                                                                <asp:LinkButton ID="lnkUPowerattpost" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDPowerattpost" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwPowerattpost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblPowerattpost" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div23" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            2.Provisional Priority or Thrust Status Certificate in original if issued
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flpporthrustcertificate" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnpporthrustcertificate_code" runat="server" Value="D295" />
                                                                <asp:HiddenField ID="hdnpporthrustcertificate_name" runat="server" />
                                                                <asp:LinkButton ID="lnkUpporthrustcertificate" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDpporthrustcertificate" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwpporthrustcertificate" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblpporthrustcertificate" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div24" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            3. Certificate of registration under Indian Partnership Act-1932 / Societies Registration Act-1860 /Certificate of incorporation (Memorandum of Association & Article of Association) under Company Act-1956
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flcertofregpost" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdncertofregpost_code" runat="server" Value="D296" />
                                                                <asp:HiddenField ID="hdncertofregpost_name" runat="server" />
                                                                <asp:LinkButton ID="lnkUcertofregpost" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDcertofregpost" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwcertofregpost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblcertofregpost" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div25" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            4. Approved DPR (Details Project Repot) As approved in DLSWCA/SLSWCA/HLCA
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flAppDPRpost" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnAppDPRpost_code" runat="server" Value="D297" />
                                                                <asp:HiddenField ID="hdnAppDPRpost_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUAppDPRpost" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDAppDPRpost" OnClick="LnkBtn_Delete_Doc_Click" runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwAppDPRpost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblAppDPRpost" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div26" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            5. Production Certificate / PEAL approval letter with undertaking for PC
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flPCorEINPost" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnPCorEINPost_code" runat="server" Value="D298" />
                                                                <asp:HiddenField ID="hdnPCorEINPost_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUPCorEINPost" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDPCorEINPost" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwPCorEINPost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblPCorEINPost" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div27" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            6. Loan sanction order ofBank / FI if applied/availed
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flloansancorFIappliedpost" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnloansancorFIappliedpost_code" runat="server" Value="D299" />
                                                                <asp:HiddenField ID="hdnloansancorFIappliedpost_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUloansancorFIappliedpost" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDloansancorFIappliedpost" OnClick="LnkBtn_Delete_Doc_Click" runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwloansancorFIappliedpost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblloansancorFIappliedpost" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div28" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            7. Amount of Capital Investment made / to be made 
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flCapitalInvstPost" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnCapitalInvstPost_code" runat="server" Value="D300" />
                                                                <asp:HiddenField ID="hdnCapitalInvstPost_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUCapitalInvstPost" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDCapitalInvstPost" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwCapitalInvstPost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblCapitalInvstPost" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div29" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            8. Investment in Plant & machinery made / to be made
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flInvplantmachinaryPost" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnflInvplantmachinaryPost_code" runat="server" Value="D301" />
                                                                <asp:HiddenField ID="hdnflInvplantmachinaryPost_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUInvplantmachinaryPost" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDInvplantmachinaryPost" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwInvplantmachinaryPost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblInvplantmachinaryPost" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div30" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            9. Document in support of date of first investment in fixed capital i.e. first investment in land/bilding/plant & machinery and balancing equipment
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flPlantmachinerypost" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnPlantmachinerypost_code" runat="server" Value="D302" />
                                                                <asp:HiddenField ID="hdnPlantmachinerypost_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUPlantmachinerypost" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDPlantmachinerypost" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwPlantmachinerypost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblPlantmachinerypost" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div31" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            10. Details notes on production /manufacturing process or service provided
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flproductionormanufactpost" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnproductionormanufactpost_code" runat="server" Value="D303" />
                                                                <asp:HiddenField ID="hdnproductionormanufactpost_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUproductionormanufactpost" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDproductionormanufactpost" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwproductionormanufactpost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblproductionormanufactpost" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div32" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            11. Document/certificate in support of Catagory fall under Priority Sector / Thrust Sector
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flcatagoryfalprioritypost" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdncatagoryfalprioritypost_code" runat="server" Value="D304" />
                                                                <asp:HiddenField ID="hdncatagoryfalprioritypost_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUcatagoryfalprioritypost" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDcatagoryfalprioritypost" OnClick="LnkBtn_Delete_Doc_Click" runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwcatagoryfalprioritypost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblcatagoryfalprioritypost" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div33" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            12. Clearance from Pollution Control Board and or requied statutory clearances or Undertaking for CTO with copy of CTE
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flclearancefromPCB" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnclearancefromPCB_code" runat="server" Value="D305" />
                                                                <asp:HiddenField ID="hdnclearancefromPCB_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUclearancefromPCB" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDclearancefromPCB" OnClick="LnkBtn_Delete_Doc_Click" runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwclearancefromPCB" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblclearancefromPCB" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div34" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            13. Certificate/Document on Migrated industrial unit treated as new industrial unit under Priority or Thrust Sector / Rehabilitated sick industrial unitseizwd under Section 29 of the State FInncial Corporation Act, 1951/SARFAESI Act,2002 or under the provisions
                                                            of Insolvency and Bankruptcy Code of India,2016 or any other applicable provisions of law and thereafter sold to a new enterpreneur on sale of assets basis and treated as new industrial unit for the purpose of this IPR under Priority or Thrust Sector
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flmigratedindustunitpost" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnmigratedindustunitpost_code" runat="server" Value="D306" />
                                                                <asp:HiddenField ID="hdnmigratedindustunitpost_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUmigratedindustunitpost" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDmigratedindustunitpost" OnClick="LnkBtn_Delete_Doc_Click" runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwmigratedindustunitpost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblmigratedindustunitpost" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div35" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            14.Undertaking in context of that Industrial units shall have to go in to production within three years for MSMEs and within five years for Large Industrial units from the date of starting first fixed capital investment
                                                        </label> 
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flproductionforMSMEPost" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnproductionforMSMEPost_code" runat="server" Value="D307" />
                                                                <asp:HiddenField ID="hdnproductionforMSMEPost_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUproductionforMSMEPost" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDproductionforMSMEPost" OnClick="LnkBtn_Delete_Doc_Click" runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwproductionforMSMEPost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblproductionforMSMEPost" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div36" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            15.Document in support of delay in implementation condoned by Empowered Committee
                                                        </label> 
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flEmpoweredcommitpost" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnEmpoweredcommitpost_code" runat="server" Value="D308" />
                                                                <asp:HiddenField ID="hdnEmpoweredcommitpost_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUEmpoweredcommitpost" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDEmpoweredcommitpost" OnClick="LnkBtn_Delete_Doc_Click" runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwEmpoweredcommitpost" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblEmpoweredcommitpost" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>





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
                                                <asp:Button ID="BtnApply" runat="server" style="font-weight:bold;" Text="Submit" OnClick="BtnApply_Click" 
 CssClass="btn btn-success" OnClientClick="return validateThrustprioritysectorstatus();" 
                                                     ToolTip="Click Here to Submit" />
                                                <asp:Button ID="BtnDraft" style="font-weight:bold;" runat="server" OnClick="BtnDraft_Click" Text="Draft" 
 CssClass="btn btn-warning" OnClientClick="return validateThrustprioritysectorstatus();" 
                                                     ToolTip="Click Here to Draft" />
                                                 <asp:Button ID="BtnCancel" runat="server" style="font-weight:bold;" OnClick="BtnCancel_Click" Text="Cancel" CssClass="btn btn-danger"
                                                     ToolTip="Click Here to Cancel" />
                                                <asp:HiddenField ID="Hid_Is_Exist_Before" runat="server" />
                                                <asp:HiddenField ID="Hid_Is_Exist_After" runat="server" />
                                                <asp:HiddenField ID="Hid_Data_Source" runat="server" />
                                                <asp:HiddenField ID="Hid_PC_Status" runat="server" />
                                                <asp:HiddenField ID="Hid_Project_Type" runat="server" />
                                                <asp:HiddenField ID="Hid_Inct_Mode" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-footer">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <h4 style="color: #777;">
                                                    General Instructions (To fill up subsequent incentive applications)</h4>
                                                <div class="listdiv">
                                                    <ol>
                                                        <li>Need to be provid by department </li>
                                                        
                                                    </ol>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkUPowerattpre" />
            <asp:PostBackTrigger ControlID="lnkUcertofreg" />
            <asp:PostBackTrigger ControlID="lnkUAppDPR" />
            <asp:PostBackTrigger ControlID="lnkUEIN" />
            <asp:PostBackTrigger ControlID="lnkUPlantmachinery" />
            <asp:PostBackTrigger ControlID="lnkUCapitalInvst" />
            <asp:PostBackTrigger ControlID="lnkUInvplantmachinary" />
            <asp:PostBackTrigger ControlID="lnkUproposedprod" />
            <asp:PostBackTrigger ControlID="lnkUpresentstageimplemnt" />
            <asp:PostBackTrigger ControlID="lnkUmigrantindustrial" />
            <asp:PostBackTrigger ControlID="lnkUfixedcapitalinvst" />
            <asp:PostBackTrigger ControlID="lnkUcatagoryfalpriority" />
            <asp:PostBackTrigger ControlID="lnkUPowerattpost" />
            <asp:PostBackTrigger ControlID="lnkUpporthrustcertificate" />
            <asp:PostBackTrigger ControlID="lnkUcertofregpost" />
            <asp:PostBackTrigger ControlID="lnkUAppDPRpost" />
            <asp:PostBackTrigger ControlID="lnkUPCorEINPost" />
            <asp:PostBackTrigger ControlID="lnkUloansancorFIappliedpost" />
            <asp:PostBackTrigger ControlID="lnkUCapitalInvstPost" />
            <asp:PostBackTrigger ControlID="lnkUInvplantmachinaryPost" />
            <asp:PostBackTrigger ControlID="lnkUPlantmachinerypost" />
            <asp:PostBackTrigger ControlID="lnkUproductionormanufactpost" />
            <asp:PostBackTrigger ControlID="lnkUcatagoryfalprioritypost" />
            <asp:PostBackTrigger ControlID="lnkUclearancefromPCB" />
            <asp:PostBackTrigger ControlID="lnkUmigratedindustunitpost" />
            <asp:PostBackTrigger ControlID="lnkUproductionforMSMEPost" />
            <asp:PostBackTrigger ControlID="lnkUEmpoweredcommitpost" />
            
              
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField ID="Hid_Pop" runat="server" />
    <asp:HiddenField ID="Hid_Pop_2" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1"
        TargetControlID="Hid_Pop" BackgroundCssClass="modalBackground" CancelControlID="Btn_Close">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="modalfade" Style="display: none;">
        <div id="undertakingipr2015">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header bg-purpul">
                        <h4 class="modal-title">
                            Please provide undertaking stating that your unit is not a part of the negative
                            unit list under <strong>IPR 2015</strong></h4>
                    </div>
                    <div class="modal-body">
                        <h4>
                            UNDERTAKING</h4>
                        <p>
                            I hereby declare that my Unit/Enterprise does not fall under the following ineligible
                            unit.
                        </p>
                        <%--   <p>
                            In my application, I will produce required documents for the same.</p>--%>
                        <h4>
                            List of Ineligible Unit Types</h4>
                        <h5 class="text-red">
                            Reference IPR-2015 Point-16,Annexure II</h5>
                        <div class="listdiv">
                            <ul type="I">
                                <li>B. Industrial Unit will not include non-manufacturing/servicing industries
                                    <ol>
                                        <li>General workshops including repair workshops having investment less than 50 Lakhs
                                            and running with power </li>
                                        <li>Cold storage and seafood freezing unit having investment less than Rs. 25 Lakhs
                                        </li>
                                        <li>Electronics repair and maintenance unit for professional grade equipment and computer
                                            software ITES/BPO and related with invest less than Rs. 25 lakhs </li>
                                        <li>Technology Development Laboratory/Prototype Development Centre/Research & Development
                                            with investment less than Rs. 25 Lakh </li>
                                        <li>Printing press with investment in plant and machinery/equipment of less than Rs.
                                            50 Lakhs </li>
                                        <li>Laundry/Dry Cleaning with investment in plant and machinery/equipment of less than
                                            Rs.25 Lakh </li>
                                    </ol>
                                </li>
                                <li>C. The following units shall neither be eligible for fiscal incentives specified
                                    under this IPR nor for allotment of land at concessional rates in the State, but
                                    shall be eligible for investment facilitation, allotment of land under normal rules
                                    at benchmark value/market rate and recommendation to the financial institutions
                                    for term loan and working capital and for recommendation, if necessary to the Power
                                    Distribution Companies:
                                    <ol>
                                        <li>Hullers and Rice mills with investment in plant and machinery of less than Rs.25
                                            Lakhs for industrially backward districts and less than one crore rupees for other
                                            areas </li>
                                        <li>Flour mills including manufacture of besan, pulse mills and chuda mills expect investment
                                            in plant and machinery of more than Rs. 25 Lakhs for industrially backward districts
                                            and less than 1 crore for other areas (Excluding Roller Flour mills) </li>
                                        <li>
                                            <ol>
                                                1. Processing of spices with investment in plant and machinery with less than Rs
                                                10 lakhs for industrially backward districts and less than 2 crore rupees for other
                                                areas.
                                            </ol>
                                            <ol>
                                                2. Units without Spice-mark or Agmark
                                            </ol>
                                        </li>
                                        <li>Confectionary with investment in plant and machinery with less than Rs.10 Lakhs
                                            for industrially backward districts and less than two crore rupees for other areas
                                        </li>
                                        <li>Oil mills with expellers including oil processing, filtering , de-coloring ,coloring
                                            ,refining of edible oils and hydro-generation thereof except investment in plant
                                            and machinery of RS. 10 Lakhs in industrial backward areas. </li>
                                        <li>Preparation of sweets and savories etc. excluding units using mechanized process
                                            with an investment in plant and machinery </li>
                                        <li>Bread making(excluding mechanized bakery) </li>
                                        <li>Mixture.Bhujia and chanachur preparation units </li>
                                        <li>Manufacture of ice candy </li>
                                        <li>Manufacture and processing of betel nuts </li>
                                        <li>Hatcheries, Piggeries, rabbit or Broiler farming </li>
                                        <li>Standalone sponge iron plants </li>
                                        <li>Iron and steel processors, such as cutting of sheets,bars,angles,coils,M.S. sheets,
                                            recoiling, straightening,corrugating,drophammer units etc with low value addition
                                        </li>
                                        <li>Cracker-making units </li>
                                        <li>Tyre retreading units with investment in plant and machinery of less Rs.20 Lakhs
                                        </li>
                                        <li>Stone crushing units </li>
                                        <li>Coal/coke screening coal /coke Briquetting </li>
                                        <li>Production of firewood and charcoal </li>
                                        <li>Painting and spray-painting units with investment in plant and machinery of less
                                            than Rs. 20 Lakhs </li>
                                        <li>Units for physical mixing of fertilizers. </li>
                                        <li>Brick- making units (except units making refractory bricks and those making bricks
                                            from flyash, red mud and similar industrial waste not less than 25% as base martial)
                                        </li>
                                        <li>Manufacturing of tarpaulin out of canvas cloth with investment in plant and machinery
                                            of less than Rs. 20 Lakhs. </li>
                                        <li>Saw mills, sawing of timber. </li>
                                        <li>Carpentry, joinery and wooden furniture making except when part of a wood based
                                            cluster of at least 20 units. </li>
                                        <li>Drilling rigs, Bore-wells and Tube-wells </li>
                                        <li>Units for mixing or blending/packaging of tea. </li>
                                        <li>Units for cutting raw tobacco and sprinkling jiggery for chewing purpose and Gudakhu
                                            manufacturing units. </li>
                                        <li>Units for bottling of medicines. </li>
                                        <li>Bookbinding/Rubber stamp making/making notebooks, exercise notebook s and envelopes.
                                        </li>
                                        <li>Distilled water units </li>
                                        <li>Tailoring (other than readymade garment manufacturing units) </li>
                                        <li>Repacking /stitching/printing of woven sacks out of woven fabrics. </li>
                                        <li>Pre-Processing of oil seeds-Decorticating, expelling, crushing, parching and frying.
                                        </li>
                                        <li>Aerated water and soft drinks units </li>
                                        <li>Bottling units or any activity in respect of IMFL or liquor of any kind </li>
                                        <li>Size reducing/size separating units/ Grinding / mixing units with investment in
                                            plant and machinery of less than ten crore rupees except manufacturing of cement
                                            with clinker. </li>
                                        <li>Polythene less than 40 micron in thickness /recycling of plastic materials.
                                        </li>
                                        <li>Thermal power plants. </li>
                                        <li>Repackaging units. </li>
                                        <%--  <li>Industries falling within the purview of the following Boards and public Agencies.
                                    <ol>
                                        <li>Coir Board </li>
                                        <li>Silk Board</li>
                                        <li>All India handloom and Handicraft Board</li>
                                        <li>Khadi and village industries Commission/Board</li>
                                        <li>Any other Agency con situation by Government for industrial department.</li>
                                    </ol>
                                </li>--%>
                                    </ol>
                                    <small class="text-red">Note: List of industrial units indicated above may be modified
                                        by the Government from time to time.</small> </li>
                            </ul>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <div class="row">
                            <div class="col-sm-6 text-left">
                                <asp:CheckBox ID="ChkBx_Agree" runat="server" Text="I agree that provided information is correct." /></div>
                            <div class="col-sm-6">
                                <asp:Button ID="Btn_Submit" runat="server" Text="Submit"
                                    class="btn btn-success" OnClientClick="return validate_checkbox();" ToolTip="Click Here to Submit and Proceed" />
                                <asp:Button ID="Btn_Close" runat="server" Text="Close" class="btn btn-danger" ToolTip="Click Here to Close Window" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
    </asp:Panel>
    <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel2"
        TargetControlID="Hid_Pop_2" BackgroundCssClass="modalBackground" CancelControlID="Btn_No">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" CssClass="modalfade" Style="display: none;">
        <div id="Div2">
            <div class="modal-dialog modal-lg">
                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header bg-purpul">
                        <h4 class="modal-title">
                            Alert</h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            Your unit has exceeded the time period permitted by the IPR 2015 to commence production
                            from the Date of FFCI by
                            <asp:Label ID="Lbl_Dynamic_No" runat="server"></asp:Label>
                            years. You may only apply for incentives, once your application for condonation
                            of delay in implementation is approved by the Empowered Committee .
                        </p>
                        <p>
                            Do you wish to proceed to apply for condonation of delay?
                            <asp:Button ID="Btn_Yes" runat="server" Text="Yes"  class="btn btn-success"
                                ToolTip="Click here if you wish to proceed to apply for condonation of delay" />
                            <asp:Button ID="Btn_No" runat="server" Text="No" class="btn btn-danger" ToolTip="Click here if you don't wish to apply for condonation of delay." />
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    <uc3:footer ID="footer" runat="server" />
   <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />



   
    <script type="text/javascript">

        function pageLoad() {
            $(function () {
                $('.datePicker').datepicker({
                    minDate: new Date(),
                    autoclose: true,
                    format: "dd-M-yyyy",
                    clearBtn: true
                });
            });

            $('.menuincentive').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });
            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #ProductionEmploymentDetails, #IndustryDetails").removeClass('in');
                $(hdn).addClass("in");
            }

            $(".panel-title > a").on("click", function () {
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });
        }

        function validate_checkbox() {
            if (document.getElementById('<%= ChkBx_Agree.ClientID %>').checked == false) {
                jAlert('<strong>Please Click on CheckBox to Agree !!</strong>', projname);
                document.getElementById('<%= ChkBx_Agree.ClientID %>').focus();
                return false;
            }
        }

    </script>
   
    </form>
</body>
</html>
