using System;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;
using Admin.CommonFunction;
using System.Configuration;


public partial class AdminConsoleLeftMenu : System.Web.UI.UserControl
{
    #region Varibles and Objects
    TreeNode trnode = new TreeNode();
    int intIndex = -1;

    static ArrayList alUserLoc = new ArrayList();
    public int intCnt = 0, intLastNodeCnt = 0;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
        Response.Cache.SetNoStore();
        Response.AppendHeader("Pragma", "no-cache");

        if (Session["UserId"] == null)
        {
            Response.Redirect("~/SessionRedirect.aspx");
        }
        if (Request.QueryString["isparent"] != null)
        {
            Session["LinkIndex"] = 0;
        }
        if (Request.QueryString["kwt0e"] != null)
        {
            Session["LinkIndex"] = 0;
        }
        // TreeViewMenu.Attributes.Add("onclick", "return chk();");
        if (!IsPostBack)
        {
            Session["menuCnt"] = 0;//Code Added By Dilip Kumar Tripathy on dated 24-jan-2012
            alUserLoc = Admin.CommonFunction.CommonFunction.GetUserLocation(int.Parse(Session["UserId"].ToString()));
            loadData();

        }

    }
    protected void loadData()
    {
        try
        {
            string[] arrMenu = { "ManageLinks", "ManageUser", "RoamingFacilitates", "OfficeTimings", "Reports", "PersonalizeLoginScreen", "PersonalizeInnerpage","ManageLogIn" };
            ArrayList menuList = new ArrayList();

            foreach (string strMenu in arrMenu)
                menuList.Add(strMenu);
            string strRootNodeName = null;
            if (Session["adminstat"].ToString() == "super")
            {
                strRootNodeName = "Super Administrator";
            }
            else
            {
                strRootNodeName = "Location Administrator";
            }
            XmlDocument dom = new XmlDocument();
            dom.Load(Server.MapPath("~/Console/Menu/KwantifyMenu.xml"));
            TreeViewMenu.Nodes.Clear();
            TreeViewMenu.Nodes.Add(new TreeNode(strRootNodeName));
            TreeNode tNode = new TreeNode();
            tNode = TreeViewMenu.Nodes[0];
            if (dom.DocumentElement.HasChildNodes)
            {
                AddNode(dom.DocumentElement.ChildNodes[0], tNode, 0, menuList);
                TreeViewMenu.ExpandAll();
            }
            for (int i = 0; i < TreeViewMenu.Nodes[0].ChildNodes[0].ChildNodes.Count; i++)
            {
                TreeViewMenu.Nodes[0].ChildNodes[0].ChildNodes[i].SelectAction = TreeNodeSelectAction.None;
                string s = TreeViewMenu.Nodes[0].ChildNodes[0].ChildNodes[i].Text;
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            //throw new Exception(ex.Message);
        }
        finally
        {

        }
    }

    private void AddNode(XmlNode inXmlNode, TreeNode inTreeNode, int NodeIndex, ArrayList menuList)
    {
        try
        {
            XmlNode xNode = null;
            TreeNode tNode;
            XmlNodeList nodeList;
            string addAttr = null;
            int i;
            bool flag = false;
            // Loop through the XML nodes until the leaf is reached.
            // Adding nodes to the TreeView during the looping process.
            ////inTreeNode.SelectAction = TreeNodeSelectAction.None;
            TreeViewMenu.Nodes[0].SelectAction = TreeNodeSelectAction.None;
            int j = 0;
            if (inXmlNode.HasChildNodes)
            {
                nodeList = inXmlNode.ChildNodes;
                for (i = 0; i <= (nodeList.Count - 1); i++)
                {
                    //*************Code Addes By Dilip Kumar Tripathy On Dated 1-Feb-2012
                    //Purpose     : To Separate Links for Super Admin and Location Admin
                    //Modified By : Dilip Kumar Tripathy On Dated 3-Feb-2012 to add the new else if for 'OfficeTimings'
                    if (inXmlNode.Name == "ManageHierarchy")
                    {
                        if (alUserLoc.Contains(inXmlNode.ChildNodes[i].Attributes["NAME"].Value))
                        {
                            xNode = inXmlNode.ChildNodes[i];
                        }
                        else
                        {
                            xNode = null;
                        }
                    }
                    else if (inXmlNode.Name == "ManageLinks")
                    {
                        if (Session["adminstat"].ToString() == "super")
                        {
                            xNode = inXmlNode.ChildNodes[i];
                        }
                        else
                        {
                            if (inXmlNode.ChildNodes[i].Attributes["NAME"].Value == "Global Link" || inXmlNode.ChildNodes[i].Attributes["NAME"].Value == "Primary Link")
                            {
                                xNode = inXmlNode.ChildNodes[i];
                            }
                            else
                            {
                                xNode = null;
                            }
                        }

                    }
                    else if (inXmlNode.Name == "ManageDatabase")
                    {
                        if (Session["userName"].ToString() == "superadmin")
                        {
                            xNode = inXmlNode.ChildNodes[i];
                        }
                        else
                        {
                            xNode = null;
                        }

                    }
                    else if (inXmlNode.Name == "ManageUser")
                    {

                        if (Session["adminstat"].ToString() == "super")
                        {
                            xNode = inXmlNode.ChildNodes[i];
                        }
                        else
                        {
                            if (inXmlNode.ChildNodes[i].Attributes["NAME"].Value == "Assign Admin" || inXmlNode.ChildNodes[i].Attributes["NAME"].Value == "Location" || inXmlNode.ChildNodes[i].Attributes["NAME"].Value == "Management Group")
                            {
                                xNode = null;
                            }
                            else
                            {
                                xNode = inXmlNode.ChildNodes[i];
                            }
                        }

                    }
                    else if (inXmlNode.Name == "ManageLogIn")
                    {
                        if (Session["adminstat"].ToString() == "super")
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
                    if (xNode != null)
                    {
                        //********* Code Ended By Dilip Kumar Tripathy On Same Date
                        if (inXmlNode.ChildNodes[i].Attributes["FLAG"].Value != "1")
                        {
                            //--------------
                            if (NodeIndex == 0)
                            {
                                if (!xNode.Name.Contains(System.Configuration.ConfigurationManager.AppSettings["HierMenuName"].ToString()))
                                {
                                    if (xNode.ParentNode.Name == System.Configuration.ConfigurationManager.AppSettings["HierMenuName"].ToString())
                                    {

                                        if (Session["adminstat"].ToString() == "super")
                                        {
                                            //Code Added by Dilip Kumar Tripathy on dated 05-Apr-2012
                                            //Purpose: To separate the navigate url of treeview nodes
                                            if (xNode.Attributes["NAME"].Value == "Manage Hierarchy")
                                            {
                                                TreeNode objNode = new TreeNode(xNode.Attributes["NAME"].Value, xNode.Attributes["ID"].Value, "", "../AdminDefault.aspx?kwt0e=" + CommonFunction.EncryptData("1"), "_top");
                                                objNode.SelectAction = TreeNodeSelectAction.Expand;
                                                inTreeNode.ChildNodes.Add(objNode);
                                            }
                                            else
                                            {
                                                if (xNode.Attributes["NAME"].Value == "Manage Database")
                                                {
                                                    if (Session["userName"].ToString() == "superadmin")
                                                    {
                                                        TreeNode objNode = new TreeNode(xNode.Attributes["NAME"].Value, xNode.Attributes["ID"].Value);
                                                        objNode.SelectAction = TreeNodeSelectAction.Expand;
                                                        inTreeNode.ChildNodes.Add(objNode);
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
                                                    TreeNode objNode = new TreeNode(xNode.Attributes["NAME"].Value, xNode.Attributes["ID"].Value);
                                                    objNode.SelectAction = TreeNodeSelectAction.Expand;
                                                    inTreeNode.ChildNodes.Add(objNode);
                                                }
                                            }
                                            //---------code Ended By Dilip--------------
                                        }
                                        else
                                        {
                                            if (xNode.Attributes["NAME"].Value == "Manage Hierarchy")
                                            {
                                                TreeNode objNode = new TreeNode(xNode.Attributes["NAME"].Value, xNode.Attributes["ID"].Value, "", "../AdminDefault.aspx?kwt0e=" + CommonFunction.EncryptData("2"), "_top");
                                                objNode.SelectAction = TreeNodeSelectAction.Expand;
                                                inTreeNode.ChildNodes.Add(objNode);
                                            }
                                            else
                                            {
                                                TreeNode objNode = new TreeNode(xNode.Attributes["NAME"].Value, xNode.Attributes["ID"].Value, "");
                                                objNode.SelectAction = TreeNodeSelectAction.Expand;
                                                inTreeNode.ChildNodes.Add(objNode);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (menuList.Contains(xNode.ParentNode.Name))
                                        {
                                            addAttr = xNode.Attributes["ID"].Value;
                                            AddpathToNode(inTreeNode, xNode, addAttr, "", "mainFrame");
                                        }
                                        else
                                        {
                                            if (xNode.Name == "ManageHierarchy" || xNode.ParentNode.Name == "ManageHierarchy")
                                            {
                                                addAttr = xNode.Attributes["NAME"].Value + "|" + xNode.Attributes["HID"].Value + "|" + xNode.Attributes["LID"].Value + "|" + xNode.Attributes["PID"].Value;
                                                TreeNode objNode = new TreeNode(xNode.Attributes["NAME"].Value, addAttr);
                                                objNode.SelectAction = TreeNodeSelectAction.Expand;
                                                inTreeNode.ChildNodes.Add(objNode);
                                            }
                                            else
                                            {
                                                //if (xNode.HasChildNodes)
                                                //{
                                                    addAttr = xNode.Attributes["NAME"].Value + "|" + xNode.Attributes["HID"].Value + "|" + xNode.Attributes["LID"].Value + "|" + xNode.Attributes["PID"].Value;
                                                    string strSafeAsciiCode = CommonFunction.EncryptData(addAttr);
                                                    inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes["NAME"].Value, addAttr, "", "../Manage_Hierarchy/AdminLeveldetails.aspx?isparent=" + xNode.ParentNode.Attributes["NAME"].Value + "&att=" + strSafeAsciiCode + "", "_top"));
                                                //}
                                                //else
                                                //{
                                                //    addAttr = xNode.Attributes["ID"].Value;
                                                //    AddpathToNode(inTreeNode, xNode, addAttr, "", "mainFrame");
                                                //}
                                            }
                                            //Code added by Dilip Kumar Tripathy on dated 10-Apr-2012
                                            //Purpose : To Sub Node of ManageHierarchy childnodes
                                            //else if (xNode.ParentNode.ParentNode.Name == "ManageHierarchy" || xNode.ParentNode.ParentNode.ParentNode.Name == "ManageHierarchy" || xNode.ParentNode.ParentNode.ParentNode.ParentNode.Name == "ManageHierarchy")
                                            //{
                                            //    addAttr = xNode.Attributes["NAME"].Value + "|" + xNode.Attributes["HID"].Value + "|" + xNode.Attributes["LID"].Value + "|" + xNode.Attributes["PID"].Value;
                                            //    string strSafeAsciiCode = CommonFunction.EncryptData(addAttr);
                                            //    inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes["NAME"].Value, addAttr, "", "../Manage_Hierarchy/AdminLeveldetails.aspx?isparent=" + xNode.ParentNode.Attributes["NAME"].Value + "&att=" + strSafeAsciiCode + "", "_top"));
                                            //}
                                            //else
                                            //{
                                            //    addAttr = xNode.Attributes["ID"].Value;
                                            //    AddpathToNode(inTreeNode, xNode, addAttr, "", "mainFrame");
                                            //}
                                        }
                                    }
                                }
                                else
                                    inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes["NAME"].Value, xNode.Attributes["ID"].Value));
                            }
                            else
                            {
                                inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes["NAME"].Value));
                            }
                            //-----------------------
                            if (flag == true)
                            {
                                tNode = inTreeNode.ChildNodes[i - intCnt];
                            }
                            else
                            {
                                tNode = inTreeNode.ChildNodes[j];
                                j++;
                            }
                            if (xNode.InnerText.Trim() != "")
                            {
                                AddNode(xNode, tNode, 1, menuList);
                            }
                            else
                            {
                                AddNode(xNode, tNode, 0, menuList);
                            }
                        }
                        else
                        {
                            if (xNode.ParentNode.Name == "ManageHierarchy")
                            {
                                intCnt = 0;
                            }
                            flag = true;
                            intCnt++;

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
                    inTreeNode.Text = (inXmlNode.Attributes["NAME"].Value);
                }
                else
                {
                    inTreeNode.Text = (inXmlNode.OuterXml).Trim();
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
    private void AddXmlNode(XmlNode inXmlNode, TreeNode inTreeNode, int NodeIndex)
    {
        try
        {
            int j = 0;
            if (inXmlNode.Name == "ManageHierarchy" || inXmlNode.Name == ConfigurationManager.AppSettings["HierMenuName"].ToString() || inXmlNode.Name == "Menu" || inXmlNode.ParentNode.Name == "ManageHierarchy")
            {
                XmlNode xNode;
                TreeNode tNode;
                XmlNodeList nodeList;
                int i;
                // Loop through the XML nodes until the leaf is reached.
                // Adding nodes to the TreeView during the looping process.
                ////inTreeNode.SelectAction = TreeNodeSelectAction.None;
                TreeViewMenu.Nodes[0].SelectAction = TreeNodeSelectAction.None;
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
                                if (alUserLoc.Contains(inXmlNode.ChildNodes[i].Attributes["NAME"].Value))
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
                                    if (Session["adminstat"].ToString() == "super")
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
                                    inTreeNode.ChildNodes.Add(new TreeNode(xNode.Attributes["NAME"].Value, xNode.Attributes["ID"].Value));
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
                        inTreeNode.Text = (inXmlNode.Attributes["NAME"].Value).Trim();
                    }
                    else
                    {
                        inTreeNode.Text = (inXmlNode.OuterXml).Trim();
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
    public void AddpathToNode(TreeNode tn, XmlNode xn, string val, string imppath, string frame)
    {
        string strSafeAscii = CommonFunction.EncryptData(tn.Parent.Text.ToString() + "~" + xn.ParentNode.Attributes["NAME"].Value.ToString() + "~" + xn.Attributes["NAME"].Value);
        switch (xn.Attributes["NAME"].Value)
        {
            //Manage Links
            case "Function Master":
                //tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_Links/AdminFunctionMaster.aspx?Parent=" + tn.Parent.Text.ToString() + "&firstlink=" + xn.ParentNode.Name.ToString() + "&lastlink=" + xn.Attributes["NAME"].Value, frame));
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_Links/AdminFunctionMaster.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Global Link":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_Links/admin_GlobalLink.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Primary Link":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_Links/adminPrimaryLink.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Button":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_application/adminbuttonmaster.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Tab Master":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_application/AdminTabMaster.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Assign Link":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_Links/admin_AssignLink.aspx?att=" + strSafeAscii, "_top"));
                break;
            //Manage User
            case "Add User":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_User/AdminAddUser.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "User Profile":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_User/AdminUserProfile.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Set Permission":

                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_User/SetPermission.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Designation":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_User/DesignationMaster.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Location":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_User/PhysicalLocation.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Management Group":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_User/AdminGroupManagement.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Grade Master":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_User/GradeMaster.aspx?att=" + strSafeAscii, "_top"));
                break;
            //Reports
            case "Admin Reports":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Reports/AdminReports.aspx?att=" + strSafeAscii, "_top"));
                break;
            //Office Timings
            case "Shift Master":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Office_Timing/AdminShiftMaster.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Office Time":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Office_Timing/AdminOfficeTime.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Flexi Time":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Office_Timing/AdminFlexiTime.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Shift Assignment":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Office_Timing/AdminShiftAssignment.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Assign Admin":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_Hierarchy/AssignAdmin.aspx?att=" + strSafeAscii, "_top"));
                break;
            //Roaming Facilitates
            case "Roaming Access":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../RoamingAccess/admin_ManageRoaming.aspx?att=" + strSafeAscii, "_top"));
                break;
            //Personalize Login Screen
            case "Color Scheme":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Personalise_LoginScreen/adminCustomizeLayout.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Login Logo":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Personalise_LoginScreen/adminCustomizeLayoutLogo.aspx?att=" + strSafeAscii, "_top"));
                break;
            //Personalize inner Page
            case "Logo":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Personalise_InnerPage/adminCustomizeLayoutInner.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Header Footer":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Personalise_InnerPage/adminCustomizeLayoutHeader.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "BackUp And Restore":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_Database/Admin_BackUpRestoreDatabse.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Configuration Master":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_LogIn/Configuration.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Reset Password":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_LogIn/PasswordReset.aspx?att=" + strSafeAscii, "_top"));
                break;
            case "Proxy Login":
                tn.ChildNodes.Add(new TreeNode(xn.Attributes["NAME"].Value, val, imppath, "../Manage_LogIn/ProxyLogin.aspx?att=" + strSafeAscii, "_top"));
                break;

        }
    }

    protected void TreeViewMenu_SelectedNodeChanged(object sender, EventArgs e)
    {
        Session["hidcid"] = 0;
        if (TreeViewMenu.SelectedNode.Text == "Manage Hierarchy")
        {
            loadData();
        }
        //TreeViewMenu.CollapseAll();
        //TreeViewMenu.SelectedNode.Expand();
    }

    protected void TreeViewMenu_TreeNodeExpanded(object sender, TreeNodeEventArgs e)
    {

        if (e.Node.Text != "Super Administrator" && e.Node.Text != "Location Administrator")
        {
            if (e.Node.Parent.Text == "Super Administrator" || e.Node.Parent.Text == "Location Administrator")
            {
                foreach (TreeNode rootNode in TreeViewMenu.Nodes)
                {
                    if (e.Node.Text == rootNode.ChildNodes[0].Text)
                    {
                        e.Node.SelectAction = TreeNodeSelectAction.Expand;
                        e.Node.Expand();
                        foreach (TreeNode rootNodeChilds2 in rootNode.ChildNodes)
                        {
                            if (rootNodeChilds2.Text != rootNode.ChildNodes[0].Text)
                            {
                                rootNodeChilds2.Collapse();
                                rootNodeChilds2.SelectAction = TreeNodeSelectAction.Expand;
                            }
                        }
                        if (e.Node.ChildNodes.Count > 0)
                        {
                            foreach (TreeNode tcn in e.Node.ChildNodes)
                            {
                                if (Request.QueryString["isparent"] != null)
                                {
                                    if (tcn.Text == Request.QueryString["isparent"].ToString())
                                    {
                                        tcn.SelectAction = TreeNodeSelectAction.Expand;
                                        tcn.Expand();
                                    }
                                    else
                                    {
                                        tcn.SelectAction = TreeNodeSelectAction.Expand;
                                        tcn.Collapse();
                                    }

                                }
                                else
                                {
                                    tcn.SelectAction = TreeNodeSelectAction.Expand;
                                    tcn.Collapse();
                                }
                            }
                        }
                        if (Session["LinkIndex"] != null)
                        {
                            intIndex = Convert.ToInt32(Session["LinkIndex"]);
                        }

                    }
                    else
                    {
                        int intCount = 0;
                        if (e.Node.Parent.Text == rootNode.Text)
                        {

                            foreach (TreeNode rootNodeChilds2 in rootNode.ChildNodes)
                            {
                                intCount += 1;
                                if (rootNodeChilds2.Text == e.Node.Text)
                                {
                                    if (e.Node.Text == rootNode.ChildNodes[rootNode.ChildNodes.Count - 1].Text && int.Parse(Session["menuCnt"].ToString()) == 0)
                                    {
                                        if (intIndex == (rootNode.ChildNodes.Count - 1))
                                        {
                                            intLastNodeCnt += 1;
                                        }
                                        rootNodeChilds2.Collapse();
                                        if (intIndex >= 0)
                                        {
                                            if (intLastNodeCnt == 1)
                                            {
                                                Session["LinkIndex"] = intIndex;
                                                Session["menuCnt"] = 1;
                                                rootNode.ChildNodes[intIndex].Expand();

                                            }
                                            else
                                            {
                                                rootNode.ChildNodes[intIndex].Expand();
                                                Session["LinkIndex"] = intIndex;
                                            }


                                        }
                                        else
                                        {
                                            rootNode.ChildNodes[0].Expand();
                                            Session["LinkIndex"] = null;
                                        }
                                        Session["menuCnt"] = 1;
                                    }
                                    else
                                    {
                                        e.Node.Expand();
                                        rootNodeChilds2.Expand();
                                        Session["LinkIndex"] = intCount - 1;
                                    }
                                }
                                else
                                {
                                    rootNodeChilds2.SelectAction = TreeNodeSelectAction.Expand;
                                    rootNodeChilds2.Collapse();
                                }
                            }
                        }
                    }
                }
            }
        }

    }




}
