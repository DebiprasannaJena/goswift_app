$(document).ready(function () {

    debugger;

    ///------------------------------------------------EditData--------------------------------
    if ($('#sel_7').attr("EditData") !== 'undefined' && $('#sel_7').attr("EditData") !== "" && $('#sel_7').attr("EditData") !== null) {
        EditfillBlockDataAuto($('#sel_6').val(), $('#sel_7').attr("EditData"));
    }
    ///--------------------------------------------------------------------------------------------
    $('#h2_78').find('small').show();

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

    $("#btnSubmit").attr("disabled", true); //-----for disble button
   
    $('#Dec_1').click(function () {
        if (document.getElementById('Dec_1').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });

    /*---------------------------------------------------------------------------------*/

    $('#div_txt_11').hide();
    $("#txt_1").removeClass("req rset");

    var ProposalId = "";
    //    $("#lbl_txt_85").hide();
    $("#div_txt_16").hide();

    // District

    //$("#Plugn_2 >tbody > tr").each(function () {
    //    var $row = $(this);

    //    //        FillCatagory('Plg_2_AreaOfOperation_0');
    //    debugger;
    //    fillDistrict();
    //  //  EditfillPloginDist($('#Plg_2_AreaOfOperation_0').val())

    //    //        var parentId = $row.closest('tr').find('[name*=' + 'Catagory' + ']').val();
    //    //        total = eval(total) + eval(parentId);

    //});

    /*----------------------------for Plogin-----------------------------------------------*/

    var i = 0;
    $("#Plugn_2 >tbody > tr").each(function () {
      //  alert("Plogin data");
       // var $row = $(this);
       // var strTitle = $('#Plg_2').attr("title");
        var idCate1 = "Plugn_2_AreaOfOperation_" + i;
        i++;
        //var idCate = "Plg_2_Catagory_0";
        //var idSubCate = "Plg_2_SubCategory_2";
        //var total = 0;

        EditfillPloginDist(idCate1);

    });


    /*---------------------------------------------------------------------------------*/

    $('input:radio[name=ExistanceOfSociety]').change(function () {
        if ($('input:radio[name=ExistanceOfSociety]:checked').val() === "Yes") {
            //            $("#lbl_txt_85").show();
            $("#div_txt_11").show();
            $("#txt_1").addClass("req rset");

        }
        if ($('input:radio[name=ExistanceOfSociety]:checked').val() === "No") {
            //            $("#lbl_txt_85").hide();
            $("#div_txt_11").hide();
            $("#txt_1").removeClass("req rset");

        }
    });

    /*---------------------------------------------------------------------------------*/

    $('#txt_3').datetimepicker({
        format: 'DD-MMM-YYYY'
    }).on('dp.show', function () {
        return $(this).data('DateTimePicker').maxDate(new Date());
    });

    /*---------------------------------------------------------------------------------*/
    //----------------AutoFill Dropdwon-------------------------
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        ProposalId = urlparam[1];
    }
    ProposalId = $('#hdnProposalNo').val();
    var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + "";
    //var distiD = distvalue(query, 'sel_2', ProposalId);

    /*---------------------------------------------------------------------------------*/

    $("#sel_3").removeClass("reqD rset");
    $("#sel_4").removeClass("reqD rset");
    $("#div_sel_42").hide();
    $("#div_sel_31").hide();
    $("#div_Plugn_2").hide();

    /*---------------------------------------------------------------------------------*/

    if ($('#sel_2').val() !== 'undefined' && $('#sel_2').val() !== "" && $('#sel_2').val() !== null) {

        if ($('#sel_2').val() === "District Level") {
            $("#div_sel_31").show();
        }
        else if ($('#sel_2').val() === "State Level") {
            $("#div_Plugn_2").show();
        }
    }

    /*---------------------------------------------------------------------------------*/

    $("#sel_2").change(function () {
        if ($(this).val() === "District Level") {
            $("#sel_3").removeClass("reqD rset");
            $("#div_sel_42").hide();
            $("#div_sel_31").show();
            $("#sel_3").addClass("reqD rset");
            $("#div_Plugn_2").hide();
        }
        else if ($(this).val() === "State Level") {
            $("#div_sel_31").hide();
            $("#div_sel_42").show();
            $("#sel_4").addClass("reqD rset");
            $("#div_Plugn_2").show();
        }
        else {
            $("#div_sel_42").hide();
            $("#div_sel_31").hide();
            $("#div_Plugn_2").hide();
            $("#sel_3").removeClass("reqD rset");
            $("#sel_4").removeClass("reqD rset");
        }
    });

    /*---------------------------------------------------------------------------------*/

    $("#sel_6").change(function () {
        var selValue = $('#sel_6').val();
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
                $('#sel_7').html('');
                $('#sel_7').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });
    });

    /*---------------------------------------------------------------------------------*/

    var i = "";
    $("#btnPlugn_1").click(function () {
        $("#Plugn_1 tr:last").clone().find(".form-control").each(function () {
            $(this).val('').attr('id', function (_, id) { return id + i });
        }).end().appendTo("#Plugn_1");
        i++;
        return false;
    });

    /*---------------------------------------------------------------------------------*/

    var i = "";
    $("#btnPlugn_2").click(function () {
        $("#Plugn_2 tr:last").clone().find(".form-control").each(function () {
            $(this).val('').attr('id', function (_, id) { return id + i });
        }).end().appendTo("#Plugn_2");
        i++;
        return false;
    });

});


/*---------------------------------------------------------------------------------*/

//function fillDistrict() {

//    debugger;

//    //    var selValue = $('#sel_55').val();
//    //    var dtLevel = "3";
//    var query1 = "Select vchDistrictName as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District WHERE intStateId=20  order by vchDistrictName asc";

//    $.ajax({
//        type: "POST",
//        url: "FormView.aspx/FillDemographyData",
//        data: "{'query':'" + query1 + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (r) {
//            $('#Plugn_2_AreaOfOperation_0').html('');
//            $('#Plugn_2_AreaOfOperation_0').append($("<option></option>").val('0').html('Select'));
//            $.each(r.d, function () {
//                $('#Plugn_2_AreaOfOperation_0').append($("<option></option>").val(this.Value).html(this.Text));
//            });
//        }
//    });
//}

/*---------------------------------------------------------------------------*/

function EditfillPloginDist(ControlIds) {
    //alert("file catagory");
    // alert(ControlIds);
    var ControlId = ControlIds;
   // var vll = ControlIds;
    // var vll = 'Plg_2_Catagory_0';
    //    alert(ControlId);
   // var subCateId1 = vll.replace('Catagory', 'SubCategory');

    var query1 = "Select vchDistrictName as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District WHERE intStateId=20  order by vchDistrictName asc";
    debugger;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#' + ControlId).html('');
            $('#' + ControlId).append($("<option></option>").val('0').html('Select'))
            $.each(r.d, function () {
                if ($('#' + ControlId).attr("EditData") == this.Value) {
                    $('#' + ControlId).append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#' + ControlId).append($("<option></option>").val(this.Value).html(this.Text));
                }
            })
            //if ($('#' + ControlId).attr("EditData") != '' && $('#' + ControlId).attr("EditData") != null) {
            //    EditOnchangeCloneDrp(ControlId);
            //    FillSubCatagory(subCateId1, $('#' + ControlId).attr("EditData"));
            //}
        }
    });
}


/*---------------------------------------------------------------------------------*/



//function EditfillPloginDist(distname) {

//    debugger;

//    //    var selValue = $('#sel_55').val();
//    //    var dtLevel = "3";
//    var query1 = "Select vchDistrictName as COLUMN_NAME_VALUE , vchDistrictName as COLUMN_NAME_TEXT from M_District WHERE intStateId=20  and vchDistrictName='Cuttack' order by vchDistrictName asc";

//    $.ajax({
//        type: "POST",
//        url: "FormView.aspx/FillDemographyData",
//        data: "{'query':'" + query1 + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (r) {
//            $('#Plugn_2_AreaOfOperation_0').html('');
//            $('#Plugn_2_AreaOfOperation_0').append($("<option></option>").val('0').html('Select'));
//            $.each(r.d, function () {
//                $('#Plugn_2_AreaOfOperation_0').append($("<option></option>").val(this.Value).html(this.Text));
//            });
//        }
//    });
//}

/*---------------------------------------------------------------------------------*/

function distvalue(query2, dropid, ProposalId) {
    debugger;
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

/*---------------------------------------------------------------------------------*/

function blockvalue(query2, distid, ProposalId) {

    debugger;
    var blockid = "";
    query2 = "select intBlockId from T_LandAndUtility where vchProposalNo=" + ProposalId + "";
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

/*---------------------------------------------------------------------------------*/

function fillBlockDataAuto(selValue, setVal) {

    debugger;
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

                if (setVal === this.Value) {
                    $('#sel_3').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_3').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });


}

/*---------------------------------------------------------------------------------*/

function AmountDetails(amount) {
    var strText = "";
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>"
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(amount);
}

/*---------------------------------------------------------------------------------*/

function OnchangeCloneDrp(ids) {
}

/*---------------------------------------------------------------------------------*/

function ControlChangeEvt(id) {
}

/*---------------------------------------------------------------------------------*/

function EditfillBlockDataAuto(selValue, setVal) {

    debugger;

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
            $('#sel_7').html('');
            $('#sel_7').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {

                if (setVal === this.Text) {
                    $('#sel_7').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*---------------------------------------------------------------------------------*/