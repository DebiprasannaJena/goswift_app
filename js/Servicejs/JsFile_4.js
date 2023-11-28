$(document).ready(function () {
    debugger;
    var ProposalId = "";
    $("#txt_5").focusout(function () {
        debugger;
                if ((parseInt($("#txt_5").val()) >= 1) &&
                (parseInt($("#txt_5").val()) <= 100)) {
                    AmountDetails(100);

                }
//        if ((parseInt($("#txt_5").val()) <= 100)) {
//            AmountDetails(100);

//        }
        else if ((parseInt($("#txt_5").val()) >= 101) &&
        (parseInt($("#txt_5").val()) <= 500)) {
            AmountDetails(500);
        }

        else if ((parseInt($("#txt_5").val()) >= 501)) {
            AmountDetails(1000);
        }
        else if ((parseInt($("#txt_5").val()) == 0)) {
            AmountDetails(0.00);
        }
    });

    //----------------------------------DateValidation----------------------------
    //    $('#txt_6').datetimepicker({
    //        format: 'DD-MMM-YYYY'
    //    }).on('dp.show', function () {
    //        return $(this).data('DateTimePicker').minDate(new Date());
    //    });

//    $('#txt_5').blur(function () {
//        // alert('hi');
//        if ($(this).val() < 20) {
//            jAlert("Maximum number of building workers Minimum 20");
//            // return false;
//            $(this).val('');
//            AmountDetails('');
//        }
//        else {
//            $(this).val();
//        }
//    });


    //    $("#txt_7").on("dp.change", function (e) {
    //        $('#txt_7').data("DateTimePicker").minDate(e.date);
    //    });

    //----------------------SameAs Show---------------------
    $('#divsmchk_txt_4').show();
    //-------------------End------------------
    //    $("#txt_1").attr("disabled", true);
    //    $("#txt_2").attr("disabled", true)
    //------------------SubmitButton Active----------------
    $("#btnSubmit").attr("disabled", true); //-----for disble button
    $('#chk_2').click(function () {
        if (document.getElementById('chk_2').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });
    //---------------------End-----------------------

    //---------datevalidation-----------
    //    $('#txt_6').datetimepicker({
    //        format: 'DD-MMM-YYYY'
    //    }).on('dp.show', function () {
    //        return $(this).data('DateTimePicker').maxDate(new Date());
    //    });
    $("#txt_7").on("dp.change", function (e) {
        $('#txt_7').data("DateTimePicker").minDate(e.date);
    });

    //------------------------------NameFieldValdiation------------------
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
    $('#txt_3').keypress(function (e) {
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

    //-----------------------------------------------
    //----------------AutoFill Dropdwon-------------------------
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        ProposalId = urlparam[1];
    }
    ProposalId = $('#hdnProposalNo').val();
    if (ProposalId != "") {
        var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + ""
        var distiD = distvalue(query, 'sel_2', ProposalId);
    }
    //------------------------------------------

    $("#sel_2").change(function () {

        var selValue = $('#sel_2').val();
        var dtLevel = "3";
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_3').html('');
                $('#sel_3').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });

    $("#sel_4").change(function () {

        var selValue = $('#sel_4').val();
        var dtLevel = "3";
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_5').html('');
                $('#sel_5').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_5').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });

    $('#smchk_txt_4').click(function () {
        if (document.getElementById("smchk_txt_4").checked) {
            $('#sel_4').val($('#sel_2').val());


            var selValue = $('#sel_4').val();
            var dtLevel = "3";
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_5').html('');
                    $('#sel_5').append($("<option></option>").val('0').html('Select'))
                    $.each(r.d, function () {
                        //                        $('#sel_5').append($("<option></option>").val(this.Value).html(this.Text));
                        //                        console.log(r.d);

                        if ($('#sel_3').val() == this.Value) {

                            $('#sel_5').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_5').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    })
                }
            });
            $('#sel_5').val($('#sel_3').val());
            $('#txt_4').val($('#txt_2').val());
        } else {
            //Clear on uncheck
            $('#sel_4').val("0");
            $('#sel_5').html('');
            $('#sel_5').val("0");
            $('#sel_5').append($("<option></option>").val('0').html('Select'))
            $('#txt_4').val("");
        }
    });

});

function distvalue(query2, dropid, ProposalId) {
    var distid = "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
           if (r.d != "") {
                $("#sel_2").attr("disabled", true); 
                distid = r.d;
                $('#' + dropid).val(r.d);
                blockvalue('', r.d, ProposalId);
            }
        }
    });
 
    return distid;
}
function blockvalue(query2, distid, ProposalId) {
    var blockid = "";
    query2 = "select intBlockId from T_LandAndUtility where vchProposalNo=" + ProposalId + ""
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            blockid = r.d;
            fillBlockDataAuto(distid, r.d);
        }
    });
    $("#sel_3").attr("disabled", true); 
    return blockid;
}

function fillBlockDataAuto(selValue, setVal) {
    var dtLevel = "3";
    var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_3').html('');
            $('#sel_3').append($("<option></option>").val('0').html('Select'))
            $.each(r.d, function () {

                if (setVal == this.Value) {
                    $('#sel_3').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
                }
            })
        }
    });


}
 function AmountDetails(amount) {
            var strText = "";
            strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>"
            lblAmount.innerHTML = strText;
            $('#hdnTotalAmount').val(amount);
        }
