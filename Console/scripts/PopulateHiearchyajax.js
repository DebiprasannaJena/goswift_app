var xmlHttp = null;
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

var fillctlname = null;
var stData = null;
var hidids = null;
var hidids2 = null;
var hidLvl = 0; ;
var CtrlId = null;
var IdToConcat = null;
var LabelId = null;
var flag = 0;
var ctrlNo = 0;
var intCount;
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
//***********************************End****************************************************

//Function Name             : MakeHidBlank() 
//Purpose                   : To make blank hidden field
//InPut Parameters Name     :ctlid, startval
//InPut Parameters DataType : dropdown control,string
//OutPut Parameters Name    : None
//OutPut Parameters DataType: None
//Retun  Value              : None
//Retun Datatype            : None
//Created By                : Dilip Kumar Tripathy
//Created Date              : 19-Mar-2012
//*****************************************************
function MakeHidBlank(ctlid, startval) {

    var IdToConcat = ctlid.id.substring(0, ctlid.id.indexOf('_sdrplayers'));
    for (var i = startval; i <= 4; i++) {
        var hidids = IdToConcat + "_shidIDs" + i;
        document.getElementById(hidids).value = "";
    }
}

//-------------
//Function Name             : ClearListBox()
//Purpose                   : To make ListBox blank
//InPut Parameters Name     : listBoxId
//InPut Parameters DataType : listbox controlid 
//OutPut Parameters Name    : None
//OutPut Parameters DataType: None
//Retun  Value              : None
//Retun Datatype            : None
//Created By                : Dilip Kumar Tripathy
//Created Date              : 18-Apr-2012
//*****************************************************
function ClearListBox(listBoxId) {

    var lboxId = document.getElementById(listBoxId);
    var lboxLen = lboxId.length;
    while (lboxId.length > 0) {
        lboxId.remove(0);
    }
    var opt = document.createElement('option');
    opt.text = '--Select--';
    opt.value = '0';
    lboxId.options.add(opt)
}


function GetData(ctlid, pfillctlname, hidid, lblId, requestURL) {
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
function GetDataForUser(ctlid, pfillctlname, hidid, hidLayers, lblId, requestURL) {

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
         xmlHttp.open('GET', url, false);
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
function GetUserData(ctlid, pfillctlname, requestURL) {

    fillctlname = pfillctlname;
    if (ctlid.selectedIndex > 0) {
        var url = requestURL + ctlid.value;
        xmlHttp = GetXmlHttpObject();
        xmlHttp.open('GET', url, false);
        intCount = 0;
        //alert('GetUserData');
        //alert(intCount);
        xmlHttp.onreadystatechange = FillDropdown;
        xmlHttp.send(null);
    }
    else {
        RemoveAllOptions(fillctlname);
    }
}
//----------------------------------------------------------
//Function Name             : GetLevelName(),FillLabel()
//Purpose                   : To assign Hierarchy LevelName  in label control
//InPut Parameters Name     : ctlid, pfillctlname, requestURL for GetLevelName()
//InPut Parameters DataType : DropDown List controlid,Label Control Id,string
//OutPut Parameters Name    : None
//OutPut Parameters DataType: None
//Retun  Value              : None
//Retun Datatype            : None
//Created By                : Dilip Kumar Tripathy
//Created Date              : 19-Apr-2012
//*****************************************************
function GetLevelName(ctlid, pfillctlname, requestURL) {

    fillctlname = pfillctlname;
    if (ctlid.selectedIndex > 0) {
        var url = requestURL + ctlid.value;
        xmlHttp = GetXmlHttpObject();
        xmlHttp.open('GET', url, false);
        intCount = 0;
        xmlHttp.onreadystatechange = FillLabel;
        xmlHttp.send(null);
    }
    else {
        RemoveAllOptions(fillctlname);
    }
}

function FillLabel() {

    var len;

    if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
        var str = xmlHttp.responseText.toString();
        var select2 = document.getElementById(fillctlname);
        select2.innerHTML = str;
    }
}
function ShowHideAssignLinkTr(ddlId, lvlTr, userTr) {
 
    if (ddlId.selectedIndex > 0) {
        document.getElementById(lvlTr).style.display = "";
        document.getElementById(userTr).style.display = "";
    }
    else {
        document.getElementById(lvlTr).style.display = "none";
        document.getElementById(userTr).style.display = "none";
    }
}
//*****************************************************

function RemoveAllOptions(fillctlname) {

    var obj = document.getElementById(fillctlname);
    if (obj.length > 0) {
        obj.length = 0;

    }
    if (obj.length == 0) {
        var e = document.createElement("option");

        e.value = 0;
        if(document.all){
             e.innerText = "--Select--";
        }
       else{
         e.textContent="--Select--";
       }        
        obj.appendChild(e);
    }
}

function FillDropdown() {

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
                    document.getElementById(hidLvl).value = arrUser[1];
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
                    if(document.all){
                    e.innerText = el[1];
                    }
                    else{
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
    //alert(hidLayers);
    //alert(no);
    //         if (CtrlId.selectedIndex > 0) 
    //         {
    //alert(no);

    if (Id != "") {
        for (i = 2; i <= 10; i++) {
            if (i <= hidLayers) {
                if (i <= parseInt(no) + 2) {
                    document.getElementById(Id + '_tr' + i).style.display = "block";
                }
                else {
                    document.getElementById(Id + '_tr' + i).style.display = "none";
                }
            }
            else {
                document.getElementById(Id + '_tr' + i).style.display = "none";
            }
        }
    }
}

//=====================================
//Function to clear labels 
//Added By Pratik On 19-Jul-2010=======
var lblId;
function ClearLabels(ctlid) {
    for (var i = 2; i < 10; i++) {
        if (ctlid.id.indexOf('_drplayer') > 0) {
            lblId = ctlid.id.substring(0, ctlid.id.indexOf('_drplayer')) + '_Label' + i;
        }
        else if (ctlid.id.indexOf('_sdrplayers') > 0) {
            lblId = ctlid.id.substring(0, ctlid.id.indexOf('_sdrplayers')) + '_Labels' + i;
        }
        if(document.all){        
            document.getElementById(lblId).innerText = "";
        }else{
          document.getElementById(lblId).textContent = "";
        }
    }
}
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
function GetDataHierarchy(ctlid, pfillctlname, hidid, lblId, layer, requestURL) {
    CtrlId = ctlid;

    intLayer = parseInt(layer);

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

        //alert(intLayer);
        xmlHttp.onreadystatechange = FillDropdownHierarchy;
        xmlHttp.send(null);
    }
    else {
        RemoveAllOptions(fillctlname);
    }
}
function GetDataForUserHierarchy(ctlid, pfillctlname, hidid, hidLayers, lblId, layer, requestURL) {
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
        xmlHttp.open('GET', url, false);
        intCount = 1;
        intLayer = parseInt(layer);
        //alert('GetDataForUser');
        //alert(intCount);
        intLayer = 10;
        xmlHttp.onreadystatechange = FillDropdownHierarchy;
        xmlHttp.send(null);
    }
    else {
        RemoveAllOptions(fillctlname);
    }
}