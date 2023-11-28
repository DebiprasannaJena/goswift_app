//// Created By Sushant Kumar Jena
/// Edited By Ritika Lath on 23rd January 2017 for adding focus to controls when validated
/*------------------------------------------------------------------------------------------------------------------------*/
/////// Total Employee Calculation (Before)

function funCalTotalEmp_Before() {
    var mngr = 0;
    var sup = 0;
    var skilled = 0;
    var semiskilled = 0;
    var unskilled = 0;

    var gen = 0;
    var sc = 0;
    var st = 0;

    if ($('#Txt_Managarial_Before').val() != '') {
        mngr = $('#Txt_Managarial_Before').val();
    }
    if ($('#Txt_Supervisor_Before').val() != '') {
        sup = $('#Txt_Supervisor_Before').val();
    }
    if ($('#Txt_Skilled_Before').val() != '') {
        skilled = $('#Txt_Skilled_Before').val();
    }
    if ($('#Txt_Semi_Skilled_Before').val() != '') {
        semiskilled = $('#Txt_Semi_Skilled_Before').val();
    }
    if ($('#Txt_Unskilled_Before').val() != '') {
        unskilled = $('#Txt_Unskilled_Before').val();
    }

    $('#Txt_Total_Emp_Before').val(parseInt(mngr) + parseInt(sup) + parseInt(skilled) + parseInt(semiskilled) + parseInt(unskilled));

    if ($('#Txt_General_Before').val() != '') {
        gen = $('#Txt_General_Before').val();
    }
    if ($('#Txt_SC_Before').val() != '') {
        sc = $('#Txt_SC_Before').val();
    }
    if ($('#Txt_ST_Before').val() != '') {
        st = $('#Txt_ST_Before').val();
    }

    $('#Txt_Total_Cast_Emp_Before').val(parseInt(sc) + parseInt(st) + parseInt(gen));
}

/*------------------------------------------------------------------------------------------------------------------------*/
/////// Total Employee Calculation (After)

function funCalTotalEmp_After() {
    var mngr = 0;
    var sup = 0;
    var skilled = 0;
    var semiskilled = 0;
    var unskilled = 0;

    var gen = 0;
    var sc = 0;
    var st = 0;

    if ($('#Txt_Managarial_After').val() != '') {
        mngr = $('#Txt_Managarial_After').val();
    }
    if ($('#Txt_Supervisor_After').val() != '') {
        sup = $('#Txt_Supervisor_After').val();
    }
    if ($('#Txt_Skilled_After').val() != '') {
        skilled = $('#Txt_Skilled_After').val();
    }
    if ($('#Txt_Semi_Skilled_After').val() != '') {
        semiskilled = $('#Txt_Semi_Skilled_After').val();
    }
    if ($('#Txt_Unskilled_After').val() != '') {
        unskilled = $('#Txt_Unskilled_After').val();
    }

    $('#Txt_Total_Emp_After').val(parseInt(mngr) + parseInt(sup) + parseInt(skilled) + parseInt(semiskilled) + parseInt(unskilled));

    if ($('#Txt_General_After').val() != '') {
        gen = $('#Txt_General_After').val();
    }
    if ($('#Txt_SC_After').val() != '') {
        sc = $('#Txt_SC_After').val();
    }
    if ($('#Txt_ST_After').val() != '') {
        st = $('#Txt_ST_After').val();
    }

    $('#Txt_Total_Cast_Emp_After').val(parseInt(sc) + parseInt(st) + parseInt(gen));
}

/*------------------------------------------------------------------------------------------------------------------------*/
////// Total Investment Amount Calculation (Before)

function funCalTotalInvestAmtBefore() {

    var land_amt = 0;
    var building_amt = 0;
    var plant_mach_amt = 0;
    var other_amt = 0;

    if ($('#Txt_Land_Before').val() != '') {
        land_amt = $('#Txt_Land_Before').val();
    }
    if ($('#Txt_Building_Before').val() != '') {
        building_amt = $('#Txt_Building_Before').val();
    }
    if ($('#Txt_Plant_Mach_Before').val() != '') {
        plant_mach_amt = $('#Txt_Plant_Mach_Before').val();
    }
    if ($('#Txt_Other_Fixed_Asset_Before').val() != '') {
        other_amt = $('#Txt_Other_Fixed_Asset_Before').val();
    }

    $('#Txt_Total_Capital_Before').val(parseFloat(land_amt) + parseFloat(building_amt) + parseFloat(plant_mach_amt) + parseFloat(other_amt));
}


/*------------------------------------------------------------------------------------------------------------------------*/
////// Total Investment Amount Calculation (After)

function funCalTotalInvestAmtAfter() {

    var land_amt = 0;
    var building_amt = 0;
    var plant_mach_amt = 0;
    var other_amt = 0;

    if ($('#Txt_Land_After').val() != '') {
        land_amt = $('#Txt_Land_After').val();
    }
    if ($('#Txt_Building_After').val() != '') {
        building_amt = $('#Txt_Building_After').val();
    }
    if ($('#Txt_Plant_Mach_After').val() != '') {
        plant_mach_amt = $('#Txt_Plant_Mach_After').val();
    }
    if ($('#Txt_Other_Fixed_Asset_After').val() != '') {
        other_amt = $('#Txt_Other_Fixed_Asset_After').val();
    }

    $('#Txt_Total_Capital_After').val(parseFloat(land_amt) + parseFloat(building_amt) + parseFloat(plant_mach_amt) + parseFloat(other_amt));
}

/*------------------------------------------------------------------------------------------------------------------------*/
///// Item of Manufacture Add More (Before)

function validateItemAddBefore() {
    debugger;
    if (!blankFieldValidation('Txt_Product_Name_Before', 'Product Name', projname)) {
        return false;
    }
    if (!WhiteSpaceValidation1st('Txt_Product_Name_Before', 'Product Name', projname)) {
        $("#popup_ok").click(function () { $("#Txt_Product_Name_Before").focus(); });
        return false;
    }
    if (!blankFieldValidation('Txt_Quantity_Before', 'Quantity', projname)) {
        return false;
    }

    if (!DropDownValidation('DrpDwn_Unit_Before', '0', 'Unit', projname)) {
        $("#popup_ok").click(function () { $("#DrpDwn_Unit_Before").focus(); });
        return false;
    }
    var unitB = $('#DrpDwn_Unit_Before').val();
    if (unitB == '52') {
        if (!blankFieldValidation('Txt_Other_Unit_Before', 'Other Unit', projname)) {
            return false;
        }
    }
    if (!blankFieldValidation('Txt_Value_Before', 'Value', projname)) {
        return false;
    }
}

/*------------------------------------------------------------------------------------------------------------------------*/
///// Item of Manufacture Add More (After)

function validateItemAddAfter() {
    if (!blankFieldValidation('Txt_Product_Name_After', 'Product Name', projname)) {
        return false;
    }
    if (!WhiteSpaceValidation1st('Txt_Product_Name_After', 'Product Name', projname)) {
        $("#popup_ok").click(function () { $("#Txt_Product_Name_After").focus(); });
        return false;
    }
    if (!blankFieldValidation('Txt_Quantity_After', 'Quantity', projname)) {
        return false;
    }
    if (!DropDownValidation('DrpDwn_Unit_After', '0', 'Unit', projname)) {
        $("#popup_ok").click(function () { $("#DrpDwn_Unit_After").focus(); });
        return false;
    }
    var cc = $('#DrpDwn_Unit_After').val();
    if (cc == '52') {
        if (!blankFieldValidation('Txt_Other_Unit_After', 'Other Unit', projname)) {
            return false;
        }
    }
    if (!blankFieldValidation('Txt_Value_After', 'Value', projname)) {
        return false;
    }
}


///*------------------------------------------------------------------------------------------------------------------------*/
///////// Term Loan (Add More)

function validateTermLoan() {
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
    if (new Date($('#Txt_TL_Availed_Date').val()) > new Date()) {
        jAlert('<strong>Avail Date should not be greater than Current Date.</strong>', projname);
        $("#popup_ok").click(function () { $("#Txt_TL_Availed_Date").focus(); });
        return false;
    }
    if (new Date($('#Txt_TL_Availed_Date').val()) < new Date($('#Txt_TL_Sanction_Date').val())) {
        jAlert('<strong>Availed Date cannot be less than sanction date.</strong>', projname);
        $("#popup_ok").click(function () { $("#Txt_TL_Availed_Date").focus(); });
        return false;
    }
}

///*------------------------------------------------------------------------------------------------------------------------*/
//////// Working Capital Loan (Add More)

function validateWCLoan() {
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
    if (new Date($('#Txt_WC_Availed_Date').val()) > new Date()) {
        jAlert('<strong>Avail Date should not be greater than Current Date.</strong>', projname);
        $("#popup_ok").click(function () { $("#Txt_WC_Availed_Date").focus(); });
        return false;
    }
    if (new Date($('#Txt_WC_Availed_Date').val()) < new Date($('#Txt_WC_Sanction_Date').val())) {
        jAlert('<strong>Availed Date cannot be less than sanction date.</strong>', projname);
        $("#popup_ok").click(function () { $("#Txt_WC_Availed_Date").focus(); });
        return false;
    }
}

///*------------------------------------------------------------------------------------------------------------------------*/
/////// Page Validation

function validateBasicDetails() {

    if (!blankFieldValidation('Txt_EnterPrise_Name', 'EnterPrise/Industrial Unit', projname)) {
        return false;
    }
    if (!DropDownValidation('DrpDwn_Org_Type', '0', 'Organization Type', projname)) {
        $("#popup_ok").click(function () { $("#DrpDwn_Org_Type").focus(); });
        return false;
    }
    if (!blankFieldValidation('Txt_Industry_Address', 'Industry Address', projname)) {
        return false;
    }
    if (!WhiteSpaceValidation1st('Txt_Industry_Address', 'Industry Address', projname)) {
        $("#popup_ok").click(function () { $("#Txt_Industry_Address").focus(); });
        return false;
    }

    var indAddLength = $('#Txt_Industry_Address').val().length;
    if (indAddLength > 500) {
        jAlert('<strong>Industry Address Should be Maximum 500 Characters  !!</strong>', projname);
        $("#popup_ok").click(function () { $("#Txt_Industry_Address").focus(); });
        return false;
    }

    if (!DropDownValidation('DrpDwn_Unit_Cat', '0', 'Unit Category', projname)) {
        $("#popup_ok").click(function () { $("#DrpDwn_Unit_Cat").focus(); });
        return false;
    }
    if (!DropDownValidation('DrpDwn_Unit_Type', '0', 'Unit Type', projname)) {
        $("#popup_ok").click(function () { $("#DrpDwn_Unit_Type").focus(); });
        return false;
    }
    var unitTypeVal = $('#DrpDwn_Unit_Type').val();
    if (unitTypeVal == '61' || unitTypeVal == '62' || unitTypeVal == '63') {
        if ($('#Hid_Unit_Type_File_Name').val() == '') {
            var unitTypeDocName = $('#Lbl_Unit_Type_Doc_Name').text();
            jAlert('<strong>Please Upload ' + unitTypeDocName + ' !!</strong>', projname);
            $("#popup_ok").click(function () { $("#FU_Unit_Type").focus(); });
            return false;
        }
    }
    if (!blankFieldValidation('Txt_Regd_Office_Address', 'Registered Office Address', projname)) {
        return false;
    }
    if (!WhiteSpaceValidation1st('Txt_Regd_Office_Address', 'Registered Office Address', projname)) {
        $("#popup_ok").click(function () { $("#Txt_Regd_Office_Address").focus(); });
        return false;
    }
    var offAddLength = $('#Txt_Regd_Office_Address').val().length;
    if (offAddLength > 500) {
        jAlert('<strong>Registered Office Address Should be Maximum 500 Characters  !!</strong>', projname);
        $("#popup_ok").click(function () { $("#Txt_Regd_Office_Address").focus(); });
        return false;
    }

    var orgName = $('#Lbl_Org_Name_Type').text();
    if (!blankFieldValidation('Txt_Partner_Name', orgName, projname)) {
        return false;
    }
    if (!WhiteSpaceValidation1st('Txt_Partner_Name', orgName, projname)) {
        $("#popup_ok").click(function () { $("#Txt_Partner_Name").focus(); });
        return false;
    }

    if ($('#Hid_Org_Doc_File_Name').val() == '') {
        var orgTypeDocName = $('#Lbl_Org_Doc_Type').text();
        jAlert('<strong>Please Upload ' + orgTypeDocName + ' !!</strong>', projname);
        $("#popup_ok").click(function () { $("#FU_Org_Doc").focus(); });
        return false;
    }

    if (!WhiteSpaceValidation1st('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname)) {
        return false;
    }

    if ($('#Txt_EIN_IL_NO').val() != '') {

        if (!blankFieldValidation('Txt_EIN_IL_Date', 'EIN/ IEM/ IL Date', projname)) {
            return false;
        }
    }

    var EINDt = $('#Txt_EIN_IL_Date').val()
    if (EINDt != '') {

        if (!blankFieldValidation('Txt_EIN_IL_NO', 'EIN/ IEM/ IL No.', projname)) {
            return false;
        }
        //if ($('#Txt_EIN_IL_NO').val().length != 10) {
        //    jAlert('<strong>EIN/ IEM/ IL Number Should be 10 Digits.</strong>', projname);
        //    $("#popup_ok").click(function () { $("#Txt_EIN_IL_NO").focus(); });
        //    return false;
        //}
        if (new Date(EINDt) > new Date()) {
            jAlert('<strong>EIN/ IEM/ IL Date should not be greater than Current Date.</strong>', projname);
            $("#popup_ok").click(function () { $("#Txt_EIN_IL_Date").focus(); });
            return false;
        }
    }

    if ($('#Hid_PC_Status').val() == 'Y') {

        if ($('#Hid_Is_Exist_Before').val() == 'Y') {
            if (!blankFieldValidation('Txt_PC_No_Before', 'PC No.', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Prod_Comm_Date_Before', 'Date of Production Commencement', projname)) {
                return false;
            }
            if (new Date($('#Txt_Prod_Comm_Date_Before').val()) > new Date()) {
                jAlert('<strong>Date of Production Commencement should not be greater than Current Date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Prod_Comm_Date_Before").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_PC_Issue_Date_Before', 'PC Issuance Date', projname)) {
                return false;
            }
            if (new Date($('#Txt_PC_Issue_Date_Before').val()) > new Date()) {
                jAlert('<strong>PC Issuance Date should not be greater than Current Date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_PC_Issue_Date_Before").focus(); });
                return false;
            }
        }
        if ($('#Hid_Is_Exist_After').val() == 'Y') {
            if (!blankFieldValidation('Txt_PC_No_After', 'PC No.', projname)) {
                return false;
            }
            if (!blankFieldValidation('Txt_Prod_Comm_Date_After', 'Date of Production Commencement', projname)) {
                return false;
            }
            if (new Date($('#Txt_Prod_Comm_Date_After').val()) > new Date()) {
                jAlert('<strong>Date of Production Commencement should not be greater than Current Date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_Prod_Comm_Date_After").focus(); });
                return false;
            }
            if (!blankFieldValidation('Txt_PC_Issue_Date_After', 'PC Issuance Date', projname)) {
                return false;
            }
            if (new Date($('#Txt_PC_Issue_Date_After').val()) > new Date()) {
                jAlert('<strong>PC Issuance Date should not be greater than Current Date.</strong>', projname);
                $("#popup_ok").click(function () { $("#Txt_PC_Issue_Date_After").focus(); });
                return false;
            }
        }
    }

    if (!DropDownValidation('DrpDwn_District', '0', 'District', projname)) {
        return false;
    }
    if (!DropDownValidation('DrpDwn_Sector', '0', 'Sector', projname)) {
        return false;
    }
    if (!DropDownValidation('DrpDwn_Sub_Sector', '0', 'Sub Sector', projname)) {
        return false;
    }

    if (($("input[name='Rad_Nature_Of_Activity']:checked").val() != '40') && ($("input[name='Rad_Nature_Of_Activity']:checked").val() != '41')) {
        jAlert('<strong>Please Select Nature of Activity !!</strong>', projname);
        return false;
    }
    if (($("input[name='Rad_Priority_User']:checked").val() != '1') && ($("input[name='Rad_Priority_User']:checked").val() != '2')) {
        jAlert('<strong>Please Select Whether in Priority IPR-2015 !!</strong>', projname);
        return false;
    }

    if (($("input[name='Rad_Is_Priority']:checked").val() != '1') && ($("input[name='Rad_Is_Priority']:checked").val() != '2') && ($("input[name='Rad_Is_Priority']:checked").val() != '3')) {
        jAlert('<strong>Please Select Priority Sector Status Granted !!</strong>', projname);
        return false;
    }

    if ($("input[name='Rad_Is_Priority']:checked").val() == '1') {

        if (($("input[name='Rad_Is_Pioneer']:checked").val() != '1') && ($("input[name='Rad_Is_Pioneer']:checked").val() != '2')) {
            jAlert('<strong>Please Select Pioneer Option !!</strong>', projname);
            return false;
        }
        if ($("input[name='Rad_Is_Pioneer']:checked").val() == '1') {
            if ($('#Hid_Pioneer_Doc_File_Name').val() == '') {
                jAlert('<strong>Please Upload Document in Support of Pioneer Unit !!</strong>', projname);
                $("#popup_ok").click(function () { $("#FU_Pioneer_Doc").focus(); });
                return false;
            }
        }
    }

    if ($('#Txt_GSTIN').val() != '') {
        if ($('#Txt_GSTIN').val().length != 15) {
            jAlert('<strong>GSTIN Should be 15 Digits !!</strong>', projname);
            $("#popup_ok").click(function () { $("#Txt_GSTIN").focus(); });
            return false;
        }
    }

    if (($("input[name='Rad_Is_Ancillary']:checked").val() != '1') && ($("input[name='Rad_Is_Ancillary']:checked").val() != '2')) {
        jAlert('<strong>Please Choose Is Ancillary/DownStream !!</strong>', projname);
        return false;
    }

    if ($("input[name='Rad_Is_Ancillary']:checked").val() == '1') {
        if ($('#Hid_Ancillary_Doc_File_Name').val() == '') {
            jAlert('<strong>Please Upload Document in Support of Ancillary/DownStream !!</strong>', projname);
            $("#popup_ok").click(function () { $("#FU_Ancillary_Doc").focus(); });
            return false;
        }
    }

    if ($('#Hid_Is_Exist_Before').val() == 'Y') {

        if ($("#Grd_Production_Before tr").length > 0) {
        }
        else {
            jAlert('<strong>Please Insert Atleast One Record for Items of Manufacture/Activity !!</strong>', projname);
            return false;
        }
        if (!blankFieldValidation('Txt_Direct_Emp_Before', 'Direct Empolyment in Numbers', projname)) {
            return false;
        }
        if (!blankFieldValidation('Txt_Contract_Emp_Before', 'Contractual Empolyment in Numbers', projname)) {
            return false;
        }
        //        if ($('#Hid_Direct_Emp_Before_File_Name').val() == '') {
        //            jAlert('<strong>Please Upload Document in Support of Number of Employes shown as directly employed !!</strong>', projname);
        //            $('#FU_Direct_Emp_Before').focus();
        //            return false;
        //        }

        if (!blankFieldValidation('Txt_Managarial_Before', 'Managerial Employee', projname)) {
            return false;
        }
        if (!blankFieldValidation('Txt_Supervisor_Before', 'Supervisor Employee', projname)) {
            return false;
        }
        if (!blankFieldValidation('Txt_Skilled_Before', 'Skilled Employee', projname)) {
            return false;
        }
        if (!blankFieldValidation('Txt_Semi_Skilled_Before', 'Semi Skilled Employee', projname)) {
            return false;
        }
        if (!blankFieldValidation('Txt_Unskilled_Before', 'Un Skilled Employee', projname)) {
            return false;
        }
        if (!blankFieldValidation('Txt_General_Before', 'General Employee', projname)) {
            return false;
        }
        if (!blankFieldValidation('Txt_SC_Before', 'SC Employee', projname)) {
            return false;
        }
        if (!blankFieldValidation('Txt_ST_Before', 'ST Employee', projname)) {
            return false;
        }
        if (!blankFieldValidation('Txt_Women_Before', 'Women Employee', projname)) {
            return false;
        }
        if (!blankFieldValidation('Txt_PHD_Before', 'Differently Abled Persons Employee', projname)) {
            return false;
        }

        var direct_emp_before = $('#Txt_Direct_Emp_Before').val();
        var contract_emp_before = $('#Txt_Contract_Emp_Before').val();

        var mngr_before = $('#Txt_Managarial_Before').val();
        var sup_before = $('#Txt_Supervisor_Before').val();
        var skilled_before = $('#Txt_Skilled_Before').val();
        var semiskilled_before = $('#Txt_Semi_Skilled_Before').val();
        var unskilled_before = $('#Txt_Unskilled_Before').val();
        var gen_before = $('#Txt_General_Before').val();
        var sc_before = $('#Txt_SC_Before').val();
        var st_before = $('#Txt_ST_Before').val();

        var women_before = $('#Txt_Women_Before').val();
        var phd_before = $('#Txt_PHD_Before').val();

        var totalDirContBefore = parseInt(direct_emp_before) + parseInt(contract_emp_before);
        var totalEmpBefore = parseInt(mngr_before) + parseInt(sup_before) + parseInt(skilled_before) + parseInt(semiskilled_before) + parseInt(unskilled_before);
        var totalCastEmpBefore = parseInt(sc_before) + parseInt(st_before) + parseInt(gen_before);

        if (totalDirContBefore != totalEmpBefore) {
            jAlert('<strong>Total Employees and Sum of Direct and Contractual Employees must be Same !!</strong>', projname);
            $("#popup_ok").click(function () { $("#Txt_Direct_Emp_Before").focus(); });
            return false;
        }
        if (totalEmpBefore != totalCastEmpBefore) {
            jAlert('<strong>Total Employees and Sum of General,SC and ST Employees must be Same !!</strong>', projname);
            return false;
        }
        if (women_before > totalEmpBefore) {
            jAlert('<strong>Total women employees must be less than or equal to total employees !!</strong>', projname);
            $("#popup_ok").click(function () { $("#Txt_Women_Before").focus(); });
            return false;
        }
        if (phd_before > totalEmpBefore) {
            jAlert('<strong>Total differently abled persons employees must be less than or equal to total employees !!</strong>', projname);
            $("#popup_ok").click(function () { $("#Txt_PHD_Before").focus(); });
            return false;
        }
    }

    if ($("#Grd_Production_After tr").length > 0) {
    }
    else {
        jAlert('<strong>Please Insert Atleast One Record for Items of Manufacture/Activity !!</strong>', projname);
        return false;
    }
    if (!blankFieldValidation('Txt_Direct_Emp_After', 'Direct Empolyment in Numbers', projname)) {
        return false;
    }
    if (!blankFieldValidation('Txt_Contract_Emp_After', 'Contractual Empolyment in Numbers', projname)) {
        return false;
    }

    //    if ($('#Hid_Direct_Emp_After_File_Name').val() == '') {
    //        jAlert('<strong>Please upload document in support of number of employes shown as directly employed !!</strong>', projname);
    //        $('#FU_Direct_Emp_After').focus();
    //        return false;
    //    }
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

///*------------------------------------------------------------------------------------------------------------------------*/