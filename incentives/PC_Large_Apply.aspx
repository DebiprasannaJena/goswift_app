<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PC_Large_Apply.aspx.cs" Inherits="incentives_PC_Large_Apply" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/includes/pealwebdoctype.ascx" TagName="doctype" TagPrefix="uc1" %>
<%@ Register Src="~/includes/pealwebheader.ascx" TagName="header" TagPrefix="uc2" %>
<%@ Register Src="~/includes/investorfooter.ascx" TagName="footer" TagPrefix="uc3" %>
<%@ Register Src="~/includes/PealMenu.ascx" TagName="investerMenu" TagPrefix="uc4" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <uc1:doctype ID="doctype" runat="server" />
    <link href="../css/custom.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/WebValidation.js"> </script>
    <script src="../js/decimalrstr.js" type="text/javascript"></script>
    <script src="../js/Incentive/JS_Inct_Common_Validation.js" type="text/javascript"></script>

    <%--<script type="text/javascript" src="../js/Incentive/Js_PCFormLarge.js"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                format: 'dd-M-yyyy',
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });
        });



        function ValidatePage() {
            var hdnOfflineStatus = $("#hdnOfflineStatus").val();
            if (hdnOfflineStatus != null && hdnOfflineStatus != undefined && (hdnOfflineStatus == "1" || hdnOfflineStatus == "3")) {
                if (!blankFieldValidation('txtOfflinePcNo', 'Production Certificate/IEM-II No.', 'GO-SWIFT')) {
                    CollapsePCDiv();
                    return false;
                }
                if (!blankFieldValidation('txtPcIssueDate', 'Issue date of Production Certificate/IEM-II', 'GO-SWIFT')) {
                    CollapsePCDiv();
                    return false;
                }
                var pcIssueDate = new Date($("#txtPcIssueDate").val());
                var todayDate = new Date();
                if (pcIssueDate > todayDate) {
                    jAlert('<strong>Date of Issue of Production Certificate/IEM-II cannot be greater than current date</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtPcIssueDate").focus(); });
                    CollapsePCDiv();
                    return false;
                }
                var hdnProductionCertificate = $("#hdnProductionCertificate").val().trim();
                if (hdnProductionCertificate == "" || hdnProductionCertificate == undefined || hdnProductionCertificate == null) {
                    jAlert('<strong>Please upload Production Certificate/IEM-II</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#fuProductionCertificate").focus(); });
                    CollapsePCDiv();
                    return false;
                }
            }

            if (hdnOfflineStatus != null && hdnOfflineStatus != undefined && (hdnOfflineStatus == "2")) {
                var txtPcIssueDate = $("#txtPcIssueDate").val().trim();
                if (txtPcIssueDate != null && txtPcIssueDate != undefined && txtPcIssueDate != '') {
                    var pcIssueDate = new Date($("#txtPcIssueDate").val());
                    var todayDate = new Date();
                    if (pcIssueDate > todayDate) {
                        jAlert('<strong>Date of Issue of Production Certificate/EM-II cannot be greater than current date</strong>', 'GO-SWIFT');
                        $("#popup_ok").click(function () { $("#txtPcIssueDate").focus(); });
                        CollapsePCDiv();
                        return false;
                    }
                }
            }

            if (!DropDownValidation('drpApplicationType', '0', 'Application for', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#drpApplicationType").focus(); });
                CollapseFirst();
                return false;
            }

            var intApplicationType = $("#drpApplicationType").val();
            if ($("#divChangeIn").is(":visible")) {
                if (!ValidateCheckBoxList("chkLstChange")) {
                    jAlert('<strong>Please Select atleast one option in Change</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#chkLstChange").focus(); });
                    CollapseFirst();
                    return false;
                }
            }

            var ein = $("#txtEin").val().trim();
            if (ein != null && ein != undefined && ein != '') {
                /*if ($("#txtEin").val().length != 50) {*/  //********Change on-14-03-2023
                    jAlert('<strong>IEM/IL must be a 10-digit number</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtEin").focus(); });
                    CollapseFirst();
                    return false;
                //}

                if (!blankFieldValidation('txtDateOfIssuance', 'Date of IEM/IL Issue', 'GO-SWIFT')) {
                    CollapseFirst();
                    return false;
                }
                var einIssueDate = new Date($("#txtDateOfIssuance").val());
                var todayDate = new Date();
                if (ein != null && ein != undefined && ein != '' && einIssueDate > todayDate) {
                    jAlert('<strong>Date of IEM/IL Issue cannot be greater than current date</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtEin").focus(); });
                    CollapseFirst();
                    return false;
                }
                var hdnIEM = $("#hdnIEM").val();
                if (hdnIEM == null || hdnIEM == undefined || hdnIEM == '') {
                    jAlert('<strong>Please upload IEM Certificate</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#fupIEM").focus(); });
                    CollapseFirst();
                    return false;
                }
            }

            var UAN = $("#txtUan").val().trim();
            if (UAN != null && UAN != undefined && UAN != '') {
                if ($("#txtUan").val().trim().length != 12) {
                    jAlert('<strong>Udyog Aadhaar Number must be of 12 alpha-numeric digits</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtUan").focus(); });
                    CollapseFirst();
                    return false;
                }
                if (!CheckAlphaNumeric("txtUan", "Udyog Aadhaar number", 'GO-SWIFT')) {
                    return false;
                }
            }

            var ein = $("#txtEin").val();
            var UAN = $("#txtUan").val();
            if ((ein == null || ein == undefined || ein == '') && (UAN == null || UAN == undefined || UAN == '')) {
                jAlert('<strong>Please enter either the UAN or the IEM/IL number</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtUan").focus(); });
                CollapseFirst();
                return false;
            }

            if (!blankFieldValidation('txtEnterpriseName', 'Name of Industrial Unit', 'GO-SWIFT')) {
                CollapseFirst();
                return false;
            }


            if (!DropDownValidation('ddlSector', '0', 'Sector of activity', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#ddlSector").focus(); });
                CollapseFirst();
                return false;
            }
            if (!DropDownValidation('ddlSubSector', '0', 'Sub sector', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#ddlSubSector").focus(); });
                CollapseFirst();
                return false;
            }
            if (!DropDownValidation('drpUnitCategory', '0', 'Unit Category', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#drpUnitCategory").focus(); });
                CollapseFirst();
                return false;
            }
            if (!DropDownValidation('drpCompanyType', '0', 'Nature of Activity', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#drpCompanyType").focus(); });
                CollapseFirst();
                return false;
            }
            var gstin = $("#txtGST").val();
            if (gstin != null && gstin != undefined && gstin != '') {
                if ($("#txtGST").val().length != 15) {
                    jAlert('<strong>GSTIN must be of 15 alpha-numeric digits</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtGST").focus(); });
                    CollapseFirst(); return false;
                }

                if (!CheckAlphaNumeric("txtGST", "GSTIN", 'GO-SWIFT')) {
                    return false;
                }

                var hdnVAT = document.getElementById("hdnVAT").value;
                if (hdnVAT == "" || hdnVAT == undefined || hdnVAT == null) {
                    jAlert('<strong>Please Upload GSTIN</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#fupVAT").focus(); });
                    CollapseFirst();
                    return false;
                }
            }

            var hdnFactoryLic = document.getElementById("hdnFactoryLic").value;
            if (hdnFactoryLic == "" || hdnFactoryLic == null || hdnFactoryLic == '') {
                jAlert('<strong>Please provide Factory License No. document</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fupFactoryLic").focus(); });
                CollapseFirst();
                return false;
            }
            if (!DropDownValidation('drpOrganizationType', '0', 'Constitution of Organization', 'GO-SWIFT')) {
                CollapseFirst();
                $("#popup_ok").click(function () { $("#drpOrganizationType").focus(); });
                return false;
            }
            var organizationType = $("#drpOrganizationType").val();
            if (organizationType == "24" && !blankFieldValidation('txtOtherOrg', 'others for Constitution of Organization', 'GO-SWIFT')) {
                return false;
            }
            var rdBtnOrg = $("#rdBtnOrg input[type=radio]:checked").val();
            if (rdBtnOrg == null || rdBtnOrg == undefined || rdBtnOrg == "0") {
                jAlert('<strong>Please select document type in support of Constitution of Organization</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#rdBtnOrg").focus(); });
                CollapseFirst();
                return false;
            }

            var hdnOrgDocument = $("#hdnOrgDocument").val().trim();
            if (hdnOrgDocument == "" || hdnOrgDocument == undefined || hdnOrgDocument == null) {
                jAlert('<strong>Please upload ' + $("#lblOrgTypeDoc").text() + '</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fuOrgDocument").focus(); });
                CollapseFirst();
                return false;
            }
            if (!DropDownValidation('drpSalutation', '0', 'salutation', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#drpSalutation").focus(); });
                CollapseFirst();
                return false;
            }
            if (!blankFieldValidation('txtOwnerName', $("#lblOwnerLabel").text(), 'GO-SWIFT')) {
                CollapseFirst();
                return false;
            }

            var ownername = $("#txtOwnerName").val();
            var strownername = ownername.replace(/[^.]/g, "").length;
            if (strownername == ownername.length) {
                jAlert('<strong>Invalid name of' + $("#lblOwnerLabel").text() + '</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtOwnerName").focus(); });
                CollapseFirst();
                return false;
            }

            var rdBtnOwnerTYpe = $("#rdBtnOwnerTYpe input[type=radio]:checked").val();
            if (rdBtnOwnerTYpe == null || rdBtnOwnerTYpe == undefined || rdBtnOwnerTYpe == "0") {
                jAlert('<strong>Please select document type in support of Owner</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#rdBtnOwnerTYpe").focus(); });
                CollapseFirst();
                return false;
            }

            var hdnDocumentType = $("#hdnDocumentType").val().trim();
            if (hdnDocumentType == "" || hdnDocumentType == null || hdnDocumentType == undefined) {
                jAlert('<strong>Please upload ' + $("#lblDocumentType").text() + '</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fuDocumentType").focus(); });
                CollapseFirst();
                return false;
            }

            if (!DropDownValidation('drpOwnerType', '0', 'Ownership Pattern', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#drpOwnerType").focus(); });
                CollapseFirst();
                return false;
            }

            if ($("#divOwnerCategoryDoc").is(":visible")) {
                var rdBtnOwnerCategory = $("#rdBtnOwnerCategory input[type=radio]:checked").val();
                if (rdBtnOwnerCategory == null || rdBtnOwnerCategory == undefined || rdBtnOwnerCategory == "0") {
                    jAlert('<strong>Please provide Certificate in support of SC / ST / OBC </strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#rdBtnOwnerCategory").focus(); });
                    CollapseFirst();
                    return false;
                }

                var hdnOwnerCategory = document.getElementById("hdnOwnerCategory").value.trim();
                if (hdnOwnerCategory == "" || hdnOwnerCategory == null || hdnOwnerCategory == undefined) {
                    jAlert('<strong>Please provide Certificate in support of SC / ST / OBC </strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#fuOwnerCategory").focus(); });
                    CollapseFirst();
                    return false;
                }
            }
            if (!blankFieldValidation('txtOfficeAddress', 'Office Address in Registered Office / Communication Address', 'GO-SWIFT')) {
                CollapseAddress();
                return false;
            }
            if (!DropDownValidation('ddlCode', '0', 'Code for Telephone No. in Registered Office / Communication Address ', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#ddlCode").focus(); });
                CollapseAddress();
                return false;
            }
            if (!blankFieldValidation('txtOfficePhone', 'Telephone No. in Registered Office / Communication Address', 'GO-SWIFT')) {
                CollapseAddress();
                return false;
            }
            if (!ValidateNumber('txtOfficePhone', 'Telephone No. in Registered Office / Communication Address', 'GO-SWIFT')) {
                CollapseAddress();
                return false;
            }
            if (parseInt($("#txtOfficePhone").val()) == 0) {
                jAlert('<strong>Please enter valid Telephone No. in Registered Office / Communication Address</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtOfficePhone").focus(); });
                CollapseAddress();
                return false;
            }
            if (!ValidateFieldLength('txtOfficePhone', 10, 'Telephone No. in Registered Office / Communication Address', 'GO-SWIFT')) {
                CollapseAddress();
                return false;
            }
            var officeFax = $("#txtOfficeFax").val();
            if (officeFax != null && officeFax != undefined && officeFax != '' && officeFax != '0') {
                if (parseInt(officeFax) == 0) {
                    jAlert('<strong>Please enter valid office Fax in Registered Office / Communication Address</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtOfficeFax").focus(); });
                    CollapseAddress();
                    return false;
                }
                if (officeFax.length != 10) {
                    jAlert('<strong>Please enter valid office fax in Registered Office / Communication Address</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtOfficeFax").focus(); });
                    CollapseAddress();
                    return false;
                }
                if (!DropDownValidation('drpFx', '0', 'Code for Fax in Registered Office / Communication Address', 'GO-SWIFT')) {
                    $("#popup_ok").click(function () { $("#drpFx").focus(); });
                    CollapseAddress();
                    return false;
                }
            }
            if (!blankFieldValidation('txtOfficeEmail', 'Email', 'GO-SWIFT')) {
                CollapseAddress();
                return false;
            }
            if (!ValidateEmail($("#txtOfficeEmail").val())) {
                jAlert('<strong>Please enter valid office email in Location of the unit</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtOfficeEmail").focus(); });
                CollapseAddress();
                return false;
            }
            if (!DropDownValidation('ddlDistrict', '0', 'District', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#ddlDistrict").focus(); });
                CollapseAddress();
                return false;
            }
            if (!DropDownValidation('ddlBlock', '0', 'Block', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#ddlBlock").focus(); });
                CollapseAddress();
                return false;
            }

            if (!blankFieldValidation('txtEnterpriseAddress', 'Address of Enterprise in Location of the unit', 'GO-SWIFT')) {
                CollapseAddress();
                return false;
            }
            if (!DropDownValidation('drpEntCode', '0', 'Code for Telephone No. in Location of the unit', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#drpEntCode").focus(); });
                CollapseAddress();
                return false;
            }
            if (!blankFieldValidation('txtPhoneNo', 'Telephone No. in Location of the unit', 'GO-SWIFT')) {
                CollapseAddress();
                return false;
            }
            if (parseInt($("#txtPhoneNo").val()) == 0) {
                jAlert('<strong>Please enter valid Telephone No. in Location of the unit</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtPhoneNo").focus(); });
                CollapseAddress();
                return false;
            }
            if (!ValidateFieldLength('txtPhoneNo', 10, 'Telephone No. in Location of the unit', 'GO-SWIFT')) {
                CollapseAddress();
                return false;
            }
            var EnterPriseFax = $("#txtFax").val();
            if (EnterPriseFax != null && EnterPriseFax != undefined && EnterPriseFax != '' && EnterPriseFax != '0') {
                if (parseInt($("#txtFax").val()) == 0) {
                    jAlert('<strong>Please enter valid fax in Location of the unit</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtFax").focus(); });
                    CollapseAddress();
                    return false;
                }
                if (EnterPriseFax.length != 10) {
                    jAlert('<strong>Please enter valid enterprise fax in Location of the unit</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtFax").focus(); });
                    CollapseAddress();
                    return false;
                }
                if (!DropDownValidation('drpEnterpriseFax', '0', 'Code for Fax in Location of the unit', 'GO-SWIFT')) {
                    $("#popup_ok").click(function () { $("#drpEnterpriseFax").focus(); });
                    CollapseAddress();
                    return false;
                }

            }
            if (!blankFieldValidation('txtEmail', 'Email', 'GO-SWIFT')) {
                CollapseAddress();
                return false;
            }
            if (!ValidateEmail($("#txtEmail").val())) {
                jAlert('<strong>Please enter valid enterprise email in Location of the unit</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtEmail").focus(); });
                CollapseAddress();
                return false;
            }

            if (!blankFieldValidation('txtDateFFI', 'Date of first fixed capital investment', 'GO-SWIFT')) {
                CollapseIndustry();
                return false;
            }
            var dtmFfi = new Date($("#txtDateFFI").val());
            var currDate = new Date();
            if (dtmFfi > currDate) {
                jAlert('<strong>Date of first fixed investment cannot be greater than current date</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtDateFFI").focus(); });
                CollapseIndustry();
                return false;
            }
            if (!DropDownValidation('drpChangeIn', '0', 'Mode of Investment', 'GO-SWIFT')) {
                $("#popup_ok").click(function () { $("#drpChangeIn").focus(); });
                CollapseIndustry();
                return false;
            }
            if (!blankFieldValidation('txtland', 'investment in Land including land development', 'GO-SWIFT')) {
                CollapseIndustry();
                return false;
            }

            var landInv = parseFloat($("#txtland").val());
            if (landInv != 0.00) {
                var rdBtnLand = $("#rdBtnLand input[type=radio]:checked").val();
                if (rdBtnLand == null || rdBtnLand == undefined || rdBtnLand == "0") {
                    jAlert('<strong>Please select document type provided in support of land</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#rdBtnLand").focus(); });
                    CollapseIndustry();
                    return false;
                }
                var hdnLand = document.getElementById("hdnLand").value.trim();
                if (hdnLand == null || hdnLand == undefined || hdnLand == "") {
                    jAlert('<strong>Please upload support document for Land including land development</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#fuLand").focus(); });
                    CollapseIndustry();
                    return false;
                }
            }
            if (!blankFieldValidation('txtBuilding', 'investment in Building & Civil Construction', 'GO-SWIFT')) {
                CollapseIndustry();
                return false;
            }
            var BuildInv = parseFloat($("#txtBuilding").val());
            if (BuildInv != 0.00) {
                var hdnBuildingValuation = document.getElementById("hdnBuildingValuation").value;
                if (hdnBuildingValuation == "" || hdnBuildingValuation == undefined || hdnBuildingValuation == null) {
                    jAlert('<strong>Please upload building valuation report</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#fupBuildingValReport").focus(); });
                    CollapseIndustry();
                    return false;
                }
            }
            if (!blankFieldValidation('txtPlantMachinery', 'investment in Plant & Machinery', 'GO-SWIFT')) {
                CollapseIndustry();
                return false;
            }

            var plantAndMachinery = $("#txtPlantMachinery").val();
            if (parseFloat(plantAndMachinery) == 0.00) {
                jAlert('<strong>Investment in Plant and machinery cannot be 0</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtPlantMachinery").focus(); });
                CollapseIndustry();
                return false;
            }
            var hdnPlant = document.getElementById("hdnPlant").value.trim();
            if (hdnPlant == "" || hdnPlant == undefined || hdnPlant == '') {
                jAlert('<strong>Please upload support document for Plant & Machinery </strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fuPlant").focus(); });
                CollapseIndustry();
                return false;
            }
            var invtotal = $("#lblTotal").text().trim();
            if (invtotal == null || invtotal == undefined || invtotal == '') {
                jAlert('<strong>Please enter capital investment details</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtland").focus(); });
                CollapseIndustry();
                return false;
            }
            if (parseFloat(invtotal) == 0.00) {
                jAlert('<strong>Total capital investment cannot be 0.</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtland").focus(); });
                CollapseIndustry();
                return false;
            }
            if (!blankFieldValidation('txtWorkingCapital', 'Working Capital', 'GO-SWIFT')) {
                CollapseIndustry();
                return false;
            }
            var wCapital = $("#txtWorkingCapital").val();
            if (parseFloat(wCapital) == 0.00) {
                jAlert('<strong>Working Capital cannot be zero.</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtWorkingCapital").focus(); });
                CollapseIndustry();
                return false;
            }
            var hdnCompAuthority = document.getElementById("hdnCompAuthority").value;
            if (hdnCompAuthority == "" || hdnCompAuthority == undefined || hdnCompAuthority == null) {
                jAlert('<strong>Please upload Investment as on the date of commercial production from the competent authority</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fupComAuthority").focus(); });
                CollapseIndustry();
                return false;
            }

            if (!blankFieldValidation('txtEquity', 'Equity', 'GO-SWIFT')) {
                CollapseIndustry();
                return false;
            }
            var equity = $("#txtEquity").val();
            if (parseFloat(equity) == 0.00) {
                jAlert('<strong>Equity cannot be 0</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtEquity").focus(); });
                CollapseIndustry();
                return false;
            }

            if (!CompareWCapitalInvest()) {
                jAlert('<strong>Total investment should be equal to sum of Loan and Equity.</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtland").focus(); });
                CollapseIndustry();
                return false;
            }

            var loan = $("#txtLoan").val();
            if (loan != null && loan != undefined && loan != '' && parseFloat(loan) != 0.00) {
                var hdnBank = document.getElementById("hdnBank").value;
                if (hdnBank == "" || hdnBank == undefined || hdnBank == null) {
                    jAlert('<strong>Please provide Sanction Letter of Bank / Financial Institution</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#fuBank").focus(); });
                    CollapseIndustry();
                    return false;
                }
            }
            var fdi = $("#txtFdiComponent").val();
            if (fdi != null && fdi != undefined && fdi != "" && parseFloat(fdi) != 0.00) {
                if (parseFloat(fdi) > parseFloat($("#txtEquity").val())) {
                    jAlert('<strong>Invalid FDI. FDI cannot be greater than Equity</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtFdiComponent").focus(); });
                    CollapseIndustry();
                    return false;
                }
            }

            if (!ValidateMachinery()) {
                jAlert('<strong>Please enter atleast one or more plant & machinery</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtMachinery").focus(); });
                CollapseIndustry();
                return false
            }

            if (!blankFieldValidation('txtDirectEmployement', 'Direct Employment (in Numbers) As on Company Payroll', 'GO-SWIFT')) {
                CollapseEmployee();
                return false;
            }

            if (parseInt($("#txtDirectEmployement").val()) == 0) {
                jAlert('<strong>Please provide valid value for Direct Employment (in Numbers) As on Company Payroll</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtDirectEmployement").focus(); });
                $("#txtDirectEmployement").focus();
                CollapseEmployee();
                return false;
            }
            if (!blankFieldValidation('txtContractualEmp', 'Contractual Employment', 'GO-SWIFT')) {
                CollapseEmployee();
                return false;
            }
            if (parseInt($("#txtContractualEmp").val()) == 0) {
                jAlert('<strong>Please provide valid value for Contractual Employment</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtContractualEmp").focus(); });
                $("#txtContractualEmp").focus();
                CollapseEmployee();
                return false;
            }
            var rbtnTechnical = $("#rbtnTechnical input[type=radio]:checked").val();
            if (rbtnTechnical == "1") {
                var hdnEmployement = document.getElementById("hdnEmployement").value.trim();
                if (hdnEmployement == "" || hdnEmployement == undefined || hdnEmployement == null) {
                    jAlert('<strong>Please provide Certificate in support of Technical Qualificaiton</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#fuEmployement").focus(); });
                    CollapseEmployee();
                    return false;
                }
            }

            var totalRole = $("#txtGrandTotal").val().trim();
            if (totalRole == null || totalRole == undefined || totalRole == '') {
                jAlert('<strong>Total No. of employees by Skill cannot be 0</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtManagarial").focus(); });
                CollapseEmployee();
                return false;
            }

            if (parseInt(totalRole) == 0) {
                jAlert('<strong>Total No. of employees by Skill cannot be 0</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtManagarial").focus(); });
                CollapseEmployee();
                return false;
            }

            var totalCat = $("#txtTotal").val();
            if (totalCat == null || totalCat == undefined || totalCat == '') {
                jAlert('<strong>Total No. of employees by Category cannot be 0</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtGeneral").focus(); });
                CollapseEmployee();
                return false;
            }
            if (parseInt(totalCat) == 0) {
                jAlert('<strong>Total No. of employees by Category cannot be 0</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtGeneral").focus(); });
                CollapseEmployee();
                return false;
            }

            var totalDirectEmp = parseInt($("#txtDirectEmployement").val());
            var totalContractEmp = parseInt($("#txtContractualEmp").val());

            if ((totalRole != null && totalRole != undefined && totalRole != '' && parseInt(totalRole) != 0) && (totalCat != null && totalCat != undefined && totalCat != '' && parseInt(totalCat) != 0)) {
                if ((totalContractEmp + totalDirectEmp) != parseInt(totalRole)) {
                    jAlert('<strong>Sum of Direct and Contractual employees should be equal to total employee by category</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtDirectEmployement").focus(); });
                    CollapseEmployee();
                    return false;
                }
                if (!CompareEmployement()) {
                    jAlert('<strong>Total employee by skills should be equal to total employee by category</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtManagarial").focus(); });
                    CollapseEmployee();
                    return false;
                }
            }
            debugger;
            var womenEmp = $("#txtWomen").val();
            if (womenEmp != null && womenEmp != undefined && womenEmp != '' && parseInt(womenEmp) != 0) {
                if (parseInt(womenEmp) > parseInt(totalRole)) {
                    jAlert('<strong>Total no of women employee should be less than total no of employee</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtWomen").focus(); });
                    CollapseEmployee();
                    return false;
                }
            }

            var txtPhd = $("#txtPhd").val();
            if (txtPhd != null && txtPhd != undefined && txtPhd != '' && parseInt(txtPhd) != 0) {
                if (parseInt(txtPhd) > parseInt(totalRole)) {
                    jAlert('<strong>Total no of differently abled employees should be less than total no of employee</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtPhd").focus(); });
                    CollapseEmployee();
                    return false;
                }
            }
            var productCode = $("#txtProductCode").val().trim();
            if (productCode != "" && productCode != undefined && productCode != null) {
                if (productCode.length != 5) {
                    jAlert('<strong>Code(Code may be entered as per NIC 2008) should be of 5 digit</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtProductCode").focus(); });
                    CollapseEmployee();
                    return false;
                }
            }
            if (!blankFieldValidation('txtProdComm', 'Date of Commencement of Production', 'GO-SWIFT')) {
                CollapseEmployee();
                return false;
            }
            var dtmProdComm = new Date($("#txtProdComm").val());
            var currDate = new Date();
            if (dtmProdComm > currDate) {
                jAlert('<strong>Date of product commencement cannot be greater than current date</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtProdComm").focus(); });
                CollapseEmployee();
                return false;
            }
            if (dtmProdComm < dtmFfi) {
                jAlert('<strong>Date of FFCI cannot be greater that date of production commencement</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtProdComm").focus(); });
                CollapseIndustry();
                return false;
            }
            var hdnRawMaterialPre = document.getElementById("hdnRawMaterialPre").value;
            if (hdnRawMaterialPre == undefined || hdnRawMaterialPre == null || hdnRawMaterialPre == "") {
                jAlert('<strong>Please provide Statement of Raw Material Purchased for Processing Before Production for 3 months</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fupRawMaterialStatementPre").focus(); });
                CollapseEmployee();
                return false;
            }

            var hdnRawMaterialPost = document.getElementById("hdnRawMaterialPost").value;
            if (hdnRawMaterialPost == null || hdnRawMaterialPost == undefined || hdnRawMaterialPost == "") {
                jAlert('<strong> Please provide Statement of Raw Material Purchased for Processing After Production for 3 months</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fupRawMaterialStatementPost").focus(); });
                CollapseEmployee();
                return false;
            }

            var hdnProductionPost = document.getElementById("hdnProductionPost").value;
            if (hdnProductionPost == null || hdnProductionPost == undefined || hdnProductionPost == "") {
                jAlert('<strong>Please provide Statement Showing Production for last 3 months</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fupProductStatementPost").focus(); });
                CollapseEmployee();
                return false;
            }

            var hdnFistSaleBill = document.getElementById("hdnFistSaleBill").value;
            if (hdnFistSaleBill == "" || hdnFistSaleBill == undefined || hdnFistSaleBill == null) {
                jAlert('<strong>Please provide Statement Showing Sale of last 3 months</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fuFirstSaleBill").focus(); });
                CollapseEmployee();
                return false;
            }

            var hdnSaleInvoice = document.getElementById("hdnSaleInvoice").value;
            if (hdnSaleInvoice == null || hdnSaleInvoice == undefined || hdnSaleInvoice == "") {
                jAlert('<strong>Please provide Sale Invoice </strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fupSaleInvoice").focus(); });
                CollapseEmployee();
                return false;
            }

            if (!ValidateProducts()) {
                jAlert('<strong>Please enter atleast one or more products</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtItemProduct").focus(); });
                CollapseEmployee();
                return false
            }

            var hdnProductfilename = document.getElementById("hdnProductfilename").value;
            if (hdnProductfilename == null || hdnProductfilename == undefined || hdnProductfilename == "") {
                jAlert('<strong>Please provide 1st Raw material purchase Bill</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fuProduct").focus(); });
                CollapseEmployee();
                return false;
            }

            var power = $("#rdBtnLstPower input[type=radio]:checked ").val();
            if (power == 1) {
                if (!blankFieldValidation('txtContractDemand', ' Contract Demand (KW)', 'GO-SWIFT')) {
                    CollapseEmployee();
                    return false;
                }
                if (parseFloat($("#txtContractDemand").val()) == 0.00) {
                    jAlert('<strong>Value for contract Demand (KW) cannot be zero</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtContractDemand").focus(); });
                    CollapseEmployee();
                    return false;
                }
                if (!blankFieldValidation('txtPowerConnection', 'Date of connection', 'GO-SWIFT')) {
                    CollapseEmployee();
                    return false;
                }
                var txtPowerConnection = new Date($("#txtPowerConnection").val());
                var currDate = new Date();
                if (txtPowerConnection > currDate) {
                    jAlert('<strong>Date of power connection cannot be greater than current date</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtPowerConnection").focus(); });
                    CollapseEmployee();
                    return false;
                }
                var rdBtnPower = $("#rdBtnPower input[type=radio]:checked").val();
                if (rdBtnPower == null || rdBtnPower == undefined || rdBtnPower == "0") {
                    jAlert('<strong>Please select document type in support of power requirements</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#rdBtnPower").focus(); });
                    CollapseEmployee();
                    return false;
                }

                var hdnPower = document.getElementById("hdnPower").value;
                if (hdnPower == null || hdnPower == undefined || hdnPower == "") {
                    jAlert('<strong>Please provide Certificate of Power Connection</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#fuPower").focus(); });
                    CollapseEmployee();
                    return false;
                }

                var hdnAgreement = document.getElementById("hdnAgreement").value;
                if (document.getElementById("hdnAgreement").value == "") {
                    jAlert('<strong> Upload Agreement With CESU/NESCO</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#fupAgreement").focus(); });
                    CollapseEmployee();
                    return false;
                }
            }


            var hdnClearence = document.getElementById("hdnClearence").value;
            if (hdnClearence == null || hdnClearence == undefined || hdnClearence == "") {
                jAlert('<strong>Please provide document for OSPCB consent to operate (except white category)</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#fuClearence").focus(); });
                CollapseAdditional();
                return false;
            }

            jConfirm('Are You Sure You Want To Save?', 'GO-SWIFT', function (callback) {
                if (callback) {
                    __doPostBack('btnApply', '');
                } else {
                    return false;
                }
            });
            return false;
        }

        function CompareWCapitalInvest() {
            debugger;
            var totalCapital = 0.00, equity = 0.00, loan = 0.00;
            var lblTotal = $("#lblTotal").text();
            var txtEquityVal = $("#txtEquity").val();
            var txtLoanval = $("#txtLoan").val();
            if (txtEquityVal != null && txtEquityVal != undefined && txtEquityVal != '') {
                equity = parseFloat(txtEquityVal);
            }
            if (txtLoanval != null && txtLoanval != undefined && txtLoanval != '') {
                loan = parseFloat(txtLoanval);
            }
            if (lblTotal != null && lblTotal != undefined && lblTotal != '') {
                totalCapital = parseFloat(lblTotal);
            }
            var totalInvestment = 0.00;
            totalInvestment = equity + loan;

            if (totalInvestment != totalCapital) {
                return false;
            }
            else {
                return true;
            }

        }


        function CollapseFirst() {
            debugger;
            $("#collapseOne").addClass('in');
            $("#AddressDetails, #IndustryDetails, #EmployementInformation, #Additional").removeClass('in')
            var hdnOffline = $("#hdnOfflineStatus").val();
            if (hdnOffline != null && hdnOffline != undefined && (hdnOffline == "1" || hdnOffline == "2" || hdnOffline == "3")) {
                $("#divProductionCertificate").removeClass('in');
            }
        }

        function CollapsePCDiv() {
            $("#divProductionCertificate").addClass('in');
            $("#AddressDetails, #IndustryDetails, #EmployementInformation, #Additional, #collapseOne").removeClass('in');


        }

        function CollapseAddress() {
            $("#AddressDetails").addClass('in');
            $("#collapseOne, #IndustryDetails, #EmployementInformation, #Additional").removeClass('in');
            var hdnOffline = $("#hdnOfflineStatus").val();
            if (hdnOffline != null && hdnOffline != undefined && (hdnOffline == "1" || hdnOffline == "2" || hdnOffline == "3")) {
                $("#divProductionCertificate").removeClass('in');
            }
        }

        function CollapseIndustry() {
            $("#IndustryDetails").addClass('in');
            $("#collapseOne, #AddressDetails, #EmployementInformation, #Additional").removeClass('in');
            var hdnOffline = $("#hdnOfflineStatus").val();
            if (hdnOffline != null && hdnOffline != undefined && (hdnOffline == "1" || hdnOffline == "2" || hdnOffline == "3")) {
                $("#divProductionCertificate").removeClass('in');
            }
        }

        function CollapseEmployee() {
            $("#EmployementInformation").addClass('in');
            $("#collapseOne, #AddressDetails, #IndustryDetails, #Additional").removeClass('in');
            var hdnOffline = $("#hdnOfflineStatus").val();
            if (hdnOffline != null && hdnOffline != undefined && (hdnOffline == "1" || hdnOffline == "2" || hdnOffline == "3")) {
                $("#divProductionCertificate").removeClass('in');
            }
        }

        function CollapseAdditional() {
            $("#Additional").addClass('in');
            $("#collapseOne, #AddressDetails, #IndustryDetails, #EmployementInformation").removeClass('in');
            var hdnOffline = $("#hdnOfflineStatus").val();
            if (hdnOffline != null && hdnOffline != undefined && (hdnOffline == "1" || hdnOffline == "2" || hdnOffline == "3")) {
                $("#divProductionCertificate").removeClass('in');
            }
        }

        function pageLoad() {
            $('.datePicker').datepicker({
                format: 'dd-M-yyyy',
                changeMonth: true,
                changeYear: true,
                autoclose: true
            });

            CheckLengthKeyUp('txtOfficeAddress', 'lblOfficeAddress', 200);
            CheckLengthKeyUp('txtEnterpriseAddress', 'lblRemark', 200);

            $('.menuPc').addClass('active');
            $("#printbtn").click(function () {
                window.print();
            });

            debugger;
            var hdnOffline = $("#hdnOfflineStatus").val();

            //IF THE USER IS APPLYING AND NOT OFFLINE PC
            if (!(hdnOffline != null && hdnOffline != undefined && (hdnOffline == "1" || hdnOffline == "2" || hdnOffline == "3"))) {
                $("#collapseOne").attr("aria-expanded", "true");
                $("#collapseOne").addClass("in");
                $("#headingOne .panel-title a i").addClass("fa-minus");
                $("#headingOne .panel-title a i").removeClass("fa-plus");
            }


            var hdn = $("#hdnVisibleAcc").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                $("#collapseOne, #AddressDetails, #IndustryDetails, #Additional, #EmployementInformation").removeClass('in');
                var hdnOffline = $("#hdnOfflineStatus").val();
                if (hdnOffline != null && hdnOffline != undefined && (hdnOffline == "1" || hdnOffline == "2" || hdnOffline == "3")) {
                    $("#divProductionCertificate").removeClass('in');
                }
                $(hdn).addClass("in");

            }
            var hdn = $("#hdnPeal").val();
            if (hdn != null && hdn != undefined && hdn != '') {
                if (hdn != "0") {
                    $('.fieldinfo2').title = "Value Before IPR 2015 effective date";
                }
            }

            CalculateCatTotal();
            CalculateTotalStaff();
            CalculateTotalCapital();
            $("#txtManagarial, #txtSupervisor, #txtSkilled, #txtSemiSkilled, #txtUnSKilled").on('keyup', CalculateTotalStaff);
            $("#txtOthers, #txtPlantMachinery, #txtBuilding, #txtland").on('change', CalculateTotalCapital);
            $("#txtTotalSc, #txtTotalSt, #txtGeneral").on('keyup', CalculateCatTotal);

            //    if ($("#divOldFfci").is(":visible")) {
            CalculateTotalStaffOriginal();
            CalculateCatTotalOriginal();
            CalculateTotalCapitalOriginal();

            $("#lblManagerialOld, #lblSupOld, #lblSkilledOld, #lblSemiSkilledOld, #lblUnSkilledOld").on('keyup', CalculateTotalStaffOriginal);
            $("#lblOtherOld, #lblPlantOld, #lblBuildingOld, #lblLandOld, #txtLoan, #txtEquity").on('change', CalculateTotalCapitalOriginal);
            $("#lblTotalScOld, #lblTotalStOld, #lblGeneralOld").on('keyup', CalculateCatTotalOriginal);
            //    }

            $('.datePicker').datepicker({
                minDate: new Date(),
                autoclose: true,
                format: "dd-M-yyyy"
            });



            $(".panel-title > a").on("click", function () {
                //hdnVisibleAcc
                var href = $(this).attr("href");
                $("#hdnVisibleAcc").val(href);
            });

            $("#lnkMachinery").on('click', ValidateMachineryLnk);
            $("#lnkAdd").on('click', ValidateAdd);
        }

        function OpenPopUpId(lnkButton) {
            var parentTd = $(lnkButton).parent("td");
            var fileTd = $(parentTd).closest('td').prev('td');
            var fld = $(fileTd).find("input:file");
            return false;
        }
        function openpopup(flu) {
            var i = flu.id;
            $("#" + i).click();
            return false;
        }

        function FileCheck(e) {
            var ids = e.id;
            var fileExtension = ['pdf'];
            if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
                alert('Only pdf formats are allowed.');
                $("#" + ids).val(null);
                return false;
            }
            else {
                $("#hdnFileName").val($('input[type=file]').val());
            }
        }

        function ValidateProducts() {
            var arrTr = $("#grdProducts>tbody>tr");
            if ($(arrTr).length <= 1) {
                return false;
            }
            else {
                return true;
            }
        }

        function ValidateAdd() {
            var txtProduct = $("#txtItemProduct");
            var txtItemCode = $("#txtItemCode");
            var txtQuantity = $("#txtQuantity");
            var drpUnit = $("#drpUnitType");
            var txtCost = $("#txtCost");
            var product = $(txtProduct).val();
            var code = $(txtItemCode).val();
            var quantity = $(txtQuantity).val();
            var Unit = $(drpUnit).val();
            var Cost = $(txtCost).val();
            var codelen = $(txtItemCode).val().length;
            if (!blankFieldValidation('txtItemProduct', 'Product Name', 'alert')) {
                return false;
            }
            if (code != "") {
                if (codelen != 5) {
                    jAlert('<strong>Code should be of 5-digit</strong>', 'alert');
                    $("#popup_ok").click(function () { $(txtQuantity).focus(); });
                    return false;
                }
            }
            if (!blankFieldValidation('txtQuantity', 'quantity', 'alert')) {
                return false;
            }

            if (quantity == "0") {
                jAlert('<strong>Please select quantity for the product</strong>', 'alert');
                $("#popup_ok").click(function () { $(txtQuantity).focus(); });
                return false;
            }
            if (!DropDownValidation('drpUnitType', '0', 'Unit Type', 'alert')) {
                $("#popup_ok").click(function () { $("#drpUnitType").focus(); });
                return false;
            }
            if (Unit == "52") {
                if (!blankFieldValidation('txtUnitType', 'Unit Type for others', 'alert')) {
                    return false;
                }
            }
            if (!blankFieldValidation('txtCost', 'Value of product', 'alert')) {
                return false;
            }
            if (isNaN(Cost)) {
                jAlert('<strong>Please enter valid value for product</strong>', 'alert');
                $("#popup_ok").click(function () { $("#txtCost").focus(); });
                return false;
            }
            if (parseFloat(Cost).toFixed() == 0.00) {
                jAlert('<strong>Please enter valid value for product</strong>', 'alert');
                $("#popup_ok").click(function () { $("#txtCost").focus(); });
                return false;
            }
        }

        function ValidateMachinery() {
            var arrTr = $("#gvPlant>tbody>tr");
            if ($(arrTr).length <= 1) {
                return false;
            }
            else {
                return true;
            }
        }
        function ValidateMachineryLnk() {
            var txtMachinery = $("#txtMachinery");
            var txtDateofPurchase = $("#txtDateofPurchase");
            var txtAmt = $("#txtAmt");


            if (!blankFieldValidation('txtMachinery', 'Plant & Machinery Name', 'GO-SWIFT')) {
                return false;
            }

            if (!blankFieldValidation('txtDateofPurchase', 'Date of Purchase', 'GO-SWIFT')) {
                return false;
            }
            if (!blankFieldValidation('txtAmt', 'Investment Amount', 'GO-SWIFT')) {
                return false;
            }
            if (txtAmt.val() == "0") {
                jAlert('<strong>Investment amount Cannot be zero</strong>', 'GO-SWIFT');
                $(txtQuantity).focus();
                return false;
            }
            if (parseFloat(txtAmt.val()) == 0.00) {
                jAlert('<strong>Please enter valid value for product</strong>', 'GO-SWIFT');
                $(txtCost).focus();
                return false;
            }
        }




        function ValidateCheckBoxList(chkLstid) {
            var cnt = 0;
            $("#" + chkLstid).find("input[type=checkbox]").each(function () {
                if ($(this).prop("checked")) {
                    cnt = cnt + 1;
                }
            });
            if (cnt == 0) {
                return false;
            }
            else {
                return true;
            }
        }

        function CheckLengthKeyUp(cntr, labelId, chr) {
            maxLen = chr; // max number of characters allowed            
            var strValue = $('#' + cntr).val();

            //alert(strValue); alert(strValue.length);
            if (strValue.length > maxLen) {

                // Reached the Maximum length so trim the textarea
                $('#' + cntr).val(strValue.substring(0, maxLen));
                $("#" + labelId).val(0);
            }
            else {
                // Maximum length not reached so update the value of my_text counter
                $("#" + labelId).text(maxLen - strValue.length);
            }
        }

        function checkLength(cntr, labelId, chr) {
            maxLen = chr; // max number of characters allowed            
            var strValue = $('#' + cntr).val();

            //alert(strValue); alert(strValue.length);
            if (strValue.length > maxLen) {
                // Reached the Maximum length so trim the textarea
                $('#' + cntr).val(strValue.substring(0, maxLen));
                $("#" + labelId).val(0);

                // Alert message if maximum limit is reached.
                alert("You have reached your maximum limit of characters allowed");
            }
            else {
                // Maximum length not reached so update the value of my_text counter
                $("#" + labelId).text(maxLen - strValue.length);
            }
        }


        function CompareEmployement() {
            var sup = 0, managerial = 0, skilled = 0, unskilled = 0, semiskilled = 0;
            managerial = $("#txtManagarial").val();
            sup = $("#txtSupervisor").val();
            skilled = $("#txtSkilled").val();
            unskilled = $("#txtUnSKilled").val();
            semiskilled = $("#txtSemiSkilled").val();
            var totalSc = 0, totalSt = 0, general = 0;
            totalSt = $("#txtTotalSc").val();
            totalSc = $("#txtTotalSt").val();
            general = $("#txtGeneral").val();
            CalculateCatTotal();
            CalculateTotalStaff();

            var totalType = 0, totalCategory = 0;
            var txttype = $("#txtTotal").val();
            var txtGrandTotal = $("#txtGrandTotal").val();
            if (txttype != null && txttype != undefined && txttype != '') {
                totalType = parseInt(txttype);
            }
            if (txtGrandTotal != null && txtGrandTotal != undefined && txtGrandTotal != '') {
                totalCategory = parseInt(txtGrandTotal);
            }
            if (totalType != 0 && totalCategory != 0 && totalCategory != totalType) {
                jAlert('<strong>Total employee by skills should be equal to total employee by category</strong>', 'GO-SWIFT');
                CalculateCatTotal();
                CalculateTotalStaff();
                return false;
            }
            else {
                return true;
            }
        }

        function CalculateTotalStaff() {
            var sup = 0, managerial = 0, skilled = 0, unskilled = 0, semiskilled = 0;
            var intsup = 0, intmanagerial = 0, intskilled = 0, intunskilled = 0, intsemiskilled = 0;
            managerial = $("#txtManagarial").val();
            sup = $("#txtSupervisor").val();
            skilled = $("#txtSkilled").val();
            unskilled = $("#txtUnSKilled").val();
            semiskilled = $("#txtSemiSkilled").val();
            if (managerial != null && managerial != undefined && managerial != '') {
                intmanagerial = parseInt(managerial);
            }
            if (sup != null && sup != undefined && sup != '') {
                intsup = parseInt(sup);
            }
            if (skilled != null && skilled != undefined && skilled != '') {
                intskilled = parseInt(skilled);
            }
            if (unskilled != null && unskilled != undefined && unskilled != '') {
                intunskilled = parseInt(unskilled);
            }
            if (semiskilled != null && semiskilled != undefined && semiskilled != '') {
                intsemiskilled = parseInt(semiskilled);
            }
            var total = 0;
            total = intsup + intmanagerial + intsemiskilled + intskilled + intunskilled;
            $("#txtGrandTotal").val(total);
        }

        function CalculateCatTotal() {
            var totalSc = 0, totalSt = 0, general = 0;
            var inttotalSc = 0, inttotalSt = 0, intgeneral = 0;
            totalSt = $("#txtTotalSc").val();
            totalSc = $("#txtTotalSt").val();
            general = $("#txtGeneral").val();
            if (totalSt != null && totalSt != undefined && totalSt != '') {
                inttotalSt = parseInt(totalSt);
            }
            if (totalSc != null && totalSc != undefined && totalSc != '') {
                inttotalSc = parseInt(totalSc);
            }
            if (general != null && general != undefined && general != '') {
                intgeneral = parseInt(general);
            }
            var total = 0;
            total = inttotalSc + inttotalSt + intgeneral;
            $("#txtTotal").val(total);
        }



        function CalculateTotalCapital() {
            debugger;
            var land = 0.0, Plant = 0.0, Building = 0.0, Others = 0.0;
            var intland = 0, intPlant = 0, intBuilding = 0, intOthers = 0;
            land = $("#txtland").val();
            Building = $("#txtBuilding").val();
            Plant = $("#txtPlantMachinery").val();
            Others = $("#txtOthers").val();
            if (land != null && land != undefined && land != '') {
                intland = parseFloat(land);
            }
            else {
                $("#txtland").val(0.00);
            }
            if (Plant != null && Plant != undefined && Plant != '') {
                intPlant = parseFloat(Plant);
            }
            else {
                $("#txtPlantMachinery").val(0.00);
            }
            if (Building != null && Building != undefined && Building != '') {
                intBuilding = parseFloat(Building);
            }
            else {
                $("#txtBuilding").val(0.00);
            }
            if (Others != null && Others != undefined && Others != '') {
                intOthers = parseFloat(Others);
            }
            else {
                $("#txtOthers").val(0.00);
            }
            var total = 0.0;
            total = intPlant + intland + intBuilding + intOthers;
            $("#lblTotal").text(parseFloat(total).toFixed(2));
            return total;
        }

        function ValidateInvestmentTotal() {
            var txtequity = $("#txtEquity").val();
            var txtloan = $("#txtLoan").val();
            var equity = 0.00, loan = 0.00;
            var isValid = true;
            var total = 0.00;
            total = $("#lblTotal").text();
            equity = parseFloat(txtequity);
            loan = parseFloat(txtloan);
            var totalInvestment = 0.00;
            totalInvestment = equity + loan;
            if (totalInvestment != 0.00 && total != 0.00) {
                if (totalInvestment != total) {
                    isValid = false;
                }
                else {
                    isValid = true;
                }
            }
            else {
                isValid = true;
            }
            if (!isValid) {
                //$(this).val(0.00);
                total = CalculateTotalCapital();
                $("#lblTotal").text(parseFloat(total).toFixed(2));
                jAlert('<strong>Total capital investment should be equal to sum of equity and loan</strong>', 'GO-SWIFT');
                return false;
            }
            else {
                return true;
            }

        }
        function CalculateTotalStaffOriginal() {
            var supold = 0, managerialold = 0, skilledlold = 0, unskilledlold = 0, semiskilledlold = 0;
            var intsuplold = 0, intmanagerialold = 0, intskilledold = 0, intunskilledold = 0, intsemiskilledold = 0;
            managerialold = $("#lblManagerialOld").val();
            supold = $("#lblSupOld").val();
            skilledold = $("#lblSkilledOld").val();
            unskilledold = $("#lblUnSkilledOld").val();
            semiskilledold = $("#lblSemiSkilledOld").val();
            if (managerialold != null && managerialold != undefined && managerialold != '') {
                intmanagerialold = parseInt(managerialold);
            }
            if (intsuplold != null && supold != undefined && supold != '') {
                intsuplold = parseInt(supold);
            }
            if (skilledold != null && skilledold != undefined && skilledold != '') {
                intskilledold = parseInt(skilledold);
            }
            if (unskilledold != null && unskilledold != undefined && unskilledold != '') {
                intunskilledold = parseInt(unskilledold);
            }
            if (semiskilledold != null && semiskilledold != undefined && semiskilledold != '') {
                intsemiskilledold = parseInt(semiskilledold);
            }
            var totalold = 0;
            totalold = intsuplold + intmanagerialold + intsemiskilledold + intskilledold + intunskilledold;
            $("#lblGrandTotalOld").val(totalold);
        }


        function CalculateCatTotalOriginal() {
            var totalScold = 0, totalStold = 0, generalold = 0;
            var inttotalScold = 0, inttotalStold = 0, intgeneralold = 0;
            totalStold = $("#lblTotalScOld").val();
            totalScold = $("#lblTotalStOld").val();
            generalold = $("#lblGeneralOld").val();
            if (totalStold != null && totalStold != undefined && totalStold != '') {
                inttotalStold = parseInt(totalStold);
            }
            if (totalScold != null && totalScold != undefined && totalScold != '') {
                inttotalScold = parseInt(totalScold);
            }
            if (generalold != null && generalold != undefined && generalold != '') {
                intgeneralold = parseInt(generalold);
            }
            var totalold = 0;
            totalold = inttotalScold + inttotalStold + intgeneralold;
            $("#lblEmpTotalOld").val(totalold);
        }


        function CalculateTotalCapitalOriginal() {
            var landold = 0.0, Plantold = 0.0, Buildingold = 0.0, Othersold = 0.0;
            var intlandold = 0, intPlantold = 0, intBuildingold = 0, intOthersold = 0;
            landold = $("#lblLandOld").val();
            Buildingold = $("#lblBuildingOld").val();
            Plantold = $("#lblPlantOld").val();
            Othersold = $("#lblOtherOld").val();
            if (landold != null && landold != undefined && landold != '') {
                intlandold = parseFloat(landold);
            }
            if (Plantold != null && Plantold != undefined && Plantold != '') {
                intPlantold = parseFloat(Plantold);
            }
            if (Buildingold != null && Buildingold != undefined && Buildingold != '') {
                intBuildingold = parseFloat(Buildingold);
            }
            if (Othersold != null && Othersold != undefined && Othersold != '') {
                intOthersold = parseFloat(Othersold);
            }

            var totalold = 0.0;
            totalold = intPlantold + intlandold + intBuildingold + intOthersold;
            $("#lblTotalOld").text(parseFloat(totalold).toFixed(2));
            return totalold;
        }

        function SaveAsDraft() {

            if (!DropDownValidation('drpApplicationType', '0', 'Application for', 'GO-SWIFT')) {
                jAlert('<strong>Please Select application for</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#drpApplicationType").focus(); });
                CollapseFirst();
                return false;
            }
            if ($("#divChangeIn").is(":visible")) {
                if (!ValidateCheckBoxList("chkLstChange")) {
                    jAlert('<strong>Please Select atleast one option in Change</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#chkLstChange").focus(); });
                    CollapseFirst();
                    return false;
                }
            }

            var ein = $("#txtEin").val().trim();
            if (ein != null && ein != undefined && ein != '') {

                jAlert('<strong>EIN must be a 10-digit number</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtEin").focus(); });
                CollapseFirst();
                return false;
                //*****************Below Line Comment -14-03-2023**********************//
                //if ($("#txtEin").val().length != 50)
                //{
                //     jAlert('<strong>EIN must be a 10-digit number</strong>', 'GO-SWIFT');                 
                //    $("#popup_ok").click(function () { $("#txtEin").focus(); });
                //    CollapseFirst();
                //    return false;
                //********************************************** 
            }


            //        var strEin = 0;
            //        strEin = ein.replace(/[^-]/g, "").length;
            //        if (strEin != 1) {
            //            jAlert('<strong>Invalid Ein Number</strong>', 'GO-SWIFT');
            //            $("#popup_ok").click(function () { $("#txtEin").focus(); });
            //            CollapseFirst();
            //            return false;
            //        }

            if (!blankFieldValidation('txtDateOfIssuance', 'Date of EIN Issue', 'GO-SWIFT')) {
                CollapseFirst();
                return false;
            }
            var einIssueDate = new Date($("#txtDateOfIssuance").val());
            var todayDate = new Date();
            if (ein != null && ein != undefined && ein != '' && einIssueDate > todayDate) {
                jAlert('<strong>Date of EIN Issue cannot be greater than current date</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtEin").focus(); });
                CollapseFirst();
                return false;
            }
        }
        
            var UAN = $("#txtUan").val().trim();
            if (UAN != null && UAN != undefined && UAN != '') {
                if ($("#txtUan").val().trim().length != 12) {
                    jAlert('<strong>Udyog Aadhaar Number must be of 12 alpha-numeric digits</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtUan").focus(); });
                    CollapseFirst();
                    return false;
                }
                if (!CheckAlphaNumeric("txtUan", "Udyog Aadhaar number", 'GO-SWIFT')) {
                    return false;
                }
            }

            var gstin = $("#txtGST").val();
            if (gstin != null && gstin != undefined && gstin != '') {
                if ($("#txtGST").val().length != 15) {
                    jAlert('<strong>GSTIN must be of 15 alpha-numeric digits</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtGST").focus(); });
                    CollapseFirst();
                    return false;
                }
                if (!CheckAlphaNumeric("txtGST", "GSTIN", 'GO-SWIFT')) {
                    return false;
                }
            }

            var officePhone = $("#txtOfficePhone").val();
            if (officePhone != null && officePhone != undefined && officePhone != '') {
                if (parseInt($("#txtOfficePhone").val()) == 0) {
                    jAlert('<strong>Please enter valid Telephone No. in Registered Office / Communication Address</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtOfficePhone").focus(); });
                    CollapseAddress();
                    return false;
                }
                if (!ValidateFieldLength('txtOfficePhone', 10, 'Telephone No. in Registered Office / Communication Address', 'GO-SWIFT')) {
                    CollapseAddress();
                    return false;
                }
                if (!DropDownValidation('ddlCode', '0', 'Code for Telephone No. in Registered Office / Communication Address ', 'GO-SWIFT')) {
                    $("#popup_ok").click(function () { $("#ddlCode").focus(); });
                    CollapseAddress();
                    return false;
                }
            }
            var officeFax = $("#txtOfficeFax").val();
            if (officeFax != null && officeFax != undefined && officeFax != '' && officeFax != '0') {
                if (parseInt(officeFax) == 0) {
                    jAlert('<strong>Please enter valid office Fax in Registered Office / Communication Address</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtOfficeFax").focus(); });
                    CollapseAddress();
                    return false;
                }
                if (officeFax.length != 10) {
                    jAlert('<strong>Please enter valid office fax in Registered Office / Communication Address</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtOfficeFax").focus(); });
                    CollapseAddress();
                    return false;
                }
                if (!DropDownValidation('drpFx', '0', 'Code for Fax in Registered Office / Communication Address', 'GO-SWIFT')) {
                    $("#popup_ok").click(function () { $("#drpFx").focus(); });
                    CollapseAddress();
                    return false;
                }
            }
            var email = $("#txtOfficeEmail").val();
            if (email != null && email != undefined && email != '' && !ValidateEmail($("#txtOfficeEmail").val())) {
                jAlert('<strong>Please enter valid office email in Location of the unit</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtOfficeEmail").focus(); });
                CollapseAddress();
                return false;
            }

            var phoneno = $("#txtPhoneNo").val();
            if (phoneno != null && phoneno != undefined && phoneno != '') {
                if (parseInt($("#txtPhoneNo").val()) == 0) {
                    jAlert('<strong>Please enter valid Telephone No. in Location of the unit</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtPhoneNo").focus(); });
                    CollapseAddress();
                    return false;
                }
                if (!ValidateFieldLength('txtPhoneNo', 10, 'Telephone No. in Location of the unit', 'GO-SWIFT')) {
                    CollapseAddress();
                    return false;
                }
                if (!DropDownValidation('drpEntCode', '0', 'Code for Telephone No. in Location of the unit', 'GO-SWIFT')) {
                    $("#popup_ok").click(function () { $("#drpEntCode").focus(); });
                    CollapseAddress();
                    return false;
                }
            }
            var EnterPriseFax = $("#txtFax").val();
            if (EnterPriseFax != null && EnterPriseFax != undefined && EnterPriseFax != '' && EnterPriseFax != '0') {
                if (parseInt($("#txtFax").val()) == 0) {
                    jAlert('<strong>Please enter valid fax in Location of the unit</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtFax").focus(); });
                    CollapseAddress();
                    return false;
                }
                if (EnterPriseFax.length != 10) {
                    jAlert('<strong>Please enter valid enterprise fax in Location of the unit</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtFax").focus(); });
                    CollapseAddress();
                    return false;
                }
                if (!DropDownValidation('drpEnterpriseFax', '0', 'Code for Fax in Location of the unit', 'GO-SWIFT')) {
                    $("#popup_ok").click(function () { $("#drpEnterpriseFax").focus(); });
                    CollapseAddress();
                    return false;
                }

            }
            var entEmail = $("#txtEmail").val();
            if (entEmail != null && entEmail != undefined && entEmail != '' && !ValidateEmail($("#txtEmail").val())) {
                jAlert('<strong>Please enter valid enterprise email in Location of the unit</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtEmail").focus(); });
                CollapseAddress();
                return false;
            }

            var txtDateFFI = $("#txtDateFFI").val();
            if (txtDateFFI != null && txtDateFFI != undefined && txtDateFFI != '') {
                var dtmFfi = new Date($("#txtDateFFI").val());
                var currDate = new Date();
                if (dtmFfi > currDate) {
                    jAlert('<strong>Date of first fixed investment cannot be greater than current date</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtDateFFI").focus(); });
                    CollapseIndustry();
                    return false;
                }
            }

            var invtotal = $("#lblTotal").text().trim();
            if (invtotal == null || invtotal == undefined || invtotal == '') {
                jAlert('<strong>Please enter capital investment details</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtland").focus(); });
                CollapseIndustry();
                return false;
            }
            if (parseFloat(invtotal) == 0.00) {
                jAlert('<strong>Total capital investment cannot be 0.</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtland").focus(); });
                CollapseIndustry();
                return false;
            }

            if (!CompareWCapitalInvest()) {
                jAlert('<strong>Total investment should be equal to sum of Loan and Equity.</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtland").focus(); });
                CollapseIndustry();
                return false;
            }
            var totalRole = $("#txtGrandTotal").val().trim();
            var totalCat = $("#txtTotal").val();
            if ((totalRole != null && totalRole != undefined && totalRole != '' && parseInt(totalRole) != 0) && (totalCat != null && totalCat != undefined && totalCat != '' && parseInt(totalCat) != 0)) {
                if (!CompareEmployement()) {
                    jAlert('<strong>Total employee by skills should be equal to total employee by category</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtManagarial").focus(); });
                    CollapseEmployee();
                    return false;
                }
            }

            var txtProdComm = $("#txtProdComm").val();
            if (txtProdComm != null & txtProdComm != undefined && txtProdComm != '') {
                var dtmProdComm = new Date($("#txtProdComm").val());
                var currDate = new Date();
                if (dtmProdComm > currDate) {
                    jAlert('<strong>Date of product commencement cannot be greater than current date</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtProdComm").focus(); });
                    CollapseEmployee();
                    return false;
                }
                if (dtmProdComm < dtmFfi) {
                    jAlert('<strong>Date of FFCI cannot be greater that date of production commencement</strong>', 'GO-SWIFT');
                    $("#popup_ok").click(function () { $("#txtProdComm").focus(); });
                    CollapseIndustry();
                    return false;
                }
            }

            jConfirm('Are You Sure You Want To Save As Draft?', 'GO-SWIFT', function (callback) {
                if (callback) {
                    __doPostBack('btnSaveAsDraft', '');
                } else {
                    return false;
                }
            });

            return false;
        }
        



        

        function alertredirect(msg) {
            jAlert(msg, 'SWP', function (r) {
                if (r) {
                    location.href = '../PcViewPage.aspx';
                    return true;
                }
                else {
                    return false;
                }
            });
        }

        function alertredirectno(msg) {
            jAlert(msg, 'SWP', function (r) {
                if (r) {
                    location.href = 'PC_Large_Apply.aspx';
                    return true;
                }
                else {
                    return false;
                }
            });
        }

        function alertredirectOffline(msg) {
            jAlert(msg, 'SWP', function (r) {
                if (r) {
                    location.href = 'PC_Large_Apply.aspx?offline=3';
                    return true;
                }
                else {
                    return false;
                }
            });
        }

        function alertredirectFeedBack(msg, intRetValue, intFormType) {
            jAlert(msg, 'SWP', function (r) {
                if (r) {
                    location.href = 'IncentiveFeedback.aspx?InctUniqueNo=' + intRetValue + '&ServiceId=' + intFormType;
                    return true;
                }
                else {
                    return false;
                }
            });
        }
    </script>
    
    <style>
        .fieldinfo2
        {
            float: right;
            margin-right: 8px;
            font-size: 17px;
            margin-top: 1px;
            color: #3abffb;
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
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.6;
        }
        .modalPopup
        {
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

    <script type="text/javascript">
        $(document).ready(function () {
            $('.datePicker').datepicker({
                autoclose: true
               
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:header ID="header" runat="server" />
    <asp:ScriptManager ID="sm1" runat="server">
    </asp:ScriptManager>
    <div class="container wrapper">
    <div class="registration-div investors-bg">
        <div id="exTab1" class="container">
            <div class="investrs-tab">
                <uc4:investerMenu ID="objInvestorMenu" runat="server" />
            </div>
            <div class="tab-content clearfix">
                <asp:HiddenField ID="hdnVisibleAcc" runat="server" />
                <asp:HiddenField ID="hdnApplyFlag" runat="server" />
                <asp:HiddenField ID="hdnChangeIn" runat="server" />
                <asp:HiddenField ID="hdnPolicyEffectiveDate" runat="server" />
                <asp:HiddenField ID="hdnPrevProdCommDate" runat="server" />
                <asp:HiddenField ID="hdnOfflineStatus" runat="server" />
                <asp:HiddenField ID="hdnIsOsPCBDownloaded" runat="server" Value="0" />
                <asp:HiddenField ID="hdnBoilderDownloaded" runat="server" Value="0" />
                <div class="form-sec">
                    <div class="form-header">
                        <br />
                        <h4 style="text-align: center; font-weight: bold;">
                            Application for Production Certificate for Large Industrial Units</h4>
                    </div>
                    <div class="form-body">
                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="panel panel-default" id="divUploadPc" runat="server" visible="false">
                                <div class="panel-heading" role="tab" id="headingPc">
                                    <h4 class="panel-title">
                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#divProductionCertificate"
                                            aria-expanded="true" aria-controls="collapsePC"><i class="more-less fa  fa-minus">
                                            </i>
                                            <asp:Label ID="lblPcMessage" runat="server" CssClass="text-red pull-right" Text="(All the fields are mandatory)"
                                                Style="margin-right: 20px;"></asp:Label>Production Certificate/IEM-II Details
                                            <asp:Label ID="lblPcTypeDetails" runat="server" CssClass="text-red"></asp:Label></a>
                                    </h4>
                                </div>
                                <div id="divProductionCertificate" class="panel-collapse collapse in" role="tabpanel"
                                    aria-labelledby="headingPc">
                                    <div class="panel-body">
                                        <asp:Panel ID="pnlOfflinePcDetails" runat="server">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Production Certificate/IEM-II No.</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtOfflinePcNo" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteOfflinePc" TargetControlID="txtOfflinePcNo" runat="server"
                                                            ValidChars="-" FilterType="Custom,Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        Production Certificate/IEM-II Issue Date
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div8">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="txtPcIssueDate" class="form-control"
                                                                runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        Upload Production Certificate/IEM-II <a data-toggle="tooltip" class="fieldinfo2"
                                                            title="Scan of Production Certificate in PDF Format"><i class="fa fa-question-circle"
                                                                aria-hidden="true"></i></a>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group">
                                                            <asp:FileUpload ID="fuProductionCertificate" CssClass="form-control" runat="server" />
                                                            <asp:HiddenField ID="hdnProductionCertificate" runat="server" />
                                                            <asp:LinkButton ID="lnkPCUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkOrgDocumentPdf_Click" ToolTip="Scan of Production Certificate in PDF Format"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkPCDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypProductionCert" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf file only and max file size 4 MB)</small>
                                                        <asp:Label ID="lblProdCertificate" Style="font-size: 12px;" CssClass="text-blue"
                                                            Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingOne">
                                    <h4 class="panel-title">
                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne"
                                            aria-expanded="false" aria-controls="collapseOne"><i class="more-less fa  fa-minus">
                                            </i><span class="text-red pull-right " style="margin-right: 20px;">* All fields in this
                                                section are mandatory</span>Industrial Unit's Details </a>
                                    </h4>
                                </div>
                                <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                                    <div class="panel-body">
                                        <asp:Panel ID="pnlIndustry" runat="server">
                                            <div class="form-group" id="dvInd" runat="server">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Industry Code</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblIndustryCode" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div id="divOldPC" runat="server" visible="false">
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            Last PC No. recorded</label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblOldPcNo" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                            PC Issuance Date</label>
                                                        <div class="col-sm-6">
                                                            <span class="colon">:</span>
                                                            <asp:Label ID="lblPcIssueDate" runat="server"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <div class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Application For</label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:RadioButtonList ID="rdBtnApplicationFor" runat="server" AutoPostBack="true"
                                                        OnSelectedIndexChanged="rdBtnApplicationFor_SelectedIndexChanged" RepeatColumns="2"
                                                        RepeatDirection="Horizontal" RepeatLayout="Table">
                                                        <asp:ListItem Text="New" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="Existing" Value="2"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:DropDownList ID="drpApplicationType" CssClass="form-control" runat="server"
                                                        OnSelectedIndexChanged="drpApplicationType_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:Panel ID="divChangeIn" runat="server" Visible="false" class="form-group">
                                            <div class="row">
                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                    Change in
                                                </label>
                                                <div class="col-sm-6">
                                                    <span class="colon">:</span>
                                                    <asp:CheckBoxList ID="chkLstChange" runat="server" RepeatColumns="5" RepeatLayout="Table"
                                                        RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="chkLstChange_SelectedIndexChanged">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlApp" runat="server">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Application No.
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:Label ID="lblAppNo" runat="server"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        IEM/IL
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtEin" runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                                        <%--<cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" TargetControlID="txtEin"
                                                            runat="server" FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Date of IEM/IL Issue
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div7">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="txtDateOfIssuance" class="form-control"
                                                                runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        Upload IEM Certificate
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:HiddenField ID="hdnIEMDoc" runat="server" />
                                                            <asp:FileUpload ID="fupIEM" CssClass="form-control" runat="server" />
                                                            <asp:HiddenField ID="hdnIEM" runat="server" />
                                                            <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <asp:LinkButton ID="lnkIEM" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkOrgDocumentPdf_Click" ToolTip="IEM Certificate"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkIEMDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypIEMDelete" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                        <asp:Label ID="lblIEM" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Document uploaded successfully"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Udyog Aadhaar Number
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtUan" runat="server" MaxLength="12" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" TargetControlID="txtUan"
                                                            runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlname" runat="server">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Name of Industrial Unit
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span><asp:TextBox ID="txtEnterpriseName" CssClass="form-control"
                                                            runat="server" Onkeypress="return inputLimiter(event,'NameCharacters')" MaxLength="100"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteEnterPriseName" runat="server" FilterMode="InvalidChars"
                                                            InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;.,{}+=[];'.,/|\_-~`" TargetControlID="txtEnterpriseName">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlSector" runat="server">
                                            <asp:UpdatePanel ID="up1" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Sector of Activity
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="ddlSector" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlSector_SelectedIndexChanged">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Sub Sector
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span><asp:DropDownList ID="ddlSubSector" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlIndOthers" runat="server">
                                            <asp:UpdatePanel ID="upUnit" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Unit Category
                                                            </label>
                                                            <div class="col-sm-6 margin-bottom10">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="drpUnitCategory" CssClass="form-control" runat="server">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Nature of Activity
                                                    </label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="drpCompanyType" CssClass="form-control" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        GSTIN No.
                                                    </label>
                                                    <div class="col-sm-6 margin-bottom10">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtGST" runat="server" MaxLength="15" CssClass="form-control"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" TargetControlID="txtGST"
                                                            runat="server" FilterType="LowercaseLetters,UppercaseLetters,Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <asp:Label ID="Label21" runat="server" class="col-sm-4 col-sm-offset-1">Upload GSTIN</asp:Label>
                                                    <div class="col-sm-6">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="fupVAT" CssClass="form-control" runat="server" />
                                                            <asp:HiddenField ID="hdnGSTDocId" runat="server" />
                                                            <asp:HiddenField ID="hdnVAT" runat="server" />
                                                            <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <asp:LinkButton ID="lnkVATPDF" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkOrgDocumentPdf_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkVATDel" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypVAT" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                        <asp:Label ID="lblVAT" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Document uploaded successfully"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <asp:Label ID="Label2" runat="server" class="col-sm-4 col-sm-offset-1">Upload Factory License No.</asp:Label>
                                                    <div class="col-sm-6">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="fupFactoryLic" CssClass="form-control" runat="server" />
                                                            <asp:HiddenField ID="hdnFactoryDoc" runat="server" />
                                                            <asp:HiddenField ID="hdnFactoryLic" runat="server" />
                                                            <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                            <asp:LinkButton ID="lnkFactoryLic" runat="server" CssClass="input-group-addon bg-green"
                                                                OnClick="lnkOrgDocumentPdf_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkFactoryLicDel" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypFactoryLic" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                        <asp:Label ID="lblFactoryLic" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Document uploaded successfully"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlOrgType" runat="server">
                                            <asp:UpdatePanel ID="upOrgType" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Constitution of Organization
                                                            </label>
                                                            <div class="col-sm-6 margin-bottom10">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="drpOrganizationType" CssClass="form-control" runat="server"
                                                                    OnSelectedIndexChanged="drpOrganizationType_SelectedIndexChanged" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                                <br />
                                                                <asp:TextBox ID="txtOtherOrg" CssClass="form-control" runat="server" MaxLength="200"
                                                                    Visible="false"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="fteOtherOrg" runat="server" FilterMode="InvalidChars"
                                                                    InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;.,{}+=[];'.,/|\_-~`" TargetControlID="txtOtherOrg">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <asp:Label ID="lblOrgTypeDoc" runat="server" CssClass="col-sm-4 col-sm-offset-1"></asp:Label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:RadioButtonList ID="rdBtnOrg" runat="server" RepeatLayout="Flow" RepeatDirection="Vertical">
                                                                </asp:RadioButtonList>
                                                                <div class="input-group">
                                                                    <asp:FileUpload ID="fuOrgDocument" CssClass="form-control" runat="server" />
                                                                    <asp:HiddenField ID="hdnOrgDocument" runat="server" />
                                                                    <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                    <asp:LinkButton ID="lnkOrgDocumentPdf" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip=" Registration of Firms / Certificate of Incorporation under Company Act / Co- operative
                                                            Society Registration / Registration of SHG"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkOrgDocumentDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypOrdDocument" runat="server" Target="_blank" Visible="false"
                                                                        CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblOrgDocument" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <asp:Label ID="lblOwnerLabel" runat="server" class="col-sm-4 col-sm-offset-1"></asp:Label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="drpSalutation" runat="server" CssClass="form-control phcode">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtOwnerName" MaxLength="100" CssClass="form-control phnum" runat="server"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="fteOwnername" runat="server" FilterMode="InvalidChars"
                                                                    InvalidChars=":&quot;1234567890~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtOwnerName">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <asp:Label ID="lblDocumentType" runat="server" class="col-sm-4 col-sm-offset-1"></asp:Label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:RadioButtonList ID="rdBtnOwnerTYpe" runat="server" RepeatLayout="Table" RepeatDirection="Vertical">
                                                                </asp:RadioButtonList>
                                                                <div class="input-group">
                                                                    <asp:FileUpload ID="fuDocumentType" CssClass="form-control" runat="server" />
                                                                    <asp:HiddenField ID="hdnDocumentType" runat="server" />
                                                                    <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                    <asp:LinkButton ID="lnkDocTypeUpload" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkDocTypeDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypDocType" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblDocType" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkOrgDocumentPdf" />
                                                    <asp:PostBackTrigger ControlID="lnkDocTypeUpload" />
                                                    <asp:PostBackTrigger ControlID="lnkVATPDF" />
                                                    <asp:PostBackTrigger ControlID="lnkFactoryLic" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <asp:UpdatePanel ID="upOwnerCategory" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Ownership Pattern
                                                            </label>
                                                            <div class="col-sm-6 margin-bottom10">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="drpOwnerType" CssClass="form-control" runat="server" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="drpOwnerType_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group" id="divOwnerCategoryDoc" runat="server" visible="false">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Please provide Certificate in support of SC / ST / OBC etc</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:RadioButtonList ID="rdBtnOwnerCategory" runat="server" RepeatLayout="Table"
                                                                    RepeatDirection="Vertical" Enabled="false">
                                                                </asp:RadioButtonList>
                                                                <div class="input-group">
                                                                    <asp:FileUpload ID="fuOwnerCategory" CssClass="form-control" runat="server" />
                                                                    <asp:HiddenField ID="hdnOwnerCategory" runat="server" />
                                                                    <asp:LinkButton ID="lnkOnwerCatAdd" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip="Certificate in support of SC / ST / OBC / Technical etc"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkOwnerCatDel" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypOwnerCategory" runat="server" Target="_blank" Visible="false"
                                                                        CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblOwnerCategoryFile" Style="font-size: 12px;" CssClass="text-blue"
                                                                    Visible="false" runat="server" Text="Document uploaded successfully"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkOnwerCatAdd" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingTwo">
                                    <h4 class="panel-title">
                                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                            href="#AddressDetails" aria-expanded="false" aria-controls="collapseTwo"><i class="more-less fa  fa-plus">
                                            </i>Address Details</a>
                                    </h4>
                                </div>
                                <div id="AddressDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <h4 class="h4-header">
                                            Registered Office / Communication Address &nbsp; &nbsp;&nbsp;&nbsp;
                                        </h4>
                                        <asp:Panel ID="pnlRedOff" runat="server">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Address<span class="text-red">*</span></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtOfficeAddress" runat="server" TextMode="MultiLine" Rows="10"
                                                            Columns="10" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                                        &nbsp;(Maximum&nbsp;
                                                        <asp:Label ID="lblOfficeAddress" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                                        &nbsp; characters allowed)
                                                        <cc1:FilteredTextBoxExtender ID="fteOfficeAddress" runat="server" FilterMode="InvalidChars"
                                                            InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;{}+=[];'|\~`" TargetControlID="txtOfficeAddress">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Telephone No.<span class="text-red">*</span></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="ddlCode" TabIndex="7" runat="server" CssClass="form-control phcode">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtOfficePhone" MaxLength="10" CssClass="form-control phnum" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteOfficePhone" TargetControlID="txtOfficePhone"
                                                            runat="server" FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        FAX</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="drpFx" TabIndex="9" runat="server" CssClass="form-control phcode">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtOfficeFax" TabIndex="10" CssClass="form-control phnum" MaxLength="10"
                                                            runat="server"></asp:TextBox>
                                                        <span class="mandatoryspan"><i>(Specify with STD Code, Example: 0674256123)</i></span>
                                                        <cc1:FilteredTextBoxExtender ID="fteOfficeFax" TargetControlID="txtOfficeFax" runat="server"
                                                            FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        E-mail<span class="text-red">*</span></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span></span><asp:TextBox ID="txtOfficeEmail" MaxLength="100"
                                                            CssClass="form-control" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteOfficeEmail" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                            ValidChars="@.-_" TargetControlID="txtOfficeEmail">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Website</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span><asp:TextBox ID="txtOfficeWebsite" MaxLength="100" CssClass="form-control"
                                                            runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteOfficeWebsite" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                            ValidChars="." TargetControlID="txtOfficeWebsite">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <h4 class="h4-header">
                                            Location of the unit
                                        </h4>
                                        <asp:Panel ID="pnlLocation" runat="server">
                                            <asp:UpdatePanel ID="up2" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                District<span class="text-red">*</span></label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span><asp:DropDownList ID="ddlDistrict" runat="server" CssClass="form-control"
                                                                    OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Text="--Select District--" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Block<span class="text-red">*</span></label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:DropDownList ID="ddlBlock" runat="server" CssClass="form-control">
                                                                    <asp:ListItem Text="-Select Block-" Value="0"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Address of Enterprise<span class="text-red">*</span></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtEnterpriseAddress" runat="server" TextMode="MultiLine" Rows="10"
                                                            Columns="10" MaxLength="200" CssClass="form-control"></asp:TextBox>
                                                        &nbsp;(Maximum&nbsp;
                                                        <asp:Label ID="lblRemark" runat="server" Text="200" ForeColor="Red"></asp:Label>
                                                        &nbsp; characters allowed)
                                                        <cc1:FilteredTextBoxExtender ID="fteEnterpriseAddress" runat="server" FilterMode="InvalidChars"
                                                            InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;{}+=[];'|\~`" TargetControlID="txtEnterpriseAddress">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Telephone No.<span class="text-red">*</span></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="drpEntCode" TabIndex="7" runat="server" CssClass="form-control phcode">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtPhoneNo" MaxLength="10" CssClass="form-control phnum" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="ftePhoneNo" TargetControlID="txtPhoneNo" runat="server"
                                                            FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        FAX</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="drpEnterpriseFax" TabIndex="9" runat="server" CssClass="form-control phcode">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtFax" MaxLength="10" CssClass="form-control phnum" runat="server"></asp:TextBox><span
                                                            class="mandatoryspan"><i>(Specify with STD Code, Example: 0674256123)</i></span>
                                                        <cc1:FilteredTextBoxExtender ID="fteFax" TargetControlID="txtFax" runat="server"
                                                            FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        E-mail<span class="text-red">*</span></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span><asp:TextBox ID="txtEmail" MaxLength="100" CssClass="form-control"
                                                            runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteEmail" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                            ValidChars="@.-_" TargetControlID="txtEmail">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Website</label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span><asp:TextBox ID="txtWebsite" MaxLength="100" CssClass="form-control"
                                                            runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteWebsite" runat="server" FilterType="Custom,LowercaseLetters,UppercaseLetters,Numbers"
                                                            ValidChars="." TargetControlID="txtWebsite">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingThree">
                                    <h4 class="panel-title">
                                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                            href="#IndustryDetails" aria-expanded="false" aria-controls="collapseThree"><i class="more-less fa  fa-plus">
                                            </i><span class="text-red pull-right " style="margin-right: 20px;">All fields marked
                                                as (*) are mandatory</span>Investment Details </a>
                                    </h4>
                                </div>
                                <div id="IndustryDetails" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingThree">
                                    <div class="panel-body">
                                        <asp:Panel ID="pnlInvestment" runat="server">
                                            <p class="text-red text-right">
                                                All Amounts to be Entered in INR(in Lakhs)</p>
                                            <div class="form-group" id="divOldFfci" runat="server" visible="false">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Date of First Fixed Capital Investment - Original E/M/D <a data-toggle="tooltip"
                                                            class="fieldinfo2" title="Value Prior to the E\M\D"><i class="fa fa-question-circle"
                                                                aria-hidden="true"></i></a>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div5">
                                                            <input name="ffciold" type="text" id="lblOldFcci" class="form-control" runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        <asp:Label ID="lblFfciName" runat="server" Text="Date of First Fixed Capital Investment"></asp:Label><span
                                                            class="text-red">*</span></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div10">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="txtDateFFI" class="form-control"
                                                                runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Mode of Investment<span class="text-red">*</span>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:DropDownList ID="drpChangeIn" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="upPlant" runat="server">
                                                <ContentTemplate>
                                                    <h4 class="h4-header">
                                                        Project cost</h4>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-12 ">
                                                                Total Capital Investment</label>
                                                            <div class="col-sm-12">
                                                                <table class="table table-bordered" id="tblLandPlant" runat="server">
                                                                    <tr>
                                                                        <th>
                                                                            Sl #
                                                                        </th>
                                                                        <th>
                                                                            Investment Head
                                                                        </th>
                                                                        <th visible="false">
                                                                            Investment Amount - Original E/M/D<a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to the E\M\D"><i
                                                                                class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                        </th>
                                                                        <th>
                                                                            Investment Amount(in Lakh)<span class="text-red">*</span>
                                                                        </th>
                                                                        <th>
                                                                            Upload Document<span class="text-red">*</span>
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            1
                                                                        </td>
                                                                        <td>
                                                                            Land including land development
                                                                        </td>
                                                                        <td visible="false">
                                                                            <asp:TextBox ID="lblLandOld" runat="server" Style="text-align: right;" CssClass="form-control"
                                                                                onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"
                                                                                MaxLength="13"> </asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteLandOld" TargetControlID="lblLandOld" runat="server"
                                                                                FilterType="Custom,Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:TextBox ID="txtland" runat="server" Style="text-align: right;" CssClass="form-control"
                                                                                onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"
                                                                                MaxLength="13"> </asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteLand" TargetControlID="txtland" runat="server"
                                                                                FilterType="Custom,Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rdBtnLand" runat="server" RepeatLayout="Table" RepeatDirection="Vertical">
                                                                            </asp:RadioButtonList>
                                                                            <div class="input-group">
                                                                                <asp:FileUpload ID="fuLand" CssClass="form-control" runat="server" />
                                                                                <asp:HiddenField ID="hdnLand" runat="server" />
                                                                                <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                                <asp:LinkButton ID="lnkLandAdd" runat="server" CssClass="input-group-addon bg-green"
                                                                                    OnClick="lnkOrgDocumentPdf_Click" ToolTip="RoR of Land / lease deed of Land / rent deed of premises/ self- certificate in case of rented premises / Registration details in case of IDCO Land"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkLandDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                                    OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                <asp:HyperLink ID="hypLandDelete" runat="server" Target="_blank" Visible="false"
                                                                                    CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                            </div>
                                                                            <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                            <asp:Label ID="lblLand" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            2
                                                                        </td>
                                                                        <td>
                                                                            Building & Civil Construction
                                                                        </td>
                                                                        <td visible="false">
                                                                            <asp:TextBox ID="lblBuildingOld" runat="server" Style="text-align: right;" CssClass="form-control"
                                                                                onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"
                                                                                MaxLength="13"> </asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteBuildingOld" TargetControlID="lblBuildingOld"
                                                                                runat="server" FilterType="Custom,Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:TextBox ID="txtBuilding" runat="server" Style="text-align: right;" CssClass="form-control"
                                                                                onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"
                                                                                MaxLength="13"> </asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteBuilding" TargetControlID="txtBuilding" runat="server"
                                                                                FilterType="Custom,Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <b>(Building Valuation Report)</b>
                                                                            <div class="input-group">
                                                                                <asp:FileUpload ID="fupBuildingValReport" CssClass="form-control" runat="server" />
                                                                                <asp:HiddenField ID="hdnBuildingValuation" runat="server" />
                                                                                <asp:HiddenField ID="hdnBuildDocId" runat="server" />
                                                                                <asp:LinkButton ID="lnkBuildVal" runat="server" CssClass="input-group-addon bg-green"
                                                                                    OnClick="lnkOrgDocumentPdf_Click" ToolTip="RoR of Land / lease deed of Land / rent deed of premises/ self- certificate in case of rented premises / Registration details in case of IDCO Land"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkBuildValdel" runat="server" CssClass="input-group-addon bg-red"
                                                                                    OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                <asp:HyperLink ID="hypBuildVal" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                                 <i class="fa fa-download"></i></asp:HyperLink>
                                                                            </div>
                                                                            <br />
                                                                            <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                            <asp:Label ID="lblBuildVal" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            3
                                                                        </td>
                                                                        <td>
                                                                            Plant & Machinery
                                                                        </td>
                                                                        <td visible="false">
                                                                            <asp:TextBox ID="lblPlantOld" runat="server" Style="text-align: right;" CssClass="form-control"
                                                                                onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"
                                                                                MaxLength="13"> </asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="ftePlantOld" TargetControlID="lblPlantOld" runat="server"
                                                                                FilterType="Custom,Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:TextBox ID="txtPlantMachinery" AutoPostBack="true" Style="text-align: right;"
                                                                                runat="server" CssClass="form-control" onkeypress="return FloatOnly(event, this);"
                                                                                onblur="isNumberBlur(event, this, 2);" MaxLength="13" OnTextChanged="txtPlantMachinery_TextChanged"> </asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="ftePlantMachinery" TargetControlID="txtPlantMachinery"
                                                                                runat="server" FilterType="Custom,Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                            <asp:Label ID="lblPlantMsg" Font-Bold="true" ForeColor="Red" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rdBtnPlant" runat="server" RepeatLayout="Table" RepeatDirection="Vertical">
                                                                            </asp:RadioButtonList>
                                                                            <div class="input-group">
                                                                                <asp:FileUpload ID="fuPlant" CssClass="form-control" runat="server" />
                                                                                <asp:HiddenField ID="hdnPlant" runat="server" />
                                                                                <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                                <asp:LinkButton ID="lnkPlantadd" runat="server" CssClass="input-group-addon bg-green"
                                                                                    OnClick="lnkOrgDocumentPdf_Click" ToolTip="Bills / Invoices of Plant & Machinery and equipment"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                                <asp:LinkButton ID="lnkPlandDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                                    OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                                <asp:HyperLink ID="hypPlant" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                            </div>
                                                                            <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                            <asp:Label ID="lblPlant" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                                runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            4
                                                                        </td>
                                                                        <td>
                                                                            Other Fixed Assets
                                                                        </td>
                                                                        <td visible="false">
                                                                            <asp:TextBox ID="lblOtherOld" runat="server" CssClass="form-control" Style="text-align: right;"
                                                                                onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"
                                                                                MaxLength="13"> </asp:TextBox>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:TextBox ID="txtOthers" runat="server" Style="text-align: right;" CssClass="form-control"
                                                                                onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"
                                                                                MaxLength="13"> </asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteOthersInv" TargetControlID="txtOthers" runat="server"
                                                                                FilterType="Custom,Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td>
                                                                            <strong>Total</strong><a data-toggle="tooltip" class="fieldinfo2" title="Sum of equity and loan should be equal to total capital investment"><i
                                                                                class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                        </td>
                                                                        <td class="text-right" visible="false">
                                                                            <asp:Label ID="lblTotalOld" runat="server" Text="0.00"></asp:Label>
                                                                        </td>
                                                                        <td class="text-right">
                                                                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkLandAdd" />
                                                    <asp:PostBackTrigger ControlID="lnkPlantadd" />
                                                    <asp:PostBackTrigger ControlID="lnkBuildVal" />
                                                    <asp:PostBackTrigger ControlID="txtPlantMachinery" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Working capital<span class="text-red">*</span>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtWorkingCapital" runat="server" CssClass="form-control" MaxLength="13"
                                                            onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteWorkingCapital" TargetControlID="txtWorkingCapital"
                                                            runat="server" FilterType="Custom,Numbers" ValidChars=".">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <asp:Label ID="Label3" runat="server" class="col-sm-4 col-sm-offset-1">Investment as on the date of commercial production from the competent authority</asp:Label>
                                                    <div class="col-sm-6">
                                                        <div class="input-group">
                                                            <span class="colon">:</span>
                                                            <asp:FileUpload ID="fupComAuthority" CssClass="form-control" runat="server" />
                                                            <asp:HiddenField ID="hdnCompAuthority" runat="server" />
                                                            <asp:HiddenField ID="hdnCompAuthorityDocId" runat="server" />
                                                            <asp:LinkButton ID="lnkCompAuthority" runat="server" CssClass="input-group-addon bg-green"
                                                                ToolTip="Investment as on the date of commercial production from the competent authority"
                                                                OnClick="lnkOrgDocumentPdf_Click"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkCompAuthorityDel" runat="server" CssClass="input-group-addon bg-red"
                                                                OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                            <asp:HyperLink ID="hypCompAuthority" runat="server" Target="_blank" Visible="false"
                                                                CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                        </div>
                                                        <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                        <asp:Label ID="lblComInvestment" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                            runat="server" Text="Document uploaded successfully"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <h4 class="h4-header">
                                                Finance</h4>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Equity<span class="text-red">*</span><a data-toggle="tooltip" class="fieldinfo2"
                                                            title="Sum of equity and loan should be equal to total capital investment"><i class="fa fa-question-circle"
                                                                aria-hidden="true"></i></a>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtEquity" runat="server" CssClass="form-control" MaxLength="13"
                                                            onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteEquity" TargetControlID="txtEquity" runat="server"
                                                            FilterType="Custom,Numbers" ValidChars=".">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Loan<span class="text-red">*</span><a data-toggle="tooltip" class="fieldinfo2" title="Sum of equity and loan should be equal to total capital investment"><i
                                                            class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtLoan" runat="server" CssClass="form-control" MaxLength="13" onkeypress="return FloatOnly(event, this);"
                                                            onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteLoan" TargetControlID="txtLoan" runat="server"
                                                            FilterType="Custom,Numbers" ValidChars=".">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="upBank" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Upload Bank Appraisal Report</label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <div class="input-group">
                                                                    <asp:FileUpload ID="fuBank" CssClass="form-control" runat="server" />
                                                                    <asp:HiddenField ID="hdnBank" runat="server" />
                                                                    <asp:HiddenField ID="hdnBankAppDocId" runat="server" />
                                                                    <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                    <asp:LinkButton ID="lnkbankAdd" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip="Sanction Letter of Bank / Financial Institution, if financed"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkbankDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypBank" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblBank" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkbankAdd" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        FDI (if any)
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtFdiComponent" runat="server" CssClass="form-control" MaxLength="13"
                                                            onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteFdiComponent" TargetControlID="txtFdiComponent"
                                                            runat="server" FilterType="Custom,Numbers" ValidChars=".">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <h4 class="h4-header">
                                            Plant & Machinery&nbsp;
                                            <asp:CheckBox ID="chkPlant" runat="server" Visible="false" OnCheckedChanged="chkplant_checkChanged"
                                                AutoPostBack="true" Text="(Do you want to make changes in investment for plant and
                                                    machinery?)" />
                                        </h4>
                                        <div class="form-group">
                                            <asp:Panel ID="pnlPlantMachinery" runat="server">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-sm-12  margin-bottom10">
                                                                <table class="table table-bordered">
                                                                    <tr>
                                                                        <th style="width: 15%">
                                                                            Plant & Machinery Name<span class="text-red">*</span>
                                                                        </th>
                                                                        <th style="width: 15%">
                                                                            Date of Purchase <span class="text-red">*</span>
                                                                        </th>
                                                                        <th style="width: 15%">
                                                                            Investment Amount<span class="text-red">*</span>
                                                                        </th>
                                                                        <th style="width: 5%">
                                                                            Add
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:TextBox ID="txtMachinery" runat="server" Width="70%" CssClass="form-control"
                                                                                MaxLength="100"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteMachinery" runat="server" FilterMode="InvalidChars"
                                                                                InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;.,{}+=[];',/|\_-~`" TargetControlID="txtMachinery">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <div class="input-group date datePicker">
                                                                                <asp:TextBox ID="txtDateofPurchase" runat="server" MaxLength="40" CssClass="form-control date-picker"></asp:TextBox>
                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtAmt" runat="server" MaxLength="10" Width="50%" CssClass="form-control"
                                                                                onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" TargetControlID="txtAmt"
                                                                                runat="server" FilterType="Custom,Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkMachinery" CssClass="btn btn-success btn-sm" runat="server"
                                                                                OnClick="lnkMachinery_Click" CommandName="add"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <br />
                                                                <asp:GridView ID="gvPlant" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                                                    CssClass="table table-bordered" DataKeyNames="id" OnRowCommand="gvPlant_RowCommand"
                                                                    Style="width: 100%;">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Plant & Machinery Name" DataField="MachineryName" />
                                                                        <asp:BoundField HeaderText="Date of Purchase" DataField="DateofPurchase" />
                                                                        <asp:BoundField HeaderText="Amount" DataField="Cost" />
                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                    CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="Div1">
                                    <h4 class="panel-title">
                                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                            href="#EmployementInformation" aria-expanded="false" aria-controls="collapseTwo">
                                            <i class="more-less fa  fa-plus"></i><span class="text-red pull-right " style="margin-right: 20px;">
                                                All fields marked as (*) are mandatory</span>Production & Employment Details</a>
                                    </h4>
                                </div>
                                <div id="EmployementInformation" class="panel-collapse collapse" role="tabpanel"
                                    aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <asp:Panel ID="pnlEmployement" runat="server">
                                            <p class="text-red text-right">
                                                All Amounts to be Entered in INR(in Lakhs)</p>
                                            <h4 class="h4-header">
                                                Employment Details</h4>
                                            <div class="form-group" id="divOldDirect" runat="server" visible="false">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Direct Employment (in Numbers) As on Company Payroll - Original E/M/D<a data-toggle="tooltip"
                                                            class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                aria-hidden="true"></i></a>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <%--<asp:Label ID="lblOldDirectEmp" runat="server"></asp:Label>--%>
                                                        <asp:TextBox ID="lblOldDirectEmp" MaxLength="6" CssClass="form-control phnum" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteOldDirectEmp" TargetControlID="lblOldDirectEmp"
                                                            runat="server" FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        <asp:Label ID="lblEmployementName" runat="server" Text="Direct Employment (in Numbers) As on Company Payroll"></asp:Label><span
                                                            class="text-red">*</span></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtDirectEmployement" MaxLength="6" CssClass="form-control phnum"
                                                            runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteDirectEmployement" TargetControlID="txtDirectEmployement"
                                                            runat="server" FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divOldContractual" runat="server" visible="false">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Contractual Employment - Original E/M/D<a data-toggle="tooltip" class="fieldinfo2"
                                                            title="Value Prior to E\M\D"><i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="lblOldContractual" MaxLength="4" CssClass="form-control phnum" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteOldContractual" TargetControlID="lblOldContractual"
                                                            runat="server" FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        <asp:Label ID="lblContractual" runat="server" Text="Contractual Employment"></asp:Label><span
                                                            class="text-red">*</span></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtContractualEmp" MaxLength="4" CssClass="form-control phnum" runat="server"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteContractualEmp" TargetControlID="txtContractualEmp"
                                                            runat="server" FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:UpdatePanel ID="upEmployement" runat="server">
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                <asp:Label ID="Label1" runat="server" Text="Do You Have Technical Qualification?"></asp:Label><span
                                                                    class="text-red">*</span></label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:RadioButtonList ID="rbtnTechnical" runat="server" RepeatDirection="Horizontal"
                                                                    AutoPostBack="true" OnSelectedIndexChanged="rbtnTechnical_SelectedIndexChanged">
                                                                    <asp:ListItem Text="Yes" Value="1" Selected="True">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2">
                                                                    </asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="form-group" id="dvTechnical" runat="server">
                                                                <div class="row">
                                                                    <label class="col-sm-4 col-sm-offset-1">
                                                                        Please provide Certificate in Support of Technical Qualification<span class="text-red">*</span></label>
                                                                    <div class="col-sm-6">
                                                                        <span class="colon">:</span>
                                                                        <asp:HiddenField ID="rdBtnEmployement" runat="server" />
                                                                        <%--  <asp:RadioButtonList ID="rdBtnEmployement" runat="server" RepeatLayout="Table" RepeatDirection="Vertical">
                                                                </asp:RadioButtonList>--%>
                                                                        <div class="input-group">
                                                                            <asp:FileUpload ID="fuEmployement" CssClass="form-control" runat="server" />
                                                                            <asp:HiddenField ID="hdnEmployement" runat="server" />
                                                                            <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                            <asp:LinkButton ID="lnkEmployementAdd" runat="server" CssClass="input-group-addon bg-green"
                                                                                OnClick="lnkOrgDocumentPdf_Click" ToolTip="Certificate in support of Skilled / Semi-Skilled / UnSkilled / Technical etc"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkEmployementDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                                OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                            <asp:HyperLink ID="hypEmployement" runat="server" Target="_blank" Visible="false"
                                                                                CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                        </div>
                                                                        <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                        <asp:Label ID="lblEmployement" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                            runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkEmployementAdd" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <table class="table table-bordered" id="tblEmployement" runat="server">
                                                            <tr>
                                                                <td style="width: 20%;">
                                                                    Managerial
                                                                </td>
                                                                <td style="width: 15%;" visible="false">
                                                                    <%--  <asp:Label ID="lblManagerialOld" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lblManagerialOld" runat="server" MaxLength="6"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteManagerialOld" TargetControlID="lblManagerialOld"
                                                                        runat="server" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:TextBox ID="txtManagarial" runat="server" CssClass="form-control" MaxLength="6"
                                                                        Text="0"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteManagarial" TargetControlID="txtManagarial" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td style="width: 20%;">
                                                                    General
                                                                </td>
                                                                <td style="width: 15%;" visible="false">
                                                                    <%--  <asp:Label ID="lblGeneralOld" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lblGeneralOld" runat="server" MaxLength="6"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteGeneralOld" TargetControlID="lblGeneralOld" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <asp:TextBox ID="txtGeneral" runat="server" CssClass="form-control" MaxLength="6"
                                                                        Text="0"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteGeneral" TargetControlID="txtGeneral" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Supervisor
                                                                </td>
                                                                <td visible="false">
                                                                    <%-- <asp:Label ID="lblSupOld" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lblSupOld" runat="server" MaxLength="4"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteSupOld" TargetControlID="lblSupOld" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSupervisor" runat="server" CssClass="form-control" MaxLength="6"
                                                                        Text="0"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteSupervisor" TargetControlID="txtSupervisor" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    SC
                                                                </td>
                                                                <td visible="false">
                                                                    <%--<asp:Label ID="lblTotalScOld" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lblTotalScOld" runat="server" MaxLength="6"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteTotalScOld" TargetControlID="lblTotalScOld" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTotalSc" runat="server" CssClass="form-control" MaxLength="6"
                                                                        Text="0"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteTotalSc" TargetControlID="txtTotalSc" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Skilled
                                                                </td>
                                                                <td visible="false">
                                                                    <%--  <asp:Label ID="lblSkilledOld" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lblSkilledOld" runat="server" MaxLength="6"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteSkilledOld" TargetControlID="lblSkilledOld" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSkilled" runat="server" CssClass="form-control" MaxLength="6"
                                                                        Text="0"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="ftetxtSkilled" TargetControlID="txtSkilled" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    ST
                                                                </td>
                                                                <td visible="false">
                                                                    <%--          <asp:Label ID="lblTotalStOld" runat="server"></asp:Label>--%>
                                                                    <asp:TextBox ID="lblTotalStOld" runat="server" MaxLength="6"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteTotalStOld" TargetControlID="lblTotalStOld" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTotalSt" runat="server" CssClass="form-control" MaxLength="6"
                                                                        Text="0"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteTotalSt" TargetControlID="txtTotalSt" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Semi Skilled
                                                                </td>
                                                                <td visible="false">
                                                                    <asp:TextBox ID="lblSemiSkilledOld" runat="server" MaxLength="6"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteSemiSkilledOld" TargetControlID="lblSemiSkilledOld"
                                                                        runat="server" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtSemiSkilled" runat="server" CssClass="form-control" MaxLength="6"
                                                                        Text="0"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteSemiSkilled" TargetControlID="txtSemiSkilled"
                                                                        runat="server" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    Total
                                                                </td>
                                                                <td visible="false">
                                                                    <asp:TextBox ID="lblEmpTotalOld" runat="server" MaxLength="6" ReadOnly="true"></asp:TextBox>
                                                                    <%--     <asp:Label ID="lblEmpTotalOld" runat="server"></asp:Label>--%>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTotal" ReadOnly="true" runat="server" CssClass="form-control"
                                                                        Text="0"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Un Skilled
                                                                </td>
                                                                <td visible="false">
                                                                    <asp:TextBox ID="lblUnSkilledOld" runat="server" MaxLength="6"></asp:TextBox>
                                                                    <%--  <asp:Label ID="lblUnSkilledOld" runat="server"></asp:Label>--%>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                    <cc1:FilteredTextBoxExtender ID="fteUnSkilledOld" TargetControlID="lblUnSkilledOld"
                                                                        runat="server" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtUnSKilled" runat="server" CssClass="form-control" MaxLength="6"
                                                                        Text="0"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteUnSKilled" TargetControlID="txtUnSKilled" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    Women
                                                                </td>
                                                                <td visible="false">
                                                                    <asp:TextBox ID="lblWomenOld" runat="server" MaxLength="6"></asp:TextBox>
                                                                    <%--  <asp:Label ID="lblWomenOld" runat="server"></asp:Label>--%>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                    <cc1:FilteredTextBoxExtender ID="fteWomenOld" TargetControlID="lblWomenOld" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtWomen" runat="server" CssClass="form-control" MaxLength="6" Text="0"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteWomen" TargetControlID="txtWomen" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    Total
                                                                </td>
                                                                <td visible="false">
                                                                    <asp:TextBox ID="lblGrandTotalOld" runat="server" MaxLength="6"></asp:TextBox>
                                                                    <%--       <asp:Label ID="lblGrandTotalOld" runat="server"></asp:Label>--%>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                    <cc1:FilteredTextBoxExtender ID="fteGrandTotalOld" TargetControlID="lblGrandTotalOld"
                                                                        runat="server" FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtGrandTotal" ReadOnly="true" runat="server" CssClass="form-control"
                                                                        Text="0"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteGrandTotal" TargetControlID="txtGrandTotal" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    Differently Abled Persons
                                                                </td>
                                                                <td visible="false">
                                                                    <asp:TextBox ID="lblPhdOld" runat="server" MaxLength="6"></asp:TextBox>
                                                                    <%--   <asp:Label ID="lblPhdOld" runat="server"></asp:Label>--%>
                                                                    <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                    <cc1:FilteredTextBoxExtender ID="ftePhdOld" TargetControlID="lblPhdOld" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPhd" runat="server" CssClass="form-control" MaxLength="6" Text="0"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="ftePhd" TargetControlID="txtPhd" runat="server"
                                                                        FilterType="Numbers">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                        <br />
                                        <h4 class="h4-header">
                                            Main Category of product
                                            <asp:CheckBox ID="chkProductsAmd" runat="server" AutoPostBack="true" OnCheckedChanged="chkProductsAmd_CheckChanged"
                                                Visible="false" Text="(Do
                                            you want to make changes in Production details ?)" /></h4>
                                        <asp:Panel ID="pnlProduction" runat="server">
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Code(Code may be entered as per NIC 2008) <a data-toggle="tooltip" class="fieldinfo2"
                                                            title="Accepts 5-digit code only"><i class="fa fa-question-circle" aria-hidden="true">
                                                            </i></a>
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtProductCode" runat="server" CssClass="form-control" MaxLength="5"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteProductCode" TargetControlID="txtProductCode"
                                                            runat="server" FilterType="Numbers">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        Name
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" MaxLength="100"
                                                            Onkeypress="return inputLimiter(event,'NameCharacters')"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="fteProductName" runat="server" FilterMode="InvalidChars"
                                                            InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;.,{}+=[];',/|\_-~`" TargetControlID="txtName">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group" id="divOldProd" runat="server" visible="false">
                                                <div class="row">
                                                    <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                        <a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i class="fa fa-question-circle"
                                                            aria-hidden="true"></i></a>Date of commencement of Production - Original E/M/D
                                                    </label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div6">
                                                            <input name="txtDateofProd" type="text" id="lblOldProdValue" class="form-control"
                                                                runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                        <%--<asp:Label ID="lblOldProdValue" runat="server"></asp:Label>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="row">
                                                    <label class="col-sm-4 col-sm-offset-1">
                                                        <asp:Label ID="lblProdComm" runat="server" Text="Date of commencement of Production"></asp:Label><span
                                                            class="text-red">*</span></label>
                                                    <div class="col-sm-6">
                                                        <span class="colon">:</span>
                                                        <div class="input-group  date datePicker" id="Div2">
                                                            <input name="txtTimescheduleforyearofcomm" type="text" id="txtProdComm" class="form-control"
                                                                runat="server" />
                                                            <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <asp:UpdatePanel ID="upSaleBill" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Upload Invoice of Raw Material</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <span class="colon">:</span>
                                                                    <asp:FileUpload ID="fupInvoice" TabIndex="31" CssClass="form-control" runat="server" />
                                                                    <asp:HiddenField ID="hdnInovice" runat="server" />
                                                                    <asp:HiddenField ID="hdnInvoiceDocId" runat="server" />
                                                                    <asp:LinkButton ID="lnkInovice" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip="Upload Invoice of Raw Material"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkInvoiceDel" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypInovice" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblProductFile" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully."></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Statement of Raw Material Purchased for Processing Before Production for 3 months
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <span class="colon">:</span>
                                                                    <asp:FileUpload ID="fupRawMaterialStatementPre" TabIndex="31" CssClass="form-control"
                                                                        runat="server" />
                                                                    <asp:HiddenField ID="hdnRawMaterialPre" runat="server" />
                                                                    <asp:HiddenField ID="hdnRawMaterialPreDocId" runat="server" />
                                                                    <asp:LinkButton ID="lnkRawMaterialPre" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip="Statement of Raw Material Purchased for Processing Before Production for 3 months"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkRawMaterialPreDel" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypRawMaterialPre" runat="server" Target="_blank" Visible="false"
                                                                        CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblRawMaterialPre" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully."></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Statement of Raw Material Purchased for Processing After Production for 3 months
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <span class="colon">:</span>
                                                                    <asp:FileUpload ID="fupRawMaterialStatementPost" TabIndex="31" CssClass="form-control"
                                                                        runat="server" />
                                                                    <asp:HiddenField ID="hdnRawMaterialPost" runat="server" />
                                                                    <asp:HiddenField ID="hdnRawMaterialPostDocId" runat="server" />
                                                                    <asp:LinkButton ID="lnkRawMaterialPost" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip="Statement of Raw Material Purchased for Processing After Production for 3 months "><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkRawMaterialPostDel" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypRawMaterialPost" runat="server" Target="_blank" Visible="false"
                                                                        CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblRawMaterialPost" Style="font-size: 12px;" CssClass="text-blue"
                                                                    Visible="false" runat="server" Text="Document uploaded successfully."></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Statement Showing Production for last 3 months</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <span class="colon">:</span>
                                                                    <asp:FileUpload ID="fupProductStatementPost" TabIndex="31" CssClass="form-control"
                                                                        runat="server" />
                                                                    <asp:HiddenField ID="hdnProductionPost" runat="server" />
                                                                    <asp:HiddenField ID="hdnProductiondocId" runat="server" />
                                                                    <asp:LinkButton ID="lnkProductionPost" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip="Statement Showing Production for last 3 months"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkProdcutionDel" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypProduction" runat="server" Target="_blank" Visible="false"
                                                                        CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblProduction" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully."></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Statement Showing Sale of last 3 months</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <span class="colon">:</span>
                                                                    <asp:FileUpload ID="fuFirstSaleBill" CssClass="form-control" runat="server" />
                                                                    <asp:HiddenField ID="hdnFistSaleBill" runat="server" />
                                                                    <asp:HiddenField ID="hdnFistSaleBillDocId" runat="server" />
                                                                    <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                    <asp:LinkButton ID="lnkFirstSalBillAdd" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip="Statement Showing Sale of last 3 months"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkFirstSaleBillDel" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypFirstSaleBill" runat="server" Target="_blank" Visible="false"
                                                                        CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblFirstSaleBill" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Sale Invoice</label>
                                                            <div class="col-sm-6">
                                                                <div class="input-group">
                                                                    <span class="colon">:</span>
                                                                    <asp:FileUpload ID="fupSaleInvoice" CssClass="form-control" runat="server" />
                                                                    <asp:HiddenField ID="hdnSaleInvoice" runat="server" />
                                                                    <asp:HiddenField ID="hdnSaleInvoiceDocId" runat="server" />
                                                                    <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                    <asp:LinkButton ID="lnkSaleInovice" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip="Sale Invoice"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkSaleInoviceDel" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypSaleInvoice" runat="server" Target="_blank" Visible="false"
                                                                        CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblSaleInvoice" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkFirstSalBillAdd" />
                                                    <asp:PostBackTrigger ControlID="lnkProductionPost" />
                                                    <asp:PostBackTrigger ControlID="lnkRawMaterialPost" />
                                                    <asp:PostBackTrigger ControlID="lnkRawMaterialPre" />
                                                    <asp:PostBackTrigger ControlID="lnkInovice" />
                                                    <asp:PostBackTrigger ControlID="lnkSaleInovice" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <h4 class="h4-header">
                                                Item of Production / Service with Capacity</h4>
                                            <div id="divOldProducts" runat="server" visible="false">
                                                <h4 class="h4-header">
                                                    Original E/M/D<a data-toggle="tooltip" class="fieldinfo2" title="Value Prior to E\M\D"><i
                                                        class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                </h4>
                                                <div class="form-group">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <div class="row">
                                                                <div class="col-sm-12  margin-bottom10">
                                                                    <br />
                                                                    <asp:GridView ID="grdOldProducts" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                                                        CssClass="table table-bordered" DataKeyNames="id" Style="width: 100%;" OnRowDataBound="grdProductsold_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="2%">
                                                                                <ItemTemplate>
                                                                                    <%#Container.DataItemIndex+1 %>
                                                                                    <asp:HiddenField ID="hdnUnit" runat="server" Value='<%#Eval("UnitId") %>' />
                                                                                    <asp:HiddenField ID="hdnUnitOthers" runat="server" Value='<%#Eval("Unitothers") %>' />
                                                                                    <asp:HiddenField ID="hdnIsMainProduct" runat="server" Value='<%#Eval("bitMainProduct") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField HeaderText="Item Of Product" DataField="item" />
                                                                            <asp:BoundField HeaderText="Code" DataField="Code" />
                                                                            <asp:BoundField HeaderText="Quantity" DataField="qty" />
                                                                            <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                                            <asp:BoundField HeaderText="Value" DataField="Cost" />
                                                                            <asp:BoundField HeaderText="Date of Production" DataField="dtmprod" />
                                                                            <asp:BoundField HeaderText="Is Main Category Product" DataField="VchIsMainProduct" />
                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                                <br />
                                                            </div>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <%-- <asp:PostBackTrigger ControlID="lnkAddOld" />--%>
                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                              <%--  <asp:UpdatePanel ID="up3" runat="server">--%>
                                                    <ContentTemplate>
                                                        <div class="row">
                                                            <div class="col-sm-12  margin-bottom10">
                                                                <table class="table table-bordered">
                                                                    <tr>
                                                                        <th style="width: 5%">
                                                                            Is Main Category<span class="text-red">*</span>
                                                                        </th>
                                                                        <th style="width: 15%">
                                                                            Product Name<span class="text-red">*</span>
                                                                        </th>
                                                                        <th style="width: 15%">
                                                                            Code <a data-toggle="tooltip" class="fieldinfo2" title="Accepts 5-digit code only"><i
                                                                                class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                                        </th>
                                                                        <th colspan="2" style="width: 30%">
                                                                            Quantity<span class="text-red">*</span>
                                                                        </th>
                                                                        <th style="width: 15%">
                                                                            Value<span class="text-red">*</span>
                                                                        </th>
                                                                        <th style="width: 15%">
                                                                            Date of Production<span class="text-red">*</span>
                                                                        </th>
                                                                        <th style="width: 5%">
                                                                            Add
                                                                        </th>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:CheckBox ID="chkMainCategory" runat="server" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtItemProduct" runat="server" CssClass="form-control" MaxLength="100"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteItemProduct" runat="server" FilterMode="InvalidChars"
                                                                                InvalidChars=":&quot;~!@#$%^&amp;*()?&gt;&lt;,{}+=[];',/|\_-~`" TargetControlID="txtItemProduct">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtItemCode" runat="server" MaxLength="5" CssClass="form-control"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteItemCode" runat="server" TargetControlID="txtItemCode"
                                                                                FilterType="Numbers">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtQuantity" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox><br />
                                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtQuantity"
                                                                                FilterType="Numbers">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="drpUnitType" runat="server" CssClass="form-control" AutoPostBack="true"
                                                                                OnSelectedIndexChanged="drpUnitType_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                            <asp:TextBox ID="txtUnitType" runat="server" CssClass="form-control" Onkeypress="return inputLimiter(event,'NameCharacters')"
                                                                                Visible="false"></asp:TextBox>
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtCost" runat="server" CssClass="form-control" MaxLength="11" onkeypress="return FloatOnly(event, this);"
                                                                                onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                                            <cc1:FilteredTextBoxExtender ID="fteCost" TargetControlID="txtCost" runat="server"
                                                                                FilterType="Custom,Numbers" ValidChars=".">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                        <td>
                                                                            <div class="input-group  date datePicker">
                                                                                <asp:TextBox ID="txtDateOfProd" class="form-control datePicker" runat="server" />
                                                                                <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                            </div>
                                                                        </td>
                                                                        <td>
                                                                            <asp:LinkButton ID="lnkAdd" CssClass="btn btn-success btn-sm" runat="server" OnClick="lnkAdd_ClicK"
                                                                                CommandName="add"><i class="fa fa-plus-square" ></i></asp:LinkButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <br />
                                                                <asp:GridView ID="grdProducts" runat="server" AutoGenerateColumns="false" EmptyDataText="No records found..."
                                                                    CssClass="table table-bordered" OnRowCommand="grdProducts_RowCommand" DataKeyNames="id"
                                                                    Style="width: 100%;" OnRowDataBound="grdProducts_RowDataBound">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="SlNo." ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <%#Container.DataItemIndex+1 %>
                                                                                <asp:HiddenField ID="hdnUnit" runat="server" Value='<%#Eval("UnitId") %>' />
                                                                                <asp:HiddenField ID="hdnUnitOthers" runat="server" Value='<%#Eval("Unitothers") %>' />
                                                                                <asp:HiddenField ID="hdnIsMainProduct" runat="server" Value='<%#Eval("bitMainProduct") %>' />
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField HeaderText="Product Name" DataField="item" />
                                                                        <asp:BoundField HeaderText="Code" DataField="Code" />
                                                                        <asp:BoundField HeaderText="Quantity" DataField="qty" />
                                                                        <asp:BoundField HeaderText="Unit" DataField="Unit" />
                                                                        <asp:BoundField HeaderText="Value" DataField="Cost" />
                                                                        <asp:BoundField HeaderText="Date of Production" DataField="dtmprod" />
                                                                        <asp:BoundField HeaderText="Is Main Category Product" DataField="VchIsMainProduct" />
                                                                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="5%">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkDelete" CssClass="btn btn-warning btn-sm" runat="server" CommandArgument="<%#Container.DataItemIndex%>"
                                                                                    CommandName="D"><i class="fa fa-trash" ></i></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                1st Raw material purchase Bill<span class="text-red">*</span></label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:RadioButtonList ID="rdBtnRawMaterial" runat="server" RepeatLayout="Table" RepeatDirection="Vertical">
                                                                </asp:RadioButtonList>
                                                                <div class="input-group">
                                                                    <asp:FileUpload ID="fuProduct" TabIndex="31" CssClass="form-control" runat="server" />
                                                                    <%--<asp:HiddenField ID="hdn1" runat="server" />--%>
                                                                    <asp:HiddenField ID="hdnProductfilename" runat="server" />
                                                                    <%--<asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                    <asp:LinkButton ID="lnkProductAdd" runat="server" OnClick="lnkOrgDocumentPdf_Click"
                                                                        CssClass="input-group-addon bg-green"> <i class="fa fa-upload" aria-hidden="true" ToolTip="1st Raw material purchase Bill"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkProductDel" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"> <i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink CssClass="input-group-addon bg-blue" ID="hypViewProductFile" Visible="false"
                                                                        runat="server" Target="_blank" Style="background: #31b0d5 !important; color: #FFF !important;">
                                                             <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblProductDetails" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>

                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="lnkProductAdd" />
                                                    </Triggers>
                                              <%--  </asp:UpdatePanel>--%>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlPower" runat="server">
                                            <h4 class="h4-header">
                                                Power Requirement
                                            </h4>
                                            <asp:UpdatePanel ID="upPower" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                Is Power Required
                                                            </label>
                                                            <div class="col-sm-6 margin-bottom10">
                                                                <span class="colon">:</span>
                                                                <asp:RadioButtonList ID="rdBtnLstPower" runat="server" RepeatColumns="2" RepeatLayout="Table"
                                                                    RepeatDirection="Horizontal" OnSelectedIndexChanged="rdBtnLstPower_SelectedIndexChanged"
                                                                    AutoPostBack="true">
                                                                    <asp:ListItem Text="Yes" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                                                </asp:RadioButtonList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div id="divPower" runat="server" visible="false">
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-4 col-sm-offset-1">
                                                                    Contract Demand (KW)<span class="text-red">*</span>
                                                                </label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:TextBox ID="txtContractDemand" runat="server" CssClass="form-control" MaxLength="11"
                                                                        onkeypress="return FloatOnly(event, this);" onblur="isNumberBlur(event, this, 2);"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="fteContractDemand" TargetControlID="txtContractDemand"
                                                                        runat="server" FilterType="Custom,Numbers" ValidChars=".">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label for="Iname" class="col-sm-4 col-sm-offset-1 ">
                                                                    Date of Power Connection<span class="text-red">*</span></label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <div class="input-group  date datePicker" id="Div4">
                                                                        <input name="txtTimescheduleforyearofcomm" type="text" id="txtPowerConnection" class="form-control"
                                                                            runat="server">
                                                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4 col-sm-offset-1">
                                                                    Please provide Certificate of Power Connection</label>
                                                                <div class="col-sm-6">
                                                                    <span class="colon">:</span>
                                                                    <asp:RadioButtonList ID="rdBtnPower" runat="server" RepeatLayout="Table" RepeatDirection="Vertical">
                                                                    </asp:RadioButtonList>
                                                                    <div class="input-group">
                                                                        <asp:FileUpload ID="fuPower" CssClass="form-control" runat="server" />
                                                                        <asp:HiddenField ID="hdnPower" runat="server" />
                                                                        <asp:LinkButton ID="lnkPowerAdd" runat="server" CssClass="input-group-addon
                                                                            bg-green" OnClick="lnkOrgDocumentPdf_Click" ToolTip="Certificate on Power Connection
                                                                            / 1st Electricity Bill / Certificate of Electrical Inspector for using Generator"><i
                                                                            class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkPowerDel" runat="server" CssClass="input-group-addon bg-red"
                                                                            OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:HyperLink ID="hypPower" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon
                                                                               bg-blue"> <i class="fa fa-download"></i></asp:HyperLink>
                                                                    </div>
                                                                    <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                    <asp:Label ID="lblPower" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                        runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="form-group">
                                                            <div class="row">
                                                                <label class="col-sm-4 col-sm-offset-1">
                                                                    Upload Agreement With CESU/NESCO</label>
                                                                <div class="col-sm-6">
                                                                    <div class="input-group">
                                                                        <span class="colon">:</span>
                                                                        <asp:FileUpload ID="fupAgreement" CssClass="form-control" runat="server" />
                                                                        <asp:HiddenField ID="hdnAgreement" runat="server" />
                                                                        <asp:HiddenField ID="hdnAgreementDocId" runat="server" />
                                                                        <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                        <asp:LinkButton ID="lnkAgreement" runat="server" CssClass="input-group-addon bg-green"
                                                                            OnClick="lnkOrgDocumentPdf_Click" ToolTip="Certificate on Power Connection / 1st Electricity Bill / Certificate of Electrical Inspector for using Generator"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkAgreementDel" runat="server" CssClass="input-group-addon bg-red"
                                                                            OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                        <asp:HyperLink ID="hypAgreement" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                    </div>
                                                                    <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                    <asp:Label ID="lblAgreement" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                        runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkPowerAdd" />
                                                    <asp:PostBackTrigger ControlID="lnkAgreement" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="Div3">
                                    <h4 class="panel-title">
                                        <a class="collapsed" role="button" data-toggle="collapse" data-parent="#accordion"
                                            href="#Additional" aria-expanded="false" aria-controls="collapseTwo"><i class="more-less fa  fa-plus">
                                            </i><span class="text-red pull-right " style="margin-right: 20px;">All fields marked
                                                as (*) are mandatory</span>Other Documents</a>
                                    </h4>
                                </div>
                                <div id="Additional" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingTwo">
                                    <div class="panel-body">
                                        <asp:Panel ID="pnlAdditional" runat="server">
                                            <asp:UpdatePanel ID="upAdditional" runat="server">
                                                <ContentTemplate>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                OSPCB consent to operate (except white category) <a data-toggle="tooltip" class="fieldinfo2"
                                                                    title="Consent to operate Except White category"><i class="fa fa-question-circle"
                                                                        aria-hidden="true"></i></a>
                                                                <%-- Please provide Statutory clearances & approvals / NOC / Consent to operate & clearance
                                                                on sitting criteria / Registration under Factory Act / Registration with Commercial
                                                                Tax authority--%>
                                                                <span class="text-red">*</span></label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <%--  <asp:RadioButtonList ID="rdBtnClearence" runat="server" RepeatLayout="Table" RepeatDirection="Vertical">
                                                                </asp:RadioButtonList>--%>
                                                                <asp:HiddenField ID="rdBtnClearence" runat="server" />
                                                                <div class="input-group">
                                                                    <asp:FileUpload ID="fuClearence" CssClass="form-control" runat="server" />
                                                                    <asp:HiddenField ID="hdnClearence" runat="server" />
                                                                    <%-- <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                    <asp:LinkButton ID="lnkCLearenceAdd" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip="OSPCB consent to operate (except white category)"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkClearenceDel" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypCLearence" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.zip file only and max file size 4 MB)</small>
                                                                <asp:Label ID="lblClearence" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully"></asp:Label>
                                                                <a data-toggle="tooltip" class="fieldinfo3" title="Except White Category"></a>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Sector Relevant Document <a data-toggle="tooltip" class="fieldinfo2" title="1. FSSAI/Food License- For food processing unit 
                                                                2. Explosive License -For Explosive manufacturing unit  
                                                                3. BIS Certification -For Packaged drinking water  
                                                               "><i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <%--   <asp:RadioButtonList ID="rdBtnProject" runat="server" RepeatLayout="Table" RepeatDirection="Vertical">
                                                                </asp:RadioButtonList>--%>
                                                                <asp:HiddenField ID="rdBtnProject" runat="server" />
                                                                <div class="input-group">
                                                                    <asp:FileUpload ID="fuProject" CssClass="form-control" runat="server" />
                                                                    <asp:HiddenField ID="hdnProject" runat="server" />
                                                                    <%--  <asp:Button ID="btnUpload7" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />--%>
                                                                    <asp:LinkButton ID="lnkProjectAdd" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip="FSSAI/Food License- for Food Processing/Explosive License -Explosive Products/BIS Certification for Packaged Drinking Water/Factory & Boiler for all industry 10 with power/20 without power "><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkProjectDel" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypProject" runat="server" Target="_blank" Visible="false" CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.pdf ,PNG,JPG,JPEG file only and max file size 4 MB)</small>
                                                                <a data-toggle="tooltip" class="fieldinfo3" title="Except White Category"></a>
                                                                <asp:Label ID="lblProject" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <div class="row">
                                                            <label class="col-sm-4 col-sm-offset-1">
                                                                Factory & Boiler <a data-toggle="tooltip" class="fieldinfo2" title="Factory & Boiler-1- 10 with direct employees with power/20 direct employees without power">
                                                                    <i class="fa fa-question-circle" aria-hidden="true"></i></a>
                                                            </label>
                                                            <div class="col-sm-6">
                                                                <span class="colon">:</span>
                                                                <asp:HiddenField ID="hdnBoilerDocId" runat="server" />
                                                                <div class="input-group">
                                                                    <asp:FileUpload ID="fupBoiler" CssClass="form-control" runat="server" />
                                                                    <asp:HiddenField ID="hdnBoiler" runat="server" />
                                                                    <asp:LinkButton ID="lnkBoilerAdd" runat="server" CssClass="input-group-addon bg-green"
                                                                        OnClick="lnkOrgDocumentPdf_Click" ToolTip="1-10  employment with power 2- 20 without power"><i class="fa fa-upload" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:LinkButton ID="lnkBoilerDelete" runat="server" CssClass="input-group-addon bg-red"
                                                                        OnClick="lnkOrgDocumentDelete_Click" Visible="false"><i class="fa fa-trash-o" aria-hidden="true"></i></asp:LinkButton>
                                                                    <asp:HyperLink ID="hypBoilerView" runat="server" Target="_blank" Visible="false"
                                                                        CssClass="input-group-addon bg-blue">
                                                                <i class="fa fa-download"></i></asp:HyperLink>
                                                                </div>
                                                                <small class="text-danger">(.zip file only and max file size 4 MB)</small> <a data-toggle="tooltip"
                                                                    class="fieldinfo3" title="Except White Category"></a>
                                                                <asp:Label ID="lblBoiler" Style="font-size: 12px;" CssClass="text-blue" Visible="false"
                                                                    runat="server" Text="Document uploaded successfully"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="lnkProjectAdd" />
                                                    <asp:PostBackTrigger ControlID="lnkCLearenceAdd" />
                                                    <asp:PostBackTrigger ControlID="lnkBoilerAdd" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="form-footer">
                            <asp:Button ID="btnSaveAsDraft" runat="server" Text="Save as Draft" CssClass="btn btn-warning"
                                CommandArgument="d" OnClick="btnSaveAsDraft_Click" OnClientClick="return SaveAsDraft();" />
                            <asp:Button ID="btnApply" runat="server" Text="Apply" CssClass="btn btn-success"
                                OnClientClick="return ValidatePage()" OnClick="btnSaveAsDraft_Click" CommandArgument="s" />
                            <asp:HiddenField ID="hidSubmitFlag" runat="server" Value="0" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hdnPeal" runat="server" Value="0" />
    </div>

        <uc3:footer ID="footer" runat="server" />
    </div>
    <asp:HiddenField ID="hdnOfflineEmd" runat="server" />
    <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="pnlOfflineEmd"
        TargetControlID="hdnOfflineEmd" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlOfflineEmd" runat="server" CssClass="modalfade" Style="display: none;">
        <div class="modal-dialog modal-lg">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header bg-purpul">
                    <h4 class="modal-title">
                        <asp:Label ID="lblPopUpHeader" runat="server" />
                    </h4>
                </div>
                <div class="modal-body">
                    <ol>
                        <li>
                            <asp:Label ID="lblPcMandatory" runat="server"></asp:Label>
                        </li>
                        <li>TThen fill all the data for the E/M/D details of the unit </li>
                    </ol>
                </div>
                <div class="modal-footer">
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:Button ID="btnOkPopup" runat="server" Text="Ok" class="btn btn-danger" ToolTip="Click here to fill existing details."
                                OnClick="btnokPopup_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
    
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div class="overlay">
                <div class="overlayContent">
                    <img alt="" src="../images/basicloader.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <script src="../js/bootstrap-datetimepicker.js" type="text/javascript"></script>
    <link href="../css/bootstrap-datetimepicker.css" rel="stylesheet" type="text/css" />
    </form>
    <style>
        .collapse.in
        {
            display: block;
            height: auto !important;
        }
    </style>
</body>
</html>
