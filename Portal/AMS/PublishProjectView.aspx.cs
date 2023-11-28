using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class SingleWindow_PublishProjectView : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    DataTable dt = null;
    string strVal = "";
    static int intType = 0;
    int gIntRowsCount;
    #endregion

    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                FillGrid();
                intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
                if (intType == 4)
                {
                    grdProjmst.Columns[6].Visible = true;
                }
                else
                {
                    grdProjmst.Columns[6].Visible = false;
                }
            }
        }
    }

    #endregion

    #region "Fill Grid"

    private void FillGrid()
    {
        AMS objams = new AMS();
        dt = new DataTable();
        try
        {

            objams.Action = "P";
            objams.TypeId = 0;
            dt = AMServices.ViewProjectMaster(objams);
            grdProjmst.DataSource = dt;
            grdProjmst.DataBind();
            
            gIntRowsCount = dt.Rows.Count;
            if (gIntRowsCount > 0)
            {
                DisplayPaging();
                lblMessage.Visible = false;
                btnArchieve.Visible = true;
            }
            else
            {
                lblMessage.Visible = true;
                lbtnAll.Visible = false;
                lblPaging.Visible = false;
                btnArchieve.Visible = false;
            }
        }
        catch (Exception)
        {
        }
        finally { objams = null; dt = null; }
    }

    #endregion

    #region "Paging"
    private void DisplayPaging()
    {
        if (this.grdProjmst.Rows.Count > 0)
        {
            this.lblPaging.Visible = true;
            lbtnAll.Visible = true;
        }
        if (this.grdProjmst.PageIndex + 1 == this.grdProjmst.PageCount)
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text + "</b> - </b>" + gIntRowsCount + "</b> of <b>" + gIntRowsCount + "</b>";
        }
        else
        {
            this.lblPaging.Text = "Results <b>" + ((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text + "</b>-<b>" + (Convert.ToInt32(((Label)grdProjmst.Rows[0].FindControl("lblsl")).Text) + Convert.ToInt32((grdProjmst.PageSize - 1))) + "</b> of <b>" + gIntRowsCount + "</b>";
        }

    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "View Paging";
            grdProjmst.AllowPaging = false;
            grdProjmst.PageIndex = 0;
        }
        else
        {
            lbtnAll.Text = "All";
            grdProjmst.AllowPaging = true;
        }
        FillGrid();
    }

    protected void grdProjmst_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.grdProjmst.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    #endregion

    #region "Fill Grid"

    protected void btnArchieve_Click(object sender, EventArgs e)
    {
        string cbSelect = "";      
        for (int i = 0; i <= grdProjmst.Rows.Count - 1; i++)
        {
            CheckBox chkItem = (CheckBox)grdProjmst.Rows[i].FindControl("chkSelectSingle");
            if (chkItem.Checked == true)
                cbSelect += grdProjmst.DataKeys[i].Values[0].ToString() + ",";

        }

        if (cbSelect != "")
        {
            cbSelect = cbSelect.TrimEnd(',');
            try
            {
                objams.Action = "UPA";
                objams.Business = cbSelect;
                objams.ApplicationDate = DateTime.Now;
                strVal = AMServices.AddProjectMaster(objams);
                string msg = Messages.ShowMessage(strVal).ToString();
                if (strVal == "2")
                {
                    FillGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('Project Archieved Successfully');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(btnArchieve, this.GetType(), "OnClick", "alert('" + msg + "');", true);
            }
            catch { }
            finally { objams = null; }
        }
        else
            ScriptManager.RegisterStartupScript(btnArchieve, this.GetType(), "OnClick", "alert('Select The Record(s) Want To Archive')", true);

        
    }

    #endregion

     protected void grdProjmst_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        objams = new AMS();
        try
        {
            
            if (e.CommandName == "Reopen")
            {
                //Added by Surya Prakash Barik to REOPEN a Published Project
                int rowIndex = int.Parse(e.CommandArgument.ToString());
                int ProjectId = Convert.ToInt32(grdProjmst.DataKeys[rowIndex].Values[0]);

                objams = new AMS();
                objams.Action = "RO";
                objams.ProjectId = ProjectId;
                
                strVal = AMServices.Reopen_Published_Project(objams);

                string[] ss = strVal.Split(',');
              

                if (ss[0] == "1")
                {
                    string intNewID = ss[1].ToString();
                    string strNewPath = Server.MapPath("~/SingleWindow/FinDoc/" + intNewID);
                    string strOldPath = Server.MapPath("~/SingleWindow/FinDoc/" + ProjectId);
                    CopyDirectory(strOldPath, strNewPath); //Copy the Financial Documents of the Previous Agenda Folder to New Agenda Folder
                    
                    ScriptManager.RegisterStartupScript(updPanel1, this.GetType(), "propsal", "alert('Project Reopened Successfully.');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(updPanel1, this.GetType(), "propsal", "alert('Project does not exist.');", true);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objams = null;
        FillGrid();
        }

     }

     public static void CopyDirectory(string sourceDirectory, string targetDirectory)
     {
         DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
         DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);
         CopyAll(diSource, diTarget);
     }

     public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
     {
         // Check if the target directory exists, if not, create it.
         if (!Directory.Exists(target.FullName))
         {
             Directory.CreateDirectory(target.FullName);
         }
         // Copy each file into it's new directory.
         if (Directory.Exists(source.FullName))  //Checking whether Source Folder Exist
         {
             foreach (FileInfo fi in source.GetFiles())
             {
                 fi.CopyTo(Path.Combine(target.ToString(), fi.Name), true);
             }
         }
     } 
     protected void grdProjmst_RowDataBound(object sender, GridViewRowEventArgs e)
     {

         if (e.Row.RowType == DataControlRowType.DataRow)
         {
             int intReopen = Convert.ToInt32(grdProjmst.DataKeys[e.Row.RowIndex].Values[1]);
             Button btnopen = (Button)e.Row.FindControl("btnReopen");

             if (intReopen == 0)
             {
                 btnopen.Text = "Reopen";
                 btnopen.Enabled = true;
             }
             else
             {
                 btnopen.Text = "Reopened";
                 btnopen.Enabled = false;
             }
         }

     }
}   