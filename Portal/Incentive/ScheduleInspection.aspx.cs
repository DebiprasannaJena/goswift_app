using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Globalization;
using System.Data;


public partial class Portal_Incentive_ScheduleInspection : SessionCheck
{
    string str_Retvalue = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null)
        {
            Response.Redirect("../default.aspx");
        }
        if (!IsPostBack)
        {
            try
            {
                txtDateOfInspection.Attributes.Add("readonly", "readonly");
                hdnType.Value = Request.QueryString["type"].ToString();
                fillField();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Incentive");
            }
        }
    }



    private void fillField()
    {
        try
        {
            DataSet objds = new DataSet();
            PcSearch objSearch = new PcSearch()
            {
                strActionCode = "ir",
                intPageIndex = 0,
                intPageSize = 0,
                strFromDate = string.Empty,
                strToDate = string.Empty,
                intAppFor = Convert.ToInt32(Request.QueryString["id"]),
            };
            
            IncentiveMasterBusinessLayer objBuisness = new IncentiveMasterBusinessLayer();
            if(hdnType.Value=="1")
               objds = objBuisness.Incentive_PcForm_View(objSearch);
            else
                objds = objBuisness.Incentive_PcForm_Large_View(objSearch);
            if (objds.Tables[0].Rows.Count > 0)
            {
                DataTable dt=objds.Tables[0];
                lblCompanyName.InnerText=dt.Rows[0]["vchCompName"].ToString();
                lblAppNo.InnerText = dt.Rows[0]["vchAppFormattedNo"].ToString();
                lblApplicationFor.InnerText = dt.Rows[0]["appfor"].ToString();
                lblOrgType.InnerText = dt.Rows[0]["OrgType"].ToString();
                txtDateOfInspection.Value = dt.Rows[0]["dtmIRScheduleOn"].ToString();
                lblUnitCat.InnerText = dt.Rows[0]["unitcat"].ToString();
                txtRemark.Text = dt.Rows[0]["vchIRRemark"].ToString();
                if (dt.Rows[0]["dtmIRScheduleOn"].ToString() != "")
                {
                    btnSave.Text = "Update";
                }
            }
            else
            {
                lblCompanyName.InnerText = "--";
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
        Incentive_PCMaster objPCMaster = new Incentive_PCMaster();

        try{
            objPCMaster.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            objPCMaster.IRRemark = txtRemark.Text.Trim();
            objPCMaster.dtmIRScheduleOn = txtDateOfInspection.Value;
            objPCMaster.intAppFor = Convert.ToInt32(Request.QueryString["id"]);
            objPCMaster.strActionCode = "sc";
            if (hdnType.Value == "1")
              str_Retvalue =Convert.ToString(objLayer.Incentive_PcDetails_AED(objPCMaster));
            else
               str_Retvalue = Convert.ToString(objLayer.PC_Large_AED(objPCMaster));
            if (str_Retvalue == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('Inspection Scheduled Successfully');</script>", false);
                //ScriptManager.RegisterStartupScript(btnSave, this.GetType(), "Myalert", "alert('Inspection Scheduled Successfully');window.location.href='ViewIncentiveApplication.aspx?ID=" + Request.QueryString["ID"].ToString() + "&linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "';", true);
                        
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
    

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewIncentiveApplication.aspx?PIndex=" + Request.QueryString["PIndex"] + "&linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "");
    }
}