//******************************************************************************************************************
// File Name             :   Incentive_Name_Master.aspx.cs
// Description           :   Add,Update Incentive Name Master
// Created by            :   Sushant Kumar Jena
// Created on            :   7th Sept 2017
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
using System.Data;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;

public partial class Portal_Incentive_Incentive_Name_Master : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    ///// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillOG();
            fillDisburseType();
            fillAvailibility();
            fillNature();

            Hid_Inct_Id.Value = "0";

            if (Request.QueryString["inct_id"] != null)
            {
                string strInctId = Request.QueryString["inct_id"].ToString();
                fillEditData(strInctId);
            }
        }
    }

    ///// Function Used
    #region Function Used

    ///// Bind OG Name
    private void fillOG()
    {
        IncentiveMaster objIncentive = new IncentiveMaster();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        try
        {
            objIncentive.Action = "D";
            objLayer.BindDropdown(DrpDwn_OG_Name, objIncentive);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
            objLayer = null;
        }
    }
    ///// Bind Disbursement Type
    private void fillDisburseType()
    {
        IncentiveMaster objIncentive = new IncentiveMaster();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        try
        {
            objIncentive.Action = "E";
            objLayer.BindDropdown(DrpDwn_Disburse_Type, objIncentive);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
            objLayer = null;
        }
    }
    ///// Bind Availibility
    private void fillAvailibility()
    {
        IncentiveMaster objIncentive = new IncentiveMaster();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        try
        {
            objIncentive.Action = "F";
            objLayer.BindDropdown(DrpDwn_Avail, objIncentive);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
            objLayer = null;
        }
    }
    ///// Bind Nature
    private void fillNature()
    {
        IncentiveMaster objIncentive = new IncentiveMaster();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        try
        {
            objIncentive.Action = "G";
            objLayer.BindDropdown(DrpDwn_Nature, objIncentive);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objIncentive = null;
            objLayer = null;
        }
    }
    ///// Clear Input Fields
    private void clearFields()
    {
        Txt_Incentive_Name.Text = "";
        Txt_Max_Limit.Text = "";
        Txt_Max_Limit_Pioneer.Text = "";
        Txt_Max_Limit_Priority.Text = "";
        Txt_Short_Code.Text = "";
        Txt_Time_Frame.Text = "";

        DrpDwn_Avail.SelectedIndex = 0;
        DrpDwn_Disburse_Type.SelectedIndex = 0;
        DrpDwn_Nature.SelectedIndex = 0;
        DrpDwn_OG_Name.SelectedIndex = 0;
        DrpDwn_Periodicity.SelectedIndex = 0;

        Btn_Submit.Text = "Submit";
        Btn_Reset.Text = "Reset";
        Hid_Inct_Id.Value = "0";
    }
    ///// Fill Data for Edit
    private void fillEditData(string strInctId)
    {
        DataSet ds = new DataSet();
        IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
        Incentive_Master_Entity objBU_Entity = new Incentive_Master_Entity();
        try
        {
            int? i_OG_id = null;

            objBU_Entity.intInctId = Convert.ToInt32(strInctId);
            objBU_Entity.intOGId = i_OG_id;
            objBU_Entity.strAction = "E";
            ds = ObjIMB.Inct_Name_Master_View(objBU_Entity);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Txt_Incentive_Name.Text = ds.Tables[0].Rows[0]["vchInctName"].ToString();
                DrpDwn_OG_Name.SelectedValue = ds.Tables[0].Rows[0]["intOGId"].ToString();
                DrpDwn_Disburse_Type.SelectedValue = ds.Tables[0].Rows[0]["intDisburseType"].ToString();
                DrpDwn_Avail.SelectedValue = ds.Tables[0].Rows[0]["intAvailType"].ToString();
                DrpDwn_Nature.SelectedValue = ds.Tables[0].Rows[0]["intInctNature"].ToString();
                DrpDwn_Periodicity.SelectedValue = ds.Tables[0].Rows[0]["vchPeriodicity"].ToString();
                Txt_Max_Limit.Text = ds.Tables[0].Rows[0]["intMaxLimit"].ToString();
                Txt_Max_Limit_Priority.Text = ds.Tables[0].Rows[0]["intMaxLimitPriority"].ToString();
                Txt_Max_Limit_Pioneer.Text = ds.Tables[0].Rows[0]["intMaxLimitPioneer"].ToString();
                Txt_Time_Frame.Text = ds.Tables[0].Rows[0]["intTimeFrame"].ToString();

                if (ds.Tables[0].Rows[0]["bitProvisional"].ToString() == "True")
                {
                    RadBtn_Is_Provisional.SelectedValue = "1";
                }
                else if (ds.Tables[0].Rows[0]["bitProvisional"].ToString() == "False")
                {
                    RadBtn_Is_Provisional.SelectedValue = "0";
                }

                Txt_Short_Code.Text = ds.Tables[0].Rows[0]["vchShortCode"].ToString();
                Hid_Inct_Id.Value = ds.Tables[0].Rows[0]["intInctId"].ToString();

                Btn_Submit.Text = "Update";
                Btn_Reset.Text = "Cancel";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            ds = null;
            ObjIMB = null;
            objBU_Entity = null;
        }
    }

    #endregion

    ///// Button Submit and Update
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
        Incentive_Master_Entity objIMEntity = new Incentive_Master_Entity();
        try
        {
            objIMEntity.intInctId = Convert.ToInt32(Hid_Inct_Id.Value);
            objIMEntity.strInctName = Txt_Incentive_Name.Text.Trim();
            objIMEntity.intOGId = Convert.ToInt32(DrpDwn_OG_Name.SelectedValue);
            objIMEntity.intDisburseType = Convert.ToInt32(DrpDwn_Disburse_Type.SelectedValue);
            objIMEntity.intAvailType = Convert.ToInt32(DrpDwn_Avail.SelectedValue);
            objIMEntity.intInctNature = Convert.ToInt32(DrpDwn_Nature.SelectedValue);

            objIMEntity.intMaxLimit = Convert.ToInt32(Txt_Max_Limit.Text);

            int? maxLimitPriority = null;
            if (Convert.ToInt32(Txt_Max_Limit_Priority.Text) > 0)
            {
                maxLimitPriority = Convert.ToInt32(Txt_Max_Limit_Priority.Text);
            }
            objIMEntity.intMaxLimitPriority = maxLimitPriority;

            int? maxLimitPioneer = null;
            if (Convert.ToInt32(Txt_Max_Limit_Pioneer.Text) > 0)
            {
                maxLimitPioneer = Convert.ToInt32(Txt_Max_Limit_Pioneer.Text);
            }
            objIMEntity.intMaxLimitPioneer = maxLimitPioneer;

            objIMEntity.intTimeFrame = Convert.ToInt32(Txt_Time_Frame.Text);
            objIMEntity.strPeriodicity = DrpDwn_Periodicity.SelectedValue;
            objIMEntity.intIsProvisional = Convert.ToInt16(RadBtn_Is_Provisional.SelectedValue);
            objIMEntity.strShortCode = Txt_Short_Code.Text.Trim().ToUpper();
            objIMEntity.intCreatedBy = Convert.ToInt32(Session["UserId"]);

            if (Btn_Submit.Text == "Submit")
            {
                objIMEntity.strAction = "A";
            }
            else if (Btn_Submit.Text == "Update")
            {
                objIMEntity.strAction = "U";
            }

            ///// Record Add and Update
            string strReturnStatus = ObjIMB.Inct_Name_Master_AED(objIMEntity);
            if (Btn_Submit.Text == "Update")
            {
                clearFields();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>" + Messages.ShowMessage(strReturnStatus) + "</strong>');</script>", false);
                //ScriptManager.RegisterStartupScript(Btn_Submit, this.GetType(), "Myalert", "alert('" + Messages.ShowMessage(strReturnStatus) + "');window.location.href='View_Incentive_Name_Master.aspx?linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "';", true);
                //ScriptManager.RegisterStartupScript(Btn_Submit, this.GetType(), "Myalert", "alert('" + Messages.ShowMessage(strReturnStatus) + "');window.location.href='View_Incentive_Name_Master.aspx'", true);
            }
            else
            {
                clearFields();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>" + Messages.ShowMessage(strReturnStatus) + "</strong>','" + strProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            ObjIMB = null;
            objIMEntity = null;
        }
    }

    ///// Button Reset and Cancel
    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        if (Btn_Reset.Text == "Reset")
        {
            clearFields();
        }
        else if (Btn_Reset.Text == "Cancel")
        {
            Response.Redirect("View_Incentive_Name_Master.aspx");
        }
    }
}