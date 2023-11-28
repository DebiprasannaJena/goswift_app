using BPAS;
using BusinessLogicLayer.CMS;
using BusinessLogicLayer.Service;
using EntityLayer.CMS;
using EntityLayer.Proposal;
using EntityLayer.Service;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ServiceProcess : System.Web.UI.Page
{
    #region Globalvariable
    /// <summary>
    /// Prasun Kali
    /// All global variable are declared here
    /// </summary>
    string str_FormId = "";
    int Int_ServiceType = 0;
    string str_ServiceType = "";
    string str_ProposalNo = "";
    string str_Amount = "0";
    string str_RequestMode = "";
    CmsBusinesslayer objService = new CmsBusinesslayer();
    List<EntityLayer.CMS.CMSDetails> onjentity = new List<EntityLayer.CMS.CMSDetails>();
    SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
    UserDetail _objUser = new UserDetail();
    ExternalServiceIntegration.Serviceinfo objservice = new ExternalServiceIntegration.Serviceinfo();
    ExternalServiceIntegration objSvc = new ExternalServiceIntegration();
    List<ExternalServiceIntegration.Serviceinfo> objServicelist = new List<ExternalServiceIntegration.Serviceinfo>();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["InvestorId"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        GetNavigation();
    }

    public void GetNavigation()
    {
        myNavbar.Visible = false;

        if (Request.QueryString["ReqMode"] != "" && Request.QueryString["ReqMode"] != null)
        {
            str_RequestMode = Request.QueryString["ReqMode"].ToString();

            //if (str_RequestMode == "S")
            //{
            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("intSlNo", typeof(int));
            //    dt.Columns.Add("intServiceId", typeof(string));
            //    dt.Columns.Add("vchFormName", typeof(string));
            //    dt.Columns.Add("vchServiceName", typeof(string));
            //    dt.Columns.Add("intServiceType", typeof(string));
            //    dt.Columns.Add("intExternalType", typeof(string));
            //    dt.Columns.Add("vchProposalNo", typeof(string));
            //    dt.Columns.Add("decAmount", typeof(string));
            //    dt.Columns.Add("intCompletedStatus", typeof(string));
            //    dt.Columns.Add("vchApplicationKey", typeof(string));
            //    dt.Columns.Add("vchUrl", typeof(string));
            //    dt.Columns.Add("vchUpdateUrl", typeof(string));
            //    dt.Columns.Add("vchDeptName", typeof(string));
            //    dt.Columns.Add("intHoaAccount", typeof(string));
            //    dt.Columns.Add("vchTrackingId", typeof(string));

            //    string strProposalNo = Request.QueryString["ProposalNo"];
            //    int intServiceType = 0;
            //    int intServiceId = Convert.ToInt32(Request.QueryString["FormId"].ToString());
            //    decimal decAmount = 0;
            //    int intExternalType = 0;
            //    int intCompletedStatus = 0;
            //    string vchurl = string.Empty;
            //    string strServiceName = string.Empty;
            //    string vchApplicationKey = Request.QueryString["AppKey"];
            //    string GroupKey = Request.QueryString["GroupKey"];
            //    string strDeptName = "";

            //    if (vchApplicationKey == "" || vchApplicationKey == null)// get service list and filter service id wise.
            //    {
            //        BusinessLogicLayer.Service.ServiceBusinessLayer objService = new BusinessLogicLayer.Service.ServiceBusinessLayer();
            //        EntityLayer.Service.ServiceDetails objServiceEntity = new EntityLayer.Service.ServiceDetails();
            //        objServiceEntity.intProposalId = strProposalNo;
            //        List<EntityLayer.Service.ServiceDetails> ServiceDetail = objService.ViewDepartmentWiseServiceDetails(objServiceEntity).ToList();

            //        List<EntityLayer.Service.ServiceDetails> ObjInternalSvc = ServiceDetail.Where(n => n.intServiceId == intServiceId).ToList();
            //        if (ObjInternalSvc != null)
            //        {
            //            foreach (var ObjInternal in ObjInternalSvc)
            //            {
            //                intExternalType = ObjInternal.intExternalType;
            //                vchurl = ObjInternal.Str_ExtrnalServiceUrl;
            //                strServiceName = ObjInternal.strServiceName;
            //                intServiceType = ObjInternal.Int_ServiceType;
            //                decAmount = ObjInternal.Dec_Amount;
            //                vchApplicationKey = ObjInternal.str_ApplicationNo;
            //                intServiceId = ObjInternal.intServiceId;
            //            }
            //            int intFormCount = 1;

            //            string strFormName = strServiceName.Length > 20 ? strServiceName.Substring(0, 20) + "..." : strServiceName;

            //            dt.Rows.Add(intFormCount, intServiceId, strFormName, strServiceName, intServiceType, intExternalType, strProposalNo, decAmount, intCompletedStatus, vchApplicationKey, vchurl, "", "", 0, GroupKey);
            //        }
            //        Session["SvcMasterData"] = dt;
            //    }
            //    else
            //    {
            //        // get service list and filter service id wise from generated/drafted service application list
            //        ServiceBusinessLayer objService = new ServiceBusinessLayer();
            //        ProposalDet objProposal = new ProposalDet();
            //        List<ProposalDet> objProposalList = new List<ProposalDet>();
            //        ProposalDet objProp = new ProposalDet();
            //        objProp.strAction = "D";
            //        objProp.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            //        List<ServiceDetails> ServiceDetail = objService.GetAllDraftedApplicationDetails(Session["UserId"].ToString()).ToList();

            //        if (GroupKey != "" && GroupKey != null)
            //        {
            //            List<EntityLayer.Service.ServiceDetails> ObjInternalSvc = ServiceDetail.Where(n => n.vchTranscationNo == GroupKey).ToList();
            //            if (ObjInternalSvc.Count > 0)
            //            {
            //                foreach (var ObjInternal in ObjInternalSvc)
            //                {
            //                    intExternalType = ObjInternal.intExternalType;
            //                    vchurl = ObjInternal.Str_ExtrnalServiceUrl;
            //                    strServiceName = ObjInternal.str_ServicesName;
            //                    intServiceType = ObjInternal.Int_ServiceType;
            //                    decAmount = ObjInternal.Dec_Amount;
            //                    vchApplicationKey = ObjInternal.str_ApplicationNo;
            //                    intServiceId = ObjInternal.intServiceId;
            //                    strDeptName = ObjInternal.str_Department;

            //                    if (intExternalType == 1 && intServiceType == 0)
            //                    {
            //                        string strRes = "SELECT ApplicationNo from T_CMN_FIN_DETAILS_LOG WHERE  ApplicationNo ='" + vchApplicationKey + "'";

            //                        SqlCommand cmd = new SqlCommand(strRes, conn);
            //                        SqlDataAdapter da = new SqlDataAdapter(cmd);
            //                        DataSet ds = new DataSet();
            //                        da.Fill(ds);

            //                        if (ds.Tables[0].Rows.Count > 0)
            //                        {
            //                            intCompletedStatus = 1;
            //                        }
            //                    }
            //                    else
            //                    {
            //                        intCompletedStatus = 1;
            //                    }


            //                    int intFormCount = 1;

            //                    string strFormName = strServiceName.Length > 20 ? strServiceName.Substring(0, 20) + "..." : strServiceName;

            //                    dt.Rows.Add(intFormCount, intServiceId, strFormName, strServiceName, intServiceType, intExternalType, strProposalNo, decAmount, intCompletedStatus, vchApplicationKey, vchurl, "", strDeptName, 0, GroupKey);
            //                }
            //            }
            //            Session["SvcMasterData"] = dt;
            //        }
            //        else
            //        {
            //            // check application head of account payment update details and set completed status accordingly  
            //            EntityLayer.Service.ServiceDetails ObjInternalSvc = ServiceDetail.Where(n => n.intServiceId == intServiceId && n.str_ApplicationNo == vchApplicationKey).FirstOrDefault();
            //            if (ObjInternalSvc != null)
            //            {
            //                intExternalType = ObjInternalSvc.intExternalType;
            //                vchurl = ObjInternalSvc.Str_ExtrnalServiceUrl;
            //                strServiceName = ObjInternalSvc.str_ServicesName;
            //                intServiceType = ObjInternalSvc.Int_ServiceType;
            //                decAmount = ObjInternalSvc.Dec_Amount;
            //                vchApplicationKey = ObjInternalSvc.str_ApplicationNo;
            //                strDeptName = ObjInternalSvc.str_Department;

            //                if (intExternalType == 1 && intServiceType == 0)
            //                {
            //                    string strRes = "SELECT ApplicationNo from T_CMN_FIN_DETAILS_LOG WHERE  ApplicationNo ='" + vchApplicationKey + "'";

            //                    SqlCommand cmd = new SqlCommand(strRes, conn);
            //                    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //                    DataSet ds = new DataSet();
            //                    da.Fill(ds);

            //                    if (ds.Tables[0].Rows.Count > 0)
            //                    {
            //                        intCompletedStatus = 1;
            //                    }
            //                }
            //                else
            //                {
            //                    intCompletedStatus = 1;

            //                }

            //            }
            //            int intFormCount = 1;

            //            string strFormName = strServiceName.Length > 20 ? strServiceName.Substring(0, 20) + "..." : strServiceName;

            //            dt.Rows.Add(intFormCount, intServiceId, strFormName, strServiceName, intServiceType, intExternalType, strProposalNo, decAmount, intCompletedStatus, vchApplicationKey, vchurl, "", strDeptName, 0, GroupKey);
            //            Session["SvcMasterData"] = dt;
            //        }
            //    }
            //}

            /*----------------------------------------------------------------------------*/
            /// The below section will be executed when the request come for multiple services.
            /*----------------------------------------------------------------------------*/

            if (Request.QueryString["ReqMode"] == "M" || Request.QueryString["ReqMode"] == "S") //// Multiple Services
            {
                if (Session["SvcMasterData"] != null)
                {
                    int paycnt = 0;

                    DataTable dt = (DataTable)Session["SvcMasterData"];
                    string strAppKey = string.Empty;
                    string strAppKeyS = string.Empty;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["vchApplicationKey"].ToString() != "")
                        {
                            strAppKey += "'" + Convert.ToString(dt.Rows[i]["vchApplicationKey"]) + "',";
                            strAppKeyS += "" + Convert.ToString(dt.Rows[i]["vchApplicationKey"]) + ",";
                        }
                    }

                    /*-------------------------------------------------------------------*/

                    if (strAppKey.Length > 0)
                    {
                        strAppKey = strAppKey.Substring(0, strAppKey.Length - 1);
                        strAppKeyS = strAppKeyS.Substring(0, strAppKeyS.Length - 1);
                        string[] AppKey = strAppKeyS.Split(new char[] { ',' });

                        string strRes = "SELECT ApplicationNo FROM T_CMN_FIN_DETAILS_LOG WHERE ApplicationNo IN (" + strAppKey + ")";
                        SqlCommand cmd = new SqlCommand(strRes, conn);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i1 = 0; i1 < ds.Tables[0].Rows.Count; i1++)
                            {
                                DataRow drupdate = dt.AsEnumerable().Where(r => ((string)r["vchApplicationKey"]).Equals(ds.Tables[0].Rows[i1][0].ToString())).First();
                                drupdate["intCompletedStatus"] = 1;
                            }
                        }
                        else
                        {
                            for(int i2=0; i2< AppKey.Length;i2++) // add by anil  for show status 
                            {
                             DataRow drupdate = dt.AsEnumerable().Where(k => ((string)k["vchApplicationKey"]).Equals(AppKey[i2])).First();
                           // dt.Columns["intCompletedStatus"] = 1;
                            drupdate["intCompletedStatus"] = 1;
                                // dt.Rows.  drupdate["intCompletedStatus"] = 1;
                            }
                        }
                    }

                    /*-------------------------------------------------------------------*/

                    if (dt.Rows.Count > 0)
                    {
                        StringBuilder sbMenu = new StringBuilder();
                        sbMenu.Append("<ul class='custom-accordion' runat='server' id='ulmenuid'>");

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            int intSerialNo = Convert.ToInt32(dt.Rows[i]["intSlNo"]);
                            string strServiceId = Convert.ToString(dt.Rows[i]["intServiceId"]);
                            //string strFormName = Convert.ToString(dt.Rows[i]["vchFormName"]);
                            string strServiceName = Convert.ToString(dt.Rows[i]["vchServiceName"]);
                            int intServiceType = Convert.ToInt32(dt.Rows[i]["intServiceType"]);
                            int intExternalType = Convert.ToInt32(dt.Rows[i]["intExternalType"]);
                            string strProposalNo = Convert.ToString(dt.Rows[i]["vchProposalNo"]);
                            decimal decAmount = Convert.ToDecimal(dt.Rows[i]["decAmount"]);
                            int intCompletedStatus = Convert.ToInt32(dt.Rows[i]["intCompletedStatus"]);
                            string strApplicationKey = Convert.ToString(dt.Rows[i]["vchApplicationKey"]);
                            string vchUrl = Convert.ToString(dt.Rows[i]["vchUrl"]);

                            /*-------------------------------------------------------------------*/
                            ///URL Formation
                            /*-------------------------------------------------------------------*/
                            string actcls = string.Empty;
                            string strUrl = string.Empty;
                            string act = string.Empty;

                            if (intCompletedStatus == 1)
                            {
                                if (intExternalType == 1 && intServiceType == 0)
                                {
                                    string strUnqId = strApplicationKey;

                                    str_FormId = strServiceId;
                                    if (str_FormId.ToString() == "10")
                                    {
                                        string strReq1 = "Token=" + strUnqId + "&" + GetUserProfile();
                                        strReq1 = EncryptQueryString(strReq1);

                                        strUrl = vchUrl + "?" + strReq1;
                                    }
                                else if (str_FormId == "5" || str_FormId == "6" || str_FormId == "7" || str_FormId == "34" || str_FormId == "35" || str_FormId == "36" || str_FormId == "39" || str_FormId == "40" || str_FormId == "70" || str_FormId == "71" || str_FormId == "72") //FOR F&B and Labour Service add by anil
                                    {
                                        // string strData = PAReSHRAM();
                                        //  urlchk = ConfigurationManager.AppSettings["PARESHRAMREDIRECTIONURL"].ToString() + "?" + strData + "";
                                        // DataTable Dtdetails = GetInveserDeatils();
                                       // strUrl = Convert.ToString(dt.Rows[i]["vchUpdateUrl"]); // to send direct ULR with data on page refresh 
                                        DataTable Dtdetails = GetInveserDeatils(); // on url parameter   send to pareshram protal
                                        strUrl = vchUrl.Replace("{{servid}}", str_FormId.ToString()).Replace("{{prno}}", strProposalNo.ToString()).Replace("{{uid}}", Session["InvestorId"].ToString()).Replace("{{apkey}}", strUnqId).Replace("{{name}}", Dtdetails.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString()).Replace("{{mobileno}}", Dtdetails.Rows[0]["VCH_OFF_MOBILE"].ToString()).Replace("{{panno}}", Dtdetails.Rows[0]["VCH_PAN"].ToString());
                                    }
                                    else
                                    {
                                        strUrl = vchUrl.Replace("{{servid}}", str_FormId.ToString()).Replace("{{uid}}", Session["InvestorId"].ToString()).Replace("{{apkey}}", strApplicationKey).Replace("{{ssoid}}", Session["UID"].ToString()).Replace("{{prno}}", strProposalNo);
                                    }
                                }
                                else
                                {
                                    strUrl = "FormEditView.aspx?FormId=" + strServiceId + "&AppKey=" + strApplicationKey + "&ProposalNo=" + strProposalNo + "&ReqMode=" + Request.QueryString["ReqMode"].ToString();
                                }

                                actcls = " style='color: #397543!important;'";
                                act = "<div  runat='server' id=div" + strServiceId + " class='spanicon'><span class='spanicon'><i class='fa fa-check' aria-hidden='true'></i></span></div>";
                                paycnt++;
                            }
                            else
                            {
                                if (intExternalType == 1 && intServiceType == 0)
                                {
                                    if (strApplicationKey != "")
                                    {
                                        Session["ProposalNo"] = str_ProposalNo;
                                        str_FormId = strServiceId;
                                        if (str_FormId.ToString() == "10")
                                        {
                                            string strReq1 = "Token=" + strApplicationKey + "&" + GetUserProfile();
                                            strReq1 = EncryptQueryString(strReq1);

                                            strUrl = vchUrl + "?" + strReq1;
                                        }
                                        else if (str_FormId == "5" || str_FormId == "6" || str_FormId == "7" || str_FormId == "34" || str_FormId == "35" || str_FormId == "36" || str_FormId == "39" || str_FormId == "40" || str_FormId == "70" || str_FormId == "71" || str_FormId == "72") //FOR F&B and Labour Service add by anil
                                        {
                                            // string strData = PAReSHRAM();
                                            //  urlchk = ConfigurationManager.AppSettings["PARESHRAMREDIRECTIONURL"].ToString() + "?" + strData + "";
                                           // DataTable Dtdetails = GetInveserDeatils();
                                            strUrl = Convert.ToString(dt.Rows[i]["vchUpdateUrl"]); // to send direct ULR with data on page refresh 
                                        }
                                        else
                                        {
                                            strUrl = vchUrl.Replace("{{servid}}", str_FormId.ToString()).Replace("{{uid}}", Session["InvestorId"].ToString()).Replace("{{apkey}}", strApplicationKey).Replace("{{ssoid}}", Session["UID"].ToString()).Replace("{{prno}}", strProposalNo);
                                        }
                                    }
                                    else
                                    {
                                        strUrl = "ServiceInstruction.aspx?ReqMode=M&FormId=" + strServiceId + "&ServiceType=" + intServiceType + "&ProposalNo=" + strProposalNo + "&Amount=" + decAmount;
                                    }
                                }
                                else
                                {
                                    if (strApplicationKey != "")
                                    {
                                        strUrl = "FormEditView.aspx?FormId=" + strServiceId + "&AppKey=" + strApplicationKey + "&ProposalNo=" + strProposalNo + "&ReqMode=" + Request.QueryString["ReqMode"].ToString();
                                    }
                                    else
                                    {
                                        strUrl = "ServiceInstruction.aspx?ReqMode=M&FormId=" + strServiceId + "&ServiceType=" + intServiceType + "&ProposalNo=" + strProposalNo + "&Amount=" + decAmount;
                                    }
                                }
                                act = "<div  runat='server' id=div" + strServiceId + " class='spanicon pending'><span class='spanicon pending'><i class='fa fa-times' aria-hidden='true'></i> </span></div>";
                            }
                            //<span class='loader'><img src='images/loader-new.gif' alt='GO Swift'></span>


                            if (Request.QueryString["FormId"] == "" || Request.QueryString["FormId"] == null)//// When the request come for 1st time,this section will be executed.
                            {
                                if (intSerialNo == 1) //// Keep the TAB in active stage for service present on SlNo-1
                                {
                                    sbMenu.Append("<li><a  runat='server' id=" + strServiceId + " href ='javascript: void(0);' class='accordion-heading' data-url=\"" + strUrl + "\"><h5>" + strServiceName + "</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");
                                }
                                else
                                {
                                    sbMenu.Append("<li><a  runat='server' id=" + strServiceId + " href='javascript: void(0);' class='accordion-heading' data-url=\"" + strUrl + "\"><h5>" + strServiceName + "</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");
                                }
                                paycnt++;
                            }
                            else
                            {
                                if (Request.QueryString["FormId"] == strServiceId)
                                {
                                    sbMenu.Append("<li><a  runat='server' id=" + strServiceId + " href='javascript: void(0);' class='accordion-heading' data-url=\"" + strUrl + "\"><h5>" + strServiceName + "</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");
                                    hdnmenuactive.Value = strServiceId;
                                }
                                else
                                {
                                    sbMenu.Append("<li><a runat='server'  id=" + strServiceId + " href='javascript: void(0);' class='accordion-heading' data-url=\"" + strUrl + "\"><h5>" + strServiceName + "</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");
                                }
                                paycnt++;
                            }
                        }

                        if (paycnt > 0)
                        {
                            string strUrl = "ApplicationConsolidate.aspx";
                            sbMenu.Append("<li data-tooltip='Consolidated Payment'><a runat='server' id='six' href='javascript: void(0);'  class='accordion-heading' data-url=\"" + strUrl + "\"><h5>Consolidated Payment</h5><i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");
                        }

                        sbMenu.Append("</ul>");

                        myNavbar.InnerHtml = sbMenu.ToString();
                        myNavbar.Visible = true;
                    }
                }
            }
        }
    }

    #region GetNavigation

    [System.Web.Services.WebMethod]
    public static string getnavigations(string intServiceId)
    {
        //string navbar = "";
        StringBuilder navbar = new StringBuilder();
        StringBuilder sbMenu = new StringBuilder();

        try
        {
            /*----------------------------------------------------------------------------*/
            //// The below section will be executed when the request come for multiple services.
            /*----------------------------------------------------------------------------*/
            if (HttpContext.Current.Session["InvestorId"] == null)
            {
                return "404";
            }
            else if (HttpContext.Current.Session["SvcMasterData"] != null)
            {
                DataTable dt = (DataTable)HttpContext.Current.Session["SvcMasterData"];
                string strAppKey = string.Empty;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["vchApplicationKey"].ToString() != "" && dt.Rows[i]["intCompletedStatus"].ToString() != "1")
                    {
                        strAppKey += "'" + Convert.ToString(dt.Rows[i]["vchApplicationKey"]) + "',";
                    }
                }

                if (strAppKey.Length > 0)
                {
                    strAppKey = strAppKey.Substring(0, strAppKey.Length - 1);

                    string strRes = "SELECT ApplicationNo FROM T_CMN_FIN_DETAILS_LOG WHERE ApplicationNo IN (" + strAppKey + ")";
                    SqlConnection conn1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
                    SqlCommand cmd = new SqlCommand(strRes, conn1);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        for (int i1 = 0; i1 < ds.Tables[0].Rows.Count; i1++)
                        {
                            DataRow drupdate = dt.AsEnumerable().Where(r => ((string)r["vchApplicationKey"]).Equals(ds.Tables[0].Rows[i1][0].ToString())).First();
                            drupdate["intCompletedStatus"] = 1;

                            ServiceBusinessLayer objService = new BusinessLogicLayer.Service.ServiceBusinessLayer();
                            ServiceDetails objServiceEntity = new ServiceDetails();
                            objServiceEntity.intProposalId = drupdate["vchProposalNo"].ToString();

                            List<EntityLayer.Service.ServiceDetails> ServiceDetail = objService.ViewDepartmentWiseServiceDetails(objServiceEntity).ToList();
                            int intServiceIds = Convert.ToInt32(drupdate["intServiceId"]);
                            ServiceDetails ObjInternalSvc = ServiceDetail.Where(n => n.intServiceId == intServiceIds).FirstOrDefault();

                            if (ObjInternalSvc != null)
                            {
                                drupdate["decAmount"] = ObjInternalSvc.Dec_Amount;
                            }

                            string strServiceId = Convert.ToString(drupdate["intServiceId"]);
                            sbMenu.Append(strServiceId + "|");
                        }
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    int paycnt = 0;

                    //sbMenu.Append("<ul class='custom-accordion'>");
                    int count = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        int intSerialNo = Convert.ToInt32(dt.Rows[i]["intSlNo"]);
                        string strServiceId = Convert.ToString(dt.Rows[i]["intServiceId"]);
                       // string strFormName = Convert.ToString(dt.Rows[i]["vchFormName"]);
                        string strServiceName = Convert.ToString(dt.Rows[i]["vchServiceName"]);
                        int intServiceType = Convert.ToInt32(dt.Rows[i]["intServiceType"]);
                        int intExternalType = Convert.ToInt32(dt.Rows[i]["intExternalType"]);
                        string strProposalNo = Convert.ToString(dt.Rows[i]["vchProposalNo"]);
                        decimal decAmount = Convert.ToDecimal(dt.Rows[i]["decAmount"]);
                        int intCompletedStatus = Convert.ToInt32(dt.Rows[i]["intCompletedStatus"]);
                        string strApplicationKey = Convert.ToString(dt.Rows[i]["vchApplicationKey"]);
                        string vchUrl = Convert.ToString(dt.Rows[i]["vchUrl"]);

                        //// URL Formation 
                        string actcls = string.Empty;
                        string strUrl = string.Empty;
                        string act = string.Empty;
                        if (intCompletedStatus == 1)
                        {
                            if (intExternalType == 1 && intServiceType == 0)
                            {
                                string strUnqId = strApplicationKey;

                                strUrl = vchUrl.Replace("{{servid}}", strServiceId).Replace("{{uid}}", HttpContext.Current.Session["InvestorId"].ToString()).Replace("{{apkey}}", strUnqId).Replace("{{ssoid}}", HttpContext.Current.Session["UID"].ToString()).Replace("{{prno}}", strProposalNo);
                                sbMenu.Append(strServiceId + "|");


                                //DataTable Dtdetails = GetInveserDeatils();
                                //strUrl = vchUrl.Replace("{{servid}}", strServiceId.ToString()).Replace("{{prno}}", strProposalNo.ToString()).Replace("{{uid}}", HttpContext.Current.Session["InvestorId"].ToString()).Replace("{{apkey}}", strUnqId).Replace("{{name}}", Dtdetails.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString()).Replace("{{mobileno}}", Dtdetails.Rows[0]["VCH_OFF_MOBILE"].ToString()).Replace("{{panno}}", Dtdetails.Rows[0]["VCH_PAN"].ToString());
                            }
                            else
                            {
                                strUrl = "FormEditView.aspx?FormId=" + strServiceId + "&AppKey=" + strApplicationKey + "&ProposalNo=" + strProposalNo + "&ReqMode=M";
                                sbMenu.Append(strServiceId + "|");
                            }

                            actcls = " style='color: #397543!important;'";
                            act = "<div  runat='server' id=div" + strServiceId + "><span class='spanicon'><i class='fa fa-check' aria-hidden='true'></i></span></div>";
                            count++;
                            paycnt++;
                        }
                        else
                        {
                            act = "<div  runat='server' id=div" + strServiceId + "><span class='spanicon pending'><i class='fa fa-times' aria-hidden='true'></i> </span></div>";
                            strUrl = "ServiceInstruction.aspx?ReqMode=M&FormId=" + strServiceId + "&ServiceType=" + intServiceType + "&ProposalNo=" + strProposalNo + "&Amount=" + decAmount;
                        }

                        if (strServiceId == "" || strServiceId == null)//// When the request come for 1st time,this section will be executed.
                        {
                            if (intSerialNo == 1) //// Keep the TAB in active stage for service present on SlNo-1
                            {
                                // sbMenu.Append("<li><a  runat='server' id=" + strServiceId + " href='javascript: void(0);' class='accordion-heading' data-url=\"" + strUrl + "\"><h5>" + strServiceName + "</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");

                            }
                            else
                            {
                                // sbMenu.Append("<li><a runat='server'  id=" + strServiceId + " href='javascript: void(0);' class='accordion-heading' data-url=\"" + strUrl + "\"><h5>" + strServiceName + "</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");
                            }
                        }
                        else
                        {
                            if (intServiceId == strServiceId)
                            {
                                if (intCompletedStatus != 1)
                                {
                                    if (intExternalType == 1 && intServiceType == 0)
                                    {
                                        act = "<span class='loader'><img src='images/loader-new.gif' alt='GO Swift'></span>";
                                    }
                                }
                                //  sbMenu.Append("<li ><a runat='server' id=" + strServiceId + " href='javascript: void(0);' class='accordion-heading active' data-url=\"" + strUrl + "\"><h5>" + strServiceName + "</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");

                            }
                            else
                            {
                                // sbMenu.Append("<li><a runat='server'  id=" + strServiceId + " href='javascript: void(0);' class='accordion-heading' data-url=\"" + strUrl + "\"><h5>" + strServiceName + "</h5>" + act + "<i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");
                            }
                        }
                    }

                    if (dt.Rows.Count >= 1)
                    {
                        sbMenu.Append("0" + "|");
                        paycnt++;
                    }

                    if (paycnt > 0)
                    {
                        string strUrl = "ApplicationConsolidate.aspx";
                        sbMenu.Append("<li data-tooltip='Consolidated Payment'><a  runat='server'  id='six' href='javascript: void(0);'  class='accordion-heading' data-url=\"" + strUrl + "\"><h5>Consolidated Payment</h5><i class='plus'><i class='fa fa-plus' aria-hidden='true'></i></i><i class='minus'><i class='fa fa-minus' aria-hidden='true'></i></i></a></li>");
                    }

                    navbar = sbMenu;
                    // return navbar.ToString();
                }
            }
        }
        catch (Exception)
        {
            return "404";
        }
        finally
        {
        }

        return navbar.ToString();
    }

    /// <summary>
    /// Get investor details  add by anil
    /// </summary>
    /// <returns></returns>
    private static DataTable GetInveserDeatils()
    {
        DataTable dt = new DataTable();
        SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ToString());
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "USP_PAReSHRAM_SERVICE_DISPLAY";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(HttpContext.Current.Session["InvestorId"].ToString()));
            cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
            cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "PAReSHRAMSP");
            throw ex;
        }
        return dt;
    }

    [System.Web.Services.WebMethod]
    public static string getnavdataurl(string intServiceId)
    {
        string navbar = string.Empty; ;
        try
        {
            /*----------------------------------------------------------------------------*/
            //// The below section will be executed when the request come for multiple services.
            /*----------------------------------------------------------------------------*/
            if (HttpContext.Current.Session["InvestorId"] == null)
            {
                return "404";
            }
            else if (HttpContext.Current.Session["SvcMasterData"] != null)
            {
                DataTable dt = (DataTable)HttpContext.Current.Session["SvcMasterData"];
                DataRow dr = dt.AsEnumerable().Where(r => ((string)r["intServiceId"].ToString()).Equals(intServiceId)).First();

                navbar = dr["vchUpdateUrl"].ToString();
                int intServiceType = Convert.ToInt32(dr["intServiceType"]);
                int intExternalType = Convert.ToInt32(dr["intExternalType"]);
                if (intExternalType == 1 && intServiceType == 0)
                {
                    navbar += "|<span class='loader'><img src='images/loader-new.gif' alt='GO Swift'></span>";
                }
            }
        }
        catch (Exception)
        {
            navbar = "404";
        }
        finally
        {
        }
        return navbar;
    }
    #endregion

    //public string GetServiceName(string strServiceID)
    //{
    //    string strRes = "";
    //    SqlCommand cmd = new SqlCommand("SELECT VCH_SERVICENAME,(SELECT nvchLevelName FROM M_ADM_LevelDetails WHERE intLevelId=2 AND intLevelDetailId=INT_DEPARTMENT_ID) As Department FROM M_SERVICEMASTER_TBL WHERE  INT_SERVICEID=" + strServiceID, conn);
    //    SqlDataAdapter da = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds);

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        strRes = ds.Tables[0].Rows[0][1].ToString() + " > " + ds.Tables[0].Rows[0][0].ToString();
    //    }
    //    return strRes;
    //}

    //private void FillContent()
    //{
    //    try
    //    {
    //        EntityLayer.CMS.CMSDetails objServiceEntity = new EntityLayer.CMS.CMSDetails();
    //        objServiceEntity.StrAction = "GCD";
    //        objServiceEntity.IntServiceId = Convert.ToInt32(str_FormId);
    //        IList<CMSDetails> obj = objService.GetCMSData(objServiceEntity);
    //        if (obj.Count > 0)
    //        {
    //            // divabout.InnerHtml = obj[0].StrContent;
    //            HyprLnk.NavigateUrl = obj[0].strAttachment;
    //        }
    //        else
    //        {

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
    //    }
    //}

    #region get user manual
    [System.Web.Services.WebMethod]
    public static string FillContent(string strFormId)
    {
        string attch = string.Empty;
        try
        {
            EntityLayer.CMS.CMSDetails objServiceEntity = new EntityLayer.CMS.CMSDetails();
            objServiceEntity.StrAction = "GCD";
            objServiceEntity.IntServiceId = Convert.ToInt32(strFormId);
            CmsBusinesslayer objService = new CmsBusinesslayer();
            IList<CMSDetails> obj = objService.GetCMSData(objServiceEntity);
            if (obj.Count > 0)
            {
                attch = obj[0].strAttachment;
            }
            else
            {

            }
        }
        catch (Exception)
        {
            return "404";
        }
        finally
        {
        }
        return attch;
    }
    #endregion

    //protected void BtnProceed_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Int_ServiceType == 0) //For Internal Service
    //        {
    //            if (str_RequestMode == "S")
    //            {
    //                Response.Redirect("FormView.aspx?FormId=" + str_FormId + "&ProposalNo=" + str_ProposalNo + "&ReqMode=" + str_RequestMode);
    //            }
    //            else
    //            {
    //                DataTable dt = (DataTable)Session["SvcMasterData"];
    //                DataRow dr = dt.AsEnumerable().Where(r => ((string)r["intSlNo"].ToString()).Equals("1")).First();
    //                Response.Redirect("FormView.aspx?FormId=" + dr["intServiceId"] + "&ProposalNo=" + dr["vchProposalNo"] + "&ReqMode=" + str_RequestMode);
    //            }
    //        }
    //        else  // For External Service
    //        {
    //            if (str_FormId == "52") // Trade Licenece Service
    //            {
    //                Response.Redirect("TradeLicenceData.aspx?FormId=" + str_FormId.ToString() + "&ProposalNo=" + str_ProposalNo);
    //            }
    //            //else if (str_FormId == "30" || str_FormId == "31" || str_FormId == "32") // DrugLicense Service
    //            //{
    //            //    lblMessage.Text = "Health and Family Welfare";
    //            //    ServiceModalPopup.Show();
    //            //}
    //            //else if (str_FormId == "43" || str_FormId == "44" || str_FormId == "45" || str_FormId == "46" || str_FormId == "50" || str_FormId == "53" || str_FormId == "54" || str_FormId == "65" || str_FormId == "66") // Pollution Control Board
    //            //{
    //            //    lblMessage.Text = "Odisha State Pollution Control Board";
    //            //    ServiceModalPopup.Show();
    //            //}
    //            //else if (str_FormId == "25" || str_FormId == "26") // Tree Transit Service
    //            //{
    //            //    lblMessage.Text = "Forest and Environment";
    //            //    ServiceModalPopup.Show();
    //            //}
    //            //else if (str_FormId == "28") // Land Allocated Service
    //            //{
    //            //    Response.Redirect("LandAllocated.aspx?ServiceID=" + str_FormId.ToString() + "&ProposalNo=" + str_ProposalNo + "&UserId=" + Session["InvestorId"].ToString());
    //            //}
    //            //else if (str_FormId == "49") // PartnershipFirm Service
    //            //{
    //            //    lblMessage.Text = "Revenue & Disaster Management";
    //            //    ServiceModalPopup.Show();
    //            //}
    //            //else if (str_FormId == "10")
    //            //{
    //            //    Response.Redirect("ProfessionalTaxData.aspx?FormId=" + str_FormId.ToString() + "&ProposalNo=" + str_ProposalNo);
    //            //}
    //            //else if (str_FormId == "20") // Building Plan Approval Service
    //            //{
    //            //    lblMessage.Text = "Housing and Urban Development";
    //            //    ServiceModalPopup.Show();
    //            //}
    //            //else if (str_FormId == "29") // Obtaining water connection
    //            //{
    //            //    lblMessage.Text = "IDCO";
    //            //    ServiceModalPopup.Show();
    //            //}
    //            else if (str_FormId == "41") // Permission to draw Water
    //            {
    //                if (str_ProposalNo != "" || str_ProposalNo != null)
    //                {
    //                    Response.Redirect("FormView.aspx?FormId=" + str_FormId + "&ProposalNo=" + str_ProposalNo);
    //                }
    //                else
    //                {
    //                    Response.Redirect("FormView.aspx?FormId=" + str_FormId + "&ProposalNo=" + 0);
    //                }
    //            }
    //            else if (str_FormId == "5" || str_FormId == "6" || str_FormId == "7" || str_FormId == "34" || str_FormId == "35" || str_FormId == "36" || str_FormId == "39" || str_FormId == "40" || str_FormId == "70" || str_FormId == "71" || str_FormId == "72") //F&B and Labour Service
    //            {
    //                lblMessage.Text = "PAReSHRAM";
    //                ServiceModalPopup.Show();
    //            }
    //            else if (str_FormId == "62" || str_FormId == "63") // Fire Service
    //            {
    //                lblMessage.Text = "Directorate General Fire Services, Home Guards & Civil Defence";
    //                ServiceModalPopup.Show();
    //            }
    //            else if (str_FormId == "67" || str_FormId == "68") // Excise Service
    //            {
    //                lblMessage.Text = "Odisha State Excise";
    //                ServiceModalPopup.Show();
    //            }
    //            else if (str_FormId == "69") // OSBC Service
    //            {
    //                lblMessage.Text = "Odisha State Beverages Corporation Limited";
    //                ServiceModalPopup.Show();
    //            }
    //            else if (str_FormId == "73") // Mobile Tower Service
    //            {
    //                lblMessage.Text = "Department of Electronics and Information Technologies (E & IT)";
    //                ServiceModalPopup.Show();
    //            }
    //            else if (str_FormId == "74") // DG SET INSTALLATION
    //            {
    //                lblMessage.Text = "Department of Energy";
    //                ServiceModalPopup.Show();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "ServiceRedirect");
    //    }
    //}

    //protected void BtnYes_Click(object sender, EventArgs e)
    //{
    //    lblMessage.Text = "";
    //    ServiceModalPopup.Hide();
    //    string Data = "";
    //    string Result = "";

    //    if (str_FormId == "30" || str_FormId == "31" || str_FormId == "32") // DrugLicense Service
    //    {
    //        Response.Redirect(ConfigurationManager.AppSettings["DrugLicense"].ToString() + "?ServiceID=" + str_FormId.ToString() + "&UserId=" + Session["InvestorId"].ToString() + "&ProposalNo=" + str_ProposalNo);
    //    }
    //    else if (str_FormId == "43" || str_FormId == "44" || str_FormId == "45" || str_FormId == "46" || str_FormId == "50" || str_FormId == "53" || str_FormId == "54" || str_FormId == "65" || str_FormId == "66") // Pollution Control Board
    //    {
    //        Response.Redirect(ConfigurationManager.AppSettings["PollutionControl"].ToString() + "?ServiceID=" + str_FormId.ToString() + "&UserId=" + Session["InvestorId"].ToString() + "&StrProposalID=" + str_ProposalNo);
    //    }
    //    else if (str_FormId == "25" || str_FormId == "26") // Tree Transit Service
    //    {
    //        Response.Redirect(ConfigurationManager.AppSettings["TreeTransit"].ToString() + "?ServiceID=" + str_FormId.ToString() + "&ProposalNo=" + str_ProposalNo + "&UserId=" + Session["InvestorId"].ToString() + "&page=");
    //    }
    //    else if (str_FormId == "49") // PartnershipFirm Service
    //    {
    //        Response.Redirect(ConfigurationManager.AppSettings["PartnershipFirm"].ToString() + "?UserId=" + Session["InvestorId"].ToString() + "&ProposalId=" + str_ProposalNo);
    //    }
    //    else if (str_FormId == "20") // Building Plan Approval Service
    //    {
    //        Data = BPAS();
    //        Response.Redirect(ConfigurationManager.AppSettings["BPASRedirectionURL"].ToString() + Data);
    //    }
    //    else if (str_FormId == "29") // Obtaining water connection
    //    {
    //        Data = GOIPAS();
    //        Response.Redirect(ConfigurationManager.AppSettings["GOIPASRedirectionURL"].ToString() + "?Query=" + Data + "");
    //    }
    //    else if (str_FormId == "5" || str_FormId == "6" || str_FormId == "7" || str_FormId == "34" || str_FormId == "35" || str_FormId == "36" || str_FormId == "39" || str_FormId == "40" || str_FormId == "70" || str_FormId == "71" || str_FormId == "72") //F&B and Labour Service
    //    {
    //        Data = PAReSHRAM();
    //        Response.Redirect(ConfigurationManager.AppSettings["PARESHRAMREDIRECTIONURL"].ToString() + "?" + Data + "");
    //    }
    //    else if (str_FormId == "62" || str_FormId == "63") // Fire Service
    //    {
    //        Data = FIRE();
    //        Response.Redirect(ConfigurationManager.AppSettings["FIREREDIRECTIONURL"].ToString() + "?Query=" + Data + "");
    //    }
    //    else if (str_FormId == "67" || str_FormId == "68") // Excise Service
    //    {
    //        if (str_FormId == "67")
    //        {
    //            Data = EXCISE();
    //            var client = new RestClient(ConfigurationManager.AppSettings["EXCISESRREDIRECTIONURL"].ToString());
    //            client.Timeout = -1;
    //            var request = new RestRequest(Method.POST);
    //            request.AddHeader("Content-Type", "application/json");
    //            request.AddParameter("application/json", "{\r\n    \"EncryptedString\":\"" + Data + "\"\r\n}", ParameterType.RequestBody);
    //            IRestResponse response = client.Execute(request);
    //            Result = response.StatusCode.ToString();
    //            if (Result != "OK")
    //            {
    //                Util.LogRequestResponse("ExciseServiceCall67", client.ToString(), Result.ToString());
    //            }
    //            else
    //            {
    //                Response.Redirect(response.ResponseUri.ToString());
    //            }
    //        }
    //        else
    //        {
    //            Data = EXCISE();
    //            var client = new RestClient(ConfigurationManager.AppSettings["EXCISEGNSREDIRECTIONURL"].ToString());
    //            client.Timeout = -1;
    //            var request = new RestRequest(Method.POST);
    //            request.AddHeader("Content-Type", "application/json");
    //            request.AddParameter("application/json", "{\r\n    \"EncryptedString\":\"" + Data + "\"\r\n}", ParameterType.RequestBody);
    //            IRestResponse response = client.Execute(request);
    //            Result = response.StatusCode.ToString();
    //            if (Result != "OK")
    //            {
    //                Util.LogRequestResponse("ExciseServiceCall68", client.ToString(), Result.ToString());
    //            }
    //            else
    //            {
    //                Response.Redirect(response.ResponseUri.ToString());
    //            }
    //        }
    //    }
    //    else if (str_FormId == "69") // OSBC Service
    //    {
    //        Data = OSBC();
    //        Response.Redirect(ConfigurationManager.AppSettings["OSBCREDIRECTIONURL"].ToString() + "?encData=" + Data + "");
    //    }
    //    else if (str_FormId == "73") // MT Service
    //    {
    //        Data = EIT();
    //        Response.Redirect(ConfigurationManager.AppSettings["MobileTowerRedirectionUrl"].ToString() + "?" + Data + "");
    //    }
    //    else if (str_FormId == "74") // DG SET INSTALLATION
    //    {
    //        Response.Redirect(ConfigurationManager.AppSettings["DGSETREDIRECTIONURL"].ToString());
    //    }
    //}
    //protected void BtnNo_Click(object sender, EventArgs e)
    //{
    //    lblMessage.Text = "";
    //    ServiceModalPopup.Hide();
    //    Response.Redirect("ServiceInstruction1.aspx?FormId=" + str_FormId + "&ProposalNo=" + str_ProposalNo + "&Amount=" + str_Amount + "&ServiceType=" + Int_ServiceType + "&ReqMode=" + str_RequestMode);
    //}

    //private string BPAS()
    //{
    //    DataTable dt = new DataTable();
    //    string EncryptValue = "";
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        if (conn.State == ConnectionState.Closed)
    //        {
    //            conn.Open();
    //        }
    //        try
    //        {
    //            cmd.Connection = conn;
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = "USP_BPAS_SERVICE_DISPLAY";
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
    //            cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
    //            cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //        }
    //        catch (Exception ex)
    //        {
    //            Util.LogError(ex, "BPASSP");
    //        }
    //        finally
    //        {
    //            cmd = null;
    //            conn.Close();
    //        }
    //        string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
    //        if (output != "")
    //        {
    //            if (dt.Rows.Count > 0)
    //            {
    //                string AppBmcUrl = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
    //                var client = new RestClient("" + AppBmcUrl + "");
    //                var request = new RestRequest(Method.POST);
    //                request.AddHeader("postman-token", "487391e8-90a4-0edf-d80a-85927ba52b8f");
    //                request.AddHeader("cache-control", "no-cache");
    //                request.AddHeader("authorization", "Basic YmJzcm9uZUAyMDE4OlZLY0VoNFduQk9SVXAyY21GUmQzWTBSell4UVcxV1I=");
    //                request.AddHeader("content-type", "application/json");
    //                request.AddParameter("application/json", "{\"action\":\"encrypt\",\"encString\":\"" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "~::~" + dt.Rows[0]["VCH_CONTACT_MIDDLENAME"].ToString() + "~::~" + dt.Rows[0]["VCH_CONTACT_LASTNAME"].ToString() + "~::~" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "~::~" + dt.Rows[0]["VCH_EMAIL"].ToString() + "~::~" + dt.Rows[0]["VCH_INV_USERID"].ToString() + "~::~" + output + "~::~" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "~::~" + "1" + "\"}", ParameterType.RequestBody);
    //                IRestResponse response = client.Execute(request);
    //                string JSON = response.Content;
    //                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
    //                EncryptValue = dict["result"].ToString();
    //            }
    //            else
    //            {
    //                string AppBmcUrl = ConfigurationManager.AppSettings["BPASCHECKSTATUSURL"].ToString();
    //                var client = new RestClient("" + AppBmcUrl + "");
    //                var request = new RestRequest(Method.POST);
    //                request.AddHeader("postman-token", "487391e8-90a4-0edf-d80a-85927ba52b8f");
    //                request.AddHeader("cache-control", "no-cache");
    //                request.AddHeader("authorization", "Basic YmJzcm9uZUAyMDE4OlZLY0VoNFduQk9SVXAyY21GUmQzWTBSell4UVcxV1I=");
    //                request.AddHeader("content-type", "application/json");
    //                request.AddParameter("application/json", "{\"action\":\"encrypt\",\"encString\":\"" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + "NA" + "~::~" + output + "~::~" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "~::~" + "1" + "\"}", ParameterType.RequestBody);
    //                IRestResponse response = client.Execute(request);
    //                string JSON = response.Content;
    //                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
    //                EncryptValue = dict["result"].ToString();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "BPASENC");
    //    }
    //    return EncryptValue;
    //}
    //private string GOIPAS()
    //{
    //    string EncryptValue = "";
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        if (conn.State == ConnectionState.Closed)
    //        {
    //            conn.Open();
    //        }
    //        try
    //        {
    //            cmd.Connection = conn;
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = "USP_GOIPAS_SERVICE_DISPLAY";
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
    //            cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
    //            cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //        }
    //        catch (Exception ex)
    //        {
    //            Util.LogError(ex, "GOIPASSP");
    //        }
    //        finally
    //        {
    //            cmd = null;
    //            conn.Close();
    //        }
    //        string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
    //        if (output != "")
    //        {
    //            if (dt.Rows.Count > 0)
    //            {
    //                EncryptDecryptQueryString obj = new EncryptDecryptQueryString();
    //                string Data = "" + dt.Rows[0]["VCH_PAN"].ToString() + "&" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&" + dt.Rows[0]["VCH_UNIQUEID"].ToString() + "&" + dt.Rows[0]["INT_DISTRICT"].ToString() + "&" + output + "&" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "";
    //                EncryptValue = obj.Encrypt(Data, "m8s3e3k5");
    //            }
    //            else
    //            {
    //                EncryptValue = "";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "GOIPASENC");
    //    }
    //    return EncryptValue;
    //}

    //private string PAReSHRAM()
    //{
    //    string EncryptValue = "";
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        if (conn.State == ConnectionState.Closed)
    //        {
    //            conn.Open();
    //        }
    //        try
    //        {
    //            cmd.Connection = conn;
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = "USP_PAReSHRAM_SERVICE_DISPLAY";
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
    //            cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
    //            cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //        }
    //        catch (Exception ex)
    //        {
    //            Util.LogError(ex, "PAReSHRAMSP");
    //            throw ex;
    //        }
    //        finally
    //        {
    //            cmd = null;
    //            conn.Close();
    //        }
    //        string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
    //        if (output != "")
    //        {
    //            if (dt.Rows.Count > 0)
    //            {
    //                EncryptValue = "appln_id=" + output + "&service_code=" + str_FormId.ToString() + "&pan=" + dt.Rows[0]["VCH_PAN"].ToString() + "&name=" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "&mobile_number=" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "&email=" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&est_name=" + dt.Rows[0]["VCH_INV_NAME"].ToString();
    //            }
    //            else
    //            {
    //                EncryptValue = "appln_id=" + output + "&service_code=" + str_FormId.ToString() + "&pan=" + "" + "&name=" + "" + "&mobile_number=" + "" + "&email=" + "" + "&est_name=''";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "PAReSHRAMENC");
    //    }
    //    return EncryptValue;
    //}

    //private string FIRE()
    //{
    //    string EncryptValue = "";
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        if (conn.State == ConnectionState.Closed)
    //        {
    //            conn.Open();
    //        }
    //        try
    //        {
    //            cmd.Connection = conn;
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = "USP_FIRE_SERVICE_DISPLAY";
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
    //            cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
    //            cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //        }
    //        catch (Exception ex)
    //        {
    //            Util.LogError(ex, "FIRESP");
    //        }
    //        finally
    //        {
    //            cmd = null;
    //            conn.Close();
    //        }
    //        string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
    //        if (output != "")
    //        {
    //            if (dt.Rows.Count > 0)
    //            {
    //                var client = new RestClient("" + ConfigurationManager.AppSettings["FIRECHECKSTATUSURL"].ToString() + "?name='" + dt.Rows[0]["VCH_INV_NAME"].ToString() + "'&email='" + dt.Rows[0]["VCH_EMAIL"].ToString() + "'&mobile='" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "'&serviceId=" + str_FormId + "&applicationId='" + output + "'&mode=" + 1 + "&source='GOSWIFT'&returnUrl='" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "'");
    //                client.Timeout = -1;
    //                var request = new RestRequest(Method.GET);
    //                request.AlwaysMultipartFormData = true;
    //                IRestResponse response = client.Execute(request);
    //                string JSON = response.Content;
    //                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
    //                EncryptValue = dict["result"].ToString();
    //            }
    //            else
    //            {
    //                var client = new RestClient("" + ConfigurationManager.AppSettings["FIRECHECKSTATUSURL"].ToString() + "?name=''&email=''&mobile=''&serviceId=" + str_FormId + "&applicationId='" + output + "'&mode=" + 1 + "&source='GOSWIFT'&returnUrl='" + ConfigurationManager.AppSettings["GOSWIFTDRAFTURL"].ToString() + "'");
    //                client.Timeout = -1;
    //                var request = new RestRequest(Method.GET);
    //                request.AlwaysMultipartFormData = true;
    //                IRestResponse response = client.Execute(request);
    //                string JSON = response.Content;
    //                var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(JSON);
    //                EncryptValue = dict["result"].ToString();
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "FIREENC");
    //    }
    //    return EncryptValue;
    //}

    //private string EXCISE()
    //{
    //    string strplaintext = "";
    //    string EncryptValue = "";
    //    string strSuppliedKey = "?åLˆ'KX¾p ;™¶%M8º}ÌqE-ƒU§©	;±½";
    //    byte[] key = { };
    //    key = Encoding.ASCII.GetBytes(strSuppliedKey);
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        if (conn.State == ConnectionState.Closed)
    //        {
    //            conn.Open();
    //        }
    //        try
    //        {
    //            cmd.Connection = conn;
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = "USP_EXCISE_SERVICE_DISPLAY";
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
    //            cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
    //            cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //        }
    //        catch (Exception ex)
    //        {
    //            Util.LogError(ex, "EXCISESP");
    //        }
    //        finally
    //        {
    //            cmd = null;
    //            conn.Close();
    //        }
    //        string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
    //        if (output != "")
    //        {
    //            if (dt.Rows.Count > 0)
    //            {
    //                strplaintext = "" + dt.Rows[0]["VCH_INV_NAME"].ToString() + "|" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "|" + str_FormId + "|" + output + "|1";
    //                EncryptValue = ExciseEncryptAlgorthim(strplaintext, key);
    //            }
    //            else
    //            {
    //                strplaintext = "NA|NA|NA|NA|1";
    //                EncryptValue = ExciseEncryptAlgorthim(strplaintext, key);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "EXCISEENC");
    //    }
    //    return EncryptValue;
    //}

    //public static string ExciseEncryptAlgorthim(string plainText, byte[] Key)
    //{
    //    if (plainText == null || plainText.Length <= 0)
    //        throw new ArgumentNullException("plainText");
    //    if (Key == null || Key.Length <= 0)
    //        throw new ArgumentNullException("Key");
    //    byte[] encrypted;
    //    string base64encrypted;
    //    using (AesManaged aesAlg = new AesManaged())
    //    {
    //        aesAlg.Key = Key;
    //        aesAlg.Mode = CipherMode.ECB;
    //        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
    //        using (MemoryStream msEncrypt = new MemoryStream())
    //        {
    //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
    //            {
    //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
    //                {
    //                    swEncrypt.Write(plainText);
    //                }
    //                encrypted = msEncrypt.ToArray();
    //            }
    //        }
    //    }
    //    base64encrypted = Convert.ToBase64String(encrypted, 0, encrypted.Length);
    //    return base64encrypted.Replace("/", "-");
    //}

    //public string EIT()
    //{
    //    string EncryptValue = "";
    //    DataTable dt = new DataTable();
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        if (conn.State == ConnectionState.Closed)
    //        {
    //            conn.Open();
    //        }

    //        try
    //        {
    //            cmd.Connection = conn;
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = "USP_EIT_SERVICE_DISPLAY";
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
    //            cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
    //            cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //        }
    //        catch (Exception ex)
    //        {
    //            Util.LogError(ex, "EITSP");
    //            throw ex;
    //        }
    //        finally
    //        {
    //            cmd = null;
    //            conn.Close();
    //        }
    //        string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
    //        if (output != "")
    //        {
    //            if (dt.Rows.Count > 0)
    //            {
    //                EncryptValue = "authorised_person=" + dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString() + "&phone_number=" + dt.Rows[0]["VCH_OFF_MOBILE"].ToString() + "&email=" + dt.Rows[0]["VCH_EMAIL"].ToString() + "&district=" + dt.Rows[0]["vchDistrictName"].ToString() + "&uniqid=" + output + "&ServiceId=" + str_FormId + "";
    //            }
    //            else
    //            {
    //                EncryptValue = "authorised_person=" + "" + "&phone_number=" + "" + "&email=" + "" + "&district=" + "" + "&uniqid=" + output + "&ServiceId=" + str_FormId + "";
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "EITENC");
    //    }
    //    return EncryptValue;
    //}

    //private string OSBC()
    //{
    //    string EncryptValue = "";
    //    DataTable dt = new DataTable();
    //    ExciseOSBCServiceReference.OSBCSoftSoapClient objEx = new ExciseOSBCServiceReference.OSBCSoftSoapClient();
    //    ExciseOSBCServiceReference.SupDetails objEntity = new ExciseOSBCServiceReference.SupDetails();
    //    try
    //    {
    //        SqlCommand cmd = new SqlCommand();
    //        if (conn.State == ConnectionState.Closed)
    //        {
    //            conn.Open();
    //        }
    //        try
    //        {
    //            cmd.Connection = conn;
    //            cmd.CommandType = CommandType.StoredProcedure;
    //            cmd.CommandText = "USP_OSBC_SERVICE_DISPLAY";
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@INT_INVESTOR_ID", Convert.ToInt32(Session["InvestorId"].ToString()));
    //            cmd.Parameters.AddWithValue("@VCH_ACTION", "INDUSTRYINFO");
    //            cmd.Parameters.AddWithValue("@VCH_APPLICATION_UNQ_KEY", "");
    //            SqlDataAdapter da = new SqlDataAdapter(cmd);
    //            da.Fill(dt);
    //        }
    //        catch (Exception ex)
    //        {
    //            Util.LogError(ex, "OSBCSP");
    //        }
    //        finally
    //        {
    //            cmd = null;
    //            conn.Close();
    //        }
    //        string output = objSvc.ExternalServiceData("GA", Convert.ToInt32(str_FormId), str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), Session["PAN"].ToString());
    //        if (output != "")
    //        {
    //            if (dt.Rows.Count > 0)
    //            {
    //                objEntity.Application_No = output;
    //                objEntity.GoSwiftUserID = dt.Rows[0]["VCH_INV_USERID"].ToString();
    //                objEntity.MobileNo = "8249761028";
    //                objEntity.Name = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
    //                objEntity.Email = dt.Rows[0]["VCH_EMAIL"].ToString();
    //                objEntity.Sector_Type = dt.Rows[0]["VCH_SECTOR"].ToString();
    //                objEntity.Sector_Subtype = dt.Rows[0]["vchSubSectorName"].ToString();
    //                objEntity.ServiceID = str_FormId;
    //                objEntity.Source = "GOSWIFT";
    //                objEntity.Active_Status = "Yes";
    //                EncryptValue = objEx.AESEncryptForSignUP(objEntity);
    //            }
    //            else
    //            {
    //                objEntity.Application_No = output;
    //                objEntity.GoSwiftUserID = "";
    //                objEntity.MobileNo = "";
    //                objEntity.Name = "";
    //                objEntity.Email = "";
    //                objEntity.Sector_Type = "";
    //                objEntity.Sector_Subtype = "";
    //                objEntity.ServiceID = str_FormId;
    //                objEntity.Source = "GOSWIFT";
    //                objEntity.Active_Status = "Yes";
    //                EncryptValue = objEx.AESEncryptForSignUP(objEntity);
    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Util.LogError(ex, "OSBCENC");
    //    }
    //    return EncryptValue;
    //}


    private string GetUserProfile()
    {
        string EncryptValue = "";
        DataTable dt = new DataTable();

        try
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "Usp_GetUserProfile";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@P_UserId", Convert.ToInt32(Session["InvestorId"].ToString()));

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                EncryptValue = "Phone=" + dt.Rows[0]["Unit_MobileNo"].ToString() + "&Email=" + dt.Rows[0]["Unit_EmailId"].ToString() + "&PanNo=" + dt.Rows[0]["VCH_PAN"].ToString();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ServiceProcess");
            conn.Close();
        }

        return EncryptValue;
    }

    private string EncryptQueryString(string strQueryString)
    {
        EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
        return objEDQueryString.Encrypt(strQueryString, "m8s3e3k5");
    }

    /// <summary>
    /// Summary description for EncryptDecryptQueryString
    /// </summary>
    public class EncryptDecryptQueryString
    {
        private byte[] key = { };
        private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        public string Encrypt(string stringToEncrypt, string SEncryptionKey)
        {
            try
            {
                key = System.Text.Encoding.UTF8.GetBytes(SEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}