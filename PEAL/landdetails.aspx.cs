#region  Page Info
//******************************************************************************************************************
// File Name             :   landdetails.aspx.cs
// Description           :   land details of Investor
// Created by            :   Subhasmita Behera
// Created On            :   18-Aug-2017
//  "VERION= v2"
// Modification History  :
//                          <CR no.>            <Date>                <Modified by>                 <Modification Summary>'                                                         
//
// FUNCTION NAME         :   1                18-Feb-2020              Sushant Jena           Land Required unit changed.(For MSME in Square Meter and For Large in Acres)
//******************************************************************************************************************
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ServiceModel;
using BusinessLogicLayer.Proposal;
using System.IO;
using EntityLayer.Proposal;
using System.Collections.Specialized;
using System.Net;
using System.Configuration;
using RestSharp;
using System.Text;
using Newtonsoft.Json;

public partial class landdetails : SessionCheck
{
    string strRetval = "";
    string FilePath = "";
    string FilePathHazar = "";
    string allFileVal = "";

    ProposalBAL objservice = new ProposalBAL();
    PromoterDet objProposal = new PromoterDet();
    CommonValidation objcmv = new CommonValidation();

    //protected override void OnInit(EventArgs e)
    //{
    //    base.OnInit(e);
    //    ViewStateUserKey = Session.SessionID;
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DelBtnHideshow();
            BindDistrict();

            if ((!string.IsNullOrEmpty(Request.QueryString["StrPropNo"])) || (!string.IsNullOrEmpty(Session["proposalno"] as string)))
            {
                if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
                {
                    ViewLandDetails(Convert.ToString(Request.QueryString["StrPropNo"]));
                }
                else
                {
                    if (!string.IsNullOrEmpty(Session["proposalno"] as string))
                    {
                        ViewLandDetails(Session["proposalno"].ToString());
                    }
                }
            }

            rdbExIndustry_SelectedIndexChanged(sender, e);
        }
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        btnSave_Click(sender, e);
        Session["AllFileValue"] = hdnAllFileValue.Value;
        if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            Response.Redirect("Declaration.aspx?StrPropNo=" + Request.QueryString["StrPropNo"].ToString());
        }
        else
        {
            Response.Redirect("Declaration.aspx");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Session["AllFileValue"] = hdnAllFileValue.Value;
        if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            Response.Redirect("proposeddetails.aspx?StrPropNo=" + Request.QueryString["StrPropNo"].ToString());
        }
        else
        {
            Response.Redirect("proposeddetails.aspx");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        LandDet objlanDet = new LandDet();
        try
        {
            //string str = "";
            //if (txtExtent.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtExtent.Text.Trim(), "Extent of land required(in acre)");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtLoadGrid.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtLoadGrid.Text.Trim(), "Power demand from GRID (in KW)");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtPowerDrawalCPP.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtPowerDrawalCPP.Text.Trim(), "Power drawal from CPP (in KW)");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtCapacityCPP.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtCapacityCPP.Text.Trim(), "Capacity of the CPP plant (in KW)");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtOtherSpecify.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Alphabets_Num(txtOtherSpecify.Text.Trim(), "Other (Please specify)");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtWaterRequirExist.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtWaterRequirExist.Text.Trim(), "Existing");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtWaterRequirProposed.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtWaterRequirProposed.Text.Trim(), "Proposed");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}
            //if (txtQuantumRecylling.Text != "")
            //{
            //    str = objcmv.ValidateTextbox_Mandatory_Numbers(txtQuantumRecylling.Text.Trim(), "Water required for production (in cusec)");
            //    if (str != "PASS")
            //    {
            //        return;
            //    }
            //}

            objlanDet.strAction = "A";

            if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
            {
                objlanDet.vchProposalNo = Request.QueryString["StrPropNo"];
            }
            else
            {
                objlanDet.vchProposalNo = Session["proposalno"].ToString();
            }

            objlanDet.intDistrictId = Convert.ToInt32(ddlDistrict.SelectedValue);
            objlanDet.intBlockId = Convert.ToInt32(ddlBlock.SelectedValue.ToString());

            if (txtExtent.Text.Trim() == "")
            {
                objlanDet.decExtendLand = 0;
            }
            else
            {
                objlanDet.decExtendLand = Convert.ToDecimal(txtExtent.Text.Trim());
            }

            if (rdbExIndustry.SelectedValue.ToString() == "1")
            {
                objlanDet.bitLandRequired = true;
                objlanDet.sintLandRequiredIDCO = Convert.ToInt32(ddlrequired.SelectedValue);
                objlanDet.vchIDCOInustrialName = ddlIDCOName.SelectedValue;
                objlanDet.sintLandAcquiredIDCO = Convert.ToInt32(ddlLandacquiredIDCO.SelectedValue);
            }
            else
            {
                objlanDet.bitLandRequired = false;
                objlanDet.sintLandRequiredIDCO = 0;
                objlanDet.vchIDCOInustrialName = "0";
                objlanDet.sintLandAcquiredIDCO = 0;
            }

            /*-------------------------------------------------------------------------------------------------------*/
            ///// Power requirement during production.
            /*-------------------------------------------------------------------------------------------------------*/

            if (chkGr.Checked == true)
            {
                objlanDet.bitGridSource = true;
                objlanDet.decPowerDemandGrid = Convert.ToDecimal(txtLoadGrid.Text.Trim());
            }

            if (chkCP.Checked == true)
            {
                objlanDet.bitCppSource = true;
                objlanDet.decPowerDrawalCPP = Convert.ToDecimal(txtPowerDrawalCPP.Text.Trim());
                objlanDet.decCapacityofCPPPlant = Convert.ToDecimal(txtCapacityCPP.Text.Trim());
            }

            if (chkIP.Checked == true) ////Added by Sushant Jena Dt:23-Aug-2021
            {
                objlanDet.BitIppSource = true;
                objlanDet.DecPowerProducerIpp = Convert.ToDecimal(txtPowerProducerIpp.Text.Trim());
            }

            /*-------------------------------------------------------------------------------------------------------*/
            ///// Water requirement
            /*-------------------------------------------------------------------------------------------------------*/

            if (txtWaterRequirExist.Text.Trim() != "")
            {
                objlanDet.decWaterRequireExist = Convert.ToDecimal(txtWaterRequirExist.Text.Trim());
            }
            else
            {
                objlanDet.decWaterRequireExist = 0;
            }

            if (txtWaterRequirProposed.Text.Trim() != "")
            {
                objlanDet.decWaterReqireProposed = Convert.ToDecimal(txtWaterRequirProposed.Text.Trim());
            }
            else
            {
                objlanDet.decWaterReqireProposed = 0;
            }

            if (txtWaterRequirProduction.Text != "")
            {
                objlanDet.decWaterRequirProduct = Convert.ToDecimal(txtWaterRequirProduction.Text.Trim());
            }
            else
            {
                objlanDet.decWaterRequirProduct = 0;
            }

            if (chkSurfacedWtr.Checked == true)
            {
                objlanDet.vchSurfaceWater = Convert.ToString(chkSurfacedWtr.Text);
            }
            else
            {
                objlanDet.vchSurfaceWater = "";
            }

            if (chkIdco.Checked == true)
            {
                objlanDet.vchIdcoSupply = Convert.ToString(chkIdco.Text);
            }
            else
            {
                objlanDet.vchIdcoSupply = "";
            }

            if (chkRainWtr.Checked == true)
            {
                objlanDet.vchRainWtrHarvesting = Convert.ToString(chkRainWtr.Text);
            }
            else
            {
                objlanDet.vchRainWtrHarvesting = "";
            }

            if (chkOther.Checked == true)
            {
                objlanDet.vchother = Convert.ToString(chkOther.Text);
                objlanDet.vchOtherSpecify = Convert.ToString(txtOtherSpecify.Text);
            }
            else
            {
                objlanDet.vchother = "";
                objlanDet.vchOtherSpecify = "0";
            }

            /*-------------------------------------------------------------------------------------------------------*/
            ///// Waste Water Management
            /*-------------------------------------------------------------------------------------------------------*/

            objlanDet.vchQuntRecyllingWaste = Convert.ToString(txtQuantumRecylling.Text.Trim());
            objlanDet.vchWasteConserFile = Convert.ToString(hdnWaterFile.Value);
            objlanDet.vchWaterHazardousFile = Convert.ToString(hdnHazardousFile.Value);
            objlanDet.strProjectLayOut = Convert.ToString(hdnProjectLaoutPlan.Value);
            objlanDet.strProjectLandStmt = Convert.ToString(hdnProjectlandStatement.Value);


            /*-------------------------------------------------------------------------------------------------------*/
            ///// Add Proposal Land Details
            ////*-------------------------------------------------------------------------------------------------------*/
            strRetval = objservice.ProposalLandAED(objlanDet);

            Session["AllFileValue"] = hdnAllFileValue.Value;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Record(s) saved successfully.</strong>', '" + Messages.TitleOfProject + "'); </script>", false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }

    public void ViewLandDetails(string proposalno)
    {
        List<LandDet> objProperty = new List<LandDet>();
        LandDet objinput = new LandDet();

        objinput.strAction = "V";
        objinput.vchProposalNo = proposalno;
        try
        {
            int pp = 0;
            objProperty = objservice.ViewLandDetails(objinput).ToList();
            if (objProperty != null && objProperty.Count > 0)
            {
                if (objProperty[0].bitLandRequired == true)
                {
                    rdbExIndustry.SelectedValue = "1";
                    dvD.Visible = true;
                    DivNB2.Visible = false;
                    pp = 1;
                }
                else
                {
                    rdbExIndustry.SelectedValue = "0";
                    DivNB2.Visible = true;
                }

                ddlDistrict.SelectedValue = Convert.ToString(objProperty[0].intDistrictId);

                BindBlock(ddlDistrict.SelectedValue);
                BindIndustrial(ddlDistrict.SelectedValue);

                ddlBlock.SelectedValue = Convert.ToString(objProperty[0].intBlockId);
                txtExtent.Text = Convert.ToString(objProperty[0].decExtendLand);
                ddlrequired.SelectedValue = Convert.ToString(objProperty[0].sintLandRequiredIDCO);
                ddlIDCOName.SelectedValue = Convert.ToString(objProperty[0].vchIDCOInustrialName.ToString());
                ddlLandacquiredIDCO.SelectedValue = Convert.ToString(objProperty[0].sintLandAcquiredIDCO);

                chkGr.Checked = Convert.ToBoolean(objProperty[0].bitGridSource);
                chkCP.Checked = Convert.ToBoolean(objProperty[0].bitCppSource);
                chkIP.Checked = Convert.ToBoolean(objProperty[0].BitIppSource); //// Added by Sushant Jena On Dt:- 24-Aug-2021

                txtLoadGrid.Text = Convert.ToString(objProperty[0].decPowerDemandGrid);
                txtPowerDrawalCPP.Text = Convert.ToString(objProperty[0].decPowerDrawalCPP);
                txtCapacityCPP.Text = Convert.ToString(objProperty[0].decCapacityofCPPPlant);
                txtPowerProducerIpp.Text = Convert.ToString(objProperty[0].DecPowerProducerIpp); //// Added by Sushant Jena On Dt:- 24-Aug-2021

                txtWaterRequirExist.Text = Convert.ToString(objProperty[0].decWaterRequireExist);
                txtWaterRequirProposed.Text = Convert.ToString(objProperty[0].decWaterReqireProposed);
                txtWaterRequirProduction.Text = Convert.ToString(objProperty[0].decWaterRequirProduct);

                if (objProperty[0].vchSurfaceWater == "Surface water")
                {
                    chkSurfacedWtr.Checked = true;
                }

                if (objProperty[0].vchIdcoSupply == "IDCO supply")
                {
                    chkIdco.Checked = true;
                }

                if (objProperty[0].vchRainWtrHarvesting == "Rain water harvesting")
                {
                    chkRainWtr.Checked = true;
                }

                if (objProperty[0].vchother == "Others")
                {
                    chkOther.Checked = true;
                }

                txtOtherSpecify.Text = Convert.ToString(objProperty[0].vchOtherSpecify);
                txtQuantumRecylling.Text = Convert.ToString(objProperty[0].vchQuntRecyllingWaste);

                if (Convert.ToString(objProperty[0].vchWasteConserFile) != "")
                {
                    hdnWaterFile.Value = Convert.ToString(objProperty[0].vchWasteConserFile);
                    hlDoc1.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProperty[0].vchWasteConserFile);
                    hlDoc1.Visible = true;
                    lnkDelWaterCon.Visible = true;
                    lblWaterCon.Visible = true;
                    FileUpldWaterCon.Enabled = false;
                }

                if (Convert.ToString(objProperty[0].vchWaterHazardousFile) != "")
                {
                    hdnHazardousFile.Value = Convert.ToString(objProperty[0].vchWaterHazardousFile);
                    hlDoc2.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProperty[0].vchWaterHazardousFile);
                    hlDoc2.Visible = true;
                    lnkDelHazardousFile.Visible = true;
                    lblHazardousFile.Visible = true;
                    FileUpldHazardous.Enabled = false;
                }

                if (Convert.ToString(objProperty[0].strProjectLandStmt) != "")
                {
                    hdnProjectlandStatement.Value = Convert.ToString(objProperty[0].strProjectLandStmt);
                    hypProjectlandStatement.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProperty[0].strProjectLandStmt);
                    hypProjectlandStatement.Visible = true;
                    lnkDelProjectlandStatement.Visible = true;
                    lblProjectlandStatement.Visible = true;
                    upldProjectlandStatement.Enabled = false;
                }

                if (Convert.ToString(objProperty[0].strProjectLayOut) != "")
                {
                    hdnProjectLaoutPlan.Value = Convert.ToString(objProperty[0].strProjectLayOut);
                    hypProjectLaoutPlan.NavigateUrl = "~/Enclosure/" + Convert.ToString(objProperty[0].strProjectLayOut);
                    hypProjectLaoutPlan.Visible = true;
                    lnkDelProjectLaoutPlan.Visible = true;
                    lblProjectLaoutPlan.Visible = true;
                    upldProjectLaoutPlan.Enabled = false;
                }
            }
            else
            {
                if (Session["NswsInvSwsId"] != null && Convert.ToString(Session["NswsInvSwsId"]) != "")
                {
                    ///// Pull State CAF from NSWS portal and Populate at respective places.
                    string strInvSwsId = Convert.ToString(Session["NswsInvSwsId"]);
                    PullStateCafNsws(strInvSwsId);
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PEAL");
        }
    }

    private void BindDistrict()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objservice.PopulateProjDropdowns(objProp).ToList();

        ddlDistrict.DataSource = objProjList;
        ddlDistrict.DataTextField = "vchDistName";
        ddlDistrict.DataValueField = "intDistId";
        ddlDistrict.DataBind();

        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlDistrict.Items.Insert(0, list);

    }
    private void BindBlock(string strdist)
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();

        objProp.strAction = "BL";
        objProp.vchProposalNo = strdist;
        objProjList = objservice.PopulateProjDropdowns(objProp).ToList();

        ddlBlock.DataSource = objProjList;
        ddlBlock.DataTextField = "vchBlockName";
        ddlBlock.DataValueField = "intBlockId";
        ddlBlock.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlBlock.Items.Insert(0, list);
    }
    private void BindIndustrial(string strdist)
    {
        List<LandDet> objList = new List<LandDet>();
        LandDet objProp = new LandDet();

        objProp.strAction = "I";
        objProp.vchProposalNo = strdist;
        objList = objservice.Industrial(objProp).ToList();

        ddlIDCOName.DataSource = objList;
        ddlIDCOName.DataTextField = "vchIndustrialName";
        ddlIDCOName.DataValueField = "intIndustrialEstateId";
        ddlIDCOName.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlIDCOName.Items.Insert(0, list);
    }

    protected void rdbExIndustry_SelectedIndexChanged(object sender, EventArgs e)
    {
        /*---------------------------------------------------------------------*/
        ///// The below changes incorporated by Sushant Jena on Date :- 18-Feb-2020.
        ///// Implemented on Date:---------
        ///// If project cost >= 50 Cr (LARGE),then extent of land in acres.
        ///// If project cost < 50 Cr (MSME),then extent of land in square meter.
        ///// Below unit (acres/square meter) is used for display purpose,
        ///// But the actual unit insertion is handled through stored procedure([USP_LandAndUtility]).
        ///// The changes not implemented due to some reason and finally the code reverted back on dated 09.06.2020
        /*---------------------------------------------------------------------*/
        string strProjCat = Convert.ToString(Session["ProjectCategory"]);

        if (rdbExIndustry.SelectedValue == "1")
        {
            dvD.Visible = true;
            dvB.Visible = true;
            DivNB2.Visible = false;
            //divLD.Visible = false;
            //divLD2.Visible = false;

            //if (strProjCat == "1")
            //{
            //    lblDet.Text = "required (in acre)";
            //}
            //else if (strProjCat == "2")
            //{
            //    lblDet.Text = "required (in square meter)";
            //}
        }
        else
        {
            dvD.Visible = false;
            dvB.Visible = false;
            DivNB2.Visible = true;
            //divLD.Visible = false;
            //divLD2.Visible = false;
            // lblDet.Text = "";

            //if (strProjCat == "1")
            //{
            //    lblDet.Text = "(in acre)";
            //}
            //else if (strProjCat == "2")
            //{
            //    lblDet.Text = "(in Square Meter)";
            //}
        }
    }
    protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBlock(ddlDistrict.SelectedValue);
        BindIndustrial(ddlDistrict.SelectedValue);
    }

    private bool IsFileValidWaterCon(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpldWaterCon.FileName) != ".pdf") && (Path.GetExtension(FileUpldWaterCon.FileName) != ".png") && (Path.GetExtension(FileUpldWaterCon.FileName) != ".jpg") && (Path.GetExtension(FileUpldWaterCon.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload PDF,PNG,JPG,JPEG File Only!')", true);
                    return false;
                }
                int fileSize = FileUpldWaterCon.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }

                else
                    FilePath = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "WaterCon" + Path.GetExtension(FileUpldWaterCon.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUpldWaterCon.FileName))
                {
                    allFileVal = allFileVal + FileUpldWaterCon.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    hdn1.Value = FileUpldWaterCon.FileName;
                    if (dir.Exists)
                    {
                        FileUpldWaterCon.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpldWaterCon.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    hdnWaterFile.Value = FilePath;
                    if (hdnWaterFile.Value != "")
                    {
                        hlDoc1.NavigateUrl = "~/Enclosure/" + FilePath;
                        hlDoc1.Visible = true;
                        lnkDelWaterCon.Visible = true;
                        lblWaterCon.Visible = true;
                        FileUpldWaterCon.Enabled = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }

    protected void lnkWaterCon_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidWaterCon(FileUpldWaterCon);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
            Util.LogError(ex, "PEAL");
        }
        finally
        {

        }
    }
    protected void lnkProjectLaoutPlan_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidLandLayOutStmt(upldProjectLaoutPlan);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
            Util.LogError(ex, "PEAL");
        }
        finally
        {

        }
    }

    private bool IsFileValidLandLayOutStmt(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(upldProjectLaoutPlan.FileName) != ".pdf") && (Path.GetExtension(upldProjectLaoutPlan.FileName) != ".png") && (Path.GetExtension(upldProjectLaoutPlan.FileName) != ".jpg") && (Path.GetExtension(upldProjectLaoutPlan.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload PDF,PNG,JPG,JPEG File Only!')", true);
                    return false;
                }
                int fileSize = upldProjectLaoutPlan.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }

                else
                    FilePath = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "LayOut" + Path.GetExtension(upldProjectLaoutPlan.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(upldProjectLaoutPlan.FileName))
                {
                    allFileVal = allFileVal + upldProjectLaoutPlan.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    hdn2ProjectLaoutPlan.Value = upldProjectLaoutPlan.FileName;
                    if (dir.Exists)
                    {
                        upldProjectLaoutPlan.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        upldProjectLaoutPlan.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    hdnProjectLaoutPlan.Value = FilePath;
                    if (hdnProjectLaoutPlan.Value != "")
                    {
                        hypProjectLaoutPlan.NavigateUrl = "~/Enclosure/" + FilePath;
                        hypProjectLaoutPlan.Visible = true;
                        lnkDelProjectLaoutPlan.Visible = true;
                        lblProjectLaoutPlan.Visible = true;
                        upldProjectLaoutPlan.Enabled = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }

    protected void lnkProjectlandStatement_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidLandUseStmt(upldProjectlandStatement);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
            Util.LogError(ex, "PEAL");
        }
        finally
        {

        }
    }

    private bool IsFileValidLandUseStmt(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(upldProjectlandStatement.FileName) != ".pdf") && (Path.GetExtension(upldProjectlandStatement.FileName) != ".png") && (Path.GetExtension(upldProjectlandStatement.FileName) != ".jpg") && (Path.GetExtension(upldProjectlandStatement.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload PDF,PNG,JPG,JPEG File Only!')", true);
                    return false;
                }
                int fileSize = upldProjectlandStatement.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }

                else
                    FilePath = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "LandUseStatement" + Path.GetExtension(upldProjectlandStatement.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(upldProjectlandStatement.FileName))
                {
                    allFileVal = allFileVal + upldProjectlandStatement.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    hdn2ProjectlandStatement.Value = upldProjectlandStatement.FileName;
                    if (dir.Exists)
                    {
                        upldProjectlandStatement.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        upldProjectlandStatement.SaveAs(Server.MapPath("~/Enclosure/" + FilePath));
                    }
                    hdnProjectlandStatement.Value = FilePath;
                    if (hdnProjectlandStatement.Value != "")
                    {
                        hypProjectlandStatement.NavigateUrl = "~/Enclosure/" + FilePath;
                        hypProjectlandStatement.Visible = true;
                        lnkDelProjectlandStatement.Visible = true;
                        lblProjectlandStatement.Visible = true;
                        upldProjectlandStatement.Enabled = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }
    private bool IsFileValidHazardousFile(FileUpload FileUpload1)
    {
        string strFiletype = ""; string fileExt = ""; int count = 0;
        StringCollection imageTypes = new StringCollection();
        StringCollection imageExtension = new StringCollection();
        if (FileUpload1.HasFile)
        {
            string[] allowedImageTyps = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedImageTyps);
            imageExtension.AddRange(allowedExtension);
            strFiletype = MimeType.GetMimeType(FileUpload1.FileBytes, FileUpload1.FileName);
            fileExt = System.IO.Path.GetExtension(FileUpload1.FileName);
            count = FileUpload1.FileName.Count(f => f == '.');
            string filename = System.IO.Path.GetFileNameWithoutExtension(FileUpload1.FileName);
            CommonHelperCls cmmf = new CommonHelperCls();
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Enclosure/"));
        if (FileUpload1.HasFile)
        {
            if (imageTypes.Contains(strFiletype) == true && imageExtension.Contains(fileExt) && count == 1)
            {
                if ((Path.GetExtension(FileUpldHazardous.FileName) != ".pdf") && (Path.GetExtension(FileUpldHazardous.FileName) != ".png") && (Path.GetExtension(FileUpldHazardous.FileName) != ".jpg") && (Path.GetExtension(FileUpldHazardous.FileName) != ".jpeg"))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload  PDF,PNG,JPG,JPEG file Only!')", true);
                    return false;
                }
                int fileSize = FileUpldHazardous.PostedFile.ContentLength;
                if (fileSize > (4 * 1024 * 1024))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                    return false;
                }
                FilePathHazar = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + "Hazardous" + Path.GetExtension(FileUpldHazardous.FileName), DateTime.Now);
                if (!string.IsNullOrEmpty(FileUpldHazardous.FileName))
                {
                    hdn2.Value = FileUpldHazardous.FileName;
                    allFileVal = allFileVal + FileUpldHazardous.FileName + ',';
                    hdnAllFileValue.Value = allFileVal;
                    if (dir.Exists)
                    {
                        FileUpldHazardous.SaveAs(Server.MapPath("~/Enclosure/" + FilePathHazar));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/Enclosure"));
                        FileUpldHazardous.SaveAs(Server.MapPath("~/Enclosure/" + FilePathHazar));

                    }
                    hdnHazardousFile.Value = FilePathHazar;
                    if (hdnHazardousFile.Value != "")
                    {
                        //lnkPan.Text = FilePath;
                        hlDoc2.NavigateUrl = "~/Enclosure/" + FilePathHazar;
                        hlDoc2.Visible = true;
                        lnkDelHazardousFile.Visible = true;
                        lblHazardousFile.Visible = true;
                        FileUpldHazardous.Enabled = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('FileMime type is not Correct !','" + Messages.TitleOfProject + "'); </script>", false);
                return false;
            }
        }
        return true;
    }

    protected void lnkHazardousFile_Click(object sender, EventArgs e)
    {
        try
        {
            IsFileValidHazardousFile(FileUpldHazardous);
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
            Util.LogError(ex, "PEAL");
        }
        finally
        {

        }
    }
    protected void lnkDelWaterCon_Click(object sender, EventArgs e)
    {

        UpdFileRemove(hdnWaterFile, lnkWaterCon, lnkDelWaterCon, hlDoc1, lblWaterCon, FileUpldWaterCon, "1");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "O";
            string strRetVal = objservice.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "O";
            string strRetVal = objservice.ProposalEnclosurUpdate(objProposal);
        }

    }
    protected void lnkDelHazardousFile_Click(object sender, EventArgs e)
    {

        UpdFileRemove(hdnHazardousFile, lnkHazardousFile, lnkDelHazardousFile, hlDoc2, lblHazardousFile, FileUpldHazardous, "2");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "P";
            string strRetVal = objservice.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "P";
            string strRetVal = objservice.ProposalEnclosurUpdate(objProposal);
        }

    }

    private void UpdFileRemove(HiddenField hdnFile, LinkButton lnkBtn, LinkButton lnkDel, HyperLink hplnk, Label lblFile, FileUpload updFile, string Position)
    {
        if (Position == "1")
        {
            string strPanFiletoRemove = hdn1.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "2")
        {
            string strPanFiletoRemove = hdn2.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "3")
        {
            string strPanFiletoRemove = hdn2ProjectlandStatement.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        if (Position == "4")
        {
            string strPanFiletoRemove = hdn2ProjectLaoutPlan.Value;
            string x = hdnAllFileValue.Value;
            string valueToRemove = strPanFiletoRemove;
            var result = string.Join(", ", from v in x.Split(',')
                                           where v.Trim() != valueToRemove
                                           select v);
            hdnAllFileValue.Value = result;
        }
        string filename = hdnFile.Value;
        string path = "~/Enclosure/" + filename;
        string completePath = Server.MapPath(path);
        if (System.IO.File.Exists(completePath))
        {
            System.IO.File.Delete(completePath);
            hdnFile.Value = "";
            lnkDel.Visible = false;
            lnkBtn.Visible = true;
            hplnk.Visible = false;
            lblFile.Visible = false;
            updFile.Enabled = true;
        }
    }

    private void DelBtnHideshow()
    {
        lnkDelWaterCon.Visible = false;
        lnkDelHazardousFile.Visible = false;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("landdetails.aspx");
    }

    //private void BindUnits()
    //{
    //    List<ProjectInfo> objProjList = new List<ProjectInfo>();
    //    ProjectInfo objProp = new ProjectInfo();

    //    objProp.strAction = "UT";
    //    objProp.vchProposalNo = "";
    //    objProjList = objservice.PopulateProjDropdowns(objProp).ToList();

    //    drpCp.DataSource = objProjList;
    //    drpCp.DataTextField = "vchUnitName";
    //    drpCp.DataValueField = "intUnitId";
    //    drpCp.DataBind();
    //    ListItem list = new ListItem();
    //    list.Text = "--Select--";
    //    list.Value = "0";
    //    drpCp.Items.Insert(0, list);
    //}

    protected void lnkDelProjectlandStatement_Click(object sender, EventArgs e)
    {

        UpdFileRemove(hdnProjectlandStatement, lnkProjectlandStatement, lnkDelProjectlandStatement, hypProjectlandStatement, lblProjectlandStatement, upldProjectlandStatement, "3");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "T";
            string strRetVal = objservice.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "T";
            string strRetVal = objservice.ProposalEnclosurUpdate(objProposal);
        }

    }
    protected void lnkDelProjectLaoutPlan_Click(object sender, EventArgs e)
    {
        UpdFileRemove(hdnProjectLaoutPlan, lnkProjectLaoutPlan, lnkDelProjectLaoutPlan, hypProjectLaoutPlan, lblProjectLaoutPlan, upldProjectLaoutPlan, "4");
        if (!string.IsNullOrEmpty(Session["proposalno"] as string))
        {
            objProposal.vchProposalNo = Session["proposalno"].ToString();
            objProposal.strAction = "S";
            string strRetVal = objservice.ProposalEnclosurUpdate(objProposal);
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["StrPropNo"]))
        {
            objProposal.vchProposalNo = Request.QueryString["StrPropNo"];
            objProposal.strAction = "S";
            string strRetVal = objservice.ProposalEnclosurUpdate(objProposal);
        }

    }

    /// <summary>
    /// Added by Sushant Jena On Dt:- 05-04-2021.
    /// This mathod is used pull CAF details from NSWS,Invest India portal.
    /// This method will be executed only when the user is coming from NSWS portal.
    /// </summary>
    /// <param name="strInvestorSwsId"></param>
    private void PullStateCafNsws(string strInvestorSwsId)
    {
        try
        {
            objProposal.strAction = "B";
            objProposal.strInvestorSWSId = strInvestorSwsId;
            DataTable dt = new DataTable();
            dt = objservice.GetCAFDetailsNSWS(objProposal);
            if (dt.Rows.Count > 0)
            {
                /*--------------------------------------------------------------*/
                ///Proposed location of land
                /*--------------------------------------------------------------*/
                string strLandReGovt = Convert.ToString(dt.Rows[0]["vchLandRequiredGovt"]);
                if (strLandReGovt == "Yes")
                {
                    rdbExIndustry.SelectedValue = "1";
                }
                else if (strLandReGovt == "No")
                {
                    rdbExIndustry.SelectedValue = "0";
                }

                string strDistId = Convert.ToString(dt.Rows[0]["vchLandDistrict"]);
                ddlDistrict.SelectedValue = strDistId;
                BindBlock(strDistId);
                ddlBlock.SelectedValue = Convert.ToString(dt.Rows[0]["vchLandBlock"]);                
                txtExtent.Text = Convert.ToString(dt.Rows[0]["decExtentOfLand"]);
                
                string strLandReqIdco= Convert.ToString(dt.Rows[0]["vchLandRequiredIDCO"]);
                if (strLandReqIdco != "")
                {
                    if (strLandReqIdco.ToUpper() == "YES")
                    {
                        ddlrequired.SelectedValue = "1";

                        BindIndustrial(strDistId);
                        ddlIDCOName.SelectedValue = Convert.ToString(dt.Rows[0]["vchIDCOInustrialName"]);
                    }
                    else if (strLandReqIdco.ToUpper() == "NO")
                    {
                        ddlrequired.SelectedValue = "2";

                        string strLandAcquiredIdco = Convert.ToString(dt.Rows[0]["vchLandAcquiredIDCO"]);
                        if (strLandAcquiredIdco != "")
                        {
                            if (strLandAcquiredIdco.ToUpper() == "YES")
                            {
                                ddlLandacquiredIDCO.SelectedValue = "1";
                            }
                            else if (strLandAcquiredIdco.ToUpper() == "NO")
                            {
                                ddlLandacquiredIDCO.SelectedValue = "2";
                            }
                        }
                    }
                }               

                /*--------------------------------------------------------------*/
                ///Power requirement during production
                /*--------------------------------------------------------------*/
                //txtLoadGrid.Text = Convert.ToString(dt.Rows[0]["decPowerDemandGrid"]);
                //txtPowerDrawalCPP.Text = Convert.ToString(dt.Rows[0]["decPowerDrawalCPP"]);
                string strSupplySource = Convert.ToString(dt.Rows[0]["vchPowerSource"]).Trim();
                if (strSupplySource == "Grid")
                {
                    chkGr.Checked = true;
                    txtLoadGrid.Text = Convert.ToString(dt.Rows[0]["decPowerDemandGrid"]);
                }
                else if (strSupplySource== "IPP")
                {
                    chkIP.Checked = true;
                    txtPowerProducerIpp.Text = Convert.ToString(dt.Rows[0]["decPowerProducerIpp"]);
                }
                else if (strSupplySource== "CPP")
                {
                    chkCP.Checked = true;
                    txtPowerDrawalCPP.Text = Convert.ToString(dt.Rows[0]["decPowerDrawalCPP"]);
                    txtCapacityCPP.Text = Convert.ToString(dt.Rows[0]["decCapacityOfCppPlant"]);
                }
                //txtCapacityCPP.Text = Convert.ToString(dt.Rows[0]["decCapacityOfCppPlant"]);

                /*--------------------------------------------------------------*/
                ////Water requirement
                /*--------------------------------------------------------------*/
                txtWaterRequirProduction.Text = Convert.ToString(dt.Rows[0]["decWaterRequiredProduction"]);
                txtWaterRequirProposed.Text = Convert.ToString(dt.Rows[0]["decWaterRequiredProposed"]);

                string strOtherWater = Convert.ToString(dt.Rows[0]["vchOtherWater"]);
                if (strOtherWater != null && strOtherWater != "")
                {
                    txtOtherSpecify.Text = strOtherWater;
                    chkOther.Checked = true;
                }

                string strWaterSource = Convert.ToString(dt.Rows[0]["vchWaterSource"]);
                chkOther.Checked = false;
                chkIdco.Checked = false;
                chkSurfacedWtr.Checked = false;
                chkRainWtr.Checked = false;
                if (!string.IsNullOrEmpty(strWaterSource))
                {
                    List<object> listOfWaterSource = JsonConvert.DeserializeObject<List<object>>(strWaterSource);                   
                    foreach (var listWS in listOfWaterSource)
                    {
                       if(listWS.ToString().ToUpper()== "IDCO Supply".ToUpper())
                        {
                            chkIdco.Checked = true;
                        }
                        else if (listWS.ToString().ToUpper() == "Surface water".ToUpper())
                        {
                            chkSurfacedWtr.Checked = true;
                        }
                        else if (listWS.ToString().ToUpper() == "Rain water harvesting".ToUpper())
                        {
                            chkRainWtr.Checked = true;
                        }
                    }
                }
                
                //if (strWaterSource.Contains("IDCO Supply"))
                //{
                //    chkIdco.Checked = true;
                //}

                //if (strWaterSource.Contains("Surface water"))
                //{
                //    chkSurfacedWtr.Checked = true;
                //}

                //if (strWaterSource.Contains("Rain water harvesting"))
                //{
                //    chkRainWtr.Checked = true;
                //}

                /*--------------------------------------------------------------*/
                ///Waste Water Management
                /*--------------------------------------------------------------*/
                txtQuantumRecylling.Text = Convert.ToString(dt.Rows[0]["vchQuntRecyclingWaste"]);

                /*============================================================================================*/
                ///Download Waste conservation measures document from NSWS portal and display in PEAL form. 
                /*============================================================================================*/
                string strWasteConversionDoc = Convert.ToString(dt.Rows[0]["vchWasteConversion"]);
                if (strWasteConversionDoc != "")
                {
                    DownloadDocFromNSWS(strWasteConversionDoc, "WaterCon", hdnWaterFile, lnkDelWaterCon, hlDoc1, FileUpldWaterCon, lblWaterCon);
                }

                /*============================================================================================*/
                ///Download Waste conservation measures document from NSWS portal and display in PEAL form. 
                /*============================================================================================*/
                string strWasteTreatmentDoc = Convert.ToString(dt.Rows[0]["vchWasteTreatTech"]);
                if (strWasteTreatmentDoc != "")
                {
                    DownloadDocFromNSWS(strWasteTreatmentDoc, "Hazardous", hdnHazardousFile, lnkDelHazardousFile, hlDoc2, FileUpldHazardous, lblHazardousFile);
                }


                //else if (fieldName == "Waste conservation measures")
                //{
                //    vchWasteConversion
                //}
                //else if (fieldName == "Waste water treatment technology and management of solid/hazardous waste")
                //{
                //    vchWasteTreatTech
                //}
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    private void DownloadDocFromNSWS(string strNswsFileName, string strFileType, HiddenField HdnFileName, LinkButton LnkBtnDelete, HyperLink HypLnkView, FileUpload FileUpd, Label LblMsg)
    {
        try
        {
            string strFileContentId = JsonConvert.DeserializeObject<Dictionary<string, object>>(strNswsFileName.Replace("[", "").Replace("]", ""))["value"].ToString();

            /*---------------------------------------------------------------------------------------*/
            /////Get the API address,access-id,access-secret and api key from web configuration file.
            /*---------------------------------------------------------------------------------------*/
            string strPullDocApiUrl = ConfigurationManager.AppSettings["NswsPullDocApiUrl"].ToString();
            string strAccessId = ConfigurationManager.AppSettings["NswsApiAccessId"].ToString();
            string strAccessSecret = ConfigurationManager.AppSettings["NswsApiAccessSecret"].ToString();
            string strApiKeyPullDoc = ConfigurationManager.AppSettings["NswsApiKeyPullDoc"].ToString();
            /*---------------------------------------------------------------------------------------*/

            ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
            var client = new RestClient(strPullDocApiUrl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("access-id", strAccessId);
            request.AddHeader("access-secret", strAccessSecret);
            request.AddHeader("api-key", strApiKeyPullDoc);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\"contentId\":[\"" + strFileContentId + "\"]}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            string strCmnLogMsg = "File Type:- " + strFileType + " <----> File Content Id:- " + strFileContentId;
            string strFileResposnse = response.Content.Length > 200 ? response.Content.ToString().Substring(0, 200) : response.Content.ToString();
            Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> Response Content:- " + strFileResposnse);

            if (response.Content.ToString() != "")
            {
                PullApiDoc objApp = JsonConvert.DeserializeObject<PullApiDoc>(response.Content);

                string strStatus = objApp.status;
                if (strStatus == "200")
                {
                    List<DocResponseFile> objDocRes = new List<DocResponseFile>();
                    objDocRes = objApp.response.ToList();

                    string strFileName = objDocRes[0].fileName;
                    string strFileResponse = objDocRes[0].fileResponse; ////Byte stream of the file to be downloaded.

                    byte[] data = Convert.FromBase64String(strFileResponse);
                    if (data.Length > 0)
                    {
                        if (IsFileValidNSWS(data, strFileName))
                        {
                            /*--------------------------------------------------------------*/
                            ///// Rename the file as per the GOSWIFT naming format.
                            /*--------------------------------------------------------------*/
                            string strFileExtention = Path.GetExtension(strFileName);
                            string strGoswiftFileName = Convert.ToInt32(Session["InvestorId"]) + string.Format("{0:yyyyMMddhhmmss}" + strFileType + strFileExtention, DateTime.Now);

                            /*--------------------------------------------------------------*/
                            //// Physical path of GOSWIFT document folder.
                            /*--------------------------------------------------------------*/
                            string strPath = Server.MapPath("~/Enclosure/");

                            /*--------------------------------------------------------------*/
                            /////Save the file to destination folder
                            /*--------------------------------------------------------------*/
                            FileStream fileStream = null;
                            fileStream = new FileStream(strPath + strGoswiftFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            using (System.IO.FileStream fs = fileStream)
                            {
                                fs.Write(data, 0, data.Length);

                                HdnFileName.Value = strGoswiftFileName;
                                HypLnkView.NavigateUrl = "~/Enclosure/" + strGoswiftFileName;
                                HypLnkView.Visible = true;
                                LnkBtnDelete.Visible = true;
                                LblMsg.Visible = true;
                                FileUpd.Enabled = false;
                            }
                        }
                        else
                        {
                            Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> Invalid or corrupted file found");
                        }
                    }
                    else
                    {
                        Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> No file found for download.");
                    }
                }
                else
                {
                    Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> No file found (404 Error).");
                }
            }
            else
            {
                Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strCmnLogMsg + " <----> No response found for file.");
            }
        }
        catch (Exception)
        {
            throw;
        }
    }

    public class PullApiDoc
    {
        public string status { get; set; }
        public string message { get; set; }
        public List<DocResponseFile> response { get; set; }
    }
    public class DocResponseFile
    {
        public string fileName { get; set; }
        public string fileResponse { get; set; }
    }
    private bool IsFileValidNSWS(byte[] filebyte, string strFileName)
    {
        try
        {
            StringCollection imageTypes = new StringCollection();
            StringCollection imageExtension = new StringCollection();

            string[] allowedMimeTypes = { "application/pdf", "image/jpeg", "image/png", "image/jpg" };
            string[] allowedExtension = { ".jpeg", ".jpg", ".png", ".pdf" };
            imageTypes.AddRange(allowedMimeTypes);
            imageExtension.AddRange(allowedExtension);
            string strFileMimeType = MimeType.GetMimeType(filebyte, strFileName);
            string strFileExt = System.IO.Path.GetExtension(strFileName);
            int intDotCount = strFileName.Count(f => f == '.');

            if (imageTypes.Contains(strFileMimeType) == true && imageExtension.Contains(strFileExt.ToLower()) && intDotCount == 1)
            {
                return true;

                //int fileSize = FileUpldPan.PostedFile.ContentLength;
                //if (fileSize > (4 * 1024 * 1024))
                //{
                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size is too large. Maximum file size permitted is 4 MB')", true);
                //    return false;
                //} 
            }
            else
            {
                Util.LogRequestResponse("NSWS", "NSWS_FILE_RESPONSE", strFileName + " ---- " + "File Mime type is not Correct.");
                return false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}