//******************************************************************************************************************
// File Name             :   draftedapplicationdetails.aspx.cs
// Description           :   View Drafted Application Details
// Created by            :   Sushant Kumar Jena
// Created on            :   13th Sept 2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Data;

public partial class incentives_incentiveoffered : SessionCheck
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();
    string strUserId = "";

    /////// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            strUserId = Request.QueryString["UserId"].ToString();
            fillData(strUserId);
        }
    }

    /////// Function Used
    #region Function Used

    /////// Bind Gridview and Textboxes
    private void fillData(string strUserId)
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_Drafted_Application_Entity objEntity = new Inct_Drafted_Application_Entity();

        try
        {
            objEntity.strUserID = strUserId;

            IList<Inct_Drafted_Application_Entity> list = new List<Inct_Drafted_Application_Entity>();
            list = objBAL.View_Drafted_Application(objEntity);

            if (list.Count > 0)
            {
                Txt_Industry_Code.Text = list[0].strIndustryCode.ToString();
                Txt_Industry_Name.Text = Session["IndustryName"].ToString();

                Session["UnitCode"] = list[0].strIndustryCode.ToString();
            }

            Grd_Drafted_App.DataSource = list;
            Grd_Drafted_App.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }

    #endregion

    /////// Gridview Row Button Click
    protected void LnkBtn_Details_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

            HiddenField Hid_Form_Id = (HiddenField)row.FindControl("Hid_Form_Id");
            HiddenField Hid_Inct_Id = (HiddenField)row.FindControl("Hid_Inct_Id");
            HiddenField Hid_Inct_Unique_Id = (HiddenField)row.FindControl("Hid_Inct_Unique_Id");

            if (Hid_Form_Id.Value != "")
            {
                Session["ApplySource"] = "0"; //// 0 - Drafted Application
                Response.Redirect(Hid_Form_Id.Value + "?InctUniqueNo=" + Hid_Inct_Unique_Id.Value + "" + "&IncentiveNo=" + Hid_Inct_Id.Value, false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Form Available for this Incentive !!');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void Btn_Hidden_Click(object sender, EventArgs e)
    {
        LnkBtn_Discard_Click(sender, e);
    }

    /////// Discard Drafted Application
    protected void LnkBtn_Discard_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_Drafted_Application_Entity objEntity = new Inct_Drafted_Application_Entity();

        try
        {
            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

            HiddenField Hid_Inct_Unique_Id = (HiddenField)row.FindControl("Hid_Inct_Unique_Id");

            strUserId = Request.QueryString["UserId"].ToString();

            objEntity.intInctUniqueId = Convert.ToInt32(Hid_Inct_Unique_Id.Value);
            objEntity.strUserID = strUserId;

            /*---------------------------------------------------------------*/
            /////// Data Update

            string strReturnStatus = objBAL.Drafted_Application_AED(objEntity);
            if (strReturnStatus == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Application Discarded Successfully !!</strong>','" + strProjName + "')", true);
                fillData(strUserId);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Internal server error,Please try again !!</strong>','" + strProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
        }
    }
}