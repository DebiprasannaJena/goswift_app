using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAcessLayer.Service;
using System.Web.Services;
using BusinessLogicLayer.Proposal;
using System.IO;
using BusinessLogicLayer.Service;
using EntityLayer.Service;
using EntityLayer.Proposal;
using System.Web.UI.HtmlControls;
//using TradeLicenseSrvc;
using Ionic.Zip;

public partial class Portal_Service_ApplicationStatusDetails : System.Web.UI.Page
{
    # region variables Declaration
    #region Variables
    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    ServiceDetails objServiceEntity = new ServiceDetails();
    ServiceStatus objServcStatus = new ServiceStatus();
    DataTable dtable;
    DataSet ds;
    static string Str_UsrName = "";
    #endregion
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["UserId"] != null)
            {
                Str_UsrName = Session["UserId"].ToString();
                BindStatusData();
                BindQueryTable();
                ShowHideButton();
                
            }
            else
            {
                Response.Redirect("~/inestorlogin.aspx");
            }
        }

    }
    public void BindStatusData()
    {
        try
        {
            objServiceEntity.str_ApplicationNo = Request.QueryString["ApplicationNo"].ToString();
            objServiceEntity.strInvesterName = Session["UserId"].ToString();
            if (Request.QueryString["type"].ToString() != "") //If Redirect comes from ViewServiceApplication.aspx (Portal/Service)
            {
                if (Request.QueryString["type"].ToString() == "S")
                {
                    objServiceEntity.Typeid = "S";
                }
            }
            List<ServiceDetails> ServiceDetail = objService.GetParticularApplicationDetails(objServiceEntity).ToList();
            lblDepartmntName.Text = ServiceDetail[0].str_Department;
            lblServiceName.Text = ServiceDetail[0].str_ServicesName;
            lblApplicantName.Text = ServiceDetail[0].str_ApplicantName;
            lblApplicationNo.Text = ServiceDetail[0].str_ApplicationNo;
            lblApplicationStatus.Text = objServcStatus.CheckStatus(lblApplicationNo.Text, Convert.ToInt32(ServiceDetail[0].str_checkStatus));


        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
    [WebMethod]
    public static List<ProposalDet> ShowQuery(string id, string sid)
    {
        string gStrRetVal = null;
        int returnval = 0;
        ProposalBAL objService = new ProposalBAL();
        ProposalDet objProp = new ProposalDet();
        List<ProposalDet> objProposalList = new List<ProposalDet>();
        try
        {
            gStrRetVal = id;
            objProp.strAction = "E";
            objProp.strProposalNo = id;
            objProp.intNoOfTimes = Convert.ToInt32(sid);
            objProposalList = objService.ServicegetRaisedQueryDetails(objProp).ToList();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            //objLinc = null;
        }
        return objProposalList;
    }
    #region "Added By Pranay Kumar"

    #region Bind Query History Table"
    private void BindQueryTable()
    {
        List<ProposalDet> objProposalList = new List<ProposalDet>();
        ProposalBAL objService = new ProposalBAL();
        ProposalDet objProp = new ProposalDet();
        objProp.strAction = "QD";
        objProp.strProposalNo = Convert.ToString(Request.QueryString["ApplicationNo"]);
        objProposalList = objService.ServicegetRaisedQueryDetails(objProp).ToList();
        if (objProposalList.Count > 0)
        {
            //string strHTMlQuery1 = "<table class='table table-bordered table-hover'><tr><th>User Name</th><th> Query Details</th><th>Date</th><th>Files</th></tr>";
            //for (int i = 0; i < objProposalList.Count; i++)
            //{
            //    if (objProposalList[i].strFileName == null || objProposalList[i].strFileName == "")
            //    {
            //        strHTMlQuery1 = strHTMlQuery1 + "<tr><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a  href='#'>--</a>" + "</td></tr>";
            //    }
            //    else
            //    {
            //        strHTMlQuery1 = strHTMlQuery1 + "<tr><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a target='_blank' href='./QueryFiles/Services/" + objProposalList[i].strFileName + "'>Download</a>" + "</td></tr>";
            //    }
            //}
            //strHTMlQuery1 = strHTMlQuery1 + "</table>";
            //QueryHist1.InnerHtml = strHTMlQuery1;
            //string strHTMlQuery = "<table style='margin-left:60px;' <tbody>";
            //strHTMlQuery += "<tr><td class=''><div class='legendColorBox blue'></div></td><td class='legendLabel'>Raised </td><td width='10'>&nbsp;</td><td class=' '><div class='legendColorBox green'></div></td><td class='legendLabel'>Reverted</td><td width='10'>&nbsp;</td></tr>";
            //strHTMlQuery += "<tr><td height='10'>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td class='legendColorBox'> &nbsp;</td><td>&nbsp;</td><td>&nbsp; </td> <td class='legendColorBox'> &nbsp;</td></tr>";
            //strHTMlQuery += "</tbody></table>";
            //strHTMlQuery += "<div class='messagebox'>";
            string strHTMlQuery = "<div class='messagebox'>";
            for (int i = 0; i < objProposalList.Count; i++)
            {

                if (i % 2 == 0) // If Odd then bind data
                {
                    strHTMlQuery += "<div class='itemdiv dialogdiv'><div class='user'><img src='images/user.png' alt='user img'></div>";
                }
                else
                {
                    strHTMlQuery += "<div class='itemdiv dialogdiv msgright'>";
                }
                strHTMlQuery += "<div class='body'><div class='time'><i class='ace-icon fa fa-calendar'></i>" + objProposalList[i].dtmCreatedOn + "</div>";
                strHTMlQuery += "<div class='name'><a href='#'>" + objProposalList[i].strActionToBeTakenBY + "</a></div>";
                strHTMlQuery += "<div class='form-sec '><div class='form-header'> <div class='pull-left'><table><tr><td>";
                if (i % 2 == 0) // If Even then bind Raised data
                {
                    strHTMlQuery += "<div class='legendColorBox blue'></div></td><td>Raised</td><td></td>";
                }
                else    // If Odd then bind Reverted data
                {
                    strHTMlQuery += "<div class='legendColorBox green'></div></td><td>Reverted</td><td></td>";
                }
                strHTMlQuery += "</tr></table></div>";
                strHTMlQuery += "<div class='pull-right'>";
                if (objProposalList[i].strFileName == null || objProposalList[i].strFileName == "")
                {
                    strHTMlQuery += "<a href='#' class='btn btn-info btn-sm'>--</a>";
                }
                else
                {
                    strHTMlQuery += "<a target='_blank' href='../../QueryFiles/Services/" + objProposalList[i].strFileName + "' class='btn btn-info btn-sm'><i class='fa fa-download'></i></a>";
                }
                strHTMlQuery += "</div><div class='clearfix'></div></div><div class='form-body'>" + objProposalList[i].strRemarks + "</div>";
                if (i % 2 != 0) // for reverted query bind add more concept
                {

                    List<ProposalDet> objProposalList1 = new List<ProposalDet>();
                    objProp.strAction = "QF";
                    objProp.intQueryId = objProposalList[i].intQueryId;
                    objProposalList1 = objService.ServicegetRaisedQueryDetails(objProp).ToList();// FOR ADD MORE FILES AND DESCRIPTION
                    if (objProposalList1.Count > 0)
                    {
                        strHTMlQuery += " <table class='table table-bordered table-hover'><tr><th>File Description</th><th width='60px'>Download</th></tr>";
                        for (int j = 0; j < objProposalList1.Count; j++)
                        {
                            strHTMlQuery += "<tr><td>" + objProposalList1[j].strRemarks + "</td><td class='text-center'>";

                            if (objProposalList1[j].strFileName == null || objProposalList1[j].strFileName == "")
                            {
                                strHTMlQuery += "<a href='#' class='btn btn-info btn-sm'>--</a>";
                            }
                            else
                            {
                                strHTMlQuery += "<a target='_blank' href='../../QueryFiles/Services/" + objProposalList1[j].strFileName + "' class='btn btn-info btn-sm'><i class='fa fa-download'></i></a></td></tr>";
                            }

                        }
                        strHTMlQuery += "</table>";
                    }

                }
                strHTMlQuery += "</div></div>";
                if (i % 2 != 0) // If Odd then bind data
                {
                    strHTMlQuery += "<div class='user'><img src='images/user.png' alt='user img'></div>";
                }
                strHTMlQuery += "</div>";
            }
            strHTMlQuery += "</div>";
            QueryHist.InnerHtml = strHTMlQuery;
        }

    }
    private void ShowHideButton()
    {
        ProposalBAL objService = new ProposalBAL();
        ProposalDet objProp = new ProposalDet();
        List<ProposalDet> objProposalList = new List<ProposalDet>();
        try
        {
            objProp.strAction = "SH";
            objProp.strProposalNo = Convert.ToString(Request.QueryString["ApplicationNo"]);
            objProposalList = objService.ServicegetRaisedQueryDetails(objProp).ToList();
            if (objProposalList.Count > 0)
            {
                int intQueryStatus = Convert.ToInt32(objProposalList[0].intQueryStatus);
                int intExtendedStatus = Convert.ToInt32(objProposalList[0].intExtendedStatus);
                
                lblQueryStatus.Text = Convert.ToString(objProposalList[0].strStatus);
                if (lblQueryStatus.Text == "--")
                {
                    dvQueryMain.Visible = false;
                    dvQueryMain1.Visible = true;
                }
                else
                {
                    dvQueryMain1.Visible = false;
                    dvQueryMain.Visible = true;
                }
               

            }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        finally
        {
            objProp = null;
        }

    }
    #endregion
   
    #region "ZIP DOWNLOAD"
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName("QueryFiles");
            if (hdnFileNames.Value != "")
            {
                string[] arrFileName = hdnFileNames.Value.Split(',');
                for (int i = 0; i <= arrFileName.Count() - 1; i++)
                {
                    string FileName = "../../QueryFiles/Services/" + Convert.ToString(arrFileName[i]);
                    string filePath = Server.MapPath(FileName);
                    zip.AddFile(filePath, "QueryFiles");
                }
            }
            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("QueryFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            zip.Save(Response.OutputStream);
            Response.End();
        }
    }
    #endregion
    #endregion
}