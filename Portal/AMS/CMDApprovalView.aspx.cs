using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_CMDApprovalView : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    DataTable dt = null;
    string strVal = "";
    int gIntRowsCount;
    static int intType = 0;
    string ProjectName = "";
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
                if (grdProjmst.AllowPaging == true)
                {
                    grdProjmst.PageIndex = string.IsNullOrEmpty(Request.QueryString["PIndex"]) ? 0 : Convert.ToInt32(Request.QueryString["PIndex"]);
                }
                FillGrid();
                ViewCMDComments();
            }
        }
    }

    #endregion

    #region "Fill Grid"

    private void FillGrid()
    {
        AMS objams = new AMS();

        objams.Action = "VCMD";
        objams.OfficerId = Convert.ToInt32(Session["UserId"]);
        objams.TypeId = intType;
        dt = new DataTable();
        dt = AMServices.ViewProjectMaster(objams);
        grdProjmst.DataSource = dt;
        grdProjmst.DataBind();
        gIntRowsCount = dt.Rows.Count;
        if (gIntRowsCount > 0)
        {
            DisplayPaging();
            lblMessage.Visible = false;
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
    protected void grdProjmst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdProjmst.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    #endregion

    #region "Gridview Command"

    protected void grdProjmst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objams = new AMS();
        try
        {

          if (e.CommandName == "E")
            {
                var s = Convert.ToString(e.CommandArgument).Split(',');
                if (s.Count() == 1)
                {
                    objams.ProjectId = Convert.ToInt32(0);
                }
                Session["PType"] = null;
                objams.ProjectId = Convert.ToInt32(s[0]);
                Response.Redirect("ProjectMasterAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + objams.ProjectId + "&PIndex=" + grdProjmst.PageIndex.ToString() + "", false);
            }
          if (e.CommandName == "A")
          {            
            //if (intType == 3)
            //{
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                hdnProjNm.Value= grdProjmst.DataKeys[rowIndex].Values[2].ToString();
                int id = Convert.ToInt32(grdProjmst.DataKeys[rowIndex].Values[1]);
                ProjectName = hdnProjNm.Value;
                //MemberDetails1(4, id, hdnProjNm.Value, 4, " The Agenda form for " + ProjectName + " has been Approved by CMD, IPICOL"); //TO GM(SLNA) 
                SubmitAction1("AR", id, HdnCmdRemark.Value, "Agenda Form Approved Successfully", 3, 7);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('Agenda Form Approved Successfully');", true);
                FillGrid();
          }

            else if (e.CommandName == "Reject")
            {

                int rowIndex = int.Parse(e.CommandArgument.ToString());
                //int id = Convert.ToInt32(grdProjmst.DataKeys[rowIndex].Values[1]);
                //ProjectName = grdProjmst.DataKeys[rowIndex].Values[2].ToString();
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "msg1", "Model('" + id + "' , 'Reject Agenda','" + ProjectName + "') ;", true);
                ModalPopupExtender1.Show();
                Control ctl = e.CommandSource as Control;
                GridViewRow CurrentRow = ctl.NamingContainer as GridViewRow;
                int id = Convert.ToInt32(grdProjmst.DataKeys[CurrentRow.RowIndex].Values[1]);
                ViewState["Pid"] = id.ToString();
     

                string ProjectName = Convert.ToString(grdProjmst.DataKeys[CurrentRow.RowIndex].Values[2]);
                ViewState["Pname"] = ProjectName.ToString();
               


            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objams = null; }

    }

    public void MemberDetails1(int MailType, int ProjectId, string ProjectName, int MemberType, string Subject)
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
                    CCMail = ds.Tables[2].Rows[0]["OFFICEREMAIL"].ToString();
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
    public void SubmitAction1(string StrAction, int ProjectId, string Comments, String strMessage, int ActionType, int ForwardStatus)
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

            //if (strVal == "2")
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "<script>alert('" + strMessage + "');location.href='CMDApprovalView.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "'</script>", false);
                
            //}
            //else
            //{
            //    string msg = Messages.ShowMessage(strVal).ToString();
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "<script>alert('" + msg + "');location.href='CMDApproval.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
            //}
        }
        catch { }
        finally { objams = null; }
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
                DataView dv1 = new DataView(dt);
                dv1.RowFilter = "intCreatedBy=2";  //CMD

                DataView dv2 = new DataView(dt);
                dv2.RowFilter = "intCreatedBy=3"; // GM

                HdnCmdRemark.Value = dv1[dv1.Count - 1][0].ToString().Replace("<br />", " . ");
                ViewState["RemarkId"] = dv1[dv1.Count - 1][4].ToString();
              
            }
        }
        catch (Exception)
        {

        }
    }
    protected void grdProjmst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int intFinDoc = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[3]);
            int intReOpenStatus = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[4]);

            if (intFinDoc == 0)
                e.Row.Cells[7].Text = "";
            if (intReOpenStatus == 1)
            {
                e.Row.Style.Add("border-left", "4px solid #ff4348");
            }
           

        }
     
    }
    #endregion

    protected void btnReturn_Click(object sender, EventArgs e)
    {
        //Added by Surya Prakash Barik
        // After Rejection Status is 8.
        MemberDetails(6, Convert.ToInt32(ViewState["Pid"]), hdName.Value, 4, "Rejected Agenda form of " + ViewState["Pname"]);
        SubmitAction("AR", Convert.ToInt32(ViewState["Pid"]), txtRemark.Text.Trim(), "Agenda Form Rejected Successfully", 3, 8);

        //Added by Surya Prakash Barik
        //After Reject Send details to Single Window if the Published Project comes from Single Window
        try
        {
            objams = new AMS();
            dt = new DataTable();

            objams.ProjectId = Convert.ToInt32(hdnId.Value);
            objams.Action = "PN";                                //Get Project Name and VCH_UID
            dt = AMServices.ViewFinace(objams);                  // Checking Project comes from Single window or not.

            if (dt.Rows.Count == 1)
            {
                string UID = dt.Rows[0]["VCH_UID"].ToString();

                if (UID != "")                                  // if Comes from Single window
                {
                    //SWPSVC.AgendaBALClient obj = new SWPSVC.AgendaBALClient();
                    //string retval = obj.AddAgenda(3, UID, txtRemark.Text.Trim(), "", dt.Rows[0]["Land"].ToString());

                    objams = new AMS();
                    objams.strUID = UID;
                    objams.TypeId = 3;
                    objams.Remark = txtRemark.Text.Trim();
                    objams.strUrl = "";
                    objams.strlandVal = dt.Rows[0]["Land"].ToString();
                    string retval = AMServices.UpdateStatus(objams);

                    objams = new AMS();
                    objams.Remark = 3 + ";" + UID + ";" + txtRemark.Text.Trim() + ";" + dt.Rows[0]["Land"].ToString();

                    string strRes = AMServices.InsertStatus(objams);
                }
            }
           
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
            objams = new AMS();
            objams.Remark = ex.Message;
            string strRes = AMServices.InsertStatus(objams);
        }
        finally { objams = null; }
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
                    CCMail = ds.Tables[2].Rows[0]["OFFICEREMAIL"].ToString();
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

            strVal = AMServices.TakeAction(objams);

            if (strVal == "2")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "<script>alert('" + strMessage + "');location.href='CMDApprovalView.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&RNum=" + Request.QueryString["RNum"] + "'</script>", false);
                FillGrid();
            }
            //else
            //{
            //    string msg = Messages.ShowMessage(strVal).ToString();
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "<script>alert('" + msg + "');location.href='PublishProjectAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&RNum=" + Request.QueryString["RNum"] + "'</script>", false);
            //}
        }
        catch { }
        finally { objams = null; }
    }
    
    public void SendMail(int MailType, string UserId, string ProjectName, string FromMailId, string Tomail, string SenderName, string ToName, string MobileNo, string NodalOfficerName, string Subject, string CCMail)
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
          
            else if (MailType == 6) //TO GM
                strMessage2 += "The Agenda form for " + ProjectName + " has been Rejected by CMD, IPICOL</br></br>";

            strMessage2 += "<div>Please login to Agenda Portal for more details.</div>";
            objMsg.PHeader = "";
            objMsg.FromMailId = FromMailId;
            objMsg.Message1 = "";
            objMsg.Message2 = strMessage2;
            objMsg.Grid = "";
            objMsg.Subject = Subject;
            objMsg.ToMailId = Tomail;
            objMsg.CcMailId = CCMail;
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
