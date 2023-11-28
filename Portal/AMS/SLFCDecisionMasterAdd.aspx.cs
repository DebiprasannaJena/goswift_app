#region  PAGE INFO
//******************************************************************************************************************
// File Name             :  SingleWindow/SLFCDecisionMasterAdd.aspx
// Description           :  Add Term and Condition
// Created by            :  Monalisa nayak 
// Created On            :  06-02-2017
// Modification History  :  <CR no.>               <Date>                <Modified by>         <Modification Summary>'                                                          
//                           
// FUNCTION NAME         :  TextCheck()
// PROCEDURE USE         :  USP_AMS_SLFC_DECISION
//******************************************************************************************************************
#endregion
using System;
using System.Data;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Net;

public partial class SingleWindow_SLFCDecisionMasterAdd : System.Web.UI.Page
{   
    #region "Member Variable"
    AMS objams = new AMS();
    DataTable dt = null;
    String strval = null;
    #endregion

    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }

        if (!IsPostBack)
        {
            FillSector();
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                EditSLFCDiscussion(Convert.ToInt32(Request.QueryString["ID"]));
            }
            
        }       
        TextCheck(txtBrief, lblBrief);   
    }

    #endregion

    public void EditSLFCDiscussion(int ID)
    {
        DataTable dt = new DataTable();
        objams = new AMS();
        objams.Action = "ES";
        objams.ID = ID;
        dt = AMServices.EditSLFCDiscussion(objams);
        txtBrief.Text = dt.Rows[0]["vchChecklistPoint"].ToString();
        btnSubmit.Text = "Update";
        btnReset.Text = "Cancel";
    }

    #region "Fill Sector"

    private void FillSector()
    {
        try
        {
            dt = new DataTable();
            dt = AMServices.FillSector();
            ddlSector.DataSource = dt;
            ddlSector.DataTextField = "VchSectorName";
            ddlSector.DataValueField = "intSectorId";
            ddlSector.DataBind();
            ddlSector.Items.Insert(0, new ListItem("--Select--", "0"));
            ListItem removeItem = ddlSector.Items.FindByValue("21");
            ddlSector.Items.Remove(removeItem);
            
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { dt = null; }
    }
    #endregion
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Save")
        {
            if (txtBrief.Text != "")
            {
                objams.Action = "CA";
                objams.DECISION = txtBrief.Text;
                if (rbtAll.Checked == true)
                    objams.TypeId = 0;
                else
                    objams.TypeId = Convert.ToInt32(ddlSector.SelectedValue);

                strval = AMServices.AddSLFCComments(objams);
                Response.Write("<script>alert('Terms & Condition Added Successfully.');document.location.href='ViewSLFCDecisionMaster.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&pageIndex=" + Request.QueryString["PageNo"] + "'</script>");
            }
            else
                Response.Write("<script>alert('Please Add Terms & Conditions');</script>");
        }
        if (btnSubmit.Text == "Update")
        {
            objams.Action = "UDC";//Update Discussion Comments
            objams.ID = Convert.ToInt32(Request.QueryString["ID"]);
            objams.DECISION = txtBrief.Text;
            strval = AMServices.UpdateComments(objams);
            if (strval == "11")
            {
                Response.Write("<script>alert('Terms & Conditions Updated Successfully.');document.location.href='ViewSLFCDecisionMaster.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&pageIndex=" + Request.QueryString["PageNo"] + "'</script>");
            }

        }


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        string URL = "SLFCDecisionMasterAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "";
        Response.Redirect(URL);
    }
    private void TextCheck(TextBox txt, Label lbl)
    {
        try
        {
            int count = Convert.ToInt32(txt.Text.Length);
            double diff = 500 - Convert.ToInt32(count);
            lbl.Text = Convert.ToString(diff);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { }
    }
}