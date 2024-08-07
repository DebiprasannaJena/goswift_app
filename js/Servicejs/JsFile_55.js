﻿//----------------Begin Jquery Function-------------------------
$(document).ready(function () {

    /*-------------------------------------------------------------------*/
    ///// Added by Sushant during v2.0 Implementation
    /*-------------------------------------------------------------------*/
    $("#btnDraft").attr("disabled", true);

    $('#chk_1').click(function () {
        if (document.getElementById('chk_1').checked) {
            $("#btnDraft").attr("disabled", false);
        }
        else {
            $("#btnDraft").attr("disabled", true);
        }
    });
    /*-------------------------------------------------------------------*/


    /////------------------SubmitButton Active----------------
    $("#btnSubmit").attr("disabled", true); //-----for disble button

    $('#chk_1').click(function () {
        if (document.getElementById('chk_1').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });

    $("#div_txt_211").hide();
    $("#div_txt_224").hide();

    $('input:radio[name=CriminalProc]').change(function () {

        if ($('input:radio[name=CriminalProc]:checked').val() === "Yes") {
            $("#div_txt_211").show();
            $('#txt_21').val("");
            $("#txt_21").addClass("req rset");
        }
        else {
            $("#div_txt_211").hide();
            $('#txt_21').val("");
            $("#txt_21").removeClass("req rset");
        }
    });

    $('input:radio[name=ParticularOtherLicences]').change(function () {

        if ($('input:radio[name=ParticularOtherLicences]:checked').val() === "Yes") {
            $("#div_txt_224").show();
            $('#txt_22').val("");
            $("#txt_22").addClass("req rset");
        }
        else {
            $("#div_txt_224").hide();
            $('#txt_22').val("");
            $("#txt_22").removeClass("req rset");
        }
    });


    BindPermisesDistrict();
    BindDistrict();
    FillProcessingDistrict();
    AmountDetails(500);
});

/*------------------------------------------------------------------------------------------------------------*/

function BindDistrict() {

    var selValue = 20;
    var query1 = "select intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District  where intStateId=" + parseInt(selValue) + " order by vchDistrictName asc";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_2').html('');
            $('#sel_2').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_2').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*------------------------------------------------------------------------------------------------------------*/

function BindPermisesDistrict() {

    var selValue = 20;
    var query1 = "select intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District  where intStateId=" + parseInt(selValue) + " order by vchDistrictName asc";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_4').html('');
            $('#sel_4').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*------------------------------------------------------------------------------------------------------------*/

function FillProcessingDistrict() {
    var query1 = "select intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District where  intStateId=20  order by vchDistrictName asc";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_5').html('');
            $('#sel_5').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_5').append($("<option ></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*------------------------------------------------------------------------------------------------------------*/

function AmountDetails(amount) {

    var strText = "";
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>";
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(amount);
    $('#hdnApplicationFee').val(0);
}

/*------------------------------------------------------------------------------------------------------------*/