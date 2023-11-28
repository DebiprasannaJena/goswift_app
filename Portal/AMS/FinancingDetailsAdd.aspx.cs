//******************************************************************************************************************
// File Name             :   SingleWindow/FinancingDetailsAdd.aspx
// Description           :   To Add Financial details against a project by Nodal Officer
// Created by            :   
// Created on            :   
// Modification History  :
//       <CR no.>                      <Date>             <Modified by>                <Modification Summary>'                                                          
//          1                          03-Oct-2017         Surya Prakash Barik           Show data from SWP,add CMD Comment
//********************************************************************************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class SingleWindow_FinancingDetailsAdd : System.Web.UI.Page
{
    AMS objams = new AMS();
    DataTable dt = null;
    Agenda objcs = null;
    int intType = 0;
    string strVal = "";
    int Status = 0;

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

                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    if (System.Web.HttpContext.Current.Session["PType"] == null)
                    {
                        FillDetails("M");
                    }
                    else if (Session["PType"].ToString() == "2")
                    {
                        FillDetails("VFD"); //SWP Details
                    }
                }
                else
                {
                    ShowFinGrid();
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
              
            }
           
        }
        FEFinDescription.ValidChars = FEFinDescription.ValidChars + "\r\n";
        FERemark.ValidChars = FERemark.ValidChars + "\r\n";
    }

    public void FillDetails(string ACTION)
    {
        objcs = new Agenda();
        objcs.Action = ACTION;
        objcs.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
        DataSet ds = new DataSet();
        if (System.Web.HttpContext.Current.Session["PType"] == null)
        {
            ds = AMServices.ViewProjectDetailsMaster(objcs);
            if (ds.Tables[0].Rows.Count > 0)
            {
                Status = Convert.ToInt32(ds.Tables[0].Rows[0]["INTSTATUS"].ToString());
            }
        }
        else if(Session["PType"].ToString() == "2")
        {
            ds = AMServices.ViewSWPProjectDetailsMaster(objcs);
        }
        DataRow dr2 = ds.Tables[0].NewRow();
        dr2["FinDescription"] = 0;
        dr2["FinAmount"] = "";
        dr2["Percentage"] = "";
        ds.Tables[0].Rows.Add(dr2);
        grvFinDtls.DataSource = ds.Tables[0];
        grvFinDtls.DataBind();
        if (grvFinDtls.Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                DropDownList ddlfin = (DropDownList)grvFinDtls.Rows[i].FindControl("ddlFinDtls");
                TextBox textFinAmount = (TextBox)grvFinDtls.Rows[i].FindControl("txtFinAmnt");
                TextBox txtPercentage = (TextBox)grvFinDtls.Rows[i].FindControl("txtPercentage");
                ddlfin.SelectedValue = ds.Tables[0].Rows[i]["FinDescription"].ToString();
                textFinAmount.Text = Convert.ToString(ds.Tables[0].Rows[i]["FinAmount"]);
                txtPercentage.Text = ds.Tables[0].Rows[i]["Percentage"].ToString();  
            }
           
        }
        if (ds.Tables[0].Rows.Count > 1)
        {
            btnSubmit.CommandArgument=ds.Tables[0].Rows[0]["PTYPE"].ToString();
            if (System.Web.HttpContext.Current.Session["PType"] == null)
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
            }
            else
            {
                this.btnReset.Text = "Reset";
                this.btnSubmit.Text = "Next";
            }
        }
        else
        {
            this.btnReset.Text = "Reset";
            this.btnSubmit.Text = "Next";
            btnSubmit.CommandArgument = "2";
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            txtFinDescription.Text = ds.Tables[1].Rows[0]["vchFinanceDescription"].ToString();
        }

        if (intType == 3 || intType == 4)
        {
            if (ds.Tables[2].Rows.Count > 0)
            {
                DataView dv1 = new DataView(ds.Tables[2]);
                dv1.RowFilter = "intCreatedBy=3";  //CMD

                DataView dv2 = new DataView(ds.Tables[2]);
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
    }
  
    //public void FillSWPDetails()
    //{
    //    objcs = new Agenda();
    //    objcs.Action = "VFD";
    //    objcs.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
    //    DataSet ds = new DataSet();
    //    ds = AMServices.ViewSWPProjectDetailsMaster(objcs);
    //    DataRow dr2 = ds.Tables[0].NewRow();
    //    dr2["FinDescription"] = 0;
    //    dr2["FinAmount"] = "";
    //    dr2["Percentage"] = "";
    //    ds.Tables[0].Rows.Add(dr2);
    //    grvFinDtls.DataSource = ds.Tables[0];
    //    grvFinDtls.DataBind();
    //    if (grvFinDtls.Rows.Count > 0)
    //    {
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            DropDownList ddlfin = (DropDownList)grvFinDtls.Rows[i].FindControl("ddlFinDtls");
    //            TextBox textFinAmount = (TextBox)grvFinDtls.Rows[i].FindControl("txtFinAmnt");
    //            TextBox txtPercentage = (TextBox)grvFinDtls.Rows[i].FindControl("txtPercentage");
    //            ddlfin.SelectedValue = ds.Tables[0].Rows[i]["FinDescription"].ToString();
    //            textFinAmount.Text = Convert.ToString(ds.Tables[0].Rows[i]["FinAmount"]);
    //            txtPercentage.Text = ds.Tables[0].Rows[i]["Percentage"].ToString();
    //        }

    //    }
    //    if (ds.Tables[0].Rows.Count > 2)
    //    {
    //        if (Request.QueryString["PType"].ToString() == "1")
    //        {
    //            this.btnReset.Text = "Cancel";
    //            this.btnSubmit.Text = "Update";
    //        }
    //        else
    //        {
    //            this.btnReset.Text = "Reset";
    //            this.btnSubmit.Text = "Next";
    //        }
    //    }
    //    else
    //    {
    //        this.btnReset.Text = "Reset";
    //        this.btnSubmit.Text = "Next";
    //    }
    //    if (ds.Tables[1].Rows.Count > 0)
    //    {
    //        txtFinDescription.Text = ds.Tables[1].Rows[0]["vchFinanceDescription"].ToString();

    //    }

    //}

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objcs = new Agenda();
            objcs.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            objcs.FinanceDescription = txtFinDescription.Text;
            if (txtRemark.Visible == true)
            {
                objcs.Remark = txtRemark.Text.Trim() == "" ? "No Comments" : txtRemark.Text.Trim();
                objcs.intRemarkID = hdnRemarkID.Value == "" ? 0 : Convert.ToInt32(hdnRemarkID.Value);
                intType = AMServices.GetOfficersType(Convert.ToInt32(Session["Userid"]));
                objcs.CreatedBy = intType;
            }
            else
            {
                objcs.CreatedBy = Convert.ToInt32(Session["UserId"]);
            }
          
            
            DataTable dtFin = new DataTable();
            dtFin.Columns.Add("FinDescription");
            dtFin.Columns.Add("FinAmount");
            dtFin.Columns.Add("Percentage");
            for (int i = 0; i < grvFinDtls.Rows.Count; i++)
            {
                DataRow dr = dtFin.NewRow();

                DropDownList ddlFinDescription = (DropDownList)grvFinDtls.Rows[i].FindControl("ddlFinDtls");
                TextBox txtFinAmount = (TextBox)grvFinDtls.Rows[i].FindControl("txtFinAmnt");
                TextBox txtPercentage = (TextBox)grvFinDtls.Rows[i].FindControl("txtPercentage");
                dr["FinDescription"] = ddlFinDescription.Text;
                dr["FinAmount"] = txtFinAmount.Text;
                dr["Percentage"] = txtPercentage.Text;
                if (ddlFinDescription.Text != "0" && txtFinAmount.Text != "")
                {
                    dtFin.Rows.Add(dr);
                }
            }
            dtFin.TableName = "tblFinDtls";
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                dtFin.WriteXml(sw);
                objcs.VCH_XMLFINTBL = sw.ToString();
            }
           
            if (btnSubmit.CommandArgument == "1")
            {
                objcs.Action = "UF";
            }
            else if (btnSubmit.CommandArgument == "2")
            {
                objcs.Action = "IF";
            }

            strVal = AMServices.AddFinDetails(objcs);
            string msg = Messages.ShowMessage(strVal).ToString();
          
            if (strVal == "2")
            {
               ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Updated Successfully.');location.href='FinancialPerformanceAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);             
            }
            else 
            {
                ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Saved Successfully.');location.href='FinancialPerformanceAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert(' Proposal Details Updated Sucessfully ');window.location.href='ProjectMasterView.aspx?ranNum=" + Request.QueryString["ranNum"] + "&linkm=" + Request.QueryString["linkm"] + "&linkn=" + Request.QueryString["linkn"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "';", true);
            }

        }

        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally { objams = null; }
    }

    private DataTable CreateDataTableFin()
    {
        DataTable Data_table = new DataTable();
        DataColumn Data_Coloumn = new DataColumn();
        Data_Coloumn = new System.Data.DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "FinDescription";
        Data_table.Columns.Add(Data_Coloumn);
        Data_Coloumn = new DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "FinAmount";
        Data_table.Columns.Add(Data_Coloumn);
        Data_Coloumn = new DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "Percentage";
        Data_table.Columns.Add(Data_Coloumn);
        return Data_table;

    }

    protected void imgbtnDeleteFinDtls_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = null;
        try
        {
            dt = CreateDataTableFin();
            ImageButton imgbtn = (ImageButton)sender;
            Label lblSlno = (Label)imgbtn.FindControl("lblSlno");
            for (int i = 0; i <= grvFinDtls.Rows.Count - 1; i++)
            {
                DataRow dr = dt.NewRow();
                DropDownList ddlFinDescription = (DropDownList)grvFinDtls.Rows[i].FindControl("ddlFinDtls");
                TextBox textFinAmount = (TextBox)grvFinDtls.Rows[i].FindControl("txtFinAmnt");
                TextBox txtPercentage = (TextBox)grvFinDtls.Rows[i].FindControl("txtPercentage");
                dr["FinDescription"] = ddlFinDescription.Text;
                dr["FinAmount"] = textFinAmount.Text;
                dr["Percentage"] = txtPercentage.Text;
                dt.Rows.Add(dr);
            }
            if (dt.Rows.Count > 0)
            {
                dt.Rows.Remove(dt.Rows[int.Parse(lblSlno.Text) - 1]);
                grvFinDtls.DataSource = dt;
                grvFinDtls.DataBind();
                if (grvFinDtls.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList ddlfin = (DropDownList)grvFinDtls.Rows[i].FindControl("ddlFinDtls");
                        TextBox textFinAmount = (TextBox)grvFinDtls.Rows[i].FindControl("txtFinAmnt");
                        TextBox txtPercentage = (TextBox)grvFinDtls.Rows[i].FindControl("txtPercentage");
                        ddlfin.SelectedValue = dt.Rows[i]["FinDescription"].ToString();
                        textFinAmount.Text = Convert.ToString(dt.Rows[i]["FinAmount"]);
                        txtPercentage.Text = Convert.ToString(dt.Rows[i]["Percentage"]);
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

    private void ShowFinGrid()
    {
        objams = new AMS();
        List<AMS> lstFin = new List<AMS>();
        objams.ID = 1;
        objams.FinID = 0;
        objams.FinAmount = "";
        objams.Percentage = "";
        lstFin.Add(objams);
        grvFinDtls.DataSource = lstFin;
        grvFinDtls.DataBind();
    }

    protected void grvFinDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var ddlFinDtls = (DropDownList)e.Row.FindControl("ddlFinDtls");
            dt = new DataTable();
            dt = AMServices.FillFinDetails();
            ddlFinDtls.DataSource = dt;
            ddlFinDtls.DataTextField = "VCH_FIN_DTLS_DESC";
            ddlFinDtls.DataValueField = "INT_FIN_ID";
            ddlFinDtls.DataBind();
            ddlFinDtls.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlFinDtls.SelectedValue = "0";

            ImageButton ddl = (ImageButton)e.Row.FindControl("imgbtnDeleteFinDtls");
            DropDownList ddlfin = (DropDownList)e.Row.FindControl("ddlFinDtls");
            TextBox txtFinAmnt = (TextBox)e.Row.FindControl("txtFinAmnt");
            TextBox txtPercentage = (TextBox)e.Row.FindControl("txtPercentage");
            Button BtnAddMore = (Button)e.Row.FindControl("BtnAddMoreFin");

            if (ddlfin.SelectedValue == "0" && txtFinAmnt.Text == "")
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

    protected void BtnAddMoreFin_Click(object sender, EventArgs e)
    {
        AddNewRowFinToGrid();
    }

    private List<AMS> ConvertToFinList()
    {
        List<AMS> lstDescAdd = new List<AMS>();
        int count = grvFinDtls.Rows.Count;
        foreach (GridViewRow gr in grvFinDtls.Rows)
        {
            int index = gr.RowIndex;
            DataTable dt = new DataTable();
            if (gr.RowType == DataControlRowType.DataRow)
            {
                objams = new AMS();
                objams.ID = count + 1;
                objams.FinID = Convert.ToInt32(((DropDownList)gr.FindControl("ddlFinDtls")).SelectedValue);
                objams.FinAmount = ((TextBox)gr.FindControl("txtFinAmnt")).Text;
                objams.Percentage = ((TextBox)gr.FindControl("txtPercentage")).Text;
                lstDescAdd.Add(objams);
            }
        }
        return lstDescAdd;
    }

    private void AddNewRowFinToGrid()
    {
        List<AMS> lstFin = new List<AMS>();
        try
        {
            if (grvFinDtls.Rows.Count > 0)
            {
                lstFin = ConvertToFinList();
            }
            var dupes = lstFin.GroupBy(x => new { x.FinID }).Where(x => x.Skip(1).Any()).ToArray();
            if (dupes.Any())
            {
                ScriptManager.RegisterStartupScript(UpdatePanelFin, this.GetType(), "", "alert('Duplicate Financing Details.');", true);
                DropDownList box1 = (DropDownList)grvFinDtls.Rows[grvFinDtls.Rows.Count-1].Cells[1].FindControl("ddlFinDtls");
                TextBox box2 = (TextBox)grvFinDtls.Rows[grvFinDtls.Rows.Count-1].Cells[2].FindControl("txtFinAmnt");
                TextBox box3 = (TextBox)grvFinDtls.Rows[grvFinDtls.Rows.Count-1].Cells[2].FindControl("txtPercentage");
                box1.SelectedValue = "0";
                box2.Text = "";
                box3.Text = "";
                return;
            }
            objams = new AMS();
            objams.FinID = 0;
            objams.FinAmount = "";
            objams.Percentage = "";
            lstFin.Add(objams);
            grvFinDtls.DataSource = lstFin;
            grvFinDtls.DataBind();
        }
        
        catch (Exception ex)
        {
            throw ex;
        }
        SetPreviousFinData(lstFin);
    }

    private void SetPreviousFinData(List<AMS> lstFin)
    {
        int rowIndex = 0;
        if (lstFin.Count > 0)
        {
            for (int i = 0; i < lstFin.Count; i++)
            {
                DropDownList box1 = (DropDownList)grvFinDtls.Rows[rowIndex].Cells[1].FindControl("ddlFinDtls");
                TextBox box2 = (TextBox)grvFinDtls.Rows[rowIndex].Cells[2].FindControl("txtFinAmnt");
                TextBox box3 = (TextBox)grvFinDtls.Rows[rowIndex].Cells[2].FindControl("txtPercentage");
                box1.SelectedValue = lstFin[i].FinID.ToString();
                box2.Text = lstFin[i].FinAmount.ToString();
                box3.Text = lstFin[i].Percentage.ToString();
                rowIndex++;
            }
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        string URL = "FinancialPerformanceAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + Request.QueryString["ID"] + "&PIndex=" + Request.QueryString["PIndex"] + "";
        Response.Redirect(URL);
    }
   
}