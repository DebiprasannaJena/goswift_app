using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;

public partial class Portal_SuperAdmin_AddDynamicNotification : System.Web.UI.Page
{
    string Notification = null, DefaultPage = null, LoginPage = null;
    string IndustrialPage = null,  NonIndustrialPage = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        string path = (Server.MapPath("~/UserXML/" + "Test.xml"));
        if (!File.Exists(path))
        {
            create();
            BindDataToGridview();
            btnSave.Text = "Save";
          
        }
       else if (!IsPostBack)
        {
            
            BindDataToGridview();
            btnSave.Text = "Edit";
        }

    }
    protected void BindDataToGridview()
    {
        //open the tender xml file  
        XmlTextReader xmlreader = new XmlTextReader(Server.MapPath("~/UserXML/" + "Test.xml"));
        //reading the xml data  
        DataSet ds = new DataSet();
        ds.ReadXml(xmlreader);
        xmlreader.Close();
        //if ds is not empty  
        if (ds.Tables.Count != 0)
        {
            //Bind Data to gridview  
            GridView1.DataSource = ds;
            GridView1.DataBind();  
            TxtNotification.Text= ds.Tables[0].Rows[0]["Notification"].ToString();
            rdnDefaultpage.SelectedValue= ds.Tables[0].Rows[0]["DefaultPage"].ToString();
            rdnLoginPage.SelectedValue = ds.Tables[0].Rows[0]["LoginPage"].ToString();
            rdnIndustrialPage.SelectedValue = ds.Tables[0].Rows[0]["IndustrialPage"].ToString();
            rdnNonIndustrialPage.SelectedValue = ds.Tables[0].Rows[0]["NonIndustrialPage"].ToString();
        }
    }

    public void create()
    {
        //Start writer
        XmlTextWriter writer = new XmlTextWriter(Server.MapPath("~/UserXML/" + "Test.xml"), System.Text.Encoding.UTF8);

        //Start XM DOcument
        writer.WriteStartDocument(true);
        writer.Formatting = Formatting.Indented;
        writer.Indentation = 2;

        //ROOT Element
        writer.WriteStartElement("Notifications");

        //call create nodes method
        createNode(Notification, DefaultPage, LoginPage, IndustrialPage, NonIndustrialPage, writer);

        writer.WriteEndElement();

        //End XML Document
        writer.WriteEndDocument();

        //Close writer
        writer.Close();
    }

    private void createNode(string notification, string defaultpage, string loginpage, string industrialpage, string nonindustrialpage, XmlTextWriter writer)
    {  
        writer.WriteStartElement("Notification");
        writer.WriteString(notification);
        writer.WriteEndElement();

        writer.WriteStartElement("DefaultPage");
        writer.WriteString(defaultpage);
        writer.WriteEndElement();

        writer.WriteStartElement("LoginPage");
        writer.WriteString(loginpage);
        writer.WriteEndElement();

        writer.WriteStartElement("IndustrialPage");
        writer.WriteString(industrialpage);
        writer.WriteEndElement();

        writer.WriteStartElement("NonIndustrialPage");
        writer.WriteString(nonindustrialpage);
        writer.WriteEndElement();


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        try
        {
          
                Notification = Convert.ToString(TxtNotification.Text);
                DefaultPage = Convert.ToString(rdnDefaultpage.SelectedValue);
                LoginPage = Convert.ToString(rdnLoginPage.SelectedValue);
                IndustrialPage = Convert.ToString(rdnIndustrialPage.SelectedValue);
                NonIndustrialPage = Convert.ToString(rdnNonIndustrialPage.SelectedValue);

                create();
                BindDataToGridview();
           
        }
        catch(Exception ex)
        {

        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        TxtNotification.Text = "";
        rdnDefaultpage.SelectedIndex = -1;
        rdnLoginPage.SelectedIndex = -1;
        rdnIndustrialPage.SelectedIndex = -1;
        rdnNonIndustrialPage.SelectedIndex = -1;
        btnSave.Text = "Save";
    }
}