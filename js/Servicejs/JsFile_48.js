
$(document).ready(function () {

    $('#txt_14').change(function () {
        $('#txt_14').val(numberWithCommas($('#txt_14').val()));
    });

    /*---------------------------------------------------------------------------------*/

    $('#txt_9').change(function () {
        CalculateAmount();      
    });

    /*---------------------------------------------------------------------------------*/

    $('#txt_90').change(function () {
        CalculateAmount();       
    });

    /*---------------------------------------------------------------------------------*/

    $("#txt_11").on("dp.change", function (e) {
        var dates = new Date($('#txt_11').val());
        var end = new Date();
        $('#txt_11').data("DateTimePicker").maxDate(new Date(Date.now() - 86400000));
    });

    /*---------------------------------------------------------------------------------*/

    $("#txt_20").focusout(function (me) {
        debugger;
        if ($("#txt_20").val() == "") {
            return true;
        }
        else {
            var len = $("#txt_20").val().length;
            if (len < 10) {
                jAlert('Mobile number should contain 10 digits!');
                $('#txt_20').val("");
                $('#txt_20').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });

    /*---------------------------------------------------------------------------------*/

    $("#txt_12").on("dp.change", function (e) {
        var dates = new Date($('#txt_12').val());
        $('#txt_12').data("DateTimePicker").minDate(new Date());
    });   

    /*---------------------------------------------------------------------------------*/
    ///// Added by Sushant during v2.0 Implementation
    /*---------------------------------------------------------------------------------*/
    $("#btnDraft").attr("disabled", true);

    $('#Dec_1').click(function () {
        if (document.getElementById('Dec_1').checked) {
            $("#btnDraft").attr("disabled", false);
        }
        else {
            $("#btnDraft").attr("disabled", true);
        }        
    });

    /*---------------------------------------------------------------------------------*/
    var ProposalId = "";
    $("#btnSubmit").attr("disabled", true); //-----for disble button
    $('#Dec_1').click(function () {
        debugger;
        if (document.getElementById('Dec_1').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });

    /*---------------------------------------------------------------------------------*/

    $("#rad_20").prop('checked', 'checked');
    FillMunicipal("#rad_20");

    $('#div_rad_311').hide();
    $('#div_rad_3').removeClass("reqR rset");
    $('#div_txt_1019').hide();
    $('#div_txt_1122').hide();
    $('#div_txt_1223').hide();
    //$('#div_txt_1419').hide();
    $('#div_txt_1116').hide();
    $('#div_txt_1217').hide();

    /*---------------------------------------------------------------------------------*/
    ////Autofill District Dropdown and Auto select district belongs to land district of the proposal.
    /*---------------------------------------------------------------------------------*/   
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        ProposalId = urlparam[1];
    }

    ProposalId = $('#hdnProposalNo').val();
    if (ProposalId != "") {
        var query = "SELECT intDistrictId FROM T_LandAndUtility WHERE vchProposalNo=" + ProposalId + ""
        var distiD = distvalue(query, 'sel_1', ProposalId);
    }

    /*---------------------------------------------------------------------------------*/

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

    /*---------------------------------------------------------------------------------*/

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

    /*---------------------------------------------------------------------------------*/

    $("#sel_1").change(function () {
        debugger;
        var selValue = $('#sel_1').val();
        var dtLevel = "3";
        var query1 = "select intTehesilId as COLUMN_NAME_VALUE , vchTehesilName as COLUMN_NAME_TEXT from M_Tehesil where intDistrictId=" + parseInt(selValue) + " order by vchTehesilName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_2').html('');
                $('#sel_2').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_2').append($("<option></option>").val(this.Value).html(this.Text));

                })
            }
        });
    });

    /*---------------------------------------------------------------------------------*/

    $("#sel_4").change(function () {
        debugger;
        var selValue = $('#sel_4').val();
        var dtLevel = "3";
        var query1 = "select intTehesilId as COLUMN_NAME_VALUE , vchTehesilName as COLUMN_NAME_TEXT from M_Tehesil where intDistrictId=" + parseInt(selValue) + " order by vchTehesilName asc";

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
    
    /*---------------------------------------------------------------------------------*/

    $('input:radio[name=SituationOfLand]').change(function () {         
        FillMunicipal(this);
    });

    /*---------------------------------------------------------------------------------*/

    $('#div_txt_1115').hide();
    $('#div_txt_1216').hide();

    /*---------------------------------------------------------------------------------*/

    $('input:radio[name=LandUsed]').change(function () {
        if ($('input:radio[name=LandUsed]:checked').val() == "Already Used") {
            $('#div_txt_1115').show();
            $('#div_txt_1216').hide();
        }

        if ($('input:radio[name=LandUsed]:checked').val() == "Intend to be used") {
            $('#div_txt_1115').hide();
            $('#div_txt_1216').show();
        }
    });

    /*---------------------------------------------------------------------------------*/

    $('input:radio[name=AreaSituated]').change(function () {

        if ($('input:radio[name=AreaSituated]:checked').val() == "Yes") {
            $('#div_rad_311').show();
            $('#div_rad_3').addClass("reqR rset");
        }

        if ($('input:radio[name=AreaSituated]:checked').val() == "No") {
            $("#div_rad_4").removeClass("reqD rset");
            $('#div_rad_311').hide();
            $('#div_rad_3').removeClass("reqR rset");
        }
    });
});

/*---------------------------------------------------------------------------------*/
/////Convert the amount in Indian Rupees Format.
/*---------------------------------------------------------------------------------*/
function numberWithCommas(x) {
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

/*---------------------------------------------------------------------------------*/
/////Fill Municipality,Municipality Corporation,NAC in the dropdown list.
/*---------------------------------------------------------------------------------*/
function FillMunicipal(selVal) {

    var type = "";
    var selValue = $(selVal).val();

    if (selValue == "Municipal Corporation") {
        type = "MC";
    }
    else if (selValue == "Municipality") {
        type = "M";
    }
    else if (selValue == "NAC") {
        type = "N";
    }

    //    if (selValue == "Devloping Area") {
    ////        $('#div_sel_713').show();
    ////        $('#div_txt_1514').hide();
    ////      //  type = "DA";
    //    }
    //    else {

    //    }

    //    if (selValue =="Devloping Area") {
    //        $('#div_sel_713').show();
    //        $('#div_txt_1514').hide();
    //    }

    if (selValue != "Rural Area" && selValue != "Developing Area") {

        $('#div_sel_712').show();
        $('#div_txt_1513').hide();

        var query1 = "SELECT intId AS COLUMN_NAME_VALUE , vchName AS COLUMN_NAME_TEXT FROM T_MunciplDetails WHERE vchType='" + type + "' AND bitDeletedFlag=0 ORDER BY vchName ASC";
        var ob = {};
        ob.query = query1;

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: JSON.stringify(ob),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {

                $('#lbl_sel_712').html(selValue);
                $('#sel_7').html('');
                $('#sel_7').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                })
            },
            error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
            }
        });
    }
    else {
        $('#lbl_txt_1513').html(selValue);
        //        $('#div_sel_712').hide();
        //        $('#div_txt_1514').show();
        $('#div_sel_712').hide();
        $('#div_txt_1513').show();
    }
}

/*---------------------------------------------------------------------------------*/
/////Fill District
/*---------------------------------------------------------------------------------*/
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
                $("#sel_1").attr("disabled", true);
                distid = r.d;
                $('#' + dropid).val(r.d);
                fillTehesilDataAuto(distid);
            }
        }
    });
    return distid;
}

/*---------------------------------------------------------------------------------*/
/////Fill Tehesil against District.
/*---------------------------------------------------------------------------------*/
function fillTehesilDataAuto(selValue) {

    var query2 = "SELECT intTehesilId AS COLUMN_NAME_VALUE , vchTehesilName AS COLUMN_NAME_TEXT FROM M_Tehesil WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchTehesilName ASC";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_2').html('');
            $('#sel_2').append($("<option></option>").val('0').html('Select'))
            $.each(r.d, function () {
                $('#sel_2').append($("<option></option>").val(this.Value).html(this.Text));
            })
        }
    });
}

/*---------------------------------------------------------------------------------*/
/////Amount Calculation.
/*---------------------------------------------------------------------------------*/
function CalculateAmount() {

    var convertLand = $('#txt_9').val();
    var areaLand = $('#txt_90').val();

    var convertLand1 = 0;
    var areaLand1 = 0;

    if (convertLand == "") {
        convertLand1 = 0;
    }
    else {
        convertLand1 = convertLand;
    }

    if (areaLand == "") {
        areaLand1 = 0;
    }
    else {
        areaLand1 = areaLand;
    }

    if (eval(convertLand1) == eval(areaLand1)) {
        AmountValidate(0);
    }
    else if (eval(convertLand1) < eval(areaLand1)) {
        AmountValidate(20);
    }

    if (convertLand != "") {
        if (eval(convertLand) > eval(areaLand)) {
            AmountValidate(0);
            jAlert("<strong>Area to be converted can not be greater than total area of plot !!</strong>");
            $('#txt_90').val('');
            return false;
        }
    }
}

/*---------------------------------------------------------------------------------*/
/////Display Calculated Amount in Payment Details Section.
/*---------------------------------------------------------------------------------*/
function AmountValidate(subDivFee) {

    var strText = "";
    var strTotalAmnt = 30 + parseInt(subDivFee);

    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Application Fee</th><td width='50%'><b>10/-</tr><tr><th width='50%'>Notice Fee</th><td width='50%'><b>20/-</b></td></tr><tr><th width='50%'>Sub-division of plot Fee</th><td width='50%'><b>" + subDivFee + "/-</b></td></tr><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + strTotalAmnt + "/-</b></td></tr></table>"
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(strTotalAmnt);
}

/*---------------------------------------------------------------------------------*/