using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Security.Cryptography;

using EntityLayer.Service;
using System.IO;
using BusinessLogicLayer.Service;

public partial class Service_ApprovalConfig : System.Web.UI.Page
{
    #region Variables
    ServiceDetails objService1 = new ServiceDetails();
    public string strManageRight = "";
    public int intLevelDetailId;
    //string strUserId, strPassword, strRandomPassword;
    #endregion
    DataTable dtable;
    DataSet ds = new DataSet();
    ServiceBusinessLayer objService = new ServiceBusinessLayer();
    List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                BindGrid();
                BindDept();
                //BindLocation();
                //BindUser();
            }
            catch (Exception ex)
            {
                Util.LogError(ex, "Service");
            }
        }
    }
    private void BindGrid()
    {
        ds = new DataSet();
        dtable = new DataTable();
        DataRow dtRow;
        try
        {
        DataColumn formname = new DataColumn("intEscalationId", Type.GetType("System.String"));
        DataColumn Forward = new DataColumn("strUsername", Type.GetType("System.String"));
        DataColumn Timeline = new DataColumn("strExcalationDays", Type.GetType("System.String"));
        DataColumn hdnUserid = new DataColumn("userid", Type.GetType("System.String"));

        DataColumn DEPTID = new DataColumn("DEPTID", Type.GetType("System.String"));
        DataColumn LOCATIONID = new DataColumn("LOCATIONID", Type.GetType("System.String"));
        DataColumn INTDIRECTORATEID = new DataColumn("DirectId", Type.GetType("System.String"));
        DataColumn INTDIVISIONID = new DataColumn("DivisionId", Type.GetType("System.String"));
        DataColumn DISTID = new DataColumn("DISTID", Type.GetType("System.String"));
        DataColumn Typeid = new DataColumn("Typeid", Type.GetType("System.String"));
        DataColumn desigid = new DataColumn("desigid", Type.GetType("System.String"));
        dtable.Columns.Add(DEPTID);
        dtable.Columns.Add(LOCATIONID);
        dtable.Columns.Add(INTDIRECTORATEID);
        dtable.Columns.Add(INTDIVISIONID);
        dtable.Columns.Add(DISTID);
        dtable.Columns.Add(Typeid);
        dtable.Columns.Add(desigid);
        dtable.Columns.Add(formname);
        dtable.Columns.Add(Forward);
        dtable.Columns.Add(Timeline);
        dtable.Columns.Add(hdnUserid);
        dtRow = dtable.NewRow();
        //dtRow[formname] = "Level 1";
        dtRow[formname] = "1";
        dtable.Rows.Add(dtRow);
        ViewState["dtstored"] = dtable;
        gvService.DataSource = dtable;// ds.Tables[0];
        gvService.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }
    private void BindDept()
    {
        try
        {
            //ApprovalConfigService.ServiceBusinessLayerClient objService = new ApprovalConfigService.ServiceBusinessLayerClient();
            //List<ServiceDetails> objServicelist = new List<ServiceDetails>();
            objServicelist = objService.BindDepartment("DP").ToList();
            ddldept.DataSource = objServicelist;
            ddldept.DataTextField = "strdeptname";
            ddldept.DataValueField = "Deptid";
            ddldept.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddldept.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }

    }
    private void BindService()
    {
        try
        {
            //ApprovalConfigService.ServiceBusinessLayerClient objService = new ApprovalConfigService.ServiceBusinessLayerClient();
            //List<ServiceDetails> objServicelist = new List<ServiceDetails>();
            objServicelist = objService.BindService("S", int.Parse(ddldept.SelectedValue)).ToList();
            ddlService.DataSource = objServicelist;
            ddlService.DataTextField = "strServiceName";
            ddlService.DataValueField = "intServiceId";
            ddlService.DataBind();
            ListItem list = new ListItem();
            list.Text = "--Select--";
            list.Value = "0";
            ddlService.Items.Insert(0, list);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }

    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindService();
    }
    protected void ddlService_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridDetails(sender, e);
    }
    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlLocation = (e.Row.FindControl("ddlLocation") as DropDownList);
            //DropDownList ddluser = (e.Row.FindControl("ddluser") as DropDownList);
            ServiceBusinessLayer objService = new ServiceBusinessLayer();
            List<ServiceDetails> objServicelist = new List<ServiceDetails>();
            objServicelist = objService.BindLocation("L").ToList();
            ddlLocation.DataSource = objServicelist;
            ddlLocation.DataTextField = "StrLocationName";
            ddlLocation.DataValueField = "LocationId";
            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, new ListItem("--Select--", "0"));

            if (ddlLocation.SelectedValue == "0")
            {
                objServicelist = objService.BindAllUser("AL").ToList();
            }
            else if (ddlLocation.SelectedValue != "0")
            {
                objServicelist = objService.BindUser("SU", int.Parse(ddlLocation.SelectedValue)).ToList();
            }
            //DropDownList ddltype = (e.Row.FindControl("ddltype") as DropDownList);
            //HiddenField hdntype = (e.Row.FindControl("hdntype") as HiddenField);
            //ddltype.SelectedValue = hdntype.Value;
            //ddluser.DataSource = objServicelist;
            //ddluser.DataTextField = "strUsername";
            //ddluser.DataValueField = "userid";
            //ddluser.DataBind();
            //ddluser.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        int count = 0;
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        ServiceDetails objService1 = new ServiceDetails();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        DataTable dtbTemp = new DataTable();
        try
        {
        dtbTemp.Columns.Add("SERVICEID");
        dtbTemp.Columns.Add("ESCALATIONLEVELID");
        dtbTemp.Columns.Add("DEPTID");
        dtbTemp.Columns.Add("LOCATIONID");
        dtbTemp.Columns.Add("DirectId");
        dtbTemp.Columns.Add("DivisionId");
        dtbTemp.Columns.Add("DISTID");
        dtbTemp.Columns.Add("USERID");
        dtbTemp.Columns.Add("ESCALATIONDAYS");
        dtbTemp.Columns.Add("Typeid");
        dtbTemp.Columns.Add("desigid");
        foreach (GridViewRow gvr in gvService.Rows)
        {
            //objService1.strAction = "V";
            //objService1.Deptid = Convert.ToInt32(ddldept.SelectedValue);
            //objService1.intServiceId = Convert.ToInt32(ddlService.SelectedValue);
            //objServicelist = objService.ViewServiceConfigurationData(objService1).ToList();
            //int aa = objServicelist.Count;
            //if (aa > 0)
            //{
            //    gvService.DataSource = objService.ViewServiceConfigurationData(objService1).ToList();
            //    gvService.DataBind();

            //}
            Label lbllevel = (Label)gvr.FindControl("Label3");
            TextBox txtgrdTimeline = (TextBox)gvr.FindControl("txtTimeline");
            TextBox txtforwardname = (TextBox)gvr.FindControl("txtForward");
            HiddenField hdnusername = (HiddenField)gvr.FindControl("hdnforarduserid");
            DropDownList ddlLocation = (DropDownList)gvr.FindControl("ddlLocation");
           
            DropDownList ddldirectorate = (DropDownList)gvr.FindControl("ddldirectorate");
            DropDownList ddlDivision = (DropDownList)gvr.FindControl("ddlDivision");
            DropDownList ddlDistrict = (DropDownList)gvr.FindControl("ddlDistrict");

            HiddenField hdnLocation = (HiddenField)gvr.FindControl("hdnLoc");
            HiddenField hdnDept = (HiddenField)gvr.FindControl("hdnDept");
            HiddenField hdnDirectorate = (HiddenField)gvr.FindControl("hdnDirectorate");
            HiddenField hdnDiv = (HiddenField)gvr.FindControl("hdnDiv");
            HiddenField hdnDist = (HiddenField)gvr.FindControl("hdnDist");
            HiddenField hdndesig = (HiddenField)gvr.FindControl("hdndesig");
           // DropDownList ddltype = (DropDownList)gvr.FindControl("ddltype");
            DataRow dtrTemp = dtbTemp.NewRow();
            dtrTemp["SERVICEID"] = ddlService.SelectedValue;
            count = count + 1;
            dtrTemp["ESCALATIONLEVELID"] = count;
            //dtrTemp["DEPTID"] = ddlDepartment.SelectedValue;
            //dtrTemp["DEPTID"] = ddldept.SelectedValue;
            //dtrTemp["LOCATIONID"] = ddlLocation.SelectedValue;
            //dtrTemp["INTDIRECTORATEID"] = ddldirectorate.SelectedValue;
            //dtrTemp["INTDIVISIONID"] = ddlDivision.SelectedValue;
            //dtrTemp["DISTID"] = ddlDistrict.SelectedValue;
            //dtrTemp["USERID"] = hdnusername.Value;//txtforwardname.Text;

            dtrTemp["DEPTID"] = ddldept.SelectedValue;
            dtrTemp["LOCATIONID"] = hdnLocation.Value;
            dtrTemp["DirectId"] = hdnDirectorate.Value;
            dtrTemp["DivisionId"] = hdnDiv.Value;
            dtrTemp["DISTID"] = hdnDist.Value;
            dtrTemp["USERID"] = hdnDept.Value;//hdnusername.Value;

            dtrTemp["Typeid"] = ddltype.SelectedValue;
            dtrTemp["desigid"] = hdndesig.Value;
            dtrTemp["ESCALATIONDAYS"] = txtgrdTimeline.Text;
            dtbTemp.Rows.Add(dtrTemp);

        }
        ds.Tables.Add(dtbTemp);
        objService1.strAction = "I";
        objService1.Deptid = Convert.ToInt32(ddldept.SelectedValue);
        objService1.intServiceId = Convert.ToInt32(ddlService.SelectedValue);
        objService1.Typeid =ddltype.SelectedValue;
        objService1.XMLDATA = GetSTRXMLResult(dtbTemp);
        objService.ServiceConfigurationData(objService1);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "err_msg", "alert('Data Save SuccessFully');", true);

        string rawURL = Request.RawUrl;
        string strShowMsg = "Data Save SuccessFully!";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "OnClick", "<script>  jAlert('" + strShowMsg + "', '" + Messages.TitleOfProject + "', function () {location.href = '" + rawURL + "';});   </script>", false);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        //Objasset.XMLDATA = GetSTRXMLResult(dtbTemp);
        //clear();
    }

    public static string GetSTRXMLResult(DataTable dtTable)
    {
        string strXMLResult = "";
        if ((dtTable != null))
        {
            if (dtTable.Rows.Count > 0)
            {
                StringWriter sw = new StringWriter();
                dtTable.WriteXml(sw);
                strXMLResult = sw.ToString();
                sw.Close();
                sw.Dispose();
            }
        }
        return strXMLResult;
    }

    public void BindGridDetails(object sender, EventArgs e)
    {
        ServiceBusinessLayer objService = new ServiceBusinessLayer();
        List<ServiceDetails> objServicelist = new List<ServiceDetails>();
        try{
        objService1.strAction = "V";
        objService1.Deptid = Convert.ToInt32(ddldept.SelectedValue);
        objService1.intServiceId = Convert.ToInt32(ddlService.SelectedValue);
        objService1.Typeid = ddltype.SelectedValue;
        objServicelist = objService.ViewServiceConfigurationData(objService1).ToList();
        int aa = objServicelist.Count;
        if (aa > 0)
        {
            gvService.DataSource = objServicelist;//objService.ViewServiceConfigurationData(objService1).ToList();
            gvService.DataBind();

            DataTable dtCurrentTable = new DataTable();
            DataColumn formname = new DataColumn("intEscalationId", Type.GetType("System.String"));
            DataColumn Forward = new DataColumn("strUsername", Type.GetType("System.String"));
            DataColumn Timeline = new DataColumn("strExcalationDays", Type.GetType("System.String"));
            DataColumn hdnUserid = new DataColumn("userid", Type.GetType("System.String"));
            DataColumn DEPTID = new DataColumn("DEPTID", Type.GetType("System.String"));
            DataColumn LOCATIONID = new DataColumn("LOCATIONID", Type.GetType("System.String"));
            DataColumn INTDIRECTORATEID = new DataColumn("DirectId", Type.GetType("System.String"));
            DataColumn INTDIVISIONID = new DataColumn("DivisionId", Type.GetType("System.String"));
            DataColumn DISTID = new DataColumn("DISTID", Type.GetType("System.String"));
            DataColumn Typeid = new DataColumn("Typeid", Type.GetType("System.String"));
            DataColumn desigid = new DataColumn("desigid", Type.GetType("System.String"));

            dtCurrentTable.Columns.Add(formname);
            dtCurrentTable.Columns.Add(Forward);
            dtCurrentTable.Columns.Add(Timeline);
            dtCurrentTable.Columns.Add(hdnUserid);
            dtCurrentTable.Columns.Add(DEPTID);
            dtCurrentTable.Columns.Add(LOCATIONID);
            dtCurrentTable.Columns.Add(INTDIRECTORATEID);
            dtCurrentTable.Columns.Add(INTDIVISIONID);
            dtCurrentTable.Columns.Add(DISTID);
            dtCurrentTable.Columns.Add(Typeid);
            dtCurrentTable.Columns.Add(desigid);
            for (int i = 0; i <= gvService.Rows.Count - 1; i++)
            {
                Label lbllevel = (Label)gvService.Rows[i].FindControl("Label3");
                TextBox txtgrdTimeline = (TextBox)gvService.Rows[i].FindControl("txtTimeline");
                TextBox txtforwardname = (TextBox)gvService.Rows[i].FindControl("txtForward");
                HiddenField hdnusername = (HiddenField)gvService.Rows[i].FindControl("hdnforarduserid");
                //DataRow drCurrentRow = null;

                HiddenField hdnLocation = (HiddenField)gvService.Rows[i].FindControl("hdnLoc");
                HiddenField hdnDept = (HiddenField)gvService.Rows[i].FindControl("hdnDept");
                HiddenField hdnDirectorate = (HiddenField)gvService.Rows[i].FindControl("hdnDirectorate");
                HiddenField hdnDiv = (HiddenField)gvService.Rows[i].FindControl("hdnDiv");
                HiddenField hdnDist = (HiddenField)gvService.Rows[i].FindControl("hdnDist");
                HiddenField hdndesig = (HiddenField)gvService.Rows[i].FindControl("hdndesig");
               // DropDownList ddltype = (DropDownList)gvService.Rows[i].FindControl("ddltype");
                DataRow dr = dtCurrentTable.NewRow();

                //dtCurrentTable.Rows[i]["strUsername"] = txtgrdTimeline.Text;
                //dtCurrentTable.Rows[i]["strExcalationDays"] = txtforwardname.Text;
                //dtCurrentTable.Rows[i]["userid"] = hdnusername.Value;
                dr["intEscalationId"] = lbllevel.Text;
                dr["strExcalationDays"] = txtgrdTimeline.Text;
                dr["strUsername"] = txtforwardname.Text;
                dr["userid"] = hdnusername.Value;
                dr["DEPTID"] = hdnDept.Value;
                dr["LOCATIONID"] = hdnLocation.Value;
                dr["DirectId"] = hdnDirectorate.Value;
                dr["DivisionId"] = hdnDiv.Value;
                dr["DISTID"] = hdnDist.Value;
                dr["Typeid"] = ddltype.SelectedValue;
                dr["desigid"] = hdndesig.Value;
                dtCurrentTable.Rows.Add(dr);
                dtCurrentTable.AcceptChanges();
            }
            ViewState["dtstored"] = dtCurrentTable;
            Session["dtToGrid"] = dtCurrentTable;
        }
        else
        {
            BindGrid();
        }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e) 
    {
        Button btnsave = (Button)sender;
        try{
        //DropDownList ddluservalue = (DropDownList)btnsave.FindControl("ddluser");
        string row="00";
        string gvrow="";
        if (Convert.ToInt32(hdnRowid.Value) < 10)
        {
          
            gvrow = "0" + (Convert.ToInt32(hdnRowid.Value) + 2).ToString();
        }
        else
        {
            
            gvrow =  (Convert.ToInt32(hdnRowid.Value) + 2).ToString();
        }
        TextBox txtforwarddata = (TextBox)btnsave.FindControl("txtForward");
        HiddenField hdnusernameid = (HiddenField)btnsave.FindControl("hdnforarduserid");
        HiddenField hdndesig = (HiddenField)btnsave.FindControl("hdndesig");

        HiddenField hdnLocation = (HiddenField)btnsave.FindControl("hdnLoc");
        HiddenField hdnDept = (HiddenField)btnsave.FindControl("hdnDept");
        HiddenField hdnDirectorate = (HiddenField)btnsave.FindControl("hdnDirectorate");
        HiddenField hdnDiv = (HiddenField)btnsave.FindControl("hdnDiv");
        HiddenField hdnDist = (HiddenField)btnsave.FindControl("hdnDist");

        //txtforwarddata.Text = ddluservalue.SelectedItem.Text;
        //hdnusernameid.Value = ddluservalue.SelectedValue;

        //Added BY Pritiprangya Pattanaik
        hdnDist.Value = Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlDistrict"];
        hdnDiv.Value = Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlDivision"];
        hdnDirectorate.Value = Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddldirectorate"];
        hdnLocation.Value = Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlLocation"];
        hdnDept.Value = Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlDepartment"];
        hdndesig.Value = Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddldiv"];
        if (Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlDepartment"] != "0")
        {
            txtforwarddata.Text = hdnHirarchyText.Value;
            hdnusernameid.Value = Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlDistrict"];          
        }
        else {

            if (Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlDivision"] != "0")
            {
                txtforwarddata.Text = hdnHirarchyText.Value;
                hdnusernameid.Value = Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlDivision"];              
            }
            else
            {

                if (Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddldirectorate"] != "0")
                {
                    txtforwarddata.Text = hdnHirarchyText.Value;
                    hdnusernameid.Value = Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddldirectorate"];                   
                }
                else
                {
                    if (Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlDepartment"] != "0")
                    {
                        txtforwarddata.Text = hdnHirarchyText.Value;
                        hdnusernameid.Value = Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlDepartment"];
                    }
                    else
                    {
                        if (Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlLocation"] != "0")
                        {
                            txtforwarddata.Text = hdnHirarchyText.Value;
                            hdnusernameid.Value = Request["ctl" + row + "$ContentPlaceHolder1$gvService$ctl" + gvrow + "$ddlLocation"];
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Location');", true);
                            return;
                        }
                    }

                }
            }
        
        
        }

        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
      
        
    }
    protected void imgbtnAdd_Click(object sender, EventArgs e) 
    {
        AddNewRowToGrid();
    }

    protected void imgbtnDelete_Click(object sender, EventArgs e) 
    {
        try
        {
            var argument = ((ImageButton)sender).CommandArgument;
            int ss = Convert.ToInt32(argument);
            DataTable dtDel = new DataTable();
            dtDel = (DataTable)ViewState["dtstored"];
            dtDel.Rows.RemoveAt(ss);

            dtDel.AcceptChanges();
            ViewState["dtstored"] = dtDel;
            Bindgrid(dtDel);
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        
    }

    private void Bindgrid(DataTable dataTable) //Using for fill data in gridview...
    {
        gvService.DataSource = dataTable;
        gvService.DataBind();
    }

    private void AddNewRowToGrid()
    {
        try{
        if (ViewState["dtstored"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["dtstored"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                drCurrentRow = dtCurrentTable.NewRow();
                //drCurrentRow["intEscalationId"] = "Level " + (dtCurrentTable.Rows.Count + 1).ToString();
                drCurrentRow["intEscalationId"] = dtCurrentTable.Rows.Count + 1;
                //dtCurrentTable.Rows.Count + 1;
                //add new row to DataTable
                //hdnlevelname.Value = drCurrentRow["formname"].ToString();
                dtCurrentTable.Rows.Add(drCurrentRow);
                //Store the current data to ViewState
                ViewState["dtstored"] = dtCurrentTable;

                for (int i = 0; i < dtCurrentTable.Rows.Count - 1; i++)
                {
                    Label lbllevel = (Label)gvService.Rows[i].FindControl("Label3");
                    TextBox txtForward = (TextBox)gvService.Rows[i].FindControl("txtForward");
                    TextBox txtTimeline = (TextBox)gvService.Rows[i].FindControl("txtTimeline");
                    HiddenField hdnforarduserid = (HiddenField)gvService.Rows[i].FindControl("hdnforarduserid");


                    HiddenField hdnLocation = (HiddenField)gvService.Rows[i].FindControl("hdnLoc");
                    HiddenField hdnDept = (HiddenField)gvService.Rows[i].FindControl("hdnDept");
                    HiddenField hdnDirectorate = (HiddenField)gvService.Rows[i].FindControl("hdnDirectorate");
                    HiddenField hdnDiv = (HiddenField)gvService.Rows[i].FindControl("hdnDiv");
                    HiddenField hdnDist = (HiddenField)gvService.Rows[i].FindControl("hdnDist");

                    HiddenField hdndesig = (HiddenField)gvService.Rows[i].FindControl("hdndesig");
                   // DropDownList ddltype = (DropDownList)gvService.Rows[i].FindControl("ddltype");

                    dtCurrentTable.Rows[i]["intEscalationId"] = lbllevel.Text;
                    dtCurrentTable.Rows[i]["strUsername"] = txtForward.Text;
                    dtCurrentTable.Rows[i]["strExcalationDays"] = txtTimeline.Text;
                    dtCurrentTable.Rows[i]["userid"] = hdnforarduserid.Value;

                    dtCurrentTable.Rows[i]["DEPTID"] = hdnDept.Value;
                    dtCurrentTable.Rows[i]["LOCATIONID"] = hdnLocation.Value;
                    dtCurrentTable.Rows[i]["DirectId"] = hdnDirectorate.Value;
                    dtCurrentTable.Rows[i]["DivisionId"] = hdnDiv.Value;
                    dtCurrentTable.Rows[i]["DISTID"] = hdnDist.Value;
                    dtCurrentTable.Rows[i]["Typeid"] = ddltype.SelectedValue;
                    dtCurrentTable.Rows[i]["desigid"] = hdndesig.Value;
                }
                //Rebind the Grid with the current data
                gvService.DataSource = dtCurrentTable;
                gvService.DataBind();
                //clear();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks
        SetPreviousData();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }

    private void SetPreviousData()
    {
        try{
        int rowIndex = 0;
        if (ViewState["dtstored"] != null)
        {
            DataTable dt = (DataTable)ViewState["dtstored"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox txtForward = (TextBox)gvService.Rows[i].FindControl("txtForward");
                    TextBox txtTimeline = (TextBox)gvService.Rows[i].FindControl("txtTimeline");
                 //   DropDownList ddltype = (DropDownList)gvService.Rows[i].FindControl("ddltype");
                    if (i < dt.Rows.Count - 1)
                    {
                        txtForward.Text = dt.Rows[i]["strUsername"].ToString();
                        txtTimeline.Text = dt.Rows[i]["strExcalationDays"].ToString();
                       // ddltype.SelectedValue = dt.Rows[i]["Typeid"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
    }

    [WebMethod]
    //public static List<ServiceDetails> FillLocation(string id) 
    //{
    //    string gStrRetVal = null;
    //    int returnval = 0;
    //    ApprovalConfigService.ServiceBusinessLayerClient objService = new ApprovalConfigService.ServiceBusinessLayerClient();
    //    List<ServiceDetails> objServicelist = new List<ServiceDetails>();
    //    ServiceDetails objServiceDet = new ServiceDetails(); 
    //    try
    //    {
    //        gStrRetVal = id;
    //        //objProposal.strAction = "E";
    //        objServicelist = objService.BindAllDepartment("D", Convert.ToInt32(id)).ToList();

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    finally
    //    {
    //        //objLinc = null;
    //    }
    //    return objServicelist;
    //}
    public static List<ListItem> FillDepartment(string id) 
    {
        List<ListItem> branches = new List<ListItem>();
        try
        {
            string query = "SELECT INTLEVELDETAILID,NVCHLEVELNAME FROM [M_ADM_LEVELDETAILS] WHERE INTPARENTID='" + id + "' AND BITSTATUS=1";
            string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            branches.Add(new ListItem
                            {
                                Value = sdr["INTLEVELDETAILID"].ToString(),
                                Text = sdr["NVCHLEVELNAME"].ToString()
                            });
                        }
                    }
                    con.Close();

                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        return branches;
    }
    [WebMethod]
    public static List<ListItem> FillDirectorate(string id) 
    {
        List<ListItem> branches = new List<ListItem>();
        try
        {
            string query = "SELECT INTLEVELDETAILID,NVCHLEVELNAME FROM [M_ADM_LEVELDETAILS] WHERE INTPARENTID='" + id + "' AND BITSTATUS=1";
            string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                 
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            branches.Add(new ListItem
                            {
                                Value = sdr["INTLEVELDETAILID"].ToString(),
                                Text = sdr["NVCHLEVELNAME"].ToString()
                            });
                        }
                    }
                    con.Close();
                    
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        return branches;
    }
    [WebMethod]
    public static List<ListItem> FillDivision(string id) 
    {
        List<ListItem> branches = new List<ListItem>();
        try
        {
            string query = "SELECT INTLEVELDETAILID,NVCHLEVELNAME FROM [M_ADM_LEVELDETAILS] WHERE INTPARENTID='" + id + "' AND BITSTATUS=1";
            string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            branches.Add(new ListItem
                            {
                                Value = sdr["INTLEVELDETAILID"].ToString(),
                                Text = sdr["NVCHLEVELNAME"].ToString()
                            });
                        }
                    }
                    con.Close();
                   
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        return branches;
    }
    [WebMethod]
    public static List<ListItem> FillDistrict(string id) 
    {
        List<ListItem> branches = new List<ListItem>();
        try{
        string query = "SELECT INTLEVELDETAILID,NVCHLEVELNAME FROM [M_ADM_LEVELDETAILS] WHERE INTPARENTID='" + id + "' AND BITSTATUS=1";
        string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
               
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        branches.Add(new ListItem
                        {
                            Value = sdr["INTLEVELDETAILID"].ToString(),
                            Text = sdr["NVCHLEVELNAME"].ToString()
                        });
                    }
                }
                con.Close();
              
            }
        }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        return branches;
    }
    [WebMethod]
    public static List<ListItem> FillUser(string id) 
    {
        List<ListItem> branches = new List<ListItem>();
        try{
        string query = "SELECT INTUSERID,vchFullName FROM M_POR_USER WHERE INTLEVELDETAILID='" + id + "' AND BITSTATUS=1";
        string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(query))
            {
               
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        branches.Add(new ListItem
                        {
                            Value = sdr["INTUSERID"].ToString(),
                            Text = sdr["vchFullName"].ToString()
                        });
                    }
                }
                con.Close();
                return branches;
            }
        }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        return branches;
    }
    [WebMethod]
    public static List<ListItem> FillDesignation()
    {
        List<ListItem> branches = new List<ListItem>();
        try
        {
            string query = "select intDesigId,nvchDesigName from M_ADM_Designation where bitStatus=1";
            string constr = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            branches.Add(new ListItem
                            {
                                Value = sdr["intDesigId"].ToString(),
                                Text = sdr["nvchDesigName"].ToString()
                            });
                        }
                    }
                    con.Close();

                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Service");
        }
        return branches;
    }
    //protected void gvService_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    GridView GridView1 = (GridView)sender;
    //    HiddenField hdnusernameid = (HiddenField)GridView1.FindControl("hdnforarduserid");
    //    //lblconfigid.Text = gvService.DataKeys[e.RowIndex].Value.ToString();
    //    ApprovalConfigService.ServiceBusinessLayerClient objService = new ApprovalConfigService.ServiceBusinessLayerClient();
    //    ServiceDetails objService1 = new ServiceDetails();
    //    List<ServiceDetails> objServicelist = new List<ServiceDetails>();

    //    objService1.strAction = "R";
    //    objService1.intConfigId = Convert.ToInt32(hdnusernameid.Value);
    //    objServicelist = objService.DeleteServiceConfigurationData(objService1).ToList();
    //    gvService.DataSource = objService.DeleteServiceConfigurationData(objService1).ToList();
    //    gvService.DataBind();
    //}
    protected void gvService_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "Delete")
        //{
        //    int ID = Convert.ToInt32(e.CommandArgument);
        //    DataTable updt = (DataTable)Session["dtToGrid"];
        //    int i = 0;
        //    while (i != 0)
        //    {
        //        if (Convert.ToInt32(updt.Rows[i]["userid"]) == ID)
        //            updt.Rows[i].Delete();
        //        i++;
        //    }
        //}

        if (e.CommandName == "Delete")
        {
            //var RowNum = Convert.ToInt32(e.CommandArgument.ToString()) - 1;
            //DataTable updt = (DataTable)Session["dtToGrid"];
            //DataRow dr = updt.Rows[RowNum];
            //dr.Delete();


        }
    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridDetails(sender, e);
    }
}