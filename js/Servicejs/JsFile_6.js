
function ControlChangeEvtDate() {
   // alert("thhj");
}
function deleteEvt(ids) {
    ControlChangeEvt(ids);
}
function ControlChangeEvt(ids) {

    //alert("hi");

    var totalEmp = 0;
    $("[id*=" + ids + "] >tbody > tr").each(function () {
        debugger;
    
        //$("[id*=" + "plg_1" + "] >tbody > tr").each(function () {
        var $row = $(this);
        var parentId = $row.closest('tr').find('[name*=' + 'MaximumNoMigrant' + ']').val();
        totalEmp = eval(totalEmp) + eval(parentId);
        if(totalEmp < 5)
        {
        
            $row.closest('tr').find('[name*=' + 'MaximumNoMigrant' + ']').val("");
        }

    });

    if (totalEmp < 5) {

        jAlert(" Number of migrant workmen can  not be less than 5 !");
//        $row.closest('tr').find('[name*=' + 'MaximumNoMigrant' + ']').val();
    }
    //alert( totalEmp);
            if ((totalEmp) >= 5  && (totalEmp) <= 20) {
                AmountDetails(400);

            }
            else if ((totalEmp) >= 21 && (totalEmp) <= 50) {
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
            else if ((totalEmp) >= 401 && (totalEmp) <= 800) {
                AmountDetails(12000);
            }
            else if ((totalEmp) >= 801 && (totalEmp) <= 1000) {
                AmountDetails(13000);
            }
            else if ((totalEmp) >= 1001) {
                AmountDetails(20000);
            }



    //AmountDetails("50");
//    $('#plg_1 > tbody  > tr').each(function() {
//    
//    alert($(this).find("input[id^='MaximumNoMigrant']").val());
//    });


}
$(document).ready(function () {

    $("[id*=" + "plg_1" + "] >tbody > tr").each(function () {
        var $row = $(this);
        var parentId = $row.closest('tr').find('[name*=' + 'EstimateddateofCommencement' + ']').val();
        //        totalEmp = eval(totalEmp) + eval(parentId);
       // alert(parentId);


        $row.closest('tr').find('[name*=' + 'EstimateddateofCommencement' + ']').on("dp.change", function (e) {
            $row.closest('tr').find('[name*=' + 'EstimatedDateOfTermination' + ']').data("DateTimePicker").minDate(e.date);
        });



    });








    //$('#tblOne > tbody  > tr').each(function() {...code...});
    //    $("#plg_1_MaximumNoMigrant_4").focusout(function () {
    //        debugger;
    //        if ((parseInt($("#plg_1_MaximumNoMigrant_4").val()) >= 5) &&
    //        (parseInt($("#plg_1_MaximumNoMigrant_4").val()) <= 20)) {
    //            AmountDetails(400);

    //        }
    //        else if ((parseInt($("#plg_1_MaximumNoMigrant_4").val()) >= 21) &&
    //        (parseInt($("#plg_1_MaximumNoMigrant_4").val()) <= 50)) {
    //            AmountDetails(1000);
    //        }
    //        else if ((parseInt($("#plg_1_MaximumNoMigrant_4").val()) >= 51) &&
    //        (parseInt($("#plg_1_MaximumNoMigrant_4").val()) <= 100)) {
    //            AmountDetails(2000);
    //        }
    //        else if ((parseInt($("#plg_1_MaximumNoMigrant_4").val()) >= 101) &&
    //        (parseInt($("#plg_1_MaximumNoMigrant_4").val()) <= 200)) {
    //            AmountDetails(4000);
    //        }
    //        else if ((parseInt($("#plg_1_MaximumNoMigrant_4").val()) >= 201) &&
    //        (parseInt($("#plg_1_MaximumNoMigrant_4").val()) <= 400)) {
    //            AmountDetails(8000);
    //        }
    //        else if ((parseInt($("#plg_1_MaximumNoMigrant_4").val()) >= 401) &&
    //        (parseInt($("#plg_1_MaximumNoMigrant_4").val()) <= 800)) {
    //            AmountDetails(12000);
    //        }
    //        else if ((parseInt($("#plg_1_MaximumNoMigrant_4").val()) >= 801) &&
    //        (parseInt($("#plg_1_MaximumNoMigrant_4").val()) <= 1000)) {
    //            AmountDetails(13000);
    //        }
    //        else if ((parseInt($("#plg_1_MaximumNoMigrant_4").val()) >= 1001)) {
    //            AmountDetails(20000);
    //        }
    //    });

    // $('.nmCnt').click
    //-------------------------------------------------------------------------------
    //    strTitle = $('#plg_1').attr("title");
    //    $("[id*=EstimateddateofCommencement]").change(function ()
    //    {
    //    alert('hi');
    //});

    //    $("#plg_1 >tbody > tr").each(function () {
    //        var $row = $(this);
    //        debugger;
    //        var tblidd = this.id;
    //        var id = 0;
    //        var titlearray = strTitle.split('-');
    //        var $row = $(this);
    //        var parentId = $row.closest('tr').find('[name*=' + titlearray[0] + ']').val();
    //        if (parentId != undefined && parentId != 'undefined') {
    //            id = parseInt(id) + 1;
    //            for (var i = 0; i < titlearray.length; i++) {
    //                var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
    //                strName = titlearray[i];
    //                var idd = "plugin_4" + '_' + titlearray[i] + '_' + id;
    //                $("#" + idd).removeClass("req rset");
    //            }

    //        }

    //    });
    //---------------------------------------------------------------------------------


    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        ProposalId = urlparam[1];
    }
    ProposalId = $('#hdnProposalNo').val();
    if (ProposalId != "")
  {
    var query = "select intDistrictId from T_LandAndUtility where vchProposalNo=" + ProposalId + ""
    var distiD = distvalue(query, 'sel_1', ProposalId);
    }
    //----------------------SameAs Show---------------------
    $('#divsmchk_txt_56').show();
    //-------------------End------------------

//    $("#txt_1").attr("disabled", true);
//    $("#txt_3").attr("disabled", true);
//    if ($("#txt_4").val()!="")
//    {
//      $("#txt_4").attr("disabled", true); 
//    }
//   else
//    {
//     $("#txt_4").attr("disabled", false); 
//    }

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
    //---------------------------------NameValidation--------------
    $('#txt_4').keypress(function (e) {
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
    $('#txt_8').keypress(function (e) {
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
    //---------------------------------------------------------end---------
    ///-----------------------------------------------------
//    $('#plg_1_EstimateddateofCommencement_5').focusout(function (e) {
//        debugger;
//        //        alert('hi');
//        strTitle = $('#plg_1').attr("title");
//        $("#plg_1 >tbody > tr").each(function () {
//            var frmDt = "EstimateddateofCommencement";
//            var toDt = "EstimatedDateOfTermination";
//            var $row = $(this);
//            debugger;
//            var tblidd = this.id;
//            var id = 0;
//            var titlearray = strTitle.split('-');
//            var $row = $(this);
//            var parentId = $row.closest('tr').find('[name*=' + frmDt + ']').val();
//            if (parentId != undefined && parentId != 'undefined') {
////                var toId = $row.closest('tr').find('[name*=' + toDt + ']').val();
//                $('[name*=' + toDt + ']').attr("min", parentId);
//                //                id = parseInt(id) + 1;
//                //                for (var i = 0; i < titlearray.length; i++) {
//                //                    var strJsonValue = $row.closest('tr').find('[name*=' + titlearray[i] + ']').val();
//                //                    strName = titlearray[i];
//                //                    var idd = "plugin_8" + '_' + titlearray[i] + '_' + id;
//                //                    $("#" + idd).removeClass("req rset");
//                //                }

//            }

//        });
//    });



    ////----------------------------
    $("#btnid").click(function () {
        $("#plg_1 tr:last").clone().find(".form-control").each(function () {
            $(this).val('').attr('id', function (_, id) { return id + i });
            $('.btn-del').show();


            // $(this).attr("onchange", "ControlChangeEvt();");

        }).end().appendTo("#plg_1");
        i++;
        $('.date').datetimepicker({
            format: 'DD-MMM-YYYY'
        });

        var ctr = 0;

        $("[id*=" + "plg_1" + "] >tbody > tr").each(function () {
            var $row = $(this);
            var parentId = $row.closest('tr').find('[name*=' + 'EstimateddateofCommencement' + ']').val();
            //        totalEmp = eval(totalEmp) + eval(parentId);
           // alert(parentId);
            ctr = ctr + 1;
            if (ctr > 1) {
                $row.closest('tr').find('[name*=' + 'EstimateddateofCommencement' + ']').on("dp.change", function (e) {
                    $row.closest('tr').find('[name*=' + 'EstimatedDateOfTermination' + ']').data("DateTimePicker").minDate(e.date);
                });
            }



        });



        return false;
    });

    $("#sel_1").change(function () {
        debugger;
        var selValue = $('#sel_1').val();
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
                $('#sel_2').html('');
                $('#sel_2').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_2').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });



    $("#sel_3").change(function () {
        debugger;
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
                $('#sel_4').append($("<option></option>").val('0').html('Select'))
                $.each(r.d, function () {
                    $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                })
            }
        });
    });


    $('#smchk_txt_56').click(function () {
        debugger;
        if (document.getElementById("smchk_txt_56").checked) {
            $('#sel_3').val($('#sel_1').val());


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
                    $('#sel_4').html('');
                    $('#sel_4').append($("<option></option>").val('0').html('Select'))
                    $.each(r.d, function () {

                        if ($('#sel_2').val() == this.Value) {
                            $('#sel_4').append($("<option selected='true'></option>").val(this.Value).html(this.Text));
                        }
                        else {
                            $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                        }
                        //                        $('#sel_4').append($("<option></option>").val(this.Value).html(this.Text));
                        //                        console.log(r.d);
                    })
                }
            });
            debugger;
            $('#txt_5').val($('#txt_3').val());
        } else {
            $('#sel_3').val("0");
            $('#sel_4').val("0");
            $('#sel_4').html('');
            $('#sel_4').append($("<option></option>").val('0').html('Select'))
            $('#txt_5').val("");
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
                $("#sel_1").attr("disabled", true);
                distid = r.d;
                //  alert(r.d);
                $('#' + dropid).val(r.d);
                //fillBlockDataAuto(r.d);

                blockvalue('', r.d);
            }
            
        }

    });
    //$("#sel_1").attr("disabled", true); 
    return distid;

}

function blockvalue(query2, distid) {
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
            // alert(r.d);
            //  alert(dropid);
            // $('#' + dropid).val(r.d);
            // fillBlockDataAuto(r.d);


            fillBlockDataAuto(distid, r.d);

            // return blockid;
        }

    });
    $("#sel_2").attr("disabled", true); 
    return blockid;

}




function fillBlockDataAuto(selValue, setVal) {


    // var selValue = $('#sel_6').val();
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


    //    var query = "select intExisBlock from T_PEAL_PROMOTER where vchProposalNo=" + ProposalId + ""
    //    var blockid = blockvalue(query, 'sel_5');

    //    alert(blockvalue(query, 'sel_5'));


}
 function AmountDetails(amount) {
            var strText = "";
            strText = strText + "<h4 class='text-left'><b>Payment Details</b></h4><table class='table table-bordered'><tr><th width='50%'>Total Amount</th><td width='50%'><b>" + amount + "/-</b></td></tr></table>"
            lblAmount.innerHTML = strText;
            $('#hdnTotalAmount').val(amount);
        }

        function DateValidation(ths) {
                debugger;
                    alert('hi');
                strTitle = $('#plg_1').attr("title");
                $("#plg_1 >tbody > tr").each(function () {
                    var frmDt = "EstimateddateofCommencement";
                    var toDt = "EstimatedDateOfTermination";
                    var $row = $(this);
                    debugger;
                    var tblidd = this.id;
                    var id = 0;
                    var titlearray = strTitle.split('-');
                    var $row = $(this);
                    var parentId = $row.closest('tr').find('[name*=' + frmDt + ']').val();
                    if (parentId != undefined && parentId != 'undefined') {
                        $('[name*=' + toDt + ']').attr("min", parentId);
                    }

                });
        }
