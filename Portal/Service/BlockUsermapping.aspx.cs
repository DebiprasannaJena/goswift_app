using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;

using EntityLayer.Service;
using EntityLayer.Proposal;
using System.IO;
using BusinessLogicLayer.Service;
using BusinessLogicLayer.Proposal;

public partial class Portal_Service_BlockUsermapping : System.Web.UI.Page
{
    #region Variables
    ServiceDetails objService1 = new ServiceDetails();
    public string strManageRight = "";
    public int intLevelDetailId;
    //string strUserId, strPassword, strRandomPassword;
    #endregion
    DataTable dtable;
    DataSet ds = new DataSet();
    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillLocation();
            BindDistrict();
          
        }
    }
    private void FillLocation()
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        objServicelist = objService.BindLocation("L").ToList();
        ddlLocation.DataSource = objServicelist;
        ddlLocation.DataTextField = "StrLocationName";
        ddlLocation.DataValueField = "LocationId";
        ddlLocation.DataBind();
        ddlLocation.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    
    private void BindDistrict()
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objservice = new ProposalBAL();
        objProp.strAction = "DT";
        objProp.vchProposalNo = " ";
        objProjList = objservice.PopulateProjDropdowns(objProp).ToList();

        ddldist.DataSource = objProjList;
        ddldist.DataTextField = "vchDistName";
        ddldist.DataValueField = "intDistId";
        ddldist.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddldist.Items.Insert(0, list);

    }
    private void BindBlock(string strdist)
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objservice = new ProposalBAL();
        objProp.strAction = "BL";
        objProp.vchProposalNo = strdist;
        objProjList = objservice.PopulateProjDropdowns(objProp).ToList();

        lstBlock.DataSource = objProjList;
        lstBlock.DataTextField = "vchBlockName";
        lstBlock.DataValueField = "intBlockId";
        lstBlock.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        lstBlock.Items.Insert(0, list);
    }


       [WebMethod]
    public static List<ListItem> FillDepartment(string id)
    {
        string query = "SELECT INTLEVELDETAILID,NVCHLEVELNAME FROM [M_ADM_LEVELDETAILS] WHERE INTPARENTID='" + id + "' AND BITSTATUS=1";
        string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                List<ListItem> branches = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        branches.Add(new ListItem
                        {
                            Value = sdr["INTLEVELDETAILID"].ToString(),
                            Text = sdr["NVCHLEVELNAME"].ToString()
                        });
                    }
                }
                con.Close();
                return branches;
            }
        }
    }
    [WebMethod]
    public static List<ListItem> FillDirectorate(string id)
    {
        string query = "SELECT INTLEVELDETAILID,NVCHLEVELNAME FROM [M_ADM_LEVELDETAILS] WHERE INTPARENTID='" + id + "' AND BITSTATUS=1";
        string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                List<ListItem> branches = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        branches.Add(new ListItem
                        {
                            Value = sdr["INTLEVELDETAILID"].ToString(),
                            Text = sdr["NVCHLEVELNAME"].ToString()
                        });
                    }
                }
                con.Close();
                return branches;
            }
        }
    }
    [WebMethod]
    public static List<ListItem> FillDivision(string id)
    {
        string query = "SELECT INTLEVELDETAILID,NVCHLEVELNAME FROM [M_ADM_LEVELDETAILS] WHERE INTPARENTID='" + id + "' AND BITSTATUS=1";
        string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                List<ListItem> branches = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        branches.Add(new ListItem
                        {
                            Value = sdr["INTLEVELDETAILID"].ToString(),
                            Text = sdr["NVCHLEVELNAME"].ToString()
                        });
                    }
                }
                con.Close();
                return branches;
            }
        }
    }
    [WebMethod]
    public static List<ListItem> FillDistrict(string id)
    {
        string query = "SELECT INTLEVELDETAILID,NVCHLEVELNAME FROM [M_ADM_LEVELDETAILS] WHERE INTPARENTID='" + id + "' AND BITSTATUS=1";
        string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                List<ListItem> branches = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        branches.Add(new ListItem
                        {
                            Value = sdr["INTLEVELDETAILID"].ToString(),
                            Text = sdr["NVCHLEVELNAME"].ToString()
                        });
                    }
                }
                con.Close();
                return branches;
            }
        }
    }
    [WebMethod]
    public static List<ListItem> FillUser(string id)
    {
        string query = "SELECT INTUSERID,vchFullName FROM M_POR_USER WHERE INTLEVELDETAILID='" + id + "' AND BITSTATUS=1";
        string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                List<ListItem> branches = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        branches.Add(new ListItem
                        {
                            Value = sdr["INTUSERID"].ToString(),
                            Text = sdr["vchFullName"].ToString()
                        });
                    }
                }
                con.Close();
                return branches;
            }
        }
    }
    [WebMethod]
    public static List<ProjectInfo> FillBlock(string id)
    {
        List<ProjectInfo> objProjList = new List<ProjectInfo>();
        ProjectInfo objProp = new ProjectInfo();
        ProposalBAL objservice = new ProposalBAL();
        objProp.strAction = "BL";
        objProp.vchProposalNo = id;
        objProjList = objservice.PopulateProjDropdowns(objProp).ToList();
        return objProjList;
    }

   
    [WebMethod]
    public static List<ListItem> FillDesignation()
    {
        string query = "select intDesigId,nvchDesigName from M_ADM_Designation where bitStatus=1";
        string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
                List<ListItem> branches = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        branches.Add(new ListItem
                        {
                            Value = sdr["intDesigId"].ToString(),
                            Text = sdr["nvchDesigName"].ToString()
                        });
                    }
                }
                con.Close();
                return branches;
            }
        }
    }


    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        Button btnsave = (Button)sender;
        int count = 0;
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        ServiceDetails objService1 = new ServiceDetails();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        DataTable dtbTemp = new DataTable();   
        dtbTemp.Columns.Add("DEPTID");
        dtbTemp.Columns.Add("LOCATIONID");
        dtbTemp.Columns.Add("DirectId");
        dtbTemp.Columns.Add("DivisionId");
        dtbTemp.Columns.Add("DISTID");
        dtbTemp.Columns.Add("USERID");
       
        dtbTemp.Columns.Add("userdist");
        dtbTemp.Columns.Add("userblock");
        string str = "";
        ListBox lstBlockadd1 = (ListBox)btnsave.FindControl("lstBlockadd");
        foreach (ListItem liItem in lstBlockadd1.Items)
        {
        DataRow dtrTemp = dtbTemp.NewRow();
        dtrTemp["DEPTID"] = ddlDepartment.SelectedValue;
            dtrTemp["LOCATIONID"] = ddlLocation.SelectedValue;
            dtrTemp["DirectId"] = ddldirectorate.SelectedValue;
            dtrTemp["DivisionId"] = ddlDivision.SelectedValue;
            dtrTemp["DISTID"] = ddlDistrict.SelectedValue;
            dtrTemp["USERID"] = ddluser.SelectedValue;

            dtrTemp["userdist"] = ddldist.SelectedValue;
            dtrTemp["userblock"] = liItem.Value;          
            dtbTemp.Rows.Add(dtrTemp);
            }

        objService1.strAction = "B";      
        objService1.XMLDATA = GetSTRXMLResult(dtbTemp);
        objService.ServiceConfigurationData(objService1);      
        string rawURL = Request.RawUrl;
        string strShowMsg = "Data Saved SuccessFully!";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false);
    }
    public static string GetSTRXMLResult(DataTable dtTable)
    {
        string strXMLResult = "";
        if ((dtTable != null))
        {
            if (dtTable.Rows.Count > 0)
            {
                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw);
                strXMLResult = sw.ToString();
                sw.Close();
                sw.Dispose();
            }
        }
        return strXMLResult;
    }
}