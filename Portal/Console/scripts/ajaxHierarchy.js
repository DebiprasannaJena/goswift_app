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
        
        var fillctlname=null;
        var stData;
        var hidids;
        var hidids2;
        var hidLvl = 0;;
        var CtrlId;
        var IdToConcat;
        var LabelId;
        var flag = 0;
        var ctrlNo = 0;
        var intCount;
        var intLayer;
        var lbladmin=null;
        
        function GetDataHierarchy(ctlid, pfillctlname,hidid,lblId,layer, requestURL) {
            CtrlId = ctlid;
           
             intLayer=parseInt(layer);
             
            if(ctlid.id.indexOf('_drplayer')>0)
            {
                IdToConcat = ctlid.id.substring(0,ctlid.id.indexOf('_drplayer'));
                fillctlname = IdToConcat + pfillctlname;
                hidids = IdToConcat + hidid;
                document.getElementById(hidids).value = ctlid.value;
                LabelId = IdToConcat + lblId ;
                ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_drplayer'))+9,ctlid.id.lenghth);
            }
            else  if(ctlid.id.indexOf('_sdrplayers')>0)
            {
                 IdToConcat = ctlid.id.substring(0,ctlid.id.indexOf('_sdrplayers'));
                 fillctlname = IdToConcat + pfillctlname;
                 hidids2 = IdToConcat + hidid;
                 document.getElementById(hidids2).value = ctlid.value;
                 LabelId = IdToConcat + lblId ;
                 ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_sdrplayers'))+11,ctlid.id.length);
            }
            if (ctlid.selectedIndex > 0) {
                var url = requestURL + ctlid.value;
                xmlHttp = GetXmlHttpObject();
                xmlHttp.open('GET', url, false);
                intCount=1;
                //alert('getdata');
                //alert(intCount);
               
                //alert(intLayer);
                xmlHttp.onreadystatechange = FillDropdownHierarchy;
                xmlHttp.send(null);
            }
            else
            {
                RemoveAllOptions(fillctlname);
            }
        }
        function GetDataForUserHierarchy(ctlid, pfillctlname, hidid, hidLayers, lblId, layer, requestURL) {
         
            flag = 1;
            CtrlId = ctlid;
            if(ctlid.id.indexOf('_drplayer')>0)
            {
                IdToConcat = ctlid.id.substring(0,ctlid.id.indexOf('_drplayer'));
                fillctlname = IdToConcat + pfillctlname;
                hidids = IdToConcat + hidid;
                document.getElementById(hidids).value = ctlid.value;
                hidLvl = IdToConcat + hidLayers;
                LabelId = IdToConcat + lblId ;
                ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_drplayer'))+9,ctlid.id.length);
            }
            else  if(ctlid.id.indexOf('_sdrplayers')>0)
            {
                 IdToConcat = ctlid.id.substring(0,ctlid.id.indexOf('_sdrplayers'));
                 fillctlname = IdToConcat + pfillctlname;
                 hidids2 = IdToConcat + hidid;
                 document.getElementById(hidids2).value = ctlid.value;
                 hidLvl = IdToConcat + hidLayers;
                 LabelId = IdToConcat + lblId ;
                 ctrlNo = ctlid.id.substring(parseInt(ctlid.id.indexOf('_sdrplayers'))+11,ctlid.id.length);
            }
            if (ctlid.selectedIndex > 0) {
                var url = requestURL + ctlid.value;
                xmlHttp = GetXmlHttpObject();
                xmlHttp.open('GET', url, false);
                intCount=1;
                intLayer=parseInt(layer);
                 //alert('GetDataForUser');
                //alert(intCount);
                 intLayer=10;
                xmlHttp.onreadystatechange = FillDropdownHierarchy;
                xmlHttp.send(null);
            }
            else
            {
                RemoveAllOptions(fillctlname);
            }
        }
        function GetUserData(ctlid, pfillctlname, requestURL) {
   
            fillctlname = pfillctlname;
            if (ctlid.selectedIndex > 0) {
                var url = requestURL + ctlid.value;
                xmlHttp = GetXmlHttpObject();
                xmlHttp.open('GET', url, false);
                intCount=0;
                //alert('GetUserData');
                //alert(intCount);
                intLayer=10;
                xmlHttp.onreadystatechange = FillDropdownHierarchy;
                xmlHttp.send(null);
            }
            else
            {
                RemoveAllOptions(fillctlname);
            }
        }
        //-----------
        function GetUserData3(ctlid, pfillctlname, requestURL) {

            fillctlname = pfillctlname;
            if (ctlid.selectedIndex > 0) {
                var url = requestURL + ctlid.value;
                xmlHttp = GetXmlHttpObject();
                xmlHttp.open('GET', url, false);
                intCount = 0;
                //alert('GetUserData');
                //alert(intCount);
                intLayer = 10;
                xmlHttp.onreadystatechange = FillDropdownHierarchy;
                xmlHttp.send(null);
            }
            else {
                RemoveAllOptions(fillctlname);
            }
        }
        //----------------
        function RemoveAllOptions(fillctlname) {

            var obj = document.getElementById(fillctlname);
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

        function FillDropdownHierarchy() {
            var len;
            if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") {
                var str = xmlHttp.responseText.toString();
                var select2 = document.getElementById(fillctlname);
                var arrUser = new Array();
                var ar = new Array();
                if(str!="")
                {
                  if(flag == '1')
                    {
                        arrUser = str.split('`');
                        if(arrUser[1]!='?')
                        {
                            document.getElementById(hidLvl).value = arrUser[1];
                        }
                        if(arrUser[0]!='')
                        {
                            ar = arrUser[0].split('~');
                        }
                        else
                        {
                            ar = null;
                        }
                    }
                    else
                    {
                        ar = str.split('~');
                    }
                }
              
                if(ar != null)
                {
                    len = ar.length;
                }
                else
                {
                    len = 0;
                }
               
                if (len > 0)  //If More than one element retured then add it to the dropdown lists
                {
                    RemoveAllOptions(fillctlname); //Remove all the Options 
                    for (var i = 0; i < ar.length; i++) {
                    if(ar[i]!=""){
                            var e = document.createElement("option");
                            var el = ar[i].split('|');
                            e.value = el[0];
                           
                        if(document.all)
                        {
                            e.innerText = el[1];
                        }
                        else
                        {
                            e.textContent = el[1];
                    
                        }
                            if(flag == '1')
                             {
                                if(el[2]!='^')
                                {
                                    if(document.all)
                                    {
                                        document.getElementById(LabelId).innerText = el[2];
                                    }
                                    else{
                                        document.getElementById(LabelId).textContent = el[2];
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
                if(hidLvl!='0')
                {
                    if(document.getElementById(hidLvl).value>0)
                        {
                           if (intCount==1)
                           {
                            //alert("Ajax Hierarchy");
                            HideShowRow(document.getElementById(hidLvl).value,IdToConcat,ctrlNo);
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
      
       function HideShowRow(hidLayers,Id,no)
       {

           for(i=2; i<=10; i++)
            {
                if(i <= hidLayers)
                {
                    if(i <= parseInt(no) + 2)
                    {
                        document.getElementById(Id + '_tr' + i).style.display = "block";
                    }
                    else
                    {
                        document.getElementById(Id + '_tr' + i).style.display = "none";
                    }
                }
                else
                {
                    document.getElementById(Id + '_tr' + i).style.display = "none";
                }
            }
//         }
       }
       
       //=====================================
       //Function to clear labels 
       //Added By Pratik On 19-Jul-2010=======
       var lblId;
       function ClearLabels(ctlid)
       {
         for(var i=2; i<10 ; i++)
            {
                if(ctlid.id.indexOf('_drplayer')>0)
                {
                    lblId = ctlid.id.substring(0,ctlid.id.indexOf('_drplayer'))+'_Label'+i;
                }
                else if(ctlid.id.indexOf('_sdrplayers')>0)
                {
                    lblId = ctlid.id.substring(0,ctlid.id.indexOf('_sdrplayers'))+'_Labels'+i;
                }
                
                if(document.all)
                {
                    document.getElementById(lblId).innerText = "";
                }
                else
                {
                    document.getElementById(lblId).textContent = "";
                }
            }
            
            }
            
       function GetHierarchyAdmin(ctlid,lblid,requestURL)
        {
//            alert(ctlid.id);
//            alert(lblid.id);
//            alert(requestURL);

            if (ctlid.id.indexOf('_sdrplayers') >= 0) {

                lbladmin = lblid;
               // alert(lbladmin);
            }
//            else {
//                lbladmin = null; 
//            }
             if(ctlid.id.indexOf('_drplayer')>=0)
                {
           
                lbladmin=lblid;
            }
//            else {
//                lbladmin = null;
//            }
            //alert(ctlid.selectedIndex);
            if (ctlid.selectedIndex >= 0) 
                {
               
                var url = requestURL + ctlid.value;
               // alert(url);
                xmlHttp = GetXmlHttpObject();
                xmlHttp.open('GET', url, false);
                
             xmlHttp.onreadystatechange = GetAdminName;
              xmlHttp.send(null);
                
            }
       }
       

        
        function GetAdminName()
        {
        if (xmlHttp.readyState == 4 || xmlHttp.readyState == "complete") 
         
        {
           var strintl;
          var str = xmlHttp.responseText.toString();
          //alert(str);
          
            strint=str.length;
           
            
            if (parseInt(strint)>0)
            {
                if(document.all)
                {
                    document.getElementById(lbladmin).innerText='Administrator :' + str;
                }
                else
                {
                    document.getElementById(lbladmin).textContent='Administrator :' + str;
                }
            
            
           
           
            }
            else
            {
                if(document.all)
                {
                    document.getElementById(lbladmin).innerText="Administrator Not Assigned";
                }
                else
                {
                    document.getElementById(lbladmin).textContent="Administrator Not Assigned";
                }
            
            
            
            }
          }
        }
    
