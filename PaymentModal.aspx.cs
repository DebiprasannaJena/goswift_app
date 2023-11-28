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
using System.Security.Cryptography;

public partial class PaymentModal : System.Web.UI.Page
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
            throw ex;
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
}