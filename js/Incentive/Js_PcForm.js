
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
        if (!blankFieldValidation('txtOfflinePcNo', 'Production Certificate/EM-II No.', 'GO-SWIFT')) {
            CollapsePCDiv();
            return false;
        }
        if (!blankFieldValidation('txtPcIssueDate', 'Issue date of Production Certificate/EM-II', 'GO-SWIFT')) {
            CollapsePCDiv();
            return false;
        }
        var pcIssueDate = new Date($("#txtPcIssueDate").val());
        var todayDate = new Date();
        if (pcIssueDate > todayDate) {
            jAlert('<strong>Date of Issue of Production Certificate/EM-II cannot be greater than current date</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtPcIssueDate").focus(); });
            CollapsePCDiv();
            return false;
        }
        var hdnProductionCertificate = $("#hdnProductionCertificate").val().trim();
        if (hdnProductionCertificate == "" || hdnProductionCertificate == undefined || hdnProductionCertificate == null) {
            jAlert('<strong>Please upload Production Certificate/EM-II</strong>', 'GO-SWIFT');
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
        if ($("#txtEin").val().length != 10) {
            jAlert('<strong>EIN must be a 10-digit number</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtEin").focus(); });
            CollapseFirst();
            return false;
        }
        if (!blankFieldValidation('txtDateOfIssuance', 'Date of EIN Issue', 'GO-SWIFT')) {
            CollapseFirst();
            return false;
        }
        var einIssueDate = new Date($("#txtDateOfIssuance").val());
        var todayDate = new Date();
        if (ein != null && ein != undefined && ein != '' && einIssueDate > todayDate) {
            jAlert('<strong>Date of EIN Issue cannot be greater than current date</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtDateOfIssuance").focus(); });
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

    var ein = $("#txtEin").val().trim();
    var UAN = $("#txtUan").val().trim();
    if ((ein == null || ein == undefined || ein == '') && (UAN == null || UAN == undefined || UAN == '')) {
        jAlert('<strong>Please enter either the UAN or the EIN number</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtUan").focus(); });
        CollapseFirst();
        return false;
    }


    if (!blankFieldValidation('txtEnterpriseName', 'Name of Enterprise/Industrial Unit', 'GO-SWIFT')) {
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
            CollapseFirst();
            return false;
        }
        if (!CheckAlphaNumeric("txtGST", "GSTIN", 'GO-SWIFT')) {
            return false;
        }
    }

    if (!DropDownValidation('drpOrganizationType', '0', 'Constitution of Organization', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#drpOrganizationType").focus(); });
        CollapseFirst();
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
        jAlert('<strong>Please enter valid email in Registered Office / Communication Address</strong>', 'GO-SWIFT');
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

    var ChangeIn = $("#drpChangeIn").val();
    debugger;
    if (ChangeIn == "1120") { //if user has selected ein number
        var ein = $("#txtEin").val().trim();
        if (ein == null || ein == undefined || ein == '') {
            jAlert('<strong>As you have selected EIN as your mode of investment, Please enter your ein number.</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtEin").focus(); });
            CollapseFirst();
            return false;
        }
        else if (ein != null && ein != undefined && ein != '') {
            if ($("#txtEin").val().length != 10) {
                jAlert('<strong>EIN must be a 10-digit number</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#txtEin").focus(); });
                CollapseFirst();
                return false;
            }
        }

        var txtIssuanceDate = $("#txtDateOfIssuance").val();
        if (txtIssuanceDate == null || txtIssuanceDate == undefined || txtIssuanceDate == '') {
            jAlert('<strong>As you have selected EIN as your mode of investment, Please enter your ein Issuance Date.</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtDateOfIssuance").focus(); });
            CollapseFirst();
            return false;
        }

        var einIssueDate = new Date($("#txtDateOfIssuance").val());

        if (einIssueDate > (new Date())) {
            jAlert('<strong>EIN Issuance date cannot be greater than current date</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtDateFFI").focus(); });
            CollapseIndustry();
            return false;
        }

        dtmFfi = dtmFfi.setHours(0, 0, 0, 0);
        einIssueDate = einIssueDate.setHours(0, 0, 0, 0);
        if (dtmFfi != einIssueDate) {
            jAlert('<strong>As you have selected EIN as your mode of investment, your Date of FFCI and Date of EIN Issuance should be same. Please check.</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtDateFFI").focus(); });
            CollapseIndustry();
            return false;
        }
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

    var hdnFistSaleBill = document.getElementById("hdnFistSaleBill").value;
    if (hdnFistSaleBill == null || hdnFistSaleBill == undefined || hdnFistSaleBill == "") {
        jAlert('<strong>Please provide 1st Sale Bill</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#fuFirstSaleBill").focus(); });
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
    }

    var hdnClearence = document.getElementById("hdnClearence").value;
    if (hdnClearence == null || hdnClearence == undefined || hdnClearence == "") {
        jAlert('<strong>Please provide document for OSPCB consent to operate (except white category)</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#fuClearence").focus(); });
        CollaspseAdditional();
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

function CollaspseAdditional() {
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
    var hdn = $("#hdnPeal").val();
    if (hdn != null && hdn != undefined && hdn != '') {
        if (hdn != "0") {
            $('.fieldinfo2').title = "Value Before IPR 2015 effective date";
        }
    }
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
    $("#lblOtherOld, #lblPlantOld, #lblBuildingOld, #lblLandOld").on('change', CalculateTotalCapitalOriginal);
    $("#lblTotalScOld, #lblTotalStOld, #lblGeneralOld").on('keyup', CalculateCatTotalOriginal);

    //  }
    $(".panel-title > a").on("click", function () {
        //hdnVisibleAcc
        var href = $(this).attr("href");
        $("#hdnVisibleAcc").val(href);
    });
    $("#lnkMachinery").on('click', ValidateMachineryLnk);
    $("#lnkAdd").on('click', ValidateAdd);

    $("#txtDateOfIssuance").change(function () {
        var changeIn = $("#drpChangeIn").val();
        if (changeIn != null && changeIn != undefined && changeIn != '' && changeIn == 1120) {
            var val = $(this).val();
            if (val != null && val != undefined && val != '') {
                $("#txtDateFFI").val($(this).val());
            }
        }
    });

}

function OpenPopUpId(lnkButton) {
    var parentTd = $(lnkButton).parent("td");
    var fileTd = $(parentTd).closest('td').prev('td');
    var fld = $(fileTd).find("input:file");
    alert(fld.id);
    //$("#" + i).click();
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
    debugger;
    if (!blankFieldValidation('txtMachinery', 'Plant & Machinery Name', 'GO-SWIFT')) {
        return false;
    }

    if (!blankFieldValidation('txtDateofPurchase', 'Date of Purchase', 'GO-SWIFT')) {
        return false;
    }
    if (!blankFieldValidation('txtAmt', 'Investment Amount', 'GO-SWIFT')) {
        return false;
    }
    var investAmount = $("#txtAmt").val();
    if (parseFloat(investAmount) == 0.00) {
        jAlert('<strong>Investment amount Cannot be zero</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtAmt").focus(); });
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
    var txtDateOfProd = $("#txtDateOfProd");
    var product = $(txtProduct).val();
    var code = $(txtItemCode).val();
    var quantity = $(txtQuantity).val();
    var Unit = $(drpUnit).val();
    var Cost = $(txtCost).val();
    var DateOfProd = $(txtDateOfProd).val();
    var codelen = $(txtItemCode).val().length;
    if (!blankFieldValidation('txtItemProduct', 'Product Name', 'GO-SWIFT')) {
        return false;
    }
    if (code != "") {
        if (codelen != 5) {
            jAlert('<strong>Code should be of 5-digit</strong>', 'alert');
            $("#popup_ok").click(function () { $("#txtItemCode").focus(); });
            return false;
        }
    }

    if (!blankFieldValidation('txtQuantity', 'quantity', 'GO-SWIFT')) {
        return false;
    }

    if (quantity == "0") {
        jAlert('<strong>Please select quantity for the product</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtQuantity").focus(); });
        return false;
    }
    if (!DropDownValidation('drpUnitType', '0', 'Unit Type', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#drpUnitType").focus(); });
        return false;
    }
    if (Unit == "52") {
        if (!blankFieldValidation('txtUnitType', 'Unit Type for others', 'GO-SWIFT')) {

            return false;
        }
    }
    if (!blankFieldValidation('txtCost', 'Value of product', 'GO-SWIFT')) {
        return false;
    }
    if (isNaN(Cost)) {
        jAlert('<strong>Please enter valid value for product</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtCost").focus(); });
        return false;
    }
    if (parseFloat(Cost) == 0.00) {
        jAlert('<strong>Please enter valid value for product</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtCost").focus(); });
        return false;
    }
    if (!blankFieldValidation('txtDateOfProd', 'Date of Production of product - ' + $("#txtItemProduct").val(), 'GO-SWIFT')) {
        return false;
    }
    var dtmprod = new Date(DateOfProd);
    var todayDate = new Date();
    if (dtmprod > todayDate) {
        jAlert('<strong>Date of Production of ' + $("#txtItemProduct").val() + ' cannot be greater than current date</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtDateOfProd").focus(); });
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
        $(this).val("0");
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
    debugger;
    var txtequity = $("#txtEquity").val();
    var txtloan = $("#txtLoan").val();
    var equity = 0.00, loan = 0.00;
    var land = 0.0, Plant = 0.0, Building = 0.0, Others = 0.0;
    land = $("#txtland").val();
    Building = $("#txtBuilding").val();
    Plant = $("#txtPlantMachinery").val();
    Others = $("#txtOthers").val();
    var isValid = true;
    var total = 0.00;
    total = CalculateTotalCapital();
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
        $(this).val(0.00);
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
    var intsupold = 0, intmanagerialold = 0, intskilledold = 0, intunskilledold = 0, intsemiskilledold = 0;
    managerialold = $("#lblManagerialOld").val();
    supold = $("#lblSupOld").val();
    skilledold = $("#lblSkilledOld").val();
    unskilledold = $("#lblUnSkilledOld").val();
    semiskilledold = $("#lblSemiSkilledOld").val();
    if (managerialold != null && managerialold != undefined && managerialold != '') {
        intmanagerialold = parseInt(managerialold);
    }
    if (supold != null && supold != undefined && supold != '') {
        intsupold = parseInt(supold);
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
    totalold = intsupold + intmanagerialold + intsemiskilledold + intskilledold + intunskilledold;
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


function CompareEmployementold() {
    var supold = 0, managerialold = 0, skilledold = 0, unskilledold = 0, semiskilledold = 0;
    managerialold = $("#lblManagerialOld").val();
    supold = $("#lblSupOld").val();
    skilledold = $("#lblSkilledOld").val();
    unskilledold = $("#lblUnSkilledOld").val();
    semiskilledold = $("#lblSemiSkilledOld").val();
    var totalScold = 0, totalSt = 0, general = 0;
    totalStold = $("#lblTotalScOld").val();
    totalScold = $("#lblTotalStOld").val();
    generaoldl = $("#lblGeneralOld").val();
    CalculateCatTotaloriginal();
    CalculateTotalStafforiginal();

    if (supold == null || supold == undefined || supold == '' || supold == 0) {
        return true;
    }
    if (managerialold == null || managerialold == undefined || managerialold == '' || managerialold == 0) {
        return true;
    }
    if (skilledold == null || skilledold == undefined || skilledold == '' || skilledold == 0) {
        return true;
    }
    if (unskilledold == null || unskilledold == undefined || unskilledold == '' || unskilledold == 0) {
        return true;
    }
    if (semiskilledold == null || semiskilledold == undefined || semiskilledold == '' || semiskilledold == 0) {
        return true;
    }
    if (totalStold == null || totalStold == undefined || totalStold == '' || totalStold == 0) {
        return true;
    }
    if (totalScold == null || totalScold == undefined || totalScold == '' || totalScold == 0) {
        return true;
    }
    if (generalold == null || generalold == undefined || generalold == '' || generalold == 0) {
        return true;
    }


    var totalTypeold = 0, totalCategoryold = 0;
    var txttypeold = $("#lblTotalOld").val();
    var txtGrandTotalold = $("#lblGrandTotalOld").val();
    if (txttypeold != null && txttypeold != undefined && txttypeold != '') {
        totalTypeold = parseInt(txttype);
    }
    if (txtGrandTotalold != null && txtGrandTotalold != undefined && txtGrandTotalold != '') {
        totalCategoryold = parseInt(txtGrandTotalold);
    }
    if (totalCategoryold != totalTypeold) {
        jAlert('<strong>Total employee by skills should be equal to total employee by category</strong>', 'GO-SWIFT');
        $(this).val("0");
        CalculateCatTotalOriginal();
        CalculateTotalStaffOriginal();
        return false;
    }
    else {
        return true;
    }
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
        if ($("#txtEin").val().length != 10) {
            jAlert('<strong>EIN must be a 10-digit number</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtEin").focus(); });
            CollapseFirst();
            return false;
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
            location.href = 'IncentivePCForm.aspx';
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
            location.href = 'IncentivePCForm.aspx?offline=3';
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