// JScript File
// JScript File

 //==========================================================================

// Declare patterns for different Regular Expression

var PatternsDict = new Object()


// mathes USA telephone no.
PatternsDict.telpat  = /^(\d{10}|(\d{3}-\d{3}-\d{4}))?$/
// example:-325-672-6433

// mathes telephone Indian no.
PatternsDict.telpatIND  = /^((\+){1}[1-9]{1}[0-9]{0,1}[0-9]{0,1}(\s){1}[\(]{2}[1-9]{1}[0-9]{1,5}[\)]{1}[\s]{1})[1-9]{1}[0-9]{4,9}$/
// example:+91 (22) 24440444
//
// matches numeric
PatternsDict.numericpat  = "^\d*$" // Any number is allowed, but are optional

// matches white space
PatternsDict.whitespacepat = /\s+/

// matches zip code
PatternsDict.zippat = /^(\d{5}|\d{9}|(\d{5}-\d{4}))?$/
//example:-78731
//PatternsDict.zippat = "^(\d{5}|\d{9}|(\d{5}-\d{4}))?$"

// matches IP address
PatternsDict.IPpat =/^\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}$/

// matches hex number
PatternsDict.hexpat = "^([a-fA-F0-9]+)?$"

// matches any alphanumeric character,hyphen(-) or an underscore(_)
// including white space
PatternsDict.validpat = "^[a-zA-Z0-9-_]+$"

// matches required field
PatternsDict.requiredpat = "^((/\s+)|'')?$"

// matches character
 PatternsDict.charpat = /^[a-zA-Z]+$/
 
 //PatternsDict.urlpat="(?<protocol>http(s)?|ftp)://(?<server>([A-Za-z0-9-]+\.)*(?<basedomain>[A-Za-z0-9-]+\.[A-Za-z0-9]+))+((/?)(?<path>(?<dir>[A-Za-z0-9\._\-]+)(/){0,1}[A-Za-z0-9.-/]*)){0,1}"
//PatternsDict.urlpat="/http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?/"

 





// mathes email
var emailpat = /^[A-Za-z0-9\-_\.]+@+[A-Za-z0-9\-\.]+\.+[A-Za-z]{2,10}$/

// matches unsigned float
var ufloatpat = /^((\d+(\.\d*)?)|((\d*\.)?\d+))$/

// matches signed float
var sfloatpat = /^(((\+|\-)?\d+(\.\d*)?)|((\+|\-)?(\d*\.)?\d+))$/

// PatternsDict.datepat=/^([1-9]|0[1-9]|[12][1-9]|3[01])\D([1-9]|0[1-9]|1[012])\D(19[0-9][0-9]|20[0-9][0-9])$/


// End of pattern declaration
//=================================================================================


// Check for valid email format

function isEmail(Object,msg)
 {

   var strInput   = new String(Object.value)

   if (trim(strInput) == "")

     {
       return true
     }

   var objregExp  = emailpat

   if(objregExp.test(strInput))

     {
       return true

     }
     alert(msg)
     Object.focus()
     return false

 }

// Checks a character type field

function isChar(Object,msg)
 {

   var strInput = new String(Object.value)

   if (trim(strInput) == "")

     {
        return true
     }

   var objregExp  = new RegExp(PatternsDict.charpat)

   if(objregExp.test(strInput))

     {

       return true

     }

     alert(msg)
     Object.focus()
     return false

 }

// Check if field contains any character along with alplanumeric and -/_
// including white space

function isValid(Object,msg)
 {

   var strInput = new String(Object.value)
   var objregExp  = new RegExp(PatternsDict.validpat)


   if(objregExp.test(strInput))

     {

       return true

     }

     alert(msg)
     Object.focus()
     return false

 }

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
 
 //=================================

// Checks valid hexa decimal number

function isHex(Object,msg)

 {

   var strInput   = new String(Object.value)

   if (trim(strInput) == "")

     {
     
       return true
     }

   var objregExp  = new RegExp(PatternsDict.hexpat)
//alert(objregExp)
   if(objregExp.test(strInput))

     {
       return true

     }

     alert(msg)
     Object.focus()
     return false

 }

// Checks valid IP address


function isValidIP(Object,msg)
{
   var ipaddr   = new String(Object.value)

   if (trim(ipaddr) == "")

     {
       return true
     }
  
   var objregExp  = new RegExp(PatternsDict.IPpat)

   if(objregExp.test(ipaddr))

     {  
         var parts = ipaddr.split(".");
         if (parseInt(parseFloat(parts[0])) == 0)
            { 
              alert(msg);
              return false; 
            }
         for (var i=0; i<parts.length; i++) 
            {
              if (parseInt(parseFloat(parts[i])) > 255) 
                { 
                   alert(msg);
                   return false;
                }  
            }
       return true

     }
else
   {
     alert(msg)
     Object.focus()
     return false   
     
   }
}



// Checks for valid zip no.
function isUSAZip(Object,msg)

 {
   var strInput   = new String(Object.value)

   if (trim(strInput) == "")

     {
       return true
     }

   var objregExp  = new RegExp(PatternsDict.zippat)


   if(objregExp.test(strInput))

     {
       return true

     }

     alert(msg)
     Object.focus()
     return false

 }


// Checks for white space in first place

function isWhitespace1st(Object,msg)
 {
   var strInput   = new String(Object.value)
   var objregExp  = new RegExp(PatternsDict.whitespacepat)  
   if(objregExp.test(strInput))
     {
     if(strInput.charAt(0)==" ")
        {
        if (msg != null)	     
	    alert(msg);
	    Object.focus()
        return false
       }
   	}
     return true 

 }
 
 // Checks for white space every where or at any place

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

// Checks for numeric input


/*
function isNumeric(Object,msg)

 {
   var strInput   = new String(Object.value)
   var objregExp  = new RegExp(PatternsDict.numericpat)

   if(objregExp.test(strInput))

     {
       return true

     }

     alert(msg)
     Object.focus()
     return false

 }

*/
// Checks for USA telephone number

function isUSATel(Object,msg)
 {

   var strInput   = new String(Object.value)

   if (trim(strInput) == "")

     {
       return true
     }

   var objregExp  = new RegExp(PatternsDict.telpat)
    
   if(objregExp.test(strInput))

     {
       return true

     }

     alert(msg)
     Object.focus()
     return false

 }

// Checks for INDIAN telephone number

function isINDTel(Object,msg)
 {

   var strInput   = new String(Object.value)

    if (trim(strInput) == "")

     {
       return true
     } 

   var objregExp  = new RegExp(PatternsDict.telpatIND)

   if(objregExp.test(strInput))

     {
       return true
     }

     alert(msg)
     Object.focus()
     return false

 }

// Checks partial phone number

 function isFilled(Object,msg)
  {
   var strInput   = new String(Object.value)

   if (trim(strInput) == "")

     {
       return true
     }

   var objregExp  = new RegExp(PatternsDict.telpat)

   if(objregExp.test(strInput))

     {
       return true

     }

     alert(msg)
     Object.focus()
     return false
  }


// =======================================================================================
// This function is used to change any text to Uppercase text

function UpperCase(toconvert)
 {
  text      = new String(toconvert);
  toconvert = text.toUpperCase();

  return  toconvert;
 }


  // Check for numeric field

 function isNumeric(Object,length,msg)
   {
    var strInput = new String(Object.value)
    
     //if(strInput.length > 0 && !isWhitespace (Object,"White Space Not allowed"))
    if(strInput.length > 0)
      {
          if(strInput.length > length)
            {
             alert("Field must be " + length + " characters long")
            Object.focus()
            return false
            }

       for(i = 0; i < strInput.length; i++)
        {
         if(strInput.charAt(i) < '0' ||  strInput.charAt(i) > '9')
          {
           alert(msg)
           Object.focus()
           return false
          }
        }
     }
      return true
   }



 // Check whether Passwords are matched

 function isPwdMatch(pwd,cpwd,msg)
  {

    if (pwd.value != cpwd.value )
     {
       alert(msg);
       cpwd.focus()
       return false
     }
    else
      return true
  }



 // Check if the field is of min length

function isMinlen(Object,len,msg)
 {
   strInput = trim(new String(Object.value))
  sLength  = strInput.length
   if(sLength < len)
   {
    alert(msg)
    Object.focus()
    return false
   }
  return true
 }

// Check if the maximum length of the field

function isMaxlen(Object,len,msg)
 {
  strInput = trim(new String(Object.value))
  sLength  = strInput.length
  if(sLength > len)
   {
    alert(msg)
    Object.focus()
    return false
   }
  return true
 }

 // Check if the field is of fixed length

function isReslen(Object,len,msg)
 {
   strInput = trim(new String(Object.value))
  sLength  = strInput.length
   if(sLength != len)
   {
    alert(msg)
    Object.focus()
    return false
   }
  return true
 }

// Check if two fields are indentical

 function isSimilar(Object1,Object2,msg)
  {
   strInput1 = new String(Object1.value)
   strInput2 = new String(Object2.value)

   if(strInput1.valueOf() == strInput2.valueOf())
       {
     alert(msg)
     Object2.focus()
     return false
    }

    return true
  }

// Check if two or more email ids are indentical

 function isEmailSimilar(str1,str2,str3,str4,str5,msg)
  {

   strInput1 = new String(str1.value)
   strInput2 = new String(str2.value)
   strInput3 = new String(str3.value)
   strInput4 = new String(str4.value)
   strInput5 = new String(str5.value)


     var fstr,sstr;
     for(i=1;i<=5;i++)
      {
      fstr = new String(eval("strInput" + i))

        for(j=i+1;j<=5;j++)
        {

         sstr = new String(eval("strInput" + j))

        if(fstr.valueOf() != "" && sstr.valueOf() != "")
          {

			if(fstr.valueOf() == sstr.valueOf())
              {
                alert(msg)
if (j == 1)
str1.focus()
else if (j==2)
str2.focus()
else if (j==3)
str3.focus()
else if (j==4)
str4.focus()
else if (j==5)
str5.focus()
               
                return false
		      }
		  }
	    }
      }
     return true
  }
  
  
  //==Check whether Pwd Question & Ans. are entered..

  function isValidQA(Object1,Object2,msg1,msg2)
  {
//	Object = new String(val1.value)
//	str2 = new String(val2.value)
var str1=new String(Object1.value)
var str2=new String(Object2.value)

	//if(Object.length > 0 || str2.length > 0)
	//{
		if(str1.length == 0)
		{
			alert(msg1)
			Object1.focus()
			return false
		}
		else if(str2.length == 0)
		{
			alert(msg2)
			Object2.focus()
			return false
		}
		else
			return true
	//}return true
  } //End of ffunction isValidQA
  
  

function trim(strString)
{
   var strCopy = new String(strString)
   strCopy = strCopy.replace(/^\s+/,"")
   strCopy = strCopy.replace(/\s+$/, "")
   return strCopy.toString()
}


//checks number of days in a month, and leap year related validations.
function isDate(Day,Mon,Yr)
{
  var rem;
  var dateOk = true;
    if(Yr.length != 4)
  {
  alert("PLease Check! The Year you entered is Invalid.It should be in 'YYYY' format!")
  dateOk = false;
   }
  if(Day>31)
  {
  alert("PLease Check! The Day you entered is Invalid.")
   dateOk = false;
  }
  if(Mon.length !=2)
    {
    alert("PLease Check! The Month you entered is Invalid.It should be in 'MM' format!")
     dateOk = false;
    }
    if(Mon>12||Mon<01)
    {
    alert("PLease Check! The Month you entered is Invalid.")
     dateOk = false;
    }
else
{
  if (Day == 31)
  {
    if ((Mon == "02") || (Mon == "04") || (Mon == "06") || (Mon == "09") || (Mon == "11"))
    	  {
    	   dateOk = false;
    	   alert("PLease Check! The month you entered doesn't have 31 days.")
    	   }
  }
  else
  {
     if ((Day > 29) && (Mon == "02"))
     {
         dateOk = false;
         alert("PLease Check! The month you entered doesn't have " + Day + " days.")
     }
	 else
       {
        if ((Day == 29) && (Mon == "02"))
          {
           rem = Yr % 400;
           if (rem == 0)
              dateOk = true;
           else
           {
             rem = Yr % 100;
             if (rem == 0)
                dateOk = true;
             else
           	 {
               rem = Yr % 4;
               if (rem == 0)
               dateOk = true;
              // alert("February can have 29 days in a leap year only. Please select a leap year")
              else
                {
                 dateOk = false;
                alert("February can have 29 days in a leap year only. Please select a leap year")
                }
             }
             
           }
     }
  }
 }
 return dateOk
}  //end of month check condition
} // end function



 function isDateinMMM(day, month, year)

{

  var isValid = true;

  var enteredDate = new Date(day + " " + month + " " + year);

  if (enteredDate.getDate() != day)

  {

    isValid = false;

  }

  return isValid;

}


// Check for valid signed float format

function isSignedFloat(Object,msg)
 {

   var strInput   = new String(Object.value)

   if (trim(strInput) == "")

    {
       return true
    }

   var objregExp  = sfloatpat

   if(objregExp.test(strInput))

     {
       return true

     }
     alert(msg)
     Object.focus()
     return false

 }

// Check for valid unsigned float format

function isUnSignedFloat(Object,msg)
 {

   var strInput   = new String(Object.value)

   if (trim(strInput) == "")

     {
          return true
     }

   var objregExp  = ufloatpat

   if(objregExp.test(strInput))

     {
       return true

     }
     alert(msg)
     Object.focus()
     return false

 }


function Replace(str1, str2, str3)
 {
str1 = str1.replace(new RegExp(str2),str3);
return str1
 }

function isNumInRange(Object, low, high,msg)

// Results: alert if textBox does not contain an integer & clears contents
{

   var strInput   = new String(Object.value)

   if (trim(strInput) == "")

    {
       return true
    }

   strInput = parseFloat(strInput);

   Object.value = strInput

   if(isNaN(strInput))
     {
       Object.value = 0
       //return true
     }



   if (isNaN(high) && !isNaN(strInput))
      {

        if ((high.toUpperCase() == "LT") || (high == "<"))
   	    Operator = "<"
   	if((high.toUpperCase() == "LE") || (high == "<="))
   	    Operator = "<="
   	if((high.toUpperCase() == "GT") || (high == ">"))
   	    Operator = ">"
   	if((high.toUpperCase() == "GE") || (high == ">="))
   	    Operator = ">="
   	else if((high.toUpperCase() == "EQ") || (high == "="))
   	    Operator = "=="

   	if (!eval(strInput + 	" " + Operator + " " + low))
   	  {
   	      alert(msg+ " i.e. in between " + "" + low + "" + "-" + "" +high+ "")
   	     Object.focus()
   	     return false
    	  }

    	return true

      }


   if ( isNaN(strInput) || (strInput < low) || (strInput > high))

    {
    alert(msg+ " i.e. in between " + "" + low + "" + "-" + "" +high+ "")
    
	Object.focus()
	return false

    }

    return true
}


function isIntInRange(Object, low, high,msg)

// Results: alert if textBox does not contain an integer in range & clears
{
    var strInput   = new String(Object.value)
    var Operator

    if (trim(strInput) == "")

     {
        return true
     }


    strInput = parseInt(strInput,10);

    Object.value = strInput

    if(isNaN(strInput))
       {
          Object.value = 0
	  //return true
       }



    if (isNaN(high) && !isNaN(strInput))
      {

	if ((high.toUpperCase() == "LT") || (high == "<"))
	    Operator = "<"
	if((high.toUpperCase() == "LE") || (high == "<="))
	    Operator = "<="
	if((high.toUpperCase() == "GT") || (high == ">"))
	    Operator = ">"
	if((high.toUpperCase() == "GE") || (high == ">="))
	    Operator = ">="
	else if((high.toUpperCase() == "EQ") || (high == "="))
	    Operator = "=="

	if (!eval(strInput + 	" " + Operator + " " + low))
	  {
	     alert(msg+ " i.e. in between " + "" + low + "" + "-" + "" +high+ "")
	     Object.focus()
	     return false
 	  }


 	 return true

      }

    if ( isNaN(strInput) || (strInput % 1 != 0) || (strInput < low) || (strInput > high))
       {
	 alert(msg+ " i.e. in between " + "" + low + "" + "-" + "" +high+ "")

	 Object.focus()
	 return false

       }

    return true
}

//Checks for multiple checked checkboxes

function isMultipleChecked(Object,MsgOption,msg)
{
    var intNoOfLines = 0;
    var NumChecked;

    NumChecked = 0;

    if(Object)
     {
       intNoOfLines = Object.length;
     }

    if(isNaN(intNoOfLines))
     {
       intNoOfLines = 1;

     }

    if(intNoOfLines == 1)
    {
       if (Object.checked)
	{
	  NumChecked++;
	}
    }
   else
    {
     for(i=0;i<intNoOfLines;i++)
      {

       if (Object[i].checked)
	{

	 NumChecked++;

	}
      }
    }

    if(NumChecked > 1)
       {
         if(MsgOption)
            {

	      if(trim(msg) != "" || isMultipleChecked.arguments.length > 2)
       		alert(msg);
    	    }

         return true;
       }

     if(!MsgOption)
	 {

	   if(trim(msg) != "" && isMultipleChecked.arguments.length > 2)
	   alert(msg);
    	 }

    return false;

}
//Checks at lease one checkbox/radio button has been checked or not

function isAtleastOneChecked(Object,msg)
{

	  var intNoOfLines = 0;
	  var boolChecked  = false;

	  if(Object)
	   {
	      intNoOfLines = Object.length;
	   }

	  if (isNaN(intNoOfLines))
	   {
	      intNoOfLines = 1;

	   }


	   if(intNoOfLines == 1)
	    {
	       if (Object.checked)
	        {
	          boolChecked = true;
	        }
	    }
	   else
	    {
	     for(i=0;i<intNoOfLines;i++)
	      {

	       if (Object[i].checked)
	        {

	          boolChecked = true;
	          break;

	        }
	      }
	    }

	   if(boolChecked)
	     {

	       return true;
	     }

	   alert(msg);
	   return false;
}

//==== Checks and unchecks all the check boxes

function selectsAll(Object1,Object2)
{

  var intNoOfItems = 0;

  if(Object2)
   {
	intNoOfItems = Object2.length;
   }

  if (isNaN(intNoOfItems))
   {
      intNoOfItems = 1;

   }

  if(Object1.checked)
   {
	 if(intNoOfItems == 1)
	 {
		Object2.checked = true;
	 }
	else
	 {
		for (i=0;i<intNoOfItems;i++)
		{
		 Object2[i].checked = true;
		}
   	 }

   	Object1.checked = true;



   }
  else
    {
	  if(intNoOfItems == 1)
	   {
            Object2.checked = false;
	   }
	  else
	  {
	    for (i=0;i<intNoOfItems;i++)
	     {
	       Object2[i].checked = false;
	     }
	  }

         Object1.checked = false;



    }
}

function chkAllCheckBoxes(Object1,Object2)
{

	var TB=TO=0;
	var intNoOfItems = 0;

	  if(Object2)
	   {
		intNoOfItems = Object2.length;
	   }

	  if (isNaN(intNoOfItems))
	   {
	      intNoOfItems = 1;

   	   }

	for (var i=0;i<intNoOfItems;i++)
	{
	   TB++;

	   if(intNoOfItems == 1)
	     {
	       if(Object2.checked)
	          TO++;
	     }
	   else
	     {
	       if(Object2[i].checked)
		TO++;
	     }
	}

	if (TO==TB)
		Object1.checked=true;
	else
		Object1.checked=false;
}
//check dropdown is selected or not
function isSelectDropDown(Object,msg)
  {  
    if( Object == false || Object.selectedIndex== 0 )
    {
     alert(msg)
     Object.focus()
     return false
    }
     return true
 }
 //checks Single Quote
function isSingleQuote(Object,msg)
  {
    var str1 = trim(new String(Object.value))
    for (var i = 0; i < str1.length; i++) 
	{
		var ch = str1.substring(i, i + 1);
		if (ch=="'") 
		{
			alert(msg);			
			Object.focus();
			return false;
		}
	}
	return true;
    }
    
//checks valid URL
function isValidURL(Object,msg)
  {
    var str1 = trim(new String(Object.value))
     //if ((str2.value == "") ||(str2.value.indexOf("www.") == -1) ||(str2.value.indexOf(".") == -1)) 
    if ((str1 == "") ||(str1.indexOf("www.") == -1) ||(str1.indexOf(".") == -1)) 
        {
        alert(msg);
        Object.focus();
        return false;
        }
 return true;

}
    
 //checks valid URL
 
//  function isValidURL(Object,msg)
// {

//   var strInput   = new String(Object.value)

//   if (trim(strInput) == "")

//     {
//       return true
//     }

//   var objregExp  = new RegExp(PatternsDict.urlpat)
//   if(objregExp.test(strInput))

//     {
//       return true

//     }

//     alert(msg)
//     Object.focus()
//     return false

// }
//selects or checks all checkboxes in a grid
function isSelectAll(CheckBoxControl,GridId,Formname) 
		{
            var FormNM = document.getElementById(Formname)
				if (CheckBoxControl.checked == true) 
				 {			    
					var i;
					for (i=0; i < FormNM.elements.length; i++) 
					{
							if ((FormNM.elements[i].type == 'checkbox') && (FormNM.elements[i].name.indexOf(GridId) > -1)) 
								{
									FormNM.elements[i].checked = true;
								}
					}
				} 
				else 
				{
					var i;
					for (i=0; i < FormNM.elements.length; i++) 
						{
							if ((FormNM.elements[i].type == 'checkbox') && (FormNM.elements[i].name.indexOf(GridId) > -1)) 
						{
						 FormNM.elements[i].checked = false;
				 }
		}
		}
		}

//=======================END OF INPUT VALIDATION SERVICE==============================

// JScript File

var strSizeMinAlert             = "<Field> can not be less than <n> characters!"
var strSizeMinResAlert          = "<Field> must contain <n> characters!"
var strSizeMaxAlert             = "<Field> can not be more than <n> characters long!"
var strSizeMaxResAlert          = "<Field> should be maximum <n> characters long!"
var strResAllowAlert1           = "Invalid characters entered in <Field>. Allows only <\"_\",\"-\",\"/\" > as special characters!"
var strResAllowAlert2           = "<Field> allows only  <\"_\",\"-\",\"/\" >characters!"
var strResAllowAlert3           = "Special characters \"-,_\" are only allowed!"
var strResNotAllowAlert1        = "Invalid characters entered in <Field>. Characters <\"-\",\"&\",\"$\">, are not allowed!"
var strResNotAllowAlert2        = "<Field> can not contain <\"-\",\"&\",\"$\"> characters!"
var strResNotAllowAlert3        = "<Field> does not accepts space(s)!"
var strNumericAlert             = "<Field> accepts only numeric values!"
var strAlphabeticAlert1         = "<Field> should be alphabetic!"
var strAlphabeticAlert2         = "<Field> accepts only alphabetic values!"
var strMandatoryAlert1          = "<Field> is mandatory!"
var strMandatoryAlert2          = "<Field> can not be left blank!"
var strMandatoryAlert3          = "<Field> is a required filed!"
var strPositiveAllowAlert       = "<Field> accepts positive values only!"
var strNegetiveAllowAlert       = "<Field> accepts negative values only!"
var strInvalidFomatAlert        = "Invalid format. Please enter <Field> as <AA-BB#CCC@DD>!"
var strNotAllowSimilarAlert     = "<Field1> and <Field2> must not be same!"
var strDoNotMatchAlert          = "Information mismatched in <Field1>. Please re-enter <Field1>!"
var strInvalidSignedFloatAlert  = "<Field> should be a floating point (real) number. (Integers also OK.)!"
var strInvalidUnSignedFloatAlert= "<Field> should be an unsigned floating point (real) number. (Integers also OK.)!"

//MESSAGES FOR DATE SERVICE//
var strValidDate            = "Valid date!";
var strInvalidDateFormat    = "Invalid date format!";
var strInvalidMonth         = "Invalid month!";
var strInvalidFebDays       = "February cannot have 29 days other than a leap year!";
var strInvalidMonthDays     = "Invalid number of days in the specified month!";
var strInvalidYear          = "Invalid year!";
var strInvalidParameter     = "Invalid parameter!";
var strFormatMismatch       = "Format mismatch!";
var strInternalError        = "Internal error!";
var strSimilarEmail         = "Email Id should be unique!"
//------------------Message added by Pramod-------------------------------------------------
var strEmail            = "Invalid Email_Id!";
var strHex              = "Invalid Entry.HexaDecimalNos. only contains a-f,A-F,0-9 characters"
var strIp               = "Invalid IP address!"
var strZip              = "Invalid ZipCode!"
var strWhiteSpace       = "White space(s) not allowed!"
var strWhiteSpace1st    = "White space(s) not allowed in first place!"
var strTelNo            = "Invalid telephone number!"
var strFilled           = "Partial phone number not filled properly!"
var strAllowSimilarAlert= "<Field1> and <Field2> must be same.Please Re_enter <Field2>!"
var strNuminRange       = "<Field> must be with in specified range!"
var strSelecteDropDown  = "Please select <Field>!"
var strSingleQuote      = "Single Quote not allowed!"  
var strValidURL         ="Enter URL in Website Format!.Ex-: www.google.com"
//=========================================END OF  STANDRD ERROR ALERT==========================================
function InputValidator()
{
    this.isEmail		 		= isEmail;
    this.isEmailSimilar         = isEmailSimilar
    this.isChar			 		= isChar;
    this.isValid		 		= isValid;
    this.isRequired		 		= isRequired;
    this.isHex			 		= isHex;
    this.isValidIP		 		= isValidIP;
    this.isUSAZip		 		= isUSAZip;
    this.isWhitespace	 		= isWhitespace;
    this.isWhitespace1st        = isWhitespace1st;
    this.isNumeric		 		= isNumeric;
    this.isUSATel		 		= isUSATel;
    this.isINDTel               = isINDTel;
    this.isFilled               = isFilled;//Added By Pramod Kumar Pradhan on 6th jan'07
    this.isPwdMatch		 		= isPwdMatch;
    this.isMinlen		 		= isMinlen;
    this.isReslen		 	    = isReslen;
    this.isMaxlen        		= isMaxlen;
    this.isSimilar		 		= isSimilar;
    this.isDateinMMM	 		= isDateinMMM;
    this.isDate     	 		= isDate;
    this.UpperCase		 		= UpperCase;
    this.isValidQA		 		= isValidQA;
    this.trim			 		= trim;
    this.Replace		 		= Replace;
    this.isSignedFloat   		= isSignedFloat;
    this.isUnSignedFloat 		= isUnSignedFloat;
    this.isNumInRange    		= isNumInRange;
	this.isIntInRange    		= isIntInRange;
	this.isAtleastOneChecked	= isAtleastOneChecked;
	this.selectsAll				= selectsAll;
	this.chkAllCheckBoxes		= chkAllCheckBoxes;
	this.isMultipleChecked		= isMultipleChecked;
	this.isSelectDropDown       = isSelectDropDown;      
    this.isSingleQuote          = isSingleQuote;
    this.isValidURL             = isValidURL;
    this.isSelectAll            = isSelectAll;

}

function ErrorAlert()
{
	this.SizeMinAlert				= strSizeMinAlert;
	this.SizeMinResAlert			= strSizeMinResAlert;
	this.SizeMaxAlert				= strSizeMaxAlert;
	this.SizeMaxResAlert			= strSizeMaxResAlert;
	this.ResAllowAlert1				= strResAllowAlert1;
	this.ResAllowAlert2				= strResAllowAlert2;
	this.ResAllowAlert3				= strResAllowAlert3;
	this.ResNotAllowAlert1			= strResNotAllowAlert1;
	this.ResNotAllowAlert2			= strResNotAllowAlert2;
	this.ResNotAllowAlert3			= strResNotAllowAlert3;
	this.NumericAlert				= strNumericAlert;
	this.AlphabeticAlert1			= strAlphabeticAlert1;
	this.AlphabeticAlert2			= strAlphabeticAlert2;
	this.MandatoryAlert1			= strMandatoryAlert1;
	this.MandatoryAlert2			= strMandatoryAlert2;
	this.MandatoryAlert3			= strMandatoryAlert3;
	this.PositiveAllowAlert			= strPositiveAllowAlert;
	this.NegetiveAllowAlert			= strNegetiveAllowAlert;
	this.InvalidFomatAlert			= strInvalidFomatAlert;
	this.NotAllowSimilarAlert		= strNotAllowSimilarAlert;
	this.DoNotMatchAlert			= strDoNotMatchAlert;
	this.InvalidSignedFloatAlert 	= strInvalidSignedFloatAlert
	this.InvalidUnSignedFloatAlert 	= strInvalidUnSignedFloatAlert
//---------------------Added By Pramod-------------------------------------------------------
    this.validEmail                 = strEmail
    this.validHex                   = strHex
    this.validIP                    = strIp
    this.validZIP                   = strZip
    this.whitespace                 = strWhiteSpace
    this.whitespace1st              = strWhiteSpace1st
    this.validPhoneNo               = strTelNo
    this.validFill                  = strFilled
    this.matchpwd                   = strAllowSimilarAlert
    this.similarEmailalert          = strSimilarEmail
    this.numinRangealert            = strNuminRange
    this.strSelecteDropDown         = strSelecteDropDown
    this.strSingleQuote             = strSingleQuote
    this.strValidURL                = strValidURL
}
//====================================END OF TEST PAGE=======================================
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
//============2.Function checking maximum length of a field ==============================
function MaxlengthValidation(Controlname,Fieldname,maxlen)
  {
    //var objform=document.getElementById(Formname);
	var objfrm=document.getElementById(Controlname);
	var objFieldname=Fieldname;
	var objmaxlen=maxlen
	
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
    objValidator.isMaxlen(objfrm,objmaxlen,objValidator.Replace(objValidator.Replace(objError.SizeMaxAlert,"<Field>",objFieldname),"<n>",objmaxlen)) 
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
 
//============3.Function checking minimum length of a field ==============================

function MinlengthValidation(Controlname,Fieldname,minlen)
  {
     //var objform=document.getElementById(Formname);
	var objfrm=document.getElementById(Controlname);
	var objFieldname=Fieldname;
	var objminlen=minlen
	
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
    objValidator.isMinlen(objfrm,objminlen,objValidator.Replace(objValidator.Replace(objError.SizeMinAlert,"<Field>",objFieldname),"<n>",objminlen)) 
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
  //============4.Function checking Restricted length of a field ==============================

function ReslengthValidation(Controlname,Fieldname,reslen)
  {
      //var objform=document.getElementById(Formname);
	var objfrm=document.getElementById(Controlname);
	var objFieldname=Fieldname;
	var objminlen=reslen
	
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
    objValidator.isReslen(objfrm,objminlen,objValidator.Replace(objValidator.Replace(objError.SizeMinResAlert,"<Field>",objFieldname),"<n>",objminlen)) 
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
  
//============5.Function checking EmailValidation of a field ==============================
function EmailValidation(Controlname)
  {
    var objfrm=document.getElementById(Controlname);
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
      objValidator.isEmail(objfrm,objError.validEmail)
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
  
  
  //====================6.Function Checks a character type field==============================
function CharValidation(Controlname,Fieldname)
  {
    var objfrm=document.getElementById(Controlname);
    var objFieldname=Fieldname;
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
     objValidator.isChar(objfrm, objValidator.Replace(objError.AlphabeticAlert2,"<Field>",objFieldname))
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
  
  //====================7.Function checking isValid Characters (Check if field contains any character except alplanumeric and -/_.including white space)==============================
function isValidCharValidation(Controlname)
  {
    var objfrm=document.getElementById(Controlname);
   //var objFieldname=Fieldname;
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
  //objValidator.isValid(objfrm, objValidator.Replace(objError.ResAllowAlert3,"<Field>",objFieldname))
objValidator.isValid(objfrm,objError.ResAllowAlert3)
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
  
  //====================8.Function checking HexaDecimal no's ==============================
function HexaValidation(Controlname)
  {
    
    var objfrm=document.getElementById(Controlname);
   
       //var objFieldname=Fieldname;
  
	var flag;
	flag=false;
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
	

   if
    (
   
      objValidator.isHex(objfrm, objError.validHex)
       
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
  
//====================9.Function checking valid IP adress ==============================
function IPValidation(Controlname)
  {
    var objfrm=document.getElementById(Controlname);
    var flag;
	flag=false;
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
  objValidator.isValidIP(objfrm, objError.validIP)
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

  
  //====================10.Function checking USA ZIP CODE ==============================
  function USAZIPCodeValidation(Controlname)
  {
    var objfrm=document.getElementById(Controlname);
   	var flag;
	flag=false;
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
  objValidator.isUSAZip(objfrm, objError.validZIP)
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

  //====================11.Function checking WhiteSpaces ==============================
function WhiteSpaceValidation(Controlname)
  {
    var objfrm=document.getElementById(Controlname);
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
  
  
 //====================12.Function checking USA TELEPHONE Number ==============================
 function USATelNoValidation(Controlname)
  {
     var objfrm=document.getElementById(Controlname);
     var flag;
	flag=false;
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   
   if
    (
  objValidator.isUSATel(objfrm, objError.validPhoneNo)
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
 
 
 //====================13.Function checking IND TELEPHONE Number ==============================
 function INDTelNoValidation(Controlname)
  {
     var objfrm=document.getElementById(Controlname);
     var flag;
	flag=false;
	var objValidator = new InputValidator();
	var objError = new ErrorAlert();
	
   if(objValidator.isINDTel(objfrm, objError.validPhoneNo))
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
//====================14.Function checking  isFilled (Checks partial phone number)==============================
//function isFilled_Validation(Controlname,Fieldname)
//  {
//    var objfrm=document.getElementById(Controlname);
//    var objFieldname=Fieldname;
//	var flag;
//	flag=false;
//	var objValidator = new InputValidator();
//	var objError 	 = new ErrorAlert();
//   if
//    (
//  objValidator.isFilled(objfrm,objValidator.Replace(objError.validFill,"<Field>",objFieldname))
//    )
//        {
//	  	objError = null
//		objValidator = null
//		alert ("Form has been validated successfully.")
//		flag=true
//        }
//	objError = null
//	objValidator = null
//	return flag
//  }
  
   //====================15.Function for toUpperCase ==============================
function toUpperValidation(Controlname)
{
var objfrm=document.getElementById(Controlname);
var msg=objfrm.value
var objValidator = new InputValidator();
var str=objValidator.UpperCase(msg)
alert(str)
}

 //====================16.Function to check numeric values ==============================
function NumericValidation(Controlname,Fieldname,length)
  {
    var objfrm=document.getElementById(Controlname);
  	var objFieldname=Fieldname;
	var objlen=length
	
	
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
    objValidator.isNumeric(objfrm,objlen,objValidator.Replace(objError.NumericAlert,"<Field>",objFieldname)) 
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
//====================17.Function for to check match password ==============================
function PasswordValidation(Controlname1,Controlname2,Fieldname1,Fieldname2)
  {
    var objfrm1=document.getElementById(Controlname1);
    var objfrm2=document.getElementById(Controlname2);
  	var objFieldname1=Fieldname1;
	var objFieldname2=Fieldname2;
	
	
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
    objValidator.isPwdMatch(objfrm1,objfrm2,objValidator.Replace(objValidator.Replace(objValidator.Replace(objError.matchpwd,"<Field2>",objFieldname2),"<Field2>",objFieldname2),"<Field1>",objFieldname1)) 
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
  
  //====================18.Function for to not allow similar fields ==============================
function DisSimilarValidation(Controlname1,Controlname2,Fieldname1,Fieldname2)
  {
    var objfrm1=document.getElementById(Controlname1);
    var objfrm2=document.getElementById(Controlname2);
  	var objFieldname1=Fieldname1;
	var objFieldname2=Fieldname2;
	
	
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
    objValidator.isSimilar(objfrm1,objfrm2,objValidator.Replace(objValidator.Replace(objError.NotAllowSimilarAlert,"<Field2>",objFieldname2),"<Field1>",objFieldname1)) 
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
  
  //====================19.Function for checking unique values in different fields ==============================
function chkSimilarID(Controlname1,Controlname2,Controlname3,Controlname4,Controlname5)
  {
    var objfrm1=document.getElementById(Controlname1);
    var objfrm2=document.getElementById(Controlname2);
    var objfrm3=document.getElementById(Controlname3);
    var objfrm4=document.getElementById(Controlname4);
    var objfrm5=document.getElementById(Controlname5);
   
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
    objValidator.isEmailSimilar(objfrm1,objfrm2,objfrm3,objfrm4,objfrm5,objError.similarEmailalert) 
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
   //====================20.Function for checking whether Pwd Question & Ans. are entered.(i.e two fields simultaneously are filled)==============================
function isValidQAValidation(Controlname1,Controlname2,Fieldname1,Fieldname2)
  {
    var objfrm1=document.getElementById(Controlname1);
    var objfrm2=document.getElementById(Controlname2);
  	var objFieldname1=Fieldname1;
	var objFieldname2=Fieldname2;
	
	
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
 
    objValidator.isValidQA(objfrm1,objfrm2,objValidator.Replace(objError.MandatoryAlert2,"<Field>",objFieldname1),objValidator.Replace(objError.MandatoryAlert2,"<Field>",objFieldname2)) 
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
 //====================21.Function for checking valid date in dd-MMM-YYYY format==============================
function isValidDateValidation(Controlname1)
  {
    var objfrm1=document.getElementById(Controlname1);
    var strdate=objfrm1.value
    var dt=strdate.split("/")
    var flag=false;

	var objValidator = new InputValidator();
    if
	(
	strdate !=dt
	)
	{
	flag=objValidator.isDateinMMM(dt[0],dt[1],dt[2]) 
	}
	else
	{
	dt=strdate.split("-")
	flag=objValidator.isDateinMMM(dt[0],dt[1],dt[2]) 
	}
	if(flag==true)
	alert("Valid Date")
	else
	alert("invalid Date")
	objfrm1.focus()
     flag==false
//	  	objValidator = null
//		alert ("Form has been validated successfully.")
//		flag=true
//        }
//	objValidator = null
	return flag
  }
 
 //====================22.Function for checking valid date in dd-MM-YYYY format==============================
function isValidDateValidation1(Controlname1)
  {
    var objfrm1=document.getElementById(Controlname1);
    var strdate=objfrm1.value
    var dt=strdate.split("/")
    var flag=false;

	var objValidator = new InputValidator();
    if
	(
	strdate !=dt
	)
	{
	flag=objValidator.isDate(dt[0],dt[1],dt[2]) 
	}
	else
	{
	dt=strdate.split("-")
	flag=objValidator.isDate(dt[0],dt[1],dt[2]) 
	}
	if(flag==true)
	alert("Valid Date")
	else
	alert("invalid Date")
	objfrm1.focus()
     flag==false
//	  	objValidator = null
//		alert ("Form has been validated successfully.")
//		flag=true
//        }
//	objValidator = null
	return flag
  }
 
  
  
  //====================23.Function checking  Signed Float value==============================
function isSignedFloatValidation(Controlname,Fieldname)
  {
    var objfrm=document.getElementById(Controlname);
    var objFieldname=Fieldname;
	var flag;
	flag=false;
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
  objValidator.isSignedFloat(objfrm,objValidator.Replace(objError.InvalidSignedFloatAlert,"<Field>",objFieldname))
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
  
    
   //====================24.Function checking  Unsigned Float value==============================
 function isUnSignedFloatValidation(Controlname,Fieldname)
  {
    var objfrm=document.getElementById(Controlname);
    var objFieldname=Fieldname;
	var flag;
	flag=false;
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
  objValidator.isUnSignedFloat(objfrm,objValidator.Replace(objError.InvalidUnSignedFloatAlert,"<Field>",objFieldname))
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
  
  //====================25.Function specifying Number with in a particular range==============================
 function isNumInRangeValidation(Controlname,Low,High,Fieldname)
  {
    var objfrm=document.getElementById(Controlname);
    var objFieldname=Fieldname;
	var flag;
	flag=false;
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
  objValidator.isNumInRange(objfrm,Low,High,objValidator.Replace(objError.numinRangealert,"<Field>",objFieldname))
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
  
 
  //====================26.Function specifying An integer with in a particular range==============================
 function isIntInRangeValidation(Controlname,Low,High,Fieldname)
  {
    var objfrm=document.getElementById(Controlname);
    var objFieldname=Fieldname;
	var flag;
	flag=false;
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
  objValidator.isIntInRange(objfrm,Low,High,objValidator.Replace(objError.numinRangealert,"<Field>",objFieldname))
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
  //============27.Function checking select dropdown==============================
      
function DropDownValidation(Controlname,Fieldname)
  {
  	var objfrm=document.getElementById(Controlname);
  	var objFieldname=Fieldname;
  	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
        objValidator.isSelectDropDown(objfrm, objValidator.Replace(objError.strSelecteDropDown,"<Field>",objFieldname))
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
  
  //============28.Function checking Single Quote==============================
function chkSingleQuote(Controlname)
  {
   	var objfrm=document.getElementById(Controlname);
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
       objValidator.isSingleQuote(objfrm, objError.strSingleQuote)
  
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
  //============29.Function checking valid URL==============================
function chkURL(Controlname)
  {
  	var objfrm=document.getElementById(Controlname);
	var flag;
	flag=false;
	
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
       objValidator.isValidURL(objfrm, objError.strValidURL) 
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
//============30.Function to check/select all checkboxes==============================
  
 function SelectAll(chkboxControlname,GridID,FormName)
  {
	var objValidator = new InputValidator();
    objValidator.isSelectAll(chkboxControlname, GridID,FormName)
  }
//====================31.Function checking WhiteSpaces in first place==============================
function WhiteSpaceValidation1st(Controlname)
  {
    var objfrm=document.getElementById(Controlname);
    var flag;
	flag=false;
	var objValidator = new InputValidator();
	var objError 	 = new ErrorAlert();
   if
    (
           objValidator.isWhitespace1st(objfrm, objError.whitespace1st)
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