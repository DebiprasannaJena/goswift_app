using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.IO;
using System.Collections.Specialized;
using EntityLayer.GrievanceEntity;

public partial class Portal_Grievance_AddGrievanceType : System.Web.UI.Page
{
    #region for global veriable

    static string val = string.Empty;
    public string Result = string.Empty;

    #endregion
    

    #region for page load

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            if (Request.QueryString["intGrivTypeId"] != null)
            {
                val = Request.QueryString["intGrivTypeId"].ToString();


                try
                {
                    fillEditData(val);
                }
                catch (Exception ex)
                {
                    Util.LogError(ex, "Grivience");
                }
            }
        }
    }

    #endregion

    

    #region   data bind in controll for Edit
    private void fillEditData(string intGrivTypeId)
    {
        try
        {
            DataTable dt = new DataTable();
            GrievanceServices objBAL = new GrievanceServices();
            GrievanceEntity objGrivEntity = new GrievanceEntity();
            objGrivEntity.StrAction = "VGTS";
            objGrivEntity.intGrivTypeId = Convert.ToInt32(intGrivTypeId);
            dt = objBAL.ViewGrivTypeDetails(objGrivEntity);
            if (dt.Rows.Count > 0)
            {
                Txt_Griv_Name.Text = dt.Rows[0]["vchGrivType"].ToString();
                Rbl_griv_status.SelectedValue = dt.Rows[0]["intActiveStatus"].ToString();
                Btn_Submit.Text = "Update";
                Btn_Reset.Text = "Cancel";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }



    }

    #endregion
  

    #region for clear fields
    private void clearFields()
    {
        Txt_Griv_Name.Text = "";
        Btn_Submit.Text = "Submit";
        Btn_Reset.Text = "Reset";
        Hid_OG_Id.Value = "0";
    }

    #endregion

   

    #region for Submit and Update button

    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Txt_Griv_Name.Text.Trim() == "")
            {
                Txt_Griv_Name.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter Grievance Name.</strong>');", true);
                return;
            }
            else
            {
                GrievanceServices objBAL = new GrievanceServices();

                GrievanceEntity objEntity = new GrievanceEntity();

                if (Btn_Submit.Text.ToUpper() == "SUBMIT")
                {
                    /////Add Grievance Type
                    objEntity.StrAction = "AGT";
                    objEntity.strGrivType = Convert.ToString(Txt_Griv_Name.Text.Trim());
                    objEntity.intGrivActiveStatus = Convert.ToInt32(Rbl_griv_status.SelectedItem.Value);                   
                    objEntity.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());                   
                    Result = objBAL.AddUpdateGrievanceType(objEntity);
                }
                else if (Btn_Submit.Text.ToUpper() == "UPDATE")
                {
                    ////Update Grievance Type
                    objEntity.StrAction = "EDGT";
                    objEntity.strGrivType = Convert.ToString(Txt_Griv_Name.Text.Trim());
                    objEntity.intGrivActiveStatus = Convert.ToInt32( Rbl_griv_status.SelectedItem.Value);

                    objEntity.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());

                    objEntity.intGrivTypeId = Convert.ToInt32(val);

                    Result = objBAL.AddUpdateGrievanceType(objEntity);
                }

                if (Result == "2")
                {
                   
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Data Saved Successfully .</strong>');", true);
                    Txt_Griv_Name.Text = "";
                    return;
                }
                else if (Result == "1")
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Grievance type already exist.</strong>');", true);
                    return;
                }
                else if (Result == "3")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something Went wrong,Please try again.</strong>');", true);
                    return;
                }
                else if (Result == "4")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Updated Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = '../Grievance/ViewGrievanceType.aspx';}); </script>", false);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grivience");
        }
    }

    #endregion
    

    #region for reset and cancel

    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        if (Btn_Reset.Text == "Reset")
        {
            clearFields();
        }
        else if (Btn_Reset.Text == "Cancel")
        {
            Response.Redirect("ViewGrievanceType.aspx");
        }
    }
    #endregion
}