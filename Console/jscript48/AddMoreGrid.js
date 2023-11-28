// JScript File
 
 
    var current = 0;
    function HideRows(hidCount,CountTotRows)
    {  
        var rows=CountTotRows;
        if (document.getElementById(hidCount).value=="0")       
        {
            current=1;
            for(var i=1;i<rows;i++)
            document.getElementById('Row'+i).style.display="None";
        }
        else
        {
            var i;
            current = document.getElementById(hidCount).value;
            for(i = current ;i< rows ;i++)
               {
                  document.getElementById('Row'+i).style.display="None";
               }
        }
    }
     function Showrows(nrows,CountTotRows)
    {
        var st=current;
        current=parseInt(current)+nrows;
        if(st==CountTotRows)
        {
            alert('Sorry, Cannot add More Rows.');
            current = current-1;
            return false;
        }
        for(var i=st;i<current;i++)
        {
            document.getElementById('Row'+i).style.display="";
        }  
        return false;
    }

    function Removerows(nrows)
    {
        var st=current;
        if(st==1)
        {
            alert('Sorry, Cannot Remove More Rows.');
            current=current+1;
            return false;
        }
        for (var j=2;j<=parseInt(st)+1;j++)
        {
            if (current==1)
              alert('Sorry, Cannot Remove More Rows.');
            else  
             {
              var i=parseInt(st)-1;
              document.getElementById('Row'+i).style.display="None";
              current=current-1;
             } 
         break;   
               
       }
      return false;
    }
    
    
    function ReturnData(hidCount)
    {
        document.getElementById(hidCount).value=current;
    }

 //-------validation for blank field and white space in 1st place in textbox inside gridview -----------
   function BlankGridValidation(hidCount,gridviewId,controlId,alertMsg)
    {
        var rowno,i;
        var cur;
        var val = 0;
        if (document.getElementById(hidCount).value=="0")
            cur = parseInt(current) + 2;
        else
            cur = parseInt(current) + 1;
                       
        for (i=2;i<cur;i++)
        {
            if (i<10)
                rowno = '0'+i;
            else
                rowno = i;
                
                if (!blankFieldValidation(gridviewId+"_ctl"+rowno+"_"+controlId,alertMsg))
                  {
                      return false;
                  }
                if(!WhiteSpaceValidation1st(gridviewId+"_ctl"+rowno+"_"+controlId))
                  {
                      return false;
                  }
        }
        return true;
    }
   //-------validation for decimal value in textbox inside gridview -------------------
     function GridDecimalValidation(hidCount,gridviewId,controlId)
     {
        var rowno,i;
        var cur;
        var val = 0;
        
        if (document.getElementById(hidCount).value=="0")
            cur = parseInt(current) + 2;
        else
            cur = parseInt(current) + 1;
                    
        for (i=2;i<cur;i++)
        {
            if (i<10)
                rowno = '0'+i;
            else
                rowno = i;
               
                 if (!isValidDecimal(gridviewId+"_ctl"+rowno+"_"+controlId))
                  {
                     return false;
                  }
        }
        return true;
    }
    
   //-------validation for max length in textbox inside gridview ---------------------------
    function MaxGridCtrllengthValidation(hidCount,gridviewId,controlId,alertMsg,length)
    {
        var rowno,i;
        var cur;
        var val = 0;
        
        if (document.getElementById(hidCount).value=="0")
            cur = parseInt(current) + 2;
        else
            cur = parseInt(current) + 1;
                    
        for (i=2;i<cur;i++)
        {
            if (i<10)
                rowno = '0'+i;
            else
                rowno = i;
               
                 if (!MaxlengthValidation(gridviewId+"_ctl"+rowno+"_"+controlId,alertMsg,length))
                  {
                     return false;
                  }
        }
        return true;
    }
    
     //-------validation for Single quote in textbox inside gridview ---------------------------
    function GridCtrlchkSingleQuote(hidCount,gridviewId,controlId)
    {
        var rowno,i;
        var cur;
        var val = 0;
        
        if (document.getElementById(hidCount).value=="0")
            cur = parseInt(current) + 2;
        else
            cur = parseInt(current) + 1;
                    
        for (i=2;i<cur;i++)
        {
            if (i<10)
                rowno = '0'+i;
            else
                rowno = i;
               
                 if (!chkSingleQuote(gridviewId+"_ctl"+rowno+"_"+controlId))
                  {
                     return false;
                  }
        }
        return true;
    }
    
    
    //-------validation for blank field and white space in 1st place in textbox inside gridview where gridview is in a datalist -----------
   function BlankDlGridValidation(dlrowCount,datalistId,grdrowcount,gridviewId,controlId,alertMsg)
    {
        var dlrowno,i;
        var grrowno,j;
        var dlCnt,grCnt;
        var val = 0;
        dlCnt = parseInt(dlrowCount);
        grCnt = parseInt(grdrowcount) + 2;
        for (i=0;i<dlCnt;i++)
        {
            if (i<10)
                dlrowno = '0'+i;
            else
                dlrowno = i;
                
            for(j=2;j<grCnt;j++)
            {
                if (j<10)
                    grrowno = '0'+ j;
                else
                    grrowno = j;
                
                if (!blankFieldValidation(datalistId+"_ctl"+dlrowno+"_"+gridviewId+"_ctl"+grrowno+"_"+controlId,alertMsg))
                  {
                      return false;
                  }
                if(!WhiteSpaceValidation1st(datalistId+"_ctl"+dlrowno+"_"+gridviewId+"_ctl"+grrowno+"_"+controlId))
                  {
                      return false;
                  }
            }
        }
        return true;
    }
    //---------------------------------------------------------------------------------------------------------
    //-------validation for Single Quote in textbox inside gridview where gridview is in a datalist -----------
    function DlGridCtrlSingleQuote(dlrowCount,datalistId,grdrowcount,gridviewId,controlId)
    {
        var dlrowno,i;
        var grrowno,j;
        var dlCnt,grCnt;
        var val = 0;
        dlCnt = parseInt(dlrowCount);
        grCnt = parseInt(grdrowcount) + 2;
        
        for (i=0;i<dlCnt;i++)
        {
            if (i<10)
                dlrowno = '0'+i;
            else
                dlrowno = i;
                
            for(j=2;j<grCnt;j++)
            {
                if (j<10)
                    grrowno = '0'+ j;
                else
                    grrowno = j;
                
                if (!chkSingleQuote(datalistId+"_ctl"+dlrowno+"_"+gridviewId+"_ctl"+grrowno+"_"+controlId))
                  {
                      return false;
                  }
            }
        }
        return true;
    }
    //---------------------------------------------------------------------------------------------------------
    //-------validation for blank field and white space in 1st place in textbox inside gridview where gridview is in a datalist -----------
   function MaxLenDlGridCtrlValidation(dlrowCount,datalistId,grdrowcount,gridviewId,controlId,alertMsg,maxLength)
    {
        var dlrowno,i;
        var grrowno,j;
        var dlCnt,grCnt;
        var val = 0;
        dlCnt = parseInt(dlrowCount);
        grCnt = parseInt(grdrowcount) + 2;
        for (i=0;i<dlCnt;i++)
        {
            if (i<10)
                dlrowno = '0'+i;
            else
                dlrowno = i;
                
            for(j=2;j<grCnt;j++)
            {
                if (j<10)
                    grrowno = '0'+ j;
                else
                    grrowno = j;
                
                if (!MaxlengthValidation(datalistId+"_ctl"+dlrowno+"_"+gridviewId+"_ctl"+grrowno+"_"+controlId,alertMsg,maxLength))
                  {
                      return false;
                  }
            }
        }
        return true;
    }
    //---------------------------------------------------------------------------------------------------------
  //-------validation for blank field and white space in 1st place in textbox inside gridview where gridview is in a datalist -----------
   function MaxLenDlGridCtrlValidation(dlrowCount,datalistId,grdrowcount,gridviewId,controlId,alertMsg,maxLength)
    {
        var dlrowno,i;
        var grrowno,j;
        var dlCnt,grCnt;
        var val = 0;
        dlCnt = parseInt(dlrowCount);
        grCnt = parseInt(grdrowcount) + 2;
        for (i=0;i<dlCnt;i++)
        {
            if (i<10)
                dlrowno = '0'+i;
            else
                dlrowno = i;
                
            for(j=2;j<grCnt;j++)
            {
                if (j<10)
                    grrowno = '0'+ j;
                else
                    grrowno = j;
                
                if (!MaxlengthValidation(datalistId+"_ctl"+dlrowno+"_"+gridviewId+"_ctl"+grrowno+"_"+controlId,alertMsg,maxLength))
                  {
                      return false;
                  }
            }
        }
        return true;
    }
    //---------------------------------------------------------------------------------------------------------
  //-------validation for blank field and white space in 1st place in textbox inside gridview where gridview is in a datalist -----------
   function DecimalDlGridCtrlValidation(dlrowCount,datalistId,grdrowcount,gridviewId,controlId)
    {
        var dlrowno,i;
        var grrowno,j;
        var dlCnt,grCnt;
        var val = 0;
        dlCnt = parseInt(dlrowCount);
        grCnt = parseInt(grdrowcount) + 2;
        for (i=0;i<dlCnt;i++)
        {
            if (i<10)
                dlrowno = '0'+i;
            else
                dlrowno = i;
                
            for(j=2;j<grCnt;j++)
            {
                if (j<10)
                    grrowno = '0'+ j;
                else
                    grrowno = j;
                
                if (!isValidDecimal(datalistId+"_ctl"+dlrowno+"_"+gridviewId+"_ctl"+grrowno+"_"+controlId))
                  {
                      return false;
                  }
            }
        }
        return true;
    }
    //---------------------------------------------------------------------------------------------------------
 