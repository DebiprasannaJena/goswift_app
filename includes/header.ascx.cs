using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Web.UI.HtmlControls;
public partial class Application_includes_header : System.Web.UI.UserControl
{
    #region Variable Declaration
    CmsBusinesslayer objService = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["InvestorId"] != null)
            {
                if (Session["UserId"] == null)
                {


                    liDashBoardId.Visible = true;
                    // lblUserName.Text = "Admin";
                    lidept.Visible = true;
                    userDetails.Visible = false;
                }
                else
                {
                    invlogin.Visible = false;
                    liDashBoardId.Visible = true;
                    liDashBoardId.Style.Add("display", "block");


                    //lblUserName.Text = Session["UserId"].ToString();
                    lblUserName.Text = Session["IndustryName"].ToString();// Session["UserName"].ToString();

                    lidept.Visible = false;
                }
            }
            else
            {
                liDashBoardId.Visible = true;
                // lblUserName.Text = "Admin";
                lidept.Visible = true;
                userDetails.Visible = false;
            }
            string straction = "D";
            DataTable dt = objService.BindDepartment(straction);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    Label lsi = new Label();
                    li.Attributes.Add("class", "plSWClearance");
                    uldeparmentid.Controls.Add(li);
                    HtmlGenericControl anchor = new HtmlGenericControl("a");
                    anchor.Attributes.Add("href", "Department.aspx?deptid=" + dt.Rows[i]["intLevelDetailId"] + "");
                   
                    anchor.Attributes.Add("title", "" + dt.Rows[i]["nvchLevelName"] + "");
                    lsi.Text = "" + dt.Rows[i]["nvchLevelName"] + "";
                    anchor.Controls.Add(lsi);
                    li.Controls.Add(anchor);
                }
            }
            else
            {
                uldeparmentid.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
        }
    }
}