﻿
$(document).ready(function () {

    

    $("#btnDraft").attr("disabled", true);

    $('#chk_1').click(function () {
        if (document.getElementById('chk_1').checked) {
            $("#btnDraft").attr("disabled", false);
        }
        else {
            $("#btnDraft").attr("disabled", true);
        }
    });

    /*--------------------------------------------------------------------------------------------------*/

    $("#btnSubmit").attr("disabled", true); //-----for disble button

    $('#chk_1').click(function () {
        if (document.getElementById('chk_1').checked) {
            $("#btnSubmit").attr("disabled", false);
        }
        else {
            $("#btnSubmit").attr("disabled", true);
        }
    });


    /*--------------------------------------------------------------------------------------------------*/
    $("#plugin_1").change(function () {
        PluginCalfun();
        acrecalcutaion();
    });

    $("#txt_1").attr("disabled", "disabled");
    $("#txt_15").attr("disabled", "disabled");
    $("#txt_16").attr("disabled", "disabled");
    $("#txt_17").attr("disabled", "disabled");

    //$('#pnl_1134').hide();
    

    //$('input:radio[name=Otherlocation]').change(function () {
    //    if ($('input:radio[name=Otherlocation]:checked').val() === "Yes") {
    //        $("#txt_15").val('');
    //        $("#txt_16").val('');
    //        $("#txt_17").val('');
    //        $("#plugin_1").change(function () {
    //            $("#txt_15").attr("disabled", "disabled");
    //            $("#txt_16").attr("disabled", "disabled");
    //            $("#txt_17").attr("disabled", "disabled");
    //            PluginCalfun();
    //            acrecalcutaion();             
    //        });
    //    }
    //    else if ($('input:radio[name=Otherlocation]:checked').val() === "No")
    //    {
    //        $("#txt_15").val('');
    //        $("#txt_16").val('');
    //        $("#txt_17").val('');
    //        $('#pnl_1134').hide();
    //        $("#txt_15").attr("disabled", false);
    //        $("#txt_16").attr("disabled", false);
    //        $("#txt_17").attr("disabled", "disabled");
    //        acrecalcutaion();
    //    }
        
    //})
         
});

/*-----------------------This section for calculate land base on investment amount-----------------------*/


$(document).on('keyup', '#txt_15', function () {
    var invstamou = $('#txt_15').val();
    if (parseInt(invstamou) <= 5000) {
        $('#txt_17').val(0);
    }
    else if (parseInt(invstamou) <= 10000) {
        $('#txt_17').val(0.5);
    }
    else if (parseInt(invstamou) <= 50000 ) {
        $('#txt_17').val(1);
    }
    else
    {
        $('#txt_17').val(2);
    }
    acrecalcutaion();
})

/*-------------------------------------------End---------------------------------------------------------*/


/*-----------------------This section for calculate land base on Direct Employment-----------------------*/

$(document).on('keyup', '#txt_16', function () {
    
    var employ = $('#txt_16').val();
    if ( parseInt(employ) <= 500) {
        $('#txt_17').val(0);
    }
    else if ( parseInt(employ) <= 1000) {
        $('#txt_17').val(0.5);
    }
    else if ( parseInt(employ) <=5000) {
        $('#txt_17').val(1);
    }
    else {
        $('#txt_17').val(2);
    }
    acrecalcutaion();
})

/*-------------------------------------------End---------------------------------------------------------*/


/*-------For autocalculation of total investment and tolal employement on base of plugin record--------------*/

function PluginCalfun() {

    var newrow = $('#plugin_1 >tbody >tr').length; //2

    var k = 1;
    var totalinvst = 0;
    var totalemploy = 0;
    for (var i = 1; i < newrow; i++)//4
    {
 
        if (i % 2 == ! 0) {

            var invst = "plugin_1_InvestmentAmount_" + k;
            var employ = "plugin_1_Employment_" + k;
          
            k++;
            totalinvst = parseInt(totalinvst) + parseInt($('#' + invst).val());

            totalemploy = parseInt(totalemploy) + parseInt($('#' + employ).val());
        }
    }

    $('#txt_15').val(totalinvst);
    $('#txt_16').val(totalemploy);
    
}


/*-------------------------------End-------------------------------------------------------------*/

/*---------------------------This Function for Calculation of Land Acres---------------------------------*/


function acrecalcutaion() {

    var invstamou = $('#txt_15').val();
    var employ = $('#txt_16').val();
    if (parseInt(invstamou) > 0 || parseInt(employ) > 0) {

        if (parseInt(invstamou) <= 5000 && parseInt(employ) <= 500) {
            $('#txt_17').val(0);
        }
        else if (parseInt(invstamou) <= 10000 && parseInt(employ) <= 1000) {
            $('#txt_17').val(0.5);
        }
        else if (parseInt(invstamou) <= 50000 && parseInt(employ) <= 5000) {
            $('#txt_17').val(1);
        }
        else {
            $('#txt_17').val(2);
        }

    }
    else {
        $('#txt_17').val(0);
    }
      
}



/*-------------------------------End-------------------------------------------------------------*/


/*---------------------------This Section for  Bind Block-----------------------------------------------*/
$(function () {

    $("#sel_6").change(function () {
       
        var selValue = $('#sel_6').val();
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
                    $('#sel_7').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });

    });

})


/*----------------------------------------------End---------------------------------------------------------*/
   
/*-----------------------------------This Section for  Bind Subsector-------------------------------------------------*/


$(function () {

    $("#sel_4").change(function () {
        
        var selValue = $('#sel_4').val();
        var query2 = "select intSubSectorId as COLUMN_NAME_VALUE , vchSubSectorName as COLUMN_NAME_TEXT from M_IIMS_SUBSECTOR  where intSectorId=" + parseInt(selValue) + " order by vchSubSectorName asc";
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
                    $('#sel_5').append($("<option></option>").val(this.Value).html(this.Text));
                });
            }
        });

    });

})

/*----------------------------------------------End---------------------------------------------------------*/


/*------------------------------------Plugin show and hide---------------------------------------------------*/

//$(function () {
    
//    $('input:radio[name=Otherlocation]').change(function () {
//        if ($('input:radio[name=Otherlocation]:checked').val() === "Yes")
//        {
//            $('#pnl_1134').show();
//            $("#txt_15").attr("disabled", "disabled");
//            $("#txt_16").attr("disabled", "disabled");
//            $("#txt_17").attr("disabled", "disabled");

//        }
//        else if ($('input:radio[name=Otherlocation]:checked').val() === "No")
//        {
//            $('#pnl_1134').hide();
//            $("#txt_15").attr("disabled", false);
//            $("#txt_16").attr("disabled", false);
//            $("#txt_17").attr("disabled", "disabled");
//        }
//    })
//})

/*------------------------------------------End----------------------------------------------------------*/






