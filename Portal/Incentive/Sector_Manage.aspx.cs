using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAcessLayer.Incentive;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Data;

public partial class Incentive_Sector_Manage : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    ///// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillPolicy();
            fillSector();

            if (Request.QueryString["id"] != null)
            {
                fillEditData();
                Btn_Submit.Text = "Update";
                Btn_Reset.Text = "Cancel";
            }
        }
    }

    ///// Function Used
    #region FunctionUsed

    ///// Fill Data for Edit
    private void fillEditData()
    {
        DataSet ds = new DataSet();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Sector_Master_Entity objEntity = new Sector_Master_Entity();
        try
        {
            objEntity.strAction = "E";
            objEntity.intPageNo = 0;
            objEntity.intPageSize = 0;
            objEntity.intSecTagId = Convert.ToInt32(Request.QueryString["ID"].ToString());

            ds = objLayer.Sector_Master_View(objEntity);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DrpDwn_Policy_Name.SelectedValue = ds.Tables[0].Rows[0]["intPlcId"].ToString();
                DrpDwn_Sector.SelectedValue = ds.Tables[0].Rows[0]["intSectorId"].ToString();

                fillSubSectorFiltered();

                DrpDwn_Sub_Sector.SelectedValue = ds.Tables[0].Rows[0]["intSubsectorId"].ToString();

                if (ds.Tables[0].Rows[0]["bitSectoralPolicy"].ToString() == "True")
                {
                    ChkBx_Sectorial_Policy.Checked = true;
                }
                if (ds.Tables[0].Rows[0]["bitPriorityIPR"].ToString() == "True")
                {
                    ChkBx_Priority_IPR.Checked = true;
                }

                Txt_Description.Text = ds.Tables[0].Rows[0]["vchDesc"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            ds = null;
            objLayer = null;
            objEntity = null;
        }
    }
    //// Bind Policy Name
    private void fillPolicy()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "C";
            objBAL.BindDropdown(DrpDwn_Policy_Name, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    //// Bind Sector Name
    private void fillSector()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "L";
            objBAL.BindDropdown(DrpDwn_Sector, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    //// Bind Filtered SubSector
    private void fillSubSectorFiltered()
    {
        IncentiveMaster objEntity = new IncentiveMaster();
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        try
        {
            objEntity.Action = "sub";
            objEntity.Param_2 = DrpDwn_Sector.SelectedValue;
            objBAL.BindDropdown(DrpDwn_Sub_Sector, objEntity);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objEntity = null;
            objBAL = null;
        }
    }
    //// Clear Input Fields
    private void clearFields()
    {
        DrpDwn_Policy_Name.SelectedIndex = 0;
        DrpDwn_Sector.SelectedValue = "0";
        DrpDwn_Sub_Sector.SelectedValue = "0";
        ChkBx_Sectorial_Policy.Checked = false;
        ChkBx_Priority_IPR.Checked = false;
        Txt_Description.Text = "";
    }

    #endregion

    //// Button Submit
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Sector_Master_Entity objSectorManage = new Sector_Master_Entity();
        try
        {
            if (Request.QueryString["ID"] == null)
            {
                objSectorManage.strAction = "A";
                objSectorManage.intSecTagId = 0;
            }
            else
            {
                objSectorManage.strAction = "U";
                objSectorManage.intSecTagId = Convert.ToInt32(Request.QueryString["ID"].ToString());
            }

            objSectorManage.intPolicyId = Convert.ToInt32(DrpDwn_Policy_Name.SelectedValue);
            objSectorManage.intSectorId = Convert.ToInt32(DrpDwn_Sector.SelectedValue);
            objSectorManage.intSubSecTagId = Convert.ToInt32(DrpDwn_Sub_Sector.SelectedValue);
            objSectorManage.bitPriorityIPR = (ChkBx_Sectorial_Policy.Checked) ? 1 : 0;
            objSectorManage.bitSectoralPolicy = (ChkBx_Priority_IPR.Checked) ? 1 : 0;
            objSectorManage.strDescription = Convert.ToString(Txt_Description.Text);
            objSectorManage.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());

            string strRetValue = objBAL.Sector_Master_AED(objSectorManage);

            if (strRetValue == "1")
            {
                clearFields();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>" + Messages.ShowMessage(strRetValue) + " !</strong>','" + strProjName + "')", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "Myalert", "alert('" + Messages.ShowMessage("1") + "');window.location.href='Sector_Manage.aspx?linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "';", true);
            }
            else if (strRetValue == "2")
            {
                clearFields();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('<strong>" + Messages.ShowMessage(strRetValue) + " !</strong>');</script>", false);
                //ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "Myalert", "alert('" + Messages.ShowMessage("2") + "');window.location.href='SectorView.aspx?ID=" + Request.QueryString["ID"].ToString() + "&linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "';", true);
            }
            else if (strRetValue == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>The above sector subsector already tagged for this policy !</strong>','" + strProjName + "')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>" + Messages.ShowMessage("4") + " !</strong>','" + strProjName + "')", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objSectorManage = null;
        }
    }
    //// Button Reset
    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        if (Btn_Reset.Text == "Reset")
        {
            clearFields();
        }
        else if (Btn_Reset.Text == "Cancel")
        {
            Response.Redirect("SectorView.aspx?PIndex=" + Request.QueryString["PIndex"] + "&linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "");
        }

        //if (Request.QueryString["ID"] == null) //WHILE IN UPDATE RESET OPERATION
        //{
        //    Response.Redirect("Sector_Manage.aspx?linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "");
        //}
        //else // DURING ADD RESET OPERATION
        //{
        //    Response.Redirect("SectorView.aspx?PIndex=" + Request.QueryString["PIndex"] + "&linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ranNum=" + Session["RandomNo"].ToString() + "");
        //}
    }

    //// DropDownList SelectedIndexChanged
    protected void DrpDwn_Sector_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillSubSectorFiltered();
    }
}