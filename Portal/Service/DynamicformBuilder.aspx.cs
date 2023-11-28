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

public partial class Formbuilder_DynamicformBuilder : System.Web.UI.Page
{
    #region "Global variable"
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    #endregion
    #region "Page Load"
    /// <summary>
    /// Radhika Rani Patri on 08-05-2017
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillService(ddlServiceId);
        }
    }
    #endregion
    //[System.Web.Services.WebMethod]
    //public static List<Complaint> FillTableName()
    //{
    //    List<Complaint> listUser = new List<Complaint>();
    //    DataTable dt = new DataTable();
    //    SqlConnection sqlConnection = new SqlConnection(connectionString);
    //    sqlConnection.Open();
    //    string Qury = "SELECT DISTINCT TABLE_NAME as TBLNAME FROM INFORMATION_SCHEMA.COLUMNS";
    //    using (SqlConnection con = new SqlConnection(connectionString))
    //    {
    //        using (SqlCommand cmd = new SqlCommand(Qury))
    //        {
    //            cmd.CommandType = CommandType.Text;
    //            cmd.Connection = con;
    //            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
    //            {
    //                DataSet ds = new DataSet();
    //                sda.Fill(ds);
    //                dt = ds.Tables[0];
    //                for (int i = 0; i < dt.Rows.Count; i++)
    //                {
    //                    Complaint objList = new Complaint();
    //                    objList.tblName = dt.Rows[0]["TBLNAME"].ToString();
    //                    objList.tblValue = dt.Rows[0]["TBLNAME"].ToString();
    //                    listUser.Add(objList);
    //                }
    //                return listUser;
    //            }
    //        }


    //    }
    //}
    #region "GetTableName"
    /// <summary>
    /// Radhika Rani Patri on 08-05-2017 all table name retrive from database
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static List<ListItem> GetTableName()
    {
        string Qury = "SELECT DISTINCT TABLE_NAME as TBLNAME FROM INFORMATION_SCHEMA.COLUMNS";
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
                            Value = sdr["TBLNAME"].ToString(),
                            Text = sdr["TBLNAME"].ToString()
                        });
                    }
                }
                con.Close();
                return customers;
            }
        }
    }
    #endregion
    #region "GetColumnList"
    /// <summary>
    /// Radhika Rani Patri on 08-05-2017 retrive all column list
    /// </summary>
    /// <param name="tablename"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static List<ListItem> GetColumnList(string tablename)
    {
        string Qury = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = " + "'" + tablename + "'" + "";
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
                            Value = sdr["COLUMN_NAME"].ToString(),
                            Text = sdr["COLUMN_NAME"].ToString()
                        });
                    }
                }
                con.Close();
                return customers;
            }
        }
    }
    #endregion
    #region "Button Click Event"
    /// <summary>
    /// RadhikaRaniPatri on 08-05-2017 here save all dynamically created control and it's propertites
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {




        DataTable DynamicForm = new DataTable();
        DynamicForm.TableName = "MyTable";
        DynamicForm.Columns.Add(new DataColumn("INT_SEQUENCEID"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_CONTROL_NAME"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_CONTROL_TYPE"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_CONTROL_ID"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_LABEL_NAME"));
        DynamicForm.Columns.Add(new DataColumn("PINT_LENGTH"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_VALIDATIONTYPE"));
        DynamicForm.Columns.Add(new DataColumn("PINT_REQVALIDATION"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_TOOLTIP"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_TEXTMODE"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_CSSCLASS"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_DATASOURCE"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_DATAVALUEFIELD"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_DATATEXTFIELD"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_FILEALLOWED"));
        DynamicForm.Columns.Add(new DataColumn("PINT_MAXSIZE"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_OPTION"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_DEFAULTVALUE"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_HEADINGTEXT"));
        DynamicForm.Columns.Add(new DataColumn("PVCH_PLUGINID"));
        DynamicForm.Columns.Add(new DataColumn("PINT_AUTOMAPPING"));
        DynamicForm.Columns.Add(new DataColumn("INT_DELETED_FLAG"));


        DynamicForm.Columns.Add(new DataColumn("INT_FORM_ID"));
        DynamicForm.Columns.Add(new DataColumn("NVCH_HEADERTEXT"));
        DynamicForm.Columns.Add(new DataColumn("NVCH_FOOTERTEXT"));
        DynamicForm.Columns.Add(new DataColumn("VCH_FORM_TITLE"));
        DynamicForm.Columns.Add(new DataColumn("VCH_LOG"));
        DynamicForm.Columns.Add(new DataColumn("INT_ALLIGNMENT"));

        string LiDataValues = hdnLiDataValues.Value.TrimEnd('@');
        string[] AllData = LiDataValues.Split('@');
        string ids = "";
        string name = "";
        string label = "";
        int length = 0;
        string tooltip = "";
        string textMode = "";
        string cssCls = "";
        string CntlType = "";
        string validation = "";
        int maxsize = 0;
        string fileAllowed = "";
        string dataTextField = "";
        string dataValueField = "";
        int requiredfield = 0;
        string datasource = "";
        string option = "";
        string defaultval = "";
        string pluginid = "";
        string hdngText = "";
        int AutoMapping =0;

        string strFormId = (ddlServiceId.SelectedValue).ToString();
        string HeadingText = "";
        string footertext = "";
        string strFormTitle = "";
        int intallignment = Convert.ToInt32(rdbAllignment.SelectedValue);
        string strLogo = Upload_File();
        HeadingText = editHeader.Content;
        footertext = editFooter.Content;
        for (int i = 0; i < AllData.Count(); i++)
        {
            DataRow dr = DynamicForm.NewRow();
            string[] LiData = AllData[i].Split('+');
            ids = LiData[0].ToString();
            if (LiData[1] != null && LiData[1] != "")
            {
                name = LiData[1].ToString();
            }
            else
            {
                name = "";
            }

            if (LiData[2] != null && LiData[2] != "")
            {
                label = LiData[2].ToString();
            }
            else
            {
                label = "";
            }
            if (LiData[3] != null && LiData[3] != "")
            {
                length = Convert.ToInt32(LiData[3]);
            }
            else
            {
                length = 0;
            }
            if (LiData[4] != null && LiData[4] != "")
            {
                tooltip = LiData[4].ToString();
            }
            else
            {
                tooltip = "";
            }
            if (LiData[5] != null && LiData[5] != "")
            {
                textMode = LiData[5].ToString();
            }
            else
            {
                textMode = "";
            }
            if (LiData[6] != null && LiData[6] != "")
            {
                cssCls = LiData[6].ToString();
            }
            else
            {
                cssCls = "";
            }
            if (LiData[7] != null && LiData[7] != "")
            {
                CntlType = LiData[7].ToString();
            }
            else
            {
                CntlType = "";
            }
            if (LiData[8] != null && LiData[8] != "")
            {
                validation = LiData[8].ToString();
            }
            else
            {
                validation = "";
            }
            if (LiData[9] != null && LiData[9] != "")
            {
                maxsize = Convert.ToInt32(LiData[9]);
            }
            else
            {
                maxsize = 0;
            }
            if (LiData[10] != null && LiData[10] != "")
            {
                fileAllowed = LiData[10].ToString();
            }
            else
            {
                fileAllowed = "";
            }
            if (LiData[11] != null && LiData[11] != "")
            {
                dataTextField = LiData[11].ToString();
            }
            else
            {
                dataTextField = "";
            }
            if (LiData[12] != null && LiData[12] != "")
            {
                dataValueField = LiData[12].ToString();
            }
            else
            {
                dataValueField = "";
            }
            if (LiData[13] != null && LiData[13] != "")
            {
                requiredfield = Convert.ToInt32(LiData[13].ToString());
            }
            else
            {
                requiredfield = 0;
            }
            if (LiData[14] != null && LiData[14] != "")
            {
                datasource = LiData[14].ToString();
            }
            else
            {
                datasource = "";
            }
            if (LiData[15] != null && LiData[15] != "")
            {
                option = LiData[15].ToString();
            }
            else
            {
                option = "";
            }
            if (LiData[16] != null && LiData[16] != "")
            {
                defaultval = LiData[16].ToString();
            }
            else
            {
                defaultval = "";
            }
            if (LiData[17] != null && LiData[17] != "")
            {
                pluginid = LiData[17].ToString();
            }
            else
            {
                pluginid = "";
            }
            if (LiData[18] != null && LiData[18] != "")
            {
                hdngText = LiData[18].ToString();
            }
            else
            {
                hdngText = "";
            }

            if (LiData[19] != null && LiData[19] != "")
            {
                AutoMapping = Convert.ToInt32(LiData[19].ToString());
            }
            else
            {
                AutoMapping =0;
            }
            dr["INT_FORM_ID"] = strFormId;
            dr["PVCH_CONTROL_NAME"] = name;
            dr["PVCH_CONTROL_TYPE"] = CntlType;
            dr["PVCH_CONTROL_ID"] = ids;
            dr["PVCH_LABEL_NAME"] = label;
            dr["PINT_LENGTH"] = length;
            dr["PVCH_VALIDATIONTYPE"] = validation;
            dr["PINT_REQVALIDATION"] = requiredfield;
            dr["PVCH_TOOLTIP"] = tooltip;
            dr["PVCH_TEXTMODE"] = textMode;
            dr["PVCH_CSSCLASS"] = cssCls;
            dr["PVCH_DATASOURCE"] = datasource;
            dr["PVCH_DATAVALUEFIELD"] = dataValueField;
            dr["PVCH_DATATEXTFIELD"] = dataTextField;
            dr["PVCH_FILEALLOWED"] = fileAllowed;
            dr["PINT_MAXSIZE"] = maxsize;
            dr["PVCH_OPTION"] = option;
            dr["PVCH_DEFAULTVALUE"] = defaultval;
            dr["PVCH_PLUGINID"] = pluginid;
            dr["INT_DELETED_FLAG"] = 0;
            dr["PVCH_HEADINGTEXT"] = hdngText;
            dr["PINT_AUTOMAPPING"] = AutoMapping;
            
            dr["INT_SEQUENCEID"] =i+1;

            dr["VCH_FORM_TITLE"] = strFormTitle;
            dr["VCH_LOG"] = strLogo;
            dr["NVCH_HEADERTEXT"] = HeadingText;
            dr["NVCH_FOOTERTEXT"] = footertext;
            dr["INT_ALLIGNMENT"] = intallignment;
            DynamicForm.Rows.Add(dr);
            if (CntlType == "FromToDate")
            {
               
                var newRow = DynamicForm.NewRow();
                newRow.ItemArray = dr.ItemArray;
                newRow["PVCH_CONTROL_NAME"] = name + "_To";
                newRow["INT_DELETED_FLAG"] = 100;
                DynamicForm.Rows.Add(newRow);             
                newRow = null;
            }
            if (CntlType == "DateTime")
            {
               
                var newRow = DynamicForm.NewRow();
                newRow.ItemArray = dr.ItemArray;
                newRow["PVCH_CONTROL_NAME"] = name + "_mnth";
                newRow["INT_DELETED_FLAG"] = 100;
                DynamicForm.Rows.Add(newRow);
                newRow = null;
            }

        }
        string xmltable = GetSTRXMLResult(DynamicForm);
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("USP_DYNAMICFORM_CREATION_USINGXML", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@P_XMLTABLE", xmltable);
        int status = cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Saved Successfully.');", true);
    }
    #endregion
    #region "DataTable to XMLString convert"
    /// <summary>
    /// Radhika Rani Patri o 08-05-2017 
    /// </summary>
    /// <param name="dtTable" DataTable that convert to XML string></param>
    /// <returns></returns>
    public static string GetSTRXMLResult(DataTable dtTable)
    {
        string strXMLResult = string.Empty;
        if (dtTable.Rows.Count > 0)
        {
            StringWriter sw = new StringWriter();
            dtTable.WriteXml(sw);
            strXMLResult = sw.ToString();
            sw.Close();
            sw.Dispose();
        }

        return strXMLResult;
    }
    #endregion
    #region "Fill Service"
    /// <summary>
    /// Radhika Rani Patri on 08-05-2017  Fill all service
    /// </summary>
    /// <param name="ddlService"></param>
    public void FillService(DropDownList ddlService)
    {

        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        string Qury = "select INT_SERVICEID,VCH_SERVICENAME from M_SERVICEMASTER_TBL where INT_DELETED_FLAG=0";
        SqlCommand cmd = new SqlCommand(Qury, con); // table name 
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);  // fill dataset
        ddlService.DataTextField = ds.Tables[0].Columns["VCH_SERVICENAME"].ToString(); // text field name of table dispalyed in dropdown
        ddlService.DataValueField = ds.Tables[0].Columns["INT_SERVICEID"].ToString();             // to retrive specific  textfield name 
        ddlService.DataSource = ds.Tables[0];      //assigning datasource to the dropdownlist
        ddlService.DataBind();  //binding dropdownlist
        ddlService.Items.Insert(0, new ListItem("Select", "0"));
    }
    #endregion
    #region "File Upload"
    /// <summary>
    /// Radhika Rani Patri on 08-05-2017 FileUploaded here  
    /// </summary>
    /// <returns></returns>
    private string Upload_File()
    {

        //string AppPath = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"].ToString();
        string gFilePath = "";
        string strFileReturn = "";
        string strtime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        if (fulLogo.HasFile)
        {
            string lPageName = string.Empty;
            string AppPageName = string.Empty;
            string lFileExt = string.Empty;
            try
            {
                if ((fulLogo.HasFile))
                {
                    lFileExt = System.IO.Path.GetExtension(fulLogo.FileName);
                    if (lFileExt == ".png" || lFileExt == ".pdf" || lFileExt == ".doc" || lFileExt == ".docx" || lFileExt == ".xls" || lFileExt == ".xlsx")
                    {
                        //lPageName = AppPath + "/Logo/" + strtime + "_Logo" + lFileExt;
                        lPageName = strtime + "_Logo" + lFileExt;
                    }
                    else
                    {

                        return "";
                    }
                    if (!Directory.Exists(Server.MapPath("Logo/")))
                    {
                        // Create the directory.
                        Directory.CreateDirectory(Server.MapPath("../Logo/"));
                    }
                    gFilePath = Server.MapPath("../Logo/" + lPageName);
                    if (File.Exists(gFilePath))
                    {
                        File.Delete(gFilePath);
                    }
                    fulLogo.PostedFile.SaveAs(gFilePath);
                    strFileReturn = lPageName;
                }

            }
            catch (Exception ex)
            {
                throw ex;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + Resources.Resource.Sorry___Some_Internal_Error_Found__Please_Try_Later + "', '" + Resources.Resource.TitleOfProject + "','" + Resources.Resource.Ok + "','" + Resources.Resource.Cancel + "');   </script>", false);
            }
        }
        return strFileReturn;
    }
    #endregion
    #region "Get pluginPage"
    /// <summary>
    /// RadhikaRaniPatri on 08-05-2017 get all pages of plugin folder
    /// </summary>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static List<ListItem> GetPluginPages()
    {
        //try
        //{
        string Qury = "SELECT VCH_PLUGINNAME FROM M_PLUGINPAGES_TBL  ";
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
                            Value = sdr["VCH_PLUGINNAME"].ToString(),
                            Text = sdr["VCH_PLUGINNAME"].ToString()
                        });
                    }
                }
                con.Close();
                return customers;
            }
        }
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}
    }
    #endregion

 
}