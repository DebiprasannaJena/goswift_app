// allows 0-9, decimal, and all navigation keys (backspace, enter, arrows, page up/down end, home, etc.) 
function isNumberDecimalKey(evt, txtBox, decimalPlaces) { 
 
 
    var charCode = (evt.which) ? evt.which : event.keyCode; 
    var txtValue = txtBox.value; //does not include current keypress 
    if (charCode == 8 || charCode == 9 || charCode == 46)    // Backspace or tab or delete 
        return true; 
 
 
    if (charCode == 190 || charCode == 110) //decimal 
    { 
        if (txtValue.indexOf(".") != -1) 
            return false; 
        else 
            return true; 
    } 
    else if ((charCode > 57 || (charCode > 31 && charCode < 48)) && !(charCode >= 96 && charCode <= 105)) { 
        return false; 
    } 
    else { 
        if (decimalPlaces != null && txtValue.indexOf(".") != -1) { 
            var txtSplit = txtValue.split("."); 
            var caretPosition = doGetCaretPosition(txtBox); 
            if (txtSplit[1].length == decimalPlaces && caretPosition > txtSplit[0].length) { 
                // if input is over the max decimal place, detect whether current selection existing. 
                var selectionText = ""; 
                if (window.getSelection) { 
                    selectionText = window.getSelection(); 
                } 
                else if (document.selection) { 
                    selectionText = document.selection.createRange().text; 
                } 
                return selectionText.length != 0; 
            } 
        } 
        return true; 
    } 
} 
//must use onkeydown, not onkeypress 
//onkeydown="return isNumberDecimalKey(event)" 
 
 
 
 
//for copy & pasting into numeric only textfields 
function isNumberCopyPaste(evt, txtBox, decimalPlaces) { 
    var charCode = (evt.which) ? evt.which : event.keyCode; 
 
 
    if (charCode == 86) // "v" 
    { 
        removeBadChars(txtBox, decimalPlaces); 
    } 
} 
 
 
function isNumberBlur(evt, txtBox, decimalPlaces) { 
    removeBadChars(txtBox, decimalPlaces); 
} 
 
 
function removeBadChars(txtBox, decimalPlaces) { 
 
 
    var txtValue = parseFloat(txtBox.value); 
    if (isNaN(txtValue)) { 
        txtBox.value = ""; 
    } 
    else if (decimalPlaces == null) { 
        txtBox.value = txtValue; 
    } 
    else { 
        txtBox.value = txtValue.toFixed((decimalPlaces == null) ? 0 : decimalPlaces); 
    } 
}


function doGetCaretPosition(ctrl) {
    var CaretPos = 0;
    // IE Support
    if (document.selection) {
        ctrl.focus();
        var Sel = document.selection.createRange();
        Sel.moveStart('character', -ctrl.value.length);
        CaretPos = Sel.text.length;
    }
    // Firefox support
    else if (ctrl.selectionStart || ctrl.selectionStart == '0') {
        CaretPos = ctrl.selectionStart;
    }


    return (CaretPos);
}




function chkSessionFnYr(ssval, ctrlfrm, ctrlto) {
    
    if (ssval == '') 
    {
        alert('Please Choose the Financial Year');
        return false;
    }
    else 
    {
        var sFnYr = ssval.split('-');
        var styr = '01-Apr-' + sFnYr[0];
        var enyr = '31-Mar-20' + sFnYr[1];

        var ctrlfrmdt = document.getElementById(ctrlfrm).value;
        var ctrlenddt = document.getElementById(ctrlto).value;

        if (customCheck(ctrlfrmdt) < customCheck(styr)) {
            alert('Date Range not coming within the financial year');
            document.getElementById(ctrlfrm).focus();
            return false;
        }
        if (customCheck(ctrlenddt) > customCheck(enyr)) {
            alert('Date Range not coming within the financial year');
            document.getElementById(ctrlto).focus();
            return false;
         }      
        
    }

}
function customCheck(str) {

    var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
               n = months.length, re = /(\d{2})-([a-z]{3})-(\d{4})/i, matches;
    while (n--) { months[months[n]] = n; }
    matches = str.match(re);
    return new Date(matches[3], months[matches[2]], matches[1]);
}

function inputLimiter(e, allow) {
    var AllowableCharacters = '';

    if (allow == 'NameCharacters') {
        AllowableCharacters = ' ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
    }
    if (allow == 'NameCharactersAndNumbers') {
        AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz';
    }
    if (allow == 'Numbers') {
        AllowableCharacters = '1234567890';
    }
    if (allow == 'Decimal') {
        AllowableCharacters = '1234567890.';
    }
    if (allow == 'Email') {
        AllowableCharacters = '1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz@@._';
    }
    if (allow == 'Address') {
        AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
    }
    if (allow == 'DateFormat') {
        AllowableCharacters = '1234567890/-';
    }
    if (allow == 'NumbersSSN') {
        AllowableCharacters = '1234567890-';
    }
    if (allow == 'RawMetrial') {
        AllowableCharacters = '1234567890 ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!"#$%&()*+,-./:;<=>?@[\]^_`{|}~';
    }
    var k;
    k = document.all ? parseInt(e.keyCode) : parseInt(e.which);
    if (k != 13 && k != 8 && k != 0) {
        if ((e.ctrlKey == false) && (e.altKey == false)) {
            return (AllowableCharacters.indexOf(String.fromCharCode(k)) != -1);
        }
        else {
            return true;
        }
    }
    else {
        return true;
    }
}