/********************************************************************************************************************
' File Name             :   AdminDefault.aspx.cs
' Description           :   To Add/edit/Delete Hierarchy And Level.
' Created by            :   Subhasis Kumar dash
' Created On            :   11-Jul-2010
' Modification History  :
'                           <CR no.>                    <Date>              <Modified by>                <Modification Summary>'                                                          
'                            1                          12-Mar-2012         Dilip Tripathy                 Adding ArrayList varivle alLoc to store the location name as per Loc Admin
'                            2                          16-Mar-2012         Ashish Patnaik                 Adding of dynamic node
'                            3                          23-Mar-2012         Ashish Patnaik                 Putting validation for adding dynamic node
'                            4                          27-Jun-2012         Dilip Tripathy                 Adding the request url code for Edit,Add,Delete button

' Function Name         :   
' Procedures Used       :    
' User Defined Namespace:   
' Inherited classes     :                                              
**********************************************************************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
//using Admin.CommonFunction;
//using CSMPDK_3_0;
using KWAdminConsole.Messages;
//using System.Xml.Resolvers;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using Admin.CommonFunction;
using Microsoft.Xml.XQuery; 
public partial class AdminDefault : System.Web.UI.Page
{
    #region "Variable Declaration"
    //CommonDLL objCmnDll = new CommonDLL();
    public string subNdHeader;
    public static int selectIndex;
    string strPath;
    public static TreeNode trChildNode;
    public static string NodeVal;
    string strInnerXml;
    static ArrayList alLoc = new ArrayList();
    public string xqry;
    public static string totalNo;
    public int intOutPut;
    public int IntEdretVal;
    static int intSessinCnt = 1;
    static int intFlagCnt = 0;
    AdminAppService Objadminbal = new AdminAppService();
    Hierarchy objHierarchyConfig = new Hierarchy();
    #endregion

    #region "Page Load"
    protected void Page_Load(object sender, EventArgs e)
    {
        
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
        Response.Cache.SetNoStore();
        //objAdmin = objKwantify.CreateAdminConsole();
        //objHierarchy = objAdmin.CreateHirarchy();
        //objHierarchyConfig = objHierarchy.CreateHierarchyConfig();

        CheckIsSession();

        if (!IsPostBack)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            string strBrowserUrl = Request.Url.ToString();
            ViewState["cnt"] = 1;
            //if (CommonFunction.UrlSpecialCharChecking(strBrowserUrl))
            //{
            //   // ClientScript.RegisterStartupScript(GetType(), "", "alert('Url never accept special character.'); window.location='" + Request.ApplicationPath + "/Default.aspx';", true);
            //    Response.Redirect("~/SessionRedirect.aspx");

            //}

            //Code added by Dilip Kumar Tripathy on dated 20-June-2012
            //purpose to provide security
            if (Request.QueryString["kwt0e"] == null)
            {
                if (Request.QueryString["dwXb"] != null && Session["RandomNo"] != null)
                {
                    if (Session["RandomNo"].ToString() == Request.QueryString["dwXb"].ToString())
                    {
                        //Session["RandomNo"] = CommonFunction.GenerateRandomNum();
                    }
                    else
                    {
                        Response.Redirect("~/SessionRedirect.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/SessionRedirect.aspx");
                }
            }
            else
            {
                if (Convert.ToInt16(CommonFunction.DecryptData(Request.QueryString["kwt0e"].ToString())) > 2)
                {
                    Response.Redirect("~/SessionRedirect.aspx");
                }
            }
            //code ended on same date
            txtrootName.ReadOnly = true;
            txtrootSummary.ReadOnly = true;
            Session["txtValues"] = null;
            Session["hidcid"] = 0;
            alLoc = Admin.CommonFunction.CommonFunction.GetUserLocation(int.Parse(Session["UserId"].ToString()));
            btnEdit.Visible = false;
            btnAdd.Visible = false;

            btnClear.Visible = false;
            btnDelete.Visible = false;
            btnshow.Visible = false;

            loadXmlData();
            btnAdd.BorderColor = System.Drawing.Color.Black;
            txtrootName.Focus();
            //Code Added By Dilip Kumar Tripathy On Dated 4-Feb-2012
            if (Request.QueryString["kwt0e"] != null)
            {
                ShowLevels(int.Parse(CommonFunction.DecryptData(Request.QueryString["kwt0e"].ToString())));
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "", "ShowHideTrDiv(0)", true);
            }
            //Code ended by   By Dilip Kumar Tripathy On same date
        }
        string s = txtsubnodeNo.Text;
        txtsubnodeNo.Focus();
        Page.Form.DefaultButton = btnEdit.ClientID;
    }
    #endregion

    #region "Button Events"
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (CheckDuplicateEntry() != true)
        {
            AddSubnode();
            loadXmlData();
            string url = "AdminDefault.aspx?dwXb=" + Session["RandomNo"].ToString();
            ClientScript.RegisterClientScriptBlock(GetType(), "", "document.location.href='" + url + "';", true);

        }
        else
        {
            string url = "AdminDefault.aspx?dwXb=" + Session["RandomNo"].ToString();
            ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('Duplicate entry is not allowed.');", true);
        }


    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
        loadXmlData();
    }
    ///<summary>    
    /// Function Name   : CheckDuplicateEntry
    /// Parameters Name : N/A
    /// parameters Type : N/A
    /// Return Type     : bool
    /// Purpose         : To check the duplicate entry while creating hierarchy
    /// Created By      : Dilip Kumar Tripathy
    /// Date            : 23-Jul-2012
    /// </summary>
    public bool CheckDuplicateEntry()
    {
        // TextBox txtDemo = (TextBox)tblnode.Rows[1].Cells[2].Controls[0];
        string[] arrHierarchyName = new string[tblnode.Rows.Count / 2];
        bool returnValue = false;
        List<string> vals = new List<string>();
        int j = 0;
        for (int i = 0; i < tblnode.Rows.Count; i++)
        {
            if ((i + 1) % 2 != 0)
            {
                arrHierarchyName[j] = (tblnode.Rows[i].Cells[2].Controls[0] as TextBox).Text;
                j++;
            }
        }
        foreach (string s in arrHierarchyName)
        {
            if (vals.Contains(s))
            {
                returnValue = true;
                break;
            }
            vals.Add(s);
        }
        return returnValue;
    }
    ///<summary>    
    /// Function Name   : CheckInitialWhiteSpace
    /// Parameters Name : N/A
    /// parameters Type : N/A
    /// Return Type     : bool
    /// Purpose         : To check the duplicate entry while creating hierarchy
    /// Created By      : Dilip Kumar Tripathy
    /// Date            : 04-May-2013
    /// </summary>
    public bool CheckInitialWhiteSpace()
    {
        // TextBox txtDemo = (TextBox)tblnode.Rows[1].Cells[2].Controls[0];
        string[] arrHierarchyName = new string[tblnode.Rows.Count / 2];
        bool returnValue = false;
        List<string> vals = new List<string>();
        int j = 0;
        for (int i = 0; i < tblnode.Rows.Count; i++)
        {
            if ((i + 1) % 2 != 0)
            {
                arrHierarchyName[j] = (tblnode.Rows[i].Cells[2].Controls[0] as TextBox).Text;
                j++;
            }
        }
        foreach (string s in arrHierarchyName)
        {
            if (s[0].ToString() == " ")
            {
                return true;
            }

        }
        return returnValue;
    }

    private void CheckIsSession()
    {
        try
        {
            if (Session["UserId"] == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Your Project Does not contain a session as UserId.');location.href='" + ConfigurationManager.AppSettings["Logout"].ToString() + "'", true);
            }
            if (Session["LevelID"] == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Your Project Does not contain a session as LevelID.');location.href='" + ConfigurationManager.AppSettings["Logout"].ToString() + "'", true);
            }
            if (Session["locId"] == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Your Project Does not contain a session as locId.');location.href='" + ConfigurationManager.AppSettings["Logout"].ToString() + "'", true);
            }
            if (Session["DeptId"] == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Your Project Does not contain a session as DeptId'.);location.href='" + ConfigurationManager.AppSettings["Logout"].ToString() + "'", true);
            }
            if (Session["SubDept"] == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Your Project Does not contain a session as SubDept.');location.href='" + ConfigurationManager.AppSettings["Logout"].ToString() + "'", true);
            }
            if (Session["userName"] == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Your Project Does not contain a session as userName.');location.href='" + ConfigurationManager.AppSettings["Logout"].ToString() + "'", true);
            }
            if (Session["adminstat"] == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Your Project Does not contain a session as adminstat.');location.href='" + ConfigurationManager.AppSettings["Logout"].ToString() + "'", true);
            }
            else
            {
                if (Session["adminstat"].ToString().ToUpper() != "SUPER" && Session["adminstat"].ToString().ToUpper() != "LOC")
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "alert('The adminstat session value is not SUPER or LOC.');location.href='" + ConfigurationManager.AppSettings["Logout"].ToString() + "'", true);
                }
            }
            if (Session["fullName"] == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Your Project Does not contain a session as fullName'.);location.href='" + ConfigurationManager.AppSettings["Logout"].ToString() + "'", true);
            }
            if (Session["menuCnt"] == null)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Your Project Does not contain a session as menuCnt.');location.href='" + ConfigurationManager.AppSettings["Logout"].ToString() + "'", true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(GetType(), "", "alert('" + ex.Message + "')", true);
        }
    }
    private int IsBlankField()
    {
        int intCnt = 0;
        for (int i = 0; i < tblnode.Rows.Count; i++)
        {
            if ((i + 1) % 2 != 0)
            {
                intCnt += 1;
                if ((tblnode.Rows[i].Cells[2].Controls[0] as TextBox).Text == string.Empty)
                {
                    (tblnode.Rows[i].Cells[2].Controls[0] as TextBox).Focus();
                    return intCnt;
                }
            }
        }
        return 0;
    }
    private void ClearFields()
    {
        for (int i = 0; i < tblnode.Rows.Count; i++)
        {
            (tblnode.Rows[i].Cells[2].Controls[0] as TextBox).Text = string.Empty;
        }
    }
    private int CheckStatus(ArrayList dynamicName, ArrayList dynamicAlias)
    {
        int intStat = 0;
        string[] strSpecialChar = { "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")",  ",", "'", ":" };
        string[] strDigits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        ArrayList alDigit = new ArrayList(strDigits);

        for (int i = 0; i < dynamicName.Count; i++)
        {
            string strName = dynamicName[i].ToString();
            string strAlias = dynamicAlias[i].ToString();
            for (int j = 0; j < strSpecialChar.Length; j++)
            {
                if (strName != "")
                {
                    if (strName.Contains(strSpecialChar[j].ToString()) || strAlias.Contains(strSpecialChar[j].ToString()))
                    {
                        intStat = 1;
                    }
                    if (alDigit.Contains(strName[0].ToString()))
                    {
                        intStat = 2;
                    }
                    if (strAlias != "")
                    {
                        if (alDigit.Contains(strAlias[0].ToString()))
                        {
                            intStat = 2;
                        }
                    }
                }
                else
                {
                    intStat = 3;
                }
            }
        }
        return intStat;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        int intBlankNo = IsBlankField();
        if (intBlankNo == 0)
        {
            if (CheckDuplicateEntry() != true)
            {
                if (CheckInitialWhiteSpace() != true)
                {
                    ArrayList dynamicName = new ArrayList();
                    ArrayList dynamicAlias = new ArrayList();
                    int intStatus = 0;
                    string[] strSpecialChar = { "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")",  ",", "'", ":" };
                    string[] strDigits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    ArrayList alDigit = new ArrayList(strDigits);
                    for (int i = 1; i <= Convert.ToInt32(txtsubnodeNo.Text) * 2; i++)
                    {
                        TextBox txtName = (TextBox)tblnode.Rows[i - 1].Cells[2].Controls[0];
                        if (i % 2 == 0)
                        {
                            dynamicAlias.Add(txtName.Text);
                        }
                        else
                        {
                            dynamicName.Add(txtName.Text);
                        }
                    }
                    intStatus = CheckStatus(dynamicName, dynamicAlias);
                    if (intStatus == 0)
                    {
                        EditSubnode(dynamicName, dynamicAlias);
                        loadXmlData();
                        string url = null;
                        if (Request.QueryString["kwt0e"] != null)
                        {
                            url = "AdminDefault.aspx?dwXb=" + Session["RandomNo"].ToString() + "&kwt0e=" + Request.QueryString["kwt0e"].ToString();
                        }
                        else
                        {
                            url = "AdminDefault.aspx?dwXb=" + Session["RandomNo"].ToString();
                        }
                        Response.Write("<script>document.location.href='" + url + "';</script>");
                        //ClientScript.RegisterClientScriptBlock(GetType(), "", "document.location.href='" + url + "';", true);
                    }
                    else
                    {
                        if (intStatus == 1)
                        {
                            // Response.Write("<script>window.alert('Special Characters are not allowed !');</script>");
                            ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('Special Characters are not allowed !');", true);
                        }
                        else if (intStatus == 2)
                        {
                            //Response.Write("<script>window.alert('Digits are not accepted as first Character !');</script>");
                            ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('Digits are not accepted as first Character !');", true);
                        }
                        else if (intStatus == 3)
                        {
                            //Response.Write("<script>window.alert('Hierarchy name can not be left blank !');</script>");
                            ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('Hierarchy name can not be left blank  !');", true);
                        }
                    }
                }
                else
                {
                    string url = "AdminDefault.aspx?dwXb=" + Session["RandomNo"].ToString();
                    ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('White space(s) are not allowed at Initial Position.');", true);
                }
            }
            else
            {
                string url = "AdminDefault.aspx?dwXb=" + Session["RandomNo"].ToString();
                ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('Duplicate entry is not allowed.');", true);
            }
        }
        else
        {
            string url = "AdminDefault.aspx?dwXb=" + Session["RandomNo"].ToString();
            ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('Node" + intBlankNo.ToString() + " name can not be left blank.');", true);
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        DeleteData(selectIndex, "U");
        loadXmlData();
        btnAdd.Visible = false;
        btnEdit.Visible = false;
        btnDelete.Visible = false;
        btnClear.Visible = false;
        btnshow.Visible = false;//modified by Dilip Tripathy from true to false on 7-Mar-2012
        string url = "AdminDefault.aspx?dwXb=" + Session["RandomNo"].ToString();
        Response.Write("<script>document.location.href='" + url + "'</script>");
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        //btnAdd.Visible = false;
        //btnEdit.Visible = false;
        //btnDelete.Visible = false;
        //btnClear1.Visible = false;
        //btnshow.Visible = false;//modified by Dilip Tripathy from true to false on 7-Mar-2012
        ClearFields();
    }
    #endregion

    #region "User functions"
    /// <summary>
    /// Load the treeview.
    /// </summary>
    protected void loadXmlData()
    {
        try
        {
            // SECTION 1. Create a DOM Document and load the XML data into it.
            XmlDocument dom = new XmlDocument();
            dom.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            // SECTION 2. Initialize the TreeView control.
            treemenu.Nodes.Clear();
            treemenu.Nodes.Add(new TreeNode(dom.DocumentElement.Name));
            TreeNode tNode = new TreeNode();
            tNode = treemenu.Nodes[0];
            // SECTION 3. Populate the TreeView with the DOM nodes.
            AddXmlNode(dom.DocumentElement, tNode, 0);
            treemenu.ExpandAll();
            if (treemenu.Nodes[0].ChildNodes.Count > 0)
            {
                treemenu.Nodes[0].ChildNodes[0].SelectAction = TreeNodeSelectAction.None;
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    /// <summary>
    /// Add the subnode values to tree.
    /// </summary>
    /// <param name="inXmlNode"></param>
    /// <param name="inTreeNode"></param>
    /// <param name="NodeIndex"></param>
    private void AddXmlNode(XmlNode inXmlNode, TreeNode inTreeNode, int NodeIndex)
    {
        try
        {
            int j = 0;
            //if (inXmlNode.Name == "ManageHierarchy" || inXmlNode.Name == ConfigurationManager.AppSettings["HierMenuName"].ToString() || inXmlNode.Name == "Menu" || inXmlNode.ParentNode.Name == "ManageHierarchy" || inXmlNode.ParentNode.ParentNode.Name == "ManageHierarchy" || inXmlNode.ParentNode.ParentNode.ParentNode.Name == "ManageHierarchy" || inXmlNode.ParentNode.ParentNode.ParentNode.ParentNode.Name == "ManageHierarchy")
            //{
                XmlNode xNode;
                TreeNode tNode;
                XmlNodeList nodeList;
                int i;
                // Loop through the XML nodes until the leaf is reached.
                // Adding nodes to the TreeView during the looping process.
                // inTreeNode.SelectAction = TreeNodeSelectAction.None;
                treemenu.Nodes[0].SelectAction = TreeNodeSelectAction.None;
                if (inXmlNode.HasChildNodes)
                {
                    nodeList = inXmlNode.ChildNodes;
                    for (i = 0; i <= nodeList.Count - 1; i++)
                    {
                        if (inXmlNode.Name == ConfigurationManager.AppSettings["HierMenuName"].ToString())
                        {
                            xNode = inXmlNode.ChildNodes[0];
                            if (i > 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (inXmlNode.Name == "ManageHierarchy")
                            {
                                if (alLoc.Contains(inXmlNode.ChildNodes[i].Attributes["NAME"].Value))
                                {
                                    xNode = inXmlNode.ChildNodes[i];
                                }
                                else
                                {
                                    xNode = null;
                                }
                            }
                            else
                            {
                                xNode = inXmlNode.ChildNodes[i];
                            }
                        }
                        if (xNode != null)
                        {
                            if (xNode.Attributes["FLAG"].Value != "1")
                            {
                                if (xNode.Name == "ManageHierarchy")
                                {
                                    if (Session["adminstat"].ToString().ToLower() == "super")
                                    {
                                        inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes["NAME"].Value, xNode.Attributes["ID"].Value));
                                    }
                                    else
                                    {
                                        TreeNode objNode = new TreeNode(xNode.Attributes["NAME"].Value, xNode.Attributes["ID"].Value);
                                        objNode.SelectAction = TreeNodeSelectAction.None;
                                        inTreeNode.ChildNodes.Add(objNode);
                                    }
                                }
                                else
                                {
                                    if (xNode.Name == ConfigurationManager.AppSettings["HierMenuName"].ToString())
                                    {
                                        inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes["NAME"].Value, xNode.Attributes["ID"].Value));
                                    }
                                    else
                                    {
                                        inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes["NAME"].Value, xNode.Attributes["ID"].Value));
                                    }
                                }

                                tNode = inTreeNode.ChildNodes[j];
                                j++;
                                if (xNode.InnerText.Trim() != "")
                                {
                                    AddXmlNode(xNode, tNode, 1);
                                }
                                else
                                {
                                    AddXmlNode(xNode, tNode, 0);
                                }
                            }
                        }
                    }
                }
                else
                {
                    // pulling the data from the XmlNode based on the
                    // type of node, whether attribute values are required, and so forth.
                    if (NodeIndex == 0)
                    {
                        inTreeNode.Text = (inXmlNode.Attributes["NAME"].Value.Replace("2F", "/")).Trim();
                    }
                    else
                    {
                        inTreeNode.Text = (inXmlNode.OuterXml).Trim();
                    }
                }
           // }

        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }

    }
    /// <summary>
    /// Adding the New node to xml document
    /// </summary>
    public void AddSubnode()
    {
        try
        {
            ArrayList dynamicName = new ArrayList();
            ArrayList dynamicAlias = new ArrayList();
            int intStat = 0;
            string[] strSpecialChar = { @"\", "~", "`", "!", "@", "#", "$", "%", "^", "&", "*",  ",", "(", ")", "'", ":" };
            string[] strDigits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            ArrayList alDigit = new ArrayList(strDigits);

            for (int i = 1; i <= Convert.ToInt32(txtsubnodeNo.Text) * 2; i++)
            {
                TextBox txtName = (TextBox)tblnode.Rows[i - 1].Cells[2].Controls[0];
                if (i % 2 == 0)
                {
                    dynamicAlias.Add(txtName.Text);
                }
                else
                {
                    dynamicName.Add(txtName.Text);
                }
            }
            for (int i = 0; i < dynamicName.Count; i++)
            {
                string strName = dynamicName[i].ToString();
                string strAlias = dynamicAlias[i].ToString();
                for (int j = 0; j < strSpecialChar.Length; j++)
                {
                    if (strName.Contains(strSpecialChar[j].ToString()) || strAlias.Contains(strSpecialChar[j].ToString()))
                    {
                        intStat = 1;
                    }
                    if (alDigit.Contains(strName[0].ToString()))
                    {
                        intStat = 2;
                    }
                    if (strAlias != "")
                    {
                        if (alDigit.Contains(strAlias[0].ToString()))
                        {
                            intStat = 2;
                        }
                    }
                }
            }
            if (intStat == 0)
            {
                // Create an XML document instance, and load XML data.
                XmlDocument xmlDoc = new XmlDocument();
                // This code assumes that the XML file is in the same folder.
                xmlDoc.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
                //Create a new Element
                XmlElement newElem = null;
                bool nodeAdded = false;
                if (txtsubnodeNo.Text == "")
                {
                    txtsubnodeNo.Text = "0";
                }
                if (xmlDoc.GetElementsByTagName(ConfigurationManager.AppSettings["HierMenuName"].ToString()).Count < 1)
                {
                    newElem = xmlDoc.CreateElement(ConfigurationManager.AppSettings["HierMenuName"].ToString());
                    XmlAttribute xAttr = xmlDoc.CreateAttribute("NAME");
                    xAttr.Value = ConfigurationManager.AppSettings["HierMenuName"].ToString();
                    newElem.Attributes.Append(xAttr);
                    strInnerXml = "";
                }
                //Adding Innerxml value to Element.
                else
                {
                    newElem = (XmlElement)xmlDoc.GetElementsByTagName(ConfigurationManager.AppSettings["HierMenuName"].ToString())[0];
                    strInnerXml = newElem.InnerXml;
                }
                if (strInnerXml.Contains(txtrootName.Text.Replace(" ", "")))
                {
                    newElem.InnerXml = strInnerXml;
                }
                else
                {
                    int intVal = 0;
                    if (xmlDoc.DocumentElement.HasChildNodes)
                    {
                        if (xmlDoc.GetElementsByTagName(ConfigurationManager.AppSettings["HierMenuName"].ToString())[0].ChildNodes.Count > 0)
                        {
                            foreach (XmlNode xNode in xmlDoc.GetElementsByTagName(ConfigurationManager.AppSettings["HierMenuName"].ToString())[0].ChildNodes)
                            {
                                if (Convert.ToInt32(xNode.Attributes["ID"].Value) > intVal)
                                {
                                    intVal = Convert.ToInt32(xNode.Attributes["ID"].Value);
                                }
                            }
                        }
                    }

                    newElem.InnerXml = strInnerXml + "<" + txtrootName.Text.Replace(" ", "").Replace("/", "2F") + " ID='" + Convert.ToString(intVal + 1) + "' HID='0' TOT='" + txtsubnodeNo.Text + "' LID='0' PID='0' FLAG='0'  NAME='" + txtrootName.Text.Trim() + "' DESC='" + txtrootSummary.Text + "' ></" + txtrootName.Text.Replace(" ", "").Replace("/", "2F") + ">";
                }
                XmlAttribute Kattr = xmlDoc.CreateAttribute("ID");
                if (!xmlDoc.DocumentElement.HasChildNodes)
                {
                    Kattr.Value = "0";
                    newElem.Attributes.Append(Kattr);
                    xmlDoc.DocumentElement.AppendChild(newElem);
                }
                else
                {
                    xmlDoc.DocumentElement.AppendChild(newElem);
                }
                //added
                int maxLid = 0;
                if (xmlDoc.DocumentElement.FirstChild.FirstChild.HasChildNodes)
                {
                    maxLid = GetMaxLid();
                }
                else
                {
                    maxLid = 0;
                }

                for (int i = 1; i <= dynamicName.Count; i++)
                {
                    //TextBox txtName = (TextBox)tblnode.FindControl("txt" + i);
                    string strDynamicName = dynamicName[i - 1].ToString();
                    string strAliasName = dynamicAlias[i - 1].ToString();
                    XmlNode xElmnt = null;
                    if (trChildNode != null)
                    {
                        string strPath = getPath(trChildNode, 0);
                        foreach (XmlNode xnode in xmlDoc.DocumentElement.SelectNodes(strPath)[0].ChildNodes)
                        {
                          
                            if (xnode.Name == txtrootName.Text.Trim().Replace(" ", "").Replace("/","2F"))
                            {
                                xElmnt = xnode;
                                xElmnt.Attributes["TOT"].Value = txtsubnodeNo.Text;
                                break;
                            }
                        }
                    }
                    else
                    {
                        xElmnt = xmlDoc.GetElementsByTagName(txtrootName.Text.Replace(" ", ""))[0];
                    }

                    string strhid = null;
                    int intpid;
                    int intParentId = 0;

                    maxLid++;
                    if (xElmnt.Name == xmlDoc.DocumentElement.FirstChild.FirstChild.Name)
                    {
                        strhid = i.ToString();
                        intpid = 1;
                        intParentId = 0;
                    }
                    else
                    {
                        strhid = xElmnt.Attributes["HID"].Value;
                        intpid = Convert.ToInt32(xElmnt.Attributes["PID"].Value) + 1;
                        intParentId = Convert.ToInt32(xElmnt.Attributes["LID"].Value);
                    }


                    //Adding Data To Hierarchy Table.(1st Level)
                    if (xElmnt.Name == xmlDoc.DocumentElement.FirstChild.FirstChild.Name)
                    {
                        //Modified By Subrat Acharya
                       objHierarchyConfig.Strflag="N";
                        //Modified By Subrat Acharya
                        // objHierarchyConfig.Strflag = "Y";
                        objHierarchyConfig.ActionCode = "A";
                        objHierarchyConfig.HierarchyID = Convert.ToInt32(strhid);
                        objHierarchyConfig.HierarchyName = strDynamicName.Trim();
                        objHierarchyConfig.LeveliD = maxLid;
                        objHierarchyConfig.PositionID = intpid;
                        objHierarchyConfig.AliasName = strAliasName.Trim();
                        objHierarchyConfig.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                        objHierarchyConfig.ParentId = intParentId;
                        intOutPut = Objadminbal.AddHierarDetails(objHierarchyConfig);
                    }
                    //Adding Data To Admin Level Table.(2nd Level)
                    else
                    {
                        //Modified By Subrat Acharya
                       objHierarchyConfig.Strflag="N";
                        //Modified By Subrat Acharya
                        //objHierarchyConfig.Strflag = "N";
                        objHierarchyConfig.ActionCode = "A";
                        objHierarchyConfig.HierarchyID = Convert.ToInt32(strhid);
                        objHierarchyConfig.LevelName = strDynamicName.Trim();
                        objHierarchyConfig.LeveliD = maxLid;
                        objHierarchyConfig.PositionID = intpid;
                        objHierarchyConfig.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                        objHierarchyConfig.ParentId = intParentId;
                        intOutPut = Objadminbal.AddHierarLevel(objHierarchyConfig);
                    }

                    if (intOutPut == 1)
                    {
                        string strDyn2="";
                        if (strDynamicName.Contains("/"))
                        {
                            strDyn2 = strDynamicName.Replace("/", "2F");
                            subNdHeader += "<" + strDyn2.Replace(" ", "") + " ID='" + i.ToString() + "' HID='" + strhid + "' TOT='0' LID='" + maxLid.ToString() + "' PID='" + intpid.ToString() + "' PARENTID='" + intParentId.ToString() + "' ALIAS='" + strAliasName.Replace(" ", "") + "' FLAG='0'  NAME='" + strDynamicName.Trim() + "'  DESC='" + txtrootSummary.Text + "'></" + strDyn2.Replace(" ", "") + ">";
                        }
                        else
                        {
                            subNdHeader += "<" + strDynamicName.Replace(" ", "") + " ID='" + i.ToString() + "' HID='" + strhid + "' TOT='0' LID='" + maxLid.ToString() + "' PID='" + intpid.ToString() + "' PARENTID='" + intParentId.ToString() + "' ALIAS='" + strAliasName.Replace(" ", "") + "' FLAG='0'  NAME='" + strDynamicName.Trim() + "'  DESC='" + txtrootSummary.Text + "'></" + strDynamicName.Replace(" ", "") + ">";
                        }
                        
                        xElmnt.InnerXml = subNdHeader;
                    }
                    else
                    {
                        xElmnt.Attributes["TOT"].Value = "0";
                    }
                    if (trChildNode != null)
                    {
                        string strPath = getPath(trChildNode, 0);
                        int val = Convert.ToInt32(xElmnt.Attributes["ID"].Value);
                        if (val == Convert.ToInt32(xmlDoc.DocumentElement.SelectNodes(strPath)[0].ChildNodes[0].Attributes["ID"].Value))
                        {
                            xmlDoc.DocumentElement.SelectNodes(strPath)[0].PrependChild(xElmnt);
                        }
                        else
                        {
                            foreach (XmlNode xChldNd in xmlDoc.DocumentElement.SelectNodes(strPath)[0].ChildNodes)
                            {
                                if (Convert.ToInt32(xChldNd.Attributes["ID"].Value) < val)
                                {
                                    xmlDoc.DocumentElement.SelectNodes(strPath)[0].InsertAfter(xElmnt, xChldNd);
                                }
                                else
                                {
                                    if (val < ++val && nodeAdded == false)
                                    {
                                        nodeAdded = true;
                                        xmlDoc.DocumentElement.SelectNodes(strPath)[0].InsertBefore(xElmnt, xChldNd);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        xmlDoc.DocumentElement.SelectNodes("/Menu/" + ConfigurationManager.AppSettings["HierMenuName"].ToString())[0].AppendChild(xElmnt);
                    }

                }
                // 3. Save the modified XML to a file in UTF format.
                string strOutmsg = StaticValues.message(intOutPut, "Hierarchy ");
                // ClientScript.RegisterStartupScript(GetType(), "", "alert('" + strOutmsg + "')", true);
                // Page.Controls.Add(new LiteralControl("<script>window.alert('" + strOutmsg + "')</script>"));
                Response.Write("<script>window.alert('" + strOutmsg + "')</script>");
                xmlDoc.PreserveWhitespace = true;
                XmlTextWriter xmlTxtWrtr = new XmlTextWriter(Server.MapPath("../Console/Menu/KwantifyMenu.xml"), System.Text.Encoding.UTF8);
                xmlDoc.WriteTo(xmlTxtWrtr);
                xmlTxtWrtr.Close();
            }
            else
            {
                if (intStat == 1)
                {
                    Response.Write("<script>window.alert('Special Characters are not allowed !');</script>");
                }
                else if (intStat == 2)
                {
                    Response.Write("<script>window.alert('Digits are not accepted as first Character !');</script>");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    /// <summary>
    /// To get the path Where to add the node.
    /// </summary>
    /// <param name="trChildNode"></param>
    /// <param name="j"></param>
    /// <returns></returns>
    private string getPath(TreeNode trChildNode, int j)
    {
        try
        {
            if (trChildNode.Text != ConfigurationManager.AppSettings["HierMenuName"].ToString())
            {
                if (j == 0)
                {

                    strPath = trChildNode.Parent.Text.Replace(" ", "").Replace("/", "2F");
                }
                else
                {
                  
                    strPath = trChildNode.Parent.Text.Replace(" ", "").Replace("/", "2F") + "/" + strPath;
                }
                j++;

                getPath(trChildNode.Parent, j);
            }

            return "/Menu/" + strPath;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    /// <summary>
    /// To fill the fields after Selecting a node.
    /// </summary>
    /// <param name="selectedIndex"></param>
    /// <param name="strNodeName"></param>
    /// Modified By : Dilip Kumar Tripathy
    /// Modify Date : 10-Apr-2012
    /// Description : Change in the code for dynamic table creation to add another column

    private void FindXmlData(int selectedIndex, string strNodeName)
    {
        try
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            string strPath = getPath(trChildNode, 0);
            XmlNode xmlnode = null;
            if (strNodeName == "Manage Hierarchy")
            {
                xmlnode = xmldoc.GetElementsByTagName(strNodeName.Replace(" ", ""))[0];
            }
            else
            {
                if (strNodeName.Contains("/"))
                {
                    strNodeName = strNodeName.Replace("/", "2F");
                }
                xmlnode = xmldoc.DocumentElement.SelectNodes(strPath + "/" + strNodeName.Replace(" ", ""))[0];
            }
            txtrootName.Text = xmlnode.Attributes["NAME"].Value;
            txtrootName.Enabled = false;
            if (xmlnode.ParentNode.ParentNode.Name == "ManageHierarchy")
            {
                Btnvisible(true);
            }
            else
            {
                Btnvisible(true);
            }
            if (!xmlnode.HasChildNodes)
            {
                if (xmlnode.Attributes["DESC"].Value != "")
                {
                    txtrootSummary.Text = xmlnode.Attributes["DESC"].Value;
                }
                if (xmlnode.Attributes["TOT"].Value != "")
                {
                    txtsubnodeNo.Text = xmlnode.Attributes["TOT"].Value;
                }
            }
            if (xmlnode.HasChildNodes)
            {
                intFlagCnt = 0;
                intSessinCnt = 1;
                txtsubnodeNo.Text = xmlnode.Attributes["TOT"].Value.ToString();
                Session["count"] = Convert.ToInt16(txtsubnodeNo.Text) * 2;
                txtrootSummary.Text = xmlnode.Attributes["DESC"].Value;
                tblnode.Rows.Clear();
                int nodecnt = 0;
                int intCnt = 1;
                for (int i = 0; i < Convert.ToInt32(xmlnode.ChildNodes.Count) * 2; i++)
                {
                    if (xmlnode.ChildNodes[intCnt - 1].Attributes.GetNamedItem("FLAG").Value == "True" || xmlnode.ChildNodes[intCnt - 1].Attributes.GetNamedItem("FLAG").Value == "0")
                    {
                        nodecnt = nodecnt + 1;
                        intFlagCnt += 1;
                        int tblCols = 3;
                        string k = "";

                        HtmlTableRow tr = new HtmlTableRow();
                        for (int j = 0; j < tblCols; j++)
                        {

                            HtmlTableCell tc = new HtmlTableCell();
                            if (j == 2)
                            {
                                TextBox txtBox = new TextBox();
                                txtBox.ID = "txt" + Convert.ToString(nodecnt);
                                txtBox.MaxLength = 50;
                                txtBox.Width = Unit.Pixel(200);
                                tc.Controls.Add(txtBox);
                                int intResult = nodecnt % 2;
                                if (intResult != 0)
                                {
                                    txtBox.Text = xmlnode.ChildNodes[intCnt - 1].Attributes.GetNamedItem("NAME").Value;
                                }
                                else
                                {
                                    txtBox.Text = xmlnode.ChildNodes[intCnt - 1].Attributes.GetNamedItem("ALIAS").Value;
                                }
                                tr.Cells.Add(tc);
                            }
                            else if (j == 0)
                            {

                                Label lbl = new Label();
                                k = Convert.ToString(nodecnt);
                                int intResult = int.Parse(k) % 2;

                                if (intResult == 0)
                                {
                                    //if (intSessinCnt < intCnt)
                                    //{
                                    //    lbl.Text = "<font color='#FF0000'>*</font>Node" + intSessinCnt + " Alias";
                                    //}
                                    //else
                                    //{
                                    lbl.Text = "Node" + intSessinCnt + " Alias";
                                    //}

                                }
                                else
                                {
                                    //if (intSessinCnt < intCnt)
                                    //{
                                    lbl.Text = "Node" + intSessinCnt + " Name<font color='#FF0000'>*</font>";
                                    //}
                                    //else
                                    //{
                                    //    lbl.Text = "<font color='#FF0000'>*</font>Node" + intSessinCnt + " Name";
                                    //}

                                }
                                tc.Controls.Add(lbl);
                                tc.Width = "140";
                                tr.Cells.Add(tc);
                            }
                            //Code added by Dilip Kumar Tripathy on dated 10-Apr-2012
                            //Purpse : To add a another coumn in dynamic table
                            else if (j == 1)
                            {
                                Label lbl = new Label();
                                lbl.Text = ":";
                                tc.Controls.Add(lbl);
                                tc.Width = "7";
                                tr.Cells.Add(tc);
                            }
                            //---Code ended by Dilip on same date---------

                        }
                        if (intFlagCnt % 2 == 0)
                        {
                            intSessinCnt += 1;
                            intFlagCnt = 0;
                        }
                        tblnode.Rows.Add(tr);
                        tblnode.EnableViewState = true;
                        ViewState["tbl"] = true;
                        if ((i + 1) % 2 == 0)
                        {
                            intCnt += 1;
                        }
                    }
                    else
                    {
                        i = i + 1;
                        intCnt += 1;
                    }

                }
            }
            else
            {
                tblnode.Rows.Clear();
                Session["intCnt"] = null;
            }
            totalNo = txtsubnodeNo.Text.Trim();

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    /// <summary>
    /// Edit the Selected treeview node data.
    /// </summary>
    protected void EditSubnode2()
    {
        try
        {
            ArrayList dynamicName = new ArrayList();
            ArrayList dynamicAlias = new ArrayList();
            int intStat = 0;
            string strHierid = null;
            string[] strSpecialChar = { "~", "`", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", ",", "'", ":" };
            string[] strDigits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            ArrayList alDigit = new ArrayList(strDigits);
            for (int i = 1; i <= Convert.ToInt32(txtsubnodeNo.Text) * 2; i++)
            {
                TextBox txtName = (TextBox)tblnode.Rows[i - 1].Cells[2].Controls[0];
                if (i % 2 == 0)
                {
                    dynamicAlias.Add(txtName.Text);
                }
                else
                {
                    dynamicName.Add(txtName.Text);
                }
            }
            for (int i = 0; i < dynamicName.Count; i++)
            {
                string strName = dynamicName[i].ToString();
                string strAlias = dynamicAlias[i].ToString();
                for (int j = 0; j < strSpecialChar.Length; j++)
                {
                    if (strName.Contains(strSpecialChar[j].ToString()) || strAlias.Contains(strSpecialChar[j].ToString()))
                    {
                        intStat = 1;
                    }
                    if (alDigit.Contains(strName[0].ToString()))
                    {
                        intStat = 2;
                    }
                    if (strAlias != "")
                    {
                        if (alDigit.Contains(strAlias[0].ToString()))
                        {
                            intStat = 2;
                        }
                    }
                }
            }
            if (intStat == 0)
            {
                XmlDocument xmldoc1 = new XmlDocument();
                xmldoc1.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
                XmlElement newElem = null;
                newElem = (XmlElement)xmldoc1.GetElementsByTagName(ConfigurationManager.AppSettings["HierMenuName"].ToString())[0];
                string strPath1 = getPath(trChildNode, 0);
                XmlNode lst1 = null;
                bool nodeEdited = false;
                string strDynamicName = null;
                string strAliasName = null;
                ArrayList arrlst = new ArrayList();
                int intflagcnt = 0;
                string strDeletedLoc = null;

                foreach (XmlNode xnode in xmldoc1.DocumentElement.SelectNodes(strPath1)[0].ChildNodes)
                {
                    if (NodeVal == xnode.Attributes["ID"].Value)
                    {
                        //Store All the Data of Parent Node of selected node into a xmlnode. 
                        lst1 = xnode;
                        foreach (XmlNode xnd1 in lst1.ChildNodes)
                        {
                            if (xnd1.Attributes["FLAG"].Value == "1")
                            {
                                arrlst.Add(xnd1.Name + "|" + xnd1.Attributes["PID"].Value + "|" + xnd1.Attributes["LID"].Value);
                                intflagcnt++;
                            }
                        }
                        break;
                    }
                }
                //To store the max Level iD.
                int maxLid = 0;
                if (xmldoc1.DocumentElement.FirstChild.FirstChild.HasChildNodes)
                {
                    maxLid = GetMaxLid();
                }
                else
                {
                    maxLid = 0;
                }
                //Delete the parent node of xml of selected Node of tree view.
                //DeleteData(selectIndex, "D");
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
                XmlElement xroot = xmldoc.CreateElement(txtrootName.Text.Replace(" ", ""));
                XmlElement xroot22 = null;
                string ss = lst1.Attributes["TOT"].Value.ToString();
                for (int i = 0; i < lst1.Attributes.Count; i++)
                {
                    xroot.SetAttributeNode((XmlAttribute)lst1.Attributes[i].CloneNode(true));
                    if (xroot.Attributes[i].Name == "NAME")
                    {
                        xroot.Attributes["NAME"].Value = txtrootName.Text;
                    }
                    if (xroot.Attributes[i].Name == "DESC")
                    {
                        xroot.Attributes["DESC"].Value = txtrootSummary.Text.Replace(" ", "");
                    }
                    if (xroot.Attributes[i].Name == "TOT")
                    {
                        xroot.Attributes["TOT"].Value = lst1.Attributes["TOT"].Value.ToString(); ;// txtsubnodeNo.Text.Replace(" ", "");
                    }
                }
                if (txtsubnodeNo.Text == "")
                {
                    txtsubnodeNo.Text = "0";
                }


                int intNewcnt = 1;
                bool bCountChk = false;

                for (int i = 1; i <= dynamicName.Count + intflagcnt; )
                {

                    if (i <= lst1.ChildNodes.Count)
                    {
                        foreach (string m in arrlst)
                        {
                            string[] arrlstval = m.Split('|');
                            if (lst1.ChildNodes[i - 1].Name == arrlstval[0] && lst1.ChildNodes[i - 1].Attributes["LID"].Value == arrlstval[2])
                            {
                                //txtName.Text = arrlstval[0].ToString();
                                strDeletedLoc = arrlstval[0].ToString();
                                bCountChk = true;
                                goto A1;
                            }
                        }
                    }
                    // txtName = (TextBox)tblnode.FindControl("txt" + intNewcnt);
                    if (bCountChk != true)
                    {
                        strDynamicName = dynamicName[intNewcnt - 1].ToString();//commented by dilip on dated 06-Apr-2012
                        strAliasName = dynamicAlias[intNewcnt - 1].ToString();
                    }
                    //Menupnl.FindControl("txtNdName" + intNewcnt);
                    int cnt = lst1.ChildNodes.Count;
                //Execution of Goto statement.
                A1:
                    string strhid = null;
                    int strpid;
                    if (xroot.Name == xmldoc1.DocumentElement.FirstChild.FirstChild.Name)
                    {
                        strhid = i.ToString();
                        strpid = 1;
                    }
                    else
                    {
                        strhid = xroot.Attributes["ID"].Value;
                        strpid = i + 1;
                    }

                    if (i <= lst1.ChildNodes.Count)
                    {
                        XmlElement xroot1 = null;
                        if (bCountChk == true)
                        {
                            xroot1 = xmldoc.CreateElement(strDeletedLoc.Replace(" ", ""));
                        }
                        else
                        {
                            xroot1 = xmldoc.CreateElement(strDynamicName.Replace(" ", ""));
                        }
                        if (bCountChk == true)
                        {
                            xmldoc1.DocumentElement.SelectNodes(strPath1)[0].ChildNodes[i - 1].ChildNodes[i - 1].Attributes["NAME"].Value = strDeletedLoc;
                        }
                        else
                        {
                            if (xmldoc1.DocumentElement.FirstChild.FirstChild.Name == "ManageHierarchy")
                            {
                                xmldoc1.DocumentElement.SelectNodes(strPath1)[0].ChildNodes[0].ChildNodes[i - 1].Attributes["NAME"].Value = strDynamicName;
                                xmldoc1.DocumentElement.SelectNodes(strPath1)[0].ChildNodes[0].ChildNodes[i - 1].Attributes["ALIAS"].Value = strAliasName;
                                strHierid = xmldoc1.DocumentElement.SelectNodes(strPath1)[0].ChildNodes[0].ChildNodes[i - 1].Attributes["HID"].Value;
                            }
                            else
                            {
                                xmldoc1.DocumentElement.SelectNodes(strPath1)[0].ChildNodes[i - 1].ChildNodes[i - 1].Attributes["NAME"].Value = strDynamicName;
                                xmldoc1.DocumentElement.SelectNodes(strPath1)[0].ChildNodes[i - 1].ChildNodes[i - 1].Attributes["ALIAS"].Value = strAliasName;
                                strHierid = xmldoc1.DocumentElement.SelectNodes(strPath1)[0].ChildNodes[i - 1].ChildNodes[i - 1].Attributes["HID"].Value;
                            }
                        }

                        if (xroot.Name == xmldoc1.DocumentElement.FirstChild.FirstChild.Name)
                        {
                            //Modified By Subrat Acharya
                           objHierarchyConfig.Strflag="N";
                            //Modified By Subrat Acharya
                            objHierarchyConfig.ActionCode = "U";
                            objHierarchyConfig.HierarchyID = Convert.ToInt32(strHierid);
                            if (bCountChk == true)
                            {
                                objHierarchyConfig.HierarchyName = strDeletedLoc;
                            }
                            else
                            {
                                objHierarchyConfig.HierarchyName = strDynamicName.Trim();
                            }
                            objHierarchyConfig.AliasName = strAliasName.Trim();
                            objHierarchyConfig.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                            IntEdretVal = Objadminbal.EditHierarDetails(objHierarchyConfig);
                        }
                        else
                        {
                            //Modified By Subrat Acharya
                           objHierarchyConfig.Strflag="N";
                            //Modified By Subrat Acharya
                            objHierarchyConfig.ActionCode = "U";
                            objHierarchyConfig.HierarchyID = Convert.ToInt32(strHierid);
                            if (bCountChk == true)
                            {
                                objHierarchyConfig.LevelName = strDeletedLoc;
                            }
                            else
                            {
                                objHierarchyConfig.LevelName = strDynamicName.Trim();
                            }
                            objHierarchyConfig.PositionID = Convert.ToInt32(xroot1.Attributes["PID"].Value);
                            objHierarchyConfig.LeveliD = Convert.ToInt32(xroot1.Attributes["LID"].Value);
                            objHierarchyConfig.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                            IntEdretVal = Objadminbal.EditHierarLevel(objHierarchyConfig);
                        }


                    }
                    else
                    {
                        maxLid++;
                        

                     

                        if (xroot.Name == xmldoc1.DocumentElement.FirstChild.FirstChild.Name)
                        {
                            //Modified By Subrat Acharya
                           objHierarchyConfig.Strflag="N";
                            //Modified By Subrat Acharya
                            objHierarchyConfig.ActionCode = "A";
                            objHierarchyConfig.HierarchyID = Convert.ToInt32(strhid);
                            objHierarchyConfig.HierarchyName = strDynamicName.Trim();
                            objHierarchyConfig.AliasName = strAliasName.Trim();
                            objHierarchyConfig.LeveliD = maxLid;
                            objHierarchyConfig.PositionID = Objadminbal.GetMaxPOSId(objHierarchyConfig);
                            objHierarchyConfig.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                            IntEdretVal = Objadminbal.AddHierarDetails(objHierarchyConfig);

                            if (IntEdretVal == 1)
                            {
                                xroot22 = xmldoc1.CreateElement(strDynamicName.Replace(" ", ""));
                                xroot22.InnerXml = "<" + strDynamicName.Replace(" ", "") + " ID='" + Convert.ToString(i) + "' HID='" + strhid + "' TOT='0' LID='" + maxLid.ToString() + "' PID='" + objHierarchyConfig.PositionID.ToString() + "' ALIAS='" + strAliasName.Replace(" ", "") + "' FLAG='0' NAME='" + strDynamicName.Trim() + "' DESC='" + txtrootSummary.Text + "' ></" + strDynamicName.Replace(" ", "") + ">";
                                // xroot22.Attributes["TOT"].Value = (Convert.ToInt32(xroot.Attributes["TOT"].Value.ToString()) + 1).ToString();
                            }
                        }
                        else
                        {
                            //Modified By Subrat Acharya
                           objHierarchyConfig.Strflag="N";
                            //Modified By Subrat Acharya
                            objHierarchyConfig.ActionCode = "A";
                            objHierarchyConfig.HierarchyID = Convert.ToInt32(strhid);
                            objHierarchyConfig.LevelName = strDynamicName.Trim();
                            objHierarchyConfig.LeveliD = maxLid;
                            objHierarchyConfig.PositionID = Objadminbal.GetMaxPOSId(objHierarchyConfig);
                            objHierarchyConfig.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                            IntEdretVal = Objadminbal.AddHierarLevel(objHierarchyConfig);
                            if (IntEdretVal == 1)
                            {
                                xroot22 = xmldoc1.CreateElement(strDynamicName.Replace(" ", ""));
                                xroot22.InnerXml = "<" + strDynamicName.Replace(" ", "") + " ID='" + Convert.ToString(i) + "' HID='" + strhid + "' TOT='0' LID='" + maxLid.ToString() + "' PID='" + objHierarchyConfig.PositionID.ToString() + "' ALIAS='" + strAliasName.Replace(" ", "") + "' FLAG='0' NAME='" + strDynamicName.Trim() + "' DESC='" + txtrootSummary.Text + "' ></" + strDynamicName.Replace(" ", "") + ">";
                                // xroot22.Attributes["TOT"].Value = (Convert.ToInt32(xroot.Attributes["TOT"].Value.ToString()) + 1).ToString();
                            }
                        }
                        //--------Modify By Dilip Kumar Tripathy By Addnig Follwing Code On Dated 24-Feb-2012

                        //Code Ended By Dilip Kumar Tripathy On The Same Date
                    }
                    if (bCountChk == false)
                    {
                        intNewcnt++;
                    }
                    bCountChk = false;
                    i++;
                }
                //xmldoc.Save(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
                string strOutmsg = StaticValues.message(IntEdretVal, "Hierarchy ");
                Response.Write("<script>window.alert('" + strOutmsg + "');</script>");
                int intAttrval = Convert.ToInt32(xroot.Attributes["ID"].Value);

                if (xmldoc1.DocumentElement.SelectNodes(strPath1)[0].HasChildNodes)
                {
                    if (xroot22 != null)
                    {
                        if (xroot.Name == "ManageHierarchy")
                        {
                            xmldoc1.DocumentElement.SelectNodes(strPath1)[0].PrependChild(xroot22);
                        }
                        if (intAttrval == Convert.ToInt32(xmldoc1.DocumentElement.SelectNodes(strPath1)[0].ChildNodes[0].Attributes["ID"].Value))
                        {
                            xmldoc1.DocumentElement.SelectNodes(strPath1)[0].PrependChild(xroot22);
                        }
                        else
                        {
                            foreach (XmlNode xChldNd in xmldoc.DocumentElement.SelectNodes(strPath1)[0].ChildNodes)
                            {
                                if (Convert.ToInt32(xChldNd.Attributes["ID"].Value) < intAttrval)
                                {
                                    xmldoc.DocumentElement.SelectNodes(strPath1)[0].InsertAfter(xroot22, xChldNd);
                                }
                                else
                                {
                                    if (intAttrval < ++intAttrval && nodeEdited == false)
                                    {
                                        nodeEdited = true;
                                        xmldoc1.DocumentElement.SelectNodes(strPath1)[0].InsertBefore(xroot22, xChldNd);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    xmldoc1.DocumentElement.SelectNodes(strPath1)[0].PrependChild(xroot);
                }
                xmldoc1.Save(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            }
            else
            {
                if (intStat == 1)
                {
                    Response.Write("<script>window.alert('Special Characters are not allowed !');</script>");
                }
                else if (intStat == 2)
                {
                    Response.Write("<script>window.alert('Digits are not accepted as first Character !');</script>");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }

    protected void EditSubnode(ArrayList dynamicName, ArrayList dynamicAlias)
    {
        try
        {
            XmlDocument xmldoc1 = new XmlDocument();
            xmldoc1.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            XmlElement newElem = null;
            newElem = (XmlElement)xmldoc1.GetElementsByTagName(ConfigurationManager.AppSettings["HierMenuName"].ToString())[0];
            string strPath1 = getPath(trChildNode, 0);
            XmlNode lst1 = null;
            bool nodeEdited = false;
            string strDynamicName = "";
            string strAliasName = "";
            ArrayList arrlst = new ArrayList();
            int intflagcnt = 0;
            string strDeletedLoc = "";
            string strDeletedLocAlias = "";

            foreach (XmlNode xnode in xmldoc1.DocumentElement.SelectNodes(strPath1)[0].ChildNodes)
            {
                if (NodeVal == xnode.Attributes["ID"].Value)
                {
                    //Store All the Data of Parent Node of selected node into a xmlnode. 
                    lst1 = xnode;
                    foreach (XmlNode xnd1 in lst1.ChildNodes)
                    {
                        if (xnd1.Attributes["FLAG"].Value == "1")
                        {
                            arrlst.Add(xnd1.Name + "|" + xnd1.Attributes["PID"].Value + "|" + xnd1.Attributes["LID"].Value + "|" + xnd1.Attributes["ALIAS"].Value);
                            intflagcnt++;
                        }
                    }
                    break;
                }
            }
            //To store the max Level iD.
            int maxLid = 0;
            if (xmldoc1.DocumentElement.FirstChild.FirstChild.HasChildNodes)
            {
                maxLid = GetMaxLid();
            }
            else
            {
                maxLid = 0;
            }
            //Delete the parent node of xml of selected Node of tree view.
            DeleteData(selectIndex, "D");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            XmlElement xroot = xmldoc.CreateElement(txtrootName.Text.Replace(" ", "").Replace("/", "2F"));
            string ss = lst1.Attributes["TOT"].Value.ToString();
            for (int i = 0; i < lst1.Attributes.Count; i++)
            {
                xroot.SetAttributeNode((XmlAttribute)lst1.Attributes[i].CloneNode(true));
                if (xroot.Attributes[i].Name == "NAME")
                {
                    xroot.Attributes["NAME"].Value = txtrootName.Text;
                }

                if (xroot.Attributes[i].Name == "DESC")
                {
                    xroot.Attributes["DESC"].Value = txtrootSummary.Text.Replace(" ", "");
                }
                if (xroot.Attributes[i].Name == "TOT")
                {
                    xroot.Attributes["TOT"].Value = lst1.Attributes["TOT"].Value.ToString(); ;// txtsubnodeNo.Text.Replace(" ", "");
                }
            }
            if (txtsubnodeNo.Text == "")
            {
                txtsubnodeNo.Text = "0";
            }


            int intNewcnt = 1;
            bool bCountChk = false;

            for (int i = 1; i <= dynamicName.Count + intflagcnt; )
            {

                if (i <= lst1.ChildNodes.Count)
                {
                    foreach (string m in arrlst)
                    {
                        string[] arrlstval = m.Split('|');
                        if (lst1.ChildNodes[i - 1].Name == arrlstval[0] && lst1.ChildNodes[i - 1].Attributes["LID"].Value == arrlstval[2])
                        {
                            //txtName.Text = arrlstval[0].ToString();
                            strDeletedLoc = arrlstval[0].ToString();
                            strDeletedLocAlias = arrlstval[3].ToString();
                            bCountChk = true;
                            goto A1;
                        }
                    }
                }
                // txtName = (TextBox)tblnode.FindControl("txt" + intNewcnt);
                if (bCountChk != true)
                {
                    strDynamicName = dynamicName[intNewcnt - 1].ToString();//commented by dilip on dated 06-Apr-2012
                    strAliasName = dynamicAlias[intNewcnt - 1].ToString();
                }
                //Menupnl.FindControl("txtNdName" + intNewcnt);
                int cnt = lst1.ChildNodes.Count;
            //Execution of Goto statement.
            A1:
                string strhid = null;
                int intpid;
                int intParentId;
                if (xroot.Name == xmldoc1.DocumentElement.FirstChild.FirstChild.Name)
                {
                    strhid = i.ToString();
                    intpid = 1;
                    intParentId = 0;
                }
                else
                {
                    strhid = xroot.Attributes["HID"].Value;
                    intpid = Convert.ToInt32(xroot.Attributes["PID"].Value) + 1;
                    intParentId = Convert.ToInt32(xroot.Attributes["LID"].Value);

                }
                if (i <= lst1.ChildNodes.Count)
                {
                    XmlElement xroot1 = null;
                    string strDeletedLocroot="", strDynamicNameRoot = "";
                   
                  
                    if (bCountChk == true)
                    {
                        if (strDeletedLoc.Contains("/"))
                        {
                            strDeletedLocroot = strDeletedLoc.Replace("/", "2F");
                            xroot1 = xmldoc.CreateElement(strDeletedLocroot.Replace(" ", ""));
                        }
                        else
                        {
                            xroot1 = xmldoc.CreateElement(strDeletedLoc.Replace(" ", ""));
                        }
                       
                    }
                    else
                    {
                        if (strDynamicName.Contains("/"))
                        {
                            strDynamicNameRoot = strDynamicName.Replace("/", "2F");
                            xroot1 = xmldoc.CreateElement(strDynamicNameRoot.Replace(" ", ""));
                        }
                        else
                        {
                            xroot1 = xmldoc.CreateElement(strDynamicName.Replace(" ", ""));
                        }
                        
                    }
                    for (int j = 0; j < lst1.ChildNodes[i - 1].Attributes.Count; j++)
                    {
                        xroot1.SetAttributeNode((XmlAttribute)lst1.ChildNodes[i - 1].Attributes[j].CloneNode(true));
                        if (xroot1.Attributes[j].Name == "NAME")
                        {
                            if (bCountChk == true)
                            {
                                xroot1.Attributes["NAME"].Value = strDeletedLoc;
                                xroot1.Attributes["ALIAS"].Value = strDeletedLocAlias;
                            }
                            else
                            {
                                xroot1.Attributes["NAME"].Value = strDynamicName;
                                xroot1.Attributes["ALIAS"].Value = strAliasName;
                            }
                        }

                        xroot1.InnerXml = lst1.ChildNodes[i - 1].InnerXml;
                    }
                    string s = xroot1.Attributes["HID"].Value;
                    xroot.AppendChild(xroot1);
                    if (xroot.Name == xmldoc1.DocumentElement.FirstChild.FirstChild.Name)
                    {
                        //Modified By Subrat Acharya
                       objHierarchyConfig.Strflag="N";
                        //Modified By Subrat Acharya
                        objHierarchyConfig.ActionCode = "U";
                        objHierarchyConfig.HierarchyID = Convert.ToInt32(xroot1.Attributes["HID"].Value);
                        if (strDeletedLoc.Contains("2F"))
                        {
                            strDeletedLoc = strDeletedLoc.Replace("2F", "/");
                        }
                        if (strDynamicName.Contains("2F"))
                        {
                            strDynamicName = strDynamicName.Replace("2F", "/");
                        }
                        if (bCountChk == true)
                        {
                            objHierarchyConfig.HierarchyName = strDeletedLoc;
                        }
                        else
                        {
                            objHierarchyConfig.HierarchyName = strDynamicName.Trim();
                        }
                        if (bCountChk == true)
                        {
                            objHierarchyConfig.AliasName = strDeletedLocAlias;
                        }
                        else
                        {
                            objHierarchyConfig.AliasName = strAliasName.Trim();
                        }
                        objHierarchyConfig.ParentId = intParentId;
                        objHierarchyConfig.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                        IntEdretVal = Objadminbal.EditHierarDetails(objHierarchyConfig);
                    }
                    else
                    {
                        //Modified By Subrat Acharya
                       objHierarchyConfig.Strflag="N";
                        //Modified By Subrat Acharya
                        objHierarchyConfig.ActionCode = "U";
                        objHierarchyConfig.HierarchyID = Convert.ToInt32(xroot1.Attributes["HID"].Value);
                        if (strDeletedLoc.Contains("2F"))
                        {
                            strDeletedLoc = strDeletedLoc.Replace("2F", "/");
                        }
                        if (strDynamicName.Contains("2F"))
                        {
                            strDynamicName = strDynamicName.Replace("2F", "/");
                        }
                        if (bCountChk == true)
                        {
                            objHierarchyConfig.LevelName = Convert.ToString(strDeletedLoc);
                        }
                        else
                        {
                            objHierarchyConfig.LevelName = Convert.ToString(strDynamicName.Trim());
                        }
                        objHierarchyConfig.PositionID = Convert.ToInt32(xroot1.Attributes["PID"].Value);
                        objHierarchyConfig.LeveliD = Convert.ToInt32(xroot1.Attributes["LID"].Value);
                        objHierarchyConfig.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                        objHierarchyConfig.ParentId = intParentId;
                        IntEdretVal = Objadminbal.EditHierarLevel(objHierarchyConfig);
                    }


                }
                else
                {
                    maxLid++;
                    //object objPid = null;
                    //Commented By Dilip Kumar Tripathy on dated 27-Jan-2013
                    //string strQry = "select isnull(max(int_Position),0)+1 from  M_Admin_Level where  int_HierarchyId=" + strhid + " and int_DeletedFlag not in(1,3)";
                    //objPid = objCmnDll.ExeScalar("ConnectionString", strQry, 0);

                    if (xroot.Name == xmldoc1.DocumentElement.FirstChild.FirstChild.Name)
                    {
                        //Modified By Subrat Acharya
                       objHierarchyConfig.Strflag="N";
                        //Modified By Subrat Acharya
                        objHierarchyConfig.ActionCode = "A";
                        objHierarchyConfig.HierarchyID = Convert.ToInt32(strhid);
                        objHierarchyConfig.HierarchyName = strDynamicName.Trim();
                        objHierarchyConfig.AliasName = strAliasName.Trim();
                        objHierarchyConfig.LeveliD = maxLid;
                        //objHierarchyConfig.PositionID = Convert.ToInt32(objPid);
                        objHierarchyConfig.PositionID = intpid;
                        objHierarchyConfig.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                        objHierarchyConfig.ParentId = intParentId;
                        IntEdretVal = Objadminbal.AddHierarDetails(objHierarchyConfig);

                        if (IntEdretVal == 1)
                        {
                            string strDyn2 = "";
                            if (strDynamicName.Contains("/"))
                            {
                                strDyn2 = strDynamicName.Replace("/", "2F");
                                xroot.InnerXml += "<" + strDyn2.Replace(" ", "") + " ID='" + Convert.ToString(i) + "' HID='" + strhid + "' TOT='0' LID='" + maxLid.ToString() + "' PID='" + intpid + "' PARENTID='" + intParentId.ToString() + "' ALIAS='" + strAliasName.Replace(" ", "") + "' FLAG='0' NAME='" + strDynamicName.Trim() + "' DESC='" + txtrootSummary.Text + "' ></" + strDyn2.Replace(" ", "") + ">";
                            }
                            else
                            {
                                xroot.InnerXml += "<" + strDynamicName.Replace(" ", "") + " ID='" + Convert.ToString(i) + "' HID='" + strhid + "' TOT='0' LID='" + maxLid.ToString() + "' PID='" + intpid + "' PARENTID='" + intParentId.ToString() + "' ALIAS='" + strAliasName.Replace(" ", "") + "' FLAG='0' NAME='" + strDynamicName.Trim() + "' DESC='" + txtrootSummary.Text + "' ></" + strDynamicName.Replace(" ", "") + ">";
                            }
                            
                            xroot.Attributes["TOT"].Value = (Convert.ToInt32(xroot.Attributes["TOT"].Value.ToString()) + 1).ToString();
                        }
                    }
                    else
                    {
                        //Modified By Subrat Acharya
                       objHierarchyConfig.Strflag="N";
                        //Modified By Subrat Acharya
                        objHierarchyConfig.ActionCode = "A";
                        objHierarchyConfig.HierarchyID = Convert.ToInt32(strhid);
                        objHierarchyConfig.LevelName = strDynamicName.Trim();
                        objHierarchyConfig.LeveliD = maxLid;
                        objHierarchyConfig.PositionID = intpid;
                        objHierarchyConfig.CreatedBy = Convert.ToInt32(Session["UserId"].ToString());
                        objHierarchyConfig.ParentId = intParentId;
                        IntEdretVal = Objadminbal.AddHierarLevel(objHierarchyConfig);
                        if (IntEdretVal == 1)
                        {
                            string strDyn2 = "";
                            if (strDynamicName.Contains("/"))
                            {
                                strDyn2 = strDynamicName.Replace("/", "2F");
                                xroot.InnerXml += "<" + strDyn2.Replace(" ", "") + " ID='" + Convert.ToString(i) + "' HID='" + strhid + "' TOT='0' LID='" + maxLid.ToString() + "' PID='" + intpid + "' PARENTID='" + intParentId.ToString() + "' ALIAS='" + strAliasName.Replace(" ", "") + "' FLAG='0' NAME='" + strDynamicName.Trim() + "' DESC='" + txtrootSummary.Text + "' ></" + strDyn2.Replace(" ", "") + ">";
                            }
                            else
                            {
                                xroot.InnerXml += "<" + strDynamicName.Replace(" ", "") + " ID='" + Convert.ToString(i) + "' HID='" + strhid + "' TOT='0' LID='" + maxLid.ToString() + "' PID='" + intpid + "' PARENTID='" + intParentId.ToString() + "' ALIAS='" + strAliasName.Replace(" ", "") + "' FLAG='0' NAME='" + strDynamicName.Trim() + "' DESC='" + txtrootSummary.Text + "' ></" + strDynamicName.Replace(" ", "") + ">";
                            }
                            
                            xroot.Attributes["TOT"].Value = (Convert.ToInt32(xroot.Attributes["TOT"].Value.ToString()) + 1).ToString();
                        }
                    }
                    //--------Modify By Dilip Kumar Tripathy By Addnig Follwing Code On Dated 24-Feb-2012

                    //Code Ended By Dilip Kumar Tripathy On The Same Date
                }
                if (bCountChk == false)
                {
                    intNewcnt++;
                }
                bCountChk = false;
                i++;
            }
            //xmldoc.Save(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            string strOutmsg = StaticValues.message(IntEdretVal, "Hierarchy ");
            Response.Write("<script>window.alert('" + strOutmsg + "')</script>");
            //  ClientScript.RegisterClientScriptBlock(GetType(), "", "alert('" + strOutmsg + "');", true);
            int intAttrval = Convert.ToInt32(xroot.Attributes["ID"].Value);

            if (xmldoc.DocumentElement.SelectNodes(strPath1)[0].HasChildNodes)
            {
                if (xroot.Name == "ManageHierarchy")
                {
                    xmldoc.DocumentElement.SelectNodes(strPath1)[0].PrependChild(xroot);
                }
                if (intAttrval == Convert.ToInt32(xmldoc.DocumentElement.SelectNodes(strPath1)[0].ChildNodes[0].Attributes["ID"].Value))
                {
                    xmldoc.DocumentElement.SelectNodes(strPath1)[0].PrependChild(xroot);
                }
                else
                {
                    foreach (XmlNode xChldNd in xmldoc.DocumentElement.SelectNodes(strPath1)[0].ChildNodes)
                    {
                        if (Convert.ToInt32(xChldNd.Attributes["ID"].Value) < intAttrval)
                        {
                            xmldoc.DocumentElement.SelectNodes(strPath1)[0].InsertAfter(xroot, xChldNd);
                        }
                        else
                        {
                            if (intAttrval < ++intAttrval && nodeEdited == false)
                            {
                                nodeEdited = true;
                                xmldoc.DocumentElement.SelectNodes(strPath1)[0].InsertBefore(xroot, xChldNd);
                            }
                        }
                    }
                }
            }
            else
            {
                xmldoc.DocumentElement.SelectNodes(strPath1)[0].PrependChild(xroot);
            }
            xmldoc.Save(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    /// <summary>
    /// Delete the selected node from treeview.
    /// </summary>
    /// <param name="index"></param>
    /// 

    protected void DeleteData(int index, string strAction)
    {
        try
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            string strPath = getPath(trChildNode, 0);
            XmlNode xmlnode = xmldoc.DocumentElement.SelectNodes(strPath)[0].ChildNodes[index];
            XmlNode xmlnode2 = xmldoc.DocumentElement.SelectNodes("/Menu")[0].ChildNodes[0];
            XmlNode refXmlNode = xmldoc.DocumentElement.SelectNodes(strPath)[0].ChildNodes[index + 1];//Added By Dilip Kumar Tripathy on dated 10-Apr-2012
            //Code Added By Dilip Kumar Tripathy on dated 10-Apr-2012
            //Purpose : To get the xml document by its tagname .Previous method to get xmlnode was sometimes un effective in delete case
            //string strNodeName = treemenu.SelectedNode.Text;
            //string strNodeVal = treemenu.SelectedNode.Value;
            //XmlNode xmlnode = xmldoc.GetElementsByTagName(strNodeName.Replace(" ", ""))[0];
            if (strAction == "D")
            {
                xmlnode.ParentNode.RemoveChild(xmlnode);
                xmldoc.Save(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            }
            else
            {
                XmlNode xmlpnode;
                xmlpnode = xmlnode.ParentNode;
                if (xmlpnode.Name == xmldoc.DocumentElement.FirstChild.FirstChild.Name)
                {
                    objHierarchyConfig.ActionCode = "D";
                    objHierarchyConfig.HierarchyID = Convert.ToInt32(xmlnode.Attributes["HID"].Value);
                    objHierarchyConfig.CreatedBy = int.Parse(Session["UserId"].ToString());
                    intOutPut = Objadminbal.DeleteHierarDetails(objHierarchyConfig);
                }
                else
                {
                    objHierarchyConfig.ActionCode = "D";
                    objHierarchyConfig.HierarchyID = Convert.ToInt32(xmlnode.Attributes["HID"].Value);
                    objHierarchyConfig.LeveliD = Convert.ToInt32(xmlnode.Attributes["LID"].Value);
                    objHierarchyConfig.CreatedBy = int.Parse(Session["UserId"].ToString());
                    intOutPut = Objadminbal.DeleteHierarLevel(objHierarchyConfig);
                }

                //Code Added By Dilip Kumar Tripathy On Dated 24-Feb-2012
                if (intOutPut == 3)
                {
                    xmlnode.Attributes["FLAG"].Value = "1";
                    if (xmlnode.HasChildNodes)
                    {
                        foreach (XmlNode xnode in xmlnode.ChildNodes)
                        {
                            xnode.Attributes["FLAG"].Value = "1";
                        }
                    }

                    if (xmlpnode.Name != ConfigurationManager.AppSettings["HierMenuName"].ToString())
                    {
                        int count = 0;
                        if (xmlpnode.HasChildNodes)
                        {
                            foreach (XmlNode xnode in xmlpnode.ChildNodes)
                            {
                                if (xnode.Attributes["FLAG"].Value != "1")
                                {
                                    count++;
                                }
                            }
                        }
                        xmlpnode.Attributes["TOT"].Value = count.ToString();

                    }
                    RenameDeletedNode(xmldoc, xmlnode, refXmlNode);
                    xmldoc.Save(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
                    string strOutmsg = StaticValues.message(intOutPut, "Hierarchy ");
                    //Page.ClientScript.RegisterStartupScript(GetType(), "", "alert('" + strOutmsg + "')", true);
                    Response.Write("<script language='javascript'>window.alert('" + strOutmsg + "')</script>");

                }
                else if (intOutPut == 5)
                {
                    //Page.ClientScript.RegisterStartupScript(GetType(), "", "alert('Hierarchy Data Can not be Deleted. Because It has its Subordinates')", true);
                    Response.Write("<script language='javascript'>window.alert('Hierarchy Data Can not be Deleted. Because It has its Subordinates')</script>");
                }
            }

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    /// <summary>
    /// To get the Maximum Level Id.
    /// </summary>
    /// <returns></returns>
    public int GetMaxLid()
    {
        try
        {
            int intmaxval = 0;
            XQueryNavigatorCollection navcol = new XQueryNavigatorCollection();
            XmlDocument doc = new XmlDocument();
            doc.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            navcol.AddNavigator(doc.CreateNavigator(), "xqt");
            string strnode = doc.DocumentElement.FirstChild.FirstChild.Name;
            xqry = "FOR $a IN document('xqt')//" + strnode + " RETURN ( max($a//@LID) )";
            XQueryExpression expr = new XQueryExpression(xqry);
            XQueryNavigator nav;
            nav = expr.Execute(navcol);
            intmaxval = Convert.ToInt32(nav.ToXml().ToString());
            return intmaxval;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    /// <summary>
    /// Method Created By: Dilip Kumar Tripathy
    /// Created Date     : 22-03-2012
    /// Purpose          : To Rename the Deleted Node of XML file
    /// </summary>   
    private void RenameDeletedNode(XmlDocument xmldoc, XmlNode xmlnode, XmlNode refXmlNode)
    {
        try
        {
            // Code added by Dilip Kumar Tripathy on dated 
            //if (xmlnode.Attributes["PID"].Value == "1")
            //{
            XmlNode newNode = xmldoc.CreateElement(xmlnode.Attributes["NAME"].Value.Replace(" ", "") + xmlnode.Attributes["HID"].Value + xmlnode.Attributes["LID"].Value);

            //Append Attribute ID 

            XmlAttribute xAttr = xmldoc.CreateAttribute("ID");
            xAttr.Value = xmlnode.Attributes["ID"].Value;
            newNode.Attributes.Append(xAttr);
            //Append Attribute HID
            xAttr = null;
            xAttr = xmldoc.CreateAttribute("HID");
            xAttr.Value = xmlnode.Attributes["HID"].Value;
            newNode.Attributes.Append(xAttr);
            //Append Attribute TOT
            xAttr = null;
            xAttr = xmldoc.CreateAttribute("TOT");
            xAttr.Value = xmlnode.Attributes["TOT"].Value;
            newNode.Attributes.Append(xAttr);
            //Append Attribute LID
            xAttr = null;
            xAttr = xmldoc.CreateAttribute("LID");
            xAttr.Value = xmlnode.Attributes["LID"].Value;
            newNode.Attributes.Append(xAttr);
            //Append Attribute PID
            xAttr = null;
            xAttr = xmldoc.CreateAttribute("PID");
            xAttr.Value = xmlnode.Attributes["PID"].Value;
            newNode.Attributes.Append(xAttr);
            //Append Attribute ALIAS
            xAttr = null;
            xAttr = xmldoc.CreateAttribute("ALIAS");
            xAttr.Value = xmlnode.Attributes["ALIAS"].Value;
            newNode.Attributes.Append(xAttr);

            //Append Attribute FLAG  
            xAttr = null;
            xAttr = xmldoc.CreateAttribute("FLAG");
            xAttr.Value = xmlnode.Attributes["FLAG"].Value;
            newNode.Attributes.Append(xAttr);
            //Append Attribute NAME
            xAttr = null;
            xAttr = xmldoc.CreateAttribute("NAME");
            xAttr.Value = xmlnode.Attributes["NAME"].Value;
            newNode.Attributes.Append(xAttr);

            //Append Attribute DESC
            xAttr = null;
            xAttr = xmldoc.CreateAttribute("DESC");
            xAttr.Value = xmlnode.Attributes["DESC"].Value;
            newNode.Attributes.Append(xAttr);
            //---------------
            newNode.InnerXml = xmlnode.InnerXml;
            xmlnode.ParentNode.InsertBefore(newNode, refXmlNode);
            xmlnode.ParentNode.RemoveChild(xmlnode);
            //}
        }
        catch 
        {

        }

    }
    /// <summary>
    /// To make Visible Visible true false to controls. 
    /// </summary>
    /// <param name="Condition"></param>
    public void Btnvisible(Boolean Condition)
    {
        btnAdd.Visible = Condition;
        btnEdit.Visible = Condition;
        btnClear.Visible = Condition;
        // btnshow.Visible = Condition; commented by Dilip Kumar Tripathy on dated 7-Mar-2012
    }
    /// <summary>
    /// Function Name  : Create_Copy_XMLFile
    /// Purpose : To Create and Copy a File
    /// Created By : Priyabrat Routray
    /// Created on  : 22/10/2010
    /// Parameter Name : strFileName
    /// Parameter Datatype : String
    /// Return Parameter Name : None
    /// Return Parameter Type : None
    /// </summary>
    private void Create_Copy_File(string strSrcFileName, string strDstFileName)
    {
        try
        {
            System.IO.File.Create(strSrcFileName);
            DataSet objDs = new DataSet();
            objDs.ReadXml(strDstFileName);
            objDs.WriteXml(strDstFileName);
            objDs.Dispose();
        }
        catch
        {
            throw new Exception("Invalid Operation. File type is not Compactible.");
        }
    }
    #endregion

    #region "Append a Line to File"
    /// <summary>
    /// Function Name : AppendFile
    /// Parameters Name : strErrLogpath, srtErrDesc, strFunctionName
    /// parameters Type : String, String, String
    /// Purpose : To write a Line within a Error log File
    /// Created By : Priyabrat Routray
    /// Date : 22/10/2010
    /// </summary>
    /// <param name="strErrLogpath"></param>
    public void AppendFile(string strErrLogpath, string srtErrDesc, string strFunctionName)
    {
        if (!File.Exists(strErrLogpath))
        {
            File.Create(strErrLogpath);
        }
        FileInfo file1 = new FileInfo(strErrLogpath);
        StreamWriter sw = File.AppendText(file1.FullName);
        sw.WriteLine("********************************************************************");
        sw.WriteLine("Date :" + DateTime.Now.ToString());
        sw.WriteLine("Function Name :" + strFunctionName);
        sw.WriteLine("Error Description :" + srtErrDesc); // Writing a string directly to the file
        sw.WriteLine("********************************************************************");		// Writing content read from the textbox in the form	
        sw.Close();
    }
    #endregion

    #region "treeView Ecvents"
    protected void treemenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        try
        {
            ClientScript.RegisterStartupScript(GetType(), "", "ShowHideTrDiv(1)", true);
            btnDelete.Visible = true;
            txtrootName.Visible = true;
            txtrootSummary.Visible = true;
            txtsubnodeNo.Visible = true;

            XmlDocument xmldoc1 = new XmlDocument();
            xmldoc1.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            NodeVal = treemenu.SelectedNode.Value;
            if (treemenu.SelectedNode.Text == "Manage Hierarchy")
            {
                ViewState["MH"] = 1;
                btnDelete.Visible = false;
            }
            else
            {
                ViewState["MH"] = 0;
                btnDelete.Visible = true;
            }
            trChildNode = treemenu.SelectedNode;
            string namess = treemenu.SelectedNode.Parent.Text;
            foreach (TreeNode xParentNd in treemenu.SelectedNode.Parent.ChildNodes)
            {
                if (xParentNd.Text == treemenu.SelectedNode.Text)
                {
                    selectIndex = Convert.ToInt32(treemenu.SelectedNode.Value) - 1;//treemenu.SelectedNode.Parent.ChildNodes.IndexOf(xParentNd);
                    goto findxml;
                }
            }
        findxml:
            FindXmlData(selectIndex, treemenu.SelectedNode.Text);

            if (trChildNode != null)
            {
                if (Convert.ToInt32(txtsubnodeNo.Text.Trim()) > 0)
                {
                    btnEdit.Visible = true;
                    btnAdd.Visible = false;
                    txtsubnodeNo.ReadOnly = false;//Code added by Dilip Kumar Tripathy on dated 07-04-2012
                }
                else
                {
                    //Code added by Dilip Kumar Tripathy on dated 07-04-2012
                    XmlNode xmlnode = xmldoc1.GetElementsByTagName(("Manage Hierarchy").Replace(" ", ""))[0];
                    if (xmlnode.HasChildNodes == true)
                    {
                        btnAdd.Visible = false;
                        btnEdit.Visible = true;
                        txtsubnodeNo.ReadOnly = false;//Code added by Dilip Kumar Tripathy on dated 07-04-2012


                    }
                    else
                    {
                        btnAdd.Visible = true;
                        btnEdit.Visible = false;
                        txtsubnodeNo.ReadOnly = false;//Code added by Dilip Kumar Tripathy on dated 07-04-2012


                    }
                    //-----------------------------


                }
                if (trChildNode.ChildNodes.Count == 0)
                {
                    if (trChildNode.Text != "Manage Hierarchy")
                    {
                        if (trChildNode.Parent.Text != "Manage Hierarchy")
                        {
                            btnAdd.Visible = true;
                            btnEdit.Visible = false;//Code added by Dilip Kumar Tripathy on dated 07-04-2012
                            // txtsubnodeNo.ReadOnly = true;//Code added by Dilip Kumar Tripathy on dated 07-04-2012
                        }
                        //Code added by Dilip Kumar Tripathy on dated 07-04-2012
                        else
                        {
                            btnAdd.Visible = true;
                            btnEdit.Visible = false;
                            txtsubnodeNo.ReadOnly = false;//Code added by Dilip Kumar Tripathy on dated 07-04-2012

                        }
                        //----------end by dilip------------------------
                    }
                    else
                    {
                        //Code added by Dilip Kumar Tripathy on dated 07-04-2012
                        XmlNode xmlnode = xmldoc1.GetElementsByTagName(("Manage Hierarchy").Replace(" ", ""))[0];
                        if (xmlnode.HasChildNodes == true)
                        {
                            btnAdd.Visible = false;
                            btnEdit.Visible = true;
                            txtsubnodeNo.ReadOnly = false;//Code added by Dilip Kumar Tripathy on dated 07-04-2012

                        }
                        else
                        {
                            btnAdd.Visible = true;
                            btnEdit.Visible = false;
                            txtsubnodeNo.ReadOnly = false;//Code added by Dilip Kumar Tripathy on dated 07-04-2012

                        }
                        //-----------------------------

                    }
                }

            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    private void ShowLevels(int intArg)
    {
        try
        {
            ClientScript.RegisterStartupScript(GetType(), "", "ShowHideTrDiv(1)", true);
            btnDelete.Visible = true;
            txtrootName.Visible = true;
            txtrootSummary.Visible = true;
            txtsubnodeNo.Visible = true;

            XmlDocument xmldoc1 = new XmlDocument();
            xmldoc1.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            NodeVal = intArg.ToString();
            btnDelete.Visible = false;

            trChildNode = treemenu.Nodes[0].ChildNodes[0].ChildNodes[0];

            //Code Addred By Dilip Kumar Tripathy on dated 15-Jun-2012
            //Purpose To make  availavle th DEfault page for Loc Administrator
            if (Session["adminstat"].ToString().ToLower() == "super")
            {
                FindXmlData(0, treemenu.Nodes[0].ChildNodes[0].ChildNodes[0].Text);
            }
            else
            {
                FindXmlData(0, treemenu.Nodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[0].Text);
            }


            if (Convert.ToInt32(txtsubnodeNo.Text.Trim()) > 0)
            {
                btnEdit.Visible = true;
                btnAdd.Visible = false;

            }
            else
            {
                btnEdit.Visible = false;
                btnAdd.Visible = true;
            }


        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {

        }
    }
    #endregion

    #region "Dynamic Node Addition"
    //Coding Added by Ashish Patnaik on 15-Mar-2012 for dynamic generation of nodes
    //------------------------------------------------------------------------------
    private void CreateDynamicTable()
    {
        int tblRows = 0;
        if (Session["count"] == null || Session["count"].ToString() == "")
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            string strNodeName = "Manage Hierarchy";
            XmlNode xmlnode = xmldoc.GetElementsByTagName(strNodeName.Replace(" ", ""))[0];
            tblRows = int.Parse(xmlnode.Attributes["TOT"].Value);
        }
        else
        {
            tblRows = int.Parse(Session["count"].ToString());
        }
        int tblCols = 3;
        string k = "";
        int intCnt = 1;
        for (int i = 0; i < tblRows; i++)
        {
            HtmlTableRow tr = new HtmlTableRow();
            for (int j = 0; j < tblCols; j++)
            {
                HtmlTableCell tc = new HtmlTableCell();
                if (j == 2)
                {
                    TextBox txtBox = new TextBox();
                    txtBox.ID = "txt" + Convert.ToString(i + 1);
                    txtBox.Width = Unit.Pixel(200);
                    txtBox.MaxLength = 50;
                    txtBox.AutoCompleteType = AutoCompleteType.None;
                    tc.Controls.Add(txtBox);
                    tr.Cells.Add(tc);
                }
                else if (j == 0)
                {
                    Label lbl = new Label();
                    k = Convert.ToString(i + (j + 1));
                    int intResult = int.Parse(k) % 2;
                    if (intResult == 0)
                    {
                        lbl.Text = "Node" + intCnt + " Alias";
                    }
                    else
                    {
                        lbl.Text = "Node" + intCnt + " Name<font color='#FF0000'>*</font>";
                    }
                    tc.Controls.Add(lbl);
                    tc.Width = "140";
                    tr.Cells.Add(tc);
                }
                //Code added by Dilip Kumar Tripathy on dated 10-Apr-2012
                //Purpse : To add a another coumn in dynamic table
                else if (j == 1)
                {
                    Label lbl = new Label();
                    lbl.Text = ":";
                    tc.Controls.Add(lbl);
                    tc.Width = "7";
                    tr.Cells.Add(tc);
                }
                //---Code ended by Dilip on same date---------
            }
            tblnode.Rows.Add(tr);
            tblnode.EnableViewState = true;
            ViewState["tbl"] = true;
            if ((i + 1) % 2 == 0)
            {
                intCnt += 1;
                Session["intCnt"] = intCnt;
            }

        }
    }
    private void AddDynamicTable(int tablerows)
    {
        int tblCols = 3;
        string k = "";
        sbyte sbCnt = 0;
        if (Session["intCnt"] == null)
        {
            sbCnt = 1;
        }
        else
        {
            sbCnt = Convert.ToSByte(Session["intCnt"]);
        }
        for (int i = 1; i <= tablerows; i++)
        {
            HtmlTableRow tr = new HtmlTableRow();
            for (int j = 0; j < tblCols; j++)
            {
                k = Convert.ToString(tblnode.Rows.Count + 1);
                HtmlTableCell tc = new HtmlTableCell();
                if (j == 2)
                {
                    TextBox txtBox = new TextBox();
                    txtBox.ID = "txt" + k;
                    txtBox.MaxLength = 50;
                    txtBox.Width = Unit.Pixel(200);
                    txtBox.AutoCompleteType = AutoCompleteType.None;
                    tc.Controls.Add(txtBox);
                    tr.Cells.Add(tc);
                }

                else if (j == 0)
                {

                    Label lbl = new Label();
                    int intResult = int.Parse(k) % 2;
                    if (intResult == 0)
                    {
                        lbl.Text = "Node" + sbCnt + " Alias";
                    }
                    else
                    {
                        lbl.Text = "Node" + sbCnt + " Name<font color='#FF0000'>*</font>";
                    }

                    tc.Controls.Add(lbl);
                    tc.Width = "140";
                    tr.Cells.Add(tc);
                }
                //Code added by Dilip Kumar Tripathy on dated 10-Apr-2012
                //Purpse : To add a another coumn in dynamic table
                else if (j == 1)
                {
                    Label lbl = new Label();
                    lbl.Text = ":";
                    tc.Controls.Add(lbl);
                    tc.Width = "7";
                    tr.Cells.Add(tc);
                }
                //---Code ended by Dilip on same date---------
            }
            tblnode.Rows.Add(tr);
            tblnode.EnableViewState = true;
            ViewState["tbl"] = true;
            if (i % 2 == 0)
            {
                sbCnt += 1;

            }
        }
    }
    private void DeleteDynamicTable(int sessionrow, int tablerows)
    {
        for (int i = tablerows; i > sessionrow; i--)
        {
            HtmlTableRow tr = tblnode.Rows[i - 1];
            tblnode.Rows.Remove(tr);
            tblnode.EnableViewState = true;
            ViewState["tbl"] = true;

        }
    }
    protected override object SaveViewState()
    {
        object[] newViewState = new object[2];
        List<string> txtValues = new List<string>();

        foreach (HtmlTableRow row in tblnode.Controls)
        {
            foreach (HtmlTableCell cell in row.Controls)
            {
                if (cell.Controls[0] is TextBox)
                {
                    txtValues.Add(((TextBox)cell.Controls[0]).Text);
                }
            }
        }

        newViewState[0] = txtValues.ToArray();
        newViewState[1] = base.SaveViewState();
        return newViewState;
    }
    protected override void LoadViewState(object savedState)
    {
        //if we can identify the custom view state as defined in the override for SaveViewState
        if (savedState is object[] && ((object[])savedState).Length == 2 && ((object[])savedState)[0] is string[])
        {
            object[] newViewState = (object[])savedState;
            string[] txtValues = (string[])(newViewState[0]);
            if (txtValues.Length > 0)
            {

                //re-load tables
                CreateDynamicTable();
                int i = 0;
                foreach (HtmlTableRow row in tblnode.Controls)
                {
                    foreach (HtmlTableCell cell in row.Controls)
                    {
                        if (cell.Controls[0] is TextBox && i < txtValues.Length)
                        {
                            ((TextBox)cell.Controls[0]).Text = txtValues[i].ToString();
                        }
                    }
                    i += 1;
                }
            }
            //load the ViewState normally
            base.LoadViewState(newViewState[1]);
        }
        else
        {
            base.LoadViewState(savedState);
        }
    }
    protected void LodSessionvalues()
    {
        int tblRows = 0;
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
        string strNodeName = "Manage Hierarchy";
        XmlNode xmlnode = xmldoc.GetElementsByTagName(strNodeName.Replace(" ", ""))[0];
        tblRows = int.Parse(xmlnode.Attributes["TOT"].Value);
        if (tblRows > 0)
        {
            if (Session["txtValues"] != null && tblnode.Rows.Count > 0)
            {
                string[] txtValues2 = (string[])Session["txtValues"];
                for (int l = 0; l < txtValues2.Length; l++)
                {
                    ((TextBox)tblnode.Rows[l].Cells[1].Controls[0]).Text = txtValues2[l].ToString();
                }
            }
        }
    }

    #endregion

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (txtsubnodeNo.Text != string.Empty)
        {
            if (int.Parse(txtsubnodeNo.Text) >= int.Parse(totalNo))
            {
                Session["count"] = (Convert.ToInt16(txtsubnodeNo.Text) * 2).ToString();
                int newrows = 0;
                if (Session["count"] != null)
                {
                    if (int.Parse(Session["count"].ToString()) > tblnode.Rows.Count)
                    {
                        newrows = (int.Parse(Session["count"].ToString())) - tblnode.Rows.Count;
                        AddDynamicTable(newrows);
                    }
                    else if (int.Parse(Session["count"].ToString()) < tblnode.Rows.Count)
                    {
                        DeleteDynamicTable(int.Parse(Session["count"].ToString()), tblnode.Rows.Count);
                    }
                }
            }
            else { txtsubnodeNo.Focus(); }

        }
        else
        {
            txtsubnodeNo.Text = Session["count"].ToString();
            txtsubnodeNo.Focus();
        }
    }

}


