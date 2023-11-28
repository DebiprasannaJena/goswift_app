using System;



public partial class Admin_Menu_Manage_AdminConsoleFooter : System.Web.UI.Page
{
    
    public string strUrl;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        GeUrl();
    }
    private void GeUrl()
    {
        try
        {
            //hypVisit.Text = ("http://" + objComapy.CompanyUrl(objComapy));
            //strUrl = ("http://" + objComapy.CompanyUrl(objComapy));
        }
        catch(Exception ex)
        {
            Response.Write("<script>alert('"+ex.Message+"');</script>");
        }
    }
}
