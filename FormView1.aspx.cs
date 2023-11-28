//******************************************************************************************************************
// File Name             :   FormView.aspx.cs
// Description           :   Dynamic Service data save to database
// Created by            :   Radhika Rani Patri
// Created on            :   25th July 2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Services;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ClientsideEncryption;
using EntityLayer.Service;
using System.Net;
using System.Text;

public partial class FormView : System.Web.UI.Page
{
    #region "Global Variable"
    /// <summary>
    /// Radhika Rani Patri
    /// All global variable declared here
    /// </summary>
    /// 
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    string ApplicationPath = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"];
    CommonFunctions objCrypt = new CommonFunctions();
    string FormHeader = "";
    string FormFooter = "";
    int intAllignment = 0;
    string strUnqId = "";
    string outStatus = "";
    public string jsfile = "";
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    #endregion

    #region "Page Load Event"
    /// <summary>
    /// Radhika Patri
    /// Load dynamic form according to the  service id passed through query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        //myNavbar.Visible = false;

        if (!IsPostBack)
        {
            if (Request.QueryString["ReqMode"] != "" && Request.QueryString["ReqMode"] != null)
            {
                if (Request.QueryString["ReqMode"].ToString() == "M")
                {
                    btnDraft.Text = "Save And Next";
                }
                else
                {
                    btnDraft.Text = "Save As Draft";
                }
            }

            if (Request.QueryString["FormId"] != "" && Request.QueryString["FormId"] != null)
            {
                hdnProposalNo.Value = (Request.QueryString["ProposalNo"].ToString());
                hdnFormId.Value = (Request.QueryString["FormId"].ToString());
                FillDataTable(Convert.ToInt32(Request.QueryString["FormId"].ToString()));
                ServiceAmount(Convert.ToInt32(Request.QueryString["FormId"]));
                hdnInvestor.Value = Session["InvestorId"].ToString();

                if (Request.QueryString["ReqMode"].ToString() == "M")
                {
                    CreateNavigationMenu();
                }
            }
            else
            {
                FillDataTable(1);
                if (Request.QueryString["ReqMode"].ToString() == "M")
                {
                    CreateNavigationMenu();
                }
            }
        }
    }
    #endregion

    #region "Dynamic form Build"
    /// <summary>
    /// Radhika Rani Patri
    /// Here all control generated for dynamic form
    /// </summary>
    /// <param name="FormId"></param>
    /// 
    public void FillDataTable(int FromId)
    {
        string strHtmls = "";

        FormDetails(FromId);

        DataTable PnlDt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "select * from T_PANELMAPPING_TBL WHERE INT_FORM_ID=" + FromId + "and INT_DELETEDFLAG=0";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                PnlDt.Load(cmd.ExecuteReader());
                con.Close();
            }
        }

        for (int i1 = 0; i1 < PnlDt.Rows.Count; i1++)
        {
            strHtmls = strHtmls + "<div class='sectionPanel' id='pnl_" + PnlDt.Rows[i1]["INT_PANELID"].ToString() + "' ><h2 id='h2_" + PnlDt.Rows[i1]["INT_PANELID"].ToString() + "'>" + PnlDt.Rows[i1]["VCH_PANETEXT"].ToString() + "<small class='text-red' style='display:none;font-size:85%;margin-left:4px;'>*</small></h2><div class='row'>" + GetHTML(FromId, Convert.ToInt32(PnlDt.Rows[i1]["INT_PANELID"])) + "</div></div>";
        }


        hdnPluginJson.Value = hdnPluginJson.Value.TrimEnd(',');
        StringWriter myWriter = new StringWriter();
        HttpUtility.HtmlDecode(FormHeader, myWriter);
        string strTex = myWriter.ToString();
        divHeaderId.InnerHtml = strTex;
        StringWriter myWriter1 = new StringWriter();
        HttpUtility.HtmlDecode(FormFooter, myWriter1);
        string strTex1 = myWriter1.ToString();
        //divFooterId.InnerHtml = strTex1;
        frmContent.InnerHtml = strHtmls;
    }
    public string GetHTML(int FormId, int PanelID)
    {
        DataTable dt = new DataTable();
        SqlConnection connection = new SqlConnection(connectionString);
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("USP_DYNAMICFORM_VIEW"))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@P_FORMID", SqlDbType.Int, 10).Value = Convert.ToInt32(FormId);
                cmd.Parameters.Add("@P_PANELID", SqlDbType.Int, 10).Value = Convert.ToInt32(PanelID);
                cmd.Connection = con;
                con.Open();
                dt.Load(cmd.ExecuteReader());
                con.Close();
            }
        }

        string strProposalId = "";
        if (Request.QueryString["ProposalNo"].ToString() != "" || Request.QueryString["ProposalNo"].ToString() != null)
        {
            strProposalId = Request.QueryString["ProposalNo"].ToString();//Request.QueryString["ProposalNo"].ToString();
        }
        else
        {
            strProposalId = "";
        }

        string strHtml = "";
        string strrequired = "";
        string strReq = "1";
        string strTitle = "";
        var qut = '"';
        string lebeltext = "";
        string controlText = "";
        string strReqT = "";
        string strReqC = "";
        string strReqR = "";
        string strReqD = "";
        string strReqE = "";
        //LayOut
        string strJsonStr = "";
        //Pluginjson
        string strRow = "";
        //string strGroupDiv = "<div class='form-group'><div class='row'>";//";
        string strGroupDiv = "";//";
        //string divDtl = "class='form-group'";

        int counter = 0;
        for (int k = 0; k < dt.Rows.Count; k++)
        {
            strRow = "";

            string strClass = "";

            if (dt.Rows[k]["PVCH_CSSCLASS"] != DBNull.Value)
            {
                if (dt.Rows[k]["PVCH_CSSCLASS"].ToString() != "")
                {
                    strClass = dt.Rows[k]["PVCH_CSSCLASS"].ToString();
                }
                else
                {
                    strClass = "form-control";
                }
            }
            else
            {
                strClass = "form-control inpt";
            }

            string strOption = "<option value='0' formCntl='yes' >Select</option>";
            if (dt.Rows[k]["PINT_REQVALIDATION"].ToString() == "1")
            {
                strrequired = "required";
                strReqT = " req rset";
                strReqC = " reqC rset";
                strReqR = " reqR rset";
                strReqD = " reqD rset";
                strReqE = " email emailR rset";
                strReq = "1";
            }
            else
            {
                strReqT = "";
                strReqC = "";
                strReqR = "";
                strReqD = "";
                strReqE = " email rset nmcls";
                strReq = "0";
            }


            if (dt.Rows[k]["PVCH_TOOLTIP"].ToString() != null && dt.Rows[k]["PVCH_TOOLTIP"].ToString() != "")
            {
                strTitle = dt.Rows[k]["PVCH_TOOLTIP"].ToString();
            }

            if (dt.Rows[k]["PVCH_LABEL_NAME"].ToString() != "" && dt.Rows[k]["PVCH_LABEL_NAME"].ToString() != null)
            {
                //  strHtml = strHtml + "<label for='" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "'>" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "</label>";
                lebeltext = dt.Rows[k]["PVCH_LABEL_NAME"].ToString();
            }
            else
            {
                controlText = "";
            }

            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "select" || dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "select-multiple")
            {
                if (dt.Rows[k]["PVCH_DATASOURCE"].ToString() != null && dt.Rows[k]["PVCH_DATASOURCE"].ToString() != "")
                {
                    List<ListItem> list = GetDataList(dt.Rows[k]["PVCH_DATASOURCE"].ToString(), dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString(), dt.Rows[k]["PVCH_DATATEXTFIELD"].ToString());
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (dt.Rows[k]["PVCH_DEFAULTVALUE"].ToString() == list[i].Text)
                        {
                            strOption = strOption + "<option value='" + list[i].Value.TrimEnd().TrimStart() + "' selected > " + list[i].Text.TrimEnd().TrimStart() + "</option>";
                        }
                        else
                        {
                            strOption = strOption + "<option value='" + list[i].Value.TrimEnd().TrimStart() + "'  > " + list[i].Text.TrimEnd().TrimStart() + "</option>";
                        }
                    }
                }
                else
                {
                    if (dt.Rows[k]["PVCH_OPTION"].ToString() != null)
                    {
                        string[] drdarray = dt.Rows[k]["PVCH_OPTION"].ToString().Split(',');

                        for (int i = 0; i < drdarray.Length; i++)
                        {
                            if (dt.Rows[k]["PVCH_DEFAULTVALUE"].ToString() == drdarray[i])
                            {
                                strOption = strOption + "<option value='" + drdarray[i].TrimEnd().TrimStart() + "' selected  > " + drdarray[i].TrimEnd().TrimStart() + "</option>";
                            }
                            else
                            {
                                strOption = strOption + "<option value='" + drdarray[i].TrimEnd().TrimStart() + "'  > " + drdarray[i].TrimEnd().TrimStart() + "</option>";
                            }
                        }
                    }
                }
                if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "select-multiple")
                {
                    controlText = "  <select  id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  multiple dtSource='" + dt.Rows[k]["PVCH_DATASOURCE"].ToString() + "' dtValue='" + dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString() + "' dtText='" + dt.Rows[k]["PVCH_DATATEXTFIELD"].ToString() + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes'  name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' class='" + strClass + strReqD + "' > " + strOption + " </select>";
                }
                else
                {
                    controlText = "  <select  id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' dtSource='" + dt.Rows[k]["PVCH_DATASOURCE"].ToString() + "' dtValue='" + dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString() + "' dtText='" + dt.Rows[k]["PVCH_DATATEXTFIELD"].ToString() + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' class='" + strClass + strReqD + "' > " + strOption + " </select>";
                }
            }


            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "DateTime")
            {
                int[] arrYear = { 2017, 2018, 2019, 2020, 2021, 2022, 2023, 2024, 2025, 2026, 2027, 2028 };
                string[] arraymnth = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                string strYearOption = "";
                string strMnthOption = "";

                strYearOption = "<option value='0' selected > Year</option>";
                for (int i = 0; i < arrYear.Length; i++)
                {
                    strYearOption = strYearOption + "<option value='" + arrYear[i] + "'  > " + arrYear[i] + "</option>";
                }

                strMnthOption = "<option value='0' selected > Month</option>";
                for (int i = 0; i < arraymnth.Length; i++)
                {
                    strMnthOption = strMnthOption + "<option value='" + arraymnth[i] + "'  > " + arraymnth[i] + "</option>";
                }

                controlText = "<div class='col-sm-6 padding-left-0'> <select  id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "_m'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' class='" + strClass + strReqD + " drpDtm' > " + strMnthOption + " </select></div><div class='col-sm-6 padding-right-0'><select  id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "_y'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'  name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' class='" + strClass + strReqD + " drpDtm' > " + strYearOption + " </select></div><div class='row'></div>";
            }

            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "FromToDate")
            {
                controlText = "<div class='col-sm-12 padding-left0'><div class='row'><div class='col-sm-6 padding-left-0'><div class='input-group '><input type='text' placeholder='From'   id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "_frmdt'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='date " + strClass + strReqR + "'><span class='input-group-addon'><i class='fa fa-calendar'></i></span></div></div><div class='col-sm-6 padding-right-0'><div class='input-group '><input type='text' placeholder='To'   id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "_Todt'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='date " + strClass + strReqR + "'><span class='input-group-addon'><i class='fa fa-calendar'></i></span></div></div></div></div>";
            }

            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "FullName")
            {
                controlText = "<div class='col-sm-3 padding-left-0'> <select name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' id='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "_N' class='form-control flnm' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' ><option value='Mr'>Mr</option><option value='Ms'>Ms</option></select></div><div class='col-sm-3 padding-0'><input type='text' ID='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "_FN' class='form-control flnm " + strReqT + "'  placeholder='First' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'> </div><div class='col-sm-3 padding-right-0'><input type='text' ID='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "_MN' class='form-control flnm'  placeholder='Middle' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'> </div><div class='col-sm-3 padding-right-0'> <input type='text' ID='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "_LN' class='form-control flnm " + strReqT + "' runat='server' placeholder='Last' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'></div>";
            }

            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "LatLong") //// Added by Sushant Jena
            {
                controlText = "<div class='col-sm-4 padding-left-0'><input type='text' ID='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "_DEG' class='form-control lat" + strReqT + "' placeholder='Degrees' groupName='DEG' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' onkeypress='return FloatOnly(event, this)' title='Degrees' autocomplete='off'></div>"
                            + "<div class='col-sm-4 padding-left-0'><input type='text' ID='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "_MIN' class='form-control lat" + strReqT + "' placeholder='Minutes' groupName='MIN' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' onkeypress='return FloatOnly(event, this)' title='Minutes' autocomplete='off'></div>"
                            + "<div class='col-sm-4 padding-left-0'><input type='text' ID='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "_SEC' class='form-control lat" + strReqT + "' placeholder='Seconds' groupName='SEC' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' onkeypress='return FloatOnly(event, this)' title='Seconds' autocomplete='off'></div>";
            }

            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "text")
            {
                if (dt.Rows[k]["PVCH_TEXTMODE"].ToString() == "MultiLine")
                {
                    if (dt.Rows[k]["PINT_AUTOMAPPING"].ToString() == "1")
                    {
                        string autovalue = GloblMappingValue(dt.Rows[k]["PVCH_DATASOURCE"].ToString(), dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString(), strProposalId);
                        //if (autovalue != "")
                        //{
                        controlText = "<div class='col-sm-12 text-right sameasabove' id='divsmchk_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'><input class='' type='checkbox'  id='smchk_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'   value='" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'>Same as above</input></div><textarea  id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "' Onkeypress='return inputLimiter(event," + qut + "Address" + qut + ")'  value='" + autovalue + "' >" + GloblMappingValue(dt.Rows[k]["PVCH_DATASOURCE"].ToString(), dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString(), strProposalId) + "</textarea>";
                        //}
                        //else
                        //{
                        //    controlText = "<div class='col-sm-12 text-right sameasabove' id='divsmchk_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'><input class='' type='checkbox'  id='smchk_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'   value='" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'>Same as above</input></div><textarea  id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "' Onkeypress='return inputLimiter(event," + qut + "Address" + qut + ")' value='" + GloblMappingValue(dt.Rows[k]["PVCH_DATASOURCE"].ToString(), dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString(), strProposalId) + "' >" + GloblMappingValue(dt.Rows[k]["PVCH_DATASOURCE"].ToString(), dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString(), strProposalId) + "</textarea>";

                        //}
                    }
                    else
                    {
                        controlText = "<div class='col-sm-12 text-right sameasabove' id='divsmchk_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'><input class='' type='checkbox'  id='smchk_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  value='" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' >Same as above</input></div><textarea  id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'  formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "' Onkeypress='return inputLimiter(event," + qut + "Address" + qut + ")'></textarea>";
                    }
                }
                else if (dt.Rows[k]["PVCH_TEXTMODE"].ToString() == "SingleLine")
                {
                    if (dt.Rows[k]["PVCH_VALIDATIONTYPE"].ToString() == "PhoneNumber")
                    {
                        if (dt.Rows[k]["PINT_AUTOMAPPING"].ToString() == "1")
                        {
                            string autovalue = GloblMappingValue(dt.Rows[k]["PVCH_DATASOURCE"].ToString(), dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString(), strProposalId);
                            //if (autovalue != "")
                            //{
                            controlText = "<input type='text' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "' Onkeypress='return inputLimiter(event," + qut + "Numbers" + qut + ")' value='" + autovalue + "'  onchange='return MobileValidation(" + qut + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + qut + ");'>";

                            //}
                            //else
                            //{
                            //    controlText = "<input type='text' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "' Onkeypress='return inputLimiter(event," + qut + "Numbers" + qut + ")' onchange='return MobileValidation(" + qut + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + qut + ");'>";

                            //}
                        }
                        else
                        {
                            controlText = "<input type='text' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "' Onkeypress='return inputLimiter(event," + qut + "Numbers" + qut + ")' onchange='return MobileValidation(" + qut + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + qut + ");'>";
                        }
                    }
                    else if (dt.Rows[k]["PVCH_VALIDATIONTYPE"].ToString() == "Number")
                    {
                        controlText = "<input type='text'   id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'  formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "' Onkeypress='return inputLimiter(event," + qut + "Numbers" + qut + ")'>";
                    }

                    if (dt.Rows[k]["PVCH_VALIDATIONTYPE"].ToString() == "Decimal")
                    {
                        controlText = "<input type='text'  id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "'  Onkeypress='return isNumberKey(event," + qut + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + qut + ")'>";
                    }
                    else if (dt.Rows[k]["PVCH_VALIDATIONTYPE"].ToString() == "Character")
                    {
                        if (dt.Rows[k]["PINT_AUTOMAPPING"].ToString() == "1")
                        {
                            string autovalue = GloblMappingValue(dt.Rows[k]["PVCH_DATASOURCE"].ToString(), dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString(), strProposalId);
                            //if (autovalue != "")
                            //{
                            controlText = "<input type='text'   id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "' value='" + autovalue + "'  Onkeypress='return inputLimiter(event," + qut + "Address" + qut + ")'>";
                            //}
                            //else
                            //{
                            //    controlText = "<input type='text'   id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "' Onkeypress='return inputLimiter(event," + qut + "Address" + qut + ")'>";

                            //}
                        }
                        else
                        {
                            controlText = "<input type='text'   id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "' Onkeypress='return inputLimiter(event," + qut + "Address" + qut + ")'>";

                        }
                    }
                    else if (dt.Rows[k]["PVCH_VALIDATIONTYPE"].ToString() == "Email")
                    {
                        if (dt.Rows[k]["PINT_AUTOMAPPING"].ToString() == "1")
                        {
                            string autovalue = GloblMappingValue(dt.Rows[k]["PVCH_DATASOURCE"].ToString(), dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString(), strProposalId);
                            //if (autovalue != "")
                            //{
                            controlText = "<input type='text'   id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqE + "' value='" + autovalue + "'  Onkeypress='return inputLimiter(event," + qut + "Email" + qut + ")'>";
                            //}
                            //else
                            //{
                            //    controlText = "<input type='text'   id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqE + "' Onkeypress='return inputLimiter(event," + qut + "Email" + qut + ")'>";

                            //}
                        }
                        else
                        {
                            controlText = "<input type='text'   id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqE + "' Onkeypress='return inputLimiter(event," + qut + "Email" + qut + ")'>";
                        }
                    }
                    //else
                    //{
                    //    controlText = "<input type='text'   id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + k + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='" + strClass + strReqT + "' Onkeypress='return inputLimiter(event," + qut + "Email" + qut + ")'>";
                    //}
                }
                else if (dt.Rows[k]["PVCH_TEXTMODE"].ToString() == "Password")
                {
                }
                else if (dt.Rows[k]["PVCH_TEXTMODE"].ToString() == "DateTime")
                {
                    controlText = "<div class='input-group'><input type='text'   id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='date " + strClass + strReqT + "'><span id='sp_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' class='input-group-addon'><i class='fa fa-calendar'></i></span></div>";
                }
            }

            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "Heading")
            {
                controlText = dt.Rows[k]["PVCH_HEADINGTEXT"].ToString();
            }

            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "Plugin")
            {
                controlText = "<iframe src=" + dt.Rows[k]["PVCH_PLUGINID"].ToString() + " id='ifm'></iframe>";
                //controlText= "<Label  id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'  name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "'  class='" +strClass + "'>" + dt.Rows[k]["PVCH_HEADINGTEXT"].ToString() + "</Label>";
            }

            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "file")
            {
                if (dt.Rows[k]["PINT_AUTOMAPPING"].ToString() == "1")
                {
                    string autovalue = GloblMappingValue(dt.Rows[k]["PVCH_DATASOURCE"].ToString(), dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString(), strProposalId);
                    if (autovalue != "" && autovalue != null)
                    {
                        controlText = "<div class='input-group'><input type='file' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'  formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' value='" + autovalue + "' onchange=ValidFileExtentionAndSize(" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + ",'" + dt.Rows[k]["PVCH_FILEALLOWED"].ToString() + "'," + dt.Rows[k]["PINT_MAXSIZE"].ToString() + ",'MB',\"btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\") class='form-control'><a class='input-group-addon bg-green' href='javascript:void(0);' onclick='fileUploadEvent(\"btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\");' title='Click here to upload file'><i class='fa fa-upload' aria-hidden='true'></i></a><a id='btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' href='#' class='input-group-addon bg-blue' target='_blank' style='visibility:hidden' title='Click here to download file'><i class='fa fa-download'></i></a><a id='btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' class='input-group-addon bg-red' href='javascript:void(0);' style='visibility:hidden' onclick='fileDeleteEvent(\"" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\");' title='Click here to delete file'><i class='fa fa-trash-o' aria-hidden='true'></i></a></div><small><span class='text-red' id='spn_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' >Only " + dt.Rows[k]["PVCH_FILEALLOWED"].ToString() + " and max size " + dt.Rows[k]["PINT_MAXSIZE"].ToString() + " MB files allowed.</span></small><a href=" + autovalue + " target='_blank'><i class='fa fa-download'></i></a><input id='hdn" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' name='hdn" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' type='hidden' value='" + autovalue + "'><input id='hdn_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' name='hdn_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' type='hidden' >";
                    }
                    else
                    {
                        controlText = "<div class='input-group'><input type='file' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'  formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' onchange=ValidFileExtentionAndSize(" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + ",'" + dt.Rows[k]["PVCH_FILEALLOWED"].ToString() + "'," + dt.Rows[k]["PINT_MAXSIZE"].ToString() + ",'MB',\"btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\") class='" + strClass + strReqT + "'><a class='input-group-addon bg-green' href='javascript:void(0);' onclick='fileUploadEvent(\"btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\");' title='Click here to upload file'><i class='fa fa-upload' aria-hidden='true'></i></a><a id='btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' href='#' class='input-group-addon bg-blue' target='_blank' style='visibility:hidden' title='Click here to download file'><i class='fa fa-download'></i></a><a id='btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' class='input-group-addon bg-red' href='javascript:void(0);' style='visibility:hidden' onclick='fileDeleteEvent(\"" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\");' title='Click here to delete file'><i class='fa fa-trash-o' aria-hidden='true'></i></a></div><small><span class='text-red' id='spn_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' >Only " + dt.Rows[k]["PVCH_FILEALLOWED"].ToString() + " and max size " + dt.Rows[k]["PINT_MAXSIZE"].ToString() + " MB files allowed.</span></small><input id='hdn" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' name='hdn" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' type='hidden' value=''><input id='hdn_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' name='hdn_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' type='hidden' >";
                    }
                }
                else
                {
                    controlText = "<div class='input-group'><input type='file' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'  formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' onchange=ValidFileExtentionAndSize(" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + ",'" + dt.Rows[k]["PVCH_FILEALLOWED"].ToString() + "'," + dt.Rows[k]["PINT_MAXSIZE"].ToString() + ",'MB',\"btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\") class='" + strClass + strReqT + "'><a class='input-group-addon bg-green' href='javascript:void(0);' onclick='fileUploadEvent(\"btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\");' title='Click here to upload file'><i class='fa fa-upload' aria-hidden='true'></i></a><a id='btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' href='#' class='input-group-addon bg-blue' target='_blank' style='visibility:hidden' title='Click here to download file'><i class='fa fa-download'></i></a><a id='btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' class='input-group-addon bg-red' href='javascript:void(0);' style='visibility:hidden' onclick='fileDeleteEvent(\"" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndownload" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\",\"btndel" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "\");' title='Click here to delete file'><i class='fa fa-trash-o' aria-hidden='true'></i></a></div><small><span class='text-red' id='spn_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' >Only " + dt.Rows[k]["PVCH_FILEALLOWED"].ToString() + " and max size " + dt.Rows[k]["PINT_MAXSIZE"].ToString() + " MB files allowed.</span></small><input id='hdn" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' name='hdn" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' type='hidden' value=''><input id='hdn_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' name='hdn_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' type='hidden' >";
                }
            }
            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "radio")
            {
                if (dt.Rows[k]["PVCH_DATASOURCE"].ToString() != null && dt.Rows[k]["PVCH_DATASOURCE"].ToString() != "")
                {
                    List<ListItem> list = GetDataList(dt.Rows[k]["PVCH_DATASOURCE"].ToString(), dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString(), dt.Rows[k]["PVCH_DATATEXTFIELD"].ToString());
                    controlText = "<div class='" + strReqR + "' " + "' title='" + strTitle + "' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' id='div_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'>";
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (dt.Rows[k]["PVCH_DEFAULTVALUE"].ToString() == list[i].Text)
                        {
                            controlText = "<input type='radio' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + i + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'  formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' value='" + list[i].Value + "' checked='checked' class=''>" + list[i].Text + "</input>";
                        }
                        else
                        {
                            controlText = "<input type='radio' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + i + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'  name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' value='" + list[i].Value + "' class=''>" + list[i].Text + "</input>";
                        }
                    }
                    controlText = controlText + "</div>";
                }
                else
                {
                    if (dt.Rows[k]["PVCH_OPTION"].ToString() != null)
                    {
                        controlText = "<div class='" + strReqR + "' " + "' title='" + strTitle + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' id='div_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'>";
                        string[] drdarray = dt.Rows[k]["PVCH_OPTION"].ToString().Split(',');
                        for (int i = 0; i < drdarray.Length; i++)
                        {
                            if (dt.Rows[k]["PVCH_DEFAULTVALUE"].ToString() == drdarray[i])
                            {
                                controlText = controlText + "<input type='radio' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + i + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' value='" + drdarray[i] + "' checked='checked' class=''>" + drdarray[i] + "</input>";
                            }
                            else
                            {
                                controlText = controlText + "<input type='radio' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + i + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' value='" + drdarray[i] + "' class=''>" + drdarray[i] + "</input>";
                            }
                        }
                        controlText = controlText + "</div>";
                    }
                }
                //  controlText= "";
            }

            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "checkbox")
            {
                if (dt.Rows[k]["PVCH_DATASOURCE"].ToString() != null && dt.Rows[k]["PVCH_DATASOURCE"].ToString() != "")
                {
                    List<ListItem> list = GetDataList(dt.Rows[k]["PVCH_DATASOURCE"].ToString(), dt.Rows[k]["PVCH_DATAVALUEFIELD"].ToString(), dt.Rows[k]["PVCH_DATATEXTFIELD"].ToString());
                    controlText = "<div class='" + strReqC + "' " + "' title='" + strTitle + "' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "'>";
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (dt.Rows[k]["PVCH_DEFAULTVALUE"].ToString() == list[i].Text)
                        {
                            controlText = " <input type='checkbox' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + i + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' value='" + list[i].Value + "'  class=''>" + list[i].Text + "</input>";
                        }
                        else
                        {
                            controlText = " <input type='checkbox' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + i + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'  formCntl='yes'  name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' value='" + list[i].Value + "' class=''>" + list[i].Text + "</input>";
                        }
                    }
                    controlText = controlText + "</div>";
                }
                else
                {
                    if (dt.Rows[k]["PVCH_OPTION"].ToString() != null)
                    {
                        controlText = "<div class='" + strReqC + "' " + "' title='" + strTitle + "' formCntl='yes'  name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "'>";
                        string[] drdarray = dt.Rows[k]["PVCH_OPTION"].ToString().Split(',');
                        for (int i = 0; i < drdarray.Length; i++)
                        {
                            if (dt.Rows[k]["PVCH_DEFAULTVALUE"].ToString() == drdarray[i])
                            {
                                controlText = " <input type='checkbox' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + i + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' value='" + drdarray[i] + "' checked='checked' class=''>" + drdarray[i] + "</input>";
                            }
                            else
                            {
                                controlText = controlText + " <input type='checkbox' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + i + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' value='" + drdarray[i] + "' class=''>" + drdarray[i] + "</input>";
                            }
                        }

                        controlText = controlText + "</div>";
                    }
                }

                // strHtml = strHtml + "";
            }

            strRow = strRow + LayOut(intAllignment, lebeltext, controlText, strReq, dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + k);

            //for (int ctr = 0; ctr < intAllignment; ctr++)
            //{
            //}

            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "BlankRow")
            {
                strHtml = strHtml + "<div class='col-sm-12'></div><div class='clearfix'></div>";
            }
            else if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "Heading")
            {
                // counter = counter;
                strHtml = strHtml + "<div class='col-sm-12'><h2>" + dt.Rows[k]["PVCH_HEADINGTEXT"].ToString() + "</h2><hr/></div> <div class='clearfix'></div>";// +"</div></div>";
            }
            else if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "Label")
            {
                // counter = counter;
                strHtml = strHtml + "<div class='col-sm-12'><h3>" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "</h3></div> <div class='clearfix'></div>";// +"</div></div>";
            }
            else if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "Center")
            {
                // counter = counter;
                strHtml = strHtml + "<div class='col-sm-12'><h3 class='text-center'>" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "</h3></div> <div class='clearfix'></div>";// +"</div></div>";
            }
            else if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "Declaration")
            {
                // counter = counter;
                strHtml = strHtml + "<div class='col-sm-12 paddingLR15'><input type='checkbox' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' formCntl='yes'  name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' value='" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "' class=''>" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "</input></div><div class='clearfix'></div>";
            }
            else if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "SameAs")
            {
                // counter = counter;
                strHtml = strHtml + "<div class='col-sm-6 text-right sameasabove'><input type='checkbox' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'    value='" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "' class=''>" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "</input></div><div class='clearfix'></div>";

                // strHtml = strHtml + "<div class='col-sm-6 text-right sameasabove'><input type='checkbox' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + k + "' title='" + strTitle + "' tabindex='" + k + "' formCntl='yes'  name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' value='" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "' class=''>" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "</input></div><div class='clearfix'></div>";
            }
            else if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "Plugin")
            {
                if (dt.Rows[k]["PVCH_DATASOURCE"].ToString() == "1")
                {
                    StringWriter sw = new StringWriter();
                    string queryStr = "?AppplicationNo=1&FormId=1";
                    strHtml = strHtml + "<div class='col-sm-12' id='div_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'><div class='col-sm-12 '><h4 id='h4_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'>" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "<small class='text-red' style='display:none;font-size:85%;margin-left:4px;'>*</small></h4></div><div class='clearfix'></div><div class='col-sm-12'><table class='table table-bordered' formCntl='yes' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' data='" + dt.Rows[k]["PVCH_PLUGINID"].ToString() + "' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' title='" + dt.Rows[k]["PVCH_OPTION"].ToString().TrimStart().TrimEnd() + "'></table>" + sw + "</div> <div class='clearfix'></div></div>";// +"</div></div>";
                    //strHtml = strHtml + "<div class='col-sm-12'><iFrame  src=" + dt.Rows[k]["PVCH_PLUGINID"].ToString() + queryStr + " style='overflow: hidden; height='300px  ' height='300px' width='100%' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' title='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "' name='" + dt.Rows[k]["PVCH_DATASOURCE"].ToString() + "' ></iFrame></div> <div class='clearfix'></div>";// +"</div></div>";
                    strJsonStr = strJsonStr + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + ',';

                    hdnPluginJson.Value = hdnPluginJson.Value + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + ',';

                    //-------------------------
                    //string htmltext2 = "<div><table id='tblId' class='table table-bordered addmoretable' title='" + dt.Rows[k]["PVCH_PLUGINID"].ToString() + "' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "'>";
                    //string strCntrl = "";
                    //strOption = "";
                    //string hml = "";
                    //string rw = "<tr>";
                    //string strtd = "";
                    //string[] lbltext = dt.Rows[k]["PVCH_HEADINGTEXT"].ToString().Split('-');
                    //string[] lblotype = dt.Rows[k]["PVCH_DEFAULTVALUE"].ToString().Split('-');
                    //string[] txtOption = dt.Rows[k]["PVCH_OPTION"].ToString().Split('-');
                    //string[] txtUnqNm = dt.Rows[k]["PVCH_PLUGINID"].ToString().Split('-');

                    //for (int i = 0; i < lbltext.Length; i++)
                    //{
                    //    hml = hml + "<th>" + lbltext[i] + "</th>";
                    //}
                    //rw = rw + hml+"</tr><tr>";
                    //for (int j = 0; j < lblotype.Length; j++)
                    //{

                    //    if (lblotype[j] == "text")
                    //    {
                    //        string ida = lbltext[j].ToString();
                    //        strCntrl = "<div class='col-sm-4 margin-bottom10'><input type='text' id='lbl_" + txtUnqNm[j] + "_" + j + "'  title='This filed' class='" + strClass + strReqT + "' name='lbl_" + txtUnqNm[j] + "_" + j + "'></input></div>";
                    //    }
                    //    if (lblotype[j] == "file")
                    //    {
                    //        string ida = lbltext[j].ToString();
                    //        strCntrl = "<div class='col-sm-4 margin-bottom10'><input type='file' id='lbl_" + txtUnqNm[j] + "_" + j + "'  title='This filed' class='" + strClass + strReqT + "' name='lbl_" + txtUnqNm[j] + "_" + j + "'></input></div>";
                    //    }
                    //    if (lblotype[j] == "select")
                    //    {
                    //        string[] txtOpt = txtOption[j].Split(',');
                    //        for (int j1 = 0; j1 < txtOpt.Length; j1++)
                    //        {
                    //            strOption = strOption + "<option value='" + txtOpt[j1] + "'  > " + txtOpt[j1] + "</option>";
                    //        }
                    //        strCntrl = "<div class='col-sm-4 margin-bottom10'><select type='text' id='sel_" + txtUnqNm[j] + "_" + j + "'  title='This filed' class='" + strClass + strReqT + "' name='sel_" + txtUnqNm[j] + "_" + j + "'>" + strOption + "</select></div>";
                    //    }
                    //    strtd = strtd + "<td>" + strCntrl + "</td>";

                    //}
                    //rw =rw + strtd + "</tr>";
                    //htmltext2 = htmltext2 + rw + "</table><button id='btnid' class='btn btn-success btn-add'><i class='fa fa-plus'></i></button></div>";

                    //strHtml = strHtml + "<div class='col-sm-12'>" + htmltext2 + "</div> <div class='clearfix'></div>";// +"</div></div>";
                }
                else if (dt.Rows[k]["PVCH_DATASOURCE"].ToString() == "2")
                {
                    string strHTML1 = "<div class='' id='div_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'><div class='col-sm-12' ><h4 id='h4_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'>" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "<small class='text-red' style='display:none;font-size:85%;margin-left:4px;'>*</small></h4></div><div class='clearfix'></div><div class='col-sm-12 '><table class='table table-bordered' formCntl='yes' id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' title='" + dt.Rows[k]["PVCH_PLUGINID"].ToString().TrimStart().TrimEnd() + "' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "'></div>";
                    string hml = "";
                    string clmn = "";
                    string strHeader = dt.Rows[k]["PVCH_HEADINGTEXT"].ToString();
                    string unqclmn = dt.Rows[k]["PVCH_PLUGINID"].ToString();
                    string strColumn = dt.Rows[k]["PVCH_OPTION"].ToString();
                    string tblNm = dt.Rows[k]["PVCH_CONTROL_ID"].ToString();
                    string[] hedarry = strHeader.Split('-');
                    string[] clmnarry = strColumn.Split('-');
                    string[] unqarry = unqclmn.Split('-');
                    string[] type = dt.Rows[k]["PVCH_DEFAULTVALUE"].ToString().Split('-');
                    for (int i = 0; i < hedarry.Length; i++)
                    {
                        hml = hml + "<th>" + hedarry[i] + "</th>";
                    }
                    for (int j = 0; j < clmnarry.Length; j++)
                    {
                        //clmn = "";
                        int idd = j + 1;
                        string textids = tblNm.Trim() + "_" + unqarry[0].Trim() + "_" + idd.ToString().Trim();// "txt_" + hedarry[0] + "_" + j;

                        clmn = clmn + "<tr ><td  ><input type='text' title='This filed' class='" + strClass + strReqT + "'  id='" + textids + "' maxlength=10 name='" + textids + "' readonly value=" + clmnarry[j] + "></td>";
                        for (int i = 0; i < hedarry.Length - 1; i++)
                        {
                            string textids1 = tblNm + "_" + unqarry[i + 1] + "_" + idd; // "txt_" + hedarry[i + 1] + "_" + i;
                            if (type[i + 1].ToString() == "text")
                            {
                                clmn = clmn + "<td ><input type='text'  title='This filed' class='" + strClass + strReqT + "' id='" + textids1 + "' maxlength=10 name='" + textids1 + "'>";
                            }
                            else if (type[i + 1].ToString() == "Decimal")
                            {
                                clmn = clmn + "<td ><input type='text'  title='This filed' class='" + strClass + strReqT + "' id='" + textids1 + "' maxlength=10 name='" + textids1 + "' Onkeypress='return isNumberKey(event," + qut + textids1 + qut + ")'>";
                            }
                            else if (type[i + 1].ToString() == "Number")
                            {
                                clmn = clmn + "<td ><input type='text'  title='This filed' class='" + strClass + strReqT + "' id='" + textids1 + "' maxlength=10 name='" + textids1 + "' Onkeypress='return inputLimiter(event," + qut + "Numbers" + qut + ")'>";
                            }
                            else if (type[i + 1].ToString() == "file")
                            {
                                clmn = clmn + "<td ><input type='file'  title='This filed' class='" + strClass + strReqT + "' id='" + textids1 + "' maxlength=10 name='" + textids1 + "' Onkeypress='return inputLimiter(event," + qut + "Numbers" + qut + ")'>";
                            }
                            //clmn = clmn + "<td ><input type='text'  title='This filed' class='" + strClass + strReqT + "' id='" + textids1 + "' maxlength=50 name='" + textids1 + "'>";
                        }
                        clmn = clmn + "</tr>";
                    }
                    strHTML1 = strHTML1 + hml + clmn + "</table></div>";
                    strHtml = strHtml + "<div class='col-sm-12'>" + strHTML1 + "</div> <div class='clearfix'></div>";// +"</div></div>";
                }
                else if (dt.Rows[k]["PVCH_DATASOURCE"].ToString() == "3")
                {
                    string htmltext = "<div class='' id='div_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "'><div class='col-sm-12 ' ><table id='" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' formCntl='yes' class='table table-bordered addmoretable' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "' title='" + dt.Rows[k]["PVCH_PLUGINID"].ToString().TrimStart().TrimEnd() + "' name='" + dt.Rows[k]["PVCH_CONTROL_NAME"].ToString() + "'><tr><td>";
                    string strCntrl = "";
                    string strlbl = "";
                    strOption = "";

                    string tblNm = dt.Rows[k]["PVCH_CONTROL_ID"].ToString();
                    string[] lbltext = dt.Rows[k]["PVCH_HEADINGTEXT"].ToString().Split('-');
                    string[] lblotype = dt.Rows[k]["PVCH_DEFAULTVALUE"].ToString().Split('-');
                    string[] txtOption = dt.Rows[k]["PVCH_OPTION"].ToString().Split('-');
                    string[] txtUnqNm = dt.Rows[k]["PVCH_PLUGINID"].ToString().Split('-');

                    for (int i = 0; i < lblotype.Length; i++)
                    {
                        int idd = 0;
                        string idss = tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd;
                        if (lblotype[i] == "FullName")
                        {
                            string ida = lbltext[i].ToString();
                            strCntrl = "<div class='col-sm-4 ' id='div_'" + idss + "><input type='text' id='" + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + "' maxlength=50 title='This filed' class='cls " + strClass + strReqT + "' name='lbl_" + txtUnqNm[i] + "_" + i + "' Onkeypress='return inputLimiter(event," + qut + "NameCharacters" + qut + ")'></input></div>";
                        }
                        if (lblotype[i] == "Decimal")
                        {
                            string ida = lbltext[i].ToString();
                            strCntrl = "<div class='col-sm-4 ' id='div_'" + idss + "><input onchange='ControlChangeEvt(" + qut + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + qut + ");' type='text' id='" + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + "' maxlength=50 title='This filed' class='cls " + strClass + strReqT + "' name='lbl_" + txtUnqNm[i] + "_" + i + "'  Onkeypress='return isNumberKey(event," + qut + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + qut + ")'></input></div>";
                        }
                        if (lblotype[i] == "Email")
                        {
                            string ida = lbltext[i].ToString();
                            strCntrl = "<div class='col-sm-4 ' id='div_'" + idss + "><input type='text' id='" + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + "' maxlength=50 title='This filed' class='cls " + strClass + strReqE + "' name='lbl_" + txtUnqNm[i] + "_" + i + "' Onkeypress='return inputLimiter(event," + qut + "Email" + qut + ")'></input></div>";
                        }
                        if (lblotype[i] == "text")
                        {
                            string ida = lbltext[i].ToString();
                            strCntrl = "<div class='col-sm-4 ' id='div_'" + idss + "><input type='text' id='" + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + "' maxlength=50 title='This filed' class='cls " + strClass + strReqT + "' name='lbl_" + txtUnqNm[i] + "_" + i + "' Onkeypress='return inputLimiter(event," + qut + "Address" + qut + ")'></input></div>";
                        }
                        if (lblotype[i] == "PhoneNumber")
                        {
                            string ida = lbltext[i].ToString();
                            strCntrl = "<div class='col-sm-4 ' id='div_'" + idss + "><input  id='" + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + "' maxlength=10 title='This filed' class='cls " + strClass + strReqT + "' name='lbl_" + txtUnqNm[i] + "_" + i + "' onchange='return MobileValidation(" + qut + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + qut + ");' Onkeypress='return inputLimiter(event," + qut + "Mobile" + qut + ")'></input></div>";
                        }
                        if (lblotype[i] == "number")
                        {
                            string ida = lbltext[i].ToString();
                            strCntrl = "<div class='col-sm-4 ' id='div_'" + idss + "><input onchange='ControlChangeEvt(" + qut + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + qut + ");' type='text' id='" + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + "' maxlength=15 title='This filed' class='cls " + strClass + strReqT + " nmCnt' name='lbl_" + txtUnqNm[i] + "_" + i + "' Onkeypress='return inputLimiter(event," + qut + "Numbers" + qut + ")'></input></div>";
                        }
                        if (lblotype[i] == "DateTime")
                        {
                            string ida = lbltext[i].ToString();
                            strCntrl = "<div class='col-sm-4' id='div_'" + idss + "><div class='input-group '><input tt='dt' type='text'   id='" + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + "'  title='" + strTitle + "' tabindex='" + dt.Rows[k]["INT_SEQUENCEID"].ToString() + "'  name='lbl_" + txtUnqNm[i] + "_" + i + "' maxlength='" + Convert.ToInt32(dt.Rows[k]["PINT_LENGTH"].ToString()) + "' class='cls date " + strClass + strReqT + "' click='return DateValidation(this)'><span id='sp_" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "' class='input-group-addon'><i class='fa fa-calendar'></i></span></div></div>";
                        }
                        if (lblotype[i] == "file")
                        {
                            string ida = lbltext[i].ToString();
                            strCntrl = "<div class='col-sm-4 ' id='div_'" + idss + "><input type='file' id='" + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + "'  title='This filed' class='cls " + strClass + strReqT + "' name='lbl_" + txtUnqNm[i] + "_" + i + "'></input></div>";
                        }
                        if (lblotype[i] == "select")
                        {
                            strOption = "";
                            string[] txtOpt = txtOption[i].Split(',');
                            for (int j = 0; j < txtOpt.Length; j++)
                            {
                                strOption = strOption + "<option value='" + txtOpt[j] + "'  > " + txtOpt[j] + "</option>";
                            }
                            strCntrl = "<div class='col-sm-4 margin-bottom10' id='div_'" + idss + "><select type='text' id='" + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + "'  onchange='OnchangeCloneDrp(this);'  title='This filed' class='cls " + strClass + strReqT + "' name='sel_" + txtUnqNm[i] + "_" + i + "'>" + strOption + "</select></div>";
                        }

                        strlbl = "<Label class='cls col-sm-3 margin-bottom8' id='lbl_" + tblNm + "_" + txtUnqNm[i].Replace(" ", "") + "_" + idd + "'>" + lbltext[i] + "</label>";
                        htmltext = htmltext + strlbl + strCntrl + "<div class='clearfix margin-bottom6'></div>";
                    }

                    string clickevt = dt.Rows[k]["PVCH_CONTROL_ID"].ToString();
                    htmltext = htmltext + "</td><td width='50px' style='position:relative'><a class='btn-del text-danger' onClick='$(this).closest(" + qut + "tr" + qut + ").remove();'><i class='fa fa-times'></i></a></td></tr></table>";
                    htmltext = htmltext + "<a id=btn" + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + "  onclick='cloneTable(" + qut + dt.Rows[k]["PVCH_CONTROL_ID"].ToString() + qut + ")' class='btn btn-success btn-add'><i class='fa fa-plus'></i></a></div></div>";

                    strHtml = strHtml + "<div class='col-sm-12'>" + htmltext + "</div> <div class='clearfix'></div>";// +"</div></div>";<a class="delete" onclick="DeleteFunc(' + chkids + ')">
                }
            }
            else
            {
                counter = counter + 1;
                if (counter > 1)
                {
                    if ((intAllignment % counter) == 0)
                    {
                        strHtml = strHtml + strRow + "";
                        counter = 0;
                    }
                    else
                    {
                        strHtml = strHtml + strRow;
                    }
                }
                else
                {
                    strHtml = strHtml + strGroupDiv + strRow;
                    // strHtml = strHtml + strRow;
                }
            }
        }

        return strHtml;

        //divLogo.InnerText = FormLogo;

    }
    #endregion

    #region "Service form Details"
    /// <summary>
    /// Radhika Rani Patri
    /// All about service i.e Service logo,Service name,Department
    /// </summary>
    /// <param name="intFormId"></param>
    public void FormDetails(int intFormId)
    {

        DataTable ServiceDtl = new DataTable();
        SqlConnection connection = new SqlConnection(connectionString);
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("USP_SERVICE_DETAILS"))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@P_FORMID", SqlDbType.Int, 10).Value = intFormId;
                cmd.Connection = con;
                con.Open();
                ServiceDtl.Load(cmd.ExecuteReader());
                con.Close();
            }
        }
        if (ServiceDtl.Rows.Count > 0)
        {

            FormHeader = ServiceDtl.Rows[0]["NVCH_HEADERTEXT"].ToString();

            FormFooter = ServiceDtl.Rows[0]["NVCH_FOOTERTEXT"].ToString();
            imgLogo.Src = ApplicationPath + "/Logo/" + ServiceDtl.Rows[0]["VCH_LOG"].ToString();
            //FormLogo = "<img src=Logo/" + ServiceDtl.Rows[0]["VCH_LOG"].ToString() + " alt='logo'/>";
            intAllignment = Convert.ToInt32(ServiceDtl.Rows[0]["INT_ALLIGNMENT"].ToString());
        }
        // <img src=" + imgUrl + " alt='logo'/>
    }
    #endregion

    #region "Fill table column"
    /// <summary>
    /// Radhika Rani Patri
    /// Dynamic fill of table from which dropdwon bind
    /// </summary>
    /// <param name="tablename"></param>
    /// <param name="firstClmn"></param>
    /// <param name="secondndClmn"></param>
    /// <returns></returns>
    public static List<ListItem> GetDataList(string tablename, string firstClmn, string secondndClmn)
    {
        string Qury = "SELECT " + firstClmn + " AS COLUMN_NAME_VALUE," + secondndClmn + " AS COLUMN_NAME_TEXT FROM " + tablename + " where intstateid=20 order by " + secondndClmn + " asc ";
        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(Qury))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(new ListItem
                        {
                            Value = sdr["COLUMN_NAME_VALUE"].ToString(),
                            Text = sdr["COLUMN_NAME_TEXT"].ToString()
                        });
                    }
                }
                con.Close();
                return customers;
            }
        }
    }

    #endregion

    #region "LayOut setting"
    /// <summary>
    /// Radhika Rani Patri
    /// Dynamic layout setting i.e one,two,three
    /// </summary>
    /// <param name="AllignmentType"></param>
    /// <param name="lblText"></param>
    /// <param name="controls"></param>
    /// <param name="isRequired"></param>
    /// <returns></returns>
    /// 
    public string LayOut(int AllignmentType, string lblText, string controls, string isRequired, string id)
    {
        string strText = "";
        if (lblText != "")
        {
            if (AllignmentType == 3)
            {
                if (isRequired == "1")
                {

                    strText = "<div class='col-sm-4'><label for='sss'>" + lblText + "</label><span class='text-red'>*</span>" + controls + "</div>";

                }
                else
                {
                    strText = "<div class='col-sm-4'><label for='sss'>" + lblText + "</label>" + controls + "</div>";
                }
            }
            else if (AllignmentType == 2)
            {
                if (isRequired == "1")
                {
                    strText = "<label for='sss' class='col-sm-2' id='lbl_" + id + "'>" + lblText + " <span class='text-red'>*</span></label><div class='col-sm-4'><span class='colon'>:</span>" + controls + "</div>";

                }
                else
                {
                    // strText = "<div class='col-sm-6'><label for='sss'>" + lblText + "</label>" + controls + "</div>";
                    strText = "<label for='sss' class='col-sm-2' id='lbl_" + id + "'>" + lblText + " </label><div class='col-sm-4'><span class='colon'>:</span>" + controls + "</div>";
                }
            }
            else if (AllignmentType == 1)
            {
                if (isRequired == "1")
                {
                    strText = "<div class='' id='div_" + id + "'><div class='col-sm-6 margin-bottom8'><label for='sss' class='col-sm-5' id='lbl_" + id + "'>" + lblText + "<span class='text-red'>*</span></label><div class='col-sm-7'><span class='colon'>:</span>" + controls + "</div></div></div>";

                }
                else
                {
                    // strText = "<div class='col-sm-12'><label for='sss'>" + lblText + "</label>" + controls + "</div>";
                    strText = "<div class='' id='div_" + id + "'><div class='col-sm-6 margin-bottom8'><label for='sss' class='col-sm-5' id='lbl_" + id + "'>" + lblText + "</label><div class='col-sm-7'><span class='colon'>:</span>" + controls + "</div></div></div>";
                }
            }
            else
            {
                if (isRequired == "1")
                {

                    strText = "<div class='col-sm-2 '><label for='sss'>" + lblText + "</label><span class='text-red'>*</span>" + controls + "</div> <div class='clearfix'></div>";

                }
                else
                {
                    strText = "<div class='col-sm-2'><label for='sss'>" + lblText + "</label>" + controls + "</div>";
                }
            }
        }

        else
        {
            strText = "<div class='col-sm-12'>" + controls + "</div>";
        }
        return strText;
    }
    //public string LayOut(int AllignmentType, string lblText, string controls, string isRequired, string id)
    //{
    //    string strText = "";
    //    if (lblText != "")
    //    {
    //        if (AllignmentType == 3)
    //        {
    //            if (isRequired == "1")
    //            {

    //                strText = "<div class='col-sm-4'><label for='sss'>" + lblText + "</label><span class='mandetory'>*</span>" + controls + "></div>";

    //            }
    //            else
    //            {
    //                strText = "<div class='col-sm-4'><label for='sss'>" + lblText + "</label>" + controls + "</div>";
    //            }
    //        }
    //        else if (AllignmentType == 2)
    //        {
    //            if (isRequired == "1")
    //            {
    //                strText = "<label for='sss' class='col-sm-2' id='lbl_" + id + "'>" + lblText + " </label><div class='col-sm-4'><span class='mandetory'>*</span>" + controls + "</div>";

    //            }
    //            else
    //            {
    //                // strText = "<div class='col-sm-6'><label for='sss'>" + lblText + "</label>" + controls + "</div>";
    //                strText = "<label for='sss' class='col-sm-2' id='lbl_" + id + "'>" + lblText + " </label><div class='col-sm-4'>" + controls + "</div>";
    //            }
    //        }
    //        else if (AllignmentType == 1)
    //        {
    //            if (isRequired == "1")
    //            {
    //                strText = "<label for='sss' class='col-sm-4 col-sm-offset-1' id='lbl_" + id + "'>" + lblText + "</label><div class='col-sm-6 margin-bottom15'><span class='mandetory'>*</span>" + controls + "</div><div class='clearfix'></div>";

    //            }
    //            else
    //            {
    //                // strText = "<div class='col-sm-12'><label for='sss'>" + lblText + "</label>" + controls + "</div>";
    //                strText = "<label for='sss' class='col-sm-4 col-sm-offset-1' id='lbl_" + id + "'>" + lblText + "</label><div class='col-sm-6 margin-bottom15'>" + controls + "</div><div class='clearfix'></div>";
    //            }
    //        }
    //        else
    //        {
    //            if (isRequired == "1")
    //            {

    //                strText = "<div class='col-sm-4 col-sm-offset-2'><label for='sss'>" + lblText + "</label> <span class='mandetory'>*</span>" + controls + "</div> <div class='clearfix'></div>";

    //            }
    //            else
    //            {
    //                strText = "<div class='col-sm-4'><label for='sss'>" + lblText + "</label>" + controls + "</div>";
    //            }
    //        }
    //    }

    //    else
    //    {
    //        strText = "<div class='col-sm-12'>" + controls + "</div>";
    //    }
    //    return strText;
    //}
    #endregion

    #region "Submit button"
    /// <summary>
    /// Radhika Rani Patri
    /// Dynamic data receive from client site json format insert into table
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string strSQLQuery = "";
        try
        {
            string key = "";
            string value = "";

            string strColumnList = "VCH_APPLICATION_UNQ_KEY,";
            string strValueList = "@VCH_APPLICATION_UNQ_KEY,";
            string strTableName = "";
            var jsonResult1 = hdnResultOutPut.Value;
            if (jsonResult1 != "" && jsonResult1 != null)
            {
                string val1 = AESEncrytDecry.DecryptStringAES(jsonResult1);
                var jsonResult = val1;
                if ((jsonResult != "" && jsonResult != null) || (hdnPluginValue.Value != "" && hdnPluginValue.Value != null))
                {
                    strUnqId = uniqueKeyGenerate(Convert.ToInt32(Request.QueryString["FormId"].ToString()));
                }

                if (Request.QueryString["FormId"] != "" && Request.QueryString["FormId"] != null)
                {
                    strTableName = "TABLE_" + (Request.QueryString["FormId"].ToString());
                }
                else
                {
                    strTableName = "TABLE_1";
                }


                if (jsonResult != "" && jsonResult != null)
                {
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResult);
                    foreach (var kv in dict)
                    {
                        key = kv.Key;
                        value = kv.Value;
                        strColumnList = strColumnList + key + ',';
                        strValueList = strValueList + "@" + key + ",";
                    }
                    strColumnList = strColumnList.TrimEnd(',');
                    strValueList = strValueList.TrimEnd(',');
                    strSQLQuery = "insert into " + strTableName + "(" + strColumnList + ")values(" + strValueList + ")";

                    SqlConnection con1 = new SqlConnection(connectionString);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand(strSQLQuery, con1);
                    cmd1.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strUnqId);
                    foreach (var kvs in dict)
                    {
                        cmd1.Parameters.AddWithValue("@" + kvs.Key, kvs.Value);
                    }
                    //int status1 = 0;
                    int status1 = cmd1.ExecuteNonQuery();

                    if (status1 >= 1)
                    {

                        // string strquery = "update T_APPLICATION_TBL set VCH_INDUSTRYCODE='sd',INT_SERVICEID=" + Convert.ToInt32(Request.QueryString["FormId"].ToString()) + ", VCH_PROPOSALID=" + Request.QueryString["FormId"].ToString() + "DTM_CREATEDON=getdate(),VCH_STATUS=1 where VCH_APPLICATION_UNQ_KEY=" + strUnqId + "";
                        // SqlCommand cmd2 = new SqlCommand(strquery, con1);
                        // cmd2.ExecuteNonQuery();
                    }
                    con1.Close();
                    outStatus = "1";
                }
                ///--------------------------------------plugindata--------------------------
                ///   strUnqId = uniqueKeyGenerate(Convert.ToInt32(Request.QueryString["FormId"].ToString()));

                JArray array = new JArray();
                string strColumn = "";
                string pluginData = string.Empty;
                if (hdnPluginValue.Value != null && hdnPluginValue.Value != "")
                {
                    if (jsonResult == "" || jsonResult == null)
                    {
                        string strInsert1 = "INSERT INTO " + strTableName + "(VCH_APPLICATION_UNQ_KEY) VALUES(" + strUnqId + ")";
                        SqlConnection con = new SqlConnection(connectionString);
                        con.Open();
                        SqlCommand cmd = new SqlCommand(strInsert1, con);
                        int status = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    pluginData = hdnPluginValue.Value.TrimEnd(';');//plugin to plugin separation
                    string strInsert = "";
                    string[] tabledata = pluginData.Split(';');
                    for (int i = 0; i < tabledata.Length; i++)
                    {
                        string[] tblName = tabledata[i].Split('?');//tablename separeted by data
                        string[] tblNmWthColmn = tblName[0].Split(',');//
                        string strTableNm = tblNmWthColmn[0];

                        pluginStringEntry(strTableName, tblNmWthColmn[0], strUnqId, tblName[1]);
                    }

                    ///---------------------------------------plugin end-----------------------------
                    outStatus = "1";

                }
                if (outStatus == "1")
                {
                    MasterTableInsert();
                    string rawURL = Request.RawUrl;
                    // string strHead= AccountHeadWithAmount(Convert.ToInt32(Request.QueryString["FormId"].ToString()), hdnTotalAmount.Value.ToString(), strUnqId);
                    //if (strHead != "" && strHead != "NA")
                    //{
                    //    //strHead = strHead;
                    //}
                    //else
                    //{
                    //    strHead = "NA";
                    //}
                    //Response.Redirect("ServicePayment.aspx?ApplicationKey=" + strUnqId + "&Amount=" + hdnTotalAmount.Value + "&transactionDetail=" + strHead + "&AccountHd=" + hdnAccountHead.Value + "&ServiceID=" + Convert.ToInt32(Request.QueryString["FormId"].ToString()) + "");

                    int intenableEsign = 0;
                    string strSQLQueryEsign = "SELECT Esign FROM  tbl_Config";
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(strSQLQueryEsign, con);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        if (ds.Tables[0].Rows[0][0] != DBNull.Value)
                        {
                            intenableEsign = Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                        }

                    }

                    DataAcessLayer.Service.ServiceDataLayer objData = new DataAcessLayer.Service.ServiceDataLayer();
                    ServiceDetails clsobj = new ServiceDetails();
                    clsobj.strAction = "I";
                    clsobj.vchAccountHead = hdnAccountHead.Value.ToString();
                    clsobj.Dec_Amount = Convert.ToDecimal(hdnTotalAmount.Value.ToString());
                    clsobj.intServiceId = Convert.ToInt32(Request.QueryString["FormId"].ToString());
                    clsobj.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
                    clsobj.str_ApplicationNo = strUnqId;
                    if (hdnApplicationFee.Value == "")
                    {
                        clsobj.decAppFee = 0;
                    }
                    else
                    {
                        clsobj.decAppFee = Convert.ToInt32(hdnApplicationFee.Value);
                    }
                    objData.AddTransactionDetails(clsobj);
                    Response.Redirect("FormEditView1.aspx?FormId=" + Request.QueryString["FormId"].ToString() + "&AppKey=" + strUnqId + "&ProposalNo=" + Request.QueryString["ProposalNo"].ToString() + "&ReqMode=" + Request.QueryString["ReqMode"].ToString());

                    //if (intenableEsign == 1)
                    //{
                    //    Response.Redirect("ServiceFileCheck.aspx?ApplicationKey=" + strUnqId + "&Amount=" + hdnTotalAmount.Value + "&AccountHd=" + hdnAccountHead.Value + "&ServiceID=" + Convert.ToInt32(Request.QueryString["FormId"].ToString()) + "");
                    //}
                    //else
                    //{
                    //if (Request.QueryString["FormId"] == "7") ////Contract Labour then application fee is the calculated value else zero.
                    //{
                    //    Response.Redirect("ServicePayment.aspx?ApplicationKey=" + strUnqId + "&Amount=" + hdnTotalAmount.Value + "&AccountHd=" + hdnAccountHead.Value + "&ServiceID=" + Convert.ToInt32(Request.QueryString["FormId"].ToString()) + "&AppFee=" + hdnApplicationFee.Value + "");
                    //}
                    //else if (Request.QueryString["FormId"] == "41") ////DOWR then application fee is the calculated value else zero.
                    //{
                    //    Response.Redirect("ServicePayment.aspx?ApplicationKey=" + strUnqId + "&Amount=" + hdnTotalAmount.Value + "&AccountHd=" + hdnAccountHead.Value + "&ServiceID=" + Convert.ToInt32(Request.QueryString["FormId"].ToString()) + "&AppFee=" + hdnApplicationFee.Value + "");
                    //}
                    //else if (Request.QueryString["FormId"] == "16") ////ENERGY then application fee is the calculated value else zero.
                    //{
                    //    Response.Redirect("ServicePayment.aspx?ApplicationKey=" + strUnqId + "&Amount=" + hdnTotalAmount.Value + "&AccountHd=" + hdnAccountHead.Value + "&ServiceID=" + Convert.ToInt32(Request.QueryString["FormId"].ToString()) + "&AppFee=" + hdnApplicationFee.Value + "");
                    //}
                    //else
                    //{
                  //  Response.Redirect("FormEditView1.aspx?FormId=" +  Request.QueryString["FormId"].ToString() + "&AppKey=" + strUnqId + "&ProposalNo=" + Request.QueryString["ProposalNo"].ToString() + "&ReqMode=" + Request.QueryString["ReqMode"].ToString());
                           // Response.Redirect("ServicePayment.aspx?ApplicationKey=" + strUnqId + "&Amount=" + hdnTotalAmount.Value + "&AccountHd=" + hdnAccountHead.Value + "&ServiceID=" + Convert.ToInt32(Request.QueryString["FormId"].ToString()) + "&AppFee=0");
                        //}
                    //}
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString() + " Dynamic Query :" + strSQLQuery);
        }
    }
    protected void btnDraft_Click(object sender, EventArgs e)
    {
        string strSQLQuery = "";
        try
        {
            string key = "";
            string value = "";

            string strColumnList = "VCH_APPLICATION_UNQ_KEY,";
            string strValueList = "@VCH_APPLICATION_UNQ_KEY,";
            string strTableName = "";
            var jsonResult1 = hdnResultOutPut.Value;
            if (jsonResult1 != "" && jsonResult1 != null)
            {
                string val1 = AESEncrytDecry.DecryptStringAES(jsonResult1);
                var jsonResult = val1;
                if ((jsonResult != "" && jsonResult != null) || (hdnPluginValue.Value != "" && hdnPluginValue.Value != null))
                {
                    strUnqId = uniqueKeyGenerate(Convert.ToInt32(Request.QueryString["FormId"].ToString()));
                }
                if (Session["vchGROUPID"] == null)
                {
                    Session["vchGROUPID"] = GETGROUPID();
                }

                if (Request.QueryString["FormId"] != "" && Request.QueryString["FormId"] != null)
                {
                    strTableName = "TABLE_" + (Request.QueryString["FormId"].ToString());
                }
                else
                {
                    strTableName = "TABLE_1";
                }


                if (jsonResult != "" && jsonResult != null)
                {
                    var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResult);
                    foreach (var kv in dict)
                    {
                        key = kv.Key;
                        value = kv.Value;
                        strColumnList = strColumnList + key + ',';
                        strValueList = strValueList + "@" + key + ",";
                    }
                    strColumnList = strColumnList.TrimEnd(',');
                    strValueList = strValueList.TrimEnd(',');
                    strSQLQuery = "insert into " + strTableName + "(" + strColumnList + ")values(" + strValueList + ")";

                    SqlConnection con1 = new SqlConnection(connectionString);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand(strSQLQuery, con1);
                    cmd1.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", strUnqId);
                    foreach (var kvs in dict)
                    {
                        cmd1.Parameters.AddWithValue("@" + kvs.Key, kvs.Value);
                    }
                    //int status1 = 0;
                    int status1 = cmd1.ExecuteNonQuery();

                    if (status1 >= 1)
                    {

                        // string strquery = "update T_APPLICATION_TBL set VCH_INDUSTRYCODE='sd',INT_SERVICEID=" + Convert.ToInt32(Request.QueryString["FormId"].ToString()) + ", VCH_PROPOSALID=" + Request.QueryString["FormId"].ToString() + "DTM_CREATEDON=getdate(),VCH_STATUS=1 where VCH_APPLICATION_UNQ_KEY=" + strUnqId + "";
                        // SqlCommand cmd2 = new SqlCommand(strquery, con1);
                        // cmd2.ExecuteNonQuery();
                    }
                    con1.Close();
                    outStatus = "1";
                }
                ///--------------------------------------plugindata--------------------------
                ///   strUnqId = uniqueKeyGenerate(Convert.ToInt32(Request.QueryString["FormId"].ToString()));

                JArray array = new JArray();
                string strColumn = "";
                string pluginData = string.Empty;
                if (hdnPluginValue.Value != null && hdnPluginValue.Value != "")
                {
                    if (jsonResult == "" || jsonResult == null)
                    {
                        string strInsert1 = "INSERT INTO " + strTableName + "(VCH_APPLICATION_UNQ_KEY) VALUES(" + strUnqId + ")";
                        SqlConnection con = new SqlConnection(connectionString);
                        con.Open();
                        SqlCommand cmd = new SqlCommand(strInsert1, con);
                        int status = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    pluginData = hdnPluginValue.Value.TrimEnd(';');//plugin to plugin separation
                    string strInsert = "";
                    string[] tabledata = pluginData.Split(';');
                    for (int i = 0; i < tabledata.Length; i++)
                    {
                        string[] tblName = tabledata[i].Split('?');//tablename separeted by data
                        string[] tblNmWthColmn = tblName[0].Split(',');//
                        string strTableNm = tblNmWthColmn[0];

                        pluginStringEntry(strTableName, tblNmWthColmn[0], strUnqId, tblName[1]);
                    }

                    ///---------------------------------------plugin end-----------------------------
                    outStatus = "1";

                }
                if (outStatus == "1")
                {
                    MasterTableInsertDraft();

                    //Changes By Manoj Kumar Behera for multipule Service

                    if (Request.QueryString["ReqMode"].ToString() == "M")
                    {
                        try
                        {
                            DataAcessLayer.Service.ServiceDataLayer objData = new DataAcessLayer.Service.ServiceDataLayer();
                            ServiceDetails clsobj = new ServiceDetails();
                            clsobj.action = "MI";
                            clsobj.vchAccountHead = hdnAccountHead.Value.ToString();
                            clsobj.Dec_Amount = Convert.ToDecimal(hdnTotalAmount.Value.ToString());
                            clsobj.intServiceId = Convert.ToInt32(Request.QueryString["FormId"].ToString());
                            clsobj.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
                            clsobj.str_ApplicationNo = strUnqId;
                            if (hdnApplicationFee.Value == "")
                            {
                                clsobj.decAppFee = 0;
                            }
                            else
                            {
                                clsobj.decAppFee = Convert.ToInt32(hdnApplicationFee.Value);
                            }
                            objData.AddTransactionDetails(clsobj);
                        }
                        catch
                        {
                        }

                        CreateNavigationMenuAfterDraft(strUnqId, hdnTotalAmount.Value.ToString());
                    }
                    else
                    {
                        string rawURL = Request.RawUrl;
                        Response.Redirect("DraftedServices1.aspx");
                    }
                    //Changes By Manoj Kumar Behera
                }
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message.ToString() + " Dynamic Query :" + strSQLQuery);
            Util.LogError(ex, "Service");
        }
    }
    #endregion

    #region "UniqueKey Generation"
    /// <summary>
    /// Radhika Rani Patri
    /// Using service id generate unique key from database level
    /// </summary>
    /// <param name="ServiceId"></param>
    /// <returns></returns>
    public string uniqueKeyGenerate(int ServiceId)
    {
        string EmpName = "";
        //SqlConnection connection = new SqlConnection(connectionString);
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("USP_UNIQUE_KEY"))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PINT_SERVICEID", SqlDbType.Int).Value = Convert.ToInt32(ServiceId);
                if(Session["vchGROUPID"]!=null)
                cmd.Parameters.Add("@P_VCH_GROUP_ID", SqlDbType.VarChar).Value = Session["vchGROUPID"].ToString();
                cmd.Parameters.Add("@PMSG_OUT", SqlDbType.VarChar, 50);
                cmd.Parameters["@PMSG_OUT"].Direction = ParameterDirection.Output;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                EmpName = cmd.Parameters["@PMSG_OUT"].Value.ToString();
                con.Close();
            }
        }
        return EmpName;
    }
    #endregion

    public string pluginStringEntry(string tablename, string colmname, string rowname, string data)
    {
        string strInsert = "update " + tablename + " set " + colmname + "='" + data + "'" + "where VCH_APPLICATION_UNQ_KEY='" + rowname + "'";
        try
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(strInsert, con);
            int status = cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            CommonHelperCls ob = new CommonHelperCls();
            string[] InvtoEmail = new string[2];
            InvtoEmail[0] = ConfigurationManager.AppSettings["InternalMail1"].ToString();
            InvtoEmail[1] = ConfigurationManager.AppSettings["InternalMail2"].ToString();
            ob.sendMail("Plugin error :" + Request.QueryString["FormId"].ToString(), strInsert, InvtoEmail, true);
            Util.LogError(ex, "Service");
        }
        return "";
    }
    public void MasterTableInsert()
    {
        try
        {
            string InvestorName = "";
            int investorId = 0;
            string strProposalNo = "";

            DataTable PrpDt = new DataTable();
            SqlConnection con1 = new SqlConnection(connectionString);
            con1.Open();

            if (Request.QueryString["ProposalNo"].ToString() != "" && Request.QueryString["ProposalNo"].ToString() != "0")
            {
                SqlCommand cmd3 = new SqlCommand("USP_PEAL_PROMOTER_AED");
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@PvchAction", SqlDbType.VarChar, 1).Value = "V";
                cmd3.Parameters.Add("@PvchProposalNo", SqlDbType.VarChar, 50).Value = Request.QueryString["ProposalNo"].ToString();
                cmd3.Connection = con1;
                PrpDt.Load(cmd3.ExecuteReader());
                for (int i = 0; i < PrpDt.Rows.Count; i++)
                {
                    if (PrpDt.Rows[0]["vchCompName"] != "" && PrpDt.Rows[0]["vchCompName"] != null)
                    {
                        InvestorName = PrpDt.Rows[0]["vchCompName"].ToString();
                    }
                    else
                    {
                        InvestorName = "NA";
                    }
                    investorId = Convert.ToInt32(PrpDt.Rows[0]["intCreatedBy"].ToString());
                }
                strProposalNo = Request.QueryString["ProposalNo"].ToString();
            }
            else
            {
                InvestorName = Session["IndustryName"].ToString();
                investorId = Convert.ToInt32(hdnInvestor.Value.ToString());
                strProposalNo = "NA";
            }

            /*-----------------------------------------------------------------------*/

            SqlCommand cmd2 = new SqlCommand("USP_ApplicationMasterInsertUpdate");
            cmd2.CommandType = CommandType.StoredProcedure;

            cmd2.Parameters.Add("@P_SERVICEID", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["FormId"].ToString());
            cmd2.Parameters.Add("@P_VCH_PROPOSALID", SqlDbType.VarChar, 50).Value = strProposalNo;
            cmd2.Parameters.Add("@P_VCH_INVESTOR_NAME", SqlDbType.VarChar, 50).Value = InvestorName;
            cmd2.Parameters.Add("@P_VCH_APPLICATION_UNQ_KEY", SqlDbType.VarChar, 50).Value = strUnqId;
            cmd2.Parameters.Add("@INT_CREATEDBY", SqlDbType.VarChar, 50).Value = investorId;
            cmd2.Parameters.Add("@P_CHAR_ACTION", SqlDbType.VarChar, 50).Value = "U";
            cmd2.Parameters.Add("@P_INT_STATUS", SqlDbType.Int).Value = 1;
            cmd2.Parameters.Add("@P_INT_PAYMENT_STATUS", SqlDbType.Int).Value = 0; //0 for submit mode

            cmd2.Connection = con1;
            cmd2.ExecuteReader();
            con1.Close();
        }
        catch (Exception ex)
        {
            Response.Write("Master Tabele Insert " + ex.Message);
            return;
        }
    }
    public void MasterTableInsertDraft()
    {
        try
        {
            string InvestorName = "";
            int investorId = 0;
            string strProposalNo = "";
            DataTable PrpDt = new DataTable();
            SqlConnection con1 = new SqlConnection(connectionString);
            con1.Open();
            if (Request.QueryString["ProposalNo"].ToString() != "" && Request.QueryString["ProposalNo"].ToString() != "0")
            {
                SqlCommand cmd3 = new SqlCommand("USP_PEAL_PROMOTER_AED");
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.Add("@PvchAction", SqlDbType.VarChar, 1).Value = "V";
                cmd3.Parameters.Add("@PvchProposalNo", SqlDbType.VarChar, 50).Value = Request.QueryString["ProposalNo"].ToString();
                cmd3.Connection = con1;
                PrpDt.Load(cmd3.ExecuteReader());
                for (int i = 0; i < PrpDt.Rows.Count; i++)
                {
                    if (PrpDt.Rows[0]["vchCompName"] != "" && PrpDt.Rows[0]["vchCompName"] != null)
                    {
                        InvestorName = PrpDt.Rows[0]["vchCompName"].ToString();
                    }
                    else
                    {
                        InvestorName = "NA";
                    }
                    investorId = Convert.ToInt32(PrpDt.Rows[0]["intCreatedBy"].ToString());
                }
                strProposalNo = Request.QueryString["ProposalNo"].ToString();
            }
            else
            {
                InvestorName = Session["IndustryName"].ToString();
                investorId = Convert.ToInt32(hdnInvestor.Value.ToString());
                strProposalNo = "NA";

            }
            SqlCommand cmd2 = new SqlCommand("USP_ApplicationMasterInsertUpdate");
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.Add("@P_SERVICEID", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["FormId"].ToString());
            cmd2.Parameters.Add("@P_VCH_PROPOSALID", SqlDbType.VarChar, 50).Value = strProposalNo;
            cmd2.Parameters.Add("@P_VCH_INVESTOR_NAME", SqlDbType.VarChar, 50).Value = InvestorName;
            cmd2.Parameters.Add("@P_VCH_APPLICATION_UNQ_KEY", SqlDbType.VarChar, 50).Value = strUnqId;
            cmd2.Parameters.Add("@INT_CREATEDBY", SqlDbType.VarChar, 50).Value = investorId;
            cmd2.Parameters.Add("@P_CHAR_ACTION", SqlDbType.VarChar, 50).Value = "U";
            cmd2.Parameters.Add("@P_INT_STATUS", SqlDbType.Int).Value = 10;
            cmd2.Parameters.Add("@P_INT_PAYMENT_STATUS", SqlDbType.Int).Value = 3;

            cmd2.Connection = con1;

            cmd2.ExecuteReader();
            con1.Close();
        }
        catch (Exception ex)
        {

            Response.Write("Master Tabele Insert " + ex.Message);
            return;
        }



    }
    public string DecriptData(string strEncData)
    {
        string strDecryptData = objCrypt.AESDecrypt(strEncData);
        return strDecryptData;
    }

    public string GloblMappingValue(string tablename, string columnname, string proposalid)
    {
        string varField = "";
        if (proposalid != "")
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Usp_GlobalMaping"))
                {
                    List<ListItem> customers = new List<ListItem>();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@PVCH_TABLENAME", SqlDbType.VarChar, 50).Value = tablename;
                    cmd.Parameters.Add("@PVCH_COLUMNNAME", SqlDbType.VarChar, 50).Value = columnname;
                    cmd.Parameters.Add("@PVCH_PROPOSALID", SqlDbType.VarChar, 50).Value = proposalid;
                    cmd.Parameters.Add("@PMSG_OUT", SqlDbType.VarChar, 500);
                    cmd.Parameters["@PMSG_OUT"].Direction = ParameterDirection.Output;
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    varField = cmd.Parameters["@PMSG_OUT"].Value.ToString();
                    if (varField == "NA")
                    {
                        varField = "";
                    }
                    else
                    {
                        varField = cmd.Parameters["@PMSG_OUT"].Value.ToString();
                    }

                    con.Close();
                }
            }
        }
        else
        {
            varField = "";
        }
        return varField;
    }
    [WebMethod]
    public static List<ListItem> FillDemographyData(string query)
    {
        //string Qury = "SELECT " + firstClmn + " AS COLUMN_NAME_VALUE," + secondndClmn + " AS COLUMN_NAME_TEXT FROM " + tablename + " where ";
        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        customers.Add(new ListItem
                        {
                            Value = sdr["COLUMN_NAME_VALUE"].ToString(),
                            Text = sdr["COLUMN_NAME_TEXT"].ToString()
                        });
                    }
                }
                con.Close();
                return customers;
            }
        }
    }
    [WebMethod]
    public static string FormToPealMappingWithValidateProposal(string query, string ProposalId)
    {
        int ProsNoCnt = 0;
        string queryCnt = "SELECT COUNT(vchProposalNo) FROM T_PEAL_PROMOTER WHERE vchProposalNo='" + ProposalId + "'";
        string strOut = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                ProsNoCnt = Convert.ToInt32(cmd1.ExecuteScalar().ToString());
                con.Close();
            }
        }

        if (ProsNoCnt > 0)
        {
            using (SqlConnection con = new SqlConnection(connectionString)) 
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    List<ListItem> customers = new List<ListItem>();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    strOut = cmd.ExecuteScalar().ToString();
                    con.Close();
                }
            }
        }
        else
        {
            strOut = "";
        }
        return strOut;
    }
    [WebMethod]
    public static string FormToPealMapping(string query)
    {
        string strOut = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                strOut = cmd.ExecuteScalar().ToString();
            }
        }
        return strOut;
    }
    public void ServiceAmount(int serviceid)
    {
        DataTable AmntTbl = new DataTable();
        string strAmount = "";
        string strInsert = "select isnull(NUM_PAYMENT_AMOUNT,'0.00') as Amnt,VCH_ACCOUNTHEAD from M_SERVICEMASTER_TBL where INT_SERVICEID=" + serviceid + "";
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand(strInsert, con);
        AmntTbl.Load(cmd.ExecuteReader());
        if (AmntTbl.Rows.Count > 0)
        {
            strAmount = AmntTbl.Rows[0]["Amnt"].ToString();
            hdnTotalAmount.Value = AmntTbl.Rows[0]["Amnt"].ToString();
            hdnTotalAmount1.Value = AmntTbl.Rows[0]["Amnt"].ToString();
            hdnAccountHead.Value = AmntTbl.Rows[0]["VCH_ACCOUNTHEAD"].ToString();
        }
        con.Close();
        lblAmount.Text = "<div class='sectionPanel'><h2 class='text-left'>Payment Details</h2><div class='col-sm-12'><table class='table table-bordered'><tr><th width='50%'>Total Amount </th><td width='50%'><b>" + strAmount + "/-</b></td></tr></table></div><div class='clearfix'></div></div>";

    }
    [WebMethod]
    public static string ForSpecialCondionStringReturn(string query)
    {
        string strOut = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                strOut = cmd.ExecuteScalar().ToString();
                con.Close();
                return strOut;
            }
        }
    }
    [WebMethod]
    public static string Form34_Calculation(string intPerson, string intPower)
    {
        string varField = "";
        decimal power = Math.Round(Convert.ToDecimal(intPower), MidpointRounding.ToEven);
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("USP_FORM_34_CALCULATION"))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@P_INTAMOUNT", SqlDbType.Int).Value = Convert.ToInt32(power);
                cmd.Parameters.Add("@P_INTPERSON", SqlDbType.Int).Value = Convert.ToInt32(intPerson);
                cmd.Parameters.Add("@P_OUT_MSG", SqlDbType.VarChar, 50);
                cmd.Parameters["@P_OUT_MSG"].Direction = ParameterDirection.Output;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                varField = cmd.Parameters["@P_OUT_MSG"].Value.ToString();
                con.Close();
            }
        }
        return varField;
    }
    [WebMethod]
    public static string GetWeightCalculation(string query)
    {
        string strOut = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {

                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                strOut = cmd.ExecuteScalar().ToString();
                con.Close();
                return strOut;
            }
        }
    }
    [WebMethod]
    public static string GetEncriptCode(string strData)
    {
        CommonFunctions objCrypt1 = new CommonFunctions();
        string strEnc = objCrypt1.AESEncrypt(strData);
        return strEnc;
    }

    public string AccountHeadWithAmount(int intServiceId, string strAmount, string strUnqId)
    {
        string transactionDetail = "", transactionDetail1 = "";
        string FAccountHead = "";
        string AccountHead = "";
        string strQuery = "SELECT  dbo.M_SWP_SERVICEACCOUNT.vchServiceName, dbo.M_SWP_SERVICEACCOUNT.intServiceid,M_SWP_SERVICEACCOUNT.vchAccountHead from M_SWP_SERVICEACCOUNT   where intServiceid= " + intServiceId;
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        SqlCommand cmd = new SqlCommand(strQuery, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string Description = "NA";
            string Amount = "0";
            if (System.Configuration.ConfigurationManager.AppSettings["ActualAmount"].ToString() == "Yes")
            {
                Amount = strAmount;
            }
            else
            {
                Amount = "1";
            }
            if (intServiceId.ToString() == "48")
            {
                if (strAmount == "50")
                {
                    transactionDetail = "(0030-03-800-0097-01076-000," + "	Application Fee and Notice Fee" + ",30!~!" + "0029-00-800-0097-01077-000,Conversion fee,20)";
                }
                else
                {
                    transactionDetail = "(0030-03-800-0097-01076-000," + "	Application Fee and Notice Fee" + ",30)";
                }
            }
            else
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    AccountHead = AccountHead + ds.Tables[0].Rows[i]["vchAccountHead"].ToString() + ",";
                }
                FAccountHead = AccountHead.ToString().Remove(AccountHead.Length - 1);
                if (FAccountHead.Contains(','))
                {
                    string[] xx = FAccountHead.Split(',');
                    if (xx[1].ToString() != "")
                    {
                        for (int j = 0; j < xx.Length; j++)
                        {
                            transactionDetail1 = transactionDetail1 + xx[j] + "," + Description + "," + Amount + "!~!";
                        }


                    }
                    transactionDetail = "(" + transactionDetail1.Remove(transactionDetail1.Length - 3) + ")";
                }
                else
                {
                    if (intServiceId.ToString() == "16")
                    {
                        string AcntHd = PowerConnectionAccountHead(strUnqId);//"0852-80-800-0234-02233-000";//
                        transactionDetail = "(" + AcntHd + "," + Description + "," + Amount + "!~!0875-60-800-0097-02241-000,User Fee,1)";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + transactionDetail + "')", true);
                    }
                    else
                    {
                        transactionDetail = "(" + FAccountHead + "," + Description + "," + Amount + ")";
                    }
                }
            }
        }
        return transactionDetail;
    }
    public string PowerConnectionAccountHead(string strUnqId)
    {
        string strUtility = "";
        string strAccountHd = "";
        string strInsert1 = " select Utility from table_16 where VCH_APPLICATION_UNQ_KEY='" + strUnqId + "'";
        if (strInsert1 != "" && strInsert1 != null)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(strInsert1, con);
            strUtility = cmd.ExecuteScalar().ToString();
            con.Close();
        }
        if (strUtility != "" && strUtility != null)
        {
            if (strUtility == "NESCO Utility")
            {
                strAccountHd = "0000-00-000-0000-00000-004";
            }
            else if (strUtility == "SOUTHCO Utility")
            {
                strAccountHd = "0000-00-000-0000-00000-006";
            }
            else if (strUtility == "WESCO Utility")
            {
                strAccountHd = "0000-00-000-0000-00000-005";
            }
            else if (strUtility == "CESU Utility")
            {
                strAccountHd = "0000-00-000-0000-00000-003";
            }
        }

        return strAccountHd;
    }


    //// Multiple Services
    public void CreateNavigationMenu()
    {

        if (Session["SvcMasterData"] != null)
        {
            //DataTable dt = (DataTable)Session["SvcMasterData"];
            //string strUrl = "";

            //if (dt.Rows.Count > 0)
            //{
                //StringBuilder sbMenu = new StringBuilder();
                //sbMenu.Append("<ul class='nav nav-tabs'>");

                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    string strServiceId = Convert.ToString(dt.Rows[i]["intServiceId"]);
                //    string strFormName = Convert.ToString(dt.Rows[i]["vchFormName"]);
                //    string strServiceName = Convert.ToString(dt.Rows[i]["vchServiceName"]);
                //    int intServiceType = Convert.ToInt32(dt.Rows[i]["intServiceType"]);
                //    string strProposalNo = Convert.ToString(dt.Rows[i]["vchProposalNo"]);
                //    decimal decAmount = Convert.ToDecimal(dt.Rows[i]["decAmount"]);
                //    int intCompletedStatus = Convert.ToInt32(dt.Rows[i]["intCompletedStatus"]);
                //    string strApplicationKey = Convert.ToString(dt.Rows[i]["vchApplicationKey"]);

                //    //// URL Formation
                //    if (intCompletedStatus == 1)
                //    {
                //        strUrl = "FormEditView1.aspx?FormId=" + strServiceId + "&AppKey=" + strApplicationKey + "&ProposalNo=" + strProposalNo + "&ReqMode=" + Request.QueryString["ReqMode"].ToString();
                //    }
                //    else
                //    {
                //        strUrl = "FormView1.aspx?FormId=" + strServiceId + "&ProposalNo=" + strProposalNo + "&ReqMode=" + Request.QueryString["ReqMode"].ToString();
                //    }

                //    /*--------------------------------------------------------------------------------*/

                //    if (Request.QueryString["FormId"].ToString().Trim() == strServiceId)
                //    {
                //        sbMenu.Append("<li class='active' data-tooltip=\"" + strServiceName + "\"><a href=\"" + strUrl + "\">" + "" + strFormName + "" + "</a></li>");
                //    }
                //    else
                //    {
                //        sbMenu.Append("<li data-tooltip=\"" + strServiceName + "\"><a href=\"" + strUrl + "\">" + "" + strFormName + "" + "</a></li>");
                //    }
                //}
                //sbMenu.Append("</ul>");

                //myNavbar.InnerHtml = sbMenu.ToString();
                //myNavbar.Visible = true;
            //}
        }
    }
    public void CreateNavigationMenuAfterDraft(string strApplicationKey, string Amount)
    {
        if (Session["SvcMasterData"] != null)
        {
           // int SlNo = 0;

            DataTable dt = (DataTable)Session["SvcMasterData"];

            DataRow drupdate = dt.AsEnumerable().Where(r => ((string)r["intServiceId"]).Equals(Request.QueryString["FormId"].ToString().Trim())).First();
            drupdate["intCompletedStatus"] = 1;
            drupdate["vchApplicationKey"] = strApplicationKey;
            drupdate["decAmount"] = Amount;
            drupdate["vchUrl"] = "FormEditView1.aspx?FormId=" + drupdate["intServiceId"] + "&AppKey=" + drupdate["vchApplicationKey"] + "&ProposalNo=" + drupdate["vchProposalNo"] + "&ReqMode=" + Request.QueryString["ReqMode"].ToString();
            drupdate["vchUpdateUrl"] = "FormEditView1.aspx?FormId=" + drupdate["intServiceId"] + "&AppKey=" + drupdate["vchApplicationKey"] + "&ProposalNo=" + drupdate["vchProposalNo"] + "&ReqMode=" + Request.QueryString["ReqMode"].ToString();

            Response.Redirect("FormEditView1.aspx?FormId=" + drupdate["intServiceId"] + "&AppKey=" + drupdate["vchApplicationKey"] + "&ProposalNo=" + drupdate["vchProposalNo"] + "&ReqMode=" + Request.QueryString["ReqMode"].ToString());

            //SlNo = Convert.ToInt32(drupdate["intSlNo"]);

            //int Count = dt.Rows.Count;

            //DataRow[] results = dt.Select("intCompletedStatus <> 1").OrderBy(u => u["intSlNo"]).ToArray();

            //if (results.Length == 0)
            //{
            //    //Consolidate Page
            //    // Response.Redirect("ApplicationConsolidate.aspx");
            //    Response.Redirect("FormEditView1.aspx?FormId=" + Request.QueryString["FormId"].ToString() + "&AppKey=" + strUnqId + "&ProposalNo=" + Request.QueryString["ProposalNo"].ToString() + "&ReqMode=" + Request.QueryString["ReqMode"].ToString());

            //}
            //else
            //{
            //    if (SlNo == Count)
            //    {
            //        //Consolidate Page
            //       // Response.Redirect("ApplicationConsolidate.aspx");
            //        Response.Redirect("FormEditView1.aspx?FormId=" + Request.QueryString["FormId"].ToString() + "&AppKey=" + strUnqId + "&ProposalNo=" + Request.QueryString["ProposalNo"].ToString() + "&ReqMode=" + Request.QueryString["ReqMode"].ToString());

            //    }
            //    else
            //    {
            //        DataRow dr = dt.AsEnumerable().Where(r => ((int)r["intSlNo"]).Equals((SlNo + 1))).First();
            //        Response.Redirect("FormEditView1.aspx?FormId=" + Request.QueryString["FormId"].ToString() + "&AppKey=" + strUnqId + "&ProposalNo=" + Request.QueryString["ProposalNo"].ToString() + "&ReqMode=" + Request.QueryString["ReqMode"].ToString());

            //    }
            //}
        }
    }
    //// Multiple Services
    ///
    private string GETGROUPID()
    {
        DataTable dt = new DataTable();
        string EncryptValue = "";

        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            try
            {

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "USP_GET_GROUP_ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@VCH_ACTION", "U");

                SqlParameter par;
                par = cmd.Parameters.Add("@P_VCHOUT", SqlDbType.VarChar, 100);
                par.Direction = System.Data.ParameterDirection.Output;
                cmd.Connection = conn;
                cmd.ExecuteReader();

                conn.Close();
                EncryptValue = (string)cmd.Parameters["@P_VCHOUT"].Value;

            }
            catch (Exception ex)
            {
                Util.LogError(ex, "GETGROUPID");
            }
            finally
            {
                cmd = null;
                conn.Close();
            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "GETGROUPID");
        }
        return EncryptValue;
    }
}
