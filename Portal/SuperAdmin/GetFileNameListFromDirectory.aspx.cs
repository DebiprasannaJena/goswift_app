using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Net;

public partial class Portal_SuperAdmin_GetFileNameListFromDirectory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        /// This page can only be accessed by goadmin.
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/Default.aspx");
        }

        if (Convert.ToInt32(Session["UserId"]) != 1)
        {
            Response.Redirect("~/Default.aspx");
        }

        LblTotal.Text = "";
    }

    //This function is uses to get the files available in virtual path
    private void FillGridVirtual()
    {
        try
        {
            string Path;
            if (DdlVirtualDirectoryPath.SelectedItem.Value != "0")
            {
                Path = Server.MapPath(DdlVirtualDirectoryPath.SelectedItem.Text);
            }
            else
            {
                Path = Server.MapPath(TxtVirtualDirectoryPath.Text.Trim());
            }

            DirectoryInfo Directory = new DirectoryInfo(Path);
            FileInfo[] FilesPaths = Directory.GetFiles("*" + (TxtVirtualFilePattern.Text.Trim() != "" ? TxtVirtualFilePattern.Text.Trim() : "*")).OrderByDescending(f => f.CreationTime).ToArray();

            ///Filter the required file list by extension.
            ArrayList arrList = new ArrayList();
            foreach (FileInfo file2 in FilesPaths)
            {
                string strFileExt = file2.Extension.ToLower();
                if (strFileExt == ".jpg" || strFileExt == ".jpeg" || strFileExt == ".gif" || strFileExt == ".png" || strFileExt == ".pdf" || strFileExt == ".txt" || strFileExt == ".xls" || strFileExt == ".xlsx" || strFileExt == ".doc" || strFileExt == ".docx")
                {
                    arrList.Add(file2);
                }
            }

            GrdVirtual.DataSource = arrList;
            GrdVirtual.DataBind();

            LblTotal.ForeColor = System.Drawing.Color.DarkRed;
            LblTotal.Text = "Total File(s) Available :- " + "<span style='color: red;font-size:16px;font-weight:900;'>" + arrList.Count;
        }
        catch (Exception ex)
        {
            Lbl_Error.Text = ex.Message;
        }

    }
    protected void BtnSearchVirtual_Click(object sender, EventArgs e)
    {      
        try
        {
            if (DdlVirtualDirectoryPath.SelectedValue == "0" && TxtVirtualDirectoryPath.Text.Trim() == "")
            {
                DdlVirtualDirectoryPath.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select or enter a path.</strong>');", true);
                return;
            }

            GrdPhysical.Visible = false;
            GrdVirtual.Visible = true;
            Lbl_Error.Text = "";

            if (DdlVirtualDirectoryPath.SelectedItem.Value == "0")
            {
                if (TxtVirtualDirectoryPath.Text != "")
                {
                    FillGridVirtual();
                }
                else
                {
                    TxtVirtualDirectoryPath.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter directory path.</strong>');", true);
                    return;
                }
            }
            else
            {
                FillGridVirtual();
            }
        }
        catch (Exception ex)
        {
            Lbl_Error.Text = ex.Message.ToString();
            GrdVirtual.Visible = false;
        }
    }
    protected void BtnResetVirtual_Click(object sender, EventArgs e)
    {
        DdlVirtualDirectoryPath.SelectedIndex = 0;
        TxtVirtualDirectoryPath.Text = "";
        TxtVirtualFilePattern.Text = "";
    }
    protected void GrdVirtual_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GrdVirtual.Columns[2].ItemStyle.BackColor = System.Drawing.Color.White;
                Label LblName = (Label)e.Row.FindControl("LblName");
                Label LblDirectoryName = (Label)e.Row.FindControl("LblDirectoryName");
                HyperLink HlDisplay = (HyperLink)e.Row.FindControl("HlDisplay");

                if (DdlVirtualDirectoryPath.SelectedIndex > 0)
                {
                    HlDisplay.NavigateUrl = DdlVirtualDirectoryPath.SelectedItem.Text + "/" + LblName.Text;
                }
                else if (TxtVirtualDirectoryPath.Text != "")
                {
                    HlDisplay.NavigateUrl = TxtVirtualDirectoryPath.Text + "/" + LblName.Text;
                }
                else
                {
                    HlDisplay.NavigateUrl = TxtPhysicalDirectoryPath.Text + "/" + LblName.Text;
                }
            }
        }
        catch (Exception ex)
        {
            Lbl_Error.Text = ex.Message;
        }
    }

    //This function is used to get the files available in physical path
    private void FillGridPhysical()
    {
        try
        {
            string Path;
            if (DdlPhysicalDirectoryPath.SelectedItem.Value != "0")
            {
                Path = DdlPhysicalDirectoryPath.SelectedItem.Text;
            }
            else
            {
                Path = TxtPhysicalDirectoryPath.Text.Trim();
            }
            
            DirectoryInfo Directory = new DirectoryInfo(Path);           
            FileInfo[] FilesPaths = Directory.GetFiles("*" + (TxtPhysicalFilePattern.Text.Trim() != "" ? TxtPhysicalFilePattern.Text.Trim() : "*")).OrderByDescending(f => f.CreationTime).ToArray();
            
            ///Filter the required file list by extension.
            ArrayList arrList1 = new ArrayList();
            foreach (FileInfo file2 in FilesPaths)
            {
                string strFileExt = file2.Extension.ToLower();
                if (strFileExt == ".jpg" || strFileExt == ".jpeg" || strFileExt == ".gif" || strFileExt == ".png" || strFileExt == ".pdf" || strFileExt == ".txt" || strFileExt == ".xls" || strFileExt == ".xlsx" || strFileExt==".doc" || strFileExt == ".docx")
                {
                    arrList1.Add(file2);
                }
            }

            GrdPhysical.DataSource = arrList1;
            GrdPhysical.DataBind();

            LblTotal.ForeColor = System.Drawing.Color.DarkRed;
            LblTotal.Text = "Total File(s) Available :- " + "<span style='color: red;font-size:16px;font-weight:900;'>" + arrList1.Count;
        }
        catch (Exception ex)
        {
            Lbl_Error.Text = ex.Message;
            GrdPhysical.Visible = false;
        }
    }
    protected void BtnSearchPhysical_Click(object sender, EventArgs e)
    {
        try
        {
            if (DdlPhysicalDirectoryPath.SelectedValue == "0" && TxtPhysicalDirectoryPath.Text.Trim() == "")
            {
                DdlPhysicalDirectoryPath.Focus();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please select or enter a path.</strong>');", true);
                return;
            }

            GrdPhysical.Visible = true;
            GrdVirtual.Visible = false;
            Lbl_Error.Text = "";

            if (DdlPhysicalDirectoryPath.SelectedItem.Value == "0")
            {
                if (TxtPhysicalDirectoryPath.Text != "")
                {
                    FillGridPhysical();
                }
                else
                {
                    TxtPhysicalDirectoryPath.Focus();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Fail", "jAlert('<strong>Please enter physical directory path.</strong>');", true);
                    return;
                }
            }
            else
            {
                FillGridPhysical();
            }
        }
        catch (Exception ex)
        {
            Lbl_Error.Text = ex.Message;
            GrdPhysical.Visible = false;
        }
    }
    protected void BtnResetPhysical_Click(object sender, EventArgs e)
    {       
        DdlPhysicalDirectoryPath.SelectedIndex = 0;
        TxtPhysicalDirectoryPath.Text = "";
        TxtPhysicalFilePattern.Text = "";
    }
    protected void BtnDownload_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.Parent.Parent;

        Label LblFullName1 = (Label)row.FindControl("LblFullName1");
        Label LblName1 = (Label)row.FindControl("LblName1");

        Response.Redirect("~/DownloadFileFromRemoteServer.ashx?fileName=" + LblName1.Text + "&fileFullPath=" + LblFullName1.Text);
    }
}
