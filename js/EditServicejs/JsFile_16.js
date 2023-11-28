//----------------Begin Jquery Function-------------------------
$(document).ready(function () {
   
    AmountValidate(0, 0, 0);
    fillUtilityFirst($('#sel_1').attr("EditData"));

    /*--------------------------------------------------------------------------------------------------*/
    ///Begin Edit Code
    /*--------------------------------------------------------------------------------------------------*/
    if ($('#sel_10').attr("EditData") !== 'undefined' && $('#sel_10').attr("EditData") !== "" && $('#sel_10').attr("EditData") !== null) {
        EditfillBlockDataAuto($('#sel_9').val(), $('#sel_10').attr("EditData"));
    }

    if ($('#sel_4').attr("EditData") !== 'undefined' && $('#sel_4').attr("EditData") !== "" && $('#sel_4').attr("EditData") !== null) {
        EditfillBlockDataAuto2($('#sel_3').val(), $('#sel_4').attr("EditData"));
    }
    /*--------------------------------------------------------------------------------------------------*/

    $('#txt_19').removeClass('req rset');
    $('#div_txt_194').hide();
    $('#div_rad_2222').hide();

    /*--------------------------------------------------------------------------------------------------*/

    if ($('input:radio[name=ElectricPower]:checked').val() === "Yes") {
        $('#div_txt_194').show();
        $('#txt_19').addClass('req rset');
        if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {

            $("#div_rad_2246").show();
            $("#div_rad_224").addClass("reqR rset");
        }
        else {
            $("#div_rad_224").removeClass("reqR rset");
            $("#div_rad_2246").hide();
        }
    }
    else {
        $('#txt_19').removeClass('req rset');
        $('#div_txt_194').hide();
        if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {

            $("#div_rad_2246").show();
            $("#div_rad_224").addClass("reqR rset");
        }
        else {
            $("#div_rad_224").removeClass("reqR rset");
            $("#div_rad_2246").hide();
        }

        //$("#div_rad_224").removeClass("reqR rset");
        //$("#div_rad_2246").hide();

    }

    /*--------------------------------------------------------------------------------------------------*/

    if ($('input:radio[name=Areatype]:checked').val() === "Rural") {
        $('#div_sel_635').show();
        $('#div_sel_103').show();
        $('#div_sel_646').hide();
        $('#div_sel_624').hide();
    }
    else if ($('input:radio[name=Areatype]:checked').val() === "Urban") {
        $('#div_sel_646').show();
        $('#div_sel_624').show();
        $('#div_sel_635').hide();
        $('#div_sel_103').hide();
    }
    else {
        $('#div_sel_646').hide();
        $('#div_sel_624').hide();
        $('#div_sel_635').hide();
        $('#div_sel_103').hide();
    }

    /*--------------------------------------------------------------------------------------------------*/

    if ($('input:radio[name=AreatypePremises]:checked').val() === "Rural") {
        $('#div_sel_83').show();
        $('#div_sel_665').show();
        $('#div_sel_654').hide();
        $('#div_sel_676').hide();
    }
    else if ($('input:radio[name=AreatypePremises]:checked').val() === "Urban") {
        $('#div_sel_654').show();
        $('#div_sel_676').show();
        $('#div_sel_83').hide();
        $('#div_sel_665').hide();
    }
    else {
        $('#div_sel_654').hide();
        $('#div_sel_676').hide();
        $('#div_sel_83').hide();
        $('#div_sel_665').hide();
    }

    /*--------------------------------------------------------------------------------------------------*/

    if ($('input:radio[name=EnergyReq]:checked').val() === "Temporary") {
        $('#div_rad_2222').show();
    }
    else {
        $('#div_rad_2222').hide();
    }

    /*--------------------------------------------------------------------------------------------------*/

    if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {        
        if (parseInt($('#txt_16').val()) > 1111) {
            $("#div_rad_2246").show();
            $("#div_rad_224").addClass("reqR rset");
            $('#rad_2241').attr('disabled', true);
            $('#rad_2240').removeAttr('checked');
            $('#rad_2241').removeAttr('checked');
        }
        else if (parseInt($('#txt_16').val()) < 70) {
            $("#div_rad_2246").show();
            $("#div_rad_224").addClass("reqR rset");
            $('#rad_2241').attr('disabled', false);
        }
        else if (parseInt($('#txt_16').val()) > 70 && parseInt($('#txt_16').val()) < 1111) {
            $("#div_rad_2246").show();
            $("#div_rad_224").show();
            $("#div_rad_224").addClass("reqR rset");
            $('#rad_2241').attr('disabled', false);
            $('#rad_2231').removeAttr('checked');
        }
        else {
            $("#div_rad_2246").hide();
            $("#div_rad_224").removeClass("reqR rset");
            $('#rad_2241').attr('rad_2241', false);
        }
    }
    else {
        $("#div_rad_224").removeClass("reqR rset");
        $("#div_rad_2246").hide();
    }

    /*--------------------------------------------------------------------------------------------------*/

    $('#div_rad_2235').hide();
    $('#txt_20').attr("readonly", false);
    var FormId = "";
    var estimatvl = "";
    var ProposalId = "";
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
    ProposalId = $('#hdnProposalNo').val(); // This below section is commeted for autoselection of Premises dist and block change by investor not same in T_LandAndUtility table  Changed by Anil 28-12-2022
    //if (ProposalId !== "" && ProposalId !== "0") {
    //    var query = "SELECT intDistrictId FROM T_LandAndUtility WHERE vchProposalNo=" + ProposalId + "";
    //    var distiD = distvalue(query, 'sel_7', ProposalId);
    //}

    /*--------------------------------------------------------------------------------------------------*/

    $('#divsmchk_txt_5').show();

    $("#rad_222_frmdt").on("dp.change", function (e) {
        $('#rad_222_Todt').data("DateTimePicker").minDate(e.date);
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#rad_222_Todt").on("dp.change", function (e) {
        format: 'dd-M-yy';
        var start = new Date($("#rad_222_frmdt").val());
        var end = new Date($("#rad_222_Todt").val());
        var newdate = new Date(start);
        newdate.setDate(newdate.getDate() + 180); // minus the date
        var maxDate = new Date(newdate);
        if (end > maxDate) {
            jAlert("End date can not be greater than 6 month !!");
            $("#rad_222_Todt").val("");
        }
    });

    /*--------------------------------------------------------------------------------------------------*/
    /*-------------------------------------------------------------------*/
    ///// Added by Sushant during v2.0 Implementation
    /*-------------------------------------------------------------------*/
    $("#btnDraft").attr("disabled", true);

    $('#rad_21').click(function () {
        if (document.getElementById('rad_21').checked) {
            $("#btnDraft").attr("disabled", false);
        }
        else {
            $("#btnDraft").attr("disabled", true);
        }

        EditCalculation();
    });
    /*-------------------------------------------------------------------*/

    $("#btnSubmit").attr("disabled", true);

    $('#rad_21').click(function () {
        if (document.getElementById('rad_21').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }

        EditCalculation();
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#txt_12").focusout(function (me) {
        if ($("#txt_12").val() === "") {
            return true;
        }
        else {
            var len = $("#txt_12").val().length;
            if (len < 10) {
                jAlert('Mobile number should contain 10 digits !');
                $('#txt_12').val("");
                $('#txt_12').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#txt_11").focusout(function (me) {
        if ($("#txt_11").val() === "") {
            return true;
        }
        else {
            var len = $("#txt_11").val().length;
            if (len < 12) {
                jAlert('Aadhaar Number should contain 12 digits !');
                $('#txt_11').val("");
                $('#txt_11').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#txt_10").focusout(function (me) {
        if ($("#txt_10").val() === "") {
            return true;
        }
        else {
            var len = $("#txt_10").val().length;
            if (len < 10) {
                jAlert('PAN number should contain 10 digits !');
                $('#txt_10').val("");
                $('#txt_10').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });

    /*--------------------------------------------------------------------------------------------------*/

    ///$("#div_rad_224").removeClass("reqR rset");
    /// $("#div_rad_2246").hide();
    /// var x;

    /*--------------------------------------------------------------------------------------------------*/

    $('input:radio[name=InfrastructerAvailable]').change(function () {
        var x = 0;
        var y = 0;
        var z = 0;

        if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
            x = 2600;
            if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {
                y = 500;
            }
            if ($('input:radio[name=EnergyRequired]:checked').val() === "LT") {
                y = 100;
            }
            jAlert("The infrastructure which is located in the IDCO industrial area.");
        }
        else if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "No") {
            x = 0;
            if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {
                y = 0;
            }
            if ($('input:radio[name=EnergyRequired]:checked').val() === "LT") {
                y = 0;
            }
            jAlert("The cost will be calculated as per the actual estimate prepared by DISCOM engineer on the basis of OERC norms and site conditions.");
        }

        if ($('#txt_16').val !== '') {
            z = x * $('#txt_16').val();
        }

        AmountValidate(x, y, z);
    });

    /*--------------------------------------------------------------------------------------------------*/

    $('input:radio[name=EnergyRequired]').change(function () {
        var x = 0;
        var y = 0;
        var z = 0;

        if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
            x = 2600;
        }
        else if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "No") {
            x = 0;
        }

        if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {
            if (parseInt($('#txt_16').val()) > 1111) {
                $("#div_rad_2246").show();
                $("#div_rad_224").addClass("reqR rset");
                $('#rad_2241').attr('disabled', true);
                if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
                    y = 500;
                }
                else {
                    y = 0;
                }
                $('#rad_2240').removeAttr('checked');
                $('#rad_2241').removeAttr('checked');
            }
            else if (parseInt($('#txt_16').val()) < 70) {
                $("#div_rad_2246").show();
                $("#div_rad_224").addClass("reqR rset");
                $('#rad_2241').attr('disabled', false);
                if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
                    y = 500;
                }
                else {
                    y = 0;
                }
            }
            else if (parseInt($('#txt_16').val()) > 70 && parseInt($('#txt_16').val()) < 1111) {
                $("#div_rad_2246").show();
                $("#div_rad_224").addClass("reqR rset");
                $('#rad_2241').attr('disabled', false);
                $('#rad_2231').removeAttr('checked');
                //$('#rad_2230').removeAttr('checked')
                if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
                    y = 500;
                }
                else {
                    y = 0;
                }
            }
            else {
                $("#div_rad_2246").hide();
                $("#div_rad_224").removeClass("reqR rset");
                $('#rad_2241').attr('rad_2241', false);
            }
        }
        else {
            $("#div_rad_224").removeClass("reqR rset");
            $("#div_rad_2246").hide();
            if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
                y = 100;
            }
            else {
                y = 0;
            }
        }

        if ($('#txt_16').val !== '') {
            z = x * $('#txt_16').val();
        }

        AmountValidate(x, y, z);
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#txt_16").attr("disabled", "disabled");

    $("#txt_14").change(function () {

        EditCalculation();

        //var txt_14 = document.getElementById('txt_14').value;
        //var txt_15 = document.getElementById('txt_15').value;

        //if (txt_14 === "") {
        //    txt_14 = 0;
        //}

        //if (txt_15 === "") {
        //    txt_15 = 0;
        //}

        //var result = parseInt(txt_14) + parseInt(txt_15);

        //if (result < 70) {
        //    $('#div_rad_2235').show();
        //    $("#div_rad_224").removeClass("reqR rset");
        //    $('#rad_2231').attr('disabled', false);
        //    $("#div_rad_2246").hide();
        //}
        //else if (result > 70 && result < 1111) {
        //    $('#div_rad_2235').show();
        //    $('#rad_2231').attr('disabled', true);
        //    $("#div_rad_224").removeClass("reqR rset");
        //    $("#div_rad_2246").hide();
        //    $('#rad_2231').removeAttr('checked');
        //}
        //else {
        //    $('#rad_2230').removeAttr('checked');
        //    $('#div_rad_2235').show();
        //    $('#div_rad_2246').hide();
        //}

        //if (result >= "110") {
        //    $("#txt_16").removeAttr("disabled");
        //}
        //else if (result < "110") {
        //    $("#txt_16").attr("disabled", "disabled");
        //}

        //if (!isNaN(result)) {
        //    document.getElementById('txt_16').value = result;
        //}

        //var x = 0;
        //var y = 0;
        //var z = 0;

        //if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
        //    x = 2600;
        //}
        //else if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "No") {
        //    x = 0;
        //}

        //if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {
        //    if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
        //        y = 500;
        //    }
        //    else {
        //        y = 0;
        //    }
        //}
        //else {
        //    if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
        //        y = 100;
        //    }
        //    else {
        //        y = 0;
        //    }
        //}

        //if ($('#txt_16').val !== '') {
        //    z = x * $('#txt_16').val();
        //}

        //AmountValidate(x, y, z);
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#txt_15").change(function () {
        EditCalculation();
        //var txt_14 = document.getElementById('txt_14').value;
        //var txt_15 = document.getElementById('txt_15').value;

        //if (txt_14 === "") {
        //    txt_14 = 0;
        //}

        //if (txt_15 === "") {
        //    txt_15 = 0;
        //}

        //var result = parseInt(txt_14) + parseInt(txt_15);

        //if (result < 70) {
        //    $('#div_rad_2235').show();
        //    $("#div_rad_224").removeClass("reqR rset");
        //    $('#rad_2231').attr('disabled', false);
        //    $("#div_rad_2246").hide();
        //}
        //else if (result > 70 && result < 1111) {
        //    $('#div_rad_2235').show();
        //    $('#rad_2231').attr('disabled', true);
        //    $("#div_rad_224").removeClass("reqR rset");
        //    $("#div_rad_2246").hide();
        //    $('#rad_2231').removeAttr('checked');
        //}
        //else {
        //    $('#rad_2230').removeAttr('checked');
        //    $('#div_rad_2235').show();
        //    $('#div_rad_2246').hide();
        //}

        //if (result >= "110") {
        //    $("#txt_16").removeAttr("disabled");
        //}
        //else if (result < "110") {
        //    $("#txt_16").attr("disabled", "disabled");
        //}

        //if (!isNaN(result)) {
        //    document.getElementById('txt_16').value = result;
        //}

        //var x = 0;
        //var y = 0;
        //var z = 0;

        //if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
        //    x = 2600;
        //}
        //else if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "No") {
        //    x = 0;
        //}

        //if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {
        //    if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
        //        y = 500;
        //    }
        //    else {
        //        y = 0;
        //    }
        //}
        //else {
        //    if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
        //        y = 100;
        //    }
        //    else {
        //        y = 0;
        //    }
        //}

        //if ($('#txt_16').val !== '') {
        //    z = x * $('#txt_16').val();
        //}

        //AmountValidate(x, y, z);
    });

    /*--------------------------------------------------------------------------------------------------*/

    $('#txt_2').keypress(function (e) {
        var regex = new RegExp("^[a-zA-Z .&-0123456789]+$");
        var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
        if (regex.test(str)) {
            return true;
        }
        else {
            e.preventDefault();
            return false;
        }
    });

    /*--------------------------------------------------------------------------------------------------*/

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

    /*--------------------------------------------------------------------------------------------------*/

    $('#smchk_txt_5').click(function () {

        if (document.getElementById("smchk_txt_5").checked) {
            $('#sel_3').val($('#sel_9').val());
            $('#txt_5').val($('#txt_20').val());
            var selValue = $('#sel_3').val();
            var dtSource = "M_ADM_LocationDetails";
            var dtValue = "intLevelDetailId";
            var dtText = "nvchLevelName";
            var dtLevel = "3";

            var query1 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM M_Block WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
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

                        if ($('#sel_10').val() === this.Value) {
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

    /*--------------------------------------------------------------------------------------------------*/

    $("#sel_1").change(function () {
        FillDivision_Utility(this);
        FillUtility_District(this);
    });

    /*--------------------------------------------------------------------------------------------------*/

    $('#smchk_txt_9').click(function () {
        if (document.getElementById("smchk_txt_9").checked) {
            $('#sel_7').val($('#sel_3').val());
            $('#txt_9').val($('#txt_5').val());
            var selValue = $('#sel_7').val();
            var dtSource = "M_ADM_LocationDetails";
            var dtValue = "intLevelDetailId";
            var dtText = "nvchLevelName";
            var dtLevel = "3";

            var query1 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM M_Block WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_8').html('');
                    $('#sel_8').append($("<option></option>").val('0').html('Select'));
                    $.each(r.d, function () {

                        if ($('#sel_4').val() === this.Value) {
                            $('#sel_8').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_8').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    });
                }
            });

        } else {
            $('#sel_7').val("0");
            $('#sel_8').val("0");
            $('#sel_8').html('');
            $('#sel_8').append($("<option></option>").val('0').html('Select'));
            $('#txt_9').val("");
        }
    });

    /*--------------------------------------------------------------------------------------------------*/

    $('input:radio[name=EnergyReq]').change(function () {

        if ($('input:radio[name=EnergyReq]:checked').val() === "Temporary") {
            $('#div_rad_2222').show();
        }
        else {
            $('#div_rad_2222').hide();
        }
    });

    /*--------------------------------------------------------------------------------------------------*/

    $('input:radio[name=ElectricPower]').change(function () {
        var query2 = "";
        if ($('input:radio[name=ElectricPower]:checked').val() === "Yes") {

            $('#div_txt_194').show();
            $('#txt_19').addClass('req rset');

            if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {

                $("#div_rad_2246").show();
                $("#div_rad_224").addClass("reqR rset");
            }
            else {
                $("#div_rad_224").removeClass("reqR rset");
                $("#div_rad_2246").hide();
            }
        }
        else {
            $('#txt_19').removeClass('req rset');
            $('#div_txt_194').hide();

            if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {

                $("#div_rad_2246").show();
                $("#div_rad_224").addClass("reqR rset");
            }
            else {
                $("#div_rad_224").removeClass("reqR rset");
                $("#div_rad_2246").hide();
            }

            //$("#div_rad_224").removeClass("reqR rset");
            //$("#div_rad_2246").hide();
        }
    });


    /*--------------------------------------------------------------------------------------------------*/

    //$('#div_sel_646').hide();
    //$('#div_sel_624').hide();

    /*--------------------------------------------------------------------------------------------------*/

    $('input:radio[name=Areatype]').change(function () {
        if ($('input:radio[name=Areatype]:checked').val() === "Rural") {
            $('#div_sel_635').show();
            $('#div_sel_103').show();
            $('#div_sel_646').hide();
            $('#div_sel_624').hide();
        }
        else if ($('input:radio[name=Areatype]:checked').val() === "Urban") {
            $('#div_sel_646').show();
            $('#div_sel_624').show();
            $('#div_sel_635').hide();
            $('#div_sel_103').hide();
        }
        else {
            $('#div_sel_646').hide();
            $('#div_sel_624').hide();
            $('#div_sel_635').hide();
            $('#div_sel_103').hide();
        }
    });

    /*--------------------------------------------------------------------------------------------------*/

    //$('#div_sel_654').hide();
    //$('#div_sel_676').hide();

    /*--------------------------------------------------------------------------------------------------*/

    $('input:radio[name=AreatypePremises]').change(function () {
        if ($('input:radio[name=AreatypePremises]:checked').val() === "Rural") {
            $('#div_sel_83').show();
            $('#div_sel_665').show();
            $('#div_sel_654').hide();
            $('#div_sel_676').hide();
        }
        else if ($('input:radio[name=AreatypePremises]:checked').val() === "Urban") {
            $('#div_sel_654').show();
            $('#div_sel_676').show();
            $('#div_sel_83').hide();
            $('#div_sel_665').hide();
        }
        else {
            $('#div_sel_654').hide();
            $('#div_sel_676').hide();
            $('#div_sel_83').hide();
            $('#div_sel_665').hide();
        }
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#sel_1").change(function () {
        FillHeader($("#sel_1").val());
    });

    /*--------------------------------------------------------------------------------------------------*/

    ULBvalueByText($("#sel_9").val());
    WARDvalueByText($("#sel_9").val());

    /*--------------------------------------------------------------------------------------------------*/

    $("#sel_9").change(function () {
        debugger;
        var selValue = $('#sel_9').val();      

        var query2 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM M_Block WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_10').html('');
                $('#sel_10').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_10').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });

        //query2 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM M_Block WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
        //$.ajax({
        //    type: "POST",
        //    url: "FormView.aspx/FillDemographyData",
        //    data: "{'query':'" + query2 + "'}",
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (r) {
        //        $('#sel_10').html('');
        //        $('#sel_10').append($("<option></option>").val('0').html('Select'));
        //        $.each(r.d, function () {
        //            $('#sel_10').append($("<option></option>").val(this.Value).html(this.Text));
        //        });
        //    }
        //});

        ULBvalue($("#sel_9").val());
        WARDvalue($("#sel_9").val());
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#sel_10").change(function () {
        gpvalue($("#sel_10").val());
    });

    /*--------------------------------------------------------------------------------------------------*/

    ULBvaluePremisesByText($("#sel_7").val());
    WARDvaluePremisesByText($("#sel_7").val());

    /*--------------------------------------------------------------------------------------------------*/

    $("#sel_7").change(function () {
        var selValue = $('#sel_7').val();
        var dtSource = "M_ADM_LocationDetails";
        var dtValue = "intLevelDetailId";
        var dtText = "nvchLevelName";
        var dtLevel = "3";

        var query2 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM M_Block WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_8').html('');
                $('#sel_8').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_8').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });
        ULBvaluePremises($("#sel_7").val());
        WARDvaluePremises($("#sel_7").val());
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#sel_8").change(function () {
        gpvaluePremises($("#sel_8").val());
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#sel_3").change(function () {
        var selValue = $('#sel_3').val();
        var dtSource = "M_ADM_LocationDetails";
        var dtValue = "intLevelDetailId";
        var dtText = "nvchLevelName";
        var dtLevel = "3";
        var query2 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM M_Block WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
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
});

/*--==================================================================================================================================================--*/


$(function () {
    $("#rad_222_frmdt").datetimepicker({
        format: 'DD-MMM-YYYY'
    }).on('dp.show', function () {
        return $(this).data('DateTimePicker').minDate(new Date());
    });
});

/*--------------------------------------------------------------------------------------------------*/

$(function () {
    $('#txt_16').blur(function () {
        if (eval($('#txt_16').val()) < 110) {
            jAlert('Contact Demand can not be less than 110');
            var txt_14 = document.getElementById('txt_14').value;
            var txt_15 = document.getElementById('txt_15').value;

            if (txt_14 === "")
                txt_14 = 0;
            if (txt_15 === "")
                txt_15 = 0;

            document.getElementById('txt_16').value = parseInt(txt_14) + parseInt(txt_15);
        }
    });
    EditCalculation();
});

/*--------------------------------------------------------------------------------------------------*/
///Amount Calculation
/*--------------------------------------------------------------------------------------------------*/
function EditCalculation() {
   
    var txt_14 = document.getElementById('txt_14').value;
    var txt_15 = document.getElementById('txt_15').value;

    if (txt_14 === "") {
        txt_14 = 0;
    }

    if (txt_15 === "") {
        txt_15 = 0;
    }

    var result = parseInt(txt_14) + parseInt(txt_15);

    if (result > 1111) {
        $("#div_rad_2246").show();
        $("#div_rad_224").addClass("reqR rset");
        $('#rad_2241').attr('disabled', true);
        $('#rad_2240').removeAttr('checked');
        $('#rad_2241').removeAttr('checked');
    }
    else if (result < 70) {
        $('#div_rad_2235').show();
        $("#div_rad_224").removeClass("reqR rset");
        $('#rad_2231').attr('disabled', false);
        $("#div_rad_2246").show();
    }
    else if (result > 70 && result < 1111) {
        $('#div_rad_2235').show();
        $('#rad_2231').attr('disabled', true);
        $("#div_rad_224").removeClass("reqR rset");
        $("#div_rad_2246").show();
        $('#rad_2231').removeAttr('checked');
    }
    else {
        $('#rad_2230').removeAttr('checked');
        $('#div_rad_2235').show();
        $('#div_rad_2246').hide();
    }  

    if (result >= "110") {
        $("#txt_16").removeAttr("disabled");
    }
    else if (result < "110") {
        $("#txt_16").attr("disabled", "disabled");
    }

    if (!isNaN(result)) {
        document.getElementById('txt_16').value = result;
    }

    var x = 0;
    var y = 0;
    var z = 0;

    if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
        x = 2600;
    }
    else if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "No") {

        x = 0;
    }

    if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {
        if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
            y = 500;
        }
        else {
            y = 0;
        }
    }
    else if ($('input:radio[name=EnergyRequired]:checked').val() === "LT") {
        $("#div_rad_2246").hide(); /// ADD ANIL
        if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
            y = 100;
        }
        else {
            y = 0;
        }
    }

    if ($('#txt_16').val !== '') {
        z = x * $('#txt_16').val();
    }

    AmountValidate(x, y, z);
}

/*--------------------------------------------------------------------------------------------------*/
///Display Payment Section
/*--------------------------------------------------------------------------------------------------*/
function AmountValidate(unit, application, estimated) {
    var strText = "";
    var UserFees = 1;

    if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "Yes") {
        UserFees = 1;
    }
    else {
        UserFees = 0;
    }

    var strTotalAmnt = application + estimated + UserFees;
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Per KVA</th><td width='50%'><b>" + unit + "/-</b></td></tr><tr><th width='50%'>Application Fee</th><td width='50%'><b>" + application + "</b></td></tr><tr><th width='50%'>User Fee</th><td width='50%'><b>" + UserFees + "</b></td></tr><tr><th width='50%'>Estimated Amount</th><td width='50%'><b>" + estimated + "</b></td></tr><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + strTotalAmnt + "/-</b></td></tr></table>"

    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(strTotalAmnt);
    $('#hdnApplicationFee').val(UserFees);
}

/*--------------------------------------------------------------------------------------------------*/
///Fill Utility Function
/*--------------------------------------------------------------------------------------------------*/
function fillUtilityFirst(setVl) {
    var query2 = "SELECT intLevelDetailId AS COLUMN_NAME_VALUE,nvchLevelName AS COLUMN_NAME_TEXT FROM M_ADM_LevelDetails WHERE intLevelDetailId IN (SELECT intDiscomeid FROM T_Energy_Block_ServiceUserMapping)";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_1').html('');
            $('#sel_1').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if (setVl == this.Text) {
                    $('#sel_1').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_1').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });

            FillHeader(setVl);
            FillDivision_Utility(setVl);
            FillUtility_District(setVl);
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/
///Fill Header
/*--------------------------------------------------------------------------------------------------*/
function FillHeader(val) {
    var h1 = 'NESCO Utility';
    var h2 = 'SOUTHCO Utility';
    var h3 = 'WESCO Utility';
    var h4 = 'CESU';
    var p1 = 'Government of Odisha';
    var p2 = 'Power Connection Application';
    var imgurl = "";

    if (val === "365") {
        $('#divHeaderId').show();
        imgurl = 'images/NESCOLogo.jpg';
        $('#divHeaderId').html('<center><img src="' + imgurl + '"></center>' + '<h2>' + h1 + '</h2>' + '<p>' + p1 + '</p>' + '<p>' + p2 + '</p>');
    }
    else if (val === "366") {
        $('#divHeaderId').show();
        imgurl = 'images/SOUTHCOLogo.jpg';
        $('#divHeaderId').html('<center><img src="' + imgurl + '"></center>' + '<h2>' + h2 + '</h2>' + '<p>' + p1 + '</p>' + '<p>' + p2 + '</p>');
    }
    else if (val === "367") {
        $('#divHeaderId').show();
        imgurl = 'images/WESCOLogo.jpg';
        $('#divHeaderId').html('<center><img src="' + imgurl + '"></center>' + '<h2>' + h3 + '</h2>' + '<p>' + p1 + '</p>' + '<p>' + p2 + '</p>');
    }
    else if (val === "368") {
        $('#divHeaderId').show();
        imgurl = 'images/CESUlogo.jpg';
        $('#divHeaderId').html('<center><img src="' + imgurl + '"></center>' + '<h2>' + h4 + '</h2>' + '<p>' + p1 + '</p>' + '<p>' + p2 + '</p>');
    }
}

/*--------------------------------------------------------------------------------------------------*/
///Fill Division against Utility
/*--------------------------------------------------------------------------------------------------*/
function FillDivision_Utility(setVl) {
    var selValue = "";

    if ($('#sel_1').val() !== "0") {
        selValue = $('#sel_1').val();
    }
    else {
        selValue = setVl;
    }   

    var query1 = "SELECT L.intLevelDetailId AS COLUMN_NAME_VALUE,L.nvchLevelName AS COLUMN_NAME_TEXT FROM [dbo].[T_Energy_Block_ServiceUserMapping] AS M INNER JOIN M_ADM_LevelDetails AS L ON L.intLevelDetailId=M.intLevelDetailId WHERE M.intDiscomeid=" + parseInt(selValue) + " AND L.bitStatus=1 ORDER BY L.nvchLevelName";
    var ob = {};
    ob.query = query1;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
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

/*--------------------------------------------------------------------------------------------------*/
///Fill District against Utility 
/*--------------------------------------------------------------------------------------------------*/
function FillUtility_District(setVl) {
    var selValue = "";

    if ($('#sel_1').val() !== "0") {
        selValue = $('#sel_1').val();
    }
    else {
        selValue = setVl;
    }
   
    var query1 = "SELECT DISTINCT D.intDistrictId AS COLUMN_NAME_VALUE ,D.vchDistrictName AS COLUMN_NAME_TEXT FROM [dbo].[T_Energy_Block_ServiceUserMapping] AS M INNER JOIN M_District AS D ON D.intDistrictId=M.intDistrictId WHERE M.intDiscomeid=" + selValue + " ORDER BY D.vchDistrictName";
    var ob = {};
    ob.query = query1;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            var Selvalue = $('#sel_7').val();
            $('#sel_7').html('');
            $('#sel_7').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if (Selvalue === this.Value) {
                    $('#sel_7').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
            fillBlockDataAutoByText(Selvalue, $('#sel_8').attr("EditData"));
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/
///Fill Block against District
/*--------------------------------------------------------------------------------------------------*/
function EditfillBlockDataAuto(selValue, setVal) {   
    var query2 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM m_block WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
    var SelBlock = "0";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_10').html('');
            $('#sel_10').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if (setVal === this.Text) {
                    SelBlock = this.Value;
                    $('#sel_10').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_10').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
            gpvalue(SelBlock);
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function EditfillBlockDataAuto2(selValue, setVal) {   
    var query2 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM m_block  WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
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
                if (setVal === this.Text) {
                    $('#sel_4').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

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
            distid = r.d;            
            if (r.d != "") {
                $('#' + dropid).val(r.d);
               // $("#" + dropid).attr("disabled", "disabled"); // change by anil 28-12-2022
                blockvalue('', r.d, ProposalId);
                AutoFillUtility(distid, ProposalId);
                //ULBvalue(distid); Sushant
                //WARDvalue(distid);
                //ULBvaluePremises(distid);
            }
        }
    });
    return distid;
}

/*--------------------------------------------------------------------------------------------------*/

function blockvalue(query2, distid, ProposalId) {
    var blockid = "";
    query2 = "SELECT intBlockId FROM T_LandAndUtility WHERE vchProposalNo=" + ProposalId + "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            blockid = r.d;
            fillBlockDataAuto(distid, r.d);
            gpvaluePremises(r.d);
        }
    });
    return blockid;
}

/*--------------------------------------------------------------------------------------------------*/

function fillBlockDataAuto(selValue, setVal) {

    var dtSource = "M_ADM_LocationDetails";
    var dtValue = "intLevelDetailId";
    var dtText = "nvchLevelName";
    var dtLevel = "3";
    var query2 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM m_block  WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_8').html('');
            $('#sel_8').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if (setVal === this.Value) {
                    $('#sel_8').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_8').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
            if (setVal !== "0" && setVal !== undefined && setVal !== 'NaN') {
               // $("#sel_8").attr("disabled", "disabled"); //// change by anil 28-12-2022
            }
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function fillBlockDataAutoByText(selValue, setVal) {   
    var SelBlockvalue = "0";
    var query2 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM m_block  WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_8').html('');
            $('#sel_8').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if (setVal === this.Text) {
                    SelBlockvalue = this.Value;
                    $('#sel_8').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_8').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
            gpvaluePremises(SelBlockvalue);
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function gpvaluePremises(distid) {
    var query2 = "SELECT intGPId AS COLUMN_NAME_VALUE , vchGPName AS COLUMN_NAME_TEXT FROM M_GP  WHERE intBlockId=" + parseInt(distid) + " ORDER BY vchGPName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_66').html('');
            $('#sel_66').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_66').attr("EditData") === this.Text) {
                    $('#sel_66').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_66').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function AutoFillUtility(distid, ProposalId) {
    query2 = "SELECT distinct intDiscomeid AS COLUMN_NAME_VALUE  FROM T_Energy_EI_Block_ServiceUserMapping AS M WHERE  intDistrictId=" + parseInt(distid) + "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            if (r.d !== "") {
                $('#sel_1').val(r.d);
                headerFill(r.d);
                FillDivision_Utility(distid);
                FillUtility_District(distid);
                $("#sel_1").attr("disabled", "disabled");
            }
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function ULBvalue(distid) {    
    var query2 = "SELECT intULBId AS COLUMN_NAME_VALUE , vchULBName AS COLUMN_NAME_TEXT FROM M_ULB  WHERE intDistrictId=" + parseInt(distid) + " ORDER BY vchULBName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_62').html('');
            $('#sel_62').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_62').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function ULBvalueByText(distid) {   
    var query2 = "SELECT intULBId AS COLUMN_NAME_VALUE , vchULBName AS COLUMN_NAME_TEXT FROM M_ULB  WHERE intDistrictId=" + parseInt(distid) + " ORDER BY vchULBName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_62').html('');
            $('#sel_62').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_62').attr("EditData") === this.Text) {
                    $('#sel_62').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_62').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function WARDvalue(distid) {   
    debugger;
    var query2 = "SELECT intWARDId AS COLUMN_NAME_VALUE , vchWARDName AS COLUMN_NAME_TEXT FROM M_WARD  WHERE intDistrictId=" + parseInt(distid) + " ORDER BY vchWARDName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_64').html('');
            $('#sel_64').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {              

                if ($('#sel_64').attr("EditData") === this.Text) {
                    $('#sel_64').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_64').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function WARDvalueByText(distid) {
    debugger;
    var query2 = "SELECT intWARDId AS COLUMN_NAME_VALUE , vchWARDName AS COLUMN_NAME_TEXT FROM M_WARD  WHERE intDistrictId=" + parseInt(distid) + " ORDER BY vchWARDName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_64').html('');
            $('#sel_64').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_64').attr("EditData") === this.Text) {
                    $('#sel_64').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_64').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/
///Fill ULB against District (For Premises District).
/*--------------------------------------------------------------------------------------------------*/
function ULBvaluePremises(distid) {
    var query2 = "SELECT intULBId AS COLUMN_NAME_VALUE , vchULBName AS COLUMN_NAME_TEXT FROM M_ULB WHERE intDistrictId=" + parseInt(distid) + " ORDER BY vchULBName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_65').html('');
            $('#sel_65').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_65').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function ULBvaluePremisesByText(distid) {   
    var query2 = "SELECT intULBId AS COLUMN_NAME_VALUE , vchULBName AS COLUMN_NAME_TEXT FROM M_ULB WHERE intDistrictId=" + parseInt(distid) + " ORDER BY vchULBName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_65').html('');
            $('#sel_65').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_65').attr("EditData") === this.Text) {
                    $('#sel_65').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_65').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function WARDvaluePremises(distid) {
    var query2 = "SELECT intWARDId AS COLUMN_NAME_VALUE , vchWARDName AS COLUMN_NAME_TEXT FROM M_WARD WHERE intDistrictId=" + parseInt(distid) + " ORDER BY vchWARDName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_67').html('');
            $('#sel_67').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_67').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function WARDvaluePremisesByText(distid) {
    var query2 = "SELECT intWARDId AS COLUMN_NAME_VALUE , vchWARDName AS COLUMN_NAME_TEXT FROM M_WARD WHERE intDistrictId=" + parseInt(distid) + " ORDER BY vchWARDName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_67').html('');
            $('#sel_67').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_67').attr("EditData") === this.Text) {
                    $('#sel_67').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_67').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function gpvalue(distid) {
    var query2 = "SELECT intGPId AS COLUMN_NAME_VALUE , vchGPName AS COLUMN_NAME_TEXT FROM M_GP WHERE intBlockId=" + parseInt(distid) + " ORDER BY vchGPName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_63').html('');
            $('#sel_63').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_63').attr("EditData") === this.Text) {
                    $('#sel_63').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_63').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function FillUtility() {
    var selValue = $('#sel_7').val();
    var query1 = " SELECT distinct intDiscomeid AS COLUMN_NAME_VALUE,(SELECT nvchLevelname FROM  M_adm_leveldetails  T WHERE T.intLevelDetailId=M.intdiscomeid)  AS COLUMN_NAME_TEXT  FROM T_Energy_Block_ServiceUserMapping AS M WHERE  intDistrictId=" + parseInt(selValue) + " ";
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
                $('#sel_1').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function checkboxDet() {
    if (document.getElementById("sm_15").checked) {
        $('#txt_9').val($('#txt_5').val());
        $('#sel_7').val($('#sel_9').val());
        var selValue = $('#sel_9').val();       

        var query2 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM m_block  WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_8').html('');
                $('#sel_8').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    if ($('#sel_10').val() === this.Value) {
                        $('#sel_8').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                    }
                    else {
                        $('#sel_8').append($("<option></option>").val(this.Value).html(this.Text));
                    }
                });
            }
        });
    }
    else {
        $('#txt_9').val('');
        $('#drpCorCountry').val('0');
        $('#sel_7').val('0');
        $('#sel_8').val('0');
        $('#sel_8').html('');
        $('#sel_8').append($("<option></option>").val('0').html('Select'));
    }
}

/*--------------------------------------------------------------------------------------------------*/

function headerFill(setVal) {
    var h1 = 'NESCO Utility';
    var h2 = 'SOUTHCO Utility';
    var h3 = 'WESCO Utility';
    var h4 = 'CESU';
    var p1 = 'Government of Odisha';
    var p2 = 'Power Connection Application';
    var imgurl = "";

    if (setVal === "365") {
        $('#divHeaderId').show();
        imgurl = 'images/NESCOLogo.jpg';
        $('#divHeaderId').html('<center><img src="' + imgurl + '"></center>' + '<h2>' + h1 + '</h2>' + '<p>' + p1 + '</p>' + '<p>' + p2 + '</p>');
    }
    else if (setVal === "366") {
        $('#divHeaderId').show();
        imgurl = 'images/SOUTHCOLogo.jpg';
        $('#divHeaderId').html('<center><img src="' + imgurl + '"></center>' + '<h2>' + h2 + '</h2>' + '<p>' + p1 + '</p>' + '<p>' + p2 + '</p>');
    }
    else if (setVal === "367") {
        $('#divHeaderId').show();
        imgurl = 'images/WESCOLogo.jpg';
        $('#divHeaderId').html('<center><img src="' + imgurl + '"></center>' + '<h2>' + h3 + '</h2>' + '<p>' + p1 + '</p>' + '<p>' + p2 + '</p>');
    }
    else if (setVal === "368") {
        $('#divHeaderId').show();
        imgurl = 'images/CESUlogo.jpg';
        $('#divHeaderId').html('<center><img src="' + imgurl + '"></center>' + '<h2>' + h4 + '</h2>' + '<p>' + p1 + '</p>' + '<p>' + p2 + '</p>');
    }
}

/*--------------------------------------------------------------------------------------------------*/