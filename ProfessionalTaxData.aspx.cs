using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;



public partial class ProfessionalTaxData : SessionCheck
{
    ExternalServiceIntegration objSvc = new ExternalServiceIntegration();
    ExternalServiceIntegration.Serviceinfo objservice = new ExternalServiceIntegration.Serviceinfo();       
    List<ExternalServiceIntegration.Serviceinfo> objServicelist = new List<ExternalServiceIntegration.Serviceinfo>();
    string str_ProposalNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            str_ProposalNo = Request.QueryString["ProposalNo"];
        }
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string strPanno = txtPanNo.Text.Trim();
            objservice.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objservice.vchAction = "GU";
            objServicelist = objSvc.GetUserDetail(objservice);
            if (objServicelist.Count > 0)
            {
                objservice.vchEmailId = objServicelist[0].vchEmailId.ToString();
                objservice.vchMobileNo = objServicelist[0].vchPassword.ToString();
            }

            string appLandUrl = ConfigurationManager.AppSettings["ProfessionalTax"].ToString();
            string output = objSvc.ExternalServiceData("GA", 10, str_ProposalNo, Convert.ToInt32(Session["InvestorId"]), strPanno);
            string Token = EncryptQueryString(string.Format("Token={0}&Phone={1}&Email={2}&PanNo={3}", output, objservice.vchMobileNo, objservice.vchEmailId, strPanno));           
            Response.Redirect(appLandUrl + "?" + Token);            
        }
        catch(Exception ex)
        {
            Util.LogError(ex, "PT");
        }
    }
    public string EncryptQueryString(string strQueryString)
    {
        EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
        return objEDQueryString.Encrypt(strQueryString, "m8s3e3k5");
    }
}