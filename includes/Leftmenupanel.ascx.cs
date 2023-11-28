/*'*******************************************************************************************************************
'' File Name             :   Leftmenupanel.ascx.cs
'' Description           :   To Create Global link and Primary Link Dynamically
'' Created by            :   
'' Created On            :   
'' Modification History  :
''                           <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
''                              1                       15/10/2013          Mahesh Kumar Nayak          For URL encryption   
''                         
'' Function Name         :   
'' User Defined Namespace:  
*/
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;
using CSMPDK_3_0;
using System.Web.Services;
using System.Web.Script.Services;


public partial class includes_Leftmenupanel : System.Web.UI.UserControl
{

    #region Global Variable
    private int tempUser = 0;
    private string glink;
    private string plink = null;
    private string btnURL;
    private string BtnId;

    public string btn;
    public string tab = null;
    public string strSpName = string.Empty;
    private int i = 0;

    DataTable tblButton;
    DataTable tblTab;
    DataView dvw;
    string strconnectionstring = "AdminAppConnectionProd";
    CommonDLL objComm = new CommonDLL();
    string strTabaccess, strBtnaccess = null;
    //CGRCService.CGRCServiceClient objService = new CGRCServiceClient();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/LogOut.aspx", false);
            }
            else
            {
                int UserId = Convert.ToInt32(Session["UserId"].ToString());
              //  lblName.Text = Session["fullName"].ToString();
                //lblDesg.Text = objService.DesignationName(UserId);
                if (Session["strImage"] != "")
                {
                  //  imgUser.Src = "../Console/Manage_User/User_Image/" + Session["strImage"].ToString();
                    //Session["strImage"]
                }
                CreateNodesinULLI();
            }

        }

    }


    /// <summary>
    /// Function Created By Biswaranjan on 7-Sept-2010 to Get type of access of a concern plink(ex:Add,veiw,manage)
    /// </summary>
    /// <param name="Action"></param>
    /// <param name="Buttonid"></param>
    /// <param name="TabAccess"></param>
    private void CreateNodesinULLI()
    {
       litMenuStr.Text = string.Empty;
        StringBuilder sb = new StringBuilder();
        sb.Append(string.Format("<li class=\"treeview\"><a href=\"../Dashboard/Default.aspx\" class=\"\"><Img alt=\"{0}\" src=\"../images/{1}\" border=\"0\" align=\"absmiddle\"> {2} <span class=\"fa arrow\"></span></a>", "Dashboard", "../Images/dashboard_w.png", "Dashboard"));
        sb.Append(string.Format("</li>"));
        
        string strval;
        strval = Session["UserId"].ToString();

        DataSet ds = new DataSet();
        DataTable tblGLink = new DataTable();
        DataTable tblPLink = new DataTable();

        CommonDLL comDll = new CommonDLL();

        strSpName = "USP_GET_USER_PERMISSION_BY_ID";
        object[] objParams = { "P_INT_USER_ID", strval };
        ds = comDll.GetDataSet(strconnectionstring, strSpName, objParams);


        //ds = comDll.GetDataSet(strconnectionstring, "EXEC USP_GET_USER_PERMISSION_BY_ID " + strval, "System.Data.SqlClient");
        tblGLink = ds.Tables[1];
        tblPLink = ds.Tables[2];


        if (tblGLink.Rows.Count > 0)
        {
            for (int i = 0; i < tblGLink.Rows.Count; i++)
            {
                //sb.Append("<div class=\"glossymenu\">");
                //For display GLINK
                //string iconName = "1.gif"; //"Business.png";            
               // string glinkName = tblGLink.Rows[i]["VCH_GLINK_NAME"].ToString();
               // string altText = tblGLink.Rows[i]["VCH_GLINK_NAME"].ToString();
                string iconName = tblGLink.Rows[i]["INT_GLINK_ID"].ToString() + ".png"; //"Business.png";            
                string glinkName = "<span class='xn-text'>" + tblGLink.Rows[i]["VCH_GLINK_NAME"].ToString() + "</span>";
                string altText = tblGLink.Rows[i]["VCH_GLINK_NAME"].ToString();
                string strPlinkID = "0";
                string glinkId = tblGLink.Rows[i]["INT_GLINK_ID"].ToString();
                //sb.Append(string.Format("<a href=\"#\" class=\"menuitem submenuheader th_menuitembg\"><Img alt=\"{0}\" src=\"../images/{1}\" border=\"0\" align=\"absmiddle\"> {2} </a>", altText, iconName, glinkName));
                DataView dvw = new DataView();
                dvw = tblPLink.DefaultView;
                dvw.RowFilter = "INT_GLINK_ID='" + tblGLink.Rows[i]["INT_GLINK_ID"].ToString() + "'";
                DataTable tblPLink_Filtered = new DataTable();
                tblPLink_Filtered = dvw.ToTable();
                
                if (Request.QueryString["linkm"] != null)
                {
                    string strP = Admin.CommonFunction.CommonFunction.DecryptData(Request.QueryString["linkm"]);
                    

                    strPlinkID = strP;
                }
                if (Request.QueryString["linkm"] != null)
                {
                    if (tblGLink.Rows[i]["INT_GLINK_ID"].ToString() == strPlinkID)
                    {
                        sb.Append(string.Format("<li class=\"treeview active\"><a href=\"#\" class=\"\"><Img alt=\"{0}\" src=\"../images/{1}\" border=\"0\" align=\"absmiddle\"> {2} <span class=\"fa arrow\"></span></a>", altText, iconName, glinkName));
                    }
                    else
                    {
                        sb.Append(string.Format("<li class=\"treeview\"><a href=\"#\" class=\"\"><Img alt=\"{0}\" src=\"../images/{1}\" border=\"0\" align=\"absmiddle\"> {2} <span class=\"fa arrow\"></span></a>", altText, iconName, glinkName));
                    }
                }
                else
                {
                    sb.Append(string.Format("<li class=\"treeview\"><a href=\"#\" class=\"\"><Img alt=\"{0}\" src=\"../images/{1}\" border=\"0\" align=\"absmiddle\"> {2} <span class=\"fa arrow\"></span></a>", altText, iconName, glinkName));
                }
                if (tblPLink_Filtered.Rows.Count > 0)
                {
                    sb.Append(string.Format("<ul class=\"treeview-menu\">"));

                    for (int j = 0; j < tblPLink_Filtered.Rows.Count; j++)
                    {
                        string plinkName, plinkId, pFileName, btnId, tabId, functionid = "";

                        plinkName = tblPLink_Filtered.Rows[j]["VCH_PLINK_NAME"].ToString();
                        plinkId = tblPLink_Filtered.Rows[j]["INT_PLINK_ID"].ToString();

                        tblButton = ds.Tables[3];
                        tblTab = ds.Tables[4];
                        pFileName = CreateURL(glinkId, plinkId);
                        if (pFileName == "")
                        {
                            pFileName = tblPLink_Filtered.Rows[j]["VCH_FILE_NAME"].ToString() + "?linkm=" + Admin.CommonFunction.CommonFunction.EncryptData( glink) + "&linkn=" + Admin.CommonFunction.CommonFunction.EncryptData( plink )+ "&btn=0&tab=0";
                        }

                        btnId = tblPLink_Filtered.Rows[j]["INT_LNKBTN_ID"].ToString();
                        tabId = tblPLink_Filtered.Rows[j]["INT_LNKTAB_ID"].ToString();
                        functionid = tblPLink_Filtered.Rows[j]["INT_FUNCTION_ID"].ToString();
                        string url;
                        string strchek = string.Empty;
                        if (functionid != "0")
                        {
                            url = "../" + pFileName;
                        }
                        else
                        {
                            strchek = "target=\"_balnk\"";
                            url = pFileName;
                        }
                        string name = plinkName;
                        sb.Append(string.Format("<li><a  href=\"{0}\" {2} >{1} </a></li>", url, name, strchek));

                    }

                   

                }
                sb.Append(string.Format("</ul></li>"));
            }
            litMenuStr.Text = sb.ToString();
        }
    }
       
    protected string CreateURL(string Glinkid, string Plinkid)
    {
        btnURL = "";
        string strFrstBtnID = "";
        DataTable DTbtn = null;
        DataTable DTtab = null;
        int inttabID = 0;
        glink = Glinkid;
        plink = Plinkid;
        if (!object.ReferenceEquals(Session["UserId"], "") && Session["UserId"] != null)
        {
            tempUser = Convert.ToInt32(Session["UserId"]);
        }
        try
        {
            //GetAccessRights("A", Convert.ToInt32(plink), tempUser);
            DTbtn = GetButtons(strBtnaccess, Convert.ToInt32(plink));
            if (DTbtn.Rows.Count > 0)
            {
                if (Convert.ToInt32(DTbtn.Rows[0].ItemArray[3]) == 1)
                {
                    if (i != 1)
                    {
                        strFrstBtnID = DTbtn.Rows[0].ItemArray[0].ToString();
                        i += 1;
                    }
                    //Code To Get the URL of first tab
                    BtnId = DTbtn.Rows[0].ItemArray[0].ToString();
                    //objTabbtncreate.chr_ActionCode = "B";
                    //objTabbtncreate.vchTabAccess = strTabaccess;
                    //objTabbtncreate.intBtnid = Convert.ToInt32(BtnId);
                    //DTtab = GetTabs(strTabaccess, Convert.ToInt32(BtnId));
                    //DTtab = GetTab("B", strTabaccess, Convert.ToInt32(BtnId));
                    //if (DTtab.Rows.Count > 0)
                    //{
                    //    inttabID = Convert.ToInt32(DTtab.Rows[0].ItemArray[0]);
                    //    if (DTtab.Rows[0].ItemArray[3].ToString().Contains("?"))
                    //    {
                    //        btnURL = DTtab.Rows[0].ItemArray[3].ToString() + "&linkm=" + Admin.CommonFunction.CommonFunction.EncryptData(glink) + "&linkn=" + Admin.CommonFunction.CommonFunction.EncryptData(plink) + "&btn=" + Admin.CommonFunction.CommonFunction.EncryptData(DTtab.Rows[0].ItemArray[1].ToString()) + "&tab=" + Admin.CommonFunction.CommonFunction.EncryptData(inttabID.ToString());
                    //    }
                    //    else
                    //    {
                    //        btnURL = DTtab.Rows[0].ItemArray[3].ToString() + "?linkm=" + Admin.CommonFunction.CommonFunction.EncryptData(glink) + "&linkn=" + Admin.CommonFunction.CommonFunction.EncryptData(plink) + "&btn=" + Admin.CommonFunction.CommonFunction.EncryptData(DTtab.Rows[0].ItemArray[1].ToString()) + "&tab=" + Admin.CommonFunction.CommonFunction.EncryptData(inttabID.ToString()); ;
                    //    }
                    //}
                    //else
                    //{
                    //    if (DTbtn.Rows[0].ItemArray[2].ToString().Contains("?"))
                    //    {
                    //        btnURL = DTbtn.Rows[0].ItemArray[2].ToString() + "&linkm=" + Admin.CommonFunction.CommonFunction.EncryptData(glink) + "&linkn=" + Admin.CommonFunction.CommonFunction.EncryptData(plink) + "&btn=" + Admin.CommonFunction.CommonFunction.EncryptData(DTbtn.Rows[0].ItemArray[0].ToString()) + "&tab=" + Admin.CommonFunction.CommonFunction.EncryptData(inttabID.ToString());
                    //    }
                    //    else
                    //    {
                    //        btnURL = DTbtn.Rows[0].ItemArray[2].ToString() + "?linkm=" + Admin.CommonFunction.CommonFunction.EncryptData(glink) + "&linkn=" + Admin.CommonFunction.CommonFunction.EncryptData(plink) + "&btn=" + Admin.CommonFunction.CommonFunction.EncryptData(DTbtn.Rows[0].ItemArray[0].ToString()) + "&tab=" + Admin.CommonFunction.CommonFunction.EncryptData(inttabID.ToString());
                    //    }
                    //}
                }
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
            //Response.Write(ex.Message);
        }
        return btnURL;
    }

    private void GetAccessRights(string Action, int Plinkid, int Userid)
    {
        try
        {
            string[] StrAccess = null;

            strSpName = "usp_Tab_Btn_Create";
            object[] objParams = { "chr_ActionCode", Action,
                                    "IntPlinkID",Plinkid,
                                    "IntUserid",Userid};


            //string strQuery = "EXEC usp_Tab_Btn_Create '" + Action + "','" + Plinkid + "',0,'" + Userid + "'";
            //DbDataReader objDbreader = (DbDataReader)objComm.ExeReader(strconnectionstring, strQuery, "System.Data.SqlClient");
            DbDataReader objDbreader = (DbDataReader)objComm.ExeReader(strconnectionstring, strSpName, objParams);

            if (objDbreader.Read())
            {
                string strReturnval = objDbreader[0].ToString() == DBNull.Value.ToString() ? "" : objDbreader[0].ToString();
                if (strReturnval == "")
                {
                    strReturnval = "blank~space";
                    StrAccess = strReturnval.Split('~');
                    StrAccess[0] = "";
                    StrAccess[1] = "";
                }
                else
                {
                    StrAccess = strReturnval.Split('~');
                }

                strTabaccess = StrAccess[0];
                strBtnaccess = StrAccess[1];
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Execution Failed", ex);
        }
    }

    private DataTable GetButtons(string strAccess, int intPlinkId)
    {
        DataTable objDt = new DataTable();
        string strField = "";
        string strFilter = "";
        if (strAccess == "M")
        {
            strAccess = "Y";
            strField = "CHR_MANAGE";
            strFilter = "  AND " + strField + "='" + strAccess + "'";
        }
        else if (strAccess == "A")
        {
            strAccess = "Y";
            strField = "CHR_ADD";
            strFilter = "  AND " + strField + "='" + strAccess + "'";
        }
        else if (strAccess == "V")
        {
            strAccess = "Y";
            strField = "CHR_VIEW";
            strFilter = "  AND " + strField + "='" + strAccess + "'";
        }


        dvw = tblButton.DefaultView;
        dvw.RowFilter = "INT_PLINK_ID='" + intPlinkId + "'" + strFilter;
        tblButton = dvw.ToTable();

        objDt.Columns.Add("int_ButtonId");
        objDt.Columns.Add("nvch_Button");
        objDt.Columns.Add("vch_Url");
        objDt.Columns.Add("int_TabAvail");

        if (tblButton.Rows.Count > 0)
        {
            for (int index = 0; index <= tblButton.Rows.Count - 1; index++)
            {
                DataRow dtRow = objDt.NewRow();
                dtRow[0] = tblButton.Rows[index]["INT_BUTTON_ID"].ToString();
                dtRow[1] = tblButton.Rows[index]["VCH_BUTTON_NAME"].ToString();
                dtRow[2] = tblButton.Rows[index]["VCH_FILE_NAME"].ToString();
                dtRow[3] = tblButton.Rows[index]["INT_TAB_AVAIL"].ToString();
                objDt.Rows.Add(dtRow);
            }
        }
        return objDt;

    }

    public DataTable GetTab(string strAction, string tabAccess, int intButtonId)
    {
        DataTable dt = new DataTable();
        try
        {
            CommonDLL comDll = new CommonDLL();
            strSpName = "usp_Tab_Btn_Create";
            object[] objParams = { "chr_ActionCode", strAction,
                                    "vchTabAccess",tabAccess,
                                    "vchBtnAccess",intButtonId};

            //string strQuery = "EXEC usp_Tab_Btn_Create '" + strAction + "',0,0,0," + intButtonId + ",'" + tabAccess + "'";
            //dt = comDll.GetDataTable(strconnectionstring, strQuery, "System.Data.SqlClient");
            dt = comDll.GetDataTable(strconnectionstring, strSpName, objParams);
        }
        catch (Exception exception)
        {
            throw new Exception("Execution Failed", exception);
        }
        return dt;
    }



}





