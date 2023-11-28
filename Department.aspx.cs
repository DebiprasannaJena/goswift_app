using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLogicLayer.CMS;
using EntityLayer.CMS;
using System.Web.UI.HtmlControls;
using System.Data;
public partial class Department : System.Web.UI.Page
{
    #region Variable Declaration
    CmsBusinesslayer objService = new CmsBusinesslayer();
    CMSDetails objServiceEntity = new CMSDetails();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        linkfb.Visible = false; linkfscw.Visible = false;
        if (Request.QueryString["deptid"].ToString() == "10")
        {
            linkfb.Visible = true;
        }
        else if (Request.QueryString["deptid"].ToString() == "4")
        {
            linkfscw.Visible = true;
        }
        else
        {
            linkfb.Visible = false; linkfscw.Visible = false;
        }

        try
        {
            int intDeptId = Convert.ToInt32(Request.QueryString["deptid"]);
            DataTable dt = objService.BindServiceData("S", intDeptId);

            if (intDeptId == 659)
            {
                DataRow dr = dt.NewRow();
                dr["INT_SERVICEID"] = "51";
                dr["VCH_SERVICENAME"] = "Road cutting request form";
                dr["nvchLevelName"] = "IDCO";
                dr["INT_CategoryType"] = "1";
                dt.Rows.Add(dr);
            }
            else if (intDeptId == 12)
            {
                DataRow dr = dt.NewRow();
                dr["INT_SERVICEID"] = "41";
                dr["VCH_SERVICENAME"] = "Permission to draw Water";
                dr["nvchLevelName"] = "Department of Water Resources";
                dr["INT_CategoryType"] = "2";
                dt.Rows.Add(dr);
                dt.Rows.RemoveAt(0);
            }
            //else if (intDeptId == 12)
            //{
            //    DataRow dr = dt.NewRow();
            //    dr["INT_SERVICEID"] = "29";
            //    dr["VCH_SERVICENAME"] = "Obtaining water connection";
            //    dr["nvchLevelName"] = "Department of Water Resources";
            //    dr["INT_CategoryType"] = "2";
            //    dt.Rows.Add(dr);
            //    dt.Rows.RemoveAt(0);
            //}
            else if (intDeptId == 363)
            {
                DataRow dr = dt.NewRow();
                dr["INT_SERVICEID"] = "51";
                dr["VCH_SERVICENAME"] = "Road cutting request form";
                dr["nvchLevelName"] = "Rural Development";
                dr["INT_CategoryType"] = "1";
                dt.Rows.Add(dr);
            }
            else if (intDeptId == 878)
            {
                DataRow dr = dt.NewRow();
                dr["INT_SERVICEID"] = "51";
                dr["VCH_SERVICENAME"] = "Road cutting request form";
                dr["nvchLevelName"] = "Works Department";
                dr["INT_CategoryType"] = "1";
                dt.Rows.Add(dr);
            }
            else if (intDeptId == 5)
            {
                DataRow dr = dt.NewRow();
                dr["INT_SERVICEID"] = "51";
                dr["VCH_SERVICENAME"] = "Road cutting request form";
                dr["nvchLevelName"] = "Housing and Urban Development Department (H&UD)";
                dr["INT_CategoryType"] = "1";
                dt.Rows.Add(dr);
            }
            else if (intDeptId == 8)
            {
                dt.Rows.RemoveAt(1);
            }

            Label lblhead = new Label();
            lblhead.Text = "" + dt.Rows[0]["nvchLevelName"] + "";
            hdid.Controls.Add(lblhead);
            Label1.Text = dt.Rows[0]["nvchLevelName"].ToString();
            //Session["Department"] = dt.Rows[0]["nvchLevelName"].ToString();

            DataView dv = new DataView();
            dv = dt.DefaultView;
            dv.RowFilter = "int_CategoryType=1";
            DataTable dt1;
            dt1 = dv.ToTable();
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                Label lsi = new Label();
                li.Attributes.Add("class", "plSWClearance");
                oldeptid.Controls.Add(li);
                HtmlGenericControl anchor = new HtmlGenericControl("a");
                //anchor.Attributes.Add("href", "ServiceDetails.aspx?Srvcid=" + dt1.Rows[i]["INT_SERVICEID"] + "");
                anchor.Attributes.Add("href", "ServiceDetails.aspx?Deptid=" + intDeptId + "&Department=" + dt.Rows[0]["nvchLevelName"].ToString() + "&Srvcid=" + dt1.Rows[i]["INT_SERVICEID"] + "");
                anchor.Attributes.Add("target", "");
                anchor.Attributes.Add("title", "" + dt1.Rows[i]["VCH_SERVICENAME"] + "");
                lsi.Text = "" + dt1.Rows[i]["VCH_SERVICENAME"] + "";
                anchor.Controls.Add(lsi);
                li.Controls.Add(anchor);
            }

            dv = dt.DefaultView;
            dv.RowFilter = "int_CategoryType=2";
            DataTable dt2;
            dt2 = dv.ToTable();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                Label lsi = new Label();
                li.Attributes.Add("class", "plSWClearance");
                ol1.Controls.Add(li);
                HtmlGenericControl anchor = new HtmlGenericControl("a");
                //anchor.Attributes.Add("href", "ServiceDetails.aspx?Srvcid=" + dt2.Rows[i]["INT_SERVICEID"] + "");
                anchor.Attributes.Add("href", "ServiceDetails.aspx?Deptid=" + intDeptId + "&Department=" + dt.Rows[0]["nvchLevelName"].ToString() + "&Srvcid=" + dt2.Rows[i]["INT_SERVICEID"] + "");
                anchor.Attributes.Add("target", "");
                anchor.Attributes.Add("title", "" + dt.Rows[i]["VCH_SERVICENAME"] + "");
                lsi.Text = "" + dt2.Rows[i]["VCH_SERVICENAME"] + "";
                anchor.Controls.Add(lsi);
                li.Controls.Add(anchor);
            }

            DataRow[] rowsFiltered = dt2.Select("INT_SERVICEID=29");
            foreach (DataRow row in rowsFiltered)
            {
                if (row[0].ToString() == "29")
                {
                    h3id.Visible = true;
                    ol2.Visible = true;
                }
                else
                {
                    h3id.Visible = false;
                    ol2.Visible = false;
                }
            }

            //Session["SERVICE"] = dt.Rows[0]["VCH_SERVICENAME"].ToString();
            //Session["deptid"] = Request.QueryString["deptid"].ToString();
            string straction = "D";
            DataTable dtnew = objService.BindDepartment(straction);
            if (dtnew.Rows.Count > 0)
            {
                for (int i = 0; i < dtnew.Rows.Count; i++)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    Label lsinew = new Label();
                    li.Attributes.Add("class", "plDIndustries");
                    depulid.Controls.Add(li);
                    HtmlGenericControl anchor = new HtmlGenericControl("a");
                    if (Request.QueryString["deptid"].ToString() != dtnew.Rows[i]["intLevelDetailId"].ToString())
                    {
                        anchor.Attributes.Add("href", "Department.aspx?deptid=" + dtnew.Rows[i]["intLevelDetailId"] + "");
                        anchor.Attributes.Add("target", "");
                        anchor.Attributes.Add("title", "" + dtnew.Rows[i]["nvchLevelName"] + "");
                    }
                    else
                    {
                        anchor.Attributes.Add("title", "" + dtnew.Rows[i]["nvchLevelName"] + "");
                        li.Attributes.Add("class", "active");

                    }
                    lsinew.Text = "" + dtnew.Rows[i]["nvchLevelName"] + "";
                    anchor.Controls.Add(lsinew);
                    li.Controls.Add(anchor);
                }
            }
            else
            {
                depulid.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Departmentpage");
            Response.Redirect("Default.aspx");
            // throw new Exception(ex.Message);
        }
    }
}