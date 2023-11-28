$(document).ready(function () {
    AmountDetails(0)

    if ($('#sel_4').attr("EditData") != 'undefined' && $('#sel_4').attr("EditData") != "" && $('#sel_4').attr("EditData") != null) {
        EditfillBlockDataAuto($('#sel_3').val(), $('#sel_4').attr("EditData"));
    }
    $("#btnSubmit").attr("disabled", true); //-----for disble button
    $('#chk_22').click(function () {
        if (document.getElementById('chk_22').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
        DpChangeCalculation();
    });
    var ProposalId = "";
    //----------------AutoFill Dropdwon-------------------------
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        ProposalId = urlparam[1];
    }
    ProposalId = $('#hdnProposalNo').val();
    var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + ""
    //    var distiD = distvalue(query, 'sel_3', ProposalId);
    //------------validation-----

    /////-----==============================================================================================

    $('#txt_10').blur(function () {
        // alert('hi');
        if ($(this).val() < 20) {
            jAlert("Maximum number of building workers Minimum 20");
            // return false;
            $(this).val('');
            AmountDetails('');
        }
        else {
            $(this).val();
        }
    });

    /////-----==============================================================================================

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

    /////-----==============================================================================================

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

    /////-----==============================================================================================
    //-----------hide show validation-------------

    $("#txt_11").removeClass("req rset");
    $("#div_txt_116").hide();
    $('input:radio[name=LicenceOfContractor ]').change(function () {
        if ($('input:radio[name=LicenceOfContractor ]:checked').val() == "Yes") {
            $("#div_txt_116").show();
            $("#txt_11").addClass("req rset");
        }
        else {
            $("#txt_11").removeClass("req rset");
            $("#div_txt_116").hide();

        }
    });

    /////-----==============================================================================================
    //--------------Date Validation----------

    $('#txt_8').datetimepicker({
        format: 'DD-MMM-YYYY'
    }).on('dp.change', function (selected) {

        var date = new Date(selected.date),
           days = parseInt(1);

        if (!isNaN(date.getTime())) {
            var year = parseInt(date.getFullYear()) + 1;
            //date.setDate(date.getFullYear() + 1);

            var month = (1 + date.getMonth()).toString();
            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Juy', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];
            // alert(months[month - 1]);

            $("#txt_9").val(date.getDate() + '-' + months[month - 1] + '-' + year);
            $("#txt_9").datetimepicker({ format: 'DD-MM-YYYY' });

            var months1 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Juy', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];
            var dt = new Date($("#txt_9").val());
            var month1 = (dt.getMonth()).toString();
            var dttt = dt.getDate() + '-' + months1[month1] + '-' + dt.getFullYear();
            var dt2 = new Date(dttt);
            var CurrentDate = new Date()

            if (dt2 < CurrentDate) {
                DpChangeCalculation();
            }
        }
    });

    /////-----==============================================================================================
    /////-----Added by Sushant Jena On Dt:-17-Feb-2020
    $('#txt_9').datetimepicker({
        format: 'DD-MMM-YYYY'
    }).on('dp.change', function (selected) {

        $("#txt_8").val('');
        $("#txt_10").val('');

    });

    /////-----==============================================================================================

    $("#sel_3").change(function () {
       
        var selValue = $('#sel_3').val();

        var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + "";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_4').html('');
                $('#sel_4').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });
    jQuery("input[name='ApplicationFrom']").each(function (i) {
        $("#rad_11").prop("checked", true);
        jQuery(this).attr('disabled', 'disabled');
    });

    /////-----==============================================================================================

    $("#txt_10").focusout(function () {
        DpChangeCalculation();
    });
});

/////-----==============================================================================================

function AmountDetails(amount) {

    //    var months1 = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Juy', 'Aug', 'Sept', 'Oct', 'Nov', 'Dec'];
    //    var dt = new Date($("#txt_9").val());
    //    var month1 = (dt.getMonth()).toString();
    //    var dttt = dt.getDate() + '-' + months1[month1] + '-' + dt.getFullYear();
    //    var dt2 = new Date(dttt);     

    var expiryDate = $("#txt_9").val().split('-');
    var expiryDay = expiryDate[0];
    var expiryMonth = expiryDate[1];
    var expiryYear = expiryDate[2];

    //// Convert DD-MMM-YYYY format date using space as separator.It will support in all browser(Tested for Mozilla,Chrome,IE). 
    var dt2 = new Date(expiryMonth + ' ' + expiryDay + ' ' + expiryYear);
    var CurrentDate = new Date();    

    if (CurrentDate > dt2) {
        amount = parseInt(amount) + ((parseInt(amount) * 25) / 100);
    }

    var strText = "";
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th>Total Amount</th><td><b>" + amount + "/-</b></td></tr></table>"
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(amount);
}

/////-----==============================================================================================

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

/////-----==============================================================================================

function blockvalue(query2, distid, ProposalId) {
    var blockid = "";
    query2 = "select intBlockId from T_LandAndUtility where vchProposalNo=" + ProposalId + ""
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

/////-----==============================================================================================

function fillBlockDataAuto(selValue, setVal) {
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
            $('#sel_4').append($("<option></option>").val('0').html('Select'))
            $.each(r.d, function () {

                if (setVal == this.Value) {
                    $('#sel_4').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                }
            })
        }
    });
}

/////-----==============================================================================================

function getdate(tt) {

    var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var date = new Date(tt);
    var newdate = new Date(date);

    newdate.setDate(newdate.getDate() + 365);

    var dd = newdate.getDate();
    var mm = newdate.getMonth();
    var y = newdate.getFullYear();

    var someFormattedDate = dd + '-' + monthNames[mm] + '-' + y;
    return someFormattedDate;
    $('#txt_9').val(someFormattedDate);
    alert(someFormattedDate);
}

/////-----==============================================================================================

function DpChangeCalculation() {
    //if ((parseInt($("#txt_10").val()) <= 20)) {
    //    AmountDetails(200);
    //}
    //else if ((parseInt($("#txt_10").val()) >= 21) && (parseInt($("#txt_10").val()) <= 50)) {
    //    AmountDetails(500);
    //}
    //else if ((parseInt($("#txt_10").val()) >= 51) && (parseInt($("#txt_10").val()) <= 100)) {
    //    AmountDetails(1000);
    //}
    //else if ((parseInt($("#txt_10").val()) >= 101) && (parseInt($("#txt_10").val()) <= 200)) {
    //    AmountDetails(2000);
    //}
    //else if ((parseInt($("#txt_10").val()) >= 201) && (parseInt($("#txt_10").val()) <= 400)) {
    //    AmountDetails(4000);
    //}
    //else if ((parseInt($("#txt_10").val()) >= 401)) {
    //    AmountDetails(5000);
    //}
    if ((parseInt($("#txt_10").val()) <= 20)) {
        AmountDetails(0);
    }
    else if ((parseInt($("#txt_10").val()) >= 21) && (parseInt($("#txt_10").val()) <= 49)) {
        AmountDetails(0);
    }
    else if ((parseInt($("#txt_10").val()) >= 49) && (parseInt($("#txt_10").val()) <= 50)) {
        AmountDetails(500);
    }
    else if ((parseInt($("#txt_10").val()) >= 51) && (parseInt($("#txt_10").val()) <= 100)) {
        AmountDetails(1000);
    }
    else if ((parseInt($("#txt_10").val()) >= 101) && (parseInt($("#txt_10").val()) <= 200)) {
        AmountDetails(2000);
    }
    else if ((parseInt($("#txt_10").val()) >= 201) && (parseInt($("#txt_10").val()) <= 400)) {
        AmountDetails(4000);
    }
    else if ((parseInt($("#txt_10").val()) >= 401)) {
        AmountDetails(5000);
    }
}

/////-----==============================================================================================

function EditfillBlockDataAuto(selValue, setVal) {    
   
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
            $('#sel_4').append($("<option></option>").val('0').html('Select'))
            $.each(r.d, function () {

                if (setVal == this.Text) {
                    $('#sel_4').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                }
            })
        }
    });
}

/////-----==============================================================================================