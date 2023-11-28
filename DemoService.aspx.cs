using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DemoService : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void hypProfessionalTaxApply_Click(object sender, EventArgs e)
    {
        string PTToken = "PT/2017/0001";
        string strURL = "https://odishatax.gov.in/portaldemo/eRegistration/FirstTimeRegistrationPT.aspx?";
            if (HttpContext.Current != null)
            {
                string strURLWithData = strURL + EncryptQueryString(string.Format("Token={0}", PTToken));
                HttpContext.Current.Response.Redirect(strURLWithData);
            }
    }
    public string EncryptQueryString(string strQueryString)
    {
        EncryptDecryptQueryString objEDQueryString = new EncryptDecryptQueryString();
        return objEDQueryString.Encrypt(strQueryString, "m8s3e3k5");
    }
}