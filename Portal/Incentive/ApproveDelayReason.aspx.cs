/*--'*******************************************************************************************************************
' File Name         : ApproveDelayReason.aspx 
' Description       : View details of Delay Reason & Approve Details
' Created by        : Gouri Shankar Chhotray
' Created On        : 15 Dec 2017
' Modification History:
' Procedure used    : USP_INCT_EC_DELAY_VIEW/USP_INCT_DELAY_REASON_APPROVE
' Table Used        : T_INCT_EC_DELAY_REASON

'   <CR no.>                          <Date>                <Modified by>        <Modification Summary>                   <Instructed By>        

'   *********************************************************************************************************************-
*/

using System;
using System.Data;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

public partial class Portal_Incentive_ApproveDelayReason : SessionCheck
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Hid_Delay_Id.Value = Request.QueryString["Did"].ToString();

        Txt_Time_Allowed.Attributes.Add("readonly", "readonly");
        if (!IsPostBack)
        {
            DataBind();
        }
    }

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        Inct_EC_Delay_Reason_Entity objEntity = new Inct_EC_Delay_Reason_Entity();
        IncentiveMasterBusinessLayer objLayer = new IncentiveMasterBusinessLayer();

        try
        {
            objEntity.strAction = "S";
            objEntity.INT_DELAY_ID = Request.QueryString["Did"] == null ? 0 : int.Parse(Request.QueryString["Did"].ToString());
            objEntity.vchRemark = Txt_Remark.Text.Trim();
            objEntity.intStatus = Convert.ToInt32(DrpDwn_Status.SelectedValue);
            objEntity.intTimeAllowed = Convert.ToInt32(Txt_Time_Allowed.Text);
            objEntity.vchECLetter = FileUpload(FU_EC_Letter, "~/incentives/Files/InctEcDelayDoc/", "ECLetter_");
            objEntity.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());

            /*------------------------------------------------------------*/

            string str_Retvalue = objLayer.DelayReason_Approval(objEntity);

            string qrystring = "?linkm=" + Request.QueryString["linkm"].ToString() + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"];

            if (str_Retvalue == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> alertredirect('" + qrystring + "'); </script>", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Try after sometime','SWP'); </script>", false);
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

    #region Methods

    //// File Save Option    
    private string FileUpload(FileUpload fupUpload, string FilePath, string FileName)
    {
        string retval = "";
        try
        {
            if (FU_EC_Letter.HasFile)
            {
                var directory = new DirectoryInfo(FilePath);
                if (System.IO.Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(FilePath)) == false)
                {
                    System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(FilePath));
                }

                string file = System.Web.HttpContext.Current.Server.MapPath(FilePath) + FileName + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(fupUpload.FileName);
                retval = FileName + DateTime.Now.ToString("ddMMyyhhmmss") + Path.GetExtension(fupUpload.FileName);
                string files = System.Web.HttpContext.Current.Server.MapPath(FilePath) + file;

                if (File.Exists(file))
                {
                    File.Delete(file);
                }

                fupUpload.SaveAs(file);
                //retval = true;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "alert", ex.Message.ToString(), true);
        }
        return retval;
    }

    //// Field Bind   
    private void DataBind()
    {
        IncentiveMasterBusinessLayer objBAL = new IncentiveMasterBusinessLayer();
        Inct_EC_Delay_Reason_Entity objEntity = new Inct_EC_Delay_Reason_Entity();
        DataSet ds = new DataSet();
        try
        {
            objEntity.strAction = "B";
            objEntity.INT_DELAY_ID = Request.QueryString["Did"] == null ? 0 : int.Parse(Request.QueryString["Did"].ToString());

            ds = objBAL.Inct_EC_Delay_Reason_VIEW(objEntity);

            Lbl_Enterprise_Name.Text = ds.Tables[0].Rows[0]["vchEnterpriseName"].ToString();
            Lbl_Unit_Cat.Text = ds.Tables[0].Rows[0]["vchUnitCat"].ToString();
            Lbl_Unit_Type.Text = ds.Tables[0].Rows[0]["vchUnitType"].ToString();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
        finally
        {
            objBAL = null;
            objEntity = null;
            ds = null;
        }
    }
    #endregion
}