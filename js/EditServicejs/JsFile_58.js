//----------------Begin Jquery Function-------------------------
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

    $('input:radio[name=LicenceCancelled]').change(function () {

        if ($('input:radio[name=LicenceCancelled]:checked').val() === "Yes") {
            $("#div_txt_185").show();
            $('#txt_18').val("");
            $("#txt_18").addClass("req rset");
        }
        else {
            $("#div_txt_185").hide();
            $('#txt_18').val("");
            $("#txt_18").removeClass("req rset");
        }
    });

    var LicenceCancelled = $('input:radio[name=LicenceCancelled]:checked').val();

    if (LicenceCancelled === "Yes") {
        $("#div_txt_185").show();
        $("#txt_18").addClass("req rset");
    }
    else {
        $("#div_txt_185").hide();
        $('#txt_18').val("");
        $("#txt_18").removeClass("req rset");
    }

    if ($('#sel_2').val() !== 'undefined' && $('#sel_2').val() !== "" && $('#sel_2').val() !== null) {
        AutoBindApplicantDistrict();
    }
    else {
        BindApplicantDistrict();
    }

    if ($('#sel_3').val() !== 'undefined' && $('#sel_3').val() !== "" && $('#sel_3').val() !== null) {
        AutoBindPremisesDistrict();
    }
    else {
        BindPremisesDistrict();
    }

    AmountDetails(300);

});

/*------------------------------------------------------------------------------------------------------------*/

function BindApplicantDistrict() {
    var query1 = "SELECT intDistrictId AS COLUMN_NAME_VALUE , vchDistrictName AS COLUMN_NAME_TEXT FROM M_District WHERE intStateId=20 ORDER BY vchDistrictName asc";
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
                $('#sel_2').append($("<option ></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*------------------------------------------------------------------------------------------------------------*/

function AutoBindApplicantDistrict() {
    var query1 = "SELECT intDistrictId AS COLUMN_NAME_VALUE ,vchDistrictName AS COLUMN_NAME_TEXT FROM M_District WHERE intStateId=20 ORDER BY vchDistrictName ASC";
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
                if ($('#sel_2').attr("EditData") === this.Text) {
                    $('#sel_2').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_2').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*------------------------------------------------------------------------------------------------------------*/

function BindPremisesDistrict() {
    var query1 = "SELECT intDistrictId AS COLUMN_NAME_VALUE ,vchDistrictName AS COLUMN_NAME_TEXT FROM M_District WHERE intStateId=20 ORDER BY vchDistrictName ASC";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_3').html('');
            $('#sel_3').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_3').append($("<option ></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*------------------------------------------------------------------------------------------------------------*/

function AutoBindPremisesDistrict() {
    var query1 = "SELECT intDistrictId as COLUMN_NAME_VALUE ,vchDistrictName as COLUMN_NAME_TEXT FROM M_District WHERE intStateId=20 ORDER BY vchDistrictName ASC";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_3').html('');
            $('#sel_3').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_3').attr("EditData") === this.Text) {
                    $('#sel_3').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
                }
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