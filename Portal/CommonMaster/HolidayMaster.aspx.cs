//******************************************************************************************************************
// File Name         : HolidayMaster.aspx
// Description       : This page is to create holiday 
// Created by        : Dharmasis sahoo
// Created On        : 06th Dec 2021
// Modification History:
//                                                          
//<CR no.>                          <Date>                <Modified by>        <Modification Summary>                      <Instructed By>
//
//********************************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


public partial class SWP_HolidayMaster_HolidayMaster : System.Web.UI.Page
{
    readonly SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        Txt_Holiday_From.Attributes.Add("readonly", "readonly");
        Txt_Holiday_To.Attributes.Add("readonly", "readonly");
    }

    public void ClearField()
    {
        Txt_Holiday_Description.Text = "";
        Txt_Holiday_From.Text = "";
        Txt_Holiday_Title.Text = "";
        Txt_Holiday_To.Text = "";
    } 


   /// <summary>
   /// This function is used to create new holiday
   /// </summary>
   /// <param name="sender"></param>
   /// <param name="e"></param>
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            #region Validation
            
            if (Txt_Holiday_Title.Text.Trim() == "")
            {
                Txt_Holiday_Title.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter holiday title.</strong>');", true);
                return;
            }
            if (Txt_Holiday_From.Text.Trim() == "")
            {
                Txt_Holiday_From.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter holiday from date.</strong>');", true);
                return;
            }
            if (Txt_Holiday_To.Text.Trim() == "")
            {
                Txt_Holiday_To.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter holiday to date .</strong>');", true);
                return;
            }
            if (Convert.ToDateTime(Txt_Holiday_From.Text.Trim()) > Convert.ToDateTime(Txt_Holiday_To.Text.Trim()))
            {
                Txt_Holiday_From.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong> Holiday From date should not be greater than holiday To date.</strong>');", true);
                return;
            }           
            if (Txt_Holiday_Description.Text.Trim() == "")
            {
                Txt_Holiday_Description.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter holiday description.</strong>');", true);
                return;
            }

            #endregion

            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_HOLIDAY_MASTER_AED";

            cmd.Parameters.AddWithValue("@P_VCH_HOLIDAY_TITLE", Txt_Holiday_Title.Text.Trim());
            cmd.Parameters.AddWithValue("@P_VCH_HOLIDAY_FROM", Txt_Holiday_From.Text.Trim());
            cmd.Parameters.AddWithValue("@P_VCH_HOLIDAY_TO", Txt_Holiday_To.Text.Trim());
            cmd.Parameters.AddWithValue("@P_VCH_DESCRIPTION", Txt_Holiday_Description.Text.Trim());
            cmd.Parameters.AddWithValue("@P_VCH_ACTION","I");

            SqlParameter par = new SqlParameter("@P_VCH_MSG_OUT", SqlDbType.VarChar, 104);
            par.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(par);

            cmd.ExecuteNonQuery();

            string strRetrunStatus = cmd.Parameters["@P_VCH_MSG_OUT"].Value.ToString();
            if (strRetrunStatus == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Holiday already exists in selected date.</strong>');", true);               
                return;
            }
            else if (strRetrunStatus == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Holiday added successfully.</strong>');", true);
                ClearField();
                return;
            }
            else if (strRetrunStatus == "3")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('<strong>Something went wrong.Please try again.</strong>');", true);               
                return;                
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Holiday");
        }
    }

    protected void BtnReset_Click(object sender, EventArgs e)
    {
        ClearField();
    }
}