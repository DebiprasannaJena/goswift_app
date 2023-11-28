
$(document).ready(function () {

    //---------------------Main Function Begin-------------------------------

    OwnershipRoad();

    //----------------------SameAs Show---------------------
    $('#divsmchk_txt_45').show();
    //    $("#txt_1").attr("readonly", false);
    //    $("#txt_2").attr("readonly", false);
    //    $("#txt_3").attr("readonly", false);
    //-------------------End------------------

    /*---------------------------------------------------------------------------------*/
    //--------------------------------EditData--------------------------------------------

    if ($('input:radio[name=AppliedTo]:checked').val() === "IDCO") {
        $("#sel_1").val("1");
        $("#sel_1").attr("disabled", true);
        FillDivision_Road();
    }
    else if ($('input:radio[name=AppliedTo]:checked').val() === "RD") {
        $("#sel_1").val("4");
        $("#sel_1").attr("disabled", true);
        FillRD_Road();
    }
    else if ($('input:radio[name=AppliedTo]:checked').val() === "WORKS") {
        $("#sel_1").val("2");
        $("#sel_1").attr("disabled", true);
        FillWork_Road();
    }
    
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
    //------------------SubmitButton Active----------------

    $("#btnSubmit").attr("disabled", true);

    $('#Dec_1').click(function () {
        if (document.getElementById('Dec_1').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });

    /*---------------------------------------------------------------------------------*/


    //    $("#txt_1").attr("disabled", true);
    //    $("#txt_2").attr("disabled", true);
    //    $("#txt_3").attr("disabled", true);
    //    $("#sel_2").attr("disabled", true);
    //    $("#txt_5").attr("disabled", true);
    //    $("#sel_1").attr("disabled", true);
    //------------------------------MobileNoValidation----------------


    $("#txt_2").focusout(function (me) {
        if ($("#txt_2").val() === "") {
            return true;
        }
        else {
            var len = $("#txt_2").val().length;
            if (len < 10) {
                jAlert('Mobile number should contain 10 digits !');
                $('#txt_2').val("");
                $('#txt_2').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });

    /*---------------------------------------------------------------------------------*/


    //------------------------------NameFieldValdiation------------------

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

    /*---------------------------------------------------------------------------------*/
    //----------------AutoFill Dropdwon-------------------------
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        ProposalId = urlparam[1];
    }
    ProposalId = $('#hdnProposalNo').val();
    if (ProposalId !== "") {
        var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + "";
    }

    /*---------------------------------------------------------------------------------*/  

    $('#div_txt_1110').hide();
    $('#txt_11').removeClass('req rset');

    /*---------------------------------------------------------------------------------*/

    $('input:radio[name=PurposeOfRoadcutting]').change(function () {        
        if ($('input:radio[name=PurposeOfRoadcutting]:checked').val() === "Any other") {
            $('#div_txt_1110').show();
            $('#txt_11').addClass('req rset');
        }
        else {
            $('#txt_11').removeClass('req rset');
            $('#div_txt_1110').hide();
        }
    });

    /*---------------------------------------------------------------------------------*/

    $('input:radio[name=AppliedTo]').change(function () {
        if ($('input:radio[name=AppliedTo]:checked').val() === "IDCO") {
            $("#sel_1").val("1");
            $("#sel_1").attr("disabled", true);
            FillDivision_Road();
        }
        else if ($('input:radio[name=AppliedTo]:checked').val() === "RD") {
            $("#sel_1").val("4");
            $("#sel_1").attr("disabled", true);
            FillRD_Road();
        }
        else if ($('input:radio[name=AppliedTo]:checked').val() === "WORKS") {
            $("#sel_1").val("2");
            $("#sel_1").attr("disabled", true);
            FillWork_Road();
        }
    });

    /*---------------------------------------------------------------------------------*/

    $('input:radio[name=AppliedTo]').change(function () {
      
        if ($('input:radio[name=AppliedTo]:checked').val() === "NHAI") {           
            window.open('http://www.nhai.org/', '_blank');
        }
        else if ($('input:radio[name=AppliedTo]:checked').val() === "ULB") {           
            window.open('https://www.ulbodisha.gov.in/or/emun/ulb-profile', '_blank');
        }
        else {
            return 1;
        }
    }); 
});

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
            if (r.d !== "") {
                $("#sel_2").attr("disabled", true);
                distid = r.d;
                $('#' + dropid).val(r.d);
            }
        }
    });
    return distid;
}

/*---------------------------------------------------------------------------------*/

function OwnershipRoad() { 
    var query1 = "SELECT intOwnershipRoadId as COLUMN_NAME_VALUE,vchOwnershipRoadName AS COLUMN_NAME_TEXT FROM T_OwnerShipRoad ";
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
                if ($('#sel_1').attr("EditData") === this.Text) {
                    $('#sel_1').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_1').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*---------------------------------------------------------------------------------*/

function AutoFillOwner(distid) {
    var selValue = $('#sel_7').val();
    var query1 = "SELECT DISTINCT intDiscomeid AS COLUMN_NAME_VALUE,(SELECT nvchLevelname FROM M_adm_leveldetails T WHERE T.intLevelDetailId=M.intdiscomeid) AS COLUMN_NAME_TEXT FROM T_Energy_Block_ServiceUserMapping AS M WHERE intDistrictId=" + parseInt(distid) + " ";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMapping",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            fillUtilityFirst(r.d);
        }
    });
}

/*---------------------------------------------------------------------------------*/

function FillDivision_Road() {
    var query1 = "SELECT intLevelDetailId AS COLUMN_NAME_VALUE,nvchLevelName AS COLUMN_NAME_TEXT FROM M_ADM_LevelDetails WHERE intLevelId=4 AND intParentId=432 ORDER BY nvchLevelName";
    var ob = {};
    ob.query = query1;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_21').html('');
            $('#sel_21').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_21').attr("EditData") === this.Text) {
                    $('#sel_21').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_21').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*---------------------------------------------------------------------------------*/

function FillRD_Road() {
    var query1 = "SELECT intLevelDetailId AS COLUMN_NAME_VALUE,nvchLevelName AS COLUMN_NAME_TEXT FROM M_ADM_LevelDetails WHERE intLevelId=4 AND intParentId=364 ORDER BY nvchLevelName";
    var ob = {};
    ob.query = query1;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_21').html('');
            $('#sel_21').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_21').attr("EditData") === this.Text) {
                    $('#sel_21').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_21').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*---------------------------------------------------------------------------------*/

function FillWork_Road() {
    var query1 = "SELECT intLevelDetailId AS COLUMN_NAME_VALUE,nvchLevelName AS COLUMN_NAME_TEXT FROM M_ADM_LevelDetails WHERE intLevelId=4 AND intParentId=879 ORDER BY nvchLevelName";
    var ob = {};
    ob.query = query1;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob), 
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_21').html('');
            $('#sel_21').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if ($('#sel_21').attr("EditData") === this.Text) {
                    $('#sel_21').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_21').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });
}

/*---------------------------------------------------------------------------------*/