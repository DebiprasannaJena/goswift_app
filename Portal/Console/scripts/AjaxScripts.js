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
function FillControl(ddllocation, requestURL) 
{
//    fillctlname = ffillctlname;
//    stroptDeptnames = Optional;
    var obj = document.getElementById(ddllocation);
    obj = obj.value;
    if (obj.length > 0) {
        //Append the name to search for to the requestURL
        var url = requestURL + obj;
        //alert(url);
        xmlHttp = GetXmlHttpObject();
        xmlHttp.open('GET', url, true);
      //  xmlHttp.onreadystatechange = FillDepartment;
        xmlHttp.send(null);
    }
}