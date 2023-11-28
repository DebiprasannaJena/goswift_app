using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Text;
using EntityLayer.Proposal;
using System.ServiceModel;
using BusinessLogicLayer.Proposal;
using System.Web.UI.HtmlControls;
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using Ionic.Zip;
using System.IO;

public partial class ParticularApplicationDetails : SessionCheck
{
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    string WebApplicationPath = System.Configuration.ConfigurationManager.AppSettings["WebApplicationPath"];
    DataTable dt = new DataTable();
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal;

    string FormHeader = "";
    string FormFooter = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["ApplicationNo"] != null && Request.QueryString["ApplicationNo"] != "")
        {
            if (Request.QueryString["ServiceId"] != null)
            {

                FormDetails(Convert.ToInt32(Request.QueryString["ServiceId"].ToString()));
            }
            //frmContent.InnerHtml = DynamicServiceDetails(Request.QueryString["ServiceId"].ToString(), Request.QueryString["ApplicationNo"].ToString());
            //ancPdf.NavigateUrl = "ParticularApplicationDetails.aspx?ApplicationNo=" + Request.QueryString["ApplicationNo"] + "&ServiceId="+Request.QueryString["ServiceId"].ToString()+"";
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('', 'SWP', function () {window.open('../ParticularApplicationDetails.aspx?ApplicationNo=" + Request.QueryString["ApplicationNo"] + "&ServiceId=" + Request.QueryString["ServiceId"].ToString() + "'); location.href = 'ParticularApplicationDetails.aspx?ApplicationNo=" + Request.QueryString["ApplicationNo"] + "&ServiceId="+Request.QueryString["ServiceId"].ToString()"';});  </script>", false);
            FillTransaction();
            FillDataTable();
            //Changes BY MANOJ KUMAR BEHERA 
            GetHeader();
            GetActionDetails();
            //END CHANGES BY MANOJ KUMAR BEHERA
            hplPdf.Target = "_self";
            hplPdf.NavigateUrl = "pdfGenerate.aspx?ApplicationNo=" + Request.QueryString["ApplicationNo"] + "&ServiceId=" + Request.QueryString["ServiceId"].ToString() + "";
        }

        #region Querydetails
        divA1.Visible = false;
        divQ1.Visible = false;

        divQ2.Visible = false;
        divA2.Visible = false;
        divfile2.Visible = false;
        divfile1.Visible = false;
       // BindData();
        #endregion
    }

    //CHANGES BY MANOJ KUMAR BEHERA BEGIN 
    private void GetHeader()
    {
        DataTable PnlDt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("USP_SERVICE_VIEW"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VCH_APPLICATION_UNQ_KEY", SqlDbType.VarChar).Value = Request.QueryString["ApplicationNo"].ToString();
                    cmd.Parameters.Add("@INT_SERVICEID", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["ServiceId"].ToString());
                    cmd.Parameters.Add("@INT_ID", SqlDbType.Int).Value = Convert.ToInt32(0);
                    cmd.Parameters.Add("@VCH_ACTION", SqlDbType.VarChar).Value = "VIEW";
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }

                if (PnlDt.Rows.Count > 0)
                {
                    lblapplication.InnerHtml = ":" + PnlDt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString();
                    lblapplieddate.InnerHtml = ":" + PnlDt.Rows[0]["DATE"].ToString();
                }
                else
                {
                    lblapplication.InnerHtml = "";
                    lblapplieddate.InnerHtml = "";
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }
    }
    private void GetActionDetails()
    {
        DataTable PnlDt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("USP_SERVICE_VIEW"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VCH_APPLICATION_UNQ_KEY", SqlDbType.VarChar).Value = Request.QueryString["ApplicationNo"].ToString();
                    cmd.Parameters.Add("@INT_SERVICEID", SqlDbType.Int).Value = Convert.ToInt32(Request.QueryString["ServiceId"].ToString());
                    cmd.Parameters.Add("@INT_ID", SqlDbType.Int).Value = Convert.ToInt32(0);
                    cmd.Parameters.Add("@VCH_ACTION", SqlDbType.VarChar).Value = "VIEWDOC";
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }

                if (PnlDt.Rows.Count > 0)
                {
                    gvapplication.DataSource = PnlDt;
                    gvapplication.DataBind();
                    status.Visible = true;
                }
                else
                {
                    gvapplication.DataSource = null;
                    gvapplication.DataBind();
                    status.Visible = false;
                }
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }
    }
    // END OF CHANGES MANOJ KUMAR BEHERA

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
            if (Convert.ToInt32(Request.QueryString["ServiceId"].ToString()) == 16)
            {
                string headerText = DeptNameOfEnergy(Request.QueryString["ApplicationNo"].ToString());
                string EnergyHaederText = "<h2>" + headerText + "</h2><p>Govermnent of Odisha</p><p>Power Connection Application</p>";
                divHeader.InnerHtml = EnergyHaederText.ToString();
            }
            else
            {
                divHeader.InnerHtml = ServiceDtl.Rows[0]["NVCH_HEADERTEXT"].ToString();
            }

            FormFooter = ServiceDtl.Rows[0]["NVCH_FOOTERTEXT"].ToString();
        }         
    }
    public string DynamicServiceDetails(string ServiceId, string unqueKey)
    {
        string ColumnName1 = "";
        string ColumnValue1 = "";

        string strLabelName = "";
        string strLabelText = "";


        string strHtml = "";
        string strText = "";
        int counter = 0;

        string strRow = "";
        SqlConnection connection = new SqlConnection(connectionString);
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("USP_DynamicFormDetails"))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@P_SERVICEID", SqlDbType.Int, 10).Value = ServiceId;
                cmd.Parameters.Add("@P_VCH_APPLICATION_UNQ_KEY", SqlDbType.VarChar, 50).Value = unqueKey;
                cmd.Connection = con;
                con.Open();
                dt.Load(cmd.ExecuteReader());
                con.Close();
            }
        }

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i]["LABEL"] != null)
            {
                strLabelName = dt.Rows[i]["LABEL"].ToString();
            }
            else
            {
                strLabelName = "";
            }

            if (dt.Rows[i]["val"] != null)
            {
                if (dt.Rows[i]["type"] != null)
                {
                    if (dt.Rows[i]["type"].ToString() == "file")
                    {
                        if (dt.Rows[i]["val"].ToString() != "" && dt.Rows[i]["val"].ToString() != null)
                        {
                            string imgUrl = "";
                            if (dt.Rows[i]["val"].ToString().IndexOf("/") > 0)
                            {
                                imgUrl = dt.Rows[i]["val"].ToString();
                            }
                            else { imgUrl = WebApplicationPath + "Portal/Document/Upload/" + dt.Rows[i]["val"].ToString(); }

                            strLabelText = "<a href='" + imgUrl + "'>File</a>";
                        }
                        else
                        {
                            strLabelText = "NA";
                        }
                    }
                    else if (dt.Rows[i]["type"].ToString() == "Plugin")
                    {
                        if (dt.Rows[i]["LABEL"].ToString() != "")
                        {
                            strLabelName = dt.Rows[i]["LABEL"].ToString();
                            strLabelText = DynamicTableCreation(dt.Rows[i]["val"].ToString());
                        }
                    }
                    else
                    {
                        strLabelText = dt.Rows[i]["val"].ToString();
                    }
                }
            }
            else
            {
                strLabelText = "";
            }

            string strGroupDiv = "<div class='form-group'><div class='row'>";

            if (dt.Rows[i]["type"].ToString() == "Plugin")
            {
                strHtml = strHtml + "<div class='form-group'><div class='row'><label for='sss' class='col-sm-3'>" + strLabelName + "</label><div class='col-sm-8'><span class='colon'>:</span><div class='table-responsive'>" + strLabelText + "</div></div>" + "</div></div>";
            }
            else if (dt.Rows[i]["type"].ToString() != "Declaration")
            {
                if (dt.Rows[i]["type"].ToString() != "SameAs")
                {
                    strText = "<label for='sss' class='col-sm-3'>" + strLabelName + "</label><div class='col-sm-8'><span class='colon'>:</span><p>" + strLabelText + "</p></div>";
                    strRow = strText;

                    strHtml = strHtml + strGroupDiv + strRow + "</div>";
                }
            }
        }
        return strHtml;
    }
    public string DynamicTableCreation(string data)
    {
        string[] FilTyp = { ".jpg", ".png", ".pdf", "JPG", ".PNG", ".PDF" };
        string myHtmlFile = "";
        // data = "[{"Nature":"Y","Number":"Y","ConnectedLoad":"YT","TotalConnectedLoad":"YT","Remark":"TY","ManegerName":"TY","MaximumEmployee":"TY"}]";
        DataTable DynTable = (DataTable)JsonConvert.DeserializeObject(data, (typeof(DataTable)));
        if (DynTable.Rows.Count > 0)
        {

            StringBuilder myBuilder = new StringBuilder();

            myBuilder.Append("<table  ");
            myBuilder.Append("class='table table-bordered '>");

            myBuilder.Append("<tr align='left' valign='top'>");
            foreach (DataColumn myColumn in DynTable.Columns)
            {
                myBuilder.Append("<th align='left' valign='top'>");
                myBuilder.Append(myColumn.ColumnName);
                myBuilder.Append("</th>");
            }

            myBuilder.Append("</tr>");
            foreach (DataRow myRow in DynTable.Rows)
            {
                myBuilder.Append("<tr align='left' valign='top'>");
                foreach (DataColumn myColumn in DynTable.Columns)
                {
                    myBuilder.Append("<td align='left' valign='top'>");
                    string clmnVal = myRow[myColumn.ColumnName].ToString();
                    for (int i = 0; i < Convert.ToInt32(FilTyp.Length); i++)
                    {
                        if (clmnVal.Contains(FilTyp[i]))
                        {
                            clmnVal = WebApplicationPath + "Portal/Document/Upload/" + clmnVal;

                            //Added BY Priti

                            if (clmnVal.IndexOf("/") > 0)
                            {

                            }
                            else { clmnVal = WebApplicationPath + "Portal/Document/Upload/" + clmnVal; }
                            //Added BY Priti
                            clmnVal = "<div class='col-sm-6'><span class='colon'></span><a href='" + clmnVal + "'><i class='fa fa-download'></i></a></div>";

                        }
                    }
                    myBuilder.Append(clmnVal.ToString());
                    myBuilder.Append("</td>");
                }

                myBuilder.Append("</tr>");
            }
            myBuilder.Append("</table>");
            myBuilder.Append("</body>");
            myBuilder.Append("</html>");
            myHtmlFile = myBuilder.ToString();
            return myHtmlFile;

        }
        return "";
    }

    #region Querydetails
    //private void BindData()
    //{
    //    objProposal = new ProposalDet();
    //    try
    //    {
    //        objProposal.strAction = "E";
    //        objProposal.strProposalNo = Request.QueryString["ApplicationNo"].ToString();
    //        objProposal.intNoOfTimes = Convert.ToInt32(Request.QueryString["ServiceId"]);
    //        List<ProposalDet> objProposalList = objService.ServicegetRaisedQueryDetails(objProposal).ToList();
    //        hdnNoofrecord.Value = objProposalList.Count.ToString();
    //        if (objProposalList.Count == 0)
    //        { div1stcnt.Visible = true; }
    //        else if (objProposalList.Count == 2)
    //        { div2ndcnt.Visible = true; }
    //        else
    //        { div1stcnt.Visible = false; div2ndcnt.Visible = false; }

    //        if (objProposalList.Count > 0 && objProposalList.Count <= 2)
    //        {
    //            divA1.Visible = true;
    //            divQ1.Visible = true;
    //            // divQ2.Visible = true;
    //            lblq1.Text = objProposalList[0].strRemarks.ToString();
    //            txtq1.Visible = false;

    //            if (objProposalList.Count > 1)
    //            {
    //                divQ2.Visible = true;
    //                lblq2.Visible = false;

    //                lbla1.Text = objProposalList[1].strRemarks.ToString();
    //                if (objProposalList[1].strFileName.ToString() != "")
    //                {
    //                    string[] strarr = objProposalList[1].strFileName.ToString().Split(',');
    //                    if (strarr[0] != "")
    //                    {
    //                        hlDoc1.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[0];
    //                        pdficon1.Visible = true;
    //                        divfile1.Visible = true;
    //                    }
    //                    if (strarr[1] != "")
    //                    {
    //                        hlDoc2.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[1];
    //                        pdficon2.Visible = true;
    //                        divfile1.Visible = true;
    //                    }
    //                    if (strarr[2] != "")
    //                    {
    //                        hlDoc3.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[2];
    //                        pdficon3.Visible = true;
    //                        divfile1.Visible = true;
    //                    }
    //                }
    //            }
    //            else { lbla1.Visible = false; divA1.Visible = false; btnSubmit.Visible = false; btnCancel.Visible = false; }
    //        }
    //        else if (objProposalList.Count > 2 && objProposalList.Count <= 3)
    //        {
    //            divA1.Visible = true;
    //            divQ1.Visible = true;
    //            txtq1.Visible = false;
    //            txtq2.Visible = false;
    //            divQ2.Visible = true;

    //            btnSubmit.Visible = false;
    //            btnCancel.Visible = false;
    //            lblq1.Text = objProposalList[0].strRemarks.ToString();
    //            lbla1.Text = objProposalList[1].strRemarks.ToString();

    //            lblq2.Text = objProposalList[2].strRemarks.ToString();
    //            //txta2.Text = objProposalList[3].strRemarks.ToString();
    //            if (objProposalList[1].strFileName.ToString() != "")
    //            {
    //                string[] strarr = objProposalList[1].strFileName.ToString().Split(',');
    //                if (strarr[0] != "")
    //                {
    //                    hlDoc1.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[0];
    //                    pdficon1.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //                if (strarr[1] != "")
    //                {
    //                    hlDoc2.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[1];
    //                    pdficon2.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //                if (strarr[2] != "")
    //                {
    //                    hlDoc3.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[2];
    //                    pdficon3.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //            }


    //        }
    //        else if (objProposalList.Count > 2 && objProposalList.Count <= 4)
    //        {

    //            divA1.Visible = true;
    //            divQ1.Visible = true;
    //            txtq1.Visible = false;
    //            txtq2.Visible = false;
    //            divQ2.Visible = true;
    //            divA2.Visible = true;
    //            btnSubmit.Visible = false;
    //            btnCancel.Visible = false;
    //            lblq1.Text = objProposalList[0].strRemarks.ToString();
    //            lbla1.Text = objProposalList[1].strRemarks.ToString();

    //            lblq2.Text = objProposalList[2].strRemarks.ToString();
    //            lbla2.Text = objProposalList[3].strRemarks.ToString();
    //            if (objProposalList[1].strFileName.ToString() != "")
    //            {

    //                string[] strarr = objProposalList[1].strFileName.ToString().Split(',');
    //                if (strarr[0] != "")
    //                {
    //                    hlDoc1.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[0];
    //                    pdficon1.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //                if (strarr[1] != "")
    //                {
    //                    hlDoc2.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[1];
    //                    pdficon2.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //                if (strarr[2] != "")
    //                {
    //                    hlDoc3.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[2];
    //                    pdficon3.Visible = true;
    //                    divfile1.Visible = true;
    //                }
    //            }

    //            else
    //            {
    //                pdficon1.Visible = false;
    //                pdficon2.Visible = false;
    //                pdficon3.Visible = false;
    //            }
    //            if (objProposalList[3].strFileName.ToString() != "")
    //            {

    //                string[] strarr = objProposalList[3].strFileName.ToString().Split(',');
    //                if (strarr[0] != "")
    //                {
    //                    hlDoc4.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[0];
    //                    pdficon4.Visible = true;
    //                    divfile2.Visible = true;
    //                }
    //                if (strarr[1] != "")
    //                {
    //                    hlDoc5.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[1];
    //                    pdficon5.Visible = true;
    //                    divfile2.Visible = true;
    //                }
    //                if (strarr[2] != "")
    //                {
    //                    hlDoc6.NavigateUrl = "../../SWP_Web/QueryFiles/Services/" + strarr[2];
    //                    pdficon6.Visible = true;
    //                    divfile2.Visible = true;
    //                }
    //            }
    //        }
    //        else if (objProposalList.Count == 0)
    //        { divQ1.Visible = true; lblq1.Visible = false; div1stcnt.Visible = true; div2ndcnt.Visible = false; }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        objProposal = null;
    //    }
    //}
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    objProposal = new ProposalDet();

    //    try
    //    {
    //        objProposal.strAction = "Q";
    //        objProposal.intCreatedBy = Convert.ToInt32(Session["UserId"]);
    //        objProposal.intQueryId = Convert.ToInt32(Request.QueryString["ServiceId"].ToString());
    //        objProposal.strProposalNo = Request.QueryString["ApplicationNo"].ToString();
    //        objProposal.intStatus = 5;
    //        if (hdnNoofrecord.Value == "0")
    //        {
    //            objProposal.strRemarks = txtq1.Text;
    //        }
    //        else { objProposal.strRemarks = txtq2.Text; }

    //        string strRetVal = objService.ServiceProposalRaiseQuery(objProposal);

    //        if (strRetVal == "2")
    //        {
    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Saved Successfully.');", true);
    //            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Saved Successfully.');document.location.href='ViewProposal.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "'", false);
    //        }
    //        else if (strRetVal == "4")
    //        { ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Action can not be taken Successfully.');", true); }


    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //    finally
    //    {
    //        objProposal = null;
    //    }
    //}
    #endregion

    protected void btnBack_Click(object sender, EventArgs e)
    {       
        if (Request.QueryString["linkm"] == null)
        {
            Response.Redirect("ApplicationDetails.aspx");
        }
        else
        {
            Response.Redirect("ServiceViewAndTakeAction.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "");
        }
    }

    public void FillDataTable()
    {
        string strHtmls = "";

        DataTable PnlDt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "select * from T_PANELMAPPING_TBL WHERE INT_FORM_ID=" + Convert.ToInt32(Request.QueryString["ServiceId"].ToString());
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
            strHtmls = strHtmls + "<div class='sectionPanel'><h2 id='" + PnlDt.Rows[i1]["INT_PANELID"].ToString() + "'>" + PnlDt.Rows[i1]["VCH_PANETEXT"].ToString() + "</h2><div class='row'>" + GetHTML(Convert.ToInt32(Request.QueryString["ServiceId"].ToString()), Convert.ToInt32(PnlDt.Rows[i1]["INT_PANELID"])) + "</div></div>";
        }

        frmContent.InnerHtml = strHtmls;
    }
    public string GetHTML(int FormId, int PanelID)
    {
        int intAllignment = 0;
        DataTable dt = new DataTable();
        SqlConnection connection = new SqlConnection(connectionString);
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand("USP_DynamicFormDetails_PANELWISE"))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@P_SERVICEID", SqlDbType.Int).Value = Convert.ToInt32(FormId);
                cmd.Parameters.Add("@P_VCH_APPLICATION_UNQ_KEY", SqlDbType.VarChar, 50).Value = Request.QueryString["ApplicationNo"].ToString();
                cmd.Parameters.Add("@P_PANEL_ID", SqlDbType.Int).Value = Convert.ToInt32(PanelID);
                cmd.Connection = con;
                con.Open();
                dt.Load(cmd.ExecuteReader());
                con.Close();
            }
        }

        string strHtml = "";
        string strReq = "1";
        string lebeltext = "";
        string controlText = "";
        string strRow = "";
        string strGroupDiv = "";//";

        int counter = 0;
        for (int k = 0; k < dt.Rows.Count; k++)
        {
            if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() != "Declaration")
            {
                if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() != "SameAs")
                {
                    strRow = "";

                    if (dt.Rows[k]["LABEL"].ToString() != "" && dt.Rows[k]["LABEL"].ToString() != null)
                    {
                        lebeltext = dt.Rows[k]["LABEL"].ToString();
                    }

                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "select" || dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "select-multiple")
                    {
                        if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null && dt.Rows[k]["val"].ToString() != "Select" && dt.Rows[k]["val"].ToString() != "select")
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>" + dt.Rows[k]["val"].ToString() + "</label>";
                        }
                        else
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>NA</label>";
                        }
                    }

                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "DateTime")
                    {
                        if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null)
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>" + dt.Rows[k]["val"].ToString() + "</label>";
                        }
                        else
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>NA</label>";
                        }
                    }

                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "FromToDate")
                    {
                        if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null)
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>" + dt.Rows[k]["val"].ToString() + "</label>";
                        }
                        else
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>NA</label>";
                        }
                    }

                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "FullName")
                    {
                        if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null)
                        {
                            if (dt.Rows[k]["val"].ToString() != "Mr")
                            {
                                controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>" + dt.Rows[k]["val"].ToString() + "</label>";
                            }
                            else
                            {
                                controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>NA</label>";
                            }
                        }
                        else
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>NA</label>";
                        }
                    }

                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "text")
                    {
                        if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null)
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>" + dt.Rows[k]["val"].ToString() + "</label>";
                        }
                        else
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>NA</label>";
                        }
                    }

                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "file")
                    {
                        if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null && dt.Rows[k]["val"].ToString() != "NA")
                        {
                            if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null)
                            {
                                //Added BY Priti
                                string imgUrl = "";
                                if (dt.Rows[k]["val"].ToString().IndexOf("/") > 0)
                                {
                                    imgUrl = dt.Rows[k]["val"].ToString();
                                }
                                else { imgUrl = WebApplicationPath + "Portal/Document/Upload/" + dt.Rows[k]["val"].ToString(); }
                                //Added BY Priti
                                //string imgUrl = WebApplicationPath + "Portal/Document/Upload/" + dt.Rows[k]["val"].ToString();
                                controlText = "<div class='col-sm-6'><span class='colon'>:</span><a href='" + imgUrl + "' target='_blank'><i class='fa fa-download'></i></a></div>";
                            }
                            else
                            {
                                controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>NA</label>";
                            }
                            // controlText = "<label for='sss' class='col-sm-3'>" + dt.Rows[k]["val"].ToString() + "</label>";
                        }
                        else
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>NA</label>";
                        }
                    }

                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "radio")
                    {
                        if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null)
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>" + dt.Rows[k]["val"].ToString() + "</label>";
                        }
                        else
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>NA</label>";
                        }
                    }

                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "checkbox")
                    {
                        if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null)
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>" + dt.Rows[k]["val"].ToString() + "</label>";
                        }
                        else
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>NA</label>";
                        }
                    }

                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "LatLong") //// Added by Sushant Jena On Dt:-28-Jul-2020
                    {
                        if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null)
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>" + dt.Rows[k]["val"].ToString() + "</label>";
                        }
                        else
                        {
                            controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>NA</label>";
                        }
                    }

                    strRow = strRow + LayOut(intAllignment, lebeltext, controlText, strReq, "1");

                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "Label")
                    {
                        //strHtml = strHtml + "<div class='col-sm-12'><h3>" + dt.Rows[k]["PVCH_LABEL_NAME"].ToString() + "</h3></div> <div class='clearfix'></div>";// +"</div></div>";
                    }

                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "Plugin")
                    {
                        string strLabelName = dt.Rows[k]["LABEL"].ToString();
                        if (dt.Rows[k]["val"].ToString() != "NA")
                        {
                            string strLabelText = DynamicTableCreation(dt.Rows[k]["val"].ToString());
                            strHtml = strHtml + "<div class='form-group padding-lr10'><label for='sss' class='col-sm-12'>" + strLabelName + "</label><div class='col-sm-12'><div class='table-responsive'>" + strLabelText + "</div></div>" + "<div class='clearfix'></div></div>";
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
                        }
                    }
                }
            }
        }

        return strHtml;

        //divLogo.InnerText = FormLogo;
    }
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
                    strText = "<div class='col-sm-4'><label for='sss'>" + lblText + "</label>" + controls + "</div>";
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
                    strText = "<label for='sss' class='col-sm-2' id='lbl_" + id + "'>" + lblText + " </label><div class='col-sm-4'><span class='colon'>:</span>" + controls + "</div>";
                }
                else
                {
                    strText = "<label for='sss' class='col-sm-2' id='lbl_" + id + "'>" + lblText + " </label><div class='col-sm-4'><span class='colon'>:</span>" + controls + "</div>";
                }
            }
            else if (AllignmentType == 1)
            {
                if (isRequired == "1")
                {
                    strText = "<div class='' id='div_" + id + "'><div class='col-sm-6 margin-bottom8'><label for='sss' class='col-sm-5' id='lbl_" + id + "'>" + lblText + "</label><div class='col-sm-7'><span class='colon'>:</span>" + controls + "</div></div></div>";
                }
                else
                {
                    strText = "<div class='' id='div_" + id + "'><div class='col-sm-6 margin-bottom8'><label for='sss' class='col-sm-5' id='lbl_" + id + "'>" + lblText + "</label><div class='col-sm-7'><span class='colon'>:</span>" + controls + "</div></div></div>";
                }
            }
            else
            {
                if (isRequired == "1")
                {
                    strText = "<div class='col-sm-6 '><label for='sss' class='col-sm-6'>" + lblText + "</label>" + controls + "</div> ";
                }
                else
                {
                    strText = "<div class='col-sm-6'><label for='sss'>" + lblText + "</label>" + controls + "</div>";
                }
            }
        }
        else
        {
            strText = "<div class='col-sm-12'>" + controls + "</div>";
        }

        return strText;
    }

    #endregion
    public string DeptNameOfEnergy(string applicationkey)
    {
        //int intInvestorId = Convert.ToInt32(Session["InvestorId"]);
        string queryCnt = "select Utility from table_16 where VCH_APPLICATION_UNQ_KEY='" + applicationkey + "'";
        string strFormId = "";
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                strFormId = cmd1.ExecuteScalar().ToString();
                con.Close();
            }
        }
        return strFormId;
    }
    public void FillTransaction()
    {
        #region TransactionDetail

        //This region of code is to show transactionDetail//
        //HtmlGenericControl OrderList = (HtmlGenericControl)e.Row.FindControl("OrderList");
        //HtmlGenericControl OrderList1 = (HtmlGenericControl)e.Row.FindControl("OrderList1");
        List<ServiceDetails> objOrderList = new List<ServiceDetails>();
        ServiceDetails objProp1 = new ServiceDetails();
        objProp1.STRACTION = "D";
        objProp1.strApplicationUnqKey = Request.QueryString["ApplicationNo"].ToString();
        //PaymentOrderDetails(ServiceDetails objService)
        ServiceBusinessLayer objService1 = new ServiceBusinessLayer();
        objOrderList = objService1.PaymentOrderDetails(objProp1).ToList();
        if (objOrderList.Count > 0)
        {
            DataTable dt = CommonHelperCls.ConvertToDataTable<ServiceDetails>(objOrderList);
            DataView dv = new DataView(dt);
            dv.RowFilter = "strStatus = 'Success'";

            string strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th> Date</th><th>Order No.</th><th>Amount</th></tr>";
            Decimal TotalAmt = 0;
            for (int i = 0; i < dv.ToTable().Rows.Count; i++)
            {
                strHTMlQuery = strHTMlQuery + "<tr><td>" + dv.ToTable().Rows[i]["dtmCreatedOn"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchOrderNo"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchAmount"].ToString() + "</td></tr>";
                TotalAmt = TotalAmt + Convert.ToDecimal(dv.ToTable().Rows[i]["vchAmount"]);
            }
            strHTMlQuery = strHTMlQuery + "<tr><td></td><td>Total</td><td>" + TotalAmt + "</td></tr></table>";
            OrderList.InnerHtml = strHTMlQuery;

            dv = new DataView(dt);
            dv.RowFilter = "strStatus = 'Pending'";
            TotalAmt = 0;
            strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th> Date</th><th>Order No.</th><th>Amount</th></tr>";
            for (int i = 0; i < dv.ToTable().Rows.Count; i++)
            {
                strHTMlQuery = strHTMlQuery + "<tr><td>" + dv.ToTable().Rows[i]["dtmCreatedOn"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchOrderNo"].ToString() + "</td><td>" + dv.ToTable().Rows[i]["vchAmount"].ToString() + "</td></tr>";
                TotalAmt = TotalAmt + Convert.ToDecimal(dv.ToTable().Rows[i]["vchAmount"]);
            }
            strHTMlQuery = strHTMlQuery + "<tr><td></td><td>Total</td><td>" + TotalAmt + "</td></tr></table>";
            OrderList1.InnerHtml = strHTMlQuery;
            // QueryHist1.InnerHtml = strHTMlQuery;
        }
        else
        {
            OrderList.InnerHtml = "<table><tr><td>No successfull transaction   done.</td></tr></table>";
            OrderList1.InnerHtml = "<table><tr><td>No failure transaction  done.</td></tr></table>";
        }

        #endregion
    }
    protected void gvapplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {           
            Label lblapproval = (Label)e.Row.FindControl("lblapproval");
            Label lbldoc = (Label)e.Row.FindControl("lbldoc");
            Label lblnoc = (Label)e.Row.FindControl("lblnoc");
            Label lblins = (Label)e.Row.FindControl("lblins");
            Label lblres = (Label)e.Row.FindControl("lblres");

            if (lblapproval.Text == "False")
            {
                gvapplication.Columns[4].Visible = false;
            }
            if (lbldoc.Text == "False")
            {
                gvapplication.Columns[5].Visible = false;
            }
            if (lblnoc.Text == "False")
            {
                gvapplication.Columns[6].Visible = false;
            }
            if (lblins.Text == "False")
            {
                gvapplication.Columns[7].Visible = false;
            }
            if (lblres.Text == "False")
            {
                gvapplication.Columns[8].Visible = false;
            }
        }
    }

    protected void gvapplication_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable PnlDt = new DataTable();
        LinkButton lnkBtn = (LinkButton)e.CommandSource;    // the button
        GridViewRow myRow = (GridViewRow)lnkBtn.Parent.Parent;  // the row
        GridView myGrid = (GridView)sender; // the gridview       
        string ApplicationKey = gvapplication.DataKeys[myRow.RowIndex].Values[0].ToString();
        string ServiceId = gvapplication.DataKeys[myRow.RowIndex].Values[1].ToString();
        string IntId = gvapplication.DataKeys[myRow.RowIndex].Values[2].ToString();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("USP_SERVICE_VIEW"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@VCH_APPLICATION_UNQ_KEY", SqlDbType.VarChar).Value = ApplicationKey;
                    cmd.Parameters.Add("@INT_SERVICEID", SqlDbType.Int).Value = Convert.ToInt32(ServiceId);
                    cmd.Parameters.Add("@INT_ID", SqlDbType.Int).Value = Convert.ToInt32(IntId);
                    if (e.CommandName == "Approvedfile")
                    {
                        cmd.Parameters.Add("@VCH_TYPE", SqlDbType.VarChar).Value = "AF";
                    }
                    else if (e.CommandName == "Filename")
                    {
                        cmd.Parameters.Add("@VCH_TYPE", SqlDbType.VarChar).Value = "FN";
                    }
                    else if (e.CommandName == "Nocfile")
                    {
                        cmd.Parameters.Add("@VCH_TYPE", SqlDbType.VarChar).Value = "NF";
                    }
                    else if (e.CommandName == "Inspectionfile")
                    {
                        cmd.Parameters.Add("@VCH_TYPE", SqlDbType.VarChar).Value = "IF";
                    }
                    else if (e.CommandName == "Restorationfile")
                    {
                        cmd.Parameters.Add("@VCH_TYPE", SqlDbType.VarChar).Value = "RF";
                    }
                    cmd.Parameters.Add("@VCH_ACTION", SqlDbType.VarChar).Value = "DOC";
                    cmd.Connection = con;
                    con.Open();
                    PnlDt.Load(cmd.ExecuteReader());
                    con.Close();
                }
                if (PnlDt.Rows.Count > 0)
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                        zip.AddDirectoryByName("ApprovalDocs");
                        if (PnlDt.Rows[0]["vchFileName"].ToString() != "")
                        {
                            string[] arrFileName = PnlDt.Rows[0]["vchFileName"].ToString().Split(',');
                            for (int i = 0; i <= arrFileName.Count() - 1; i++)
                            {
                                string FileName = "~/Portal/ApprovalDocs/" + Convert.ToString(arrFileName[i]);
                                string filePath = Server.MapPath(FileName);
                                if (File.Exists(filePath))
                                {
                                    zip.AddFile(filePath, "ApprovalDocs");
                                }
                            }
                        }
                        Response.Clear();
                        Response.BufferOutput = false;
                        string zipName = String.Format("ApprovalDocs_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                        zip.Save(Response.OutputStream);
                        Response.End();
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                con.Close();

            }
        }
    }
}