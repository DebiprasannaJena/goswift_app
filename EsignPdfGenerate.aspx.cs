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
using System.IO;
using System.Configuration;
using System.Text;
using System.Net;
using SelectPdf;
using System.Drawing;
using System.Threading;
using System.Globalization;

public partial class EsignPdfGenerate : System.Web.UI.Page
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
        //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
        Session["Esign"] = "YES";
        //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
        if (Request.QueryString["ApplicationKey"] != null && Request.QueryString["ApplicationKey"] != "")
        {
            if (Request.QueryString["ServiceId"] != null)
            {

                FormDetails(Convert.ToInt32(Request.QueryString["ServiceId"].ToString()));
            }
            //frmContent.InnerHtml = DynamicServiceDetails(Request.QueryString["ServiceId"].ToString(), Request.QueryString["ApplicationNo"].ToString());
            FillDataTable();
        }
        #region Querydetails
        //divA1.Visible = false;
        //divQ1.Visible = false;

        //divQ2.Visible = false;
        //divA2.Visible = false;
        //divfile2.Visible = false;
        //divfile1.Visible = false;
        // BindData();
        #endregion
    }
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

            divHeader.InnerHtml = ServiceDtl.Rows[0]["NVCH_HEADERTEXT"].ToString();

            FormFooter = ServiceDtl.Rows[0]["NVCH_FOOTERTEXT"].ToString();


        }
        // <img src=" + imgUrl + " alt='logo'/>
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
                            string imgUrl = WebApplicationPath + "../../Portal/Document/Upload/" + dt.Rows[i]["val"].ToString();
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
                //    counter = counter + 1;
                //    if (counter > 1)
                //    {
                //        if ((2 % counter) == 0)
                //        {
                //            strHtml = strHtml + strRow + "</div></div>";
                //            counter = 0;
                //        }
                //        else
                //        {
                //            strHtml = strHtml + strRow;
                //        }
                //    }
                //    else
                //    {
                //        strHtml = strHtml + strGroupDiv + strRow;
                //        // strHtml = strHtml + strRow;
                //    }
            }
        }
        return strHtml;


        //foreach (DataColumn column in dt.Columns)
        //{
        //    if (column.ColumnName != "TYPE")
        //    {
        //        //if (column.ColumnName=="VCH_APPLICATION_UNQ_KEY")
        //        ColumnName1 = (column.ColumnName == "VCH_APPLICATION_UNQ_KEY" ? "ApplicationNo." : column.ColumnName);

        //        if (dt.Rows[0][column.ColumnName] != null && dt.Rows[0][column.ColumnName] != "")
        //        {
        //            ColumnValue1 = dt.Rows[0][column.ColumnName].ToString();
        //        }
        //        else
        //        {
        //            ColumnValue1 = "NA";
        //        }
        //    }
        // ColumnName1
        //ColumnName1 = column.ColumnName;
        //ColumnValue1 = dt.Rows[0][ColumnName1].ToString();


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
                            clmnVal = "<div class='col-sm-6'><span class='colon'></span><i class='fa fa-download'></i></div>";

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
    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    // 
    //    if (Request.QueryString["linkm"] == null)
    //    {
    //        Response.Redirect("ApplicationDetails.aspx");
    //    }
    //    else
    //    {
    //        Response.Redirect("ServiceViewAndTakeAction.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "");
    //    }

    //}

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


        //hdnPluginJson.Value = hdnPluginJson.Value.TrimEnd(',');
        //StringWriter myWriter = new StringWriter();
        //HttpUtility.HtmlDecode(FormHeader, myWriter);
        //string strTex = myWriter.ToString();
        //divHeaderId.InnerHtml = strTex;
        //StringWriter myWriter1 = new StringWriter();
        //HttpUtility.HtmlDecode(FormFooter, myWriter1);
        //string strTex1 = myWriter1.ToString();
        //divFooterId.InnerHtml = strTex1;
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
                cmd.Parameters.Add("@P_SERVICEID", SqlDbType.Int, 10).Value = Convert.ToInt32(FormId);
                cmd.Parameters.Add("@P_VCH_APPLICATION_UNQ_KEY", SqlDbType.VarChar, 15).Value = Request.QueryString["ApplicationKey"].ToString();
                cmd.Parameters.Add("@P_PANEL_ID", SqlDbType.Int, 10).Value = Convert.ToInt32(PanelID);
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

                    string strClass = "";

                    if (dt.Rows[k]["LABEL"].ToString() != "" && dt.Rows[k]["LABEL"].ToString() != null)
                    {

                        lebeltext = dt.Rows[k]["LABEL"].ToString();
                    }
                    else
                    {

                    }
                    if (dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "select" || dt.Rows[k]["PVCH_CONTROL_TYPE"].ToString() == "select-multiple")
                    {
                        if (dt.Rows[k]["val"].ToString() != "" || dt.Rows[k]["val"].ToString() != null)
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
                        if (dt.Rows[k]["val"].ToString() != "" || dt.Rows[k]["val"].ToString() != null)
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
                        if (dt.Rows[k]["val"].ToString() != "" || dt.Rows[k]["val"].ToString() != null)
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
                        if (dt.Rows[k]["val"].ToString() != "" || dt.Rows[k]["val"].ToString() != null)
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
                        if (dt.Rows[k]["val"].ToString() != "" || dt.Rows[k]["val"].ToString() != null)
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
                        if (dt.Rows[k]["val"].ToString() != "" || dt.Rows[k]["val"].ToString() != null)
                        {
                            if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null)
                            {
                                string imgUrl = WebApplicationPath + "Portal/Document/Upload/" + dt.Rows[k]["val"].ToString();
                                controlText = "<div class='col-sm-6'><span class='colon'>:</span><i class='fa fa-download'></i></div>";
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
                        if (dt.Rows[k]["val"].ToString() != "" || dt.Rows[k]["val"].ToString() != null)
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
                        if (dt.Rows[k]["val"].ToString() != "" || dt.Rows[k]["val"].ToString() != null)
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
                        //if (dt.Rows[k]["LABEL"].ToString() != "")
                        //{
                        string strLabelName = dt.Rows[k]["LABEL"].ToString();
                        string strLabelText = DynamicTableCreation(dt.Rows[k]["val"].ToString());

                        strHtml = strHtml + "<div class='form-group padding-lr10'><label for='sss' class='col-sm-12'>" + strLabelName + "</label><div class='col-sm-12'><div class='table-responsive'>" + strLabelText + "</div></div>" + "<div class='clearfix'></div></div>";

                        //}
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
    protected override void Render(HtmlTextWriter writer)
    {
        try
        {

            string path = Server.MapPath("Document/ServiceEsign/");
            // get html of the page
            TextWriter myWriter = new StringWriter();
            HtmlTextWriter htmlWriter = new HtmlTextWriter(myWriter);
            base.Render(htmlWriter);

            // instantiate a html to pdf converter object
            HtmlToPdf converter = new HtmlToPdf();
            //PdfMargins margin = new PdfMargins(4f, 4f, 4f, 4f);

            converter.Options.MarginLeft = 10;
            converter.Options.MarginRight = 6;
            converter.Options.MarginTop = 10;
            // create a new pdf document converting the html string of the page
            PdfDocument doc = converter.ConvertHtmlString(myWriter.ToString(), Request.Url.AbsoluteUri);
            doc.DocumentInformation.Title = "documentFile";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            doc.Save(path + Request.QueryString["ApplicationKey"].ToString() + ".pdf");

            doc.Close();
            string url = "https://secure.in1.echosign.com/public/oauth?redirect_uri=" + System.Configuration.ConfigurationManager.AppSettings["SvcRtnURL"].ToString() + "&response_type=code&client_id=" + System.Configuration.ConfigurationManager.AppSettings["SvcClientId"].ToString() + "&client_secret=" + System.Configuration.ConfigurationManager.AppSettings["SvcSecretId"].ToString() + "&scope=agreement_read:self+agreement_write:self+agreement_send:self&state=" + Request.QueryString["ApplicationKey"].ToString();
            Response.Redirect(url);
           // Response.Redirect("ServiceeSine.aspx?ApplicationKey=" + Request.QueryString["ApplicationKey"].ToString() + "&Amount=" + Request.QueryString["Amount"].ToString() + "&RedirectKey=YES" + "&AccountHd=yes&ServiceID=" + Request.QueryString["ServiceID"].ToString() + "&T=2"); 
            //doc.Save(path + Request.QueryString["ApplicationNo"].ToString() + ".pdf");ParticularApplicationDetails.aspx?ApplicationNo=2018071339000004&ServiceId=39
           //// doc.Save(Response, false, "SWP_" + Request.QueryString["ApplicationNo"].ToString() + ".pdf");
           // //doc.Save(Response, false, "SWP_" + Request.QueryString["ApplicationNo"].ToString() + ".pdf");
           // doc.Close();
           // Response.Redirect("PaymentThankYou.aspx?ApplicationKey=" + Request.QueryString["ApplicationKey"].ToString() + "&sts=yes"+"");
      

        }
        catch (Exception ex)
        {
            throw ex;
            //throw Convert.ToString(ex.Message);
        }




    }
}