$(document).ready(function () {


    //------------------------------------------EditData-----------------------------------------
    jQuery("input[name='AppliedFor']").each(function (i) {
        jQuery(this).attr('disabled', 'disabled');
    });
    jQuery("input[name='Type']").each(function (i) {
        jQuery(this).attr('disabled', 'disabled');
    });

    if ($('#sel_4').attr("EditData") != 'undefined' && $('#sel_4').attr("EditData") != "" && $('#sel_4').attr("EditData") != null) {
        EditfillBlockDataAuto($('#sel_3').val(), $('#sel_4').attr("EditData"));
    }
//-----------------------------------------------------------------------------------------------
    var FormId = "";
    var ProposalId = "";
    $('#txt_1').attr("readonly", false);
    $('#txt_7').attr("readonly", false);
    $('#h4_plugin_11,#h4_plugin_10,#h4_plugin_9,#h2_31').find('small').show();

    $('#div_txt_23').hide();
    $('#div_rad_42').hide();
    $('#txt_2').removeClass("req rset");
    $('#div_rad_4').removeClass("reqR rset");
    //----------------------SameAs Show---------------------
    $('#divsmchk_txt_4').show();
    $('#divsmchk_txt_9').show();
    //-------------------End------------------

    //------------------SubmitButton Active----------------
    $("#btnSubmit").attr("disabled", true); //-----for disble button
    $('#chk_1').click(function () {
        if (document.getElementById('chk_1').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }              
    });
    //---------------------End-----------------------

    //----------------AutoFill Dropdwon-------------------------
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
    var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + ""
    //var distiD = distvalue(query, 'sel_1', ProposalId)
    //-------------validation-----
    $('#txt_1').keypress(function (e) {
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
    $("#txt_5").focusout(function (me) {
        
        if ($("#txt_5").val() == "") {
            return true;
        }
        else {
            var len = $("#txt_5").val().length;
            if (len < 10) {
                jAlert('Mobile number should contain 10 digits!');
                $('#txt_5').val("");
                $('#txt_5').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });
    //---------------

    $('#txt_7').keypress(function (e) {
        var regex = new RegExp("^[a-zA-Z .&-0123456789]+$");
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
    $('input:radio[name=AppliedFor]').change(function () {
        
        var vrAppliedFor = $('input:radio[name=AppliedFor]:checked').val();
        var vrType = $('input:radio[name=Type]:checked').val();
        var vrRepairType = $('input:radio[name=RepairType]:checked').val();
        if (vrType != "") {
            if (vrAppliedFor == "Boiler Manufacturer") {
                if (vrType == "New") {
                    $('#div_txt_23').hide();
                    $('#txt_2').removeClass("req rset");
                    $('#div_rad_42').hide();
                    $('#div_rad_4').removeClass("reqR  rset");
                    //                    $('#lbl_txt_23').hide();
                    AmountDetails(20000);
                }
                if (vrType == "Renewal") {
                    $('#div_txt_23').show();
                    $('#txt_2').addClass("req rset");
                    $('#div_rad_42').hide();
                    $('#div_rad_4').removeClass("reqR  rset");
                    //                    $('#lbl_txt_23').show();
                    AmountDetails(5000);
                }
            }
            else if (vrAppliedFor == "Boiler Erector") {
                if (vrType == "New") {
                    $('#div_txt_23').hide();
                    $('#txt_2').removeClass("req rset");
                    $('#div_rad_42').hide();
                    $('#div_rad_4').removeClass("reqR  rset");
                    //                    $('#lbl_txt_23').hide();
                    AmountDetails(20000);
                }
                if (vrType == "Renewal") {
                    $('#div_txt_23').show();
                    $('#txt_2').addClass("req rset");
                    $('#div_rad_42').hide();
                    $('#div_rad_4').removeClass("reqR  rset");
                    //                    $('#lbl_txt_23').show();
                    AmountDetails(5000);
                }
            }
            else if (vrAppliedFor == "Boiler Repairer") {
                if (vrType == "New") {
                    $('#div_txt_23').hide();
                    $('#txt_2').removeClass("req rset");
                    $('#div_rad_42').show();
                    $('#div_rad_4').addClass("reqR  rset");
                    //                    $('#lbl_txt_23').hide();
                    AmountDetails(20000);
                }
                if (vrType == "Renewal") {
                    $('#div_txt_23').show();
                    $('#txt_2').addClass("req rset");
                    $('#div_rad_42').show();
                    $('#div_rad_4').addClass("reqR  rset");
                    AmountDetails(0.00);
                    if (vrRepairType != undefined) {
                        if (vrRepairType == "Special") {
                            AmountDetails(7500);
                        }
                        else {
                            AmountDetails(5000);
                        }
                    }
                }
            }

        }
    });
    $('#div_txt_602').hide();
    $('#txt_60').removeClass("req rset");
    $('input:radio[name=MembershipOfProfessional]').change(function () {
        
        var vrMembershipType = $('input:radio[name=MembershipOfProfessional]:checked').val();
        if (vrMembershipType == "Yes") {
            $('#div_txt_602').show();
            $('#txt_60').addClass("req rset");
        }
        else {
            $('#div_txt_602').hide();
            $('#txt_60').removeClass("req rset");
        }
    });
    $('input:radio[name=Type]').change(function () {
        
        var vrType1 = $('input:radio[name=Type]:checked').val();
        var vrAppliedFor1 = $('input:radio[name=AppliedFor]:checked').val();
        var vrRepairType1 = $('input:radio[name=RepairType]:checked').val();
        if (vrAppliedFor1 != "") {
            if (vrType1 == "New") {
                $('#div_txt_23').hide();
                $('#div_rad_42').hide();
                $('#txt_2').removeClass("req rset");
                $('#div_rad_4').removeClass("req rset");
                if (vrAppliedFor1 == "Boiler Manufacturer") {
                    $('#div_txt_23').hide();
                    $('#txt_2').removeClass("req rset");
                    $('#div_rad_42').hide();
                    $('#div_rad_4').removeClass("reqR  rset");
                    AmountDetails(20000);
                }
                else if (vrAppliedFor1 == "Boiler Erector") {
                    $('#div_txt_23').hide();
                    $('#txt_2').removeClass("req rset");
                    $('#div_rad_42').hide();
                    $('#div_rad_4').removeClass("reqR  rset");
                    AmountDetails(20000);
                }
                else if (vrAppliedFor1 == "Boiler Repairer") {
                    $('#div_txt_23').hide();
                    $('#txt_2').removeClass("req rset");
                    $('#div_rad_42').show();
                    $('#div_rad_4').addClass("reqR  rset");
                    AmountDetails(20000);
                }
            }
            else if (vrType1 == "Renewal") {
                $('#div_txt_23').show();
                $('#div_rad_42').show();
                $('#txt_2').addClass("req rset");
                $('#div_rad_4').addClass("reqR rset");
                if (vrAppliedFor1 == "Boiler Manufacturer") {
                    $('#div_txt_23').show();
                    $('#txt_2').addClass("req rset");
                    $('#div_rad_42').hide();
                    $('#div_rad_4').removeClass("reqR  rset");
                    AmountDetails(5000);
                }
                else if (vrAppliedFor1 == "Boiler Erector") {
                    $('#div_txt_23').show();
                    $('#txt_2').addClass("req rset");
                    $('#div_rad_42').hide();
                    $('#div_rad_4').removeClass("reqR  rset");
                    AmountDetails(5000);
                }
                else if (vrAppliedFor1 == "Boiler Repairer") {
                    $('#div_txt_23').show();
                    $('#txt_2').addClass("req rset");
                    $('#div_rad_42').show();
                    $('#div_rad_4').addClass("reqR rset");
                    AmountDetails(0.00);
                    if (vrRepairType1 != undefined) {
                        if (vrRepairType1 == "Special") {
                            AmountDetails(7500);
                        }
                        else {
                            AmountDetails(5000);
                        }
                    }
                }
            }
        }

        $('input:radio[name=RepairType]').change(function () {
            var vrType2 = $('input:radio[name=Type]:checked').val();
            var vrAppliedFor2 = $('input:radio[name=AppliedFor]:checked').val();
            var vrRepairType2 = $('input:radio[name=RepairType]:checked').val();
            if (vrType2 == "Renewal") {
                if (vrAppliedFor2 == "Boiler Repairer") {
                    if (vrRepairType2 == "Special") {
                        AmountDetails(7500);
                    }
                    else {
                        AmountDetails(5000);
                    }
                }
            }
        });
    });


    $("#sel_1").change(function () {

        var selValue = $('#sel_1').val();
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
                $('#sel_2').html('');
                $('#sel_2').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_2').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });

    $("#sel_3").change(function () {

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
                $('#sel_4').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });

    $("#sel_5").change(function () {

        var selValue = $('#sel_5').val();
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
                $('#sel_6').html('');
                $('#sel_6').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_6').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
        divisionvalue(selValue);
    });
 
    //    $('#h2_31').find('small').show();


    $('#smchk_txt_4').click(function () {
        
        if (document.getElementById("smchk_txt_4").checked) {
            $('#sel_3').val($('#sel_1').val());
            $('#txt_4').val($('#txt_3').val());
            var selValue = $('#sel_3').val();
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
                    $('#sel_4').append($("<option></option>").val('0').html('Select'))
                    $.each(r.d, function () {
                        //                        $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                        //                        console.log(r.d);

                        if ($('#sel_2').val() == this.Value) {
                            $('#sel_4').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    })
                }
            });

        } else {
            $('#sel_3').val("0");
            $('#sel_4').val("0");
            $('#sel_4').html('');
            $('#sel_4').append($("<option></option>").val('0').html('Select'))
            $('#txt_4').val("");
        }
    });

    $('#smchk_txt_9').click(function () {
       
        if (document.getElementById("smchk_txt_9").checked) {
            $('#sel_5').val($('#sel_3').val());
            $('#txt_9').val($('#txt_4').val());
            var selValue = $('#sel_5').val();
            var dtLevel = "3";
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_6').html('');
                    $('#sel_6').append($("<option></option>").val('0').html('Select'))
                    $.each(r.d, function () {
                        //                        $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                        //                        console.log(r.d);

                        if ($('#sel_4').val() == this.Value) {
                            $('#sel_6').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_6').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    })
                }
            });
            divisionvalue(selValue)
        } else {
            $('#sel_5').val("0");
            $('#sel_6').val("0");
            $('#sel_6').html('');
            $('#sel_6').append($("<option></option>").val('0').html('Select'))
            $('#txt_9').val("");
        }
    });

    $('input:radio[name=AppliedFor]').change(function () {
        if ($('input:radio[name=AppliedFor]:checked').val() == "Boiler Manufacturer") {

            $('#h4_plugin_2,#h4_plugin_3,#h4_plugin_8,#h4_plugin_6,#h4_plugin_5,#h4_plugin_4').find('small').show();
            var strTitle2 = $('#plugin_2').attr("title");
            $("#plugin_2 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle2.split('-');

                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_2" + '_' + titlearray[i] + '_' + id;
                        if ($("#" + idd).hasClass("req rset")) {
                        }
                        else {
                            $("#" + idd).addClass("req rset");
                            $("#" + idd).removeAttr("disabled");
                        }
                    }

                }

            });
            var strTitle3 = $('#plugin_3').attr("title");
            $("#plugin_3 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle3.split('-');

                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_3" + '_' + titlearray[i] + '_' + id;
                        if ($("#" + idd).hasClass("req rset")) {
                        }
                        else {
                            $("#" + idd).addClass("req rset");
                            $("#" + idd).removeAttr("disabled");
                        }
                    }

                }

            });
            var strTitle4 = $('#plugin_4').attr("title");
            $("#plugin_4 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle4.split('-');

                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_4" + '_' + titlearray[i] + '_' + id;
                        if ($("#" + idd).hasClass("req rset")) {
                        }
                        else {
                            $("#" + idd).addClass("req rset");
                            $("#" + idd).removeAttr("disabled");
                        }
                    }

                }

            });
            var strTitle5 = $('#plugin_5').attr("title");
            $("#plugin_5 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle5.split('-');

                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_5" + '_' + titlearray[i] + '_' + id;
                        if ($("#" + idd).hasClass("req rset")) {
                        }
                        else {
                            $("#" + idd).addClass("req rset");
                            $("#" + idd).removeAttr("disabled");
                        }
                    }

                }

            });
            var strTitle6 = $('#plugin_6').attr("title");
            $("#plugin_6 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle6.split('-');

                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_6" + '_' + titlearray[i] + '_' + id;
                        if ($("#" + idd).hasClass("req rset")) {
                        }
                        else {
                            $("#" + idd).addClass("req rset");
                            $("#" + idd).removeAttr("disabled");
                        }
                    }

                }

            });
            var strTitle8 = $('#plugin_8').attr("title");
            $("#plugin_8 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle8.split('-');

                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_8" + '_' + titlearray[i] + '_' + id;
                        if ($("#" + idd).hasClass("req rset")) {
                        }
                        else {
                            $("#" + idd).addClass("req rset");
                            $("#" + idd).removeAttr("disabled");
                        }
                    }

                }

            });
            var strTitle7 = $('#plugin_7').attr("title");
            $("#plugin_7 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle7.split('-');

                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_7" + '_' + titlearray[i] + '_' + id;
                        $("#" + idd).removeClass("req rset");
                        $("#" + idd).attr("disabled", "disabled");
                    }

                }

            });
        }
        else if ($('input:radio[name=AppliedFor]:checked').val() == "Boiler Erector") {
            $('#h4_plugin_2,#h4_plugin_6,#h4_plugin_5,#h4_plugin_8,#plugin_4,#plugin_2').find('small').show();
            $('#h4_plugin_3').find('small').hide();
            $('#h4_plugin_4').find('small').hide();

            var strTitle5 = $('#plugin_5').attr("title");
            $("#plugin_5 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle5.split('-');

                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_5" + '_' + titlearray[i] + '_' + id;
                        if ($("#" + idd).hasClass("req rset")) {
                        }
                        else {
                            $("#" + idd).addClass("req rset");
                            $("#" + idd).removeAttr("disabled");
                        }
                    }

                }

            });
            var strTitle6 = $('#plugin_6').attr("title");
            $("#plugin_6 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle6.split('-');

                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_6" + '_' + titlearray[i] + '_' + id;
                        if ($("#" + idd).hasClass("req rset")) {
                        }
                        else {
                            $("#" + idd).addClass("req rset");
                            $("#" + idd).removeAttr("disabled");
                        }
                    }

                }

            });
            var strTitle2 = $('#plugin_2').attr("title");
            $("#plugin_2 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle2.split('-');

                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_2" + '_' + titlearray[i] + '_' + id;
                        if ($("#" + idd).hasClass("req rset")) {
                        }
                        else {
                            $("#" + idd).addClass("req rset");
                            $("#" + idd).removeAttr("disabled");
                        }
                    }

                }

            });

            strTitle3 = $('#plugin_3').attr("title");
            $("#plugin_3 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle3.split('-');
                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_3" + '_' + titlearray[i] + '_' + id;
                        $("#" + idd).removeClass("req rset");
                        $("#" + idd).attr("disabled", "disabled");
                    }

                }

            });

            strTitle = $('#plugin_4').attr("title");
            $("#plugin_4 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle.split('-');
                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_4" + '_' + titlearray[i] + '_' + id;
                        $("#" + idd).removeClass("req rset");
                        $("#" + idd).attr("disabled", "disabled");
                    }

                }

            });
            strTitle = $('#plugin_7').attr("title");
            $("#plugin_7 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle.split('-');
                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_7" + '_' + titlearray[i] + '_' + id;
                        $("#" + idd).removeClass("req rset");
                        $("#" + idd).attr("disabled", "disabled");
                    }

                }

            });
        }
        else if ($('input:radio[name=AppliedFor]:checked').val() == "Boiler Repairer") {
            $('#h4_plugin_2,#h4_plugin_6,#h4_plugin_5').find('small').show();
            $('#h4_plugin_8').find('small').hide();
            $('#h4_plugin_3').find('small').hide();
            $('#h4_plugin_4').find('small').hide();
            strTitle = $('#plugin_3').attr("title");
            $("#plugin_3 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle.split('-');
                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_3" + '_' + titlearray[i] + '_' + id;
                        $("#" + idd).removeClass("req rset");
                        $("#" + idd).attr("disabled", "disabled");
                    }

                }

            });

            strTitle = $('#plugin_4').attr("title");
            $("#plugin_4 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle.split('-');
                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_4" + '_' + titlearray[i] + '_' + id;
                        $("#" + idd).removeClass("req rset");
                        $("#" + idd).attr("disabled", "disabled");
                    }

                }

            });
            strTitle = $('#plugin_7').attr("title");
            $("#plugin_7 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle.split('-');
                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_7" + '_' + titlearray[i] + '_' + id;
                        $("#" + idd).removeClass("req rset");
                        $("#" + idd).attr("disabled", "disabled");
                    }

                }

            });
            strTitle = $('#plugin_8').attr("title");
            $("#plugin_8 >tbody > tr").each(function () {
                var $row = $(this);
                
                var tblidd = this.id;
                var id = 0;
                var titlearray = strTitle.split('-');
                var $row = $(this);
                var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
                if (parentId != undefined && parentId != 'undefined') {
                    id = parseInt(id) + 1;
                    for (var i = 0; i < titlearray.length; i++) {
                        var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
                        strName = titlearray[i];
                        var idd = "plugin_8" + '_' + titlearray[i] + '_' + id;
                        $("#" + idd).removeClass("req rset");
                        $("#" + idd).attr("disabled", "disabled");
                    }

                }

            });
        }


    });

    AppliedForCal();
    TypeCal();

});

function distvalue(query2, dropid, ProposalId) {
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

function blockvalue(query2, distid, ProposalId) {
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
    return blockid;
}

function fillBlockDataAuto(selValue, setVal) {
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
            $('#sel_2').html('');
            $('#sel_2').append($("<option></option>").val('0').html('Select'))
            $.each(r.d, function () {

                if (setVal == this.Value) {
                    $('#sel_2').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_2').append($("<option></option>").val(this.Value).html(this.Text));
                }
            })
        }
    });


}

function FuncWithCondition(query2) {
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMapping",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            AmountDetails(r.d)
        }
    });

}

function AmountDetails(amount) {
    var strText = "";
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Total Amount</th><td  width='50%'><b>" + amount + "/-</b></td></tr></table>"
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(amount);
}

function CurrentDateValidation(ida) {
    
    //alert('hi');
//    $("#" + ida.id).on("dp.change", function (e) {
//        var frmDate = ida.id.replace("PeriodTo", "PeriodFrom");
//        var dtV = new Date();
//            $('#' + ida.id).data("DateTimePicker").minDate(dtV);
//    });

//    var d = new Date();
//    var PlugnId = ids.id;
//    var dtt = $.datepicker.formatDate('yy-mm-dd', new Date());
//   $('#' + PlugnId).attr("max", dtt);
}

function addbuttonclick(idd, tbWhole) {
    
    var strrType = $('input:radio[name=AppliedFor]:checked').val();
    if (strrType == "Boiler Manufacturer") {
        if (tbWhole.id == "plugin_7") {
            $("#plugin_7 >tbody > tr").each(function () {
                $('#plugin_7_Details_' + idd).removeClass("req rset");
                $('#plugin_7_Details_' + idd).attr("disabled", "disabled");
                $('#plugin_7_DateOfPurchase_' + idd).removeClass("req rset");
                $('#plugin_7_DateOfPurchase_' + idd).attr("disabled", "disabled");
                $('#plugin_7_PlaceWhereToPlace_' + idd).removeClass("req rset");
                $('#plugin_7_PlaceWhereToPlace_' + idd).attr("disabled", "disabled");
                $('#plugin_7_Remark_' + idd).removeClass("req rset");
                $('#plugin_7_Remark_' + idd).attr("disabled", "disabled");
            });
        }
    }
    if (strrType == "Boiler Erector") {
        if (tbWhole.id == "plugin_7") {
            $("#plugin_7 >tbody > tr").each(function () {
                $('#plugin_7_Details_' + idd).removeClass("req rset");
                $('#plugin_7_Details_' + idd).attr("disabled", "disabled");
                $('#plugin_7_DateOfPurchase_' + idd).removeClass("req rset");
                $('#plugin_7_DateOfPurchase_' + idd).attr("disabled", "disabled");
                $('#plugin_7_PlaceWhereToPlace_' + idd).removeClass("req rset");
                $('#plugin_7_PlaceWhereToPlace_' + idd).attr("disabled", "disabled");
                $('#plugin_7_Remark_' + idd).removeClass("req rset");
                $('#plugin_7_Remark_' + idd).attr("disabled", "disabled");
            });
        }
      
        if (tbWhole.id == "plugin_3") {
            $("#plugin_3 >tbody > tr").each(function () {
                $('#plugin_3_Details_' + idd).removeClass("req rset");
                $('#plugin_3_Details_' + idd).attr("disabled", "disabled");
                $('#plugin_3_Details_' + idd).css({ "border-color": "" })
                $('#plugin_3_DateOfPurchase_' + idd).removeClass("req rset");
                $('#plugin_3_DateOfPurchase_' + idd).attr("disabled", "disabled");
                $('#plugin_3_DateOfPurchase_' + idd).css({ "border-color": "" })
                $('#plugin_3_PlaceWhereToPlace_' + idd).removeClass("req rset");
                $('#plugin_3_PlaceWhereToPlace_' + idd).attr("disabled", "disabled");
                $('#plugin_3_PlaceWhereToPlace_' + idd).css({ "border-color": "" })
                $('#plugin_3_Remark_' + idd).removeClass("req rset");
                $('#plugin_3_Remark_' + idd).attr("disabled", "disabled");
                $('#plugin_3_Remark_' + idd).css({ "border-color": "" })
            });
        }
    
     if (tbWhole.id == "plugin_4") {
            $("#plugin_4 >tbody > tr").each(function () {
                $('#plugin_4_Details_' + idd).removeClass("req rset");
                $('#plugin_4_Details_' + idd).attr("disabled", "disabled");
                $('#plugin_4_DateOfPurchase_' + idd).removeClass("req rset");
                $('#plugin_4_DateOfPurchase_' + idd).attr("disabled", "disabled");
                $('#plugin_4_PlaceWhereToPlace_' + idd).removeClass("req rset");
                $('#plugin_4_PlaceWhereToPlace_' + idd).attr("disabled", "disabled");
                $('#plugin_4_Remark_' + idd).removeClass("req rset");
                $('#plugin_4_Remark_' + idd).attr("disabled", "disabled");
            });
        }
 }
  if (strrType == "Boiler Repairer") {
        if (tbWhole.id == "plugin_7") {
            $("#plugin_7 >tbody > tr").each(function () {
                $('#plugin_7_Details_' + idd).removeClass("req rset");
                $('#plugin_7_Details_' + idd).attr("disabled", "disabled");
                $('#plugin_7_DateOfPurchase_' + idd).removeClass("req rset");
                $('#plugin_7_DateOfPurchase_' + idd).attr("disabled", "disabled");
                $('#plugin_7_PlaceWhereToPlace_' + idd).removeClass("req rset");
                $('#plugin_7_PlaceWhereToPlace_' + idd).attr("disabled", "disabled");
                $('#plugin_7_Remark_' + idd).removeClass("req rset");
                $('#plugin_7_Remark_' + idd).attr("disabled", "disabled");
            });
        }
        }
        if (tbWhole.id == "plugin_3") {
            $("#plugin_3 >tbody > tr").each(function () {
                $('#plugin_3_Details_' + idd).removeClass("req rset");
                $('#plugin_3_Details_' + idd).attr("disabled", "disabled");
                $('#plugin_3_Details_' + idd).css({ "border-color": "" })
                $('#plugin_3_DateOfPurchase_' + idd).removeClass("req rset");
                $('#plugin_3_DateOfPurchase_' + idd).attr("disabled", "disabled");
                $('#plugin_3_DateOfPurchase_' + idd).css({ "border-color": "" })
                $('#plugin_3_PlaceWhereToPlace_' + idd).removeClass("req rset");
                $('#plugin_3_PlaceWhereToPlace_' + idd).attr("disabled", "disabled");
                $('#plugin_3_PlaceWhereToPlace_' + idd).css({ "border-color": "" })
                $('#plugin_3_Remark_' + idd).removeClass("req rset");
                $('#plugin_3_Remark_' + idd).attr("disabled", "disabled");
                $('#plugin_3_Remark_' + idd).css({ "border-color": "" })
            });
        }
         if (tbWhole.id == "plugin_4") {
            $("#plugin_4 >tbody > tr").each(function () {
                $('#plugin_4_Details_' + idd).removeClass("req rset");
                $('#plugin_4_Details_' + idd).attr("disabled", "disabled");
                $('#plugin_4_DateOfPurchase_' + idd).removeClass("req rset");
                $('#plugin_4_DateOfPurchase_' + idd).attr("disabled", "disabled");
                $('#plugin_4_PlaceWhereToPlace_' + idd).removeClass("req rset");
                $('#plugin_4_PlaceWhereToPlace_' + idd).attr("disabled", "disabled");
                $('#plugin_4_Remark_' + idd).removeClass("req rset");
                $('#plugin_4_Remark_' + idd).attr("disabled", "disabled");
            });
        }
  
      if (tbWhole.id == "plugin_8") {
            $("#plugin_8 >tbody > tr").each(function () {
                $('#plugin_8_Details_' + idd).removeClass("req rset");
                $('#plugin_8_Details_' + idd).attr("disabled", "disabled");
                $('#plugin_8_DateOfPurchase_' + idd).removeClass("req rset");
                $('#plugin_8_DateOfPurchase_' + idd).attr("disabled", "disabled");
                $('#plugin_8_PlaceWhereToPlace_' + idd).removeClass("req rset");
                $('#plugin_8_PlaceWhereToPlace_' + idd).attr("disabled", "disabled");
                $('#plugin_8_Remark_' + idd).removeClass("req rset");
                $('#plugin_8_Remark_' + idd).attr("disabled", "disabled");
            });
        }
}

function BindOnChangeFromDate(ida) {
         
         $("#" + ida.id).on("dp.change", function (e) {
             var frmDate = ida.id.replace("PeriodTo", "PeriodFrom");
             var dtV = new Date();
             if ($('#' + frmDate).val() != '') {
                 $('#' + ida.id).data("DateTimePicker").minDate($('#' + frmDate).val());

             }

          
         });

     }

function divisionvalue( distid) {
         var blockid = "";
         selValue = distid;
         // query2 = "select distinct A.intLevelDetailId as COLUMN_NAME_VALUE,nvchLevelName as COLUMN_NAME_TEXT from M_adm_leveldetails A inner join T_FB_Subdiv_ServiceUserMapping B on A.intLevelDetailId=B.intLevelDetailId where intparentid in (select intLevelDetailId from M_adm_leveldetails where intparentid in(select intLevelDetailId from M_adm_leveldetails where intparentid=422)) and bitStatus=1 and bitDeletedFlag=0 and B.intDistrictId=" + distid + ""
         var query2 = "select distinct A.intLevelDetailId as COLUMN_NAME_VALUE,nvchLevelName as COLUMN_NAME_TEXT from M_adm_leveldetails A inner join T_FB_Subdiv_ServiceUserMapping B on A.intLevelDetailId=B.intLevelDetailId where intparentid in (select intLevelDetailId from M_adm_leveldetails where intparentid in(select intLevelDetailId from M_adm_leveldetails where intparentid=422)) and bitStatus=1 and bitDeletedFlag=0 and B.intDistrictId=" + selValue + "";
         var ob = {};
         ob.query = query2;
         $.ajax({
             type: "POST",
             url: "FormView.aspx/FillDemographyData",
             data: JSON.stringify(ob), // "{'query':'" + query2 + "'}",
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
                 $('#sel_4').html('');
                 $('#sel_4').append($("<option></option>").val('0').html('Select'))
                 $.each(r.d, function () {

                     if (setVal == this.Text) {
                         $('#sel_4').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                     }
                     else {
                         $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                     }
                 })
             }
         });
}

function AppliedForCal() {

    var vrAppliedFor = $('input:radio[name=AppliedFor]:checked').val();
    var vrType = $('input:radio[name=Type]:checked').val();
    var vrRepairType = $('input:radio[name=RepairType]:checked').val();
    if (vrType != "") {
        if (vrAppliedFor == "Boiler Manufacturer") {
            if (vrType == "New") {
                $('#div_txt_23').hide();
                $('#txt_2').removeClass("req rset");
                $('#div_rad_42').hide();
                $('#div_rad_4').removeClass("reqR  rset");
                //                    $('#lbl_txt_23').hide();
                AmountDetails(20000);
            }
            if (vrType == "Renewal") {
                $('#div_txt_23').show();
                $('#txt_2').addClass("req rset");
                $('#div_rad_42').hide();
                $('#div_rad_4').removeClass("reqR  rset");
                //                    $('#lbl_txt_23').show();
                AmountDetails(5000);
            }
        }
        else if (vrAppliedFor == "Boiler Erector") {
            if (vrType == "New") {
                $('#div_txt_23').hide();
                $('#txt_2').removeClass("req rset");
                $('#div_rad_42').hide();
                $('#div_rad_4').removeClass("reqR  rset");
                //                    $('#lbl_txt_23').hide();
                AmountDetails(20000);
            }
            if (vrType == "Renewal") {
                $('#div_txt_23').show();
                $('#txt_2').addClass("req rset");
                $('#div_rad_42').hide();
                $('#div_rad_4').removeClass("reqR  rset");
                //                    $('#lbl_txt_23').show();
                AmountDetails(5000);
            }
        }
        else if (vrAppliedFor == "Boiler Repairer") {
            if (vrType == "New") {
                $('#div_txt_23').hide();
                $('#txt_2').removeClass("req rset");
                $('#div_rad_42').show();
                $('#div_rad_4').addClass("reqR  rset");
                //                    $('#lbl_txt_23').hide();
                AmountDetails(20000);
            }
            if (vrType == "Renewal") {
                $('#div_txt_23').show();
                $('#txt_2').addClass("req rset");
                $('#div_rad_42').show();
                $('#div_rad_4').addClass("reqR  rset");
                AmountDetails(0.00);
                if (vrRepairType != undefined) {
                    if (vrRepairType == "Special") {
                        AmountDetails(7500);
                    }
                    else {
                        AmountDetails(5000);
                    }
                }
            }
        }

    }
}

function TypeCal() {
    var vrType1 = $('input:radio[name=Type]:checked').val();
    var vrAppliedFor1 = $('input:radio[name=AppliedFor]:checked').val();
    var vrRepairType1 = $('input:radio[name=RepairType]:checked').val();
    if (vrAppliedFor1 != "") {
        if (vrType1 == "New") {
            $('#div_txt_23').hide();
            $('#div_rad_42').hide();
            $('#txt_2').removeClass("req rset");
            $('#div_rad_4').removeClass("req rset");
            if (vrAppliedFor1 == "Boiler Manufacturer") {
                $('#div_txt_23').hide();
                $('#txt_2').removeClass("req rset");
                $('#div_rad_42').hide();
                $('#div_rad_4').removeClass("reqR  rset");
                AmountDetails(20000);
            }
            else if (vrAppliedFor1 == "Boiler Erector") {
                $('#div_txt_23').hide();
                $('#txt_2').removeClass("req rset");
                $('#div_rad_42').hide();
                $('#div_rad_4').removeClass("reqR  rset");
                AmountDetails(20000);
            }
            else if (vrAppliedFor1 == "Boiler Repairer") {
                $('#div_txt_23').hide();
                $('#txt_2').removeClass("req rset");
                $('#div_rad_42').show();
                $('#div_rad_4').addClass("reqR  rset");
                AmountDetails(20000);
            }
        }
        else if (vrType1 == "Renewal") {
            $('#div_txt_23').show();
            $('#div_rad_42').show();
            $('#txt_2').addClass("req rset");
            $('#div_rad_4').addClass("reqR rset");
            if (vrAppliedFor1 == "Boiler Manufacturer") {
                $('#div_txt_23').show();
                $('#txt_2').addClass("req rset");
                $('#div_rad_42').hide();
                $('#div_rad_4').removeClass("reqR  rset");
                AmountDetails(5000);
            }
            else if (vrAppliedFor1 == "Boiler Erector") {
                $('#div_txt_23').show();
                $('#txt_2').addClass("req rset");
                $('#div_rad_42').hide();
                $('#div_rad_4').removeClass("reqR  rset");
                AmountDetails(5000);
            }
            else if (vrAppliedFor1 == "Boiler Repairer") {
                $('#div_txt_23').show();
                $('#txt_2').addClass("req rset");
                $('#div_rad_42').show();
                $('#div_rad_4').addClass("reqR rset");
                AmountDetails(0.00);
                if (vrRepairType1 != undefined) {
                    if (vrRepairType1 == "Special") {
                        AmountDetails(7500);
                    }
                    else {
                        AmountDetails(5000);
                    }
                }
            }
        }
    }

    var vrType2 = $('input:radio[name=Type]:checked').val();
    var vrAppliedFor2 = $('input:radio[name=AppliedFor]:checked').val();
    var vrRepairType2 = $('input:radio[name=RepairType]:checked').val();
    if (vrType2 == "Renewal") {
        if (vrAppliedFor2 == "Boiler Repairer") {
            if (vrRepairType2 == "Special") {
                AmountDetails(7500);
            }
            else {
                AmountDetails(5000);
            }
        }
    }
}