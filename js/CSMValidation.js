
//*****************Global Method******************
function ValidateEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};
//*****************End Global*********************

//*************For TextBox Validation*************
//Blank Field Validation

//function blankFieldValidation(ControlName, MessageControlName, Title) {
//    if ($('#' + ControlName).val() == '') {
//        $('#' + ControlName).focus();

//        jAlert('<strong>' + MessageControlName + ' can not blank !</strong>', Title);


//        return false;
//    }
//    else {
//        return true;
//    }
//}
function blankFieldValidation(ControlName, MessageControlName, Title) {
    if ($('#' + ControlName).val() == '') {
        //        $('#' + ControlName).focus();

        jAlert('<strong>' + MessageControlName + ' can not be blank !</strong>', Title);
        $("#popup_ok").click(
      function () {
          $('#' + ControlName).focus();
          // Do something after the OK button is clicked...
      });


        return false;
    }
    else {
        return true;
    }
}

function WhiteSpaceValidation1st(ControlName, MessageControlName, Title) {
      if ($('#' + ControlName).val().charAt(0) == ' ') {
        $('#' + ControlName).focus();

        jAlert('<strong>White Space not allowed in first place of ' + MessageControlName +' !</strong>', Title);
        return false;
    }
    else {
        return true;
    }
}

function WhiteSpaceValidationLast(ControlName, MessageControlName,Title) {
    var strVal = $('#' + ControlName).val();
// alert(strVal.length);
  //  return false;
    if (strVal.substr(strVal.length - 1) == ' ') {
        $('#' + ControlName).focus();
        jAlert('<strong>White Space not allowed in last place of ' + MessageControlName + ' !</strong>', Title);
        return false;
    }
    else {
        return true;
    }
}



//*********End Textbox Validation************************
function SpecialCharacter1st(ControlName, MessageControlName,  Title) {
    if ($('#' + ControlName).val().charAt(0) == "'") {
        jAlert('<strong> ' + MessageControlName + ' not allowed! </strong>', Title);
        return false;
    }
    else {
        return true;
    }
}

//*********For EmailId Validation******************************
function EmailValidation(Controlname,MessageControlName, Title) {

    var strVal = $('#' + Controlname).val();
    if (strVal != '') {

        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        if (!expr.test(strVal)) {
            jAlert('<strong>Invalid ' + MessageControlName + ' !</strong>', Title);
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

function DropDownValidation(ControlName, ValueForValidate, MessageControlName, Title) {
 
  

    if ($('#' + ControlName).val() == ValueForValidate || $('#' + ControlName).val() == '') {
        jAlert('<strong>Please select ' + MessageControlName + '  !</strong>', Title);
        return false;
    }
    else {
        return true;
    }
}
function checkInput(ControlName,MessageControlName, Title) {
    
    var obj = $('#' + ControlName).val();
    if (obj != '') {
        if (obj.length < 10) {
            jAlert('<strong>' + MessageControlName + ' should be atleast 10 characters long!</strong>', Title);
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

function ValidBlankFile(ControlName,  Title, MessageName) {
  
    file = $('#' + ControlName).val();
       if (file == '') {
        jAlert('<strong>' + MessageControlName + ' </strong>', Title);
        return false;
    }

}
//Extention Should be pass with comma separated like "jpeg,jpg,gif,bmp,png"
function ValidFileExtention(ControlName, fileExtensions, Title) {
    var fileExtension = fileExtensions.split(','); // ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
    file = $('#' + ControlName).val();
    var extension = file.substr((file.lastIndexOf('.') + 1));

    if ($.inArray(extension, fileExtension) == -1) {
        jAlert('<strong> Only ' + fileExtension.join(', ') + ' file allowed  !</strong>', Title);
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
        return false;
    }
}

//Extention Should be pass with comma separated like "jpeg,jpg,gif,bmp,png"
function ValidFileExtentionAndSize(ControlName, fileExtensions, Title, SizeInMB, Units) {
    var fileExtension = fileExtensions.split(','); // ['jpeg', 'jpg', 'png', 'gif', 'bmp'];
    file = $('#' + ControlName).val();
    var extension = file.substr((file.lastIndexOf('.') + 1));

    if ($.inArray(extension, fileExtension) == -1) {
        jAlert('<strong> Only ' + fileExtension.join(', ') + ' file allowed  !</strong>', Title);
        $('#' + ControlName).val('');
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
            jAlert('<strong> File size should be less than  ' + SizeInMB + ' ' + Units + ' !</strong>', Title);
            $('#' + ControlName).val('');
            return false;
        }
    }

}
//*********End File Upload Control Validation ****************

