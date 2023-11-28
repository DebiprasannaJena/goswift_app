var appendId = "ContentPlaceHolder1_";
$(document).ready(function () {
    $('.datePicker').datepicker({
        format: 'dd-M-yyyy',
        changeMonth: true,
        changeYear: true,
        autoclose: true
    });
});
function pageLoad() {
    $('.datePicker').datepicker({
        format: 'dd-M-yyyy',
        changeMonth: true,
        changeYear: true,
        autoclose: true
    });

    CheckLengthKeyUp(appendId + 'txtOfficeAddress', appendId + 'lblOfficeAddress', 200);
    CheckLengthKeyUp(appendId + 'txtEnterpriseAddress', appendId + 'lblRemark', 200);
    CheckLengthKeyUp(appendId + 'txtPowerLoad', appendId + 'lblPowerRemarks', 200);
    CheckLengthKeyUp(appendId + 'txtCpp', appendId + 'lblCpp', 200);

    UpdateLengthInProblemGrid();

    $("#txtManagarial, #txtSupervisor, #txtSkilled, #txtSemiSkilled, #txtUnskilled").on('keyup', CalculateTotalStaff);
    $("#txtTotalSc, #txtTotalSt, #txtGeneral").on('keyup', CalculateCatTotal);
    CalculateCatTotal();
    CalculateTotalStaff();
    CalculatedprTotal();
    CalculatedExpTotal();
    CalculatedExpTotalOld();
    $(".dpr").on('change', CalculatedprTotal);
    $(".exp").on('change', CalculatedExpTotal);
    $(".exps").on('change', CalculatedExpTotalOld);
    $("#" + appendId + "lnkMachinery").on('click', ValidateMachineryLnk);

    $("#" + appendId + "lnkAppliedAdd").on('click', function () {
        if (!DropDownValidation(appendId + 'drpIncentiveType', '0', 'Incentive type', 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtQuantam', "Amount", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtFromDate', "From Date", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtToDate', "To Date", 'GO-SWIFT')) {
            return false;
        }
        debugger;
        var txtFromDate = $("#" + appendId + "txtFromDate").val();
        var txtToDate = $("#" + appendId + "txtToDate").val();
        var fromdate = new Date(txtFromDate);
        var todate = new Date(txtToDate);

        if (fromdate > todate) {
            jAlert('<strong>Fromdate of already applied incentives cannot be greater than to date.</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + "txtFromDate").focus(); });
            return false;
        }
        if (!DropDownValidation(appendId + 'drpIpr', '0', 'IPR Capability', 'GO-SWIFT')) {
            return false;
        }
    });

    $("#" + appendId + "lnkOthersAdd").on('click', function () {
        if (!blankFieldValidation(appendId + 'txtUnit', "Unit Name", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtProduct', "Product", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtCapacity', "Capacity", 'GO-SWIFT')) {
            return false;
        }
        if (!DropDownValidation(appendId + 'ddlState', '0', 'State', 'GO-SWIFT')) {
            $("#popup_ok").click(function () { $("#" + appendId + "ddlState").focus(); });
            return false;
        }
        if (!DropDownValidation(appendId + 'drpDistrict', '0', 'District', 'GO-SWIFT')) {
            $("#popup_ok").click(function () { $("#" + appendId + "drpDistrict").focus(); });
            return false;
        }
    });

    $("#" + appendId + "lnkClearenceAdd").on('click', function () {

        if (!DropDownValidation(appendId + 'drpClearence', '0', 'Clearence Type', 'GO-SWIFT')) {
            $("#popup_ok").click(function () { $("#" + appendId + "drpClearence").focus(); });
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtFromClDate', "From Date", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtToClDate', "To Date", 'GO-SWIFT')) {
            return false;
        }
        var txtFromDate = $("#" + appendId + "txtFromClDate").val();
        var txtToDate = $("#" + appendId + "txtToClDate").val();
        var fromdate = new Date(txtFromDate);
        var todate = new Date(txtToDate);

        if (fromdate > todate) {
            jAlert('<strong>From date cannot be greater than to date.</strong>', 'GO-SWIFT');
            document.getElementById(appendId + "txtFromClDate").focus();
            $("#popup_ok").click(function () { $("#" + appendId + "txtFromClDate").focus(); });
            return false;
        }
    });

    $("#" + appendId + "lnkOfficerAdd").on('click', function () {
        if (!blankFieldValidation(appendId + 'txtOfficerName', "Officer name", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtDesignation', "Designation", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtOrganization', "Organization", 'GO-SWIFT')) {
            return false;
        }
    });

    $("#" + appendId + "lnkAdd").on('click', function () {
        var txtProduct = $("#" + appendId + "txtItemProduct");
        var txtQuantity = $("#" + appendId + "txtQuantity");
        var drpUnit = $("#" + appendId + "drpUnitType");
        var txtCost = $("#" + appendId + "txtCost");
        var txtDtmProd = $("#" + appendId + "txtDateOfProd");
        var product = $(txtProduct).val();
        var quantity = $(txtQuantity).val();
        var Unit = $(drpUnit).val();
        var Cost = $(txtCost).val();
        var dtmProd = $(txtDtmProd).val();
        var txtItemCode = $("#txtItemCode").val();

        if (product == null || product == undefined || product == '') {
            jAlert('<strong>Please enter product name</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $(txtProduct).focus(); });
            return false;
        }
        if (!blankFieldValidation(appendId + "txtQuantity", 'quantity', 'GO-SWIFT')) {
            return false;
        }
        if (txtItemCode != null && txtItemCode != undefined && txtItemCode != "") {
            var codelen = txtItemCode.length;
            if (codelen != 5) {
                jAlert('<strong>Code should be of 5-digit</strong>', 'alert');
                $("#popup_ok").click(function () { $("#txtItemCode").focus(); });
                return false;
            }
        }
        if (quantity == "0") {
            jAlert('<strong>Please select quantity for the product</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $(txtQuantity).focus(); });
            return false;
        }
        if (!DropDownValidation(appendId + "drpUnitType", '0', 'Unit Type', 'GO-SWIFT')) {
            $("#popup_ok").click(function () { $("#" + appendId + "drpUnitType").focus(); });
            return false;
        }
        if (Unit == "52") {
            if (!blankFieldValidation(appendId + 'txtUnitType', 'Unit Type for others', 'GO-SWIFT')) {
                return false;
            }
        }
        if (!blankFieldValidation(appendId + 'txtCost', 'Value of product', 'GO-SWIFT')) {
            return false;
        }
        if (isNaN(Cost)) {
            jAlert('<strong>Please enter valid value for product</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $(txtCost).focus(); });
            return false;
        }
        if (parseFloat(Cost) == 0.00) {
            jAlert('<strong>Please enter valid value for product</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $(txtCost).focus(); });
            return false;
        }
        if (dtmProd == null || dtmProd == undefined || dtmProd == '') {
            jAlert('<strong>Please enter date of  Production</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $(txtDtmProd).focus(); });
            return false;
        }
        var txtDateOfProd = new Date($("#" + appendId + "txtDateOfProd").val());
        var dtmProductionComm = new Date($("#" + appendId + "txtProdCommencement").val());
        if (txtDateOfProd != '') {
            if (txtDateOfProd < dtmProductionComm) {
                jAlert('<strong>Date of Production cannot be less than Date of Production Commencement</strong>', 'GO-SWIFT');
                $("#popup_ok").click(function () { $("#" + appendId + "txtDateOfProd").focus(); });
                return false
            }
        }
        if ((new Date(dtmProd)) > new Date()) {
            jAlert('<strong>Date of production cannot be greater than current date</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $(txtDtmProd).focus(); });
            return false;
        }
        else {
            return true;
        }
    });

    $("#" + appendId + "lnkTermAdd").on('click', function () {
        if (!blankFieldValidation(appendId + 'txtTlInstitue', "Name of financial institution", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtState', "State", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtCity', "City", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtAmount', "Amount", 'GO-SWIFT')) {
            return false;
        }
        if (parseFloat($("#" + appendId + 'txtAmount').val()) == 0.00) {
            jAlert('<strong>Please enter valid term loan amount</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + 'txtAmount').focus(); });
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtSanctionDate', "Term loan sanction date", 'GO-SWIFT')) {
            return false;
        }
        debugger;
        var sanctionDate = new Date(document.getElementById(appendId + 'txtSanctionDate').value);
        if (sanctionDate > (new Date())) {
            jAlert('<strong>Date of sanction cannot be greater than current date</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + 'txtSanctionDate').focus(); });
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtAvailedAmount', "term loan availed amount", 'GO-SWIFT')) {
            return false;
        }
        if (parseFloat($("#" + appendId + 'txtAvailedAmount').val()) == 0.00) {
            jAlert('<strong>Please enter valid Availed term loan amount</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + 'txtAvailedAmount').focus(); });
            return false;
        }
        var availaedAmt = parseFloat($("#" + appendId + 'txtAvailedAmount').val());
        var sanctionedAmt = parseFloat($("#" + appendId + 'txtAmount').val());
        if (availaedAmt > sanctionedAmt) {
            jAlert('<strong>Amount availed cannot be greater than amount sanctioned</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + 'txtAvailedAmount').focus(); });
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtAvailedDate', "term loan availed date", 'GO-SWIFT')) {
            return false;
        }
        var availeddate = new Date(document.getElementById(appendId + 'txtAvailedDate').value);
        if (availeddate > (new Date())) {
            jAlert('<strong>Availed date cannot be greater than current date</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + 'txtAvailedDate').focus(); });
            return false;
        }
        if (availeddate < sanctionDate) {
            jAlert('<strong>Availed date cannot be less than sanction date</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + 'txtAvailedDate').focus(); });
            return false;
        }

    });

    $("#" + appendId + "lnkWcAdd").on('click', function () {
        if (!blankFieldValidation(appendId + 'txtWcInstitue', "name of financial institution", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtWcState', "state", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtWcCity', "city", 'GO-SWIFT')) {
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtWcAmt', "Amount", 'GO-SWIFT')) {
            return false;
        }
        if (parseFloat($("#" + appendId + 'txtWcAmt').val()) == 0.00) {
            jAlert('<strong>Please enter valid Working capital loan amount</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + 'txtWcAmt').focus(); });
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtWcSanctionDate', "Working capital sanction date", 'GO-SWIFT')) {
            return false;
        }
        var sanctionDate = new Date(document.getElementById(appendId + 'txtWcSanctionDate').value);
        if (sanctionDate > (new Date())) {
            jAlert('<strong>Date of sanction cannot be greater than current date</strong>', 'GO-SWIFT');
            $("#" + appendId + 'txtWcSanctionDate').focus();
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtWcAvailedAmt', "Working capital availed amount", 'GO-SWIFT')) {
            return false;
        }
        if (parseFloat($("#" + appendId + 'txtWcAvailedAmt').val()) == 0.00) {
            jAlert('<strong>Please enter valid Availed working capital loan amount</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + 'txtWcAvailedAmt').focus(); });
            return false;
        }
        var availaedAmt = parseFloat($("#" + appendId + 'txtWcAvailedAmt').val());
        var sanctionedAmt = parseFloat($("#" + appendId + 'txtWcAmt').val());
        if (availaedAmt > sanctionedAmt) {
            jAlert('<strong>Amount availed cannot be greater than amount sanctioned</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + 'txtWcAvailedAmt').focus(); });
            return false;
        }
        if (!blankFieldValidation(appendId + 'txtWcAvailedDate', "Working capital availed date", 'GO-SWIFT')) {
            return false;
        }
        var availeddate = new Date(document.getElementById(appendId + 'txtWcAvailedDate').value);
        if (availeddate > (new Date())) {
            jAlert('<strong>Availed date cannot be greater than current date</strong>', 'GO-SWIFT');
            $("#" + appendId + 'txtWcAvailedDate').focus();
            return false;
        }
        if (availeddate < sanctionDate) {
            jAlert('<strong>Availed date cannot be less than sanction date</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + 'txtWcAvailedDate').focus(); });
            return false;
        }

    });

    $("#" + appendId + "lnkSupDocAdd").on('click', function () {
        if (!blankFieldValidation(appendId + 'txtNameOfSite', "Name of site", 'GO-SWIFT')) {
            return false;
        }
        if (!DropDownValidation(appendId + 'drpApprovalAuthority', '0', 'Approval Authority', 'GO-SWIFT')) {
            $("#popup_ok").click(function () { $("#" + appendId + "drpApprovalAuthority").focus(); });
            return false;
        }
        if ($("#" + appendId + "drpApprovalAuthority").is(":visible")) {
            if (!blankFieldValidation(appendId + 'txtOthers', "Approval Authority", 'GO-SWIFT')) {
                return false;
            }
        }
    });

}

function UpdateLengthInProblemGrid() {
    debugger;
    var arrTr = $("#" + appendId + "grdProblems>tbody>tr");
    if ($(arrTr).length > 0) {
        var trLength = $(arrTr).length;
        for (cnt = 1; cnt < trLength; cnt++) {
            var currTr = $(arrTr)[cnt];
            var txtBox = $(currTr).find("textarea");
            var lbl = $(currTr).find("span");
            if (txtBox != null && txtBox != undefined) {
                CheckLengthKeyUp($(txtBox).attr("id"), $(lbl).attr("id"), 200);
            }
        }
    }
}



function CalculateTotalStaff() {
    var sup = 0, managerial = 0, skilled = 0, unskilled = 0, semiskilled = 0;
    var intsup = 0, intmanagerial = 0, intskilled = 0, intunskilled = 0, intsemiskilled = 0;
    managerial = $("#txtManagarial").val();
    sup = $("#txtSupervisor").val();
    skilled = $("#txtSkilled").val();
    unskilled = $("#txtUnskilled").val();
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



function CalculatedprTotal() {
    var total = 0.00;
    $(".dpr").each(function () {
        var txtBoxval = parseFloat($(this).val());
        total = total + txtBoxval;
    });
    $(".spDpr").text(parseFloat(total).toFixed(2));
}

function CalculatedExpTotal() {
    var total = 0.00;
    $(".exp").each(function () {
        var txtVal = $(this).val();
        if (txtVal != null && txtVal != undefined && txtVal != '') {
            var txtBoxval = parseFloat($(this).val());
            total = total + txtBoxval;
        }
    });

    $(".spExp").text(parseFloat(total).toFixed(2));
}

function CalculatedExpTotalOld() {
    var totals = 0.00;
    $(".exps").each(function () {
        var txtVals = $(this).val();
        if (txtVals != null && txtVals != undefined && txtVals != '') {
            var txtBoxvals = parseFloat($(this).val());
            totals = totals + txtBoxvals;
        }
    });

    $(".spExps").text(parseFloat(totals).toFixed(2));
}

function ValidatePage(btn) {
    debugger;
    var btnId = btn.id;
    if (!blankFieldValidation(appendId + 'txtDateOfInspection', "Date of Inspection", 'GO-SWIFT')) {
        return false;
    }
    var dateOfinspection = new Date(document.getElementById(appendId + 'txtDateOfInspection').value);
    var hdnScheduleDt = $("#" + appendId + "hdnScheduleDt").val();

    var dateOfinspections = new Date($("#" + appendId + "txtDateOfInspection").val());
    var hdnScheduleDts = new Date(hdnScheduleDt);
    dateOfinspections.setHours(0, 0, 0, 0);
    hdnScheduleDts.setHours(0, 0, 0, 0);
    if (dateOfinspections < hdnScheduleDts) {
        jAlert('<strong>Date of Inspection cannot be less than schedule date </strong>' + hdnScheduleDt, 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtDateOfInspection").focus(); });
        return false;
    }
    if (dateOfinspections > hdnScheduleDts) {
        jAlert('<strong>Date of Inspection cannot be greater than schedule date </strong>' + hdnScheduleDt, 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtDateOfInspection").focus(); });
        return false;
    }

    var dateOfProductionComm = new Date(document.getElementById(appendId + 'txtProdCommencement').value);
    if (dateOfinspection < dateOfProductionComm) {
        jAlert('<strong>Date of Inspection cannot be greater than date of production commencement</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtDateOfInspection").focus(); });
        return false;
    }
    if (!ValidateGrid(appendId + "grdOffice")) {
        jAlert('<strong>Please enter atleast one or more name of officers available for inspection</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtOfficerName").focus(); });
        return false;
    }
    if (!DropDownValidation(appendId + 'drpApplicationType', '0', 'Application For', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "drpApplicationType").focus(); });
        return false;
    }
    if ($("#" + appendId + "divChangeIn").is(":visible")) {
        if ((!ValidateCheckBoxList(appendId + "chkLstChange"))) {
            jAlert('<strong>Please Select atleast one Change in value</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + "chkLstChange").focus(); });
            return false;
        }
    }

    if (!DropDownValidation(appendId + 'drpUnitCategory', '0', 'Unit Category', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "drpUnitCategory").focus(); });
        return false;
    }

    if (!DropDownValidation(appendId + 'drpCompanyType', '0', 'Nature of Activity', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "drpCompanyType").focus(); });
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtEnterpriseName', 'Name of Enterprise/Industrial Unit', 'GO-SWIFT')) {
        return false;
    }
    if (!DropDownValidation(appendId + 'drpOrganizationType', '0', 'Organization type', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "drpOrganizationType").focus(); });
        return false;
    }
    if (!DropDownValidation(appendId + 'ddlSector', '0', 'Sector of activity', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "ddlSector").focus(); });
        return false;
    }
    if (!DropDownValidation(appendId + 'ddlSubSector', '0', 'Sub sector', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "ddlSubSector").focus(); });
        return false;
    }
    if (!DropDownValidation(appendId + 'drpSalutation', '0', 'Salutation', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "drpSalutation").focus(); });
        return false;
    }

    if (!blankFieldValidation(appendId + 'txtOwnerName', 'Name of Owner', 'GO-SWIFT')) {
        return false;
    }
    if (!DropDownValidation(appendId + 'drpOwnerType', '0', 'Ownership Pattern', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "drpOwnerType").focus(); });
        return false;
    }

    if (!blankFieldValidation(appendId + 'txtOfficeAddress', 'Registered/Communication Address', 'GO-SWIFT')) {
        return false;
    }

    if (!DropDownValidation(appendId + 'ddlCode', '0', 'Code for Telephone No. in Registered Office / Communication Address ', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "ddlCode").focus(); });
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtOfficePhone', 'Telephone No. in Registered Office / Communication Address', 'GO-SWIFT')) {
        return false;
    }
    if (!ValidateNumber(appendId + 'txtOfficePhone', 'Telephone No. in Registered Office / Communication Address', 'GO-SWIFT')) {
        return false;
    }
    if (parseInt($("#" + appendId + "txtOfficePhone").val()) == 0) {
        jAlert('<strong>Please enter valid Telephone No. in Registered Office / Communication Address</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtOfficePhone").focus(); });
        return false;
    }
    if (!ValidateFieldLength(appendId + 'txtOfficePhone', 10, 'Telephone No. in Registered Office / Communication Address', 'GO-SWIFT')) {
        return false;
    }
    var officeFax = $("#" + appendId + "txtOfficeFax").val();
    if (officeFax != null && officeFax != undefined && officeFax != '' && officeFax != '0') {
        if (parseInt(officeFax) == 0) {
            jAlert('<strong>Please enter valid office Fax in Registered Office / Communication Address</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + "txtOfficeFax").focus(); });
            return false;
        }
        if (officeFax.length != 10) {
            jAlert('<strong>Please enter valid office fax in Registered Office / Communication Address</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + "txtOfficeFax").focus(); });
            return false;
        }
        if (!DropDownValidation(appendId + 'drpFx', '0', 'Code for Fax in Registered Office / Communication Address', 'GO-SWIFT')) {
            $("#popup_ok").click(function () { $("#" + appendId + "drpFx").focus(); });
            return false;
        }
    }
    if (!blankFieldValidation(appendId + 'txtOfficeEmail', 'Email', 'GO-SWIFT')) {
        return false;
    }
    if (!ValidateEmail($("#" + appendId + "txtOfficeEmail").val())) {
        jAlert('<strong>Please enter valid email in Registered Office / Communication Address</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtOfficeEmail").focus(); });
        return false;
    }

    if (!DropDownValidation(appendId + 'drpEntCode', '0', 'Code for Enterprise number', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "drpEntCode").focus(); });
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtEnterpriseAddress', 'Enterprise Address', 'GO-SWIFT')) {
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtPhoneNo', 'Telephone No.', 'GO-SWIFT')) {
        return false;
    }
    if (!ValidateNumber(appendId + 'txtPhoneNo', 'Telephone No.', 'GO-SWIFT')) {
        return false;
    }
    if (!ValidateFieldLength(appendId + 'txtPhoneNo', 10, 'Telephone No.', 'GO-SWIFT')) {
        return false;
    }
    if (parseInt($("#" + appendId + "txtPhoneNo").val()) == 0) {
        jAlert('<strong>Please enter valid Telephone No. in Location of the unit</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtPhoneNo").focus(); });
        return false;
    }
    var EnterPriseFax = $("#" + appendId + "txtFax").val();
    if (EnterPriseFax != null && EnterPriseFax != undefined && EnterPriseFax != '' && EnterPriseFax != '0') {
        if (!DropDownValidation(appendId + 'drpEnterpriseFax', '0', 'Code for enterprise Fax', 'GO-SWIFT')) {
            $("#popup_ok").click(function () { $("#" + appendId + "drpEnterpriseFax").focus(); });
            return false;
        }
        if (EnterPriseFax.length != 10) {
            jAlert('<strong>Please enter valid enterprise fax</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + "txtFax").focus(); });
            return false;
        }
        if (parseInt(EnterPriseFax) == 0) {
            jAlert('<strong>Please enter valid enterprise fax</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + "txtFax").focus(); });
            return false;
        }
    }
    if (!blankFieldValidation(appendId + 'txtEmail', 'Email', 'GO-SWIFT')) {
        return false;
    }
    if (!ValidateEmail($("#" + appendId + "txtEmail").val())) {
        jAlert('<strong>Please enter valid enterprise email</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtEmail").focus(); });
        return false;
    }
    if (!DropDownValidation(appendId + 'ddlDistrict', '0', 'District', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "ddlDistrict").focus(); });
        return false;
    }
    if (!DropDownValidation(appendId + 'ddlBlock', '0', 'Block', 'GO-SWIFT')) {
        $("#popup_ok").click(function () { $("#" + appendId + "ddlBlock").focus(); });
        return false;
    }
    if ($("#" + appendId + "txtProductCode").val() != "") {
        if ($("#" + appendId + "txtProductCode").val().length != 5) {
            jAlert('<strong>Code(Code may be entered as per NIC 2008) should be of 5 digit</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + "txtProductCode").focus(); });
            return false;
        }
    }
    if (!ValidateGrid(appendId + "grdProducts")) {
        jAlert('<strong>Please enter atleast one or more products</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtItemProduct").focus(); });
        return false
    }
    if (!blankFieldValidation(appendId + 'txtProdCommencement', 'Date of production commencement', 'GO-SWIFT')) {
        return false;
    }


    var txtDateOfProd = new Date($("#" + appendId + "txtDateOfProd").val());
    var dtmProductionComm = new Date($("#" + appendId + "txtProdCommencement").val());
    if (txtDateOfProd != '') {
        if (txtDateOfProd < dtmProductionComm) {
            jAlert('<strong>Date of Production cannot be less than Date of Production Commencement</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#" + appendId + "txtDateOfProd").focus(); });
            return false
        }
    }
    if (!blankFieldValidation('txtDirectEmployement', 'Direct Employment (in Numbers) As on Company Payroll', 'GO-SWIFT')) {
        return false;
    }

    if (parseInt($("#txtDirectEmployement").val()) == 0) {
        jAlert('<strong>Please provide valid value for Direct Employment (in Numbers) As on Company Payroll</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtDirectEmployement").focus(); });
        return false;
    }
    if (!blankFieldValidation('txtContractualEmp', 'Contractual Employment', 'GO-SWIFT')) {
        return false;
    }
    if (parseInt($("#txtContractualEmp").val()) == 0) {
        jAlert('<strong>Please provide valid value for Contractual Employment</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtContractualEmp").focus(); });
        return false;
    }

    var totalRole = $("#txtGrandTotal").val().trim();
    if (totalRole == null || totalRole == undefined || totalRole == '') {
        jAlert('<strong>Total No. of employees by Skill cannot be 0</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtManagarial").focus(); });
        return false;
    }

    if (parseInt(totalRole) == 0) {
        jAlert('<strong>Total No. of employees by Skill cannot be 0</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtManagarial").focus(); });
        return false;
    }

    var totalCat = $("#txtTotal").val();
    if (totalCat == null || totalCat == undefined || totalCat == '') {
        jAlert('<strong>Total No. of employees by Category cannot be 0</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtGeneral").focus(); });
        return false;
    }
    if (parseInt(totalCat) == 0) {
        jAlert('<strong>Total No. of employees by Category cannot be 0</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtGeneral").focus(); });
        return false;
    }

    var totalDirectEmp = parseInt($("#txtDirectEmployement").val());
    var totalContractEmp = parseInt($("#txtContractualEmp").val());

    if ((totalRole != null && totalRole != undefined && totalRole != '' && parseInt(totalRole) != 0) && (totalCat != null && totalCat != undefined && totalCat != '' && parseInt(totalCat) != 0)) {
        if ((totalContractEmp + totalDirectEmp) != parseInt(totalRole)) {
            jAlert('<strong>Sum of Direct and Contractual employees should be equal to total employee by category</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtDirectEmployement").focus(); });

            return false;
        }
        if (!CompareEmployement()) {
            jAlert('<strong>Total employee by skills should be equal to total employee by category</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtManagarial").focus(); });

            return false;
        }
    }

    var womenEmp = $("#txtWomen").val();
    if (womenEmp != null && womenEmp != undefined && womenEmp != '' && parseInt(womenEmp) != 0) {
        if (parseInt(womenEmp) > parseInt(totalRole)) {
            jAlert('<strong>Total no of women employee should be less than total no of employee</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtWomen").focus(); });

            return false;
        }
    }

    var txtPhd = $("#txtPhd").val();
    if (txtPhd != null && txtPhd != undefined && txtPhd != '' && parseInt(txtPhd) != 0) {
        if (parseInt(txtPhd) > parseInt(totalRole)) {
            jAlert('<strong>Total no of differently abled employees should be less than total no of employee</strong>', 'GO-SWIFT');
            $("#popup_ok").click(function () { $("#txtPhd").focus(); });

            return false;
        }
    }
    if (!blankFieldValidation(appendId + 'txtDateFFI', 'Date of FFCI', 'GO-SWIFT')) {
        return false;
    }
    var dtmFfi = new Date($("#" + appendId + "txtDateFFI").val());
    var currDate = new Date();
    if (dtmFfi > currDate) {
        jAlert('<strong>Date of first fixed investment cannot be greater than current date</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtDateFFI").focus(); });
        return false;
    }
    var dtmProductionComm = new Date($("#" + appendId + "txtProdCommencement").val());
    if (dtmFfi > dtmProductionComm) {
        jAlert('<strong>Date of FFCI cannot be greater than date of production commencement</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtDateFFI").focus(); });
    }

    if (!blankFieldValidation(appendId + 'txtDateOfPlant', 'Date of investment of plant and machinery', 'GO-SWIFT')) {
        return false;
    }

    var dtmplant = new Date($("#" + appendId + "txtDateOfPlant").val());
    if (dtmplant > currDate) {
        jAlert('<strong>Date of investment of plant and machinery cannot be greater than current date</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + appendId + "txtDateOfPlant").focus(); });
        return false;
    }

    if (!ValidateGrid(appendId + "gvPlant")) {
        jAlert('<strong>Please enter atleast one or more plant & machinery</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#txtMachinery").focus(); });
        return false
    }
    if (document.getElementById(appendId + "hdnSignature").value == "") {
        jAlert('<strong>Please upload signature for the Inspection Report</strong>', 'GO-SWIFT');
        return false;
    }
    if (confirm("Are you sure you want to submit the details?")) {
        return true;
    }
    else {
        return false;
    }
    return false;
}

//    }

function ValidateGrid(gridId) {
    var arrTr = $("#" + gridId + ">tbody>tr");
    if ($(arrTr).length <= 1) {
        return false;
    }
    else {
        return true;
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
    var totalType = 0, totalCategory = 0;
    var txttype = $("#txtTotal").val();
    var txtGrandTotal = $("#txtGrandTotal").val();
    if (txttype != null && txttype != undefined && txttype != '') {
        totalType = parseInt(txttype);
    }
    if (txtGrandTotal != null && txtGrandTotal != undefined && txtGrandTotal != '') {
        totalCategory = parseInt(txtGrandTotal);
    }
    if (totalCategory != totalType) {
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

function ValidateMachinery() {
    var arrTr = $("#" + appendId + "gvPlant>tbody>tr");
    if ($(arrTr).length <= 1) {
        return false;
    }
    else {
        return true;
    }
}
function ValidateMachineryLnk() {
    var txtMachinery = $("#" + appendId + "txtMachinery");
    var txtDateofPurchase = $("#" + appendId + "txtDateofPurchase");
    var txtAmt = $("#" + appendId + "txtAmt");


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

function alertredirect(msg) {
    jAlert(msg, 'GO-SWIFT', function (r) {

        if (r) {

            location.href = 'ViewIncentiveApplication.aspx';
            return true;
        }
        else {
            return false;
        }
    });
}

function alertredirectno(msg) {
    // alert('hi2');
    hdnAppno = $("#ContentPlaceHolder1_hdnAppNo").val();
    jAlert(msg, 'GO-SWIFT', function (r) {

        if (r) {

            location.href = 'IRForm.aspx?id=' + hdnAppno;
            return true;
        }
        else {
            return false;
        }
    });
}


