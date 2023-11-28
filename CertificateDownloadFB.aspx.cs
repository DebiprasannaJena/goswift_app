//******************************************************************************************************************
// File Name             :   DepartmentClearance.aspx.cs
// Description           :   Show the clearance details of Investors from various departments
// Created by            :   AMit Sahoo
// Created on            :   30th June 2017
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
using System.Data;
using DataAcessLayer.Service;
using EntityLayer.Service;
using BusinessLogicLayer.Service;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CertificateDownloadFB : System.Web.UI.Page
{
    #region Variables
    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    ServiceDetails objServiceEntity = new ServiceDetails();
    DataTable dtable;
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    string ApplicationPath = System.Configuration.ConfigurationManager.AppSettings["ApplicationPath"];
    DataSet ds;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        
            BindGrid();
        
        
    }
    private void BindGrid()
    {


        
        DataTable PrpDt = new DataTable();
        string servicename = "";
        string factoryname = "";
        servicename = ddlServicename.SelectedItem.Text;
        factoryname = ddlFactname.SelectedItem.Text;
        DataTable AmntTbl = new DataTable();
        string strAmount = "";
        string strInsert = "";
        if (servicename != "--Select--")
        {
            strInsert = "SELECT FACTORYNAME,SERVICENAME,CERTIFICATEURL FROM M_SWP_FB_CERTIFICATE WHERE FACTORYNAME='" + factoryname + "' AND SERVICENAME='" + servicename + "' order by FACTORYNAME";
        }
        else
        {
            strInsert = "SELECT FACTORYNAME,SERVICENAME,CERTIFICATEURL FROM M_SWP_FB_CERTIFICATE WHERE FACTORYNAME='" + factoryname + "'  order by FACTORYNAME";
        }
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand(strInsert, con);
        AmntTbl.Load(cmd.ExecuteReader());
        gvDeptClearance.DataSource = AmntTbl;
        gvDeptClearance.DataBind();



    }
    protected void gvDeptClearance_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[0].Text = ((gvDeptClearance.PageIndex * gvDeptClearance.PageSize) + e.Row.RowIndex + 1).ToString();

            HyperLink hyRcvd = (HyperLink)e.Row.FindControl("hypApply");
            string strtype;
            string strProposalId;

            hyRcvd.NavigateUrl = gvDeptClearance.DataKeys[e.Row.RowIndex].Values[0].ToString();//"ServiceInstruction.aspx?FormId=" + gvDeptClearance.DataKeys[e.Row.RowIndex].Values[0] + "&ProposalNo=" + strProposalId + "&Amount=" + gvDeptClearance.DataKeys[e.Row.RowIndex].Values[5] + "&ServiceType=" + gvDeptClearance.DataKeys[e.Row.RowIndex].Values[3] + "&type=" + strtype;
            hyRcvd.Target = "_blank";
        }



    }
    protected void gvDeptClearance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDeptClearance.PageIndex = e.NewPageIndex;
        BindGrid();
    }
    
    protected void btnApply_Click(object sender, EventArgs e)
    {
        BindGrid();
    }
}