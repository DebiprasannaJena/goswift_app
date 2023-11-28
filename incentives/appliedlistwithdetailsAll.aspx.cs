using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Data;

public partial class incentives_appliedlistwithdetailsAll : System.Web.UI.Page
{
    /////// Get Project Name From Web.Config File   
    string strProjName = System.Configuration.ConfigurationManager.AppSettings["ProjectName"].ToString();

    //// Page Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("incentiveoffered.aspx");
        }

        if (!IsPostBack)
        {
            ModalPopupExtender1.Hide();
            if (validateInctApply())
            {
                fillSummary();
                // fillGrid_Policy();
                fillGrid_Incentive();
                fillGrid_Incentive_With_Eligibel();
            }
        }
    }

    //// Function Used
    #region Function Used

    //// validate page either PEAL or PC available
    private bool validateInctApply()
    {
        bool b_returnValue = true;

        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Validate_Inct_Apply_Entity objEntity = new Validate_Inct_Apply_Entity();

        try
        {
            objEntity.intUserID = Convert.ToInt32(Session["InvestorId"]);

            string strReturnValue = objBAL.Validate_Inct_Apply(objEntity);
            if (strReturnValue == "B")
            {
                string message = @"You cannot apply for incentives because there is no valid Industrial Unit/Enterprise data against your login. \n \n1.To apply for project evaluation and(or) allotment of land go to the proposal tab. \n\n2.If your unit/enterprise is in production ,first apply for production certificate.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>alertredirect('" + message + "');</script>", false);
                b_returnValue = false;
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

        return b_returnValue;
    }

    //// Fill Summary
    private void fillSummary()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_Applied_With_PC_Entity objEntity = new Inct_Applied_With_PC_Entity();

        try
        {
            objEntity.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            objEntity.strAction = "S";

            IList<Inct_Applied_With_PC_Entity> list = new List<Inct_Applied_With_PC_Entity>();
            list = objBAL.View_Summary_With_PC(objEntity);

            if (list.Count > 0)
            {
                Lbl_Industry_Code.Text = Session["UnitCode"].ToString(); //list[0].strIndustryCode.ToString();
                Lbl_Comp_Name.Text = list[0].strCompName.ToString();

                string strFFCI = list[0].dtmFFCI.ToString();
                if (strFFCI != "")
                {
                    Lbl_FFCI_Date.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(strFFCI));
                }
                else
                {
                    Lbl_FFCI_Date.Text = "";
                }

                string strProdComm = list[0].dtmProdComm.ToString();
                if (strProdComm != "")
                {
                    Div_Prod_Comm.Visible = true;
                    Lbl_Prod_Comm_Date.Text = String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(strProdComm));
                }
                else
                {
                    Div_Prod_Comm.Visible = false;
                    Lbl_Prod_Comm_Date.Text = "";
                }

                Lbl_Unit_Type.Text = list[0].strUnitCat.ToString();
                Lbl_District_Category.Text = list[0].strDistCat.ToString();

                if (list[0].strRating.ToString() != "")
                {
                    Lbl_Empl_Invest_Rating.Text = list[0].strRating.ToString();
                    Div_Empl_Rating.Visible = true;
                }
                else
                {
                    Div_Empl_Rating.Visible = false;
                    Lbl_Empl_Invest_Rating.Text = "";
                }
                Lbl_Total_Capital_Invest.Text = list[0].strTotCapInvest.ToString();
                Lbl_PM_Invest.Text = list[0].strPlantMachInvest.ToString();
                Lbl_Sector_Name.Text = list[0].strSectorName.ToString();
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

    //// Fill Policy
    //private void fillGrid_Policy()
    //{
    //    IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
    //    Inct_Applied_With_PC_Entity objEntity = new Inct_Applied_With_PC_Entity();

    //    try
    //    {
    //        objEntity.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
    //        objEntity.strAction = "P";

    //        IList<Inct_Applied_With_PC_Entity> list = new List<Inct_Applied_With_PC_Entity>();
    //        list = ObjIMB.View_Policy_List_With_PC(objEntity);

    //        List<Inct_Applied_With_PC_Entity> tlistParent = list.Where(s => s.strPlcCat == "1").ToList();
    //        List<Inct_Applied_With_PC_Entity> tlistSectoral = list.Where(s => s.strPlcCat == "2").ToList();
    //        List<Inct_Applied_With_PC_Entity> tlistOther = list.Where(s => s.strPlcCat == "3").ToList();

    //        Grd_Parent_Policy.DataSource = tlistParent;
    //        Grd_Parent_Policy.DataBind();

    //        Grd_Sectoral_Policy.DataSource = tlistSectoral;
    //        Grd_Sectoral_Policy.DataBind();

    //        Grd_Other_policy.DataSource = tlistOther;
    //        Grd_Other_policy.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "Incentive");
    //    }
    //    finally
    //    {
    //        ObjIMB = null;
    //        objEntity = null;
    //    }
    //}

    //// Fill Incentive List

    private void fillGrid_Incentive()
    {
        IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
        Inct_Applied_With_PC_Entity objEntity = new Inct_Applied_With_PC_Entity();

        try
        {
            objEntity.intInvestorId = Convert.ToInt32(Session["InvestorId"]);
            objEntity.strAction = "I";

            IList<Inct_Applied_With_PC_Entity> list = new List<Inct_Applied_With_PC_Entity>();
            list = ObjIMB.View_Inct_List_With_PC(objEntity);

            Grd_Incentives.DataSource = list;
            Grd_Incentives.DataBind();

            Grd_Without_Eligibility.DataSource = list;
            Grd_Without_Eligibility.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            ObjIMB = null;
            objEntity = null;
        }
    }

    //// Fill Incentive List with Eligibility
    private void fillGrid_Incentive_With_Eligibel()
    {
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();
        Basic_Unit_Details_Entity objEntity = new Basic_Unit_Details_Entity();
        DataSet ds = new DataSet();
        try
        {
            objEntity.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objEntity.strIndustryCode = Session["UnitCode"].ToString();

            ds = objLayer.Bind_Inct_With_Eligible(objEntity);

            ///// Bind Applicable Policies
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataTable dt = new DataTable();

                /*-------------------------------------------------------------*/
                ///// Parent Policies

                ds.Tables[0].DefaultView.RowFilter = "intPlcCat = 1";
                dt = (ds.Tables[0].DefaultView).ToTable();

                Grd_Parent_Policy.DataSource = dt;
                Grd_Parent_Policy.DataBind();

                /*-------------------------------------------------------------*/
                ///// Sectoral Policies

                ds.Tables[0].DefaultView.RowFilter = "intPlcCat = 2";
                dt = (ds.Tables[0].DefaultView).ToTable();

                Grd_Sectoral_Policy.DataSource = dt;
                Grd_Sectoral_Policy.DataBind();

                /*-------------------------------------------------------------*/
                ///// Other  Policies

                ds.Tables[0].DefaultView.RowFilter = "intPlcCat = 3";
                dt = (ds.Tables[0].DefaultView).ToTable();

                Grd_Other_policy.DataSource = dt;
                Grd_Other_policy.DataBind();
            }

            ////// Bind Eligible Incentives
            if (ds.Tables[1].Rows.Count > 0)
            {
                Grd_Incentives_New.DataSource = ds.Tables[1];
                Grd_Incentives_New.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objLayer = null;
            objEntity = null;
        }
    }

    #endregion

    //// Gridview RowDataBound
    protected void Grd_Incentives_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField Hid_OG_Doc = (HiddenField)e.Row.FindControl("Hid_OG_Doc");
            HyperLink Hlnk_Policy_Name = (HyperLink)e.Row.FindControl("Hlnk_Policy_Name");

            //Hlnk_Policy_Name.NavigateUrl = "../Portal/OGDoc/" + Hid_OG_Doc.Value;
            Hlnk_Policy_Name.NavigateUrl = Hid_OG_Doc.Value;

            HiddenField Hid_Prov_File_Name = (HiddenField)e.Row.FindControl("Hid_Prov_File_Name");
            LinkButton LnkBtn_Read_Provision = (LinkButton)e.Row.FindControl("LnkBtn_Read_Provision");
            if (Hid_Prov_File_Name.Value != "")
            {
                LnkBtn_Read_Provision.Visible = true;
            }
            else
            {
                LnkBtn_Read_Provision.Visible = false;
            }
        }
    }

    //// Gridview Row Button Click For Apply
    protected void LnkBtn_Apply_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_Applied_With_PC_Entity objEntity = new Inct_Applied_With_PC_Entity();

        try
        {
            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

            HiddenField Hid_Form_Id = (HiddenField)row.FindControl("Hid_Form_Id");
            HiddenField Hid_Inct_Id = (HiddenField)row.FindControl("Hid_Inct_Id");

            if (Hid_Form_Id.Value != "")
            {
                objEntity.intInctId = Convert.ToInt32(Hid_Inct_Id.Value);
                objEntity.intInvestorId = Convert.ToInt32(Session["InvestorId"]);

                string strReturnStatus = objBAL.Check_Time_Frame(objEntity);

                /*--------------------------------------------------*/
                string strfy = "";
                if (strReturnStatus.Contains('-'))
                {
                    string[] ret = strReturnStatus.Split('-');
                    strReturnStatus = ret[0].ToString();
                    strfy = ret[1].ToString();
                }
                /*--------------------------------------------------*/

                if (strReturnStatus == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You have already applied for this incentive. !</strong>','" + strProjName + "')", true);
                    return;
                }
                else if (strReturnStatus == "2")
                {
                    Session["FyYear"] = strfy;
                    Session["ApplySource"] = "1";
                    Response.Redirect(Hid_Form_Id.Value + "?IncentiveNo=" + Hid_Inct_Id.Value + "", false);
                }
                else if (strReturnStatus == "3") ///// Time Frame Crossed for ONETIME Application
                {
                    Session["FyYear"] = strfy;
                    Session["ApplySource"] = "1";
                    Response.Redirect("IncentiveLateReason.aspx?IncentiveNo=" + Hid_Inct_Id.Value + "", false);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible for apply this incentive,because your time frame for filing your application has expired. !</strong>','" + strProjName + "')", true);
                    //return;
                }
                else if (strReturnStatus == "4")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You have already applied for last financial year. !</strong>','" + strProjName + "')", true);
                    return;
                }
                else if (strReturnStatus == "5")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>Your application is drafted, please go through drafted application details. !</strong>','" + strProjName + "')", true);
                    return;
                }
                else if (strReturnStatus == "6") ///// Time Frame Crossed for ANNUAL Application
                {
                    Session["FyYear"] = strfy;
                    Session["ApplySource"] = "1";
                    Response.Redirect("IncentiveLateReason.aspx?IncentiveNo=" + Hid_Inct_Id.Value + "", false);
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for this financial year because,you have exceeded the time frame of the application. !</strong>','" + strProjName + "')", true);
                    //return;
                }
                else if (strReturnStatus == "7")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for this incentive because,you have already applied for the maximum numbers of times that this incentive can be availed. !</strong>','" + strProjName + "')", true);
                    return;
                }
                //else if (strReturnStatus == "8")
                //{
                //    Response.Redirect("IncentiveLateReason.aspx?IncentiveNo=" + Hid_Inct_Id.Value + "", false);
                //    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for this incentive because,your maximum time frame for applying for this incentive has expired. !</strong>','" + strProjName + "')", true);
                //    //return;
                //}
                else if (strReturnStatus == "9")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>You are not eligible to apply for this incentive because,your financial year yet to be completed. !</strong>','" + strProjName + "')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>No form available for this incentive !</strong>','" + strProjName + "')", true);
                return;
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

    //// Gridview Row Button Click For View OG
    protected void LnkBtn_Apply_WO_Eligibility_Click(object sender, EventArgs e)
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_Applied_With_PC_Entity objEntity = new Inct_Applied_With_PC_Entity();

        try
        {
            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

            HiddenField Hid_Form_Id = (HiddenField)row.FindControl("Hid_Form_Id");
            HiddenField Hid_Inct_Id = (HiddenField)row.FindControl("Hid_Inct_Id");

            if (Hid_Form_Id.Value != "")
            {
                objEntity.intInctId = Convert.ToInt32(Hid_Inct_Id.Value);
                objEntity.intInvestorId = Convert.ToInt32(Session["InvestorId"]);

                string strReturnStatus = objBAL.Check_Time_Frame(objEntity);

                /*--------------------------------------------------*/
                string strfy = "";
                if (strReturnStatus.Contains('-'))
                {
                    string[] ret = strReturnStatus.Split('-');
                    strReturnStatus = ret[0].ToString();
                    strfy = ret[1].ToString();
                }
                /*--------------------------------------------------*/

                Session["FyYear"] = strfy;
                Session["ApplySource"] = "1";
                Response.Redirect(Hid_Form_Id.Value + "?IncentiveNo=" + Hid_Inct_Id.Value + "");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>No Form Available for this Incentive !</strong>','" + strProjName + "')", true);
                return;
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

    //// Gridview Row Button Click For View Provision
    protected void LnkBtn_Read_Provision_Click(object sender, EventArgs e)
    {
        try
        {
            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

            HiddenField Hid_Prov_File_Name = (HiddenField)row.FindControl("Hid_Prov_File_Name");
            if (Hid_Prov_File_Name.Value != "")
            {
                Img_Provision.Attributes.Add("src", "../incentives/Files/Provision/" + Hid_Prov_File_Name.Value);
                ModalPopupExtender1.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>No Form Available for this Incentive !</strong>','" + strProjName + "')", true);
                return;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }
}