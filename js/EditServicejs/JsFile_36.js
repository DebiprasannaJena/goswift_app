$(document).ready(function () {

    //------------------------EditData------------------------------------------------------------
    jQuery("input[name='Type']").each(function (i) {
        jQuery(this).attr('disabled', 'disabled');
    });

    if ($('#sel_5').attr("EditData") !== 'undefined' && $('#sel_5').attr("EditData") !== "" && $('#sel_5').attr("EditData") !== null) {
        EditfillBlockDataAuto($('#sel_4').val(), $('#sel_5').attr("EditData"));
    }

    if ($('#sel_3').attr("EditData") !== 'undefined' && $('#sel_3').attr("EditData") !== "" && $('#sel_3').attr("EditData") !== null) {        
        fillBlockDataAuto($('#sel_2').val(), $('#sel_3').attr("EditData"));
    }

    FillSubDivision($('#sel_4').val());
    //------------------------------------------------------------------------------------------------
    yearFill();
    $('#txt_33').attr("readonly", false);
    $('#txt_42').attr("readonly", false);
    $('#txt_41').attr("readonly", false);
    $('#txt_45').attr("disabled", "disabled");
    $("#divsmchk_txt_54").show();   //-----for show the same as 

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

    var ProposalId = "";
    var FormId = "";
    var vrtxt_47 = "";
    var urlparam = "";
    //--datevalidation--------------
    $('#txt_44').datetimepicker({
        format: 'DD-MMM-YYYY'
    }).on('dp.show', function () {
        return $(this).data('DateTimePicker').minDate(new Date());
    });

    //----------------AutoFill Dropdwon-------------------------
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
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
    if (ProposalId !== "") {
        var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + "";
        var distiD = distvalue(query, 'sel_2', ProposalId);
    }

    //---------------validatio---------

    $('#txt_32').keypress(function (e) {
        AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-';
        var k;
        k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
        if (k !== 13 && k !== 8 && k !== 0) {
            if ((e.ctrlKey === false) && (e.altKey === false)) {
                return (AllowableCharacters.indexOf(String.fromCharCode(k)) !== -1);
            }
            else {                
                $('#txt_32').val("");
                return true;
            }
        }
        else {            
            $('#txt_32').val("");
            return true;
        }
    });

    $('#Nameofowneroragent_FN').keypress(function (e) {
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

    $('#Nameofowneroragent_MN').keypress(function (e) {
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

    $('#Nameofowneroragent_LN').keypress(function (e) {
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

    $('#txt_51').keypress(function (e) {
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

    $("#txt_41").focusout(function (me) {
        
        if ($("#txt_41").val() === "") {
            return true;
        }
        else {
            var len = $("#txt_41").val().length;
            if (len < 10) {
                jAlert('Mobile number should contain 10 digits!');
                $('#txt_41').val("");
                $('#txt_41').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });
    //-----------------------------
    $('#div_txt_322').hide();
    $('#txt_32').removeClass("req rset");

    //    $('#txt_32').hide();
    //    $("#txt_32").removeClass("req rset");

    $('input:radio[name=Type]').change(function () {
        var query2 = "";
        var vrType = ($('input:radio[name=Type]:checked').val());
        var vrBoilerType = ($('input:radio[name=TypeBoiler]:checked').val());
        if (vrType === "Renewal") {

            $('#sel_53').removeClass("req rset");
            $('#txt_51').removeClass("req rset");
            $('#lbl_txt_511').find('span').css('display', 'none');
            $('#txt_52').removeClass("req rset");
            $('#lbl_txt_522').find('span').css('display', 'none');
            $('#txt_45').removeClass("req rset");
            $('#lbl_sel_533').find('span').css('display', 'none');
            $('#sel_53').removeClass("req rset");
            $('#lbl_txt_454').find('span').css('display', 'none');
            $('#txt_46').removeClass("req rset");
            $('#lbl_txt_465').find('span').css('display', 'none');
            $("#div_txt_322").show();
            $('#txt_32').addClass("req rset");
            vrtxt_47 = $('#txt_47').val();
            if (vrBoilerType === "Yes") {
                AmountDetails(1000);
            }
            else if (vrBoilerType === "No") {
                if (vrtxt_47 !== "") {
                    if (parseFloat(vrtxt_47) <= 3000) {
                        query2 = "select top(1)intFees from T_Renewal_Boiler where intRenewalBoiler >=" + parseFloat(vrtxt_47) + " order by intFees asc";
                        CalcuByProcedure(query2);
                    }
                    else {
                        vrtxt_47 = "3000";
                        query2 = "select top(1)intFees from T_Renewal_Boiler where intRenewalBoiler >=" + parseFloat(vrtxt_47) + " order by intFees asc";
                        CalcuByProcedure1(query2);
                    }
                }
                else {
                    AmountDetails(0);
                }
            }
            else {
                AmountDetails(0);
            }

        }
        else if (vrType === "New") {
            $('#div_txt_322').hide();

            $('#sel_53').addClass("req rset");
            $('#txt_32').removeClass("req rset");
            $('#txt_51').addClass("req rset");
            $('#lbl_txt_511').find('span').css('display', 'block');
            $('#txt_52').addClass("req rset");
            $('#lbl_txt_522').find('span').css('display', 'block');
            $('#txt_45').addClass("req rset");
            $('#lbl_sel_533').find('span').css('display', 'block');
            $('#sel_53').addClass("req rset");
            $('#lbl_txt_454').find('span').css('display', 'block');
            $('#txt_46').addClass("req rset");
            $('#lbl_txt_465').find('span').css('display', 'block');
            vrtxt_47 = $('#txt_47').val();
            if (vrBoilerType === "Yes") {
                AmountDetails(1200);
            }
            else if (vrBoilerType === "No") {
                if (vrtxt_47 !== "") {
                    if (parseFloat(vrtxt_47) <= 3000) {
                        query2 = "select top(1)intPrice from T_New_Boilerrating where intNewBoilerRating >=" + parseFloat(vrtxt_47) + " order by intPrice asc";
                        CalcuByProcedure(query2);
                    }
                    else {
                        vrtxt_47 = "3000";
                        query2 = "select top(1)intPrice from T_New_Boilerrating where intNewBoilerRating >=" + parseFloat(vrtxt_47) + " order by intPrice asc";
                        CalcuByProcedure1(query2);
                    }
                }
                else {
                    AmountDetails(0);
                }
            }
            else {
                AmountDetails(0);
            }

        }
    });

    $('input:radio[name=TypeBoiler]').change(function () {
        var query2 = "";
        var vrType = ($('input:radio[name=Type]:checked').val());
        var vrBoilerType = ($('input:radio[name=TypeBoiler]:checked').val());
        //        $("#lbl_fil_1415").show();
        //        $("#txt_32").show();
        //        $("#lbl_txt_322").show();
        var vrtxt_47 = $('#txt_47').val();
        if (vrBoilerType === "Yes") {
            if (vrType === "Renewal") {
                AmountDetails(1000);
            }
            else if (vrType === "New") {
                AmountDetails(1200);
            }
            else {
                AmountDetails(0);
            }
        }
        else if (vrBoilerType === "No") {

            if (vrType === "Renewal") {
                if (vrtxt_47 !== "") {
                    if (parseFloat(vrtxt_47) <= 3000) {
                        query2 = "select top(1)intFees from T_Renewal_Boiler where intRenewalBoiler >=" + parseFloat(vrtxt_47) + " order by intFees asc";
                        CalcuByProcedure(query2);
                    }
                    else {
                        //                        AmountDetails(0);
                        //                        $('#txt_47').val("");
                        vrtxt_47 = "3000";
                        query2 = "select top(1)intFees from T_Renewal_Boiler where intRenewalBoiler >=" + parseFloat(vrtxt_47) + " order by intFees asc";
                        CalcuByProcedure1(query2);
                    }
                }
                else {
                    AmountDetails(0);
                }
            }
            else if (vrType === "New") {
                if (vrtxt_47 !== "") {
                    if (parseFloat(vrtxt_47) <= 3000) {
                        query2 = "select top(1)intPrice from T_New_Boilerrating where intNewBoilerRating >=" + parseFloat(vrtxt_47) + " order by intPrice asc";
                        CalcuByProcedure(query2);
                    }
                    else {
                        vrtxt_47 = "3000";
                        query2 = "select top(1)intPrice from T_New_Boilerrating where intNewBoilerRating >=" + parseFloat(vrtxt_47) + " order by intPrice asc";
                        CalcuByProcedure1(query2);
                    }
                }
                else {
                    AmountDetails(0);
                }

            }
            else {
                AmountDetails(0);
            }
        }
        else {
            return true;
        }
    });

    $("#txt_47").change(function () {
        var query2 = "";
        var dval = $("#txt_47").val();
        var ival = parseFloat($("#txt_47").val());
        var fval = parseFloat($("#txt_47").val());
        var vrType = ($('input:radio[name=Type]:checked').val());
        var vrBoilerType = ($('input:radio[name=TypeBoiler]:checked').val());


        var vrtxt_47 = $('#txt_47').val();
        if (vrBoilerType === "Yes") {
            if (vrType !== "") {
                AmountDetails(1000);
            }

            else {
                AmountDetails(0);
            }
        }
        else if (vrBoilerType === "No") {
            if (vrType === "Renewal") {
                if (parseFloat(vrtxt_47) <= 3000) {
                    query2 = "select top(1)intFees from T_Renewal_Boiler where intRenewalBoiler >=" + parseFloat(vrtxt_47) + " order by intFees asc";
                    CalcuByProcedure(query2);
                }
                else {
                    // AmountDetails(0);
                    vrtxt_47 = "3000";
                    query2 = "select top(1)intFees from T_Renewal_Boiler where intRenewalBoiler >=" + parseFloat(vrtxt_47) + " order by intFees asc";
                    CalcuByProcedure1(query2);
                }
            }
            else if (vrType === "New") {
                if (parseFloat(vrtxt_47) <= 3000) {
                    query2 = "select top(1)intPrice from T_New_Boilerrating where intNewBoilerRating >=" + parseFloat(vrtxt_47) + " order by intPrice asc";
                    CalcuByProcedure(query2);
                }
                else {
                    vrtxt_47 = "3000";
                    query2 = "select top(1)intPrice from T_New_Boilerrating where intNewBoilerRating >=" + parseFloat(vrtxt_47) + " order by intPrice asc";
                    CalcuByProcedure1(query2);
                }
            }
            else {
                AmountDetails(0);
            }
        }
        else {
            AmountDetails(0);
        }

    });


    $("#txt_100").change(function () {

        var query2 = "";
        var dval = $("#txt_47").val();
        var ival = parseFloat($("#txt_47").val());
        var fval = parseFloat($("#txt_47").val());
        var vrType = ($('input:radio[name=Type]:checked').val());
        var vrBoilerType = ($('input:radio[name=TypeBoiler]:checked').val());


        var vrtxt_47 = $('#txt_47').val();
        if (vrBoilerType === "Yes") {
            if (vrType !== "") {
                AmountDetails(1000);
            }

            else {
                AmountDetails(0);
            }
        }
        else if (vrBoilerType === "No") {
            if (vrType === "Renewal") {
                if (parseFloat(vrtxt_47) <= 3000) {
                    query2 = "select top(1)intFees from T_Renewal_Boiler where intRenewalBoiler >=" + parseFloat(vrtxt_47) + " order by intFees asc";
                    CalcuByProcedure(query2);
                }
                else {
                    // AmountDetails(0);
                    vrtxt_47 = "3000";
                    query2 = "select top(1)intFees from T_Renewal_Boiler where intRenewalBoiler >=" + parseFloat(vrtxt_47) + " order by intFees asc";
                    CalcuByProcedure1(query2);
                }
            }
            else if (vrType === "New") {
                if (parseFloat(vrtxt_47) <= 3000) {
                    query2 = "select top(1)intPrice from T_New_Boilerrating where intNewBoilerRating >=" + parseFloat(vrtxt_47) + " order by intPrice asc";
                    CalcuByProcedure(query2);
                }
                else {
                    vrtxt_47 = "3000";
                    query2 = "select top(1)intPrice from T_New_Boilerrating where intNewBoilerRating >=" + parseFloat(vrtxt_47) + " order by intPrice asc";
                    CalcuByProcedure1(query2);
                }
            }
            else {
                AmountDetails(0);
            }
        }
        else {
            AmountDetails(0);
        }

    });

    //    $('#txt_47').blur(function () {
    //        var vrType = ($('input:radio[name=Type]:checked').val());
    //        var vrBoilerType = ($('input:radio[name=TypeBoiler]:checked').val());
    //        if (vrType == "Renewal") {
    //            if (vrBoilerType == "No") {
    //                if (parseFloat($('#txt_47').val()) > 2999) {
    //                    $('#txt_47').val("");
    //                }
    //            }

    //        }
    //    });

    var txt33 = $('#txt_33').val();

    $("#sel_1").change(function () {

        var vrnm = "";
        if ($("#sel_1").val() === "Agent") {
            vrnm = "Name of " + $("#sel_1").val();
            $('#lbl_txt_334').html(vrnm);
            $('#txt_33').val("");
            $("#txt_33").attr("readonly", false);
            //document.getElementById("txt_33").disable = 'false';
        }
        else if ($("#sel_1").val() === "Owner") {
            vrnm = "Name of " + $("#sel_1").val();
            $('#lbl_txt_334').html(vrnm);
            $('#txt_33').val(txt33);
            if (txt33 !== "") {
                $("#txt_33").attr("readonly", true);
            }
        }
    });

    $("#sel_2").change(function () {

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
                $('#sel_3').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });


    });

    $("#sel_4").change(function () {
        
        var selValue = $('#sel_4').val();
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
                $('#sel_5').html('');
                $('#sel_5').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_5').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });

        //----------------------division----------
        FillSubDivision(selValue);
        //        var query1 = "select L.intleveldetailid COLUMN_NAME_VALUE,L.nvchLevelname COLUMN_NAME_TEXT  from [dbo].[T_FB_Subdiv_ServiceUserMapping] As M inner join M_adm_leveldetails as L  on L.intLevelDetailId=M.intLevelDetailId where intDistrictId=" + parseInt(selValue) + " order by nvchLevelname asc";

        //        $.ajax({
        //            type: "POST",
        //            url: "FormView.aspx/FillDemographyData",
        //            data: "{'query':'" + query1 + "'}",
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (r) {
        //                $('#sel_6').html('');
        //                $('#sel_6').append($("<option></option>").val('0').html('Select'))
        //                $.each(r.d, function () {
        //                    $('#sel_6').append($("<option></option>").val(this.Value).html(this.Text));
        //                })
        //            }
        //        });
    });

    $('#smchk_txt_54').click(function () {
        
        if (document.getElementById("smchk_txt_54").checked) {
            $('#sel_4').val($('#sel_2').val());


            var selValue = $('#sel_4').val();
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
                    $('#sel_5').html('');
                    $('#sel_5').append($("<option></option>").val('0').html('Select'));
                    $.each(r.d, function () {
                        //                        $('#sel_5').append($("<option></option>").val(this.Value).html(this.Text));
                        //                        console.log(r.d);
                        if ($('#sel_3').val() === this.Value) {
                            $('#sel_5').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_5').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    });
                }
            });
            FillSubDivision(selValue);
            
            //var s = $('#sel_3').val();
            $('#sel_5').val($('#sel_3').val());
            $('#txt_54').val($('#txt_55').val());
            //alert('hi');
            //            var sel_3 = $('#sel_5 option:selected').val();
            //            $('#sel_5 option[value=' + sel_2 + ']').attr('selected','selected');
        } else {
            //Clear on uncheck
            $('#sel_4').val("0");
            $('#sel_5').val("0");
            $('#sel_5').html('');
            $('#sel_5').append($("<option></option>").val('0').html('Select'))
            $('#txt_54').val("");
            $('#sel_6').val("0");
            $('#sel_6').html('');
            //            $('#sel_5 option[value=Nothing]').attr('selected','selected');
        }
    });

    $('#sel_53').change(function () {       
        calculateAge();
    });
});

function calculateAge() {
    
    var mnth;
    var cuurentYear = (new Date()).getFullYear();
    var Year;
    if ($('#sel_53').val() !== "") {
        var difVal = parseInt(cuurentYear) - parseInt($('#sel_53').val());
        $('#txt_45').val(difVal + " Year");
        $('#txt_45').attr("disabled", "disabled");
    }
    else {
        $('#txt_45').val("0 Year");
        $('#txt_45').attr("disabled", "disabled");
    }
}

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
            //  alert(r.d);
            if (r.d !== "") {
                $('#' + dropid).val(r.d);
                //fillBlockDataAuto(r.d);
                $("#" + dropid).attr("disabled", "disabled");
                blockvalue('', r.d, ProposalId);
                //                FillSubDivision(r.d);
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
            // alert(r.d);
            //  alert(dropid);
            // $('#' + dropid).val(r.d);
            // fillBlockDataAuto(r.d);


            fillBlockDataAuto(distid, r.d);

            // return blockid;
        }

    });
    return blockid;

}




function fillBlockDataAuto(selValue, setVal) {

    

    // var selValue = $('#sel_6').val();
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
            $('#sel_3').html('');
            $('#sel_3').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                
                if (setVal === this.Text) {
                    $('#sel_3').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });

    $("#sel_3").attr("disabled", "disabled");
    //    var query = "select intExisBlock from T_PEAL_PROMOTER where vchProposalNo=" + ProposalId + ""
    //    var blockid = blockvalue(query, 'sel_5');

    //    alert(blockvalue(query, 'sel_5'));


}


function CalcuByProcedure(query) {

    $.ajax({
        type: "POST",
        url: "FormView.aspx/ForSpecialCondionStringReturn",
        data: "{'query':'" + query + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            AmountDetails(r.d);
        }
    });
}

function CalcuByProcedure1(query) {

    $.ajax({
        type: "POST",
        url: "FormView.aspx/ForSpecialCondionStringReturn",
        data: "{'query':'" + query + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            calculationexd3000(r.d);
        }
    });
}

function AmountDetails(amount) {
    
    var OnlyAmount = amount;
    var vrDistance = $('#txt_100').val();
    var varDisCal = 0;
    if (vrDistance != "") {
        //varDisCal = parseInt(vrDistance) * 2;
        if (parseInt(vrDistance) > 8) {
            varDisCal = parseInt(vrDistance) * 5 * 4;
            amount = parseInt(amount) + parseInt(varDisCal);
        }
        else {
            amount = amount;
        }

    }
    else {
        amount = amount;
    }
    //    if (varDisCal != 0 && varDisCal != "") {
    //        varDisCal = "0/-";
    //    }
    var strText = "";
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'> Amount</th><td width='50%'><b>" + OnlyAmount + "/-</b></td></tr><tr><th width='50%'>Transport Expense(TE) </th><td width='50%'><b>" + varDisCal + "/-</b></td></tr><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>"
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(amount);
}

function calculationexd3000(amount) {
    var amt1 = "";
    var amnt = "0";
    if (parseFloat($('#txt_47').val()) > 3000) {
        var vrType = ($('input:radio[name=Type]:checked').val());
        if (vrType === "Renewal") {
            amnt = ((parseFloat($('#txt_47').val()) - 3000) / 200) * 500;
        }
        else {
            amnt = ((parseFloat($('#txt_47').val()) - 3000) / 200) * 600;
        }

        amt1 = parseFloat(amnt) + parseInt(amount);
    }

    AmountDetails(amt1);
}
function yearFill() {
    var YearArray = new Array("1950", "1951", "1952", "1953", "1954", "1955", "1956", "1957", "1958", "1959", "1960", "1961", "1962", "1963", "1964", "1965", "1966", "1967", "1968", "1969", "1970", "1971", "1972", "1973", "1974", "1975", "1976", "1977", "1978", "1979", "1980", "1981", "1982", "1983", "1984", "1985", "1986", "1987", "1988", "1989", "1990", "1991", "1992", "1993", "1994", "1995", "1996", "1997", "1998", "1999", "2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018");
    //var YearArray = new Array( "2000", "2001", "2002", "2003", "2004", "2005", "2006", "2007", "2008", "2009", "2010", "2011", "2012", "2013", "2014", "2015", "2016", "2017", "2018");

    $('#sel_53').html('');
    $('#sel_53').append($("<option></option>").val('0').html('Year'));
    for (i = 0; i < YearArray.length; i++) {
        if ($('#sel_53').attr("EditData") === YearArray[i]) {
            $('#sel_53').append($("<option selected='true'></option>").val(YearArray[i]).html(YearArray[i]));
        }
        else {
            $('#sel_53').append($("<option></option>").val(YearArray[i]).html(YearArray[i]));
        }
    }
}
function FillSubDivision(selValue) {
    // alert(selValue);
    var query1 = "select distinct A.intLevelDetailId as COLUMN_NAME_VALUE,nvchLevelName as COLUMN_NAME_TEXT from M_adm_leveldetails A inner join T_FB_Subdiv_ServiceUserMapping B on A.intLevelDetailId=B.intLevelDetailId where intparentid in (select intLevelDetailId from M_adm_leveldetails where intparentid in(select intLevelDetailId from M_adm_leveldetails where intparentid=422)) and bitStatus=1 and bitDeletedFlag=0 and B.intDistrictId=" + selValue + "";

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
                if ($('#sel_6').attr("EditData") === this.Text) {
                    $('#sel_6').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_6').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

function EditCalculation() {
    var query2 = "";
    var vrType = ($('input:radio[name=Type]:checked').val());
    var vrBoilerType = ($('input:radio[name=TypeBoiler]:checked').val());
    if (vrType == "Renewal") {
        vrtxt_47 = $('#txt_47').val();
        if (vrBoilerType == "Yes") {
            AmountDetails(1000);
        }
        else if (vrBoilerType === "No") {
            if (vrtxt_47 !== "") {
                if (parseFloat(vrtxt_47) <= 3000) {
                    query2 = "select top(1)intFees from T_Renewal_Boiler where intRenewalBoiler >=" + parseFloat(vrtxt_47) + " order by intFees asc";
                    CalcuByProcedure(query2);
                }
                else {
                    vrtxt_47 = "3000";
                    query2 = "select top(1)intFees from T_Renewal_Boiler where intRenewalBoiler >=" + parseFloat(vrtxt_47) + " order by intFees asc";
                    CalcuByProcedure1(query2);
                }
            }
            else {
                AmountDetails(0);
            }
        }
        else {
            AmountDetails(0);
        }

    }
    else if (vrType === "New") {
        vrtxt_47 = $('#txt_47').val();
        if (vrBoilerType === "Yes") {
            AmountDetails(1200);
        }
        else if (vrBoilerType === "No") {
            if (vrtxt_47 !== "") {
                if (parseFloat(vrtxt_47) <= 3000) {
                    query2 = "select top(1)intPrice from T_New_Boilerrating where intNewBoilerRating >=" + parseFloat(vrtxt_47) + " order by intPrice asc";
                    CalcuByProcedure(query2);
                }
                else {
                    vrtxt_47 = "3000";
                    query2 = "select top(1)intPrice from T_New_Boilerrating where intNewBoilerRating >=" + parseFloat(vrtxt_47) + " order by intPrice asc";
                    CalcuByProcedure1(query2);
                }
            }
            else {
                AmountDetails(0);
            }
        }
        else {
            AmountDetails(0);
        }

    }
}

function EditfillBlockDataAuto(selValue, setVal) {
    
    // alert(setVal);
    var dtLevel = "3";
    var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_5').html('');
            $('#sel_5').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if (setVal === this.Text) {
                    $('#sel_5').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_5').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}