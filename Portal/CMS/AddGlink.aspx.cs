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


public partial class Portal_CMS_AddGlink : System.Web.UI.Page
{
    CmsBusinesslayer objserv = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    CMSDetails obj = new CMSDetails();
    List<CMSDetails> newlist = new List<CMSDetails>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FILLPageList();
            if (Request.QueryString["ID"] != null)
            {
                btnSave.Text = "Update";
                EditGlinkData(Convert.ToInt32(Request.QueryString["ID"]));
            }

        }
    }
    private void FILLPageList()
    {
        try
        {
            obj.actioncode = "B";
            newlist = objserv.BindPageList(obj);
            drpPageList.DataValueField = "pageid";
            drpPageList.DataTextField = "pagename";
            drpPageList.DataSource = newlist;
            drpPageList.DataBind();
            drpPageList.Items.Insert(0, new ListItem("--SELECT--", "0"));

        }
        catch (Exception ex)
        {

            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    private void EditGlinkData(int ID)
    {
        try
        {
            CMSDetails objedit = new CMSDetails();
            List<CMSDetails> objComp = new List<CMSDetails>();

            objedit.actioncode = "AB";
            objedit.Glinkid = ID;
            objComp = objserv.ViewGlinkDetails(objedit).ToList();
            if (objComp.Count > 0)
            {
                txtGlink.Text = objComp[0].Glink.ToString();
                FILLPageList();
                drpPageList.SelectedValue = objComp[0].pageid.ToString();
                rdnWindowType.SelectedValue = objComp[0].intWindowType.ToString();
                rdnPageType.SelectedValue = objComp[0].intPageType.ToString();
                txtURL.Text = objComp[0].pagename.ToString();
                txtModalId.Text = objComp[0].vchModalId.ToString();

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CMSDetails objdata = new CMSDetails();
        if (btnSave.Text == "Update")
        {
            objdata.actioncode = "AC";
            objdata.Glinkid = Convert.ToInt32(Request.QueryString["ID"]);
        }
        else
        {
            objdata.actioncode = "G";
        }

        objdata.Glink = txtGlink.Text.TrimEnd();
        objdata.intWindowType = Convert.ToInt32(rdnWindowType.SelectedValue);
        objdata.pageid = Convert.ToInt32(drpPageList.SelectedValue);
        objdata.intPageType = Convert.ToInt32(rdnPageType.SelectedValue);
        objdata.vchURL = txtURL.Text.TrimEnd();
        objdata.vchModalId = txtModalId.Text.TrimEnd();
       
        string strRes = objserv.AddGlinkDetails(objdata);

        if (strRes == "4") // Duplicate Modalid
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Success", "jAlert('<strong>Modal Id  is already exists !</strong>');", true);

        }
        else
        {   //strRes == "1" for Success
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = 'ViewGlink.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + Request.QueryString["index"] + "';});   </script>", false);
        }
        

       // Response.Redirect("ViewGlink.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddGlink.aspx");
    }
}