using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SingleWindow_CmdApproval : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    public bool isVis { get; set; }
    static int intType = 0;
    string strVal="";
    string ProjectNm = "";
    public bool IsVisible
    {
        get { return isVis; }
        set { isVis = value; }
    }
    #endregion
    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
            if (intType == 3)
            {
                if (!IsPostBack)
                {
                    if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                    {
                        ViewSLFCComments();
                        ViewCMDComments();
                    }
                }
            }
            else {
                Response.Redirect("../Login.aspx");
            }
           
        }
    }

    #region for view comments
    public void ViewSLFCComments()
    {
        try
        {
            objams.Action = "V";
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            DataSet ds = new DataSet();
            ds = AMServices.ViewComments(objams);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    RptrComments.DataSource = ds.Tables[0];
                    RptrComments.DataBind();
                    lblMessage.Visible = false;
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    rptClarification.DataSource = ds.Tables[1];
                    rptClarification.DataBind();
                    IsVisible = true;
                }
                else
                    IsVisible = false;
            }
            else
            {
                lblMessage.Visible = true;
            }
        }
        catch (Exception m) { Response.Write(m.Message); }
    }

    private void ViewCMDComments()
    {
        try
        {
            objams.Action = "FRD";
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            DataTable dt = new DataTable();
            dt = AMServices.ViewFinace(objams);
            DataTable dt1 = new DataTable();
            objams.Action = "PN"; //Get Project Name
            dt1 = AMServices.ViewFinace(objams);
            //----------------------------

            if (dt.Rows.Count > 0)
            {
                DataView dv2 = new DataView(dt);
                dv2.RowFilter = "intCreatedBy=2";  //CMD

                DataView dv1 = new DataView(dt);
                dv1.RowFilter = "intCreatedBy=3"; // GM

                RptCMDRemark.DataSource = dv1;
                RptCMDRemark.DataBind();
                if (dv2.Count>0)
                {
                RptGMRemark.DataSource = dv2;
                RptGMRemark.DataBind();
                }

                HdnCmdRemark.Value = dv1[dv1.Count-1][0].ToString().Replace("<br />", " . ");
                ViewState["RemarkId"] = dv1[dv1.Count-1][4].ToString();
                divRemark.Visible = true;
            }  
           
            else
            {
                divRemark.Visible = false;
            }

            if (dt1.Rows.Count > 0)
            {
                hdnProjNm.Value = dt1.Rows[0]["VCHPROJCT_NAME"].ToString();
                hdnUid.Value = dt1.Rows[0]["VCH_UID"].ToString();
            }
            
        }
        catch(Exception)
        {
        
        }
    }

    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e) // Accepted by CMD
    {
        try
        {
            if (intType == 3)
            {
                ProjectNm = hdnProjNm.Value;
                MemberDetails(4, Convert.ToInt32(Request.QueryString["ID"]), hdnProjNm.Value, 4, " The Agenda form for " + ProjectNm + " has been Approved by CMD, IPICOL"); //TO GM(SLNA) 
                SubmitAction("AR", Convert.ToInt32(Request.QueryString["ID"]), HdnCmdRemark.Value, "Agenda Form Approved Successfully", 3, 7);               
            }
           
        }
        catch (Exception)
        {
        }
    }

    protected void btnReturn_Click(object sender, EventArgs e)  // Return to GM SLNA
    {
        try
        {
            if (intType == 3)
            {
                ProjectNm = hdnProjNm.Value;
                MemberDetails(3, Convert.ToInt32(Request.QueryString["ID"]), hdnProjNm.Value, 4, " The Agenda form for " + ProjectNm + " has been returned by CMD, IPICOL");  //TO GM(SLNA) 
                SubmitAction("AR", Convert.ToInt32(Request.QueryString["ID"]), HdnCmdRemark.Value, "Agenda Form Return To GM(SLNA) Successfully", 3, 6);
            }
        }
        catch (Exception)
        { 
        
        }
    }

    public void MemberDetails(int MailType, int ProjectId, string ProjectName, int MemberType, string Subject)
    {
        string SenderName = string.Empty;
        string FromEmail = string.Empty;
        string NodalOfficer = string.Empty;
        string CCMail = string.Empty;

        objams = new AMS();
        DataSet ds = new DataSet();
        try
        {
            objams.Action = "M";
            objams.OfficerId = Convert.ToInt32(Session["UserId"].ToString());
            objams.TypeId = MemberType;
            objams.ProjectId = ProjectId;

            ds = AMServices.ViewSLFC(objams);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SenderName = ds.Tables[0].Rows[0]["SENDERNAME"].ToString();
                    FromEmail = ds.Tables[0].Rows[0]["SENDEREMAIL"].ToString();
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    NodalOfficer = ds.Tables[2].Rows[0]["OFFICERNAME"].ToString();
                    CCMail= ds.Tables[2].Rows[0]["OFFICEREMAIL"].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[1].Rows)
                    {
                        SendMail(MailType, Convert.ToString(item["intUserId"]), ProjectName, FromEmail, Convert.ToString(item["vchEmail"]), SenderName, Convert.ToString(item["VCHFULLNAME"]), Convert.ToString(item["vchMobTel"]), NodalOfficer, Subject, CCMail);
                    }
                }
            }
        }
        catch { }
        finally { objams = null; ds = null; }

    }

    public void SubmitAction(string StrAction, int ProjectId, string Comments, String strMessage, int ActionType, int ForwardStatus)
    {
        objams = new AMS();
        try
        {
            objams.Action = StrAction;
            objams.ProjectId = ProjectId;
            objams.OfficerId = Convert.ToInt32(Session["UserId"]);
            objams.COMMENT = Comments;
            objams.TypeId = ActionType;
            objams.ProjectStatus = ForwardStatus;
            objams.CreatedBy = Convert.ToInt32(Session["UserId"]);
            objams.intRemarkID = ViewState["RemarkId"] == "" ? 0 : Convert.ToInt32(ViewState["RemarkId"]);

            strVal = AMServices.TakeAction(objams);

            if (strVal == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "<script>alert('" + strMessage + "');location.href='CMDApprovalView.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "'</script>", false);
                //FillGrid();ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Proposal Details Updated Sucessfully.');location.href='ProjectMasterView.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
            }
            else
            {
                string msg = Messages.ShowMessage(strVal).ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "<script>alert('" + msg + "');location.href='CMDApproval.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
            }
        }
        catch { }
        finally { objams = null; }
    }


    public void SendMail(int MailType, string UserId, string ProjectName, string FromMailId, string Tomail, string SenderName, string ToName, string MobileNo, string NodalOfficerName, string Subject,string CcMail)
    {
        EmailMsg objMsg = new EmailMsg();
        SendEmail objEmail = new SendEmail();
        SMSGateway objsms = new SMSGateway();
        try
        {
            string strMessage2 = string.Empty;
            strMessage2 = "Dear Sir/Madam,</br></br>";

            if (MailType == 1)//GM Send To SLFC Member For Clarification
                strMessage2 += "GM, SLNA has ask clarification against the Agenda form for " + ProjectName + " for further action.,</br></br>You should respond with in 2 days";

            else if (MailType == 2)//GM Foraward To CDM IPICOL
                strMessage2 += "Nodal Officer Mr. " + NodalOfficerName + " has prepared the Agenda form for " + ProjectName + ",which has been checked by GM,SLNA. Put up for your views and comments.,</br></br>";

            else if (MailType == 3)//CDM IPICOL Return To GM 
                strMessage2 += "The Agenda form for " + ProjectName + " has been returned by CMD, IPICOL, for your necessary action.</br></br>";

            else if (MailType == 4) //TO GM
                strMessage2 += "The Agenda form for " + ProjectName + " has been approved by CMD, IPICOL, for your necessary action.</br></br>";

            else if (MailType == 5) //TO CDM
                strMessage2 += "The Agenda form for " + ProjectName + " has been submitted by GM,SLNA for SLSWCA</br></br>";
            

            strMessage2 += "<div>Please login to Agenda Portal for more details.</div>";
            objMsg.PHeader = "";
            objMsg.FromMailId = FromMailId;
            objMsg.Message1 = "";
            objMsg.Message2 = strMessage2;
            objMsg.Grid = "";
            objMsg.Subject = Subject;
            objMsg.ToMailId = Tomail;
            objMsg.CcMailId = CcMail;
            objMsg.status = "1";
            objMsg.ids = UserId;
            objEmail.ConfigureMail(objMsg);
            if (MobileNo != "")
            {
                string status = objsms.sendBulkSMS(MobileNo, Subject, 1, UserId);
            }

        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            objEmail = null;
            objMsg = null;
            objsms = null;
        }
    }

}