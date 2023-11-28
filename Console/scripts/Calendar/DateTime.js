var tmFocus = "";
var x = new function(){};
function tmInit(fldNm) {
	//frmObj = document.getElementById(frmName);
	this.fldObj = document.getElementById(fldNm);
	this.tmHour = document.getElementById(fldNm+'_ind_hour');
	this.tmMinute = document.getElementById(fldNm+'_ind_minute');
	this.tmMeridian = document.getElementById(fldNm+'_ind_meridian');
	this.tmYear = document.getElementById(fldNm+'_ind_year');
	this.tmMonth = document.getElementById(fldNm+'_ind_month');
	this.tmDay = document.getElementById(fldNm+'_ind_day');
	this.tmCalDiv = document.getElementById(fldNm+'CalDiv');
	this.tmCalImg = document.getElementById(fldNm+'CalImg');
	return this;
}
function tmAutoTab(evt, cur_field, char_max, goto_field){    
	if (document.getElementById){	
		if (cur_field.value.length > (char_max-1) && (goto_field)){
				goto_field.focus();
		}
	}
}
function tmCalendar(frmName,fldNm,Month,Year) {
	 var tmCalDiv = document.getElementById(fldNm+'CalDiv');
	 var output = '';
	 td = new Date();
	 tdy = td.getFullYear();
	 tdm = leadingZero(td.getMonth()+1);
	 tdd = leadingZero(td.getDate());
	 tdhr = '12';
	 tdmn = '00';
	 tdmr = 'AM';
	 if(Month == '') {
	 	tm = new Date();
	 	Month = tm.getMonth()+1;
	 	Year = tm.getFullYear();
	}
	 
	 Month = parseFloat(Month)-1;
	 Year = parseInt(Year);

	 var next_month = Month+2;
	 var next_month_year = Year;
	 if (next_month > 12) { next_month=1; next_month_year++; }
	 var last_month = Month;
	 var last_month_year = Year;
	 //window.status = last_month;
	 if (last_month < 1) { last_month=12; last_month_year--; }
	 strPre = 'tmCalendar(\''+frmName+'\',\''+fldNm+'\',\''+last_month+'\',\''+last_month_year+'\')';
	 strNex = 'tmCalendar(\''+frmName+'\',\''+fldNm+'\',\''+next_month+'\',\''+next_month_year+'\')';
        output += '<TABLE WIDTH=144 BORDER=1 BORDERWIDTH=1 CELLSPACING=0 CELLPADDING=1><TR BGCOLOR="C0C0C0"><td><TABLE WIDTH=144 BORDER=0 BORDERWIDTH=0 CELLSPACING=0 CELLPADDING=0>';
        output += '<TD ALIGN=LEFT CLASS="calHead"><a href="JavaScript:void(0);" onclick="'+strPre+'" class="cal"><<</a><\/TD>';
        output += '<TD ALIGN=CENTER class="calHead"><nobr><b>' + names[Month] + ' ' + Year + '<\/b><\/nobr><\/TD>';
        output += '<TD ALIGN=RIGHT CLASS="calHead"><a href="JavaScript:void(0);" onclick="'+strNex+'" class="cal">>></a><\/TD><\/td><\/tr><\/table>';
        output += '<\/TD><\/TR><TR><TD bgcolor="FFFFFF" ALIGN=CENTER>';
   
        firstDay = new Date(Year,Month,1);
        startDay = firstDay.getDay();
   
        if (((Year % 4 == 0) && (Year % 100 != 0)) || (Year % 400 == 0))
             days[1] = 29; 
        else
             days[1] = 28;
   
        output += '<TABLE WIDTH=120 BORDER=0 CELLSPACING=1 CELLPADDING=0 ALIGN=CENTER><TR>';
   
        for (i=0; i<7; i++)
            output += '<TD CLASS="cal" ALIGN=CENTER VALIGN=MIDDLE><B>' + dow[i] +'<\/B><\/TD>';
   
        output += '<\/TR><TR CLASS="cal" ALIGN=CENTER VALIGN=MIDDLE>';
   
        var column = 0;
        var lastMonth = Month - 1;
        if (lastMonth == -1) lastMonth = 11;
   
        for (i=0; i<startDay; i++, column++)
            output += '<TD CLASS="cal" WIDTH=14%>' + '<A CLASS="cal" href="JavaScript:void(0);" onclick="tmChangeDay(\''+frmName+'\',\''+fldNm+'\',\''+(last_month-1)+'\','+(days[lastMonth]-startDay+i+1) + ',\''+last_month_year+'\');"><font color="808080">' + (days[lastMonth]-startDay+i+1) + '</font><\/A>' +'<\/TD>';
            //output += '<TD CLASS="cal" WIDTH=14%><font color="808080">' + (days[lastMonth]-startDay+i+1) + '<\/TD>';
   
        for (i=1; i<=days[Month]; i++, column++) {
            output += '<TD CLASS="cal" WIDTH=14%>' + '<A CLASS="cal" href="JavaScript:void(0);" onclick="tmChangeDay(\''+frmName+'\',\''+fldNm+'\',\''+Month+'\','+ i + ',\''+Year+'\');">' + i + '<\/A>' +'<\/TD>';
            if (column == 6) {
                output += '<\/TR><TR ALIGN=CENTER VALIGN=MIDDLE>';
                column = -1;
            }
        }
   
        if (column > 0) {
            for (i=1; column<7; i++, column++)
             output += '<TD CLASS="cal" WIDTH=14%>' + '<A CLASS="cal" href="JavaScript:void(0);" onclick="tmChangeDay(\''+frmName+'\',\''+fldNm+'\',\''+(next_month-1)+'\','+ i + ',\''+next_month_year+'\');"><font color="808080">' + i + '</font><\/A>' +'<\/TD>';
                //output +=  '<TD class="cal"><font color="808080">' + i + '</font><\/TD>';
        }
        output += '<\/TR><\/TABLE><\/TD><\/TR><\/TABLE><center><a href="JavaScript:void(0);" onclick="tmToggleCal(\''+frmName+'\',\''+fldNm+'\');">close</a> | <a href="JavaScript:void(0);" onclick="tmCreateTime(\''+frmName+'\',\''+fldNm+'\',\''+tdy+'\',\''+tdm+'\',\''+tdd+'\',\''+tdhr+'\',\''+tdmn+'\',\''+tdmr+'\');tmToggleCal(\''+frmName+'\',\''+fldNm+'\');">today</a> | <a href="JavaScript:void(0);" onclick="tmCreateTime(\''+frmName+'\',\''+fldNm+'\',\'\',\'\',\'\',\'\',\'\',\'\');tmToggleCal(\''+frmName+'\',\''+fldNm+'\');">none</a></center>';
        tmCalDiv.innerHTML = output;
		tmCalDiv.style.display = 'block';
}

function tmToggleCal(frmName,fldNm) {
	var tmCalDiv = document.getElementById(fldNm+'CalDiv');
	var tmCalImg = document.getElementById(fldNm+'CalImg');
	tmCalDiv.style.display = 'none';
	tmCalImg.src=tmCalImg.src.replace('icon_cal_up','icon_cal_down');
}
 
function tmChangeDay(frmName,fldNm,mn,dy,yr) {
	frmObj = tmInit(fldNm);
	mn = parseFloat(mn)+1;
	frmObj.tmMonth.value = leadingZero(mn);
	frmObj.tmDay.value = leadingZero(dy);;
	frmObj.tmCalDiv.style.display = 'none';
	frmObj.tmCalImg.src=frmObj.tmCalImg.src.replace('icon_cal_up.gif','icon_cal_down.gif');
	frmObj.tmYear.value = yr; //tmYearStr.substring(tmMonthStr.length-2,tmMonthStr.length);
	tmSetTime(frmName, fldNm);
}
    
function tmMakeArray0() {
    for (i = 0; i<tmMakeArray0.arguments.length; i++)
    	this[i] = tmMakeArray0.arguments[i];
}
     var names     = new tmMakeArray0('January','February','March','April','May','June','July','August','September','October','November','December');
     var days      = new tmMakeArray0(31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31);
     var dow       = new tmMakeArray0('S','M','T','W','T','F','S');

function calSpin(frmNm,fldNm,tmpSpin, delta, ordinal) {
	if (tmpSpin.value == '' && tmpSpin.getAttribute('isRequired') == 0) return;
	frmObj = tmInit(fldNm);

	if (tmFocus == '') {tmFocus = frmObj.tmMeridian;}
	if (tmFocus.id.indexOf(fldNm) == -1) {tmFocus = frmObj.tmMeridian;}
	spinner = tmFocus;
	if (spinner.id.indexOf('meridian') == -1) {
		spinner.value = leadingZero(spinner.value);
	}
	if (delta)  {spinner.setAttribute('ordinal',parseFloat(spinner.getAttribute('ordinal'))+parseFloat(delta));}
	if (spinner.getAttribute('canonical')) {
		if (parseFloat(frmObj.tmMonth.value) == 02) {
			dys = (((frmObj.tmYear.value % 4 == 0) && ( (!(frmObj.tmYear.value % 100 == 0)) || (frmObj.tmYear.value % 400 == 0))) ? 29 : 28 );
			if (dys == 28) {
				frmObj.tmDay.setAttribute("canonical","01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28");
			} else {
				frmObj.tmDay.setAttribute("canonical","01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29");
			}
		} else if (parseFloat(frmObj.tmMonth.value) == 9 || parseFloat(frmObj.tmMonth.value) == 4 || parseFloat(frmObj.tmMonth.value) == 6 || parseFloat(frmObj.tmMonth.value) == 11) {
			frmObj.tmDay.setAttribute("canonical","01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30");
		} else {
			frmObj.tmDay.setAttribute("canonical","01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31");
		}
		arr = spinner.getAttribute('canonical').split(',');
			if (spinner.getAttribute('ordinal') < 0) spinner.ordinal = arr.length - 1;
			else if (spinner.getAttribute('ordinal') >= arr.length) spinner.ordinal = 0;
			if (delta != 0)
				spinner.value = arr[Math.abs(spinner.getAttribute('ordinal')%arr.length)];
			else {
				for (x=0; x < arr.length; x++) 					
					if (arr[x].toLowerCase() == spinner.value.toLowerCase()) {spinner.value = arr[x]; spinner.setAttribute("ordinal",x); break;}
				if (x >= arr.length) spinner.value = arr[spinner.getAttribute('ordinal')];
			}					
	} else {
		if ((ordinal) && parseFloat(ordinal) <= parseFloat(spinner.getAttribute('max')) && parseFloat(ordinal) >= parseFloat(spinner.getAttribute('min'))) {
			spinner.value = parseFloat(ordinal);
			spinner.setAttribute("ordinal",parseFloat(ordinal));
		} else {
			if (parseFloat(spinner.getAttribute('ordinal')) > parseFloat(spinner.getAttribute('max'))) spinner.setAttribute("ordinal",spinner.getAttribute('max'));
			if (parseFloat(spinner.getAttribute('ordinal')) < parseFloat(spinner.getAttribute('min'))) spinner.setAttribute("ordinal",spinner.getAttribute('min'));
			spinner.value = spinner.getAttribute('ordinal');
		}
	}
	if (spinner.id.indexOf('month') > -1 && spinner.value == 02) {
		frmObj.tmDay.value = '01';
	}
	tmSetTime(frmNm, fldNm);
}
function tmSetTime(frmNm, fldNm) {
	frmObj = tmInit(fldNm);
	tm = new Date();
	Month = tm.getMonth()+1;
	Year = tm.getFullYear();
	Day = tm.getDate();
	if (frmObj.tmHour.value == '') frmObj.tmHour.value = 12;
	if (frmObj.tmMinute.value == '') frmObj.tmMinute.value = '00';
	if (frmObj.tmMeridian.value == '') frmObj.tmMeridian.value = 'AM';
	if (frmObj.tmYear.value == '') frmObj.tmYear.value = Year;
	if (frmObj.tmDay.value == '') frmObj.tmDay.value = leadingZero(Day);
	if (frmObj.tmMonth.value == '') frmObj.tmMonth.value = leadingZero(Month);
	frmObj.fldObj.value = frmObj.tmYear.value+'-'+frmObj.tmMonth.value+'-'+frmObj.tmDay.value+' '+frmObj.tmHour.value+':'+frmObj.tmMinute.value+frmObj.tmMeridian.value;
}

function tmCreateTime(frmNm, fldNm,y,m,d,hr,mn,mr) {
	frmObj = tmInit(fldNm);
	//alert(fldNm);
	if (y != '') {
	/*
		frmObj.tmYear.value = y;
		frmObj.tmMonth.value = leadingZero(m);
		frmObj.tmDay.value = leadingZero(d);
		frmObj.tmHour.value = leadingZero(hr);
		frmObj.tmMinute.value = leadingZero(mn);
		frmObj.tmMeridian.value = leadingZero(mr);
	*/
		frmObj.tmYear.value = y;
		frmObj.tmMonth.value = m;
		frmObj.tmDay.value = d;
		frmObj.tmHour.value = hr;
		frmObj.tmMinute.value = mn;
		frmObj.tmMeridian.value = mr;	
		tmSetTime(frmNm,fldNm);
	} else {
		frmObj.tmYear.value = y;
		frmObj.tmMonth.value = m;
		frmObj.tmDay.value = d;
		frmObj.tmHour.value = hr;
		frmObj.tmMinute.value = mn;
		frmObj.tmMeridian.value = mr;
		frmObj.fldObj.value = '';
	}
	
}

