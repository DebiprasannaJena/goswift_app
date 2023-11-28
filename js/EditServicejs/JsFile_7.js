
$(document).ready(function () {
    var ProposalId = "";
    AmountDetails(0);
    FillState();    
    $('#div_fil_114').hide();
    $('#fil_1').removeClass('req rset');
    $('#div_fil_315').hide();
    $('#fil_3').removeClass('req rset');

    $('input:radio[name=PastFiveYear]').change(function () {
        
        if ($('input:radio[name=PastFiveYear]:checked').val() == "Yes") {
            $('#div_fil_114').show();
            $('#fil_1').addClass('req rset');
            $('#div_fil_315').show();
            $('#fil_3').addClass('req rset');
        }
        else {
            $('#div_fil_114').hide();
            $('#fil_1').removeClass('req rset');
            $('#div_fil_315').hide();
            $('#fil_3').removeClass('req rset');
        }
    });

    /////----------------------------------------------------------------------------

    $("#txt_16").focusout(function () {
        
        //if ((parseInt($("#txt_16").val()) <= 20)) {
        //    var cnt1 = $("#txt_16").val();
        //    var result = cnt1 * 100;
        //    AmountDetails(200 + result);
        //}
        //else if ((parseInt($("#txt_16").val()) >= 20) && (parseInt($("#txt_16").val()) <= 50)) {
        //    var cnt1 = $("#txt_16").val();
        //    var result = cnt1 * 100;
        //    AmountDetails(500 + result);
        //}
        //else if ((parseInt($("#txt_16").val()) >= 51) && (parseInt($("#txt_16").val()) <= 100)) {
        //    var cnt1 = $("#txt_16").val();
        //    var result = cnt1 * 100;
        //    AmountDetails(1000 + result);
        //}
        //else if ((parseInt($("#txt_16").val()) >= 101) && (parseInt($("#txt_16").val()) <= 200)) {
        //    var cnt1 = $("#txt_16").val();
        //    var result = cnt1 * 100;
        //    AmountDetails(2000 + result);
        //}
        //else if ((parseInt($("#txt_16").val()) >= 201) && (parseInt($("#txt_16").val()) <= 400)) {
        //    var cnt1 = $("#txt_16").val();
        //    var result = cnt1 * 100;
        //    AmountDetails(4000 + result);
        //}
        //else if ((parseInt($("#txt_16").val()) >= 401)) {
        //    var cnt1 = $("#txt_16").val();
        //    var result = cnt1 * 100;
        //    AmountDetails(5000 + result);
        //}
        if ((parseInt($("#txt_16").val()) <= 20)) {
            var cnt1 = $("#txt_16").val();
            var result = cnt1 * 0;
            AmountDetails(0);
        }
        else if ((parseInt($("#txt_16").val()) >= 20) && (parseInt($("#txt_16").val()) <= 49)) {
            var cnt1 = $("#txt_16").val();
            var result = cnt1 * 0;
            AmountDetails(0);
        }
        else if ((parseInt($("#txt_16").val()) >= 49) && (parseInt($("#txt_16").val()) <= 50)) {
            var cnt1 = $("#txt_16").val();
            var result = cnt1 * 100;
            AmountDetails(500 + result);
        }
        else if ((parseInt($("#txt_16").val()) >= 51) && (parseInt($("#txt_16").val()) <= 100)) {
            var cnt1 = $("#txt_16").val();
            var result = cnt1 * 100;
            AmountDetails(1000 + result);
        }
        else if ((parseInt($("#txt_16").val()) >= 101) && (parseInt($("#txt_16").val()) <= 200)) {
            var cnt1 = $("#txt_16").val();
            var result = cnt1 * 100;
            AmountDetails(2000 + result);
        }
        else if ((parseInt($("#txt_16").val()) >= 201) && (parseInt($("#txt_16").val()) <= 400)) {
            var cnt1 = $("#txt_16").val();
            var result = cnt1 * 100;
            AmountDetails(4000 + result);
        }
        else if ((parseInt($("#txt_16").val()) >= 401)) {
            var cnt1 = $("#txt_16").val();
            var result = cnt1 * 100;
            AmountDetails(5000 + result);
        }
    });

    //----------------------SameAs Show---------------------
    $('#divsmchk_txt_57').show();
    $('#divsmchk_txt_913').show();
    //-------------------End------------------

    /////----------------------------------------------------------------------------
    /////------------------SubmitButton Active----------------
    $("#btnSubmit").attr("disabled", true); //-----for disble button

    $('#chk_1').click(function () {
        if (document.getElementById('chk_1').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
        EditCalculation();
    });

    /////----------------------------------------------------------------------------

    $('#txt_16').blur(function () {
        if ($(this).val() < 20) {
            jAlert("Maximum number of contract labour to be engaged should be minimum 20 !");
            $(this).val('');
            AmountDetails(0);
        }
        else {
            $(this).val();
        }
    });

    /////----------------------------------------------------------------------------

    $('#txt_8').keypress(function (e) {
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

    /////----------------------------------------------------------------------------

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

    /////----------------------------------------------------------------------------

    $('#txt_14').keypress(function (e) {
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

    /////----------------------------------------------------------------------------

    $('#txt_6').keypress(function (e) {
        AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-/';
        var k;
        k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
        if (k != 13 && k != 8 && k != 0) {
            if ((e.ctrlKey == false) && (e.altKey == false)) {
                return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
            }
            else {
                return true;
                $('#txt_6').val("");
            }
        }
        else {
            return true;
            $('#txt_6').val("");
        }
    });

    /////----------------------------------------------------------------------------

    jQuery("input[name='ApplicationFrom']").each(function (i) {
        jQuery(this).attr('disabled', 'disabled');
    });

    //---------------------------------------------
    //    $('input:radio[name=ApplicationFrom]').change(function () {
    //        debugger;
    //        if ($('input:radio[name=ApplicationFrom]:checked').val() == "Renewal") {
    //            //            var ProposalNo = GetParameterValues('ProposalNo');
    //            window.location.href = "FormView.aspx?FormId=40&ProposalNo=" + ProposalId + "";
    //            alert("Hello " + name + " your ID is " + id);
    //            function GetParameterValues(param) {
    //                var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    //                for (var i = 0; i < url.length; i++) {
    //                    var urlparam = url[i].split('=');
    //                    if (urlparam[0] == param) {
    //                        return urlparam[1];
    //                    }
    //                }
    //            }
    //        }

    //    });

    //----------------------------------DateValidation----------------------------
    $('#txt_7').datetimepicker({
        format: 'DD-MMM-YYYY'
    }).on('dp.show', function () {
        return $(this).data('DateTimePicker').maxDate(new Date());
    });


    //    $('#txt_12').datetimepicker({
    //        format: 'DD-MMM-YYYY'
    //    }).on('dp.show', function () {
    //        return $(this).data('DateTimePicker').maxDate(new Date());
    //    });

    $("#txt_12").on("dp.change", function (e) {
        $('#txt_13').data("DateTimePicker").minDate(e.date);
    });

    //    $("#txt_13").on("dp.change", function (e) {
    //        $('#txt_12').data("DateTimePicker").minDate(e.date);
    //    });


    //    $("#txtDmdSentFrom").on("dp.change", function (e) {
    //        $('#txtDmdSentTo').data("DateTimePicker").minDate(e.date);
    //    });
    //    $("#txtDmdSentTo").on("dp.change", function (e) {
    //        $('#txtDmdSentFrom').data("DateTimePicker").maxDate(e.date);
    //    });

    //    $('#txt_7').datetimepicker({
    //        format: 'DD-MMM-YYYY'
    //    }).on('dp.show', function () {
    //        return $(this).data('DateTimePicker').maxDate(new Date());
    //    });
    //////////////////////////////Date/////////////////////////////////////////////////////


    //--------------------------------------------------------------------------
    //----------------AutoFill Dropdwon-------------------------

    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        if (i == 1) {
            var urlparam = url[i].split('=');
            ProposalId = urlparam[1];
        }

        else if (i == 0) {
            var urlparam = url[i].split('=');
            FormId = urlparam[1];
        }
    }
    ProposalId = $('#hdnProposalNo').val();
    if (ProposalId != "") {
        var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + "";
        var distiD = distvalue(query, 'sel_3', ProposalId);
    }

    //------------------------------------------

    $("#sel_1").change(function () {
        
        var selValue = $('#sel_1').val();
        var dtSource = "M_ADM_LocationDetails";
        var dtValue = "intLevelDetailId";
        var dtText = "nvchLevelName";
        var dtLevel = "3";
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

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
    });

    /////----------------------------------------------------------------------------

    $("#sel_3").change(function () {

        var selValue = $('#sel_3').val();
        var dtSource = "M_ADM_LocationDetails";
        var dtValue = "intLevelDetailId";
        var dtText = "nvchLevelName";
        var dtLevel = "3";
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + "order by vchBlockName asc";

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
    });

    /////----------------------------------------------------------------------------

    $("#sel_5").change(function () {

        var selValue = $('#sel_5').val();
        var dtSource = "M_ADM_LocationDetails";
        var dtValue = "intLevelDetailId";
        var dtText = "nvchLevelName";
        var dtLevel = "3";
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_6').html('');
                $('#sel_6').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_6').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });
    });

    /////----------------------------------------------------------------------------

    $("#sel_1").change(function () {
        
        var selValue = $('#sel_1').val();
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
    });

    //---------------------SameAs--------------------
    $('#smchk_txt_57').click(function () {
        
        if (document.getElementById("smchk_txt_57").checked) {
            $('#sel_3').val($('#sel_1').val());
            $('#txt_5').val($('#txt_3').val());
            var selValue = $('#sel_3').val();
            var dtSource = "M_ADM_LocationDetails";
            var dtValue = "intLevelDetailId";
            var dtText = "nvchLevelName";
            var dtLevel = "3";
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + "order by vchBlockName asc";

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
                        //                        $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                        //                        console.log(r.d);

                        if ($('#sel_2').val() == this.Value) {
                            $('#sel_4').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    });
                }
            });

        } else {
            $('#sel_3').val("0");
            $('#sel_4').val("0");
            $('#sel_4').html('');
            $('#sel_4').append($("<option></option>").val('0').html('Select'));
            $('#txt_5').val("");
        }
    });

    /////----------------------------------------------------------------------------

    $('#smchk_txt_913').click(function () {
        
        if (document.getElementById("smchk_txt_913").checked) {
            $('#sel_5').val($('#sel_3').val());
            $('#txt_9').val($('#txt_5').val());
            var selValue = $('#sel_5').val();
            var dtSource = "M_ADM_LocationDetails";
            var dtValue = "intLevelDetailId";
            var dtText = "nvchLevelName";
            var dtLevel = "3";
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + "order by vchBlockName asc";

            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_6').html('');
                    $('#sel_6').append($("<option></option>").val('0').html('Select'));
                    $.each(r.d, function () {
                        //                        $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                        //                        console.log(r.d);

                        if ($('#sel_4').val() == this.Value) {
                            $('#sel_6').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_6').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    });
                }
            });

        } else {
            $('#sel_5').val("0");
            $('#sel_6').val("0");
            $('#sel_6').html('');
            $('#sel_6').append($("<option></option>").val('0').html('Select'));
            $('#txt_9').val("");
        }
    });
    //----------------------------------end------------------------------

    if ($('#sel_4').attr("EditData") !== 'undefined' && $('#sel_4').attr("EditData") !== "" && $('#sel_4').attr("EditData") !== null) {
        AutofillBlock4($('#sel_3').val(), $('#sel_4').attr("EditData"));
    }
    if ($('#sel_6').attr("EditData") !== 'undefined' && $('#sel_6').attr("EditData") !== "" && $('#sel_6').attr("EditData") !== null) {
        AutofillBlock6($('#sel_5').val(), $('#sel_6').attr("EditData"));
    }

    AutoFillState();
    AutoFillDistrict();
    EditCalculation();

});

/////----------------------------------------------------------------------------

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
                distid = r.d;
                $('#' + dropid).val(r.d);
                blockvalue('', r.d, ProposalId);
                $("#" + dropid).attr("disabled", "disabled");
            }
        }
    });
    return distid;
}

/////----------------------------------------------------------------------------

function blockvalue(query2, distid, ProposalId) {
    var blockid = "";
    query2 = "select intBlockId from T_LandAndUtility where vchProposalNo=" + ProposalId + "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            if (r.d != "") {
                blockid = r.d;
                fillBlockDataAuto(distid, r.d);
            }
        }
    });
    return blockid;
}

/////----------------------------------------------------------------------------

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

                if (setVal == this.Value) {
                    $('#sel_4').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });

    $("#sel_4").attr("disabled", "disabled");
}

/////----------------------------------------------------------------------------

function AmountDetails(amount) {
    //    var strText = "";
    //    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>"
    //    lblAmount.innerHTML = strText;
    //    $('#hdnTotalAmount').val(amount);
    
    var strText = "";
    var cnt2 = "";
    var Emp = 0;
    if (amount != 0) {
        if ($("#txt_16").val() != "") {
            Emp = $("#txt_16").val();

            var result = Emp + ' x  100 = ' + Emp * 100;
        }
        else {
            result = 0.00;
            cnt2 = 0.00;
        }
    }
    else {
        result = 0.00;
        cnt2 = 0.00;
    }

    //if ((parseInt($("#txt_16").val()) == '')) {
    //    cnt2 = 0;
    //}
    //if ((parseInt($("#txt_16").val()) <= 20)) {
    //    cnt2 = 200;
    //}
    //else if ((parseInt($("#txt_16").val()) >= 21) && (parseInt($("#txt_16").val()) <= 50)) {
    //    cnt2 = 500;
    //}
    //else if ((parseInt($("#txt_16").val()) >= 51) && (parseInt($("#txt_16").val()) <= 100)) {
    //    cnt2 = 1000;
    //}
    //else if ((parseInt($("#txt_16").val()) >= 101) && (parseInt($("#txt_16").val()) <= 200)) {
    //    cnt2 = 2000;
    //}
    //else if ((parseInt($("#txt_16").val()) >= 201) && (parseInt($("#txt_16").val()) <= 400)) {
    //    cnt2 = 4000;
    //}
    //else if ((parseInt($("#txt_16").val()) >= 401)) {
    //    cnt2 = 5000;
    //}

    if ((parseInt($("#txt_16").val()) == '')) {
        cnt2 = 0;
    }
    if ((parseInt($("#txt_16").val()) <= 20)) {
        cnt2 = 0;
    }
    else if ((parseInt($("#txt_16").val()) >= 21) && (parseInt($("#txt_16").val()) <= 49)) {
        cnt2 = 0;
    }
    else if ((parseInt($("#txt_16").val()) >= 49) && (parseInt($("#txt_16").val()) <= 50)) {
        cnt2 = 500;
    }
    else if ((parseInt($("#txt_16").val()) >= 51) && (parseInt($("#txt_16").val()) <= 100)) {
        cnt2 = 1000;
    }
    else if ((parseInt($("#txt_16").val()) >= 101) && (parseInt($("#txt_16").val()) <= 200)) {
        cnt2 = 2000;
    }
    else if ((parseInt($("#txt_16").val()) >= 201) && (parseInt($("#txt_16").val()) <= 400)) {
        cnt2 = 4000;
    }
    else if ((parseInt($("#txt_16").val()) >= 401)) {
        cnt2 = 5000;
    }

    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Security Deposite per head (100/-)</th><td width='50%'><b>" + result + "/-</b></td></tr><tr><th width='50%'>Fees</th><td width='50%'><b>" + cnt2 + "/-</b></td></tr><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>"
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(amount);
    $('#hdnApplicationFee').val(cnt2);
}

/////----------------------------------------------------------------------------

var month = new Array();
month[0] = "Jan";
month[1] = "Feb";
month[2] = "Mar";
month[3] = "Apr";
month[4] = "May";
month[5] = "Jun";
month[6] = "Jul";
month[7] = "Aug";
month[8] = "Sep";
month[9] = "Oct";
month[10] = "Nov";
month[11] = "Dec";

function CompareTwoDate(Controlname1, Controlname2, Fieldname1, Fieldname2) {
    var fromDate = $("input#" + Controlname1).val();
    var toDate = $("input#" + Controlname2).val();
    //alert(fromDate+'==='+toDate);
    if (toDate != "") {
        var dateParts = fromDate.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var fdate = new Date(newDateStr);
        // alert(fdate);
        var dateParts1 = toDate.split("-");
        var newDateStr1 = dateParts1[1] + " " + dateParts1[0] + ", " + dateParts1[2];
        var tdate = new Date(newDateStr1);
        // alert(tdate);
        if (fdate > tdate) {

            alert(Fieldname2 + " can not be before " + Fieldname1);
            $(this).focus();
            return false;


        }
        return true;
    }
}


var tdate = new Date();
var dd = tdate.getDate(); //yields day
var MMM = month[tdate.getMonth()]; //yields month
var yyyy = tdate.getFullYear(); //yields year
var curDate = dd + "-" + MMM + "-" + yyyy;

function CheckGreaterDate(cntr, strText) {
    var myDate = $("input#" + cntr).val();
    var dateParts = myDate.split("/");

    var yourString = dateParts[0];
    yourString = Number(yourString).toString();

    var txtMMM = month[yourString]; //yields month
    var txtDD = dateParts[1];
    var txtYYYY = dateParts[2];
    var txtCombine = txtDD + "-" + txtMMM + "-" + txtYYYY;

    // alert(myDate + '===' + curDate);
    if (curDate != "") {
        var dateParts = txtCombine.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var cDate = new Date(newDateStr);
        //alert(cDate);
        var dateParts1 = curDate.split("-");
        var newDateStr1 = dateParts1[1] + " " + dateParts1[0] + ", " + dateParts1[2];
        var tdate = new Date(newDateStr1);
        //alert(tdate);
        if (cDate > tdate) {
            alert(strText + " must be less than or equal to current date");
            $('#' + cntr).focus();
            return false;
        }
        return true;
    }
}

/////----------------------------------------------------------------------------

function CheckLessDate(cntr, strText) {
    var myDate = $("input#" + cntr).val();
    var now = new Date();
    //alert(myDate + '===' + curDate);
    if (curDate != "") {
        var dateParts = myDate.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var cDate = new Date(newDateStr);
        //alert(cDate);
        var dateParts1 = curDate.split("-");
        var newDateStr1 = dateParts1[1] + " " + dateParts1[0] + ", " + dateParts1[2];
        var tdate = new Date(newDateStr1);
        //alert(tdate);
        if (cDate < tdate) {
            alert(strText + " must be greater than or equal to current date");
            $('#' + cntr).focus();
            return false;
        }
        return true;
    }
}

/////----------------------------------------------------------------------------

function FillState() {
    
    var query1 = "select INT_STATE_ID as COLUMN_NAME_VALUE , VCH_STATE as COLUMN_NAME_TEXT from M_STATE_MASTER  order by VCH_STATE asc";
    FillDistrict();
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


                if ("20" == this.Value) {
                    $('#sel_1').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_1').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/////----------------------------------------------------------------------------

function FillDistrict() {
    
    var query1 = "select intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District where  intStateId=20  order by vchDistrictName asc";

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

/////----------------------------------------------------------------------------

function EditCalculation() {
  
    //if ((parseInt($("#txt_16").val()) <= 20)) {
    //    var cnt1 = $("#txt_16").val();
    //    var result = cnt1 * 100;
    //    AmountDetails(200 + result);
    //}
    //else if ((parseInt($("#txt_16").val()) >= 21) && (parseInt($("#txt_16").val()) <= 50)) {
    //    var cnt1 = $("#txt_16").val();
    //    var result = cnt1 * 100;
    //    AmountDetails(500 + result);
    //}
    //else if ((parseInt($("#txt_16").val()) >= 51) && (parseInt($("#txt_16").val()) <= 100)) {
    //    var cnt1 = $("#txt_16").val();
    //    var result = cnt1 * 100;
    //    AmountDetails(1000 + result);
    //}
    //else if ((parseInt($("#txt_16").val()) >= 101) && (parseInt($("#txt_16").val()) <= 200)) {
    //    var cnt1 = $("#txt_16").val();
    //    var result = cnt1 * 100;
    //    AmountDetails(2000 + result);
    //}
    //else if ((parseInt($("#txt_16").val()) >= 201) && (parseInt($("#txt_16").val()) <= 400)) {
    //    var cnt1 = $("#txt_16").val();
    //    var result = cnt1 * 100;
    //    AmountDetails(4000 + result);
    //}
    //else if ((parseInt($("#txt_16").val()) >= 401)) {
    //    var cnt1 = $("#txt_16").val();
    //    var result = cnt1 * 100;
    //    AmountDetails(5000 + result);
    //}

    if ((parseInt($("#txt_16").val()) <= 20)) {
        var cnt1 = $("#txt_16").val();
        var result = cnt1 * 0;
        AmountDetails(0);
    }
    else if ((parseInt($("#txt_16").val()) >= 21) && (parseInt($("#txt_16").val()) <= 49)) {
        var cnt1 = $("#txt_16").val();
        var result = cnt1 * 0;
        AmountDetails(0);
    }
    else if ((parseInt($("#txt_16").val()) >= 49) && (parseInt($("#txt_16").val()) <= 50)) {
        var cnt1 = $("#txt_16").val();
        var result = cnt1 * 100;
        AmountDetails(500 + result);
    }
    else if ((parseInt($("#txt_16").val()) >= 51) && (parseInt($("#txt_16").val()) <= 100)) {
        var cnt1 = $("#txt_16").val();
        var result = cnt1 * 100;
        AmountDetails(1000 + result);
    }
    else if ((parseInt($("#txt_16").val()) >= 101) && (parseInt($("#txt_16").val()) <= 200)) {
        var cnt1 = $("#txt_16").val();
        var result = cnt1 * 100;
        AmountDetails(2000 + result);
    }
    else if ((parseInt($("#txt_16").val()) >= 201) && (parseInt($("#txt_16").val()) <= 400)) {
        var cnt1 = $("#txt_16").val();
        var result = cnt1 * 100;
        AmountDetails(4000 + result);
    }
    else if ((parseInt($("#txt_16").val()) >= 401)) {
        var cnt1 = $("#txt_16").val();
        var result = cnt1 * 100;
        AmountDetails(5000 + result);
    }
}

/////----------------------------------------------------------------------------


function AutofillBlock4(distval, selValue) {

    var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(distval) + " order by vchBlockName asc";

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
                if (this.Text === selValue) {
                    $('#sel_4').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

function AutofillBlock6(distval, selValue) {

    var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(distval) + " order by vchBlockName asc";

    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_6').html('');
            $('#sel_6').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if (this.Text === selValue) {
                    $('#sel_6').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_6').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

function AutoFillState() {

    var query1 = "select INT_STATE_ID as COLUMN_NAME_VALUE , VCH_STATE as COLUMN_NAME_TEXT from M_STATE_MASTER  order by VCH_STATE asc";
    FillDistrict();
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

/////----------------------------------------------------------------------------

function AutoFillDistrict() {

    var query1 = "select intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District where  intStateId=20  order by vchDistrictName asc";

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
                    $('#sel_2').append($("<option ></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}