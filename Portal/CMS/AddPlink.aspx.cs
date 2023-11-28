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

public partial class Portal_CMS_AddPlink : System.Web.UI.Page
{
    #region Variable Declaration
    CmsBusinesslayer objserv = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    CMSDetails obj = new CMSDetails();
    List<CMSDetails> newlist = new List<CMSDetails>();
    string str_Retvalue = "";
    string SaveFilePath = "";
    string SaveFileName = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FILLPageList();
            FillGlink();
            if (Request.QueryString["ID"] != null)
            {
                btnSave.Text = "Update";
                EditPlinkData(Convert.ToInt32(Request.QueryString["ID"]));
            }
        }
    }
    private void EditPlinkData(int ID)
    {
        try
        {
            CMSDetails objedit = new CMSDetails();
            List<CMSDetails> objComp = new List<CMSDetails>();

            objedit.actioncode = "S";
            objedit.Plinkid = ID;
            objComp = objserv.ViewPlinkDetails(objedit).ToList();
            if (objComp.Count > 0)
            {
                FillGlink();
                ddlGlink.SelectedValue = objComp[0].Glinkid.ToString();
                FILLPageList();
                drpPageList.SelectedValue = objComp[0].pageid.ToString();
                txtPlinkNmae.Text = objComp[0].Plink.ToString();
                txtURL.Text = objComp[0].vchURL.ToString();
                rdnLinkType.SelectedValue = objComp[0].intLinkType.ToString();
                rdnWindowType.SelectedValue = objComp[0].intWindowType.ToString();
                rdnPageType.SelectedValue = objComp[0].intPageType.ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }

    private void FillGlink()
    {
        try
        {
            obj.actioncode = "I";
            newlist = objserv.GlinkList(obj);
            ddlGlink.DataValueField = "Glinkid";
            ddlGlink.DataTextField = "Glink";
            ddlGlink.DataSource = newlist;
            ddlGlink.DataBind();
            ddlGlink.Items.Insert(0, new ListItem("--SELECT--", "0"));

        }
        catch (Exception ex)
        {

            Response.Write("<script>alert('" + ex.Message + "')</script>");
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
    protected void btnSave_Click(object sender, EventArgs e)
    {
        CMSDetails objdata = new CMSDetails();
        if (btnSave.Text == "Update")
        {
            objdata.actioncode = "X";
            objdata.Plinkid = Convert.ToInt32(Request.QueryString["ID"]);
        }
        else
        {
            objdata.actioncode = "H";
        }

        objdata.Glinkid = Convert.ToInt32(ddlGlink.SelectedValue);
        objdata.pageid = Convert.ToInt32(drpPageList.SelectedValue);
        objdata.Plink = txtPlinkNmae.Text.TrimEnd();
        objdata.intWindowType = Convert.ToInt32(rdnWindowType.SelectedValue);
        objdata.intPageType = Convert.ToInt32(rdnPageType.SelectedValue);
        objdata.intLinkType = Convert.ToInt32(rdnLinkType.SelectedValue);
        objdata.vchURL = txtURL.Text.Trim();
        obj.IntCreatedBy = Convert.ToInt32(Session["UserId"]);
        string strRes = objserv.AddPlinkDetails(objdata);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Data Saved Successfully !', '" + Messages.TitleOfProject + "', function () {location.href = 'AddPlink.aspx?linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + Request.QueryString["index"] + "';});   </script>", false);
      //  Response.Redirect("AddPlink.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddPlink.aspx");
    }
}