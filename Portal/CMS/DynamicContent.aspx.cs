using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Web.UI.HtmlControls;

public partial class Portal_CMS_DynamicContent : System.Web.UI.Page
{
    //CmsBusinesslayer objserv = new CmsBusinesslayer();
    //CMSDetails objServiceEntity = new CMSDetails();
    //CMSDetails obj = new CMSDetails();
    List<CMSDetails> newlist = new List<CMSDetails>();
    #region Variable Declaration
    CmsBusinesslayer objService = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    TemplateDetails objTempDtls = new TemplateDetails();
    string str_Retvalue = "";
    string SaveFilePath = "";
    string SaveFileName = "";
    string strTempId = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {

            if (Request.QueryString["ID"] != null)
            {
                btnSave.Text = "Update";
                EditTemplateData(Convert.ToInt32(Request.QueryString["ID"]));
            }
            

        }
    }
    private void EditTemplateData(int ID)
    {
        try
        {
            CMSDetails objedit = new CMSDetails();
            List<CMSDetails> objComp = new List<CMSDetails>();

            objedit.actioncode = "L";
            objedit.Templateid = ID;
            objComp = objService.ViewPageDetails(objedit).ToList();
            if (objComp.Count > 0)
            {
                txtMetaDescription.Text = objComp[0].description.ToString();
                txtMetaKeyWord.Text = objComp[0].keyword.ToString();
                txtPageName.Text = objComp[0].pagename.ToString().Split('/')[1].Trim().Split('.')[0].Trim();// objComp[0].pagename.ToString().Split('.')[0];
                txtMetaTitle.Text = objComp[0].title.ToString();
               // drpTemplates.SelectedValue = objComp[0].Temptid.ToString();
               // txtSnippet.Text = objComp[0].Sinppet.ToString();
                txtMetaAuthor.Text = objComp[0].Authorname.ToString();
                hdnTempVl.Value = objComp[0].Temptid.ToString();
                hdnTemplate.Value = objComp[0].Temptid.ToString();
                if (objComp[0].Temptid.ToString() == "0")
                {
                    rdbId0.Checked = true;
                }
                if (objComp[0].Temptid.ToString() == "1")
                {
                    rdbId1.Checked = true;
                }
                if (objComp[0].Temptid.ToString() == "2")
                {
                    rdbId2.Checked = true;
                }
                if (objComp[0].Temptid.ToString() == "3")
                {
                    rdbId3.Checked = true;
                }
                if (objComp[0].Temptid.ToString() == "4")
                {
                    rdbId4.Checked = true;
                }
                if (objComp[0].Temptid.ToString() == "5")
                {
                    rdbId5.Checked = true;
                }
                if (objComp[0].Temptid.ToString() == "6")
                {
                    rdbId6.Checked = true;
                }
                if (objComp[0].Temptid.ToString() == "7")
                {
                    rdbId7.Checked = true;
                }
                CKEditorControl0.Text = objComp[0].strContent1.ToString();
                CKEditorControl1.Text = objComp[0].strContent2.ToString();
                CKEditorControl2.Text = objComp[0].strContent3.ToString();
                CKEditorControl3.Text = objComp[0].strContent4.ToString();
                hdnLastPageName.Value = objComp[0].pagename.ToString().Split('/')[1].Trim();
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {



        try
        {
            string stMeta = "";
            if (txtMetaTitle.Text != "")
            {
                stMeta = stMeta + "<title>" + txtMetaTitle.Text + "</title>";
            }
            if (txtMetaAuthor.Text != "")
            {
                stMeta = stMeta + "<meta name=\"author\" content=\"" + txtMetaAuthor.Text + "\"/>";
            }

            if (txtMetaDescription.Text != "")
            {
                stMeta = stMeta + "<meta name=\"description\" content=\"" + txtMetaDescription.Text + "\"/>";
            }
            if (txtMetaKeyWord.Text != "")
            {
                stMeta = stMeta + "<meta name=\"keywords\" content=\"" + txtMetaKeyWord.Text + "\"/>";
            }
            string templtname = "";
            if (rdbId0.Checked == true)
            {
                strTempId = "0";
                templtname = "TemplatePage_0.aspx";
            }
            if (rdbId1.Checked == true)
            {
                strTempId = "1";
                templtname = "TemplatePage_1.aspx";
            }
            else if (rdbId2.Checked == true)
            {
                strTempId = "2";
                templtname = "TemplatePage_2.aspx";
            }
            else if (rdbId3.Checked == true)
            {
                strTempId = "3";
                templtname = "TemplatePage_3.aspx";

            }
            else if (rdbId4.Checked == true)
            {
                strTempId = "4";
                templtname = "TemplatePage_4.aspx";
            }
            else if (rdbId5.Checked == true)
            {
                strTempId = "5";
                templtname = "TemplatePage_5.aspx";
            }
            else if (rdbId6.Checked == true)
            {
                strTempId = "6";
                templtname = "TemplatePage_6.aspx";
            }
            else if (rdbId7.Checked == true)
            {
                strTempId = "7";
                templtname = "TemplatePage_7.aspx";
            }

            string root = Server.MapPath("~");
            string Template = root + "\\CMSPages\\" + templtname;

            if (btnSave.Text == "Save")
            {
                string ExistingTemplate1 = root + "\\CMSPages\\" + txtPageName.Text.Trim() + ".aspx";
                if ((System.IO.File.Exists(ExistingTemplate1)))
                {
                    System.IO.File.Delete(ExistingTemplate1);
                }

                StringBuilder line = new StringBuilder();
                using (StreamReader rwOpenTemplate = new StreamReader(Template))
                {
                    while (!rwOpenTemplate.EndOfStream)
                    {
                        line.Append(rwOpenTemplate.ReadToEnd());
                    }
                    line.Replace("[Meta]", stMeta);
                }
                SaveFileName = "\\CMSPages\\" + txtPageName.Text.Trim() + ".aspx";
                SaveFilePath = root + "\\" + SaveFileName;
                if (!File.Exists(SaveFilePath))
                {
                    FileStream fsSave = File.Create(SaveFilePath);
                    StreamWriter sw = null;
                    sw = new StreamWriter(fsSave);
                    sw.Write(line);
                    sw.Close();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "alert('Page with same name already exist.", true);
                }
            }
            CMSDetails objdata = new CMSDetails();
            if (btnSave.Text == "Update")
            {

                objdata.actioncode = "U";
                objdata.Templateid = Convert.ToInt32(Request.QueryString["ID"]);
                string ExistingTemplate = root + "\\CMSPages\\" + hdnLastPageName.Value.Trim();
                //if (hdnLastPageName.Value.Split('.')[0].Trim() != txtPageName.Text.Trim()  )
                //{
                if ((System.IO.File.Exists(ExistingTemplate)))
                {
                    System.IO.File.Delete(ExistingTemplate);
                }
                StringBuilder line = new StringBuilder();
                using (StreamReader rwOpenTemplate = new StreamReader(Template))
                {
                    while (!rwOpenTemplate.EndOfStream)
                    {
                        line.Append(rwOpenTemplate.ReadToEnd());
                    }
                    line.Replace("[Meta]", stMeta);
                }
                SaveFileName = "\\CMSPages\\" + txtPageName.Text.Trim() + ".aspx";
                SaveFilePath = root + "\\" + SaveFileName;
                if (!File.Exists(SaveFilePath))
                {
                    FileStream fsSave = File.Create(SaveFilePath);
                    StreamWriter sw = null;
                    sw = new StreamWriter(fsSave);
                    sw.Write(line);
                    sw.Close();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "alert('Page with same name already exist.", true);
                }

                //}

            }
            else
            {
                objdata.actioncode = "A";
            }
            objdata.pagename = "CMSPages/" + txtPageName.Text.TrimEnd() + ".aspx";
            objdata.title = txtMetaTitle.Text.TrimEnd();
            objdata.description = txtMetaDescription.Text.TrimEnd();
            objdata.keyword = txtMetaKeyWord.Text.TrimEnd();
            objdata.Authorname = txtMetaAuthor.Text;
            objdata.Temptid = Convert.ToInt32(strTempId.ToString());
            objdata.Template = "";
            objdata.strContent1 = CKEditorControl0.Text.ToString();
            objdata.strContent2 = CKEditorControl1.Text.ToString();
            objdata.strContent3 = CKEditorControl2.Text.ToString();
            objdata.strContent4 = CKEditorControl3.Text.ToString();
            string strRes = objService.AddTemplateDetails(objdata);
            if (strRes == "1")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = 'ViewPage.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + Request.QueryString["index"] + "';});   </script>", false);
                // Response.Redirect("ViewPage.aspx");
            }
            else if (strRes == "2")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Data Updated Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = 'ViewPage.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + Request.QueryString["index"] + "';});   </script>", false);

            }
            else if (strRes == "4")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Page with same name already exist !', '" + Messages.TitleOfProject + "', function () {location.href = 'DynamicContent.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + Request.QueryString["index"] + "';});   </script>", false);

            }
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "alert('" + ex.ToString().Replace("'", "") + "');", true);
        }
     
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddGlink.aspx");
    }


}