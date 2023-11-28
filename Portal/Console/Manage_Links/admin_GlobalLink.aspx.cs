/********************************************************************************************************************
' File Name             :   admin_GlobalLink.cs
' Description           :   To Add/edit/delete/activate/inactivate Global Link
' Created by            :   Pratik Ranjan Sarangi
' Created On            :   15-Jul-2010
' Modification History  :
'                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
'                            1                            26-Jul-2010        Biswaranjan                   To Modify the javascriptfunction checkSelect
'                            2                            01-Oct-2010        Biswaranjan                   Add code to set the default button of the page .
'                            3                            24-Apr-2012        Dilip Tripathy                Change the Edit tab button to Active and Active to Inactive with the associate change
'                            4                            25-Jun-2012        Dilip Tripathy                To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                        
'                            5                            02-May-2013        Dilip Tripathy                To change the navigate url as per creent tab name.
' Function Name         :   
' Procedures Used       :    
' User Defined Namespace:   KWAdminConsole.Manage_Links
' Inherited classes     :                                              
**********************************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using KWAdminConsole.Messages;

public partial class Admin_Manage_Links_admin_GlobalLink : System.Web.UI.Page
{
    #region Variable & Object Declaration


   
    static ArrayList arrListGlink;
    static int gridRowCount = 0;
    int intOutPut;
    public int RecCount;
    AdminAppService ObjAdminBal = new AdminAppService();
    Global objGLink = new Global();
    public string StrOL = "";
    #endregion

    #region Page Load
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
        tr1.Visible = false;


        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
      

        if (!IsPostBack)
        {
            if (ConfigurationManager.AppSettings["OL"] != null)
            {
                StrOL = ConfigurationManager.AppSettings["OL"].ToString();
                trOL.Visible = true;
            }
           
            this.txtGlinkName.Attributes.Add("onkeyup", "return TextCounter('" + txtGlinkName.ClientID + "','" + lblMaxcounter.ClientID + "',50);");
            this.lblpage.Visible = false;
            LnkbtnAllin.Visible = false;
            FillSlno();
            arrListGlink = Admin.CommonFunction.CommonFunction.GetGlinkIdByUserId(int.Parse(Session["UserId"].ToString()));
            FillGrid("C", grdInActiveGlobalLink);
            if (Request.QueryString["pindex"] != null)
            {
                grdActiveGLink.PageIndex = Convert.ToInt32(Request.QueryString["pindex"]);
            }
            FillGrid("V", grdActiveGLink);
            TabContainer1.ActiveTabIndex = 1;
            if (Request.QueryString["Glink"] != null)
            {
               
                int strGlinkId = Convert.ToInt32(CommonFunction.DecryptData(Request.QueryString["Glink"].ToString()));
                fillGlobalLink(strGlinkId);
                lblMaxcounter.Text = (50 - txtGlinkName.Text.Length).ToString();
                btnGAdd.Text = "Update";
                btnReset.Text = "Cancel";
                TabCreateGlink.HeaderText = "UPDATE";
                AdminConsoleNavigation.strNewLink = ">>" + "UPDATE";
                TabContainer1.ActiveTabIndex = 0;
                txtGlinkName.Focus();
                tr1.Visible = true;

            }
            else
            {
                TabCreateGlink.HeaderText = "CREATE";
                btnGAdd.Text = "Save";
                btnReset.Text = "Reset";
                tr1.Visible = false;
            }
         
          
             
            //this.txtGlinkName.Attributes.Add("onkeydown", "return AllowEnter();");
        }

        if (TabContainer1.ActiveTabIndex == 1)
        {
            btnModify.Focus();
        }
        if (TabContainer1.ActiveTabIndex == 0)
        {
            txtGlinkName.Focus();
        }

        btnModify.Attributes.Add("onclick", "return checkSelect('TabContainer1_TabActiveGlink_btnModify');");
        btninactive.Attributes.Add("onclick", "return checkSelect('TabContainer1_TabActiveGlink_btninactive');");
        btnActivate.Attributes.Add("onclick", "return checkSelect('TabContainer1_TabActiveGlink_btnActivate');");
        btnGAdd.Attributes.Add("onclick", "return dispConfm('TabContainer1_TabCreateGlink_btnGAdd');");
        Page.Form.DefaultButton = btnGAdd.UniqueID;
        AdminConsoleNavigation.strNewLink = ">>" + TabContainer1.ActiveTab.HeaderText;
    }
    #endregion

    #region "Button Events"
    /// <summary>
    /// To Add Global link
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnGAdd_Click(object sender, EventArgs e)
    {
        try
        {

            if (btnGAdd.Text.ToUpper() == "SAVE")
            {
                objGLink.ActionCode = "A";
                objGLink.GlobalLinkName = txtGlinkName.Text.Trim();
                objGLink.GlobalLinkNameinAhmaric = txtGlinkNameInAmharic.Text.Trim();
                objGLink.CreatedBy = Convert.ToInt32(Session["UserId"]);
                objGLink.DeptID = "1";

                objGLink.SLNO = Convert.ToInt32(lblGSlno.Text);
                if (chkShowHome.Checked == true)
                {
                    objGLink.OnHomePage = true;
                }
                else
                {
                    objGLink.OnHomePage = false;
                }

                intOutPut = ObjAdminBal.AddGlobalLink(objGLink);
                if (intOutPut == 1)
                {

                    string strOutmsg = StaticValues.message(intOutPut, "Global Link");
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');", true);
                }
                if (intOutPut == 4)
                {

                    string strOutmsg = StaticValues.message(intOutPut, "Global Link");
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');", true);
                }
                ResetControls();
                FillSlno();
            }
            else if (btnGAdd.Text.ToUpper() == "UPDATE")
            {


                objGLink.ActionCode = "U";
                objGLink.IntGlobalLinkID = CommonFunction.DecryptData(Request.QueryString["Glink"].ToString());
                objGLink.GlobalLinkNameinAhmaric = txtGlinkNameInAmharic.Text.Trim();
                objGLink.GlobalLinkName = txtGlinkName.Text.Trim();
                objGLink.SLNO = Convert.ToInt32(lblGSlno.Text);
                objGLink.CreatedBy = Convert.ToInt32(Session["UserId"]);
                if (chkShowHome.Checked == true)
                {
                    objGLink.OnHomePage = true;
                }
                else
                {
                    objGLink.OnHomePage = false;
                }
                //IList<Global> objGlLink = new IList<Global>();
                //objGlLink.Add(objGLink);
                //objGLink.GloballinkidNName = (List<Global>)objGlLink ;

                intOutPut = ObjAdminBal.EditGlobalLink(objGLink);
                if (intOutPut == 2)
                {
                    string strOutmsg = StaticValues.message(intOutPut, "Global Link");
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');document.location.href='admin_GlobalLink.aspx?pindex=" + Request.QueryString["pindex"].ToString() + "';", true);

                    if (cbReCXml.Checked == true)
                    {
                        Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                        CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
                    }
                }

            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }
    /// <summary>
    /// To Reset Field
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (btnReset.Text == "Cancel")
        {
            ResetControls();

            TabContainer1.ActiveTabIndex = 1;
            AdminConsoleNavigation.strNewLink = ">>" + "ACTIVE";
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "document.location.href='admin_GlobalLink.aspx?pindex=" + Request.QueryString["pindex"].ToString() + "';", true);

        }
        else if (btnReset.Text == "Reset")
        {
            ResetControls();

        }

    }

    /// <summary>
    /// To Modify Serial Number
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnModify_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < grdActiveGLink.Rows.Count; i++)
        {
            CheckBox chkGLink = (CheckBox)grdActiveGLink.Rows[i].FindControl("chkGLink");
            TextBox TxtGlinkSlno = (TextBox)grdActiveGLink.Rows[i].FindControl("txtSlNo");
            if (chkGLink.Checked == true)
            {
                objGLink.ActionCode = "S";
                objGLink.IntGlobalLinkID = grdActiveGLink.DataKeys[i].Value.ToString();
                objGLink.SLNO = Convert.ToInt32(TxtGlinkSlno.Text);
                objGLink.CreatedBy = Convert.ToInt32(Session["UserId"]);
                intOutPut = ObjAdminBal.UpdateSlno(objGLink);

            }
        }
        if (intOutPut == 9)
        {
            if (ConfigurationManager.AppSettings["XmlDel"].ToString() == "Y")
            {
                Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
            }
            string strOutmsg = StaticValues.message(intOutPut, "Global Link(s)");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');", true);

        }
        else if (intOutPut == 10)
        {
            string strOutmsg = StaticValues.message(intOutPut, "Global Link(s)");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');", true);
        }
        FillGrid("V", grdActiveGLink);
        TabContainer1.ActiveTabIndex = 1;
    }

    /// <summary>
    /// To make InActive Global Link
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btninactive_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdActiveGLink.Rows)
        {
            CheckBox chkGLink = (CheckBox)row.FindControl("chkGLink");
            if (chkGLink.Checked == true)
            {
                objGLink.ActionCode = "I";
                objGLink.IntGlobalLinkID = Convert.ToString(grdActiveGLink.DataKeys[row.RowIndex].Value);
                objGLink.CreatedBy = Convert.ToInt32(Session["UserId"]);
                //IList<Global> objGlLink = new List<Global>();
                //objGlLink.Add(objGLink);
                //objGLink.GloballinkidNName = (List<Global>)objGlLink;
                intOutPut = ObjAdminBal.InActivateGlobalLink(objGLink);

            }
        }
        if (intOutPut == 7)
        {
            if (ConfigurationManager.AppSettings["XmlDel"] == "Y")
            {
                Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
            }
            string strOutmsg = StaticValues.message(intOutPut, "Global Link(s)");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');", true);
        }
        FillGrid("C", grdInActiveGlobalLink);
        FillGrid("V", grdActiveGLink);
        TabContainer1.ActiveTabIndex = 1;

    }
    /// <summary>
    /// To make Active Global Link
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnActivate_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < grdInActiveGlobalLink.Rows.Count; i++)
        {
            CheckBox chkGLink = (CheckBox)grdInActiveGlobalLink.Rows[i].FindControl("chkInactiveGLink");
            if (chkGLink.Checked == true)
            {
                objGLink.ActionCode = "T";
                objGLink.IntGlobalLinkID = Convert.ToString(grdInActiveGlobalLink.DataKeys[i].Value);
                objGLink.CreatedBy = Convert.ToInt32(Session["UserId"]);
                //IList<Global> objGlLink = new List<Global>();
                //objGlLink.Add(objGLink);
                //objGLink.GloballinkidNName = (List<Global>)objGlLink;
                intOutPut = ObjAdminBal.ActivateGlobalLink(objGLink);

            }
        }
        if (intOutPut == 6)
        {
            if (ConfigurationManager.AppSettings["XmlDel"] == "Y")
            {
                Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
            }
            string strOutmsg = StaticValues.message(intOutPut, "Global Link(s)");
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + strOutmsg + "');", true);
        }
        else
        {

        }
        FillGrid("C", grdInActiveGlobalLink);
        FillGrid("V", grdActiveGLink);
        TabContainer1.ActiveTabIndex = 1;
        AdminConsoleNavigation.strNewLink = ">>" + "ACTIVE";
    }

    #endregion

    #region Member Functions
    /// <summary>
    /// To get Max Sl No of Globallink
    /// </summary>
    protected void FillSlno()
    {
        objGLink.ActionCode = "S";
        IList<Global> objlstGlink = ObjAdminBal.GetAllGlobalLink(objGLink);
        var data = from datafield in objlstGlink
                   select (datafield.SLNO);
        lblGSlno.Text = data.First().ToString();
    }

    private void ResetControls()
    {
        txtGlinkName.Text = "";
        chkShowHome.Checked = false;
        txtGlinkNameInAmharic.Text = "";
        //foreach (ListItem list in lbUserLoc.Items)
        //{
        //    list.Selected = false;
        //}
        txtGlinkName.Focus();
        FillSlno();
    }
    /// <summary>
    /// Method To fill gridview
    /// </summary>
    private void FillGrid(string strAction, GridView grdToFill)
    {
        IList<Global> objGLinkList = new List<Global>();
        IList<Global> objGLinkList2 = new List<Global>();
        objGLink.ActionCode = strAction;
        objGLinkList = ObjAdminBal.GetAllGlobalLink(objGLink);
        foreach (Global objIglob in objGLinkList)
        {
            objIglob.SLNO = objIglob.SLNO;
            objIglob.IntGlobalLinkID = objIglob.IntGlobalLinkID;
            objIglob.GlobalLinkName = Admin.CommonFunction.CommonFunction.GetDecodedData(objIglob.GlobalLinkName.Trim());
            objIglob.DeletedStatus = objIglob.DeletedStatus;
            objIglob.CreatedBy = objIglob.CreatedBy;
            objIglob.GlobalLinkNameinAhmaric = objIglob.GlobalLinkNameinAhmaric;
            objGLinkList2.Add(objIglob);
        }
        gridRowCount = objGLinkList2.Count;
        grdToFill.DataSource = objGLinkList2;
        grdToFill.DataBind();
        RecCount = objGLinkList2.Count;
        DisplayPaging(grdToFill, RecCount, strAction);
        if (grdToFill.Rows.Count <= 0)
        {
            btnActivate.Visible = false;
            //btnDelete.Visible = false;
            btninactive.Visible = false;
            btnModify.Visible = false;
        }
        else
        {
            if (strAction == "C")
            {
                btnActivate.Visible = true;
            }
            else
            {
                //btnDelete.Visible = true;
                btninactive.Visible = true;
                btnModify.Visible = true;
            }
        }
    }
    //Method Created By : Dilip Kumar Tripathy
    //Created Date : 13-Feb-2012
    //Purpose : To Manage Paging of Gridview
    //Modified By: Dilip Kumar Tripathy
    //Modify Date: 05-Apr-2012
    private void DisplayPaging(GridView grdToPaging, int RecCount, string strActionCode)
    {
        if (grdToPaging.Rows.Count > 0)
        {
            if (strActionCode == "V")
            {
                this.lblPaging.Visible = true;
                lbtnAll.Visible = true;
                lblPaging.Text = Admin.CommonFunction.CommonFunction.ShowGridPaging(grdToPaging, grdToPaging.PageSize, grdToPaging.PageIndex, RecCount);
            }
            else
            {
                this.lblpage.Visible = true;
                LnkbtnAllin.Visible = true;
                lblpage.Text = Admin.CommonFunction.CommonFunction.ShowGridPaging(grdToPaging, grdToPaging.PageSize, grdToPaging.PageIndex, RecCount);
            }

        }
        else
        {
            if (strActionCode == "V")
            {
                this.lblPaging.Visible = false;
                lbtnAll.Visible = false;
            }
            else
            {
                this.lblpage.Visible = false;
                LnkbtnAllin.Visible = false;
            }

        }

    }


    private void fillGlobalLink(int intGLinkID)
    {
        try
        {
            objGLink.ActionCode = "U";
            objGLink.IntGlobalLinkID = intGLinkID.ToString();
            IList<Global> objlstGlink = ObjAdminBal.GetGlobalLinkDetails(objGLink);
            foreach (Global data in objlstGlink)
            {
                txtGlinkName.Text = Admin.CommonFunction.CommonFunction.GetDecodedData(data.GlobalLinkName.ToString());
                lblGSlno.Text = data.SLNO.ToString();
                chkShowHome.Checked = data.OnHomePage;
                txtGlinkNameInAmharic.Text = data.GlobalLinkNameinAhmaric.ToString();

            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    #endregion


    #region Gridview Events
    protected void grdActiveGLink_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
        grdActiveGLink.PageIndex = e.NewPageIndex;
        FillGrid("V", grdActiveGLink);
    }
    protected void grdInActiveGlobalLink_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdInActiveGlobalLink.PageIndex = e.NewPageIndex;
        FillGrid("C", grdInActiveGlobalLink);
    }
    protected void grdActiveGLink_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (StrOL == "")
                {
                    grdActiveGLink.Columns[3].Visible = false;
                }

                if (arrListGlink.Contains(grdActiveGLink.DataKeys[e.Row.RowIndex].Value.ToString()))
                {
                    //Code Added By Dilip Kumar Tripathy on dated 22-Feb-2012
                    //Purpose: To separate the link access for Super Admin and Location Admin
                    e.Row.Visible = true;
                    if (Session["adminstat"].ToString() == "super")
                    {
                        HyperLink hyper = new HyperLink();
                        hyper = (HyperLink)e.Row.FindControl("hypEdit");

                        hyper.NavigateUrl = "admin_GlobalLink.aspx?Glink=" + CommonFunction.EncryptData(grdActiveGLink.DataKeys[e.Row.RowIndex].Value.ToString()) + "&pindex=" + grdActiveGLink.PageIndex;
                    }
                    else
                    {
                        e.Row.Cells[0].Visible = false;
                        (e.Row.FindControl("txtSlNo") as TextBox).ReadOnly = true;
                    }
                }
                else
                {
                    e.Row.Visible = false;

                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[3].Text = "Global Link(In " + StrOL + ")";
                if (Session["adminstat"].ToString().ToLower() != "super")
                {
                    e.Row.Cells[0].CssClass = "hiddencol";
                    
                }

            }
            //Code Ended By Dilip Kumar Tripathy  on dated 22-Feb-2012
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }

    protected void grdInActiveGLink_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[3].Text = "Global Link(In " + StrOL + ")";

            }
           
                if (StrOL == "")
                {
                    grdInActiveGlobalLink.Columns[3].Visible = false;
                }
          
           
           
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }

    #endregion



    #region Events

    protected void LnkbtnAllin_Click(object sender, EventArgs e)
    {
        if (LnkbtnAllin.Text == "All")
        {
            LnkbtnAllin.Text = "paging";
            this.grdInActiveGlobalLink.PageIndex = 0;
            grdInActiveGlobalLink.AllowPaging = false;
            FillGrid("C", grdInActiveGlobalLink);
            if (grdInActiveGlobalLink.Rows.Count > 0)
            {
                this.lblPaging.Text = "1-" + grdInActiveGlobalLink.Rows.Count.ToString() + " Of " + grdInActiveGlobalLink.Rows.Count.ToString();
            }
        }
        else
        {
            LnkbtnAllin.Text = "All";
            grdInActiveGlobalLink.AllowPaging = true;
            FillGrid("C", grdInActiveGlobalLink);
        }



    }

    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            this.grdInActiveGlobalLink.PageIndex = 0;
            grdActiveGLink.AllowPaging = false;
            FillGrid("V", grdActiveGLink);
            if (grdActiveGLink.Rows.Count > 0)
            {
                if (Session["adminstat"].ToString().Trim() != "loc")
                {
                    this.lblPaging.Text = "1-" + grdActiveGLink.Rows.Count.ToString() + " Of " + grdActiveGLink.Rows.Count.ToString();
                }
                else
                {
                    this.lblPaging.Text = "1-" + arrListGlink.Count.ToString() + " Of " + arrListGlink.Count.ToString();

                }
            }
        }
        else
        {
            lbtnAll.Text = "All";
            grdActiveGLink.AllowPaging = true;
            FillGrid("V", grdActiveGLink);
        }


    }

    protected void TabContainer1_ActiveTabChanged(object sender, EventArgs e)
    {

        txtGlinkName.Text = "";
        TabCreateGlink.HeaderText = "CREATE";
        btnGAdd.Text = "Save";
        btnReset.Text = "Reset";
        chkShowHome.Checked = false;
        if (TabContainer1.ActiveTabIndex == 0)
        {
            txtGlinkName.Focus();

        }
        if (TabContainer1.ActiveTabIndex == 1)
        {
            FillGrid("V", grdActiveGLink);
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "document.location.href='admin_GlobalLink.aspx';", true);
        }
        if (TabContainer1.ActiveTabIndex == 2)
        {
            FillGrid("C", grdInActiveGlobalLink);
        }
    }


    #endregion



}
