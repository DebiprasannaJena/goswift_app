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

    $("#div_txt_1013").hide();

    $('input:radio[name=ExistenceOfCinema]').change(function () {

        if ($('input:radio[name=ExistenceOfCinema]:checked').val() === "Yes") {
            $("#div_txt_1013").show();
            $('#txt_10').val("");
            $("#txt_10").addClass("req rset");
        }
        else {
            $("#div_txt_1013").hide();
            $('#txt_10').val("");
            $("#txt_10").removeClass("req rset");
        }
    });

    BindDistrict();
    AmountDetails(0);

    /*-------------------------------------------------------------------*/

    $("#sel_2").change(function () {
        
        if ($("#txt_17").val() !== "") {

            if ($('option:selected', this).val() === "A Class") {

                if (parseInt($("#txt_17").val()) <= 500) {
                    AmountDetails(525);
                }
                else if (parseInt($("#txt_17").val()) >= 501 && parseInt($("#txt_17").val()) <= 900) {
                    AmountDetails(675);
                }
                else if (parseInt($("#txt_17").val()) >= 901 && parseInt($("#txt_17").val()) <= 1500) {
                    AmountDetails(325);
                }
                else {
                    AmountDetails(0);
                }
            }
            else if ($('option:selected', this).val() === "B Class") {

                if (parseInt($("#txt_17").val()) <= 500) {
                    AmountDetails(315);
                }
                else if (parseInt($("#txt_17").val()) >= 501 && parseInt($("#txt_17").val()) <= 900) {
                    AmountDetails(405);
                }
                else if (parseInt($("#txt_17").val()) >= 901 && parseInt($("#txt_17").val()) <= 1500) {
                    AmountDetails(495);
                }
                else {
                    AmountDetails(0);
                }
            }
            else if ($('option:selected', this).val() === "C Class") {

                if (parseInt($("#txt_17").val()) <= 500) {
                    AmountDetails(105);
                }
                else if (parseInt($("#txt_17").val()) >= 501 && parseInt($("#txt_17").val()) <= 900) {
                    AmountDetails(135);
                }
                else if (parseInt($("#txt_17").val()) >= 901 && parseInt($("#txt_17").val()) <= 1500) {
                    AmountDetails(165);
                }
                else {
                    AmountDetails(0);
                }
            }
            else {
                AmountDetails(0);
            }
        }
        else {
            AmountDetails(0);
        }          
    });

    /*-------------------------------------------------------------------*/

    $("#txt_17").change(function () {

        if ($("#sel_2").val() === "A Class") {

            if (parseInt($("#txt_17").val()) <= 500) {
                AmountDetails(525);
            }
            else if (parseInt($("#txt_17").val()) >= 501 && parseInt($("#txt_17").val()) <= 900) {
                AmountDetails(675);
            }
            else if (parseInt($("#txt_17").val()) >= 901 && parseInt($("#txt_17").val()) <= 1500) {
                AmountDetails(325);
            }
            else {
                AmountDetails(0);
            }
        }
        else if ($("#sel_2").val() === "B Class") {

            if (parseInt($("#txt_17").val()) <= 500) {
                AmountDetails(315);
            }
            else if (parseInt($("#txt_17").val()) >= 501 && parseInt($("#txt_17").val()) <= 900) {
                AmountDetails(405);
            }
            else if (parseInt($("#txt_17").val()) >= 901 && parseInt($("#txt_17").val()) <= 1500) {
                AmountDetails(495);
            }
            else {
                AmountDetails(0);
            }
        }
        else if ($("#sel_2").val() === "C Class") {

            if (parseInt($("#txt_17").val()) <= 500) {
                AmountDetails(105);
            }
            else if (parseInt($("#txt_17").val()) >= 501 && parseInt($("#txt_17").val()) <= 900) {
                AmountDetails(135);
            }
            else if (parseInt($("#txt_17").val()) >= 901 && parseInt($("#txt_17").val()) <= 1500) {
                AmountDetails(165);
            }
            else {
                AmountDetails(0);
            }
        }
        else {
            AmountDetails(0);
        }        
    });
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