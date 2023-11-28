
var appendId = "ContentPlaceHolder1_";
function ValidatePage() {
    debugger;

    if (!DropDownValidation(appendId + 'drpApplicationType', '0', 'Application Type', 'alert')) {
        
        return false;
    }
    var applicationType = $("#" + appendId + "drpApplicationType").val();
    if ((applicationType == "58" || applicationType == "59" || applicationType == "60") && (!ValidateCheckBoxList(appendId + "chkLstChange"))) {
        jAlert('<strong>Please Select atleast one Change in value</strong>', 'alert');
        
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtEin', 'EIN /IEM', 'alert')) {
        
        return false;
    }
    if (!ValidateNumber(appendId + 'txtEin', 'EIN/IEM', 'alert')) {
        
        return false;
    }
    if ($("#" + appendId + "txtEin").val().length != 10) {
        jAlert('<strong>Please enter valid EIN/IEM number</strong>', 'alert');
        
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtUan', 'Udyog Aadhaar Number', 'alert')) {
        
        return false;
    }
    if (!ValidateNumber(appendId + 'txtUan', 'Udyog Aadhaar Number', 'alert')) {
        
        return false;
    }
    if ($("#" + appendId + "txtUan").val().length != 12) {
        jAlert('<strong>Please enter valid  Udyog Aadhaar Number number</strong>', 'alert');
        
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtEnterpriseName', 'Name of Enterprise/Industrial Unit', 'alert')) {
        
        return false;
    }
    if (!DropDownValidation(appendId + 'ddlSector', '0', 'Sector of activity', 'alert')) {
        
        return false;
    }
    if (!DropDownValidation(appendId + 'ddlSubSector', '0', 'Sub sector', 'alert')) {
        
        return false;
    }
    if (!DropDownValidation('drpUnitCategory', '0', 'Unit Category', 'alert')) {
        
        return false;
    }
    if (!DropDownValidation(appendId + 'drpCompanyType', '0', 'Company type', 'alert')) {
        
        return false;
    }
    if (!DropDownValidation(appendId + 'drpOrganizationType', '0', 'Organization type', 'alert')) {
        
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtOwnerName', 'Name of Owner', 'alert')) {
        
        return false;
    }
    if (!DropDownValidation(appendId + 'drpOwnerType', '0', 'Owner type', 'alert')) {
        
        return false;
    }

    if (!blankFieldValidation(appendId + 'txtOfficeAddress', 'Office Address', 'alert')) {
         
        return false;
    }
    if (!DropDownValidation(appendId + 'ddlCode', '0', 'Code for office number', 'alert')) {
        
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtOfficePhone', 'Office Telephone No.', 'alert')) {
         
        return false;
    }
    if (!ValidateNumber(appendId + 'txtOfficePhone', 'Office Telephone No.', 'alert')) {
         
        return false;
    }
    if (!ValidateFieldLength(appendId + 'txtOfficePhone', 10, 'Office Telephone No.', 'alert')) {
         
        return false;
    }
    var officeFax = $("#" + appendId + "txtOfficeFax").val();
    if (officeFax != null && officeFax != undefined && officeFax != '' && officeFax != '0') {
        if (!DropDownValidation(appendId + 'drpFx', '0', 'Code for Fax', 'alert')) {
            
            return false;
        }
        if (officeFax.length != 10) {
            jAlert('<strong>Please enter valid office fax</strong>', 'alert');
             
            return false;
        }
    }
    if (!blankFieldValidation(appendId + 'txtOfficeEmail', 'Email', 'alert')) {
         
        return false;
    }
    if (!ValidateEmail($("#" + appendId + "txtOfficeEmail").val())) {
        jAlert('<strong>Please enter valid office email</strong>', 'alert');
         
        return false;
    }
    if (!DropDownValidation(appendId + 'drpEntCode', '0', 'Code for Enterprise number', 'alert')) {
        
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtEnterpriseAddress', 'Enterprise Address', 'alert')) {
         
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtPhoneNo', 'Telephone No.', 'alert')) {
         
        return false;
    }
    if (!ValidateNumber(appendId + 'txtPhoneNo', 'Telephone No.', 'alert')) {
         
        return false;
    }
    if (!ValidateFieldLength(appendId + 'txtPhoneNo', 10, 'Telephone No.', 'alert')) {
         
        return false;
    }

    var EnterPriseFax = $("#" + appendId + "txtFax").val();
    if (EnterPriseFax != null && EnterPriseFax != undefined && EnterPriseFax != '' && EnterPriseFax != '0') {
        if (!DropDownValidation('drpEnterpriseFax', '0', 'Code for enterprise Fax', 'alert')) {
            
            return false;
        }
        if (EnterPriseFax.length != 10) {
            jAlert('<strong>Please enter valid enterprise fax</strong>', 'alert');
            $("#txtFax").focus();
             
            return false;
        }
    }
    if (!blankFieldValidation(appendId + 'txtEmail', 'Email', 'alert')) {
         
        return false;
    }
    if (!ValidateEmail($("#" + appendId + "txtEmail").val())) {
        jAlert('<strong>Please enter valid enterprise email</strong>', 'alert');
         
        return false;
    }

    if (!blankFieldValidation(appendId + 'txtDateFFI', 'Date of first fixed capital investment', 'alert')) {
       
        return false;
    }
    var dtmFfi = new Date($("#" + appendId + "txtDateFFI").val());
    var currDate = new Date();
    if (dtmFfi > currDate) {
        jAlert('<strong>Date of firstfixed investment cannot be greater than current date</strong>', 'alert');
       
        return false;
    }
    if (!ValidateCheckBoxList(appendId + "chkInvestIn")) {
        jAlert('<strong>Please Select atleast one mode of investment in value</strong>', 'alert');
       
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtModeOfInvestment', 'Mode of investment', 'alert')) {
       
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtland', 'investment in Land including land development', 'alert')) {
       
        return false;
    }

    if (!blankFieldValidation(appendId + 'txtBuilding', 'investment in Building & Civil Construction', 'alert')) {
       
        return false;
    }

    if (!blankFieldValidation(appendId + 'txtPlantMachinery', 'investment in Plant & Machinery', 'alert')) {
       
        return false;
    }

    if (!blankFieldValidation(appendId + 'txtWorkingCapital', 'Working Capital', 'alert')) {
       
        return false;
    }
    if (!blankFieldValidation(appendId + 'txtSelfFinance', 'Self Finance', 'alert')) {
       
        return false;
    }
    if (!CompareWCapitalInvest()) {
        jAlert('<strong>Total investment should be equal to sum of self and borrowed finance.</strong>', 'alert');
        $("#txtland").focus();
       
        return false;
    }
    if (!blankFieldValidation('txtManagarial', 'Count of Managerial staff', 'alert')) {
         
        return false;
    }
    if (!blankFieldValidation('txtSupervisor', 'count of Supervisor staff', 'alert')) {
         
        return false;
    }
    if (!blankFieldValidation('txtSkilled', 'count of skiled staff', 'alert')) {
         
        return false;
    }
    if (!blankFieldValidation('txtUnSKilled', 'count of un skilled staff', 'alert')) {
         
        return false;
    }
    if (!blankFieldValidation('txtSemiSkilled', 'count of semi skilled staff', 'alert')) {
         
        return false;
    }
    if (!blankFieldValidation('txtGeneral', 'count of total General staff', 'alert')) {
         
        return false;
    }
    if (!blankFieldValidation('txtTotalSc', 'count of total SC staff', 'alert')) {
         
        return false;
    }
    if (!blankFieldValidation('txtTotalSt', 'count of total ST staff', 'alert')) {
         
        return false;
    }
    if (!blankFieldValidation('txtWomen', 'count of women from total employement', 'alert')) {
         
        return false;
    }
    if (!blankFieldValidation('txtPhd', 'count of physically handicapped from total employement', 'alert')) {
         
        return false;
    }
    if (!CompareEmployement()) {
        jAlert('<strong>Total employee by skills should be equal to total employee by category</strong>', 'alert');
        $("#txtManagarial").focus();
         
        return false;
    }

    if (!blankFieldValidation(appendId + 'txtProdComm', 'Date of Commencement of Production', 'alert')) {
         
        return false;
    }
    if (!ValidateProducts()) {
        jAlert('<strong>Please enter atleast one or more products</strong>', 'alert');
        $("#txtManagarial").focus();
         
        return false
    }
    var power = $("#" + appendId + "rdBtnLstPower input[type=radio]:checked ").val();
    if (power == 1 && !blankFieldValidation(appendId + 'txtContractDemand', ' Contract Demand (KW)', 'alert')) {
         
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

function CompareWCapitalInvest() {
    debugger;
    var totalCapital = parseFloat($("#" + appendId + "lblTotal").text());
    var totalInvestment = parseFloat($("#" + appendId + "txtSelfFinance").val());
    var borrowed = 0.00;
    var txtValue = $("#" + appendId + "txtBorrowed").val();
    if (txtValue != null && txtValue != undefined && txtValue != '') {
        borrowed = parseFloat($("#" + appendId + "txtBorrowed").val());
    }
    totalInvestment = totalInvestment + borrowed;
    if (totalInvestment != totalCapital) {
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


function pageLoad() {
    $('.menuPc').addClass('active');
    $("#printbtn").click(function () {
        window.print();
    });
    CalculateCatTotal();
    CalculateTotalStaff();
    CalculateTotalCapital();
    $("#txtManagarial, #txtSupervisor, #txtSkilled, #txtSemiSkilled, #txtUnskilled").on('change', CalculateTotalStaff);
    $("#" + appendId + "txtOthers, #" + appendId + "txtPlantMachinery, #" + appendId + "txtBuilding, #" + appendId + "txtland").on('change', CalculateTotalCapital);
    $("#txtTotalSc, #txtTotalSt, #txtGeneral").on('change', CalculateCatTotal);

    $('.datePicker').datepicker({ dateFormat: 'dd:mm:yyyy', separator: ' @ ', minDate:
    new Date(), autoclose: true
    });

}

function ValidateProducts() {
    var arrTr = $("#" + appendId + "grdProducts>tbody>tr");
    if ($(arrTr).length <= 1) {
        return false;
    }
    else {
        return true;
    }
}

function ValidateAdd() {
    var txtProduct = $("#" + appendId + "txtItemProduct");
    var txtItemCode = $("#" + appendId + "txtItemCode");
    var txtQuantity = $("#" + appendId + "txtQuantity");
    var drpUnit = $("#" + appendId + "drpUnitType");
    var txtCost = $("#" + appendId + "txtCost");
    var product = $(txtProduct).val();
    var code = $(txtItemCode).val();
    var quantity = $(txtQuantity).val();
    var Unit = $(drpUnit).val();
    var Cost = $(txtCost).val();
    var filename = $(hdnFileName).val();
    if (product == null || product == undefined || product == '') {
        jAlert('<strong>Please enter product name</strong>', 'alert');
        $(txtProduct).focus();
        return false;
    }
    if (filename == null || filename == undefined || filename == '') {
        jAlert('<strong>Please enter product documents</strong>', 'alert');
        $(txtProduct).focus();
        return false;
    }
    if (code == null || code == undefined || code == '' || code == "0") {
        jAlert('<strong>Please enter valid product code</strong>', 'alert');
        $(txtItemCode).focus();
        return false;
    }

    else if ((Cost == null || Cost == undefined || Cost == '' || Cost == "0")) {
        if (quantity == null || quantity == undefined || quantity == '' || quantity == "0") {
            jAlert('<strong>Please select quantity for the product</strong>', 'alert');
            $(txtCost).focus();
            return false;
        }
        else if (Unit == null || Unit == undefined || Unit == '' || Unit == "0") {
            jAlert('<strong>Please select unit for the product quantity</strong>', 'alert');
            $(txtCost).focus();
            return false;
        }

        else if ((quantity == null || quantity == undefined || quantity == '' || quantity == "0") && (Unit == null || Unit == undefined || Unit == '' || Unit == "0")) {
            jAlert('<strong>Please enter either cost of product or the quantity with unit</strong>', 'alert');
            $(txtCost).focus();
            return false;
        }
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
    var land = 0.0, Plant = 0.0, Building = 0.0, Others = 0.0;
    var intland = 0, intPlant = 0, intBuilding = 0, intOthers = 0;
    land = $("#txtland").val();
    Building = $("#txtBuilding").val();
    Plant = $("#txtPlantMachinery").val();
    Others = $("#txtOthers").val();
    if (land != null && land != undefined && land != '') {
        intland = parseFloat(land);
    }
    if (Plant != null && Plant != undefined && Plant != '') {
        intPlant = parseFloat(Plant);
    }
    if (Building != null && Building != undefined && Building != '') {
        intBuilding = parseFloat(Building);
    }
    if (Others != null && Others != undefined && Others != '') {
        intOthers = parseFloat(Others);
    }

    var total = 0.0;
    total = intPlant + intland + intBuilding + intOthers;
    $("#lblTotal").text(parseFloat(total).toFixed(2));
}


