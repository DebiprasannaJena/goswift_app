
$(document).ready(function () {
    $('input:radio[name=ApplicationFrom]').change(function () {
        if ($('input:radio[name=ApplicationFrom]:checked').val() === "New") {
            $('#lbl_fil_140').find('span').css('display', 'none');
            $('#fil_14').removeClass("req rset");
        }
        else {
            $('#lbl_fil_140').find('span').css('display', 'block');
            $('#fil_14').addClass("req rset");
        }
    });

    $("#btnSubmit").attr("disabled", true); //-----for disble button
    $("#divsmchk_txt_10").show();

    /*------------------------------------------------------------------------------------------------*/

    $('#chk_11').click(function () {
        if (document.getElementById('chk_11').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });

    /*------------------------------------------------------------------------------------------------*/

    var ProposalId = "";
    $("#txt_13").focusout(function () {

        //---=========================================================
        // The registration fee depending on the number of employee have changed on Dt:-14-Sep-2020
        // The new amount changed as per below.
        // Employee    | Before 14-Sep-2020  |   After 14-Sep-2020
        // 1 to 9      |   112.50            |     168.75
        // 10 to 19    |   300.00            |     450.00
        // 20 or More  |   450.00            |     675.00
        //---=========================================================

        var fee1 = '168.75';
        var fee2 = '450.00';
        var fee3 = '675.00';

        var date = new Date();
        var yrPart = date.getFullYear();
        var ss = new Date("31-Oct-" + yrPart);
        var text = new Date($("#txt_12").val());
        var RegDate = new Date($("#txt_120").val());
        var CommYrPart = text.getFullYear();
        var RegYrPart = RegDate.getFullYear();
        var ss = new Date("31-Oct-" + RegYrPart);
        var result = "";

        if ($('input:radio[name=ApplicationFrom]:checked').val() === "Renewal") {

            if ($("#txt_12").val() !== "") {
                if ((CommYrPart = RegYrPart) && (text > ss)) {
                    if ((parseInt($("#txt_13").val()) >= 1) && (parseInt($("#txt_13").val()) <= 9)) {
                        result = parseFloat(fee1) + parseFloat((fee1) * (25 / 100));
                        AmountDetails(result);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 10) && (parseInt($("#txt_13").val()) <= 19)) {
                        result = parseFloat(fee2) + parseFloat((fee2) * (25 / 100));
                        AmountDetails(result);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 20)) {
                        result = parseFloat(fee3) + parseFloat((fee3) * (25 / 100));
                        AmountDetails(result);
                    }
                }
                else {
                    if ((parseInt($("#txt_13").val()) >= 1) && (parseInt($("#txt_13").val()) <= 9)) {
                        AmountDetails(fee1);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 10) && (parseInt($("#txt_13").val()) <= 19)) {
                        AmountDetails(fee2);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 20)) {
                        AmountDetails(fee3);
                    }
                }
            }
        }
        else {
            if ((parseInt($("#txt_13").val()) >= 1) && (parseInt($("#txt_13").val()) <= 9)) {
                AmountDetails(fee1);
            }
            else if ((parseInt($("#txt_13").val()) >= 10) && (parseInt($("#txt_13").val()) <= 19)) {
                AmountDetails(fee2);
            }
            else if ((parseInt($("#txt_13").val()) >= 20)) {
                AmountDetails(fee3);
            }
        }
    });

    /*------------------------------------------------------------------------------------------------*/
    //----------------AutoFill Dropdwon-------------------------

    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = "";
        if (i === 1) {
            urlparam = url[i].split('=');
            ProposalId = urlparam[1];
        }
        else if (i === 0) {
            urlparam = url[i].split('=');
            FormId = urlparam[1];
        }
    }
    ProposalId = $('#hdnProposalNo').val();
    var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + "";
    //    var distiD = distvalue(query, 'sel_3', ProposalId);

    /*------------------------------------------------------------------------------------------------*/

    $('#sel_4').html('');
    $('#sel_4').append($("<option></option>").val('0').html('Select'));
    $('#sel_9').html('');
    $('#sel_9').append($("<option></option>").val('0').html('Select'));

    /*------------------------------------------------------------------------------------------------*/
    //-----validation------------
    $('#txt_1').keypress(function (e) {
        var regex = new RegExp("^[a-zA-Z ]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        else {
            e.preventDefault();
            //alert('Please Enter Alphabate');
            return false;
        }
    });

    /*------------------------------------------------------------------------------------------------*/

    $('#txt_2').keypress(function (e) {
        var regex = new RegExp("^[a-zA-Z ]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        else {
            e.preventDefault();
            //alert('Please Enter Alphabate');
            return false;
        }
    });

    /*------------------------------------------------------------------------------------------------*/

    $("#txt_120").removeClass('req rset');
    $("#txt_110").removeClass('req rset');
    $("#div_txt_1200").hide('');
    $("#div_txt_1101").hide('');
    $("#fil_14").removeClass('req rset');
    $('input:radio[name=ApplicationFrom]').change(function () {

        if ($('input:radio[name=ApplicationFrom]:checked').val() === "Renewal") {
            $("#fil_14").removeClass('req rset');
            $("#div_txt_1101").show('');
            $("#txt_110").addClass('req rset');
            $("#div_txt_1200").show('');
            $("#txt_120").addClass('req rset');
        }
        else {
            $("#txt_110").removeClass('req rset');
            $("#div_txt_1101").hide('');
            $("#txt_120").removeClass('req rset');
            $("#div_txt_1200").hide('');

        }
    });

    //----------Date Validation--------------------------
    // $('#txt_12').datetimepicker({
    //        format: 'DD-MMM-YYYY'
    //    }).on('dp.show', function () {
    //        return $(this).data('DateTimePicker').minDate(new Date());
    //    });
    //    $("#txt_12").on("dp.change", function (e) {
    //        $('#txt_13').data("DateTimePicker").minDate(e.date);
    //    });


    /*------------------------------------------------------------------------------------------------*/

    $("#txt_12").on("dp.change", function (e) {

        //---=========================================================
        // Added by Sushant Jena
        // The registration fee depending on the number of employee have changed on Dt:-14-Sep-2020
        // The new amount changed as per below.
        // Employee    | Before 14-Sep-2020  |   After 14-Sep-2020
        // 1 to 9      |   112.50            |    168.75
        // 10 to 19    |   300.00            |    450.00
        // 20 or More  |   450.00            |    675.00
        //---=========================================================

        var fee1 = '168.75';
        var fee2 = '450.00';
        var fee3 = '675.00';

        var date = new Date();
        var yrPart = date.getFullYear();
        var ss = new Date("31-Oct-" + yrPart);
        var text = new Date($("#txt_12").val());
        var RegDate = new Date($("#txt_120").val());
        var CommYrPart = text.getFullYear();
        var RegYrPart = RegDate.getFullYear();
        var ss = new Date("31-Oct-" + RegYrPart);
        var result = "";

        if ($('input:radio[name=ApplicationFrom]:checked').val() === "Renewal") {

            if ($("#txt_13").val() !== "") {

                if ((CommYrPart = RegYrPart) && (text > ss)) {
                    if ((parseInt($("#txt_13").val()) >= 1) && (parseInt($("#txt_13").val()) <= 9)) {
                        result = parseFloat(fee1) + parseFloat((fee1) * (25 / 100));
                        AmountDetails(result);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 10) && (parseInt($("#txt_13").val()) <= 19)) {
                        result = parseFloat(fee2) + parseFloat((fee2) * (25 / 100));
                        AmountDetails(result);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 20)) {
                        result = parseFloat(fee3) + parseFloat((fee3) * (25 / 100));
                        AmountDetails(result);
                    }
                }
                else {
                    if ((parseInt($("#txt_13").val()) >= 1) && (parseInt($("#txt_13").val()) <= 9)) {
                        AmountDetails(fee1);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 10) && (parseInt($("#txt_13").val()) <= 19)) {
                        AmountDetails(fee2);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 20)) {
                        AmountDetails(fee3);
                    }
                }
            }
        }
        else {

            if ((parseInt($("#txt_13").val()) >= 1) && (parseInt($("#txt_13").val()) <= 9)) {
                AmountDetails(fee1);
            }
            else if ((parseInt($("#txt_13").val()) >= 10) && (parseInt($("#txt_13").val()) <= 19)) {
                AmountDetails(fee2);
            }
            else if ((parseInt($("#txt_13").val()) >= 20)) {
                AmountDetails(fee3);
            }
        }
    });

    /*------------------------------------------------------------------------------------------------*/

    $("#txt_120").on("dp.change", function (e) {

        //---=========================================================
        // Added by Sushant Jena
        // The registration fee depending on the number of employee have changed on Dt:-14-Sep-2020
        // The new amount changed as per below.
        // Employee    | Before 14-Sep-2020  |   After 14-Sep-2020
        // 1 to 9      |   112.50            |    168.75
        // 10 to 19    |   300.00            |    450.00
        // 20 or More  |   450.00            |    675.00
        //---=========================================================

        var fee1 = '168.75';
        var fee2 = '450.00';
        var fee3 = '675.00';

        var date = new Date();
        var yrPart = date.getFullYear();
        var ss = new Date("31-Oct-" + yrPart);
        var text = new Date($("#txt_12").val());
        var RegDate = new Date($("#txt_120").val());
        var CommYrPart = text.getFullYear();
        var RegYrPart = RegDate.getFullYear();
        var ss = new Date("31-Oct-" + RegYrPart);
        var result = "";

        if ($('input:radio[name=ApplicationFrom]:checked').val() === "Renewal") {

            if ($("#txt_13").val() !== "") {

                if ((CommYrPart = RegYrPart) && (text > ss)) {
                    if ((parseInt($("#txt_13").val()) >= 1) && (parseInt($("#txt_13").val()) <= 9)) {
                        result = parseFloat(fee1) + parseFloat((fee1) * (25 / 100));
                        AmountDetails(result);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 10) && (parseInt($("#txt_13").val()) <= 19)) {
                        result = parseFloat(fee2) + parseFloat((fee2) * (25 / 100));
                        AmountDetails(result);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 20)) {
                        result = parseFloat(fee3) + parseFloat((fee3) * (25 / 100));
                        AmountDetails(result);
                    }
                }
                else {
                    if ((parseInt($("#txt_13").val()) >= 1) && (parseInt($("#txt_13").val()) <= 9)) {
                        AmountDetails(fee1);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 10) && (parseInt($("#txt_13").val()) <= 19)) {
                        AmountDetails(fee2);
                    }
                    else if ((parseInt($("#txt_13").val()) >= 20)) {
                        AmountDetails(fee3);
                    }
                }
            }
        }
        else {

            if ((parseInt($("#txt_13").val()) >= 1) && (parseInt($("#txt_13").val()) <= 9)) {
                AmountDetails(fee1);
            }
            else if ((parseInt($("#txt_13").val()) >= 10) && (parseInt($("#txt_13").val()) <= 19)) {
                AmountDetails(fee2);
            }
            else if ((parseInt($("#txt_13").val()) >= 20)) {
                AmountDetails(fee3);
            }
        }
    });

    /*------------------------------------------------------------------------------------------------*/

    $("input[id='smchk_txt_10']").click(function () {
        checkboxDet();
    });

    /*------------------------------------------------------------------------------------------------*/

    $("#sel_3").change(function () {
        var selValue = $('#sel_3').val();
        var dtSource = "M_ADM_LocationDetails";
        var dtValue = "intLevelDetailId";
        var dtText = "nvchLevelName";
        var dtLevel = "3";
        var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + "order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
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
    });

    /*------------------------------------------------------------------------------------------------*/

    $("#sel_8").change(function () {

        var selValue = $('#sel_8').val();
        var dtSource = "M_ADM_LocationDetails";
        var dtValue = "intLevelDetailId";
        var dtText = "nvchLevelName";
        var dtLevel = "3";
        var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from FillULB  where intDistrictId=" + parseInt(selValue) + "  order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_9').html('');
                $('#sel_9').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_9').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });
    });
});

/*------------------------------------------------------------------------------------------------*/

function checkboxDet() {
    if (document.getElementById("smchk_txt_10").checked) {
        $('#txt_10').val($('#txt_5').val());
        $('#sel_8').val($('#sel_3').val());
        var selValue = $('#sel_8').val();
        // var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from FillULB  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";
        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_9').html('');
                $('#sel_9').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    //                        $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                    //                        console.log(r.d);

                    if ($('#sel_4').val() === this.Value) {
                        $('#sel_9').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                    }
                    else {
                        $('#sel_9').append($("<option></option>").val(this.Value).html(this.Text));
                    }
                });
            }
        });
    }
    else {
        $('#txt_10').val('');
        $('#sel_8').val('0');
        $('#sel_9').val('0');
        $('#sel_9').html('');
        $('#sel_9').append($("<option></option>").val('0').html('Select'));
    }
}

/*------------------------------------------------------------------------------------------------*/

function distvalue(query2, dropid, ProposalId) {
    var distid = "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMapping",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            distid = r.d;
            $('#' + dropid).val(r.d);
            blockvalue('', r.d, ProposalId);
        }
    });
    return distid;
}

/*------------------------------------------------------------------------------------------------*/

function blockvalue(query2, distid, ProposalId) {
    var blockid = "";
    query2 = "select intBlockId from T_LandAndUtility where vchProposalNo=" + ProposalId + "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMapping",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            blockid = r.d;
            fillBlockDataAuto(distid, r.d);
        }
    });
    return blockid;
}

/*------------------------------------------------------------------------------------------------*/

function fillBlockDataAuto(selValue, setVal) {
    var dtSource = "M_ADM_LocationDetails";
    var dtValue = "intLevelDetailId";
    var dtText = "nvchLevelName";
    var dtLevel = "3";
    var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_4').html('');
            $('#sel_4').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {

                if (setVal === this.Value) {
                    $('#sel_4').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*------------------------------------------------------------------------------------------------*/

function AmountDetails(amount) {
    var strText = "";
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr width='50%'><th>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>"
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(amount);
}

/*------------------------------------------------------------------------------------------------*/
