using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


public partial class Portal_CMS_CMSWithoutCkEdtor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGridview();
        }
    }
    protected void BindGridview()
    {

        string[] filesLoc = Directory.GetFiles(Server.MapPath("~/Portal/CMSImage/"));

        List<ListItem> files = new List<ListItem>();

        foreach (string file in filesLoc)
        {

            files.Add(new ListItem(Path.GetFileName(file)));

        }

        dtlist.DataSource = files;

        dtlist.DataBind();

    }


    protected void dtlist_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "imgClick")
        {
            HiddenField hid = (HiddenField)e.Item.FindControl("hdnImg");


            ImageButton btn = e.CommandSource as ImageButton;

            hid.Value = btn.ImageUrl;

            btn.Attributes.Add("onclick", "return ToPassValueToChild('" + hid.Value + "'); return false;");
           // string strImageURL = "https://localhost/SWP_AMS/" + btn.ImageUrl;


            //  Response.Write("<script>window.opener.document.getElementById('cke_80_textInput').value ='" + strImageURL.Replace("~/","") + "';window.close();</script>");

            //   Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + Request.QueryString["CKEditorFuncNum"].ToString() + ", \"" + btn.ImageUrl + "\");</script>");
        }
    }
}