using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EntityLayer.CMS;
using BusinessLogicLayer.CMS;

public partial class TemplatePage_3 : System.Web.UI.Page
{
    #region Variable Declaration
    string str_Retvalue = "";
    int retval = 0;
    string fileNM = "";
    TemplateDetails obj = new TemplateDetails();
    List<TemplateDetails> newlist = new List<TemplateDetails>();
    CmsBusinesslayer objbusiness = new CmsBusinesslayer();
    int intOutput = 0, gIntretval = 0;
    string strShowMsg = string.Empty;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        ContentFill(Request.QueryString["id"].ToString());
    }
    public void ContentFill(string strId)
    {

        Label lblMasterStatus = (Label)Master.FindControl("lblBrdcmb1");
        obj.actioncode = "TD";
        obj.TemplateId = Convert.ToInt32(strId);
        obj.IntPLinkId = Convert.ToInt32(Request.QueryString["lnkid"].ToString());
        newlist = objbusiness.TemplateContentDetails(obj);
        if (newlist.Count > 0)
        {
            obj.actioncode = "PL";
            lblMasterStatus.Text = objbusiness.PrimaryLinkName(obj);
            divId1.InnerHtml = newlist[0].strContent1;
            divId2.InnerHtml = newlist[0].strContent2;
            divId3.InnerHtml = newlist[0].strContent3;
        }
    }
}