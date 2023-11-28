using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SingleWindow_ForwardProjectAdd : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    DataTable dt = null;
    string strVal = "";
    int gIntRowsCount;
    int ProjectId;
    string ProjectName = "";
    int intProposalId = 0;
    int intDetailId = 0;
    int intFinanceId = 0;
    int intFeedback = 0;
    int intStatus = 0;
    int intFinanceDoc = 0;
    #endregion

    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["Userid"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }
    }

    #endregion

    #region "Fill Grid"

    private void FillGrid()
    {
        Agenda objcs = new Agenda();
        dt = new DataTable();
        try
        {
            objcs.Action = "VF";
            objcs.UserId = Convert.ToInt32(Session["UserId"]);
            dt = new DataTable();
            dt = AMServices.FillActiveProject(objcs);
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
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objcs = null; dt = null; }
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


    #region "User Function"

    public void ForwardProject(string ProjectName, int ProjectId, int ForwardStatus, string Comments, String strMessage)
    {
        objams = new AMS();
        try
        {
            objams.Action = "FP";
            objams.ProjectId = ProjectId;
            objams.OfficerId = Convert.ToInt32(Session["UserId"]);
            objams.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
            objams.COMMENT = Comments;
            objams.TypeId = 1;
            objams.ProjectStatus = ForwardStatus;

            strVal = AMServices.TakeAction(objams);
            string msg = Messages.ShowMessage(strVal).ToString();
            if (strVal == "2")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('" + strMessage + "');window.location.href='ForwardProjectAdd.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "';", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('" + msg + "');window.location.href='ForwardProjectAdd.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "';", true);
        }
        catch { }
        finally { objams = null; }
    }

    public void MemberDetails(string ProjectName, int MmeberType)
    {
        string SenderName = string.Empty;
        string FromEmail = string.Empty;

        objams = new AMS();
        DataSet ds = new DataSet();
        try
        {
            objams.Action = "M";
            objams.OfficerId = Convert.ToInt32(Session["UserId"].ToString());
            objams.TypeId = MmeberType;

            ds = AMServices.ViewSLFC(objams);
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
                        SendAgendaMail(ProjectName, FromEmail, Convert.ToString(item["vchEmail"]), SenderName, Convert.ToString(item["VCHFULLNAME"]), Convert.ToString(item["vchMobTel"]));
                    }
                }
            }
        }
        catch { }
        finally { objams = null; ds = null; }

    }

    #endregion

    #region "Gridview Event"

    protected void grdProjmst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "SLFC")
            {
                GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                int RowIndex = gvr.RowIndex;
                ProjectId = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[4].ToString());
                ProjectName = grdProjmst.Rows[RowIndex].Cells[1].Text;

                //TO CHECK ALL THE DETAILS ADDED OR NOT

                intProposalId = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[0]);
                intDetailId = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[1]);
                intFinanceId = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[2]);
                //intFinanceDoc = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[8]);

                if (intProposalId == 0)
                {
                    ScriptManager.RegisterStartupScript(updPanel1, this.GetType(), "propsal", "alert('Please add project propsal against project- " + grdProjmst.Rows[RowIndex].Cells[1].Text + " to forward');", true);
                    return;
                }
                else if (intDetailId == 0)
                {
                    ScriptManager.RegisterStartupScript(grdProjmst, this.GetType(), "OnClick", "alert('Please add project details against project- " + grdProjmst.Rows[RowIndex].Cells[1].Text + " to forward');", true);
                    return;
                }
                else if (intFinanceId == 0)
                {
                    ScriptManager.RegisterStartupScript(grdProjmst, this.GetType(), "OnClick", "alert('Please add project finance details against project- " + grdProjmst.Rows[RowIndex].Cells[1].Text + " to forward');", true);
                    return;
                }
                //else if (intFinanceDoc == 0)
                //{
                //    ScriptManager.RegisterStartupScript(grdProjmst, this.GetType(), "OnClick", "alert('Please add financial documents against project- " + grdProjmst.Rows[RowIndex].Cells[1].Text + " to forward');", true);
                //    return;
                //}
                else
                {
                    MemberDetails(ProjectName, 2);

                    ForwardProject(ProjectName, ProjectId, 2, "Agenda Form Forwarded To SLFC Member For Review", "Agenda Form Forwarded To SLFC Member Successfully");
                }
            }
            else if (e.CommandName == "GM")
            {
                GridViewRow gvr = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                int RowIndex = gvr.RowIndex;
                intStatus = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[3]);
                intFeedback = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[5]);
                int intMember = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[6]);
                int intDays = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[7]);
                if (intDays < 7 & intFeedback != intMember)
                {
                    ScriptManager.RegisterStartupScript(updPanel1, this.GetType(), "propsal", "alert('Sorry, All SLFC Members are not given their feedback');", true);
                    return;
                }
                else if (intStatus == 2)
                {
                    ProjectId = Convert.ToInt32(grdProjmst.DataKeys[RowIndex].Values[4].ToString());
                    ProjectName = grdProjmst.Rows[RowIndex].Cells[1].Text;
                    MemberDetails(ProjectName, 3);

                    ForwardProject(ProjectName, ProjectId, 3, "Forwarded To GM(SLNA)", "Agenda Form Forwarded To GM(SLNA) Successfully");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(grdProjmst, this.GetType(), "OnClick", "alert('Please Forward To SLFC Member Before Forward To GM(SLNA)');", true);
                    return;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { }
    }

    protected void grdProjmst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int intProposalId = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[0]);
            int intDetailsId= Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[1]);
            int intFinanceId = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[2]);
            int intStaus = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[3]);
            int intFeedback = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[5]);
            Button btnSLFC = (Button)e.Row.FindControl("btnSLFC");
            Button btnGM = (Button)e.Row.FindControl("btnGM");
            // CHANGE AS REOPEN STATUS
            int intReOpenStatus = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[9]);
            if (intStaus == 0)
            {
                btnGM.Enabled = false;
            }
            else if (intStaus == 2)
            {
                btnSLFC.Text = "Forwarded";
                btnSLFC.Enabled = false;
            }
            else if (intStaus == 4)
            {
                btnSLFC.Text = "Forward";
                btnSLFC.Enabled = true;
            }
            else if (intStaus == 3)
            {
                btnSLFC.Text = "Forwarded";
                btnGM.Text = "Forwarded";
                btnSLFC.Enabled = false;
                btnGM.Enabled = false;
            }

            if (intFeedback == 0)
            {
                e.Row.Cells[7].Text = string.Empty;
                e.Row.Cells[7].Text = "NA";
            }

            if (intFinanceId == 0 || intProposalId == 0 || intDetailsId==0)
            {
                e.Row.Cells[5].Text = string.Empty;
            }
            if (intReOpenStatus == 1)
            {
                e.Row.Style.Add("border-left", "4px solid #ff4348");
            }           
        }
    }

    #endregion

    #region Mail

    public void SendAgendaMail(string ProjectName, string FromMailId, string Tomail, string SenderName, string ToName, string MobileNo)
    {
        EmailMsg objMsg = new EmailMsg();
        SendEmail objEmail = new SendEmail();
        SMSGateway objsms = new SMSGateway();
        try
        {
            string strMessage2 = string.Empty;
            strMessage2 = "Dear Sir/Madam,</br></br>";
            strMessage2 += "Mr. " + Session["fullName"] + " has uploaded the Agenda form for " + ProjectName + ". Please review the same and give your comments.,</br></br>";
            strMessage2 += "<div>Please login to Agenda Portal for more details.</div>";

            objMsg.PHeader = "";
            objMsg.FromMailId = FromMailId;
            objMsg.Message1 = "";
            objMsg.Message2 = strMessage2;
            objMsg.Grid = "";
            objMsg.Subject = "Review agenda form for " + ProjectName;
            objMsg.ToMailId = Tomail;
            objMsg.status = "1";
            objMsg.ids = Session["UserId"].ToString();
            objEmail.ConfigureMail(objMsg);

            if (MobileNo != "")
            {
                string status = objsms.sendBulkSMS(MobileNo, " Mr. " + Session["fullName"] + " has uploaded the agenda form for " + ProjectName + " to review", 1, Session["UserId"].ToString());
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

   
}