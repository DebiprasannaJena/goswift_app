/********************************************************************************************************************
' File Name             :   adminPrimaryLink.cs
' Description           :   To Add/edit/delete/activate/inactivate Primary Link
' Created by            :   Biswaranjan Das
' Created On            :   02-jun-2010
' Modification History  :
'                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
'                            1.                            25-Apr-2012       Dilip Kumar Tripathy         Cganging Edit Tab to Active with it's'                                                                                                          associate change.
'                            2.                            25-June-2012      Dilip Tripathy               To Manage The CSRF security error added the code to check the querystring value of 'att' in page load                                                 
'                            3.                            02-May-2013       Dilip Tripathy               To change the navigate url as per creent tab name.
' Function Name         :   FillLocation(),FillActiveGloballink(),FillFunction(), Getlbitems(),FillEdittab()
'                           ResetPrimarylink(),GetDeptname(),GetDepartmentId(),FillSecondLevel(),EditPrimarylink(),
'                           AddPrimaryLink(),getfilename(), Getbrowserinfo(),GetDepartmentId(),FillGlobalLink(),
' Procedures Used       :   usp_GlobalLink_View,usp_M_AdminLevelDetails_View,usp_PrimaryLink_Manage 
' User Defined Namespace:   KWAdminConsole.Manage_Links
' Inherited classes     :                                              
**********************************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Admin.CommonFunction;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using KWAdminConsole.Messages;


public partial class Admin_Manage_Links_adminPrimaryLink : System.Web.UI.Page
{
    #region "Variables"
    //CommonDLL objCmnDll = new CommonDLL();

    int intReturnValue = 0;
    string strPlinkId = null;
   
  
 
   
    public string strbtntext = null;
   
    public int RecCount;
    int strCounterRemarks = 50;
    public static int totalRowCount = 0;
    public static ArrayList alUserLocIds = new ArrayList();
    AdminAppService ObjAdminBal = new AdminAppService();
    Primary objPrimaryLink = new Primary();
    public string StrOL = "";
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
        if (Request.QueryString["Plink"] != null)
        {
            strPlinkId = CommonFunction.DecryptData(Request.QueryString["Plink"].ToString());
        }
       

        //Code Added By : Dilip Kumar Tripathy on dated 10-May-2013
        //Purpose : To clear the browser cache
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");
        Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //Code added by Dilip Kumar Tripathy on dated 02-May-2013 to change the navigate url as per creent tab name
        AdminConsoleNavigation.strNewLink =">>" +TabPrimaryLink.ActiveTab.HeaderText;
        if (Request.QueryString["pdx"] != null)
        {
            GridActivePlink.PageIndex = Convert.ToInt32(Request.QueryString["pdx"]);
        }
        txtPlink.Attributes.Add("onkeyup", "return TextCounter('" + txtPlink.ClientID + "','" + lblRemarks.ClientID + "','" + strCounterRemarks + "');");
        if (!IsPostBack)
        {
            if (ConfigurationManager.AppSettings["OL"] != null)
            {
                StrOL = ConfigurationManager.AppSettings["OL"].ToString();
                trOL.Visible = true;
            }
            FillGlobalLink(ddlselGlink, "Create");
            FillGlobalLink(ddleditGlink, "Edit");
            FillGlobalLink(ddlinactiveGlink, "Active");
            FillFunction();
            //FillLocation();

            if (GridActivePlink.Rows.Count == 0)
            {
                btnmodify.Visible = false;
                //btnDelete.Visible = false;
                btninactive.Visible = false;
            }
            else
            {
                btnmodify.Visible = true;
                //btnDelete.Visible = true;
                // btninactive.Visible = true; Commented By Dilip Kumar Tripathy on dated 14-Mar-2012
            }
            //code added by dilip kumar tripathy on dated 17-May-2012
            if (Session["argEdit"] != null)
            {
                ddleditGlink.SelectedValue = Session["argEdit"].ToString();
                FillGridview(GridActivePlink, Convert.ToInt32(ddleditGlink.SelectedValue), 'P');
                btninactive.Visible = true;
                //btnDelete.Visible = true;
                btnmodify.Visible = true;
                Session.Remove("argEdit");
            }
            //if (Request.Params["Plid"] != null)
            //{
            //    FillSecondLevel(Convert.ToInt32(Request.Params["Plid"].ToString()));
            //}
            //lblsecondlayer.Text = Convert.ToString(hidfldlbltxt.Value);
            if (Request.QueryString["Plink"] != null)
            {               
                FillEdittab(Convert.ToInt32(CommonFunction.DecryptData(Request.QueryString["Plink"].ToString())));
                btnEdit.Visible = true;
                btnAdd.Visible = false;
                btnReset.Text = "Cancel";
                TabCreatePlink.HeaderText = "UPDATE";
                AdminConsoleNavigation.strNewLink = ">>" + "UPDATE";
                TabPrimaryLink.ActiveTabIndex = 0;
            }
            else
            {
                TabCreatePlink.HeaderText = "CREATE";
                btnReset.Text = "Reset";
                TabPrimaryLink.ActiveTabIndex = 1;
                ddlselGlink.Focus();
            }
            //Code to calll the javascript function set the default button 
        }
        if (TabPrimaryLink.ActiveTabIndex == 0)
        {
            ddlselGlink.Focus();           
        }

        if (TabPrimaryLink.ActiveTabIndex == 2)
        {
            btnactive.Visible = false; 
        }
        if (TabActivePlink.TabIndex == 1)
        {
            btnShow.Focus();
        }

        if (TabInActivePlink.TabIndex == 2)
        {
            btnInactiveShow.Focus();
        }
        btnReset.Attributes.Add("onclick", "return ResetFunction('TabPrimaryLink_TabCreatePlink_btnReset')");
        btnAdd.Attributes.Add("onclick", "return conformation('TabPrimaryLink_TabCreatePlink_btnAdd');");
        btnEdit.Attributes.Add("onclick", "return conformation('TabPrimaryLink_TabCreatePlink_btnEdit');");
        rbtExtrn.Attributes.Add("onclick", "return HideShow('TabPrimaryLink_TabCreatePlink_rbtIntern','TabPrimaryLink_TabCreatePlink_rbtExtrn');");
        rbtIntern.Attributes.Add("onclick", "return HideShow('TabPrimaryLink_TabCreatePlink_rbtIntern','TabPrimaryLink_TabCreatePlink_rbtExtrn');");
       
    }
    #region "Member Function"


    protected void FillFunction()
    {
        
        try
        {
            IList<Primary> objlstplink =ObjAdminBal.FillFunctionType();       
            ddlselFunction.DataSource = objlstplink;
            ddlselFunction.DataValueField = "PlinkId";
            ddlselFunction.DataTextField = "PlinkName";          
            ddlselFunction.DataBind();
            ddlselFunction.Items.Insert(0, "--Select--");
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    /// <summary>
    /// Function Created On 5-june-2010 To Fill the Globallink Dropdownlist in Edit Tab
    /// </summary>
    protected void FillGlobalLink(DropDownList ddlname, string Tabname)
    {
        try
        {
            IList<Primary> objlstplink = ObjAdminBal.FillGlink(Session["UserId"].ToString());
            ddlname.DataSource = objlstplink;
            ddlname.DataValueField = "GlinkId";
            ddlname.DataTextField = "GLinkName";
            ddlname.DataBind();
            ddlname.Items.Insert(0, "--Select--");
           
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

   
    /// <summary>
    /// Function Created By Biswaranjan on 14-jun-2010 to get the file name.
    /// </summary>
    /// <returns></returns>
    protected string getfilename()
    {
        try
        {
            string filename = null;
            if (rbtIntern.Checked == true)
            {
                filename = ddlselFunction.SelectedItem.Text;
            }
            else
            {
                filename = txtURL.Text;
            }
            return filename;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //*******************Summery********************
    //Function Name :AddPrimaryLink()
    //Purpose : To ADD Primarylink data
    //Parameters Name : None
    //Parameters Datatype :None
    //Returns :None
    //Retun Datatype :None
    //Created By : Biswaranjan Das
    //Created Date :  5-june-2010
    //************************************************
    protected void AddPrimaryLink()
    {
        try
        {
           
            objPrimaryLink.ActionCode = "A";
            objPrimaryLink.PlinkId = "0";
            objPrimaryLink.PlinkName = txtPlink.Text;
            objPrimaryLink.PlinkNameinAhmaric = txtPlinkNameInAmharic.Text.Trim();
            objPrimaryLink.GlinkId = Convert.ToInt32(ddlselGlink.SelectedValue);
            objPrimaryLink.FunctionId = rbtIntern.Checked == true ? Convert.ToInt32(ddlselFunction.SelectedValue) : 0;
            objPrimaryLink.SlNo = Convert.ToInt32(lblPSlNo.Text);
          
            objPrimaryLink.AccessLevel = 0;
            objPrimaryLink.Browser =  rbtExtrn.Checked == true ? txtURL.Text : null; //Pass URL For external link;
            if (chkShowHome.Checked == true)
                objPrimaryLink.OnHomePage = true;
            else
                objPrimaryLink.OnHomePage = false;
            objPrimaryLink.UpdatedBy = Convert.ToInt32(Session["UserId"]);
            intReturnValue = ObjAdminBal.AddPrimaryLink(objPrimaryLink);
            if (intReturnValue == 13)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Function is already used by another primary link.');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + StaticValues.message(intReturnValue, "PrimaryLink") + "');", true);
            }

          
            ResetPrimarylink();
            //-----------Code Added By Dilip Kumar Tripathy on dated 04-Mar-2012-------------------
            //-----------Purpose : To Reset control values after successfully insert
            //objCmnDll.ResetFunction(ddlselGlink, txtPlink, rbtExtrn, ddlselFunction, txtURL,txtPlinkNameInAmharic);
            ddlselGlink.SelectedIndex = -1;
            ddlselFunction.SelectedIndex = -1;
            txtPlink.Text = "";
            txtURL.Text = "";
            txtPlinkNameInAmharic.Text = "";
            rbtIntern.Checked = true;
          
            TRurl.Style.Add("display", "none");
            TRfunction.Style.Add("display", "");
            chkShowHome.Checked = false;
            lblPSlNo.Text = "***";
            //------------------------------Code Ended By Dilip-------------------------------------

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //*******************Summery***********************
    //Function Name :EditPrimarylink()
    //Purpose : To Edit Primarylink data
    //Parameters Name : None
    //Parameters Datatype :None
    //Returns :None
    //Retun Datatype :None
    //Created By : Biswaranjan Das
    //Created Date : 16-june-2010
    //*************************************************
    protected void EditPrimarylink(string PlinkId)
    {
        try
        {
           
            objPrimaryLink.ActionCode = "U";
            objPrimaryLink.PlinkId = PlinkId;
            objPrimaryLink.PlinkName = txtPlink.Text;
            objPrimaryLink.PlinkNameinAhmaric = txtPlinkNameInAmharic.Text.Trim();
            objPrimaryLink.GlinkId = Convert.ToInt32(ddlselGlink.SelectedValue);
            objPrimaryLink.SlNo = Convert.ToInt32(lblPSlNo.Text);
            //objPrimaryLink.AccessLevel = 0;
            if (chkShowHome.Checked == true)
                objPrimaryLink.OnHomePage = true;
            else
                objPrimaryLink.OnHomePage = false;
            //objPrimaryLink.Browser = Getbrowserinfo();
            if (rbtIntern.Checked == true)
            {
                objPrimaryLink.FunctionId = Convert.ToInt32(ddlselFunction.SelectedValue);
                objPrimaryLink.Browser = "";
            }
            else
            {
                objPrimaryLink.FunctionId = 0;
                objPrimaryLink.Browser = txtURL.Text;
            }
            objPrimaryLink.UpdatedBy = Convert.ToInt32(Session["UserId"]);
            intReturnValue = ObjAdminBal.UpdatePrimaryLink(objPrimaryLink);
            if (intReturnValue == 2)
            {
                if (ConfigurationManager.AppSettings["XmlDel"] == "Y")
                {
                    Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                    CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
                }
            }
            //ResetPrimarylink();
            Session["argEdit"] = ddlselGlink.SelectedValue;
            if (intReturnValue == 13)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Function is already used by another primary link.');", true);
            }
            else
            {
                string strUrl = "adminPrimaryLink.aspx?pdx=" + Request.QueryString["pindex"].ToString();
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + StaticValues.message(intReturnValue, "PrimaryLink") + "');document.location.href='" + strUrl + "';", true);
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
         
    }

    
    //*******************Summery***************************
    //Function Name :ResetPrimarylink()
    //Purpose : To Reset all the control of the page and also manage the tab navigation 
    //Parameters Name : None
    //Parameters Datatype :None
    //Returns :None
    //Retun Datatype : None
    //Created By : Biswaranjan Das
    //Created Date : 5-june-2010
    //*****************************************************
    protected void ResetPrimarylink()
    {
        try
        {
            if (btnAdd.Visible == true)
            {
                // Response.Write("<script>document.location.href='adminPrimaryLink.aspx'</script>");
                TabPrimaryLink.ActiveTabIndex = 0;
            }
            else
            {

                TabPrimaryLink.ActiveTabIndex = 1;//go to active tab if cancel btn is clicked while updating
                ddleditGlink.Items.Clear();
                FillGlobalLink(ddleditGlink, "Edit");
                ddleditGlink.Items.FindByValue(Convert.ToString(ViewState["Glid"])).Selected = true;
                txtPlinkNameInAmharic.Text = "";
                ViewState["Glid"] = null;
                FillGridview(GridActivePlink, Convert.ToInt32(ddleditGlink.SelectedValue), 'P');
                TabCreatePlink.HeaderText = "CREATE";
                ddlselGlink.SelectedIndex = -1;
                txtPlink.Text = "";
                rbtIntern.Checked = true;
                ddlselFunction.SelectedIndex = -1;
                //lbLocation.SelectedValue = "--Select--";
                lblPSlNo.Text = "";
                btnEdit.Visible = false;
                btnAdd.Visible = true;
                btnmodify.Visible = true;
                //btnDelete.Visible = true;
                btninactive.Visible = true;
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    

    //*******************Summery***************************
    //Function Name :FillEdittab()
    //Purpose : To Fill all the control in edit case
    //Parameters Name : Plinkid
    //Parameters Datatype :integer
    //Returns :None
    //Retun Datatype : None
    //Created By : Biswaranjan Das
    //Created Date : 14-June-2010
    //*****************************************************
    protected void FillEdittab(Int32 Plinkid)
    {
        try
        {
            objPrimaryLink.ActionCode = "E";
            objPrimaryLink.PlinkId = Plinkid.ToString();
            IList<Primary> objlstplink = ObjAdminBal.GetAllPrimaryLink(objPrimaryLink);
            foreach (Primary data in objlstplink)
            {
                ddlselGlink.Items.FindByText(data.GLinkName).Selected = true;
                ViewState["Glid"] = data.GlinkId;//store the glink name to select the dropdown list in when cancel updating.
                txtPlinkNameInAmharic.Text = data.PlinkNameinAhmaric;
                txtPlink.Text = data.PlinkName;
                lblRemarks.Text = (50 - (txtPlink.Text.Length)).ToString();
                if (data.LinkType.ToString()=="0")
                {
                    
                    rbtIntern.Attributes.Add("onclick", "return HideShow('TabPrimaryLink_TabCreatePlink_rbtIntern','TabPrimaryLink_TabCreatePlink_rbtExtrn');");

                    rbtIntern.Checked=true;                  
                    TRfunction.Style.Add("display", "");
                    TRurl.Style.Add("display", "none");
                    ddlselFunction.SelectedValue = data.FunctionId.ToString();
                    
                        
                }
                else
                {
                    rbtExtrn.Attributes.Add("onclick", "return HideShow('TabPrimaryLink_TabCreatePlink_rbtIntern','TabPrimaryLink_TabCreatePlink_rbtExtrn');");
                    rbtExtrn.Checked = true;                
                    TRurl.Style.Add("display", "");
                    TRfunction.Style.Add("display", "none");
                    txtURL.Text = data.ExternalURL.ToString();
                }
                chkShowHome.Checked = data.OnHomePage;
                lblPSlNo.Text = data.SlNo.ToString();
             

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //*******************Summery***************************
    //Function Name :FillGridview()
    //Purpose : To Fill The Gridview 
    //Parameters Name : gridview,glinkid
    //Parameters Datatype :GridView,integer
    //Returns :None
    //Retun Datatype : None
    //Created By : Biswaranjan Das
    //Created Date : 14-June-2010
    //*****************************************************
    protected void FillGridview(GridView gridview, int glinkid, Char ActionCode)
    {

        IList<Primary> objPLinkList = new List<Primary>();
       objPLinkList= ObjAdminBal.FillGridview(glinkid, ActionCode, "0");

       totalRowCount = objPLinkList.Count;
       gridview.DataSource = objPLinkList;
        gridview.DataBind();
        RecCount = gridview.Rows.Count;
        DisplayPaging(totalRowCount, gridview, ActionCode);
        if (gridview.Rows.Count == 0)
        {
            btninactive.Visible = false;
            //btnDelete.Visible = false;
            btnmodify.Visible = false;
        }

    }
    #endregion

    #region "Dropdownlist Events"
    /// <summary>
    /// To Fill Sl no on Dropdown Selected Index Change
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlselGlink_SelectedIndexChanged(object sender, EventArgs e)
    {
        objPrimaryLink.ActionCode = "F";
        objPrimaryLink.PlinkId = "0";
        objPrimaryLink.GlinkId = Convert.ToInt32(ddlselGlink.SelectedValue);
        lblPSlNo.Text = ObjAdminBal.FillSLno(objPrimaryLink);
        //string strcript = "<script>HideShow('TabPrimaryLink_TabCreatePlink_rbtIntern','TabPrimaryLink_TabCreatePlink_rbtExtrn');</script>";
        //Page.RegisterStartupScript("javascript", strcript);
    }
    /// <summary>
    /// To fill gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInactiveShow_Click(object sender, EventArgs e)
    {
        lnkInactiveAll.Text = "All";
        GridInActivePlink.AllowPaging = true;

       
        if (ddlinactiveGlink.SelectedValue != "--select--")
        {
            GridInActivePlink.Visible = true;
            FillGridview(GridInActivePlink, Convert.ToInt32(ddlinactiveGlink.SelectedValue), 'I');
            if (GridInActivePlink.Rows.Count > 0)
            {
                btnactive.Visible = true;
            }
            else
            {
                btnactive.Visible = false;
            }
        }
        else
        {
            GridInActivePlink.Visible = false;
        }
    }
    /// <summary>
    /// To fill Gridview(Edit)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnShow_Click(object sender, EventArgs e)
    {
        lbtnAll.Text = "All";
        GridActivePlink.AllowPaging = true;

        GridActivePlink.Visible = true;
        ViewState["ddleditGlinkvalue"] = ddleditGlink.SelectedValue;
        if (ddleditGlink.SelectedValue != "--select--")
        {
            FillGridview(GridActivePlink, Convert.ToInt32(ddleditGlink.SelectedValue), 'P');
            if (GridActivePlink.Rows.Count == 0)
            {
                btnmodify.Visible = false;
                //btnDelete.Visible = false;
                btninactive.Visible = false;
            }
            else
            {
                btnmodify.Visible = true;
                //btnDelete.Visible = true;
                btninactive.Visible = true;
            }
        }
        else
        {
            GridActivePlink.Visible = false;
            btnmodify.Visible = false;
            //btnDelete.Visible = false;
            btninactive.Visible = false;
        }
 
    }
   
    #endregion

    #region "Button Events"
    /// <summary>
    /// To Add a primary link
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        AddPrimaryLink();
    }
    /// <summary>
    /// To Edit a primary link
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        EditPrimarylink(CommonFunction.DecryptData(Request.QueryString["Plink"].ToString()));


    }
    

    
    /// <summary>
    /// To Make Inactive a primary link
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btninactive_click(object sender, EventArgs e)
    {
        List<Primary> objList = new List<Primary>();
        for (int i = 0; i <= GridActivePlink.Rows.Count - 1; i++)
        {
            CheckBox chknews = default(CheckBox);
            chknews = (CheckBox)GridActivePlink.Rows[i].FindControl("chkplink");
            if ((chknews.Checked == true))
            {
                objPrimaryLink.ActionCode = "I";
                objPrimaryLink.PlinkId = Convert.ToString(GridActivePlink.DataKeys[i].Value);
                objPrimaryLink.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                intReturnValue = ObjAdminBal.InactivatePrimaryLink(objPrimaryLink);

            }
        }
        FillGridview(GridActivePlink, Convert.ToInt32(ddleditGlink.SelectedValue), 'P');
        if (intReturnValue == 7)
        {
            if (ConfigurationManager.AppSettings["XmlDel"].ToString() == "Y")
            {
                Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(string), "", "alert('" + StaticValues.message(intReturnValue, "PrimaryLink") + "');", true);
            TabPrimaryLink.ActiveTabIndex = 1;
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(string), "", "alert('Primary link has already been assigned,Can not be inactivated');", true);
            TabPrimaryLink.ActiveTabIndex = 1;
        }
    }
    /// <summary>
    /// To Modify a primary link with slno
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnmodify_click(object sender, EventArgs e)
    {
        for (int i = 0; i < GridActivePlink.Rows.Count; i++)
        {
            int glinkid = 0;
            CheckBox chkPLink = (CheckBox)GridActivePlink.Rows[i].FindControl("chkplink");
            System.Web.UI.WebControls.TextBox txtPlinkSlno = (System.Web.UI.WebControls.TextBox)GridActivePlink.Rows[i].FindControl("txtslno");

            if (chkPLink.Checked == true)
            {
                objPrimaryLink.ActionCode = "E";
                objPrimaryLink.PlinkId = Convert.ToString(GridActivePlink.DataKeys[i].Value);
                IList<Primary> objplst = ObjAdminBal.GetAllPrimaryLink(objPrimaryLink);
                foreach (Primary data in objplst)
                {
                    glinkid = data.GlinkId;
                }
                objPrimaryLink.ActionCode = "S";
                objPrimaryLink.PlinkId = Convert.ToString(GridActivePlink.DataKeys[i].Value);
                objPrimaryLink.GlinkId = glinkid;
                objPrimaryLink.SlNo = Convert.ToInt32(txtPlinkSlno.Text);
                objPrimaryLink.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                intReturnValue = ObjAdminBal.UpdateSlnoPrimaryLink(objPrimaryLink);
            }
        }
        if (intReturnValue == 9)
        {
            if (ConfigurationManager.AppSettings["XmlDel"] == "Y")
            {
                Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(string), "", "alert('" + StaticValues.message(intReturnValue, "PrimaryLink") + "');", true);
            TabPrimaryLink.ActiveTabIndex = 1;
        }
        else
        {
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(string), "", "alert('" + StaticValues.message(intReturnValue, "PrimaryLink") + "');", true);
        }
        FillGridview(GridActivePlink, Convert.ToInt32(ddleditGlink.SelectedValue), 'P');


    }
    /// <summary>
    /// To clear fields
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (btnReset.Text == "Reset")
        {
            ResetPrimarylink();
            //objCmnDll.ResetFunction(ddlselGlink, txtPlink, rbtExtrn, ddlselFunction, txtURL,txtPlinkNameInAmharic);
            ddlselGlink.SelectedIndex = -1;
            ddlselFunction.SelectedIndex = -1;
            txtPlink.Text = "";
            txtURL.Text = "";
            txtPlinkNameInAmharic.Text = "";
            rbtIntern.Checked = true;
            //rbtSame.Checked = true;
            //TRsecondlayer.Style.Add("display", "none");           
            TRurl.Style.Add("display", "none");
            TRfunction.Style.Add("display", "");
            chkShowHome.Checked = false;
        }
        else if (btnReset.Text == "Cancel")
        {
            //Code Added By Dilip Kumar Tripathy on dated  08-May-2013
            Session["argEdit"] = ddlselGlink.SelectedValue;
            TabPrimaryLink.ActiveTabIndex = 1;
            string strUrl = "adminPrimaryLink.aspx?pdx=" + Request.QueryString["pindex"].ToString();
            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "location.href='" + strUrl + "'", true);
        }
    }
    #endregion

    #region "Gridview Events"
    /// <summary>
    /// To Find and show the Level name 
    /// Modify By   : Dilip Kumar Tripathy
    /// Modify Date : 08-Mar-2013
    /// Modify Desc : Add a Trim(',') method to trim ',' when primary link is for "All" 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridActivePlink_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (StrOL == "")
            {
                GridActivePlink.Columns[3].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //string concatlocname = null;

                HyperLink hyper = new HyperLink();
                hyper = (HyperLink)e.Row.FindControl("hypEdit");
                hyper.NavigateUrl = "adminPrimaryLink.aspx?Plink=" + CommonFunction.EncryptData(GridActivePlink.DataKeys[e.Row.RowIndex].Value.ToString()) + "&pindex=" + GridActivePlink.PageIndex;
               
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[3].Text = "Primary Link(In " + StrOL + ")";

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    /// <summary>
    /// To Find and show the Level name in Active grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridInActivePlink_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (StrOL == "")
            {
                GridInActivePlink.Columns[3].Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[3].Text = "Primary Link(In " + StrOL + ")";

            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");
        }
    }
    protected void GridActivePlink_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridActivePlink.PageIndex = e.NewPageIndex;
        FillGridview(GridActivePlink, Convert.ToInt32(ddleditGlink.SelectedValue), 'P');
    }
    protected void GridInActivePlink_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridInActivePlink.PageIndex = e.NewPageIndex;
        FillGridview(GridInActivePlink, Convert.ToInt32(ddlinactiveGlink.SelectedValue), 'I');
        if (GridInActivePlink.Rows.Count > 0)
        {
            btnactive.Visible = true;
        }
    }
    #endregion

    #region Tabevents
    protected void TabPrimaryLink_ActiveTabChanged(object sender, EventArgs e)
    {
        txtPlink.Text = "";
        //lblPSlNo.Text = "***";
        chkShowHome.Checked = false;
        ddlselGlink.SelectedIndex = 0;
        rbtIntern.Checked = true;
        rbtExtrn.Checked = false;
        //ddlselFunction.SelectedValue = "0";
        //TRbrowser.Style.Add("display", "none");
        // lbLocation.SelectedItem.Text = "--Select--";
        ddleditGlink.Items.Clear();
        FillGlobalLink(ddleditGlink, "Edit");
        ddlinactiveGlink.Items.Clear();
        FillGlobalLink(ddlinactiveGlink, "Active");
        GridInActivePlink.Visible = false;
        GridActivePlink.Visible = false;
        //btnDelete.Visible = false;
        btninactive.Visible = false;
        btnmodify.Visible = false;
        btnAdd.Visible = true;
        btnEdit.Visible = false;
        TabCreatePlink.HeaderText = "CREATE";
        lblPaging.Visible = false;
        lbtnAll.Visible = false;
        btnactive.Visible = false;
        lbtnAll.Text = "All";
        lnkInactiveAll.Text = "All";
        GridActivePlink.AllowPaging = true;
        GridInActivePlink.AllowPaging = true;
    }
    #endregion
    #region "Google style paging format"
    private void DisplayPaging(int recCount, GridView grd, Char actionCode)
    {
        if (grd.Rows.Count > 0)
        {
            //Added By  : Dilip Kumar Tripathy on dated 25/01/2012
            //Modify By : Dilip Kumar Tripathy on dated 25/04/2012
            //Purpose   : Add actionCode parameter to display paging in InactiveGlink Grid
            if (actionCode == 'I')
            {
                this.lblInactivePaging.Visible = true;
                lnkInactiveAll.Visible = true;
                this.lblInactivePaging.Text = CommonFunction.ShowGridPaging(grd, grd.PageSize, grd.PageIndex, recCount);
            }
            else
            {
                this.lblPaging.Visible = true;
                lbtnAll.Visible = true;
                this.lblPaging.Text = CommonFunction.ShowGridPaging(grd, grd.PageSize, grd.PageIndex, recCount);
            }
        }
        else
        {
            if (actionCode == 'I')
            {
                this.lblInactivePaging.Visible = false;
                lnkInactiveAll.Visible = false;
            }
            else
            {
                this.lblPaging.Visible = false;
                lbtnAll.Visible = false;
            }
        }
    }
    #endregion
    protected void lbtnAll_Click(object sender, EventArgs e)
    {
        if (lbtnAll.Text == "All")
        {
            lbtnAll.Text = "Paging";
            this.GridActivePlink.PageIndex = 0;
            GridActivePlink.AllowPaging = false;
            FillGridview(GridActivePlink, Convert.ToInt32(ddleditGlink.SelectedValue), 'P');
            if (GridActivePlink.Rows.Count > 0)
            {
            
                this.lblPaging.Text = "1-" + GridActivePlink.Rows.Count.ToString() + " Of " + GridActivePlink.Rows.Count.ToString();
            }
        }
        else
        {
            lbtnAll.Text = "All";
            GridActivePlink.AllowPaging = true;
            FillGridview(GridActivePlink, Convert.ToInt32(ddleditGlink.SelectedValue), 'P');
        }

    }
    protected void lnkInactiveAll_Click(object sender, EventArgs e)
    {
        if (lnkInactiveAll.Text == "All")
        {
            lnkInactiveAll.Text = "Paging";
            this.GridInActivePlink.PageIndex = 0;
            GridInActivePlink.AllowPaging = false;
            FillGridview(GridInActivePlink, Convert.ToInt32(ddlinactiveGlink.SelectedValue), 'I');
            if (GridInActivePlink.Rows.Count > 0)
            {
                btnactive.Visible = true;
                this.lblPaging.Text = "1-" + GridInActivePlink.Rows.Count.ToString() + " Of " + GridInActivePlink.Rows.Count.ToString();
            }

        }
        else
        {
            lnkInactiveAll.Text = "All";
            GridInActivePlink.AllowPaging = true;
            FillGridview(GridInActivePlink, Convert.ToInt32(ddlinactiveGlink.SelectedValue), 'I');
            if (GridInActivePlink.Rows.Count > 0)
            {
                btnactive.Visible = true;
            }
        }
    }
    protected void btnactive_Click(object sender, EventArgs e)
    {

        for (int i = 0; i <= GridInActivePlink.Rows.Count - 1; i++)
        {
            CheckBox chknews = default(CheckBox);
            chknews = (CheckBox)GridInActivePlink.Rows[i].FindControl("chkplink");
            if ((chknews.Checked == true))
            {
                objPrimaryLink.ActionCode = "T";
                objPrimaryLink.PlinkId = Convert.ToString(GridInActivePlink.DataKeys[i].Value);
                objPrimaryLink.UpdatedBy = Convert.ToInt32(Session["UserId"]);
                intReturnValue = ObjAdminBal.ActivatePrimaryLink(objPrimaryLink);
            }
        }
       
      
        FillGridview(GridInActivePlink, Convert.ToInt32(ddlinactiveGlink.SelectedValue), 'I');
        //CreateUsersXML(Convert.ToInt32(Session["UserId"]));
        Session["argEdit"] = ddlinactiveGlink.SelectedValue;
        if (intReturnValue == 6)
        {
            if (ConfigurationManager.AppSettings["XmlDel"].ToString() == "Y")
            {
                Admin.CommonFunction.CommonFunction.DeleteUserXMLFile();
                CommonFunction.CreateUsersXML(Convert.ToInt32(Session["userid"]));
            }
          
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(string), "", "alert('" + StaticValues.message(intReturnValue, "PrimaryLink") + "');document.location.href='adminPrimaryLink.aspx';", true);
            TabPrimaryLink.ActiveTabIndex = 1;
        }
         
    }



    protected void rbtIntern_CheckedChanged(object sender, EventArgs e)
    {
        txtURL.Text = "";
    }

   



}

