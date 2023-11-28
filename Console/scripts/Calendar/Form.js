window.onerror = function(){return false;}

var new_fieldname = ""
var form_name = "";
var file_fld = 0;
var gFieldsChanged = 0;
var frmSubmit = 0;
var file_attached = 0;
var error_color = "F3F3F3"
var off_error_color = "FFFFFF"
var on_error_color = "FFF8DC"
var form_color = "FFFFFF"
var curChk = /(^\$\d{1,3}(,\d{3})*\.\d{2}$)|(^\(\$\d{1,3}(,\d{3})*\.\d{2}\)$)/;
var emlChk  = /^[a-z0-9]([a-z0-9_\-\.]*)@([a-z0-9_\-\.]*)(\.[a-z]{2,3}(\.[a-z]{2}){0,2})$/i;
var numChk  =  /(^-?\d\d*\.\d*$)|(^-?\d\d*$)|(^-?\.\d\d*$)/; 
var intChk  = /(^-?\d\d*$)/;
var tmeChk = /^([1-9]|1[0-2]):[0-5]\d(:[0-5]\d(\.\d{1,3})?)?$/;
var dteChk = /^\d{1,2}(\-|\/|\.)\d{1,2}\1\d{4}$/;

function frmValidCheck(formId,strFrm,frmObj,abRoot,flPth) {
	frmObj = document.getElementById(formId);
	var x = frmObj.elements.length;
	var message = "";
	var more_message = "";
  	for (var i = 0; i < x; i++) {
		fldObj = frmObj.elements[i];
		req = fldObj.getAttribute('required');
		val = fldObj.getAttribute('vtype');
		more_message = "";
	    	if (req > 0) {
	      		more_message += frmValidRequired(frmObj,fldObj,strFrm.reqMsg);
	      	} 
			if (val == "numeric") {
	      	  	more_message += frmValidOther(frmObj,fldObj,numChk,strFrm.numMsg);
	      	} else if (val == "date") {
	      	  	more_message += frmValidOther(frmObj,fldObj,dteChk,strFrm.dteMsg);
	      	} else if (val == "currency") {
	      	  	more_message += frmValidOther(frmObj,fldObj,curChk,strFrm.numMsg);
	      	} else if (val == "integer") {
	      	  	more_message += frmValidOther(frmObj,fldObj,intChk,strFrm.numMsg);
	      	} else if (val == "time") {
	      	  	more_message += frmValidOther(frmObj,fldObj,tmeChk,strFrm.dteMsg);
	      	} else if (val == "email") {
	      	  	more_message += frmValidOther(frmObj,fldObj,emlChk,strFrm.emlMsg);
	      	} else if (val == "range") {
	      	  	more_message += frmValidRange(frmObj,fldObj,fldObj.getAttribute('vMsg'));
	      	} else if (val == "custom") {
	      	  	more_message += frmValidOther(frmObj,fldObj,fldObj.getAttribute('vReg'),fldObj.getAttribute('vMsg'));
	      	} else if (val == "file" && file_fld == 0) {
				file_fld = 1;
				if (fldObj.value.length) file_attached = 1;
			}
	if (more_message != "" && (message == "")) {
         	message = more_message;
         	more_message="";
        } else if (more_message != "") {
        	message = message + "\n" + more_message;
         	more_message="";
      	}
    }
	if (message > "") {
		alert(strFrm.formBeginMessage+":\n\n" + message + "\n\n"+strFrm.formEndMessage+".")
   		return false;
  	} else {
   		if (file_attached == 1 && flPth.length > 0) showProgress(flPth);//loadFileCopy(abRoot);
		return true;
   	}
	if (file_attached == 1 && flPth.length > 0) showProgress(flPth);//loadFileCopy(abRoot);
	return true;
} 
 
function frmValidOther(frmObj,fldObj,expChk,msg) {
	var msg_addition = "";
 	var objRegExp = eval(expChk);
	form_field_value = trimAll(fldObj.value);
    if (form_field_value != "" && (!objRegExp.test(form_field_value))) {
    	msg_addition = fldObj.getAttribute('vlabel')+' '+msg;
	    changeColor(frmObj,fldObj,1);
   	}
 	return(msg_addition);
}
 
function frmValidRequired(frmObj,fldObj,msg) {
	changeColor(frmObj,fldObj,0);
	var form_field_type = fldObj.getAttribute('type');
    msg_addition = "";
   	var strTemp = fldObj.value
	strTemp = trimAll(strTemp);
    if(strTemp.length == 0){
    	msg_addition = fldObj.getAttribute('vlabel')+' '+msg;
  		changeColor(frmObj,fldObj,1);
    }  
	return (msg_addition);
}

function frmValidRange(frmObj,fldObj,msg) {
	changeColor(frmObj,fldObj,0);
	var form_field_range = fldObj.getAttribute('range');
    msg_addition = "";
	if (form_field_range && fldObj.value.length > 0) {
		if (form_field_range.indexOf(',') > -1) { rng = form_field_range.split(',');} //we're dealing with a list
		else if (form_field_range.indexOf('-') > -1) { rng = form_field_range.split('-');} //we're dealing with a range
		val_1 = rng[0]; val_2 = (rng[1] == 'null')?'':rng[1];
		if (rng[0].indexOf('.value') > -1) val_1 = eval(rng[0]);
		if (rng[1].indexOf('.value') > -1) val_2 = eval(rng[1]);
		form_field_value = fldObj.value;
		if (val_1 > form_field_value || (val_2.length > 2 && val_2 < form_field_value)) {
			msg_addition = msg;
	 		changeColor(frmObj,fldObj,1);
	    }  
	}
	return (msg_addition);
}
 
function changeColor(frmObj,fldObj,tog) {
	fldObj.style.backgroundColor = (tog==1)?error_color:off_error_color;
	//fldObj.style.borderColor = "red";
}

function whichRequired(formId) {
	var frmObj = document.getElementById(formId);
	for (x=0; x < frmObj.elements.length; x++) {
		fldObj = frmObj.elements[x];
		if (fldObj.getAttribute('required') > 0)
			changeColor(frmObj,fldObj,1);
	}
 }	
 
function removeCurrency( strValue ) {
  var objRegExp = /\(/;
  var strMinus = '';
  var strValue = removeCommas(strValue);
  objRegExp = /\)|\(|[,]/g;
  strValue = (strValue)? strValue.replace(objRegExp,''):'';
  if(strValue.indexOf('$') >= 0){
    strValue = strValue.substring(1, strValue.length);
  }
  return strValue;
}

function removeCommas( strValue ) {
  return strValue.replace(',','');
}

function trimAll( strValue ) {
  var objRegExp = /^(\s*)$/;
    if(objRegExp.test(strValue)) {
       strValue = strValue.replace(objRegExp, '');
       if( strValue.length == 0)
          return strValue;
    }
   //check for leading & trailing spaces
   objRegExp = /^(\s*)([\W\w]*)(\b\s*$)/;
   if(objRegExp.test(strValue)) {
       //remove leading and trailing whitespace characters
       strValue = strValue.replace(objRegExp, '$2');
    }
  return strValue;
}

function showProgress(pth) {
  strAppVersion = navigator.appVersion;
	if (navigator.userAgent.indexOf("Mac") == -1 && navigator.userAgent.indexOf("MSIE") != -1)
      window.showModelessDialog(pth,null,"dialogWidth=375px; dialogHeight:130px; center:yes");
	else 
      window.open(pth,'','width=370,height=115', true);
  return true;
}

function loadFileCopy(abRoot) {
	el = document.getElementById("FileDialog");
	el.style.pixelTop = window.event.y - 140;
	el.style.pixelLeft = window.event.x - 50;	
	parent.noDisplayElement("select");
	el.style.visibility="visible";
}

function frmRecord(fldObj) {
	if (fldObj.value != fldObj.defaultValue) {
		gFieldsChanged = gFieldsChanged+1;
	}
}