using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.ServiceModel;

using System.Web.Services;
using System.IO;
using BusinessLogicLayer.Proposal;
using EntityLayer.Proposal;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Collections.Specialized;
using BusinessLogicLayer.Dashboard;
using DWHServiceReference;
using System.Configuration;
using System.Security.Cryptography;

public partial class ProposalsNew : System.Web.UI.Page
{
    EncryptDecryptQueryString objEncrypt = new EncryptDecryptQueryString();
    ProposalBAL objService = new ProposalBAL();
    ProposalDet objProposal = new ProposalDet();
    ProjectInfo objproject = new ProjectInfo();
    string strRetval = "";
    int intRetVal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                fillInvestorChildUnit();
                BindGrid();
                BindInvestorInfo();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Proposal");
            }
        }
    }
    private void BindInvestorInfo()
    {
        try
        {
            ProposalBAL objService = new ProposalBAL();
            DataTable dt = new DataTable();
            ProposalDet objProp = new ProposalDet();
            objProp.IntInvestorId = Convert.ToInt32(Session["InvestorId"].ToString());
            objProp.VCH_CONTACT_FIRSTNAME = "";
            objProp.VCH_EMAIL = "";
            objProp.VCH_OFF_MOBILE = "";
            objProp.strAction = "DIF";           
            dt.Reset();
            dt = objService.INVESTORINFODISPLAY(objProp);
            if (dt.Rows.Count > 0 && Session["SkipCount"].ToString()=="0")
            {
                txtContactPersn.Text = dt.Rows[0]["VCH_CONTACT_FIRSTNAME"].ToString();
                txtEmailId.Text = dt.Rows[0]["VCH_EMAIL"].ToString();
                txtMobileNo.Text = dt.Rows[0]["VCH_OFF_MOBILE"].ToString();
                ModalPopupExtender2.Show();
            }
            else
            {
                txtContactPersn.Text = "";
                txtEmailId.Text = "";
                txtMobileNo.Text = "";
                ModalPopupExtender2.Hide();
            }
            
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Proposal");
        }       
    }

    private void BindGrid()
    {
        try
        {
            List<ProposalDet> objProposalList = new List<ProposalDet>();
            objProposal = new ProposalDet();

            string strFilterMode = "";
            string strInvestorId = "";
            if (DrpDwn_Investor_Unit.SelectedIndex > 0)
            {
                strFilterMode = "I";
                strInvestorId = DrpDwn_Investor_Unit.SelectedValue;
            }
            else
            {
                strFilterMode = "C";
                strInvestorId = Convert.ToString(Session["InvestorId"]);
            }

            objProposal.strAction = "V";
            objProposal.intCreatedBy = Convert.ToInt32(strInvestorId);
            objProposal.strFilterMode = strFilterMode;

            objProposalList = objService.getRaisedQueryDetails(objProposal).ToList();
            gvProposal.DataSource = objProposalList;
            gvProposal.DataBind();

            intRetVal = gvProposal.Rows.Count;

            /*-----------------------------------------------------------------*/

            List<ProposalDet> objProposalList1 = new List<ProposalDet>();
            objProposal = new ProposalDet();
            objProposal.strAction = "K";
            objProposal.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objProposalList1 = objService.getRaisedQueryDetails(objProposal).ToList();

            int totalCnt = Convert.ToInt32(objProposalList1[0].EmailSubject.ToString().Split('_')[0]);
            int totalCntDFt = Convert.ToInt32(objProposalList1[0].EmailSubject.ToString().Split('_')[1]);
            if (totalCnt == 0)
            {
                if (totalCntDFt == 0)
                {
                    Response.Redirect("ProposalInstruction.aspx");
                }
                else
                {
                    Response.Redirect("DraftedProposals.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objProposal = null;
        }
    }

    protected void gvProposal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string strProposalNo = gvProposal.DataKeys[e.Row.RowIndex].Values[0].ToString();
            HyperLink hprlnkproposal = (HyperLink)e.Row.FindControl("hypLink");

            //hprlnkproposal.NavigateUrl = "~/PEAL/ProposalDetails.aspx?StrPropNo=" + objEncrypt.Encrypt(strProposalNo.ToString(), "sblw-3hn8-sqoy19");
            hprlnkproposal.NavigateUrl = "~/PEAL/ProposalDetails.aspx?StrPropNo=" + strProposalNo.ToString();
            HyperLink hplnkPEALCerti = (HyperLink)e.Row.FindControl("hplnkPEALCerti");
            Label lblPaymentShow = (Label)e.Row.FindControl("lblPaymentShow");
            HiddenField hdnPEALFile = (HiddenField)e.Row.FindControl("hdnPEALFile");
            HiddenField hdnPaymentstatus = (HiddenField)e.Row.FindControl("HdnPaymentstatus");
            LinkButton lnkMakePayment = (LinkButton)e.Row.FindControl("lnkMakePayment");
            if (hdnPaymentstatus.Value == "0")
            {
                lnkMakePayment.Visible = true;
                lnkMakePayment.Enabled = true;
                lblPaymentShow.Visible = false;
            }
            else
            {

                lnkMakePayment.Visible = false;
                lblPaymentShow.Visible = true;
                lblPaymentShow.Text = "Payment Made";
                lblPaymentShow.ForeColor = System.Drawing.Color.Gray;



            }
            hdnPEALFile.Value = gvProposal.DataKeys[e.Row.RowIndex].Values[1].ToString();
            if (hdnPEALFile.Value != "")
            {
                hplnkPEALCerti.NavigateUrl = "~/Proposal/PEALCertificate/" + hdnPEALFile.Value;
                hplnkPEALCerti.Visible = true;
            }
            else
            {
                hplnkPEALCerti.Visible = false;
            }

            HiddenField hdnProposalNo1 = (e.Row.FindControl("hdnProposalNo11") as HiddenField);
            Label Label1 = (e.Row.FindControl("Label1") as Label);
            Label1.Text = hdnProposalNo1.Value;
            DropDownList ddlServiceName = (e.Row.FindControl("ddlServiceName") as DropDownList);
            DropDownList ddlServiceType = (e.Row.FindControl("ddlServiceType") as DropDownList);
            //HiddenField hdnServiceid = (e.Row.FindControl("hdnServiceid") as HiddenField);
            //ServiceBusinessLayer objService = new ServiceBusinessLayer();
            List<ProjectInfo> objServicelist = new List<ProjectInfo>();
            if (lnkMakePayment.Text == "Make Payment")
            {
                objproject.strAction = "S1";
                objServicelist = objService.GetProposalDtls(objproject).ToList();
                ddlServiceName.DataSource = objServicelist;
                ddlServiceName.DataTextField = "vchserviceName";
                ddlServiceName.DataValueField = "intserviceid";
                ddlServiceName.DataBind();
                ddlServiceName.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlServiceName.SelectedIndex = 1;
                ddlServiceName.Enabled = false;
            }
            HyperLink hypLink = (e.Row.FindControl("hypLink") as HyperLink);
            objproject.strAction = "S4";
            objproject.vchProposalNo = hypLink.Text;
            objServicelist = objService.GetProposalDtls(objproject).ToList();
            ddlServiceType.DataSource = objServicelist;
            ddlServiceType.DataTextField = "vchServiceType";
            ddlServiceType.DataValueField = "intserviceid";
            ddlServiceType.DataBind();
            ddlServiceType.Enabled = false;

            //Added By Pranay Kumar with Dicussion with Santosh Sir on 10-Sept-2017 for Show/Hide of Revert Query Button
            int intQueryStatus = Convert.ToInt32(gvProposal.DataKeys[e.Row.RowIndex].Values[2]);
            int intExtendedStatus = Convert.ToInt32(gvProposal.DataKeys[e.Row.RowIndex].Values[3]);
            LinkButton btnQuery = (e.Row.FindControl("LinkButton1") as LinkButton);
            LinkButton lbtnExtend = (e.Row.FindControl("lbtnExtend") as LinkButton);

            if (intQueryStatus == 1)//Query Raised
            {
                btnQuery.Visible = true;
                lbtnExtend.Visible = false;
            }
            else if (intQueryStatus == 0)
            {
                btnQuery.Visible = false;
                lbtnExtend.Visible = false;
            }
            else if (intQueryStatus == 2)
            {
                btnQuery.Visible = false;
                if (intExtendedStatus == 1)
                {
                    lbtnExtend.Visible = false;
                }
                else
                {
                    lbtnExtend.Visible = true;
                }
            }
            List<ProposalDet> objProposalList = new List<ProposalDet>();
            ProposalDet objProp = new ProposalDet();
            objProp.strAction = "QD";
            objProp.strProposalNo = hdnProposalNo1.Value;
            objProposalList = objService.getRaisedQueryDetails(objProp).ToList();
            HtmlGenericControl QueryHist = (HtmlGenericControl)e.Row.FindControl("QueryHist");



            string strHTMlQuery = "<table class='table table-bordered table-hover'><tr><th>User Name</th><th> Query Details</th><th>Attachment</th><th>Files</th></tr>";
            for (int i = 0; i < objProposalList.Count; i++)
            {
                if (objProposalList[i].strFileName == null || objProposalList[i].strFileName == "")
                {
                    strHTMlQuery = strHTMlQuery + "<tr><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a  href='#'>--</a>" + "</td></tr>";
                }
                else
                {
                    strHTMlQuery = strHTMlQuery + "<tr><td>" + objProposalList[i].strActionToBeTakenBY + "</td><td>" + objProposalList[i].strRemarks + "</td><td>" + objProposalList[i].dtmCreatedOn + "</td><td>" + "<a target='_blank' href='./QueryFiles/" + objProposalList[i].strFileName + "'>Download</a>" + "</td></tr>";
                }
            }
            strHTMlQuery = strHTMlQuery + "</table>";

            QueryHist.InnerHtml = strHTMlQuery;


            //Ended By Pranay Kumar with Dicussion with Santosh Sir on 10-Sept-2017 for Show/Hide of Revert Query Button

            //Added By Pranay Kumar for Addition of Query Details on 14-Sept-2017
            HyperLink hypQueryDtls = (e.Row.FindControl("hypQueryDtls") as HyperLink);
            // hypQueryDtls.NavigateUrl="~/Portal/Proposal/QueryProposalRevert.aspx?ProposalNo=" + strProposalNo + "&linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "";
            string strCurrQueryStatus = Convert.ToString(gvProposal.DataKeys[e.Row.RowIndex].Values[4]);
            if (strCurrQueryStatus == "--")
            {

                hypQueryDtls.Text = "--";
            }
            else if (strCurrQueryStatus == "Completed")
            {
                hypQueryDtls.NavigateUrl = "QueryProposalRevert.aspx?ProposalNo=" + strProposalNo + "";
                hypQueryDtls.CssClass = "btn btn-success btn-sm";
                hypQueryDtls.Text = "<i class='fa fa-eye' aria-hidden='true'></i>";
            }
            else if (strCurrQueryStatus == "QUERY RAISED")
            {
                hypQueryDtls.NavigateUrl = "QueryProposalRevert.aspx?ProposalNo=" + strProposalNo + "";
                hypQueryDtls.Text = strCurrQueryStatus;
                hypQueryDtls.CssClass = "btn btn-success btn-sm";
            }
            else if (strCurrQueryStatus == "QUERY RESPONDED")
            {
                hypQueryDtls.NavigateUrl = "QueryProposalRevert.aspx?ProposalNo=" + strProposalNo + "";
                hypQueryDtls.Text = strCurrQueryStatus;
                hypQueryDtls.CssClass = "label-warning label label-default";
            }
            //Ended By Pranay Kumar for Addition of Query Details on 14-Sept-2017

        }
        //for (int i = 0; i < gvProposal.Rows.Count; i++)
        //{
        //    Label lblStatus = (Label)gvProposal.Rows[i].FindControl("lblstatus");
        //    LinkButton btnQuery = (LinkButton)gvProposal.Rows[i].FindControl("LinkButton1");
        //    if (lblStatus.Text == "Query Raised")
        //    {
        //        btnQuery.Visible = true;
        //    }
        //    else { btnQuery.Visible = false; }
        //}


    }
    protected void gvProposal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvProposal.PageIndex = e.NewPageIndex;
        BindGrid();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objProposal = new ProposalDet();
        try
        {
            Button btnSubmit = (Button)sender;
            TextBox txtA1 = (TextBox)btnSubmit.FindControl("txtA1");
            FileUpload FileUpload1 = (FileUpload)btnSubmit.FindControl("FileUpload1");
            objProposal.strAction = "A";
            objProposal.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objProposal.strProposalNo = btnSubmit.CommandArgument.ToString();
            objProposal.intStatus = 6;
            objProposal.strRemarks = txtA1.Text;

            string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + btnSubmit.CommandArgument.ToString() + "_Query1" + ".pdf", DateTime.Now);

            if (FileUpload1.HasFile)
            {
                if (Path.GetExtension(FileUpload1.FileName) != ".pdf")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
                    return;
                }
            }
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/QueryFiles/"));
            if (!string.IsNullOrEmpty(FileUpload1.FileName))
            {
                if (dir.Exists)
                {
                    FileUpload1.SaveAs(Server.MapPath("~/QueryFiles/" + filepath));
                }
                else
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/QueryFiles"));
                    FileUpload1.SaveAs(Server.MapPath("~/QueryFiles/" + filepath));

                }
            }
            else { filepath = ""; }

            string filepath1 = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + btnSubmit.CommandArgument.ToString() + "_Query2" + ".pdf", DateTime.Now);

            //if (FileUpload2.HasFile)
            //{

            //    if (Path.GetExtension(FileUpload2.FileName) != ".pdf")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
            //        return;
            //    }
            //}

            //if (!string.IsNullOrEmpty(FileUpload2.FileName))
            //{
            //    if (dir.Exists)
            //    {
            //        FileUpload2.SaveAs(Server.MapPath("~/QueryFiles/" + filepath1));
            //    }
            //    else
            //    {
            //        System.IO.Directory.CreateDirectory(Server.MapPath("~/QueryFiles"));
            //        FileUpload2.SaveAs(Server.MapPath("~/QueryFiles/" + filepath1));

            //    }
            //}
            //else { filepath1 = ""; }

            //string filepath2 = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_" + btnSubmit.CommandArgument.ToString() + "_Query3" + ".pdf", DateTime.Now);

            //if (FileUpload3.HasFile)
            //{

            //    if (Path.GetExtension(FileUpload3.FileName) != ".pdf")
            //    {
            //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
            //        return;
            //    }
            //}

            //if (!string.IsNullOrEmpty(FileUpload3.FileName))
            //{
            //    if (dir.Exists)
            //    {
            //        FileUpload3.SaveAs(Server.MapPath("~/QueryFiles/" + filepath2));
            //    }
            //    else
            //    {
            //        System.IO.Directory.CreateDirectory(Server.MapPath("~/QueryFiles"));
            //        FileUpload3.SaveAs(Server.MapPath("~/QueryFiles/" + filepath2));

            //    }
            //}
            //else { filepath2 = ""; }

            //objProposal.strFileName = filepath + "," + filepath1 + "," + filepath2 + ",";
            objProposal.strFileName = filepath;
            string strRetVal = objService.ProposalRaiseQuery(objProposal);
            BindGrid();
            if (strRetVal == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Query Responded Successfully.')</script>;", false);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Saved Successfully.');", true);
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Proposal");
        }
        finally
        {
            objProposal = null;
        }
    }
    [WebMethod]
    public static List<ProposalDet> ShowQuery(string id)
    {
        string gStrRetVal = null;
        ProposalBAL objService = new ProposalBAL();
        ProposalDet objProp = new ProposalDet();
        List<ProposalDet> objProposalList = new List<ProposalDet>();
        try
        {
            gStrRetVal = id;
            objProp.strAction = "E";
            objProp.strProposalNo = id;
            objProposalList = objService.getRaisedQueryDetails(objProp).ToList();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Proposal");
        }
        finally
        {
            //objLinc = null;
        }
        return objProposalList;
    }
    [WebMethod]
    public static List<ProjectInfo> ServiceDetail(string id, string Tid)
    {
        string gStrRetVal = null;
        ProposalBAL objService = new ProposalBAL();
        ProjectInfo objProp = new ProjectInfo();
        List<ProjectInfo> objProposalList = new List<ProjectInfo>();
        try
        {
            //gStrRetVal = id;
            objProp.strAction = "S2";
            objProp.intSAid = id;
            objProp.intTypeid = Convert.ToInt16(Tid);
            objProposalList = objService.GetProposalDtls(objProp).ToList();

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Proposal");
        }
        finally
        {
            //objLinc = null;
        }
        return objProposalList;
    }

    protected void btnModalSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectInfo objProp = new ProjectInfo();
            ProposalBAL objservice = new ProposalBAL();
            string retval = "";
            Button btnsave = (Button)sender;

            HiddenField hdnServiceid = (HiddenField)btnsave.FindControl("hdnServiceid");
            DropDownList ddlServiceName = (DropDownList)btnsave.FindControl("ddlServiceName");
            DropDownList ddlServiceType = (DropDownList)btnsave.FindControl("ddlServiceType");
            Label Label1 = (Label)btnsave.FindControl("Label1");
            Label lblDesc = (Label)btnsave.FindControl("lblDesc");

            objProp.Ramount = hdnTest.Value;
            objProp.RDesc = hdnDes.Value;
            objProp.RAccountHead = hdnAccountHead.Value;
            objProp.RUserName = Session["Username"].ToString();
            objProp.decPaymentAmt = Convert.ToDecimal(hdnTest.Value);
            objProp.intserviceid = Convert.ToInt16(ddlServiceName.SelectedValue);
            objProp.intTypeid = Convert.ToInt16(ddlServiceType.SelectedValue);
            objProp.vchApplicantNo = Label1.Text;
            objProp.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objProp.strAction = "I";
            retval = objservice.AddProposalDtls(objProp);
            hdnTest.Value = "0";
            hdnDes.Value = "0";
            hdnAccountHead.Value = "0";
            objProp.RApplicationNo = Label1.Text.ToString();
            Session["obj_Ap"] = objProp;
            if (retval == "1")
            {
                objproject = new ProjectInfo();
                objproject = (ProjectInfo)Session["obj_Ap"];
                List<ProjectInfo> objServicelist = new List<ProjectInfo>();
                objproject.strAction = "S3";
                objproject.vchApplicantNo = objproject.RApplicationNo;
                objServicelist = objservice.GetProposalDtls(objproject).ToList();

                HttpContext _context = HttpContext.Current;
                objproject = new ProjectInfo();
                objproject = (ProjectInfo)Session["obj_Ap"];
                string rurl = System.Configuration.ConfigurationManager.AppSettings["strRTNurl"];
                string surl = System.Configuration.ConfigurationManager.AppSettings["Responseurl"];               
                string Description = objproject.RDesc;
                string Amount = objproject.Ramount;               
                string AccountHead = objproject.RAccountHead;
                string userName = objproject.RUserName;

                string DATA =
                    "IND"
                    + "|" + objServicelist[0].vchOrderNo.ToString()

                    + "|" + AccountHead
                    + "|" + Description
                    + "|" + Amount

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""

                    + "|" + ""
                    + "|" + ""
                    + "|" + ""
                    + "|" + Amount
                    + "|" + userName
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "111111"
                    + "|" + "9999999999"
                    + "|" + ""
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + "NA"
                    + "|" + rurl;


                string INCRRIPT = HmacSHA256(DATA, "c3c6e92a");
                string datta = DATA + "|" + INCRRIPT;
                datta = encrypt(datta, "c3c6e92a");
                RedirectAndPOST(this.Page, surl, datta);

            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Proposal");
        }
    }

    #region "Added By Manoj Kumar Behera New Treasury Code"

    /// <summary>
    /// Treasury Post Coding Begin
    /// </summary>
    /// <param name="page"></param>
    /// <param name="destinationUrl"></param>
    /// <param name="data"></param>
    /// 

    private byte[] GetKey()
    {
        byte[] byteKey = File.ReadAllBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key");
        return byteKey;
    }

    public static void RedirectAndPOST(Page page, string destinationUrl, String data)
    {
        //Prepare the Posting form
        string strForm = PrepareScript(destinationUrl, data);
        //Add a literal control the specified page holding 
        //the Post Form, this is to submit the Posting form with the request.
        page.Controls.Add(new LiteralControl(strForm));
    }

    private static String PrepareScript(string url, String data)
    {
        //Set a name for the form
        string formID = "eGrassClient";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\" >");//target=\"TheWindow\"

        strForm.Append("<input type=\"hidden\" name=\"msg\" value=\"" + data + "\">");
        strForm.Append("<input type=\"hidden\" name=\"deptCode\" value=\"IND\">");

        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        //strScript.Append("javascript:void window.open('','TheWindow','menubar= 1,scrollbars=1,width=600,height=400');");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");

        return strForm.ToString() + strScript.ToString();
    }

    private static byte[] GetFileBytes(String filename)
    {
        if (!File.Exists(filename))
            return null;
        Stream stream = new FileStream(filename, FileMode.Open);
        int datalen = (int)stream.Length;
        byte[] filebytes = new byte[datalen];
        stream.Seek(0, SeekOrigin.Begin);
        stream.Read(filebytes, 0, datalen);
        stream.Close();
        return filebytes;
    }

    private string HmacSHA256(string message, string secret)
    {
        secret = secret ?? "";
        var encoding = new System.Text.ASCIIEncoding();
        byte[] keyByte = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key"); //encoding.GetBytes(secret);
        byte[] messageBytes = encoding.GetBytes(message);
        using (var hmacsha256 = new HMACSHA256(keyByte))
        {
            byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
            return Convert.ToBase64String(hashmessage);
        }
    }

    public string encrypt(string plainText, string secret)
    {

        System.Text.UTF32Encoding UTF32 = new System.Text.UTF32Encoding();
        AesManaged tdes = new AesManaged();
        tdes.Key = GetFileBytes(HttpContext.Current.Server.MapPath("IND/") + "IND.key");
        tdes.Mode = CipherMode.ECB;
        tdes.Padding = PaddingMode.PKCS7;
        ICryptoTransform crypt = tdes.CreateEncryptor();
        byte[] plain = Encoding.UTF8.GetBytes(plainText);
        byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
        return Convert.ToBase64String(cipher);

    }

    /// <summary>
    /// Treasury Post Coding End
    /// </summary>
    /// <param name="page"></param>
    /// <param name="destinationUrl"></param>
    /// <param name="data"></param>


    #endregion

    #region "Added By Pranay Kumar on 10-Sept-2017"
    #region "Extend Button Click"

    protected void lbtnExtend_Click(object sender, EventArgs e)
    {
        ProposalBAL objservice = new ProposalBAL();
        LinkButton btn = (LinkButton)(sender);
        string strProposalNo = btn.CommandArgument;
        int intProposalNo = Convert.ToInt32(strProposalNo);
        string strAction = "EQ";
        int intRetVal = objservice.intExtendDate(strAction, intProposalNo);
        BindGrid();
        if (intRetVal == 3)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Respond Query Date Extended Successfully');", true);
        }

    }
    #endregion
    #endregion

    #region Added by Sushant Jena

    /// <summary>
    /// Added by Sushant Jena On Dt.16-Aug-2018
    /// To get child unit name and self unit name for a investor.
    /// </summary>
    private void fillInvestorChildUnit()
    {
        SWPDashboard objSWP = new SWPDashboard();
        DashboardBusinessLayer objserviceDashboard = new DashboardBusinessLayer();
        try
        {
            objSWP.strAction = "INVUNIT";
            objSWP.intInvestorId = Convert.ToInt32(Session["InvestorId"]);

            DataTable dt = new DataTable();
            dt = objserviceDashboard.getInvestorChildUnit(objSWP);

            if (dt.Rows.Count > 1)
            {
                DrpDwn_Investor_Unit.DataTextField = "VCH_INV_NAME";
                DrpDwn_Investor_Unit.DataValueField = "INT_INVESTOR_ID";
                DrpDwn_Investor_Unit.DataSource = dt;
                DrpDwn_Investor_Unit.DataBind();
                DrpDwn_Investor_Unit.Items.Insert(0, new ListItem("-Select Unit-", "0"));

                divUnitName.Visible = true;
            }
            else
            {
                DrpDwn_Investor_Unit.Items.Clear();
                divUnitName.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "InvestorDashboard");
        }
    }

    /// <summary>
    /// Added by Sushant Jena On Dt.14-Aug-2018
    /// Filter details by unit 
    /// C-Chain (Means Including child users)
    /// I-Indivisual
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DrpDwn_Investor_Unit_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Proposal");
        }
    }

    #endregion
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string gStrRetVal = null;
        ProposalBAL objService = new ProposalBAL();
        ProposalDet objProp = new ProposalDet();        
        try
        {
            objProp.IntInvestorId = Convert.ToInt32(Session["InvestorId"].ToString());
            objProp.VCH_CONTACT_FIRSTNAME = txtContactPersn.Text;
            objProp.VCH_EMAIL = txtEmailId.Text;
            objProp.VCH_OFF_MOBILE = txtMobileNo.Text;
            objProp.strAction = "UIF";
            gStrRetVal = objService.INVESTORINFOUPDATE(objProp).ToString();
            if (gStrRetVal == "1")
            {
                DWHServiceHostClient objSrvRef = new DWHServiceHostClient();
                DWH_Model objEnt = new DWH_Model();
                objEnt.INTUSERID = Convert.ToInt32(Session["DWHUserId"]);
                objEnt.VCHPROMOTERFNAME = txtContactPersn.Text;
                objEnt.VCHEMAILID = txtEmailId.Text;
                objEnt.VCHMOBILENO = txtMobileNo.Text;
                string strSecurityKey = objSrvRef.KeyEncryption(ConfigurationManager.AppSettings["DWHEncryptionKey"].ToString());
                string strReturnVal = objSrvRef.ProfileUpdateGoSmile(objEnt, strSecurityKey);
                Session["SkipCount"] = 1;
                ModalPopupExtender2.Hide();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Data Updated Successfully.')</script>;", false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Proposal");
        }
        finally
        {
            gStrRetVal = null;
        }
    }
    protected void btnHide_Click(object sender, EventArgs e)
    {
        Session["SkipCount"] = 1;
        ModalPopupExtender2.Hide();
    }

    protected void Linkclose_Click(object sender, EventArgs e)
    {
        Session["SkipCount"] = 1;
        ModalPopupExtender2.Hide();
    }
}
