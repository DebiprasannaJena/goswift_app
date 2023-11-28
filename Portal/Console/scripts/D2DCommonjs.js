function isValidDate(year, month, day) {

   if (month == 2) {
      if (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) {
         if (day > 29) {
            return false;
         }
      }
      else if (day > 28) {
         return false;
      }
   }
   else if (month == 4 || month == 6 || month == 9 || month == 11) {
      if (day > 30) {
         return false;
      }
   }

   return true;

}
function openCal(selDate,selMonth,selYear,frmName)
{
	var objFrm = eval("document." + frmName);
	var D = eval("document." + frmName + "." + selDate );
	var M = eval("document." + frmName + "." + selMonth );
	var Y = eval("document." + frmName + "." + selYear );
	var strMonthValue = M.options[M.selectedIndex].value;
	var strYearValue = Y.options[Y.selectedIndex].value;
	var strDateValue = D.options[D.selectedIndex].value;
	var strDate = "";
	if ((strMonthValue!="") && (strYearValue!="") && (strDateValue!=""))
	{
		if(isValidDate(strYearValue,strMonthValue,strDateValue))
		{
		strDate = strMonthValue + "/" + strDateValue + "/" + strYearValue;
		}
	}
	//alert(strDate);
	x = window.open("include/d2dcalendar.asp?selmonth=" +selMonth + "&selDate=" + selDate + "&selYear=" + selYear + "&frmName=" + frmName + "&dtDate=" + strDate,"cal","width=150,height=135");

}

function openInnerCal(selDate,selMonth,selYear,frmName)
{
	var objFrm = eval("document." + frmName);
	var D = eval("document." + frmName + "." + selDate );
	var M = eval("document." + frmName + "." + selMonth );
	var Y = eval("document." + frmName + "." + selYear );
	var strMonthValue = M.options[M.selectedIndex].value;
	var strYearValue = Y.options[Y.selectedIndex].value;
	var strDateValue = D.options[D.selectedIndex].value;
	var strDate = "";
	if ((strMonthValue!="") && (strYearValue!="") && (strDateValue!=""))
	{
		strDate = strMonthValue + "/" + strDateValue + "/" + strYearValue;
	}
	//alert(strDate);
	x = window.open("../include/d2dcalendar.asp?selmonth=" +selMonth + "&selDate=" + selDate + "&selYear=" + selYear + "&frmName=" + frmName + "&dtDate=" + strDate,"cal","width=150,height=135");

}

function base64(c)
{
	var theChar = 0;
	if (0 <= c && c <= 25) {
		theChar = String.fromCharCode(c + 65);
	} else if (26 <= c && c <= 51) {
		theChar = String.fromCharCode(c - 26 + 97);
	} else if (52 <= c && c <= 61) {
		theChar = String.fromCharCode(c - 52 + 48);
	} else if (c == 62) {
		theChar = '+';
	} else if( c == 63 ) {
		theChar = '/';
	} else {
		theChar = String.fromCharCode(0xFF);
	}
	return theChar;
}

function baseEncode(str) {
	var result = "";
	var i = 0;
	var sextet = 0;
	var leftovers = 0;
	var octet = 0;

	for (i=0; i < str.length; i++) {
		octet = str.charCodeAt(i);
		switch( i % 3 )
		{
			case 0:
			{
				sextet = ( octet & 0xFC ) >> 2 ;
				leftovers = octet & 0x03 ;
				break;
			}
			case 1:
			{
				sextet = ( leftovers << 4 ) | ( ( octet & 0xF0 ) >> 4 );
				leftovers = octet & 0x0F ;
				break;
			}
			case 2:
			{
				sextet = ( leftovers << 2 ) | ( ( octet & 0xC0 ) >> 6 ) ;
				leftovers = ( octet & 0x3F ) ;
				break;
			}
		}
		result = result + base64(sextet);
		if( (i % 3) == 2 )
		{
			result = result + base64(leftovers);
		} 
	}

	switch( str.length % 3 )
	{
		case 0:
		{
			break ;
		}
		case 1:
		{
			leftovers =  leftovers << 4 ;
			result = result + base64(leftovers);
			result = result + "==";
			break ;
		}
		case 2:
		{
			leftovers = leftovers << 2 ;
			result = result + base64(leftovers);
			result = result + "=";
			break ;
		}
	}
	return result.toString();
}

function convertDate(dt)
{
	var	strTemp="";
	var	strChar;
	var	date1 = new Array(3);
	var	j=0;
	var strDateTo=dt;
	var todatelen=strDateTo.length;
	for(var i=0;i<=todatelen;i++)
	{
		strChar=strDateTo.charAt(i);
		if (strChar=='-' || strChar=='')
		{
			date1[j]=strTemp;
			strTemp="";
			j=j+1;
		}
		else
		{
			strTemp=strTemp+strChar;
		}
	}
	switch(date1[1])
	{
		case	'Jan'	:	date1[1]=01;
							break;
		case	'Feb'	:	date1[1]=02;
							break;
		case	'Mar'	:	date1[1]=03;
							break;
		case	'Apr'	:	date1[1]=04;
							break;
		case	'May'	:	date1[1]=05;
							break;
		case	'Jun'	:	date1[1]=06;
							break;
		case	'Jul'	:	date1[1]=07;
							break;
		case	'Aug'	:	date1[1]=08;
							break;
		case	'Sep'	:	date1[1]=09;
							break;
		case	'Oct'	:	date1[1]=10;
							break;
		case	'Nov'	:	date1[1]=11;
							break;
		case	'Dec'	:	date1[1]=12;
							break;
	}
	var	conDate = new Date(date1[1]+"/"+date1[0]+"/"+date1[2]);
	return(conDate);

}
//-->
/****************************************************************************************
' Purpose 		    : To format date according to the calender used in the application
' Input Parameters 	: valid date
' Output Parameters : false
' Function calls 	: None
' Called by		    :
' String Table/Code   None
' Domain Name :
' Dependency		: None
'*****************************************************************************************/
function formatDate(dt)
	{
		var	strTemp="";
		var	strChar;
		var	date1 = new Array(3);
		var	j=0;
		var strDateTo=dt;
		var todatelen=strDateTo.length;
		
		for(var i=0;i<=todatelen;i++)
		{
			strChar=strDateTo.charAt(i);
			
				if (strChar=='-' || strChar==" ")
				{
					date1[j]=strTemp;
					strTemp="";
					j=j+1;
				}
				else
				{
					strTemp=strTemp+strChar;
				}
			if (strChar==" ")
			break;
		}
		
		switch(date1[1])
		{
			case	'Jan'	:	date1[1]=01;
								break;
			case	'Feb'	:	date1[1]=02;
								break;
			case	'Mar'	:	date1[1]=03;
								break;
			case	'Apr'	:	date1[1]=04;
								break;
			case	'May'	:	date1[1]=05;
								break;
			case	'Jun'	:	date1[1]=06;
								break;
			case	'Jul'	:	date1[1]=07;
								break;
			case	'Aug'	:	date1[1]=08;
								break;
			case	'Sep'	:	date1[1]=09;
								break;
			case	'Oct'	:	date1[1]=10;
								break;
			case	'Nov'	:	date1[1]=11;
								break;
			case	'Dec'	:	date1[1]=12;
								break;
		}
		var	conDate = date1[1]+"/"+date1[2]+"/"+date1[0];
		return(conDate);
	
	}

//----
/****************************************************************************************
' Purpose 		    : To convert the claim amount to 0.00 format
' Input Parameters 	: textBox element name
' Output Parameters : false
' Function calls 	: None
' Called by		    :
' String Table/Code   None
' Domain Name :
' Dependency		: None
'*****************************************************************************************/
function convertAmount(num)
{
	var 	amount=	num.value;
	var len		=	amount.length;
	var decPos	=	amount.lastIndexOf(".");
	
	 numAfterDec	=	amount.substr(decPos+1,len);
	 numBeforeDec	=	new Number(amount.substr(0,decPos));
	 if (amount!="0.00" && isNaN(amount))
	{	num.value	=	"0.00";
		return;
	}	
	 
	 if (numAfterDec.length==1)
		numAfterDec	=	numAfterDec+"0";
	 if (decPos<0)
	 {
		num.value	=	new Number(amount)+".00";
	 }
	 else 
	 {
		if (numAfterDec!="")
			num.value	=	numBeforeDec+"."+numAfterDec.substr(0,2);
		else
			num.value	=	numBeforeDec+".00";
	 }	
}
//-->
/****************************************************************************************
' Purpose 		    : To check valid number
' Input Parameters 	: textBox element name
' Output Parameters : false
' Function calls 	: None
' Called by		    :
' String Table/Code   None
' Domain Name :
' Dependency		: None
'*****************************************************************************************/
function isValidNumber(num)
{
	if (isNaN(num.value))
	{
		alert("Please enter numeric values");
		num.value="";
		num.focus();
		return false;
	}
}
	
	/****************************************************************************************
	' Purpose 		    : To open a Centered Pop-Up Window 
	' Input Parameters 	: myPage	==	The URL of the page to opened in the pop-up
	'					  myname	==	Name of the Pop-Up window
						  w,h,Features	==	width of the window,height of the window
						  					,features of the window ie, menubars=yes,resizeable=yes etc.
	' Output Parameters : None
	' Function calls 	: None
	' Called by		    :
	' String Table/Code   None
	' Domain Name :
	' Dependency		: None
	'*****************************************************************************************/	
	function popUpWindow(mypage,myname,w,h,features) 
	{
	  var winl = (screen.width-w)/2;
	  var wint = (screen.height-h)/2;
	  if (winl < 0) winl = 0;
	  if (wint < 0) wint = 0;
	  var settings = 'height=' + h + ',';
	  settings += 'width=' + w + ',';
	  settings += 'top=' + wint + ',';
	  settings += 'left=' + winl + ',';
	  settings += features;
	  win = window.open(mypage,myname,settings);
	  win.window.focus();
	}
	/****************************************************************************************
	' Purpose 		    : To check blank fields
	' Input Parameters 	: textBox element name
	' Output Parameters : false
	' Function calls 	: None
	' Called by		    :
	' String Table/Code   None
	' Domain Name :
	' Dependency		: None
	'*****************************************************************************************/
	function isBlankField(eleName,label)
	{
		if (eleName.value=="")
		{
			var eleType	=	eleName.type;
			if (eleType=="text")
			alert("Please enter "+label);
			else
			alert("Please select "+label);
			
			eleName.focus();
			return false;
		}
	}
	/****************************************************************************************
	' Purpose 		    : To check upload file
	' Input Parameters 	: textBox element name
	' Output Parameters : false
	' Function calls 	: None
	' Called by		    :
	' String Table/Code   None
	' Domain Name :
	' Dependency		: None
	'*****************************************************************************************/
	function isValidFile(eleName,allowedList,label)
	{
		
		photo	=	eleName.value;
		photo	=	photo.toUpperCase();
		for (var i=0;i<=allowedList.length;i++)
		{
			var pos	=	photo.lastIndexOf(allowedList[i]);
			if (pos>0 )
			break;
		}
		
		if (pos<0 && photo!="")
		{
			alert("Please upload "+label+" only");
			eleName.select();
			eleName.focus();
			return false;
		}
	}
	/****************************************************************************************
	' Purpose 		    : To check length of the  field
	' Input Parameters 	: textBox element name,max length,description of the textbox name
	' Output Parameters : false
	' Function calls 	: None
	' Called by		    :
	' String Table/Code   None
	' Domain Name :
	' Dependency		: None
	'*****************************************************************************************/
	function isVaildLength(eleName,maxLen,label)
	{
		var eleValue	=eleName.value
		if (eleValue.length>maxLen)
		{
			alert(label+" must be within "+maxLen+" characters");
			eleName.focus();
			return false;
		}
	}
	
	/****************************************************************************************
	' Purpose 		    : To check valid Entry(First character not number and no single quotes )
	' Input Parameters 	: textBox element name, Element Description
	' Output Parameters : false
	' Function calls 	: None
	' Called by		    :
	' String Table/Code   None
	' Domain Name :
	' Dependency		: None
	'*****************************************************************************************/
	function isValidEntry(eleName,label)
	{
		var str = eleName.value;
		if((str.substring(0,1)<"a" || str.substring(0,1)>"z") && (str.substring(0,1)<"A" || str.substring(0,1)>"Z"))
		{
			alert(""+label+" should begin with an alphabet.");
			eleName.focus();
			return false;
		}
		str1	=	eleName.value
		for (var i = 0; i < str1.length; i++) 
		{
			var ch = str1.substring(i, i + 1);
			if (ch=="'") 
			{
				alert("Single quote is not allowed in "+label);
				eleName.focus();
				return false;
			}
		}
	}
	/****************************************************************************************
	' Purpose 		    : To check valid Entry(First character not number , no single quotes and no special characters )
	' Input Parameters 	: textBox element name, Element Description
	' Output Parameters : false
	' Function calls 	: None
	' Called by		    :
	' String Table/Code   None
	' Domain Name :
	' Dependency		: None
	'*****************************************************************************************/
	function isValidLoginId(eleName,label)
	{
		var str = eleName.value;
		if((str.substring(0,1)<"a" || str.substring(0,1)>"z") && (str.substring(0,1)<"A" || str.substring(0,1)>"Z"))
		{
			alert(""+label+" should begin with an alphabet.");
			eleName.focus();
			return false;
		}
		
		for (var i = 0; i < str.length; i++) 
		{
			var ch = str.substring(i, i + 1);
			if (ch=="'") 
			{
				alert("Single quote is not allowed in "+label);
				eleName.focus();
				return false;
			}
		}
		for (var i = 1; i < str.length; i++) 
		{
			var ch = str.substring(i, i + 1);
			if ( ((ch < "a" || "z" < ch) && (ch < "A" || "Z" < ch)) && (ch < "0" || "9" < ch) && (ch != '_')) 
			{
				alert("Special cahracters not allowed in "+label);
				eleName.focus();
				return false;
			}
		}
	}
	/****************************************************************************************
	' Purpose 		    : To check for single quotes )
	' Input Parameters 	: textBox element name
	' Output Parameters : false
	' Function calls 	: None
	' Called by		    :
	' String Table/Code   None
	' Domain Name :
	' Dependency		: None
	'*****************************************************************************************/	
	function checkSingleQuote(txtName,label)
		{
			str1	=	txtName.value
			for (var i = 0; i < str1.length; i++) 
			{
				var ch = str1.substring(i, i + 1);
				if (ch=="'") 
				{
					alert("Single quote is not allowed in "+label);
					txtName.focus();
					return false;
				}
			}
		}
	/****************************************************************************************
	' Purpose 		    : validates email id
	' Input Parameters 	: textBox element name
	' Output Parameters : false
	' Function calls 	: None
	' Called by		    :
	' String Table/Code   None
	' Domain Name :
	' Dependency		: None
	'*****************************************************************************************/	
		function isValidEmail(name,label)
		{
		//---------------------E-mail-----------------------------   
		   
			var verify=999;
			var email_value,temp_value,dot_val,dot_str,temp_length,temp_string;
			email_value=new String();
			temp_value=new String();
			dot_val=new String("@.");
			dot_str=new String(".");
			var   score_str=new String("~`!#$%^&*)(|}?></*; :',\+=}][{");
			email_value=name.value
			for(temp_length=0;temp_length<score_str.length;temp_length++){
				 temp_string=score_str.charAt(temp_length);
					if(email_value.indexOf(temp_string.toString()) != -1)
						verify=-111;}
						
			if(email_value.length < 7)
				{verify=-111;}
		   
			else 
				if(email_value.indexOf("@")==-1)
					{verify=-111;}
				else 
					if(email_value.indexOf("@")==0)
						{verify=-111;}
			
					else 
						if(email_value.indexOf(".")==-1)
							{verify=-111;}
						
						else  
							if(email_value.indexOf("@")==(email_value.length-1))
								{verify=-111;}
				 
							else 
								if(email_value.indexOf(dot_str.toString())==(email_value.length-1))
									{verify=-111;}
								 else 
									{temp_value=email_value.substr(email_value.indexOf("@")+1);
									if(temp_value.indexOf("@") != -1)
										{verify=-111;}
									else  
										if(email_value.indexOf(dot_val.toString())!= -1)
											{verify=-111;}
										else  
											{temp_value=email_value.substr((email_value.length-3));
											if(temp_value.indexOf("@")!=-1)
												{verify=-111;}
												else {temp_value=email_value.substr(email_value.indexOf("@")+1);
													if((temp_value.length - temp_value.indexOf(dot_str.toString())) <2)
														{verify=-111;}
									 }}}
				 
		   if(verify==-111)
		   {
				
				verify=0;
				alert("Invalid "+label);
				name.select();   
				name.focus();
				return false;
		   }
		}
		/****************************************************************************************
		' Purpose 		    : Validates Telephone Number
		' Input Parameters 	: textBox element name, Element Description
		' Output Parameters : false
		' Function calls 	: None
		' Called by		    :
		' String Table/Code   None
		' Domain Name :
		' Dependency		: None
		'*****************************************************************************************/
		function isValidTelNo(eleName,label)
		{
			var checkOK = "+0123456789-,() ";
			var checkStr = eleName.value;
			var allValid = true;
			var decPoints = 0;
			var allNum = "";
			for (i = 0;  i < checkStr.length;  i++)
			{
				ch = checkStr.charAt(i);
				for (j = 0;  j < checkOK.length;  j++)
				if (ch == checkOK.charAt(j))
				break;
				if (j == checkOK.length)
				{
				allValid = false;
				break;
				}
				if (ch != ",")
				allNum += ch;
			}
			
			if (!allValid)
			{
				alert("Please enter only digit characters in "+label);
				eleName.focus();
				return (false);
			}
			else
			{
				return (true)
			}
		}
		
		function pubChangeClass(eleName,className)
		{
			//var	dtName	=	eval("document.form1.txtDateFrom"+intCtr);
			eleName.className	=	className;
		}
		/*
		function convertUniCode(frmName,frmName)
		{
			var E = eval("document." + frmName + "." + frmName );
			var strValue = E.value;
			q='';
			for(i=0; i<strValue.length; i++) 
			  {
				j=strValue.charCodeAt(i);
				//alert(j);
				if (j==48)	
				q+='&#1632'
				else if(j==49)
				q+='&#1633'
				else if(j==50)
				q+='&#1634'
				else if(j==51)
				q+='&#1635'
				else if(j==52)
				q+='&#1636'
				else if(j==53)
				q+='&#1637'
				else if(j==54)
				q+='&#1638'
				else if(j==55)
				q+='&#1639'
				else if(j==56)
				q+='&#1640'
				else if(j==57)
				q+='&#1641'
				else
				{
				//q+=(j==38)?'&amp;':(j<128)?strValue.charAt(i):'&#'+j+';';
				q+=strValue.charAt(i)
				}
				}
				E.value = q;
		}
		*/
		function pubCheckCodes(cCodeEle,aCodeEle,telNoEle,label)
		{
		if (telNoEle.value =="") 
		{
			return true;
		}
		if (aCodeEle.value=="" && cCodeEle.value=="")
		{
			alert("Please enter the COUNTRY code and AREA code for "+label+" tel. no.");
			aCodeEle.focus();
			return false;
		}
		if (cCodeEle.value=="")
		{
			alert("Please enter the COUNTRY code for "+label+" tel. no.");
			cCodeEle.focus();
			return false;
		}
		if (aCodeEle.value=="")
		{
			alert("Please enter the AREA code for "+label+" tel. no.");
			aCodeEle.focus();
			return false;
		}
		return true;
		}
	
	function resetPhoto(iHeightMax,iWidthMax,imgId)
	{
	
		var iHeight;
		var iWidth;
		
		iHeight = document.getElementById(imgId).height;
		iWidth = document.getElementById(imgId).width;
		if (iHeight > iHeightMax && iWidth <iWidthMax)
		{
			document.getElementById(imgId).height = iHeightMax
		}
		else if(iHeight < iHeightMax && iWidth >iWidthMax)
		{
			document.getElementById(imgId).width = iWidthMax
		}
		else if(iHeight > iHeightMax && iWidth >iWidthMax)
		{
			if(iHeight>iWidth)
			document.getElementById(imgId).height = iHeightMax
			else
			document.getElementById(imgId).width = iWidthMax
		}
		
	
	}
	/****************************************************************************************
	' Purpose 		    : To show preview of the image to be uploaded
	' Input Parameters 	: textBox element name, 
	'					  Id of the image tag where the preview is to be shown
	' Output Parameters : false
	' Function calls 	: None
	' Called by		    :
	' String Table/Code   None
	' Domain Name :
	' Dependency		: None
	'*****************************************************************************************/
	function previewImg(what,imgId)
	{
	  var source=what.value;
	  var outImage=imgId;
	  globalPic=new Image();
	  globalPic.src=source;
	  var field=document.getElementById(outImage);
	  field.src=globalPic.src;
	}
	
	function selectCombo(frm,cmbName,selValue)
	{
		var	objFrmSC		=	eval("document."+frm);
		
		var comboName	=	eval("objFrmSC."+cmbName);
		var ID			=	selValue;
		var	cmblength	=	comboName.length;
		
		for (var i=1;i<cmblength;i++)
		{
			if	(ID==comboName.options[i].value)
			{
				comboName.options[i].selected=true;
				return;
			}
		}
	}
	
	/****************************************************************************************
' Purpose 		    : To check valid number
' Input Parameters 	: textBox element name
' Output Parameters : false
' Function calls 	: None
' Called by		    :
' String Table/Code   None
' Domain Name :
' Dependency		: None
'*****************************************************************************************/
function isBlank(elename,msg)
{
	if (elename.value=="")
	{
		alert(msg);
		elename.focus();
		return false;
	}
}

//#################################################################################################################
	/****************************************************************************************
	' Purpose 		    : Functions for adding options in a list box from another list box and  to  remove options from the listbox
	' Input Parameters 	: frm			==	The form name
	'					  selDeptFrom	==	Source listbox name 
						  selDeptTo		==	Destination listbox name 
	' Output Parameters : None
	' Function calls 	: None
	' Called by		    :
	' String Table/Code : None
	' Domain Name :
	' Dependency		: None
	'*****************************************************************************************/	
	
	function jsAddToList(frm,selDeptFrom,selDeptTo,hidName)
	{
		if(eval("document."+frm+"."+selDeptFrom+".length")==0)
		{
		alert("No entry left for addition");
		return false;
		}
		
		if (eval("document."+frm+"."+selDeptFrom+".selectedIndex") < 0)
		{
		  alert("Please select an entry to add");
		  return false;
		}
		
		jsTransferData(frm,selDeptFrom,selDeptTo);
		UpdateHiddenField(frm,hidName,selDeptTo);
	}
	
	function jsTransferData(frm,selDeptFrom,selDeptTo)
	{
		var fromName = eval("document."+frm+"."+selDeptFrom);
		var toName	 = eval("document."+frm+"."+selDeptTo);
		
		for(i=0;i<fromName.length;i++)
		{
			if(fromName.options[i].selected)
			{
				flag=true;
				for (ctr=0;ctr<toName.length;ctr++)
				{
					if (toName.options[ctr].value==fromName.options[i].value)
					{
						flag=false;
						break
					}
				}
			
				if (flag)
				{
					  toName.options[toName.options.length] =
					  new Option(fromName[i].text, fromName[i].value, false, false);
					  fromName.options[i]=null;
					  i--;
				}  
			}
		}
	}
	
	function jsRemoveFromList(frm,selDeptFrom,selDeptTo,hidName)
	{
		if(eval("document."+frm+"."+selDeptFrom+".length")==0)
		{
		alert("No entry left to remove");
		return false;
		}
		
		if (eval("document."+frm+"."+selDeptFrom+".selectedIndex") < 0)
		{
		  alert("Please select an entry to remove");
		  return false;
		}
		
		jsTransferData(frm,selDeptFrom,selDeptTo);
		UpdateHiddenField(frm,hidName,selDeptFrom);
	}
	
	function UpdateHiddenField(frm,hidName,selFrom)
	{
		var hidGroup	=	eval("document." + frm + "." + hidName);
		hidGroup.value = "";
		a = eval("document." + frm + "." + selFrom);
		icount = a.length;
		//alert(icount);
		for (i=0;i<icount;i++)
		{
			if (hidGroup.value!="")
			{
				hidGroup.value=hidGroup.value+","+a.options[i].value;
			}
			else
			{
				hidGroup.value=a.options[i].value;
			}
		}
		
	}

	//#################################################################################################################
