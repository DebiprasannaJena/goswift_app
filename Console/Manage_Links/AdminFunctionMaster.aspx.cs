/********************************************************************************************************************
' File Name             :   AdminFunctionMaster.aspx.cs
' Description           :   To Add/edit/activate/inactivate Function Master
' Created by            :   Subhasis Kumar dash
' Created On            :   19-Jul-2010
' Modification History  :
'                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
'                            1                          29-sept-2010         Biswaranjan                    Gridview paging
'                            2                          01-oct-2010          Biswaranjan                    To set the default buttton of the page.
'                            3                          11-oct-2010          Biswaranjan                    For bugfixing.
'                            4                          09-Nov-2010          Biswaranjan                    For Bugfixing
'                            5                          25-Jun-2012          Dilip Tripathy                 To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                        
'                            6                          02-May-2013          Dilip Tripathy                 To change the navigate url as per creent tab name.
'                            7                          14-Feb-2014          Dilip Tripathy                 To update CRUD operation as per normalised database
' Function Name         : AddFunctionmaster(),EditFunctionMaster(),ToInActivate(),fillFunction(),showActiveGrid  
' Procedures Used       : usp_Function_Manage,usp_Function_View   
' User Defined Namespace:   
' Inherited classes     :                                              
**********************************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;

using KWAdminConsole.Messages;

public partial class Admin_Manage_Links_AdminFunctionMaster : System.Web.UI.Page
{
    #region variable Declaration
    ////CommonDLL objcmndll = new CommonDLL();
    public string strName, strbtntext;
    int returnvalue = 0;
    string strmsg;
    IList<FunctionMaster> lstFunction;
    IList<FunctionMaster> arrListFuncId;
    public int RecCount;
    AdminAppService ObjAdminBal = new AdminAppService();
    FunctionMaster objFunctionMaster = new FunctionMaster();
    #endregion

    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
        }
        if (Request.QueryString["att"] != null)
        {
            string strAtt = CommonFunction.DecryptData(Request.QueryString["att"].ToString());
        }
        //Code Added By : Dilip Kumar Tripathy on dated 10-May-2013
        //Purpose : To clear the browser cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
        //objAdmin = objKwantify.CreateAdminConsole();
        //objLink = objAdmin.CreateLink();
        arrListFuncId = ObjAdminBal.GetFunctionId(Session["UserId"].ToString());
        //objFunctionMaster = objLink.CreateFunctionMaster();
        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink = ">>" + TabFunctionDetails.ActiveTab.HeaderText;
        if (!IsPostBack)
        {

            if (Request.QueryString["pdx"] != null)
            {
                GVFunction.PageIndex = Convert.ToInt32(Request.QueryString["pdx"]);
            }
            showActiveGrid(GVFunction, 1);//For Active Functions
            showActiveGrid(GvInActive, 0);//For InActive Functions


            if (Request.QueryString["FID"] != null)
            {
                fillFunction();
                TabCreateFunction.HeaderText = "UPDATE";
                AdminConsoleNavigation.strNewLink = ">>" + "UPDATE";
                btnAdd.Text = "Update";
                strbtntext = btnAdd.Text;
                btnReset.Text = "Cancel";
                TabFunctionDetails.ActiveTabIndex = 0;
            }
            else
            {
                TabCreateFunction.HeaderText = "CREATE";
                rdNoM.Checked = true;
                btnAdd.Text = "Save";
                strbtntext = btnAdd.Text;
                btnReset.Text = "Reset";
                rdNoM.Checked = true;//Added By Biswaranjan on 4-Nov-2010 instructed by priyabat sir
            }
        }
        //Code to set the default buttton of the page.
        if (TabFunctionDetails.ActiveTabIndex == 0)
        {
            // strScript = "<script>DefaultFocus('" + btnAdd.ClientID + "');</script>"; //commented by Dilip on dated 10-Apr-2012
            btnAdd.Focus();
        }
        else if (TabFunctionDetails.ActiveTabIndex == 1)
        {
            // strScript = "<script>DefaultFocus('" + btnInActive.ClientID + "');</script>";//commented by Dilip on dated 10-Apr-2012
            btnInActive.Focus();
            //showActiveGrid(GVFunction, 1);
            //showActiveGrid(GVFunction, 0);
        }
        else if (TabFunctionDetails.ActiveTabIndex == 2)
        {
            //strScript = "<script>DefaultFocus('" + btnActive.ClientID + "');</script>";//commented by Dilip on dated 10-Apr-2012
            btnActive.Focus();

            showActiveGrid(GVFunction, 0);

        }

        //this.Page.RegisterClientScriptBlock("javascript", strScript);
        this.txtDesc.Attributes.Add("onkeyup", "return TextCounter('" + txtDesc.ClientID + "','" + lblMaxcounter.ClientID + "',200);");
        this.txtDesc.Attributes.Add("onkeydown", "return AllowEnter();");
    }
    #endregion

    #region "Button Events"
    /// <summary>
    /// To Add/edit Function Master.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["FID"] != null)
        {
            EditFunctionMaster();
            //ClearData();
        }
        else
        {
            AddFunctionmaster();
        }

    }
    /// <summary>
    /// To InActive records of function Master.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInActive_Click(object sender, EventArgs e)
    {
        ToInActivate(GVFunction, "I"); // 'I' for InActive
        showActiveGrid(GVFunction, 1); // '0' for showing Active Records
        //TabFunctionDetails.ActiveTabIndex = 2;//goes to inactive tab//Commented By Biswaranjan on 9-Nov-2010
        TabFunctionDetails.ActiveTabIndex = 1;//Added By Biswaranjan on 9-Nov-2010
        showActiveGrid(GvInActive, 0); //fill inactive records
        //fill inactive records
    }
    /// <summary>
    /// To Activate records of function Master.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnActive_Click(object sender, EventArgs e)
    {
        ToInActivate(GvInActive, "T"); // 'T' for Active
        showActiveGrid(GvInActive, 0); // '3' for showing InActive Records
        // TabFunctionDetails.ActiveTabIndex = 1;//goes to active tab//Commented By Biswaranjan on 9-Nov-2010
        TabFunctionDetails.ActiveTabIndex = 2;//Added By Biswaranjan on 9-Nov-2010
        showActiveGrid(GvInActive, 0); // fill Active Records
        AdminConsoleNavigation.strNewLink = ">>" + "ACTIVE";
    }
    /// <summary>
    /// To reset the fields
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (btnReset.Text == "Reset")
        {
            ClearData();
        }
        else
        {
            string strUrl = "AdminFunctionMaster.aspx?pdx=" + Request.QueryString["pindex"].ToString();
            ScriptManager.RegisterStartupScript(btnReset, typeof(string), "", "document.location.href='" + strUrl + "';", true);

        }
    }
    #endregion

    #region "Grid Events"
    /// <summary>
    /// To show the Serial number of Active gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// PostBackUrl="AdminFunctionMaster.aspx?FID=<%#DataBinder.Eval(Container.DataItem,"FunctionId") %>"
    protected void GVFunction_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string funcID = GVFunction.DataKeys[e.Row.RowIndex].Value.ToString();
            FunctionMaster ss = e.Row.DataItem as FunctionMaster;
            if (ss.FunctionId.ToString() == funcID)
            {

                HyperLink hypEdit = new HyperLink();
                hypEdit = (HyperLink)e.Row.FindControl("hypEdit");
                hypEdit.NavigateUrl = "AdminFunctionMaster.aspx?FID=" + CommonFunction.EncryptData(funcID) + "&pindex=" + GVFunction.PageIndex;

            }
            else
            {
                HyperLink hypEdit = new HyperLink();
                hypEdit = (HyperLink)e.Row.FindControl("hypEdit");
                hypEdit.NavigateUrl = "#";
            }
            e.Row.Cells[1].Text = Convert.ToString((GVFunction.PageSize * GVFunction.PageIndex) + (e.Row.RowIndex + 1));
        }
    }
    protected void GVFunction_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GVFunction.PageIndex = e.NewPageIndex;
        if (txtSearchText.Text == "")
        {
            showActiveGrid(GVFunction, 1);
        }
        else
        {
            int intFuncId = 0;
            if (hidFuncId.Value != "")
            {
                intFuncId = Convert.ToInt32(hidFuncId.Value);
            }

            objFunctionMaster.ActionCode = "H";
            objFunctionMaster.Description = txtSearchText.Text;
            lstFunction = ObjAdminBal.GetFunctionDetails(objFunctionMaster);
            GVFunction.DataSource = lstFunction;
            GVFunction.DataBind();
            DisplayPagingactive(lstFunction.Count);
        }



    }
    protected void GvInActive_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvInActive.PageIndex = e.NewPageIndex;
        showActiveGrid(GvInActive, 0);
    }
    /// <summary>
    /// To show the Serial number of InActive gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GvInActive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Convert.ToString((GvInActive.PageSize * GvInActive.PageIndex) + (e.Row.RowIndex + 1));
        }
    }
    #endregion

    #region "User functions"

    //protected string InnerImageUpload()
    //{
    //    string strRetrunval = null;
    //    if (btnAdd.Text == "Save")
    //    {
    //        if (FileUpload1.HasFile == true)
    //        {
    //            string strRandom = CommonFunction.GenerateRandomNum();
    //            FileUpload1.SaveAs(Server.MapPath("../images/InnerImage/" + strRandom + "_" + FileUpload1.FileName));
    //            strRetrunval = "../images/InnerImage/" + strRandom + "_" + FileUpload1.FileName;
    //            return strRetrunval;
    //        }
    //        else
    //        {
    //            strRetrunval = "";
    //            return strRetrunval;
    //        }
    //    }
    //    else
    //    {
    //        if (FileUpload1.HasFile == true)
    //        {
    //            if (hidImgUrl.Value != "")
    //            {
    //                if (File.Exists(Server.MapPath("../images/InnerImage/" + hidImgUrl.Value)))
    //                {
    //                    File.Delete(Server.MapPath("../images/InnerImage/" + hidImgUrl.Value));
    //                }
    //                string strRandom = CommonFunction.GenerateRandomNum();
    //                FileUpload1.SaveAs(Server.MapPath("../images/InnerImage/" + strRandom + "_" + FileUpload1.FileName));
    //                strRetrunval = "../images/InnerImage/" + strRandom + "_" + FileUpload1.FileName;
    //                return strRetrunval;
    //            }
    //            else
    //            {
    //                string strRandom = CommonFunction.GenerateRandomNum();
    //                FileUpload1.SaveAs(Server.MapPath("../images/InnerImage/" + strRandom + "_" + FileUpload1.FileName));
    //                strRetrunval = "../images/InnerImage/" + strRandom + "_" + FileUpload1.FileName;
    //                return strRetrunval;
    //            }
    //        }
    //        else
    //        {
    //            return hidImgUrl.Value;
    //        }
    //    }
    //}
    //protected string UploadInnerImage()
    //{
    //    CommonFunction objcomn = new CommonFunction();
    //    string strFilepath, strServerpath = null;
    //    string strRetrunval = "";
    //    try
    //    {
    //        strRetrunval = FileUpload1.FileName;
    //        strFilepath = FileUpload1.PostedFile.FileName;
    //        strServerpath = Server.MapPath("~\\images\\InnerImage");
    //        //===============Conditon for update case

    //        if (lblImage.Text != null && lblImage.Text != "")
    //        {
    //            if (File.Exists(Server.MapPath("~\\images\\InnerImage\\" + lblImage.Text)))
    //            {
    //                File.Delete(Server.MapPath("~\\images\\InnerImage\\" + lblImage.Text));
    //                returnvalue = objcomn.UploadImage(strFilepath, strServerpath, FileUpload1);
    //            }
    //        }
    //        //===============Conditon for Add Case
    //        else if (FileUpload1.FileName != "")
    //        {
    //            if (File.Exists(Server.MapPath("~\\images\\InnerImage\\" + FileUpload1.FileName)))
    //            {
    //                Response.Write("<script>alert('InnerImage Already Exist so cannot be uploaded');</script>");
    //                strRetrunval = "";
    //            }
    //            else
    //            {
    //                returnvalue = objcomn.UploadImage(strFilepath, strServerpath, FileUpload1);
    //            }
    //        }
    //        //===============================
    //        objcomn = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //    return strRetrunval;
    //}
    /// <summary>
    /// function containg the common code for ClearData()
    /// </summary>
    protected void clear1()
    {
        TabCreateFunction.HeaderText = "CREATE";
        btnAdd.Text = "Save";
        btnReset.Text = "Reset";
        txtFunName.Text = "";
        txtFileName.Text = "";
        txtDesc.Text = "";
        chkAdd.Checked = false;
        chkMng.Checked = false;
        chkView.Checked = false;
        lblMaxcounter.Text = "200";
        cbReCXml.Checked = false;
        showActiveGrid(GVFunction, 0);//For Active Functions
        rdYesM.Checked = false;
        rdNoM.Checked = true;
        rdNoF.Checked = false;
        rdYesF.Checked = true;
        txtPortlet.Text = string.Empty;
        rdYesA.Checked = true;
        rdNoA.Checked = false;
    }
    /// <summary>
    /// function Created By Biswaranjan to clear data.
    /// </summary>
    protected void ClearData()
    {
        try
        {
            if (btnAdd.Text.ToLower().Trim() == "save")
            {
                //***********************Added By Biswaranjan on 9-Nov-2010**************************
                //Purpose:Navigate to concern tab with default control set
                //***************************Begin***************************************************
                if (rdNoA.Checked == true && returnvalue != 0)//if inactive the function in add case
                {
                    TabFunctionDetails.ActiveTabIndex = 0;//goes to inactive tab
                    showActiveGrid(GvInActive, 3);//fill inactive records in the grid
                    clear1();
                }
                else
                {
                    //Response.Write("<script>document.location.href='AdminFunctionMaster.aspx'</script>");
                    clear1();

                }
                //***************************Begin***************************************************

            }
            if (btnAdd.Text.ToLower().Trim() == "update")
            {
                //***********************Added By Biswaranjan on 9-Nov-2010**************************
                //Purpose:Navigate to concern tab with default control set.
                //***************************Begin***************************************************

                if (rdNoA.Checked == true)//if inactive the function in update time then
                {
                    TabFunctionDetails.ActiveTabIndex = 2;//goes to inactive tab
                    //Code Added By Dilip Tripathy on dated 06-Jun-2013 to open the gridview with the same index what was before edit.
                    if (Request.QueryString["pindex"] != null)
                    {
                        GVFunction.PageIndex = Convert.ToInt32(Request.QueryString["pindex"]);
                    }
                    showActiveGrid(GvInActive, 3);//fill inactive records in the grid
                    clear1();
                    AdminConsoleNavigation.strNewLink = ">>" + "INACTIVE";
                }
                else
                {
                    if (Request.QueryString["pindex"] != null)
                    {
                        GVFunction.PageIndex = Convert.ToInt32(Request.QueryString["pindex"]);
                    }
                    showActiveGrid(GVFunction, 0);//fill active records
                    clear1();
                    AdminConsoleNavigation.strNewLink = ">>" + "ACTIVE";
                    TabFunctionDetails.ActiveTabIndex = 1;//go to active tab if cancel btn is clicked while updating

                }
                //***************************End***************************************************
            }

            else
            {
                clear1();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    ///  To add Values of Function Master.
    /// </summary>
    protected void AddFunctionmaster()
    {
        objFunctionMaster.ActionCode = "A";
        objFunctionMaster.FunctionId = "0";
        objFunctionMaster.FunctionName = txtFunName.Text.Trim();
        objFunctionMaster.FileName = txtFileName.Text.Trim();
        objFunctionMaster.Description = txtDesc.Text.Trim();
        if (chkAdd.Checked == true)
        {
            objFunctionMaster.FAdd = "Y";
        }
        else
        {
            objFunctionMaster.FAdd = "N";
        }
        if (chkView.Checked == true)
        {
            objFunctionMaster.FView = "Y";
        }
        else
        {
            objFunctionMaster.FView = "N";
        }
        if (chkMng.Checked == true)
        {
            objFunctionMaster.FManage = "Y";
        }
        else
        {
            objFunctionMaster.FManage = "N";
        }

        if (rdYesM.Checked == true)
        {
            objFunctionMaster.MailR = 1;
        }
        else if (rdNoM.Checked == true)
        {
            objFunctionMaster.MailR = 0;
        }

        if (rdYesF.Checked == true)
        {
            objFunctionMaster.FreeBees = 1;
        }
        else if (rdNoF.Checked == true)
        {
            objFunctionMaster.FreeBees = 0;
        }

        objFunctionMaster.PortletFile = txtPortlet.Text.Trim();
        if (rdYesA.Checked == true)
        {
            objFunctionMaster.Flag = 0;
        }
        else if (rdNoA.Checked == true)
        {
            objFunctionMaster.Flag = 3;
        }
        objFunctionMaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
        returnvalue = Convert.ToInt32(ObjAdminBal.AddFunction(objFunctionMaster));
        strmsg = StaticValues.message(returnvalue, "Function Details");
        ScriptManager.RegisterStartupScript(this.upFuncManage, typeof(string), "", "alert('" + strmsg + "');", true);
        ClearData();
    }
    /// <summary>
    ///  To Edit Values of Function Master.
    /// </summary>
    protected void EditFunctionMaster()
    {
        objFunctionMaster.ActionCode = "U";
        objFunctionMaster.FunctionId = Convert.ToString(CommonFunction.DecryptData(Request.QueryString["FID"].ToString()));
        objFunctionMaster.FunctionName = txtFunName.Text.Trim();
        objFunctionMaster.FileName = txtFileName.Text.Trim();
        objFunctionMaster.Description = txtDesc.Text.Trim();
        if (chkAdd.Checked == true)
        {
            objFunctionMaster.FAdd = "Y";
        }
        else
        {
            objFunctionMaster.FAdd = "N";
        }
        if (chkView.Checked == true)
        {
            objFunctionMaster.FView = "Y";
        }
        else
        {
            objFunctionMaster.FView = "N";
        }
        if (chkMng.Checked == true)
        {
            objFunctionMaster.FManage = "Y";
        }
        else
        {
            objFunctionMaster.FManage = "N";
        }
        if (rdYesM.Checked == true)
        {
            objFunctionMaster.MailR = 1;
        }
        else if (rdNoM.Checked == true)
        {
            objFunctionMaster.MailR = 0;
        }
        if (rdYesF.Checked == true)
        {
            objFunctionMaster.FreeBees = 1;
        }
        else if (rdNoF.Checked == true)
        {
            objFunctionMaster.FreeBees = 0;
        }
        objFunctionMaster.PortletFile = txtPortlet.Text.Trim();
        if (rdYesA.Checked == true)
        {
            objFunctionMaster.Flag = 0;
        }
        else if (rdNoA.Checked == true)
        {
            objFunctionMaster.Flag = 3;
        }
        objFunctionMaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
        returnvalue = Convert.ToInt32(ObjAdminBal.EditFunction(objFunctionMaster));
        strmsg = StaticValues.message(returnvalue, "Function Details");
        if (returnvalue == 2)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["XmlDel"] == "Y")
            {
                if (cbReCXml.Checked == true)
                {
                    Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                }
            }
        }
        clear1();
        string strUrl = "AdminFunctionMaster.aspx?pdx=" + Request.QueryString["pindex"].ToString();
        ScriptManager.RegisterStartupScript(btnAdd, typeof(string), "", "alert('" + strmsg + "');document.location.href='" + strUrl + "';", true);

    }
    /// <summary>
    ///  To Activate/Deactivate Values of Function Master.
    /// </summary>
    protected void ToInActivate(GridView grd, string Actcode)
    {
        int rcount = grd.Rows.Count;
        int strdata;
        for (int i = 0; i < rcount; i++)
        {
            if (((CheckBox)grd.Rows[i].Cells[0].FindControl("cbItem")).Checked == true)
            {
                strdata = Convert.ToInt32(grd.DataKeys[i].Values[0].ToString());
                objFunctionMaster.ActionCode = Actcode;
                objFunctionMaster.FunctionId = strdata.ToString();
                objFunctionMaster.CreatedBy = Convert.ToInt32(Session["UserId"]);
                returnvalue = Convert.ToInt32(ObjAdminBal.ActiveFunction(objFunctionMaster));
            }
        }
        strmsg = StaticValues.message(returnvalue, "Function");
        if (returnvalue == 7)
        {
            ScriptManager.RegisterStartupScript(this.btnActive, typeof(string), "", "alert('" + strmsg + "');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this.btnActive, typeof(string), "", "alert('Function Cannot be inactivated as it is linked to Primary Link.');", true);
        }

    }

    #endregion

    #region "Fill/Show Function"
    /// <summary>
    /// To Fill the Records when clicking the Edit Button.
    /// </summary>
    protected void fillFunction()
    {
        IList<FunctionMaster> objFnctList = new List<FunctionMaster>();
        objFunctionMaster.ActionCode = "E";
        objFunctionMaster.FunctionId = Convert.ToString(CommonFunction.DecryptData(Request.QueryString["FID"].ToString()));
        objFnctList = ObjAdminBal.GetAllFunction(objFunctionMaster);
        foreach (var i in objFnctList)
        {
            objFunctionMaster.FunctionId = i.FunctionId;
            txtFunName.Text = CommonFunction.GetDecodedData(i.FunctionName);
            txtFileName.Text = CommonFunction.GetDecodedData(i.FileName);
            txtDesc.Text = CommonFunction.GetDecodedData(i.Description);
            lblMaxcounter.Text = (int.Parse(lblMaxcounter.Text) - txtDesc.Text.Length).ToString();
            if (i.FAdd == "Y")
            {
                chkAdd.Checked = true;
            }
            else
            {
                chkAdd.Checked = false;
            }
            if (i.FView == "Y")
            {
                chkView.Checked = true;
            }
            else
            {
                chkView.Checked = false;
            }
            if (i.FManage == "Y")
            {
                chkMng.Checked = true;
            }
            else
            {
                chkMng.Checked = false;
            }
            cbReCXml.Checked = true;
            if (i.MailR == 1)
            {
                rdYesM.Checked = true;
            }
            else if (i.MailR == 0)
            {
                rdNoM.Checked = true;
            }
            //if (i.Notification == "Y")
            //{
            //    rbtnNoticeYes.Checked = true;
            //    rbtnNoticeNo.Checked = false;
            //}
            //else if (i.Notification == "N")
            //{
            //    rbtnNoticeNo.Checked = true;
            //    rbtnNoticeYes.Checked = false;
            //}

            if (i.FreeBees == 1)
            {
                rdYesF.Checked = true;
            }
            else if (i.FreeBees == 0)
            {
                rdNoF.Checked = true;
            }
            txtPortlet.Text = i.PortletFile;
            if (i.Flag == 0)
            {
                rdYesA.Checked = true;
            }
            else if (i.Flag == 3)
            {
                rdNoA.Checked = true;
            }
        }
    }
    /// <summary>
    /// To Fill the Grid Active/inactive both.
    /// </summary>
    /// <param name="grd"></param>
    /// <param name="flag"></param>
    /// 
    ///
    private void FillGrid(GridView grd1, int flag)
    {

    }
    protected void showActiveGrid(GridView grd, int flag)
    {

        objFunctionMaster.ActionCode = "V";
        objFunctionMaster.Flag = flag;
        lstFunction = ObjAdminBal.GetAllFunction(objFunctionMaster);
        RecCount = lstFunction.Count;
        List<FunctionMaster> objFunctionList = new List<FunctionMaster>();
        foreach (FunctionMaster objIfunc in lstFunction)
        {
            objIfunc.FunctionId = objIfunc.FunctionId;
            objIfunc.FunctionName = CommonFunction.GetDecodedData(objIfunc.FunctionName);
            objIfunc.FileName = CommonFunction.GetDecodedData(objIfunc.FileName);
            objIfunc.Description = CommonFunction.GetDecodedData(objIfunc.Description);
            objIfunc.Add = objIfunc.Add;
            objIfunc.View = objIfunc.View;
            objIfunc.Manage = objIfunc.Manage;
            objIfunc.MailR = objIfunc.MailR;
            objIfunc.FreeBees = objIfunc.FreeBees;
            objIfunc.PortletFile = objIfunc.PortletFile;
            objFunctionList.Add(objIfunc);
        }
        grd.DataSource = lstFunction;
        grd.DataBind();

        if (grd.ID == "GVFunction")
        {
            if (grd.Rows.Count > 0)
            {
                btnInActive.Visible = true;
                lbtnAll.Visible = true;
                lblPaging.Visible = true;
                DisplayPagingactive(RecCount);
            }
            else
            {
                lbtnAll.Visible = false;
                lblPaging.Visible = false;
                btnInActive.Visible = false;

            }
        }
        else if (grd.ID == "GvInActive")
        {
            if (grd.Rows.Count > 0)
            {
                LnkbtnAllin.Visible = true;
                lblpage.Visible = true;
                btnActive.Visible = true;
                DisplayPagingInactive(RecCount);
            }
            else
            {
                LnkbtnAllin.Visible = false;
                lblpage.Visible = false;
                btnActive.Visible = false;
            }
        }
    }
    #endregion

    protected void TabFunctionDetails_ActiveTabChanged(object sender, EventArgs e)
    {

        txtFunName.Text = "";
        txtFileName.Text = "";
        txtDesc.Text = "";
        chkAdd.Checked = false;
        chkView.Checked = false;
        chkMng.Checked = false;
        rdYesM.Checked = false;
        rdNoM.Checked = true;
        rdYesF.Checked = true;
        rdNoF.Checked = false;
        txtPortlet.Text = "";
        rdYesA.Checked = true;
        rdNoA.Checked = false;
        TabCreateFunction.HeaderText = "CREATE";
        btnAdd.Text = "Save";
        lblImage.Text = "";
        //lblImage.Visible = false;
        //imgInner.Visible = false;
        btnReset.Text = "Reset";
        lbtnAll.Text = "All";
        LnkbtnAllin.Text = "All";
        GVFunction.AllowPaging = true;
        GvInActive.AllowPaging = true;
        if (TabFunctionDetails.ActiveTabIndex == 1)
        {
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "document.location.href='AdminFunctionMaster.aspx';", true);
        }
    }
    //#region "Google style paging format"
    //private void DisplayPaging(int RecCount)
    //{
    //    if (GVFunction.Rows.Count > 0)
    //    {
    //        this.lblPaging.Visible = true;
    //        lbtnAll.Visible = true;
    //        lblPaging.Text = Admin.CommonFunction.CommonFunction.ShowGridPaging(GVFunction, GVFunction.PageSize, GVFunction.PageIndex, RecCount);
    //    }
    //    else
    //    {
    //        this.lblPaging.Visible = false;
    //        lbtnAll.Visible = false;
    //    }

    //}
    //#endregion
    private void DisplayPagingactive(int RecCount)//Active
    {
        if (GVFunction.Rows.Count > 0)
        {
            this.lblpage.Visible = true;
            LnkbtnAllin.Visible = true;

            lblPaging.Text = Admin.CommonFunction.CommonFunction.ShowGridPaging(GVFunction, GVFunction.PageSize, GVFunction.PageIndex, RecCount);
            if (GVFunction.PageIndex + 1 == GVFunction.PageCount)
            {
                this.lblPaging.Text = " " + GVFunction.Rows[0].Cells[1].Text + " - " + RecCount + " Of " + RecCount;
            }
            else
            {
                int TotRecs;
                TotRecs = Convert.ToInt32(GVFunction.Rows[0].Cells[1].Text) + Convert.ToInt32(GVFunction.PageSize - 1);
                this.lblPaging.Text = " " + GVFunction.Rows[0].Cells[1].Text + " - " + TotRecs + " Of " + RecCount;
            }
        }
        else
        {
            this.lblpage.Visible = false;
            LnkbtnAllin.Visible = false;
        }

    }

    protected void lbtnAll_Click(object sender, System.EventArgs e)//active
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            this.GVFunction.PageIndex = 0;
            GVFunction.AllowPaging = false;
            TabFunctionDetails.ActiveTabIndex = 1;
            if (txtSearchText.Text == "")
            {
                showActiveGrid(GVFunction, 1);

                if (GVFunction.Rows.Count > 0)
                {
                    this.lblPaging.Text = "1-" + GVFunction.Rows.Count.ToString() + " Of " + GVFunction.Rows.Count.ToString();
                }
            }
            else
            {
                int intFuncId = 0;
                if (hidFuncId.Value != "")
                {
                    intFuncId = Convert.ToInt32(hidFuncId.Value);
                }

                objFunctionMaster.ActionCode = "H";
                objFunctionMaster.Description = txtSearchText.Text;
                lstFunction = ObjAdminBal.GetFunctionDetails(objFunctionMaster);
                GVFunction.DataSource = lstFunction;
                GVFunction.DataBind();
                if (GVFunction.Rows.Count > 0)
                {
                    this.lblPaging.Text = "1-" + GVFunction.Rows.Count.ToString() + " Of " + lstFunction.Count;
                }
            }

        }
        else
        {
            lbtnAll.Text = "All";
            GVFunction.AllowPaging = true;
            TabFunctionDetails.ActiveTabIndex = 1;
            if (txtSearchText.Text == "")
            {
                showActiveGrid(GVFunction, 1);
            }
            else
            {
                int intFuncId = 0;
                if (hidFuncId.Value != "")
                {
                    intFuncId = Convert.ToInt32(hidFuncId.Value);
                }

                objFunctionMaster.ActionCode = "H";
                objFunctionMaster.Description = txtSearchText.Text;
                lstFunction = ObjAdminBal.GetFunctionDetails(objFunctionMaster);
                GVFunction.DataSource = lstFunction;
                GVFunction.DataBind();
                DisplayPagingactive(lstFunction.Count);
            }
        }


    }
    private void DisplayPagingInactive(int RecCount)//Inactive
    {
        if (GvInActive.Rows.Count > 0)
        {
            this.lblpage.Visible = true;
            LnkbtnAllin.Visible = true;

            lblpage.Text = Admin.CommonFunction.CommonFunction.ShowGridPaging(GvInActive, GvInActive.PageSize, GvInActive.PageIndex, RecCount);
            if (GvInActive.PageIndex + 1 == GvInActive.PageCount)
            {
                this.lblpage.Text = " " + GvInActive.Rows[0].Cells[1].Text + " - " + RecCount + " Of " + RecCount;
            }
            else
            {
                int TotRecs;
                TotRecs = Convert.ToInt32(GvInActive.Rows[0].Cells[1].Text) + Convert.ToInt32(GvInActive.PageSize - 1);
                this.lblpage.Text = " " + GvInActive.Rows[0].Cells[1].Text + " - " + TotRecs + " Of " + RecCount;
            }
        }
        else
        {
            this.lblpage.Visible = false;
            LnkbtnAllin.Visible = false;
        }

    }

    protected void LnkbtnAllin_Click(object sender, EventArgs e)//Inactive
    {
        if (LnkbtnAllin.Text == "All")
        {
            LnkbtnAllin.Text = "Paging";
            this.GvInActive.PageIndex = 0;
            GvInActive.AllowPaging = false;
            TabFunctionDetails.ActiveTabIndex = 2;
            showActiveGrid(GvInActive, 0);
            if (GvInActive.Rows.Count > 0)
            {
                lblpage.Text = "1-" + GvInActive.Rows.Count.ToString() + " Of " + GvInActive.Rows.Count.ToString();
            }
        }
        else
        {
            LnkbtnAllin.Text = "All";
            GvInActive.AllowPaging = true;
            TabFunctionDetails.ActiveTabIndex = 2;
            showActiveGrid(GvInActive, 0);
        }

    }



    protected void imgBtnDel_Click(object sender, EventArgs e)
    {

        if (File.Exists(Server.MapPath(hidImgUrl.Value)))
        {
            File.Delete(Server.MapPath(hidImgUrl.Value));
            ObjAdminBal.DeleteFuncImage("D", Convert.ToInt32(CommonFunction.DecryptData(Request.QueryString["FID"].ToString())));
            hidImgUrl.Value = "";
            imgBtnDel.Visible = false;
            lblImage.Visible = false;
            imgInner.Visible = false;
            ScriptManager.RegisterStartupScript(imgBtnDel, typeof(string), "", "alert('Function Image Deleted Successfully.');", true);

        }
        else
        {
            ObjAdminBal.DeleteFuncImage("D", Convert.ToInt32(CommonFunction.DecryptData(Request.QueryString["FID"].ToString())));
            hidImgUrl.Value = "";
            imgBtnDel.Visible = false;
            lblImage.Visible = false;
            imgInner.Visible = false;
            ScriptManager.RegisterStartupScript(imgBtnDel, typeof(string), "", "alert('Function Image Deleted Successfully.');", true);
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        int intFuncId = 0;
        if (hidFuncId.Value != "")
        {
            intFuncId = Convert.ToInt32(hidFuncId.Value);
        }

        objFunctionMaster.ActionCode = "F";
        objFunctionMaster.FunctionId = intFuncId.ToString();
        lstFunction = ObjAdminBal.GetFunctionDetails(objFunctionMaster);
        GVFunction.DataSource = lstFunction;
        GVFunction.DataBind();
        if (GVFunction.Rows.Count > 0)
        {
            btnInActive.Visible = true;
            lbtnAll.Visible = true;
            lblPaging.Visible = true;
            DisplayPagingactive(GVFunction.Rows.Count);
        }
        else
        {
            lbtnAll.Visible = false;
            lblPaging.Visible = false;
            btnInActive.Visible = false;

        }
        txtSearchText.Focus();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        int intFuncId = 0;
        if (hidFuncId.Value != "")
        {
            intFuncId = Convert.ToInt32(hidFuncId.Value);
        }

        objFunctionMaster.ActionCode = "G";
        lstFunction = ObjAdminBal.GetFunctionDetails(objFunctionMaster);
        GVFunction.DataSource = lstFunction;
        GVFunction.DataBind();
        if (GVFunction.Rows.Count > 0)
        {
            btnInActive.Visible = true;
            lbtnAll.Visible = true;
            lblPaging.Visible = true;
            DisplayPagingactive(GVFunction.Rows.Count);
        }
        else
        {
            lbtnAll.Visible = false;
            lblPaging.Visible = false;
            btnInActive.Visible = false;

        }
        txtSearchText.Focus();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        int intFuncId = 0;
        if (hidFuncId.Value != "")
        {
            intFuncId = Convert.ToInt32(hidFuncId.Value);
        }

        objFunctionMaster.ActionCode = "H";
        objFunctionMaster.Description = txtSearchText.Text;
        lstFunction = ObjAdminBal.GetFunctionDetails(objFunctionMaster);
        GVFunction.DataSource = lstFunction;
        GVFunction.DataBind();
        if (GVFunction.Rows.Count > 0)
        {
            btnInActive.Visible = true;
            lbtnAll.Visible = true;
            lblPaging.Visible = true;
            DisplayPagingactive(lstFunction.Count);
        }
        else
        {
            lbtnAll.Visible = false;
            lblPaging.Visible = false;
            btnInActive.Visible = false;

        }

        this.txtSearchText.Focus();
        ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "setCursorPositionToEnd();", true);
    }
}
