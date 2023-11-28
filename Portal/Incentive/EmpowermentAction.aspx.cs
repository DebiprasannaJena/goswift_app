
/*
 * Created By : Suman Lata Gupta
 * Created On : 16th November 2017
 * Description : action the incentive details of empowere committee filled by the user to the admin
 * Class name :Portal_Incentive_EmpowermentAction
 * file name : EmpowermentAction.aspx.cs
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;

public partial class Portal_Incentive_EmpowermentAction :  SessionCheck
{
    string str_Retvalue = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {
          
            fillField();
        }
    }



    private void fillField()
    {
        System.Data.DataSet ds=new System.Data.DataSet();
        try
        {
            IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
            Inct_Application_Details_Entity objBU_Entity = new Inct_Application_Details_Entity();
            objBU_Entity.strAction = "DB";
            objBU_Entity.INTINCUNQUEID = Convert.ToInt16(Request.QueryString["UniqueID"].ToString());
             ds = ObjIMB.View_IncentiveEmpowerment(objBU_Entity);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblUnitName.InnerText = ds.Tables[0].Rows[0]["vchCompName"].ToString();
                    lblIncentive.InnerText = ds.Tables[0].Rows[0]["vchInctName"].ToString(); 
                    lblAppNo.InnerText = ds.Tables[0].Rows[0]["ApplicationNum"].ToString();  
                    Session["IndustryName"] = ds.Tables[0].Rows[0]["vchCompName"].ToString();
                    hypView.NavigateUrl = "../../Incentives/" + ds.Tables[0].Rows[0]["nvchFormPreviewId"].ToString() + "?InctUniqueNo=" + Request.QueryString["UniqueID"].ToString();
                    hypReason.NavigateUrl = string.Format("~/incentives/Files/LateReason/{0}", ds.Tables[0].Rows[0]["vchReasonFile"].ToString());
                 
                    txtReason.Text = ds.Tables[0].Rows[0]["vchReason"].ToString();
                    txtPCDate.Value = ds.Tables[0].Rows[0]["dtmPCDate"].ToString();
                }
            }
            else
            {
                lblUnitName.InnerText = "--";
                lblIncentive.InnerText = "--";
                lblAppNo.InnerText = "--";
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }



    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Incentive_PCMaster objIncentive = new Incentive_PCMaster();
        try
        {
            objIncentive.strActionCode = "B";
            objIncentive.IRRemark = txtRemark.Text.Trim();
            objIncentive.strFileSanction = ddlStatusPop.SelectedValue;//Request.QueryString["AppNo"]     
            objIncentive.intAppNo =Convert.ToInt32(Request.QueryString["UniqueID"].ToString());
            objIncentive.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            objIncentive.strIndustryCode = "";        
            str_Retvalue = objLayer.AddReason(objIncentive);

            if (str_Retvalue == "1")
            {

                string qrystring = "?linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString();
                
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> alertredirect('" + qrystring + "'); </script>", false);
            }


        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

  
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewEMPCommitteApproval.aspx?PIndex=" + Request.QueryString["PIndex"] + "&linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "");
    }
}