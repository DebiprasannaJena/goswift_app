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

    $("#div_txt_1419").hide();
    $("#div_txt_1522").hide();
    $("#div_txt_1827").hide();
    $("#div_txt_1930").hide();

    $('input:radio[name=PremisesAttached]').change(function () {

        if ($('input:radio[name=PremisesAttached]:checked').val() === "Yes") {
            $("#div_txt_1419").show();
            $('#txt_14').val("");
            $("#txt_14").addClass("req rset");
        }
        else {
            $("#div_txt_1419").hide();
            $('#txt_14').val("");
            $("#txt_14").removeClass("req rset");
        }
    });

    $('input:radio[name=PremisesLicensed]').change(function () {

        if ($('input:radio[name=PremisesLicensed]:checked').val() === "Yes") {
            $("#div_txt_1522").show();
            $('#txt_15').val("");
            $("#txt_15").addClass("req rset");
        }
        else {
            $("#div_txt_1522").hide();
            $('#txt_15').val("");
            $("#txt_15").removeClass("req rset");
        }
    });

    $('input:radio[name=ApplicantConviction]').change(function () {

        if ($('input:radio[name=ApplicantConviction]:checked').val() === "Yes") {
            $("#div_txt_1827").show();
            $('#txt_18').val("");
            $("#txt_18").addClass("req rset");
        }
        else {
            $("#div_txt_1827").hide();
            $('#txt_18').val("");
            $("#txt_18").removeClass("req rset");
        }
    });

    $('input:radio[name=LicenceCancelled]').change(function () {

        if ($('input:radio[name=LicenceCancelled]:checked').val() === "Yes") {
            $("#div_txt_1930").show();
            $('#txt_19').val("");
            $("#txt_19").addClass("req rset");
        }
        else {
            $("#div_txt_1930").hide();
            $('#txt_19').val("");
            $("#txt_19").removeClass("req rset");
        }
    });

    BindDistrict();
    AmountDetails(100);
});

/*------------------------------------------------------------------------------------------------------------*/

function BindDistrict() {
    var query1 = "select intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District WHERE intStateId=20 ORDER BY vchDistrictName asc";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_1').html('');
            $('#sel_1').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_1').append($("<option ></option>").val(this.Value).html(this.Text));
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