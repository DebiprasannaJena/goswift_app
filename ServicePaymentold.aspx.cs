using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using EntityLayer.Service;
using System.IO;
using System.Collections;
public partial class ServicePaymentold : System.Web.UI.Page
{
    DataAcessLayer.Service.ServiceDataLayer objData = new DataAcessLayer.Service.ServiceDataLayer();
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    DataTable dt = new DataTable();
    ArrayList lst = new ArrayList();
    ArrayList hdnlst = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //FileMatching();
            if (Request.QueryString["T"] != null)
            {
                //hrfPaymentConfrim.Visible = true;
                hrfPaymentConfrim.Visible = false; //// Above line commented and this line added by sushant jena on Dt.27-Dec-2018
                // hrfPaymentConfrim.NavigateUrl = "ServicePaymentConfirm.aspx?reqid=" + Request.QueryString["ApplicationKey"].ToString();
            }
            else
            {
                hrfPaymentConfrim.Visible = false;
            }

            /*-------------------------------------------------------*/

            if (Request.QueryString["ApplicationKey"] != null)
            {
                if (Request.QueryString["Amount"] != null)
                {
                    txtAmount.Text = Request.QueryString["Amount"].ToString();
                    txtAmount.ReadOnly = true;
                    if (Convert.ToDouble(txtAmount.Text) <= 0)
                    {
                        Response.Redirect("PaymentThankYou.aspx?ApplicationKey=" + Request.QueryString["ApplicationKey"] + "");
                        objData.UpdatePaymentService(Request.QueryString["ApplicationKey"].ToString(), 0, 0);
                    }
                    else
                    {
                        decimal decAppFee = Convert.ToDecimal(Request.QueryString["AppFee"]);
                        DataAcessLayer.Service.ServiceDataLayer objData = new DataAcessLayer.Service.ServiceDataLayer();
                        objData.UpdatePaymentService(Request.QueryString["ApplicationKey"].ToString(), Convert.ToDouble(txtAmount.Text), decAppFee);
                    }
                }
            }
        }
    }

    protected void btnApply_Click(object sender, EventArgs e)
    {
        Session["ApplicationKey"] = Request.QueryString["ApplicationKey"].ToString();
        Session["amnt"] = txtAmount.Text;
        Payment();
        //Response.Redirect("PaymentThankYou.aspx?ApplicationKey=" + Request.QueryString["ApplicationKey"] + "");
    }

    string GenerateServieOrder(string applicationkey)
    {
        string strRes = "ES" + applicationkey + "-" + DateTime.Now.ToString("ddMMyyhhmmss");
        int intRes = 0;

        string queryCnt = "INSERT INTO [T_Service_Order]([vchOrderNo],[intServiceId]";
        queryCnt = queryCnt + ",[vchApplicationNo],[dtmOrderDate],[intPaymentStatus],[intCreatedBy],[vchChallanAmount],[IntReqid])VALUES";
        queryCnt = queryCnt + "('" + strRes + "'," + GetFormId(applicationkey) + ",'" + applicationkey + "',GETDATE()," + 0 + "," + Session["InvestorId"].ToString() + "," + Request.QueryString["Amount"].ToString() + ",'" + applicationkey + "')";

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString()))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                intRes = Convert.ToInt32(cmd1.ExecuteNonQuery().ToString());
                con.Close();
            }
        }
        return strRes;
    }
    public int GetFormId(string applicationkey)
    {
        //int intInvestorId = Convert.ToInt32(Session["InvestorId"]);
        string queryCnt = " select INT_SERVICEID from T_APPLICATION_TBL where VCH_APPLICATION_UNQ_KEY='" + applicationkey + "'";
        int strFormId = 0;
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString()))
        {
            using (SqlCommand cmd1 = new SqlCommand(queryCnt))
            {
                List<ListItem> customers = new List<ListItem>();
                cmd1.CommandType = CommandType.Text;
                cmd1.Connection = con;
                con.Open();
                strFormId = Convert.ToInt32(cmd1.ExecuteScalar().ToString());
                con.Close();
            }
        }
        return strFormId;
    }
    void Payment()
    {
        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());

        string transactionDetail, transactionDetail1 = "";
        string FAccountHead = "";
        string AccountHead = "";
        try
        {
            string strQuery = "SELECT dbo.M_SWP_SERVICEACCOUNT.vchServiceName, dbo.M_SWP_SERVICEACCOUNT.intServiceid, dbo.M_SERVICEMASTER_TBL.INT_DEPARTMENT_ID, dbo.M_ADM_LevelDetails.nvchLevelName, dbo.M_SWP_SERVICEACCOUNT.vchServiceType, dbo.M_SWP_SERVICEACCOUNT.vchAccountHead ";
            strQuery = strQuery + " FROM dbo.M_SERVICEMASTER_TBL INNER JOIN  dbo.M_SWP_SERVICEACCOUNT ON dbo.M_SERVICEMASTER_TBL.INT_SERVICEID = dbo.M_SWP_SERVICEACCOUNT.intServiceid INNER JOIN";
            strQuery = strQuery + " dbo.M_ADM_LevelDetails ON dbo.M_SERVICEMASTER_TBL.INT_DEPARTMENT_ID = dbo.M_ADM_LevelDetails.intLevelDetailId WHERE (dbo.M_SWP_SERVICEACCOUNT.intServiceid = " + Request.QueryString["ServiceID"].ToString() + ")";

            SqlCommand cmd = new SqlCommand(strQuery, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                HttpContext _context = HttpContext.Current;

                string rurl = System.Configuration.ConfigurationManager.AppSettings["ServiceReturnURL"];
                string surl = System.Configuration.ConfigurationManager.AppSettings["Responseurl"];

                NameValueCollection ldata = new NameValueCollection();

                string Description = "NA";
                string Amount = "0";
                if (System.Configuration.ConfigurationManager.AppSettings["ActualAmount"].ToString() == "Yes")
                {
                    Amount = txtAmount.Text;
                }
                else
                {
                    Amount = "1";
                }

                if (Request.QueryString["ServiceID"].ToString() == "48")/////Change of Land Use
                {
                    if (Amount == "50")
                    {
                        transactionDetail = "(0030-03-800-0097-01076-000," + "	Application Fee and Notice Fee" + ",30!~!" + "0029-00-800-0097-01077-000,Conversion fee,20)";
                    }
                    else
                    {
                        transactionDetail = "(0030-03-800-0097-01076-000," + "	Application Fee and Notice Fee" + ",30)";
                    }
                }
                else if (Request.QueryString["ServiceID"].ToString() == "41")////Permission to Draw Water
                {
                    decimal applicationFee = Math.Round(Convert.ToDecimal(Request.QueryString["Amount"])) - 1000;
                    string strSrvc41AcountHdProce = System.Configuration.ConfigurationManager.AppSettings["strSrvc41AcountHdProce"];
                    string strSrvc41AcountHdSecurity = System.Configuration.ConfigurationManager.AppSettings["strSrvc41AcountHdSecurity"];
                    transactionDetail = "(" + strSrvc41AcountHdProce + "," + "	Application Fee and Notice Fee" + ",1000!~!" + "" + strSrvc41AcountHdSecurity + ",Security fee," + applicationFee + ")";
                    // transactionDetail = "(" + strSrvc41AcountHdProce + "," + "	Application Fee and Notice Fee" + ",1!~!" + "" + strSrvc41AcountHdSecurity + ",Security fee,1)";
                }
                else if (Request.QueryString["ServiceID"].ToString() == "7")////Contract Labour Service
                {
                    decimal decAppFee = Convert.ToDecimal(Request.QueryString["AppFee"]);
                    decimal decSecurityFee = Math.Round(Convert.ToDecimal(Request.QueryString["Amount"])) - decAppFee;

                    string strSrvc7AcountHdProce = ds.Tables[0].Rows[0]["vchAccountHead"].ToString();
                    string strSrvc7AcountHdSecurity = System.Configuration.ConfigurationManager.AppSettings["strSrvc7AcountHdSecurity"];

                    transactionDetail = "(" + strSrvc7AcountHdProce + "," + "	Application Fee and Notice Fee" + "," + decAppFee + "!~!" + "" + strSrvc7AcountHdSecurity + ",Security fee," + decSecurityFee + ")";
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
                        if (Request.QueryString["ServiceID"].ToString() == "16")
                        {
                            string AcntHd = PowerConnectionAccountHead();//"0852-80-800-0234-02233-000";//
                            transactionDetail = "(" + AcntHd + "," + Description + "," + Amount + "!~!0875-60-800-0097-02241-000,User Fee,1)";
                        }
                        else
                        {
                            transactionDetail = "(" + FAccountHead + "," + Description + "," + Amount + ")";
                        }
                    }
                }

                string userName = Session["IndustryName"].ToString();
                string strRTNurl = rurl;
                ldata.Add("transactionDetail", transactionDetail);
                ldata.Add("depositedBy", userName);

                if (Request.QueryString["ServiceID"].ToString() == "18")
                {
                    ldata.Add("deptName", "LGM");
                }
                else
                {
                    ldata.Add("deptName", "IND");
                }

                /*-----------------------------------------------------------------*/
                string strOrderNo = GenerateServieOrder(Request.QueryString["ApplicationKey"].ToString()); ////// Added by Sushant Jena
                ldata.Add("deptRefId", strOrderNo);////// Added by Sushant Jena
                /*-----------------------------------------------------------------*/
                string otherparam = "(OrderNo=" + strOrderNo + "!~!ReqID=" + Request.QueryString["ApplicationKey"].ToString() + "!~!redirect_url=" + strRTNurl + ")";
                ldata.Add("otherParameters", otherparam);
                RedirectAndPOST(this.Page, surl, ldata);
            }
            else
            {
                HttpContext _context = HttpContext.Current;

                string rurl = System.Configuration.ConfigurationManager.AppSettings["ServiceReturnURL"];
                string surl = System.Configuration.ConfigurationManager.AppSettings["Responseurl"];
                NameValueCollection ldata = new NameValueCollection();
                string Description = "NA";
                string Amount = "0";
                if (System.Configuration.ConfigurationManager.AppSettings["ActualAmount"].ToString() == "Yes")
                {
                    Amount = txtAmount.Text;
                }
                else
                {
                    Amount = "1";
                }
                AccountHead = "0852-80-800-0234-02233-000";

                string userName = "NA";
                transactionDetail = "(" + AccountHead + "," + Description + "," + Amount + ")";
                string strRTNurl = rurl;
                ldata.Add("transactionDetail", transactionDetail);
                ldata.Add("depositedBy", userName);

                if (Request.QueryString["ServiceID"].ToString() == "18")
                {
                    ldata.Add("deptName", "LGM");
                }
                else
                {
                    ldata.Add("deptName", "IND");
                }

                /*-----------------------------------------------------------------*/
                string strOrderNo = GenerateServieOrder(Request.QueryString["ApplicationKey"].ToString()); ////// Added by Sushant Jena
                ldata.Add("deptRefId", strOrderNo);////// Added by Sushant Jena
                /*-----------------------------------------------------------------*/

                string otherparam = "(OrderNo=" + strOrderNo + "!~!ReqID=" + Request.QueryString["ApplicationKey"].ToString() + "!~!redirect_url=" + strRTNurl + ")";
                ldata.Add("otherParameters", otherparam);
                RedirectAndPOST(this.Page, surl, ldata);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
    public static void RedirectAndPOST(Page page, string destinationUrl, NameValueCollection data)
    {
        //Prepare the Posting form
        string strForm = PreparePOSTForm(destinationUrl, data);
        //Add a literal control the specified page holding 
        //the Post Form, this is to submit the Posting form with the request.
        page.Controls.Add(new LiteralControl(strForm));
    }
    private static String PreparePOSTForm(string url, NameValueCollection data)
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\" >");//target=\"TheWindow\"

        foreach (string key in data)
        {
            strForm.Append("<input type=\"hidden\" name=\"" + key +
                           "\" value=\"" + data[key] + "\">");
        }

        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        //strScript.Append("javascript:void window.open('','TheWindow','menubar= 1,scrollbars=1,width=600,height=400');");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }
    //public void FileMatching()
    //{
    //    string strFileName = "";
    //    string wordDocName = "Portal/Document/Upload/";
    //    List<fileCheckCls> rtnFilList = new List<fileCheckCls>();
    //    fileCheckCls filObj = new fileCheckCls();
    //    filObj.PVCH_ACTIONCODE = "V";
    //    filObj.PVCH_FORMID = Convert.ToInt32(Request.QueryString["ServiceID"].ToString());

    //    rtnFilList = objData.AllFileView(filObj).ToList();
    //    for (int k = 0; k < rtnFilList.Count; k++)
    //    {
    //        List<fileCheckCls> rtnFilList1 = new List<fileCheckCls>();
    //        filObj.PVCH_ACTIONCODE = "C";
    //        filObj.PVCH_APPLICATIONKEY = Request.QueryString["ApplicationKey"].ToString();
    //        filObj.PVCH_FORMID = Convert.ToInt32(Request.QueryString["ServiceID"].ToString());
    //        filObj.PVCH_COLUMNNAME = rtnFilList[k].PVCH_CONTROL_NAME;
    //        rtnFilList1 = objData.AllFileView(filObj);
    //        strFileName = rtnFilList1[0].VCH_FILENAME;
    //        if (strFileName != "")
    //        {
    //            if (File.Exists(Server.MapPath(wordDocName + strFileName)))
    //            {
    //                //Response.Write("file exist in server");
    //            }
    //            else
    //            {
    //                if(File.Exists(Server.MapPath(strFileName)))
    //                {
    //                }
    //                else{

    //                lst.Add(rtnFilList[k].PVCH_LABEL_NAME);
    //                hdnlst.Add(strFileName);
    //                // Response.Write("file not exist in server");
    //                }
    //            }
    //        }
    //    }
    //    if (lst.Count > 0)
    //    {
    //        divPayment.Visible = false;
    //       // btnApply.Visible = false;
    //        btnFile.Visible = true;
    //        fillGrid(lst, hdnlst);
    //    }
    //    else
    //    {
    //        divPayment.Visible = true;
    //       // btnApply.Visible = true;
    //        btnFile.Visible = false;
    //    }
    //}
    //public void fillGrid(ArrayList list1, ArrayList hdnlst)
    //{
    //    try
    //    {
    //        int i = 0;
    //        #region "Creating Datatable to bind with gridview"
    //        DataColumn slno = new DataColumn("slno", typeof(string));
    //        DataColumn lblName = new DataColumn("lblName", typeof(string));
    //        DataColumn hdnVal = new DataColumn("hdnVal", typeof(string));
    //        //DataColumn loc = new DataColumn("loc", typeof(string));
    //        //DataColumn loclvl = new DataColumn("loclvl", typeof(string));
    //        //DataColumn stdP = new DataColumn("stdP", typeof(string));
    //        dt.Columns.Add(slno);
    //        dt.Columns.Add(lblName);
    //        dt.Columns.Add(hdnVal);
    //        //dt.Columns.Add(loc);
    //        //dt.Columns.Add(loclvl);
    //        //dt.Columns.Add(stdP);
    //        DataRow dr = null;
    //        #endregion

    //        if (list1.Count > 0)
    //        {
    //            while (i < list1.Count)
    //            {
    //                dr = dt.NewRow();
    //                dr["slno"] = i + 1; //stores level id
    //                dr["lblName"] = list1[i];
    //                dr["hdnVal"] = hdnlst[i];
    //                //dr["loc"] = "0";
    //                //dr["loclvl"] = "--Select--";
    //                //dr["stdP"] = string.Empty;
    //                dt.Rows.Add(dr);
    //                i++;
    //            }
    //        }
    //        gvEscalation.DataSource = dt;
    //        gvEscalation.DataBind();
    //        HiddenField1.Value = gvEscalation.Rows.Count.ToString();
    //        gvEscalation.Visible = true;

    //        ViewState["DynamicTbl"] = dt;
    //        HiddenField1.Value = gvEscalation.Rows.Count.ToString();
    //    }
    //    catch (Exception ex)
    //    {

    //        throw new Exception(ex.Message);
    //    }
    //}
    private void Upload_File(string imgName, FileUpload fileName)
    {
        string gFilePath = "Portal/Document/Upload/";
        string strtime = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

        if (!Directory.Exists(Server.MapPath(gFilePath)))
        {
            // Create the directory.
            Directory.CreateDirectory(Server.MapPath(gFilePath));
        }
        gFilePath = Server.MapPath(gFilePath + imgName);
        if (File.Exists(gFilePath))
        {
            File.Delete(gFilePath);
        }
        fileName.PostedFile.SaveAs(gFilePath);
    }
    public string PowerConnectionAccountHead()
    {
        string strUtility = "";
        string strAccountHd = "";
        string strInsert1 = " select Utility from table_16 where VCH_APPLICATION_UNQ_KEY='" + Request.QueryString["ApplicationKey"].ToString() + "'";
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

    /// <summary>
    /// Used for payment confirmation
    /// Code section commented by Sushant jena on Dt 27-Dec-2018
    /// Because it makes payment status as paid without checking the payment from Treasury or any other table.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        //#region Modification for Demo

        //Session["amnt"] = txtAmount.Text;
        //SqlConnection con1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());


        //SqlCommand cmd1 = new SqlCommand("Update T_APPLICATION_TBL set INT_PAYMENT_STATUS=1 where VCH_APPLICATION_UNQ_KEY='" + Request.QueryString["ApplicationKey"].ToString() + "'", con1);
        //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
        //DataSet ds1 = new DataSet();
        //da1.Fill(ds1);
        //Response.Redirect("PaymentThankYou.aspx?ApplicationKey=" + Request.QueryString["ApplicationKey"].ToString());
        //return;
        //#endregion
    }
}