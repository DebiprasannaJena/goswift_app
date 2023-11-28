
function ControlChangeEvtDate() {
   
}
function deleteEvt(ids)
{
    ControlChangeEvt(ids);
}
function ControlChangeEvt(ids) {
   
    var totalEmp = 0;
    $("[id*=" + ids + "] >tbody > tr").each(function () {
        //$("[id*=" + "plg_1" + "] >tbody > tr").each(function () {
        var $row = $(this);
        var parentId = $row.closest('tr').find('input:text[id*=' + 'MaximumNoWork' + ']').val();
        totalEmp = eval(totalEmp) + eval(parentId);
        //alert(parentId);
    });
    //if ((totalEmp) >= 1 && (totalEmp) <= 20) {
    //    AmountDetails(400);
    //}
    //else if ((totalEmp) >= 21 && (totalEmp) <= 50) {
    //    AmountDetails(1000);
    //}
    //else if ((totalEmp) >= 51 && (totalEmp) <= 100) {
    //    AmountDetails(2000);
    //}
    //else if ((totalEmp) >= 101 && (totalEmp) <= 200) {
    //    AmountDetails(4000);
    //}
    //else if ((totalEmp) >= 201 && (totalEmp) <= 400) {
    //    AmountDetails(8000);
    //}
    //else if ((totalEmp) >= 401) {
    //    AmountDetails(10000);
    //}
    if ((totalEmp) >= 1 && (totalEmp) <= 20) {
        AmountDetails(0);
    }
    else if ((totalEmp) >= 21 && (totalEmp) <= 49) {
        AmountDetails(0);
    }
    else if ((totalEmp) >= 49 && (totalEmp) <= 50) {
        AmountDetails(1000);
    }
    else if ((totalEmp) >= 51 && (totalEmp) <= 100) {
        AmountDetails(2000);
    }
    else if ((totalEmp) >= 101 && (totalEmp) <= 200) {
        AmountDetails(4000);
    }
    else if ((totalEmp) >= 201 && (totalEmp) <= 400) {
        AmountDetails(8000);
    }
    else if ((totalEmp) >= 401) {
        AmountDetails(10000);
    }
}

$(document).ready(function () {
    var ProposalId = "";

    //-------------Plugin Datevalidation------
    $("[id*=" + "Plugn_1" + "] >tbody > tr").each(function () {
        var $row = $(this);
        var parentId = $row.closest('tr').find('[name*=' + 'EstimateddateofCommencement' + ']').val();
        //        totalEmp = eval(totalEmp) + eval(parentId);
        // alert(parentId);

        var dtt = new Date();

        $row.closest('tr').find('[name*=' + 'EstimateddateofCommencement' + ']').datetimepicker({
            format: 'DD-MMM-YYYY'
        }).on('dp.show', function () {
            return $(this).data('DateTimePicker').minDate(new Date());
        });




    });

    //----------------------------------DateValidation----------------------------


    $('#txt_21').datetimepicker({
        format: 'DD-MMM-YYYY'
    }).on('dp.show', function () {
        return $(this).data('DateTimePicker').minDate(new Date());
    });
    //----------------------SameAs Show---------------------
    $('#divsmchk_txt_10').show();
    $('#divsmchk_txt_14').show();
    $('#divsmchk_txt_18').show();
    //-------------------End------------------

    //---------datevalidation-------------
    $('#txt_21').datetimepicker({
        format: 'DD-MMM-YYYY'
    }).on('dp.show', function () {
        return $(this).data('DateTimePicker').minDate(new Date());
    });

    //------------------SubmitButton Active----------------
    $("#btnSubmit").attr("disabled", true); //-----for disble button
    $('#chk_22').click(function () {
        if (document.getElementById('chk_22').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });
    //---------------------End-----------------------
    //    $("#txt_2").attr("disabled", true);
    //    $("#sel_1").attr("disabled", true);
    //$("#txt_5").attr("disabled", true);
    // $("#txt_6").attr("disabled", true);
    //    if ($("#txt_6").val() != "") {
    //        $("#txt_6").attr("disabled", true);
    //    }
    //    else {
    //        $("#txt_6").attr("disabled", false);
    //    }
    //    if ($("#txt_5").val() != "") {
    //        $("#txt_5").attr("disabled", true);
    //    }
    //    else {
    //        $("#txt_5").attr("disabled", false);
    //    }
    //    if ($("#txt_2").val() != "") {
    //        $("#txt_2").attr("disabled", true);
    //    }
    //    else {
    //        $("#txt_2").attr("disabled", false);
    //    }

    //----------vaidation---------
    $('#txt_2').keypress(function (e) {
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
    $('#txt_6').keypress(function (e) {
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
    $('#txt_7').keypress(function (e) {
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
    $('#txt_11').keypress(function (e) {
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
    //--------------

    $('.btn-del').hide();
    $("#btnPlugn_1").click(function () {
        $("#Plugn_1 tr:last").clone().find(".form-control").each(function () {
            $(this).val('').attr('id', function (_, id) { return id + i });
            $('.btn-del').show();


            // $(this).attr("onchange", "ControlChangeEvt();");

        }).end().appendTo("#Plugn_1");
        i++;
        $('.date').datetimepicker({
            format: 'DD-MMM-YYYY'
        });

        var ctr = 0;

        $("[id*=" + "Plugn_1" + "] >tbody > tr").each(function () {
            var $row = $(this);
            var parentId = $row.closest('tr').find('[name*=' + 'EstimateddateofCommencement' + ']').val();
            //        totalEmp = eval(totalEmp) + eval(parentId);
            // alert(parentId);
            ctr = ctr + 1;
            if (ctr > 1) {
                var dtt = new Date();

                $row.closest('tr').find('[name*=' + 'EstimateddateofCommencement' + ']').datetimepicker({
                    format: 'DD-MMM-YYYY'
                }).on('dp.show', function () {
                    return $(this).data('DateTimePicker').minDate(new Date());
                });
            }



        });



        return false;
    });
    //----------------AutoFill Dropdwon-------------------------
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        ProposalId = urlparam[1];
    }
    ProposalId = $('#hdnProposalNo').val();
    if (ProposalId != "") {
        ConstitutionFill(ProposalId);
        var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + ""
        var distiD = distvalue(query, 'sel_3', ProposalId);
    }
    //------------------------------------------
    //  $('.btn-del').hide();
    //    $("#btnid").click(function () {
    //        $("#Plugn_1 tr:last").clone().find(".form-control").each(function () {
    //            $(this).val('').attr('id', function (_, id) { return id + i });
    //            $('.btn-del').show();
    //        }).end().appendTo("#Plugn_1");
    //        i++;
    //        $('.date').datetimepicker({
    //            format: 'DD-MMM-YYYY'
    //        });
    //        return false;
    //    });

    $("#sel_3").change(function () {
        
        var selValue = $('#sel_3').val();
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
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });


    $("#sel_12").change(function () {
        
        var selValue = $('#sel_12').val();
        var dtLevel = "3";
        var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_13').html('');
                $('#sel_13').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_13').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });

    $("#sel_16").change(function () {
        
        var selValue = $('#sel_16').val();
        var dtLevel = "3";
        var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_17').html('');
                $('#sel_17').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_17').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });



    $("#sel_8").change(function () {
        
        var selValue = $('#sel_8').val();
        var dtLevel = "3";
        var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + " order by vchBlockName asc";

        $.ajax({
            type: "POST",
            url: "FormView.aspx/FillDemographyData",
            data: "{'query':'" + query2 + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (r) {
                $('#sel_9').html('');
                $('#sel_9').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_9').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });


    $('#smchk_txt_10').click(function () {
        
        if (document.getElementById("smchk_txt_10").checked) {
            $('#sel_8').val($('#sel_3').val());
            $('#txt_10').val($('#txt_5').val());
            var selValue = $('#sel_8').val();
            var dtLevel = "3";
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + "order by vchBlockName asc";

            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_9').html('');
                    $('#sel_9').append($("<option></option>").val('0').html('Select'))
                    $.each(r.d, function () {
                        //                        $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                        //                        console.log(r.d);

                        if ($('#sel_4').val() == this.Value) {
                            $('#sel_9').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_9').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    })
                }
            });

        } else {
            $('#sel_8').val("0");
            $('#sel_9').val("0");
            $('#sel_9').html('');
            $('#sel_9').append($("<option></option>").val('0').html('Select'))
            $('#txt_10').val("");
        }
    });

    $('#smchk_txt_14').click(function () {
        
        if (document.getElementById("smchk_txt_14").checked) {
            $('#sel_12').val($('#sel_8').val());
            $('#txt_14').val($('#txt_10').val());
            var selValue = $('#sel_12').val();
            var dtLevel = "3";
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + "order by vchBlockName asc";

            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_13').html('');
                    $('#sel_13').append($("<option></option>").val('0').html('Select'))
                    $.each(r.d, function () {
                        //                        $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                        //                        console.log(r.d);

                        if ($('#sel_9').val() == this.Value) {
                            $('#sel_13').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_13').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    })
                }
            });

        } else {
            $('#sel_12').val("0");
            $('#sel_13').val("0");
            $('#sel_13').html('');
            $('#sel_13').append($("<option></option>").val('0').html('Select'))
            $('#txt_14').val("");
        }
    });

    $('#smchk_txt_18').click(function () {
        
        if (document.getElementById("smchk_txt_18").checked) {
            $('#sel_16').val($('#sel_12').val());
            $('#txt_18').val($('#txt_14').val());
            var selValue = $('#sel_12').val();
            var dtLevel = "3";
            var query1 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + "order by vchBlockName asc";

            $.ajax({
                type: "POST",
                url: "FormView.aspx/FillDemographyData",
                data: "{'query':'" + query1 + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $('#sel_17').html('');
                    $('#sel_17').append($("<option></option>").val('0').html('Select'))
                    $.each(r.d, function () {
                        //                        $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                        //                        console.log(r.d);

                        if ($('#sel_13').val() == this.Value) {
                            $('#sel_17').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_17').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                    })
                }
            });

        } else {
            $('#sel_16').val("0");
            $('#sel_17').val("0");
            $('#sel_17').html('');
            $('#sel_17').append($("<option></option>").val('0').html('Select'))
            $('#txt_18').val("");
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
            if (r.d != "") {
                $("#sel_3").attr("disabled", true);
                distid = r.d;
                $('#' + dropid).val(r.d);
                blockvalue('', r.d, ProposalId);
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
    $("#sel_4").attr("disabled", true);
    return blockid;
}

function fillBlockDataAuto(selValue, setVal) {
    
    var dtLevel = "3";
    var query2 = "select intBlockId as COLUMN_NAME_VALUE , vchBlockName as COLUMN_NAME_TEXT from m_block  where intDistrictId=" + parseInt(selValue) + "order by vchBlockName asc";

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

                if (setVal == this.Value) {
                    $('#sel_4').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                }
                else {
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                }
            })
        }
    });


}
function AmountDetails(amount) {
    var strText = "";
    strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>"
    lblAmount.innerHTML = strText;
    $('#hdnTotalAmount').val(amount);
}

function ConstitutionFill(ProposalId) {
    var query1 = "select isnull(intConstitution,0) from t_peal_promoter where vchProposalNo="+ProposalId+"";
    var distid = "";
    $.ajax({
        type: "POST",
        url: "FormView.aspx/FormToPealMapping",
        data: "{'query':'" + query1 + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            fillConstitution( r.d);
        }
    });
    return distid;
}

function fillConstitution(val) {
 
    var txtVal = "";
   
     if (val == "1") {
        txtVal = "Proprietorship"; 
    }
    else if (val == "2") {
        txtVal = "Partnership";
    }
    else  if (val == "3") {
        txtVal = "Private Limited Company";
    }
    else if (val == "4") {
    
        txtVal = "Public Limited Company";
    }
    else if (val == "5") {
        txtVal = "PSU";
    }
    else if (val == "6") {
        txtVal = "SPV";
    }
    else if (val == "7") {
        txtVal = "Co-operative";
    }
    else if (val == "8") {
        txtVal = "Others";
    }
     else {
        txtVal = "Select";
    }
    $('#sel_1').val(txtVal);
}

