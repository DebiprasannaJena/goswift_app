using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Data.SqlClient;

public partial class Portal_SuperAdmin_UpdateLandDetails : System.Web.UI.Page
{
    string strRetval = "";
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    ProposalBAL objService = new ProposalBAL();      

    ///Get Project Name From Web.Config File   
    readonly string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/portal/SessionRedirect.aspx", false);
            return;
        }

        ///This page can only be accessed by goadmin.
        if (Convert.ToInt32(Session["UserId"]) != 1)
        {
            Response.Redirect("~/Default.aspx");
        }

        Lbl_Msg.Text = "";

        /*-------------------------------------------------------------*/

        if (!IsPostBack)
        {
            try
            {
                DivApplicationDetails.Visible = false;

                BindDistrict();
                RadBtn_Land_Req_Govt_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "UpdateLand");
            }
        }
    }

    #region FunctionUsed

    private void BindDistrict()
    {
        try
        {
           
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "DT";
            objProp.vchProposalNo = " ";
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            DrpDwn_District.DataSource = objProjList;
            DrpDwn_District.DataTextField = "vchDistName";
            DrpDwn_District.DataValueField = "intDistId";
            DrpDwn_District.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            DrpDwn_District.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateLand");
        }
    }
    private void BindBlock(string strdist)
    {
        try
        {
           
            ProjectInfo objProp = new ProjectInfo();

            objProp.strAction = "BL";
            objProp.vchProposalNo = strdist;
            List<ProjectInfo> objProjList = objService.PopulateProjDropdowns(objProp).ToList();

            DrpDwn_Block.DataSource = objProjList;
            DrpDwn_Block.DataTextField = "vchBlockName";
            DrpDwn_Block.DataValueField = "intBlockId";
            DrpDwn_Block.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";

            DrpDwn_Block.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateLand");
        }
    }
    private void BindIndustrial(string strdist)
    {
        try
        {
           
            LandDet objProp = new LandDet();

            objProp.strAction = "I";
            objProp.vchProposalNo = strdist;
            List<LandDet> objList = objService.Industrial(objProp).ToList();

            DrpDwn_Industrial_Estate.DataSource = objList;
            DrpDwn_Industrial_Estate.DataTextField = "vchIndustrialName";
            DrpDwn_Industrial_Estate.DataValueField = "intIndustrialEstateId";
            DrpDwn_Industrial_Estate.DataBind();

            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";

            DrpDwn_Industrial_Estate.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateLand");
        }
    }
    public void GetLandDetails(string strProposalNo)
    {
        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        try
        {
            DivLandRequired.Visible = false;
            DivIndustrialEstate.Visible = false;
            DivLandAcquired.Visible = false;

            LandDet objinputdata = new LandDet();
            DataTable dt = new DataTable();

            objinputdata.strAction = "VL";
            objinputdata.vchProposalNo = strProposalNo;

            SqlCommand cmd = new SqlCommand
            {
                Connection = conn,
                CommandType = CommandType.StoredProcedure,
                CommandText = "USP_LandAndUtility"
            };

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PAction", objinputdata.strAction);
            cmd.Parameters.AddWithValue("@PvchProposalNo", objinputdata.vchProposalNo);           

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                DivApplicationDetails.Visible = true;

                int intApprovalStatus = Convert.ToInt32(dt.Rows[0]["intApprovalStatus"]);

                if (intApprovalStatus == 0) ///Draft Stage
                {
                    DivApplicationDetails.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>The application is in draft stage, So it can not be modified.</strong>');", true);
                    return;
                }
                else if (intApprovalStatus == 3) ///Rejected
                {
                    DivApplicationDetails.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>The application has already been rejected, So it can not be modified.</strong>');", true);
                    return;
                }
                else if (intApprovalStatus == 1 || intApprovalStatus==2 || intApprovalStatus == 7) ///Applied or Approved or Deferred
                {
                    Lbl_Industry_Name.Text = Convert.ToString(dt.Rows[0]["vchCompName"]);
                    Lbl_Status.Text = Convert.ToString(dt.Rows[0]["vchStatusName"]);
                    Lbl_Proposal_No.Text = Txt_Proposal_No.Text;

                    DrpDwn_District.SelectedValue = Convert.ToString(dt.Rows[0]["intDistrictId"]);

                    ///Bind Block
                    BindBlock(DrpDwn_District.SelectedValue);

                    ///Bind Industrial Estate
                    BindIndustrial(DrpDwn_District.SelectedValue);

                    DrpDwn_Block.SelectedValue = Convert.ToString(dt.Rows[0]["intBlockId"]);
                    Txt_Extent_Of_Land.Text = Convert.ToString(dt.Rows[0]["decExtendLand"]);

                    if (Convert.ToInt32(dt.Rows[0]["bitLandRequired"]) == 1)///Yes
                    {
                        RadBtn_Land_Req_Govt.SelectedValue = "1";
                        DivLandRequired.Visible = true;

                        int intLandReqIdco = Convert.ToInt32(dt.Rows[0]["sintLandRequiredIDCO"]);
                        DrpDwn_Land_Req_Idco.SelectedValue = intLandReqIdco.ToString();

                        if (intLandReqIdco == 1)///Yes
                        {
                            DrpDwn_Industrial_Estate.SelectedValue = Convert.ToString(dt.Rows[0]["vchIDCOInustrialName"]);

                            DivIndustrialEstate.Visible = true;
                            DivLandAcquired.Visible = false;
                        }
                        else if (intLandReqIdco == 2)///No
                        {
                            DrpDwn_Land_Acquired_IDCO.SelectedValue = Convert.ToString(dt.Rows[0]["sintLandAcquiredIDCO"]);

                            DivIndustrialEstate.Visible = false;
                            DivLandAcquired.Visible = true;
                        }
                    }
                    else
                    {
                        RadBtn_Land_Req_Govt.SelectedValue = "0";
                    }
                }                
            }
            else
            {
                DivApplicationDetails.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Invalid Proposal Number.</strong>');", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateLand");
            Lbl_Msg.Text = ex.Message.ToString();
        }
    }

    #endregion

    protected void RadBtn_Land_Req_Govt_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DivLandRequired.Visible = false;
            DivIndustrialEstate.Visible = false;
            DivLandAcquired.Visible = false;

            DrpDwn_Land_Req_Idco.SelectedIndex = 0;
            DrpDwn_Industrial_Estate.SelectedIndex = 0;
            DrpDwn_Land_Acquired_IDCO.SelectedIndex = 0;

            if (RadBtn_Land_Req_Govt.SelectedValue == "1")
            {
                DivLandRequired.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateLand");
        }
    }
    protected void DrpDwn_Land_Req_Idco_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {          
            DrpDwn_Industrial_Estate.SelectedIndex = 0;
            DrpDwn_Land_Acquired_IDCO.SelectedIndex = 0;

            if (DrpDwn_Land_Req_Idco.SelectedValue == "1")
            {
                DivIndustrialEstate.Visible = true;
                DivLandAcquired.Visible = false;
            }
            else
            {
                DivIndustrialEstate.Visible = false;
                DivLandAcquired.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateLand");
        }
    }
    protected void DrpDwn_District_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindBlock(DrpDwn_District.SelectedValue);
            BindIndustrial(DrpDwn_District.SelectedValue);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateLand");
        }
    }
    
    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        try
        {
            DivApplicationDetails.Visible = false;

            if (Txt_Proposal_No.Text.Trim() == "")
            {
                Txt_Proposal_No.Focus();
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "jAlert('<strong>Please enter proposal number.</strong>')", true);
                return;
            }
            else
            {
                GetLandDetails(Txt_Proposal_No.Text);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateLand");
        }
    }
    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        DivApplicationDetails.Visible = false;
        Txt_Proposal_No.Text = string.Empty;
    }
    protected void Btn_Update_Click(object sender, EventArgs e)
    {
        LandDet objlanDet = new LandDet();
        try
        {
            #region Validation

            if (RadBtn_Land_Req_Govt.SelectedIndex == -1)
            {
                RadBtn_Land_Req_Govt.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please select land required from government.</strong>', '" + strProjName + "'); </script>", false);
                return;
            }

            if (DrpDwn_District.SelectedIndex == 0)
            {
                DrpDwn_District.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please select district.</strong>', '" + strProjName + "'); </script>", false);
                return;
            }

            if (DrpDwn_Block.SelectedIndex == 0)
            {
                DrpDwn_Block.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please select block.</strong>', '" + strProjName + "'); </script>", false);
                return;
            }

            if (Txt_Extent_Of_Land.Text.Trim() == "")
            {
                Txt_Extent_Of_Land.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please enter extent of land (in acre).</strong>', '" + strProjName + "'); </script>", false);
                return;
            }

            if (RadBtn_Land_Req_Govt.SelectedValue == "1") ///Yes
            {
                if (DrpDwn_Land_Req_Idco.SelectedIndex == 0)
                {
                    DrpDwn_Land_Req_Idco.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please select whether land is required in IDCO industrial estate.</strong>', '" + strProjName + "'); </script>", false);
                    return;
                }

                if (DrpDwn_Land_Req_Idco.SelectedValue == "1")///Yes
                {
                    if (DrpDwn_Industrial_Estate.SelectedIndex == 0)
                    {
                        DrpDwn_Industrial_Estate.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please select name of the IDCO industrial estate.</strong>', '" + strProjName + "'); </script>", false);
                        return;
                    }
                }
                else if (DrpDwn_Land_Req_Idco.SelectedValue == "2")///No
                {
                    if (DrpDwn_Land_Acquired_IDCO.SelectedIndex == 0)
                    {
                        DrpDwn_Land_Acquired_IDCO.Focus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Please select whether land to be acquired by IDCO.</strong>', '" + strProjName + "'); </script>", false);
                        return;
                    }
                }
            } 

            #endregion

            /*------------------------------------------------------------------*/

            objlanDet.strAction = "L";
            objlanDet.vchProposalNo = Lbl_Proposal_No.Text;
            objlanDet.intDistrictId = Convert.ToInt32(DrpDwn_District.SelectedValue);
            objlanDet.intBlockId = Convert.ToInt32(DrpDwn_Block.SelectedValue.ToString());

            if (Txt_Extent_Of_Land.Text.Trim() == "")
            {
                objlanDet.decExtendLand = 0;
            }
            else
            {
                objlanDet.decExtendLand = Convert.ToDecimal(Txt_Extent_Of_Land.Text.Trim());
            }

            if (RadBtn_Land_Req_Govt.SelectedValue.ToString() == "1")///Yes
            {
                objlanDet.bitLandRequired = true;
                objlanDet.sintLandRequiredIDCO = Convert.ToInt32(DrpDwn_Land_Req_Idco.SelectedValue);
                objlanDet.vchIDCOInustrialName = DrpDwn_Industrial_Estate.SelectedValue;
                objlanDet.sintLandAcquiredIDCO = Convert.ToInt32(DrpDwn_Land_Acquired_IDCO.SelectedValue);
            }
            else
            {
                objlanDet.bitLandRequired = false;
                objlanDet.sintLandRequiredIDCO = 0;
                objlanDet.vchIDCOInustrialName = "0";
                objlanDet.sintLandAcquiredIDCO = 0;
            }

            /*-----------------------------------------*/
            ///DML Operation
            /*-----------------------------------------*/
            strRetval = objService.ProposalLandAED(objlanDet);
            if (strRetval == "2")
            {
               
                DivApplicationDetails.Visible = false;
                ScriptManager.RegisterStartupScript(Btn_Update, typeof(string), "", "jAlert('<strong>Land details updated successfully.</strong>')", true);
               
            }
            else if (strRetval == "3")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something went wrong,Please try again.</strong>');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "UpdateLand");
        }
    }
    protected void Btn_Cancel_Click(object sender, EventArgs e)
    {
        DivApplicationDetails.Visible = false;
    }

    public void Clear()
    {
        Lbl_Industry_Name.Text = string.Empty;
        Lbl_Status.Text = string.Empty;
        Txt_Extent_Of_Land.Text = string.Empty;
        DrpDwn_District.Items.Clear();
        DrpDwn_Block.Items.Clear();
        DrpDwn_Industrial_Estate.Items.Clear();
        DrpDwn_Land_Acquired_IDCO.Items.Clear();
        DrpDwn_Land_Req_Idco.Items.Clear();
    }
}