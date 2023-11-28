$(document).ready(function () {

    var a = 0;
    var b = 0;
    var c = 0;
    var d = 0;
    $('#h2_99').find('small').show();
   // AmountValidate(a, b, c, d);
    var ProposalId = "";
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
        var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + ""
        var distiD = distvalue(query, 'sel_2', ProposalId);
    }



    //--------------------------------------------
    $('#lbl_sel_50').find('span').css('display', 'none');
    $('#sel_5').removeClass("reqD rset");
    $('#lbl_txt_701').find('span').css('display', 'none');
    $('#txt_70').removeClass("req rset");
    $('#lbl_sel_812').find('span').css('display', 'none');
    $('#sel_81').removeClass("reqD rset");


    $('#lbl_sel_60').find('span').css('display', 'none');
    $('#sel_6').removeClass("reqD rset");
    $('#lbl_txt_711').find('span').css('display', 'none');
    $('#txt_71').removeClass("req rset");
    $('#lbl_sel_802').find('span').css('display', 'none');
    $('#sel_80').removeClass("reqD rset");

    $('#lbl_sel_70').find('span').css('display', 'none');
    $('#sel_7').removeClass("reqD rset");
    $('#lbl_txt_721').find('span').css('display', 'none');
    $('#txt_72').removeClass("req rset");
    $('#lbl_sel_792').find('span').css('display', 'none');
    $('#sel_79').removeClass("reqD rset");

    //    $('#lbl_sel_60').find('span').css('display', 'none');
    //    $('#sel_6').removeClass("reqD rset");
    //    $('#lbl_txt_711').find('span').css('display', 'none');
    //    $('#txt_71').removeClass("req rset");
    //    $('#lbl_sel_802').find('span').css('display', 'none');
    //    $('#sel_80').removeClass("reqD rset");

    $('#lbl_sel_770').find('span').css('display', 'none');
    $('#sel_77').removeClass("reqD rset");
    $('#lbl_txt_731').find('span').css('display', 'none');
    $('#txt_73').removeClass("req rset");
    $('#lbl_sel_782').find('span').css('display', 'none');
    $('#sel_78').removeClass("reqD rset");

    //-------------------------------




    $("#plugin_2_Total_1").prop("readonly", true);
    $("#plugin_2_Total_2").prop("readonly", true);
    $("#plugin_2_Total_3").prop("readonly", true);
    $("#plugin_2_Total_4").prop("readonly", true);
    //----------------------------------
    $("#plugin_2_Current_1").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_1').val();
        var txt_32 = $('#plugin_2_Proposed_1').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_1').value = result;
            $("#plugin_2_Total_1").prop("readonly", true);
        }
    });

    $("#plugin_2_Proposed_1").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_1').val();
        var txt_32 = $('#plugin_2_Proposed_1').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_1').value = result;
            $("#plugin_2_Total_1").prop("readonly", true);
        }
    });

    $("#plugin_2_Current_2").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_2').val();
        var txt_32 = $('#plugin_2_Proposed_2').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_2').value = result;
            $("#plugin_2_Total_2").prop("readonly", true);
        }
    });

    $("#plugin_2_Proposed_2").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_2').val();
        var txt_32 = $('#plugin_2_Proposed_2').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_2').value = result;
            $("#plugin_2_Total_2").prop("readonly", true);
        }
    });

    $("#plugin_2_Current_3").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_3').val();
        var txt_32 = $('#plugin_2_Proposed_3').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_3').value = result;
            $("#plugin_2_Total_3").prop("readonly", true);
        }
    });

    $("#plugin_2_Proposed_3").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_3').val();
        var txt_32 = $('#plugin_2_Proposed_3').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_3').value = result;
            $("#plugin_2_Total_3").prop("readonly", true);
        }
    });

    $("#plugin_2_Current_4").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_4').val();
        var txt_32 = $('#plugin_2_Proposed_4').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_4').value = result;
            $("#plugin_2_Total_4").prop("readonly", true);
        }
    });

    $("#plugin_2_Proposed_4").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_4').val();
        var txt_32 = $('#plugin_2_Proposed_4').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_4').value = result;
            $("#plugin_2_Total_4").prop("readonly", true);
        }
    });
    //---------------------------

    WeightFill();
    MeasuresFill();
    InstrumentFill();
    MeasuringInstrumentFill();
    //---------------------DefaultControlHide----------------------
    //    $('#div_sel_11').hide();
    //    $('#div_txt_12').hide();
    //    $('#div_txt_23').hide();
    $('#sel_1').removeClass('reqD rset');
    $('#div_sel_11').hide();
    $('#txt_1').removeClass('req rset');
    $('#div_txt_12').hide();
    $('#txt_2').removeClass('req rset');
    $('#div_txt_23').hide();
    //------------------

    //----------------------SameAs Show---------------------
    $('#txt_7').datetimepicker({
        format: 'DD-MMM-YYYY'
    }).on('dp.show', function () {
        return $(this).data('DateTimePicker').maxDate(new Date());
    });


    //-------------------End------------------

    //------------------SubmitButton Active----------------
    $("#btnSubmit").attr("disabled", true); //-----for disble button
    $('#Dec_1').click(function () {
        if (document.getElementById('Dec_1').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });
    //---------------------End-----------------------
    $('input:radio[name=AppliedPreviouslyLicences]').change(function () {
        debugger;
        if ($('input:radio[name=AppliedPreviouslyLicences]:checked').val() == "Yes") {
            $('#div_sel_11').show();
            $('#sel_1').addClass('reqD rset');
            $('#txt_1').removeClass('req rset');
            $('#div_txt_12').hide();
            $('#sel_1').val(0);

            $("#plugin_2_Current_1").val("0");
            $("#plugin_2_Current_2").val("0");
            $("#plugin_2_Current_3").val("0");
            $("#plugin_2_Current_4").val("0");
            $("#plugin_2_Current_1").prop("readonly", false);
            $("#plugin_2_Current_2").prop("readonly", false);
            $("#plugin_2_Current_3").prop("readonly", false);
            $("#plugin_2_Current_4").prop("readonly", false);
            $("#plugin_2_Proposed_1").val("");
            $("#plugin_2_Proposed_2").val("");
            $("#plugin_2_Proposed_3").val("");
            $("#plugin_2_Proposed_4").val("");

            $("#plugin_2_Total_1").val("");
            $("#plugin_2_Total_2").val("");
            $("#plugin_2_Total_3").val("");
            $("#plugin_2_Total_4").val("");

        }
        else {
            $('#sel_1').removeClass('reqD rset');
            $('#div_sel_11').hide();
            $('#txt_1').removeClass('req rset');
            $('#div_txt_12').hide();
            $('#txt_2').removeClass('req rset');
            $('#div_txt_23').hide();


        }
    });
    $('#fil_3').removeClass('req rset');
    $('#div_fil_32').hide();

    $('input:radio[name=Monogram]').change(function () {
        debugger;
        if ($('input:radio[name=Monogram]:checked').val() == "Yes") {
            $('#div_fil_32').show();
            $('#fil_3').addClass('req rset');

        }
        else {
            $('#fil_3').removeClass('req rset');
            $('#div_fil_32').hide();


        }
    });
    $('#txt_8').removeClass('req rset');
    $('#div_txt_87').hide();

    $('input:radio[name=IsEnergyConnection]').change(function () {
        debugger;
        if ($('input:radio[name=IsEnergyConnection]:checked').val() == "Yes") {
            $('#div_txt_87').show();
            $('#txt_8').addClass('req rset');

        }
        else {
            $('#txt_8').removeClass('req rset');
            $('#div_txt_87').hide();


        }
    });
    $("#txt_4").focusout(function (me) {
        debugger;
        if ($("#txt_4").val() == "") {
            return true;
        }
        else {
            var len = $("#txt_4").val().length;
            if (len < 10) {
                jAlert('Mobile number should contain 10 digits!');
                $('#txt_4').val("");
                $('#txt_4').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });


    $("#plugin_2_Total_1").prop("readonly", true);
    $("#plugin_2_Total_2").prop("readonly", true);
    $("#plugin_2_Total_3").prop("readonly", true);
    $("#plugin_2_Total_4").prop("readonly", true);
    //----------------------------------
    $("#plugin_2_Current_1").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_1').val();
        var txt_32 = $('#plugin_2_Proposed_1').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_1').value = result;
            $("#plugin_2_Total_1").prop("readonly", true);
        }
    });

    $("#plugin_2_Proposed_1").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_1').val();
        var txt_32 = $('#plugin_2_Proposed_1').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_1').value = result;
            $("#plugin_2_Total_1").prop("readonly", true);
        }
    });

    $("#plugin_2_Current_2").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_2').val();
        var txt_32 = $('#plugin_2_Proposed_2').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_2').value = result;
            $("#plugin_2_Total_2").prop("readonly", true);
        }
    });

    $("#plugin_2_Proposed_2").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_2').val();
        var txt_32 = $('#plugin_2_Proposed_2').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_2').value = result;
            $("#plugin_2_Total_2").prop("readonly", true);
        }
    });

    $("#plugin_2_Current_3").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_3').val();
        var txt_32 = $('#plugin_2_Proposed_3').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_3').value = result;
            $("#plugin_2_Total_3").prop("readonly", true);
        }
    });

    $("#plugin_2_Proposed_3").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_3').val();
        var txt_32 = $('#plugin_2_Proposed_3').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_3').value = result;
            $("#plugin_2_Total_3").prop("readonly", true);
        }
    });

    $("#plugin_2_Current_4").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_4').val();
        var txt_32 = $('#plugin_2_Proposed_4').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_4').value = result;
            $("#plugin_2_Total_4").prop("readonly", true);
        }
    });

    $("#plugin_2_Proposed_4").change(function () {
        debugger;
        var txt_31 = $('#plugin_2_Current_4').val();
        var txt_32 = $('#plugin_2_Proposed_4').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32);
        if (!isNaN(result)) {
            document.getElementById('plugin_2_Total_4').value = result;
            $("#plugin_2_Total_4").prop("readonly", true);
        }
    });
    //---------------------------
    $('#chk_10').click(function () {
        debugger;

        var vrCate1 = $(this).val();

        if (document.getElementById('chk_10').checked) {
            $("#chk_12").attr("disabled", true);
            $("#chk_12").attr("checked", false);
            MandWeigth();
            if (document.getElementById('chk_11').checked) {
                MandMeasure();
            }

        }
        else {
            if (document.getElementById('chk_11').checked) {
                $("#chk_12").attr("disabled", true);
                $("#chk_12").attr("checked", false);
                MandMeasure();
                //                if (document.getElementById('chk_10').checked) {
                NonMandWeigth();
                // MandWeigth();
                //                }
            }
            else {
                $("#chk_12").attr("disabled", true);
                $('#chk_12').prop('disabled', false);
                NonMandMeasure();
                NonMandWeigth();
            }
        }
    });

    $('#chk_11').click(function () {
        debugger;
        var vrCate = $(this).val();

        if (document.getElementById('chk_11').checked) {
            $("#chk_12").attr("disabled", true);
            $("#chk_12").attr("checked", false);
            MandMeasure();
            if (document.getElementById('chk_10').checked) {
                MandWeigth();
            }

        }
        else {
            if (document.getElementById('chk_10').checked) {
                $("#chk_12").attr("disabled", true);
                $("#chk_12").attr("checked", false);
                MandWeigth();
                NonMandMeasure();
            }
            else {
                $("#chk_12").attr("disabled", true);

                $('#chk_12').prop('disabled', false);
                NonMandMeasure();
                NonMandWeigth();
            }

        }
    });
    $("#sel_2").change(function () {
        debugger;
        //        alert('hi');
        var selValue = $('#sel_2').val();
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
                $('#sel_3').html('');
                $('#sel_3').append($("<option></option>").val('0').html('--Select--'))
                $.each(r.d, function () {
                    $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });

    $('#chk_12').click(function () {
        debugger;
        $("#chk_10").attr("checked", false);
        $("#chk_11").attr("checked", false);
        NonMandMeasure();
        NonMandWeigth();
    });


    $("#sel_1").change(function () {
        debugger;
        if ($(this).val() == "Approved") {
            $('#div_txt_23').show();
            $('#txt_2').addClass('req rset');
            $('#div_txt_12').show();
            $('#txt_1').addClass('req rset');

            $('#plugin_2_Current_1').addClass('req rset');
            $('#plugin_2_Current_2').addClass('req rset');
            $('#plugin_2_Current_3').addClass('req rset');
            $('#plugin_2_Current_4').addClass('req rset');

            $('#plugin_2_Proposed_1').addClass('req rset');
            $('#plugin_2_Proposed_2').addClass('req rset');
            $('#plugin_2_Proposed_3').addClass('req rset');
            $('#plugin_2_Proposed_4').addClass('req rset');

            $("#plugin_2_Current_1").prop("readonly", false);
            $("#plugin_2_Current_2").prop("readonly", false);
            $("#plugin_2_Current_3").prop("readonly", false);
            $("#plugin_2_Current_4").prop("readonly", false);

        } else {
            $('#txt_2').removeClass('req rset');
            $('#txt_1').removeClass('req rset');
            $('#div_txt_23').hide();
            $('#div_txt_12').hide();

            $('#plugin_2_Current_1').removeClass('req rset');
            $('#plugin_2_Current_2').removeClass('req rset');
            $('#plugin_2_Current_3').removeClass('req rset');
            $('#plugin_2_Current_4').removeClass('req rset');

            $('#plugin_2_Proposed_1').addClass('req rset');
            $('#plugin_2_Proposed_2').addClass('req rset');
            $('#plugin_2_Proposed_3').addClass('req rset');
            $('#plugin_2_Proposed_4').addClass('req rset');

            $("#plugin_2_Current_1").prop("readonly", true);
            $("#plugin_2_Current_2").prop("readonly", true);
            $("#plugin_2_Current_3").prop("readonly", true);
            $("#plugin_2_Current_4").prop("readonly", true);
        }
    });
    $("#sel_5").change(function () {
        FillWeightUnit();

    });
    //-------------------------------------------------------------------




    //----------------------------------------------------------------
    //----------------Calculation and static Measuring instrument denomination--------
       $("#sel_6").change(function () {
        FillMeasureUnit();
    //        if ($("#sel_6").val() == "36") {
    //            $("#txt_71").removeClass('req rset');
    //            $("#div_txt_711").hide();
    //            $("#lbl_sel_802").text(" Accuracy Class");

    //        }
    //        else if ($("#sel_6").val() == "37") {
    //            $("#txt_71").removeClass('req rset');
    //            $("#div_txt_711").hide();
    //            $("#lbl_sel_802").text(" Measure Unit");
    //        }
    //        else if ($("#sel_6").val() == "38") {
    //            $("#txt_71").removeClass('req rset');
    //            $("#div_txt_711").hide();
    //            $("#lbl_sel_802").text(" Measure Unit");
    //        }
    //        else {

    //            $("#div_txt_711").show();
    //            $("#txt_71").addClass('req rset');
    //            $("#lbl_sel_802").text(" Measure Unit");

    //        }
        });

    //    $("#sel_80").change(function () {

    //        if ($("#sel_80").val() == "Class-I") {
    //            $("#txt_71").val(1);
    //        }
    //        else if ($("#sel_80").val() == "Class-II") {
    //            $("#txt_71").val(2);
    //        }
    //        else if ($("#sel_80").val() == "Class-III") {
    //            $("#txt_71").val(3);
    //        }
    //        else if ($("#sel_80").val() == "0.5Meters") {
    //            $("#txt_71").val(0.5);
    //        }
    //        else if ($("#sel_80").val() == "1Meters") {
    //            $("#txt_71").val(1);
    //        }
    //        else if ($("#sel_80").val() == "20Meters") {
    //            $("#txt_71").val(20);
    //        }
    //        else if ($("#sel_80").val() == "30Meters") {
    //            $("#txt_71").val(30);
    //        }

    //    });
    $("#sel_7").change(function () {
        FillWeighingUnit();

    });
    $("#sel_77").change(function () {
        FillMeasuringUnit();
    });
    //        if ($("#sel_77").val() == "10") {
    //            $("#txt_73").removeClass('req rset');
    //            $("#div_txt_731").hide();

    //        }
    //        else if ($("#sel_77").val() == "39") {
    //            $("#txt_73").removeClass('req rset');
    //            $("#div_txt_731").hide();

    //        }
    //        else if ($("#sel_77").val() == "13") {
    //            $("#txt_73").removeClass('req rset');
    //            $("#div_txt_731").hide();

    //        }
    //        else if ($("#sel_77").val() == "14") {
    //            $("#txt_73").removeClass('req rset');
    //            $("#div_txt_731").hide();

    //        }
    //        else if ($("#sel_77").val() == "15") {
    //            $("#txt_73").removeClass('req rset');
    //            $("#div_txt_731").hide();

    //        }
    //        else if ($("#sel_77").val() == "16") {
    //            $("#txt_73").removeClass('req rset');
    //            $("#div_txt_731").hide();

    //        }
    //        else if ($("#sel_77").val() == "17") {
    //            $("#txt_73").removeClass('req rset');
    //            $("#div_txt_731").hide();

    //        }
    //        else {

    //            $("#div_txt_731").show();
    //            $("#txt_71").addClass('req rset');

    //        }

    //    });
    //    $("#sel_78").change(function () {

    //        if ($("#sel_78").val() == "Units") {
    //            $("#txt_73").val(1);
    //        }
    //        else if ($("#sel_78").val() == "Units") {
    //            $("#txt_73").val(1);
    //        }
    //        else if ($("#sel_78").val() == "Units") {
    //            $("#txt_73").val(1);
    //        }
    //        else if ($("#sel_78").val() == "Units") {
    //            $("#txt_73").val(1);
    //        }
    //        else if ($("#sel_78").val() == "100Milliliters") {
    //            $("#txt_73").val(100);
    //        }
    //        else if ($("#sel_78").val() == "30Milliliters") {
    //            $("#txt_73").val(30);
    //        }
    //        else if ($("#sel_78").val() == "60Milliliters") {
    //            $("#txt_73").val(60);
    //        }
    //        else if ($("#sel_78").val() == "Units") {
    //            $("#txt_73").val(1);
    //        }
    //        else if ($("#sel_78").val() == "Units") {
    //            $("#txt_73").val(1);
    //        }
});

    //-------------------calculation-------------
//    $("#txt_55").keyup(function () {
//        debugger;
//        //function GetCalculationUnit(selValueCate, denomination, selValueUnit, quantity) 

//        GetCalculationUnit($('#sel_5').val(), $('#txt_70').val(), $('#sel_81').val(), $('#txt_55').val(), 'W');


//    });
//    $("#txt_70").keyup(function () {
//        debugger;
//        //function GetCalculationUnit(selValueCate, denomination, selValueUnit, quantity) 

//        a = GetCalculationUnit($('#sel_5').val(), $('#txt_70').val(), $('#sel_81').val(), $('#txt_55').val(), 'W');

//    });
//    $("#txt_71").change(function () {
//        debugger;
//        //function GetCalculationUnit(selValueCate, denomination, selValueUnit, quantity) 

//        GetCalculationUnit($('#sel_6').val(), $('#txt_71').val(), $('#sel_80').val(), $('#txt_56').val(), 'M');

//    });
//    $("#txt_56").change(function () {
//        debugger;
//        //function GetCalculationUnit(selValueCate, denomination, selValueUnit, quantity) 

//        GetCalculationUnit($('#sel_6').val(), $('#txt_71').val(), $('#sel_80').val(), $('#txt_56').val(), 'M');
//    });
//    $("#txt_72").change(function () {
//        debugger;
//        //function GetCalculationUnit(selValueCate, denomination, selValueUnit, quantity) 

//        GetCalculationUnit($('#sel_7').val(), $('#txt_72').val(), $('#sel_79').val(), $('#txt_57').val(), 'WI');

//    });
//    $("#txt_57").change(function () {
//        debugger;
//        //function GetCalculationUnit(selValueCate, denomination, selValueUnit, quantity) 

//        GetCalculationUnit($('#sel_7').val(), $('#txt_72').val(), $('#sel_79').val(), $('#txt_57').val(), 'WI');
//    });
//    $("#txt_76").change(function () {
//        debugger;
//        //function GetCalculationUnit(selValueCate, denomination, selValueUnit, quantity) 

//        GetCalculationUnit($('#sel_77').val(), $('#txt_73').val(), $('#sel_78').val(), $('#txt_76').val(), 'MI');

//    });
//    $("#txt_73").change(function () {
//        debugger;
//        //function GetCalculationUnit(selValueCate, denomination, selValueUnit, quantity) 

//        GetCalculationUnit($('#sel_77').val(), $('#txt_73').val(), $('#sel_78').val(), $('#txt_76').val(), 'MI');
//    });
//$("#sel_2").change(function () {
//    debugger;
//    alert('hi');
//    var selValue = $('#sel_2').val();
//    var dtSource = "M_ADM_LocationDetails";
//    var dtValue = "intLevelDetailId";
//    var dtText = "nvchLevelName";
//    var dtLevel = "3";
//    var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

//    $.ajax({
//        type: "POST",
//        url: "FormView.aspx/FillDemographyData",
//        data: "{'query':'" + query1 + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (r) {
//            $('#sel_3').html('');
//            $('#sel_3').append($("<option></option>").val('0').html('--Select--'))
//            $.each(r.d, function () {
//                $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
//            })
//        }
//    });



function distvalue(query2, dropid, ProposalId) {
    debugger;
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
    debugger;
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
    $("#sel_3").attr("disabled", true);
    return blockid;
}

function fillBlockDataAuto(selValue, setVal) {
    debugger;
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
            $('#sel_3').append($("<option></option>").val('0').html('--Select--'))
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

function MeasuresFill() {
    debugger;
    var query2 = "select intCategoryId as COLUMN_NAME_VALUE , vchCategoryName as COLUMN_NAME_TEXT from M_Weight_CATEGORY where IntDeletedFlag=0 and vchType='Measure' order by vchCategoryName asc";
    var ob = {};
    ob.query = query2;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_6').html('');
            $('#sel_6').append($("<option></option>").val('0').html('--Select--'))
            $.each(r.d, function () {
                $('#sel_6').append($("<option></option>").val(this.Value).html(this.Text));
               
            })
        },
        error: function (xhr, status, error) {
                var err = eval("(" + xhr.responseText + ")");
                //alert(err.Message);
            }
    });


}
function WeightFill() {

    var query2 = "select intCategoryId as COLUMN_NAME_VALUE , vchCategoryName as COLUMN_NAME_TEXT from M_Weight_CATEGORY where IntDeletedFlag=0 and vchType='Weight' order by vchCategoryName asc";
    var ob = {};
    ob.query = query2;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_5').html('');
            $('#sel_5').append($("<option></option>").val('0').html('--Select--'))
            $.each(r.d, function () {
                $('#sel_5').append($("<option></option>").val(this.Value).html(this.Text));

            })
        }
    });


}
function InstrumentFill() {

    var query2 = "select intCategoryId as COLUMN_NAME_VALUE , vchCategoryName as COLUMN_NAME_TEXT from M_Weight_CATEGORY where IntDeletedFlag=0 and vchType='WInstrument' order by vchCategoryName asc";
    var ob = {};
    ob.query = query2;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_7').html('');
            $('#sel_7').append($("<option></option>").val('0').html('--Select--'))
            $.each(r.d, function () {
                $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));

            })
        }
    });


}
function MeasuringInstrumentFill() {

    var query2 = "select intCategoryId as COLUMN_NAME_VALUE , vchCategoryName as COLUMN_NAME_TEXT from M_Weight_CATEGORY where IntDeletedFlag=0 and vchType='MInstrument' order by vchCategoryName asc";
    var ob = {};
    ob.query = query2;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_77').html('');
            $('#sel_77').append($("<option></option>").val('0').html('--Select--'))
            $.each(r.d, function () {
                $('#sel_77').append($("<option></option>").val(this.Value).html(this.Text));

            })
        }
    });


}
function FillWeightUnit() {
    var selValue = $('#sel_5').val();

    var query2 = "select Distinct vchDenomination as COLUMN_NAME_VALUE , vchDenomination as COLUMN_NAME_TEXT  from M_IIMS_LM_WEIGHTMEASURE where bitStatus=0 and intCategoryId=" + selValue + " order by vchDenomination asc";
    var ob = {};
    ob.query = query2;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_81').html('');
            $('#sel_81').append($("<option></option>").val('0').html('--Select--'))
            $.each(r.d, function () {
                $('#sel_81').append($("<option></option>").val(this.Value).html(this.Text));

            })
        }
    });


}
function FillMeasureUnit() {
    var selValue = $('#sel_6').val();
    //var query1="select intWeightMeasureId as COLUMN_NAME_VALUE , 'Up to '+convert(varchar(50),decToWeight)+vchDenomination as COLUMN_NAME_TEXT from M_IIMS_LM_WEIGHTMEASURE where bitStatus=0 and intCategoryId="+ selValue + " order by vchDenomination asc"
    var query2 = "select Distinct vchDenomination as COLUMN_NAME_VALUE , vchDenomination as COLUMN_NAME_TEXT  from M_IIMS_LM_WEIGHTMEASURE where bitStatus=0 and intCategoryId=" + selValue + " order by vchDenomination asc";
    var ob = {};
    ob.query = query2;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_80').html('');
            $('#sel_80').append($("<option></option>").val('0').html('--Select--'))
            $.each(r.d, function () {
                $('#sel_80').append($("<option></option>").val(this.Value).html(this.Text));

            })
        }
    });


}
function FillWeighingUnit() {
    var selValue = $('#sel_7').val();

    var query2 = "select Distinct vchDenomination as COLUMN_NAME_VALUE , vchDenomination as COLUMN_NAME_TEXT  from M_IIMS_LM_WEIGHTMEASURE where bitStatus=0 and intCategoryId=" + selValue + " order by vchDenomination asc";
    var ob = {};
    ob.query = query2;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_79').html('');
            $('#sel_79').append($("<option></option>").val('0').html('--Select--'))
            $.each(r.d, function () {
                $('#sel_79').append($("<option></option>").val(this.Value).html(this.Text));

            })
        }
    });


}
function FillMeasuringUnit() {
    var selValue = $('#sel_77').val();

    var query2 = "select Distinct vchDenomination as COLUMN_NAME_VALUE , vchDenomination as COLUMN_NAME_TEXT  from M_IIMS_LM_WEIGHTMEASURE where bitStatus=0 and intCategoryId=" + selValue + " order by vchDenomination asc";
    var ob = {};
    ob.query = query2;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_78').html('');
            $('#sel_78').append($("<option></option>").val('0').html('--Select--'))
            $.each(r.d, function () {
                $('#sel_78').append($("<option></option>").val(this.Value).html(this.Text));

            })
        }
    });


}
var a, b, c, d;
function  GetCalculationUnit(selValueCate, denomination, selValueUnit, quantity,vartype) {
//    var selValueCate = $('#sel_5').val();
//    var denomination = $('#txt_70').val();
//    var selValueUnit = $('#sel_81').val();
//    var quantity = $('#txt_55').val();
    
    var query2 = "select [dbo].[GET_LITRE_PRICE](" + selValueCate + "," + denomination + "," + quantity + ",'" + selValueUnit + "')";
    var ob = {};
    ob.query = query2;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/GetWeightCalculation",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            //            $('#sel_78').html('');
            //            $('#sel_78').append($("<option></option>").val('0').html('--Select--'))
            //            $.each(r.d, function () {
            // $('#sel_78').append($("<option></option>").val(this.Value).html(this.Text));
           // alert(r.d);
           // alert(vartype);
            //AmountValidate(r.d, 0, 0, 0);
            // AmountValidate(r.d, r.d, r.d, r.d);
            if (vartype == 'W') {
                a = r.d;

            }
            else if (vartype == 'M') {
                b = r.d;
            }
            else if (vartype == 'MI') {
                c = r.d;

            }
            else if (vartype == 'WI') {
                d = r.d;
            }
            AmountValidate(a, b, c, d);

        },
        error: function (xhr, status, error) {
            var err = eval("(" + xhr.responseText + ")");
            // alert(err.Message);
        }
    });

   //AmountValidate(a, b, c, d);
}
//function AmountValidate(unit, application, estimated ,Instrument) {
//    debugger;
//   // alert(unit);
//    if ($("#txt_55").val() == 0) {
//        unit= 0;
//    }
//    if ($("#txt_70").val() == 0) {
//        unit= 0;
//    }
//    if ($("#txt_71").val() == 0) {
//        application = 0;
//    }
//    if ($("#txt_56").val() == 0) {
//        application = 0;
//    }
//    if ($("#txt_72").val() == 0) {
//        Instrument = 0;
//    }
//    if ($("#txt_57").val() == 0) {
//        Instrument = 0;
//    }
//    if ($("#txt_73").val() == 0) {
//        estimated = 0;
//    }
//    if ($("#txt_76").val() == 0) {
//        estimated = 0;
//    }
//    var strText = "";
//    if (unit == undefined) {
//        parseFloat(unit) = 0;
//    }
//    if (application == undefined) {
//        application = 0;
//    }
//    if (estimated == undefined) {
//        estimated = "0";
//    }
//    if (Instrument == undefined) {
//        Instrument = "0";
//    }
//    var strTotalAmnt = parseFloat(unit) + parseFloat(application) + parseFloat(estimated) + parseFloat(Instrument);

//   // strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>"
//    //strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Weight Amount</th><td width='50%'><b> <label id='lbl1'></label>" + unit + "/-</b></td></tr><tr><th width='50%'>Measure Amount</th><td width='50%'><b><label id='lbl2'></label>" + application + "/-</b></td></tr><tr><th width='50%'>Weighing Instrument Amount</th><td width='50%'><b><label id='lbl3'></label>" + estimated + "/-</b></td></tr><tr><th width='50%'>Measuring Instrument Amount</th><td width='50%'><b><label id='lbl4'></label>" + estimated1 + "/-</b></td></tr><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + strTotalAmnt + "/-</b></td></tr></table>"
//    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Weight Amount</th><td width='50%'><b>" + parseFloat(unit) + "/-</b></td></tr><tr><th width='50%'>Measure Amount</th><td width='50%'><b>" + application + "/-</b></td></tr><tr><th width='50%'>Weighing Instrument Amount</th><td width='50%'><b>" + Instrument + "/-</b></td></tr><tr><th width='50%'>Measuring Instrument Amount</th><td width='50%'><b>" + estimated + "/-</b></td></tr><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + parseFloat(strTotalAmnt) + "/-</b></td></tr></table>"
//    lblAmount.innerHTML = strText;
//    $('#hdnTotalAmount').val(strTotalAmnt);
//}

function AmountDetails(amount) {
    var strText = "";
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>"
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(amount);
}

function MandWeigth() {
    $('#lbl_sel_50').find('span').css('display', 'block');
    $('#sel_5').addClass("reqD rset");
    $('#lbl_txt_701').find('span').css('display', 'block');
    $('#txt_70').addClass("req rset");
    $('#lbl_sel_812').find('span').css('display', 'block');
    $('#sel_81').addClass("reqD rset");

    $('#lbl_sel_70').find('span').css('display', 'block');
    $('#sel_7').addClass("reqD rset");
    $('#lbl_txt_721').find('span').css('display', 'block');
    $('#txt_72').addClass("req rset");
    $('#lbl_sel_792').find('span').css('display', 'block');
    $('#sel_79').addClass("reqD rset");
}






function NonMandWeigth() {
    $('#lbl_sel_50').find('span').css('display', 'none');
    $('#sel_5').removeClass("reqD rset");
    $('#lbl_txt_701').find('span').css('display', 'none');
    $('#txt_70').removeClass("req rset");
    $('#lbl_sel_812').find('span').css('display', 'none');
    $('#sel_81').removeClass("reqD rset");

    $('#lbl_sel_70').find('span').css('display', 'none');
    $('#sel_7').removeClass("reqD rset");
    $('#lbl_txt_721').find('span').css('display', 'none');
    $('#txt_72').removeClass("req rset");
    $('#lbl_sel_792').find('span').css('display', 'none');
    $('#sel_79').removeClass("reqD rset");
}
function MandMeasure() {
    $('#lbl_sel_60').find('span').css('display', 'block');
    $('#sel_6').addClass("reqD rset");
    $('#lbl_txt_711').find('span').css('display', 'block');
    $('#txt_71').addClass("req rset");
    $('#lbl_sel_802').find('span').css('display', 'block');
    $('#sel_80').addClass("reqD rset");

    $('#lbl_sel_770').find('span').css('display', 'block');
    $('#sel_77').addClass("reqD rset");
    $('#lbl_txt_731').find('span').css('display', 'block');
    $('#txt_73').addClass("req rset");
    $('#lbl_sel_782').find('span').css('display', 'block');
    $('#sel_78').addClass("reqD rset");
}
function NonMandMeasure() {
    $('#lbl_sel_60').find('span').css('display', 'none');
    $('#sel_6').removeClass("reqD rset");
    $('#lbl_txt_711').find('span').css('display', 'none');
    $('#txt_71').removeClass("req rset");
    $('#lbl_sel_802').find('span').css('display', 'none');
    $('#sel_80').removeClass("reqD rset");

    $('#lbl_sel_770').find('span').css('display', 'none');
    $('#sel_77').removeClass("reqD rset");
    $('#lbl_txt_731').find('span').css('display', 'none');
    $('#txt_73').removeClass("req rset");
    $('#lbl_sel_782').find('span').css('display', 'none');
    $('#sel_78').removeClass("reqD rset");
}

