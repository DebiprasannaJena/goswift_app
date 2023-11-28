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
var xmlHttp = null;
var fillctlname;
var stroptDeptnames = null;
var radioadd;
var radioview;
var radiomanage;

function FillControl(ddllocation, ffillctlname, requestURL, Optional) {
   
    fillctlname = ffillctlname;
    stroptDeptnames = Optional;
    
    var obj = document.getElementById(ddllocation);
    obj = obj.value;
    if (obj.length > 0) {
        //Append the name to search for to the requestURL
        var url = requestURL + obj;
        //alert(url);
        xmlHttp = GetXmlHttpObject();
        xmlHttp.open('GET', url, true);
        xmlHttp.onreadystatechange = FillDepartment;
        xmlHttp.send(null);
    }
}

function PassPlinkId(ddlPlink, radAdd, radview, radmanage, requestURL) {
    var objddl = document.getElementById(ddlPlink);
    radioadd = document.getElementsByTagName(radAdd);
    //alert(radioadd);
    radioview = document.getElementsByTagName(radview);
    radiomanage = document.getElementsByTagName(radmanage);
    objddl = objddl.value;
    if (objddl.length > 0) {
        var url = requestURL + objddl;
        xmlHttp = GetXmlHttpObject();
        xmlHttp.open('GET', url, true);
        xmlHttp.onreadystatechange = Getpermission;
        xmlHttp.send(null);
    }
}
//--------------
function Getpermission() {
    if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
        var strperm = xmlHttp.responseText.toString();
        var radplperm1 =radioadd; //document.getElementById(radioadd);
        var radplperm2 =radioview;//document.getElementById(radioview);
        var radplperm3 =radiomanage;  //document.getElementById(radiomanage);
        var permarry = new Array();
        permarry = strperm.split('~');
        if (permarry.length >= 1) {
            for (var i = 0; i < permarry.length; i++) {
                if (permarry[i] == "Add" || permarry[i] == "Apply") {
                    //alert(radplperm1);
                    radplperm1.innerHTML = permarry[i];
                }
                if (permarry[i] == "View") {
                    radplperm2.innerHTML = permarry[i];
                }
                if (permarry[i] == "Manage") {
                    radplperm3.innerHTML = permarry[i];
                }
            } //End For
            if (radplperm1.innerHTML == "") {
                radplperm1.style.display = 'block';
            }
            if (radplperm2.innerHTML == "") {
                radplperm2.style.display = 'block';
            }
            if (radplperm3.innerHTML == "") {
                radplperm3.style.display = 'block';
            }
        }
    }
}
//--------------
function FillDepartment() {
    if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
        var str = xmlHttp.responseText.toString();
        var select2 = document.getElementById(fillctlname);
        varDeptarry = new Array();
        var arry = new Array();
        var lblarry = new Array();
        arry = str.split('~');
        Deptarry = stroptDeptnames.split(',');
        
        if (arry.length >= 1)  //If More than one element retured then add it to the dropdown lists
        {
            RemoveAllOptions(fillctlname); //Remove all the Options 
            for (var i = 0; i < arry.length; i++) {
                var e = document.createElement("option");
                var el = arry[i].split('|')
                e.value = el[0];
                e.innerText = el[1];
                select2.appendChild(e);

            } //End Loop

            if (stroptDeptnames != "Default") {
                for (var j = 0; j <Deptarry.length; j++) {
                    for (var i = 0; i < arry.length; i++) {
                        var el = arry[i].split('|')
                        e.value = el[0];
                        e.innerText = el[1];
                        
                        if (e.innerText ==Deptarry[j]) {                           
                            select2.options[i].selected = true;
                        }
                    }
                }
            }
            //End Loop
       }
        
      else {
            RemoveAllOptions(fillctlname);
          }    
       //End
   }
}
//Clear Combo Box
function RemoveAllOptions(fillctlname) {
    var obj = document.getElementById(fillctlname);
    if (obj.length > 0) {
        obj.length = 0;
    }
}



