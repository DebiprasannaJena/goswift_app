using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using Newtonsoft.Json;
using System.Text;
using EntityLayer.Proposal;
using System.ServiceModel;
using BusinessLogicLayer.Proposal;

public partial class Portal_Service_ApplicationDetails : System.Web.UI.Page
{
    #region "Global Variable"
    /// <summary>
    /// Radhika Rani Patri
    /// All global variable declared here
    /// </summary>
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    string ApplicationPath = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"];
    string WebApplicationPath = System.Configuration.ConfigurationManager.AppSettings["WebApplicationPath"];
    DataTable dt = new DataTable();
    ServiceDetails objService1 = new ServiceDetails();
    string FormHeader = "";
    string FormFooter = "";
    int intAllignment = 0;
    string strUnqId = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Request.QueryString["ServiceId"] != null)
            {

                FormDetails(Convert.ToInt32(Request.QueryString["ServiceId"].ToString()));
            }
            //frmContent.InnerHtml = DynamicServiceDetails(Request.QueryString["ServiceId"].ToString(), Request.QueryString["ApplicationNo"].ToString());
            FillDataTable();
    
            BindGridDetails();
            GetHeader();
        }
    }
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
                            //Added BY Priti

                            if (clmnVal.IndexOf("/") > 0)
                            {
                                clmnVal = "../../" + clmnVal;
                            }
                            else
                            {
                                //Added BY Priti
                                clmnVal = WebApplicationPath + "../../Portal/Document/Upload/" + clmnVal;
                            }
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
    public string GetHTML(int FormId, int PanelID)
    {
        int intAllignment = 0;
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
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
                        if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null)
                        {
                            if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null)
                            {

                                string imgUrl = "";
                                if (dt.Rows[k]["val"].ToString().IndexOf("/") > 0)
                                {
                                    imgUrl = "../../" + dt.Rows[k]["val"].ToString();
                                }
                                else { imgUrl = WebApplicationPath + "../../Portal/Document/Upload/" + dt.Rows[k]["val"].ToString(); }
                                controlText = "<div class='col-sm-6'><span class='colon'>:</span><a href='" + imgUrl + "'><i class='fa fa-download'></i></a></div>";
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
                            if (FormId == 80 && PanelID == 1133)
                            {
                                controlText = "<label for='sss' class='col-sm-6'Id='lbl_copland'><span class='colon'>:</span>" + dt.Rows[k]["val"].ToString() + "</label>";
                            }
                            else
                            {
                                controlText = "<label for='sss' class='col-sm-6'><span class='colon'>:</span>" + dt.Rows[k]["val"].ToString() + "</label>";

                            }
                            
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
    public void FillDataTable()
    {
        string strHtmls = "";

        DataTable PnlDt = new DataTable();
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            string query = "select * from T_PANELMAPPING_TBL WHERE INT_FORM_ID=" + Convert.ToInt32(Request.QueryString["ServiceId"].ToString()) + " and INT_DELETEDFLAG=0";
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
            if (Convert.ToInt32(Request.QueryString["ServiceId"].ToString()) == 80 && Convert.ToInt32(PnlDt.Rows[i1]["INT_PANELID"]) == 1134)
            {
                strHtmls = strHtmls + "<div class='sectionPanel' Id='div_copland'><h2 id='" + PnlDt.Rows[i1]["INT_PANELID"].ToString() + "'>" + PnlDt.Rows[i1]["VCH_PANETEXT"].ToString() + "</h2><div class='row'>" + GetHTML(Convert.ToInt32(Request.QueryString["ServiceId"].ToString()), Convert.ToInt32(PnlDt.Rows[i1]["INT_PANELID"])) + "</div></div>";
            }
            else
            {
                strHtmls = strHtmls + "<div class='sectionPanel'><h2 id='" + PnlDt.Rows[i1]["INT_PANELID"].ToString() + "'>" + PnlDt.Rows[i1]["VCH_PANETEXT"].ToString() + "</h2><div class='row'>" + GetHTML(Convert.ToInt32(Request.QueryString["ServiceId"].ToString()), Convert.ToInt32(PnlDt.Rows[i1]["INT_PANELID"])) + "</div></div>";
            }
               
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
        // <img src=" + imgUrl + " alt='logo'/>
    }
    private void BindGridDetails()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        try
        {
            objService1.strAction = "V";
            objService1.strApplicationUnqKey = Request.QueryString["ApplicationNo"].ToString();
            gvService.DataSource = objService.TrakingDetailsOfTakeAction(objService1);
            gvService.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }

    }
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkCert = (LinkButton)e.Row.FindControl("lnkCert");
            LinkButton lnkAppCert = (LinkButton)e.Row.FindControl("lnkAppCert");
            lnkCert.Visible = true;
           // HyperLink hplnkdoc = (HyperLink)e.Row.FindControl("hplnkdoc");
            if (gvService.DataKeys[e.Row.RowIndex].Values[0].ToString() != "NA" && gvService.DataKeys[e.Row.RowIndex].Values[0].ToString() != "")
            {
                lnkCert.Visible = true;
            }
            else { lnkCert.Visible = false; }

            if (gvService.DataKeys[e.Row.RowIndex].Values[1].ToString() != "NA" && gvService.DataKeys[e.Row.RowIndex].Values[1].ToString() != "")
            {

                lnkAppCert.Visible = true;
            }
            else { lnkAppCert.Visible = false; }
          
        }
    }
    public static void DownLoadFileFromServer(string path, string file)
    {
        //This is used to get Project Location.
        string filePath = path;
        //This is used to get the current response.
        HttpResponse res = GetHttpResponse();
        res.Clear();
        res.AppendHeader("content-disposition", "attachment; filename=" + file);
        res.ContentType = "application/octet-stream";
        res.WriteFile(filePath);
        res.Flush();
        res.End();
    }
    public static string ServerMapPath(string path)
    {
        return HttpContext.Current.Server.MapPath(path);
    }
    public static HttpResponse GetHttpResponse()
    {
        return HttpContext.Current.Response;
    }
    protected void lnkCert_Click(object sender, EventArgs e)
    {


        LinkButton lnk = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnk.NamingContainer;
        HiddenField hdnfileval = row.FindControl("hdnfileval") as HiddenField;
        try
        {

            string path = Server.MapPath("../ApprovalDocs/" + hdnfileval.Value);
            if (File.Exists(path))
            {
                DownLoadFileFromServer(path, hdnfileval.Value);//Download File 
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('File not found !','" + Messages.TitleOfProject + "');", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }

    }
    protected void lnkAppCert_Click(object sender, EventArgs e)
    {


        LinkButton lnk = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lnk.NamingContainer;
        HiddenField hdnfileval = row.FindControl("hdnfilevalcert") as HiddenField;
        try
        {

            string path = Server.MapPath("../ApprovalDocs/" + hdnfileval.Value);
            if (File.Exists(path))
            {
                DownLoadFileFromServer(path, hdnfileval.Value);//Download File 
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "jAlert('File not found !','" + Messages.TitleOfProject + "');", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }

    }
}