/******************************************************************************************************************
File Name           : ConfigPage.aspx.cs
Description         : Create configuration file
Created by          : Dilip Kumar Tripathy
Created On          : 27-Nov-2013
Modification History:

                        <CR no.>        <Date>           <Modified by>          <Modification Summary>                                                         
                         1             02-Nov-2012       Dilip Tripathy          Re-cretae KwantifyMenu.xml when HierMenuName key value is changed.Add RenameXmlMenuRoot() method to do this
                         2             03-Mar-2013       Dilip Tripathy          Update submit button click event.
                         3             04-Mar-2013       Dilip Tripathy          Add CretaeDBObjects() method to create database objects.
 *******************************************************************************************************************/
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Xml;
using System.IO;
using System.Data.SqlClient;
using System.Linq;

namespace KwantifyportalV5._1.Console
{

    public partial class ConfigPage : System.Web.UI.Page
    {
        #region Variables and Objects
        public string strSetFlag, strXmlDel, strHome, strLogout, strHier, strIsOracle;
        public Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
        AppSettingsSection appSettingsSection;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["Config"] != null)
            {
                if (ConfigurationManager.AppSettings["Config"].ToString() == "N")
                {
                    if (ConfigurationManager.AppSettings["Logout"] != null)
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["Logout"].ToString());
                    }
                }
            }

            if (!IsPostBack)
            {
                BindInitialConfig();
                BindModuleConfigTree();
            }
        }
        private void BindInitialConfig()
        {
            try
            {
                appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");
                string[] strAppKeys = appSettingsSection.Settings.AllKeys;
                if (strAppKeys.Contains("Home"))
                {
                    txtHomeVal.Text = configuration.AppSettings.Settings["Home"].Value.Trim();
                }
                if (strAppKeys.Contains("Logout"))
                {
                    txtLogoutVal.Text = configuration.AppSettings.Settings["Logout"].Value.Trim();
                }
                if (strAppKeys.Contains("HierMenuName"))
                {
                    txtMenuName.Text = configuration.AppSettings.Settings["HierMenuName"].Value.Trim();
                }
                if (strAppKeys.Contains("SetFlag"))
                {
                    if (configuration.AppSettings.Settings["SetFlag"].Value.ToUpper().Trim() == "Y")
                    {
                        rbtSFY.Checked = true;
                        rbtSFN.Checked = false;
                    }
                    else
                    {
                        rbtSFY.Checked = false;
                        rbtSFN.Checked = true;
                    }
                }

                if (strAppKeys.Contains("XmlDel"))
                {
                    if (configuration.AppSettings.Settings["XmlDel"].Value.ToUpper().Trim() == "Y")
                    {
                        rbtXDY.Checked = true;
                        rbtXDN.Checked = false;
                    }
                    else
                    {
                        rbtXDY.Checked = false;
                        rbtXDN.Checked = true;
                    }
                }
                if (ConfigurationManager.AppSettings["HierMenuName"] != null)
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
                    XmlAttribute rootDesc = (XmlAttribute)xmlDoc.SelectSingleNode("//Menu/" + ConfigurationManager.AppSettings["HierMenuName"].ToString() + "/ManageHierarchy/@DESC");
                    if (rootDesc != null)
                    {
                        txtRootSummary.Text = rootDesc.Value; // Set to new value.
                    }
                }
                if (!strAppKeys.Contains("Config"))
                {
                    configuration.AppSettings.Settings.Add("Config", "Y");
                    configuration.Save(ConfigurationSaveMode.Full);
                }
                if (!strAppKeys.Contains("UStatus"))
                {
                    configuration.AppSettings.Settings.Add("UStatus", "New");
                    configuration.Save(ConfigurationSaveMode.Full);
                    rbtnExistingUser.Checked = false;
                    rbtnExistingUser.Enabled = false;
                    rbtnNewUser.Enabled = true;
                }
                else
                {
                    if (ConfigurationManager.AppSettings["UStatus"].ToString() == "Existing")
                    {
                        rbtnNewUser.Checked = false;
                        rbtnNewUser.Enabled = false;
                        rbtnExistingUser.Checked = true;
                        rbtnExistingUser.Enabled = true;
                    }
                    else
                    {
                        rbtnNewUser.Checked = true;
                        rbtnNewUser.Enabled = true;
                        rbtnExistingUser.Checked = false;
                        rbtnExistingUser.Enabled = false;
                    }
                }
                if (strAppKeys.Contains("SesRediPage"))
                {
                    txtSessionRedirect.Text = configuration.AppSettings.Settings["SesRediPage"].Value.Trim();
                }
                AssignCheckBoxes();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + ex.Message + "');", true);
            }
            finally { }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateControls())
                {
                    if (chkGenConfig.Checked == true)
                    {
                        string[] strAdminKeys = { "SetFlag", "XmlDel", "SRV6", "HierMenuName", "Logout", "Home", "ProviderName", "SesRediPage" };
                        appSettingsSection = (AppSettingsSection)configuration.GetSection("appSettings");

                        string[] strAppsKeys = appSettingsSection.Settings.AllKeys;
                        if (appSettingsSection != null)
                        {
                            if (rbtSFN.Checked == true)
                            {
                                strSetFlag = "N";
                            }
                            else
                            {
                                strSetFlag = "Y";
                            }
                            if (rbtXDN.Checked == true)
                            {
                                strXmlDel = "N";
                            }
                            else
                            {
                                strXmlDel = "Y";
                            }

                            for (int i = 0; i < strAdminKeys.Length; i++)
                            {
                                if (!strAppsKeys.Contains(strAdminKeys[i].ToString()))
                                {
                                    if (strAdminKeys[i].ToString() == "ProviderName")
                                    {
                                        configuration.AppSettings.Settings.Add(strAdminKeys[i].ToString(), "System.Data.OracleClient");
                                    }
                                    else if (strAdminKeys[i].ToString() == "SRV6")
                                    {
                                        configuration.AppSettings.Settings.Add(strAdminKeys[i].ToString(), "N");
                                    }
                                    else
                                    {
                                        if (strAdminKeys[i].ToString() == "SetFlag")
                                        {
                                            configuration.AppSettings.Settings.Add(strAdminKeys[i].ToString(), strSetFlag);
                                        }
                                        else if (strAdminKeys[i].ToString() == "XmlDel")
                                        {
                                            configuration.AppSettings.Settings.Add(strAdminKeys[i].ToString(), strXmlDel);
                                        }
                                        else if (strAdminKeys[i].ToString() == "Home")
                                        {
                                            string strHome = txtHomeVal.Text.Substring(0, 2);
                                            if (strHome == "~/")
                                            {
                                                configuration.AppSettings.Settings.Add(strAdminKeys[i].ToString(), txtHomeVal.Text);
                                            }
                                            else
                                            {
                                                configuration.AppSettings.Settings.Add(strAdminKeys[i].ToString(), "~/" + txtHomeVal.Text);
                                            }
                                        }
                                        else if (strAdminKeys[i].ToString() == "Logout")
                                        {
                                            string strLog = txtLogoutVal.Text.Substring(0, 2);
                                            if (strLog == "~/")
                                            {
                                                configuration.AppSettings.Settings.Add(strAdminKeys[i].ToString(), txtLogoutVal.Text);
                                            }
                                            else
                                            {
                                                configuration.AppSettings.Settings.Add(strAdminKeys[i].ToString(), "~/" + txtLogoutVal.Text);
                                            }
                                        }
                                        else if (strAdminKeys[i].ToString() == "HierMenuName")
                                        {
                                            configuration.AppSettings.Settings.Add(strAdminKeys[i].ToString(), txtMenuName.Text);
                                            if (txtMenuName.Text != "")
                                            {
                                                RenameXmlMenuRoot(txtMenuName.Text);
                                            }
                                        }
                                        else if (strAdminKeys[i].ToString() == "SesRediPage")
                                        {
                                            string strSessionPage = txtSessionRedirect.Text.Substring(0, 2);
                                            if (strSessionPage == "~/")
                                            {
                                                configuration.AppSettings.Settings.Add(strAdminKeys[i].ToString(), txtSessionRedirect.Text);
                                            }
                                            else
                                            {
                                                configuration.AppSettings.Settings.Add(strAdminKeys[i].ToString(), "~/" + txtSessionRedirect.Text);
                                            }
                                        }
                                    }
                                    configuration.Save(ConfigurationSaveMode.Full);
                                }
                                else
                                {
                                    if (strAdminKeys[i].ToString() == "ProviderName")
                                    {
                                        configuration.AppSettings.Settings["ProviderName"].Value = "System.Data.SqlClient";
                                    }
                                    else
                                    {
                                        if (strAdminKeys[i].ToString() == "SetFlag")
                                        {
                                            configuration.AppSettings.Settings["SetFlag"].Value = strSetFlag;
                                        }
                                        else if (strAdminKeys[i].ToString() == "XmlDel")
                                        {
                                            configuration.AppSettings.Settings["XmlDel"].Value = strXmlDel;
                                        }
                                        else if (strAdminKeys[i].ToString() == "Home")
                                        {
                                            string strHome = txtHomeVal.Text.Substring(0, 2);
                                            if (strHome == "~/")
                                            {
                                                configuration.AppSettings.Settings["Home"].Value = txtHomeVal.Text;
                                            }
                                            else
                                            {
                                                configuration.AppSettings.Settings["Home"].Value = "~/" + txtHomeVal.Text;
                                            }
                                        }
                                        else if (strAdminKeys[i].ToString() == "Logout")
                                        {
                                            string strLog = txtLogoutVal.Text.Substring(0, 2);
                                            if (strLog == "~/")
                                            {
                                                configuration.AppSettings.Settings["Logout"].Value = txtLogoutVal.Text;
                                            }
                                            else
                                            {
                                                configuration.AppSettings.Settings["Logout"].Value = "~/" + txtLogoutVal.Text;
                                            }
                                        }
                                        else if (strAdminKeys[i].ToString() == "HierMenuName")
                                        {
                                            configuration.AppSettings.Settings["HierMenuName"].Value = txtMenuName.Text;
                                            if (txtMenuName.Text != "")
                                            {
                                                RenameXmlMenuRoot(txtMenuName.Text);
                                            }
                                        }
                                        else if (strAdminKeys[i].ToString() == "SesRediPage")
                                        {
                                            string strSessionPage = txtSessionRedirect.Text.Substring(0, 2);
                                            if (strSessionPage == "~/")
                                            {
                                                configuration.AppSettings.Settings["SesRediPage"].Value = txtSessionRedirect.Text;
                                            }
                                            else
                                            {
                                                configuration.AppSettings.Settings["SesRediPage"].Value = "~/" + txtSessionRedirect.Text;
                                            }
                                        }
                                    }
                                    configuration.Save(ConfigurationSaveMode.Full);
                                }
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Your Web.config file does not contain aapSettings section.Please add it');", true);
                        }
                        if (!Directory.Exists(Server.MapPath("~/UserXML")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/UserXML"));
                        }
                    }
                    if (chkDbConfig.Checked == true)
                    {
                        //CretaeDBObjects();
                        if (ConfigurationManager.AppSettings["UStatus"].ToString() == "New" && rbtnNewUser.Checked == true)
                        {
                            configuration.AppSettings.Settings["UStatus"].Value = "Existing";
                        }
                        configuration.Save(ConfigurationSaveMode.Full);
                    }
                    if (chkAddConfig.Checked == true)
                    {
                        ConfigAddUser();
                    }
                    if (chkModuleConfig.Checked == true)
                    {
                        SendTreeView_CreateXml();
                    }
                    configuration.AppSettings.Settings["Config"].Value = "N";
                    configuration.Save(ConfigurationSaveMode.Full);
                    lnkGoLogin.Visible = true;

                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Configuration Done Successfully.');document.location.href='../../Default.aspx'", true);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + ex.Message + "');", true);
            }
            finally { }
        }

        #region User Defined Methods
        private bool ValidateControls()
        {
            bool resultVal = true;
            try
            {
                if (txtHomeVal.Text == "" || txtHomeVal.Text == "e.g. ~/Dashboard/Default.aspx")
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Please Type Home Key Value !');", true);
                    resultVal = false;
                }
                else if (txtLogoutVal.Text == "" || txtLogoutVal.Text == "e.g. ~/Default.aspx")
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Please Type Logout Key Value !');", true);
                    resultVal = false;
                }
                else if (txtMenuName.Text == "" || txtMenuName.Text == "e.g. Kwantify")
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Please Type Hierarchy Menu Name !');", true);
                    resultVal = false;
                }
                else if (txtSessionRedirect.Text == "" || txtMenuName.Text == "e.g. ~/SessionRedirect.aspx")
                {
                    ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('Please Enter Page Name To Redirect While Session Lost !');", true);
                    resultVal = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + ex.Message + "');", true);
            }
            finally { }
            return resultVal;
        }

        private void RenameXmlMenuRoot(string strRootNm)
        {
            try
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
                XmlNode xmlnode = xmlDoc.DocumentElement.SelectNodes("/Menu")[0].ChildNodes[0];
                XmlNode refXmlNode = xmlDoc.DocumentElement.SelectNodes("/Menu")[0].ChildNodes[1];
                RenameDeletedNode(xmlDoc, xmlnode, refXmlNode, strRootNm);
                xmlDoc.Save(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));

                //Update ManageHierarchy DESC attribute value
                if (txtRootSummary.Text != "")
                {
                    XmlAttribute rootDesc = (XmlAttribute)xmlDoc.SelectSingleNode("//Menu/" + ConfigurationManager.AppSettings["HierMenuName"].ToString() + "/ManageHierarchy/@DESC");
                    if (rootDesc != null)
                    {
                        rootDesc.Value = txtRootSummary.Text; // Set to new value.
                    }
                }
                xmlDoc.Save(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + ex.Message + "');", true);
            }
        }

        private void RenameDeletedNode(XmlDocument xmldoc, XmlNode xmlnode, XmlNode refXmlNode, string strRootNm)
        {
            try
            {
                XmlNode newNode = xmldoc.CreateElement(strRootNm.Trim().Replace(" ", ""));
                XmlAttribute xAttr = xmldoc.CreateAttribute("NAME");
                xAttr.Value = strRootNm.Trim();
                newNode.Attributes.Append(xAttr);
                xAttr = null;
                xAttr = xmldoc.CreateAttribute("ID");
                xAttr.Value = xmlnode.Attributes["ID"].Value;
                newNode.Attributes.Append(xAttr);
                xAttr = null;
                xAttr = xmldoc.CreateAttribute("FLAG");
                xAttr.Value = xmlnode.Attributes["FLAG"].Value;
                newNode.Attributes.Append(xAttr);

                newNode.InnerXml = xmlnode.InnerXml;
                xmlnode.ParentNode.InsertBefore(newNode, refXmlNode);
                xmlnode.ParentNode.RemoveChild(xmlnode);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + ex.Message + "');", true);
            }
        }

       

        private void ConfigAddUser()
        {

            if (!Directory.Exists(Server.MapPath("~/Console/ConfigXml")))
            {
                Directory.CreateDirectory(Server.MapPath("~/Console/ConfigXml"));
            }
            if (!File.Exists(Server.MapPath("~/Console/ConfigXml/AddUserConfig.xml")))
            {
                File.Delete(Server.MapPath("~/Console/ConfigXml/AddUserConfig.xml"));
            }
            XmlElement xChild1;
            XmlDocument objXdoc = new XmlDocument();
            XmlDeclaration declaration = objXdoc.CreateXmlDeclaration("1.0", "utf-8", "yes");
            objXdoc.AppendChild(declaration);
            XmlElement xRoot = objXdoc.CreateElement("PageConfig");
            objXdoc.AppendChild(xRoot);
            xChild1 = objXdoc.CreateElement("DomainUser");
            xChild1.SetAttribute("Name", "DomainUser");
            xChild1.SetAttribute("Value", chkDomainUser.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("OfficeType");
            xChild1.SetAttribute("Name", "OfficeType");
            xChild1.SetAttribute("Value", chkOfficeType.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("ProbComp");
            xChild1.SetAttribute("Name", "ProbComp");
            xChild1.SetAttribute("Value", chkProbComp.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("Grade");
            xChild1.SetAttribute("Name", "Grade");
            xChild1.SetAttribute("Value", chkGrade.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("SuperPrevil");
            xChild1.SetAttribute("Name", "SuperPrevil");
            xChild1.SetAttribute("Value", chkSuperPrevil.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("Attendance");
            xChild1.SetAttribute("Name", "Attendance");
            xChild1.SetAttribute("Value", chkAttendance.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("Payroll");
            xChild1.SetAttribute("Name", "Payroll");
            xChild1.SetAttribute("Value", chkPayroll.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("EPF");
            xChild1.SetAttribute("Name", "EPF");
            xChild1.SetAttribute("Value", chkEpf.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("Telephone");
            xChild1.SetAttribute("Name", "Telephone");
            xChild1.SetAttribute("Value", chkTelephone.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("Mobile");
            xChild1.SetAttribute("Name", "Mobile");
            xChild1.SetAttribute("Value", chkMobile.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("PAddr");
            xChild1.SetAttribute("Name", "PAddr");
            xChild1.SetAttribute("Value", chkPAddr.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            xChild1 = objXdoc.CreateElement("RA");
            xChild1.SetAttribute("Name", "RA");
            xChild1.SetAttribute("Value", chkRA.Checked.ToString());
            objXdoc.DocumentElement.AppendChild(xChild1);
            xChild1 = null;
            objXdoc.Save(Server.MapPath("~/Console/ConfigXml/AddUserConfig.xml"));




        }

        private void AssignCheckBoxes()
        {
            if (File.Exists(Server.MapPath("~/Console/ConfigXml/AddUserConfig.xml")))
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.Load(Server.MapPath("~/Console/ConfigXml/AddUserConfig.xml"));
                XmlNode xNode = xmldoc.DocumentElement.SelectSingleNode("/PageConfig");
                XmlNodeList xNodelist;
                if (xNode.HasChildNodes)
                {
                    xNodelist = xNode.ChildNodes;
                    for (int i = 0; i < xNodelist.Count; i++)
                    {
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "DomainUser" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkDomainUser.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "OfficeType" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkOfficeType.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "ProbComp" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkProbComp.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "Grade" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkGrade.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "SuperPrevil" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkSuperPrevil.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "Attendance" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkAttendance.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "Payroll" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkPayroll.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "EPF" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkEpf.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "Telephone" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkTelephone.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "Mobile" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkMobile.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "PAddr" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkPAddr.Checked = true;
                        }
                        if (xNode.ChildNodes[i].Attributes["Name"].Value == "RA" && xNode.ChildNodes[i].Attributes["Value"].Value == "True")
                        {
                            chkRA.Checked = true;
                        }
                    }
                }
            }

        }

        private void UncheckAddUser()
        {
            chkDomainUser.Checked = false;
            chkOfficeType.Checked = false;
            chkProbComp.Checked = false;
            chkGrade.Checked = false;
            chkSuperPrevil.Checked = false;
            chkAttendance.Checked = false;
            chkPayroll.Checked = false;
            chkEpf.Checked = false;
            chkTelephone.Checked = false;
            chkMobile.Checked = false;
            chkPAddr.Checked = false;
            chkRA.Checked = false;
        }

        private void BindModuleConfigTree()
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlNodeList nodelList;
            xmldoc.Load(Server.MapPath("~/Console/ConfigXml/AConsoleModules.xml"));
            XmlNode rootNode = xmldoc.DocumentElement.SelectSingleNode("/ACModules");
            if (rootNode.HasChildNodes)
            {
                nodelList = rootNode.ChildNodes;
                foreach (XmlNode xNode in nodelList)
                {
                    if (xNode.Attributes["NAME"].Value == "Manage Links")
                    {
                        BindModuleTree(xNode, tvML);
                    }
                    if (xNode.Attributes["NAME"].Value == "Manage User")
                    {
                        BindModuleTree(xNode, tvMU);
                    }
                  
                    if (xNode.Attributes["NAME"].Value == "Office Timings")
                    {
                        BindModuleTree(xNode, tvOT);
                    }

                    if (xNode.Attributes["NAME"].Value == "Manage LogIn")
                    {
                        BindModuleTree(xNode, tvPLS);
                    }
                   

                }
            }

        }
        private void BindModuleTree(XmlNode xNode, TreeView objTree)
        {
            TreeNode parentNode = new TreeNode(xNode.Attributes["NAME"].Value);
            if (xNode.HasChildNodes)
            {
                for (int i = 0; i < xNode.ChildNodes.Count; i++)
                {
                    TreeNode childNode = new TreeNode(xNode.ChildNodes[i].Attributes["NAME"].Value);
                    parentNode.ChildNodes.Add(childNode);
                }
            }
            objTree.Nodes.Add(parentNode);
            //Tick treeview checkbox if data exist in KwantifyMenu.xml
            XDocument xDoc = XDocument.Load(Server.MapPath("~/Console/Menu/KwantifyMenu.xml"));
            var root = xDoc.Descendants("Kwantify");

            if (root.Elements().Count() > 0)
            {
                if (root.Elements(xNode.Attributes["NAME"].Value.Replace(" ", "").Trim()).Any())
                {
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.Load(Server.MapPath("~/Console/Menu/KwantifyMenu.xml"));
                    XmlNode mainNode = xmldoc.DocumentElement.SelectSingleNode("/Menu/Kwantify/" + xNode.Attributes["NAME"].Value.Replace(" ", "").Trim());
                    XmlNodeList NodeList = mainNode.ChildNodes;
                    string[] strChildArr = new string[NodeList.Count];
                    int i = 0;
                    foreach (XmlNode Node in NodeList)
                    {
                        XmlElement Elem = (XmlElement)Node;
                        strChildArr[i] = Elem.GetAttribute("NAME");
                        i++;
                    }
                    foreach (TreeNode tNode in objTree.Nodes[0].ChildNodes)
                    {
                        if (strChildArr.Contains(tNode.Text))
                        {
                            tNode.Checked = true;
                            if (objTree.Nodes[0].Checked == false)
                            {
                                objTree.Nodes[0].Checked = true;
                            }
                        }
                    }
                }
            }
        }
        private void SendTreeView_CreateXml()
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(Server.MapPath("~/Console/Menu/KwantifyMenu.xml"));
            XmlNode rootNode = xmldoc.DocumentElement.SelectNodes("/Menu")[0].ChildNodes[0];
            //linq codes
            XDocument xDoc = XDocument.Load(Server.MapPath("~/Console/Menu/KwantifyMenu.xml"));
            CreateLatestXmlMenu(xmldoc, rootNode, xDoc, tvML, tvMU, tvOT, tvRF, tvReports, tvPIP, tvPLS);
        }
        private void CreateLatestXmlMenu(XmlDocument xmldoc, XmlNode rootNode, XDocument xDoc, params TreeView[] objTvArr)
        {

            var root = xDoc.Descendants("Kwantify");
            //treeview check
            int NodeID = 2;
            foreach (TreeView objTv in objTvArr)
            {//
                if(objTv.Nodes.Count>0)
                {
                    if (objTv.Nodes[0].Checked == true)
                    {
                        if (root.Elements(objTv.Nodes[0].Text.Replace(" ", "").Trim()).Any())
                        {
                            TreeNodeCollection tNodes = objTv.Nodes[0].ChildNodes;
                            int i = 0;
                            string strPrevNode = null;
                            foreach (TreeNode node in tNodes)
                            {
                                XDocument xDoc2 = XDocument.Load(Server.MapPath("~/Console/Menu/KwantifyMenu.xml"));
                                var root2 = xDoc2.Descendants("Kwantify");
                                if (i != 0)
                                {
                                    string strNode = objTv.Nodes[0].ChildNodes[i - 1].Text.Replace(" ", "").Trim();
                                    if (root2.Elements(objTv.Nodes[0].Text.Replace(" ", "").Trim()).Elements(strNode).Any())
                                    {
                                        strPrevNode = strNode;
                                    }
                                }

                                if (node.Checked == true)
                                {
                                    if (!root2.Elements(objTv.Nodes[0].Text.Replace(" ", "").Trim()).Elements(node.Text.Replace(" ", "").Trim()).Any())
                                    {
                                        if (strPrevNode != null)
                                        {
                                            XmlNode refXmlNode = xmldoc.DocumentElement.SelectSingleNode("/Menu/Kwantify/" + objTv.Nodes[0].Text.Replace(" ", "").Trim() + "/" + strPrevNode);
                                            CreateXmlNode(xmldoc, refXmlNode, refXmlNode, node.Text, i + 1, objTv, false, "After");
                                        }
                                        else
                                        {
                                            XmlNode refXmlNode = xmldoc.DocumentElement.SelectNodes("/Menu/Kwantify/" + objTv.Nodes[0].Text.Replace(" ", "").Trim())[0].ChildNodes[0];
                                            CreateXmlNode(xmldoc, refXmlNode, refXmlNode, node.Text, i + 1, objTv, false, "Before");
                                        }
                                    }
                                }
                                else
                                {
                                    if (root2.Elements(objTv.Nodes[0].Text.Replace(" ", "").Trim()).Elements(node.Text.Replace(" ", "").Trim()).Any())
                                    {
                                        root2.Elements(objTv.Nodes[0].Text.Replace(" ", "").Trim()).Elements(node.Text.Replace(" ", "").Trim()).Remove();
                                        xDoc2.Save(Server.MapPath("~/Console/Menu/KwantifyMenu.xml"));
                                    }
                                }
                                i++;
                            }
                        }
                        else
                        {
                            XmlNode refXmlNode = xmldoc.DocumentElement.SelectNodes("/Menu/Kwantify")[0].ChildNodes[xmldoc.DocumentElement.SelectNodes("/Menu/Kwantify")[0].ChildNodes.Count - 1];
                            CreateXmlNode(xmldoc, refXmlNode, refXmlNode, objTv.Nodes[0].Text, 2, objTv, true, "After");
                        }
                    }
                    //
                    else
                    {
                        if (root.Elements(objTv.Nodes[0].Text.Replace(" ", "").Trim()).Any())
                        {
                            root.Elements(objTv.Nodes[0].Text.Replace(" ", "").Trim()).Remove();
                            xDoc.Save(Server.MapPath("~/Console/Menu/KwantifyMenu.xml"));
                        }
                    }
                    xDoc = XDocument.Load(Server.MapPath("~/Console/Menu/KwantifyMenu.xml"));
                    root = xDoc.Descendants("Kwantify");
                    if (root.Elements(objTv.Nodes[0].Text.Replace(" ", "").Trim()).Any())
                    {
                        xmldoc.Load(Server.MapPath("~/Console/Menu/KwantifyMenu.xml"));
                        XmlNode mainNode = xmldoc.DocumentElement.SelectSingleNode("/Menu/Kwantify/" + objTv.Nodes[0].Text.Replace(" ", "").Trim());
                        XmlNodeList NodeList = mainNode.ChildNodes;
                     
                        int intNodeCount = 0;
                        foreach (XmlNode Node in NodeList)
                        {
                            intNodeCount++;
                            XmlElement Elem = (XmlElement)Node;
                            Elem.SetAttribute("ID", intNodeCount.ToString());
                        }
                        XmlElement mainElem = (XmlElement)mainNode;
                        mainElem.SetAttribute("TOT", intNodeCount.ToString());
                        mainElem.SetAttribute("ID", NodeID.ToString());
                        xmldoc.Save(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));
                        NodeID++;
                    }
                    //
                }
               
            }

        }
        private void CreateXmlNode(XmlDocument xmldoc, XmlNode xmlnode, XmlNode refXmlNode, string strNodeName, int intId, TreeView tvObj, bool hasChild, string strInsStat)
        {
            try
            {
                XmlNode newNode = xmldoc.CreateElement(strNodeName.Replace(" ", "").Trim());
                if (hasChild == true)
                {
                    newNode = MakeChildNodes(tvObj, xmldoc, newNode);
                }
                XmlAttribute xAttr = xmldoc.CreateAttribute("ID");
                xAttr.Value = intId.ToString();
                newNode.Attributes.Append(xAttr);
                xAttr = null;
                xAttr = xmldoc.CreateAttribute("TOT");
                xAttr.Value = newNode.ChildNodes.Count.ToString();
                newNode.Attributes.Append(xAttr);
                xAttr = null;
                xAttr = xmldoc.CreateAttribute("NAME");
                xAttr.Value = strNodeName;
                newNode.Attributes.Append(xAttr);
                xAttr = null;
                xAttr = xmldoc.CreateAttribute("DESC");
                xAttr.Value = strNodeName;
                newNode.Attributes.Append(xAttr);
                xAttr = null;
                xAttr = xmldoc.CreateAttribute("FLAG");
                xAttr.Value = "0";
                newNode.Attributes.Append(xAttr);
                if (strInsStat == "Before")
                {
                    xmlnode.ParentNode.InsertBefore(newNode, refXmlNode);
                }
                else
                {
                    xmlnode.ParentNode.InsertAfter(newNode, refXmlNode);
                }
                xmldoc.Save(Server.MapPath("../Console/Menu/KwantifyMenu.xml"));

            }
            catch (Exception ex)
            {

            }

        }

        private XmlNode MakeChildNodes(TreeView tvObj, XmlDocument xmldoc, XmlNode pNode)
        {
            TreeNodeCollection tNodes = tvObj.Nodes[0].ChildNodes;
            int intCount = 1;
            foreach (TreeNode node in tNodes)
            {
                if (node.Checked == true)
                {
                    XmlNode childNode = xmldoc.CreateElement(node.Text.Replace(" ", "").Trim());
                    XmlAttribute xAttr = xmldoc.CreateAttribute("ID");
                    xAttr.Value = intCount.ToString();
                    childNode.Attributes.Append(xAttr);
                    //Append Attribute TOT
                    xAttr = null;
                    xAttr = xmldoc.CreateAttribute("TOT");
                    xAttr.Value = "0";
                    childNode.Attributes.Append(xAttr);
                    //Append Attribute LID
                    xAttr = null;
                    xAttr = xmldoc.CreateAttribute("NAME");
                    xAttr.Value = node.Text;
                    childNode.Attributes.Append(xAttr);
                    //Append Attribute PID
                    xAttr = null;
                    xAttr = xmldoc.CreateAttribute("DESC");
                    xAttr.Value = node.Text;
                    childNode.Attributes.Append(xAttr);
                    //Append Attribute DESC
                    xAttr = null;
                    xAttr = xmldoc.CreateAttribute("FLAG");
                    xAttr.Value = "0";
                    childNode.Attributes.Append(xAttr);
                    pNode.AppendChild(childNode);
                    intCount++;
                }
            }
            return pNode;
        }


        #endregion

        protected void lnkGoLogin_Click(object sender, EventArgs e)
        {
            try
            {
                configuration.AppSettings.Settings["Config"].Value = "N";
                configuration.Save(ConfigurationSaveMode.Full);
                Response.Redirect(ConfigurationManager.AppSettings["Logout"].ToString());
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + ex.Message + "');", true);
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                UncheckAddUser();
                BindInitialConfig();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this.UpdatePanel1, typeof(string), "", "alert('" + ex.Message + "');", true);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            ConfigAddUser();
            Response.Redirect(ConfigurationManager.AppSettings["Logout"].ToString());
        }


    }
}
