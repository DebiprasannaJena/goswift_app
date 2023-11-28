
//*****************Global Method******************
function ValidateEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};
//*****************End Global*********************

//*************For TextBox Validation*************
//Blank Field Validation

function blankFieldValidation(ControlName, MessageControlName, Title,OkBtn,cancel1) {
    if ($('#' + ControlName).val() == '') {
        $('#' + ControlName).show();
        $('#' + ControlName).focus();
        //popup_ok

        jAlert('<strong>' + MessageControlName + ' </strong>', Title, OkBtn, cancel1);

        $("#popup_ok").click(function () {
            $('#' + ControlName).focus();
        });

        return false;
    }
    else {
        return true;
    }
}

//function blankFieldValidation(ControlName, MessageControlName, Title) {
//    if ($('#' + ControlName).val() == '') {


//        jAlert('<strong>' + MessageControlName + ' can not blank !</strong>', Title);

//        $("#popup_ok").click(
//      function () {
//          $('#' + ControlName).focus();
//          // Do something after the OK button is clicked...
//      });

//        return false;

//    }
//    else {
//        return true;
//    }
//}


function WhiteSpaceValidation1st(ControlName, MessageControlName, Title) {
    if ($('#' + ControlName).val().charAt(0) == ' ') {
        $('#' + ControlName).focus();


        jAlert('<strong>' + MessageControlName + ' !</strong>', Title);
        $("#popup_ok").click(function () {
            $('#' + ControlName).focus();
        });

        return false;
    }
    else {
        return true;
    }
}

function WhiteSpaceValidationLast(ControlName, MessageControlName, Title) {
    var strVal = $('#' + ControlName).val();
    // alert(strVal.length);
    //  return false;
    if (strVal.substr(strVal.length - 1) == ' ') {
        $('#' + ControlName).focus();

        jAlert('<strong>' + MessageControlName + ' !</strong>', Title);
        $("#popup_ok").click(function () {
            $('#' + ControlName).focus();
        });

        return false;
    }
    else {
        return true;
    }
}



//*********End Textbox Validation************************
function SpecialCharacter1st(ControlName, MessageControlName, Title) {
    if ($('#' + ControlName).val().charAt(0) == "'") {
        //jAlert('<strong> ' + MessageControlName + ' not allowed! </strong>', Title);
        jAlert('<strong> ' + MessageControlName + '! </strong>', Title);
        $("#popup_ok").click(function () {
            $('#' + ControlName).focus();
        });

        return false;
    }
    else {
        return true;
    }
}

//*********For EmailId Validation******************************
function EmailValidation(Controlname, MessageControlName, Title) {

    var strVal = $('#' + Controlname).val();
    if (strVal != '') {

        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        if (!expr.test(strVal)) {
            jAlert('<strong> ' + MessageControlName + ' !</strong>', Title);

            $("#popup_ok").click(function () {
                $('#' + ControlName).focus();
            });

            return false;
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }
}
//**************End EmailId Validation*************************


//*************For Dropdownlist Validation*************
//Blank Field Validation

function DropDownValidation(ControlName, ValueForValidate, MessageControlName, Title,  OkBtn, cancel1) {



    if ($('#' + ControlName).val() == ValueForValidate || $('#' + ControlName).val() == '') {
        jAlert('<strong> ' + MessageControlName + '  </strong>', Title, OkBtn, cancel1);

        $("#popup_ok").click(function () {
            //  $('#' + 'ddlComplaintype_chosen').focus();
            //  $('#' + ControlName).attr('selected', '0');        
            $('#' + ControlName).removeClass('chosen-container-active');
            //$('#' + ControlName).find('option:first').attr('selected', 'selected');
            // $('#chosen-select').trigger('liszt:activate');
            $('#' + ControlName + '_chosen .chosen-single').focus();
            $('#' + ControlName).focus();
            // $('select[name^="' + ControlName + '"]').eq(1).focus();
        });

        return false;
    }
    else {
        return true;
    }
}
function checkInput(ControlName, MessageControlName, Title) {

    var obj = $('#' + ControlName).val();
    if (obj != '') {
        if (obj.length < 10) {
            jAlert('<strong>' + MessageControlName + ' should be atleast 10 characters long!</strong>', Title);

            $("#popup_ok").click(function () {
                $('#' + ControlName).focus();
            });

            return false;
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }

}

//function SucessMessage(Messages, isNavigation, URL) {

//    jAlert('<strong>' + Messages + '</strong>','dfdfd');
//    if (isNavigation == 'Y') {
//        window.location.href = URL;
//    }
//}
////*********End Dropdownlist Validation************************



//*********File Upload Control Validation***********************

function ValidBlankFile(ControlName, Title, MessageName) {

    file = $('#' + ControlName).val();
    if (file == '') {
        jAlert('<strong>' + MessageControlName + ' </strong>', Title);

        $("#popup_ok").click(function () {
            $('#' + ControlName).focus();
        });

        return false;
    }

}
//Extention Should be pass with comma separated like "jpeg,jpg,gif,bmp,png"
function ValidFileExtention(ControlName, fileExtensions, Title) {
    var fileExtension = fileExtensions.split(','); // ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
    file = $('#' + ControlName).val();
    var extension = file.substr((file.lastIndexOf('.') + 1));

    if ($.inArray(extension, fileExtension) == -1) {
        jAlert('<strong>' + fileExtension.join(', ') + '!</strong>', Title);

        $("#popup_ok").click(function () {
            $('#' + ControlName).focus();
        });

        return false;
    }

}

function ValidFileSize(ControlName, Title, SizeInMB, Units) {
    var uploadControl = document.getElementById(ControlName);
    //  alert(uploadControl.files[0].size);
    var convertMb = 0;
    if (Units == 'MB') {
        convertMb = eval(SizeInMB) * 1024 * 1024;
    }
    else {
        convertMb = eval(SizeInMB) * 1024;
    }


    if (uploadControl.files[0].size > convertMb) {
        jAlert('<strong> File size should be less than  ' + SizeInMB + ' ' + Units + ' !</strong>', Title);
        $('#' + ControlName).val('');

        $("#popup_ok").click(function () {
            $('#' + ControlName).focus();
        });

        return false;
    }
}
//AlertConfirmation

var stat = '';
function ConfirmValidation(btn, AlertMsg, Title, Confirm1, cancel1) {
    var compvalue = true;

    var status = false;
    if (stat != '') {

        if (stat == compvalue) {
            return true;
        }
        else {
            jConfirm(AlertMsg, Title, Confirm1, cancel1, function (r) {
                if (r) {
                    stat = r;
                    $(btn)[0].click();
                }

            });

            return false;
        }
    }
    else {
        jConfirm(AlertMsg, Title, Confirm1, cancel1, function (r) {
            if (r) {

                stat = r;

                $(btn)[0].click();

            }

        });
    }
    return status;
}


//Extention Should be pass with comma separated like "jpeg,jpg,gif,bmp,png"
function ValidFileExtentionAndSize(ControlName, fileExtensions, Title, SizeInMB, Units, OkBtn, cancel1) {
    var fileExtension = fileExtensions.split(','); // ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
    file = $('#' + ControlName).val();
    var extension = file.substr((file.lastIndexOf('.') + 1));

    if ($.inArray(extension, fileExtension) == -1) {
        jAlert('<strong> ' + fileExtension.join(', ') + ' !</strong>', Title, OkBtn, cancel1);
        $('#' + ControlName).val('');

        $("#popup_ok").click(function () {
            $('#' + ControlName).focus();
        });

        return false;
    }
    else {
        //  alert("dfdf:");

        var uploadControl = document.getElementById(ControlName);
        //  alert(uploadControl.files[0].size);
        var convertMb = 0;
        if (Units == 'MB') {
            convertMb = eval(SizeInMB) * 1024 * 1024;
        }
        else {
            convertMb = eval(SizeInMB) * 1024;
        }


        if (uploadControl.files[0].size > convertMb) {
            jAlert('<strong>  ' + SizeInMB + ' ' + Units + ' !</strong>', Title, OkBtn, cancel1);
            $('#' + ControlName).val('');

            $("#popup_ok").click(function () {
                $('#' + ControlName).focus();
            });

            return false;
        }
    }

}
function JConfirmation(ControlName, MessageControlName, ControlType, ControlTypeValue) {
    jConfirm(ControlName, MessageControlName, Title, function (r) {
        debugger;
        if (r) {
            $('#' + ControlName).attr(ControlType, ControlTypeValue);
            $('#' + ControlName).click();

            $("#popup_ok").click(function () {
                $('#' + ControlName).focus();
            });

            return true;
        }
        else {
            return false;
        }
    });
}
//*********End File Upload Control Validation ****************

