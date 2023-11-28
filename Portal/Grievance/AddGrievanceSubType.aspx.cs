using EntityLayer.GrievanceEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Portal_Grievance_AddGrievanceSubType : System.Web.UI.Page
{
    DataTable Dt = new DataTable();
    GrievanceServices obj = new GrievanceServices();
    public string Result = string.Empty;
    static string Val = string.Empty;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/LogOut.aspx", true);
        }
        if (!IsPostBack)
        {
            BindGrievanceType();
            if (Request.QueryString["intGrivSubTypeId"] != null)
            {
                Val = Request.QueryString["intGrivSubTypeId"].ToString();


                FillEditData(Val);

            }
        }
        else
        {
            //ListItem list = new ListItem();
            //list.Text = "--Select--";
            //list.Value = "0";
            //ddl_griv_type.Items.Insert(0, list);
        }

    }

    protected void Btn_Reset_Click(object sender, EventArgs e)
    {
        if (Btn_Reset.Text == "Reset")
        {
            clearFields();
        }
        else if (Btn_Reset.Text == "Cancel")
        {
            Response.Redirect("ViewGrievanceSubType.aspx");
        }
    }


    private void BindGrievanceType()
    {
        try
        {
            GrievanceEntity objSearch = new GrievanceEntity()
            {
                StrAction = "BGTF"
            };
            Dt = obj.ViewGrivSubTypeDetails(objSearch);
            ddl_griv_type.DataSource = Dt;
            ddl_griv_type.DataTextField = "vchGrivType";
            ddl_griv_type.DataValueField = "intGrivTypeId";
            ddl_griv_type.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddl_griv_type.Items.Insert(0, list);
        }
        catch (Exception X)
        {
            throw X;
        }


    }

    private void clearFields()
    {
        ddl_griv_type.SelectedValue = "0";
        Txt_Griv_subtypeName.Text = "";
        Btn_Submit.Text = "Submit";
        Btn_Reset.Text = "Reset";

    }


    private void FillEditData(string intGrivSubTypeId)
    {
        try
        {

            DataTable dt = new DataTable();
            GrievanceServices objBAL = new GrievanceServices();
            GrievanceEntity objGrivEntity = new GrievanceEntity();
            objGrivEntity.StrAction = "VGSTF";
            objGrivEntity.intGrivSubTypeId = Convert.ToInt32(intGrivSubTypeId);
            dt = objBAL.ViewGrivSubTypeDetails(objGrivEntity);
            if (dt.Rows.Count > 0)
            {
                ddl_griv_type.SelectedValue = dt.Rows[0]["intGrivTypeId"].ToString();
                Txt_Griv_subtypeName.Text = dt.Rows[0]["vchGrivSubType"].ToString();
                Rbl_griv_status.SelectedValue = dt.Rows[0]["intActiveStatus"].ToString();
                ddl_griv_type.Enabled = false;
                Btn_Submit.Text = "Update";
                Btn_Reset.Text = "Cancel";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }



    }



    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddl_griv_type.SelectedIndex <= 0)
            {
                ddl_griv_type.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please Select Grievance Type.</strong>');", true);
                return;
            }
            else if (Txt_Griv_subtypeName.Text.Trim() == "")
            {
                Txt_Griv_subtypeName.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter Grievance Subtype Name.</strong>');", true);
                return;
            }

            
                GrievanceEntity objseav = new GrievanceEntity();
                if (Btn_Submit.Text.ToUpper() == "SUBMIT")
                {
                    objseav.StrAction = "AGST";
                    objseav.intGrivTypeId = Convert.ToInt32(ddl_griv_type.SelectedItem.Value);
                    objseav.strGrivSubType = Convert.ToString(Txt_Griv_subtypeName.Text.Trim());
                    objseav.intGrivActiveStatus = Convert.ToInt32(Rbl_griv_status.SelectedItem.Value);

                    objseav.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());


                    Result = obj.AddUpdateGrievanceSubType(objseav);
                }
                else if (Btn_Submit.Text.ToUpper() == "UPDATE")
                {

                    objseav.StrAction = "EDGST";
                    objseav.strGrivSubType = Convert.ToString(Txt_Griv_subtypeName.Text.Trim());
                    objseav.intGrivActiveStatus = Convert.ToInt32(Rbl_griv_status.SelectedItem.Value);
                    objseav.intGrivTypeId = Convert.ToInt32(ddl_griv_type.SelectedItem.Value);
                    objseav.intCreatedBy = Convert.ToInt32(Session["UserId"].ToString());

                    objseav.intGrivSubTypeId = Convert.ToInt32(Val);

                    Result = obj.AddUpdateGrievanceSubType(objseav);
                }


                if (Result == "4")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Updated Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = '../Grievance/ViewGrievanceSubType.aspx';}); </script>", false);
                    return;
                }
                else if (Result == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('<strong>Data Saved Successfully !', '" + Messages.TitleOfProject + "'); </script>", false);

                    FillGrivSubType();
                    // BindGrievanceType();
                    Txt_Griv_subtypeName.Text = "";
                    //return;
                }
                else if (Result == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Grievance type already exist.</strong>');", true);

                    //if (Request.QueryString["intGrivSubTypeId"] != null)
                    //{
                    //    Val = Request.QueryString["intGrivSubTypeId"].ToString();
                    //    FillEditData(Val);
                    //}
                    //else
                    //{
                    //    BindGrievanceType();
                    //    Txt_Griv_subtypeName.Text = "";
                    //}
                    return;
                }
                else if (Result == "3")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Something Went wrong,Please try again.</strong>');", true);
                    clearFields();
                    ////BindGrievanceType();     
                    return;
                }          
           
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Grivience");
        }
    }  

    protected void ddl_griv_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGrivSubType();        
    }

    private void FillGrivSubType()
    {
        try
        {
            DataTable dt = new DataTable();
            GrievanceServices objBAL = new GrievanceServices();
            GrievanceEntity objGrivEntity = new GrievanceEntity();

            objGrivEntity.StrAction = "VGST";
            objGrivEntity.intGrivTypeId = Convert.ToInt32(ddl_griv_type.SelectedItem.Value);

            dt = objBAL.ViewGrivSubTypeDetails(objGrivEntity);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        catch (Exception )
        {
            throw;
        }
    }






    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            HiddenField subtype_status = (HiddenField)e.Row.FindControl("Hid_Griev_SubType_Status");
            Label grid_status = (Label)e.Row.FindControl("Lbl_Griv_stat");
            if (subtype_status.Value.ToUpper() == "ACTIVE")
            {
                grid_status.ForeColor = System.Drawing.Color.Green;
            }
            else if (subtype_status.Value.ToUpper() == "INACTIVE")
            {
                grid_status.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}