///**********************************************************************************************************************/
/// File Name   : JsFile_75.js
/// Description : JavaScript File for FormEditView.aspx
/// Service Id  : 75 (Trade Licence and Shop Establishment)
/// Created by  : Sushant Kumar Jena
/// Created On  : 17-Feb-2021 
/// <CR no.>            <Date>                <Modified by>        <Modification Summary>                      <Instructed By>                                                     

///**********************************************************************************************************************/

$(document).ready(function () {

    $("#btnSubmit").attr("disabled", true); //-----for disble button

    $('#chk_1').click(function () {
        if (document.getElementById('chk_1').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });


    if ($('#sel_1').val() !== 'undefined' && $('#sel_1').val() !== "" && $('#sel_1').val() !== null) {
        AutoBindDistrict();
    }
    else {
        BindDistrict();
    }

    /////----------------------------------------------------------------------------------------------------------------

    if ($('#sel_7').val() !== 'undefined' && $('#sel_7').val() !== "" && $('#sel_7').val() !== null) {
        AutoBindDistrict();
    }
    else {
        BindDistrict();
    }

    /////----------------------------------------------------------------------------------------------------------------

    if ($('#sel_2').val() !== 'undefined' && $('#sel_2').val() !== "" && $('#sel_2').val() !== null) {
        AutoBindDistrictULBMap();
    }
    else {
        BindDistrictULBMap();
    }

    /////----------------------------------------------------------------------------------------------------------------

    if ($('#sel_6').val() !== 'undefined' && $('#sel_6').val() !== "" && $('#sel_6').val() !== null) {
        AutoBindTradeTypes();
    }
    else {
        BindTradeTypes();
    }

    /////-------------------------------------------------------------------------------------------------------------------------------------------
    ///// Fill ULB against District

    $("#sel_2").change(function () {
        var selValue = $('#sel_2').val();
        var query2 = "SELECT vchULBCode AS COLUMN_NAME_VALUE,vchULBName AS COLUMN_NAME_TEXT FROM M_ULB_Master WHERE intDistrictId=" + parseInt(selValue) + " AND bitDeletedFlag=0 ";

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
                    $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });



});

/////----------------------------------------------------------------------------------------------------------------

function BindDistrict() {
    var query1 = "SELECT intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District WHERE intStateId=20  ORDER BY vchDistrictName ASC";
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
                $('#sel_1').append($("<option ></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/////----------------------------------------------------------------------------------------------------------------

function AutoBindDistrict() {
    var query1 = "SELECT intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT FROM M_District WHERE intStateId=20 ORDER BY vchDistrictName ASC";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_1').html('');
            $('#sel_1').append($("<option></option>").val('0').html('Select'));

            $('#sel_7').html('');
            $('#sel_7').append($("<option></option>").val('0').html('Select'));

            $.each(r.d, function () {
                if ($('#sel_1').attr("EditData") === this.Text) {
                    $('#sel_1').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_1').append($("<option></option>").val(this.Value).html(this.Text));
                }

                if ($('#sel_7').attr("EditData") === this.Text) {
                    $('#sel_7').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}


/////----------------------------------------------------------------------------------------------------------------

function BindDistrictULBMap() {
    var query1 = "SELECT intDistrictCode AS COLUMN_NAME_VALUE,nvchDistrictName AS COLUMN_NAME_TEXT FROM T_DISTRICT_MASTER WHERE bitDeletedFlag=0 ORDER BY nvchDistrictName ASC";
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
}

/////----------------------------------------------------------------------------------------------------------------

function AutoBindDistrictULBMap() {
    debugger;
    var query1 = "SELECT intDistrictCode AS COLUMN_NAME_VALUE,nvchDistrictName AS COLUMN_NAME_TEXT FROM T_DISTRICT_MASTER WHERE bitDeletedFlag=0 ORDER BY nvchDistrictName ASC";
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
                if ($('#sel_2').attr("EditData").trim() === this.Text.trim()) {
                    $('#sel_2').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_2').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });

            if ($('#sel_2').attr("EditData") != 'undefined' && $('#sel_2').attr("EditData") != "" && $('#sel_2').attr("EditData") != null) {
                EditFillULBAuto($('#sel_2').val(), $('#sel_3').attr("EditData"));
            }
        }
    });
}

/////----------------------------------------------------------------------------------------------------------------

function EditFillULBAuto(selValue, setVal) {
    var query2 = "SELECT vchULBCode AS COLUMN_NAME_VALUE,vchULBName AS COLUMN_NAME_TEXT FROM M_ULB_Master WHERE intDistrictId=" + parseInt(selValue) + " AND bitDeletedFlag=0";
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

                if (setVal == this.Text) {
                    $('#sel_3').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
                }
            })
        }
    });
}

/////----------------------------------------------------------------------------------------------------------------

function BindTradeTypes() {
    var query1 = "SELECT intTradeId AS COLUMN_NAME_VALUE,vchTradeType AS COLUMN_NAME_TEXT FROM M_TRADE_TYPES WHERE bitDeletedFlag=0 ORDER BY vchTradeType ASC";
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
}

/////----------------------------------------------------------------------------------------------------------------

function AutoBindTradeTypes() {
    var query1 = "SELECT intTradeId AS COLUMN_NAME_VALUE,vchTradeType AS COLUMN_NAME_TEXT FROM M_TRADE_TYPES WHERE bitDeletedFlag=0 ORDER BY vchTradeType ASC";
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


/////----------------------------------------------------------------------------------------------------------------