using System;
////using CSMPDK_3_0;

public partial class Admin_Menu_Manage_AdminConsoleHeader : System.Web.UI.Page
{
    //// CommonDLL objCmndl = new CommonDLL();
    protected void Page_Init(object sender, EventArgs e)
    {        
        if (Session["userName"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
        }
    }
    protected void logout_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        //Response.Redirect("~/login.aspx");
        //Response.Write("<script>top.location.href='../../default.aspx'</script>");
        Response.Write("<script>top.location.href='../../AdminConsoleLogin.aspx'</script>");
        //Response.Write("<script>top.location.href='../../SessionRedirect.aspx'</script>");
    }    
}
