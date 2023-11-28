using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class SingleWindow_MemberCommentsAdd : System.Web.UI.Page
{
    #region "Member Variable"
    public string DecisionText { get; set; }
    AMS objams;
    Double x = 0;

    public bool isGM { get; set; }
    public bool isSLFC { get; set; }

    public bool IsGMVisible
    {
        get { return isGM; }
        set { isGM = value; }
    }
    public bool isSLFCVisible
    {
        get { return isSLFC; }
        set { isSLFC = value; }
    }
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
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    GetDetails();
                }
            }
        }
        FEComments.ValidChars = FEComments.ValidChars + "\r\n";
        FEClarification.ValidChars = FEClarification.ValidChars + "\r\n";
    }

    #endregion

    #region "Get Details"

    private void GetDetails()
    {
        AMS objams = new AMS();
        DataTable ObjDt = new DataTable();
        try
        {
            objams = new AMS();
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            objams.OfficerId = Convert.ToInt32(Session["UserId"]);
            objams.Action = "AV";
            DataSet ds = new DataSet();
            ds = AMServices.ViewMom(objams);

            if (ds.Tables[0].Rows.Count > 0)
            {

                fillProjdetails(ds);
                fillProposaldetails(ds);
                fillPromoterdetails(ds);
                fillProjInfo(ds);                
                fillFinancialdetails(ds);
                FillProjectCostDtls(ds);
                FillDistrictLocation(ds);
                FillMaterialSource(ds);
               
                try
                {
                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        rptCapacity.DataSource = ds.Tables[5];
                        rptCapacity.DataBind();
                      
                    }

                    if (ds.Tables[6].Rows.Count > 0)
                    {
                        txtComments.Text = ds.Tables[6].Rows[0]["VCHCOMMENT"].ToString();                        
                    }
                    if (ds.Tables[9].Rows.Count > 0)
                    {
                        rptFinDtls.DataSource = ds.Tables[9];
                        rptFinDtls.DataBind();
                    }
                  
                }
                catch (Exception m) { }
            }
        }
        catch (Exception m) { }
        finally { objams = null; }
    }

    #endregion



    #region for fetch data

    public void fillProjdetails(DataSet ds)
    {
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblProjTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJECT_TITLE"]);
                lblSector.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHSECTOR"]);
                lblProjNm.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJCT_NAME"]);
                lblApplDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DTMAPPLICATION_EBIZ"]).ToString("dd-MMM-yyyy");
                //lblLocation.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJCT_LOCATION"]);
                //lblProduct.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPRODUCT"]);
                //lblCapacity.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHCAPACITY"]);
                lblCategory.Text = Convert.ToString(ds.Tables[0].Rows[0]["CATEGORY"].ToString());
                lblNew.Text = ds.Tables[0].Rows[0]["PROJECTTYPE"].ToString();

                if (Convert.ToString(ds.Tables[0].Rows[0]["INTSTATUS"]) == "4" || Convert.ToString(ds.Tables[0].Rows[0]["INTSTATUS"]) == "9")
                {
                    txtComments.Enabled = false;
                    if (ds.Tables[7].Rows.Count > 0)
                    {
                        if (Convert.ToInt32(ds.Tables[7].Rows[0]["intUserId"]) == Convert.ToInt32(Session["UserId"]))
                        {
                            IsGMVisible = true;
                            lblComments.Text = ds.Tables[7].Rows[0]["VCHCOMMENT"].ToString();
                            if (Convert.ToInt32(ds.Tables[7].Rows[0]["NOOFDAYS"]) <= 2)
                            {
                                isSLFCVisible = true;
                                hdnStatus.Value = "4";
                                hdnClariID.Value = ds.Tables[7].Rows[0]["intSendId"].ToString();
                            }
                            else
                                isSLFCVisible = false;
                        }
                        else
                        {
                            IsGMVisible = false;
                            isSLFCVisible = false;
                        }
                    }
                }
            }
        }
        catch (Exception m) { }

    }

    public void fillProposaldetails(DataSet ds)
    {
        try
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                RptrProposal.DataSource = ds.Tables[1];
                RptrProposal.DataBind();
            }
        }
        catch (Exception m) { }
    }

    public void fillPromoterdetails(DataSet ds)
    {
        try
        {
            if (ds.Tables[2].Rows.Count > 0)
            {
                DataTable dtDirector = null;
                var rows = ds.Tables[2].AsEnumerable()
                    .Where(x => ((int)x["INTTYPE"]) == 1);

                if (rows.Any())
                {
                    dtDirector = rows.CopyToDataTable();
                    RptrPromoterDirectors.DataSource = dtDirector;
                    RptrPromoterDirectors.DataBind();

                }

                DataTable dtBusiness = null;
                var rows1 = ds.Tables[2].AsEnumerable()
                    .Where(x => ((int)x["INTTYPE"]) == 2);
                if (rows1.Any())
                {
                    dtBusiness = rows1.CopyToDataTable();
                    RptrPromoterBusiness.DataSource = dtBusiness;
                    RptrPromoterBusiness.DataBind();

                }

            }

        }
        catch (Exception m) { }
    }

    public void fillProjInfo(DataSet ds)
    {
        try
        {
            if (ds.Tables[3].Rows.Count > 0)
            {
                //DataTable dt = new DataTable();
                //DataColumn dc = new DataColumn("FinaceDetails", typeof(String));
                //dt.Columns.Add(dc);
                //string[] s = ds.Tables[3].Rows[0]["vchFinaceDetails"].ToString().Split('~');
                //for (int i = 0; i < s.Length; i++)
                //{
                //    DataRow dr = dt.NewRow();
                //    dr[0] = s[i].ToString();
                //    dt.Rows.InsertAt(dr, i);
                //}
                //rptFinDtls.DataSource = dt;
                //rptFinDtls.DataBind();

                //lblmeansfin.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchFinaceDetails"].ToString());
                lblFinDesc.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchFinanceDescription"].ToString());
                lblLand.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchLand"].ToString());
                lblWater.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchWater"].ToString());
                lblPower.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchPower"].ToString());

                //lblcivilstructure.Text = Convert.ToString(ds.Tables[3].Rows[0]["decCivilCost"].ToString());
                //lblplantsEquip.Text = Convert.ToString(ds.Tables[3].Rows[0]["decPlantCost"].ToString());
                //lblOthers.Text = Convert.ToString(ds.Tables[3].Rows[0]["decOtherCost"].ToString());
                //lblTotal.Text = Convert.ToString(Convert.ToDecimal(lblcivilstructure.Text) + Convert.ToDecimal(lblplantsEquip.Text) + Convert.ToDecimal(lblOthers.Text));
                //lblProjCost.Text = Convert.ToString(Convert.ToDecimal(lblcivilstructure.Text) + Convert.ToDecimal(lblplantsEquip.Text) + Convert.ToDecimal(lblOthers.Text)) + " Crores";

                //if ((Convert.ToString(ds.Tables[3].Rows[0]["vchPower"].ToString()) == "1"))
                //{
                //    lblCPP.Text = "CPP";
                //}
                //else
                //{
                //    lblCPP.Text = "GRID";
                //}

                if ((Convert.ToString(ds.Tables[3].Rows[0]["intPowerSource"].ToString()) == "1"))
                    lblCPP.Text = "CPP";
                else if ((Convert.ToString(ds.Tables[3].Rows[0]["intPowerSource"].ToString()) == "2"))
                {
                    lblCPP.Text = "GRID";
                }
                else if ((Convert.ToString(ds.Tables[3].Rows[0]["intPowerSource"].ToString()) == "3"))
                {
                    lblCPP.Text = "CPP & GRID";
                }

                //lblRawmaterial.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchRawMaterial"]);
                lblImple.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchImplementPeriod"]);
                lblDirect.Text = Convert.ToString(ds.Tables[3].Rows[0]["intEmployement"]) + " Numbers";
                lblContra.Text = Convert.ToString(ds.Tables[3].Rows[0]["intContractual"]) + " Numbers";
                lblEmTotal.Text = Convert.ToString(Convert.ToInt32(ds.Tables[3].Rows[0]["intEmployement"]) + Convert.ToInt32(ds.Tables[3].Rows[0]["intContractual"])) + " Numbers";

            }

        }
        catch (Exception m) { }

    }
     
    public void fillFinancialdetails(DataSet ds)
    {
        try
        {
            List<Finance> objFinance = new List<Finance>();
            Finance objFin = new Finance();

            if (ds.Tables[4].Rows.Count > 0)
            {
                objFinance = new List<Finance>();
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    objFin = new Finance();
                    objFin.FinanceId = Convert.ToInt32(dr[0]);
                    objFin.ComapnyName = Convert.ToString(dr[1]);
                    objFin.Particulars = Convert.ToString(dr[2]);
                    objFin.FinYear1 = Convert.ToString(dr[3]);
                    objFin.FinYear2 = Convert.ToString(dr[4]);
                    objFin.FinYear3 = Convert.ToString(dr[5]);
                    objFin.Remark = Convert.ToString(dr[6]);
                    objFin.FinDoc = Convert.ToString(dr[7]);
                    objFinance.Add(objFin);
                }

                GrdFinanace.DataSource = objFinance;
                GrdFinanace.DataBind();
            }

        }
        catch (Exception m) { }

    }

    public class Finance
    {
        public int SlNo { get; set; }
        public int KeyId { get; set; }
        public int FinanceId { get; set; }
        public string ComapnyName { get; set; }
        public string Particulars { get; set; }
        public string FinYear1 { get; set; }
        public string FinYear2 { get; set; }
        public string FinYear3 { get; set; }
        public string Remark { get; set; }
        public string FinDoc { get; set; }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        int[] a = new int[2] { 0, 1 };
        for (int i = GrdFinanace.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = GrdFinanace.Rows[i];
            GridViewRow previousRow = GrdFinanace.Rows[i - 1];
            foreach (var j in a)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                        }
                        if (j == 0)
                        {
                            if (row.Cells[5].RowSpan == 0)
                            {
                                previousRow.Cells[5].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[5].RowSpan = row.Cells[5].RowSpan + 1;
                            }
                            row.Cells[5].Visible = false;
                        }
                        row.Cells[j].Visible = false;
                    }
                }
            }
        }
    }

    #endregion

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objams = new AMS();
        string strVal = "";
        string MbrMessage;
        try
        {
            if (hdnStatus.Value == "4" & txtClarification.Text=="")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('Please Enter Your Clarification');", true);
                return;
            }
            else if (hdnStatus.Value != "4" & txtComments.Text=="")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('Please Enter Your Comment');", true);
                return;
            }
            else
            {
                if (hdnStatus.Value == "4")
                {
                    objams.Action = "C";
                    objams.COMMENT = txtClarification.Text.Trim();
                    MbrMessage = "Clarification against agenda form of " + lblProjNm.Text;
                    objams.ProjectId = Convert.ToInt32(hdnClariID.Value);  // Pass Clarification ID Beacuse in Resend Clarification If we Update against Project ID previous Clarification Updated
                }
                else
                {
                    objams.Action = "A";
                    objams.COMMENT = txtComments.Text.Trim();
                    MbrMessage="Feedback against agenda form of " + lblProjNm.Text;
                    objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
                }               
                //objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
                objams.OfficerId = Convert.ToInt32(Session["UserId"]);
                
                strVal = AMServices.AddComments(objams);

                MemberDetails(lblProjNm.Text, MbrMessage);
                if (strVal == "1")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('Comments Added Successfully');top.$('#pageModal').modal('hide');top.location.reload();", true);
                }
                if (strVal == "2")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('Comments Updated Successfully');top.$('#pageModal').modal('hide');top.location.reload();", true);
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
        finally { objams = null; }
    }


    public void MemberDetails(string ProjectName, string Subject)
    {
        string SenderName = string.Empty;
        string FromEmail = string.Empty;
       
        objams = new AMS();
        DataSet ds = new DataSet();
        try
        {
            objams.Action = "MN";
            objams.OfficerId = Convert.ToInt32(Session["UserId"].ToString());
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);

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
                    SendPublishMail(ds.Tables[1].Rows[0]["UserId"].ToString(), ProjectName, FromEmail, ds.Tables[1].Rows[0]["TOEMAIL"].ToString(), SenderName, ds.Tables[1].Rows[0]["TONAME"].ToString(), ds.Tables[1].Rows[0]["TOMOBILE"].ToString(), Subject);
                }
            }
        }
        catch { }
        finally { objams = null; ds = null; }

    }

    public void SendPublishMail(string UserId, string ProjectName, string FromMailId, string Tomail, string SenderName, string ToName, string MobileNo, string Subject)
    {
        EmailMsg objMsg = new EmailMsg();
        SendEmail objEmail = new SendEmail();
        SMSGateway objsms = new SMSGateway();
        try
        {
            string strMessage2 = string.Empty;
            strMessage2 = "Dear Sir/Madam,</br></br>";
            strMessage2 += "Mr. " + SenderName + " , SLFC Member, has submitted  his/her views on the Agenda form for " + ProjectName + ".,</br></br>";
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
            //if (MobileNo != "")
            //{
            //    string status = objsms.sendBulkSMS(MobileNo, " GM,SLNA has published the agenda form for " + ProjectName + " to review", 1, UserId);
            //}

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
    /// <summary>
    /// Created by Monalisa Nayak on 29-12-2016 to bind Project Cost details
    /// </summary>
    /// <param name="ds"></param>
    public void FillProjectCostDtls(DataSet ds)
    {
        try
        {
            if (ds.Tables[8].Rows.Count >= 0)
            {
                GrdProjectCostDtls.DataSource = ds.Tables[8];
                GrdProjectCostDtls.DataBind();
                GrdProjectCostDtls.FooterRow.Cells[0].Text = "Total";
                var total = GrdProjectCostDtls.FooterRow.FindControl("lblGrandTotal") as Label;
                total.Text = x.ToString("N2");
                lblProjCost.Text = x.ToString("N2");
            }
        }
        catch (Exception m) { Response.Write(m.Message); }
    }
    protected void GrdProjectCostDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGrandTotal = (Label)e.Row.FindControl("lblGrandTotal");
            Label lblGrand = (Label)e.Row.FindControl("lblCost");
            x = x + Convert.ToDouble(lblGrand.Text);
        }
    }
    public void FillDistrictLocation(DataSet ds)
    {
        try
        {
            if (ds.Tables[10].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                {
                    Label label = new Label();
                    string District = ds.Tables[10].Rows[i]["District"].ToString();
                    string Loc = ds.Tables[10].Rows[i]["Location"].ToString();
                    string Com = District + "," + Loc + "<br/>";
                    label.Text = Com;
                    placeholder.Controls.Add(label);
                } 
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void FillMaterialSource(DataSet ds)
    {
        try
        {
            if (ds.Tables[11].Rows.Count > 0)
            {
                GrdSource.DataSource = ds.Tables[11];
                GrdSource.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GrdFinanace_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlinkFinDoc = (HyperLink)e.Row.FindControl("hlDoc");
            HiddenField hdhDoc = (HiddenField)e.Row.FindControl("hdnFinDoc");
            string strDocs = hdhDoc.Value;
            if (hdhDoc.Value != "")
            {
                hlinkFinDoc.NavigateUrl = "../SingleWindow/FinDoc/" + Request.QueryString["ID"] + "/" + strDocs;
            }
            else
            {
                hlinkFinDoc.ImageUrl = "";
            }
        }
    }
}