
$(document).ready(function () {       

    var ProposalId = "";
    AmountDetails(0);

    /*---------------------------------------------------------------------------*/

    $("#Plg_2 >tbody > tr").each(function () {
        var $row = $(this);
        var idCate = "Plg_2_Catagory_0";
        var idSubCate = "Plg_2_SubCategory_2";
        var total = 0;
        FillCatagory('Plg_2_Catagory_0');
    });

    /*---------------------------------------------------------------------------*/

    FillCatagory();

    /*---------------------------------------------------------------------------*/

    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        ProposalId = urlparam[1];
    }
    ProposalId = $('#hdnProposalNo').val();
    if (ProposalId != "") {
        var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + ""
        var distiD = distvalue(query, 'sel_1', ProposalId);
        distvalue1(query, 'sel_3', ProposalId);
    }

    /*---------------------------------------------------------------------------*/

    $('#divsmchk_txt_45').show();

    /*---------------------------------------------------------------------------*/
    /*-------------------------------------------------------------------*/
    ///// Added by Sushant during v2.0 Implementation
    /*-------------------------------------------------------------------*/
    $("#btnDraft").attr("disabled", true);
    $('#Dec_1').click(function () {
        if (document.getElementById('Dec_1').checked) {
            $("#btnDraft").attr("disabled", false);
            CalFunc(61);
        }
        else {
            $("#btnDraft").attr("disabled", true);
        }
    });

    /*---------------------------------------------------------------------------*/

    $("#btnSubmit").attr("disabled", true); //-----for disble button
    $('#Dec_1').click(function () {
        if (document.getElementById('Dec_1').checked) {
            $("#btnSubmit").attr("disabled", false);
            CalFunc(61);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });

    /*---------------------------------------------------------------------------*/

    $('#txt_11').datetimepicker({
        format: 'DD-MMM-YYYY'
    }).on('dp.show', function () {
        return $(this).data('DateTimePicker').maxDate(new Date());
    });

    /*---------------------------------------------------------------------------*/

    $('#txt_10').keypress(function (e) {
        AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz-/';
        var k;
        k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
        if (k != 13 && k != 8 && k != 0) {
            if ((e.ctrlKey == false) && (e.altKey == false)) {
                return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
            }
            else {
                return true;
                $('#txt_6').val("");
            }
        }
        else {
            return true;
            $('#txt_6').val("");
        }
    });

    /*---------------------------------------------------------------------------*/

    $("#txt_5").focusout(function (me) {
        if ($("#txt_5").val() == "") {
            return true;
        }
        else {
            var len = $("#txt_5").val().length;
            if (len < 6) {
                jAlert('PIN number should contain 6 digits !');
                $('#txt_5').val("");
                $('#txt_5').focus();
                return false;
            }
            else {
                return true;
            }
        }
    });

    /*---------------------------------------------------------------------------*/

    $("#div_sel_47").hide();
    $('#sel_4').removeClass("reqD rset");

    /*---------------------------------------------------------------------------*/

    $('#chk_10').click(function () {
        if (document.getElementById('chk_10').checked) {
            $("#div_sel_47").show();
            $('#sel_4').addClass("reqD rset");

            $("#chk_11").attr("disabled", true);
            $("#chk_11").attr("checked", false);
            $("#sel_4").val("415v");
        }
        else {

            $("#div_sel_47").hide();
            $('#sel_4').removeClass("reqD rset");
            $("#chk_11").attr("disabled", false);
        }
    });

    /*---------------------------------------------------------------------------*/

    $("#pnl_43").hide();
    $("#txt_8").removeClass('req rset');
    $("#sel_5").removeClass('reqD rset');
    $("#div_txt_80").hide();
    $("#div_sel_51").hide();

    /*---------------------------------------------------------------------------*/

    $('#chk_11').click(function () {
        if (document.getElementById('chk_11').checked) {
            $("#chk_10").attr("disabled", true);
            $("#chk_10").attr("checked", false);
            $("#pnl_43").show();
            $("#div_txt_80").show();
            $("#div_sel_51").show();
            $("#txt_8").addClass('req rset');
            $("#sel_5").addClass('reqD rset');
        }
        else {
            $("#chk_10").attr("disabled", false);
            $("#txt_8").removeClass('req rset');
            $("#sel_5").removeClass('reqD rset');
            $("#div_txt_80").hide();
            $("#div_sel_51").hide();
            $("#pnl_43").hide();
        }
    });

    /*---------------------------------------------------------------------------*/

    $("#btnplg_1").click(function () {
        $("#plg_1 tr:last").clone().find(".form-control").each(function () {
            $(this).val('').attr('id', function (_, id) { return id + i });
            $('.btn-del').show();
        }).end().appendTo("#plg_1");
        i++;
        $('.date').datetimepicker({
            format: 'DD-MMM-YYYY'
        });
    });

    /*---------------------------------------------------------------------------*/

    $("#btnPlg_2").click(function () {
        debugger;
        $("#Plg_2 tr:last").clone().find(".cls").each(function () {
            $(this).val('').attr('id', function (_, id) { return id + i });

           // $('#delPlgPlg_2').prop('onClick', null);
           // $("#delPlgPlg_2").attr("onclick", "").unbind("click");
            $("#delPlgPlg_2").removeAttr("onclick");

            $('.btn-del').show();
        }).end().appendTo("#Plg_2");
        i++;

        

        $('.date').datetimepicker({
            format: 'DD-MMM-YYYY'
        });
    });

    /*---------------------------------------------------------------------------*/

    $("#sel_1").change(function () {
        var selValue = $('#sel_1').val();
        var dtLevel = "3";

        var query1 = "SELECT intBlockId AS COLUMN_NAME_VALUE , vchBlockName AS COLUMN_NAME_TEXT FROM m_block WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY vchBlockName ASC";
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
    });

    /*---------------------------------------------------------------------------*/

    $("#sel_3").change(function () {
        var selValue = $('#sel_3').val();
        var query1 = "SELECT DISTINCT intDiscomeid COLUMN_NAME_VALUE,(SELECT nvchLevelname FROM M_adm_leveldetails T WHERE T.intLevelDetailId=M.intdiscomeid)AS COLUMN_NAME_TEXT FROM T_Energy_EI_Block_ServiceUserMapping AS M WHERE intDistrictId=" + parseInt(selValue) + " ORDER BY COLUMN_NAME_TEXT";
        var ob = {};
        ob.query = query1;
        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: JSON.stringify(ob), //"{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_566').html('');
                $('#sel_566').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_566').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });
    });

    /*---------------------------------------------------------------------------*/

    $("#div_rad_25").hide();
    $("#div_rad_36").hide();

    /*---------------------------------------------------------------------------*/

    $("#sel_55").change(function () {
        var selValue = $('#sel_55').val();
        var dtLevel = "3";

        var query1 = "SELECT intSubCatagoryId AS COLUMN_NAME_VALUE , vchSubCatagory AS COLUMN_NAME_TEXT FROM T_Subcatagory WHERE intCatagoryId=" + parseInt(selValue) + " ORDER BY vchSubCatagory ASC";
        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query1 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_88').html('');
                $('#sel_88').append($("<option></option>").val('0').html('Select'));
                $.each(r.d, function () {
                    $('#sel_88').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });

        if ($("#sel_55").val() == "7") {
            $("#div_rad_25").show();
            $("#div_rad_36").hide();
        }
        else if ($("#sel_55").val() == "8") {
            $("#div_rad_25").hide();
            $("#div_rad_36").show();
        }
        else {
            $("#div_rad_25").hide();
            $("#div_rad_36").hide();
        }
    });

    /*---------------------------------------------------------------------------*/

    $("#sel_88").change(function () {
        
        if (($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") || ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") ||
            ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") || ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro")) {

            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                if ($("#sel_88").val() == "83") {
                    AmountDetails(250);
                }
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                if ($("#sel_88").val() == "83") {
                    AmountDetails(250);
                }
            }

            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                if ($("#sel_88").val() == "81") {
                    AmountDetails(250);
                }
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                if ($("#sel_88").val() == "81") {
                    AmountDetails(250);
                }
            }

            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                if ($("#sel_88").val() == "62") {
                    AmountDetails(50);
                }
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                if ($("#sel_88").val() == "62") {
                    AmountDetails(100);
                }
            }

            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                if ($("#sel_88").val() == "64") {
                    AmountDetails(200);
                }
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                if ($("#sel_88").val() == "64") {
                    AmountDetails(300);
                }
            }

            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                if ($("#sel_88").val() == "66") {
                    AmountDetails(250);
                }
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                if ($("#sel_88").val() == "66") {
                    AmountDetails(400);
                }
            }

            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                if ($("#sel_88").val() == "68") {
                    AmountDetails(850);
                }
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                if ($("#sel_88").val() == "68") {
                    AmountDetails(900);
                }
            }

            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                if ($("#sel_88").val() == "70") {
                    AmountDetails(2250);
                }
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                if ($("#sel_88").val() == "70") {
                    AmountDetails(2400);
                }
            }

            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                if ($("#sel_88").val() == "72") {
                    AmountDetails(3500);
                }
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                if ($("#sel_88").val() == "72") {
                    AmountDetails(4000);
                }
            }

            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                if ($("#sel_88").val() == "74") {
                    AmountDetails(4500);
                }
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                if ($("#sel_88").val() == "74") {
                    AmountDetails(5000);
                }
            }

            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                if ($("#sel_88").val() == "76") {
                    AmountDetails(7500);
                }
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                if ($("#sel_88").val() == "76") {
                    AmountDetails(8000);
                }
            }

            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                if ($("#sel_88").val() == "79") {
                    AmountDetails(12000);
                }
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                if ($("#sel_88").val() == "79") {
                    AmountDetails(15000);
                }
            }

            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                if ($("#sel_88").val() == "105") {
                    AmountDetails(250);
                }
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                if ($("#sel_88").val() == "105") {
                    AmountDetails(250);
                }
            }

            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                if ($("#sel_88").val() == "100") {
                    AmountDetails(200);
                }
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                if ($("#sel_88").val() == "100") {
                    AmountDetails(0);
                }
            }

            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                if ($("#sel_88").val() == "101") {
                    AmountDetails(400);
                }
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                if ($("#sel_88").val() == "101") {
                    AmountDetails(400);
                }
            }

            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                if ($("#sel_88").val() == "88") {
                    AmountDetails(1100);
                }
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                if ($("#sel_88").val() == "88") {
                    AmountDetails(1200);
                }
            }

            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                if ($("#sel_88").val() == "90") {
                    AmountDetails(2750);
                }
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                if ($("#sel_88").val() == "90") {
                    AmountDetails(3000);
                }
            }

            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                if ($("#sel_88").val() == "94") {
                    AmountDetails(12000);
                }
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                if ($("#sel_88").val() == "94") {
                    AmountDetails(15000);
                }
            }

            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                if ($("#sel_88").val() == "98") {
                    AmountDetails(20000);
                }
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                if ($("#sel_88").val() == "98") {
                    AmountDetails(22500);
                }
            }

            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                if ($("#sel_88").val() == "86") {
                    AmountDetails(250);
                }
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                if ($("#sel_88").val() == "86") {
                    AmountDetails(250);
                }
            }

            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                if ($("#sel_88").val() == "93") {
                    AmountDetails(3250);
                }
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                if ($("#sel_88").val() == "93") {
                    AmountDetails(3500);
                }
            }

            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                if ($("#sel_88").val() == "96") {
                    AmountDetails(17500);
                }
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                if ($("#sel_88").val() == "96") {
                    AmountDetails(2000);
                }
            }

            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                if ($("#sel_88").val() == "84") {
                    AmountDetails(100);
                }
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                if ($("#sel_88").val() == "84") {
                    AmountDetails(100);
                }
            }
        }
        else {

            var selValue = $('#sel_88').val();
            var query11 = "select top(1)intFees as COLUMN_NAME_TEXT  from T_Subcatagory where intSubCatagoryId=" + parseInt(selValue) + "";
            var query1 = "SELECT TOP(1) intFees FROM T_Subcatagory WHERE intSubCatagoryId=" + parseInt(selValue) + "";
            var ob = {};
            ob.query = query1;
            $.ajax({
                type: "POST",
                url: "FormView.aspx/ForSpecialCondionStringReturn",
                data: JSON.stringify(ob),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    AmountDetails(r.d);
                    console.log(r.d);
                }
            });
        }
    });

    /*---------------------------------------------------------------------------*/
    ///HT,LT calculation
    /*---------------------------------------------------------------------------*/
    $('input:radio[name=PeriodicalInspection ]').change(function () {
        debugger;
        //alert($("#sel_88").val());
        if ($("#sel_88").val() == "83") {
            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                AmountDetails(250);
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                AmountDetails(250);
            }
        }

        if ($("#sel_88").val() == "81") {
            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                AmountDetails(250);
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                AmountDetails(250);
            }
        }

        if ($("#sel_88").val() == "62") {
            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                AmountDetails(50);
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                AmountDetails(100);
            }
        }

        if ($("#sel_88").val() == "64") {
            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                AmountDetails(200);
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                AmountDetails(300);
            }
        }

        if ($("#sel_88").val() == "66") {
            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                AmountDetails(250);
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                AmountDetails(400);
            }
        }

        if ($("#sel_88").val() == "68") {
            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                AmountDetails(850);
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                AmountDetails(900);
            }
        }

        if ($("#sel_88").val() == "70") {
            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                AmountDetails(2250);
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                AmountDetails(2400);
            }
        }

        if ($("#sel_88").val() == "72") {
            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                AmountDetails(3500);
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                AmountDetails(4000);
            }
        }

        if ($("#sel_88").val() == "74") {
            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                AmountDetails(4500);
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                AmountDetails(5000);
            }
        }

        if ($("#sel_88").val() == "76") {
            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                AmountDetails(7500);
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                AmountDetails(8000);
            }
        }

        if ($("#sel_88").val() == "79") {
            if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "L.T /M.V") {
                AmountDetails(12000);
            }
            else if ($('input:radio[name=PeriodicalInspection ]:checked').val() == "HT") {
                AmountDetails(15000);
            }
        }
    });

    /*---------------------------------------------------------------------------*/

    $('input:radio[name=PeriodicalInspectionOFGenerator ]').change(function () {

        debugger;

        if ($("#sel_88").val() == "105") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(250);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(250);
            }
        }
        if ($("#sel_88").val() == "103") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(250);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(250);
            }
        }
        if ($("#sel_88").val() == "100") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(200);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(0);
            }
        }
        if ($("#sel_88").val() == "101") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(400);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(400);
            }
        }
        if ($("#sel_88").val() == "88") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(1100);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(1200);
            }
        }
        if ($("#sel_88").val() == "90") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(2750);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(3000);
            }
        }
        if ($("#sel_88").val() == "94") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(12000);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(15000);
            }
        }
        if ($("#sel_88").val() == "98") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(20000);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(22500);
            }
        }
        if ($("#sel_88").val() == "86") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(250);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(250);
            }
        }
        if ($("#sel_88").val() == "93") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(3250);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(3500);
            }
        }
        if ($("#sel_88").val() == "96") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(17500);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(20000);
            }
        }
        if ($("#sel_88").val() == "84") {
            if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Disel/Thermal") {
                AmountDetails(100);
            }
            else if ($('input:radio[name=PeriodicalInspectionOFGenerator ]:checked').val() == "Hydro") {
                AmountDetails(100);
            }
        }
    });

    /*---------------------------------------------------------------------------*/

    $("input[id*='txt_8']").keypress(function (e) {
        if ($('#txt_8').val().indexOf('.') != -1) {
            var cmp = e.which;
            if (e.which == 46) {
                e.preventDefault();
                return false;
            }
        }
    });

    /*---------------------------------------------------------------------------*/

    $("input[id*='txt_8']").keypress(function (e) {
        var unicode = e.charCode ? e.charCode : e.keyCode;
        if (unicode == 8 || unicode == 9 || (unicode >= 48 && unicode <= 57) || unicode == 46) {
            return true;
        }
        else {
            return false;
        }
    });

    /*---------------------------------------------------------------------------*/

    $("input[id*='txt_9']").keypress(function (e) {
        if ($('#txt_9').val().indexOf('.') != -1) {
            var cmp = e.which;
            if (e.which == 46) {
                e.preventDefault();
                return false;
            }
        }
    });

    /*---------------------------------------------------------------------------*/

    $("input[id*='txt_9']").keypress(function (e) {
        var unicode = e.charCode ? e.charCode : e.keyCode;
        if (unicode == 8 || unicode == 9 || (unicode >= 48 && unicode <= 57) || unicode == 46) {
            return true;
        }
        else {
            return false;
        }
    });

    /*---------------------------------------------------------------------------*/

    $("#Plg_2").change(function () {        
        CalFunc(0);
    });

    /*---------------------------------------------------------------------------*/
    
    //$("#Plg_2 >tbody > tr").on("remove", function () {
    //    alert("Element was removed");
    //});

    //$("#delPlg").click(function () {
    //    alert("ll");
    //});
   

    //$("#delPlg a").change(function () {
    //    debugger;
    //    CalFunc(0);
    //});


    //$("#delPlg a").on('click', function () {
    //    alert("ll");
    //});     
});

/*--============================================================================================================--*/

function distvalue(query2, dropid, ProposalId) {
    var distid = "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            if (r.d != "") {
                $("#" + distid).attr("disabled", true);
                distid = r.d;
                $('#' + dropid).val(r.d);
                blockvalue('', r.d, ProposalId);
            }
        }
    });
    return distid;
}

/*---------------------------------------------------------------------------*/

function distvalue1(query2, dropid, ProposalId) {
    var distid = "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMappingWithValidateProposal",
        data: "{'query':'" + query2 + "','ProposalId':'" + ProposalId + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
        if(r.d!="")
        {
            //$("#sel_3").attr("disabled", true);
            distid = r.d;
            $('#' + dropid).val(r.d);
            FillDiscom(r.d);
        }           
      }
    });
    return distid;
}

/*---------------------------------------------------------------------------*/

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
            if (r.d != "") {
                fillBlockDataAuto(distid, r.d);
            }
        }
    });
    return blockid;
}

/*---------------------------------------------------------------------------*/

function ControlChangeEvt(ids) {    
    var total = 0;
    $("#" + ids + " >tbody > tr").each(function () {
        var $row = $(this);
        var parentId = $row.closest('tr').find('[name*=' + 'Capacity' + ']').val();
        total = eval(total) + eval(parentId);
    });
    $('#txt_12').val(total);
}

/*---------------------------------------------------------------------------*/

function fillBlockDataAuto(selValue, setVal) {
    var dtLevel = "3";
    var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query2 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_2').html('');
            $('#sel_2').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                if (setVal == this.Value) {
                    $('#sel_2').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_2').append($("<option></option>").val(this.Value).html(this.Text));
                }
            });
        }
    });   
}

/*---------------------------------------------------------------------------*/

var ctr = 1;
function cloneTables(tblId) {
    var i = 0;
    $("#" + tblId + " tr:last").clone().find(".form-control").each(function () {
        $(this).val('').attr('id', function (_, id) { return id + i });

        $('.btn-del').show();

    }).end().appendTo("#" + tblId);
    i++;

    $('.date').datetimepicker({
        format: 'DD-MMM-YYYY'
    });
}

/*---------------------------------------------------------------------------*/

function FillCatagory() {
    var query1 = "select intCatagoryId as COLUMN_NAME_VALUE , vchCatagoryName as COLUMN_NAME_TEXT from T_Catagory order by vchCatagoryName asc";

    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_55').html('');
            $('#sel_55').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_55').append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*---------------------------------------------------------------------------*/

function FillCatagory(ControlIds) {   
    var ControlId = ControlIds;
    var query1 = "select intCatagoryId as COLUMN_NAME_VALUE , vchCatagoryName as COLUMN_NAME_TEXT from T_Catagory order by vchCatagoryName asc";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#' + ControlId).html('');
            $('#' + ControlId).append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#' + ControlId).append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*---------------------------------------------------------------------------*/

function FillSubCatagory(ControlId, CatVl) {
    var query1 = "select intSubCatagoryId as COLUMN_NAME_VALUE , vchSubCatagory as COLUMN_NAME_TEXT from T_Subcatagory where intCatagoryId=" + parseInt(CatVl) + " order by vchSubCatagory asc";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#' + ControlId).html('');
            $('#' + ControlId).append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#' + ControlId).append($("<option></option>").val(this.Value).html(this.Text));
            });
        }
    });
}

/*---------------------------------------------------------------------------*/

function DdIndexChange(ids) {
    var curntId = ids.id
    var idsvl = $('#' + curntId).val();
    var subCateId = curntId.replace('Catagory', 'SubCategory');
    FillSubCatagory(subCateId, idsvl);
}

/*---------------------------------------------------------------------------*/

function AmountDetails(amount) {
    debugger;
    var strText = "";
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr width='50%'><th>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>"
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(amount);
}

/*---------------------------------------------------------------------------*/

function FillDiscom(selValue) {
    var query1 = "select distinct intDiscomeid COLUMN_NAME_VALUE,(select nvchLevelname from M_adm_leveldetails T where T.intLevelDetailId=M.intdiscomeid)as COLUMN_NAME_TEXT from T_Energy_EI_Block_ServiceUserMapping as M where intDistrictId=" + parseInt(selValue) + " order by COLUMN_NAME_TEXT";
    var ob = {};
    ob.query = query1;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FillDemographyData",
        data: JSON.stringify(ob), 
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            $('#sel_566').html('');
            $('#sel_566').append($("<option></option>").val('0').html('Select'));
            $.each(r.d, function () {
                $('#sel_566').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
            });
        }
    });
   // $("#sel_566").attr("disabled", true);
}

/*---------------------------------------------------------------------------*/

function OnchangeCloneDrp(ids) {

    var vrids = ids.id;
    var vrinsc = vrids.replace("Catagory", "PeriodicalInspection");
    var vrGen = vrids.replace("Catagory", "PeriodicalInspectionOFGenerato");
    var varinslbl = "lbl_" + vrinsc;
    var varGenlbl = "lbl_" + vrGen;

    if (vrids.indexOf("Catagory") >= 0) {
        if ($("#" + ids.id).val() == "7") {

            $('#' + varinslbl).show();
            $('#' + vrinsc).show();
            $('#' + vrGen).hide();
            $('#' + varGenlbl).hide();
        }
        else if ($("#" + ids.id).val() == "8") {

            $('#' + vrGen).show();
            $('#' + varGenlbl).show();
            $('#' + varinslbl).hide();
            $('#' + vrinsc).hide();
        }
        else {
            $('#' + varinslbl).hide();
            $('#' + vrinsc).hide();
            $('#' + vrGen).hide();
            $('#' + varGenlbl).hide();
        }
    }
    else {

    }

    var subIds = ids.id.replace("Catagory", "SubCategory");
    if (vrids.indexOf("Catagory") >= 0) {
        FillSubCatagory(subIds, $("#" + ids.id).val());
    }
}

/*---------------------------------------------------------------------------*/

function CalFunc(subCatVal) {

    debugger;

    var totalAmnt = 0;
    var amt = "0";
    var ids = "";

    $("#Plg_2 >tbody > tr").each(function () {
        var $row = $(this);
        var total = 0;      

        //----------------------------
        var vrSub = $row.closest('tr').find('[name*=' + 'SubCategory' + ']').val();
        var cateVal = $row.closest('tr').find('[name*=' + 'Catagory' + ']').val();
        var InsVal = $row.closest('tr').find('[name*=' + 'PeriodicalInspection' + ']').val();
        var InsValGener = $row.closest('tr').find('[name*=' + 'PeriodicalInspectionOFGenerato' + ']').val();
        if (cateVal == 8) {    
            var amt2 = GeneratorCal(vrSub, InsValGener);
            if (amt2 == "" || amt2 == "NaN" || amt2 == null) {
                amt2 = 0;
            }
            if (amt == "NaN" || amt == "undefined") {
                amt = 0;
            }
            amt = parseInt(amt) + parseInt(amt2);
        }
        else if (cateVal == 7) {          
            var amt1 = MotorCal(vrSub, InsVal);
            if (amt == "NaN" || amt == "undefined") {
                amt = 0;
            }
            amt = parseInt(amt) + parseInt(amt1);
        }
        else {
            //-----------------------------------
            if (ids == "") {
                ids = $row.closest('tr').find('[name*=' + 'SubCategory' + ']').val();
            }

            else {
                if ($row.closest('tr').find('[name*=' + 'SubCategory' + ']').val() != "7" || $row.closest('tr').find('[name*=' + 'SubCategory' + ']').val() != "8") {
                    ids = ids + "," + $row.closest('tr').find('[name*=' + 'SubCategory' + ']').val();
                }
                else {
                }
            }           
        }
    });

    if (ids != "") {
        var amt = Calucaltion(ids, amt);
    }
    else {
        AmountDetails(amt);
    }
}

/*---------------------------------------------------------------------------*/

function Calucaltion(subCatVal, amt) {

    var selValue = "";
    var strAmount = 0;

    var query1 = "SELECT SUM(intFees) AS fees FROM T_Subcatagory WHERE intSubCatagoryId IN (" + subCatVal + ")";
    var ob = {};
    ob.query = query1;
    $.ajax({
        type: "POST",
        url: "FormView.aspx/ForSpecialCondionStringReturn",
        data: JSON.stringify(ob),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
           var ammmt= parseInt(r.d) + parseInt(amt);
           AmountDetails(ammmt);
            selValue = r.d;
            strAmount = r.d;           
            console.log(r.d);
        }
    });
    return selValue;
}

/*---------------------------------------------------------------------------*/

function GeneratorCal(subcatval, GeneVal) {

    debugger;

    var val = "";

    if (subcatval == "105") {
        if (GeneVal == "Disel/Thermal") {
            val = 250;
        }
        else if (GeneVal == "Hydro") {
            val = 250;
        }
    }

    if (subcatval == "103") {
        if (GeneVal == "Disel/Thermal") {
            val = 250;
        }
        else if (GeneVal == "Hydro") {
            val = 250;
        }
    }

    if (subcatval == "100") {
        if (GeneVal == "Disel/Thermal") {
            val = 200;
        }
        else if (GeneVal == "Hydro") {
            val = 0;
        }
    }

    if (subcatval == "101") {
        if (GeneVal == "Disel/Thermal") {
            val = 400;
        }
        else if (GeneVal == "Hydro") {
            val = 400;
        }
    }

    if (subcatval == "88") {
        if (GeneVal == "Disel/Thermal") {
            val = 1100;
        }
        else if (GeneVal == "Hydro") {
            val = 1200;
        }
    }
    if (subcatval == "90") {
        if (GeneVal == "Disel/Thermal") {
            val = 2750;
        }
        else if (GeneVal == "Hydro") {
            val = 3000;
        }
    }
    if (subcatval == "94") {
        if (GeneVal == "Disel/Thermal") {
            val = 12000;
        }
        else if (GeneVal == "Hydro") {
            val = 15000;
        }
    }
    if (subcatval == "98") {
        if (GeneVal == "Disel/Thermal") {
            val = 20000;
        }
        else if (GeneVal == "Hydro") {
            val = 22500;
        }
    }
    if (subcatval == "86") {
        if (GeneVal == "Disel/Thermal") {
            val = 250;
        }
        else if (GeneVal == "Hydro") {
            val = 250;
        }
    }
    if (subcatval == "93") {
        if (GeneVal == "Disel/Thermal") {
            val = 3250;
        }
        else if (GeneVal == "Hydro") {
            val = 3500;
        }
    }
    if (subcatval == "96") {
        if (GeneVal == "Disel/Thermal") {
            val = 17500;
        }
        else if (GeneVal == "Hydro") {
            val = 20000;
        }
    }
    if (subcatval == "84") {
        if (GeneVal == "Disel/Thermal") {
            val = 100;
        }
        else if (GeneVal == "Hydro") {
            val = 100;
        }
    }
    return val;
}

/*---------------------------------------------------------------------------*/

function MotorCal(subcatval, GeneVal) {
    var val1 = "";
    if (subcatval == "83") {
        if (GeneVal == "L.T /M.V") {
            val1 = 250;
        }
        else if (GeneVal == "HT") {
            val1 = 250;
        }
    }
    if (subcatval == "81") {
        if (GeneVal == "L.T /M.V") {
            val1 = 250;
        }
        else if (GeneVal == "HT") {
            val1 = 250;
        }
    }
    if (subcatval == "62") {
        if (GeneVal == "L.T /M.V") {
            val1 = 50;
        }
        else if (MotrVal == "HT") {
            val1 = 100;
        }
    }
    if (subcatval == "64") {
        if (GeneVal == "L.T /M.V") {
            val1 = 200;
        }
        else if (GeneVal == "HT") {
            val1 = 300;
        }
    }
    if (subcatval == "66") {
        if (GeneVal == "L.T /M.V") {
            val1 = 250;
        }
        else if (GeneVal == "HT") {
            val1 = 400;
        }
    }
    if (subcatval == "68") {
        if (GeneVal == "L.T /M.V") {
            val1 = 850;
        }
        else if (GeneVal == "HT") {
            val1 = 900;
        }
    }
    if (subcatval == "70") {
        if (GeneVal == "L.T /M.V") {
            val1 = 2250;
        }
        else if (GeneVal == "HT") {
            val1 = 2400;
        }
    }
    if (subcatval == "72") {
        if (GeneVal == "L.T /M.V") {
            val1 = 3500;
        }
        else if (GeneVal == "HT") {
            val1 = 4000;
        }
    }
    if (subcatval == "74") {
        if (GeneVal == "L.T /M.V") {
            val1 = 4500;
        }
        else if (GeneVal == "HT") {
            val1 = 5000;
        }
    }
    if (subcatval == "76") {
        if (GeneVal == "L.T /M.V") {
            val1 = 7500;
        }
        else if (GeneVal == "HT") {
            val1 = 8000;
        }
    }
    if (subcatval == "79") {
        if (GeneVal == "L.T /M.V") {
            val1 = 12000;
        }
        else if (GeneVal == "HT") {
            val1 = 15000;

        }
    }
    return val1;
}

/*---------------------------------------------------------------------------*/