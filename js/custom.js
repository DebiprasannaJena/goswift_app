$(function () {

 $('.toggle_btn button').click(function () {
        $('.toggle_btn button').removeClass('active');
        $(this).addClass('active');
    });
});

function openHTMLStrModal(header, body, footer) {
    $('#pageModal .modal-header #myModalLabel').html(header);
    $('#pageModal .modal-body').html(body);
    $('#pageModal .modal-footer').html(footer);
    $('#pageModal').modal();
}
function openPageModal(header, page, footer, frm_hit) {
    $('#pageModal .modal-header #myModalLabel').html(header);
    $('#pageModal .modal-body').html("<iframe width='100%' height='" + frm_hit + "px' src='" + page + "' frameborder='0'></iframe>");
    if (footer) {
        $('#pageModal .modal-footer').html(footer);
    } else {$('#pageModal .modal-footer').remove(); }
    $('#pageModal').modal();
}
function hidePageModal(opt) {
    $('#pageModal').modal('hide');
    $('#pageModal .modal-header #myModalLabel').html("");
    $('#pageModal .modal-body').html("");
    $('#pageModal .modal-footer').html("");
    if (opt.reload == true) {
        location.reload();
    }
}

var specialKeys = new Array();
specialKeys.push(8); //Backspace
specialKeys.push(9); //Tab
specialKeys.push(46); //Delete
specialKeys.push(36); //Home
specialKeys.push(35); //End
specialKeys.push(37); //Left
specialKeys.push(39); //Right
function IsAlphaNumeric(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = (((keyCode >= 48 && keyCode <= 57) || (keyCode == 32) || (keyCode == 46) || (keyCode == 44)) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    //document.getElementById("error").style.display = ret ? "none" : "inline";
    return ret;
}

function OnlyAlphaNumeric(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = (((keyCode >= 48 && keyCode <= 57)) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
   // document.getElementById("sampleError").style.display = ret ? "none" : "inline";
    return ret;
}

function IsSpecialCharacter1stPalce(cntr) {
    // alert(cntr);
    var strValue = $('#' + cntr).val();
    // alert(strValue);
    var FistChar = strValue.charAt(0);
    if (/^[a-zA-Z0-9]*$/.test(FistChar) == false) {
        alert('Special characters and white space are not allowed at 1st place !!!');
        $('#' + cntr).focus();
        return false;
    } else { return true; }
    return true;
}

function OnlyNumeric(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = (((keyCode >= 48 && keyCode <= 57)) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    //document.getElementById("numbError").style.display = ret ? "none" : "inline";
    return ret;
}

function BlankTextBox(cntr, strText) {
    var strValue = $('#' + cntr).val();
    if (strValue == "") {
        alert(strText + " can not be left blank");
        $('#' + cntr).focus();
        return false;
    }
    else
        return true;
}

function ValidateDropdown(cntr, strText) {
    var strValue = $('#' + cntr).val();
    if (strValue.length == 0 || strValue == "0") {
        alert("Please Select " + strText);
        $('#' + cntr).focus();
        return false;
    }
    else
        return true;
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
var tdate = new Date();
var dd = tdate.getDate(); //yields day
var MMM = month[tdate.getMonth()]; //yields month
var yyyy = tdate.getFullYear(); //yields year
var curDate = dd + "-" + MMM + "-" + yyyy;

//========================Added by Tapan Kumar Mishra ==================================


function CompareDateRange(Controlname1, Controlname2, Fieldname1, Fieldname2) {    
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
            alert("Invalid Date Range!\n" + Fieldname2 + " can not be before " + Fieldname1);
            $(this).focus();
            return false;
        }
        return true;
    }
}

function CheckGreaterDate(cntr, strText) {
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
            alert(strText + " must be less than or equal to current date");
            $('#' + cntr).focus();
            return false;
        }
        return true;
    }
}

function CheckLessDate(cntr, strText) {
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
            alert(strText + " must be greater than or equal to current date");
            $('#' + cntr).focus();
            return false;
        }
        return true;
    }
}

function ValidateCaptcha(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = (((keyCode >= 48 && keyCode <= 57)) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    //document.getElementById("captchaError").style.display = ret ? "none" : "inline";
    return ret;
}

function OnlyAlphabets(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = (((keyCode >= 65 && keyCode <= 90) || (keyCode == 32)) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    //            document.getElementById("alphaError").style.display = ret ? "none" : "inline";
    return ret;
};
function CheckMobile(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = (((keyCode >= 48 && keyCode <= 57)) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    //            document.getElementById("mobError").style.display = ret ? "none" : "inline";
    return ret;
};

function CheckAdharId(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = (((keyCode >= 48 && keyCode <= 57)) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    //            document.getElementById("mobError").style.display = ret ? "none" : "inline";
    return ret;
};

function validateEmail(email) {
    var reg = /^[A-Za-z0-9]([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/
    if (reg.test(email)) {
        return true;
    }
    else {
        return false;
    }
};

function ValidateAddress(e) {
    var keyCode = e.keyCode == 0 ? e.charCode : e.keyCode;
    var ret = (((keyCode >= 48 && keyCode <= 57) || (keyCode == 32) || (keyCode == 46) || (keyCode == 44) || (keyCode == 47)) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || (specialKeys.indexOf(e.keyCode) != -1 && e.charCode != e.keyCode));
    //document.getElementById("error").style.display = ret ? "none" : "inline";
    return ret;
}

function blink() {
    $('.blink').toggleClass('on');
}

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

function ValidateFile(cntr, strText) {
    //debugger;
    var strValue = $('#' + cntr).get(0).files.length;
    if (strValue == "0") {
        alert("Please Upload " + strText);
        $('#' + cntr).focus();
        return false;
    }
    else
        return true;
}

function CheckFileType(cntr, ftype) {

    // Get the file upload control file extension
    var extn = $('#' + cntr).val().split('.').pop().toLowerCase();
    if (extn != '') {
        //debugger;        
        // Create array with the files extensions to upload
        var fileListToUpload;
        if (parseInt(ftype) == 1)
            fileListToUpload = new Array('pdf', 'png', 'jpg', 'jpeg');
        else if (parseInt(ftype) == 2)
            fileListToUpload = new Array('png', 'jpg', 'jpeg');
        else
            fileListToUpload = new Array('pdf');

        //Check the file extension is in the array.               
        var isValidFile = $.inArray(extn, fileListToUpload);

        // isValidFile gets the value -1 if the file extension is not in the list.  
        if (isValidFile == -1) {
            if (parseInt(ftype) == 1)
                alert('Please select a valid file of type pdf/png/jpg/jpeg.');
            else if (parseInt(ftype) == 2)
                alert('Please select a valid file of type png/jpg/jpeg.');
            else
                alert('Please select a valid pdf file only');
            $('#' + cntr).replaceWith($('#' + cntr).val('').clone(true));
        }
        else {
            // Restrict the file size to 2 MB.
            if ($('#' + cntr).get(0).files[0].size > (1024 * 1024 * 2)) {
                alert('File size should not exceed 2MB.');
                $('#' + cntr).replaceWith($('#' + cntr).val('').clone(true));
            }
            else
                return true;
        }
    }
    else
        return true;
}

function CheckFileLength(cntr, flength) {
    debugger;
    if (parseInt(flength) == 1) {
        if ($('#' + cntr).get(0).files[0].size > (4000000)) {
            BootstrapAlert('File size should not exceed 4 MB.', cntr);
            $('#' + cntr).replaceWith($('#' + cntr).val('').clone(true));
        }
        else {
            return true;
        }
    }
    else if (parseInt(flength) == 2) {
        if ($('#' + cntr).get(0).files[0].size > (10000000)) {
            BootstrapAlert('File size should not exceed 10 MB.', cntr);
            $('#' + cntr).replaceWith($('#' + cntr).val('').clone(true));
        }
        else {
            return true;
        }
    }
    else
        return true;
}

function DecimalNumber(cntr, strText) {
    var regexPattern = /^\d{0,18}(\.\d{1,3})?$/;
    var entered_value = $('#' + cntr).val();
    if (!regexPattern.test(entered_value)) {
        alert('Enter a valid ' + strText);
        $('#' + cntr).focus();
        return false;
    }
    else
        return true;
}

function MobileNumber(cntr) {
    var Mobile = /^[7-9][0-9]{9}$/
    var entered_no = $('#' + cntr).val();
    if (!Mobile.test(entered_no)) {
        alert('Enter a valid Mobile Number');
        $('#' + cntr).focus();
        return false;
    }
    else
        return true;
}


function CompareTwoDate(Controlname1, Controlname2, Fieldname1, Fieldname2) {
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
            alert(Fieldname2 + " can not be before " + Fieldname1);
            $("input#" + Controlname2).focus();
            return false;
        }
        return true;
    }
}

function CheckTime(ctrlDate, ctrlToDate, cntrFromTime, cntrToTime) {
    var myDate = $("input#" + ctrlDate).val();
    var toDate = $("input#" + ctrlToDate).val();
    var myFromTime = $("input#" + cntrFromTime).val();
    var myToTime = $("input#" + cntrToTime).val();
    //alert(myDate + '===' + curDate);
    if (myDate != "") {
        var dateParts = myDate.split("-");
        var newDateStr = dateParts[1] + " " + dateParts[0] + ", " + dateParts[2];
        var StartTime = new Date(newDateStr + ' ' + myFromTime);
        // alert(StartTime);       
        var dateParts1 = toDate.split("-");
        var newDateStr1 = dateParts1[1] + " " + dateParts1[0] + ", " + dateParts1[2];
        var EndTime = new Date(newDateStr1 + ' ' + myToTime);
        //alert(EndTime);
        var DiffTime = new Number(EndTime.getTime() - StartTime.getTime());
        if (DiffTime < 0) {
            alert('End Time Can Not Be Earlier Than Start Time');
            $("input#" + cntrToTime).focus();
            return false;
        }
        return true;
    }
}


function ValidateEmail(cntr) {
    var email = $('#' + cntr).val();
    //alert(email);
    if (email != "") {
        var reg = /^[A-Za-z0-9]([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/
        if (!reg.test(email)) {
            alert('Enter a valid email id');
            $('#' + cntr).focus();
            return false;
        }
        else
            return true;
    }
    else
        return true;
};

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

function ConfirmAction(cntr, msgSave, msgUpdate) {
    var strValue = $('#' + cntr).val();
    if (strValue == 'Update') {
        if (confirm(msgUpdate)) {
            return true;
        }
        else
            return false;
    }
    else {
        if (confirm(msgSave)) {
            return true;
        }
        else
            return false;
    }
}

function ConfirmDelete(msg) {
    if (confirm(msg)) {
        return true;
    }
    else
        return false;
}

//=================End==================================