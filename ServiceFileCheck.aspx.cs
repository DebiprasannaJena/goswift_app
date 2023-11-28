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

public partial class ServiceFileCheck : System.Web.UI.Page
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
            //if (Request.QueryString["RedirectKey"].ToString() == "NO")
            //{
                FileMatching();
            //}
            //{

            //}
          

        }
    }

    protected void btnFile_Click(object sender, EventArgs e)
    {

        if (gvEscalation.Rows.Count > 0)
        {
            for (int i = 0; i <= gvEscalation.Rows.Count - 1; i++)
            {
                GridViewRow gvr = (GridViewRow)gvEscalation.Rows[i];
                HiddenField hdnValue = (HiddenField)gvr.Cells[2].FindControl("hdnFile");
                // Convert.ToInt32(((DropDownList)gvr.Cells[1].FindControl("DdlDesg")).SelectedValue.ToString());
                FileUpload fil = ((FileUpload)gvr.Cells[2].FindControl("filUpld"));
                Upload_File(hdnValue.Value.ToString(), fil);
            }
            Response.Redirect(Request.RawUrl);
        }
    }
    string GenerateServieOrder(string applicationkey)
    {
        string strRes = "ES" + applicationkey + "-" + DateTime.Now.ToString("ddmmyyhhmmss");
        int intRes = 0;

        string queryCnt = "INSERT INTO [T_Service_Order]([vchOrderNo],[intServiceId]";
        queryCnt = queryCnt + ",[vchApplicationNo],[dtmOrderDate],[intPaymentStatus],[intCreatedBy],[vchChallanAmount],[IntReqid])VALUES";
        queryCnt = queryCnt + "('" + strRes + "'," + GetFormId(applicationkey) + ",'" + applicationkey + "','" + DateTime.Now.ToString() + "'," + 0 + "," + Session["InvestorId"].ToString() + "," + Request.QueryString["Amount"].ToString() + ",'" + applicationkey + "')";

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


    public void FileMatching()
    {
        string strFileName = "";
        string wordDocName = "Portal/Document/Upload/";
        List<fileCheckCls> rtnFilList = new List<fileCheckCls>();
        fileCheckCls filObj = new fileCheckCls();
        filObj.PVCH_ACTIONCODE = "V";
        filObj.PVCH_FORMID = Convert.ToInt32(Request.QueryString["ServiceID"].ToString());

        rtnFilList = objData.AllFileView(filObj).ToList();
        for (int k = 0; k < rtnFilList.Count; k++)
        {
            List<fileCheckCls> rtnFilList1 = new List<fileCheckCls>();
            filObj.PVCH_ACTIONCODE = "C";
            filObj.PVCH_APPLICATIONKEY = Request.QueryString["ApplicationKey"].ToString();
            filObj.PVCH_FORMID = Convert.ToInt32(Request.QueryString["ServiceID"].ToString());
            filObj.PVCH_COLUMNNAME = rtnFilList[k].PVCH_CONTROL_NAME;
            rtnFilList1 = objData.AllFileView(filObj);
            strFileName = rtnFilList1[0].VCH_FILENAME;
            if (strFileName != "")
            {
                if (File.Exists(Server.MapPath(wordDocName + strFileName)))
                {
                    //Response.Write("file exist in server");
                }
                else
                {
                    if (File.Exists(Server.MapPath(strFileName)))
                    {
                    }
                    else
                    {

                        lst.Add(rtnFilList[k].PVCH_LABEL_NAME);
                        hdnlst.Add(strFileName);
                        // Response.Write("file not exist in server");
                    }
                }
            }
        }
        if (lst.Count > 0)
        {
           // divPayment.Visible = false;
            // btnApply.Visible = false;
            diveSine.Visible = false;
            btnFile.Visible = true;
            fillGrid(lst, hdnlst);
            btnFile.Visible = true;
        }
        else
        {
          //  divPayment.Visible = true;
            // btnApply.Visible = true;
         //   Response.Redirect();
            diveSine.Visible = true;
            btnFile.Visible = false;
           
        }
    }
    public void fillGrid(ArrayList list1, ArrayList hdnlst)
    {
        try
        {
            int i = 0;
            #region "Creating Datatable to bind with gridview"
            DataColumn slno = new DataColumn("slno", typeof(string));
            DataColumn lblName = new DataColumn("lblName", typeof(string));
            DataColumn hdnVal = new DataColumn("hdnVal", typeof(string));
            //DataColumn loc = new DataColumn("loc", typeof(string));
            //DataColumn loclvl = new DataColumn("loclvl", typeof(string));
            //DataColumn stdP = new DataColumn("stdP", typeof(string));
            dt.Columns.Add(slno);
            dt.Columns.Add(lblName);
            dt.Columns.Add(hdnVal);
            //dt.Columns.Add(loc);
            //dt.Columns.Add(loclvl);
            //dt.Columns.Add(stdP);
            DataRow dr = null;
            #endregion

            if (list1.Count > 0)
            {
                while (i < list1.Count)
                {
                    dr = dt.NewRow();
                    dr["slno"] = i + 1; //stores level id
                    dr["lblName"] = list1[i];
                    dr["hdnVal"] = hdnlst[i];
                    //dr["loc"] = "0";
                    //dr["loclvl"] = "--Select--";
                    //dr["stdP"] = string.Empty;
                    dt.Rows.Add(dr);
                    i++;
                }
            }
            gvEscalation.DataSource = dt;
            gvEscalation.DataBind();
            HiddenField1.Value = gvEscalation.Rows.Count.ToString();
            gvEscalation.Visible = true;

            ViewState["DynamicTbl"] = dt;
            HiddenField1.Value = gvEscalation.Rows.Count.ToString();
        }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }
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

  
    protected void btneSign_Click(object sender, EventArgs e)
    {
        //string url = "https://secure.in1.echosign.com/public/oauth?redirect_uri=https://localhost/swp/ServiceeSine.aspx&response_type=code&client_id=CBJCHBCAABAAN7wXMKGTjYCx1QPkjuYiI8nTRL4PuBap&client_secret=UGhvKImYx6Kqs0nQVq4ftu-UTeSNFyXG&scope=agreement_read:self+agreement_write:self+agreement_send:self&state=" + Request.QueryString["ApplicationKey"].ToString();

       // string url = "https://secure.in1.echosign.com/public/oauth?redirect_uri=https://localhost/swp/ServiceFileCheck.aspx&response_type=code&client_id=CBJCHBCAABAAN7wXMKGTjYCx1QPkjuYiI8nTRL4PuBap&client_secret=UGhvKImYx6Kqs0nQVq4ftu-UTeSNFyXG&scope=agreement_read:self+agreement_write:self+agreement_send:self";

     //   string url = "https://secure.in1.echosign.com/public/oauth?redirect_uri=https://localhost/swp/ServiceeSine.aspx&response_type=code&client_id=CBJCHBCAABAAon0oT6tA9Q42HE_jC-NFdLBxjzt2O3a6&scope=user_login:self+agreement_send:account&state=123";
       // Response.Redirect(url);
   Response.Redirect("EsignPdfGenerate.aspx?ApplicationKey=" + Request.QueryString["ApplicationKey"] + "&Amount=" + Request.QueryString["Amount"] + "&AccountHd=" + Request.QueryString["AccountHd"] + "&ServiceID=" + Convert.ToInt32(Request.QueryString["ServiceID"].ToString()) + "");
        btnFile.Visible = false;
    }
}