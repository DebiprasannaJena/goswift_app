
var caltxtName;
var curTime;
function isValidTime(Str) {
    // Time validation function courtesty of 
    // Sandeep V. Tamhankar (stamhankar@hotmail.com) -->

    // Checks if time is in HH:MM:SS AM/PM format.
    // The seconds and AM/PM are optional.
    //var timeStr	=	Str.value;
    var timeStr = document.getElementById(Str).value;
    alert(timeStr);
    if (timeStr != "") {
        var timePat = /^(\d{1,2}):(\d{2})(:(\d{2}))?(\s?(AM|am|PM|pm))?$/;

        var matchArray = timeStr.match(timePat);
        if (matchArray == null) {
            alert("Time is not in a valid format.");
            Str.value = "";
            Str.focus();
            return false;
        }
        hour = matchArray[1];
        minute = matchArray[2];
        second = matchArray[4];
        ampm = matchArray[6];

        if (second == "") { second = null; }
        if (ampm == "") { ampm = null }

        if (hour < 0 || hour > 23) {
            alert("Hour must be between 1 and 12. (or 0 and 23 for military time)");
            Str.value = "";
            Str.focus();
            return false;
        }
        if (hour <= 12 && ampm == null) {
            // if (confirm("Please indicate which time format you are using.  OK = Standard Time, CANCEL = Military Time")) {
            alert("You must specify AM or PM.");
            return false;
            //}
        }
        if (hour > 12 && ampm != null) {
            alert("You can't specify AM or PM for military time.");
            Str.value = "";
            Str.focus();
            return false;
        }
        if (minute < 0 || minute > 59) {
            alert("Minute must be between 0 and 59.");
            Str.value = "";
            Str.focus();
            return false;
        }
        if (second != null && (second < 0 || second > 59)) {
            alert("Second must be between 0 and 59.");
            Str.value = "";
            Str.focus();
            return false;
        }
        return true;
    }
}
function TimeValidation(Str) {
    // Time validation function courtesty of 
    // Sandeep V. Tamhankar (stamhankar@hotmail.com) -->

    // Checks if time is in HH:MM:SS AM/PM format.
    // The seconds and AM/PM are optional.
    //var timeStr	=	Str.value;
    var timeStr = document.getElementById(Str).value;
    if (timeStr != "") {
        var timePat = /^(\d{1,2}):(\d{2})(\s?(AM|am|PM|pm))?$/;

        var matchArray = timeStr.match(timePat);
        if (matchArray == null) {
            alert("Time is not in a valid format");
            document.getElementById(Str).value = "";
            document.getElementById(Str).focus();
            return false;
        }
        hour = matchArray[1];
        minute = matchArray[2];
        ampm = matchArray[4];
        if (hour <= 12 && minute <= 59) {

            if (ampm == "") { ampm = null }

            if (hour < 0 || hour > 23) {
                alert("Hour must be between 1 and 12");
                document.getElementById(Str).value = "";
                document.getElementById(Str).focus();
                return false;
            }
            if (hour <= 12 && ampm == null) {
                // if (confirm("Please indicate which time format you are using.  OK = Standard Time, CANCEL = Military Time")) {
                alert("You must specify AM or PM");
                return false;
                //}
            }
            if (hour > 12 && ampm != null) {
                alert("Please Provide The valid Time Format");
                document.getElementById(Str).value = "";
                document.getElementById(Str).focus();
                return false;
            }
            if (minute < 0 || minute > 59) {
                alert("Minute must be between 0 and 59");
                document.getElementById(Str).value = "";
                document.getElementById(Str).focus();
                return false;
            }

            return true;
        }
        else {
            alert("Please Provide The valid Time Format");
            document.getElementById(Str).value = "";
            document.getElementById(Str).focus();
            return false;
        }
    }
}

function setTxtTimer() {
    curTime = document.forms["frmTimer"].selTimer.options[document.forms["frmTimer"].selTimer.selectedIndex].value
    eval('document.getElementById("' + caltxtName + '").value = "' + curTime + '"');
    //hideCalendar();
    hideTimer();

}
function d2dTimer() {
    var outputTime = '';
    if (ppcNN6) {
        outputTime += '<form name="frmTimer"><table width="185" border="0" class="body" cellspacing="0" cellpadding="0"><tr>';
    }
    else {
        outputTime += '<table width="185" border="0" class="body" cellspacing="0" cellpadding="0"><form name="frmTimer"><tr>';
    }
    outputTime += '<td class="body" align="left" width="100%"><SELECT class="cal-TextBox" NAME="selTimer" onChange="setTxtTimer();">';
    outputTime += ' <option value="">Select<\/option>';
    outputTime += ' <option value="12:00 AM">12:00 AM <\/option>';
    outputTime += ' <option value="12:15 AM">12:15 AM <\/option>';
    outputTime += ' <option value="12:30 AM">12:30 AM <\/option>';
    outputTime += ' <option value="12:45 AM">12:45 AM <\/option>';
    outputTime += ' <option value="1:00 AM">1:00 AM <\/option>';
    outputTime += ' <option value="1:15 AM">1:15 AM <\/option>';
    outputTime += ' <option value="1:30 AM">1:30 AM <\/option>';
    outputTime += ' <option value="1:45 AM">1:45 AM <\/option>';
    outputTime += ' <option value="2:00 AM">2:00 AM <\/option>';
    outputTime += ' <option value="2:15 AM">2:15 AM <\/option>';
    outputTime += ' <option value="2:30 AM">2:30 AM <\/option>';
    outputTime += ' <option value="2:45 AM">2:45 AM <\/option>';
    outputTime += ' <option value="3:00 AM">3:00 AM <\/option>';
    outputTime += ' <option value="3:15 AM">3:15 AM <\/option>';
    outputTime += ' <option value="3:30 AM">3:30 AM <\/option>';
    outputTime += ' <option value="3:45 AM">3:45 AM <\/option>';
    outputTime += ' <option value="4:00 AM">4:00 AM <\/option>';
    outputTime += ' <option value="4:15 AM">4:15 AM <\/option>';
    outputTime += ' <option value="4:30 AM">4:30 AM <\/option>';
    outputTime += ' <option value="4:45 AM">4:45 AM <\/option>';
    outputTime += ' <option value="5:00 AM">5:00 AM <\/option>';
    outputTime += ' <option value="5:15 AM">5:15 AM <\/option>';
    outputTime += ' <option value="5:30 AM">5:30 AM <\/option>';
    outputTime += ' <option value="5:45 AM">5:45 AM <\/option>';
    outputTime += ' <option value="6:00 AM">6:00 AM <\/option>';
    outputTime += ' <option value="6:15 AM">6:15 AM <\/option>';
    outputTime += ' <option value="6:30 AM">6:30 AM <\/option>';
    outputTime += ' <option value="6:45 AM">6:45 AM <\/option>';
    outputTime += ' <option value="7:00 AM">7:00 AM <\/option>';
    outputTime += ' <option value="7:15 AM">7:15 AM <\/option>';
    outputTime += ' <option value="7:30 AM">7:30 AM <\/option>';
    outputTime += ' <option value="7:45 AM">7:45 AM <\/option>';
    outputTime += ' <option value="8:00 AM">8:00 AM <\/option>';
    outputTime += ' <option value="8:15 AM">8:15 AM <\/option>';
    outputTime += ' <option value="8:30 AM">8:30 AM <\/option>';
    outputTime += ' <option value="8:45 AM">8:45 AM <\/option>';
    outputTime += ' <option value="9:00 AM">9:00 AM <\/option>';
    outputTime += ' <option value="9:15 AM">9:15 AM <\/option>';
    outputTime += ' <option value="9:30 AM">9:30 AM <\/option>';
    outputTime += ' <option value="9:45 AM">9:45 AM <\/option>';
    outputTime += ' <option value="10:00 AM">10:00 AM <\/option>';
    outputTime += ' <option value="10:15 AM">10:15 AM <\/option>';
    outputTime += ' <option value="10:30 AM">10:30 AM <\/option>';
    outputTime += ' <option value="10:45 AM">10:45 AM <\/option>';
    outputTime += ' <option value="11:00 AM">11:00 AM <\/option>';
    outputTime += ' <option value="11:15 AM">11:15 AM <\/option>';
    outputTime += ' <option value="11:30 AM">11:30 AM <\/option>';
    outputTime += ' <option value="11:45 AM">11:45 AM <\/option>';


    outputTime += ' <option value="12:00 PM">12:00 PM <\/option>';
    outputTime += ' <option value="12:15 PM">12:15 PM <\/option>';
    outputTime += ' <option value="12:30 PM">12:30 PM <\/option>';
    outputTime += ' <option value="12:45 PM">12:45 PM <\/option>';
    outputTime += ' <option value="1:00 PM">1:00 PM <\/option>';
    outputTime += ' <option value="1:15 PM">1:15 PM <\/option>';
    outputTime += ' <option value="1:30 PM">1:30 PM <\/option>';
    outputTime += ' <option value="1:45 PM">1:45 PM <\/option>';
    outputTime += ' <option value="2:00 PM">2:00 PM <\/option>';
    outputTime += ' <option value="2:15 PM">2:15 PM <\/option>';
    outputTime += ' <option value="2:30 PM">2:30 PM <\/option>';
    outputTime += ' <option value="2:45 PM">2:45 PM <\/option>';
    outputTime += ' <option value="3:00 PM">3:00 PM <\/option>';
    outputTime += ' <option value="3:15 PM">3:15 PM <\/option>';
    outputTime += ' <option value="3:30 PM">3:30 PM <\/option>';
    outputTime += ' <option value="3:45 PM">3:45 PM <\/option>';
    outputTime += ' <option value="4:00 PM">4:00 PM <\/option>';
    outputTime += ' <option value="4:15 PM">4:15 PM <\/option>';
    outputTime += ' <option value="4:30 PM">4:30 PM <\/option>';
    outputTime += ' <option value="4:45 PM">4:45 PM <\/option>';
    outputTime += ' <option value="5:00 PM">5:00 PM <\/option>';
    outputTime += ' <option value="5:15 PM">5:15 PM <\/option>';
    outputTime += ' <option value="5:30 PM">5:30 PM <\/option>';
    outputTime += ' <option value="5:45 PM">5:45 PM <\/option>';
    outputTime += ' <option value="6:00 PM">6:00 PM <\/option>';
    outputTime += ' <option value="6:15 PM">6:15 PM <\/option>';
    outputTime += ' <option value="6:30 PM">6:30 PM <\/option>';
    outputTime += ' <option value="6:45 PM">6:45 PM <\/option>';
    outputTime += ' <option value="7:00 PM">7:00 PM <\/option>';
    outputTime += ' <option value="7:15 PM">7:15 PM <\/option>';
    outputTime += ' <option value="7:30 PM">7:30 PM <\/option>';
    outputTime += ' <option value="7:45 PM">7:45 PM <\/option>';
    outputTime += ' <option value="8:00 PM">8:00 PM <\/option>';
    outputTime += ' <option value="8:15 PM">8:15 PM <\/option>';
    outputTime += ' <option value="8:30 PM">8:30 PM <\/option>';
    outputTime += ' <option value="8:45 PM">8:45 PM <\/option>';
    outputTime += ' <option value="9:00 PM">9:00 PM <\/option>';
    outputTime += ' <option value="9:15 PM">9:15 PM <\/option>';
    outputTime += ' <option value="9:30 PM">9:30 PM <\/option>';
    outputTime += ' <option value="9:45 PM">9:45 PM <\/option>';
    outputTime += ' <option value="10:00 PM">10:00 PM <\/option>';
    outputTime += ' <option value="10:15 PM">10:15 PM <\/option>';
    outputTime += ' <option value="10:30 PM">10:30 PM <\/option>';
    outputTime += ' <option value="10:45 PM">10:45 PM <\/option>';
    outputTime += ' <option value="11:00 PM">11:00 PM <\/option>';
    outputTime += ' <option value="11:15 PM">11:15 PM <\/option>';
    outputTime += ' <option value="11:30 PM">11:30 PM <\/option>';
    outputTime += ' <option value="11:45 PM">11:45 PM <\/option>';

    outputTime += '<\/SELECT>'
    outputTime += '<img src="../jscript48/btn_close_small.gif" width="12" height="10" onClick="hideTimer();">'
    outputTime += '<\/td><\/tr>';

    if (ppcNN6) {
        outputTime += '<\/table><\/form>';
    }
    else {
        outputTime += '<\/form><\/table>';
    }
    return outputTime;
    //alert(outputTime);
}

function showTimer(frmName, txtName, btnImg) {

    calfrmName = frmName;
    caltxtName = txtName;
    if (ppcIE) {
        ppcX = getOffsetLeft(document.images[btnImg]);
        ppcY = getOffsetTop(document.images[btnImg]) + document.images[btnImg].height;
    }
    else if (ppcNN) {
        ppcX = document.images[btnImg].x;
        ppcY = document.images[btnImg].y + document.images[btnImg].height;
    }
    //domlay('popupcalendar',1,ppcX,ppcY,Calendar(todayDate.getMonth(),todayDate.getFullYear()));       
    //domlay('popupcalendar',1,ppcX,ppcY,Calendar(curDate.getMonth(),curDate.getFullYear()));
    //IsCalendarVisible = true;

    if (document.layers) {
        document.layers['popTimer'].visibility = "show";
        document.layers['popTimer'].left = ppcX;
        document.layers['popTimer'].top = ppcY;
    }
    else if (document.all) {
        document.all['popTimer'].style.visibility = "visible";
        document.all['popTimer'].style.top = ppcY;
        document.all['popTimer'].style.left = ppcX;
    }
    else if (document.getElementById) {
        document.getElementById('popTimer').style.visibility = "visible";
        document.getElementById('popTimer').style.left = ppcX + "px";
        document.getElementById('popTimer').style.top = ppcY + "px";
    }
    if (document.layers) {
        sprite1 = document.layers['popTimer'].document;
        // add father layers if needed! document.layers[''+father+'']...
        sprite1.open();
        sprite1.write(d2dTimer());
        sprite1.close();
    }
    else if (document.all) document.all['popTimer'].innerHTML = d2dTimer();
    else if (document.getElementById) {
        //Thanx Reyn!
        rng = document.createRange();
        el = document.getElementById('popTimer');
        rng.setStartBefore(el);
        htmlFrag = rng.createContextualFragment(d2dTimer())
        while (el.hasChildNodes()) el.removeChild(el.lastChild);
        el.appendChild(htmlFrag);
        // end of Reyn ;)
    }
    //document.popTimer.style.visible=true;


}
function hideTimer() {
    if (document.layers) document.layers['popTimer'].visibility = "hide"
    else if (document.all) document.all['popTimer'].style.visibility = "hidden"
    else if (document.getElementById) document.getElementById('popTimer').style.visibility = "hidden"

}
