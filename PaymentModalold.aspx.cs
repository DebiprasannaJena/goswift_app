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
using System.Collections.Specialized;
using System.Text;

public partial class PaymentModalold : System.Web.UI.Page
{
    ProjectInfo objproject = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        objproject = (ProjectInfo)Session["obj_App"];
        if (!IsPostBack)
        {

            lblRequestNo.Text = objproject.RApplicationNo;
            FetchDetails(lblRequestNo.Text);
            lblDesc.Text = ViewState["Desc"].ToString();
            lblAmount.Text = ViewState["Amount"].ToString();
        }
    }
    //ADDED BY SUROJ KUMAR PRADHAN TO FETCH DESCRIPTION AND AMOUNT
    private void FetchDetails(string ProposalNo)
    {
        try
        {
            List<ProjectInfo> objPayementlist = new List<ProjectInfo>();
            ProjectInfo objProp = new ProjectInfo();
            ProposalBAL objservice = new ProposalBAL();
            objProp.vchProposalNo = ProposalNo;
            objProp.strAction = "S6";
            objPayementlist = objservice.GetProposalDtls(objProp);
            if (objPayementlist.Count > 0)
            {
                ViewState["Amount"] = objPayementlist[0].decPaymentAmt.ToString();
                ViewState["Head"] = objPayementlist[0].vchAccountHead.ToString();
                ViewState["Desc"] = objPayementlist[0].vchDescription.ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnModalSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            ProjectInfo objProp = new ProjectInfo();
            ProposalBAL objservice = new ProposalBAL();
            string retval = "";
            objproject = (ProjectInfo)Session["obj_App"];
            //objProp.decPaymentAmt = Convert.ToDecimal(hdnTest.Value);
            objProp.vchApplicantNo = objproject.RApplicationNo;
            objProp.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objProp.strAction = "I";
            retval = objservice.AddProposalDtls(objProp);
            objProp.RApplicationNo = objproject.RApplicationNo;
            objProp.RAccountHead = ViewState["Head"].ToString();
            objProp.RUserName = Session["Username"].ToString();
            objProp.Ramount = ViewState["Amount"].ToString();
            objProp.RDesc = ViewState["Desc"].ToString();
            Session["obj_Ap"] = objProp;
            if (retval == "1")
            {
                //{
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
                NameValueCollection ldata = new NameValueCollection();
                string Description = objproject.RDesc;
                string Amount = objproject.Ramount;
                //string Amount = "1";
                string AccountHead = objproject.RAccountHead;
                string userName = objproject.RUserName;
                //string transactionDetail = "(0029-00-101-0229-01057-000,ProcessingFee,10)";
                string transactionDetail = "(" + AccountHead + "," + Description + "," + Amount + ")";
                //string transactionDetail = "(0853-00-102-0121-02021-000 ," + Description + "," + Amount + ")";
                string strRTNurl = rurl;
                ldata.Add("transactionDetail", transactionDetail);
                ldata.Add("depositedBy", userName);
                //ldata.Add("contactNo", "NA");
                //ldata.Add("emailId", "NA");
                //ldata.Add("district", "NA");
                //ldata.Add("depositorAddress", "NA");
                ldata.Add("deptName", "IND");
                ldata.Add("deptRefId", objServicelist[0].vchOrderNo.ToString());////// Added by Sushant Jena On Dt. 27-Dec-2018
                string otherparam = "(OrderNo=" + objServicelist[0].vchOrderNo.ToString() + "!~!redirect_url=" + strRTNurl + ")";
                ldata.Add("otherParameters", otherparam);
                RedirectAndPOST(this.Page, surl, ldata);

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public static void RedirectAndPOST(Page page, string destinationUrl, NameValueCollection data)
    {

        //Prepare the Posting form
        string strForm = PreparePOSTForm(destinationUrl, data);
        //Add a literal control the specified page holding 
        //the Post Form, this is to submit the Posting form with the request.
        page.Controls.Add(new LiteralControl(strForm));
    }
    private static String PreparePOSTForm(string url, NameValueCollection data)
    {
        //Set a name for the form
        string formID = "PostForm";
        //Build the form using the specified data to be posted.
        StringBuilder strForm = new StringBuilder();
        strForm.Append("<form id=\"" + formID + "\" name=\"" +
                       formID + "\" action=\"" + url +
                       "\" method=\"POST\" >");//target=\"TheWindow\"

        foreach (string key in data)
        {
            strForm.Append("<input type=\"hidden\" name=\"" + key +
                           "\" value=\"" + data[key] + "\">");
        }

        strForm.Append("</form>");
        //Build the JavaScript which will do the Posting operation.
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        //strScript.Append("javascript:void window.open('','TheWindow','menubar= 1,scrollbars=1,width=600,height=400');");
        strScript.Append("var v" + formID + " = document." +
                         formID + ";");
        strScript.Append("v" + formID + ".submit();");
        strScript.Append("</script>");
        //Return the form and the script concatenated.
        //(The order is important, Form then JavaScript)
        return strForm.ToString() + strScript.ToString();
    }
}