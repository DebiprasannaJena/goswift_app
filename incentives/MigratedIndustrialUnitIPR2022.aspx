<%--'*******************************************************************************************************************
' File Name         : MigratedIndustrialUnitIPR2022.aspx
' Description       : Migrated Industrial Unit IPR-2022 Add and Draft Page
' Created by        : Debiprasanna Jena
' Created On        : 17th Nov 2023
' Modification History:

'<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

'   *********************************************************************************************************************--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MigratedIndustrialUnitIPR2022.aspx.cs" Inherits="incentives_MigratedIndustrialUnitIPR2022" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="investoemenu" TagPrefix="uc4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> </title>
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <link href="../css/incentive.css" rel="stylesheet" type="text/css"/>
    <script src="../js/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../js/WebValidation.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Basic_Details.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>



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
            var cc = $('#Txt_Address_unit').val();
            if ($("#ChkSameData").is(':checked')) {
                $('#Txt_Regd_Office_Address').val(cc);
            }
        }

        ///*------------------------------------------------------------------------------------------------------------------------*/
        /// Term Loan (Add More)

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
           
            if (new Date($('#Txt_TL_Availed_Date').val()) < new Date($('#Txt_TL_Sanction_Date').val())) {
                jAlert('<strong>Availed Date cannot be less than sanction date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_TL_Availed_Date").focus(); });
                return false;
            }
        }

        ///*------------------------------------------------------------------------------------------------------------------------*/
        /// Working Capital Loan (Add More)

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
           
            if (new Date($('#Txt_WC_Availed_Date').val()) < new Date($('#Txt_WC_Sanction_Date').val())) {
                jAlert('<strong>Availed Date cannot be less than sanction date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_WC_Availed_Date").focus(); });
                return false;
            }
        }

     /*------------------------------------------------------------------------------------------------------------------------*/
        /// Total Investment Amount Calculation (Before)

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

      /*------------------------------------------------------------------------------------------------------------------------*/
        /// Total Investment Amount Calculation (After)

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

  ///*-----------------------------------------------------------------------------------------------------------------------*//
        ///Details of incentive availed,if any under earlier IPRs 
  ///*----------------------------------------------------------------------------------------------------------------------*//
        function validateItemAdd() {
            debugger;
            if (!blankFieldValidation('Txt_Incentive', 'Incentive', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_Incentive', 'Incentive', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Incentive").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_Quantum', 'Quantum/ Value', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_Quantum', 'Quantum/ Value', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Quantum").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_Perod', 'Period', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_Perod', 'Period', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Perod").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_Ipr_Applicability', 'IPR Applicability', projname)) {
                return false;
            }
            if (!WhiteSpaceValidation1st('Txt_Ipr_Applicability', 'IPR Applicability', projname)) {
                $("#popup_ok").click(function () { $("#Txt_Ipr_Applicability").focus(); });
                return false;
            }
        }

///*-------------------------------------------------------------------------------------------------------------------------*/
        ///Add by Debiprasanna Jena on Dt-11-07-2023
        function validateThrustprioritysectorstatus() {
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
            if (Phoneno < 8 || Phoneno > 15) {
                jAlert('<strong>The minimum and maximum length of the mobile number should be 8 to 15 digit.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Phone_no").focus(); });
                return false;
            }
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

            var orgName = $('#Lbl_Org_Name_Type').text();
            if (!WhiteSpaceValidation1st('Txt_Partner_Name', orgName, projname)) {
                $("#popup_ok").click(function () { $("#Txt_Partner_Name").focus(); });
                return false;
            }
           
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
        <uc2:header ID="header" runat="server" />
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
                <div class="container wrapper">
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
                                           
                                            <li><a href="ViewApplicationStatus.aspx" title="Click Here to View Application Status !!">
                                                View Application Status</a></li>
                                        </ul>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="form-header">                                      
                                        <h2>
                                            Migrated Industrial Unit IPR-2022</h2>
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
                                                                                </div>
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4 ">
                                                                                   2.Detailed address of the Unit &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_Address_unit" CssClass="form-control" MaxLength="500" TextMode="MultiLine"
                                                                                        runat="server" ToolTip="Enter Detailed address of the Unit Here !!"></asp:TextBox>
                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender24" runat="server"
                                                                                        TargetControlID="Txt_Address_unit" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom"
                                                                                        ValidChars=",-/. ">
                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    

                                                                     

                                                                        <div class="form-group">
                                                                            
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    3.Address of Registered Office Unit &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:CheckBox ID="ChkSameData" runat="server" Text="Same as Detailed address of the Unit"
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
                                                                                <label for="Iname" class="col-sm-4 ">
                                                                                   4.Category of the  Unit  &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:DropDownList ID="DrpDwn_Unit_Cat" CssClass="form-control" runat="server" ToolTip="Select Category of the  Unit Here !!">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                            
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                    5.Type of  Organization  &nbsp;</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:DropDownList ID="DrpDwn_Org_Type" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DrpDwn_Org_Type_SelectedIndexChanged" ToolTip="Select Type of  Organization Here !!">
                                                                                    </asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="form-group">
                                                                           
                                                                            <div class="row">
                                                                                <label for="Iname" class="col-sm-4">
                                                                                   6. <asp:Label ID="Lbl_Org_Name_Type" runat="server" Text="Name of Managing Partner"></asp:Label>
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
                                                                               

                                                                                <label for="Iname" class="col-sm-4">
                                                                                   7. EIN/ IEM/ IL/In principle approval letter No. issued by SLNA/DLNA</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <asp:TextBox ID="Txt_EIN_IL_NO" Onkeypress="return inputLimiter(event,'NameCharactersAndNumbers')" CssClass="form-control" MaxLength="100" runat="server"
                                                                                        ToolTip="Enter EIN/ IEM/ IL/In principle approval letter No. issued by SLNA/DLNA Here !!"></asp:TextBox>
                                                                                   
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="form-group">
                                                                             
                                                                            <div class="row">
                                                                                
                                                                                <label for="Iname" class="col-sm-4">
                                                                                 EIN/IEM/IL/In principle approval Date issued by SLNA/DLNA</label>
                                                                                <div class="col-sm-8">
                                                                                    <span class="colon">:</span>
                                                                                    <div class="input-group date datePicker" id="Div_Date_EIN" runat="server">
                                                                                        <asp:TextBox ID="Txt_EIN_IL_Date" CssClass="form-control" type="text" runat="server"
                                                                                            MaxLength="11" ToolTip="Enter EIN/IEM/IL/In principle approval Date issued by SLNA/DLNA Here !!"></asp:TextBox>
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

                                                                              <div class="form-group">
                                                                               
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                               8. Date of first fixed capital investment i.e land / building / plant and machinery and balacing equipment</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group date datePicker" id="Div7" runat="server">
                                                                                    <asp:TextBox ID="Txt_Proposed_Date" CssClass="form-control" type="text" runat="server"
                                                                                        MaxLength="11" ToolTip="Enter Date of first fixed capital investment i.e land / building / plant and machinery and balacing equipment Here !!"></asp:TextBox>
                                                                                    <span id="Span1" runat="server" class="input-group-addon"><i class="fa fa-calendar">
                                                                                    </i></span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                             <div class="form-group">
                                                                                  
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                               9.  Date of Commencement of production / Activity(estimated)</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <div class="input-group date datePicker" id="Div5" runat="server">
                                                                                    <asp:TextBox ID="Txt_Commence_production" CssClass="form-control" type="text" runat="server"
                                                                                        MaxLength="11" ToolTip="Enter  Date of Commencement of production / Activity(estimated) Here !!"></asp:TextBox>
                                                                                    <span id="Span4" runat="server" class="input-group-addon"><i class="fa fa-calendar">
                                                                                    </i></span>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                            <div id="Div_Prod_Emp_Before" runat="server">
                                                                               
                                                                                <div class="form-group">
                                                                                   
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
                                                                        
                                                                            <h4>
                                                                                <asp:Label ID="Lbl_Header_Prod_Emp" runat="server"></asp:Label></h4>

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
                                                                                <label for="Iname" class="col-sm-12 ">
                                                                                   11. Total Capital Investment</label>
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
                                                                  
                                                                    <div class="form-group row">
                                                                       
                                                                        <label class="col-sm-2">                                                                                                                                                             
                                                                            12.Internal sources(Lakhs) &nbsp; </label>
                                                                        <div class="col-sm-4">                                                                          
                                                                            <span class="colon">:</span>
                                                                            <asp:TextBox ID="Txt_Equity_Amt" CssClass="form-control text-right" runat="server" Text="0"
                                                                                onkeypress="return FloatOnly(event, this);" MaxLength="10" ToolTip="Enter Internal sources Here !!"></asp:TextBox>
                                                                           
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender25" runat="server" TargetControlID="Txt_Equity_Amt"
                                                                                FilterMode="ValidChars" FilterType="Custom, Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </div>
                                                                       
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
                                                                            <label for="Iname" class="col-sm-4">
                                                                             13. Details of incentive availed,if any under earlier IPRs</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                              <asp:RadioButtonList ID="Rad_Incentive_availed" OnSelectedIndexChanged="Rad_Incentive_availed_SelectedIndexChanged"  AutoPostBack="true" runat="server" RepeatDirection="Horizontal"
                                                                                        CssClass="radio-inline">
                                                                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                                                                        <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                      <div id="Div_Incentive_Availed" runat="server">

                                                                            <div class="form-group">                                                                             
                                                                                <div class="row">
                                                                                    <label for="Iname" class="col-sm-12">
                                                                                       Details of incentive availed&nbsp;
                                                                                    </label>
                                                                                    <div class="col-sm-12  margin-bottom10">
                                                                                        <table class="table table-bordered">
                                                                                            <tr>
                                                                                                <th width="5%">SlNo
                                                                                                </th>
                                                                                                <th> Incentive
                                                                                                </th>
                                                                                                <th width="15%">Quantum / Value
                                                                                                </th>
                                                                                                <th width="20%">Period
                                                                                                </th>
                                                                                                <th width="20%">IPR applicability
                                                                                                </th>
                                                                                              
                                                                                                <th width="5%">Action
                                                                                                </th>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>&nbsp;
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Incentive" runat="server" CssClass="form-control"
                                                                                                        MaxLength="100" ToolTip="Enter Incentive Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender26" runat="server" TargetControlID="Txt_Incentive"
                                                                                                        FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Quantum" runat="server" CssClass="form-control" MaxLength="10"
                                                                                                        onkeypress="return FloatOnly(event, this);" ToolTip="Enter Quantum / Value Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender28" runat="server" TargetControlID="Txt_Quantum"
                                                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Perod" runat="server" CssClass="form-control" MaxLength="10"
                                                                                                        onkeypress="return FloatOnly(event, this);" ToolTip="Enter Period Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender30" runat="server" TargetControlID="Txt_Perod"
                                                                                                        FilterType="Numbers,Custom" ValidChars=".">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="Txt_Ipr_Applicability" runat="server" CssClass="form-control" MaxLength="10"
                                                                                                       ToolTip="Enter IPR applicability Here !!"></asp:TextBox>
                                                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender31" runat="server" TargetControlID="Txt_Ipr_Applicability"
                                                                                                        FilterType="LowercaseLetters,UppercaseLetters,Custom" ValidChars=" ">
                                                                                                    </cc1:FilteredTextBoxExtender>
                                                                                                </td>
                                                                                                 
                                                                                                <td>
                                                                                                    <asp:LinkButton ID="LnkBtn_Add_Item" OnClick="LnkBtn_Add_Item_Click" CssClass="btn btn-success btn-sm"  runat="server" OnClientClick="return validateItemAdd();" 
                                                                                                        ToolTip="Click Here to Add Items !!"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                        <asp:GridView ID="Grd_Incentive" runat="server" CssClass="table table-bordered"
                                                                                            AutoGenerateColumns="false" ShowHeader="false">
                                                                                            <Columns>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Sl_No_Incentive_Avail" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="5%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Incentive" runat="server" Text='<%# Eval("vchIncentive ") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Quantum" runat="server" Text='<%# Eval("decValue ") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="15%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_Period" runat="server" Text='<%# Eval("decPeriod ") %>'></asp:Label>
                                                                                                        <%--<asp:HiddenField ID="Hid_Unit_Before" runat="server" Value='<%# Eval("intUnit") %>' />--%>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="10%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="Lbl_IPR_Applicability" runat="server" Text='<%# Eval("vchIPRApplica") %>'></asp:Label>
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle Width="10%"></ItemStyle>
                                                                                                </asp:TemplateField>
                                                                                               
                                                                                                <asp:TemplateField>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:ImageButton ID="ImgBtn_Delete" OnClick="ImgBtn_Delete_Click" runat="server"   ImageUrl="~/Portal/images/deleteIcon.png"
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
                                                                           
                                                                            <h4>
                                                                                <asp:Label ID="Label1" runat="server"></asp:Label></h4>
                                                                        
                                                                           
                                                                         <div class="form-group">
                                                                        
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                               14.Present implementation status</label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:TextBox ID="Txt_Present_status" CssClass="form-control" MaxLength="500"
                                                                                    runat="server" ToolTip="Enter  Present implementation status Here !!"></asp:TextBox>
                                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender23" runat="server"
                                                                                    TargetControlID="Txt_Present_status" FilterType="Numbers,LowercaseLetters,UppercaseLetters,Custom"
                                                                                    ValidChars=",-/. ">
                                                                                </cc1:FilteredTextBoxExtender>

                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    
                                                                            <div class="form-group">
                                                                               
                                                                        <div class="row">
                                                                            <label for="Iname" class="col-sm-4">
                                                                               15. Whether the project needs
                                                                            </label>
                                                                            <div class="col-sm-8">
                                                                                <span class="colon">:</span>
                                                                                <asp:RadioButtonList ID="Rad_Project_needs" class="optradioPriority" runat="server"
                                                                                    CssClass="radio-inline" RepeatLayout="Flow" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="Rad_Project_needs_SelectedIndexChanged"  ToolTip="Choose Whether the project needs Here !!">
                                                                                   
                                                                                    <asp:ListItem Value="1" >Yes</asp:ListItem> 
                                                                                     <asp:ListItem Value="2" Selected="True">No</asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                             <div id="Div_Clearance_pcb" runat="server">
                                                                            <div class="form-group">
                                                                                <div class="row">
                                                                                    <label for="Iname" class="col-sm-4">
                                                                                      Clearance from pollution control board.</label>
                                                                                    <div class="col-sm-8">
                                                                                        <span class="colon">:</span>
                                                                                        <asp:TextBox ID="Txt_Clearance_pcb" CssClass="form-control" MaxLength="100" runat="server"
                                                                                            ToolTip="Enter Clearance from pollution control board Here !!"></asp:TextBox>

                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                                 </div>

                                                              
                                                                </div>
                                                            </div>
                                                        </div>                                                       
                                                    </div>

                                                        <div class="panel panel-default">
                                                            <div class="panel-heading" role="tab" id="Div_other">
                                                               
                                                                <h4 class="panel-title">
                                                                    <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                                                        href="#InterestSubsidyDetails" aria-expanded="false" aria-controls="collapseThree">Other Documents </a>

                                                                </h4>
                                                            </div>
                                                           
                                                          
                                        <div id="InterestSubsidyDetails" class="panel-collapse collapse" role="tabpanel"
                                            aria-labelledby="headingThree">
                                            <div class="panel-body">
                                              

                                                <div class="form-group" id="div8" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            1. Power of Attorney / Board Resolution / Society Resolution as applicable while signing as Partner / Managing Director / Authorized person
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flPoweratt" CssClass="form-control" runat="server" onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnPoweratt_code" runat="server" Value="D334" />
                                                                <asp:HiddenField ID="hdnPoweratt_name" runat="server" />
                                                                <asp:LinkButton ID="lnkUPoweratt" OnClick="LnkBtn_Add_Doc_Click" runat="server" CssClass="input-group-addon bg-green"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDPoweratt" OnClick="LnkBtn_Delete_Doc_Click"
                                                                    runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypPoweratt" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblPoweratt" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
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
                                                                <asp:HiddenField ID="certofreg_code" runat="server" Value="D335" />
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
                                         
                                                <div class="form-group" id="div12" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            3. EIN / IEM / In principle approval letter issued by SLNA/DLNA
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flEIN" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnEIN_Code" runat="server" Value="D336" />
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
                                                            4. Document in support of date of first investment in fixed capital i.e. first investmentin land / building / plant & machinery and balancing equipment 
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flPlantmachinery" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnPlantmachinery_Code" runat="server" Value="D337" />
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

                                                <div class="form-group" id="div27" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            5. Loan sanction order ofBank / FI(if any)
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flloansancorFIapplied" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnloansancorFIapplied_code" runat="server" Value="D338" />
                                                                <asp:HiddenField ID="hdnloansancorFIapplied_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUloansancorFIapplied" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDloansancorFIapplied" OnClick="LnkBtn_Delete_Doc_Click" runat="server" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypvwloansancorFIapplied" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblloansancorFIapplied" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div17" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            6. Documents in support of incentive availed,if any under earlier IPRs.
                                                        </label>
                                                        <div class="col-sm-6"> 
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flIncentiveAvail" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnIncentiveAvail_code" runat="server" Value="D339" />
                                                                <asp:HiddenField ID="hdnIncentiveAvail_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUIncentiveAvail" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDIncentiveAvail" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwIncentiveAvail" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblIncentiveAvail" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group" id="div18" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                             7.Undertaking to the effect that
                                                          
                                                                <ul>
                                                                   
                                                                 (i)it shall go into commercial production within three/five years (three years for Micro,Small & Medium enterprises and five years for the large industries) from the date of first fixed capital investment.                                                            
                                                                  </ul>
                                                         
                                                             <ul>

                                                                 (ii)surrender and / or refund the incentives availed (specifying the quantum,value,period & IPR applicability) under earlier IPRs within 30 days from the date of issue of the advice letter by RIC /DIC or MID,IPICOL and

                                                                 </ul>
                                                            <ul>
                                                                (iii)once the option is exercised,it shall be final and irrevocable.
                                                                </ul>
                                                        </label>
                                                       
                                                      
                                                        <div class="col-sm-6" >
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flUndertakingeffect" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnUndertakingeffect_code" runat="server" Value="D340" />
                                                                <asp:HiddenField ID="hdnUndertakingeffect_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUUndertakingeffect" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDUndertakingeffect" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwUndertakingeffect" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblUndertakingeffect" Style="font-size: 12px;" CssClass="text-blue"
                                                                Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                        </div>
                                                     
                                                        
                                                    </div>
                                                </div>

                                      

                                              


                                                      <div class="form-group" id="div3" runat="server">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-5">
                                                            8.Clearance from pollution control board (CTE) / copy of acknowldgement against submission of application for CTE, if required 
                                                        </label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <div class="input-group">
                                                                <asp:FileUpload ID="flclearancefromPCB" CssClass="form-control" runat="server"
                                                                    onchange="return FileCheck(this);" />
                                                                <asp:HiddenField ID="hdnclearancefromPCB_code" runat="server" Value="D341" />
                                                                <asp:HiddenField ID="hdnclearancefromPCB_name" runat="server" Value="" />
                                                                <asp:LinkButton ID="lnkUclearancefromPCB" runat="server" OnClick="LnkBtn_Add_Doc_Click" CssClass="input-group-addon bg-green"
                                                                    OnClientClick="return HasFile('flCleanApproveAuthority','Plase Upload Factory & Boiler for all industry related Document');"
                                                                    ToolTip="Click here to upload the file."><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkDclearancefromPCB" runat="server" OnClick="LnkBtn_Delete_Doc_Click" CssClass="input-group-addon bg-red"
                                                                    Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                <asp:HyperLink ID="hypVwclearancefromPCB" runat="server" Target="_blank" Visible="false"
                                                                    CssClass="input-group-addon bg-blue"><i class="fa fa-download"></i></asp:HyperLink>
                                                            </div>
                                                            <small class="text-danger">(.pdf/.zip file only and Max size file Size 4 MB)</small>
                                                            <asp:Label ID="lblclearancefromPCB" Style="font-size: 12px;" CssClass="text-blue"
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
                                                <asp:Button ID="BtnApply" runat="server" OnClick="BtnApply_Click" style="font-weight:bold;" Text="Submit"  
 CssClass="btn btn-success" OnClientClick="return validateThrustprioritysectorstatus();" 
                                                     ToolTip="Click Here to Submit" />
                                                <asp:Button ID="BtnDraft" style="font-weight:bold;" OnClick="BtnDraft_Click" runat="server"  Text="Draft" 
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
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkUPoweratt" />
            <asp:PostBackTrigger ControlID="lnkUcertofreg" />
            <asp:PostBackTrigger ControlID="lnkUEIN" />
            <asp:PostBackTrigger ControlID="lnkUPlantmachinery" />
            <asp:PostBackTrigger ControlID="lnkUloansancorFIapplied" />
            <asp:PostBackTrigger ControlID="lnkUIncentiveAvail" />
            <asp:PostBackTrigger ControlID="lnkUUndertakingeffect" />
           <asp:PostBackTrigger ControlID="lnkUclearancefromPCB" />
             
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
