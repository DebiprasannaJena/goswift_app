using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Data;
public partial class faq : System.Web.UI.Page
{
    CmsBusinesslayer objService = new CmsBusinesslayer();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["id"] != null)
        {
            FillContent();
        }
    }
    private void FillContent()
    {
        try
        {
            int intmenuid = Convert.ToInt32(Request.QueryString["id"]);
            string straction = "CHK";
            DataTable dt = objService.ChkCMSData(straction, intmenuid);
            if (dt.Rows.Count > 0)
            {
                string sValueTobeShowninDiv = dt.Rows[0]["vchContent"].ToString();
                divabout.InnerHtml = sValueTobeShowninDiv;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "jAlert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }

}