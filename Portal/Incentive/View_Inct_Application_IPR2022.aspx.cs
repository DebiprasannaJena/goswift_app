using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Incentive;
using BusinessLogicLayer.Incentive;
using System.Globalization;
using System.IO;
using Ionic.Zip;
using System.Web.UI.HtmlControls;
using EntityLayer.Service;
using Common;

public partial class Portal_Incentive_View_Inct_Application_IPR2022 : System.Web.UI.Page
{
    
    bool IsPageRefresh = false;
    IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
    ServiceDetails objServiceEntity = new ServiceDetails();
    DepartmentSMSClass objDepartmntSms = new DepartmentSMSClass();

    string str_Retvalue = string.Empty;

    static int intDataLoadType;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           

            if (Session["UserId"] == null)
            {
                Response.Redirect("Default.aspx");
            }

            if (!IsPostBack)
            {
                
                

                fillGrid();
              
                ViewState["ViewStateId"] = System.Guid.NewGuid().ToString();
                Session["SessionId"] = ViewState["ViewStateId"].ToString();
               
            }
            else
            {
               
                if (ViewState["ViewStateId"].ToString() != Session["SessionId"].ToString())
                {
                    IsPageRefresh = true;
                }

                Session["SessionId"] = System.Guid.NewGuid().ToString();
                ViewState["ViewStateId"] = Session["SessionId"].ToString();
               
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }

    }

    private void fillGrid()
    {
        try
        {
            IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
            Inct_Application_Details_Entity objBU_Entity = new Inct_Application_Details_Entity();
            objBU_Entity.strAction = "G";
            objBU_Entity.intStatus =Convert.ToInt32(DrpDwn_Status.SelectedValue);
            objBU_Entity.strAppNo = Txt_App_No.Text == "" ? "0" : Convert.ToString(Txt_App_No.Text);
                      
            objBU_Entity.strUserID = Convert.ToString(Session["UserId"]); // Passing of User Id to check DIC/RIC/DI ON 23-OCT-2017

            IList<Inct_Application_Details_Entity> list = new List<Inct_Application_Details_Entity>();
            list = ObjIMB.IPR2022View_Application_Details(objBU_Entity);
            if (list.Count > 0)
            {
                Grd_Application.DataSource = list;
                Grd_Application.DataBind();
                divPaging.Visible = true;

                divPagingShow.Visible = true;
            }
            else
            {
                Grd_Application.DataSource = null;
                Grd_Application.DataBind();
                divPaging.Visible = false;
                divPagingShow.Visible = false;
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }

    protected void Btn_Search_Click(object sender, EventArgs e)
    {
        try
        {

            fillGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Incentive");
        }
    }

    protected void LnkBtn_View_Application_Click(object sender, EventArgs e)
    {

        try
        {
            LinkButton lnkbtn = (LinkButton)sender;
            GridViewRow row = (GridViewRow)lnkbtn.Parent.Parent;

            HiddenField Hid_Form_Preview_Id = (HiddenField)row.FindControl("Hid_Form_Preview_Id");
            HiddenField Hid_Unique_Id = (HiddenField)row.FindControl("Hid_Unique_Id");

            if (Hid_Form_Preview_Id.Value != "")
            {
              
                Response.Redirect("~/Incentives/" + Hid_Form_Preview_Id.Value + "?InctUniqueNo=" + Hid_Unique_Id.Value + "", false);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "jAlert('<strong>No Form Preview Available for this Application !</strong>','SWP')", true);
                return;
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
        finally
        {

        }
    }


    protected void Grd_Application_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "ViewDetail")
            {
                GridViewRow row = ((e.CommandSource as LinkButton).NamingContainer) as GridViewRow;
                LinkButton lnkButton = (LinkButton)row.FindControl("lnkButton");
                string unit = (row.FindControl("Lbl_Unit_Name") as Label).Text;
                string incname = (row.FindControl("Lbl_Inct_Name") as Label).Text;
              
                string Uniqueid = (row.FindControl("Hid_Unique_Id") as HiddenField).Value;
                string AppNo = (row.FindControl("HiddenField1") as HiddenField).Value;
                string code = (row.FindControl("hdnIncentiveNo") as HiddenField).Value;
               string FormPreviewId= (row.FindControl("Hid_Form_Preview_Id") as HiddenField).Value;
                LinkButton lbtnRaise = row.FindControl("lbtnRaise") as LinkButton;
                if (lnkButton.Text.Trim().ToLower() == "take action")
                {
                    Response.Redirect("ApproveIncentive_IPR2022.aspx?URL=1&UniqueID=" + Uniqueid + "&IncentiveName=" + incname + "&ApplicationNo=" + AppNo + "&UnitName=" + unit + "&FormId=" + FormPreviewId);

                   
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }

    protected void Grd_Application_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string toptable = "";
                string strHTMlQuery2 = "";
                Label Lbl_Status = (Label)e.Row.FindControl("Lbl_Status");
                HiddenField hdnAppStatus = (HiddenField)e.Row.FindControl("hdnAppStatus");
                LinkButton lbtnDisbursedDtls = (e.Row.FindControl("lbtnDisbursedDtls") as LinkButton);
                LinkButton lnkButton = (LinkButton)e.Row.FindControl("lnkButton");
                LinkButton LnkBtn_View_Application = (LinkButton)e.Row.FindControl("LnkBtn_View_Application");
                string unit = (e.Row.FindControl("Lbl_Unit_Name") as Label).Text;
                string IncentiveName = (e.Row.FindControl("Lbl_Inct_Name") as Label).Text;
                HiddenField hdnSanFileName = (HiddenField)e.Row.FindControl("hdnSanFileName");
                HiddenField hdnRemarks = (HiddenField)e.Row.FindControl("hdnRemarks");
                HtmlGenericControl DisbursedList = (HtmlGenericControl)e.Row.FindControl("DisbursedList");
                LinkButton lbtnRaises = (e.Row.FindControl("lbtnRaise") as LinkButton);
                LinkButton lbtnQueryDtls = (e.Row.FindControl("lbtnQueryDtls") as LinkButton);
                HiddenField hdnTextVal1 = (HiddenField)e.Row.FindControl("hdnTextVal1");
                string strCurrQueryStatus = Convert.ToString(Grd_Application.DataKeys[e.Row.RowIndex].Values[1]);

                toptable = "<table class='table table-bordered table-hover'>";
                toptable += "<tr><th colspan='2'>Sanction Details</th></tr>";
                toptable += "<tr><td>Application No.</td><td>" + LnkBtn_View_Application.Text + "</td></tr>";
                toptable += "<tr><td>Unit Name</td><td>" + unit + "</td></tr>";
                toptable += "<tr><td>Incentive Name</td><td>" + IncentiveName + "</td></tr>";
                toptable += "<tr><td>Sanction Order Document</td><td><a target=\"_blank\"  href=\"../../Portal/Incentive/Sanctionorder/" + hdnSanFileName.Value + "\" ><i class=\"fa fa-file-text-o\" ></i></a>   </td></tr>";
                toptable += "<tr><td>Remarks</td><td>" + hdnRemarks.Value + "</td></tr>";
                toptable += "</table>";

                if (hdnAppStatus.Value == "1")
                {
                    if(strCurrQueryStatus == "QUERY RAISED")
                    {
                        lnkButton.Visible = false;
                        Lbl_Status.Text = "Applied";
                        lbtnDisbursedDtls.Visible = false;
                        lbtnDisbursedDtls.Visible = false;
                    }
                    else
                    {
                        Lbl_Status.Text = "Applied";
                        lnkButton.Visible = true;
                        lbtnDisbursedDtls.Visible = false;
                        lbtnDisbursedDtls.Visible = false;
                    }
                    
              
                }
                else if (hdnAppStatus.Value == "4")
                {
                    if (strCurrQueryStatus == "QUERY RAISED")
                    {
                        lnkButton.Visible = false;
                        Lbl_Status.Text = "Pending";
                        lbtnDisbursedDtls.Visible = false;
                        lbtnDisbursedDtls.Visible = false;
                    }
                    else 
                    {
                        Lbl_Status.Text = "Pending";
                        lnkButton.Visible = true;
                        lbtnDisbursedDtls.Visible = false;
                        lbtnDisbursedDtls.Visible = false;
                    }
                    
                }
                else if (hdnAppStatus.Value == "8")
                {
                    Lbl_Status.Text = "Forwarded";
                    lnkButton.Visible = false;
                    lbtnDisbursedDtls.Visible = false;
                    lbtnDisbursedDtls.Visible = false;
                }

                int intQueryStatus = Convert.ToInt32(Grd_Application.DataKeys[e.Row.RowIndex].Values[0]);
                LinkButton lbtnRaise = (e.Row.FindControl("lbtnRaise") as LinkButton);
                if (intQueryStatus == 1)//Query Raised
                {
                    lbtnRaise.Visible = true;
                  
                }
                else if (intQueryStatus == 0) //IF Query Date is Blank
                {
                    lbtnRaise.Visible = false;
                   


                }
                else if (intQueryStatus == 2) //If Query Date is Expired
                {
                    lbtnRaise.Visible = false;
                   
                }
                List<QueryMgntDtls> objProposalList = new List<QueryMgntDtls>();
                QueryMgntDtls objQueryMgtDtls = new QueryMgntDtls();
                objQueryMgtDtls.strAction = "QD";
                objQueryMgtDtls.strIncentiveUnqNo = hdnTextVal1.Value;
                objProposalList = ObjIMB.getInctRaisedQueryDetails(objQueryMgtDtls).ToList();
                HtmlGenericControl QueryHist = (HtmlGenericControl)e.Row.FindControl("QueryHist");
                HtmlGenericControl QueryHist1 = (HtmlGenericControl)e.Row.FindControl("QueryHist1");

                if (objProposalList.Count > 0)
                {
                    string strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th>Query Reference No.</th><th>User Name</th><th> Query Details</th><th>Date</th><th>Files</th></tr>";
                    for (int i = 0; i < objProposalList.Count; i++)
                    {
                        if (objProposalList[i].strFileName == null || objProposalList[i].strFileName == "")
                        {
                            strHTMlQuery = strHTMlQuery + "<tr><td>" + objProposalList[i].strQueryUnqNo + "</td><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a target='' href='#'>--</a>" + "</td></tr>";
                        }
                        else
                        {
                            strHTMlQuery = strHTMlQuery + "<tr><td>" + objProposalList[i].strQueryUnqNo + "</td><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a   class='testd' href='../../incentives/Files/QueryDocs/" + objProposalList[i].strFileName + "'>Download</a>" + "</td></tr>";
                        }
                    }
                    strHTMlQuery = strHTMlQuery + "</table>";

                    QueryHist.InnerHtml = strHTMlQuery;
                    QueryHist1.InnerHtml = strHTMlQuery;
                }
               

                if (strCurrQueryStatus == "--")
                {
                    lbtnQueryDtls.Visible = false;
                    lbtnQueryDtls.Text = "--";
                   
                }
                else if (strCurrQueryStatus == "Completed")
                {
                    lbtnQueryDtls.CssClass = "btn btn-success btn-sm";
                    lbtnQueryDtls.Text = "<i class='fa fa-eye' aria-hidden='true'></i>";
                    lbtnRaise.Visible = false;
                }
                else if (strCurrQueryStatus == "QUERY RAISED")
                {
                    lbtnQueryDtls.Text = strCurrQueryStatus;
                    lbtnQueryDtls.CssClass = "btn btn-success btn-sm";
                    lbtnRaise.Visible = false;
                }
                else if (strCurrQueryStatus == "QUERY RESPONDED")
                {
                   
                    ///// To display query details when the application is in pending/scrutiny stage but the query raise time expired.

                    if (intQueryStatus == 2) //If Query Date is Expired
                    {
                        lbtnQueryDtls.Text = "QRY RESPONDED";
                        lbtnQueryDtls.CssClass = "btn btn-warning btn-sm";
                        lbtnQueryDtls.Visible = true;
                        lbtnRaise.Text = "QRY RESPONDED";
                        lbtnRaise.CssClass = "btn btn-warning btn-sm";
                    }
                    else
                    {
                        lbtnQueryDtls.Text = "QRY RESPONDED";
                        lbtnQueryDtls.CssClass = "btn btn-warning btn-sm";
                        lbtnQueryDtls.Visible = false;
                        lbtnRaise.Text = "QRY RESPONDED";
                        lbtnRaise.CssClass = "btn btn-warning btn-sm";
                    }
                }
                HiddenField hdnDisburedId = (HiddenField)e.Row.FindControl("hdnDisburedId");
                IList<Inct_Application_Details_Entity> list = new List<Inct_Application_Details_Entity>();
                Inct_Application_Details_Entity objBU_Entity = new Inct_Application_Details_Entity();

                objBU_Entity.strAction = "DA";
                objBU_Entity.INTINCUNQUEID = Convert.ToInt32(hdnDisburedId.Value);
                list = ObjIMB.View_Application_ApprveFetch(objBU_Entity);

                DateTime strTime = DateTime.Now;
                string strDatetime = "";
                if (list.Count > 0)
                {
                    strHTMlQuery2 = "<table class='table table-bordered table-hover'><tr><th colspan='8'>Disbursement Details</th></tr><tr><th>Application No.</th><th>Unit Name</th><th>Bank Name</th><th>UTR (Transaction ID)</th><th>Disbursed Amount</th><th>Date</th><th>Time</th><th>Files</th></tr>";
                    if (!string.IsNullOrEmpty(list[0].DisburseTime.ToString()) && list[0].DisburseTime.ToString() != "")
                        strTime = Convert.ToDateTime(list[0].DisburseTime.ToString());
                    else
                        strTime = DateTime.Now;

                    if (!string.IsNullOrEmpty(list[0].DisburseDate.ToString()) && list[0].DisburseDate.ToString() != "")
                        strDatetime = list[0].DisburseDate.ToString();
                    else
                        strDatetime = "";

                    if (list[0].DisbursementDocument == null || list[0].DisbursementDocument == "")
                    {
                        strHTMlQuery2 = strHTMlQuery2 + "<tr><td>" + list[0].strApplicationNum.ToString() + "</td><td>" + list[0].strUnitName + "</td><td>" + list[0].BankName + "</td><td>" + list[0].DisburseNo + "</td><td>" + list[0].DisburseAmount + "</td><td>" + strDatetime + "</td><td>" + strTime.ToString("hh:mm tt", CultureInfo.InvariantCulture) + "</td><td>" + "<a class='testd' target='' href='#'>--</a>" + "</td></tr>";
                    }
                    else
                    {
                        strHTMlQuery2 = strHTMlQuery2 + "<tr><td>" + list[0].strApplicationNum.ToString() + "</td><td>" + list[0].strUnitName + "</td><td>" + list[0].BankName + "</td><td>" + list[0].DisburseNo + "</td><td>" + list[0].DisburseAmount + "</td><td>" + strDatetime + "</td><td>" + strTime.ToString("hh:mm tt", CultureInfo.InvariantCulture) + "</td><td>" + "<a  class='testd'  target='_blank' href='../Incentive/Disbursement/" + list[0].DisbursementDocument + "'>Download</a>" + "</td></tr>";
                    }
                    strHTMlQuery2 = strHTMlQuery2 + "</table>";

                    lbtnDisbursedDtls.CssClass = "btn btn-success btn-sm";
                    lbtnDisbursedDtls.Text = "<i class='fa fa-eye' aria-hidden='true'></i>";
                }
                else
                {
                    //lnkButton.Visible = false;
                    //lbtnRaises.Visible = false;
                    //lbtnQueryDtls.Visible = false;
                    //lbtnDisbursedDtls.Visible = false;

                }
               
            }

        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }

    protected void btnQuerySubmit_Click(object sender, EventArgs e)
    {
        try
        {
            QueryMgntDtls objQueryMgtDtls = new QueryMgntDtls();
            if (IsPageRefresh == false)
            {
                Button btnQuerySubmit = (Button)sender;
                TextBox txtq1 = (TextBox)btnQuerySubmit.FindControl("txtq1");
                FileUpload FileUpload1 = (FileUpload)btnQuerySubmit.FindControl("FileUpload1");
                objQueryMgtDtls.strAction = "Q";
                objQueryMgtDtls.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                objQueryMgtDtls.strIncentiveUnqNo = btnQuerySubmit.CommandArgument.ToString();
                objQueryMgtDtls.intStatus = 5;
                objQueryMgtDtls.strRemarks = txtq1.Text;
                string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + btnQuerySubmit.CommandArgument.ToString() + "_Query1" + ".pdf", DateTime.Now);

                if (FileUpload1.HasFile)
                {
                    if (Path.GetExtension(FileUpload1.FileName) != ".pdf")
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
                        return;
                    }
                }
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/incentives/Files/QueryDocs/"));
                if (!string.IsNullOrEmpty(FileUpload1.FileName))
                {
                    if (dir.Exists)
                    {
                        FileUpload1.SaveAs(Server.MapPath("~/incentives/Files/QueryDocs/" + filepath));
                    }
                    else
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~/incentives/Files/QueryDocs"));
                        FileUpload1.SaveAs(Server.MapPath("~/incentives/Files/QueryDocs/" + filepath));

                    }
                }
                else { filepath = ""; }
                objQueryMgtDtls.strFileName = filepath;
                string strRetVal = ObjIMB.IncentivesRaiseQuery(objQueryMgtDtls);
                fillGrid();
                if (strRetVal == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Query Raised Successfully.')</script>;", false);
                    //FOR SENDING MAIL & SMS
                    CommonHelperCls comm = new CommonHelperCls();
                    List<QueryMgntDtls> objProposalList = new List<QueryMgntDtls>();
                    QueryMgntDtls objProp = new QueryMgntDtls();

                    objProp.strAction = "S";
                    objProp.strIncentiveUnqNo = btnQuerySubmit.CommandArgument.ToString();
                    objProp.intCreatedBy = Convert.ToInt32(Session["UserId"]);
                    objProposalList = ObjIMB.getInctRaisedQueryDetails(objProp).ToList();
                    string mobile = "";
                    string smsContent = "";
                    string strSubject = "";
                    string[] toEmail = new string[1];

                    if (objProposalList.Count > 0)
                    {
                        mobile = Convert.ToString(objProposalList[0].strMobileNo);
                        smsContent = Convert.ToString(objProposalList[0].strEmailBody);
                        toEmail[0] = Convert.ToString(objProposalList[0].strEmailID);
                        strSubject = Convert.ToString(objProposalList[0].strEmailSubject);
                        string strBody = smsContent;
                        comm.sendMail(strSubject, strBody, toEmail, true);
                        comm.SendSms(mobile, smsContent);
                    }
                    //For Sending SMS TO HOD
                    objProp.strAction = "T";
                    objProp.strIncentiveUnqNo = btnQuerySubmit.CommandArgument.ToString();
                    objProposalList = ObjIMB.getInctRaisedQueryDetails(objProp).ToList();

                    if (objProposalList.Count > 0)
                    {
                        if (objProposalList[0].intNoOfTimes >= 2) // for fetching how many times query raised by dept
                        {
                            objServiceEntity.INT_SERVICEID = 501; //service id for Incentives is 501
                            objServiceEntity.strSubject = strSubject;
                            objServiceEntity.strBody = smsContent;
                            objServiceEntity.smsContent = smsContent;

                            objDepartmntSms.DepartmentSendSms(objServiceEntity);
                        }
                    }
                }
                else if (strRetVal == "4")
                { ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Action can not be taken Successfully.')</script>;", false); }
            }
        }
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
        finally
        {
            // objProposal = null;
        }
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        try
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName("QueryDocs");
                if (hdnFileNames.Value != "")
                {
                    string[] arrFileName = hdnFileNames.Value.Split(',');
                    for (int i = 0; i <= arrFileName.Count() - 1; i++)
                    {
                        string FileName = "../../incentives/Files/QueryDocs/" + Convert.ToString(arrFileName[i]);
                        string filePath = Server.MapPath(FileName);
                        zip.AddFile(filePath, "QueryDocs");
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
        catch (Exception x)
        {
            Util.LogError(x, "Incentive");
        }
    }
}