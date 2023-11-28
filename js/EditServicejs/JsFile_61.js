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

    $('input:radio[name=ForestAreaInvolved]').change(function () {

        if ($('input:radio[name=ForestAreaInvolved]:checked').val() === "Yes") {
            $("#div_txt_2331").show();
            $('#txt_23').val("");
            $("#txt_23").addClass("req rset");
        }
        else {
            $("#div_txt_2331").hide();
            $('#txt_23').val("");
            $("#txt_23").removeClass("req rset");
        }
    });

    $('input:radio[name=DefenceInvolved]').change(function () {

        if ($('input:radio[name=DefenceInvolved]:checked').val() === "Yes") {
            $("#div_txt_2434").show();
            $('#txt_24').val("");
            $("#txt_24").addClass("req rset");
        }
        else {
            $("#div_txt_2434").hide();
            $('#txt_24').val("");
            $("#txt_24").removeClass("req rset");
        }
    });

    var ForestAreaInvolved = $('input:radio[name=ForestAreaInvolved]:checked').val();
    var DefenceInvolved = $('input:radio[name=DefenceInvolved]:checked').val();

    if (ForestAreaInvolved === "Yes") {
        $("#div_txt_2331").show();
        $("#txt_23").addClass("req rset");
    }
    else {
        $("#div_txt_2331").hide();
        $('#txt_23').val("");
        $("#txt_23").removeClass("req rset");
    }

    if (DefenceInvolved === "Yes") {
        $("#div_txt_2434").show();
        $("#txt_24").addClass("req rset");
    }
    else {
        $("#div_txt_2434").hide();
        $('#txt_24').val("");
        $("#txt_24").removeClass("req rset");
    }

    if ($('#sel_1').val() !== 'undefined' && $('#sel_1').val() !== "" && $('#sel_1').val() !== null) {
        AutoBindDistrict();
    }
    else {
        BindDistrict();
    }

    AmountDetails(0);
});

/*------------------------------------------------------------------------------------------------------------*/

function BindDistrict() {
    var query1 = "select intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District where  intStateId=20  order by vchDistrictName asc";
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

function AutoBindDistrict() {
    var query1 = "select intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District  where intStateId=20 order by vchDistrictName asc";
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
                if ($('#sel_1').attr("EditData") === this.Text) {
                    $('#sel_1').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_1').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*------------------------------------------------------------------------------------------------------------*/