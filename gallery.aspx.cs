using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using BusinessLogicLayer.Common;
using EntityLayer.Common;
using System.IO;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Text;

public partial class gallery : System.Web.UI.Page
{
    CmsBusinesslayer objService2 = new CmsBusinesslayer();
    CommonBusinessLayer objService = new CommonBusinessLayer();
    CmsBusinesslayer objService1 = new CmsBusinesslayer();
    CMSDetails objCms = new CMSDetails();
    Feedback objServiceEntity = new Feedback();
    Gallery objServiceGallery = new Gallery();
    CmsBusinesslayer objServif = new CmsBusinesslayer();
    string str_Retvalue = "";
    string strprojname = System.Configuration.ConfigurationManager.AppSettings["ProjectName"];
    //CommonBusinessLayer objService = new CommonBusinessLayer();
    EntityLayer.Common.Gallery objProp = new EntityLayer.Common.Gallery();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
         
            BindRepeater();
        }
    }
    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //Image img = (Image)e.Item.FindControl("Image1");

        //HiddenField hid1 = (HiddenField)e.Item.FindControl("hid1");
        //img.ImageUrl = "ImageGallery/" + hid1.Value; 
        ////string filePath = System.Configuration.ConfigurationManager.AppSettings["ImageGallery"].ToString();
        ////img.ImageUrl = filePath + hid1.Value;
        // HiddenField hidLiteral = (HiddenField)e.Item.FindControl("hidLiteral");       
        //Literal  Literal1=(Literal)e.Item.FindControl("Literal1");
        //Literal1.Text = hidLiteral.Value;
        //Literal1.Visible = true;
        Image img = (Image)e.Item.FindControl("Image1");
        HiddenField hid1 = (HiddenField)e.Item.FindControl("hid1");
        img.ImageUrl = "~/Portal/ImageGallery/" + "S_" + hid1.Value;
    }

    private void BindRepeater()
    {
        List<Gallery> objList = new List<Gallery>();
        try
        {
            objProp.strAction = "V";
            objList = objService.ViewGallery(objProp).ToList();
            Repeater1.DataSource = objList;
            Repeater1.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, typeof(Page), "Failure", "alert('" + ex.ToString().Replace("'", "") + "');", true);
        }
    }

    
}