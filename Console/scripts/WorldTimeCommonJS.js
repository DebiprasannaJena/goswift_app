	
	
	function stopclock(){ 
		if(timerRunning) 
			clearTimeout(timerID) 
		timerRunning = false 
	} 
	 
	
	 
	function showClock(timezone,id){ 
	
		var timeObj		= new Date(); 
		var seconds1		= timeObj.getTime(); 
		var curdate;
		var month,date,year
		timetoday	= new Date(); 
		diff		= timetoday.getTimezoneOffset(); 
		diff		= 60000 * diff; 
		seconds1		= seconds1 + diff + timezone; 
	 
		timetoday.setTime(seconds1); 
		hoursGMT		= timetoday.getHours(); 
		minutes		= timetoday.getMinutes(); 
		month		= timetoday.getMonth(); 
		seconds = timetoday.getSeconds() 
		month	   += 1; 
		date		= timetoday.getDate(); 
		year		= timetoday.getYear(); 
		
		month		= ( month < 10 ) ? "0" + month : month; 
		date		= ( date < 10 ) ? "0" + date : date; 
		curdate		=  date+ "-" + getMonthName(month) + "-" + year; 
		
		var timeValueGMT  = "" + ( (hoursGMT >  12) ? hoursGMT - 12 : hoursGMT ) 
			timeValueGMT +=      ( (minutes <  10) ? ":0" : ":" ) + minutes 
			timeValueGMT +=      ( (seconds <  10) ? ":0" : ":" ) + seconds 
			timeValueGMT +=        (hoursGMT >= 12) ? " PM" : " AM" 
		document.getElementById(id).innerHTML	= timeValueGMT + "<br>" + curdate; 
	 
		
	 
		timerID = setTimeout("showClock("+timezone+",'"+id+"')",1000) 
		timerRunning = true 
	} 
	function showClockHome(timezone,timeId,dateId){ 
	
		var timeObj		= new Date(); 
		var seconds1		= timeObj.getTime(); 
		var curdate;
		var month,date,year
		timetoday	= new Date(); 
		diff		= timetoday.getTimezoneOffset(); 
		diff		= 60000 * diff; 
		seconds1		= seconds1 + diff + timezone; 
	 
		timetoday.setTime(seconds1); 
		hoursGMT		= timetoday.getHours(); 
		minutes		= timetoday.getMinutes(); 
		month		= timetoday.getMonth(); 
		seconds = timetoday.getSeconds() 
		month	   += 1; 
		date		= timetoday.getDate(); 
		year		= timetoday.getYear(); 
		
		month		= ( month < 10 ) ? "0" + month : month; 
		date		= ( date < 10 ) ? "0" + date : date; 
		curdate		=  date+ "-" + getMonthName(month) + "-" + year.toString().substr(2,4); 
		//----COMMENTED FOR SHOWING 24 HOUR FORMAT
		//var timeValueGMT  = "" + ( (hoursGMT >  12) ? hoursGMT - 12 : hoursGMT ) 
		var timeValueGMT  = "" + hoursGMT 
			timeValueGMT +=      ( (minutes <  10) ? ":0" : ":" ) + minutes 
			//timeValueGMT +=      ( (seconds <  10) ? ":0" : ":" ) + seconds 
			timeValueGMT +=        (hoursGMT >= 12) ? " PM" : " AM" 
	
		if (document.getElementById(dateId)==null)	
		{
			document.getElementById(timeId).innerHTML	= timeValueGMT //-----COMMENTED TO REMOVE DATE FORM HOEMPAGE+ "<br>" + curdate; 
		}
		else
		{
			document.getElementById(timeId).innerHTML	= timeValueGMT ; 
			document.getElementById(dateId).innerHTML	=  curdate; 
		}
		
	 
		timerID = setTimeout("showClockHome("+timezone+",'"+timeId+"','"+dateId+"')",1000) 
		timerRunning = true 
	} 
	
	function showTime(timezone,timeId,dateId){ 
	
		var timeObj		= new Date(); 
		var seconds1		= timeObj.getTime(); 
		var curdate;
		var month,date,year
		timetoday	= new Date(); 
		diff		= timetoday.getTimezoneOffset(); 
		diff		= 60000 * diff; 
		seconds1		= seconds1 + diff + timezone; 
	 
		timetoday.setTime(seconds1); 
		hoursGMT		= timetoday.getHours(); 
		minutes		= timetoday.getMinutes(); 
		month		= timetoday.getMonth(); 
		seconds = timetoday.getSeconds() 
		month	   += 1; 
		date		= timetoday.getDate(); 
		year		= timetoday.getYear(); 
		
		month		= ( month < 10 ) ? "0" + month : month; 
		date		= ( date < 10 ) ? "0" + date : date; 
		curdate		=  date+ "-" + getMonthName(month) + "-" + year; 
		//----COMMENTED FOR SHOWING 24 HOUR FORMAT
		//var timeValueGMT  = "" + ( (hoursGMT >  12) ? hoursGMT - 12 : hoursGMT ) 
		var timeValueGMT  = "" + hoursGMT 
			timeValueGMT +=      ( (minutes <  10) ? ":0" : ":" ) + minutes 
			//timeValueGMT +=      ( (seconds <  10) ? ":0" : ":" ) + seconds 
			timeValueGMT +=        (hoursGMT >= 12) ? " PM" : " AM" 
		if (document.getElementById(dateId)==null)	
		{
			document.getElementById(timeId).innerHTML	= timeValueGMT ;//+ "<br>" + curdate; 
		}
		else
		{
			document.getElementById(timeId).innerHTML	= timeValueGMT ; 
			document.getElementById(dateId).innerHTML	=  curdate; 
		}
	 
		
	} 

	function getMonthName(month,abbrivate)
	{
			
			month	=	new Number(month);
			var monthnameShort	=	new Array("","Jan","Feb","Mar","Apr","May","Jun","Jul","Aug","Sep","Oct","Nov","Dec")
			var monthnameFull	=	new Array("","January","February","March","April","May","June","July","August","September","October","November","December")
			switch (abbrivate)
			{
				case	true	:	return(monthnameShort[month]);
									break;
				case	false	:	return(monthnameFull[month]);
									break;					
				default			:	return(monthnameFull[month]);
									break;					
			}
	}

var timerID = null 
var timerRunning = false 

timezone = new Array(315) 
 
	// Countries that start with the letter A; 
timezone[0] = 16200000 
timezone[1] = 3600000 
timezone[2] = 3600000 
timezone[3] = -39600000 
timezone[4] = 3600000 
timezone[5] = 3600000 
timezone[6] = -14400000 
timezone[7] = 7200000 
timezone[8] = -14400000 
timezone[9] = -10800000 
timezone[10] = -14400000 
timezone[11] = 14400000 
timezone[12] = -14400000 
timezone[13] = 0 
timezone[14] = 34200000 
timezone[15] = 37800000 
timezone[16] = 36000000 
timezone[17] = 36000000 
timezone[18] = 36000000 
timezone[19] = 36000000 
timezone[20] = 34200000 
timezone[21] = 36000000 
timezone[22] = 28800000 
timezone[23] = 3600000 
timezone[24] = 10800000 
timezone[25] = -3600000 
 
	// Countries that start with the letter B; 
timezone[26] = -18000000 
timezone[27] = 10800000 
timezone[28] = 3600000 
timezone[29] = 21600000 
timezone[30] = -14400000 
timezone[31] = 7200000 
timezone[32] = 3600000 
timezone[33] = -21600000 
timezone[34] = 3600000 
timezone[35] = -14400000 
timezone[36] = 21600000 
timezone[37] = -14400000 
timezone[38] = -14400000 
timezone[39] = 3600000 
timezone[40] = 7200000 
timezone[41] = -14400000 
timezone[42] = -3600000 
timezone[43] = 10800000 
timezone[44] = -14400000 
timezone[45] = -14400000 
timezone[46] = 28800000 
timezone[47] = 7200000 
timezone[48] = 0 
timezone[49] = 7200000 
 
	// Countries that start with the letter C; 
timezone[50] = 25200000 
timezone[51] = 3600000 
timezone[52] = -21600000 
timezone[53] = -18000000 
timezone[54] = -25200000 
timezone[55] = -28800000 
timezone[56] = -14400000 
timezone[57] = -12600000 
timezone[58] = 0 
timezone[59] = -39600000 
timezone[60] = -3600000 
timezone[61] = 39600000 
timezone[62] = -18000000 
timezone[63] = 3600000 
timezone[64] = 3600000 
timezone[65] = 0 
timezone[66] = 45900000 
timezone[67] = -14400000 
timezone[68] = 28800000 
timezone[69] = -36000000 
timezone[70] = 0 
timezone[71] = -18000000 
timezone[72] = 3600000 
timezone[73] = -36000000 
timezone[74] = -21600000 
timezone[75] = 0 
timezone[76] = 3600000 
timezone[77] = -18000000 
timezone[78] = -14400000 
timezone[79] = 7200000 
timezone[80] = 3600000 
 
	// Countries that start with the letter D; 
timezone[81] = 3600000 
timezone[82] = 3600000 
timezone[83] = 10800000 
timezone[84] = -14400000 
timezone[85] = -14400000 
 
	// Countries that start with the letter E; 
timezone[86] = -21600000 
timezone[87] = -18000000 
timezone[88] = 7200000 
timezone[89] = -21600000 
timezone[90] = 0 
timezone[91] = 3600000 
timezone[92] = 10800000 
timezone[93] = 7200000 
timezone[94] = 10800000 
 
	// Countries that start with the letter F; 
timezone[95] = -14400000 
timezone[96] = 0 
timezone[97] = 43200000 
timezone[98] = 7200000 
timezone[99] = 3600000 
timezone[100] = -10800000 
timezone[101] = -36000000 
 
	// Countries that start with the letter G; 
timezone[102] = 3600000 
timezone[103] = -18000000 
timezone[104] = 0 
timezone[105] = -32400000 
timezone[106] = 14400000 
timezone[107] = 3600000 
timezone[108] = 0 
timezone[109] = 3600000 
timezone[110] = 7200000 
timezone[111] = -10800000 
timezone[112] = 14400000 
timezone[113] = 3600000 
timezone[114] = -14400000 
timezone[115] = -14400000 
timezone[116] = -14400000 
timezone[117] = 36000000 
timezone[118] = -21600000 
timezone[119] = 0 
timezone[120] = 0 
timezone[121] = -10800000 
 
	// Countries that start with the letter H; 
timezone[122] = -18000000 
timezone[123] = -21600000 
timezone[124] = 28800000 
timezone[125] = 3600000 
 
	// Countries that start with the letter I; 
timezone[126] = 0 
timezone[127] = 19800000 
timezone[128] = 28800000 
timezone[129] = 32400000 
timezone[130] = 25200000 
timezone[131] = 12600000 
timezone[132] = 10800000 
timezone[133] = 0 
timezone[134] = 7200000 
timezone[135] = 3600000 
 
	// Countries that start with the letter J; 
timezone[136] = -18000000 
timezone[137] = 32400000 
timezone[138] = -36000000 
timezone[139] = 10800000 
 
	// Countries that start with the letter K; 
timezone[140] = 21600000 
timezone[141] = 10800000 
timezone[142] = 43200000 
timezone[143] = 32400000 
timezone[144] = 32400000 
timezone[145] = 43200000 
timezone[146] = 10800000 
timezone[147] = -43200000 
timezone[148] = 18000000 
 
	// Countries that start with the letter L; 
timezone[149] = 25200000 
timezone[150] = 7200000 
timezone[151] = 7200000 
timezone[152] = -14400000 
timezone[153] = 7200000 
timezone[154] = 0 
timezone[155] = 7200000 
timezone[156] = 7200000 
timezone[157] = 3600000 
 
	// Countries that start with the letter M; 
timezone[158] = 3600000 
timezone[159] = 10800000 
timezone[160] = 0 
timezone[161] = 7200000 
timezone[162] = 28800000 
timezone[163] = 18000000 
timezone[164] = 0 
timezone[165] = 3600000 
timezone[166] = 3600000 
timezone[167] = 36000000 
timezone[168] = 34200000 
timezone[169] = 43200000 
timezone[170] = -14400000 
timezone[171] = 0 
timezone[172] = 14400000 
timezone[173] = 10800000 
timezone[174] = 3600000 
timezone[175] = -21600000 
timezone[176] = -28800000 
timezone[177] = -25200000 
timezone[178] = -25200000 
timezone[179] = -25200000 
timezone[180] = -39600000 
timezone[181] = 7200000 
timezone[182] = 7200000 
timezone[183] = 3600000 
timezone[184] = 28800000 
timezone[185] = 0 
timezone[186] = 7200000 
timezone[187] = 23400000 
 
	// Countries that start with the letter N; 
timezone[188] = 3600000 
timezone[189] = 43200000 
timezone[190] = 20700000 
timezone[191] = 3600000 
timezone[192] = -14400000 
timezone[193] = -14400000 
timezone[194] = 39600000 
timezone[195] = 39600000 
timezone[196] = 43200000 
timezone[197] = -21600000 
timezone[198] = 3600000 
timezone[199] = 3600000 
timezone[200] = 3600000 
timezone[201] = 41400000 
timezone[202] = 0 
timezone[203] = 36000000 
timezone[204] = 3600000 
 
	// Countries that start with the letter O; 
timezone[205] = 10800000 
 
	// Countries that start with the letter P; 
timezone[206] = 18000000 
timezone[207] = 32400000 
timezone[208] = -18000000 
timezone[209] = 36000000 
timezone[210] = -14400000 
timezone[211] = -18000000 
timezone[212] = 28800000 
timezone[213] = 43200000 
timezone[214] = 3600000 
timezone[215] = 39600000 
timezone[216] = 3600000 
timezone[217] = 0 
timezone[218] = -14400000 
 
	// Countries that start with the letter Q; 
timezone[219] = 10800000 
 
	// Countries that start with the letter R; 
timezone[220] = 14400000 
timezone[221] = 7200000 
timezone[222] = 32400000 
timezone[223] = 43200000 
timezone[224] = 21600000 
timezone[225] = 18000000 
timezone[226] = 36000000 
timezone[227] = 7200000 
timezone[228] = 28800000 
timezone[229] = 25200000 
timezone[230] = 39600000 
timezone[231] = 14400000 
timezone[232] = 14400000 
timezone[233] = 7200000 
 
	// Countries that start with the letter S; 
timezone[234] = -14400000 
timezone[235] = -39600000 
timezone[236] = 3600000 
timezone[237] = 0 
timezone[238] = 10800000 
timezone[239] = 0 
timezone[240] = 0 
timezone[241] = 14400000 
timezone[242] = 0 
timezone[243] = 28800000 
timezone[244] = 3600000 
timezone[245] = 3600000 
timezone[246] = -36000000 
timezone[247] = 39600000 
timezone[248] = 10800000 
timezone[249] = 7200000 
timezone[250] = 3600000 
timezone[251] = 19800000 
timezone[252] = -14400000 
timezone[253] = -14400000 
timezone[254] = 0 
timezone[255] = -14400000 
timezone[256] = -14400000 
timezone[257] = -14400000 
timezone[258] = -14400000 
timezone[259] = -10800000 
timezone[260] = -14400000 
timezone[261] = -14400000 
timezone[262] = 10800000 
timezone[263] = -10800000 
timezone[264] = 7200000 
timezone[265] = 3600000 
timezone[266] = 3600000 
timezone[267] = 7200000 
 
	// Countries that start with the letter T; 
timezone[268] = -36000000 
timezone[269] = 28800000 
timezone[270] = 21600000 
timezone[271] = 10800000 
timezone[272] = 25200000 
timezone[273] = 0 
timezone[274] = 46800000 
timezone[275] = -14400000 
timezone[276] = -36000000 
timezone[277] = -36000000 
timezone[278] = 3600000 
timezone[279] = 7200000 
timezone[280] = 18000000 
timezone[281] = -18000000 
timezone[282] = 43200000 
 
	// Countries that start with the letter U; 
timezone[283] = 10800000 
timezone[284] = 7200000 
timezone[285] = 14400000 
timezone[286] = 0 
timezone[287] = -21600000 
timezone[288] = -18000000 
timezone[289] = -25200000 
timezone[290] = -25200000 
timezone[291] = -18000000 
timezone[292] = -28800000 
timezone[293] = -32400000 
timezone[294] = -36000000 
timezone[295] = -36000000 
timezone[296] = -10800000 
timezone[297] = 18000000 
 
	// Countries that start with the letter V; 
timezone[298] = 39600000 
timezone[299] = 3600000 
timezone[300] = -14400000 
timezone[301] = 25200000 
timezone[302] = -14400000 
 
	// Countries that start with the letter W; 
timezone[303] = 43200000 
timezone[304] = 0 
timezone[305] = 43200000 
timezone[306] = -14400000 
 
	// Countries that start with the letter Y; 
timezone[307] = 10800000 
timezone[308] = 3600000 
 
	// Countries that start with the letter Z; 
timezone[309] = 7200000 
timezone[310] = 3600000 
timezone[311] = 7200000 
timezone[312] = 7200000 
timezone[313] = 7200000 
timezone[314] = 7200000 
timezone[315] = 7200000 
 
 
	// Country Array; 
country = new Array(315) 
 
	// Countries that start with the letter B 
country[0] = "Aghanistan" 
country[1] = "Albania" 
country[2] = "Algeria" 
country[3] = "American Samoa" 
country[4] = "Andorra" 
country[5] = "Angola" 
country[6] = "Anguila" 
country[7] = "Antarctica" 
country[8] = "Antiqua" 
country[9] = "Argentina" 
country[10] = "Argentina western prov." 
country[11] = "Armenia" 
country[12] = "Aruba" 
country[13] = "Ascension" 
country[14] = "Australia Northern Territory" 
country[15] = "Australia Lord Howe Island" 
country[16] = "Australia New South Wales" 
country[17] = "Australia Queensland" 
country[18] = "Australia Victoria" 
country[19] = "Australia Australian Capital Territory" 
country[20] = "Australia South" 
country[21] = "Australia Tasmania" 
country[22] = "Australia Western" 
country[23] = "Austria" 
country[24] = "Azerbajian" 
country[25] = "Azores" 
 
	// Countries that start with the letter B 
country[26] = "Bahamas" 
country[27] = "Bahrain" 
country[28] = "Baleari" 
country[29] = "Bangladesh" 
country[30] = "Barbados" 
country[31] = "Belarus" 
country[32] = "Belgium" 
country[33] = "Belize" 
country[34] = "Benin" 
country[35] = "Bermuda" 
country[36] = "Bhutan" 
country[37] = "Bolivia" 
country[38] = "Bonaire" 
country[39] = "Bosnia Hercegovina" 
country[40] = "Botswana" 
country[41] = "Brazil Acre" 
country[42] = "Brazil Atlantic Islands" 
country[43] = "Brazil East" 
country[44] = "Brazil West" 
country[45] = "British Virgin Islands" 
country[46] = "Brunei" 
country[47] = "Bulgaria" 
country[48] = "Burkina Faso" 
country[49] = "Burundi" 
 
	// Countries that start with the letter C 
country[50] = "Cambodia" 
country[51] = "Cameroon" 
country[52] = "Canada Central" 
country[53] = "Canada Eastern" 
country[54] = "Canada Mountain" 
country[55] = "Canada Yukon & Pacific" 
country[56] = "Canada Atlantic" 
country[57] = "Canada Newfoundland" 
country[58] = "Canary Islands" 
country[59] = "Canton Enderbury Islands" 
country[60] = "Cape Verde" 
country[61] = "Caroline Islands" 
country[62] = "Cayman Islands" 
country[63] = "Central African Republic" 
country[64] = "Chad" 
country[65] = "Channel Islands" 
country[66] = "Chatham Island" 
country[67] = "Chile" 
country[68] = "China People's Republic" 
country[69] = "Christmas Islands" 
country[70] = "Cocos (Keeling) Islands" 
country[71] = "Colombia" 
country[72] = "Congo" 
country[73] = "Cook Islands" 
country[74] = "Costa Rica" 
country[75] = "Cote d'Ivoire" 
country[76] = "Croatia" 
country[77] = "Cuba" 
country[78] = "Curacao" 
country[79] = "Cyprus" 
country[80] = "Czech Republic" 
 
	// Countries that start with the letter D 
country[81] = "Dahomey" 
country[82] = "Denmark" 
country[83] = "Djibouti" 
country[84] = "Dominica" 
country[85] = "Dominican Republic" 
 
	// Countries that start with the letter E; 
country[86] = "Easter Island" 
country[87] = "Ecuador" 
country[88] = "Egypt" 
country[89] = "El Salvador" 
country[90] = "England" 
country[91] = "Eguitotial Guinea" 
country[92] = "Eritrea" 
country[93] = "Estonia" 
country[94] = "Ethiopia" 
 
 	// Countries that start with the letter F; 
country[95] = "Falkland Islands" 
country[96] = "Faroe Islands" 
country[97] = "Fiji" 
country[98] = "Finland" 
country[99] = "France" 
country[100] = "French Guiana" 
country[101] = "French Polynesia" 
 
	// Countries that start  with the letter G; 
country[102] = "Gabon" 
country[103] = "Galapagos Islands" 
country[104] = "Gambia" 
country[105] = "Gambier Island" 
country[106] = "Georgia" 
country[107] = "Germany" 
country[108] = "Ghana" 
country[109] = "Gibraltar" 
country[110] = "Greece" 
country[111] = "Greenland" 
country[112] = "Greenland Thule" 
country[113] = "Greenland Scoresbysun" 
country[114] = "Grenada" 
country[115] = "Grenadines" 
country[116] = "Guadeloupe" 
country[117] = "Guam" 
country[118] = "Guatemala" 
country[119] = "Guinea" 
country[120] = "Guinea Bissau" 
country[121] = "Guyana" 
 
	// Countries that start with the letter H; 
country[122] = "Haiti" 
country[123] = "Honduras" 
country[124] = "Hong Kong" 
country[125] = "Hungary" 
 
	// Countries that start with the letter I; 
country[126] = "Iceland" 
country[127] = "India" 
country[128] = "Indonesia Central" 
country[129] = "Indonesia East" 
country[130] = "Indonesia West" 
country[131] = "Iran" 
country[132] = "Iraq" 
country[133] = "Republic of Ireland" 
country[134] = "Israel" 
country[135] = "Italy" 
 
	// Countries that start with the letter J; 
country[136] = "Jamaica" 
country[137] = "Japan" 
country[138] = "Johnston Island" 
country[139] = "Jordan" 
 
	// Countries that start with the letter K; 
country[140] = "Kazakhstan" 
country[141] = "Kenya" 
country[142] = "Kiribati" 
country[143] = "Korea Dem Republic of" 
country[144] = "Korea Republic of" 
country[145] = "Kusaie" 
country[146] = "Kuwait" 
country[147] = "Kwajalein" 
country[148] = "Kyrgyzstan" 
 
	// Countries that start with the letter L; 
country[149] = "Laos" 
country[150] = "Latvia" 
country[151] = "Lebanon" 
country[152] = "Leeward Islands" 
country[153] = "Lesotho" 
country[154] = "Liberia" 
country[155] = "Libya" 
country[156] = "Lithuania" 
country[157] = "Luxembourg" 
 
	// Countries that start with the letter M; 
country[158] = "Macedonia" 
country[159] = "Madagascar" 
country[160] = "Madeira" 
country[161] = "Malawi" 
country[162] = "Malaysia" 
country[163] = "Maldives" 
country[164] = "Mali" 
country[165] = "Mallorca Islands" 
country[166] = "Malta" 
country[167] = "Mariana Islands" 
country[168] = "Marquesas Islands" 
country[169] = "Marshall Islands" 
country[170] = "Martinique" 
country[171] = "Mauritania" 
country[172] = "Mauritius" 
country[173] = "Mayotte" 
country[174] = "Melille" 
country[175] = "Mexico" 
country[176] = "Mexico Baja Calif Norte" 
country[177] = "Mexico Nayarit" 
country[178] = "Mexico Sinaloa" 
country[179] = "Mexico Sonora" 
country[180] = "Midway Island" 
country[181] = "Moldova" 
country[182] = "Molodivian Rep Pridnestrovye" 
country[183] = "Monaco" 
country[184] = "Mongolia" 
country[185] = "Morocco" 
country[186] = "Mozambique" 
country[187] = "Myanmar" 
 
	// Countries that start with the letter N; 
country[188] = "Namibia" 
country[189] = "Nauru Republic of:" 
country[190] = "Nepal" 
country[191] = "Netherlands" 
country[192] = "Nethrelands Antilles" 
country[193] = "Nevis Montserrat" 
country[194] = "New Caledonia" 
country[195] = "New Hebrides" 
country[196] = "New Zealand" 
country[197] = "Nicaragua" 
country[198] = "Niger" 
country[199] = "Nigeria" 
country[200] = "Niue Island" 
country[201] = "Norfolk Island" 
country[202] = "Nothern Ireland" 
country[203] = "Northern Mariana Islands" 
country[204] = "Norway" 
 
	// Countries that start with the letter O; 
country[205] = "Oman" 
 
	// Countries that start with the letter P; 
country[206] = "Pakistan" 
country[207] = "Palau" 
country[208] = "Panama" 
country[209] = "Papua New Quinea" 
country[210] = "Paraguay" 
country[211] = "Peru" 
country[212] = "Philippines" 
country[213] = "Pingelap" 
country[214] = "Poland" 
country[215] = "Ponape Island" 
country[216] = "Portugal" 
country[217] = "Principe Island" 
country[218] = "Puerto Rico" 
 
	// Countries that start with the letter Q; 
country[219] = "Qatar" 
 
	// Countries that start with the letter R; 
country[220] = "Reunion" 
country[221] = "Romania" 
country[222] = "Russian Federation zone eight:" 
country[223] = "Russian Federation zone eleven:" 
country[224] = "Russian Federation zone five:" 
country[225] = "Russian Federation zone four:" 
country[226] = "Russian Federation zone nine:" 
country[227] = "Russian Federation zone one:" 
country[228] = "Russian Federation zone seven:" 
country[229] = "Russian Federation zone six:" 
country[230] = "Russian Federation zone ten:" 
country[231] = "Russian Federation zone three:" 
country[232] = "Russian Federation zone two:" 
country[233] = "Rwanda" 
 
	// Countries that start with the letter S; 
country[234] = "Saba" 
country[235] = "Samoa" 
country[236] = "San Marino" 
country[237] = "Sao Tome e Principe" 
country[238] = "Saudi Arabia" 
country[239] = "Scotland" 
country[240] = "Sengal" 
country[241] = "Seychelles" 
country[242] = "Sierra Leone" 
country[243] = "Singapore" 
country[244] = "Slovakia" 
country[245] = "Slovenia" 
country[246] = "Society Island" 
country[247] = "Solomon Island" 
country[248] = "Somalia" 
country[249] = "South Africa" 
country[250] = "Spain" 
country[251] = "Sri Lanka" 
country[252] = "St Christopher" 
country[253] = "St Croix" 
country[254] = "St Helena" 
country[255] = "St John" 
country[256] = "St Kitts Nevis" 
country[257] = "St Lucia" 
country[258] = "St Maarten" 
country[259] = "St Pierre & Miquelon" 
country[260] = "St Thomas" 
country[261] = "St Vincent" 
country[262] = "Sudan" 
country[263] = "Suriname" 
country[264] = "Swaziland" 
country[265] = "Sweden" 
country[266] = "Switzerland" 
country[267] = "Syria" 
 
	// Countries that start with the letter T; 
country[268] = "Tahiti" 
country[269] = "Taiwan" 
country[270] = "Tajikistan" 
country[271] = "Tanzania" 
country[272] = "Thailand" 
country[273] = "Togo" 
country[274] = "Tonga" 
country[275] = "Trinidad and Tobago" 
country[276] = "Tuamotu Island" 
country[277] = "Tubuai Island" 
country[278] = "Tunisia" 
country[279] = "Turkey" 
country[280] = "Turkmenistan" 
country[281] = "Turks and Caicos Islands" 
country[282] = "Tuvalu" 
 
	// Countries that start with the letter U; 
country[283] = "Uganda" 
country[284] = "Ukraine" 
country[285] = "United Arab Emirates" 
country[286] = "United Kingdom" 
country[287] = "USA Central" 
country[288] = "USA Eastern" 
country[289] = "USA Mountain" 
country[290] = "USA Arizona" 
country[291] = "USA Indiana East" 
country[292] = "USA Pacific" 
country[293] = "USA Alaska" 
country[294] = "USA Aleutian" 
country[295] = "USA Hawaii" 
country[296] = "Uruguay" 
country[297] = "Uzbekistan" 
 
	// Countries that start with the letter V; 
country[298] = "Vanuatu" 
country[299] = "Vatican City" 
country[300] = "Venezuala" 
country[301] = "Vietnam" 
country[302] = "Virgin Islands" 
 
	// Countries that start with the letter W; 
country[303] = "Wake Island" 
country[304] = "Wales" 
country[305] = "Wallis and Futuna Islands" 
country[306] = "Windward Islands" 
 
	// Countries that start with the letter Y; 
country[307] = "Yemen" 
country[308] = "Yugoslavia" 
 
	// Countries that start with the letter Z; 
country[309] = "Zaire Kasai" 
country[310] = "Zaire Kinshasa Mbandaka" 
country[311] = "Zaire Haut Zaire" 
country[312] = "Zaire Kivu" 
country[313] = "Zaire Shaba" 
country[314] = "Zambia" 

country[315] = "Zimbabwe" 
 

 
 

 
