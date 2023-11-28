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
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using System.Web.Script.Serialization;
using Ionic.Zip;
using System.IO;

public partial class Service_ServiceDetailsView : System.Web.UI.Page
{
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    string WebApplicationPath = System.Configuration.ConfigurationManager.AppSettings["WebApplicationPath"];
   
   

    string FormFooter = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        { 

        if (Request.QueryString["ApplicationNo"] != null && Request.QueryString["ApplicationNo"] != "")
        {
            if (Request.QueryString["ServiceId"] != null)
            {
                FormDetails(Convert.ToInt32(Request.QueryString["ServiceId"].ToString()));
            }

            FillDataTable();
            FillTransaction();
            hplPdf.Target = "_self";
            hplPdf.NavigateUrl = "~/pdfGenerate.aspx?ApplicationNo=" + Request.QueryString["ApplicationNo"] + "&ServiceId=" + Request.QueryString["ServiceId"].ToString() + "";
            BindApplicationDetail();
            GetHeader();
        }
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "ServiceDetailsView");
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
                    lblapplication.Text = PnlDt.Rows[0]["VCH_APPLICATION_UNQ_KEY"].ToString();
                    lblapplieddate.Text = PnlDt.Rows[0]["DATE"].ToString();
                }
                else
                {
                    lblapplication.Text = "";
                    lblapplieddate.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }
    }
    private void BindApplicationDetail()
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
                    cmd.Parameters.Add("@VCH_ACTION", SqlDbType.VarChar).Value = "VIEWDOCDEPT";
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
                throw ex.InnerException;
            }
        }
    }
    public void FormDetails(int intFormId)
    {
        try
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
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    
    public string DynamicTableCreation(string data)
    {
        try
        {      
        string[] FilTyp = { ".jpg", ".png", ".pdf", "JPG", ".PNG", ".PDF" };
        string myHtmlFile = "";
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

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ServiceDetailsView");
        }
        return "";
        
        
    }
    
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
        Response.Redirect("ServiceViewAndTakeAction.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "");
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "ServiceDetailsView");
        }
    }

    public void FillDataTable()
    {
        try
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
            strHtmls = strHtmls + "<div class='sectionPanel'><h2 id='" + PnlDt.Rows[i1]["INT_PANELID"].ToString() + "'>" + PnlDt.Rows[i1]["VCH_PANETEXT"].ToString() + "</h2><div class='row'>" + GetHTML(Convert.ToInt32(Request.QueryString["ServiceId"].ToString()), Convert.ToInt32(PnlDt.Rows[i1]["INT_PANELID"])) + "</div></div>";
        }


      
        frmContent.InnerHtml = strHtmls;

        }
        catch(Exception ex)
        {
            Util.LogError(ex, "ServiceDetailsView");
        }
    }
    public string GetHTML(int FormId, int PanelID)
    {
        try
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
        string strGroupDiv = "";

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
                            if (dt.Rows[k]["val"].ToString() != "" && dt.Rows[k]["val"].ToString() != null && dt.Rows[k]["val"].ToString() != "NA")
                            {
                                //Added BY Priti
                                string imgUrl = "";
                                if (dt.Rows[k]["val"].ToString().IndexOf("/") > 0)
                                {
                                    imgUrl = "../../" + dt.Rows[k]["val"].ToString();
                                }
                                else { imgUrl = WebApplicationPath + "../../Portal/Document/Upload/" + dt.Rows[k]["val"].ToString(); }
                                //Added BY Priti
                              
                                controlText = "<div class='col-sm-6'><span class='colon'>:</span><a href='" + imgUrl + "' target='_blank'><i class='fa fa-download'></i></a></div>";
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
                        string strLabelText = DynamicTableCreation(dt.Rows[k]["val"].ToString());

                        strHtml = strHtml + "<div class='form-group padding-lr10'><label for='sss' class='col-sm-12'>" + strLabelName + "</label><div class='col-sm-12'><div class='table-responsive'>" + strLabelText + "</div></div>" + "<div class='clearfix'></div></div>";

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

           
        }
        catch(Exception ex)
        {
            throw ex.InnerException;
        }

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
        try
        {

       
        #region TransactionDetail
       
        
        ServiceDetails objProp1 = new ServiceDetails();
        objProp1.STRACTION = "D";
        objProp1.strApplicationUnqKey = Request.QueryString["ApplicationNo"].ToString();
        //PaymentOrderDetails(ServiceDetails objService)
        ServiceBusinessLayer objService1 = new ServiceBusinessLayer();
            List<ServiceDetails> objOrderList = objService1.PaymentOrderDetails(objProp1).ToList();
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
          
        }
        else
        {
            OrderList.InnerHtml = "<table><tr><td>No successfull transaction done.</td></tr></table>";
            OrderList1.InnerHtml = "<table><tr><td>No failure transaction done.</td></tr></table>";
        }
            #endregion
        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }
    protected void gvapplication_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

        

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hprApprodoc = (HyperLink)e.Row.FindControl("hprApprodoc");
            HyperLink hprReferndoc = (HyperLink)e.Row.FindControl("hprReferndoc");
            HyperLink hprInspectdoc = (HyperLink)e.Row.FindControl("hprInspectdoc");
            HyperLink hprRestordoc = (HyperLink)e.Row.FindControl("hprRestordoc");
            HyperLink hprNocdoc = (HyperLink)e.Row.FindControl("hprNocdoc");

            HiddenField hdnFilevalcert = (HiddenField)e.Row.FindControl("hdnfilevalcert");
            HiddenField hdnFileval = (HiddenField)e.Row.FindControl("hdnfileval");
            HiddenField hdnInspectionDocu = (HiddenField)e.Row.FindControl("hdnInspectionDocu");
            HiddenField hdnRestorationDocu = (HiddenField)e.Row.FindControl("hdnRestorationDocu");
            HiddenField hdnNocDocu = (HiddenField)e.Row.FindControl("hdnNocDocu");

            Label lblapproval = (Label)e.Row.FindControl("lblapproval");
            Label lblReferdoc = (Label)e.Row.FindControl("lblReferdoc");
            Label lblinspdoc = (Label)e.Row.FindControl("lblinspdoc");
            Label lblrestdoc = (Label)e.Row.FindControl("lblrestdoc");
            Label lblrNocdoc = (Label)e.Row.FindControl("lblrNocdoc");

            if (hdnFilevalcert.Value == "")
            {
                lblapproval.Visible = true;
                lblapproval.Text = "NA";
                lblapproval.ForeColor = System.Drawing.Color.Red;
                hprApprodoc.Visible = false;
            }
            else
            {
                hprApprodoc.ToolTip = hdnFilevalcert.Value;
                hprApprodoc.Target = "_Blank";
                hprApprodoc.NavigateUrl = "~/Portal/ApprovalDocs/" + hdnFilevalcert.Value;
            }

            /*---------------------------------------------------------------------------*/

            if (hdnFileval.Value == "")
            {
                lblReferdoc.Visible = true;
                lblReferdoc.Text = "NA";
                lblReferdoc.ForeColor = System.Drawing.Color.Red;
                hprReferndoc.Visible = false;
            }
            else
            {
                hprReferndoc.ToolTip = hdnFileval.Value;
                hprReferndoc.Target = "_Blank";
                hprReferndoc.NavigateUrl = "~/Portal/ApprovalDocs/" + hdnFileval.Value;
            }

            /*---------------------------------------------------------------------------*/

            if (hdnInspectionDocu.Value == "")
            {
                lblinspdoc.Visible = true;
                lblinspdoc.Text = "NA";
                lblinspdoc.ForeColor = System.Drawing.Color.Red;
                hprInspectdoc.Visible = false;
            }
            else
            {
                hprInspectdoc.ToolTip = hdnInspectionDocu.Value;
                hprInspectdoc.Target = "_Blank";
                hprInspectdoc.NavigateUrl = "~/Portal/ApprovalDocs/" + hdnInspectionDocu.Value;
            }

            /*---------------------------------------------------------------------------*/

            if (hdnRestorationDocu.Value == "")
            {
                lblrestdoc.Visible = true;
                lblrestdoc.Text = "NA";
                lblrestdoc.ForeColor = System.Drawing.Color.Red;
                hprRestordoc.Visible = false;
            }
            else
            {
                hprRestordoc.ToolTip = hdnRestorationDocu.Value;
                hprRestordoc.Target = "_Blank";
                hprRestordoc.NavigateUrl = "~/Portal/ApprovalDocs/" + hdnRestorationDocu.Value;
            }

            /*---------------------------------------------------------------------------*/

            if (hdnNocDocu.Value == "")
            {
                lblrNocdoc.Visible = true;
                lblrNocdoc.Text = "NA";
                lblrNocdoc.ForeColor = System.Drawing.Color.Red;
                hprNocdoc.Visible = false;
            }
            else
            {
                hprNocdoc.ToolTip = hdnNocDocu.Value;
                hprNocdoc.Target = "_Blank";
                hprNocdoc.NavigateUrl = "~/Portal/ApprovalDocs/" + hdnNocDocu.Value;
            }
        }
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "ServiceDetailsView");
        }
    }
 
}


