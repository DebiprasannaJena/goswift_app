//// Created By Sushant Kumar Jena
/// Created on Gouri Shankar Chhotray- Dt 04.12.2017
//Modified by Ritika lath on 29th January 2018

$(document).ready(function () {

    $('.menuincentive').addClass('active');
    $("#printbtn").click(function () {

        window.print();
    });

});

function pageLoad() {
    $('.Pioneersec,.attorneysec,.adhardetails,.availuploadsec,.availdetailsec,.availundertakingsec').hide();
    //    var msgTitle = '<%=System.Configuration.ConfigurationManager.AppSettings["ProjectName"] %>';
    $(function () {
        $('.datePicker').datepicker({
            minDate: new Date(),
            autoclose: true,
            format: "dd-M-yyyy",
            clearBtn: true
        });
    });
    DocCheckList();
    OnChangeApplyBy();
    OnChangeAvailed();

    var hdn = $("#hdnVisibleAcc").val();
    if (hdn != null && hdn != undefined && hdn != '') {
        $("#DivProductionEmploymentDetails, #DivEmpCostSubsidy, #DivInvestDetails, #DivAvailedClaimDetails, #DivBankDetails, #DivAdditionalDocuments").removeClass('in');
        $(hdn).addClass("in");
    }

    $(".panel-title > a").on("click", function () {
        var href = $(this).attr("href");
        $("#hdnVisibleAcc").val(href);
    });


    if ($("#Lbl_Term_Loan_Doc_Name").text() == '') {
        $("#termloan").hide();
    }
    if ($("#Lbl_Direct_Emp_After_Doc_Name").text() == '') {
        $("#after").hide();
    }
    if ($("#Lbl_Direct_Emp_Before_Doc_Name").text() == '') {
        $("#before").hide();
    }
}


function OnChangeApplyBy() {
    //    $('.attorneysec,.adhardetails').hide();
    //    alert($("input[name='radApplyBy']:checked").val());
    if ($("input[name='radApplyBy']:checked").val() == '1') {
        $('.adhardetails').show();
        $('.attorneysec').hide();
        ImgSrc('', $('#ImgSign').attr("id"));
    }
    else if ($("input[name='radApplyBy']:checked").val() == '2') {
        $('.attorneysec').show();
        $('.adhardetails').hide();
        ImgSrc($('#hdnAUTHORIZEDFILE').val(), $('#ImgSign').attr("id"));
    }
}


function OnChangeAvailed() {

    if ($("input[name='RadBtn_Availed_Earlier']:checked").val() == '1') {
        $('.availdetailsec').show();
        $('.availuploadsec').show();
        $('.availundertakingsec').hide();
        ImgSrc('', $('#ImgUndetaking').attr("id"));
        ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#ImgAssistance').attr("id"));
    }
    else if ($("input[name='RadBtn_Availed_Earlier']:checked").val() == '2') {
        $('.availuploadsec').hide();
        $('.availdetailsec').hide();
        $('.availundertakingsec').show();
        ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#ImgUndetaking').attr("id"));
        ImgSrc('', $('#ImgAssistance').attr("id"));
    }
}

function validationProduction() {
    if (!blankFieldValidation("txtProductName", "Product/Service Name", msgTitle)) {
        return false;
    }
    if (!blankFieldValidation("txtQuantity", "Quantity", msgTitle)) {
        return false;
    }
    if (!DropDownValidation("ddlUnit", "0", "Units", msgTitle)) {
        return false;
    }
    if (!blankFieldValidation("txtValue", "Value", msgTitle)) {
        return false;
    }
}

/////////////////// jquery method for Industrial Unit////////////////////////////////////////

function openpopup(flu) {
    var i = flu.id;
    $("#" + i).click();
    return false;
}



function SameAddressIndustry() {
    var cc = $('#TxtAddressInd').val();
    if ($("#ChkSameData").is(':checked')) {
        $('#TxtRegAddress').val(cc);
    }
}
/////////////////// jquery method for Industrial Unit////////////////////////////////////////

function CurrentDateCheck(obj, msg) {

    //var CommDt = $('#TxtESIEPFDate').val();

    var CommDt = $('#' + obj.id).val();
    if (new Date(CommDt) > new Date()) {
        jAlert(msg, msgTitle);
        $('#' + obj.id).val('');
        $('#' + obj.id).focus();
        return false;
    }
}

function calculetotal() {

    var cal = 0;
    if ($("#txtLandtype").val() != '') {
        cal = cal + parseFloat($("#txtLandtype").val());

    }
    if ($("#txtBuilding").val() != '') {
        cal = cal + parseFloat($("#txtBuilding").val());

    }
    if ($("#txtPlantMachinery").val() != '') {
        cal = cal + parseFloat($("#txtPlantMachinery").val());
    }

    if ($("#txtOtherFixedAssests").val() != '') {
        cal = cal + parseFloat($("#txtOtherFixedAssests").val());
    }
    if ($("#TxtBalancEquip").val() != '') {
        cal = cal + parseFloat($("#TxtBalancEquip").val());
    }


    $("#lblTotalAmount").text(cal);

}
function calculetotalOrg() {

    var cal = 0;
    if ($("#txtLandtypeOrg").val() != '') {
        cal = cal + parseFloat($("#txtLandtypeOrg").val());

    }
    if ($("#txtBuildingOrg").val() != '') {
        cal = cal + parseFloat($("#txtBuildingOrg").val());

    }
    if ($("#txtPlantMachineryOrg").val() != '') {
        cal = cal + parseFloat($("#txtPlantMachineryOrg").val());
    }

    if ($("#txtOtherFixedAssestsOrg").val() != '') {
        cal = cal + parseFloat($("#txtOtherFixedAssestsOrg").val());
    }
    if ($("#TxtBalancEquipOrg").val() != '') {
        cal = cal + parseFloat($("#TxtBalancEquipOrg").val());
    }


    $("#lblTotalAmountOrg").text(cal);

}
function DecimalValidation(Ctl) {
    var CtlId = Ctl.id;
    var amount = $("#" + CtlId).val();
    amount = Number(amount).toFixed(2);
    if (isNaN(amount)) {
        amount = Number(0).toFixed(2);
    }
    $("#" + CtlId).val(amount);
}


function ValidFileUpload(CtlId) {
    var ids = CtlId.id;
    if ($("#" + ids).val() == '') {
        jAlert('Please upload a valid document .', msgTitle);
        $("#" + ids).focus();
        return false;
    }
}
function HasFile(fuControlId, strError) {
    if ($('#' + fuControlId).val() == "") {
        jAlert(strError, msgTitle);
        return false;
    }
}

function ValidPrice(ths) {
    var numeric = ths.val();
    if (numeric != "") {
        var regex = /^\d{0,12}(\.\d{1,2})?$/;
        if (!regex.test(numeric)) {
            jAlert('Enter Valid Amount .', msgTitle);
            ths.val("");
            ths.focus();
            ths.select();
            return false;
        }
    }
    else {
        ths.val("0.00");
        ths.select();
    }
}

function validAvailgrid() {

    if (!blankFieldValidation('txtagency', 'Disbursing agency')) {
        return false;
    }
    if (!blankFieldValidation('txtsacamt', 'Sanctioned amount')) {
        return false;
    }
    if (!blankFieldValidation('txtsacord', 'Sanction order no.')) {
        return false;
    }
    if (!blankFieldValidation('txtsacdat', 'Date of sanction')) {
        return false;
    }
    var CommDt = $('#txtsacdat').val(); // end time   Format: '11:00 AM' 
    //how do i compare time
    if (new Date(CommDt) > new Date()) {
        jAlert('<strong>Date of Sanction should not be greater than current date.</strong>', msgTitle);
        $('#txtsacdat').val('');
        $('#txtsacdat').focus();
        return false;
    }
    if (!blankFieldValidation('txtavilamt', 'Availed amount')) {
        return false;
    }

    var sanctionAmt = parseFloat($('#txtsacamt').val());
    var availedAmt = parseFloat($('#txtavilamt').val());
    if (availedAmt > sanctionAmt) {
        jAlert('<strong>Availed amount cannot be greater than sanctioned amount !!</strong>', msgTitle);
        $("#popup_ok").click(function () { $("#txtavilamt").focus(); });
        //$('#txtavilamt').focus();
        //$('#txtavilamt').val('');      
        return false;
    }
}


function validAddDoc() {

    if (!blankFieldValidation('txtFileName', 'File Name')) {
        return false;
    }

    if ($("#LnkOSPCB").text() == '') {
        jAlert('Please upload OSPCB document.', msgTitle);
        return false;
    }
    $('#btnAddmoreTraing').click();

}
function SaveDraft() {
    if ($('#TxtAdhaar1').val() != "") {
        var adhar = $('#TxtAdhaar1').val();
        if (adhar.length < 12) {
            jAlert('Aadhaar no should be 12 digits.', msgTitle);
            return false;
        }
    }

    if ($('#txtAccNo').val().trim() != '') {
        if ($('#txtAccNo').val().length > 16) {
            jAlert('Please enter Account No. within 16 characters .', msgTitle);
            $("#txtAccNo").focus();
            return false;
        }
    }

    if ($('#txtIFSC').val().trim() != '') {
        var IFSCODE = $('#txtIFSC').val();
        if (IFSCODE.length < 7) {
            jAlert('IFSC Code should be 7 digits.', msgTitle);
            return false;
        }
    }
}


function SaveValidation() {

    if (!DropDownValidation('DdlGender', '0', 'Applicant Salutation', msgTitle)) {
        return false;
    }
    if (!blankFieldValidation('TxtApplicantName', 'Applicant Name', msgTitle)) {
        return false;
    }
    if (($("input[name='radApplyBy']:checked").val() != '1') && ($("input[name='radApplyBy']:checked").val() != '2')) {
        jAlert('Please select Application By option', msgTitle);
        return false;
    }
    if ($("input[name='radApplyBy']:checked").val() == '1') {
        if (!blankFieldValidation('TxtAdhaar1', 'Aadhaar card no', msgTitle)) {
            return false;
        };

        var adhar = $('#TxtAdhaar1').val();
        if (adhar.length < 12) {
            jAlert('Aadhaar card no should be 12 digits.', msgTitle);
            return false;
        }
    }
    if ($("input[name='radApplyBy']:checked").val() == '2') {
        if ($("#hdnAUTHORIZEDFILE").val() == '') {
            jAlert('Please upload Authorizing letter .', msgTitle);
            return false;
        }
    }

    ////----------------Employee cost subsidy
    ////            if (!($("#RadSI").is(":checked")) && !($("#RadEPF").is(":checked"))) {
    ////                    jAlert('Please select  radiobutton.', msgTitle);
    ////                    return false;
    ////                
    ////            }
    if (($('#TxtESIRegNo').val() == '') && ($('#TxtEPFRegNo').val() == '')) {
        jAlert('Please enter atleast one of Registration Details, either ESI or EPF.', msgTitle);
        $('#TxtESIRegNo').focus();
        return false;
    }
    else {
        if ($('#TxtESIRegNo').val() != '') {
            if (!blankFieldValidation('TxtESIAuthName', 'Employer ESI Authority Name', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('TxtESIEPFDate', 'Employer ESI Registration Date', msgTitle)) {
                return false;
            }
            if ($("#HdnRegAttachment").val() == '') {
                jAlert('Please upload ESI registration attachment.', msgTitle);
                return false;
            }

        } /////------ ESI Else End
        else if ($('#TxtEPFRegNo').val() != '') {
            if (!blankFieldValidation('TxtEPFAuthName', 'Employer EPF Authority Name', msgTitle)) {
                return false;
            }
            if (!blankFieldValidation('TxtEPFregDate', 'Employer EPF Registration Date', msgTitle)) {
                return false;
            }
            if ($("#HdnEPFRegAttachment").val() == '') {
                jAlert('Please upload EPF registration attachment.', msgTitle);
                return false;
            }
        } /////----- EPF End
    }



    ////            if (!blankFieldValidation('TxtESIRegNo', 'Employer Registration No', msgTitle)) {
    ////                return false;
    ////            }
    ////            if (!blankFieldValidation('TxtESIEPFDate', 'Employer ESI Registration Date', msgTitle)) {
    ////                return false;
    ////            }

    var CommDt = $('#TxtESIEPFDate').val(); // end time   Format: '11:00 AM' 
    //how do i compare time
    if (new Date(CommDt) > new Date()) {
        jAlert('Employer ESI Registration Date should not be greater than current Date.', msgTitle);
        $('#TxtESIEPFDate').val('');
        $('#TxtESIEPFDate').focus();
        return false;
    }

    if ($("#hdnPayrollDoc").val() == '') {
        jAlert('Please upload Company Payroll document.', msgTitle);
        return false;
    }
    if ($("#HdnContESI").val() == '') {
        jAlert('Please upload Employer Contribution document.', msgTitle);
        return false;
    }
    if ($("#hdnESIComp").val() == '') {
        jAlert('Please upload Company Contribution document.', msgTitle);
        return false;
    }
    //////            if (!blankFieldValidation('TxtReasonDelay', 'Delay Reason', msgTitle)) {
    //////                return false;
    //////            }
    //////            if ($("#HdnDelayDoc").val() == '') {
    //////                jAlert('Please upload Delay Reason document.', msgTitle);
    //////                return false;
    //////            }


    ////----------------avail details

    //            if ($("#Hid_Asst_Sanc_File_Name").val() == '') {
    //                jAlert('Please upload assistance applied for Sanctioned order document.', msgTitle);
    //                return false;
    //            }


    if (($("input[name='RadBtn_Availed_Earlier']:checked").val() != '1') && ($("input[name='RadBtn_Availed_Earlier']:checked").val() != '2')) {
        jAlert('Please select whether Subsidy/Incentive availed earlier!', msgTitle);
        return false;
    }
    if ($("input[name='RadBtn_Availed_Earlier']:checked").val() == '1') {

        if (!blankFieldValidation('txtdiffclaimamt', 'Differential amount of claim', msgTitle)) {
            return false;
        }

        ////                if ($('#txtdiffclaimamt').val() == 0) {
        ////                    jAlert('Differential amount of claim should be greater than zero .', msgTitle);
        ////                    return false;
        ////                }
        if ($("#Hid_Asst_Sanc_File_Name").val() == '') {
            jAlert('Please upload assistance applied for Sanctioned order document.', msgTitle);
            return false;
        }

        if ($("#grdAssistanceDetailsAD tr").length > 0) {
        }
        else {
            jAlert('<strong>Please atleast one row to availed detail data</strong>', 'Incentives');
            return false;
        }
    }
    if ($("input[name='RadBtn_Availed_Earlier']:checked").val() == '2') {
        if ($("#Hid_Undertaking_File_Name").val() == '') {
            jAlert('Please upload Undertaking on non-availment of subsidy document .', msgTitle);
            return false;
        }
    }
    if (!blankFieldValidation('txtreimamt', 'Present Claim for reimbursement of ESI', msgTitle)) {
        return false;
    }

    ////            if ($('#txtreimamt').val() == 0) {
    ////                jAlert('Present Claim for reimbursement should be greater than zero .', msgTitle);
    ////                return false;
    ////            }
    if (!blankFieldValidation('txtreimamtEPF', 'Present Claim for reimbursement of EPF', msgTitle)) {
        return false;
    }



    ////////////////////////bank details-------------
    if (!blankFieldValidation('txtAccNo', 'Account No of Industrial Unit', msgTitle)) {
        return false;
    }

    if ($('#txtAccNo').val().trim() != '') {
        if ($('#txtAccNo').val().length > 16) {
            jAlert('Please enter Account No. within 16 characters .', msgTitle);
            $("#txtAccNo").focus();
            return false;
        }
    }
    if (!blankFieldValidation('txtBnkNm', 'Bank Name', msgTitle)) {
        return false;
    }
    if (!blankFieldValidation('txtBranch', 'Branch Name', msgTitle)) {
        return false;
    }
    if (!blankFieldValidation('txtIFSC', 'IFSC Code', msgTitle)) {
        return false;
    }
    var IFSCODE = $('#txtIFSC').val();
    if (IFSCODE.length < 7) {
        jAlert('IFSC Code should be 7 digits.', msgTitle);
        return false;
    }
    //////            if (!blankFieldValidation('txtMICRNo', 'MICR No.', msgTitle)) {
    //////                return false;
    //////            }
    if ($("#hdnBank").val() == '') {
        jAlert('Please Upload cancelled cheuque document to verify account details .', msgTitle);
        return false;
    }
    ////            if ($("#D275").val() == '') {
    ////                jAlert('Please Upload OSPCB consent to operate related document .', msgTitle);
    ////                return false;
    ////            }
    ////            if ($("#D274").val() == '') {
    ////                jAlert('Please Upload Sector Relevant Document.', msgTitle);
    ////                return false;
    ////            }
    ////            if ($("#D280").val() == '') {
    ////                jAlert('Please Upload Factory & Boiler - For all industry related Document.', msgTitle);
    ////                return false;
    ////            }

}


function ImgSrc(hdnfld, Img) {
    if (hdnfld != "") {
        $('#' + Img).attr('src', '../images/incapproved.png');
    }
    else {
        $('#' + Img).attr('src', '../images/cancel-square.png');
    }
}

function DocCheckList() {
    ImgSrc($('#hdnAUTHORIZEDFILE').val(), $('#ImgSign').attr("id"));
    ImgSrc($('#HdnRegAttachment').val(), $('#ImgESI').attr("id"));
    ImgSrc($('#HdnEPFRegAttachment').val(), $('#ImgEPF').attr("id"));
    ImgSrc($('#hdnPayrollDoc').val(), $('#ImgDocCompPayRoll').attr("id"));
    ImgSrc($('#HdnContESI').val(), $('#ImgEmpCont').attr("id"));
    ImgSrc($('#hdnESIComp').val(), $('#ImgCompCont').attr("id"));
    ImgSrc($('#Hid_Asst_Sanc_File_Name').val(), $('#ImgAssistance').attr("id"));
    ImgSrc($('#Hid_Undertaking_File_Name').val(), $('#ImgUndetaking').attr("id"));
    ImgSrc($('#hdnBank').val(), $('#ImgCancelCheque').attr("id"));
    ImgSrc($('#D275').val(), $('#ImgOSPCB').attr("id"));
    ImgSrc($('#D274').val(), $('#ImgSectRel').attr("id"));
    ImgSrc($('#D280').val(), $('#ImgCleanApproveAuthority').attr("id"));

}
        






