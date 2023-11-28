///*------------------------------------------------------------------------------------------------------------------------*/
//////// NUMERIC VALIDATION

function numeralsOnly(evt, tt) {
    evt = (evt) ? evt : event;
    var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        //alert("Enter Numerals only in this Field!");
        //tt.value="";              
        return false;
    }
    return true;
}

/*------------------------------------------------------------------------------------------------------------------------*/
///// FLOAT VALIDATION

function FloatOnly(evt, control) {
    evt = (evt) ? evt : event;
    var charCode = (evt.charCode) ? evt.charCode : ((evt.keyCode) ? evt.keyCode : ((evt.which) ? evt.which : 0));
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46) {
        //alert("Enter Numerals only in this Field!");             
        return false;
    }
    if (charCode == 46) {
        var patt1 = new RegExp("\\.");
        var ch = patt1.exec(control.value);
        if (ch == ".") {
            //alert("More then One Decimal Point not Allowed");
            return false;
        }
    }
    return true;
}

/*------------------------------------------------------------------------------------------------------------------------*/

/*-----------------------------------------Text area length while typing------------------------------------------------*/
function CheckLengthKeyUp(cntr, labelId, chr) {
    maxLen = chr; // max number of characters allowed            
    var strValue = $('#' + cntr).val();

    //alert(strValue); alert(strValue.length);
    if (strValue.length > maxLen) {

        // Reached the Maximum length so trim the textarea
        $('#' + cntr).val(strValue.substring(0, maxLen));
        $("#" + labelId).val(0);
    }
    else {
        // Maximum length not reached so update the value of my_text counter
        $("#" + labelId).text(maxLen - strValue.length);
    }
}
/*------------------------------------------------------------------------------------------------------------------------*/

/*-----------------------------------------Text area length while typing------------------------------------------------*/
function checkLength(cntr, labelId, chr) {
    maxLen = chr; // max number of characters allowed            
    var strValue = $('#' + cntr).val();

    //alert(strValue); alert(strValue.length);
    if (strValue.length > maxLen) {
        // Reached the Maximum length so trim the textarea
        $('#' + cntr).val(strValue.substring(0, maxLen));
        $("#" + labelId).val(0);

        // Alert message if maximum limit is reached.
        alert("You have reached your maximum limit of characters allowed");
    }
    else {
        // Maximum length not reached so update the value of my_text counter
        $("#" + labelId).text(maxLen - strValue.length);
    }
}

/*
Function to check if the string contains both alphabets and numbers
*/
function CheckAlphaNumeric(controlId, controlType, ntitle) {
    var regAlphaNumberic = /^(?=.*[a-zA-Z])(?=.*[0-9])/;
    var controlValue = $("#" + controlId).val().trim();
    if (!controlValue.match(regAlphaNumberic)) {
        jAlert('<strong>' + controlType + ' should be in alpha-numeric format!</strong>', ntitle);
        $("#popup_ok").click(function () { $("#" + controlId).focus(); });
        return false;
    }
    else {
        return true;
    }
}

function CheckGSTINFormat(controlId, controlType, ntitle) {
    var reggst = /^([0-9]){2}([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}([0-9]){1}([a-zA-Z]){1}([0-9]){1}?$/;
    var controlValue = $("#" + controlId);
    if (!reggst.test(controlValue)) {
        jAlert('<strong>GSTIN is not valid. It should be in this "11AAAAA1111Z1A1" format</strong>', 'GO-SWIFT');
        $("#popup_ok").click(function () { $("#" + controlId).focus(); });
        return false;
    }
    else {
        return true;
    }
}

function HasFile(fuControlId, strError) {
    if ($('#' + fuControlId).val() == "") {
        jAlert(strError, 'GO-SWIFT');
        return false;
    }
    else {
        return true;
    }
}

function FileCheck(e, arrExtension, strType, size) {
    debugger;
    var ids = e.id;
    var fileExtension = arrExtension.split(',');
    var thisval = $("#" + ids).val();
    if (thisval != null && thisval != undefined && thisval != '') {
        if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
            jAlert('<strong>Only ' + strType + ' formats are allowed.</strong>', 'GO-SWIFT');
            $("#" + ids).val(null);
            return false;
        }
        else {
            var fullSize = parseInt(size * 1024 * 1024);
            if ((e.files[0].size > fullSize) && ($("#" + ids).val() != '')) {
                jAlert('<strong>File size must be less then ' + size + 'mb!</strong>', 'GO-SWIFT');
                $("#" + ids).val('');
                e.preventDefault();
                return false;
            }
        }
    }
}

function FileCheckGrid(objFileUpload, objLinkButton, arrExtension, strType, size) {
    var ids = objFileUpload.id;
    var lnkid = objLinkButton.id;
    var fileExtension = arrExtension.split(',');

    //if document is uploaded
    var thisval = $("#" + ids).val();
    if (thisval != null && thisval != undefined && thisval != '') {
        if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
            jAlert('<strong>Only ' + strType + ' formats are allowed.</strong>', 'GO-SWIFT');
            $("#" + ids).val(null);
            $('#' + lnkid).text('');
            return false;
        }
        else {
            var fullSize = parseInt(size * 1024 * 1024);
            if ((objFileUpload.files[0].size > fullSize) && ($("#" + ids).val() != '')) {
                jAlert('<strong>File size must be less then ' + size + 'mb!</strong>', 'GO-SWIFT');
                $("#" + ids).val('');
                $('#' + lnkid).text('');
                e.preventDefault();
                return false;
            }
            else {
                $('#' + lnkid).text($("#" + ids).val().split('\\').pop());
            }
        }
    }
}


function readURL(input) {
    if (input.files && input.files[0]) {//Check if input has files.

        var ids = input.id;
        var fileExtension = ['png', 'jpg', 'jpeg'];
        if ($.inArray($("#" + ids).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
            jAlert('Only png / jpg / jpeg files are allowed.', 'GO-SWIFT');
            $("#" + ids).val(null);
            return false;
        }
        else {
            if ((input.files[0].size > parseInt(4 * 1024 * 1024)) && ($("#" + ids).val() != '')) {
                jAlert('File must be less then 4MB!', 'GO-SWIFT');
                $("#" + ids).val(null);
                //e.preventDefault();
                return false;
            }
            else {
                var reader = new FileReader(); //Initialize FileReader.

                reader.onload = function (e) {
                    $('#PreviewImage').attr('src', e.target.result);
                    $('#PreviewImage').attr('style', 'display:block');
                };
                reader.readAsDataURL(input.files[0]);
            }
        }

    }
}