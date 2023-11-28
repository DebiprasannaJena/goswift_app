#region  PAGE INFO
//******************************************************************************************************************
// File Name             :   webfooter.ascx.cs
// Description           :   Apply Feedback Development
// Created by            :   Sanghamitra Samal
// Created On            :   05 Sep 2017
// Modification History  :
//                          <CR no.>                      <Date>                <Modified by>                        <Modification Summary>'                                                         
//
// FUNCTION NAME         :   
//******************************************************************************************************************
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Common;
using EntityLayer.Common;

public partial class Application_includes_footer : System.Web.UI.UserControl
{
    #region Variable Declaration
   
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        BindRepeater();
    } 
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
      

    }
    private void BindRepeater()
    {
        
    }
    
    public void Clear()
    {
      
    }
}