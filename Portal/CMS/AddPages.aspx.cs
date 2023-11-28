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
using CKEditor.NET;
using System.Web.UI.HtmlControls;

public partial class Portal_CMS_AddPages : System.Web.UI.Page
{
   
    #region Variable Declaration
    CmsBusinesslayer objService = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    TemplateDetails objTempDtls = new TemplateDetails();
    string str_Retvalue = "";
    string SaveFilePath = "";
    string SaveFileName = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            dv1.Visible = false;
            Dv2.Visible = false;
            Dv3.Visible = false;
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
                txtPageName.Text = objComp[0].pagename.ToString().Split('.')[0];
                txtMetaTitle.Text = objComp[0].title.ToString();
                drpTemplates.SelectedValue = objComp[0].Temptid.ToString();
                txtSnippet.Text = objComp[0].Sinppet.ToString();
                txtMetaAuthor.Text = objComp[0].Authorname.ToString();
                txtContent.Text = objComp[0].PageContent.ToString();
                if (drpTemplates.SelectedValue == "1")
                {
                    dv1.Visible = true;
                    Dv2.Visible = false;
                    Dv3.Visible = false;
                }
                if (drpTemplates.SelectedValue == "2")
                {
                    Dv2.Visible = true;
                    dv1.Visible = false;
                    Dv3.Visible = false;
                }
                if (drpTemplates.SelectedValue == "3")
                {
                    Dv3.Visible = true;
                    Dv2.Visible = false;
                    dv1.Visible = false;
                }
                hdnLastPageName.Value = objComp[0].pagename.ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    protected void drpTemplates_SelectedIndexChanged(object sender, EventArgs e)
    {
        //List<TemplateDetails> newlist = new List<TemplateDetails>();
        //objTempDtls.actioncode = "V";
        //objTempDtls.TemplateId = 0;

        //HtmlControl htmDiv;
        //htmDiv.ID="divHtm";
        //string strhtml="";
        //CKEditorControl ckeditorCntl = new CKEditorControl();
        //ckeditorCntl.ID = "txtContent";
        //ckeditorCntl.BasePath = "../../CKEditor/ckeditor";
        //ckeditorCntl.CustomConfig = "../../CKEditor/ckeditor/config.js";
        //ckeditorCntl.FilebrowserBrowseUrl = "CMSImages.aspx";


        // CKEditor.NET txtbx= null;    
        //txtbx =new CkEditor();
        //TextBox 

           // newlist = objService.TemplateContentDetails(objTempDtls);
            //strhtml = " <div id='divContent' runat='server'>";
            //string strhtmlFrm="<div class='form-group'> <div class='row'> <label class='col-sm-4'>Page Name</label><div class='col-sm-8'><span class='colon'>:</span>    <CKEditor:CKEditorControl ID='txtContent'  runat='server' Width='80%' Height='200px'  BasePath='../../CKEditor/ckeditor' CustomConfig='../../CKEditor/ckeditor/config.js'  FilebrowserBrowseUrl='CMSImages.aspx' HtmlEncodeOutput='False' ></CKEditor:CKEditorControl> <span class='mandetory'>*</span></div></div>";            
            //strhtml=strhtml+strhtmlFrm+" </div>";
            //divContent.InnerHtml = strhtml;

        if (drpTemplates.SelectedValue == "1")
        {
            dv1.Visible = true;
            Dv2.Visible = false;
            Dv3.Visible = false;
        }
        if (drpTemplates.SelectedValue == "2")
        {
            Dv2.Visible = true;
            dv1.Visible = false;
            Dv3.Visible = false;
        }
        if (drpTemplates.SelectedValue == "3")
        {
            Dv3.Visible = true;
            Dv2.Visible = false;
            dv1.Visible = false;
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
            if (drpTemplates.SelectedValue == "1")
            {
                templtname = "Template1.aspx";
            }
            if (drpTemplates.SelectedValue == "2")
            {
                templtname = "Template2.aspx";
            }
            if (drpTemplates.SelectedValue == "3")
            {
                templtname = "Template3.aspx";
            }
            string root = Server.MapPath("~");
            string Template = root + "\\" + templtname;
            if (btnSave.Text == "Save")
            {
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
                if (hdnLastPageName.Value.Split('.')[0].Trim() != txtPageName.Text.Trim())
                {
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

                }

            }
            else
            {
                objdata.actioncode = "A";
            }
            objdata.pagename = txtPageName.Text.TrimEnd() + ".aspx";
            objdata.title = txtMetaTitle.Text.TrimEnd();
            objdata.description = txtMetaDescription.Text.TrimEnd();
            objdata.keyword = txtMetaKeyWord.Text.TrimEnd();
            objdata.Template = drpTemplates.SelectedItem.Text;
            objdata.Sinppet = txtSnippet.Text.Trim();
            objdata.PageContent = txtContent.Text;
            objdata.Authorname = txtMetaAuthor.Text;
            objdata.Temptid = Convert.ToInt32(drpTemplates.SelectedValue);
         //   string strRes = objService.AddTemplateDetails(objdata);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = 'ViewPage.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + Request.QueryString["index"] + "';});   </script>", false);
           // Response.Redirect("ViewPage.aspx");
        }
        catch (Exception ex)
        {

            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "alert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddPages.aspx");
    }
}