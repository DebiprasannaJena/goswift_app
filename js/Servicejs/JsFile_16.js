
$(document).ready(function () {
    debugger;

    AmountValidate(0, 0, 0);
    fillUtilityFirst(0);
    BindBlockAddress();
    BindGpAddress();

    /*--------------------------------------------------------------------------------------------------*/
    $('#sel_8').val($('#sel_7').val());// select  value '0' block add by  anil 28-12-2022 for select show
    $('#sel_66').val($('#sel_8').val()); // select value '0' gp add by  anil 28-12-2022 for select show
    $('#div_rad_2235').hide();
    $('#txt_20').attr("readonly", false);
    
    /*--------------------------------------------------------------------------------------------------*/

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
    ProposalId = $('#hdnProposalNo').val();
    if (ProposalId !== "") {
        var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + "";
        var distiD = distvalue(query, 'sel_7', ProposalId);
    }
    else {
        
        BindWARDAddress();
    }

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
            jAlert("End date can not be greater than 6 month!!");
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
    });
    /*--------------------------------------------------------------------------------------------------*/

    $("#btnSubmit").attr("disabled", true); //-----for disble button

    $('#rad_21').click(function () {
        if (document.getElementById('rad_21').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#txt_12").focusout(function (me) {
        if ($("#txt_12").val() === "") {
            return true;
        }
        else {
            var len = $("#txt_12").val().length;
            if (len < 10) {
                jAlert('Mobile number should contain 10 digits!');
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
                jAlert('Aadhaar Number should contain 12 digits!');
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
                jAlert('PAN number should contain 10 digits!');
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

    $("#div_rad_224").removeClass("reqR rset");
    $("#div_rad_2246").hide();
    //var x;

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

            jAlert("<strong>The infrastructure which is located in the IDCO industrial area.</strong>");
        }
        else if ($('input:radio[name=InfrastructerAvailable]:checked').val() === "No") {
            x = 0;
            if ($('input:radio[name=EnergyRequired]:checked').val() === "HT") {
                y = 0;
            }
            if ($('input:radio[name=EnergyRequired]:checked').val() === "LT") {
                y = 0;
            }

            jAlert("<strong>The cost will be calculated as per the actual estimate prepared by DISCOM engineer on the basis of OERC norms and site conditions.</strong>");
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

    /*--------------------------------------------------------------------------------------------------*/

    $("#txt_14").change(function () {
        NewCalculation();
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#txt_15").change(function () {
        //var txt_14 = document.getElementById('txt_14').value;
        //var txt_15 = document.getElementById('txt_15').value;
        //if (txt_14 === "")
        //    txt_14 = 0;
        //if (txt_15 === "")
        //    txt_15 = 0;
        //var result = parseInt(txt_14) + parseInt(txt_15); //+ parseInt(txtstdir) + parseInt(txtstind);
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

        NewCalculation();
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
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";
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
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";
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

    $('#txt_19').removeClass('req rset');
    $('#div_txt_194').hide();
    $('#div_rad_2222').hide();

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
        }
        else {
            $('#txt_19').removeClass('req rset');
            $('#div_txt_194').hide();
        }
    });

    /*--------------------------------------------------------------------------------------------------*/

    $('#div_sel_646').hide();
    $('#div_sel_624').hide();    

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

    $('#div_sel_654').hide();
    $('#div_sel_676').hide();
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

    $("#sel_9").change(function () {
        var selValue = $('#sel_9').val();
        var dtLevel = "3";
        var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";
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

        query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";
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
    });

    /*--------------------------------------------------------------------------------------------------*/

    ULBvalue($("#sel_9").val());
    WARDvalue($("#sel_9").val());

    /*--------------------------------------------------------------------------------------------------*/

    $("#sel_10").change(function () {
        gpvalue($("#sel_10").val());
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#sel_7").change(function () {
        var selValue = $('#sel_7').val();
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
                $('#sel_8').html('');
                $('#sel_8').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_8').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });
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
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });
    });
});

/*--============================================================================================================================================================--*/

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
});

/*--------------------------------------------------------------------------------------------------*/
///Amount Calculation
/*--------------------------------------------------------------------------------------------------*/
function NewCalculation() {
    var txt_14 = document.getElementById('txt_14').value;
    var txt_15 = document.getElementById('txt_15').value;
    if (txt_14 === "") {
        txt_14 = 0;
    }      
    if (txt_15 === "") {
        txt_15 = 0;
    }       
    var result = parseInt(txt_14) + parseInt(txt_15); //+ parseInt(txtstdir) + parseInt(txtstind);
    if (result < 70) {
        $('#div_rad_2235').show();
        $("#div_rad_224").removeClass("reqR rset");
        $('#rad_2231').attr('disabled', false);
        $("#div_rad_2246").hide();
    }
  else if (result > 70 && result < 1111) {
        $('#div_rad_2235').show();
        $('#rad_2231').attr('disabled', true);
        $("#div_rad_224").removeClass("reqR rset");
        $("#div_rad_2246").hide();
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
    else {
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
///Fill DISCOMs/Utility Names
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
                $('#sel_1').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });

    FillHeader(setVl);
    //FillDivision_Utility(setVl); Sushant
    //FillUtility_District(setVl);Sushant
}

/*--------------------------------------------------------------------------------------------------*/
///Fille Header Images as per the DISCOM/Utility Name
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
///Fill Division Name against Utility/DISCOM.
/*--------------------------------------------------------------------------------------------------*/
function FillDivision_Utility(setVl) {
    var selValue = "";

    if ($('#sel_1').val() !== "0") {
        selValue = $('#sel_1').val();        
    }
    else {
        selValue = setVl;       
    }   

    var selValue1 = $('#sel_9').val();
    var dtLevel = "3";
    var query1 = "SELECT L.intLevelDetailId as COLUMN_NAME_VALUE,L.nvchLevelName AS COLUMN_NAME_TEXT FROM [dbo].[T_Energy_Block_ServiceUserMapping] As M INNER JOIN M_ADM_LevelDetails AS L ON L.intLevelDetailId=M.intLevelDetailId WHERE M.intDiscomeid=" + parseInt(selValue) + " AND L.bitStatus=1 ORDER BY L.nvchLevelName"
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
                $('#sel_11').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/
//Fill District against Utility/DISCOM.
/*--------------------------------------------------------------------------------------------------*/
function FillUtility_District(setVl) {
    var selValue = "";

    if ($('#sel_1').val() !== "0") {
        selValue = $('#sel_1').val();
    }
    else {
        selValue = setVl;
    }

    var selValue1 = $('#sel_7').val();
    var dtLevel = "3";
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
            $('#sel_7').html('');
            $('#sel_7').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if (this.Value === setVl) {
                    $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text).attr("selected", "selected"));
                }
                else {
                    $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/
///Fill District According to proposal number
/*--------------------------------------------------------------------------------------------------*/
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
            if (r.d != "") {
                $('#' + dropid).val(r.d);
               // $("#" + dropid).attr("disabled", "disabled"); //// change by anil 28-12-2022
                blockvalue('', r.d, ProposalId);
                AutoFillUtility(distid, ProposalId); 
                ULBvalue(distid);
                WARDvalue(distid);
                ULBvaluePremises(distid);               
                WARDvaluePremises(distid);
            }
        }
    });
    return distid;
}

/*--------------------------------------------------------------------------------------------------*/
///Fill Block According to proposal number
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
///Fill BlockDataAuto According to proposal number
/*--------------------------------------------------------------------------------------------------*/
function fillBlockDataAuto(selValue, setVal) {
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
                if (setVal === this.Value) {
                    $('#sel_8').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_8').append($("<option></option>").val(this.Value).html(this.Text));
                }

               
            });
            //$("#sel_8").attr("disabled", "disabled");  // change by anil 28-12-2022
        }
    });    
}

/*--------------------------------------------------------------------------------------------------*/
///Fill GPDataAuto According to proposal number
/*--------------------------------------------------------------------------------------------------*/
function gpvaluePremises(distid) {
    var query2 = "SELECT intGPId AS COLUMN_NAME_VALUE , vchGPName AS COLUMN_NAME_TEXT FROM M_GP WHERE intBlockId=" + parseInt(distid) + " ORDER BY vchGPName";
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
                $('#sel_66').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/
///Begin Fill Utility According to proposal number
/*--------------------------------------------------------------------------------------------------*/
function AutoFillUtility(distid, ProposalId) {
    var selValue = $('#sel_7').val();
    query2 = "select distinct intDiscomeid as COLUMN_NAME_VALUE  from T_Energy_EI_Block_ServiceUserMapping as M where  intDistrictId=" + parseInt(distid) + ""
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            if (r.d != "") {
                $('#sel_1').val(r.d);
                headerFill(r.d);
                FillDivision_Utility(distid);
                ///FillDivision_Utility(r.d); /// Sushant
                FillUtility_District(distid);
                $("#sel_1").attr("disabled", "disabled");
            }
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/
///Fill Header According to proposal number
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
///Begin Fill ULB According to proposal number
/*--------------------------------------------------------------------------------------------------*/
function ULBvalue(distid) {
    var query2 = "SELECT intULBId AS COLUMN_NAME_VALUE , vchULBName AS COLUMN_NAME_TEXT FROM M_ULB WHERE intDistrictId=" + parseInt(distid) + " ORDER BY vchULBName";
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
///Begin Fill WARD According to proposal number
/*--------------------------------------------------------------------------------------------------*/
function WARDvalue(distid) {
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
                $('#sel_64').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/
///Begin Fill ULB Premises According to proposal number
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

//function ULBvaluePremises(distid) {
//    var query2 = "select intULBId as COLUMN_NAME_VALUE , vchULBName as COLUMN_NAME_TEXT from M_ULB  where intDistrictId=" + parseInt(distid) + " order by vchULBName";
//    $.ajax({
//        type: "POST",
//        url: "FormView.aspx/FillDemographyData",
//        data: "{'query':'" + query2 + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (r) {
//            $('#sel_65').html('');
//            $('#sel_65').append($("<option></option>").val('0').html('Select'));
//            $.each(r.d, function () {
//                $('#sel_65').append($("<option></option>").val(this.Value).html(this.Text));
//            });
//        }
//    });
//}

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

function gpvalue(blkid) {  
    var query2 = "SELECT intGPId AS COLUMN_NAME_VALUE , vchGPName AS COLUMN_NAME_TEXT FROM M_GP WHERE intBlockId=" + parseInt(blkid) + " ORDER BY vchGPName";
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
                $('#sel_63').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function BindBlockAddress() {
    var query2 = "SELECT intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT FROM M_Block ORDER BY vchBlockName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_10').html('');
            $('#sel_10').append($("<option></option>").val('0').html('Select'));
            $('#sel_4').html('');
            $('#sel_4').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_10').append($("<option></option>").val(this.Value).html(this.Text));
                $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function BindGpAddress() {
    var query2 = "SELECT intGPId AS COLUMN_NAME_VALUE , vchGPName AS COLUMN_NAME_TEXT FROM M_GP ORDER BY vchGPName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {       
            $('#sel_63').html('');
            $('#sel_63').append($("<option></option>").val('0').html('Select'));
            for (var i = 0; i <= r.d.length - 1; i++) {
                $('#sel_63').append($("<option></option>").val(r.d[i].Value).html(r.d[i].Text));
            }           
        },
        error: function (textStatus, errorThrown) {
            $('#sel_63').html('');
            $('#sel_63').append($("<option></option>").val('0').html('Select'));
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/

function BindWARDAddress() {
    var query2 = "SELECT intWARDId AS COLUMN_NAME_VALUE , vchWARDName AS COLUMN_NAME_TEXT FROM M_WARD ORDER BY vchWARDName";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_67').html('');
            $('#sel_67').append($("<option></option>").val('0').html('Select'));
            $('#sel_64').html('');
            $('#sel_64').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_67').append($("<option></option>").val(this.Value).html(this.Text));
                $('#sel_64').append($("<option></option>").val(this.Value).html(this.Text));
            });
        },
        error: function (textStatus, errorThrown) {
            $('#sel_67').html('');
            $('#sel_67').append($("<option></option>").val('0').html('Select'));
            $('#sel_64').html('');
            $('#sel_64').append($("<option></option>").val('0').html('Select'));
        }
    });
}

/*--------------------------------------------------------------------------------------------------*/