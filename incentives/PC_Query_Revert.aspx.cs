//******************************************************************************************************************
// File Name             :   PC_Query_Revert.aspx.cs
// Description           :   Show the details of Query & Take Action as Revert/Extent for PC
// Created by            :   Pranay Kumar
// Created on            :   25-OCT-2017
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAcessLayer.Service;
using System.Web.Services;
using BusinessLogicLayer.Proposal;
using System.IO;
using BusinessLogicLayer.Service;
using System.Web.UI.HtmlControls;
using Ionic.Zip;
using BusinessLogicLayer.Incentive;
using EntityLayer.Incentive;


public partial class incentives_PC_Query_Revert : SessionCheck
{
    # region variables Declaration
    #region Variables
    IncentiveMasterBusinessLayer ObjIMB = new IncentiveMasterBusinessLayer();
    QueryMgntDtls objQueryMgtDtls = new QueryMgntDtls();
    //ProposalDet objProposal = new ProposalDet();
    //ProjectInfo objproject = new ProjectInfo();
    static string Str_UsrName = "";
    string strRetVal = "";
    int intRetVal = 0;
    #endregion
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["UserId"] != null)
            {
                Str_UsrName = Session["UserId"].ToString();
                BindQueryTable();
                ShowHideButton();
                LoadGrid();
            }
            else
            {
                Response.Redirect("~/inestorlogin.aspx");
            }
        }

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        DataTable dt1 = null;
        string strFilename = "";
        try
        {

            dt1 = CreateDataTable();
            objQueryMgtDtls.strAction = "A";
            objQueryMgtDtls.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
            objQueryMgtDtls.strApplicationNum = Convert.ToString(Request.QueryString["AppNo"]);
            objQueryMgtDtls.intStatus = 6;
            objQueryMgtDtls.strRemarks = txtA1.Text;
            foreach (GridViewRow rr in grdRevertQuery.Rows)
            {
                DataRow dr = dt1.NewRow();
                TextBox txtFileContent = (TextBox)rr.FindControl("txtFileContent");
                Label lblFileName = (Label)rr.FindControl("lblFileName");
                dr["SLNO"] = "1";
                dr["FileContent"] = txtFileContent.Text;
                dr["FileName"] = lblFileName.Text;
                dt1.Rows.Add(dr);
                if (lblFileName.Text != "")
                {
                    strFilename += lblFileName.Text + ",";
                }
            }
            strFilename = strFilename.TrimEnd(',');
            dt1.Rows[(dt1.Rows.Count - 1)].Delete(); //Delete last blank row
            dt1.TableName = "tblQueryDtls";
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                dt1.WriteXml(sw);
                objQueryMgtDtls.strXML = sw.ToString();
            }
            objQueryMgtDtls.strFileName = strFilename;
            if (Request.QueryString["UnitCat"] != "")
            {
                if (Request.QueryString["UnitCat"] == "39") //for PC LARGE
                {
                    strRetVal = ObjIMB.PCLargeRaiseQuery(objQueryMgtDtls);
                }
                else
                {
                    strRetVal = ObjIMB.PCMSMERaiseQuery(objQueryMgtDtls);
                }
            }
            

            if (strRetVal == "2")
            {
                string rawURL = "../pcViewPage.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Query Responded Successfully.', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false);
                //FOR SENDING MAIL & SMS
                CommonHelperCls comm = new CommonHelperCls();
                List<QueryMgntDtls> objQueryList = new List<QueryMgntDtls>();
                QueryMgntDtls objQuery = new QueryMgntDtls();

                objQuery.strAction = "S";
                objQuery.strApplicationNum = Convert.ToString(Request.QueryString["AppNo"]);
                objQuery.intCreatedBy = Convert.ToInt32(Session["InvestorId"]);
                if (Request.QueryString["UnitCat"] != "")
                {
                    if (Request.QueryString["UnitCat"] == "39")
                    {
                        objQueryList = ObjIMB.getPCLargeRaisedQueryDetails(objQuery).ToList();
                    }
                    else
                    {
                        objQueryList = ObjIMB.getPCMSMERaisedQueryDetails(objQuery).ToList();
                    }
                }
                
                string mobile = "";
                string smsContent = "";
                string strSubject = "";
                string strBody = "";
                string[] toEmail = new string[1];

                if (objQueryList.Count > 0)
                {
                    mobile = Convert.ToString(objQueryList[0].strMobileNo);
                    smsContent = Convert.ToString(objQueryList[0].strSMSContent);
                    toEmail[0] = Convert.ToString(objQueryList[0].strEmailID);
                    strSubject = Convert.ToString(objQueryList[0].strEmailSubject);
                    strBody = Convert.ToString(objQueryList[0].strEmailBody);
                 bool mailStatus =  comm.sendMail(strSubject, strBody, toEmail, true);
                 bool smsStatus =  comm.SendSmsNew(mobile, smsContent);

                    comm.UpdateMailSMSStaus("PCQueryRevert", mobile, toEmail[0], "Query Revert on PC", "0", "0", 0, "0", smsContent, strBody, smsStatus, mailStatus);
                }
            }
            else if (strRetVal == "4")
            { ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('Action can not be taken Successfully.')</script>;", false); }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objQueryMgtDtls = null;
        }
    }
    #region "Extend Button Click"

    protected void lbtnExtend_Click(object sender, EventArgs e)
    {
        int intAppNo = Convert.ToInt32(Request.QueryString["AppNo"]);
        string strAction = "EQ";
        if (Request.QueryString["UnitCat"] != "")
        {
            if (Request.QueryString["UnitCat"] == "39") //for PC LARGE
            {
                intRetVal = ObjIMB.PCLargeExtendDate(strAction, intAppNo);
            }
            else
            {
                intRetVal = ObjIMB.PCMSMEExtendDate(strAction, intAppNo);
            }
        }        

        if (intRetVal == 3)
        {
            string rawURL = "../pcViewPage.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('Respond Query Date Extended Successfully.', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false);
        }

    }
    #endregion
    #region Bind Query History Table"
    private void BindQueryTable()
    {
        List<QueryMgntDtls> objQueryList = new List<QueryMgntDtls>();
        QueryMgntDtls objQuery = new QueryMgntDtls();
        objQuery.strAction = "QD";
        objQuery.strApplicationNum = Convert.ToString(Request.QueryString["AppNo"]);
        if (Request.QueryString["UnitCat"] != "")
        {
            if (Request.QueryString["UnitCat"] == "39") //for PC LARGE
            {
                objQueryList = ObjIMB.getPCLargeRaisedQueryDetails(objQuery).ToList(); // FOR MAIN CONVERSATION        
            }
            else
            {
                objQueryList = ObjIMB.getPCMSMERaisedQueryDetails(objQuery).ToList(); // FOR MAIN CONVERSATION        
            }
        }   
        
        if (objQueryList.Count > 0)
        {
            string strHTMlQuery = "<div class='messagebox'>";
            for (int i = 0; i < objQueryList.Count; i++)
            {

                if (i % 2 == 0) // If Odd then bind data
                {
                    strHTMlQuery += "<div class='itemdiv dialogdiv'><div class='user'><img src='../images/user.png' alt='user img'></div>";
                }
                else
                {
                    strHTMlQuery += "<div class='itemdiv dialogdiv msgright'>";
                }
                strHTMlQuery += "<div class='body'><div class='time'><i class='ace-icon fa fa-calendar'></i>" + objQueryList[i].dtmCreatedOn + "</div>";
                strHTMlQuery += "<div class='name'><a href='#'>" + objQueryList[i].strActionToBeTakenBY + "</a> (" + objQueryList[i].strQueryDesc + ")</div>";
                strHTMlQuery += "<div class='form-sec '><div class='form-header'> <div class='pull-left'><table><tr><td>";
                if (i % 2 == 0) // If Even then bind Raised data
                {
                    strHTMlQuery += "<div class='legendColorBox blue'></div></td><td>Raised &nbsp;</td><td><div class='text-blue'>(Query Reference No : " + objQueryList[i].strQueryUnqNo + ")</div></td>";
                }
                else    // If Odd then bind Reverted data
                {
                    strHTMlQuery += "<div class='legendColorBox green'></div></td><td>Responded &nbsp;</td><td><div class='text-blue'>(Query Reference No : " + objQueryList[i].strQueryUnqNo + ")</div> </td>";
                }
                strHTMlQuery += "</tr></table></div> ";
                strHTMlQuery += "<div class='pull-right'>";
                if (objQueryList[i].strFileName == null || objQueryList[i].strFileName == "")
                {
                    strHTMlQuery += "<a href='#' class='btn btn-info btn-sm'>--</a>";
                }
                else
                {
                    strHTMlQuery += "<a target='_blank' href='./Files/QueryDocs/" + objQueryList[i].strFileName + "' class='btn btn-info btn-sm'><i class='fa fa-download'></i></a>";
                }
                strHTMlQuery += "</div><div class='clearfix'></div></div><div class='form-body'>" + objQueryList[i].strRemarks + "<div>";
                if (i % 2 != 0) // for reverted query bind add more concept
                {
                    List<QueryMgntDtls> objQueryList1 = new List<QueryMgntDtls>();
                    objQuery.strAction = "QF";
                    objQuery.intQueryId = objQueryList[i].intQueryId;
                    if (Request.QueryString["UnitCat"] != "")
                    {
                        if (Request.QueryString["UnitCat"] == "39") //for PC LARGE
                        {
                            objQueryList1 = ObjIMB.getPCLargeRaisedQueryDetails(objQuery).ToList();// FOR ADD MORE FILES AND DESCRIPTION
                        }
                        else
                        {
                            objQueryList1 = ObjIMB.getPCMSMERaisedQueryDetails(objQuery).ToList();// FOR ADD MORE FILES AND DESCRIPTION      
                        }
                    }                       
                    if (objQueryList1.Count > 0)
                    {
                        strHTMlQuery += " <table class='table table-bordered table-hover'><tr><th>File Description</th><th width='60px' >Download</th></tr>";
                        for (int j = 0; j < objQueryList1.Count; j++)
                        {
                            strHTMlQuery += "<tr><td>" + objQueryList1[j].strRemarks + "</td><td class='text-center'>";

                            if (objQueryList1[j].strFileName == null || objQueryList1[j].strFileName == "")
                            {
                                strHTMlQuery += "<a href='#' class='btn btn-info btn-sm'>--</a>";
                            }
                            else
                            {
                                strHTMlQuery += "<a target='_blank' href='./Files/QueryDocs/" + objQueryList1[j].strFileName + "' class='btn btn-info btn-sm'><i class='fa fa-download'></i></a></td></tr>";
                            }

                        }
                        strHTMlQuery += "</table>";
                    }

                }
                strHTMlQuery += "</div></div></div>";
                strHTMlQuery += "</div>";
                if (i % 2 != 0) // If Odd then bind data
                {
                    strHTMlQuery += "<div class='user'><img src='../images/user.png' alt='user img'></div>";
                }
                strHTMlQuery += "</div>";
            }
            strHTMlQuery += "</div>";
            QueryHist.InnerHtml = strHTMlQuery;
        }

    }
    private void ShowHideButton()
    {
        QueryMgntDtls objQuery = new QueryMgntDtls();
        List<QueryMgntDtls> objQueryList = new List<QueryMgntDtls>();
        try
        {
            objQuery.strAction = "SH";
            objQuery.strApplicationNum = Convert.ToString(Request.QueryString["AppNo"]);
            if (Request.QueryString["UnitCat"] != "")
            {
                if (Request.QueryString["UnitCat"] == "39") //for PC LARGE
                {
                    objQueryList = ObjIMB.getPCLargeRaisedQueryDetails(objQuery).ToList();
                }
                else
                {
                    objQueryList = ObjIMB.getPCMSMERaisedQueryDetails(objQuery).ToList();
                }
            }     
            
            if (objQueryList.Count > 0)
            {
                int intQueryStatus = Convert.ToInt32(objQueryList[0].intQueryStatus);
                int intExtendedStatus = Convert.ToInt32(objQueryList[0].intExtendedStatus);
                if (intQueryStatus == 1)//Query Raised
                {
                    dvRevert.Visible = true;
                    dvExtent.Visible = false;
                }
                else if (intQueryStatus == 0) // No Query
                {
                    dvRevert.Visible = false;
                    dvExtent.Visible = false;
                }
                else if (intQueryStatus == 2) //Expired
                {
                    dvRevert.Visible = false;
                    dvExtent.Visible = true;
                    if (intExtendedStatus == 1)
                    {
                        dvExtent.Visible = false;
                    }
                    else
                    {
                        dvExtent.Visible = true;
                    }
                }
                lblQueryStatus.Text = Convert.ToString(objQueryList[0].strStatus);
                if (lblQueryStatus.Text == "--")
                {
                    dvQueryMain.Visible = false;
                    dvQueryMain1.Visible = true;
                }
                else
                {
                    dvQueryMain1.Visible = false;
                    dvQueryMain.Visible = true;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            objQuery = null;
        }

    }
    #endregion
    #region "load Grid"
    private DataTable CreateDataTable()
    {
        DataTable Data_table = new DataTable();
        DataColumn Data_Coloumn = new DataColumn();

        Data_Coloumn = new System.Data.DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "SLNO";
        Data_table.Columns.Add(Data_Coloumn);

        Data_Coloumn = new DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "CheckStatus";
        Data_table.Columns.Add(Data_Coloumn);

        Data_Coloumn = new System.Data.DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "FileContent";
        Data_table.Columns.Add(Data_Coloumn);

        Data_Coloumn = new System.Data.DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "FileName";
        Data_table.Columns.Add(Data_Coloumn);
        return Data_table;
    }
    public void LoadGrid()
    {
        DataTable dt1 = null;
        dt1 = CreateDataTable();
        dt1.TableName = "RevertQueryDetails";
        dt1.PrimaryKey = new DataColumn[] { dt1.Columns["SLNO"] };
        DataRow dr = dt1.NewRow();
        dr["SLNO"] = 1;
        dr["CheckStatus"] = true;
        dt1.Rows.Add(dr);
        ViewState["dtbTemp"] = dt1;
        grdRevertQuery.DataSource = dt1;
        grdRevertQuery.DataBind();

    }
    #endregion
    #region "GridView Events"
    #region "GridView Row Command Events"
    #region"Grid Row Command Evt."
    protected void grdRevertQuery_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnAddMore")
        {
            string strFileContent = "";
            var parentRow = ((Button)e.CommandSource).Parent;
            if ((((TextBox)parentRow.FindControl("txtFileContent")).Text).ToString() == "")
            {
                strFileContent = "";
            }
            else
            {
                strFileContent = Convert.ToString(((TextBox)parentRow.FindControl("txtFileContent")).Text);
            }

            if ((ViewState["dtbTemp"] != null))
            {
                DataTable dtnn = new DataTable();
                Button btnAddMore = (Button)e.CommandSource;
                Sb_LoadGrdidFromTable(true, (DataTable)ViewState["dtbTemp"], (btnAddMore.Text == "Add" ? 1 : 2));
            }
        }
        else if (e.CommandName == "cmdDelete")
        {
            DataTable dtbTemp = (DataTable)ViewState["dtbTemp"];
            foreach (GridViewRow row in grdRevertQuery.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {

                    if (dtbTemp.Rows.Count > 1)
                    {
                        int intSLNO = Convert.ToInt16(((HiddenField)row.FindControl("hdfItem")).Value);
                        int intArguID = Convert.ToInt32(e.CommandArgument);
                        if (intArguID == intSLNO)
                        {
                            dtbTemp.Rows.Remove((DataRow)dtbTemp.Select("SLNO=" + intSLNO).GetValue(0));
                            string filename = ((HiddenField)row.FindControl("hdnFileName")).Value;
                            string path = "./Files/QueryDocs/" + filename;
                            string completePath = Server.MapPath(path);
                            if (System.IO.File.Exists(completePath))
                            {
                                System.IO.File.Delete(completePath);
                            }
                        }
                    }

                }
            }
            if (dtbTemp.Rows.Count == 1)
            {
                dtbTemp.Rows[0]["SLNO"] = 1;

            }
            else
            {
                for (int i = 0; i <= dtbTemp.Rows.Count - 1; i++)
                {
                    dtbTemp.Rows[i]["SLNO"] = i + 1;
                }
            }
            dtbTemp.AcceptChanges();
            Sb_LoadGrdidFromTable(false, dtbTemp, 3);
        }
    }
    #endregion
    #endregion
    #region"GridRowDatabound"
    protected void grdRevertQuery_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            try
            {
                Label lblFileName = (Label)e.Row.FindControl("lblFileName");
                HyperLink hypFiles = (HyperLink)e.Row.FindControl("hypFiles");
                if (lblFileName.Text == "")
                {
                    hypFiles.Visible = false;
                }
                else
                {
                    hypFiles.Visible = true;
                    hypFiles.NavigateUrl = "./Files/QueryDocs/" + lblFileName.Text;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
    #endregion
    #endregion
    public void Sb_LoadGrdidFromTable(bool ArgAddNew, DataTable ArgTable, int ArgSaveMode)
    {
        int intPageIndex = grdRevertQuery.PageIndex;
        if (ArgAddNew)
        {
            if (ArgSaveMode == 1) //Save Mode
            {
                ArgTable.Rows.Clear();
                int intUniqueID = 1;
                DataRow dtr = default(DataRow);

                foreach (GridViewRow parentRow in grdRevertQuery.Rows)
                {
                    string strFileContent = ((TextBox)parentRow.FindControl("txtFileContent")).Text;
                    FileUpload FileUpload1 = ((FileUpload)parentRow.FindControl("FileUpload1"));
                    string strFileName = ((FileUpload)parentRow.FindControl("FileUpload1")).FileName;
                    string strlblFileName = ((Label)parentRow.FindControl("lblFileName")).Text;
                    dtr = ArgTable.NewRow();
                    dtr["SLNO"] = intUniqueID;
                    dtr["CheckStatus"] = true;
                    dtr["FileContent"] = strFileContent;
                    string filepath = string.Format("{0:yyyy_MM_dd_hh_mm_ss_tt_}" + "_Query_Dept_" + Session["UserId"].ToString() + ".pdf", DateTime.Now);
                    if (FileUpload1.HasFile)
                    {

                        if (Path.GetExtension(FileUpload1.FileName) != ".pdf")
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Only .pdf file accepted!');", true);
                            return;
                        }
                    }
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("./Files/QueryDocs/"));
                    if (!string.IsNullOrEmpty(FileUpload1.FileName))
                    {
                        if (dir.Exists)
                        {
                            FileUpload1.SaveAs(Server.MapPath("./Files/QueryDocs/" + filepath));
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(Server.MapPath("./Files/QueryDocs/"));
                            FileUpload1.SaveAs(Server.MapPath("./Files/QueryDocs/" + filepath));
                        }
                    }
                    else { filepath = ""; }

                    if (strFileName == "")
                    {
                        dtr["FileName"] = strlblFileName;
                    }
                    else
                    {
                        dtr["FileName"] = filepath;
                    }
                    ArgTable.Rows.Add(dtr);
                    intUniqueID += 1;

                }
                if (intUniqueID == 7)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script> jAlert('More than 5 files cannot be uploaded')</script>;", false);
                    return;
                }
                dtr = ArgTable.NewRow();
                dtr["SLNO"] = intUniqueID;
                dtr["FileContent"] = "";
                dtr["FileName"] = "";
                ArgTable.Rows.Add(dtr);

            }


        }
        ViewState["dtbTemp"] = ArgTable;
        BindGrid();
        Sb_ConfigureAddUpdateToGridview();
    }
    public void Sb_ConfigureAddUpdateToGridview()
    {
        for (int i = 0; i <= grdRevertQuery.Rows.Count - 1; i++)
        {
            if (i != grdRevertQuery.Rows.Count - 1)
            {
                ((Button)grdRevertQuery.Rows[i].FindControl("BtnAdd")).Visible = false;
            }
            //((CheckBox)grdRevertQuery.Rows[i].FindControl("chkItem")).Visible = !((Button)grdRevertQuery.Rows[i].FindControl("BtnAdd")).Visible;
            ((Label)grdRevertQuery.Rows[i].FindControl("lblSlno")).Visible = !((Button)grdRevertQuery.Rows[i].FindControl("BtnAdd")).Visible;

            if (((Button)grdRevertQuery.Rows[i].FindControl("BtnAdd")).Visible == false)
            {
                ((Button)grdRevertQuery.Rows[i].FindControl("BtnAdd")).Visible = true;
                ((Button)grdRevertQuery.Rows[i].FindControl("BtnAdd")).Text = "Delete";
                // ((Button)grdRevertQuery.Rows[i].FindControl("BtnAdd")).Visible = false;
                ((Button)grdRevertQuery.Rows[i].FindControl("BtnAdd")).CommandName = "cmdDelete";
                ((Button)grdRevertQuery.Rows[i].FindControl("BtnAdd")).CssClass = "btn btn-danger btn-sm";
            }
        }

    }
    #region"GridBind"
    public void BindGrid()
    {

        if ((ViewState["dtbTemp"] != null))
        {
            DataTable dtbTemp = (DataTable)ViewState["dtbTemp"];
            if (dtbTemp.Columns.Contains("SLNO"))
            {
                grdRevertQuery.DataSource = dtbTemp;
                grdRevertQuery.DataBind();
            }
        }
    }
    #endregion
    #region "ZIP DOWNLOAD"
    protected void btnDownload_Click(object sender, EventArgs e)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName("QueryDocs");
            if (hdnFileNames.Value != "")
            {
                string[] arrFileName = hdnFileNames.Value.Split(',');
                for (int i = 0; i <= arrFileName.Count() - 1; i++)
                {
                    string FileName = "./Files/QueryDocs/" + Convert.ToString(arrFileName[i]);
                    string filePath = Server.MapPath(FileName);
                    zip.AddFile(filePath, "QueryDocs");
                }
            }
            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("QueryFiles_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            zip.Save(Response.OutputStream);
            Response.End();
        }
    }
    #endregion

}