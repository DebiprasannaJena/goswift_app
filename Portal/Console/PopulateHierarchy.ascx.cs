using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
//using CSMPDK_3_0;
using System.Xml;
using Manage_Usercontrol_Property;
using AdminApp.Model;
using AdminApp.Persistence;
using AdminApp.Business;
using System.Collections.Generic;
public partial class Admin_PopulateHierarchy : System.Web.UI.UserControl
{
    private DataSet _objDS;
    public string PageURL;
    string strxmlFilePath = null;
    string PID;
    static int CountVal = 0;
    //CommonDLL obj = new CommonDLL();
    XmlDocument objDoc = new XmlDocument();
    int pos;
    int intLid;
    int HiracrcyId;
    string strData = null;
    AdminAppService objAdmin = new AdminAppService();
    PopHierarchy objPop = new PopHierarchy();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        pos = PopulateHierarchyProperty.PositionId;
        intLid = PopulateHierarchyProperty.LocationId;
        HiracrcyId = PopulateHierarchyProperty.HierachyId;
        PageURL = PopulateHierarchyProperty.PageUrl;
        objPop.LocationId = Convert.ToInt32(intLid.ToString());
        pos = objAdmin.GetPOSId(objPop);
        if (!IsPostBack)
        {
            CountVal = 0;
            //  strxmlFilePath = Server.MapPath("Upload file//" + "Kwantify.xml");
            FillLocation(strxmlFilePath,  pos, intLid, HiracrcyId);

            FillAllControl();
            string a = hidID1.Value;
            string b = hidID2.Value;
            ShowPosition(pos);
            if (HiracrcyId != 0)
            {
                if (CountVal == 0)
                {
                    if (drpLocation.SelectedValue.ToString() != "--Select--")
                    {  //Fill the Second  hirarchy if location is selected -Ex Division
                        FillFirstHirarchy(int.Parse(drpLocation.SelectedValue.ToString()), HiracrcyId, intLid);
                    }
                  
                }
                CountVal = CountVal + 1;
            }

        }
        if (hidID1.Value == "")
        {
            if (CountVal > 0)
            {
                if (Request.Params["CID"] != null)
                {
                    FillHirarchy(Convert.ToInt32(Request.Params["CID"].ToString()), HiracrcyId);
                }
            }
        }
        else if (hidID1.Value != "")
        {
            FillHirarchy(Convert.ToInt32(hidID1.Value), HiracrcyId);
        }
        if (PopulateHierarchyProperty.Requests != "")
        {
            // drplayer1.Items.FindByValue(PopulateHierarchyProperty.Requests).Selected = true;
        }
    }

    //Fill the CASCADEING DROPDOWN 
    private void FillAllControl()
    {

        if (HiracrcyId == 0)
        {
            //For User only  
            drpLocation.Attributes.Add("onchange", "ClearDropdown(this);GetDataL(this,'_drplayer1','_hidID0','_Label2','" + PageURL + "?CID=');");
            drplayer1.Attributes.Add("onchange", "ClearDropdown(this);GetDataL(this,'_drplayer2','_hidID1','_Label3','" + PageURL + "?CID=');");
            drplayer2.Attributes.Add("onchange", "ClearDropdown(this);GetDataL(this,'_drplayer3','_hidID2','_Label4','" + PageURL + "?CID=');");
            drplayer3.Attributes.Add("onchange", "ClearDropdown(this);GetDataL(this,'_drplayer4','_hidID3','_Label5','" + PageURL + "?CID=');");
            drplayer4.Attributes.Add("onchange", "ClearDropdown(this);GetDataL(this,'_drplayer5','_hidID4','_Label6','" + PageURL + "?CID=');");
            drplayer5.Attributes.Add("onchange", "ClearDropdown(this);GetDataL(this,'_drplayer6','_hidID5','_Label7','" + PageURL + "?CID=');");
            drplayer6.Attributes.Add("onchange", "ClearDropdown(this);GetDataL(this,'_drplayer7','_hidID6','_Label8','" + PageURL + "?CID=');");
            drplayer7.Attributes.Add("onchange", "ClearDropdown(this);GetDataL(this,'_drplayer8','_hidID7','_Label9','" + PageURL + "?CID=');");
            drplayer8.Attributes.Add("onchange", "ClearDropdown(this);GetDataL(this,'_drplayer9','_hidID8','_Label10','" + PageURL + "?CID=');");
        }
        else
        {
            //For User only
            drplayer1.Attributes.Add("onchange", "GetDataL(this,'_drplayer2','_hidID1','_Label3','" + PageURL + "?CID=');");
            drplayer2.Attributes.Add("onchange", "GetDataL(this,'_drplayer3','_hidID2','_Label4','" + PageURL + "?CID=');");
            drplayer3.Attributes.Add("onchange", "GetDataL(this,'_drplayer4','_hidID3','_Label5','" + PageURL + "?CID=');");
            drplayer4.Attributes.Add("onchange", "GetDataL(this,'_drplayer5','_hidID4','_Label6','" + PageURL + "?CID=');");
            drplayer5.Attributes.Add("onchange", "GetDataL(this,'_drplayer6','_hidID5','_Label7','" + PageURL + "?CID=');");
            drplayer6.Attributes.Add("onchange", "GetDataL(this,'_drplayer7','_hidID6','_Label8','" + PageURL + "?CID=');");
            drplayer7.Attributes.Add("onchange", "GetDataL(this,'_drplayer8','_hidID7','_Label9','" + PageURL + "?CID=');");
            drplayer8.Attributes.Add("onchange", "GetDataL(this,'_drplayer9','_hidID8','_Label10','" + PageURL + "?CID=');");
        }
    }

    /// <summary>
    ///  it will  display layer according to  layer.
    /// </summary>
    /// <param name="pos"></param>
    private void ShowPosition(int pos)
    {
        switch (pos)
        {
            case 1:
                tr1.Style.Add("display", "none");
                tr2.Style.Add("display", "none");
                tr3.Style.Add("display", "none");
                tr4.Style.Add("display", "none");
                tr5.Style.Add("display", "none");
                tr6.Style.Add("display", "none");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 2:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "none");
                tr3.Style.Add("display", "none");
                tr4.Style.Add("display", "none");
                tr5.Style.Add("display", "none");
                tr6.Style.Add("display", "none");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 3:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "none");
                tr4.Style.Add("display", "none");
                tr5.Style.Add("display", "none");
                tr6.Style.Add("display", "none");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 4:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "none");
                tr5.Style.Add("display", "none");
                tr6.Style.Add("display", "none");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 5:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "none");
                tr6.Style.Add("display", "none");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 6:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "block");
                tr6.Style.Add("display", "none");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 7:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "block");
                tr6.Style.Add("display", "block");
                tr7.Style.Add("display", "none");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 8:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "block");
                tr6.Style.Add("display", "block");
                tr7.Style.Add("display", "block");
                tr8.Style.Add("display", "none");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 9:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "block");
                tr6.Style.Add("display", "block");
                tr7.Style.Add("display", "block");
                tr8.Style.Add("display", "block");
                tr9.Style.Add("display", "none");
                tr10.Style.Add("display", "none");
                break;
            case 10:
                tr1.Style.Add("display", "block");
                tr2.Style.Add("display", "block");
                tr3.Style.Add("display", "block");
                tr4.Style.Add("display", "block");
                tr5.Style.Add("display", "block");
                tr6.Style.Add("display", "block");
                tr7.Style.Add("display", "block");
                tr8.Style.Add("display", "block");
                tr9.Style.Add("display", "block");
                tr10.Style.Add("display", "none");
                break;
        }
    }
    /// <summary>
    /// Fill the Location
    /// </summary>
    /// <param name="strxmlFilePath"></param>
    /// <param name="Constr"></param>
    /// <param name="pos"></param>
    /// <param name="intLid"></param>
    /// <param name="intHiracrcyId"></param>
    /// <param name="HiracrcyId"></param>
    /// 
    public DataSet ReadXmlToDataSet(string strXmlFilePath)
    {
        DataSet set;
        this._objDS = new DataSet();
        try
        {
            this._objDS.ReadXml(strXmlFilePath);
            set = this._objDS;
        }
        catch (Exception exception)
        {
            throw new ApplicationException("Data Error" + exception.Message);
        }
        return set;
    }
    private void FillLocation(string strxmlFilePath, int pos, int intLid, int intHiracrcyId)
    {

        string strLid = intLid.ToString();
        if (strxmlFilePath != null)
        {
            objDoc.Load(strxmlFilePath);
            DataSet ds = ReadXmlToDataSet(strxmlFilePath);
            string s1 = ds.Tables[0].ToString();
            string s2 = ds.Tables[1].ToString();

            XmlNodeList lstNode = objDoc.GetElementsByTagName(s2);
            string strID = null;
            string strName = null;
            foreach (XmlNode node in lstNode)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    strID = node.ChildNodes[i].Attributes["ID"].Value;
                    strName = node.ChildNodes[i].Attributes["NAME"].Value;
                    PID = node.ChildNodes[i].Attributes["PID"].Value;
                    drpLocation.Items.Add(new ListItem(strName, strID));
                    Label1.Text = strName;
                }
            }
        }
        else
        {
            try
            {
                objPop.HierachyId = HiracrcyId;
                IList<PopHierarchy> objlstplink = objAdmin.FillLocation(objPop);
                drpLocation.DataValueField = "LevelID";
                drpLocation.DataTextField = "LevelName";
                drpLocation.DataSource = objlstplink;
                drpLocation.DataBind();
                //drpLocation.Items.Insert(0, "--Select--");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            if (HiracrcyId != 0)
            {
                drpLocation.SelectedValue = HiracrcyId.ToString();
            }
            FillLabel(pos, HiracrcyId);
        }

    }
    /// <summary>
    /// Dispaly the All lavel name
    /// </summary>
    /// <param name="Constr"></param>
    /// <param name="pos"></param>
    /// <param name="HiracrcyId"></param>

    private void FillLabel(int pos, int HiracrcyId)
    {
       IList<PopHierarchy> objFnctList = new List<PopHierarchy>();
       objPop.LocationId = PopulateHierarchyProperty.LocationId;
        objFnctList = objAdmin.FillLabel(objPop);
        int cnts = 0;
        foreach (var i in objFnctList)
        {
            cnts = cnts + 1;

            if (cnts == 1)
            {
                Label1.Text = i.LevelName;
            }
            if (cnts == 2)
            {
                Label2.Text = i.LevelName;
            }
            if (cnts == 3)
            {
                Label3.Text = i.LevelName;
            }
            if (cnts == 4)
            {
                Label4.Text = i.LevelName;
            }
            if (cnts == 5)
            {
                Label5.Text = i.LevelName;
            }
            if (cnts == 6)
            {
                Label6.Text = i.LevelName;
            }
            if (cnts == 7)
            {
                Label7.Text = i.LevelName;
            }
            if (cnts == 8)
            {
                Label8.Text = i.LevelName;
            }
            if (cnts == 9)
            {
                Label9.Text = i.LevelName;
            }
            if (cnts == 10)
            {
                Label10.Text = i.LevelName;
            }
        }
        
        
    }
    /// <summary>
    /// If it has been choose specific locaton and the Location is display in diasble mode  then the First  layer  will populate indepently and other layer depend on it
    /// </summary>
    /// <param name="intCID">it is the int_PldId</param>
    /// <param name="HiracrcyId"> it is hirarchy id</param>
    /// <param name="HiracrcyId"> it is location id</param>
    private void FillFirstHirarchy(int intCID, int HiracrcyId,int LID)
    {
        strData = "0|--Select--";
        if (intCID > 0)
        {
            IList<PopHierarchy> objlstplink = objAdmin.FillFirstHierchy1(intCID,HiracrcyId,LID);
            drplayer1.DataValueField = "LevelID";
            drplayer1.DataTextField = "LevelName";
            drplayer1.DataSource = objlstplink;
            drplayer1.DataBind();
            drplayer1.Items.Insert(0, "--Select--");
            drplayer2.Items.Add(new ListItem("-Select-", "0"));

            drplayer3.Items.Add(new ListItem("-Select-", "0"));
            drplayer4.Items.Add(new ListItem("-Select-", "0"));

            drplayer5.Items.Add(new ListItem("-Select-", "0"));

            drplayer6.Items.Add(new ListItem("-Select-", "0"));

            drplayer7.Items.Add(new ListItem("-Select-", "0"));

            drplayer8.Items.Add(new ListItem("-Select-", "0"));

            drplayer9.Items.Add(new ListItem("-Select-", "0"));
            //Code Added By DIlip Kumar Tripathy on dated 6-Mar-2012
            //Purpose : To assign the immediate parent of a leveldetail data to the associated dropdownlist
            if (Session["dtAllPrents"] != null)
            {
                DataTable dtAllParents = (DataTable)Session["dtAllPrents"];
                if (dtAllParents.Rows.Count != 0)
                {
                    for (int i = 0; i < dtAllParents.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            drplayer1.SelectedValue = dtAllParents.Rows[i][0].ToString();
                        }
                        else
                        {
                            
                            DropDownList ddlLayer = new DropDownList();
                            string strNum = (i + 1).ToString();
                            ddlLayer = (DropDownList)this.FindControl("drplayer" + strNum);
                            ddlLayer.Items.Clear();
                            IList<PopHierarchy> objlstplink2 = objAdmin.FillFirstHierchy2(Convert.ToInt32(dtAllParents.Rows[i - 1][0].ToString()));
                            ddlLayer.DataValueField = "LevelID";
                            ddlLayer.DataTextField = "LevelName";
                            ddlLayer.DataSource = objlstplink2;
                            ddlLayer.DataBind();
                            ddlLayer.Items.Insert(0, "--Select--");
                            ddlLayer.SelectedValue = dtAllParents.Rows[i][0].ToString();
                        }
                    }
                }
            }
            Session["dtAllPrents"] = null;


        }
    }
    /// <summary>
    /// If it has been choose All locaton/Specific location  other  layer  will populate in ajax depending on the parent Dropdown

    /// </summary>
    /// <param name="intCID">it is int_PldId of table </param>
    /// <param name="intHirachyId"> it is Hirarchy id</param>
    private void FillHirarchy(int intCID, int intHirachyId)
    {
        strData = "";
        if (intCID > 0)
        {
            
            IList<PopHierarchy> objFnctList = new List<PopHierarchy>();
            objFnctList = objAdmin.FillHierchy1(intCID,intHirachyId);
            foreach (var i in objFnctList)
            {
               if (strData == "")
               {
                   strData = i.LevelID + "|" + i.LevelName;
               }
               else
               {
                   strData = strData.Trim() + "~" + i.LevelID + "|" + i.LevelName;
               }
            }

            if (hidID1.Value == "")
            {
                Response.Write(strData);
                Response.End();

            }
            else
            {
                drplayer2.Items.Clear();
                drplayer3.Items.Clear();
                drplayer4.Items.Clear();
                drplayer5.Items.Clear();
                drplayer6.Items.Clear();
                drplayer7.Items.Clear();
                drplayer8.Items.Clear();

                if (intCID > 0)
                {

                    for (int i = 1; i <= 8; i++)
                    {
                        DropDownList ddlToFill = (DropDownList)FindControl("drplayer" + (i + 1));
                        if (((HiddenField)FindControl("hidID" + i)).Value != "" && ((HiddenField)FindControl("hidID" + (i + 1))).Value != "")
                        {
                            IList<PopHierarchy> objlstplink2 = objAdmin.FillHierchy2(intHirachyId, Convert.ToInt32(((HiddenField)FindControl("hidID" + i)).Value));
                            ddlToFill.DataValueField = "LevelID";
                            ddlToFill.DataTextField = "LevelName";
                            ddlToFill.DataSource = objlstplink2;
                            ddlToFill.DataBind();
                            ddlToFill.Items.Insert(0, "--Select--");
                            string strLevelName = objAdmin.GetLevel(Convert.ToInt32(((HiddenField)FindControl("hidID" + i)).Value), Convert.ToInt32(((HiddenField)FindControl("hidID" + (i + 1))).Value));
                            if (ddlToFill.Items.Count > 0)
                            {
                                ddlToFill.Items.FindByText(strLevelName).Selected = true;
                            }
                        }
                    }
                }
            }
        }

    }
    /// <summary>
    /// To fill the USER LISTBOX OR  USER DROPDOWNLIST
    /// </summary>
    /// <param name="intDeptId"></param>
    private void FillUser(int intDeptId)
    {
        strData = "";
        IList<PopHierarchy> objFnctList = new List<PopHierarchy>();
        objFnctList = objAdmin.FillUser(intDeptId);
        foreach (var i in objFnctList)
        {
            if (strData == "")
            {
                strData =i.UserID+ "|" + i.UserName;
            }
            else
            {
                strData = strData.Trim() + "~" + i.UserID + "|" + i.UserName;
            }
        }
        Response.Write(strData);
        Response.End();
    }
}



