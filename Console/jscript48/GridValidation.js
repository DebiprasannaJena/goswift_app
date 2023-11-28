// JScript File
//chkId  -  checkbox Id
//totalRows - Total gridview rows

//-------validation for dropdownlist inside gridview ---------------------------
    function GridDropDownValidation(chkId,totalRows,gridviewId,dropdownId,alertMsg)
    {
        
        var rowno,i;
        var cur;
        var val = 0;
        cur = parseInt(totalRows) + 2;
                       
        for (i=2;i<cur;i++)
        {
          
            if (i<10)
            {
                rowno = '0'+i;
            }
            else
            {
                rowno = i;
            }
                
                if(document.getElementById(gridviewId+"_ctl"+rowno+"_"+chkId).checked==true)
                { 
                      if(document.getElementById(gridviewId+"_ctl"+rowno+"_"+dropdownId).disabled==false)
                      {
                       if (!DropDownValidation(gridviewId+"_ctl"+rowno+"_"+dropdownId,alertMsg))
                         { 
                             return false;
                         }
                      }
                }
         }
      return true;
        }
 //----------------------------------------------------------------------------------------------------------------------      
//-------validation for blank field and white space in 1st place in textbox inside gridview -----------------------------
   function GridBlankTextValidation(chkId,totalRows,gridviewId,controlId,alertMsg)
    {
       var rowno,i;
        var cur;
        var val = 0;
        cur = parseInt(totalRows) + 2;
                       
        for (i=2;i<cur;i++)
        {
              if (i<10)
            {
                rowno = '0'+i;
            }
            else
            {
                rowno = i;
            }
                 if(document.getElementById(gridviewId+"_ctl"+rowno+"_"+chkId).checked==true)
                {
                    if (!blankFieldValidation(gridviewId+"_ctl"+rowno+"_"+controlId,alertMsg))
                      {
                          return false;
                      }
                    if(!WhiteSpaceValidation1st(gridviewId+"_ctl"+rowno+"_"+controlId))
                      {
                          return false;
                      }
                }
        }
        return true;
    }
 //----------------------------------------------------------------------------------------------------------------------      
 //-------validation for decimal value in textbox inside gridview -------------------
     function GridDecimalTextValidation(chkId,totalRows,gridviewId,controlId)
     {
        var rowno,i;
        var cur;
        var val = 0;
        cur = parseInt(totalRows) + 2;
                       
        for (i=2;i<cur;i++)
        {
             if (i<10)
            {
                rowno = '0'+i;
            }
            else
            {
                rowno = i;
            }
                 if(document.getElementById(gridviewId+"_ctl"+rowno+"_"+chkId).checked==true)
                {
                 if (!isValidDecimal(gridviewId+"_ctl"+rowno+"_"+controlId))
                  {
                     return false;
                  }
                }
        }
        return true;
    }     
 //----------------------------------------------------------------------------------------------------------------------      
//-------validation for Single quote in textbox inside gridview ---------------------------
    function GridCheckSingleQuote(chkId,totalRows,gridviewId,controlId)
    {
         var rowno,i;
        var cur;
        var val = 0;
        cur = parseInt(totalRows) + 2;
                       
        for (i=2;i<cur;i++)
        {
              if (i<10)
            {
                rowno = '0'+i;
            }
            else
            {
                rowno = i;
            }
                 if(document.getElementById(gridviewId+"_ctl"+rowno+"_"+chkId).checked==true)
                {
                 if (!chkSingleQuote(gridviewId+"_ctl"+rowno+"_"+controlId))
                  {
                     return false;
                  }
                }
        }
        return true;
    }
 //----------------------------------------------------------------------------------------------------------------------      
 //-------validation for +ve Value in textbox inside gridview ---------------------------
    function GridPositiveValueValidation(chkId,totalRows,gridviewId,controlId,alertMsg)
    {
        var rowno,i;
        var cur;
        var val = 0;
        cur = parseInt(totalRows) + 2;
                       
        for (i=2;i<cur;i++)
        {
           if (i<10)
            {
                rowno = '0'+i;
            }
            else
            {
                rowno = i;
            }
                 if(document.getElementById(gridviewId+"_ctl"+rowno+"_"+chkId).checked==true)
                {
                    if (!isUnSignedFloatValidation(gridviewId+"_ctl"+rowno+"_"+controlId,alertMsg))
                      {
                          return false;
                      }
                  
                }
        }
        return true;
    }
 //----------------------------------------------------------------------------------------------------------------------      

 //============================================================================================================================================================
 //============================================================================================================================================================
 //============================================================================================================================================================
 //-------validation for decimal value in textbox inside gridview where gridview is inside another gridview -----------
   function DecimalInnerGridValidation(chkId,outGrRowCount,outGrId,chkInnerId,inGrRowCount,inGrId,controlId)
    {
        var OutRowno,i;
        var InRowno,j;
        var OutCnt,InCnt;
        var val = 0;
        OutCnt = parseInt(outGrRowCount) + 2;
        InCnt = parseInt(inGrRowCount) + 2;
        for (i=2;i<OutCnt;i++)
        {
             if (i<10)
            {
                OutRowno = '0'+i;
            }
            else
            {
                OutRowno = i;
            }
                
            for(j=2;j<InCnt;j++)
            {
                if (j<10)
                    InRowno = '0'+ j;
                else
                    InRowno = j;
                    if(document.getElementById(outGrId+"_ctl"+OutRowno+"_"+chkId).checked==true)
                    {
                        if(document.getElementById(outGrId+"_ctl"+OutRowno+"_"+inGrId+"_ctl"+InRowno+"_"+chkInnerId).checked==true)
                        {
                            if (!isValidDecimal(outGrId+"_ctl"+OutRowno+"_"+inGrId+"_ctl"+InRowno+"_"+controlId))
                              {
                                  return false;
                              }
                        }
                    }
            }
        }
        return true;
    }
    //---------------------------------------------------------------------------------------------------------
    //-------validation for blank field and white space in 1st place in textbox inside gridview where gridview is inside another gridview -----------
   function BlankInnerGridValidation(chkId,outGrRowCount,outGrId,chkInnerId,inGrRowCount,inGrId,controlId,alertMsg)
    {
        var OutRowno,i;
        var InRowno,j;
        var OutCnt,InCnt;
        var val = 0;
        OutCnt = parseInt(outGrRowCount) + 2;
        InCnt = parseInt(inGrRowCount) + 2;
        for (i=2;i<OutCnt;i++)
        {
              if (i<10)
            {
                OutRowno = '0'+i;
            }
            else
            {
                OutRowno = i;
            }
                
            for(j=2;j<InCnt;j++)
            {
                if (j<10)
                    InRowno = '0'+ j;
                else
                    InRowno = j;
                    if(document.getElementById(outGrId+"_ctl"+OutRowno+"_"+chkId).checked==true)
                    {
                           show(outGrId+"_ctl"+OutRowno+"_"+inGrId  ); 
                          
                        if(document.getElementById(outGrId+"_ctl"+OutRowno+"_"+inGrId+"_ctl"+InRowno+"_"+chkInnerId).checked==true)
                        {
                            if (!blankFieldValidation(outGrId+"_ctl"+OutRowno+"_"+inGrId+"_ctl"+InRowno+"_"+controlId,alertMsg))
                              {
                                  return false;
                              }
                            if(!WhiteSpaceValidation1st(outGrId+"_ctl"+OutRowno+"_"+inGrId+"_ctl"+InRowno+"_"+controlId))
                              {
                                  return false;
                              }
                        }
                    }
            }
        }
        return true;
    }
    //---------------------------------------------------------------------------------------------------------
    //-------validation for Single Quote in textbox inside gridview where gridview is inside another gridview -----------
   function SingleQuoteInnerGridValidation(chkId,outGrRowCount,outGrId,chkInnerId,inGrRowCount,inGrId,controlId)
    {
        var OutRowno,i;
        var InRowno,j;
        var OutCnt,InCnt;
        var val = 0;
        OutCnt = parseInt(outGrRowCount) + 2;
        InCnt = parseInt(inGrRowCount) + 2;
        for (i=2;i<OutCnt;i++)
        {
              if (i<10)
            {
                OutRowno = '0'+i;
            }
            else
            {
                OutRowno = i;
            }
                
            for(j=2;j<InCnt;j++)
            {
                if (j<10)
                    InRowno = '0'+ j;
                else
                    InRowno = j;
                    if(document.getElementById(outGrId+"_ctl"+OutRowno+"_"+chkId).checked==true)
                    {
                        if(document.getElementById(outGrId+"_ctl"+OutRowno+"_"+inGrId+"_ctl"+InRowno+"_"+chkInnerId).checked==true)
                        {
                            if (!chkSingleQuote(outGrId+"_ctl"+OutRowno+"_"+inGrId+"_ctl"+InRowno+"_"+controlId))
                              {
                                  return false;
                              }
                        }
                    }    
            }
        }
        return true;
    }
    //---------------------------------------------------------------------------------------------------------
function hideAll(gridviewId,controlId,current)//Function to hide All the rows
{
        var rowno,i;
        var cur;
        cur = parseInt(current) + 2;
        
        for (i=2;i<cur;i++)
        {
            if (i<10)
                rowno = '0'+i;
            else
                rowno = i;
//         alert(document.getElementById(gridviewId+"_ctl"+rowno+"_"+controlId));
                
                document.getElementById(gridviewId+"_ctl"+rowno+"_"+controlId).style.display="none"; 
        }
        
}

function hideAllInner(gridviewId,innerGridId,innerRowCount,controlId,current)//Function to hide All the rows
{


        var OutRowno,i;
        var InRowno,j;
        var OutCnt,InCnt;
        var val = 0;
        OutCnt = parseInt(current) + 2;
        InCnt = parseInt(innerRowCount) + 2;
            
       
            if (OutCnt<10)
            {
                OutRowno = '0'+OutCnt;
            }
            else
            {
                OutRowno = OutCnt;
            }
                
            for(j=2;j<InCnt;j++)
            {
                if (j<10)
                {
                    InRowno = '0'+ j;
                }
                else
                {
                    InRowno = j;
                }    //alert(InRowno);
                    //alert (innerRowCount);
                    if (parseInt(innerRowCount) >0)
                    {
                   // alert(gridviewId+"_ctl"+OutRowno+"_"+innerGridId+"_ctl"+InRowno+"_"+controlId);
                      document.getElementById(gridviewId+"_ctl"+OutRowno+"_"+innerGridId+"_ctl"+InRowno+"_"+controlId).style.display="none"; 
                   }
            }
        
        return true;
//        var rowno,i;
//        var cur;
//         
//       
//         cur = parseInt(current) + 2;
//        
//        for (i=2;i<cur;i++)
//        {
//            if (i<10)
//                rowno = '0'+i;
//            else
//                rowno = i;
//                document.getElementById(gridviewId+"_ctl"+rowno+"_"+controlId).style.display="none"; 
//        }
        
}
function hide(cntrlid)//Function to hide a control
{
//alert(cntrlid);
    document.getElementById(cntrlid).style.display="none";
    return false;
}
function show(cntrlid)//Function to show a control
{
    document.getElementById(cntrlid).style.display="";
    return false;
}



 //---------------------------------------------------------------------------------------------------------
    //-------validation for blank field and white space in 1st place in textbox inside gridview where gridview is inside another gridview -----------
   function PositiveValueInnerGrid(chkId,outGrRowCount,outGrId,chkInnerId,inGrRowCount,inGrId,controlId,alertMsg)
    {
        var OutRowno,i;
        var InRowno,j;
        var OutCnt,InCnt;
        var val = 0;
        OutCnt = parseInt(outGrRowCount) + 2;
        InCnt = parseInt(inGrRowCount) + 2;
        for (i=2;i<OutCnt;i++)
        {
            if (i<10)
                OutRowno = '0'+i;
            else
                OutRowno = i;
                
            for(j=2;j<InCnt;j++)
            {
                if (j<10)
                    InRowno = '0'+ j;
                else
                    InRowno = j;
                    if(document.getElementById(outGrId+"_ctl"+OutRowno+"_"+chkId).checked==true)
                    {
                        if(document.getElementById(outGrId+"_ctl"+OutRowno+"_"+inGrId+"_ctl"+InRowno+"_"+chkInnerId).checked==true)
                        {
                            if (!isUnSignedFloatValidation(outGrId+"_ctl"+OutRowno+"_"+inGrId+"_ctl"+InRowno+"_"+controlId,alertMsg))
                              {
                                  return false;
                              }
                           
                        }
                    }
            }
        }
        return true;
    }
    //---------------------------------------------------------------------------------------------------------
  //----------------------------------------------------------------------------------------------------------------------      
 //-------validation for date in textbox inside gridview -------------------
     function LessCurrDateTxtValidation(totalRows,gridviewId,controlId,alertMsg)
     {
        var rowno,i;
        var cur;
        var val = 0;
        cur = parseInt(totalRows) + 2;
                       
        for (i=2;i<cur;i++)
        {
            if (i<10)
                rowno = '0'+i;
            else
                rowno = i;
                 
                 if (!LessCurrentDateValidator(gridviewId+"_ctl"+rowno+"_"+controlId,alertMsg))
                  {
                     return false;
                  }
                
        }
        return true;
    }     
 //----------------------------------------------------------------------------------------------------------------------      
    //-------validation for special character in textbox inside gridview ---------------------------
    function GridCtrlSpecialCharValidation(totalRows,gridviewId,controlId)
    {
        var rowno,i;
        var cur;
        var val = 0;
        
       cur = parseInt(totalRows) + 2;
                    
        for (i=2;i<cur;i++)
        {
            if (i<10)
                rowno = '0'+i;
            else
                rowno = i;
               
                 if (!isValidCharValidation(gridviewId+"_ctl"+rowno+"_"+controlId))
                  {
                     return false;
                  }
        }
        return true;
    }
    
    //----------------------------------------------------------------------------------
//-------validation for dropdownlist inside gridview -------------------------------
    function MaxValueInTextBox(totalRows,gridviewId,controlId,MaxValue,alertMsg)
    {
        var rowno,i;
        var cur;
        var val = 0;
        cur = parseInt(totalRows) + 2;
                       
        for (i=2;i<cur;i++)
        {
            if (i<10)
                rowno = '0'+i;
            else
                rowno = i;
               
                if (parseFloat(document.getElementById(gridviewId+"_ctl"+rowno+"_"+controlId).value)>parseFloat(MaxValue))
                     { 
                         alert(alertMsg + ' will not accept more than ' + MaxValue);
                         return false;
                     }
               
         }
      return true;
    }
 //----------------------------------------------------------------------------------------------------------------------      
