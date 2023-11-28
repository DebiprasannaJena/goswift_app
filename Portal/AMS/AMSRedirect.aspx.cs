using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.Proposal;
using System.ServiceModel;
using BusinessLogicLayer.Proposal;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Configuration;
using System.Globalization;
public partial class Portal_AMS_AMSRedirect : SessionCheck
{
    ProposalBAL objService = new ProposalBAL();
    LandDet objLandDet = new LandDet();
    private byte[] key = {};
    private byte[] IV = {0x12,0x34,0x56,0x78,0x90,0xab,0xcd,0xef};
    private string sEncryptionKey = "!#$a54?3";
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
                if (!string.IsNullOrEmpty(Session["UserId"].ToString()))
                {
                    int Userid = Convert.ToInt32(Session["UserId"].ToString());
                    GetDetails(Userid);
                }
            }
        }
    }

    private void GetDetails(int Userid)
    {
        objLandDet = new LandDet();
        try
        {
            objLandDet.strAction = "A";
            objLandDet.intCreatedBy = Userid;
            List<LandDet> objProposalList = objService.GetAMSUserId(objLandDet).ToList();
            if (objProposalList.Count > 0)
            {
                int AMSUserId = Convert.ToInt32(objProposalList[0].intUpdatedBy.ToString());
                string AMSEncryptUid = "";
                AMSEncryptUid = CSMEncrypt(AMSUserId.ToString());
                //CultureInfo en = new CultureInfo("en-US");
              
                //DateTime dt = DateTime.Parse(DateTime.Now.ToString(), en);
DateTime dt = DateTime.Parse(DateTime.Now.ToString());

                //string dateTimedet = dt.ToString();
              string dateTimedet = dt.ToString("MM/dd/yyyy HH:mm:ss tt");



                string AMSEncryptDate = CSMEncrypt(dateTimedet.ToString());
                if (AMSUserId != 0)
                {
                    string appPayUrl = ConfigurationManager.AppSettings["AMSUrl"].ToString();
                    string URL = "" + appPayUrl + "?Uid=" + AMSEncryptUid + "&TS=" + AMSEncryptDate + "";
                    Response.Redirect(URL);

                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "ProposalDetails");
        }
        finally
        {
            objLandDet = null;
        }
    }
    public string CSMEncrypt(string stringToEncrypt)
    {
        try
        {
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey);
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