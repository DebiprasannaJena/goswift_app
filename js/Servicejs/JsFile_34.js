
function showDays(firstDate, secondDate) {



    var startDay = new Date(firstDate);
    var endDay = new Date(secondDate);
    var millisecondsPerDay = 1000 * 60 * 60 * 24;

    var millisBetween = startDay.getTime() - endDay.getTime();
    var days = millisBetween / millisecondsPerDay;

    return  (Math.floor(days));
    // Round down.
    alert(Math.floor(days));

}

function humanise(diff) {
    // The string we're working with to create the representation
    var str = '';
    // Map lengths of `diff` to different time periods
    var values = [[' Year', 365], [' Month', 30], [' Day', 1]];

    // Iterate over the values...
    for (var i = 0; i < values.length; i++) {
        var amount = Math.floor(diff / values[i][1]);

        // ... and find the largest time value that fits into the diff
        if (amount >= 1) {
            // If we match, add to the string ('s' is for pluralization)
            str += amount + values[i][0] + (amount > 1 ? 's' : '') + ' ';

            // and subtract from the diff
            diff -= amount * values[i][1];
        }
    }

    return str;
}


var globaldata = "";
var ProposalId = "";
var ProposalId = "";

$(document).ready(function () {

    var YearArray = new Array("2017", "2018", "2019", "2020", "2021", "2022", "2023", "2024", "2025", "2026", "2027", "2028", "2029", "2030");
    $('#sel_1').html('');
    $('#sel_1').append($("<option></option>").val('0').html('Year'))
    for (i = 0; i < YearArray.length; i++) {
        $('#sel_1').append($("<option></option>").val(YearArray[i]).html(YearArray[i]));
    }
    $('#h2_31').find('small').show();
    $("#txt_8").prop("readonly", true);
    $("#btnSubmit").attr("disabled", true); //-----for disble button
    $('#Dec_1').click(function () {
        debugger;
        if (document.getElementById('Dec_1').checked) {
            debugger;
            if ($('#txt_8').val() == "0") {
                jAlert('Maximum number of workers  can not be 0');
            }
            else {
                $("#btnSubmit").attr("disabled", false);
            }
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });
    debugger;

    $("#txt_10").change(function () {
        debugger;
        var txt_80 = $('#txt_8').val();
        var txt_110 = $('#txt_10').val();
        if (parseInt(txt_110) > parseInt(txt_80)) {
            $('#txt_10').val("");
        }
        else {

        }

    });


    //-----------------------------------year validation--------------------
    //    $("#sel_1").change(function () {
    //        var cuurentYear = (new Date()).getFullYear();
    //        var compYear = $('#sel_1').val();
    //        var periodText = "From " + cuurentYear + " To " + compYear;
    //        $('#txt_34').val(periodText);

    //    });
    //---------------------------------------------------------------------
    //----------------AutoFill Dropdwon-------------------------
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        ProposalId = urlparam[1];
    }
    ProposalId = $('#hdnProposalNo').val();
    if (ProposalId != "") {
        var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + ""
        var distiD = distvalue(query, 'sel_4', ProposalId);

        FillProjectType(ProposalId);
    }
    //---------------------------Manager Validation-------

    $("#divsmchk_txt_5").show();     /// for same as so show the div
    $("#divsmchk_txt_16").show();
    $("#divsmchk_txt_23").show();
    $("#divsmchk_txt_29").show();
    $("#txt_1").removeClass("req rset");
    $('#ManagerFullName_FN').keypress(function (e) {
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
    $('#ManagerFullName_MN').keypress(function (e) {
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
    $('#ManagerFullName_LN').keypress(function (e) {
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
    $('#txt_14').keypress(function (e) {
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
    $("#txt_17").focusout(function (me) {
        debugger;
        if ($("#txt_17").val() == "") {
            return true;
        }
        else {
            var len = $("#txt_17").val().length;
            if (len < 10) {
                jAlert('Mobile number should contain 10 digits!');
                $('#txt_17').val("");
                $('#txt_17').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });

    //---------------------------Occupier under the Factories-------

    $('#OcuupierFullName_FN').keypress(function (e) {
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
    $('#OcuupierFullName_MN').keypress(function (e) {
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
    $('#OcuupierFullName_LN').keypress(function (e) {
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
    $('#txt_21').keypress(function (e) {
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
    $('#txt_20').keypress(function (e) {
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
    $("#txt_24").focusout(function (me) {
        debugger;
        if ($("#txt_24").val() == "") {
            return true;
        }
        else {
            var len = $("#txt_24").val().length;
            if (len < 10) {
                jAlert('Mobile number should contain 10 digits!');
                $('#txt_24').val("");
                $('#txt_24').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });
    //--------------------------end Occupier Under the Factories---------

    $("#fil_6").attr("disabled", "disabled");
    $('#fil_6').removeClass("req rset");
    $('#lbl_fil_64').find('span').css('display', 'none');
    $("#txt_35").change(function () {
        debugger;
        var txt_31 = $('#txt_35').val();
        var txt_32 = $('#txt_36').val();
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;
        if (vrNature == "")
            vrNature = 0;
        //------------------------------------
        var yr = $('input:radio[name=TermOfLicence]:checked').val();
        if (yr == "1Yr") {
            yr = 1;
        }
        else if (yr == "5Yr") {
            yr = 5;
        }
        else if (yr == "10Yr") {
            yr = 10;
        }

        else {
            yr = 0;
        }

        //----------------------------------------------------
        var result = parseInt(txt_31) + parseInt(txt_32); //+ parseInt(txtstdir) + parseInt(txtstind);
        if (!isNaN(result)) {
            document.getElementById('txt_8').value = result;
            $("#txt_8").prop("readonly", true);
            //            if (vrNature != "") {
            CalcuByProcedure(result, vrNature, yr);
            // }
            if (parseInt(result) <= 50) {
                $("#fil_6").attr("disabled", "disabled");
                $('#fil_6').removeClass("req rset");
                $('#lbl_fil_64').find('span').css('display', 'none');

            }
            else {
                $("#fil_6").removeAttr("disabled");
                $('#fil_6').addClass("req rset");
                $('#lbl_fil_64').find('span').css('display', 'block');
            }
        }
    });
    $("#txt_36").change(function () {
        debugger;
        var txt_31 = document.getElementById('txt_35').value;
        var txt_32 = document.getElementById('txt_36').value;
        var vrNature = $('#txt_11').val();
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;
        if (vrNature == "")
            vrNature = 0;
        //---------------------------
        var yr = $('input:radio[name=TermOfLicence]:checked').val();
        if (yr == "1Yr") {
            yr = 1;
        }
        else if (yr == "5Yr") {
            yr = 5;
        }
        else if (yr == "10Yr") {
            yr = 10;
        }

        else {
            yr = 0;
        }

        //-------------------------------
        var result = parseInt(txt_31) + parseInt(txt_32); //+ parseInt(txtstdir) + parseInt(txtstind);
        if (!isNaN(result)) {
            document.getElementById('txt_8').value = result;
            $("#txt_8").prop("readonly", true);
            //            if (vrNature != "") {
            CalcuByProcedure(result, vrNature, yr);
            //}
            if (parseInt(result) <= 50) {
                $("#fil_6").attr("disabled", "disabled");
                $("#fil_6").removeClass("req rset");
                $('#lbl_fil_64').find('span').css('display', 'none');
            }
            else {
                $("#fil_6").removeAttr("disabled");
                $("#fil_6").addClass("req rset");
                $('#lbl_fil_64').find('span').css('display', 'block');
            }
        }

    });


    //---------------------------Owner of Premises-------

    $('#OwnerFullName_FN').keypress(function (e) {
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
    $('#OwnerFullName_MN').keypress(function (e) {
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
    $('#OwnerFullName_LN').keypress(function (e) {
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
    $('#txt_27').keypress(function (e) {
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
    $("#txt_30").focusout(function (me) {
        debugger;
        if ($("#txt_30").val() == "") {
            return true;
        }
        else {
            var len = $("#txt_30").val().length;
            if (len < 10) {
                jAlert('Mobile number should contain 10 digits!');
                $('#txt_30').val("");
                $('#txt_30').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });
    $('#sel_1').change(function () {
        debugger;
        var compYear = $('#sel_1').val();
        if (compYear != "0") {
            compYear = $('#sel_1').val(); ;
        }
        else {
            var d = new Date();
            compYear = d.getFullYear();
        }
        var yr = $('input:radio[name=TermOfLicence]:checked').val();
        if (yr == "1Yr") {
            yr = 1;
        }
        else if (yr == "5Yr") {
            yr = 5;
        }
        else if (yr == "10Yr") {
            yr = 10;
        }

        else {
            yr = 0;
        }


        var txt_31 = $('#txt_35').val();
        var txt_32 = $('#txt_36').val();
        var vrNature = $('#txt_11').val();
        if (vrNature == "") {
            vrNature = "0";
        }
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32); //+ parseInt(txtstdir) + parseInt(txtstind);
        if (!isNaN(result)) {
            document.getElementById('txt_8').value = result;
            $("#txt_8").prop("readonly", true);
            if (vrNature != "") {

                CalcuByProcedure(result, vrNature, yr);
            }
        }
        if (yr != "" && yr != "undefined") {
            var fnlYr = parseInt(compYear) + parseInt(yr)-1;
            var periodText = "From 01-Jan-" + compYear + " To 31-Dec-" + fnlYr;
            $('#txt_34').val(periodText);
        }
        else {
            $('#txt_34').val("");
        }
    });
    //--------------------------end Occupier Under the Factories---------
    $('input:radio[name=TermOfLicence]').change(function () {
        debugger;

        var vrterm = $('input:radio[name=TermOfLicence]:checked').val();
        var compYear = $('#sel_1').val();
        if (compYear != "0") {
            compYear = $('#sel_1').val();
        }
        else {
            var d = new Date();
            compYear = d.getFullYear();
        }
        var yrc = "";
        if (vrterm == "1Yr") {
            var nxtYr = parseInt(compYear) + 1-1;
            if (compYear != "") {
                var periodText = "From 01-Jan-" + compYear + " To 31-Dec-" + nxtYr;
                $('#txt_34').val(periodText);
                yrc = 1;
            }
        }
        else if (vrterm == "5Yr") {
            var nxtYr = parseInt(compYear) + 5-1;
            if (compYear != "") {
                var periodText = "From 01-Jan-" + compYear + " To 31-Dec-" + nxtYr;
                $('#txt_34').val(periodText);
                yrc = 5;
            }
        }
        else if (vrterm == "10Yr") {
            var nxtYr = parseInt(compYear) + 10-1;
            if (compYear != "") {
                var periodText = "From 01-Jan-" + compYear + " To 31-Dec-" + nxtYr;
                $('#txt_34').val(periodText);
                yrc = 10;
            }
        }
        else {
            yrc = 0;
        }

        var txt_31 = $('#txt_35').val();
        var txt_32 = $('#txt_36').val();
        var vrNature = $('#txt_11').val();
        if (vrNature == "")
            vrNature = 0;
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;
        var result = parseInt(txt_31) + parseInt(txt_32);

        CalcuByProcedure(result, vrNature, yrc);



    });





    $('#div_txt_14').hide();
    $('#div_txt_11').hide();
    $('#div_txt_22').hide();

    $('input:radio[name=TypeOfLicence]').change(function () {
        debugger;

        var compYear = $('#sel_1').val();
        if (compYear != "0") {
            compYear = $('#sel_1').val(); ;
        }
        else {
            var d = new Date();
            compYear = d.getFullYear();
        }
        var yr = $('input:radio[name=TermOfLicence]:checked').val();
        if (yr == "1Yr") {
            yr = 1;
        }
        else if (yr == "5Yr") {
            yr = 5;
        }
        else if (yr == "10Yr") {
            yr = 10;
        }

        else {
            yr = 0;
        }


        var txt_31 = $('#txt_35').val();
        var txt_32 = $('#txt_36').val();
        var vrNature = $('#txt_11').val();
        if (vrNature == "") {
            vrNature = "0";
        }
        if (txt_31 == "")
            txt_31 = 0;
        if (txt_32 == "")
            txt_32 = 0;

        var result = parseInt(txt_31) + parseInt(txt_32); //+ parseInt(txtstdir) + parseInt(txtstind);
        if (!isNaN(result)) {
            document.getElementById('txt_8').value = result;
            $("#txt_8").prop("readonly", true);
            if (vrNature != "") {

                CalcuByProcedure(result, vrNature, yr);
            }
        }
        if (yr != "" && yr != "undefined") {
            var fnlYr = parseInt(compYear) + parseInt(yr)-1;
            var periodText = "From 01-Jan-" + compYear + " To 31-Dec-" + fnlYr;
            $('#txt_34').val(periodText);
        }
        else {
            // $('#txt_34').val("");
        }

        if ($('input:radio[name=TypeOfLicence]:checked').val() == "Renewal") {
            $('#div_txt_14').show();
            $("#txt_6").removeAttr("disabled");
            $("#txt_6").addClass("req rset");
            $('#lbl_txt_60').find('span').css('display', 'block');
            $("#txt_77").removeAttr("disabled");
            $("#txt_77").addClass("req rset");
            $("#fil_1").attr("disabled", "disabled");
            $('#lbl_fil_10').find('span').css('display', 'none');
            $("#fil_1").removeClass("req rset");
            $('#div_txt_11').show();
            $('#div_txt_22').show();
            $("#txt_1").addClass("req rset");

            $('#lbl_fil_53').find('span').css('display', 'none');
            $('#fil_5').removeClass("req rset");

        }


        if ($('input:radio[name=TypeOfLicence]:checked').val() == "New") {
            $('#div_txt_14').hide();
            $("#txt_6").attr("disabled", "disabled");
            $("#txt_6").removeClass("req rset");
            $('#lbl_txt_60').find('span').css('display', 'none');
            $("#txt_77").attr("disabled", "disabled");
            $("#txt_77").removeClass("req rset");
            $("#fil_1").removeAttr("disabled");
            $("#fil_1").addClass("req rset");
            $('#lbl_fil_10').find('span').css('display', 'block');
            $('#div_txt_11').hide();
            $('#div_txt_22').hide();
            $("#txt_1").removeClass("req rset");

            $('#lbl_fil_53').find('span').css('display', 'block');
            $('#fil_5').addClass("req rset");
        }
    });
    $('#lbl_fil_20').find('span').css('display', 'none');
    $('#fil_2').removeClass("req rset");
    $('#lbl_fil_31').find('span').css('display', 'none');
    $('#fil_3').removeClass("req rset");
    $('#lbl_fil_42').find('span').css('display', 'none');
    $('#fil_4').removeClass("req rset");
    $("#sel_3").change(function () {
        debugger;
        if ($('#sel_3').val() == "Private Ltd.") {

            $('#lbl_fil_20').find('span').css('display', 'block');
            $('#fil_2').addClass("req rset");
            $('#lbl_fil_31').find('span').css('display', 'block');
            $('#fil_3').addClass("req rset");
      
        }
      
        else {
            $('#lbl_fil_20').find('span').css('display', 'none');
            $('#fil_2').removeClass("req rset");
            $('#lbl_fil_31').find('span').css('display', 'none');
            $('#fil_3').removeClass("req rset");


            //            $('#lbl_fil_42').find('span').css('display', 'none');
            //            $('#fil_4').removeClass("req rset");

        }

    });

    $('#div_txt_31').hide();
    $("#txt_3").removeClass("req rset");
    $("#sel_3").change(function () {
        if ($('#sel_3').val() == "Any Other") {
            $('#div_txt_31').show();
            $("#txt_3").addClass("req rset");


        }
        else {
            $('#div_txt_31').hide();
            $("#txt_3").removeClass("req rset");
        }
    });
    $("#sel_4").change(function () {
        debugger;
        var selValue = $('#sel_4').val();
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_5').html('');
                $('#sel_5').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_5').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });

        //----------------------division----------
        // var query1 = "select distinct A.intLevelDetailId as COLUMN_NAME_VALUE,nvchLevelName as COLUMN_NAME_TEXT from M_adm_leveldetails A inner join T_FB_Subdiv_ServiceUserMapping B on A.intLevelDetailId=B.intLevelDetailId where intparentid in (select intLevelDetailId from M_adm_leveldetails where intparentid=422) and bitStatus=1 and bitDeletedFlag=0 and B.intDistrictId=" + selValue + "";
        //var query1 = "select intId as COLUMN_NAME_VALUE , vchName as COLUMN_NAME_TEXT from T_MunciplDetails where vchType='" + type + "' and bitDeletedFlag=0   order by vchName asc";

        var query1 = "select distinct A.intLevelDetailId as COLUMN_NAME_VALUE,nvchLevelName as COLUMN_NAME_TEXT from M_adm_leveldetails A inner join T_FB_Subdiv_ServiceUserMapping B on A.intLevelDetailId=B.intLevelDetailId where intparentid in (select intLevelDetailId from M_adm_leveldetails where intparentid in(select intLevelDetailId from M_adm_leveldetails where intparentid=422)) and bitStatus=1 and bitDeletedFlag=0 and B.intDistrictId=" + selValue + "";
        var ob = {};
        ob.query = query1;
        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: JSON.stringify(ob),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_8').html('');
                $('#sel_8').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_8').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });

    });

    $("#sel_6").change(function () {

        var selValue = $('#sel_6').val();
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_7').html('');
                $('#sel_7').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });
    $("#sel_51").change(function () {

        var selValue = $('#sel_51').val();
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_37').html('');
                $('#sel_37').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_37').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });

    $("#sel_81").change(function () {

        var selValue = $('#sel_81').val();
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_34').html('');
                $('#sel_34').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_34').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });
    $("#sel_90").change(function () {

        var selValue = $('#sel_90').val();
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_11').html('');
                $('#sel_11').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_11').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });
    $("#sel_9").change(function () {

        var selValue = $('#sel_9').val();
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_101').html('');
                $('#sel_101').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_101').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });

    $("#sel_91").change(function () {

        var selValue = $('#sel_91').val();
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_12').html('');
                $('#sel_12').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_12').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });

    $("#sel_10").change(function () {

        var selValue = $('#sel_10').val();
        var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_111').html('');
                $('#sel_111').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_111').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });
    //-------------------------------------------------------SameAs---------------------------
    $('#smchk_txt_5').click(function () {
        debugger;
        if (document.getElementById('smchk_txt_5').checked) {
            $('#sel_6').val($('#sel_4').val());
            $('#txt_5').val($('#txt_4').val());
            var selValue = $('#sel_6').val();
            var dtLevel = "3";
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_7').html('');
                    $('#sel_7').append($("<option></option>").val('0').html('Select'))
                    $.each(r.d, function () {


                        if ($('#sel_5').val() == this.Value) {
                            $('#sel_7').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    })
                }
            });

        } else {
            $('#sel_6').val("0");
            $('#sel_7').val("0");
            $('#sel_7').html('');
            $('#sel_7').append($("<option></option>").val('0').html('Select'))
            $('#txt_5').val("");
        }
    });

    $('#smchk_txt_16').click(function () {
        debugger;
        if (document.getElementById('smchk_txt_16').checked) {
            $('#txt_16').val($('#txt_15').val());
            $('#sel_81').val($('#sel_51').val());
            var selValue = $('#sel_81').val();
            var dtLevel = "3";
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_34').html('');
                    $('#sel_34').append($("<option></option>").val('0').html('Select'))
                    $.each(r.d, function () {


                        if ($('#sel_37').val() == this.Value) {
                            $('#sel_34').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_34').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    })
                }
            });

        } else {
            $('#sel_81').val("0");
            $('#sel_34').val("0");
            $('#sel_34').html('');
            $('#sel_34').append($("<option></option>").val('0').html('Select'))
            $('#txt_16').val("");
        }
    });
    $('#smchk_txt_23').click(function () {
        if (document.getElementById('smchk_txt_23').checked) {
            $('#txt_23').val($('#txt_22').val());
            $('#sel_9').val($('#sel_90').val());
            var selValue = $('#sel_9').val();
            var dtLevel = "3";
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_101').html('');
                    $('#sel_101').append($("<option></option>").val('0').html('Select'))
                    $.each(r.d, function () {


                        if ($('#sel_11').val() == this.Value) {
                            $('#sel_101').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_101').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    })
                }
            });

        } else {
            $('#sel_9').val("0");
            $('#sel_101').val("0");
            $('#sel_101').html('');
            $('#sel_101').append($("<option></option>").val('0').html('Select'))
            $('#txt_23').val("");
        }
    });
    $('#smchk_txt_29').click(function () {
        if (document.getElementById('smchk_txt_29').checked) {
            $('#txt_29').val($('#txt_28').val());
            $('#sel_10').val($('#sel_91').val());
            var selValue = $('#sel_10').val();
            var dtLevel = "3";
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_111').html('');
                    $('#sel_111').append($("<option></option>").val('0').html('Select'))
                    $.each(r.d, function () {


                        if ($('#sel_12').val() == this.Value) {
                            $('#sel_111').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_111').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    })
                }
            });

        } else {
            $('#sel_10').val("0");
            $('#sel_111').val("0");
            $('#sel_111').html('');
            $('#sel_111').append($("<option></option>").val('0').html('Select'))
            $('#txt_29').val("");
        }
    });
    //----------------------------------------------------------------------------------------
    //    $("#txt_8").change(function () {
    //        debugger;
    //        var vrNature = $('#txt_11').val();
    //        var vrMaximumCnt = $('#txt_8').val();
    //        if (vrNature != "") {
    //            if (vrMaximumCnt != "") {
    //                CalcuByProcedure(vrMaximumCnt, vrNature);
    //            }
    //        }
    //    });
    $("#txt_11").change(function () {
        debugger;
        var yr = $('input:radio[name=TermOfLicence]:checked').val();
        if (yr == "1Yr") {
            yr = 1;
        }
        else if (yr == "5Yr") {
            yr = 5;
        }
        else if (yr == "10Yr") {
            yr = 10;
        }

        else {
            yr = 0;
        }

        var vrNature = $('#txt_11').val();
        var vrMaximumCnt = $('#txt_8').val();
        if (vrMaximumCnt == "") {
            vrMaximumCnt = 0;
        }
        if (vrNature == "") {
            vrNature = 0
        }
        else {
            vrNature = $('#txt_11').val();
        }

        if (vrMaximumCnt != "") {
            CalcuByProcedure(vrMaximumCnt, vrNature, yr);

        }
    });


});



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
                divisionvalue('', r.d, ProposalId);
                blockvalue('', r.d, ProposalId);
                $("#" + dropid).attr("disabled", "disabled");
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
            fillBlockDataAuto(distid, r.d);
        }
    });
    return blockid;
}
function divisionvalue(query2, distid, ProposalId) {
    var blockid = "";
    selValue = distid;
   // query2 = "select distinct A.intLevelDetailId as COLUMN_NAME_VALUE,nvchLevelName as COLUMN_NAME_TEXT from M_adm_leveldetails A inner join T_FB_Subdiv_ServiceUserMapping B on A.intLevelDetailId=B.intLevelDetailId where intparentid in (select intLevelDetailId from M_adm_leveldetails where intparentid in(select intLevelDetailId from M_adm_leveldetails where intparentid=422)) and bitStatus=1 and bitDeletedFlag=0 and B.intDistrictId=" + distid + ""
    var query2 = "select distinct A.intLevelDetailId as COLUMN_NAME_VALUE,nvchLevelName as COLUMN_NAME_TEXT from M_adm_leveldetails A inner join T_FB_Subdiv_ServiceUserMapping B on A.intLevelDetailId=B.intLevelDetailId where intparentid in (select intLevelDetailId from M_adm_leveldetails where intparentid in(select intLevelDetailId from M_adm_leveldetails where intparentid=422)) and bitStatus=1 and bitDeletedFlag=0 and B.intDistrictId=" + selValue + "";
    var ob = {};
    ob.query = query2;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data:JSON.stringify(ob),// "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_8').html('');
            $('#sel_8').append($("<option></option>").val('0').html('Select'))
            $.each(r.d, function () {

              
           
                    $('#sel_8').append($("<option></option>").val(this.Value).html(this.Text));
    
            })
        }
    });
      
}
function fillBlockDataAuto(selValue,setVal) {
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
            $('#sel_5').html('');
            $('#sel_5').append($("<option></option>").val('0').html('Select'))
            $.each(r.d, function () {

                if (setVal == this.Value) {
                    $('#sel_5').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_5').append($("<option></option>").val(this.Value).html(this.Text));
                }
            })
        }
    });
    $("#sel_5").attr("disabled", "disabled");

}
function CalcuByProcedure(intPersone, intPower,intyr) {

    $.ajax({
        type: "POST",
        url: "FormView.aspx/Form34_Calculation",
        data: "{'intPerson':'" + intPersone + "','intPower':'" + intPower + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            AmountDetails(r.d,intyr);
        }
    });
}

function AmountDetails(amount,intyr) {
    var extrCharges1 = "0";
    if ($('input:radio[name=TypeOfLicence]:checked').val() == "Renewal") {
        var d = new Date();
        var compYear1 = d.getFullYear();
        var compMonth1 = d.getMonth();
        var comDate = d.getDate();
        var compYear1 = $('#sel_1').val();
        if (compYear1 != "" && compYear1 != null) {
            compYear1 = parseInt(compYear1) - 1;
            var CompDate1 = new Date("01-Nov-" + compYear1);
             var CompDate2= new Date("31-Dec-" + compYear1);
            var crdt = new Date();
            if (CompDate1 < crdt && CompDate2 > crdt) {
          //alert('1st');
                extrCharges1=(parseInt(amount) * 25) / 100;
                   //alert(extrCharges1);
            }
            else if (CompDate2 < crdt) {
           //alert('2st');
                extrCharges1=amount;
                   // alert(extrCharges1);
            }
        }
    }

       amount = parseInt(amount)*parseInt(intyr);
       var amount1 = parseInt(amount) + parseInt(extrCharges1);
       if (extrCharges1 == "0") {
           extrCharges1 = "0";
       }
      
    var strText = "";
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr width='50%'><th> Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr><tr width='50%'><th>Penalty Charges</th><td width='50%'><b>" + extrCharges1 + "/-</b></td></tr><tr width='50%'><th>Total Amount</th><td width='50%'><b>" + amount1 + "/-</b></td></tr></table>"
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(amount1);
}

function myfunc() {
    var start = $("#firstDate").datepicker("getDate");
    var end = $("#secondDate").datepicker("getDate");
    days = (end - start) / (1000 * 60 * 60 * 24);

}
//$(function () {

//    $('.ManagerFullName_FN').bind('keyup input', function () {
//        debugger;
//        if (this.value.match(/[^a-zA-Z áéíóúÁÉÍÓÚüÜ]/g)) {
//            this.value = this.value.replace(/[^a-zA-Z áéíóúÁÉÍÓÚüÜ]/g, '');
//        }
//    });
//});
function FillProjectType(ProposalId) {
    debugger;

    //  alert("dssd");
    var query3 = "select Type from VW_Project_Type where vchProposalNo=" + ProposalId;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query3 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            // alert(r.d);
            AddProjectTypeText(r.d);
        }
    });

}
function AddProjectTypeText(dt) {
    // alert('hi');
    if (dt != "") {
        $('#txt_32').val(dt);
        $("#txt_32").attr("disabled", "disabled");
    }
}
function forDisableAdd(controlid,lblid,divid,rmvclass) {
    $('#' + lblid).find('span').css('display', 'none');
    $("#" + controlid).attr("disabled", "disabled");
    $('#' + controlid).removeClass(rmvclass);
}
function forDivHide(controlid, lblid, divid, rmvclass) {
    var democlass;
    $('#' + controlid).removeClass(rmvclass);
    $('#' + divid).hide();
}
function forDivshow(controlid, lblid, divid, rmvclass) {
    var democlass;
    $('#' + controlid).addClass(rmvclass);
    $('#' + divid).show();
}
function forDisableRemove(controlid, lblid, divid, rmvclass) {
    $('#' + lblid).find('span').css('display', 'block');
    $("#" + controlid).removeAttr("disabled");
    $('#' + controlid).addClass(rmvclass);
}
function addbuttonclick()
{
}