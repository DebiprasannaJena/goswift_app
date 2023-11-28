var xmlHttp = null;

var fillctlname = null;
var stData;
var hidids;
var hidids2;
var hidLvl = 0;
var CtrlId;
var IdToConcat;
var LabelId;
var flag = 0;
var ctrlNo = 0;
var intCount;
var ctrl;
var btnid = null;
var Hidval = null;
var intLayer;
//*********
var btnReset = null;
//*********




function GetXmlHttpObject() //Creates the XmlHttpObject
{

    var objXMLHttp = null;

    if (window.XMLHttpRequest) {
        objXMLHttp = new XMLHttpRequest()
    }
    else if (window.ActiveXObject) {

        objXMLHttp = new ActiveXObject("Microsoft.XMLHTTP")
    }

    return objXMLHttp;
}


//*******************************Added By Biswaranjan on 2-Nov-2010*************************

//***********************************Begin*************************************************
//*******************Summery****************************
//Function Name             : PhysicalLoc
//Purpose                   : To pass the userid to the codebehind and return physical location
//InPut Parameters Name     : ddlControlid,requestURL,Trtodisplay,lbltext
//InPut Parameters DataType : None
//OutPut Parameters Name    : None
//OutPut Parameters DataType: None
//Retun  Value              : None
//Retun Datatype            : None
//Created By                : Biswaranjan Das
//Created Date              : 2-Nov-2010
//*****************************************************
var Tr;
function PhysicalLoc(ddlControlid, requestURL, Trtodisplay, lbltext) {
    CtrlId = document.getElementById(ddlControlid);
    LabelId = document.getElementById(lbltext);
    Tr = document.getElementById(Trtodisplay);
    if (parseInt(CtrlId.value) > 0) {
        //Append the name to search for to the requestURL
        var url = requestURL + CtrlId.value;
        // alert(url);
        xmlHttp = GetXmlHttpObject();
        xmlHttp.open('GET', url, true);
        xmlHttp.onreadystatechange = FillPhysicalLoc;
        xmlHttp.send(null);
    }

}
//*******************Summery***************************
//Function Name             : FillPhysicalLoc()
//Purpose                   : To view the physicallocation in a label if available
//InPut Parameters Name     : None
//InPut Parameters DataType : None
//OutPut Parameters Name    : None
//OutPut Parameters DataType: None
//Retun  Value              : None
//Retun Datatype            : None
//Created By                : Biswaranjan Das
//Created Date              : 2-Nov-2010
//*****************************************************
function FillPhysicalLoc() {
    if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
        var strResponse = xmlHttp.responseText.toString();
        //alert(strResponse);
        Tr.style.display = (strResponse != null && strResponse != "") ? "inline" : "none";
        LabelId.innerHTML = (strResponse != null && strResponse != "") ? strResponse : "Not Alloted";
    }
}


function FillUser(ctrlid, fillctrlname, RequestURL, buttonid, hidval, parambtnReset) {
   

    CtrlId = document.getElementById(ctrlid);
    CtrlId = CtrlId.value;
    ctrl = fillctrlname;
    btnid = buttonid;
    Hidval = hidval;
    //*********
    btnReset = parambtnReset;
    //*********
    
    //alert(CtrlId);//Commented By Biswaranjan on 9-Nov-2010
    //  var id = document.getElementById(ctrlid).value;
    if (CtrlId != 0) {
        var url = RequestURL + CtrlId;
        xmlHttp = GetXmlHttpObject();
        xmlHttp.open('GET', url, false);
        xmlHttp.onreadystatechange = FillListBox;
        xmlHttp.send(null);
    }
}

function FillListBox() {
    
    if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
        var str = xmlHttp.responseText.toString();
        var selectctrl = document.getElementById(ctrl);
        var button = btnid;
        var hidden = Hidval;
        var arry = new Array();
        var btnResetclientId =document.getElementById(btnReset);
        arry = str.split('~');
        if (arry.length >= 1)  //If More than one element retured then add it to the dropdown lists
        {
            RemoveAllOptions(ctrl); //Remove all the Options 
            for (var i = 0; i < arry.length; i++) {
                var e = document.createElement("option");
                var el = arry[i].split('|')
                e.value = el[0];
                e.innerText = el[1];
                if (e.value != '') {
                    selectctrl.appendChild(e);
                    if (button != "undefined" && button != null) {
                        document.getElementById(btnid).value = "Update";
                        document.getElementById(hidden).value = "Update";
                        //******
                        btnResetclientId.value = "Cancel";
                        //******
                    }
                }
                else {
                    document.getElementById(btnid).value = "Save";
                    document.getElementById(hidden).value = "Save";
                    //*************
                    btnResetclientId.value = "Reset";
                    //*************
                }

            } //End Loop
        }

        else {
            RemoveAllOptions(fillctrlname);
        }
        //End
    }
}
//---------------------------
//'hidLayers' parameter added by Pratik on 08-Oct-2010 to set value for hidLvl
//
function GetData(ctlid, pfillctlname, hidid, lblId, requestURL, hidLayers) {
   
  
    flag = 1;
    CtrlId = ctlid;

    if (ctlid.id.indexOf('_drplayer') > 0) {
        
        IdToConcat = ctlid.id.substring(0, ctlid.id.indexOf('_drplayer'));
        
        fillctlname = IdToConcat + pfillctlname;
        
        hidids = IdToConcat + hidid;

        document.getElementById(hidids).value = ctlid.value;
         
        hidLvl = IdToConcat + hidLayers; //======Newly added By Pratik
        
        LabelId = IdToConcat + lblId;
        
        ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_drplayer')) + 9, ctlid.id.lenghth);
        
    }
    else if (ctlid.id.indexOf('_sdrplayers') > 0) {
    
    IdToConcat = ctlid.id.substring(0, ctlid.id.indexOf('_sdrplayers'));
    
    fillctlname = IdToConcat + pfillctlname;
    
    hidids2 = IdToConcat + hidid;
    
    document.getElementById(hidids2).value = ctlid.value;
   
    hidLvl = IdToConcat + hidLayers; //======Newly added By Pratik
    
    LabelId = IdToConcat + lblId;
    
    ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_sdrplayers')) + 11, ctlid.id.length);
    
    }
    if (ctlid.selectedIndex > 0) {
        
        var url = requestURL + ctlid.value;
        
        xmlHttp = GetXmlHttpObject();
        xmlHttp.open('GET', url, false);
        intCount = 1;

        //alert('getdata');
        //alert(intCount);
        xmlHttp.onreadystatechange = FillDropdown;
        xmlHttp.send(null);
    }
    else {
        RemoveAllOptions(fillctlname);
    }
}
function GetDataL(ctlid, pfillctlname, hidid, lblId, requestURL) {

    CtrlId = ctlid;
    if (ctlid.id.indexOf('_drplayer') > 0) {
        IdToConcat = ctlid.id.substring(0, ctlid.id.indexOf('_drplayer'));
        fillctlname = IdToConcat + pfillctlname;
        hidids = IdToConcat + hidid;
        document.getElementById(hidids).value = ctlid.value;
        LabelId = IdToConcat + lblId;
        ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_drplayer')) + 9, ctlid.id.lenghth);
    }
    else if (ctlid.id.indexOf('_sdrplayers') > 0) {
        IdToConcat = ctlid.id.substring(0, ctlid.id.indexOf('_sdrplayers'));
        fillctlname = IdToConcat + pfillctlname;
        hidids2 = IdToConcat + hidid;
        document.getElementById(hidids2).value = ctlid.value;
        LabelId = IdToConcat + lblId;
        ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_sdrplayers')) + 11, ctlid.id.length);
    }
    if (ctlid.selectedIndex > 0) {
        var url = requestURL + ctlid.value;
        xmlHttp = GetXmlHttpObject();
        xmlHttp.open('GET', url, false);
        intCount = 1;

        //alert('getdata');
        //alert(intCount);
        xmlHttp.onreadystatechange = FillDropdown;
        xmlHttp.send(null);
    }
    else {
        RemoveAllOptions(fillctlname);
    }
}
//function GetDataForUserL(ctlid, pfillctlname, hidid, hidLayers, lblId, requestURL) {
//   
//    flag = 1;
//    CtrlId = ctlid;
//    if (ctlid.id.indexOf('_drplayer') > 0) {
//        IdToConcat = ctlid.id.substring(0, ctlid.id.indexOf('_drplayer'));
//        fillctlname = IdToConcat + pfillctlname;
//        hidids = IdToConcat + hidid;
//        document.getElementById(hidids).value = ctlid.value;
//        hidLvl = IdToConcat + hidLayers;
//        LabelId = IdToConcat + lblId;
//        ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_drplayer')) + 9, ctlid.id.length);
//    }
//    else if (ctlid.id.indexOf('_sdrplayers') > 0) {
//        IdToConcat = ctlid.id.substring(0, ctlid.id.indexOf('_sdrplayers'));
//        fillctlname = IdToConcat + pfillctlname;
//        hidids2 = IdToConcat + hidid;
//        document.getElementById(hidids2).value = ctlid.value;
//        hidLvl = IdToConcat + hidLayers;
//        LabelId = IdToConcat + lblId;
//        ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_sdrplayers')) + 11, ctlid.id.length);
//    }
//    if (ctlid.selectedIndex > 0) {
//        var url = requestURL + ctlid.value;
//        xmlHttp = GetXmlHttpObject();
//        xmlHttp.open('GET', url, false);
//        intCount = 1;
//        //alert('GetDataForUser');
//        //alert(intCount);
//        xmlHttp.onreadystatechange = FillDropdown;
//        xmlHttp.send(null);
//    }
//    else {
//        RemoveAllOptions(fillctlname);
//    }
//}
function GetUserData(ctlid, pfillctlname, requestURL) {
 
    fillctlname = pfillctlname;
    
    //alert(fillctlname);//Commented By Biswaranjan on 9-Nov-2010
    //Commented By Biswaranjan on 9-Nov-2010
    if (ctlid.selectedIndex >= 0) {
        //alert(ctlid.value);//Commented By Biswaranjan on 9-Nov-2010
        var url = requestURL + ctlid.value;
        
        xmlHttp = GetXmlHttpObject();
        xmlHttp.open('GET', url, false);
        intCount = 0;

        xmlHttp.onreadystatechange = FillDropdown;
        xmlHttp.send(null);
    }
    else {
        RemoveAllOptions(fillctlname);
    }
}
//function GetUserDataL(ctlid, pfillctlname, requestURL) {

//    fillctlname = pfillctlname;
//    if (ctlid.selectedIndex > 0) {
//        var url = requestURL + ctlid.value;
//        xmlHttp = GetXmlHttpObject();
//        xmlHttp.open('GET', url, false);
//        intCount = 0;
//        //alert('GetUserData');
//        //alert(intCount);
//        xmlHttp.onreadystatechange = FillDropdown;
//        xmlHttp.send(null);
//    }
//    else {
//        RemoveAllOptions(fillctlname);
//    }
//}
function RemoveAllOptions(fillctlname) {

    var obj = document.getElementById(fillctlname);
    if (obj) {
        if (obj.length > 0) {
            obj.length = 0;

        }
        if (obj.length == 0) {
            var e = document.createElement("option");

            e.value = 0;
             if(document.all)
             {
                e.innerText = "--Select--";
            }
            else
            {
                e.textContent = "--Select--";
            }

            obj.appendChild(e);
        }
    }
}

function FillDropdown() {
  debugger;
    var len;
       if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {       
        var str = xmlHttp.responseText.toString();
       
        var select2 = document.getElementById(fillctlname);
        
        var arrUser = new Array();
        var ar = new Array();
        if (str != "") {
            if (flag == '1') {
                arrUser = str.split('`');
                if (arrUser[1] != '?') {
                    document.getElementById(hidLvl).value = arrUser[2];
                    
                }
                if (arrUser[0] != '') {
                    ar = arrUser[0].split('~');                    
                }
                else {
                    ar = null;
                }
            }
            else {
                ar = str.split('~');
            }
        }

        if (ar != null) {
            len = ar.length;
        }
        else {
            len = 0;
        }
        var levelvalue="";
        if (len > 0)  //If More than one element retured then add it to the dropdown lists
        {
            RemoveAllOptions(fillctlname); //Remove all the Options 
            for (var i = 0; i < ar.length; i++) {
                if (ar[i] != "") {
                    var e = document.createElement("option");
                    var el = ar[i].split('|');
                    e.value = el[0];
                    if(el[1]==el[2]){
                    levelvalue=levelvalue+el[2]+"/"
                        e.disabled =true;
                        e.style.backgroundColor="silver";
                    }
                    if(i==ar.length-1){
                         if(levelvalue.substring(levelvalue.length - 1,levelvalue.length)=="/"){
                                levelvalue = levelvalue.substring(0, levelvalue.length - 1);
                            }
                    }
                    if(document.all)
                        {
                            e.innerText = el[1];
                        }
                        else
                        {
                            e.textContent = el[1];
                        }
                    
                    
                    if (flag == '1') {
                        if (el[2] != '^') {
                        if(document.all){     
                            document.getElementById(LabelId).title= levelvalue;                           
                            if(levelvalue.length>22){
                               levelvalue = levelvalue.substring(0,22)+"..";
                            }                
                            document.getElementById(LabelId).innerText = levelvalue;
                          }
                      else{                        
                            document.getElementById(LabelId).title= levelvalue;                         
                            if(levelvalue.length>22){
                               levelvalue = levelvalue.substring(0,22)+"..";
                            } 
                            document.getElementById(LabelId).textContent =levelvalue;
                         }
                      }
                    }
                    select2.appendChild(e);
                }
            }
        }

        else {
            ctrlNo = parseInt(ctrlNo) - 1;
            RemoveAllOptions(fillctlname);
        }
        if (hidLvl != '0') {
            if (document.getElementById(hidLvl).value > 0) {

                if (intCount == 1) {

                    HideShow(document.getElementById(hidLvl).value, IdToConcat, ctrlNo);
                }
            }
        }
        
    }
}
//=====================================
//Function To Hide/Show Rows
//=======Added By Pratik on 17-Jul-2010
var rowToShow;
var Url;
var i;

function HideShow(hidLayers, Id, no) {
    
    if (Id != "") {
        for (i = 2; i <= 10; i++) {
            if (i <= hidLayers+1) {
                if (i <= parseInt(no) + 2) {
                    //                alert('1st');
                    document.getElementById(Id + '_tr' + i).style.display = "block";
                }
                else {
                    //                alert('2nd');
                    document.getElementById(Id + '_tr' + i).style.display = "none";
                }
            }

            else {
                //                alert('3rd');
                document.getElementById(Id + '_tr' + i).style.display = "none";
            }
        }
    }
}

//=====================================
//Function to clear labels
//Added By Pratik On 19-Jul-2010=======
//modifyed by Pratik on 13-oct-2010
//Purpose: remove the before search items from the  user control
var lblId;
function ClearLabels(ctlid) {

    var maxlen = ctlid.id.length;
    var start = parseInt(ctlid.id.length) - 1;
    var i = parseInt(ctlid.id.substring(start, maxlen)) + 2;


    for (; i < 10; i++) {
        if (ctlid.id.indexOf('_drplayer') > 0) {
            lblId = ctlid.id.substring(0, ctlid.id.indexOf('_drplayer')) + '_Label' + i;
            hidnId = ctlid.id.substring(0, ctlid.id.indexOf('_drplayer')) + '_hidID' + (i - 1); //----by Pratik on 13-oct-2010
        }
        else if (ctlid.id.indexOf('_sdrplayers') > 0) {
        lblId = ctlid.id.substring(0, ctlid.id.indexOf('_sdrplayers')) + '_Labels' + i;
        hidnId = ctlid.id.substring(0, ctlid.id.indexOf('_sdrplayers')) + '_shidIDs' + (i - 1); //------by Pratik on 13-oct-2010
        }
        if(document.all)
        {
            document.getElementById(lblId).innerText = "";
        }
        else
        {
            document.getElementById(lblId).textContent = "";
        }
        document.getElementById(hidnId).value = ""; //----by Pratik on 13-oct-2010
    }
}

////=====================================
////Function to clear labels
////Added By Priyabrat on 25-11-2011=======
////This function is inherits from root script folder of ajax.js file
//var lblId;
//function ClearLabelsL(ctlid) {
//    for (var i = 2; i < 10; i++) {
//        if (ctlid.id.indexOf('_drplayer') > 0) {
//            lblId = ctlid.id.substring(0, ctlid.id.indexOf('_drplayer')) + '_Label' + i;
//        }
//        else if (ctlid.id.indexOf('_sdrplayers') > 0) {
//            lblId = ctlid.id.substring(0, ctlid.id.indexOf('_sdrplayers')) + '_Labels' + i;
//        }

//        document.getElementById(lblId).innerText = "";
//    }
//}
//=====================
//Added by Subrat Hota
//Purpose:-Clear the all drop down except location and it will be choose '--select--'in location dropdown
//====================
var drpid;
var dropdwn;
function ClearDropdown(ctlid) {

    if (ctlid.selectedIndex == 0) {
        var maxlen = ctlid.id.length;
        var start = parseInt(ctlid.id.length) - 1;


        var i = parseInt(ctlid.id.substring(start, maxlen)) + 2;



        for (; i < 10; i++) {
            if (ctlid.id.indexOf('_drplayer') > 0) {
                drpid = ctlid.id.substring(0, ctlid.id.indexOf('_drplayer')) + '_tr' + i;
            }
            else if (ctlid.id.indexOf('_sdrplayers') > 0) {
                drpid = ctlid.id.substring(0, ctlid.id.indexOf('_sdrplayers')) + '_tr' + i;

            }

            document.getElementById(drpid).style.display = "none";
        }
    }
}


function GetDataForUser(ctlid, pfillctlname, hidid, hidLayers, lblId, requestURL) {   
debugger;
    flag = 1;
    CtrlId = ctlid;
    if (ctlid.id.indexOf('_drplayer') > 0) {
        IdToConcat = ctlid.id.substring(0, ctlid.id.indexOf('_drplayer'));
        fillctlname = IdToConcat + pfillctlname;
       
        hidids = IdToConcat + hidid;

        document.getElementById(hidids).value = ctlid.value;
        
        hidLvl = IdToConcat + hidLayers;

        LabelId = IdToConcat + lblId;
        
        ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_drplayer')) + 9, ctlid.id.length);        
        
    }
    else if (ctlid.id.indexOf('_sdrplayers') > 0) {
    
        IdToConcat = ctlid.id.substring(0, ctlid.id.indexOf('_sdrplayers'));
        fillctlname = IdToConcat + pfillctlname;
      
        hidids2 = IdToConcat + hidid;
        document.getElementById(hidids2).value = ctlid.value;
         
        hidLvl = IdToConcat + hidLayers;
        LabelId = IdToConcat + lblId;
        ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_sdrplayers')) + 11, ctlid.id.length);
    }
    if (ctlid.selectedIndex > 0) {
        
        var url = requestURL + ctlid.value;
        xmlHttp = GetXmlHttpObject();              
        xmlHttp.open('GET', url,false);
        intCount = 1;
        //alert('GetDataForUser');
        //alert(intCount);
        xmlHttp.onreadystatechange = FillDropdown;               
        xmlHttp.send(null);
    }
    else {
        RemoveAllOptions(fillctlname);
    }
}


//-------------------
function BindUserList(ddl0,listbox) {

    if (ddl0.selectedIndex == 0) {
        listbox.option[1] = "dd";
    }
    else {
        listbox.option[1] = "dd11";
    }
}