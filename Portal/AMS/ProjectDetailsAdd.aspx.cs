//******************************************************************************************************************
// File Name             :   SingleWindow/ProjectDetailsAdd.aspx
// Description           :   To Add project details against a project by Nodal Officer
// Created by            :   Tapan Kumar Mishra
// Created on            :   19-July-2016
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//         
//********************************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class SingleWindow_ProjectDetailsAdd : System.Web.UI.Page
{
    #region "Member Variable"    
    Agenda objcs = null;
    DataTable dt = null;
    AMS objams = new AMS();
    string strVal = "";
    int intType = 0;
    private DataTable Objdt = new DataTable();
    int Status = 0;
    #endregion

    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            Response.Redirect("ProjectMasterAdd.aspx");
        }

        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            intType = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
            if (!IsPostBack)
            {

                
                ShowGrid();
                ShowSourceGrid();
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    if (System.Web.HttpContext.Current.Session["PType"] == null)
                    {
                        FillDetails("E");
                    }
                    else if (Session["PType"].ToString() == "2")
                    {
                        FillDetails("VPD");
                       
                        this.btnReset.Text = "Reset";
                        this.btnSubmit.Text = "Next";
                    }
                }
                else
                {
                    Session["PType"] = null;
                    this.btnReset.Text = "Reset";
                    this.btnSubmit.Text = "Next";
                   
                }
                if (intType == 3 || intType == 4)
                {
                    if (Status == 3 || Status == 4 || Status == 0)
                    {
                        trRemark.Visible = false;
                        trRemarkEnt.Visible = false;
                    }
                    else
                    {
                        trRemark.Visible = true;
                        trRemarkEnt.Visible = true;
                    }
                }
                else
                {
                    trRemark.Visible = false;
                    trRemarkEnt.Visible = false;
                }
                FELand.ValidChars = FELand.ValidChars + "\r\n";
                FEWater.ValidChars = FEWater.ValidChars + "\r\n";
                FEPower.ValidChars = FEPower.ValidChars + "\r\n";
                FEMonths.ValidChars = FEMonths.ValidChars + "\r\n";
                FERemark.ValidChars = FERemark.ValidChars + "\r\n";
            }
          
        }
        TextCheck(txtLand, lblLand);
        TextCheck(txtWater, lblWater);
        TextCheck(txtPower, lblPower);
        TextCheck1(txtMonths, lblMonth);
    }

    #endregion
    
    #region "User function"

    private void TextCheck(TextBox txt, Label lbl)
    {
        try
        {
           int count =Convert.ToInt32(txt.Text.Length);
           double diff = 5000 - Convert.ToInt32(count);
           lbl.Text =Convert.ToString(diff);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { }
    }
    private void TextCheck1(TextBox txt, Label lbl)
    {
        try
        {
            int count = Convert.ToInt32(txt.Text.Length);
            double diff = 150 - Convert.ToInt32(count);
            lbl.Text = Convert.ToString(diff);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { }
    }

    private void TextCheck2(TextBox txt, Label lbl)
    {
        try
        {
            int count = Convert.ToInt32(txt.Text.Length);
            double diff = 500 - Convert.ToInt32(count);
            lbl.Text = Convert.ToString(diff);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { }
    }

    private void EnableDisableControl(bool val)
    {       
        txtLand.Enabled = val;
        txtWater.Enabled = val;
        txtPower.Enabled = val;
        txtMonths.Enabled = val;
        txtDirectEmployment.Enabled = val;
        txtContractual.Enabled = val;     
    }

    private void ClearAll()
    {
        txtLand.Text = string.Empty;
        txtWater.Text = string.Empty;
        txtPower.Text = string.Empty;
        txtMonths.Text = string.Empty;
        txtDirectEmployment.Text = string.Empty;
        txtContractual.Text = string.Empty;

    }

    public void FillDetails(string ACTION)
    {
        objcs = new Agenda();
        objcs.Action = ACTION;
        objcs.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
        dt = new DataTable();
        DataSet ds = new DataSet();
        if (System.Web.HttpContext.Current.Session["PType"] == null)
        {
            ds = AMServices.ViewProjectDetailsMaster(objcs);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Status = Convert.ToInt32(ds.Tables[0].Rows[0]["INTSTATUS"].ToString());
            }
        }
        else if (Session["PType"].ToString() == "2")
        {
            ds = AMServices.ViewSWPProjectDetailsMaster(objcs);
        }
        DataTable DTT = new DataTable();
        DTT = ds.Tables[1];
        DataRow dr1 = DTT.NewRow();
        dr1["Description"] = 0;
        dr1["Cost"] = "";
        
        DTT.Rows.Add(dr1);
        grdAddMore.DataSource = DTT;
        grdAddMore.DataBind();
        if (grdAddMore.Rows.Count > 0)
        {
            for (int i = 0; i < DTT.Rows.Count; i++)
            {
                DropDownList ddlDesc = (DropDownList)grdAddMore.Rows[i].FindControl("DdlDescription");
                TextBox txtCost = (TextBox)grdAddMore.Rows[i].FindControl("txtCost");               
                ddlDesc.SelectedValue = DTT.Rows[i]["Description"].ToString();
                txtCost.Text = DTT.Rows[i]["Cost"].ToString();               
            }
        }
        if (ds.Tables[2].Rows.Count > 0)
        {
            DataTable DTT2 = new DataTable();
            DTT2 = ds.Tables[2];
            DataRow dr2 = DTT2.NewRow();
            dr2["Materials"] = "";
            dr2["Source"] = "";
            DTT2.Rows.Add(dr2);
            GrdSource.DataSource = DTT2;
            GrdSource.DataBind();
            if (GrdSource.Rows.Count > 0)
            {
                for (int i = 0; i < DTT2.Rows.Count; i++)
                {
                    TextBox textMaterial = (TextBox)GrdSource.Rows[i].FindControl("txtMaterial");
                    TextBox txtSource = (TextBox)GrdSource.Rows[i].FindControl("txtSource");
                    textMaterial.Text = DTT2.Rows[i]["Materials"].ToString();
                    txtSource.Text = DTT2.Rows[i]["Source"].ToString();

                }
            }
        }

        if (intType == 3 || intType == 4)
        {
            if (ds.Tables[3].Rows.Count > 0)
            {
                DataView dv1 = new DataView(ds.Tables[3]);
                dv1.RowFilter = "intCreatedBy=3";  //CMD

                DataView dv2 = new DataView(ds.Tables[3]);
                dv2.RowFilter = "intCreatedBy=4"; // GM

                RptCMDRemark.DataSource = dv1;
                RptCMDRemark.DataBind();

                if (dv2.Count > 0)
                {
                    RptGMRemark.DataSource = dv2;
                    RptGMRemark.DataBind();
                }

                if (dv1.Count > 0)
                    RptCMDRemark.Visible = true;
                else
                    RptCMDRemark.Visible = false;

                if (dv2.Count > 0)
                    RptGMRemark.Visible = true;
                else
                    RptGMRemark.Visible = false;

                if (intType == 3) //CMD
                {
                    if (dv1.Count > 0)
                    {
                        txtRemark.Text = dv1[dv1.Count - 1][2].ToString();
                        hdnRemarkID.Value = dv1[dv1.Count - 1][4].ToString();
                    }
                }
                else if (intType == 4) //GM
                {
                    if (dv2.Count > 0)
                    {
                        txtRemark.Text = dv2[dv2.Count - 1][2].ToString();
                        hdnRemarkID.Value = dv2[dv2.Count - 1][4].ToString();
                    }
                }
            }
        }   
       

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (intType == 3)
            {
                this.btnReset.Text = "Reset";
                this.btnSubmit.Text = "Next";
            }
            else
            {
                this.btnReset.Text = "Cancel";
                this.btnSubmit.Text = "Update";
            }
            btnSubmit.CommandArgument = ds.Tables[0].Rows[0]["PTYPE"].ToString();
            txtLand.Text = ds.Tables[0].Rows[0]["vchLand"].ToString();
            txtWater.Text = ds.Tables[0].Rows[0]["vchWater"].ToString();
            txtPower.Text = ds.Tables[0].Rows[0]["vchPower"].ToString();
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["intPowerSource"].ToString()) == 1)
                rbtCpp.Checked = true;
            else if (Convert.ToInt32(ds.Tables[0].Rows[0]["intPowerSource"].ToString()) == 2)
                rbtGrid.Checked=true;
            else if (Convert.ToInt32(ds.Tables[0].Rows[0]["intPowerSource"].ToString()) == 3)
            {
                rbtCpp.Checked = true;
                rbtGrid.Checked = true;
            }
            txtMonths.Text = ds.Tables[0].Rows[0]["vchImplementPeriod"].ToString();
            txtDirectEmployment.Text = ds.Tables[0].Rows[0]["intEmployement"].ToString();
            txtContractual.Text = ds.Tables[0].Rows[0]["intContractual"].ToString();            
        }
        else
        {
            this.btnReset.Text = "Reset";
            this.btnSubmit.Text = "Next";
            btnSubmit.CommandArgument = "2";
            ClearAll();
        }
    }

    #endregion

    #region "Button Event"

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        
        objcs = new Agenda();
        try
        {          
            objcs.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            objcs.FinanceDescription = "";
            objcs.Land=txtLand.Text;
            objcs.Water=txtWater.Text;
            objcs.Power=txtPower.Text;
            if (rbtCpp.Checked == true && rbtGrid.Checked != true)
                objcs.Source = 1;
            else if (rbtGrid.Checked == true && rbtCpp.Checked != true)
                objcs.Source = 2;
            else if (rbtCpp.Checked == true && rbtGrid.Checked == true)
                objcs.Source = 3;   
            else
                objcs.Source = 0;   
            objcs.ImplementPeriod = txtMonths.Text;
            objcs.Employement=Convert.ToInt32(txtDirectEmployment.Text);
            objcs.Contractual = Convert.ToInt32(txtContractual.Text);

            if (txtRemark.Visible == true)
            {

                objcs.Remark = txtRemark.Text.Trim() == "" ? "No Comments" : txtRemark.Text.Trim();
                objcs.intRemarkID = hdnRemarkID.Value == "" ? 0 : Convert.ToInt32(hdnRemarkID.Value);
                intType = AMServices.GetOfficersType(Convert.ToInt32(Session["Userid"]));
                objcs.CreatedBy = intType;
            }
            else
            {
                objcs.CreatedBy = Convert.ToInt32(Session["Userid"]);
            }
          
            //Added by Monalisa nayak on 23-12-2016 for Add more option Project Cost details
            DataTable dt = new DataTable();
            dt.Columns.Add("Description");
            dt.Columns.Add("Cost");
           
            for (int i = 0; i < grdAddMore.Rows.Count; i++)
            {
                DataRow dr = dt.NewRow();
                DropDownList ddlDescription = (DropDownList)grdAddMore.Rows[i].FindControl("DdlDescription");
                TextBox txtCost = (TextBox)grdAddMore.Rows[i].FindControl("txtCost");                
                dr["Description"] = ddlDescription.Text;
                dr["Cost"] = txtCost.Text;                
                if (ddlDescription.Text != "0" && txtCost.Text != "")
                {
                    dt.Rows.Add(dr);
                }            
            }
            dt.TableName = "tblProjectCostDtls";
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                dt.WriteXml(sw);
                objcs.VCH_XMLTBL = sw.ToString();
            }
            //To Add source And Material
            DataTable dtSource = new DataTable();
            dtSource.Columns.Add("Materials");
            dtSource.Columns.Add("Source");
            for (int i = 0; i < GrdSource.Rows.Count; i++)
            {
                DataRow dr = dtSource.NewRow();
                TextBox txtMaterial = (TextBox)GrdSource.Rows[i].FindControl("txtMaterial");
                TextBox txtSource = (TextBox)GrdSource.Rows[i].FindControl("txtSource");
                dr["Materials"] = txtMaterial.Text;
                dr["Source"] = txtSource.Text;
                if (txtMaterial.Text != "" && txtSource.Text != "")
                {
                    dtSource.Rows.Add(dr);
                }
            }
            dtSource.TableName = "tblSourceDtls";
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                dtSource.WriteXml(sw);
                objcs.VCH_XMLSOURCE = sw.ToString();
            }              
            //------------------------------------------------------------------

            if (btnSubmit.CommandArgument == "1")
            {
                objcs.Action = "U";
                strVal = AMServices.UpdateProjectDetails(objcs);
            }
            else if (btnSubmit.CommandArgument == "2")
            {
                objcs.Action = "I";
                objcs.Id = 0;
                strVal = AMServices.AddProjectDetails(objcs);
            }
            string msg = Messages.ShowMessage(strVal).ToString();
            
            if (strVal == "2")
                ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Updated Successfully.');location.href='FinancingDetailsAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
            else 
                ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Saved Successfully.');location.href='FinancingDetailsAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
        }
        catch (Exception ex)
        {
            //throw new Exception(ex.Message);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('" + ex.Message + "');", true);
        }
        finally { objcs = null; }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        string URL = "ProjectDetailsAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "";
            Response.Redirect(URL);
    }
    #endregion

    /// <summary>
    /// Created by Monalisa Nayak on 23-12-2016 for Add more option Project Cost details
    /// </summary>
    /// <returns></returns>
    private DataTable CreateDataTable()
    {
        DataTable Data_table = new DataTable();
        DataColumn Data_Coloumn = new DataColumn();
        Data_Coloumn = new System.Data.DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "Description";
        Data_table.Columns.Add(Data_Coloumn);
        Data_Coloumn = new DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "Cost";
        Data_table.Columns.Add(Data_Coloumn);
       
        return Data_table;
       
    }
    
    private void ShowGrid()
    {
        objcs = new Agenda();
        List<Agenda> lstDescription = new List<Agenda>();
        objcs.Id = 1;
        objcs.ProjectId = 0;
        objcs.Cost ="";      
        lstDescription.Add(objcs);
        grdAddMore.DataSource = lstDescription;
        grdAddMore.DataBind();
    }

    protected void grdAddMore_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var ddlDescription = (DropDownList)e.Row.FindControl("DdlDescription");
            dt = new DataTable();
            dt = AMServices.FillCostDetails();
            ddlDescription.DataSource = dt;
            ddlDescription.DataTextField = "VCH_COST_DTLS_DESC";
            ddlDescription.DataValueField = "INT_COST_ID";
            ddlDescription.DataBind();
            ddlDescription.Items.Insert(0, new ListItem("--Select--", "0"));

            ImageButton ddl = (ImageButton)e.Row.FindControl("imgbtnDelete");
            DropDownList ddlDesc = (DropDownList)e.Row.FindControl("DdlDescription");
            TextBox txtCost = (TextBox)e.Row.FindControl("txtCost");            
            Button BtnAddMore = (Button)e.Row.FindControl("ButtonAdd");

            if (ddlDesc.SelectedValue == "0" && (txtCost.Text == "0" || txtCost.Text == ""))
            {
                ddl.Visible = false;
                BtnAddMore.Visible = true;
            }
            else
            {
                ddl.Visible = true;
                BtnAddMore.Visible = false;
            }
        }
    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    private List<Agenda> ConvertToList()
    {
        List<Agenda> lstDescAdd = new List<Agenda>();
        int count = grdAddMore.Rows.Count;
        foreach (GridViewRow gr in grdAddMore.Rows)
        {
            int index = gr.RowIndex;
            DataTable dt = new DataTable();
            if (gr.RowType == DataControlRowType.DataRow)
            {
                objcs = new Agenda();
                objcs.Id = count + 1;
                objcs.ProjectId = Convert.ToInt32(((DropDownList)gr.FindControl("DdlDescription")).SelectedValue);
                objcs.Cost =Convert.ToString(((TextBox)gr.FindControl("txtCost")).Text);
                lstDescAdd.Add(objcs);

            }
        }
        return lstDescAdd;
    }

    private void AddNewRowToGrid()
    {
        List<Agenda> lstDesc = new List<Agenda>();
        try
        {
            if (grdAddMore.Rows.Count > 0)
            {
                lstDesc = ConvertToList();
            }
            var dupes = lstDesc.GroupBy(x => new { x.ProjectId }).Where(x => x.Skip(1).Any()).ToArray();
            if (dupes.Any())
            {
                ScriptManager.RegisterStartupScript(UpdatePanelloc, this.GetType(), "", "alert('Duplicate Project Cost Details.');", true);
                DropDownList box1 = (DropDownList)grdAddMore.Rows[grdAddMore.Rows.Count-1].Cells[1].FindControl("DdlDescription");
                TextBox box2 = (TextBox)grdAddMore.Rows[grdAddMore.Rows.Count-1].Cells[2].FindControl("txtCost");
                box1.SelectedValue = "0";
                box2.Text = "";
                return;
            }
            objcs = new Agenda();
            objcs.ProjectId = 0;
            objcs.Cost = "";           
            lstDesc.Add(objcs);
            grdAddMore.DataSource = lstDesc;
            grdAddMore.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        SetPreviousData(lstDesc);
    }

    private void SetPreviousData(List<Agenda> lstDesc)
    {
        int rowIndex = 0;
        if (lstDesc.Count > 0)
        {
            for (int i = 0; i < lstDesc.Count; i++)
            {
                DropDownList box1 = (DropDownList)grdAddMore.Rows[rowIndex].Cells[1].FindControl("DdlDescription");
                TextBox box2 = (TextBox)grdAddMore.Rows[rowIndex].Cells[2].FindControl("txtCost");                
                box1.SelectedValue = lstDesc[i].ProjectId.ToString();
                box2.Text = lstDesc[i].Cost.ToString();               
                rowIndex++;
            }
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = null;
        try
        {
            dt = CreateDataTable();
            ImageButton imgbtn = (ImageButton)sender;
            Label lblSlno = (Label)imgbtn.FindControl("lblSlno");
            for (int i = 0; i <= grdAddMore.Rows.Count - 1; i++)
            {
                DataRow dr = dt.NewRow();
                DropDownList ddlDescription = (DropDownList)grdAddMore.Rows[i].FindControl("DdlDescription");
                TextBox textCost = (TextBox)grdAddMore.Rows[i].FindControl("txtCost");                
                dr["Description"] = ddlDescription.Text;
                dr["Cost"] = textCost.Text;               
                dt.Rows.Add(dr);
            }
            if (dt.Rows.Count > 0)
            {
                dt.Rows.Remove(dt.Rows[int.Parse(lblSlno.Text) - 1]);
                grdAddMore.DataSource = dt;
                grdAddMore.DataBind();
                if (grdAddMore.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList district = (DropDownList)grdAddMore.Rows[i].FindControl("DdlDescription");
                        TextBox textCost = (TextBox)grdAddMore.Rows[i].FindControl("txtCost");                        
                        district.SelectedValue = dt.Rows[i]["Description"].ToString();
                        textCost.Text = Convert.ToString(dt.Rows[i]["Cost"]);                       
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt = null;
        }
    }
 
    private void ShowSourceGrid()
    {
        objams = new AMS();
        List<AMS> lstSource = new List<AMS>();
        objams.ID = 1;
        objams.Materials = "";
        objams.Source = "";
        lstSource.Add(objams);
        GrdSource.DataSource = lstSource;
        GrdSource.DataBind();
    }

    protected void GrdSource_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton ImgSource = (ImageButton)e.Row.FindControl("imgbtnDltSource");
            TextBox TxtMaterial = (TextBox)e.Row.FindControl("txtMaterial");
            TextBox TxtSource = (TextBox)e.Row.FindControl("txtSource");
            Button BtnAddMore = (Button)e.Row.FindControl("BtnAddSource");

            if (TxtMaterial.Text == "" && TxtSource.Text == "")
            {
                ImgSource.Visible = false;
                BtnAddMore.Visible = true;
            }
            else
            {
                ImgSource.Visible = true;
                BtnAddMore.Visible = false;
            }
        }
    }

    protected void BtnAddSource_Click(object sender, EventArgs e)
    {
        AddNewRowSourceGrid();
    }

    private List<AMS> ConvertToSourceList()
    {
        List<AMS> lstSourceAdd = new List<AMS>();
        int count = GrdSource.Rows.Count;
        foreach (GridViewRow gr in GrdSource.Rows)
        {
            int index = gr.RowIndex;
            DataTable dt = new DataTable();
            if (gr.RowType == DataControlRowType.DataRow)
            {
                objams = new AMS();
                objams.ID = count + 1;
                objams.Materials = ((TextBox)gr.FindControl("txtMaterial")).Text;
                objams.Source = ((TextBox)gr.FindControl("txtSource")).Text;
                lstSourceAdd.Add(objams);

            }
        }
        return lstSourceAdd;
    }

    private void AddNewRowSourceGrid()
    {
        List<AMS> lstSource = new List<AMS>();
        try
        {
            if (GrdSource.Rows.Count > 0)
            {
                lstSource = ConvertToSourceList();
            }
            var dupes = lstSource.GroupBy(x => new { x.Materials, x.Source }).Where(x => x.Skip(1).Any()).ToArray();
            if (dupes.Any())
            {
                ScriptManager.RegisterStartupScript(UpdSource, this.GetType(), "", "alert('Duplicate Source Details.');", true);
                TextBox box1 = (TextBox)GrdSource.Rows[GrdSource.Rows.Count-1].Cells[1].FindControl("txtMaterial");
                TextBox box2 = (TextBox)GrdSource.Rows[GrdSource.Rows.Count-1].Cells[2].FindControl("txtSource");
                box1.Text = "";
                box2.Text = "";
                return;
            }
            objams = new AMS();
            objams.Materials = "";
            objams.Source = "";
            lstSource.Add(objams);
            GrdSource.DataSource = lstSource;
            GrdSource.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }
        SetPreviousFinData(lstSource);
    }

    private void SetPreviousFinData(List<AMS> lstSource)
    {
        int rowIndex = 0;
        if (lstSource.Count > 0)
        {
            for (int i = 0; i < lstSource.Count; i++)
            {
                TextBox box1 = (TextBox)GrdSource.Rows[rowIndex].Cells[1].FindControl("txtMaterial");
                TextBox box2 = (TextBox)GrdSource.Rows[rowIndex].Cells[2].FindControl("txtSource");
                box1.Text = lstSource[i].Materials.ToString();
                box2.Text = lstSource[i].Source.ToString();
                rowIndex++;
            }
        }
    }

    private DataTable CreateDataTableSource()
    {
        DataTable Data_table = new DataTable();
        DataColumn Data_Coloumn = new DataColumn();
        Data_Coloumn = new System.Data.DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "Materials";
        Data_table.Columns.Add(Data_Coloumn);
        Data_Coloumn = new DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "Source";
        Data_table.Columns.Add(Data_Coloumn);
        return Data_table;

    }

    protected void imgbtnDltSource_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = null;
        try
        {
            dt = CreateDataTableSource();
            ImageButton imgbtn = (ImageButton)sender;
            Label lblSlno = (Label)imgbtn.FindControl("lblSlno");
            for (int i = 0; i <= GrdSource.Rows.Count - 1; i++)
            {
                DataRow dr = dt.NewRow();
                TextBox txtMaterial = (TextBox)GrdSource.Rows[i].FindControl("txtMaterial");
                TextBox textSource = (TextBox)GrdSource.Rows[i].FindControl("txtSource");
                dr["Materials"] = txtMaterial.Text;
                dr["Source"] = textSource.Text;
                dt.Rows.Add(dr);
            }
            if (dt.Rows.Count > 0)
            {
                dt.Rows.Remove(dt.Rows[int.Parse(lblSlno.Text) - 1]);
                GrdSource.DataSource = dt;
                GrdSource.DataBind();
                if (GrdSource.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox txtMaterial = (TextBox)GrdSource.Rows[i].FindControl("txtMaterial");
                        TextBox txtsource = (TextBox)GrdSource.Rows[i].FindControl("txtSource");
                        txtMaterial.Text = dt.Rows[i]["Materials"].ToString();
                        txtsource.Text = Convert.ToString(dt.Rows[i]["Source"]);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            dt = null;
        }
    }
}