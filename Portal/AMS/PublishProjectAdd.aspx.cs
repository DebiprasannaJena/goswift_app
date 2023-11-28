using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class SingleWindow_PublishProjectAdd : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    DataTable dt = null;
   
    string strVal = "";
    int ProjectId;
    string ProjectName = "";
    int gIntRowsCount;
    static int intType = 0;
    string PID = "";
    string Pname = "";

    #endregion

    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
                FillGrid();
                FillMember();
            }
        }
        FEClarification.ValidChars = FEClarification.ValidChars + "\r\n";
    }

    #endregion

    #region "Get SLFC Member"

    private void FillMember()
    {
        Agenda objA = new Agenda();

        objA.Action = "E";
        objA.OfficerType = 2;
        dt = new DataTable();
        dt = AMServices.ViewOfficers(objA);
        if (dt.Rows.Count > 0)
        {
            cblMember.DataSource = dt;
            cblMember.DataValueField = dt.Columns["intUserId"].ColumnName;
            cblMember.DataTextField = dt.Columns["Fullname"].ColumnName;
            cblMember.DataBind();
        }
       
    }

    #endregion

    #region "Fill Grid"
    private void FillGrid()
    {
        AMS objams = new AMS();

        objams.Action = "VP";
        dt = new DataTable();
        dt = AMServices.ViewProjectMaster(objams);
        grdProjmst.DataSource = dt;
        grdProjmst.DataBind();
      
        gIntRowsCount = dt.Rows.Count;
        if (gIntRowsCount > 0)
        {
            DisplayPaging();
            lblMessage.Visible = false;
            if (intType == 3)
            {
                grdProjmst.Columns[7].Visible = false;
                grdProjmst.Columns[8].Visible = false;
                grdProjmst.Columns[0].Visible = false;
                //grdProjmst.Columns[10].Visible = false; //Added by Surya Prakash Barik as Reject Button not show to CMD 
            }
            if (intType == 4)
            {
                grdProjmst.Columns[10].Visible = false;   // Added by Surya Prakash Barik  as In CMD login Show Reject Button. Above line Commented.            
            }
        }
        else
        {
            lblMessage.Visible = true;
            lbtnAll.Visible = false;
            lblPaging.Visible = false;
        }
    }
    #endregion

    #region "Paging"
    private void DisplayPaging()
    {
        if (this.grdProjmst.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
        }
        if (this.grdProjmst.PageIndex + 1 == this.grdProjmst.PageCount)
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + gIntRowsCount + "</b> of <b>" + gIntRowsCount + "</b>";
        }
        else
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((grdProjmst.PageSize - 1))) + "</b> of <b>" + gIntRowsCount + "</b>";
        }

    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "View Paging";
            grdProjmst.AllowPaging = false;
            grdProjmst.PageIndex = 0;
        }
        else
        {
            lbtnAll.Text = "All";
            grdProjmst.AllowPaging = true;
        }
        FillGrid();
    }
    #endregion

    #region "Gridview Event"
    
    protected void grdProjmst_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int intStaus = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[0]);
            int intReOpenStatus = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[7]);
            LinkButton lbtnForward = (LinkButton)e.Row.FindControl("lbtnForward");
            LinkButton lbtnPublish = (LinkButton)e.Row.FindControl("lbtnPublish");
            LinkButton lnkbtnReturn = (LinkButton)e.Row.FindControl("lnkbtnReturn");
            LinkButton lnkbtnSend = (LinkButton)e.Row.FindControl("lnkbtnSend");        
            HtmlAnchor CMDCmnt = (HtmlAnchor)e.Row.FindControl("CMDCmnt");
            HyperLink hlEdit = (HyperLink)e.Row.FindControl("hlEdit");
            HiddenField hdnCmdRmk = (HiddenField)e.Row.FindControl("hdnCMDRmk");
            if (intReOpenStatus == 1)
            {
                e.Row.Style.Add("border-left", "4px solid #ff4348");
               
            }
            if (intType == 4)
            {
                hlEdit.Visible = true;
                if (hdnCmdRmk.Value != "0")  //Need to show all comments accepted or return so count > 1
                     CMDCmnt.Visible = true;
                else
                    CMDCmnt.Visible = false;
            }
            else
            {
                hlEdit.Visible = false;
                CMDCmnt.Visible = false;
            }

            if (intStaus == 3)
            {
                lbtnForward.Visible = true;
                lbtnPublish.Visible = false;
                lnkbtnSend.Visible = true;
                lnkbtnReturn.Visible = false;
            }
            else if (intStaus == 4 || intStaus == 9)
            {
                lnkbtnSend.Visible = false;
                lbtnForward.Visible = true;
                lbtnPublish.Visible = false;
                lnkbtnReturn.Visible = false;
              
            }
            else if (intStaus == 6)
            {
                lnkbtnSend.Visible = false;
                lbtnForward.Visible = true;
                lbtnPublish.Visible = false;
                lnkbtnReturn.Visible = false;
                if (intType == 4)                            // Add By Surya, When CMD Return an Agenda Send clareification will be reenabled.
                {
                    lnkbtnSend.Visible = true;
                }
            }
            else if (intStaus == 5) //|| intStaus == 10)
            {
                lnkbtnSend.Visible = false;
                lbtnForward.Visible = false;
                lbtnPublish.Visible = false;
                lnkbtnReturn.Visible = false;
                //if (intType == 3)                          // commented By Surya Prakash Barik as CMD Approval given in Step 7. so no need to show Action
                //    lnkbtnReturn.Visible = true;
                //else
                //    lnkbtnReturn.Visible = false;
               
            }
          
            else if (intStaus == 7)
            {
                lnkbtnSend.Visible = false;
                lbtnForward.Visible = false;                         
                lnkbtnReturn.Visible = false;
                //if (intType == 3)
                //    lbtnPublish.Visible = false;
                //else
                //    lbtnPublish.Visible = true;    
                if (intType == 4)                           // Changed By Surya as CMD Approval given in Step 7. so above if Codition is commented.
                {
                    lbtnPublish.Visible = true;
                    lnkbtnReturn.Visible = true;
                }
                else
                {
                    lbtnPublish.Visible = false;
                    lnkbtnReturn.Visible = false;
                }

            }
            else 
            {
                lnkbtnSend.Visible = false;
                lbtnPublish.Visible = false;
                lbtnForward.Visible = false;
                lnkbtnReturn.Visible = false;
            }
        }

    }
    
    protected void grdProjmst_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        objams = new AMS();
        try
        {
            if (e.CommandName == "Send")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                //int id = Convert.ToInt32(grdProjmst.DataKeys[rowIndex].Values[1]);
                //hdnId.Value =Convert.ToString(id);
                //PID = hdnId.Value;
                //ProjectName = grdProjmst.DataKeys[rowIndex].Values[6].ToString();
                //hdName.Value = ProjectName;
                //Pname = hdName.Value;
                ModalPopupExtender1.Show();
                Control ctl = e.CommandSource as Control;
                GridViewRow CurrentRow = ctl.NamingContainer as GridViewRow;
                int id = Convert.ToInt32(grdProjmst.DataKeys[CurrentRow.RowIndex].Values[1]);
                ViewState["Pid"] = id.ToString();

                string ProjectName = Convert.ToString(grdProjmst.DataKeys[CurrentRow.RowIndex].Values[6]);
                ViewState["Pname"] = ProjectName.ToString();
                

                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "CModal('" + id + "' , 'Send Clarification','" + ProjectName + "') ;", true);

            }
            else if (e.CommandName == "Forward")
            {
                int RowIndex = int.Parse(e.CommandArgument.ToString());                
                ProjectId = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[1].ToString());
                ProjectName = grdProjmst.DataKeys[RowIndex].Values[6].ToString();

                int intStatus = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[0]);
                int intMember = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[2]);
                int intComment = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[3]);
                int intDays = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[4]);
                if (intStatus == 4 || intStatus == 6 || intStatus == 9)
                {
                    if (intComment != intMember)
                    {
                        ScriptManager.RegisterStartupScript(updPanel1, this.GetType(), "propsal", "alert('Sorry, SLFC Members are not given any clarification');", true);
                        return;
                    }
                }

                ////Check 1st time forward to CMD or Resend to CMD for Approval
                //objams = new AMS();
                //dt = new DataTable();
                //objams.Action = "FT";
                //objams.ProjectId = ProjectId;
                //objams.OfficerId = Convert.ToInt32(Session["UserId"]);
                //objams.ProjectStatus = 5;

                //strVal = AMServices.TakeAction(objams);

                MemberDetails(2, ProjectId, ProjectName, 3, " GM(SLNA) has forwarded the agenda form for " + ProjectName + " to review");
                //if (strVal == "0")
                SubmitAction("FP", ProjectId, "GM(SLNA) has forwarded to CMD for review", "Agenda Form Forwarded Successfully", 4, 5);
                //else if (strVal == "1")
                //        SubmitAction("FP", ProjectId, "GM(SLNA) Resend to CMD for review", "Agenda Form Forwarded Successfully", 4, 10);
            }
            //else if (e.CommandName == "Action")
            //{
            //    int rowIndex = int.Parse(e.CommandArgument.ToString());
            //    int id = Convert.ToInt32(grdProjmst.DataKeys[rowIndex].Values[1]);
            //    ProjectName = grdProjmst.DataKeys[rowIndex].Values[6].ToString();
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "Model('" + id + "' , 'Take Action','" + ProjectName + "') ;", true);

            //}
            else if (e.CommandName == "Reject")
            {
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                int id = Convert.ToInt32(grdProjmst.DataKeys[rowIndex].Values[1]);
                ProjectName = grdProjmst.DataKeys[rowIndex].Values[6].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "Model('" + id + "' , 'Reject Agenda','" + ProjectName + "') ;", true);

            }

            else if (e.CommandName == "Publish")
            {
                int RowIndex = int.Parse(e.CommandArgument.ToString());
                ProjectId = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[1].ToString());
                ProjectName = grdProjmst.DataKeys[RowIndex].Values[6].ToString();
                int intDecision = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[5]);
         
                if (intDecision == 0)
                {
                    ScriptManager.RegisterStartupScript(updPanel1, this.GetType(), "propsal", "alert('Please add SLFC Decision point before publish the project');", true);
                    return;
                }
                MemberDetails(5, ProjectId, ProjectName, 3, "Published Agenda form of " + ProjectName);

                SubmitAction("FP", ProjectId, "Agenda Form Submitted For SLSWCA", "Agenda Form Published Successfully", 4, 1);

                //--Added by Surya Prakash Barik
                //--After Successfull Publish Send details to Single Window if the Published Project comes from Single Window

                objams = new AMS();
                dt = new DataTable();

                objams.ProjectId = ProjectId;
                objams.Action = "PN";                                //Get Project Name and VCH_UID and land Value of Step 3
                dt = AMServices.ViewFinace(objams);                  // Checking Project comes from Single window or not.

                if (dt.Rows.Count == 1)
                {
                    string UID = dt.Rows[0]["VCH_UID"].ToString();

                    if (UID != "")                                  // if Comes from Single window
                    {

                        //SWPSVC.AgendaBALClient obj = new SWPSVC.AgendaBALClient();
                        //string retval = obj.AddAgenda(2, UID, "Agenda Successfully Approved", System.Configuration.ConfigurationManager.AppSettings["AMS"] + "/Mom.aspx?ID=" + ProjectId, dt.Rows[0]["Land"].ToString());

                        objams = new AMS();
                        objams.strUID = UID;
                        objams.TypeId = 2;
                        objams.Remark = "Agenda Successfully Approved";
                        objams.strUrl = "Portal/AMS/Mom.aspx?ID=" + ProjectId;
                        objams.strlandVal = dt.Rows[0]["Land"].ToString();
                        string retval =AMServices.UpdateStatus(objams);

                        //AMS objams = new AMS();
                        objams.Remark = 2 + ";" + UID + ";" + "Agenda Successfully Approved" + ";" + System.Configuration.ConfigurationManager.AppSettings["AMS"] + "/Mom.aspx?ID=" + ProjectId + ";" + dt.Rows[0]["Land"].ToString();

                        string strRes = AMServices.InsertStatus(objams);
                    }
                }

                //SWPSVC.AgendaBALClient obj = new SWPSVC.AgendaBALClient();
                //string retval = obj.AddAgenda(2, "201712345", "Agenda Successfully Approved", System.Configuration.ConfigurationManager.AppSettings["AMS"] + "/Mom.aspx?ID=" + 3048,"1.0");


                //objams = new AMS();
                //objams.Remark = 2 + ";" + "201712345" + ";" + "Agenda Successfully Approved" + ";" + System.Configuration.ConfigurationManager.AppSettings["AMS"] + "/Mom.aspx?ID=" + 3048 +";"+ "1.0";

                //string strRes = AMServices.InsertStatus(objams); 
             }

        }
        catch (Exception ex)
        {
            objams = new AMS();
            objams.Remark = ex.Message;
            string strRes = AMServices.InsertStatus(objams);
        }
        finally 
        { 
            objams = null;
        }

    }
    
    #endregion



    #region Take Action

    #region Button Event

    //=============Commented by Surya Prakash as only 1 Button Required fro Rejection
    //protected void btnAccept_Click(object sender, EventArgs e)
    //{
    //    //if (intType == 3)  //CMD IPICOL
    //    //{
    //    MemberDetails(4, Convert.ToInt32(hdnId.Value), hdName.Value, 4, " The Agenda form for " + ProjectName + " has been Approved by CMD, IPICOL"); //TO GM(SLNA) 
    //    SubmitAction("FP", Convert.ToInt32(hdnId.Value), txtRemark.Text, "Agenda Form Approved Successfully", 3, 7);   
        
    //    //}

    //}

    protected void btnReturn_Click(object sender, EventArgs e)
    {
       
        ////Added by Surya Prakash Barik
        //// After Rejection Status is 8.
        //MemberDetails(5, Convert.ToInt32(hdnId.Value), hdName.Value, 3, "Rejected Agenda form of " + hdName.Value);
        //SubmitAction("FP", Convert.ToInt32(hdnId.Value), txtRemark.Text.Trim(), "Agenda Form Rejected Successfully", 4, 8);

        ////Added by Surya Prakash Barik
        ////After Successfull Publish Send details to Single Window if the Published Project comes from Single Window
        //try
        //{
        //    objams = new AMS();
        //    dt = new DataTable();

        //    objams.ProjectId = Convert.ToInt32(hdnId.Value);
        //    objams.Action = "PN";                                //Get Project Name and VCH_UID
        //    dt = AMServices.ViewFinace(objams);                  // Checking Project comes from Single window or not.

        //    if (dt.Rows.Count == 1)
        //    {
        //        string UID = dt.Rows[0]["VCH_UID"].ToString();

        //        if (UID != "")                                  // if Comes from Single window
        //        {
        //            SWPSVC.AgendaBALClient obj = new SWPSVC.AgendaBALClient();
        //            string retval = obj.AddAgenda(3, UID, txtRemark.Text.Trim(), "", dt.Rows[0]["Land"].ToString());
        //        }
        //    }

        //}
        //catch (Exception ex)
        //{
        //    throw new Exception(ex.Message);
        //}
        //finally { objams = null; }
    }
   

    #endregion

    #region Send Clarification

    protected void btnSend_Click(object sender, EventArgs e)
    {
        string Member = string.Empty;
        foreach (ListItem item in cblMember.Items)
        {
            if (item.Selected)
            {
                Member = Member + item.Value + ',';
            }
        }
        Member = Member == "" ? "0" : Member.Trim(',');
        GetSLFCMember(hdName.Value, 4, Member);
        SendClarification(Member, Convert.ToInt32(ViewState["Pid"]), "Agenda Form Send TO SLFC Member For Clarification");
    }

    public void GetSLFCMember(string ProjectName, int MmeberType, string MemberId)
    {
        string SenderName = string.Empty;
        string FromEmail = string.Empty;

        objams = new AMS();
        DataSet ds = new DataSet();
        try
        {
            objams.Action = "M";
            objams.SLFCMember = MemberId;
            objams.OfficerId = Convert.ToInt32(Session["UserId"].ToString());
            objams.TypeId = MmeberType;
            objams.ProjectId = Convert.ToInt32(ViewState["Pid"]);
            ds = AMServices.ViewNodalOfficerMailinfo(objams);
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    SenderName = ds.Tables[0].Rows[0]["SENDERNAME"].ToString();
                    FromEmail = ds.Tables[0].Rows[0]["SENDEREMAIL"].ToString();
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[1].Rows)
                    {
                        SendMail(1, Convert.ToString(item["intUserId"]), ProjectName, Session["EmailId"].ToString(), Convert.ToString(item["EmailId"]), SenderName, Convert.ToString(item["MemberName"]), Convert.ToString(item["MobileNo"]), "", "GM, SLNA ask some clarification against " + ProjectName);
                    }
                }
            }
        }
        catch { }
        finally { objams = null; ds = null; }

    }

    public void SendClarification(string Mmeber, int ProjectId, String strMessage)
    {
       
        try
        {
            //Check Clarification Fresh entry or Re-Clarification
            objams = new AMS();
            objams.Action = "FT";
            objams.ProjectId = ProjectId;
            objams.OfficerId = Convert.ToInt32(Session["UserId"]);
            objams.ProjectStatus = 4;
            strVal = AMServices.TakeAction(objams);

            //===========================
            objams = new AMS();

            if (strVal == "0")
            {
                objams.ProjectStatus = 4;
                objams.COMMENT = txtClarification.Text.Trim();
            }
            else if (strVal == "1")
            {
                objams.ProjectStatus = 9;
                objams.COMMENT = "Re-Clarification Required by GM(SLNA)";
            }

            objams.Action = "C";
            objams.ProjectId = ProjectId;
            objams.OfficerId = Convert.ToInt32(Session["UserId"]);
            objams.SLFCMember = Mmeber;
            objams.TypeId = 4;
            objams.CreatedBy = Convert.ToInt32(Session["UserId"]);

            strVal = AMServices.TakeAction(objams);
            if (strVal == "1")
            {
                string URL = "PublishProjectAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('" + strMessage + "');window.location.href='" + URL + "';", true);
            }
            else
            {
                string msg = Messages.ShowMessage(strVal).ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('" + msg + "');", true);
            }
        }
        catch { }
        finally { objams = null; }
    }
    
    #endregion

    #region Forward and Publish Project

    public void MemberDetails(int MailType, int ProjectId, string ProjectName, int MemberType, string Subject)
    {
        string SenderName = string.Empty;
        string FromEmail = string.Empty;
        string NodalOfficer = string.Empty;

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
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[1].Rows)
                    {
                        SendMail(MailType,Convert.ToString(item["intUserId"]), ProjectName, FromEmail, Convert.ToString(item["vchEmail"]), SenderName, Convert.ToString(item["VCHFULLNAME"]), Convert.ToString(item["vchMobTel"]), NodalOfficer, Subject);
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
            //ADDED by MONALISA NAYAK
            intType = AMServices.GetOfficersType(Convert.ToInt32(Session["Userid"]));
            objams.OfficerId = intType;
            //objams.OfficerId = Convert.ToInt32(Session["UserId"]);
            objams.COMMENT = Comments;
            objams.TypeId = ActionType;
            objams.ProjectStatus = ForwardStatus;
            objams.CreatedBy = Convert.ToInt32(Session["UserId"]);

           strVal = AMServices.TakeAction(objams);
            
            if (strVal == "2")
            {
              
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "<script>alert('" + strMessage + "');location.href='PublishProjectAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&RNum=" + Request.QueryString["RNum"] + "'</script>", false);
                FillGrid();
            }
            else
            {
                string msg = Messages.ShowMessage(strVal).ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "<script>alert('" + msg + "');location.href='PublishProjectAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&RNum=" + Request.QueryString["RNum"] + "'</script>", false);
            }
        }
        catch { }
        finally { objams = null; }
    }

    
    public void SendMail(int MailType, string UserId, string ProjectName, string FromMailId, string Tomail, string SenderName, string ToName, string MobileNo, string NodalOfficerName, string Subject)
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

    #endregion



    #endregion

    protected void grdProjmst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdProjmst.PageIndex = e.NewPageIndex;
        FillGrid();
    }
}