/********************************************************************************************************************
' File Name             :   DesignationMaster.aspx.cs
' Description           :   To Add/edit/activate/inactivate Designationmaster
' Created by            :   Biswaranjan Das
' Created On            :   23-Jul-2010
' Modification History  :
'                           <CR no.>                      <Date>             <Modified by>               <Modification Summary>'                                                          
'                            1                            27-sept-2010                                   To create the functiion ClearData()
'                            2                            1-oct-2010                                     To write the code to set the default button of the page.
'                            3                            8-Nov-2010                                     To add the Condition for Checking designation existance in usermaster while deleting in delte button click event
'                            4                            9-Nov-2010                                     To add the checkduplicate condition in ClearData()
'                            5                            25-June-2012        Dilip Tripathy             To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                        
'                            6                            02-May-2013         Dilip Tripathy             To change the navigate url as per creent tab name.                         
' Function Name         : filleditcase,FillGridview(),Filleditcase(),ClearData()
' Procedures Used       :   
' User Defined Namespace:   
' Inherited classes     :                                              
**********************************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using KWAdminConsole.Messages;
public partial class Admin_Manage_User_DesignationMaster : System.Web.UI.Page
{
    #region Variable Declaration"
    int intreturnval, intdatakey = 0;

   
    public int RecCount;
    AdminAppService ObjAdminBal = new AdminAppService();
    Designation objdesignation = new Designation();
    static ArrayList locList = null;
    #endregion

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
        //objuser = objAdmin.CreateUser();
        //objdesignation = objuser.CreateDesignation();

        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink =">>" +TabadminDesignationMaster.ActiveTab.HeaderText;        
        
        if (!IsPostBack)
        {
            locList = CommonFunction.GetUserLocationId(Convert.ToInt32(Session["UserId"].ToString()));
            this.lblpage.Visible = false;
            LnkbtnAllin.Visible = false;
            FillLocationName();
            FillGridview();
            if (Request.QueryString["DesigId"] != null)
            {
                ViewState["intdatakey"] = Convert.ToInt32(CommonFunction.DecryptData(Request.QueryString["DesigId"].ToString()));
                Filleditcase(Convert.ToInt32(ViewState["intdatakey"]));
                TabCreateDesignation.HeaderText = "Update";
                btnsave.Text = "Update";
                btncancel.Text = "Cancel";
                TabadminDesignationMaster.ActiveTabIndex = 0;
                AdminConsoleNavigation.strNewLink = ">>" + "Update";
            }
            else
            {
                TabCreateDesignation.HeaderText = "Create";
                btnsave.Text = "Save";
                btncancel.Text = "Reset";
            }
            //Code to set the default button of the page
            //strscript = "<script>DefaultFocus('" + btnsave.ClientID + "');</script>"; //Code commented by Dilip Kumar Tripathy on dated 10-Apr-2012
            btnsave.Focus();//code added by Dilip Tripathy on dated 10-Apr-2012
            //Page.RegisterClientScriptBlock("javascript", strscript);
            // Page.ClientScript.RegisterStartupScript(this.GetType(), "javascript", strscript, true);//Commented by Priyabrat Routray 30the Nov 2011

        }
        btnsave.Attributes.Add("onclick", "return checkvalidation();");

    }

    #region Button Events
    protected void btnsave_Click(object sender, EventArgs e)
    {
        try
        {
            //*********************If condition for Add*************************
            if (btnsave.Text.ToLower().Trim() == "save")
            {
                objdesignation.ActionCode = "A";
                objdesignation.DesignationID = "0";
                objdesignation.Designame = txtdesignation.Text;
                objdesignation.Aliasname = txtalias.Text;
                objdesignation.UserType = ddlUsertype.SelectedValue;
                objdesignation.CreatedBy = Convert.ToInt32(Session["UserId"]);
                intreturnval = ObjAdminBal.ManageDesignation(objdesignation);
                if (intreturnval != 0)
                {
                    DataTable dtTable = new DataTable();
                    dtTable.TableName = "Designation";
                    DataColumn dcintDesignationId = new DataColumn("intDesignationId");
                    DataColumn dcintHierarchyId = new DataColumn("intHierarchyId");
                    DataColumn dcintCreatedBy = new DataColumn("intCreatedBy");
                    dtTable.Columns.Add(dcintDesignationId);
                    dtTable.Columns.Add(dcintHierarchyId);
                    dtTable.Columns.Add(dcintCreatedBy);
                    string[] locId = hidUserLoc.Value.ToString().Split(',');
                    ArrayList alLocId = new ArrayList();
                    for (int i = 1; i < locId.Length; i++)
                    {
                        DataRow drRow = dtTable.NewRow();
                        drRow["intDesignationId"] = intreturnval;
                        if (locId[i].ToString() == "All")
                        {
                            drRow["intHierarchyId"] = "-1";
                        }
                        else
                        {
                            drRow["intHierarchyId"] = locId[i].ToString();
                        }
                        drRow["intCreatedBy"] = Session["userid"];
                        dtTable.Rows.Add(drRow);
                    }
                    string strXMLResult = string.Empty;
                    if (dtTable.Rows.Count > 0)
                    {
                        StringWriter sw = new StringWriter();
                        dtTable.WriteXml(sw);
                        objdesignation.LocId = sw.ToString();
                        sw.Close();
                        sw.Dispose();

                    }
                    objdesignation.ActionCode = "T";
                    objdesignation.DesignationID = intreturnval.ToString();
                    intreturnval = Convert.ToInt32(ObjAdminBal.ManageDesignation(objdesignation));
                }
                
               
            }
            //************************Else Condition for Edit*****************************
            else
            {
                objdesignation.ActionCode = "U";
                objdesignation.DesignationID = Convert.ToString(ViewState["intdatakey"]); ;
                objdesignation.Designame = txtdesignation.Text;
                objdesignation.Aliasname = txtalias.Text;
                objdesignation.UserType = ddlUsertype.SelectedValue;
                objdesignation.CreatedBy = Convert.ToInt32(Session["UserId"]);
                intreturnval = ObjAdminBal.ManageDesignation(objdesignation);
                if (intreturnval != 0)
                {
                    DataTable dtTable = new DataTable();
                    dtTable.TableName = "Designation";
                    DataColumn dcintDesignationId = new DataColumn("intDesignationId");
                    DataColumn dcintHierarchyId = new DataColumn("intHierarchyId");
                    DataColumn dcintCreatedBy = new DataColumn("intCreatedBy");
                    dtTable.Columns.Add(dcintDesignationId);
                    dtTable.Columns.Add(dcintHierarchyId);
                    dtTable.Columns.Add(dcintCreatedBy);
                    string[] locId = hidUserLoc.Value.ToString().Split(',');
                    ArrayList alLocId = new ArrayList();
                    for (int i = 0; i < locId.Length; i++)
                    {
                        if (locId[i].ToString() != "")
                        {
                            if (lbLocation.Items.FindByValue(locId[i].ToString()).Selected == true)
                            {
                                DataRow drRow = dtTable.NewRow();
                                drRow["intDesignationId"] = Convert.ToInt32(ViewState["intdatakey"]); ;
                                if (locId[i].ToString() == "All")
                                {
                                    drRow["intHierarchyId"] = "-1";
                                }
                                else
                                {
                                    drRow["intHierarchyId"] = locId[i].ToString();
                                }
                                drRow["intCreatedBy"] = Session["userid"];
                                dtTable.Rows.Add(drRow);
                            }
                        }
                        
                        
                    }
                    string strXMLResult = string.Empty;
                    if (dtTable.Rows.Count > 0)
                    {
                        StringWriter sw = new StringWriter();
                        dtTable.WriteXml(sw);
                        objdesignation.LocId = sw.ToString();
                        sw.Close();
                        sw.Dispose();

                    }
                    objdesignation.ActionCode = "T";
                    intreturnval = Convert.ToInt32(ObjAdminBal.ManageDesignation(objdesignation));
                    ViewState["intdatakey"] = null;
                }
                
            }
            if (intreturnval != 0)
            {
                if (intreturnval != 5)
                {
                    if (btnsave.Text.ToLower().Trim() == "save")
                    {
                        intreturnval = 1;
                    }
                    else
                    {
                        intreturnval = 2;
                    }
                }
               
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + StaticValues.message(intreturnval, "Designation") + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Designation Name Exists !');", true);
            }
            ClearData();
            AdminConsoleNavigation.strNewLink = ">>" + "View";
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            objdesignation = null;
        }
    }
    protected void btninDel_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i <= GridDesignation.Rows.Count - 1; i++)
            {
                CheckBox objchk = new CheckBox();
                objchk = (CheckBox)GridDesignation.Rows[i].FindControl("cbItem");
                if (objchk.Checked == true)
                {
                    intdatakey = Convert.ToInt32(GridDesignation.DataKeys[i].Value);
                    objdesignation.ActionCode = "D";
                    objdesignation.DesignationID = intdatakey.ToString();
                    intreturnval = Convert.ToInt32(ObjAdminBal.ManageDesignation(objdesignation));
                }

            }
            //**************************Commented/Added By Biswarnjan on 8-Nov-2010************************
            //Purpose:Condition for Checking designation existance in usermaster while deleting
            //**************************Begin************************
            // string strOutmsg = StaticValues.message(intreturnval, "Designation");
            string strOutmsg = (intreturnval == 4) ? "Designation Is Assigned To An User" : StaticValues.message(intreturnval, "Designation");
            //**************************End************************
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');", true);
            TabadminDesignationMaster.ActiveTabIndex = 1;
            FillGridview();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            objdesignation = null;
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        if (btncancel.Text == "Reset")
        {
            txtdesignation.Text = "";
            txtalias.Text = "";
            lbLocation.SelectedIndex = 0;
            ddlUsertype.SelectedIndex = 0;
        }
        else
        {
            ClearData();
            AdminConsoleNavigation.strNewLink = ">>" + "View";
        }
    }



    #endregion

    #region "Member Function"
    /// <summary>
    /// function Created By Biswaranjan on 27-sept-2010 to clear the data.
    /// </summary>
    protected void ClearData()
    {
        try
        {
            if (btnsave.Text.ToLower().Trim() == "save")
            {
                //***************Added By Biswaranjan on 9-Nov-2010*****************
                //***************Begin*********************************
                if (intreturnval == 4 || intreturnval == 1)//1:data saved to DB 4:if data exist(check Duplicate)
                {
                    TabadminDesignationMaster.ActiveTabIndex = 0;
                    txtdesignation.Text = "";
                    txtalias.Text = "";
                    ddlUsertype.SelectedValue = "0";
                    lbLocation.SelectedIndex = 0;
                    FillGridview();
                }
                else
                {
                    TabadminDesignationMaster.ActiveTabIndex = 1;
                    FillGridview();
                }
                //***************End*********************************
            }
            else
            {

                TabadminDesignationMaster.ActiveTabIndex = 1;
                GridDesignation.PageIndex = Convert.ToInt32(Request.QueryString["Pindex"].ToString());
                FillGridview();
                TabCreateDesignation.HeaderText = "CREATE";
                btnsave.Text = "Save";
                btncancel.Text = "Reset";
                txtdesignation.Text = "";
                txtalias.Text = "";
                ddlUsertype.SelectedValue = "0";
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// Function Designed by Biswaranjan on 23-july-2010 to fill the data in editcase.
    /// </summary>
    /// <param name="intdesigId"></param>
    protected void Filleditcase(int intdesigId)
    {
        try
        {
            objdesignation.ActionCode = "E";
            objdesignation.DesignationID = intdesigId.ToString();
            IList<Designation> objlstdesignatin = ObjAdminBal.GetDesignationById(objdesignation);
            foreach (Designation data in objlstdesignatin)
            {
                txtdesignation.Text = data.Designame;
                txtalias.Text = data.Aliasname;
                if (data.UserType == "P")
                {
                    ddlUsertype.Items.FindByValue(data.UserType).Selected = true;
                }
                else
                {
                    ddlUsertype.Items.FindByValue(data.UserType).Selected = true;
                }
                hidUserLoc.Value = null;
                if (data.LocId == "-1")
                {
                    hidUserLoc.Value = "All";
                }
                else
                {
                    hidUserLoc.Value = data.LocId;
                }
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "hwa", "BindLocation();", true);

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");
        }
        finally
        {
            //objdesignation = null;
        }
    }
    /// <summary>
    /// function to fill gridview
    /// </summary>
    protected void FillGridview()
    {
        try
        {
            objdesignation.ActionCode = "V";
            IList<Designation> objdesiglst = new List<Designation>();
            objdesiglst = ObjAdminBal.FillGridviewDesig(objdesignation);
            GridDesignation.DataSource = objdesiglst;
            GridDesignation.DataBind();
            DisplayPaging(GridDesignation, objdesiglst.Count);
            if (GridDesignation.Rows.Count == 0)
            {
                btninDel.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "');</script>");

        }
        finally
        {
            //objdesignation = null;
        }
    }
    private void DisplayPaging(GridView grdToPaging, int totalRowCount)
    {
        if (grdToPaging.Rows.Count > 0)
        {
            this.lblpage.Visible = true;
            LnkbtnAllin.Visible = true;
            lblpage.Text = CommonFunction.ShowGridPaging(grdToPaging, grdToPaging.PageSize, grdToPaging.PageIndex, totalRowCount);
        }
        else
        {
            this.lblpage.Visible = false;
            LnkbtnAllin.Visible = false;
        }
    }
    //Method Created By Dilip Kumar Tripathy on dated 7-Feb-2012 with the guidance of Bibhuti Sir
    protected void FillLocationName()
    {

        try
        {
            IList<Designation> objlstplink = ObjAdminBal.FillLocationDesig(Session["UserId"].ToString());
            lbLocation.DataSource = objlstplink;
            lbLocation.DataValueField = "LocationId";
            lbLocation.DataTextField = "LocationName";
            lbLocation.DataBind();
            if (Session["adminstat"].ToString().ToLower() == "super")
            {
                ListItem list = new ListItem();
                list.Text = "All";
                list.Value = "All";
                lbLocation.Items.Insert(1, list);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion

    #region "Grid Events"
    protected void GridDesignation_PageIndexChanging(Object sender, GridViewPageEventArgs e)
    {
        GridDesignation.PageIndex = e.NewPageIndex;
        FillGridview();
    }
    #endregion
    protected void TabadminDesignationMaster_ActiveTabChanged(object sender, EventArgs e)
    {
        txtdesignation.Text = "";
        txtalias.Text = "";
        lbLocation.SelectedIndex = 0;
        TabCreateDesignation.HeaderText = "CREATE";
        btnsave.Text = "Save";
        ddlUsertype.SelectedIndex = 0;
    }

    protected void LnkbtnAllin_Click1(object sender, EventArgs e)
    {
        if (LnkbtnAllin.Text == "All")
        {
            LnkbtnAllin.Text = "Paging";
            this.GridDesignation.PageIndex = 0;
            GridDesignation.AllowPaging = false;
            FillGridview();
            if (GridDesignation.Rows.Count > 0)
            {
                this.lblpage.Text = "1-" + GridDesignation.Rows.Count.ToString() + " Of " + GridDesignation.Rows.Count.ToString();
            }
        }
        else
        {
            LnkbtnAllin.Text = "All";
            GridDesignation.AllowPaging = true;
            FillGridview();
        }
    }

    protected void GridDesignation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string desigId = GridDesignation.DataKeys[e.Row.RowIndex].Value.ToString();
                HyperLink objHyper = e.Row.FindControl("Hypedit") as HyperLink;
                //string[] arrCurLocId = e.Row.Cells[3].Text.Trim(',').Split(',');
                //string strLocFinal = null;
                objHyper.NavigateUrl = "DesignationMaster.aspx?DesigId=" + CommonFunction.EncryptData(desigId) + "&Pindex=" + GridDesignation.PageIndex;
                (e.Row.FindControl("lblSlno") as Label).Text = (GridDesignation.PageIndex * GridDesignation.PageSize + e.Row.RowIndex + 1).ToString();
                //if (e.Row.Cells[3].Text.ToUpper() != "ALL")
                //{
                //    for (int i = 0; i < arrCurLocId.Length; i++)
                //    {
                //        if (locList.Contains(arrCurLocId[i].ToString()))
                //        {
                //            strLocFinal += arrCurLocId[i].ToString() + ",";
                //        }
                //    }
                //    if (strLocFinal != null)
                //    {
                //        e.Row.Visible = true;
                //        objHyper.NavigateUrl = "DesignationMaster.aspx?DesigId=" + CommonFunction.EncryptData(desigId) + "&Pindex=" + GridDesignation.PageIndex;
                //    }
                //    else
                //    {
                //        e.Row.Visible = false;
                //    }
                //}
                //else
                //{
                //    e.Row.Visible = true;
                //    strLocFinal = "All";
                //    if (Session["adminstat"].ToString().ToLower() == "super")
                //    {
                //        objHyper.NavigateUrl = "DesignationMaster.aspx?DesigId=" + CommonFunction.EncryptData(desigId) + "&Pindex=" + GridDesignation.PageIndex; ;
                //    }

                //}
                //if (strLocFinal != null)
                //{
                //    string locId = strLocFinal;
                //    string concatlocname = null;
                //    if (locId.ToLower().Trim() == "all")
                //    {
                //        e.Row.Cells[3].Text = locId;
                //        concatlocname = locId;
                //    }
                //    else
                //    {
                //        if (locId.ToString() != "&nbsp;")
                //        {
                //            e.Row.Cells[3].Text = null;

                //            string[] Var = ((locId.TrimStart(',')).TrimEnd(',')).Split(',');
                //            for (int i = 0; i < Var.Length; i++)
                //            {
                //                DataTable dtLoc = objCmnDll.GetDataTable("ConnectionString", "usp_M_AdminLevelDetails_View", "", "chrActionCode", "H", "intLevelDetailId", Convert.ToInt32(Var[i]), "intLevelID", 0);
                //                for (int j = 0; j < dtLoc.Rows.Count; j++)
                //                {
                //                    concatlocname += dtLoc.Rows[j][0].ToString() + ",";
                //                }

                //            }
                //            e.Row.Cells[3].Text = concatlocname.Remove(concatlocname.Length - 1);
                //        }
                //    }
                //}

            }
        }
        catch (Exception e1)
        {
            throw new Exception(e1.Message, e1);
        }

    }

}
