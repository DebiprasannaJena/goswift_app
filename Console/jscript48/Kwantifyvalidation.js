// JScript File
 //==========================================================================

// Declare patterns for different Regular Expression

var PatternsDict = new Object()
// matches required field
PatternsDict.requiredpat = "^((/\s+)|'')?$"
// matches white space
PatternsDict.whitespacepat = /\s+/
//===============================================================================
// Checks mandatory field
 function isRequired(Object,msg)
  {
    var strInput = trim(new String(Object.value))
    var objregExp  = new RegExp(PatternsDict.requiredpat)


   if(objregExp.test(strInput))

     {
       alert(msg)
       Object.focus()
       return false

     }

     return true

 }
 // Checks for white space

function isWhitespace(Object,msg)

 {
   var strInput   = new String(Object.value)

   var objregExp  = new RegExp(PatternsDict.whitespacepat)

   if(objregExp.test(strInput))
     {
	if (msg != null)
	    alert(msg);
	    Object.focus()
        return false
   	}
     return true 

 }
 
 function trim(strString)
{
   var strCopy = new String(strString)
   strCopy = strCopy.replace(/^\s+/,"")
   strCopy = strCopy.replace(/\s+$/, "")
   return strCopy.toString()
}

function Replace(str1, str2, str3)
 {
str1 = str1.replace(new RegExp(str2),str3);
return str1
 }
 //==============================================================================================
 var strMandatoryAlert2          = "<Field> can not be left blank!"
 var strWhiteSpace               = "White space(s) not allowed!"
 //================================================================================================
 function InputValidator()
{
this.isRequired		 		= isRequired;
this.isWhitespace	 		= isWhitespace;
this.trim			 		= trim;
this.Replace		 		= Replace;
}
//=============================================================================================
function ErrorAlert()
{
this.MandatoryAlert2			= strMandatoryAlert2;
this.whitespace                 = strWhiteSpace
 }
//============1.Function checking blank field for textboxes/areas==============================
function blankFieldValidation(Controlname,Fieldname)
  {
  
   	var objfrm=document.getElementById(Controlname);
	var objFieldname=Fieldname;
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
       objValidator.isRequired(objfrm, objValidator.Replace(objError.MandatoryAlert2,"<Field>",objFieldname))
    )
        {
	  	objError = null
		objValidator = null
		//alert ("Form has been validated successfully.")
		flag=true
        }

	objError = null
	objValidator = null
	return flag
  }
//====================2.Function checking WhiteSpaces ==============================
function WhiteSpaceValidation(Controlname,Fieldname)
  {
    var objfrm=document.getElementById(Controlname);
    var objFieldname=Fieldname;
	var flag;
	flag=false;
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
           objValidator.isWhitespace(objfrm, objError.whitespace)
    )
        {
	  	objError = null
		objValidator = null
		//alert ("Form has been validated successfully.")
		flag=true
        }
	objError = null
	objValidator = null
	return flag
  }
  
