$(document).ready(function () {

    $('input[type="radio"]').closest('div').find('.requireed').addClass('requireedRdbtn');
    $('.dateAddon').closest('div').find('.requireed').addClass('requireeDTbtn');
    $('span.colon').next('span').addClass('lableTextNew');
});

//$(function () {
//    $('#indentGenerate, .supplyOrderGenerate, #supplyOrderGenerateMd, #supplyCementOrderGenerate').click(function () {
//        var btnID = $(this).attr('id');
//        var windowName = "GeneratePage";
//        var wOption = "width=1280,height=600,menubar=yes,scrollbars=yes,location=no,left=100,top=100";
//        var cloneTable = $("#viewTable").clone();
//        cloneTable.find('input[type=text],select,textarea').each(function () {
//            var elementType = $(this).prop('tagName');
//            if (elementType == 'SELECT')
//                var textVal = $(this).find("option:selected").text();
//            else
//                var textVal = $(this).val();
//            $(this).replaceWith('<label>' + textVal + '</label>');
//        });
//        cloneTable.find('a').each(function () {
//            var anchorVal = $(this).text();
//            $(this).replaceWith('<label>' + anchorVal + '</label>');
//        });
//        var wWinPrint = window.open("", windowName, wOption);
//        wWinPrint.document.open();
//        wWinPrint.document.write("<html><head><link href='../css/Print.css' rel='stylesheet' /><link href='../css/font-awesome.min.css' rel='stylesheet' /><title></title></head><body>");
//        wWinPrint.document.write('<div style="padding:5px; margin-bottom:10px; border-bottom:#aeaeae solid 3px;"><img src=../img/HP_Govt.png height=55 style="float: left;margin-right: 10px;" /><div style="float: left;padding-top: 10px;"><h1>Himachal Pradesh State Civil Supplies Corporation Ltd</h1></div><div style="clear: both;"></div></div>');
//        if (btnID == "indentGenerate") {
//            wWinPrint.document.write("<br /><br /><div class='header'>M/s BHUBAN POWER & STEEL LTD.,<br />Factory Address : CHANDIGARH AMBALA ROAD, DERABASSI, DISTT. MOHALI-140507<br />Regd. Office: 3 INDUSTRIAL AREA, PHASE-1, CHANDIGARH-160002</div>")
//            wWinPrint.document.write("<hr /><div class='hd_title text-center'>DETAIL SHOWING RATES, DIVISION-WISE/DIA-WISE QUANTITIES & AMOUNTS F.O.R. DESTINATION FOR EXCISE DUTY NON-EXEMPT GI PIPES (September, 2014)</div><br />");
//        } else if (btnID == "supplyOrderGenerate") {
//            //wWinPrint.document.write("<br /><br /><div class='header'>M/s Associate Cement Corporation Ltd</div>")
//            //wWinPrint.document.write("<hr /><div class='hd_title text-center'></div><br />");
//        } else if (btnID == "supplyOrderGenerateMd") {
//            wWinPrint.document.write("<br /><br /><div class='header'>M/s APL APOLLO TUBES LTD,<br />REGD. OFFICE 37 HARGOVIND ENCLAVE, VIKAS MARG, DELHI-110092</div>")
//            wWinPrint.document.write("<hr /><div class='hd_title text-center'>DETAIL SHOWING THE  QUANTITY, RATES & AMOUNTS AT NEAREST RAIL HEAD FOR EXCISE DUTY EXEMPTED MEDICINE (September, 2014)</div><br />");
//        } else if (btnID == "supplyCementOrderGenerate") {
//            wWinPrint.document.write("<br /><br /><div class='header'>Ambuja Cement LTD,Khalini, Shimla-3</div>")
//        }

//        wWinPrint.document.write("<div id='printContent'>" + cloneTable.html() + "</div>");
//        //wWinPrint.document.write("<div id='printFooter'>"+printFooter+"</div>");
//        wWinPrint.document.write("</body></html>");
//        wWinPrint.document.close();
//        wWinPrint.focus();
//        return wWinPrint;
//    });
//    $('.PrintMedicineSupplyOrder').click(function () {
//        var windowName = "GeneratePage";
//        var wOption = "width=1024,height=600,menubar=yes,scrollbars=yes,location=no,left=100,top=100";
//        var wWinPrint = window.open("printSupplyOrder.aspx", windowName, wOption);
//    });
//    $('.generateStockTrnsf').click(function () {
//        var windowName = "GeneratePage";
//        var wOption = "width=1024,height=600,menubar=yes,scrollbars=yes,location=no,left=100,top=100";
//        var wWinPrint = window.open("generateStockTrnsf.aspx", windowName, wOption);
//    });
//    $('.generateStockReceipt').click(function () {
//        var windowName = "StockReceiptPage";
//        var wOption = "width=1024,height=600,menubar=yes,scrollbars=yes,location=no,left=100,top=100";
//        var wWinPrint = window.open("generateStockReceipt.aspx", windowName, wOption);
//    });
//    $('.generateCashMemo').click(function () {
//        var windowName = "CashMemoPage";
//        var wOption = "width=1024,height=600,menubar=yes,scrollbars=yes,location=no,left=100,top=100";
//        var wWinPrint = window.open("generateCashMemo.aspx", windowName, wOption);
//    });
//    $('.supplyOdrGnrate').click(function () {
//        var windowName = "supplyOdrGnrate";
//        var wOption = "width=1024,height=600,menubar=yes,scrollbars=yes,location=no,left=100,top=100";
//        var wWinPrint = window.open("printSupplyOrder.aspx", windowName, wOption);
//    });
//    $('#indentBillGenerate').click(function () {
//        var windowName = "IndentBillGeneratePage";
//        var wOption = "width=1024,height=400,menubar=yes,scrollbars=yes,location=no,left=100,top=100";
//        var wWinPrint = window.open("generateCementBill.aspx", windowName, wOption);
//    });
//    $('.printACRDetail').click(function () {
//        var windowName = "printACRDetailPage";
//        var wOption = "width=1024,height=600,menubar=yes,scrollbars=yes,location=no,left=100,top=100";
//        var wWinPrint = window.open("printACRDetail.aspx", windowName, wOption);
//    });
//    activeRadio();
//    $(document).on('click', 'input[type="radio"]', function () {
//        activeRadio();
//    });

////    $('.date-picker').datepicker({
////        changeMonth: true,
////        changeYear: true,
////        dateFormat: 'dd-M-yy'
////    });
//    $(document).on("click", ".dateAddon", function () {
//        $(this).prev('.date-picker').focus();
//    });
//});

function activeRadio() {
    $('input[type="radio"]').each(function () {
        if ($(this).is(':checked')) {
            $(this).closest('label').addClass('actvRadio');
        } else {
            $(this).closest('label').removeClass('actvRadio');
        }
    });
}
function openHTMLStrModal(header, body, footer) {
    $('#pageModal .modal-header #myModalLabel').html(header);
    $('#pageModal .modal-body').html(body);
    $('#pageModal .modal-footer').html(footer);
    if (footer == "") { $('#pageModal .modal-footer').remove(); }
    $('#pageModal').modal();
}
function openPageModal(header, page, footer, frm_hit) {
    $('#pageModal .modal-header #myModalLabel').html(header);
    $('#pageModal .modal-body').html("<iframe width='100%' height='" + frm_hit + "px' src='" + page + "' frameborder='0' scrolling='yes'></iframe>");
    $('#pageModal .modal-footer').html(footer);
    if (footer == "") { $('#pageModal .modal-footer').remove(); }
    $('#pageModal').modal();
}
function openPageModal_MD(header, page, footer, frm_hit) {
    $('#pageModal-md .modal-header #myModalLabel').html(header);
    $('#pageModal-md .modal-body').html("<iframe width='100%' height='" + frm_hit + "px' src='" + page + "' frameborder='0'></iframe>");
    $('#pageModal-md .modal-footer').html(footer);
    if (footer == "") { $('#pageModal-md .modal-footer').remove(); }
    $('#pageModal-md').modal();
}

//Added by Tapan Kumar Mishra For Refresh parent page after close modal page
function hidePageModal(opt) {
    $('#pageModal').modal('hide');
    $('#pageModal .modal-header #myModalLabel').html("");
    $('#pageModal .modal-body').html("");
    $('#pageModal .modal-footer').html("");
    if (opt.reload == true) {
        location.reload();
    }
}

//========================Added by Tapan Kumar Mishra ==================================

var month = new Array();
month[0] = "Jan";
month[1] = "Feb";
month[2] = "Mar";
month[3] = "Apr";
month[4] = "May";
month[5] = "Jun";
month[6] = "Jul";
month[7] = "Aug";
month[8] = "Sep";
month[9] = "Oct";
month[10] = "Nov";
month[11] = "Dec";

function IsSpecialCharacter1stPalce(cntr, strLang) {
    var strValue = $('#' + cntr).val();
    // alert(strValue);
    if (strValue != "") {
        var FistChar = strValue.charAt(0);
        if (/^[a-zA-Z0-9]*$/.test(FistChar) == false) {

            if (strLang == "E") {
                alert('Special characters Or White space are not allowed at 1st place !!!');
                $('#' + cntr).focus();
                return false;
            }
            else {
                alert('विशेष वर्ण या सफेद स्थान 1 जगह पर अनुमति नहीं है !!!');
                $('#' + cntr).focus();
                return false;
            }
           
        }
        else 
        {
            return true; 
        }
        return true;
    }
    else
        return true;
}

function checknumeric(cntr,strLang) {
    var strvalue = $('#' + cntr).val();
    var strvaluelng = $('#' + strLang).val();   
    if ($.isNumeric(strvalue)==true) {
        return true;
    }
    else {
        if (strvaluelng == "E") {
            alert('Enter a Numeric Value');
            $('#' + cntr).focus();
            return false;
        }
        else {
            alert('एक अंकीय मान दर्ज');
            $('#' + cntr).focus();
            return false;
        }
       
    }
}


function BlankListBox(cntr, strText, strLang) {
    debugger;
    if ($('#' + cntr + ' option').size() == 0) {
        if (strLang == "E") {
            alert(strText + " Can Not Be Left Blank");
            $('#' + cntr).focus();
            return false;
        }
        else {
            alert(strText + " खाली नहीं छोड़ा जा सकता");
            $('#' + cntr).focus();
            return false;
        }

    }
    else
        return true;
}

function DecimalNumber(cntr, strText, strLang) {
    var regexPattern = /^\d{0,18}(\.\d{1,3})?$/;
    var entered_value = $('#' + cntr).val();
    if (!regexPattern.test(entered_value)) {


        if (strLang == "E") {
            alert('Enter a valid ' + strText);
            $('#' + cntr).focus();
            return false;
        }
        else {
            alert('वैध दर्ज करें ' + strText);
            $('#' + cntr).focus();
            return false;
        }
        
    }
    else
        return true;
}


function isAlphaOrParen(str, strLang) 
{
    var strValue = $('#' + str).val();    
    if (strValue != "") 
    {
        var FistChar = strValue;
        if (/^[a-zA-Z() ]+$/.test(FistChar) == false) {


            if (strLang == "E") {
                alert('Only Alphabets are allowed !!!');
                $('#' + str).focus();
                return false;
            }
            else {
                alert('केवल अक्षर अनुमति दी जाती है !!!');
                $('#' + str).focus();
                return false;
            }
            
        }
        else
         {
            return true;
         }
        return true;
    }
    else
        return true;
}

function IsSpecialChar(cntr, strLang) {
    var str = $('#' + cntr).val();
    if (/^[a-zA-Z0-9- ]*$/.test(str) == false) {

        if (strLang == "E") {
            alert('Special characters are not allowed !!!');
            $('#' + cntr).focus();
            return false;
        }
        else {
            alert('विशेष वर्णों की अनुमति नहीं है !!!');
            $('#' + cntr).focus();
            return false;
        }
       
    }
    else {
        return true;
    }
   
}
function MobileNumber(cntr, strLang) 
{
    var Mobile = /^[7-9][0-9]{9}$/
    var entered_no = $('#' + cntr).val();
    var entered_len = $('#' + cntr).val().length;
    if (entered_no != "") 
    {
        if (entered_len < 10) {

            if (strLang == "E") {
                alert('Mobile Number should not be less than 10 digits');
                $('#' + cntr).focus();
                return false;
            }
            else {
                alert('मोबाइल नंबर कम से कम 10 अंक नहीं होना चाहिए');
                $('#' + cntr).focus();
                return false;
            }

            
        }
        else
            return true;
    }
    else
        return true;
}

function CompareDateRange(Controlname1, Controlname2, Fieldname1, Fieldname2, strLang) {
    var fromDate = $("input#" + Controlname1).val();
    var toDate = $("input#" + Controlname2).val();
    //alert(fromDate+'==='+toDate);
    if (toDate != "") {
        var dateParts = fromDate.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var fdate = new Date(newDateStr);
        // alert(fdate);
        var dateParts1 = toDate.split("-");
        var newDateStr1 = dateParts1[1] + " " + dateParts1[0] + ", " + dateParts1[2];
        var tdate = new Date(newDateStr1);
        // alert(tdate);
        if (fdate > tdate) {

            if (strLang == "E") {
                alert("Invalid Date Range!\n" + Fieldname2 + " can not be before " + Fieldname1);
                $(this).focus();
                return false;
            }
            else {
                alert("अमान्य तिथि सीमा!\n" + Fieldname2 + " इससे पहले नहीं किया जा सकता " + Fieldname1);
                $(this).focus();
                return false;
            }
          
        }
        return true;
    }
}

function CompareTwoDate(Controlname1, Controlname2, Fieldname1, Fieldname2, strLang) {
    var fromDate = $("input#" + Controlname1).val();
    var toDate = $("input#" + Controlname2).val();
    //alert(fromDate+'==='+toDate);
    if (toDate != "") {
        var dateParts = fromDate.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var fdate = new Date(newDateStr);
        // alert(fdate);
        var dateParts1 = toDate.split("-");
        var newDateStr1 = dateParts1[1] + " " + dateParts1[0] + ", " + dateParts1[2];
        var tdate = new Date(newDateStr1);
        // alert(tdate);
        if (fdate > tdate) {

            if (strLang == "E") {
                alert(Fieldname2 + " can not be before " + Fieldname1);
                $(this).focus();
                return false;
            }
            else {
                alert(Fieldname2 + " इससे पहले नहीं किया जा सकता " + Fieldname1);
                $(this).focus();
                return false;
            }
          
        }
        return true;
    }
}

function ValidateEmail(cntr, strLang) {
    var email = $('#' + cntr).val();
    //alert(email);
    if (email != "") {
        var reg = /^[A-Za-z0-9]([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/
        if (!reg.test(email)) {

            if (strLang == "E") {
                alert('Enter a valid email id');
                $('#' + cntr).focus();
                return false;
            }
            else {
                alert('एक मान्य ईमेल आईडी दर्ज करें');
                $('#' + cntr).focus();
                return false;
            }
           
        }
        else
            return true;
    }
    else
        return true;
};

function ValidateDropdown(cntr, strText, strLang) {
    var strValue = $('#' + cntr).val();
    if (strValue.length == 0 || strValue == "0") {

        if (strLang == "E") {
            alert("Please select the " + strText);
            $('#' + cntr).focus();
            return false;
        }
        else {
            alert("कृपया चयन करें " + strText);
            $('#' + cntr).focus();
            return false;
        }
       
    }
    else
        return true;
}

function BlankTextBox(cntr, strText,strLang) {
    var strValue = $('#' + cntr).val();
    if (strValue == "") {
        if (strLang == "E") {
            alert(strText + " Can Not Be Left Blank");
            $('#' + cntr).focus();
            return false;
        }
        else {
            alert(strText + " खाली नहीं छोड़ा जा सकता");
            $('#' + cntr).focus();
            return false;
        }
    }
    else
        return true;
}

function CheckZero(cntr, strText, strLang) {
    var strValue = $('#' + cntr).val();
    if (strValue == "0" || strValue == "0.0" || strValue == "0.00") {

        if (strLang == "E") {
            alert(strText + " can not be zero");
            $('#' + cntr).focus();
            return false;
        }
        else {
            alert(strText + " शून्य नहीं हो सकते");
            $('#' + cntr).focus();
            return false;
        }
       
    }
    else
        return true;
}

var tdate = new Date();
var dd = tdate.getDate(); //yields day
var MMM = month[tdate.getMonth()]; //yields month
var yyyy = tdate.getFullYear(); //yields year
var curDate = dd + "-" + MMM + "-" + yyyy;

function CheckGreaterDate(cntr, strText, strLang) {
    var myDate = $("input#" + cntr).val();
    // alert(myDate + '===' + curDate);
    if (curDate != "") {
        var dateParts = myDate.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var cDate = new Date(newDateStr);
        //alert(cDate);
        var dateParts1 = curDate.split("-");
        var newDateStr1 = dateParts1[1] + " " + dateParts1[0] + ", " + dateParts1[2];
        var tdate = new Date(newDateStr1);
        //alert(tdate);
        if (cDate > tdate) {

            if (strLang == "E") {
                alert(strText + " must be less than or equal to current date");
                $('#' + cntr).focus();
                return false;
            }
            else {
                alert(strText + " आज की तारीख से कम या बराबर होना चाहिए");
                $('#' + cntr).focus();
                return false;
            }
            
        }
        return true;
    }
}

function CheckLessDate(cntr, strText, strLang) {
    var myDate = $("input#" + cntr).val();
    var now = new Date();
    //alert(myDate + '===' + curDate);
    if (curDate != "") {
        var dateParts = myDate.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var cDate = new Date(newDateStr);
        //alert(cDate);
        var dateParts1 = curDate.split("-");
        var newDateStr1 = dateParts1[1] + " " + dateParts1[0] + ", " + dateParts1[2];
        var tdate = new Date(newDateStr1);
        //alert(tdate);
        if (cDate < tdate) {

            if (strLang == "E") {
                alert(strText + " must be greater than or equal to current date");
                $('#' + cntr).focus();
                return false;
            }
            else {
                alert(strText + " आज की तारीख से अधिक या बराबर होना चाहिए");
                $('#' + cntr).focus();
                return false;
            }
           
        }
        return true;
    }
}

function GetAge(cntr) {
 
    var myDate = $("input#" + cntr).val();
    if (curDate != "") {
        var dateParts = myDate.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var tdate = new Date(newDateStr);

        var dateParts1 = curDate.split("-");
        var newDateStr1 = dateParts1[1] + " " + dateParts1[0] + ", " + dateParts1[2];
        var cDate = new Date(newDateStr1);

     
        var age = Math.ceil((cDate - tdate) / (365.25 * 24 * 60 * 60 * 1000));
        return age;
    }
}


function CheckUncheckGrid() {
    var totChk = $('.RowCheck input[type="checkbox"]').length;
    var totChecked;
    $('[id$=chkSelectAll]').change(function () {
        if ($(this).is(':checked')) {
            $('.RowCheck input[type="checkbox"]').prop('checked', true);
        } else {
            $('.RowCheck input[type="checkbox"]').prop('checked', false);
        }
    });
    $('.RowCheck input[type="checkbox"]').change(function () {
        totChecked = $('.RowCheck input[type="checkbox"]:checked').length;
        if (totChecked == totChk) {
            $('[id$=chkSelectAll]').prop('checked', true);
        } else {
            $('[id$=chkSelectAll]').prop('checked', false);
        }
    });
}

function CheckTime(ctrlDate, cntrFromTime, cntrToTime, strLang) {
    var myDate = $("input#" + ctrlDate).val();
    var myFromTime = $("input#" + cntrFromTime).val();
    var myToTime = $("input#" + cntrToTime).val();
    //alert(myDate + '===' + curDate);
    if (myDate != "") {
        var dateParts = myDate.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var StartTime = new Date(newDateStr + ' ' + myFromTime);
        // alert(StartTime);       
        //        var dateParts1 = curDate.split("-");
        //        var newDateStr1 = dateParts1[1] + " " + dateParts1[0] + ", " + dateParts1[2];
        var EndTime = new Date(newDateStr + ' ' + myToTime);
        //alert(EndTime);
        var DiffTime = new Number(EndTime.getTime() - StartTime.getTime());
        if (DiffTime < 0) {

            if (strLang == "E") {
                alert('Out Time Can Not Be Earlier Than In Time');
                $('#txtOutTime').focus();
                return false;
            }
            else {
                alert('टाइम आउट में समय से पहले नहीं किया जा सकता');
                $('#txtOutTime').focus();
                return false;
            }
           
        }
        return true;
    }
}

function DateDifference(Controlname1, Controlname2, DType) {
    var fromDate = $("input#" + Controlname1).val();
    var toDate = $("input#" + Controlname2).val();
    //alert(fromDate+'==='+toDate);
    if (toDate != "") {
        var dateParts = fromDate.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var fdate = new Date(newDateStr);
        // alert(fdate);
        var dateParts1 = toDate.split("-");
        var newDateStr1 = dateParts1[1] + " " + dateParts1[0] + ", " + dateParts1[2];
        var tdate = new Date(newDateStr1);
        // alert(tdate);
        var diff_date = tdate - fdate; alert(diff_date);
        var num_years = diff_date / 31536000000;
        var num_months = (diff_date % 31536000000) / 2628000000;
        var num_days = ((diff_date % 31536000000) % 2628000000) / 86400000;

        if (DType == "D") {
            return Math.floor(num_days);
        }
        if (DType == "M") {
            return Math.floor(num_months);
        }
        if (DType == "Y") {
            return Math.floor(num_years);
        }
    }
}

//========================================================================== Added by Abhijit Ojha ===========
// For checking max length (controlId, CharacterLimit, SpanId)

function chkMaxLength(e, t, n,cntr) {
    var hv = $('#' + cntr).val();   
    try
     {
        if (document.getElementById(e) != null)
             {
                 e = document.getElementById(e) 
             }
        if (e != null) 
            {
                if (e.value[0] == " ") 
                {
                    e.value = e.value.substr(1, e.value.length); e.value = e.value.trim() 
                }
                if (e.value.length > t) 
                {
                    e.value = e.value.substring(0, t);

                    if (hv == "E") {
                        alert("Maximum " + t + " characters are allowed.");
                        e.focus()
                    }
                    else {
                        alert("अधिकतम " + t + " वर्णों की अनुमति है।");
                        e.focus()
                    }

                   
                }
            } 
        n = document.getElementById(n);
        if (n != null) {
            if (e.value.length == 0) {

                if (hv == "E") {
                    $(n).html("Maximum " + t + " characters are allowed.")
                }
                else {
                    $(n).html("अधिकतम " + t + " वर्णों की अनुमति है।")
                }
                
            }
            else {
                if (hv == "E") {
                    $(n).html(t - e.value.length + " characters are left.")
                }
                else {
                    $(n).html(t - e.value.length + " पात्रों छोड़ दिया जाता है।")
                }
                
            }
        }
    }
    catch (r) {
    }
        }
// To check decimal (controlId, DecimalPlaces)
        function CheckDecimal(e, t, strLang) {
            try {
                var n = ""; var r; if (parseInt(t)) { r = t } else { r = 2 } var i = document.getElementById(e); if (i == "undefined" || i == null) { i = e } if (typeof i.value === "undefined") { n = i.innerHTML.trim() } else { n = i.value.trim() } if (n.split(".").length - 1 > 1 || n.charAt(n.length - 1) == "." || n.charAt(0) == ".") {
                    if (typeof i.value === "undefined") {
                        setTimeout(function () {

                            if (strLang == "E") {
                                alert("Please enter valid decimal !");
                            }
                            else {
                                alert("वैध दशमलव दर्ज करें !");
                            }
                           
                            $("#" + i.getAttribute("id")).effect("shake", { direction: "left", times: 2, distance: 5 }, 800)
                        }, 1)
                    } else {
                        setTimeout(function () {
                            if (strLang == "E") {
                                alert("Please enter valid decimal !");
                                $(i).focus() 
                            }
                            else {
                                alert("वैध दशमलव दर्ज करें !");
                                $(i).focus() 
                            }

                        }, 1)
                    } return false
                } else {
                    if (n.substr(n.lastIndexOf(".") + 1, n.length).length > r && n.lastIndexOf(".") > -1) {
                        if (typeof i.value === "undefined") {
                            setTimeout(function () {
                                if (strLang == "E") {
                                    alert("Only " + r + " digits are allowed after decimal !");
                                }
                                else {
                                    alert("केवल " + r + " अंक दशमलव के बाद अनुमति दी जाती है !");
                                }


                                $("#" + i.getAttribute("id")).effect("shake", { direction: "left", times: 2, distance: 5 }, 800)
                            }, 1)
                        } else {
                            setTimeout(function () {
                                if (strLang == "E") {
                                    alert("Only " + r + " digits are allowed after decimal !"); $(i).focus() 
                                }
                                else {
                                    alert("केवल " + r + " अंक दशमलव के बाद अनुमति दी जाती है !"); $(i).focus() 
                                }
                                
  }, 1) } return false } else { return true } } } catch (s) { } }
// To make decimal (controlId, DecimalPlace)
function makeDecimal(e, t) { var n = document.getElementById(e); var r; if (parseInt(t)) { r = t } else { r = 2 } if (n == "undefined" || n == null) { n = e } if (typeof n.value === "undefined") { if (n.innerHTML.trim().length > 0) { n.innerHTML = parseFloat(n.innerHTML.trim()).toFixed(r) } } else { if (n.value.trim().length > 0) { n.value = parseFloat(n.value.trim()).toFixed(r) } } }
// Remove Initial space (controlId)
function RemoveInitialSpace(e) { var t = document.getElementById(e); if (t == "undefined" || t == null) { t = e } try { if (t.value[0] == " ") { t.value = t.value.substr(1, t.value.length); t.value = t.value.trim() } } catch (n) { } }
// Scroll to Page top
$.fn.scrollView = function () { return this.each(function () { $("html, body").animate({ scrollTop: $(this).offset().top - 20 }, 100) }) }
// Check selection of records before delete (GridViewId, CheckBoxCellNo)
function CheckBeforeDelete(e, t, cntr) {
    var hv = $('#' + cntr).val();
    try {
        var n = false; $("#" + e + " tr").find("td:nth-child(" + t + ")").each(function () {
            if ($(this).find("input:checkbox").prop("checked") === true) {
                n = true
            }
        });
        if (n) {
            if (hv == "E") {
                if (confirm(" Are you sure you want to Delete the Record(s) !")) { return true }
                else { return false } 
            }
            else {
                if (confirm(" क्या आप रिकॉर्ड को नष्ट करना चाहते हैं !")) { return true }
                else { return false } 
            }

          
        }
        else {
            setTimeout(function () {
                if (hv == "E") {
                    alert("Please select a Record to Delete !");
                }
                else {
                    alert("हटाने के लिए एक रिकॉर्ड का चयन करें !");
                }
       $("#" + e + " tr").each(function () { if (!$(this).find("td:eq(" + (parseInt(t) - 1) + ")").find("input:checkbox").prop("disabled")) { $(this).find("td:eq(" + (parseInt(t) - 1) + ")").effect("highlight", { color: "#d9534f" }, 1e3) } }) }, 1); return false } } catch (r) { alert(r) } }
// Put Water Mark for a TextBox / TextArea (controlId, defaultValue)
function WaterMark(a, r) { try { var t = $("#" + a); ("undefined" == t || null == t) && (t = a), t.attr("placeholder", r) } catch (e) { } }
//************************************************************************************************************

//***************************Added By Tapan*********************************  
//To Check Length of a string
function checkLength(cntr, chr, strLang) {
    maxLen = chr; // max number of characters allowed            
    var strValue = $('#' + cntr).val();
    //alert(strValue); alert(strValue.length);
    if (strValue.length > maxLen) {
        // Alert message if maximum limit is reached.
        var msg;
        if (strLang == "E") {
            msg = "You have reached your maximum limit of characters allowed";
        }
        else {
            msg = "आप वर्णों की अपनी अधिकतम सीमा तक पहुंच की अनुमति दी है";
        }
               
       
        alert(msg);
        // Reached the Maximum length so trim the textarea
        $('#' + cntr).val(strValue.substring(0, maxLen));
        $(".remaining").val(0);
    }
    else {
        // Maximum length not reached so update the value of my_text counter
        $(".remaining").val(maxLen - strValue.length);
    }
}

//==========================================================================


//Created by Sangram Das on 16-Dec-2014 to validate check list box control
function ValidateCheckListBox(chklist, msg, strLang) {
    try {
        var chkboxlist = $('#' + chklist + ' input:checked');
        if (chkboxlist.length > 0) {
            return true;
        }
        else {

            if (strLang == "E") {
                alert('Please select atleast one ' + msg);
                return false;
            }
            else {
                alert('कम से कम एक का चयन करें ' + msg);
                return false;
            }
          
        }
    } catch (e) {

    }
}
//End//

//Created by Sangram Das on 16-Dec-2014 for minimum length validation
function MinimumLengthValidation(textbox, length, strLang) {
    try {
        var textbox = $('#' + textbox);
        if (textbox.val().length < length) {

            if (strLang == "E") {
                alert("Please enter minimum " + length + " characters or numbers");
                textbox.focus();
                return false;
            }
            else {
                alert("न्यूनतम दर्ज करें " + length + " अक्षर या संख्या");
                textbox.focus();
                return false;
            }
            
        }
        else {
            return true;
        }

    } catch (e) {

    }
}
//End//

//================Created By Tapan on 29-dec-14 for merge table cell having equal value ===============

function groupTable($rows, startIndex, total) {
    if (total === 0) {
        return;
    }
    var i, currentIndex = startIndex, count = 1, lst = [];
    var tds = $rows.find('td:eq(' + currentIndex + ')');
    var ctrl = $(tds[0]);
    lst.push($rows[0]);
    for (i = 1; i <= tds.length; i++) {
        if (ctrl.text() == $(tds[i]).text()) {
            count++;
            $(tds[i]).addClass('deleted');
            lst.push($rows[i]);
        }
        else {
            if (count > 1) {
                ctrl.attr('rowspan', count);
                groupTable($(lst), startIndex + 1, total - 1)
            }
            count = 1;
            lst = [];
            ctrl = $(tds[i]);
            lst.push($rows[i]);
        }
    }
}
//=================End==================================
/********************************************************************!
* To prevent users from loosing unsaved form changes
* Created By : Manas Bej
* Created On : 29-DEC-2014
**********************************************************************/
(function ($) {

    $.fn.areYouSure = function (options) {

        var settings = $.extend(
      {
          'message': 'You have unsaved changes!',
          'dirtyClass': 'dirty',
          'change': null,
          'silent': false,
          'addRemoveFieldsMarksDirty': false,
          'fieldEvents': 'change keyup propertychange input',
          'fieldSelector': ":input:not(input[type=submit]):not(input[type=button])"
      }, options);

        var getValue = function ($field) {
            if ($field.hasClass('ays-ignore')
          || $field.hasClass('aysIgnore')
          || $field.attr('data-ays-ignore')
          || $field.attr('name') === undefined) {
                return null;
            }

            if ($field.is(':disabled')) {
                return 'ays-disabled';
            }

            var val;
            var type = $field.attr('type');
            if ($field.is('select')) {
                type = 'select';
            }

            switch (type) {
                case 'checkbox':
                case 'radio':
                    val = $field.is(':checked');
                    break;
                case 'select':
                    val = '';
                    $field.find('option').each(function (o) {
                        var $option = $(this);
                        if ($option.is(':selected')) {
                            val += $option.val();
                        }
                    });
                    break;
                default:
                    val = $field.val();
            }

            return val;
        };

        var storeOrigValue = function ($field) {
            $field.data('ays-orig', getValue($field));
        };

        var checkForm = function (evt) {

            var isFieldDirty = function ($field) {
                var origValue = $field.data('ays-orig');
                if (undefined === origValue) {
                    return false;
                }
                return (getValue($field) != origValue);
            };

            var $form = ($(this).is('form'))
                    ? $(this)
                    : $(this).parents('form');

            // Test on the target first as it's the most likely to be dirty
            if (isFieldDirty($(evt.target))) {
                setDirtyStatus($form, true);
                return;
            }

            $fields = $form.find(settings.fieldSelector);

            if (settings.addRemoveFieldsMarksDirty) {
                // Check if field count has changed
                var origCount = $form.data("ays-orig-field-count");
                if (origCount != $fields.length) {
                    setDirtyStatus($form, true);
                    return;
                }
            }

            // Brute force - check each field
            var isDirty = false;
            $fields.each(function () {
                $field = $(this);
                if (isFieldDirty($field)) {
                    isDirty = true;
                    return false; // break
                }
            });

            setDirtyStatus($form, isDirty);
        };

        var initForm = function ($form) {
            var fields = $form.find(settings.fieldSelector);
            $(fields).each(function () { storeOrigValue($(this)); });
            $(fields).unbind(settings.fieldEvents, checkForm);
            $(fields).bind(settings.fieldEvents, checkForm);
            $form.data("ays-orig-field-count", $(fields).length);
            setDirtyStatus($form, false);
        };

        var setDirtyStatus = function ($form, isDirty) {
            var changed = isDirty != $form.hasClass(settings.dirtyClass);
            $form.toggleClass(settings.dirtyClass, isDirty);

            // Fire change event if required
            if (changed) {
                if (settings.change) settings.change.call($form, $form);

                if (isDirty) $form.trigger('dirty.areYouSure', [$form]);
                if (!isDirty) $form.trigger('clean.areYouSure', [$form]);
                $form.trigger('change.areYouSure', [$form]);
            }
        };

        var rescan = function () {
            var $form = $(this);
            var fields = $form.find(settings.fieldSelector);
            $(fields).each(function () {
                var $field = $(this);
                if (!$field.data('ays-orig')) {
                    storeOrigValue($field);
                    $field.bind(settings.fieldEvents, checkForm);
                }
            });
            // Check for changes while we're here
            $form.trigger('checkform.areYouSure');
        };

        var reinitialize = function () {
            initForm($(this));
        }

        if (!settings.silent && !window.aysUnloadSet) {
            window.aysUnloadSet = true;
            $(window).bind('beforeunload', function () {
                $dirtyForms = $("form").filter('.' + settings.dirtyClass);
                if ($dirtyForms.length == 0) {
                    return;
                }
                // Prevent multiple prompts - seen on Chrome and IE
                if (navigator.userAgent.toLowerCase().match(/msie|chrome/)) {
                    if (window.aysHasPrompted) {
                        return;
                    }
                    window.aysHasPrompted = true;
                    window.setTimeout(function () { window.aysHasPrompted = false; }, 900);
                }
                return settings.message;
            });
        }

        return this.each(function (elem) {
            if (!$(this).is('form')) {
                return;
            }
            var $form = $(this);

            $form.submit(function () {
                $form.removeClass(settings.dirtyClass);
            });
            $form.bind('reset', function () { setDirtyStatus($form, false); });
            // Add a custom events
            $form.bind('rescan.areYouSure', rescan);
            $form.bind('reinitialize.areYouSure', reinitialize);
            $form.bind('checkform.areYouSure', checkForm);
            initForm($form);
        });
    };
})(jQuery);


//$(function () {
//    if (!navigator.userAgent.toLowerCase().match(/iphone|ipad|ipod|opera/)) {
//        return;
//    }
//    $('a').bind('click', function (evt) {        
//        var href = $(evt.target).closest('a').attr('href');
//        if (href !== undefined && !(href.match(/^#/) || href.trim() == '')) {
//            var response = $(window).triggerHandler('beforeunload', response);
//            if (response && response != "") {
//                var msg = response + "\n\n"
//          + "Press OK to leave this page or Cancel to stay.";
//                if (!confirm(msg)) {
//                    return false;
//                }
//            }
//            window.location.href = href;
//            return false;
//        }
//    });
//});

function ConfirmAction(cntr,strlang) {
    var strValue = $('#' + cntr).val();
    if (strValue == 'Update' || strValue == 'अपडेट')
     {
         if (strlang == "E") {
             if (!confirm('Are You Sure To Update?')) {
                 return false;
             }
             else {
                 return true;
             }
         }
         else {
             if (!confirm('आप यकीन है कि अद्यतन करना चाहते हैं?')) {
                 return false;
             }
             else {
                 return true;
             }
         }
    }
    else 
    {
        if (strlang == "E") {
            if (!confirm('Are You Sure To Save?')) {
                return false;
            }
            else {
                return true;
            }
        }
        else {
            if (!confirm('आप यकीन है कि प्रस्तुत करना चाहते हैं?')) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}
function ConfirmDelete(lang) {
    var msg = "Are you sure you want to delete the record?";
    if (lang == 'H')
        msg = "क्या आप रिकॉर्ड को नष्ट करना चाहते हैं !";
    if (confirm(msg)) {
        return true;
    }
    else
        return false;
}

// == Show confirmation for delete/restore/relieve etc. ========================
function ShowConfirmation(lang, msg) {
    var message = "";
    if (lang == 'E')
        message = "Are you sure you want to " + msg + " the record?";
    else if (lang == 'H')
        message = "क्या आप रिकॉर्ड को " + msg + " करना चाहते हैं !";
    
    if (confirm(message)) {
        return true;
    }
    else
        return false;
}
//=============================== Created By Bindeswari on 04-Feb-2015 ===============================================================================
function ConfirmCheck(gridId, checkboxName, strLang) {
    var TargetChildControl = checkboxName;
    var Inputs;
    Inputs = gridId.getElementsByTagName("input");
    for (var n = 0; n < Inputs.length; ++n)
        if (Inputs[n].type == 'checkbox' &&
            Inputs[n].id.indexOf(TargetChildControl, 0) >= 0 &&
            Inputs[n].checked)
            return true;
    if (strLang == "E") {
        alert('Select at least one Record!');
        return false;
    }
    else {
        alert('कम से कम एक रिकॉर्ड का चयन करें!');
        return false;
    }
    
}
//============================================== End ====================================================================================================

function IsWhiteSpace1stPalce(cntr, strLang) {
    var strValue = $('#' + cntr).val();
    // alert(strValue);
    if (strValue != "") {
        var FistChar = strValue.charAt(0);
        if (FistChar == " ") {

            if (strLang == "E") {
                alert('White space are not allowed at 1st place !!!');
                $('#' + cntr).focus();
                return false;
            }
            else {
                alert('व्हाइट अंतरिक्ष 1 जगह पर अनुमति नहीं है !!!');
                $('#' + cntr).focus();
                return false;
            }

        } else { return true; }
        return true;
    }
    else
        return true;
}
//++++++++++++++++++++++++++++++++
// Check selection of records before delete (GridViewId, CheckBoxCellNo,LanguageType)
//
function CheckSelectionBeforeDelete(e, t, hv) {
    //var hv = $('#' + cntr).val();
    try {
        var n = false; $("#" + e + " tr").find("td:nth-child(" + t + ")").each(function () {
            if ($(this).find("input:checkbox").prop("checked") === true) {
                n = true
            }
        });
        if (n) {
            if (hv == "E") {
                if (confirm(" Are you sure you want to Delete the Record(s) !")) { return true }
                else { return false }
            }
            else {
                if (confirm(" क्या आप रिकॉर्ड को नष्ट करना चाहते हैं !")) { return true }
                else { return false }
            }


        }
        else {
            setTimeout(function () {
                if (hv == "E") {
                    alert("Please select a Record to Delete !");
                }
                else {
                    alert("हटाने के लिए एक रिकॉर्ड का चयन करें !");
                }
                $("#" + e + " tr").each(function () { if (!$(this).find("td:eq(" + (parseInt(t) - 1) + ")").find("input:checkbox").prop("disabled")) { $(this).find("td:eq(" + (parseInt(t) - 1) + ")").effect("highlight", { color: "#d9534f" }, 1e3) } })
            }, 1); return false
        }
    } catch (r) { alert(r) }
}

//++++++++++++++++++++++++++++++++
// Check selection of records before approve (GridViewId, CheckBoxCellNo,LanguageType)
//
function CheckSelectionBeforeApprove(e, t, hv) {
    //var hv = $('#' + cntr).val();
    try {
        var n = false; $("#" + e + " tr").find("td:nth-child(" + t + ")").each(function () {
            if ($(this).find("input:checkbox").prop("checked") === true) {
                n = true
            }
        });
        if (n) {
            if (hv == "E") {
                if (confirm(" Are you sure you want to approve the record(s) !")) { return true }
                else { return false }
            }
            else {
                if (confirm(" क्या आप रिकॉर्ड को स्वीकृत करना चाहते हैं !")) { return true }
                else { return false }
            }


        }
        else {
            setTimeout(function () {
                if (hv == "E") {
                    alert("Please select a record to approve !");
                }
                else {
                    alert("स्वीकृत करने के लिए एक रिकॉर्ड का चयन करें !");
                }
                $("#" + e + " tr").each(function () { if (!$(this).find("td:eq(" + (parseInt(t) - 1) + ")").find("input:checkbox").prop("disabled")) { $(this).find("td:eq(" + (parseInt(t) - 1) + ")").effect("highlight", { color: "#d9534f" }, 1e3) } })
            }, 1); return false
        }
    } catch (r) { alert(r) }
}

    // Check selection of records before Submit (GridViewId, CheckBoxCellNo,LanguageType)
// 
function CheckSelectionBeforeSubmit(e, t, hv, cntr) {
    var strValue = $('#' + cntr).val();
    if (strValue == 'Update' || strValue == 'अपडेट') {
        try {
            var n = false; $("#" + e + " tr").find("td:nth-child(" + t + ")").each(function () {
                if ($(this).find("input:checkbox").prop("checked") === true) {
                    n = true
                }
            });
            if (n) {
                if (hv == "E") {
                    if (confirm(" Are You Sure To Update?")) { return true }
                    else { return false }
                }
                else {
                    if (confirm("क्या आप रिकॉर्ड को अद्यतन करने के लिए चाहते हैं!")) { return true }
                    else { return false }
                }


            }
            else {
                setTimeout(function () {
                    if (hv == "E") {
                        alert("Please select a Record to Update !");
                    }
                    else {
                        alert("अद्यतन करने के लिए एक रिकार्ड का चयन करें !");
                    }
                    $("#" + e + " tr").each(function () { if (!$(this).find("td:eq(" + (parseInt(t) - 1) + ")").find("input:checkbox").prop("disabled")) { $(this).find("td:eq(" + (parseInt(t) - 1) + ")").effect("highlight", { color: "#d9534f" }, 1e3) } })
                }, 1); return false
            }
        } catch (r) { alert(r) }
    }
    else {
        try {
            var n = false; $("#" + e + " tr").find("td:nth-child(" + t + ")").each(function () {
                if ($(this).find("input:checkbox").prop("checked") === true) {
                    n = true
                }
            });
            if (n) {
                if (hv == "E") {
                    if (confirm(" Are You Sure To Save?")) { return true }
                    else { return false }
                }
                else {
                    if (confirm(" क्या आप रिकॉर्ड को जमा करना चाहते हैं !")) { return true }
                    else { return false }
                }


            }
            else {
                setTimeout(function () {
                    if (hv == "E") {
                        alert("Please select a Record to Submit !");
                    }
                    else {
                        alert("सबमिट करने के लिए एक रिकॉर्ड का चयन करें !");
                    }
                    $("#" + e + " tr").each(function () { if (!$(this).find("td:eq(" + (parseInt(t) - 1) + ")").find("input:checkbox").prop("disabled")) { $(this).find("td:eq(" + (parseInt(t) - 1) + ")").effect("highlight", { color: "#d9534f" }, 1e3) } })
                }, 1); return false
            }
        } catch (r) { alert(r) }
    }
}
//Check max length of Text Area where e-Text Box Id,t-Maximum No of Character,n-Span Id,hv-E/H Language Type
function chkLangTypeMaxLength(e, t, n, hv) {
    try {
        if (document.getElementById(e) != null) {
            e = document.getElementById(e)
        }
        if (e != null) {
            if (e.value[0] == " ") {
                e.value = e.value.substr(1, e.value.length); e.value = e.value.trim()
            }
            if (e.value.length > t) {
                e.value = e.value.substring(0, t);

                if (hv == "E") {
                    alert("Maximum " + t + " characters are allowed.");
                    e.focus()
                }
                else {
                    alert("अधिकतम " + t + " वर्णों की अनुमति है।");
                    e.focus()
                }


            }
        }
        n = document.getElementById(n);
        if (n != null) {
            if (e.value.length == 0) {

                if (hv == "E") {
                    $(n).html("Maximum " + t + " characters are allowed.")
                }
                else {
                    $(n).html("अधिकतम " + t + " वर्णों की अनुमति है।")
                }

            }
            else {
                if (hv == "E") {
                    $(n).html(t - e.value.length + " characters are left.")
                }
                else {
                    $(n).html(t - e.value.length + " पात्रों छोड़ दिया जाता है।")
                }

            }
        }
    }
    catch (r) {
    }
}
//Added by Gayatri Prasad Das on 20-Aug-2015 to check the selected date is current month data or not.
function CheckCurrentMonthYear(cntr,strLang) {
    var myDate =$.trim($("input#" + cntr).val());
    if (myDate != "") {
        var currentYear = (new Date).getFullYear();
        var currentMonth = (new Date).getMonth() + 1;
        var dateParts = myDate.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var myDateYear = (new Date(newDateStr)).getFullYear();
        var myDateMonth = (new Date(newDateStr)).getMonth() + 1;
        if ((parseInt(myDateYear, 10) != parseInt(currentYear, 10)) || (parseInt(myDateMonth, 10) != parseInt(currentMonth, 10))) {

            if (strLang == "E") {
                alert("Previous month data can not be added.");
                $('#' + cntr).focus();
                return false;
            }
            else {
                alert("पिछले महीने के डेटा नहीं जोड़ा जा सकता.");
                $('#' + cntr).focus();
                return false;
            }

        }
        return true;
    }
}
// Check selection of records before Submit (GridViewId, CheckBoxCellNo,LanguageType,Button Id)
function CheckSelectionBeforeSubmitNoConfirmation(e, t, hv, cntr) {
    var strValue = $('#' + cntr).val();
    if (strValue == 'Update' || strValue == 'अपडेट') {
        try {
            var n = false; $("#" + e + " tr").find("td:nth-child(" + t + ")").each(function () {
                if ($(this).find("input:checkbox").prop("checked") === true) {
                    n = true
                }
            });
            if (n) {
                return true
            }
            else {
                setTimeout(function () {
                    if (hv == "E") {
                        alert("Please select a Record to Update !");
                    }
                    else {
                        alert("अद्यतन करने के लिए एक रिकार्ड का चयन करें !");
                    }
                    $("#" + e + " tr").each(function () { if (!$(this).find("td:eq(" + (parseInt(t) - 1) + ")").find("input:checkbox").prop("disabled")) { $(this).find("td:eq(" + (parseInt(t) - 1) + ")").effect("highlight", { color: "#d9534f" }, 1e3) } })
                }, 1); return false
            }
        } catch (r) { alert(r) }
    }
    else {
        try {
            var n = false; $("#" + e + " tr").find("td:nth-child(" + t + ")").each(function () {
                if ($(this).find("input:checkbox").prop("checked") === true) {
                    n = true
                }
            });
            if (n) {
                return true
            }
            else {
                setTimeout(function () {
                    if (hv == "E") {
                        alert("Please select a Record to Submit !");
                    }
                    else {
                        alert("सबमिट करने के लिए एक रिकॉर्ड का चयन करें !");
                    }
                    $("#" + e + " tr").each(function () { if (!$(this).find("td:eq(" + (parseInt(t) - 1) + ")").find("input:checkbox").prop("disabled")) { $(this).find("td:eq(" + (parseInt(t) - 1) + ")").effect("highlight", { color: "#d9534f" }, 1e3) } })
                }, 1); return false
            }
        } catch (r) { alert(r) }
    }
}

