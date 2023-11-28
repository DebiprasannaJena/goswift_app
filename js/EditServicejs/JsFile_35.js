$(document).ready(function () {
   
    var vtxt_51 = "";
    var vtxt_50 = "";
    var vtxt_33 = "";
    var vtxt_34 = "";
    var vrLicType = "";
    //---------------------------------Edit---------------------------------------------------
    jQuery("input[name='LicenceOfContractor']").each(function (i) {
        jQuery(this).attr('disabled', 'disabled');
    });
    
    if ($('#sel_18').attr("EditData") !== 'undefined' && $('#sel_18').attr("EditData") !== "" && $('#sel_18').attr("EditData") !== null) {
        EditfillBlockDataAuto($('#sel_17').val(), $('#sel_18').attr("EditData"));
    }
    if ($('#sel_23').attr("EditData") !== 'undefined' && $('#sel_23').attr("EditData") !== "" && $('#sel_23').attr("EditData") !== null) {
        fillSubdivision($('#sel_22').val());
    }

    $('#div_rad_50').hide();
    $('#div_rad_5').removeClass("reqR rset");
     vrLicType = ($('input:radio[name=LicenceOfContractor]:checked').val());
     vtxt_51 = document.getElementById('txt_51').value;
     vtxt_50 = document.getElementById('txt_50').value;
     vtxt_33 = document.getElementById('txt_33').value;
     vtxt_34 = document.getElementById('txt_34').value;

    if (vrLicType === "New Plan") {
        $("#div_txt_516").hide();
        $("#div_plugin_36").hide();
        $('#div_txt_501').hide();
        $('#div_plugin_37').hide();
        $("#div_txt_321").show();
        $('#txt_32').addClass('req rset');
        $("#div_txt_332").show();
        $('#txt_33').addClass('req rset');
        $("#div_txt_344").show();
        $('#txt_34').addClass('req rset');
        $("#div_txt_310").show();
        $('#txt_31').addClass('req rset');
        $("#div_fil_354").show();
        $("#div_txt_506").hide();
        $("#txt_50").removeClass("req rset");
        $("#txt_51").removeClass("req rset");

        $('#div_txt_3331').hide();
        $('#txt_333').removeClass("req rset");
        if (vtxt_33 !== "") {
            var ss1 = "0";
        }
        else {
            vtxt_33 = "0";
        }
        if (vtxt_34 !== "") {
            var dd1 = "0";
        }
        else {
            vtxt_34 = "0";
        }        
        CalcuByProcedure(vtxt_33, vtxt_34);
    }
    else {

        $('#div_txt_3331').show();
        $('#txt_333').addClass("req rset");
        $('#div_rad_50').show();
        $('#div_rad_5').addClass("reqR rset");

        $("#div_plugin_36").show(); $('#div_txt_501').show();
        $('#div_plugin_37').show();
        $("#div_txt_516").show();
        $("#div_txt_321").hide();
        $('#txt_32').removeClass('req rset');
        $("#div_txt_332").hide();
        $('#txt_33').removeClass('req rset');
        $("#div_txt_344").hide();
        $('#txt_34').removeClass('req rset');
        $("#div_txt_310").hide();
        $('#txt_31').removeClass('req rset');
        $("#div_fil_354").hide();
        $("#div_txt_506").show();
        $("#txt_50").removeClass("req rset");
        $("#txt_50").attr("disabled", "disabled");
        $("#txt_51").addClass("req rset");
        $("#txt_51").attr("disabled", "disabled");
        if (vtxt_51 !== "") {
            var ee1 = "0";
        }
        else {
            vtxt_51 = "0";
        }
        if (vtxt_50 !== "") {
            var gg = "0";
        }
        else {
            vtxt_50 = "0";
        }       
        CalcuByProcedure(vtxt_51, vtxt_50);
    }
    
    //----------------------------------------------------------------------------------------
    var ProposalId = "";
    $('#div_rad_50').hide();
    $('#div_rad_5').removeClass("reqR rset");
    $('#divsmchk_txt_19').show();
    $("#btnSubmit").attr("disabled", true); //-----for disble button
    $("#txt_3").attr("readonly", false);
    $("#txt_35").attr("readonly", false);
    $("#txt_44").attr("readonly", false);

    $('#div_txt_3331').hide();
    $('#txt_333').removeClass("req rset");

    $('#Dec_1').click(function () {

        if (document.getElementById('Dec_1').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });

    $("#txt_13").focusout(function (me) {    //-----------for pin validation

        if ($("#txt_13").val() === "") {
            return true;
        }
        else {
            var len = $("#txt_13").val().length;
            if (len < 6) {
                jAlert('Pin number should contain 6 digits!');
                $('#txt_13').val("");
                $('#txt_13').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });

    //---------Date Validation-------

    //    $('#txt_38').datetimepicker({
    //        format: 'DD-MMM-YYYY'
    //    }).on('dp.show', function () {
    //        return $(this).data('DateTimePicker').maxDate(new Date());
    //    });

    //----------------AutoFill Dropdwon-------------------------
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        ProposalId = urlparam[1];
    }
    ProposalId = $('#hdnProposalNo').val();
    if (ProposalId !== "") {
        var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + "";
        var distiD = distvalue(query, 'sel_22', ProposalId);
        StateFill(ProposalId);
        FillState();
    }

    $('input[id="rad_21"]').change(function () {

        if (($(this).val() === "Extension Plan")) {
            $("#plugin_36").show();
            $("#h2_plugin_36").show();
        }
        else {
            $("#plugin_36").hide();
            $("#h2_plugin_36").hide();
        }
    });

    $('#sel_33').change(function () {

        if ($('#sel_33').val() === "Proprietary concern" || $('#sel_33').val() === "Any Other" || $('#sel_33').val() === "Government or  local fund factory") {
            $('#fil_45').removeClass("req rset");
            $("#fil_45").attr("disabled", "disabled");
            $('#lbl_fil_454').find('span').css('display', 'none');
        }
        else {
            $('#fil_45').addClass("req rset");
            $("#fil_45").removeAttr("disabled");
            $('#lbl_fil_454').find('span').css('display', 'block');
        }
    });

    $("#sel_17").change(function () {

        var selValue = $('#sel_17').val();

        var dtLevel = "3";
        var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_18').html('');
                $('#sel_18').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_18').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });
    });
    //    $('#fil_41').removeClass("req rset");
    //    $("#fil_41").attr("disabled", "disabled");
    //    $('#lbl_fil_412').find('span').css('display', 'none');
    $('input:radio[name=WhetherconstructiononIDCO]').change(function () {

        var vrLicType = ($('input:radio[name=WhetherconstructiononIDCO]:checked').val());
        if (vrLicType === "No") {
            //            $("#fil_41").attr("disabled", "disabled");

            if ($('#hdnfil_41').val() === "") {
                $('#fil_41').addClass("req rset");
                $("#fil_41").removeAttr("disabled");
                $('#lbl_fil_412').find('span').css('display', 'block');
            }

        }
        else {
            $('#fil_41').removeClass("req rset");
            $("#fil_41").attr("disabled", "disabled");
            $('#lbl_fil_412').find('span').css('display', 'none');
        }
    });
    //    $('#fil_43').removeClass("req rset");
    //    $("#fil_43").attr("disabled", "disabled");
    //    $('#lbl_fil_431').find('span').css('display', 'none');
    //    $('input:radio[name=WhetherExtension]').change(function () {
    //        debugger;
    //        var vrLicType = ($('input:radio[name=WhetherExtension]:checked').val());
    //        if (vrLicType == "Yes") {
    //            //            $("#fil_41").attr("disabled", "disabled");
    //            $('#fil_43').addClass("req rset");
    //            $("#fil_43").removeAttr("disabled");
    //            $('#lbl_fil_431').find('span').css('display', 'block');

    //        }
    //        else {
    //            $('#fil_43').removeClass("req rset");
    //            $("#fil_43").attr("disabled", "disabled");
    //            $('#lbl_fil_431').find('span').css('display', 'none');
    //        }
    //    });
    //    $('#fil_44').removeClass("req rset");
    //    $("#fil_44").attr("disabled", "disabled");
    //    $('#lbl_fil_443').find('span').css('display', 'none');
    $('input:radio[name=Plansinduplicatedrawn]').change(function () {

        var vrLicType = ($('input:radio[name=Plansinduplicatedrawn]:checked').val());
        if (vrLicType === "Yes") {
            //            $("#fil_41").attr("disabled", "disabled");
            if ($('#hdn' + fil_44).val() !== "") {
                $('#fil_44').addClass("req rset");
            }
            $("#fil_44").removeAttr("disabled");
            $('#lbl_fil_443').find('span').css('display', 'block');

        }
        else {
            $('#fil_44').removeClass("req rset");
            $("#fil_44").attr("disabled", "disabled");
            $('#lbl_fil_443').find('span').css('display', 'none');
        }
    });

    $("#sel_11").change(function () {

        var selValue = $('#sel_11').val();

        var dtLevel = "3";
        var query2 = "select intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District  where intStateId=" + parseInt(selValue) + " order by vchDistrictName";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_12').html('');
                $('#sel_12').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_12').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });
    });

    $("#sel_22").change(function () {

        var selValue = $('#sel_22').val();
        var dtLevel = "3";
        var query2 = "select distinct A.intLevelDetailId as COLUMN_NAME_VALUE,nvchLevelName as COLUMN_NAME_TEXT from M_adm_leveldetails A inner join T_FB_Subdiv_ServiceUserMapping B on A.intLevelDetailId=B.intLevelDetailId where intparentid in (select intLevelDetailId from M_adm_leveldetails where intparentid in(select intLevelDetailId from M_adm_leveldetails where intparentid=422)) and bitStatus=1 and bitDeletedFlag=0 and B.intDistrictId=" + selValue + "";
        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_23').html('');
                $('#sel_23').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    if ($('#sel_23').attr("EditData") === this.Text) {
                        $('#sel_23').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                    }
                    else {
                        $('#sel_23').append($("<option></option>").val(this.Value).html(this.Text));
                    }
                });
            }
        });
    });
    $('#div_plugin_37').hide();
    $('#div_txt_501').hide();
    $("#div_plugin_36").hide();
    $("#div_txt_321").hide();
    $('#txt_32').removeClass('req rset');
    $("#div_txt_332").hide();
    $('#txt_33').removeClass('req rset');
    $("#div_txt_343").hide();
    $("#div_txt_310").hide();
    $('#txt_31').removeClass('req rset');
    $("#div_fil_354").hide();
    $("#div_txt_506").hide();
    $("#div_txt_344").hide();
    $('#txt_34').removeClass('req rset');
    $("#div_txt_516").hide();
    $("#txt_51").removeClass("req rset");
    $("#txt_50").removeClass("req rset");
    //--------------------
    var editLicenceOfContractor = ($('input:radio[name=LicenceOfContractor]:checked').val());
      vtxt_51 = document.getElementById('txt_51').value;
      vtxt_50 = document.getElementById('txt_50').value;
      vtxt_33 = document.getElementById('txt_33').value;
      vtxt_34 = document.getElementById('txt_34').value;
    if (editLicenceOfContractor === "New Plan") {
        $("#div_txt_516").hide();
        $("#div_plugin_36").hide();
        $('#div_txt_501').hide();
        $('#div_plugin_37').hide();
        $("#div_txt_321").show();
        $('#txt_32').addClass('req rset');
        $("#div_txt_332").show();
        $('#txt_33').addClass('req rset');
        $("#div_txt_344").show();
        $('#txt_34').addClass('req rset');
        $("#div_txt_310").show();
        $('#txt_31').addClass('req rset');
        $("#div_fil_354").show();
        $("#div_txt_506").hide();
        $("#txt_50").removeClass("req rset");
        $("#txt_51").removeClass("req rset");

        $('#div_txt_3331').hide();
        $('#txt_333').removeClass("req rset");
        if (vtxt_33 !== "") {
            var ff = "0";
        }
        else {
            vtxt_33 = "0";
        }
        if (vtxt_34 !== "") {
            var ff1 = "0";
        }
        else {
            vtxt_34 = "0";
        }       
        CalcuByProcedure(vtxt_33, vtxt_34);
    }
    else {

        $('#div_txt_3331').show();
        $('#txt_333').addClass("req rset");
        $('#div_rad_50').show();
        $('#div_rad_5').addClass("reqR rset");

        $("#div_plugin_36").show(); $('#div_txt_501').show();
        $('#div_plugin_37').show();
        $("#div_txt_516").show();
        $("#div_txt_321").hide();
        $('#txt_32').removeClass('req rset');
        $("#div_txt_332").hide();
        $('#txt_33').removeClass('req rset');
        $("#div_txt_344").hide();
        $('#txt_34').removeClass('req rset');
        $("#div_txt_310").hide();
        $('#txt_31').removeClass('req rset');
        $("#div_fil_354").hide();
        $("#div_txt_506").show();
        $("#txt_50").removeClass("req rset");
        $("#txt_50").attr("disabled", "disabled");
        $("#txt_51").addClass("req rset");
        $("#txt_51").attr("disabled", "disabled");
        if (vtxt_51 !== "") {
            var ff3 = "0";
        }
        else {
            vtxt_51 = "0";
        }
        if (vtxt_50 !== "") {
            var ff4 = "0";
        }
        else {
            vtxt_50 = "0";
        }       
        CalcuByProcedure(vtxt_51, vtxt_50);
    }

    vrLicType = ($('input:radio[name=WhetherconstructiononIDCO]:checked').val());
    if (vrLicType === "No") {

        //            $("#fil_41").attr("disabled", "disabled");
        if ($('#hdnfil_41').val() === "") {
            $('#fil_41').addClass("req rset");
            $("#fil_41").removeAttr("disabled");
            $('#lbl_fil_412').find('span').css('display', 'block');
        }

    }
    else {
        $('#fil_41').removeClass("req rset");
        $("#fil_41").attr("disabled", "disabled");
        $('#lbl_fil_412').find('span').css('display', 'none');
    }

    vrLicType = ($('input:radio[name=Plansinduplicatedrawn]:checked').val());
    if (vrLicType === "Yes") {

        //            $("#fil_41").attr("disabled", "disabled");
        if ($('#hdnfil_44').val() === "") {
            $('#fil_44').addClass("req rset");

            $("#fil_44").removeAttr("disabled");
            $('#lbl_fil_443').find('span').css('display', 'block');
        }

    }
    else {
        $('#fil_44').removeClass("req rset");
        $("#fil_44").attr("disabled", "disabled");
        $('#lbl_fil_443').find('span').css('display', 'none');
    }
    //------------------------------Edit End-------------------
    $('input:radio[name=LicenceOfContractor]').change(function () {

        $('#div_rad_50').hide();
        $('#div_rad_5').removeClass("reqR rset");
        var vrLicType = ($('input:radio[name=LicenceOfContractor]:checked').val());
        var vtxt_51 = document.getElementById('txt_51').value;
        var vtxt_50 = document.getElementById('txt_50').value;
        var vtxt_33 = document.getElementById('txt_33').value;
        var vtxt_34 = document.getElementById('txt_34').value;
        if (vrLicType === "New Plan") {
            $("#div_txt_516").hide();
            $("#div_plugin_36").hide();
            $('#div_txt_501').hide();
            $('#div_plugin_37').hide();
            $("#div_txt_321").show();
            $('#txt_32').addClass('req rset');
            $("#div_txt_332").show();
            $('#txt_33').addClass('req rset');
            $("#div_txt_344").show();
            $('#txt_34').addClass('req rset');
            $("#div_txt_310").show();
            $('#txt_31').addClass('req rset');
            $("#div_fil_354").show();
            $("#div_txt_506").hide();
            $("#txt_50").removeClass("req rset");
            $("#txt_51").removeClass("req rset");

            $('#div_txt_3331').hide();
            $('#txt_333').removeClass("req rset");
            if (vtxt_33 !== "") {
                var ccf = "0";
            }
            else {
                vtxt_33 = "0";
            }
            if (vtxt_34 !== "") {
                var ccf1 = "0";
            }
            else {
                vtxt_34 = "0";
            }            
            CalcuByProcedure(vtxt_33, vtxt_34);
        }
        else {

            $('#div_txt_3331').show();
            $('#txt_333').addClass("req rset");
            $('#div_rad_50').show();
            $('#div_rad_5').addClass("reqR rset");

            $("#div_plugin_36").show(); $('#div_txt_501').show();
            $('#div_plugin_37').show();
            $("#div_txt_516").show();
            $("#div_txt_321").hide();
            $('#txt_32').removeClass('req rset');
            $("#div_txt_332").hide();
            $('#txt_33').removeClass('req rset');
            $("#div_txt_344").hide();
            $('#txt_34').removeClass('req rset');
            $("#div_txt_310").hide();
            $('#txt_31').removeClass('req rset');
            $("#div_fil_354").hide();
            $("#div_txt_506").show();
            $("#txt_50").removeClass("req rset");
            $("#txt_50").attr("disabled", "disabled");
            $("#txt_51").addClass("req rset");
            $("#txt_51").attr("disabled", "disabled");
            if (vtxt_51 !== "") {
                var ccf2 = "0";
            }
            else {
                vtxt_51 = "0";
            }
            if (vtxt_50 !== "") {
                var ccf3 = "0";
            }
            else {
                vtxt_50 = "0";
            }           
            CalcuByProcedure(vtxt_51, vtxt_50);
        }
    });

    $('#smchk_txt_19').click(function () {

        if (document.getElementById("smchk_txt_19").checked) {
            $('#sel_17').val($('#sel_22').val());
            $('#txt_19').val($('#txt_18').val());
            var selValue = $('#sel_17').val();
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_18').html('');
                    $('#sel_18').append($("<option></option>").val('0').html('Select'));
                    $.each(r.d, function () {
                        $('#sel_18').append($("<option></option>").val(this.Value).html(this.Text));                       
                    });
                }
            });


        } else {
            $('#sel_17').val("0");
            $('#txt_19').val("");
            $('#sel_18').val("0");
            $('#sel_18').html('');
            $('#sel_18').append($("<option></option>").val('0').html('Select'));
        }

    });
    //----------------------------------TotalPowerCalculation---------------------------
    //    $("#plugin_36_POWERINKW_1").change(function () {

    //        var txt_1 = document.getElementById('plugin_36_POWERINKW_1').value;
    //        var txt_2 = document.getElementById('plugin_36_POWERINKW_2').value;
    //        if (txt_1 == "")
    //            txt_1 = 0;
    //        if (txt_2 == "")
    //            txt_2 = 0;

    //        var result = parseInt(txt_1) + parseInt(txt_2); //+ parseInt(txtstdir) + parseInt(txtstind);
    //        if (!isNaN(result)) {
    //            $('#txt_50').val(result);

    //            //            document.getElementById('txt_50').value = totalcount();
    //            //ExtensionPlanCalculate();
    //        }
    //    });


    //    $("#plugin_36_POWERINKW_2").change(function () {

    //        var txt_1 = document.getElementById('plugin_36_POWERINKW_1').value;
    //        var txt_2 = document.getElementById('plugin_36_POWERINKW_2').value;

    //        if (txt_1 == "")
    //            txt_1 = 0;
    //        if (txt_2 == "")
    //            txt_2 = 0;

    //        var result = parseInt(txt_1) + parseInt(txt_2); //+ parseInt(txtstdir) + parseInt(txtstind);
    //        if (!isNaN(result)) {

    //            $('#txt_50').val(result);
    //            //            document.getElementById('txt_50').value = totalcount();
    //            //ExtensionPlanCalculate();


    //        }

    //    });
    //-----------------------------------------------end--------------------------------------
    //    //------------------------------Power------------------------------------
    $("#plugin_37_POWERINKW_1").change(function () {

        var txt_1 = document.getElementById('plugin_37_POWERINKW_1').value;
        var txt_2 = document.getElementById('plugin_37_POWERINKW_2').value;

        if (txt_1 === "")
            txt_1 = 0;
        if (txt_2 === "")
            txt_2 = 0;

        var result = parseInt(txt_1) + parseInt(txt_2); //+ parseInt(txtstdir) + parseInt(txtstind);
        if (!isNaN(result)) {
            $('#txt_50').val(result);
            $("#txt_50").attr("disabled", "disabled");           
            ExtensionPlanCalculate();
        }
    });


    $("#plugin_37_POWERINKW_2").change(function () {

        var txt_1 = document.getElementById('plugin_37_POWERINKW_1').value;
        var txt_2 = document.getElementById('plugin_37_POWERINKW_2').value;

        if (txt_1 === "")
            txt_1 = 0;
        if (txt_2 === "")
            txt_2 = 0;

        var result = parseInt(txt_1) + parseInt(txt_2); //+ parseInt(txtstdir) + parseInt(txtstind);
        if (!isNaN(result)) {
            $('#txt_50').val(result);
            $("#txt_50").attr("disabled", "disabled");            
            ExtensionPlanCalculate();
        }
    });

    //    //------------------------------------------------------------------------
    //--------------------------------------Men---------------------------------------------
    $("#plugin_36_MEN_1").change(function () {

        var txt_1 = document.getElementById('plugin_36_MEN_1').value;
        var txt_2 = document.getElementById('plugin_36_MEN_2').value;
        var txt_3 = document.getElementById('plugin_36_WOMEN_1').value;
        var txt_4 = document.getElementById('plugin_36_WOMEN_2').value;
        if (txt_1 === "")
            txt_1 = 0;
        if (txt_2 === "")
            txt_2 = 0;
        if (txt_3 === "")
            txt_3 = 0;
        if (txt_4 === "")
            txt_4 = 0;
        var result = parseInt(txt_1) + parseInt(txt_2) + parseInt(txt_3) + parseInt(txt_4); //+ parseInt(txtstdir) + parseInt(txtstind);
        if (!isNaN(result)) {

            $('#txt_51').val(result);
            $("#txt_51").attr("disabled", "disabled");           
            ExtensionPlanCalculate();
        }
    });


    $("#plugin_36_MEN_2").change(function () {

        var txt_1 = document.getElementById('plugin_36_MEN_1').value;
        var txt_2 = document.getElementById('plugin_36_MEN_2').value;
        var txt_3 = document.getElementById('plugin_36_WOMEN_1').value;
        var txt_4 = document.getElementById('plugin_36_WOMEN_2').value;
        if (txt_1 === "")
            txt_1 = 0;
        if (txt_2 === "")
            txt_2 = 0;
        if (txt_3 === "")
            txt_3 = 0;
        if (txt_4 === "")
            txt_4 = 0;

        var result = parseInt(txt_1) + parseInt(txt_2) + parseInt(txt_3) + parseInt(txt_4); //+ parseInt(txtstdir) + parseInt(txtstind);
        if (!isNaN(result)) {
            document.getElementById('txt_51').value = result;
            $("#txt_51").attr("disabled", "disabled");            
            ExtensionPlanCalculate();
        }
    });

    //-----------------------------------------------------------------------------end--------
    //------------------------------------------------Women-------------------------------------
    $("#plugin_36_WOMEN_1").change(function () {

        var txt_1 = document.getElementById('plugin_36_MEN_1').value;
        var txt_2 = document.getElementById('plugin_36_MEN_2').value;
        var txt_3 = document.getElementById('plugin_36_WOMEN_1').value;
        var txt_4 = document.getElementById('plugin_36_WOMEN_2').value;
        if (txt_1 === "")
            txt_1 = 0;
        if (txt_2 === "")
            txt_2 = 0;
        if (txt_3 === "")
            txt_3 = 0;
        if (txt_4 === "")
            txt_4 = 0;

        var result = parseInt(txt_1) + parseInt(txt_2) + parseInt(txt_3) + parseInt(txt_4);
        if (!isNaN(result)) {
            document.getElementById('txt_51').value = result;
            $("#txt_51").attr("disabled", "disabled");
            $("#txt_51").attr("disabled", "disabled");            
            ExtensionPlanCalculate();
        }
    });


    $("#plugin_36_WOMEN_2").change(function () {

        var txt_1 = document.getElementById('plugin_36_MEN_1').value;
        var txt_2 = document.getElementById('plugin_36_MEN_2').value;
        var txt_3 = document.getElementById('plugin_36_WOMEN_1').value;
        var txt_4 = document.getElementById('plugin_36_WOMEN_2').value;
        if (txt_1 === "")
            txt_1 = 0;
        if (txt_2 === "")
            txt_2 = 0;
        if (txt_3 === "")
            txt_3 = 0;
        if (txt_4 === "")
            txt_4 = 0;

        var result = parseInt(txt_1) + parseInt(txt_2) + parseInt(txt_3) + parseInt(txt_4);
        if (!isNaN(result)) {
            document.getElementById('txt_51').value = result;
            $("#txt_51").attr("disabled", "disabled");            
            ExtensionPlanCalculate();
        }

    });

    //---------------------------------------------------------end--------------------------------------------------

    $("#txt_31").change(function () {
        var vrtxt_34 = $('#txt_34').val();
        var txt_31 = document.getElementById('txt_31').value;
        var txt_32 = document.getElementById('txt_32').value;

        if (txt_31 === "")
            txt_31 = 0;
        if (txt_32 === "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32); //+ parseInt(txtstdir) + parseInt(txtstind);
        if (!isNaN(result)) {
            document.getElementById('txt_33').value = result;
            $("#txt_33").attr("disabled", "disabled");
        }
        if (vrtxt_34 !== "") {
            CalcuByProcedure(result, vrtxt_34);
        }
        else {
            vrtxt_34 = "0";
            CalcuByProcedure(result, vrtxt_34);
        }
    });

    $("#txt_32").change(function () {

        var vrtxt_34 = $('#txt_34').val();
        var txt_31 = document.getElementById('txt_31').value;
        var txt_32 = document.getElementById('txt_32').value;

        if (txt_31 === "")
            txt_31 = 0;
        if (txt_32 === "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32); //+ parseInt(txtstdir) + parseInt(txtstind);
        if (!isNaN(result)) {
            document.getElementById('txt_33').value = result;
            $("#txt_33").attr("disabled", "disabled");
        }
        if (vrtxt_34 !== "") {
            CalcuByProcedure(result, vrtxt_34);
        }
        else {
            vrtxt_34 = "0";
            CalcuByProcedure(result, vrtxt_34);
        }
    });

    $("#txt_34").change(function () {

        var vrtxt_33 = $('#txt_33').val();
        var vrtxt_34 = $('#txt_34').val();
        if (vrtxt_33 !== "") {
            var yy7 = "0";
        }
        else {
            vrtxt_33 = "0";
        }
        CalcuByProcedure(vrtxt_33, vrtxt_34);
    });


    $('#txt_3').keypress(function (e) {
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

    $('#txt_6').keypress(function (e) {
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

});


//function distvalue(query2, dropid, ProposalId) {
//    var distid = "";
//    $.ajax({
//        type: "POST",
//        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
//        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (r) {
//            distid = r.d;
//            if (r.d != "") {
//                $('#' + dropid).val(r.d);
//                divisionvalue('', r.d, ProposalId);
//                blockvalue('', r.d, ProposalId);
//                $("#" + dropid).attr("disabled", "disabled");
//            }
//        }
//    });
//    return distid;
//}

function distvalue(query2, dropid, ProposalId) {

    var distid = "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            if (r.d !== "") {
                distid = r.d;
                $('#' + dropid).val(r.d);               
                $("#" + dropid).attr("disabled", "disabled");
                fillSubdivision(r.d);
            }
        }
    });
    return distid;
}

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
            blockid = r.d;
            fillBlockDataAuto(distid, r.d);
        }
    });
    return blockid;
}

function fillBlockDataAuto(selValue, setVal) {

    var dtLevel = "3";
    var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName";

    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_18').html('');
            $('#sel_18').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {               
                $('#sel_18').append($("<option></option>").val(this.Value).html(this.Text));               
            });
        }
    });
    $("#sel_18").attr("disabled", "disabled");

}


function CalcuByProcedure(intPersone, intPower) {

    $.ajax({
        type: "POST",
        url: "FormView.aspx/Form34_Calculation",
        data: "{'intPerson':'" + intPersone + "','intPower':'" + intPower + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            AmountDetails(r.d);
        }
    });
}

function AmountDetails(amount) {

    var vrLicType = ($('input:radio[name=LicenceOfContractor]:checked').val());
    if (vrLicType !== "") {
        if (vrLicType === "New Plan") {
            if (parseInt(parseInt(amount) * 3) < 2500) {
                amount = 2500;
            }
            else {
                amount = parseInt(amount) * 3;
            }
        }

        else if (vrLicType === "Extension Plan") {
            if (parseInt(parseInt(amount) / 2) > 30000) {
                amount = 30000;
            }
            else {
                amount = parseInt(amount) / 2;
            }
        }

        var strText = "";
        strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Total Amount</th><td width='50%'> <b>" + amount + "/-</b></td></tr></table>"
        lblAmount.innerHTML = strText;
        $('#hdnTotalAmount').val(amount);
    }
}

function StateFill(ProposalId) {             // --------------------for state bind

    var stateid = "";
    query2 = "select intState from t_peal_promoter where vchProposalNo=" + ProposalId + "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            AutoFillState(r.d);
        }
    });
}

function AutoFillState(stateid) {
    var query2 = "select INT_STATE_ID as COLUMN_NAME_VALUE , VCH_STATE as COLUMN_NAME_TEXT from M_STATE_MASTER order by VCH_STATE asc";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_21').html('');
            $('#sel_21').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ("20" == this.Value) {
                    $('#sel_21').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_21').append($("<option></option>").val(this.Value).html(this.Text));
                }

            });
        }
    });
    //$('#sel_21').attr("readonly", true);

}

function fillSubdivision(dist) {
    var selValue = $('#sel_22').val();
    var query2 = "select distinct A.intLevelDetailId as COLUMN_NAME_VALUE,nvchLevelName as COLUMN_NAME_TEXT from M_adm_leveldetails A inner join T_FB_Subdiv_ServiceUserMapping B on A.intLevelDetailId=B.intLevelDetailId where intparentid in (select intLevelDetailId from M_adm_leveldetails where intparentid in(select intLevelDetailId from M_adm_leveldetails where intparentid=422)) and bitStatus=1 and bitDeletedFlag=0 and B.intDistrictId=" + selValue + "";

    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_23').html('');
            $('#sel_23').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_23').attr("EditData") == this.Text) {
                    $('#sel_23').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_23').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

//function StateFill(ProposalId) {              // --------------------for state bind

//    var stateid = "";
//    query2 = "select intState from t_peal_promoter where vchProposalNo=" + ProposalId + "";
//    $.ajax({
//        type: "POST",
//        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
//        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (r) {
//            AutoFillState(r.d);
//        }
//    });
//}

function FillState() {
    var query2 = "select INT_STATE_ID as COLUMN_NAME_VALUE , VCH_STATE as COLUMN_NAME_TEXT from M_STATE_MASTER order by VCH_STATE asc";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_11').html('');
            $('#sel_11').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_11').attr("EditData") === this.Text) {
                    $('#sel_11').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_11').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });

}

function totalcount() {

    var cntTtl = 0;
    var txt_13 = "";
    var txt_23 = "";
    var txt_11 = "";
    var txt_21 = "";
    if (document.getElementById('plugin_36_MEN_1').value !== "") {
        txt_13 = document.getElementById('plugin_36_MEN_1').value;
    }
    else {
        txt_13 = 0;
    }

    if (document.getElementById('plugin_36_MEN_2').value !== "") {
        txt_23 = document.getElementById('plugin_36_MEN_2').value;
    }
    else {
        txt_23 = 0;
    }

    if (document.getElementById('plugin_36_WOMEN_1').value !== "") {
        txt_11 = document.getElementById('plugin_36_WOMEN_1').value;
    }
    else {
        txt_11 = 0;
    }

    if (document.getElementById('plugin_36_WOMEN_2').value !== "") {
        txt_21 = document.getElementById('plugin_36_WOMEN_2').value;
    }
    else {
        txt_21 = 0;
    }
    cntTtl = parseInt(txt_13) + parseInt(txt_23) + parseInt(txt_11) + parseInt(txt_21);
    $('#txt_50').val(cntTtl);
}

function ExtensionPlanCalculate() {
    var totlPw = "";
    var totlMP = "";
    if (document.getElementById('txt_50').value !== "") {
        totlPw = document.getElementById('txt_50').value;
    }
    else {
        totlPw = 0;
    }

    if (document.getElementById('txt_51').value !== "") {
        totlMP = document.getElementById('txt_51').value;
    }
    else {
        totlMP = 0;
    }
    totlPw = parseFloat(totlPw);
    totlMP = parseInt(totlMP);
    CalcuByProcedure(totlMP, totlPw);
}

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
            $('#sel_18').html('');
            $('#sel_18').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {

                if (setVal == this.Text) {
                    $('#sel_18').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_18').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}