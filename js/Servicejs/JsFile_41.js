$(document).ready(function () {

    $("#txt_1").attr("readonly", false);
    $("#rad_51").attr("checked", true);

    /*-------------------------------------------------------------------------------------*/

    $('input:radio[name=TypeOperation]').change(function () {

        if ($('input:radio[name=TypeOperation]:checked').val() === "Operational") {
            $("#txt_80").addClass("req rset");
            $('#lbl_txt_804').find('span').css('display', 'block');
            $("#txt_95").removeClass("req rset");
            $('#lbl_txt_9512').find('span').css('display', 'none');
        }
        else if ($('input:radio[name=TypeOperation]:checked').val() === "Constructional") {

            $("#txt_95").addClass("req rset");
            $('#lbl_txt_9512').find('span').css('display', 'block');
            $("#txt_80").removeClass("req rset");
            $('#lbl_txt_804').find('span').css('display', 'none');

        }
        else {
            $("#txt_80").addClass("req rset");
            $('#lbl_txt_804').find('span').css('display', 'block');
            $("#txt_95").addClass("req rset");
            $('#lbl_txt_9512').find('span').css('display', 'block');

        }
    });

    /*-------------------------------------------------------------------------------------*/

    $("#btnSubmit").attr("disabled", true); //-----for disble button

    /*-------------------------------------------------------------------*/
    ///Added by Sushant during v2.0 Implementation
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

    /*-------------------------------------------------------------------------------------*/

    $('#txt_1').keypress(function (e) {
        var regex = new RegExp("^[a-zA-Z ]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        else {
            e.preventDefault();
            return false;
        }
    });


    var ProposalId = "";
    var FormId = "";

    /*-------------------------------------------------------------------------------------*/
    //----------------AutoFill Dropdwon-------------------------


    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    var urlparam = "";
    for (var i = 0; i < url.length; i++) {
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
    FormId = $('#hdnFormId').val();
    if (ProposalId !== "") {
        var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + "";
        var distiD = distvalue(query, 'sel_2', ProposalId);
        FuncWithCondition(FormId, ProposalId);
        var type = FillProjectType(ProposalId);
    }

    //------------------------------------------

    $('input:radio[name=TypeOfWaterDept]').change(function () {
        if ($('input:radio[name=TypeOfWaterDept]:checked').val() === "IDCO") {
            window.location.href = "WaterAllotmentForm.aspx?FormId=41&ProposalNo=" + ProposalId + "";
        }
    });

    $("#div_fil_141").hide();
    $("#fil_14").removeClass("req rset");
    $("#div_sel_52").hide();
    $("#sel_5").removeClass("reqD rset");
    $("#div_txt_813").hide();
    $("#txt_81").removeClass("req rset");

    /*-------------------------------------------------------------------------------------*/

    $('input:radio[name=QuantityIndented]').change(function () {

        if ($('input:radio[name=QuantityIndented]:checked').val() === "Ground") {
            $("#div_fil_141").show();
            $("#div_sel_52").hide();
            $("#sel_5").removeClass("reqD rset");
            $("#div_txt_813").hide();
            $("#txt_81").removeClass("req rset");
            $('#txt_81').val("");
            $("#sel_5").val("0").change();
        }
        else {
            $("#div_fil_141").hide();
            $("#fil_14").removeClass("req rset");
            $("#div_sel_52").show();
            $("#sel_5").addClass("reqD rset");
            $("#div_txt_813").show();
            $("#txt_81").addClass("req rset");
            $('#fil_14').val('');
            $("#btndownloadfil_14").attr("style", "visibility:hidden");
            $("#btndelfil_14").attr("style", "visibility:hidden");
            $('#hdn_fil_14').val("");
            $('#hdnfil_14').val("");
        }
    });

    /*-------------------------------------------------------------------------------------*/

    $("#div_fil_139").hide();
    $("#fil_13").removeClass("req rset");
    $("#div_sel_1010").hide();
    $("#sel_10").removeClass("reqD rset");
    $("#div_txt_9011").hide();
    $("#txt_90").removeClass("req rset");

    /*-------------------------------------------------------------------------------------*/

    $('input:radio[name=QuantityIndented2]').change(function () {

        if ($('input:radio[name=QuantityIndented2]:checked').val() === "Ground") {
            $("#div_fil_139").show();
            $("#fil_13").addClass("req rset");
            $("#div_sel_1010").hide();
            $("#sel_10").removeClass("reqD rset");
            $("#div_txt_9011").hide();
            $("#txt_90").removeClass("req rset");
            $('#txt_90').val("");
            $("#sel_10").val("0").change();
        }
        else {
            $("#div_fil_139").hide();
            $("#fil_13").removeClass("req rset");
            $("#div_sel_1010").show();
            $("#sel_10").addClass("reqD rset");
            $("#div_txt_9011").show();
            $("#txt_90").addClass("req rset");
            $('#fil_13').val('');
            $("#btndownloadfil_13").attr("style", "visibility:hidden");
            $("#btndelfil_13").attr("style", "visibility:hidden");
            $('#hdn_fil_13').val("");
            $('#hdnfil_13').val("");
        }
    });

    /*-------------------------------------------------------------------------------------*/

    $("#div_sel_1126").hide();
    $("#div_txt_1147").hide();
    $("#div_fil_2215").hide();
    $("#fil_221").removeClass("req rset");
    $("#sel_112").removeClass("reqD rset");
    $("#txt_114").removeClass("req rset");

    /*-------------------------------------------------------------------------------------*/

    $('input:radio[name=ExistingQuantityIndented]').change(function () {

        if ($('input:radio[name=ExistingQuantityIndented]:checked').val() === "Ground") {

            $("#div_sel_1126").hide();
            $("#div_txt_1147").hide();
            $("#div_fil_2215").show();
            $("#fil_221").addClass("req rset");
            $("#sel_112").removeClass("reqD rset");
            $("#txt_114").removeClass("req rset");
            $('#txt_114').val("");
            $("#sel_112").val("0").change();
        }
        else {
            $("#div_sel_1126").show();
            $("#div_txt_1147").show();
            $("#div_fil_2215").hide();
            $("#fil_221").removeClass("req rset");
            $("#sel_112").addClass("reqD rset");
            $("#txt_114").addClass("req rset");
            $('#fil_221').val('');
            $("#btndownloadfil_221").attr("style", "visibility:hidden");
            $("#btndelfil_221").attr("style", "visibility:hidden");
            $('#hdn_fil_221').val("");
            $('#hdnfil_221').val("");
        }
    });

    /*-------------------------------------------------------------------------------------*/

    $("#sel_2").change(function () {

        var selValue = $('#sel_2').val();
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
                $('#sel_3').html('');
                $('#sel_3').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });
    });

    /*-------------------------------------------------------------------------------------*/
    // Begin For Phasing // For Re Phasing // For Reduction // For Renewal

    $("#txt_113").change(function () {
        FuncWithCondition(FormId, ProposalId);
    });

    $("#txt_80").change(function () {
        FuncWithCondition(FormId, ProposalId);
    });

    $("#txt_95").change(function () {
        FuncWithCondition(FormId, ProposalId);
    });

    $('input:radio[name=ExistingCategory]').change(function () {
        FuncWithCondition(FormId, ProposalId);
    });

    /*-------------------------------------------------------------------------------------*/

    $('input:radio[name=ApplicationType]').change(function () {

        if ($('input:radio[name=ApplicationType]:checked').val() === "Existing") {
            $("#txt_112").addClass("req rset");
            $("#txt_113").addClass("req rset");
            $("#fil_112").addClass("req rset");
            $("#fil_113").addClass("req rset");
            $("#div_rad_112").addClass("reqR rset");
            $("#pnl_35").show();
            $("#div_rad_61").show();
            $("#div_rad_6").addClass("reqR rset");
        }
        else {

            $('input:radio[name=ExistingCategory]').attr('checked', false);
            $("#div_rad_61").hide();
            $("#div_rad_6").removeClass("reqR rset");
            $('#txt_112').val("");
            $('#txt_113').val("");
            $('#txt_114').val("");
            $("#sel_112").val("0").change();
            $('#fil_112').val('');
            $('#fil_113').val('');
            $('#fil_221').val('');
            $('input:radio[name=ExistingQuantityIndented]').attr('checked', false);
            $("#btndownloadfil_112").attr("style", "visibility:hidden");
            $("#btndelfil_112").attr("style", "visibility:hidden");
            $("#btndownloadfil_113").attr("style", "visibility:hidden");
            $("#btndelfil_113").attr("style", "visibility:hidden");
            $("#btndownloadfil_221").attr("style", "visibility:hidden");
            $("#btndelfil_221").attr("style", "visibility:hidden");
            $("#txt_112").removeClass("req rset");
            $("#txt_113").removeClass("req rset");
            $("#fil_112").removeClass("req rset");
            $("#fil_113").removeClass("req rset");
            $("#div_rad_112").removeClass("reqR rset");
            $("#fil_221").removeClass("req rset");
            $("#sel_112").removeClass("reqD rset");
            $("#txt_114").removeClass("req rset");
            $("#div_sel_1126").hide();
            $("#div_txt_1147").hide();
            $("#div_fil_2215").hide();
            $("#pnl_35").hide();
        }
        FuncWithCondition(FormId, ProposalId);
    });

    /*-------------------------------------------------------------------------------------*/

    $('#chk_1').click(function () {
        if (document.getElementById('chk_1').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
        FuncWithCondition(FormId, ProposalId);
    });

    // End For Phasing // For Re Phasing // For Reduction // For Renewal


    /*-------------------------------------------------------------------------------------*/
    ///Added by Anil For LetterOfUndertaking Format downlod
    /*-------------------------------------------------------------------*/

    $('#spn_fil_114').each(function () {
        $(this).after('<a href="Enclosure/LetterOfUndertakingFormat.pdf" target="_blank" title="The below letter undertaking should be in the form of an affidavit or on a letter pad of the Industry with Seal and Signature of the Competent Authority." >Letter of undertaking format</a>');
    });

 

    
});


/*-------------------------------------------------------------------------------------*/

function ValidateQTY(idd) {
    var totalReq = $('#txt_80').val();
    var reqVal = $('#' + idd.id).val();
    if (eval(reqVal) > eval(totalReq)) {
        jAlert("Phase wise Quantity can not greater than Quantity indented for operation !!");
        $('#' + idd.id).val('');
    }
}

function TodateValidate(ida) {

}

/*-------------------------------------------------------------------------------------*/

function BindOnChangeFromDate(ida) {
    $("#" + ida.id).on("dp.change", function (e) {
        var frmDate = ida.id.replace("From", "To");
        var prevId = eval(frmDate.replace("Plug_1_To_", "")) - 1;
        var start = new Date($('#Plug_1_To_' + prevId).val());
        var end = new Date($("#" + ida.id).val());
        if (start > end) {
            jAlert("Current Phase From date can not be less than Previous Phase To date !!");
            $("#" + ida.id).val("");
        }
    });
}

/*-------------------------------------------------------------------------------------*/

function BindOnChangeDate(ida) {

    $("#" + ida.id).on("dp.change", function (e) {
        var frmDate = ida.id.replace("To", "From");
        var dtV = new Date();
        if ($('#' + frmDate).val() !== '') {
            $('#' + ida.id).data("DateTimePicker").minDate(getdate($('#' + frmDate).val()));
        }
    });
}

/*-------------------------------------------------------------------------------------*/

function addbuttonclick(idd, tbWhole) {

    var prevId = (eval(idd) - 1);

    if (prevId > 0) {
        if ($('#Plug_1_To_' + prevId).val() === '' || $('#Plug_1_To_' + prevId).val() === 'undefined') {
            return false;
        }
        else {
            $('#Plug_1_From_' + idd).val(getdate($('#Plug_1_To_' + prevId).val()));
        }
    }
}

/*-------------------------------------------------------------------------------------*/

function getdate(tt) {

    var monthNames = [
        "Jan", "Feb", "Mar",
        "Apr", "May", "Jun", "Jul",
        "Aug", "Sep", "Oct",
        "Nov", "Dec"
    ];
    var date = new Date(tt);
    var newdate = new Date(date);
    newdate.setDate(newdate.getDate() + 1);
    var dd = newdate.getDate();
    var mm = newdate.getMonth();
    var y = newdate.getFullYear();
    var someFormattedDate = dd + '-' + monthNames[mm] + '-' + y;
    return someFormattedDate;
}

/*-------------------------------------------------------------------------------------*/

function distvalue(query2, dropid, ProposalId) {
    var distid = "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            distid = r.d;
            if (r.d !== "") {
                $('#' + dropid).val(r.d);
                blockvalue('', r.d, ProposalId);
                $("#" + dropid).attr("enable", "enable");
            }
        }
    });
    return distid;
}

/*-------------------------------------------------------------------------------------*/

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
    return blockid;
}

/*-------------------------------------------------------------------------------------*/

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
            $('#sel_3').html('');
            $('#sel_3').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if (setVal === this.Value) {
                    $('#sel_3').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });

    $("#sel_3").attr("enable", "enable");
}

/*-------------------------------------------------------------------------------------*/

function AmountDetails(FormId, uniamt) {

    var strhdnValue = "";
    var strText = "";
    var strMulty = "";
    var strTotalAmnt = "";
    if (uniamt !== "") {
        uniamt = uniamt;
    }
    else {
        uniamt = 0;
    }
    var serviceamount = $('#hdnTotalAmount1').val();
    $('#hdnTotalAmount').val($('#hdnTotalAmount1').val());
    var strOpeUnit = 0;
    var strConsUnit = 0;
    var strUnit = 0;
    var strUnitVal = 0;
    var strQuantityearlier = 0;

    if ($('input:radio[name=ExistingCategory]:checked').val() === "Enhancement with Phasing" || $('input:radio[name=ExistingCategory]:checked').val() === "Enhancement") {

        if ($('#txt_80').val() !== 'undefined' && $('#txt_80').val() !== "") {
            strOpeUnit = parseFloat($('#txt_80').val());
        }

        if ($('#txt_95').val() !== 'undefined' && $('#txt_95').val() !== "") {
            strConsUnit = parseFloat($('#txt_95').val());
        }

        if ($('#txt_113').val() !== 'undefined' && $('#txt_113').val() !== "") {
            strQuantityearlier = parseFloat($('#txt_113').val());
        }

        strUnit = parseFloat(strOpeUnit + strConsUnit).toFixed(3); // change from toFixed(2)

        if (strQuantityearlier >= strUnit) {
            strUnit = parseFloat(strQuantityearlier - strUnit).toFixed(3); // change from toFixed(2)
        }
        else {
            strUnit = parseFloat(strUnit - strQuantityearlier).toFixed(3); // change from toFixed(2)
        }

        
        if (strUnit !== 'undefined' && strUnit !== "") {
            strMulty = strUnit + "X" + uniamt;
            strUnitVal = strUnit * parseFloat(uniamt);
            strTotalAmnt = Math.round((parseFloat(serviceamount) + parseFloat(strUnitVal)));
        }
        else {
            strMulty = "0 X" + uniamt;
            strTotalAmnt = Math.round(parseFloat(serviceamount));
        }
        $('#hdnTotalAmount').val(strTotalAmnt);
        $('#hdnApplicationFee').val(Math.round(parseFloat(serviceamount)));

    }
    else {

        if ($('#txt_80').val() !== 'undefined' && $('#txt_80').val() !== "") {
            strOpeUnit = parseFloat($('#txt_80').val());
        }

        if ($('#txt_95').val() !== 'undefined' && $('#txt_95').val() !== "") {
            strConsUnit = parseFloat($('#txt_95').val());
        }

        if ($('input:radio[name=ApplicationType]:checked').val() === "Existing") {

            if ($('input:radio[name=ExistingCategory]:checked').val() === "Phasing") {
                strUnit = 0;
            }
            else if ($('input:radio[name=ExistingCategory]:checked').val() === "RePhasing") {
                strUnit = 0;
            }
            else if ($('input:radio[name=ExistingCategory]:checked').val() === "Reduction") {
                strUnit = 0;
            }
            else if ($('input:radio[name=ExistingCategory]:checked').val() === "Renewal") {
                strUnit = 0;
            }
            else if ($('input:radio[name=ExistingCategory]:checked').val() === "Reduction with Phasing") {
                strUnit = 0;
            }
            else if ($('input:radio[name=ExistingCategory]:checked').val() === "Renewal with Phasing") {
                strUnit = 0;
            }
            else {
                strUnit = parseFloat(strOpeUnit + strConsUnit).toFixed(3); // change from toFixed(2)
            }
        }
        else {
            strUnit = parseFloat(strOpeUnit + strConsUnit).toFixed(3); // change from toFixed(2)
        }

        if (strUnit !== 'undefined' && strUnit !== "") {
            strMulty = strUnit + " X " + uniamt;
            strUnitVal = strUnit * parseFloat(uniamt);
            strTotalAmnt = Math.round((parseFloat(serviceamount) + parseFloat(strUnitVal)));
        }
        else {
            strMulty = "0 X" + uniamt;
            strTotalAmnt = Math.round(parseFloat(serviceamount));
        }

        $('#hdnTotalAmount').val(strTotalAmnt);
        $('#hdnApplicationFee').val(Math.round(parseFloat(serviceamount)));
    }
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Per Cusec </th><td><b>" + uniamt + "/-</b></td></tr><tr><th>Amount</th><td width='50%'><b>" + strMulty + "</b></td></tr><tr><th width='50%'>Application Amount</th><td width='50%'><b>" + serviceamount + "</b></td></tr><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + strTotalAmnt + "/-</b></td></tr></table>";
    lblAmount.innerHTML = strText;
}


/*-------------------------------------------------------------------------------------*/

function FuncWithCondition(FormId, ProposalId) {
    var query2 = "select NUM_CONDITION1 from M_SERVICEMASTER_TBL where INT_SERVICEID=" + parseInt(FormId) + "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMapping",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
           
            AmountDetails(FormId, r.d);
        }
    });
}

/*-------------------------------------------------------------------------------------*/

function FillProjectType(ProposalId) {

    var query3 = "select Type from VW_Project_Type where vchProposalNo=" + ProposalId;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query3 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            AddProjectTypeText(r.d);
        }
    });
}

/*-------------------------------------------------------------------------------------*/

function AddProjectTypeText(dt) {
    if (dt !== "") {
        $('#txt_2').val(dt);
        $("#txt_2").attr("disabled", "disabled");
    }
}

/*-------------------------------------------------------------------------------------*/