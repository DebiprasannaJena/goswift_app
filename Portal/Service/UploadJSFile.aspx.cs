using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;
using EntityLayer.Service;
using System.IO;
using BusinessLogicLayer.Service;

public partial class Service_UploadJSFile : System.Web.UI.Page
{
    #region Variables
    ServiceDetails objService1 = new ServiceDetails();
    public string strManageRight = "";
    public int intLevelDetailId;
    #endregion
    DataTable dtable;
    DataSet ds = new DataSet();
    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    ServiceDetails objServiceDet = new ServiceDetails();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDept();
            
        }
    }
    private void BindDept()
    {
        objServicelist = objService.BindDepartment("DP").ToList();
        ddldept.DataSource = objServicelist;
        ddldept.DataTextField = "strdeptname";
        ddldept.DataValueField = "Deptid";
        ddldept.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddldept.Items.Insert(0, list);
    }
    private void BindService()
    {
        objServicelist = objService.BindService("S", int.Parse(ddldept.SelectedValue)).ToList();
        ddlService.DataSource = objServicelist;
        ddlService.DataTextField = "strServiceName";
        ddlService.DataValueField = "intServiceId";
        ddlService.DataBind();
        ListItem list = new ListItem();
        list.Text = "--Select--";
        list.Value = "0";
        ddlService.Items.Insert(0, list);

    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindService();
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (JsUpload.HasFile)
        {

            if (Path.GetExtension(JsUpload.FileName) != ".js")
            {
                string strmsg11 = "<script>alert('Only .js file accepted!')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Click", strmsg11, true);
                return;
            }
        }
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("~/Service/UploasJs/"));
        if (!string.IsNullOrEmpty(JsUpload.FileName))
        {
            if (dir.Exists)
            {
                JsUpload.SaveAs(Server.MapPath("~/Service/UploasJs/" + JsUpload.FileName));
            }
            else
            {
                System.IO.Directory.CreateDirectory(Server.MapPath("~/Service/UploasJs"));
                JsUpload.SaveAs(Server.MapPath("~/Service/UploasJs/" + JsUpload.FileName));
            }
        }
        string Uploadname = JsUpload.FileName;
       
        objServiceDet.strAction = "I";
        objServiceDet.Deptid = Convert.ToInt32(ddldept.SelectedValue);
        objServiceDet.intServiceId = Convert.ToInt32(ddlService.SelectedValue);
        if (JsUpload.HasFile)
        {
            objServiceDet.UploadJs = Uploadname;
        }
        else
        {
            objServiceDet.UploadJs = ViewState["jsfile"].ToString();
        }
        //objServiceDet.UploadJs = Uploadname;
        objServiceDet.intCreatedBy = 1;
        string strRetVal = objService.UploadJs(objServiceDet);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Saved Successfully.');", true);
        ViewState["jsfile"] = null;
        clear();
        //BindGridDetails( sender,  e);
        Response.Write("<script>alert('Data Save SuccessFully');document.location.href='UploadJSFile.aspx'</script>");
    }
    public void clear()
    {
        ddldept.SelectedIndex = -1;
        ddlService.SelectedIndex = -1;
    }
    public void BindGridDetails(object sender, EventArgs e)
    {
        objServiceDet.strAction = "V";
        objServiceDet.Deptid = Convert.ToInt32(ddldept.SelectedValue);
        objServiceDet.intServiceId = Convert.ToInt32(ddlService.SelectedValue);
        objServicelist = objService.ViewUploadJS(objServiceDet).ToList();
        int aa = objServicelist.Count;
        if (aa > 0)
        {
            hdnjs.Value = objServicelist[0].UploadJs;
            LnkFileName.Text = objServicelist[0].UploadJs;
            //lblname.Text = objServicelist[0].UploadJs; 
            //LinkButton lnk = Page.FindControl("LnkFileName") as LinkButton;
            //lnk.Text = "aaa";
            ViewState["jsfile"] = hdnjs.Value;
        }
        //LnkFileName.Text = hdnjs.Value;
        //LnkFileName_OnClick(sender, e);
    }
    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridDetails( sender,  e);
         LnkFileName.Text = hdnjs.Value;
       
    }

    protected void LnkFileName_OnClick(object sender, EventArgs e)
    {
        string filename = LnkFileName.Text.ToString();
        if (filename != "")
        {
            string path = Server.MapPath("~/Service/UploasJs/" + filename);
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            if (file.Exists)
            {
                Response.Clear();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "application/octet-stream";
                Response.WriteFile(file.FullName);
                Response.End();
            }
            else
            {
                Response.Write("This file does not exist.");
            }
        }
    }

}