//******************************************************************************************************************
// File Name             :   SingleWindow/FinancialPerformanceAdd.aspx
// Description           :   To Add Financial details against a project by Nodal Officer
// Created by            :   
// Created on            :   
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         1                          21-July-2016        Tapan Kumar Mishra            Add Project Dropdown and List
//         2                          03-Oct-2017         Surya Prakash Barik           Show data from SWP,add CMD Comment,
//                                                                                      Remove document Upload in this step, Add Remark Field
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

public partial class SingleWindow_FinancialPerformanceAdd : System.Web.UI.Page
{
        #region "Member Variable"
    static int rowIndex = -1;
    AMS objams = null;
    Agenda objcs = null;
    string strVal = "";
    DataTable dt = null;
    DataTable dt1 = null;
    DataTable dt2 = null;
    DataTable dt3 = null;
    List<Finance> objFinance;
    Finance objFin;
    int intType = 0;
    int Status = 0;
    #endregion

    #region "Page Load"

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

                
                
                FinanceYear();
                FillDocuments();
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    if (System.Web.HttpContext.Current.Session["PType"] == null)
                    {
                        FillDetails("E");
                    }
                    else if (Session["PType"].ToString() == "2")
                    {
                        FillDetails("VFPA");
                    }

                }
                else
                {
                    this.btnReset.Text = "Reset";
                    this.btnSubmit.Text = "Next";
                    Session["PType"] = null;
                }
                //this.btnSubmit.Text = "Finish";
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
                else
                {
                    divRemark.Visible = false;
                }
            }

        }
        FERemark.ValidChars = FERemark.ValidChars + "\r\n";
        FEtxtUsrRemark.ValidChars = FEtxtUsrRemark.ValidChars + "\r\n";
    }

    #endregion

    #region "User function"
    private void FillDocuments()
    {
        objams = new AMS();
        try
        {
            objams.Action = "E";
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            dt = new DataTable();
            dt1 = new DataTable();
            if (System.Web.HttpContext.Current.Session["PType"] == null)
            {
                dt = AMServices.ViewProposalMaster(objams);
                if (dt.Rows.Count > 0)
                {
                    Status = Convert.ToInt32(dt.Rows[0]["INTSTATUS"].ToString());
                }  
            }          
        }
        catch (Exception)
        {         
        }
        finally { objams = null; }

    }
    public void FinanceYear()
    {
        try
        {
            DateTime dateTime;
            dateTime = System.DateTime.Now;

            int yrfull = (dateTime.Month >= 4 ? dateTime.Year + 1 : dateTime.Year);
            //int curfny = yrfull - 1;

            txtFinYear.Text = "FY:" + Convert.ToString(yrfull - 3 + "-" + Convert.ToString(Convert.ToInt32(yrfull - 2)).Substring(2, 2));
            txtFinYear1.Text = "FY:" + Convert.ToString(yrfull - 2 + "-" + Convert.ToString(Convert.ToInt32(yrfull - 1)).Substring(2, 2));
            txtFinYear2.Text = "FY:" + Convert.ToString(yrfull - 1 + "-" + yrfull.ToString().Substring(2, 2));

            GrdFinanace.Columns[3].HeaderText = txtFinYear.Text;
            GrdFinanace.Columns[4].HeaderText = txtFinYear1.Text;
            GrdFinanace.Columns[5].HeaderText = txtFinYear2.Text;

        }
        catch (Exception m)
        { }

    }


    private void EnableDisableControl(bool val)
    {
        txtCompany.Enabled = val;
        txtTurnover1.Enabled = val;
        txtTurnover2.Enabled = val;
        txtTurnover3.Enabled = val;
        txtProTax1.Enabled = val;
        txtProTax2.Enabled = val;
        txtProTax3.Enabled = val;
        txtNet1.Enabled = val;
        txtNet2.Enabled = val;
        txtNet3.Enabled = val;
        txtUsrRemark.Enabled = val;
        btnAddMore.Enabled = val;
    }

    private void ClearAll()
    {
        txtCompany.Text = string.Empty;
        txtTurnover1.Text = string.Empty;
        txtTurnover2.Text = string.Empty;
        txtTurnover3.Text = string.Empty;
        txtProTax1.Text = string.Empty;
        txtProTax2.Text = string.Empty;
        txtProTax3.Text = string.Empty;
        txtNet1.Text = string.Empty;
        txtNet2.Text = string.Empty;
        txtNet3.Text = string.Empty;
        //lnkUFName.Text = string.Empty;
        txtUsrRemark.Text = string.Empty;
        btnAddMore.Text = "Add More";
        rowIndex = -1;
    }

    private void FillDetails(string ACTION)
    {
        objams = new AMS();
        try
        {
            objams.Action = ACTION;
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            dt = new DataTable();
            dt1 = new DataTable();
            dt2 = new DataTable();
            dt3 = new DataTable();
            if (System.Web.HttpContext.Current.Session["PType"] == null)
            {
                dt = AMServices.ViewFinace(objams);  //action= E

                objams.Action = "VT";  // view Type
                dt3 = AMServices.ViewFinace(objams);
                if (intType == 3 || intType == 4)
                {
                    objams.Action = "FR";
                    dt1 = AMServices.ViewFinace(objams);

                    objams.Action = "PN"; //Get Project Name
                    dt2 = AMServices.ViewFinace(objams);
                }
            }
            else if (Session["PType"].ToString() == "2")
            {
                dt = AMServices.ViewSWPFinace(objams);
            }

            if (intType == 3 || intType == 4)
            {
                if (dt1.Rows.Count > 0)
                {
                    DataView dv1 = new DataView(dt1);
                    dv1.RowFilter = "intCreatedBy=3";  //CMD

                    DataView dv2 = new DataView(dt1);
                    dv2.RowFilter = "intCreatedBy=4"; // GM

                    RptCMDRemark.DataSource = dv1;
                    RptCMDRemark.DataBind();

                    if (dv2.Count > 0)
                    {
                        RptGMRemark.DataSource = dv2;
                        RptGMRemark.DataBind();
                    }

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
                            txtRemark.Text = dv1[dv1.Count-1][2].ToString();
                            hdnRemarkID.Value = dv1[dv1.Count-1][4].ToString();
                        }
                    }
                    else if (intType == 4) //GM
                    {
                        if (dv2.Count > 0)
                        {
                            txtRemark.Text = dv2[dv2.Count-1][2].ToString();
                            hdnRemarkID.Value = dv2[dv2.Count - 1][4].ToString();
                        }
                    }
                }
            }   

            if (dt.Rows.Count > 0)
            {
               
                objFinance = new List<Finance>();
                foreach (DataRow dr in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        GrdFinanace.Columns[3].HeaderText = dt.Columns[3].ColumnName.ToString(); 
                        GrdFinanace.Columns[4].HeaderText = dt.Columns[4].ColumnName.ToString();
                        GrdFinanace.Columns[5].HeaderText = dt.Columns[5].ColumnName.ToString();
                        txtFinYear.Text = dt.Columns[3].ColumnName.ToString();
                        txtFinYear1.Text = dt.Columns[4].ColumnName.ToString();
                        txtFinYear2.Text = dt.Columns[5].ColumnName.ToString(); 
                    }
                    objFin = new Finance();
                    objFin.FinanceId = Convert.ToInt32(dr[0]);
                    objFin.ComapnyName = Convert.ToString(dr[1]);
                    objFin.Particulars = Convert.ToString(dr[2]);
                    objFin.FinYear1 = Convert.ToString(dr[3]);
                    objFin.FinYear2 = Convert.ToString(dr[4]);
                    objFin.FinYear3 = Convert.ToString(dr[5]);
                    objFin.Remark = Convert.ToString(dr[6]);
                    objFin.FinDoc = Convert.ToString(dr[7]);
                    objFinance.Add(objFin);
                }

                GrdFinanace.DataSource = objFinance;
                GrdFinanace.DataBind();
            }

            btnSubmit.CommandArgument = dt3.Rows[0]["PTYPE"].ToString();
            
            if (System.Web.HttpContext.Current.Session["PType"] == null)
            {
                if (intType == 3)
                {
                    btnSubmit.Text = "Next";
                    btnReset.Text = "Cancel";
                }
                else
                {
                    this.btnReset.Text = "Cancel";
                    this.btnSubmit.Text = "Update";
                }               
            }
            else if (Session["PType"].ToString() == "2")
            {
                this.btnReset.Text = "Reset";
                this.btnSubmit.Text = "Next";
            }
        }
        catch (Exception)
        {
            //GrdFinanace.DataSource = dt;
            //GrdFinanace.DataBind();
            this.btnReset.Text = "Reset";
            this.btnSubmit.Text = "Next";
            btnSubmit.CommandArgument = "2";
            ClearAll();
        }
        finally { objams = null; }

    }

    private List<Finance> ConvertToList()
    {
        objFinance = new List<Finance>();

        foreach (GridViewRow rw in this.GrdFinanace.Rows)
        {
            objFin = new Finance();
            objFin.SlNo = Convert.ToInt32(rw.RowIndex + 1);
            objFin.KeyId = objFinance.Count + 1;
            objFin.FinanceId = Convert.ToInt32(GrdFinanace.DataKeys[rw.RowIndex].Values[0].ToString());
            objFin.ComapnyName = GrdFinanace.DataKeys[rw.RowIndex].Values[1].ToString();
            objFin.Particulars = rw.Cells[2].Text;
            objFin.FinYear1 = rw.Cells[3].Text;
            objFin.FinYear2 = rw.Cells[4].Text;
            objFin.FinYear3 = rw.Cells[5].Text;
            objFin.Remark = rw.Cells[6].Text.Replace("&nbsp;", "");
            HiddenField hdFinDoc = rw.FindControl("hdnFinDoc") as HiddenField;
            objFin.FinDoc = hdFinDoc.Value;
            objFinance.Add(objFin);
        }
        return objFinance;
    }

    private void FillGridView(List<Finance> objDetails)
    {
        GrdFinanace.DataSource = objDetails;
        GrdFinanace.DataBind();
        if (GrdFinanace.Rows.Count > 0)
            btnSubmit.Enabled = true;
        else
            btnSubmit.Enabled = false;

        ClearAll();
    }

    private void AddNewRowToGrid()
    {
        objFinance = new List<Finance>();

        if (GrdFinanace.Rows.Count > 0)
        {
            ConvertToList();
        }

        if (btnAddMore.Text == "Add More")
        {
            if (objFinance.Any(d => d.ComapnyName == txtCompany.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(btnAddMore, Page.GetType(), "", "alert('Duplicate Company Name');", true);
                txtCompany.Text = string.Empty;
                return;
            }
            else
            {
                objFin = new Finance();
                objFin.FinanceId = Convert.ToInt32(hdnFY1.Value);
                objFin.ComapnyName = txtCompany.Text;
                objFin.Particulars = "TurnOver";
                objFin.FinYear1 = txtTurnover1.Text;
                objFin.FinYear2 = txtTurnover2.Text;
                objFin.FinYear3 = txtTurnover3.Text;
                objFin.SlNo = objFinance.Count + 1;
                objFin.Remark = txtUsrRemark.Text.Trim();
                //Guid newID = Guid.NewGuid();
                //if (FinDoc.HasFile)
                //{
                //    if (FinDoc.PostedFile.ContentLength > 4096000)
                //    {
                //        string strmsg11 = "<script>alert('The file has to be less than 4MB!')</script>";
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg11, false);
                //        return;
                //    }
                //    if (Path.GetExtension(FinDoc.FileName) != ".pdf")
                //    {
                //        string strmsg = "<script>alert('Only .pdf file accepted!')</script>";
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg, false);
                //        return;
                //    }
                //    string retmsg = string.Empty;
                //    string path = Server.MapPath("~/SingleWindow/FinDoc/" + Request.QueryString["ID"].ToString());
                   
                //    string ext = System.IO.Path.GetExtension(FinDoc.FileName);
                //    string FileNewName = newID + ext;

                //    if (!Directory.Exists(path))
                //    {
                //        Directory.CreateDirectory(path);
                //    }
                //    FinDoc.SaveAs(path + "/" + FileNewName);
                //    objFin.FinDoc = path;
                //    objFin.FinDoc = FileNewName;
                //}
                objFinance.Add(objFin);

                objFin = new Finance();
                objFin.FinanceId = Convert.ToInt32(hdnFY2.Value);
                objFin.ComapnyName = txtCompany.Text;
                objFin.Particulars = "Profit after Tax";
                objFin.FinYear1 = txtProTax1.Text;
                objFin.FinYear2 = txtProTax2.Text;
                objFin.FinYear3 = txtProTax3.Text;
                objFin.SlNo = objFinance.Count + 1;
                objFin.Remark = txtUsrRemark.Text.Trim();
                //if (FinDoc.HasFile)
                //{
                //    if (FinDoc.PostedFile.ContentLength > 4096000)
                //    {
                //        string strmsg11 = "<script>alert('The file has to be less than 4MB!')</script>";
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg11, false);
                //        return;
                //    }
                //    if (Path.GetExtension(FinDoc.FileName) != ".pdf")
                //    {
                //        string strmsg = "<script>alert('Only .pdf file accepted!')</script>";
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg, false);
                //        return;
                //    }
                //    string retmsg = string.Empty;
                //    string path = Server.MapPath("~/SingleWindow/FinDoc/" + Request.QueryString["ID"].ToString());
                   
                //    string ext = System.IO.Path.GetExtension(FinDoc.FileName);
                //    string FileNewName = newID + ext;

                //    if (!Directory.Exists(path))
                //    {
                //        Directory.CreateDirectory(path);
                //    }
                //    FinDoc.SaveAs(path + "/" + FileNewName);
                //    objFin.FinDoc = path;
                //    objFin.FinDoc = FileNewName;

                //}
                objFinance.Add(objFin);

                objFin = new Finance();
                objFin.FinanceId = Convert.ToInt32(hdnFY3.Value);
                objFin.ComapnyName = txtCompany.Text;
                objFin.Particulars = "Net worth";
                objFin.FinYear1 = txtNet1.Text;
                objFin.FinYear2 = txtNet2.Text;
                objFin.FinYear3 = txtNet3.Text;
                objFin.SlNo = objFinance.Count + 1;
                objFin.Remark = txtUsrRemark.Text.Trim();
                //if (FinDoc.HasFile)
                //{
                //    if (FinDoc.PostedFile.ContentLength > 4096000)
                //    {
                //        string strmsg11 = "<script>alert('The file has to be less than 4MB!')</script>";
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg11, false);
                //        return;
                //    }
                //    if (Path.GetExtension(FinDoc.FileName) != ".pdf")
                //    {
                //        string strmsg = "<script>alert('Only .pdf file accepted!')</script>";
                //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg, false);
                //        return;
                //    }
                //    string retmsg = string.Empty;
                //    string path = Server.MapPath("~/SingleWindow/FinDoc/" + Request.QueryString["ID"].ToString());
                 
                //    string ext = System.IO.Path.GetExtension(FinDoc.FileName);
                //    string FileNewName = newID + ext;

                //    if (!Directory.Exists(path))
                //    {
                //        Directory.CreateDirectory(path);
                //    }
                //    FinDoc.SaveAs(path + "/" + FileNewName);
                //    objFin.FinDoc = path;
                //    objFin.FinDoc = FileNewName;
                // }
                objFinance.Add(objFin);
            }
        }
        else if (btnAddMore.Text == "Update")
        {
            if (objFinance.Any(d => d.ComapnyName == txtCompany.Text.Trim() && d.KeyId != Convert.ToInt32(hdnFY1.Value) && d.KeyId != Convert.ToInt32(hdnFY2.Value) && d.KeyId != Convert.ToInt32(hdnFY3.Value) && d.Remark != txtUsrRemark.Text.Trim()))
            {
                ScriptManager.RegisterStartupScript(btnAddMore, Page.GetType(), "", "alert('Duplicate Company Name');", true);
                txtCompany.Text = string.Empty;
                return;
            }
            else
            {
                Finance item = objFinance.Where(i => i.KeyId == Convert.ToInt32(hdnFY1.Value)).SingleOrDefault();
                Finance item1 = objFinance.Where(i => i.KeyId == Convert.ToInt32(hdnFY2.Value)).SingleOrDefault();
                Finance item2 = objFinance.Where(i => i.KeyId == Convert.ToInt32(hdnFY3.Value)).SingleOrDefault();
                if (item != null && item1 != null && item2 != null)
                {
                    item.ComapnyName = txtCompany.Text;
                    item1.ComapnyName = txtCompany.Text;
                    item2.ComapnyName = txtCompany.Text;

                    item.FinYear1 = txtTurnover1.Text;
                    item1.FinYear1 = txtProTax1.Text;
                    item2.FinYear1 = txtNet1.Text;

                    item.FinanceId = Convert.ToInt32(hdnFY1.Value);
                    item1.FinanceId = Convert.ToInt32(hdnFY2.Value);
                    item2.FinanceId = Convert.ToInt32(hdnFY3.Value);

                    item.FinYear2 = txtTurnover2.Text;
                    item1.FinYear2 = txtProTax2.Text;
                    item2.FinYear2 = txtNet2.Text;

                    item.FinYear3 = txtTurnover3.Text;
                    item1.FinYear3 = txtProTax3.Text;
                    item2.FinYear3 = txtNet3.Text;

                    item.Remark = txtUsrRemark.Text.Trim();
                    item1.Remark = txtUsrRemark.Text.Trim();
                    item2.Remark = txtUsrRemark.Text.Trim();

                    //if (FinDoc.HasFile)
                    //{
                    //    if (FinDoc.PostedFile.ContentLength > 4096000)
                    //    {
                    //        string strmsg11 = "<script>alert('The file has to be less than 4MB!')</script>";
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg11, false);
                    //        return;
                    //    }
                    //    if (Path.GetExtension(FinDoc.FileName) != ".pdf")
                    //    {
                    //        string strmsg = "<script>alert('Only .pdf file accepted!')</script>";
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Click", strmsg, false);
                    //        return;
                    //    }
                    //    string retmsg = string.Empty;
                    //    string path = Server.MapPath("~/SingleWindow/FinDoc/" + Request.QueryString["ID"].ToString());


                    //    Guid newID = Guid.NewGuid();
                    //    string FileOriginalNm = FinDoc.FileName;
                    //    string ext = System.IO.Path.GetExtension(FinDoc.FileName);
                    //    string FileNewName = newID + ext;

                    //    //================
                    //    if (!Directory.Exists(path))
                    //    {
                    //        Directory.CreateDirectory(path);
                    //    }

                    //    FinDoc.SaveAs(path + "/" + FileNewName);
                    //    if (btnAddMore.Text != "Update")
                    //    {
                    //        objFin.FinDoc = path;
                    //        objFin.FinDoc = FileNewName;
                    //    }

                    //    item.FinDoc = FileNewName;
                    //    item1.FinDoc = FileNewName;
                    //    item2.FinDoc = FileNewName;
                    //}
                    //else
                    //{
                    //    if (lnkUFName.Text != "")
                    //    {
                    //        objFin.FinDoc = lnkUFName.Text;
                    //        item.FinDoc = lnkUFName.Text;
                    //        item1.FinDoc = lnkUFName.Text;
                    //        item2.FinDoc = lnkUFName.Text;
                    //    }
                    //}

                }
            }
        }

        FillGridView(objFinance);
    }

    #endregion

    #region "Dropdown Event"

    //protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (Convert.ToInt32(ddlProject.SelectedValue) == 0)
    //    {
    //        EnableDisableControl(false);
    //        ClearAll();
    //        dt = new DataTable();
    //        GrdFinanace.DataSource = dt;
    //        GrdFinanace.DataBind();
    //        this.btnReset.Text = "Reset";
    //        this.btnSubmit.Text = "Save";
    //    }
    //    else
    //    {
    //        EnableDisableControl(true);
    //        txtCompany.Focus();
    //        FillDetails(Convert.ToInt32(ddlProject.SelectedValue));
    //    }
    //}

    #endregion


    #region "Gridview Event"

    protected void GrdFinanace_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        ClearAll();
        var index = 1;
        List<Finance> objList = new List<Finance>();
        objList = ConvertToList();
        rowIndex = Convert.ToInt32(e.RowIndex + 1);
        txtCompany.Text = GrdFinanace.DataKeys[e.RowIndex].Values[1].ToString();

        var CompanyList =
                        (
                        from x in objList
                        where x.ComapnyName == GrdFinanace.DataKeys[e.RowIndex].Values[1].ToString()
                        select new
                        {
                            SlNo = index++,
                            KeyId = x.KeyId,
                            FinanceId = x.FinanceId,
                            ComapnyName = x.ComapnyName,
                            Particulars = x.Particulars,
                            FinYear1 = x.FinYear1,
                            FinYear2 = x.FinYear2,
                            FinYear3 = x.FinYear3,
                            Remark = x.Remark,
                            FinDoc = x.FinDoc
                        }
                        ).ToList();

        if (CompanyList.Count == 3)
        {
            var item = CompanyList.First(x => x.SlNo == 1);
            var item1 = CompanyList.First(x => x.SlNo == 2);
            var item2 = CompanyList.First(x => x.SlNo == 3);

            txtTurnover1.Text = item.FinYear1;
            txtProTax1.Text = item1.FinYear1;
            txtNet1.Text = item2.FinYear1;

            hdnFY1.Value = item.KeyId.ToString();
            hdnFY2.Value = item1.KeyId.ToString();
            hdnFY3.Value = item2.KeyId.ToString();

            txtTurnover2.Text = item.FinYear2;
            txtProTax2.Text = item1.FinYear2;
            txtNet2.Text = item2.FinYear2;

            txtTurnover3.Text = item.FinYear3;
            txtProTax3.Text = item1.FinYear3;
            txtNet3.Text = item2.FinYear3;

            txtUsrRemark.Text = item.Remark.Trim();
            txtUsrRemark.Text = item1.Remark.Trim();
            txtUsrRemark.Text = item2.Remark.Trim();

            txtFinYear.Text = GrdFinanace.Columns[3].HeaderText;
            txtFinYear1.Text = GrdFinanace.Columns[4].HeaderText;
            txtFinYear2.Text = GrdFinanace.Columns[5].HeaderText;
            //lnkUFName.Text = item.FinDoc;
            //lnkUFName.Text = item1.FinDoc;
            //lnkUFName.Text = item2.FinDoc;
            //lnkUFName.NavigateUrl = "../SingleWindow/FinDoc/" + Request.QueryString["ID"] + "/" + lnkUFName.Text;

        }

        btnAddMore.Text = "Update";
    }

    protected void GrdFinanace_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        List<Finance> objList = new List<Finance>();
        objList = ConvertToList();
        objList.RemoveAll(d => d.ComapnyName == GrdFinanace.DataKeys[e.RowIndex].Values[1].ToString());
        FillGridView(objList);
        if (objList.Count == 0)
            btnAddMore.Text = "Add More";
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        int[] a = new int[2] { 1, 2 };
        for (int i = GrdFinanace.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = GrdFinanace.Rows[i];
            GridViewRow previousRow = GrdFinanace.Rows[i - 1];
            foreach (var j in a)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                        }
                        if (j == 1)
                        {
                            if (row.Cells[6].RowSpan == 0)
                            {
                                previousRow.Cells[6].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[6].RowSpan = row.Cells[6].RowSpan + 1;
                            }
                            row.Cells[6].Visible = false;

                            if (row.Cells[7].RowSpan == 0)
                            {
                                previousRow.Cells[7].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[7].RowSpan = row.Cells[7].RowSpan + 1;
                            }
                            row.Cells[7].Visible = false;

                            if (row.Cells[8].RowSpan == 0)
                            {
                                previousRow.Cells[8].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[8].RowSpan = row.Cells[8].RowSpan + 1;
                            }
                            row.Cells[8].Visible = false;
                        }

                        row.Cells[j].Visible = false;
                    }
                }
            }
        }
    }

    #endregion

    #region "Button Event"

    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (GrdFinanace.Rows.Count == 0)
        {
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyScript", "alert('Please add financial performance of company to save');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('Please add financial performance of company to save ');", true);
        }
        try
        {
            objams = new AMS();
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            objams.XmlData = CommonHelper.SerializeToXMLString(ConvertToList());
          
            objams.FinancialYear = txtFinYear.Text;
            objams.FinancialYear1 = txtFinYear1.Text;
            objams.FinancialYear2 = txtFinYear2.Text;
            if (txtRemark.Visible == true)
            {
                objams.Remark = txtRemark.Text.Trim() == "" ? "No Comments" : txtRemark.Text.Trim(); ;
                objams.intRemarkID = hdnRemarkID.Value == "" ? 0 : Convert.ToInt32(hdnRemarkID.Value);
                intType = AMServices.GetOfficersType(Convert.ToInt32(Session["Userid"]));
                objams.CreatedBy = intType;
            }
            else
            {
                objams.CreatedBy = Convert.ToInt16(Session["UserId"]);
            }
            if (btnSubmit.CommandArgument == "1")
                objams.Action = "U";
            else if (btnSubmit.CommandArgument == "2")
                objams.Action = "A";
         
            strVal = AMServices.AddFinanceMaster(objams);
            if (strVal == "2")
            {
                ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Updated Sucessfully.');location.href='FinancialDocumentAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
            }
            else 
            {
                ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Saved Sucessfully.');location.href='FinancialDocumentAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
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
        Response.Redirect("FinancialPerformanceAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "");
    }

    #endregion

    protected void GrdFinanace_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlinkFinDoc = (HyperLink)e.Row.FindControl("hlDoc");
            HiddenField hdhDoc = (HiddenField)e.Row.FindControl("hdnFinDoc");
            string strDocs = hdhDoc.Value;
            if (hdhDoc.Value != "")
            {
                hlinkFinDoc.NavigateUrl = "../SingleWindow/FinDoc/" + Request.QueryString["ID"] + "/" + strDocs;
            }
            else
            {
                hlinkFinDoc.ImageUrl = "";
            }
        }

    }
}

public class Finance
{
    public int SlNo { get; set; }
    public int KeyId { get; set; }
    public int FinanceId { get; set; }
    public string ComapnyName { get; set; }
    public string Remark { get; set; }
    public string Particulars { get; set; }
    public string FinYear1 { get; set; }
    public string FinYear2 { get; set; }
    public string FinYear3 { get; set; }
    public string FinDoc { get; set; }
}
