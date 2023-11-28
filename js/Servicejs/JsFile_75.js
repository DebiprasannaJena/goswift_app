///**********************************************************************************************************************/
/// File Name   : JsFile_75.js
/// Description : JavaScript File for FormView.aspx
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

    /////-------------------------------------------------------------------------------------------------------------------------------------------

    BindDistrict();
    BindDistrictULBMap(); //// For ULB mapping, the District table used as T_DISTRICT_MASTER.So separate function used for district and ULB fillup.
    BindTradeTypes();

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

    /////-------------------------------------------------------------------------------------------------------------------------------------------
});


/////-------------------------------------------------------------------------------------------------------------------------------------------
///// Bind District DropDownList

function BindDistrict() {
    var selValue = 20;
    var query1 = "SELECT intDistrictId as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT FROM M_District WHERE intStateId=" + parseInt(selValue) + " ORDER BY vchDistrictName ASC";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_1').html('');
            $('#sel_7').html('');

            $('#sel_1').append($("<option></option>").val('0').html('Select'));
            $('#sel_7').append($("<option></option>").val('0').html('Select'));


            $.each(r.d, function () {
                $('#sel_1').append($("<option></option>").val(this.Value).html(this.Text));
                $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/////-------------------------------------------------------------------------------------------------------------------------------------------

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

/////-------------------------------------------------------------------------------------------------------------------------------------------


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

/////-------------------------------------------------------------------------------------------------------------------------------------------