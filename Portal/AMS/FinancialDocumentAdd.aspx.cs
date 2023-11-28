using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Drawing;

public partial class SingleWindow_FinancialDocumentAdd : System.Web.UI.Page
{
    #region "Member Variable"
    static int rowIndex = -1;
    AMS objams = null;
    //Agenda objcs = null;
    string strVal = "";
    DataTable dt = null;
    DataTable dt1 = null;
    List<FinanceDoc> objFinanceDoc=null;
    FinanceDoc objFinDoc;
    int intType = 0;
    int Status = 0;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            Response.Redirect("ProjectMasterAdd.aspx");
        }

        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
            if (!IsPostBack)
            {

               
               
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    //if (System.Web.HttpContext.Current.Session["PType"] == null)
                    //{
                        FillDocuments();
                    //}
                    //else if (Session["PType"].ToString() == "2")
                    //{
                        FillSWPDocuments();
                    //}
                     
                }
                else
                {
                    this.btnReset.Text = "Reset";
                    this.btnSubmit.Text = "Finish";
                    Session["PType"] = null;
                }
                if (intType == 3 || intType == 4)
                {
                    if (Status == 3 || Status == 4 || Status == 0)
                    {
                        divRemark.Visible = false;
                    }
                    else
                    {
                        divRemark.Visible = true;
                    }
                }
                else{
                    divRemark.Visible = false;
                }
                //this.btnSubmit.Text = "Finish";
            }

        }
        FERemark.ValidChars = FERemark.ValidChars + "\r\n";
    }

    private void ClearAll()
    {
        btnAddDoc.Text = "Add More";
        rowIndex = -1;
    }

    private void FillDocuments()
    {
        objams = new AMS();
        try
        {
            objams.Action = "FD";  //Fetch Documents
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            dt = new DataTable();
            dt1 = new DataTable();
            dt = AMServices.ViewFinace(objams);
            Status = Convert.ToInt32(dt.Rows[0]["INTSTATUS"].ToString());
            if (intType == 3 || intType == 4)
            {
                objams.Action = "FRD";  //Fetch Remark
                dt1 = AMServices.ViewFinace(objams);
                if (dt1.Rows.Count > 0)
                {
                    DataView dv1 = new DataView(dt1);
                    dv1.RowFilter = "intCreatedBy=3";  //CMD

                    DataView dv2 = new DataView(dt1);
                    dv2.RowFilter = "intCreatedBy=4"; // GM

                    RptCMDRemark.DataSource = dv1;
                    RptCMDRemark.DataBind();

                    RptGMRemark.DataSource = dv2;
                    RptGMRemark.DataBind();

                    if (dv1.Count > 0)
                        RptCMDRemark.Visible = true;
                    else
                        RptCMDRemark.Visible = false;

                    if (dv2.Count > 0)
                        RptGMRemark.Visible = true;
                    else
                        RptGMRemark.Visible = false;

                    if (intType == 3) //CMD
                    {
                        if (dv1.Count > 0)
                        {
                            txtRemark.Text = dv1[dv1.Count -1][2].ToString();
                            hdnRemarkID.Value = dv1[dv1.Count - 1][4].ToString();
                        }
                    }
                    else if (intType == 4) //GM
                    {
                        if (dv2.Count > 0)
                        {
                            txtRemark.Text = dv2[dv2.Count - 1][2].ToString();
                            hdnRemarkID.Value = dv2[dv2.Count - 1][4].ToString();
                        }
                    }
                }  
            }

            if (dt.Rows.Count > 0)
            {
                objFinanceDoc = new List<FinanceDoc>();
                foreach (DataRow dr in dt.Rows)
                {
                    objFinDoc = new FinanceDoc();
                    objFinDoc.KeyId = Convert.ToInt32(dr[0]);
                    objFinDoc.ProjectId = Convert.ToInt32(dr[1]);
                    objFinDoc.FinOriDoc = Convert.ToString(dr[2]);
                    objFinDoc.FinNewDoc = Convert.ToString(dr[3]);
                    objFinDoc.Type = Convert.ToInt32(dr[4]);
                    objFinanceDoc.Add(objFinDoc);
                }

                grdFinDoc.DataSource = objFinanceDoc;
                grdFinDoc.DataBind();
                btnSubmit.CommandArgument = "1";
                if (intType == 3)
                {
                    btnSubmit.Text = "Next";
                    btnReset.Text = "Reset";
                }
                else
                {
                    btnReset.Text = "Cancel";
                    btnSubmit.Text = "Update";
                }
            }
            else
            {
                btnSubmit.CommandArgument = "2";
                if (intType == 3)
                {
                    btnSubmit.Text = "Next";
                    btnReset.Text = "Reset";
                }
                else
                {
                    btnReset.Text = "Reset";
                    btnSubmit.Text = "Finish";
                    
                }
            }

        }
        catch (Exception)
        {
            grdFinDoc.DataSource = dt;
            grdFinDoc.DataBind();
            btnSubmit.CommandArgument = "2";
            ClearAll();
        }
        finally { objams = null; }

    }


    private void FillSWPDocuments()
    {
        objams = new AMS();
        try
        {
            objams.Action = "VFDS";
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            dt = new DataTable();
           
            dt = AMServices.ViewSWPFinaceDoc(objams);
                 
            if (dt.Rows.Count > 0)
            {
                grdSWPDoc.DataSource = dt;
                grdSWPDoc.DataBind();
            }
         
        }
        catch (Exception)
        {
            grdSWPDoc.DataSource = dt;
            grdSWPDoc.DataBind();
           
            ClearAll();
        }
        finally { objams = null; }

    }


    protected void btnAddDoc_Click(object sender, EventArgs e)
    {
        AddNewRowToGridDoc();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
        try
        {
            objams = new AMS();
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            if (txtRemark.Visible == true)
            {
                objams.Remark = txtRemark.Text.Trim() == "" ? "No Comments" : txtRemark.Text.Trim();
                objams.intRemarkID = hdnRemarkID.Value == "" ? 0 : Convert.ToInt32(hdnRemarkID.Value);
                intType = AMServices.GetOfficersType(Convert.ToInt32(Session["Userid"]));
                objams.CreatedBy = intType;
            }
            else
            {
                objams.CreatedBy = Convert.ToInt16(Session["Userid"]);
            }

            if (btnSubmit.CommandArgument == "1")
                objams.Action = "UD";
            else if (btnSubmit.CommandArgument == "2")
                objams.Action = "AD";
               
                DataTable dt = new DataTable();
                dt.Columns.Add("FinNewDoc");
                dt.Columns.Add("FinOriDoc");

                for (int i = 0; i < grdFinDoc.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    HiddenField hdFinDoc = (HiddenField)grdFinDoc.Rows[i].FindControl("hdnFinDoc");
                    HiddenField hdOriDoc = (HiddenField)grdFinDoc.Rows[i].FindControl("hdnOriDoc");
                    
                    dr["FinNewDoc"] = hdFinDoc.Value;
                    dr["FinOriDoc"] = hdOriDoc.Value;
                   
                    dt.Rows.Add(dr);
                   
                }
                dt.TableName = "tblFinancialDoc";
                using (System.IO.StringWriter sw = new System.IO.StringWriter())
                {
                    dt.WriteXml(sw);
                    objams.XmlData = sw.ToString();
                }
            strVal = AMServices.AddFinanceDocument(objams);

            if (strVal == "2")
            {
                if (intType == 3)
                    ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Saved Sucessfully.');location.href='CmdApproval.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
                else
                    ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Updated Sucessfully.');location.href='ProjectMasterView.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "'</script>", false);
            }
            else
            {
                if (intType == 3)
                    ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Saved Sucessfully.');location.href='CmdApproval.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
                else
                ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Saved Sucessfully.');location.href='ProjectMasterView.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "'</script>", false);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objams = null; }

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect("FinancialDocumentAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "");
    }

    private void AddNewRowToGridDoc()
    {
        objFinanceDoc = new List<FinanceDoc>();

        if (grdFinDoc.Rows.Count > 0)
        {
            ConvertToListDoc();
        }

        if (btnAddDoc.Text == "Add More")
        {
            if (objFinanceDoc.Any(d => d.FinOriDoc == FinDoc.FileName))
            {
                ScriptManager.RegisterStartupScript(btnAddDoc, Page.GetType(), "", "alert('" + FinDoc.FileName  + " already Uploaded.');", true);
                return;
            }
            else
            {
                objFinDoc = new FinanceDoc();
                objFinDoc.ProjectId = Convert.ToInt32(Request.QueryString["ID"].ToString());

                Guid newID = Guid.NewGuid();
                if (FinDoc.HasFile)
                {
                    if (FinDoc.PostedFile.ContentLength > 4096000)
                    {
                        string strmsg11 = "<script>alert('The file has to be less than 4MB!')</script>";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg11, false);
                        return;
                    }
                    if (Path.GetExtension(FinDoc.FileName) != ".pdf")
                    {
                        string strmsg = "<script>alert('Only .pdf file accepted!')</script>";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg, false);
                        return;
                    }
                    string retmsg = string.Empty;
                    string path = Server.MapPath("~/Portal/AMS/FinDoc/" + Request.QueryString["ID"].ToString());

                    string FileOriName = FinDoc.FileName;
                    string ext = System.IO.Path.GetExtension(FinDoc.FileName);
                    string FileNewName = newID + ext;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FinDoc.SaveAs(path + "/" + FileNewName);
                    objFinDoc.FinNewDoc = FileNewName;
                    objFinDoc.FinOriDoc = FileOriName;

                    objFinanceDoc.Add(objFinDoc);
                }
            }
        }

        FillGridViewDoc(objFinanceDoc);
    }

    private List<FinanceDoc> ConvertToListDoc()
    {
        objFinanceDoc = new List<FinanceDoc>();

        foreach (GridViewRow rw in this.grdFinDoc.Rows)
        {
            objFinDoc = new FinanceDoc();
            objFinDoc.SlNo = Convert.ToInt32(rw.RowIndex + 1);
            objFinDoc.ProjectId = Convert.ToInt32(grdFinDoc.DataKeys[rw.RowIndex].Values[0].ToString());
            HiddenField hdFinDoc = rw.FindControl("hdnFinDoc") as HiddenField;
            HiddenField hdOriDoc = rw.FindControl("hdnOriDoc") as HiddenField;

            objFinDoc.FinOriDoc = hdOriDoc.Value;
            objFinDoc.FinNewDoc = hdFinDoc.Value;
            objFinanceDoc.Add(objFinDoc);
        }
        return objFinanceDoc;
    } 

    private void FillGridViewDoc(List<FinanceDoc> objDetails)
    {
        grdFinDoc.DataSource = objDetails;
        grdFinDoc.DataBind();

        ClearAll();
    }

    protected void grdFinDoc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<FinanceDoc> objList = new List<FinanceDoc>();
        objList = ConvertToListDoc();
        objList.RemoveAll(d => d.FinNewDoc == grdFinDoc.DataKeys[e.RowIndex].Values[1].ToString());
        FillGridViewDoc(objList);
        if (objList.Count == 0)
            btnAddDoc.Text = "Add More";
    }

    protected void grdFinDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlinkFinDoc = (HyperLink)e.Row.FindControl("hlDoc");
            HiddenField hdhDoc = (HiddenField)e.Row.FindControl("hdnFinDoc");
            string strDocs = hdhDoc.Value;
            if (hdhDoc.Value != "")
            {
                hlinkFinDoc.NavigateUrl = "../Portal/AMS/FinDoc/" + Request.QueryString["ID"] + "/" + strDocs;
            }
            else
            {
                hlinkFinDoc.ImageUrl = "";
            }
        }

    }

    protected void grdSWPDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlinkFinDoc = (HyperLink)e.Row.FindControl("hlDoc");
            HiddenField hdhDoc = (HiddenField)e.Row.FindControl("hdnFinDoc");
            string strDocs = hdhDoc.Value;
            if (hdhDoc.Value != "")
            {
                hlinkFinDoc.NavigateUrl = strDocs;
            }
            else
            {
                hlinkFinDoc.ImageUrl = "";
            }
        }

        if (grdSWPDoc.Rows.Count ==1)
        {
            GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            TableHeaderCell cell = new TableHeaderCell();
            cell.Text = "Documents Attached in Single Window";
            cell.ColumnSpan = 2;
            row.Controls.Add(cell);

            row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
            if (grdSWPDoc.Rows.Count > 0)
                grdSWPDoc.HeaderRow.Parent.Controls.AddAt(0, row);
        }
    }

    public class FinanceDoc
    {
        public int SlNo { get; set; }
        public int KeyId { get; set; }
        public int ProjectId { get; set; }
        public string FinOriDoc { get; set; }
        public string FinNewDoc { get; set; }
        public int Type { get; set; }
    }
}