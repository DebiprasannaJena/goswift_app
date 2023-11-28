//******************************************************************************************************************
// File Name             :   SingleWindow/ProposalMasterAdd.aspx
// Description           :   To Add Proposal details against a project by Nodal Officer
// Created by            :   
// Created on            :   
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//                                   20-July-2016        Tapan Kumar Mishra             Add Project Dropdown and manage accordingly
//                                   21-July-2016        Tapan Kumar Mishra             Remove Viewstate concept and List
//********************************************************************************************************************

using System;
using System.Data;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Net;


public partial class SingleWindow_ProposalMasterAdd : System.Web.UI.Page
{
    #region "Member Variable"
    static int rowIndex = -1;
    AMS objams = null;
    Agenda objcs = null;
    string strVal = "";
    DataTable dt = null;
    DataTable dt1 = null;
    List<Proposal> objItemDetails;
    Proposal objItem;
    int intType = 0;
    int Status = 0;
    
    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            Response.Redirect("ProjectMasterAdd.aspx");
        }

        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {            
            intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
            if (!IsPostBack)
            {
               
                FEtxtBrief.ValidChars = FEtxtBrief.ValidChars + "\r\n";
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    if (System.Web.HttpContext.Current.Session["PType"] == null)
                    {
                        FillProject();
                        FillDetails("E");
                    }
                    else if (Session["PType"].ToString() == "2") 
                    {
                        FillDetails("VP");
                    }
                }
                else
                {
                    Session["PType"] = null;
                    this.btnReset.Text = "Reset";
                    this.btnSubmit.Text = "Next";
                }
                if (intType == 3 || intType == 4)
                {
                    if (Status == 3 || Status == 4 || Status == 0)
                    {
                        trRemark.Visible = false;
                        trRemarkEnt.Visible = false;
                    }
                    else
                    {
                        trRemark.Visible = true;
                        trRemarkEnt.Visible = true;
                    }
                }
                else
                {
                    trRemark.Visible = false;
                    trRemarkEnt.Visible = false;
                }
             }
           
        }
        FERemark.ValidChars = FERemark.ValidChars + "\r\n";
    }
    #endregion
    #region "Fill Project"
    private void FillProject()
    {
        try
        {
            objcs = new Agenda();
            objcs.Action = "F";
            objcs.OfficerType = intType;
            objcs.UserId = Convert.ToInt32(Session["UserId"]);
            dt = new DataTable();
            dt = AMServices.FillActiveProject(objcs);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objcs = null; dt = null; }
    }

  
    #endregion

    #region "User function"
    private void FillDetails(string ACTION)
    {
        objams = new AMS();
        try
        {
            objams.Action = ACTION;
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            dt = new DataTable();
            dt1 = new DataTable();
            if (System.Web.HttpContext.Current.Session["PType"] == null)
            {
                dt = AMServices.ViewProposalMaster(objams);
                if (dt.Rows.Count > 0)
                {
                    Status = Convert.ToInt32(dt.Rows[0]["INTSTATUS"].ToString());
                }
                //Fetch Remark from M_AMS_CMD_REMARK Table against Project ID if Login Type=3 Sanjeev 
                if (intType == 3 || intType == 4)
                {
                    objams.Action = "FR";
                    dt1 = AMServices.ViewProposalMaster(objams);
                   
                }
            }
            else if (Session["PType"].ToString() == "2")
            {
                dt = AMServices.ViewSWPProposalMaster(objams);
            }

            if (intType == 3 || intType == 4)
            {
                if (dt1.Rows.Count > 0)
                {
                    DataView dv1 = new DataView(dt1);
                    dv1.RowFilter = "intCreatedBy=3";  //CMD

                    DataView dv2 = new DataView(dt1);
                    dv2.RowFilter = "intCreatedBy=4"; // GM

                    RptCMDRemark.DataSource = dv1;
                    RptCMDRemark.DataBind();
                    if (dv2.Count > 0)
                    {
                        RptGMRemark.DataSource = dv2;
                        RptGMRemark.DataBind();
                    }

                    if (dv1.Count > 0)
                        RptCMDRemark.Visible = true;
                    else
                        RptCMDRemark.Visible = false;

                    if (dv2.Count > 0)
                        RptGMRemark.Visible = true;
                    else
                        RptGMRemark.Visible = false;


                    if (intType == 3) //CMD
                    {
                        if (dv1.Count > 0)
                        {
                            txtRemark.Text = dv1[dv1.Count - 1][2].ToString();
                            hdnRemarkID.Value = dv1[dv1.Count - 1][4].ToString();
                        }
                    }
                    else if (intType == 4) //GM
                    {
                        if (dv2.Count > 0)
                        {
                            txtRemark.Text = dv2[dv2.Count-1][2].ToString();
                            hdnRemarkID.Value = dv2[dv2.Count-1][4].ToString();
                        }
                    }
                }
            }   
            if (dt.Rows.Count > 0)
            {
                txtBrief.Text = WebUtility.HtmlDecode(dt.Rows[0].ItemArray[2].ToString());
                btnSubmit.CommandArgument = dt.Rows[0]["PTYPE"].ToString();   // if value is 1 then Update if 2 then insert

                if (System.Web.HttpContext.Current.Session["PType"] == null)
                {
                    if (intType == 3)
                    {
                        btnSubmit.Text = "Next";
                        btnReset.Text = "Reset";
                    }
                    else
                    {
                        btnSubmit.Text = "Update";
                        btnReset.Text = "Cancel";
                    }

                }
                else if (Session["PType"].ToString() == "2")
                {
                    btnSubmit.Text = "Next";
                    btnReset.Text = "Reset";
                }
            }
            else
            {
                this.btnReset.Text = "Reset";
                this.btnSubmit.Text = "Next";
                txtBrief.Text = string.Empty;
                btnSubmit.CommandArgument = "2";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objams = null; }
    }


    #endregion

    #region "Button Event"
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objams = new AMS();
         
            if (btnSubmit.CommandArgument == "1")
                objams.Action = "U";
            else if (btnSubmit.CommandArgument == "2")
                objams.Action = "A";
            if (txtRemark.Visible == true)
            {
                objams.Remark = txtRemark.Text.Trim() == "" ? "No Comments" : txtRemark.Text.Trim();
                objams.intRemarkID = hdnRemarkID.Value == "" ? 0 : Convert.ToInt32(hdnRemarkID.Value);
                intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
                objams.CreatedBy = intType;
            }
            else
            {
                objams.CreatedBy = Convert.ToInt16(Session["UserId"]);
            }
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            objams.ProposalDetails = WebUtility.HtmlDecode(txtBrief.Text);
            strVal = AMServices.AddProposalMaster(objams);
            string msg = Messages.ShowMessage(strVal).ToString();
            if (strVal == "2")
                ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Updated Successfully.');location.href='ProjectDetailsAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);             
            else
                ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Saved Successfully.');location.href='ProjectDetailsAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);             
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('" + ex.Message + "');", true);
        }
        finally { objams = null; }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProposalMasterAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "");
    }

    #endregion
  
public class Proposal
{
    public int SlNo { get; set; }
    public int ProposalId { get; set; }
    public string ProposalDtl { get; set; }
}
}
