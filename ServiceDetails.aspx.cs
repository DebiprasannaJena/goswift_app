using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
public partial class ServiceDetails : System.Web.UI.Page
{
    #region Variable Declaration
    CmsBusinesslayer objService = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    int intDeptId = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["Deptid"].ToString() == null || Request.QueryString["Deptid"].ToString() == "")
            {
                Response.Redirect("Default.aspx");
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ServiceDetailPage");
            Response.Redirect("Default.aspx");
        }

        if (!IsPostBack)
        {
            try
            {
                string straction = "S";
                intDeptId = Convert.ToInt32(Request.QueryString["Deptid"].ToString());
                DataTable dtnew = objService.BindServiceData(straction, Convert.ToInt32(intDeptId));

                if (intDeptId.ToString() == "8")
                {
                    dtnew.Rows.RemoveAt(1);
                }
                else if (intDeptId.ToString() == "659")
                {
                    DataRow dr = dtnew.NewRow();
                    dr["INT_SERVICEID"] = "51";
                    dr["VCH_SERVICENAME"] = "Road cutting request form";
                    dr["nvchLevelName"] = "IDCO";
                    dr["INT_CategoryType"] = "1";
                    dtnew.Rows.Add(dr);
                }
                else if (intDeptId.ToString() == "12")
                {
                    DataRow dr = dtnew.NewRow();
                    dr["INT_SERVICEID"] = "29";
                    dr["VCH_SERVICENAME"] = "Obtaining water connection";
                    dr["nvchLevelName"] = "Department of Water Resources";
                    dr["INT_CategoryType"] = "2";
                    dtnew.Rows.Add(dr);
                    dtnew.Rows.RemoveAt(0);
                }
                else if (intDeptId.ToString() == "877")
                {
                    DataRow dr = dtnew.NewRow();
                    dr["INT_SERVICEID"] = "51";
                    dr["VCH_SERVICENAME"] = "Road cutting request form";
                    dr["nvchLevelName"] = "Rural Development";
                    dr["INT_CategoryType"] = "1";
                    dtnew.Rows.Add(dr);
                }
                else if (intDeptId.ToString() == "878")
                {
                    DataRow dr = dtnew.NewRow();
                    dr["INT_SERVICEID"] = "51";
                    dr["VCH_SERVICENAME"] = "Road cutting request form";
                    dr["nvchLevelName"] = "Works Department";
                    dr["INT_CategoryType"] = "1";
                    dtnew.Rows.Add(dr);
                }
                else if (intDeptId.ToString() == "5")
                {
                    DataRow dr = dtnew.NewRow();
                    dr["INT_SERVICEID"] = "51";
                    dr["VCH_SERVICENAME"] = "Road cutting request form";
                    dr["nvchLevelName"] = "Housing and Urban Development Department (H&UD)";
                    dr["INT_CategoryType"] = "1";
                    dtnew.Rows.Add(dr);
                }

                //Label1.Text = Session["Department"].ToString();
                Label1.Text = Request.QueryString["Department"].ToString();

                if (dtnew.Rows.Count > 0)
                {
                    for (int i = 0; i < dtnew.Rows.Count; i++)
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        Label lsinew = new Label();
                        li.Attributes.Add("class", "plDIndustries");
                        oldeptid.Controls.Add(li);
                        HtmlGenericControl anchor = new HtmlGenericControl("a");
                        if (Request.QueryString["Srvcid"].ToString() != dtnew.Rows[i]["INT_SERVICEID"].ToString())
                        {
                            //anchor.Attributes.Add("href", "ServiceDetails.aspx?Srvcid=" + dtnew.Rows[i]["INT_SERVICEID"] + "");
                            anchor.Attributes.Add("href", "ServiceDetails.aspx?Deptid=" + intDeptId + "&Department=" + Request.QueryString["Department"].ToString() + "&Srvcid=" + dtnew.Rows[i]["INT_SERVICEID"] + "");
                            anchor.Attributes.Add("target", "");
                            anchor.Attributes.Add("title", "" + dtnew.Rows[i]["VCH_SERVICENAME"] + "");
                        }
                        else
                        {
                            anchor.Attributes.Add("title", "" + dtnew.Rows[i]["VCH_SERVICENAME"] + "");
                            li.Attributes.Add("class", "active");
                        }

                        lsinew.Text = "" + dtnew.Rows[i]["VCH_SERVICENAME"] + "";
                        anchor.Controls.Add(lsinew);
                        li.Controls.Add(anchor);
                    }
                }
                else
                {
                    oldeptid.Visible = false;
                }

                string stractionnew = "BS";
                int intservcid = Convert.ToInt32(Request.QueryString["Srvcid"]);
                DataTable dt = objService.BindServiceData(stractionnew, intservcid);
                //Label lblhead = new Label();
                //lblhead.Text = "" + dt.Rows[0]["VCH_SERVICENAME"] + "";         

                //HtmlGenericControl li = new HtmlGenericControl("li");
                //Label lsi = new Label();
                //li.Attributes.Add("class", "plSWClearance");
                //hservid.Controls.Add(li);
                //HtmlGenericControl anchor = new HtmlGenericControl("a");
                //anchor.Attributes.Add("href", "#");
                //anchor.Attributes.Add("target", "");
                ////anchor.Attributes.Add("title", "" + dt.Rows[i]["VCH_SERVICENAME"] + "");
                //lsi.Text = "" + dt.Rows[i]["VCH_SERVICENAME"] + "";
                //anchor.Controls.Add(lsi);
                //li.Controls.Add(anchor);
                if (dt.Rows.Count > 0)
                {
                    Label lblhead = new Label();
                    lblhead.Text = "" + dt.Rows[0]["VCH_SERVICENAME"] + "";
                    hservid.Controls.Add(lblhead);
                    string sValueTobeShowninDiv = dt.Rows[0]["VCHCONTENT"].ToString();
                    divid.InnerHtml = sValueTobeShowninDiv;
                    HyprLnk.NavigateUrl = dt.Rows[0]["vchUserManual"].ToString();
                }
                else
                {
                    Label lblhead = new Label();
                    lblhead.Text = "";
                    hservid.Controls.Add(lblhead);
                }

                // HyperLink1.NavigateUrl = "InspectionConstruction.aspx?Srvcid=" + Convert.ToInt32(Request.QueryString["Srvcid"]);
                // HyperLink2.NavigateUrl = "OccupancyCertificate.aspx?Srvcid=" + Convert.ToInt32(Request.QueryString["Srvcid"]);
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
                Util.LogError(ex, "ServiceDetailpage");
                Response.Redirect("Default.aspx");
            }
            if (Request.QueryString["Srvcid"] != null)
            {
                lblCount.Text = GetApplicationCount(Request.QueryString["Srvcid"].ToString());
            }
        }
    }

    string GetApplicationCount(string strServiceId)
    {
        string strQuery = "";
        string strCount = "0";
        if (strServiceId == "1053")
        {
            strQuery = "Select count(1) as TotalApplication from T_PEAL_PROMOTER where bitDeletedStatus=0 and intpaymentStatus=1";
        }
        else
        {
            //strQuery = "Select count(1) as TotalApplication from V_DASHBORD_CLEARANCE v inner join M_SERVICEMASTER_TBL S on S.INT_SERVICEID=v.INT_SERVICEID inner join M_INVESTOR_DETAILS C on v.INVESTORID=C.INT_INVESTOR_ID where  S.INT_DELETED_FLAG=0   and C.BIT_DELETED_FLAG=0 and (" + strServiceId + " =0 OR v.INT_SERVICEID=" + strServiceId + ") and (" + Session["deptid"].ToString() + " =0 OR DeptId=" + Session["deptid"].ToString() + ")";
            strQuery = "Select count(1) as TotalApplication from V_DASHBORD_CLEARANCE v inner join M_SERVICEMASTER_TBL S on S.INT_SERVICEID=v.INT_SERVICEID inner join M_INVESTOR_DETAILS C on v.INVESTORID=C.INT_INVESTOR_ID where  S.INT_DELETED_FLAG=0   and C.BIT_DELETED_FLAG=0 and (" + strServiceId + " =0 OR v.INT_SERVICEID=" + strServiceId + ") and (" + intDeptId.ToString() + " =0 OR DeptId=" + intDeptId.ToString() + ")";
            //strQuery = "Select count(1) as TotalApplication from T_APPLICATION_TBL where INT_SERVICEID=" + strServiceId;
        }

        SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        SqlCommand cmd = new SqlCommand(strQuery, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0][0] != DBNull.Value)
            {
                strCount = ds.Tables[0].Rows[0][0].ToString();
            }
        }
        return strCount;
    }
    protected void btnapply_Click(object sender, EventArgs e)
    {
        Response.Redirect("CheckProposal.aspx?id=" + Request.QueryString["Srvcid"] + "");

        //if (Session["InvestorId"] != null)
        //{
        //    Response.Redirect("CheckProposal.aspx?id=" + Request.QueryString["Srvcid"] + "");
        //}
        //else
        //{
        //    Server.Transfer("inestorlogin.aspx?serviceid=" + Request.QueryString["Srvcid"] + "");
        //}
    }
}