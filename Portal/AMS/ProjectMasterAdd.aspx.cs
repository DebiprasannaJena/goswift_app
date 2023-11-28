using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Web.Services;
using System.ComponentModel;


public partial class SingleWindow_ProjectMasterAdd : System.Web.UI.Page
{
    #region "Member Variable"
    AMS objams = new AMS();
    string strVal = "";
    string BoardDirectors = "";
    string BusiComp = "";
    int BoardDirectorsId = 0;
    int PCount = 0;
    int LCount = 0;
    DataTable dt = null;
    int intUserTp = 0;
    private DataTable Objdt = new DataTable();
    int Status = 0;

    #endregion

    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    
    {
        Session["UserId"] = Session["Userid"];
        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {
            try
            {
                intUserTp = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
                this.Header.DataBind();

                if (!IsPostBack)
                {

                    FillSector();
                    txtTitle.Focus();
                    FETitle.ValidChars = FETitle.ValidChars + "\r\n";
                    if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                    {
                        MakeDt();
                        if (System.Web.HttpContext.Current.Session["PType"] == null)
                        {
                            GetDetails("E");
                        }
                        else if (Session["PType"].ToString() == "2")  //Comes from Singlewindow Portal
                        {
                            GetDetails("CNP");
                        }
                    }
                    else
                    {
                        ShowGrid();
                        ShowProductGrid();
                        Session["PType"] = null;
                        ClrData();
                    }
                    if (intUserTp == 3 || intUserTp == 4)
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
                        Session["Remark"] = "";
                    }
                    else
                    {
                        trRemark.Visible = false;
                        trRemarkEnt.Visible = false;
                    }
                }

            }

            catch (Exception ex)
            {
                Util.LogError(ex, "Agenda");
            }
        }
        FERemark.ValidChars = FERemark.ValidChars + "\r\n";
    }

    protected void ClrData()
    {
        txtTitle.Text = "";
        txtCompanyName.Text = "";
        ddlSector.SelectedValue = "0";
        txtApplDate.Text = "";

    }

    #endregion

    #region "Fill Sector"

    private void FillSector()
    {
        try
        {
            dt = new DataTable();
            dt = AMServices.FillSector();
            ddlSector.DataSource = dt;
            ddlSector.DataTextField = "VchSectorName";
            ddlSector.DataValueField = "intSectorId";
            ddlSector.DataBind();
            ddlSector.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Agenda");
        }
        finally { dt = null; }
    }

    #endregion

    #region "Button Event"

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        objams = new AMS();
        try
        {
            if (txtTitle.Text != "" & txtCompanyName.Text != "")
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    if (Session["PType"] == null)
                    {
                        objams.Action = "U";
                        objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
                    }
                    else if (Session["PType"].ToString() == "2")
                    {
                        objams.ProjectId = 0;
                        objams.Action = "A";
                    }
                }
                else
                {
                    objams.ProjectId = 0;
                    objams.Action = "A";
                }
                if (txtRemark.Visible == true)
                {
                    objams.Remark = txtRemark.Text.Trim() == "" ? "No Comments" : txtRemark.Text.Trim();
                    objams.intRemarkID = hdnRemarkID.Value == "" ? 0 : Convert.ToInt32(hdnRemarkID.Value);
                    intUserTp = AMServices.GetOfficersType(Convert.ToInt32(Session["UserId"]));
                    objams.CreatedBy = intUserTp;
                }
                else
                {
                    objams.CreatedBy = Convert.ToInt32(Session["UserId"]);
                }

                objams.ProjectTitle = Convert.ToString(txtTitle.Text.Trim());
                objams.CompanyName = Convert.ToString(txtCompanyName.Text.Trim());
                objams.ApplicationDate = Convert.ToDateTime(txtApplDate.Text);
                //objams.ProjectLocation = Convert.ToString(txtLocation.Text.Trim());             
                objams.SectorId = Convert.ToInt32(ddlSector.SelectedValue);
                objams.SectorName = Convert.ToString(txtSector.Text.Trim());
                //objams.DistrictId = Convert.ToInt32(DdlDistrict.SelectedValue);
                //objams.Capacity = hdnCapacity.Value.TrimStart('~').TrimEnd('~');

                objams.strUID = hfUID.Value;

                objams.CategoryId = Convert.ToInt32(ddlCATEGORY.SelectedValue);
                if (rbtNew.Checked == true)
                    objams.TypeId = 1;
                else
                    objams.TypeId = 2;
                objams.BoardOfDirectors = hdnDirectors.Value.TrimStart('~').TrimEnd('~');
                objams.Business = hdnBusiness.Value.TrimStart('~').TrimEnd('~');
                objams.NodalOfficerId = Convert.ToInt32(Session["UserId"]);

                //Added by Monalisa Nayak on 23-12-2016 for Add more option in product and capacity
                if (rbtnY.Checked == true)
                    objams.TourismId = 1;
                if (rbtnN.Checked == true)
                    objams.TourismId = 0;
                DataTable dt = new DataTable();
                dt.Columns.Add("Product");
                dt.Columns.Add("Capacity");
                for (int i = 0; i < grdAddMore.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();

                    TextBox Product = (TextBox)grdAddMore.Rows[i].FindControl("txtProduct");
                    TextBox Capacity = (TextBox)grdAddMore.Rows[i].FindControl("txtCapacity");
                    dr["Product"] = Product.Text;
                    dr["Capacity"] = Capacity.Text;
                    if (Product.Text != "") //|| Capacity.Text != "")
                    {
                        dt.Rows.Add(dr);
                    }

                }
                dt.TableName = "tblProposal";
                using (System.IO.StringWriter sw = new System.IO.StringWriter())
                {
                    dt.WriteXml(sw);
                    objams.VCH_XMLTBL = sw.ToString();
                }
                //ADD LOCATION DETAILS 
                if (Grdlocation.Rows.Count > 0)
                {
                    DataTable dt1 = new DataTable();
                    dt1.Columns.Add("District");
                    dt1.Columns.Add("Location");
                    for (int i = 0; i < Grdlocation.Rows.Count; i++)
                    {
                        DataRow dr1 = dt1.NewRow();
                        DropDownList ddlDistrict = (DropDownList)Grdlocation.Rows[i].FindControl("DdlDistrict");
                        TextBox TxtLocation = (TextBox)Grdlocation.Rows[i].FindControl("txtLocation");
                        dr1["District"] = ddlDistrict.Text;
                        dr1["Location"] = TxtLocation.Text;
                        if (ddlDistrict.SelectedValue != "0")
                        {
                            dt1.Rows.Add(dr1);
                        }

                    }
                    dt1.TableName = "tblLoc";
                    using (System.IO.StringWriter sw = new System.IO.StringWriter())
                    {
                        dt1.WriteXml(sw);
                        objams.XmlData = sw.ToString();
                    }
                }

                strVal = AMServices.AddProjectMaster(objams);
                string msg = Messages.ShowMessage(strVal).ToString();

                string[] ss = strVal.Split(',');
                string s1 = ss[1].ToString();

                if (ss[0] == "2")
                {
                    ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Updated Successfully.');location.href='ProposalMasterAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + s1 + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(btnSubmit, this.GetType(), "OnClick", "<script>alert('Data Saved Successfully.');location.href='ProposalMasterAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "&ID=" + s1 + "&PIndex=" + Request.QueryString["PIndex"] + "'</script>", false);
                }

            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "K1", "alert('Please Enter * Mark Field');", true);
        }

        catch (Exception ex)
        {
            Util.LogError(ex, "Agenda");
        }
        finally { objams = null; }

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
        {
            Response.Redirect("ProjectMasterView.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&PIndex=" + Request.QueryString["PIndex"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "");
        }
        else
        {
            string URL = "ProjectMasterAdd.aspx?linkn=" + Request.QueryString["linkn"] + "&linkm=" + Request.QueryString["linkm"] + "&btn=" + Request.QueryString["btn"] + "&tab=" + Request.QueryString["tab"] + "";
            Response.Redirect(URL);
        }

    }

    #endregion

    #region "Get Details For Edit"

    private void GetDetails(string ACTION)
    {
        objams = new AMS();
        objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
        Session["ss"] = Convert.ToInt32(Request.QueryString["ID"]);
        objams.OfficerId = Convert.ToInt32(Session["UserId"]);
        objams.Action = ACTION;
        DataSet ds = new DataSet();
        if (System.Web.HttpContext.Current.Session["PType"] == null)  //ACTION="E"
        {
            ds = AMServices.ViewProjectMasterEdit(objams);
            Status = Convert.ToInt32(ds.Tables[0].Rows[0]["INTSTATUS"].ToString());
        }
        else if (Session["PType"].ToString() == "2")      //ACTION="CNP"
        {
            ds = AMServices.ViewNewRequest(objams);
        }
        DataTable DTT = new DataTable();
        DTT = ds.Tables[2];
        DataRow dr2 = DTT.NewRow();
        dr2["Product"] = "";
        dr2["Capacity"] = "";
        DTT.Rows.Add(dr2);
        grdAddMore.DataSource = DTT;
        grdAddMore.DataBind();

        DataTable Dtloc = new DataTable();
        Dtloc = ds.Tables[3];
        DataRow dr1 = Dtloc.NewRow();
        dr1["DistrictId"] = 0;
        dr1["ProjectLocation"] = "";
        Dtloc.Rows.Add(dr1);
        Grdlocation.DataSource = Dtloc;
        Grdlocation.DataBind();
        if (Grdlocation.Rows.Count > 0)
        {
            for (int i = 0; i < Dtloc.Rows.Count; i++)
            {
                DropDownList district = (DropDownList)Grdlocation.Rows[i].FindControl("DdlDistrict");
                TextBox TxtLocation = (TextBox)Grdlocation.Rows[i].FindControl("txtLocation");
                district.SelectedValue = Dtloc.Rows[i]["DistrictId"].ToString();
            }
        }

        if (intUserTp == 3 || intUserTp == 4)
        {
            if (ds.Tables[4].Rows.Count > 0)
            {
                DataView dv1 = new DataView(ds.Tables[4]);
                dv1.RowFilter = "intCreatedBy=3";  //CMD

                DataView dv2 = new DataView(ds.Tables[4]);
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

                if (intUserTp == 3) //CMD
                {
                    if (dv1.Count > 0)
                    {
                        txtRemark.Text = dv1[dv1.Count - 1][2].ToString();
                        hdnRemarkID.Value = dv1[dv1.Count - 1][4].ToString();
                    }
                }
                else if (intUserTp == 4) //GM
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
            txtTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJECT_TITLE"]);
            txtCompanyName.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJCT_NAME"]);
            txtApplDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DTMAPPLICATION_EBIZ"]).ToString("dd-MMM-yyyy");
            //txtLocation.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJCT_LOCATION"]);
            //txtProduct.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPRODUCT"]);
            ddlSector.SelectedValue = ds.Tables[0].Rows[0]["INTSECTORID"].ToString();
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["INTSECTORID"]) == 21)
                txtSector.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHSECTOR"]);


            if (System.Web.HttpContext.Current.Session["PType"] == null)
            {
                hfUID.Value = "0"; //AS it is loaded from M_AMS_PROJECT
            }
            else if (Session["PType"].ToString() == "2")
            {
                hfUID.Value = Convert.ToString(ds.Tables[0].Rows[0]["VCH_UID"]);  //Project Unique ID of SWP which is stored in AMS as Reference
            }

            ddlCATEGORY.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["INTCATEGORY"].ToString());
            //if (Convert.ToInt32(ds.Tables[0].Rows[0]["INTTourism"]) == 1)
            //    rbtnY.Checked = true;
            //else
            //    rbtnN.Checked = true;
            if (Convert.ToInt32(ds.Tables[0].Rows[0]["INTTYPE"]) == 1)
                rbtNew.Checked = true;
            else
                rbtExpansion.Checked = true;


            //FillNodalOfficer();
            //ddlNodalOfficer.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OfficerId"].ToString());
            //lbNodalOfficer.SelectedValue = Convert.ToString(ds.Tables[0].Rows[0]["OfficerId"].ToString());
            //comment by monalisa
            //DdlDistrict.SelectedValue = ds.Tables[0].Rows[0]["INTDISTRICTID"].ToString();
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dtDirector = null;
                var rows = ds.Tables[1].AsEnumerable()
                    .Where(x => ((int)x["INTTYPE"]) == 1);

                if (rows.Any())
                {
                    dtDirector = rows.CopyToDataTable();
                    lbDirectors.DataSource = dtDirector;
                    lbDirectors.DataTextField = "VCHPROMOTOR";
                    lbDirectors.DataValueField = "VCHPROMOTOR";
                    lbDirectors.DataBind();

                    string Director = string.Empty;
                    foreach (DataRow dr in dtDirector.Rows)
                    {
                        Director = Director + Convert.ToString(dr["VCHPROMOTOR"]) + "~";

                    }
                    //hdnDirectors.Value = Director.TrimEnd('~');
                    ViewState["Director"] = Director.TrimEnd('~');
                    hdnDirectors.Value = ViewState["Director"].ToString();
                }

                DataTable dtBusiness = null;
                var rows1 = ds.Tables[1].AsEnumerable()
                    .Where(x => ((int)x["INTTYPE"]) == 2);
                if (rows1.Any())
                {
                    dtBusiness = rows1.CopyToDataTable();
                    lbBusiness.DataSource = dtBusiness;
                    lbBusiness.DataTextField = "VCHPROMOTOR";
                    lbBusiness.DataValueField = "VCHPROMOTOR";
                    lbBusiness.DataBind();

                    string Busines = string.Empty;
                    foreach (DataRow dr in dtBusiness.Rows)
                    {
                        Busines = Busines + Convert.ToString(dr["VCHPROMOTOR"]) + "~";

                    }
                    hdnBusiness.Value = Busines.TrimEnd('~');
                    //ViewState["Business"] = Busines.TrimEnd('~');
                }
            }
            else
            {
                lbDirectors.Items.Clear();
                lbBusiness.Items.Clear();
            }

        }
    }

    #endregion

    #region "Fill Nodal Officer"
    //private void FillNodalOfficer()
    //{
    //    try
    //    {

    //        objams.Action = "NO";
    //        dt = new DataTable();
    //        dt = AMServices.ViewNodalOfficer(objams);
    //        ddlNodalOfficer.DataSource = dt;
    //        ddlNodalOfficer.DataTextField = "VCHFULLNAME";
    //        ddlNodalOfficer.DataValueField = "intUserId";
    //        ddlNodalOfficer.DataBind();
    //        ddlNodalOfficer.Items.Insert(0, new ListItem("--Select--", "0"));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw new Exception(ex.Message);
    //    }
    //    finally { objams = null; dt = null; }
    //}

    #endregion

    private void MakeDt()
    {
        Objdt.Columns.Add("Product");
        Objdt.Columns.Add("Capacity");
    }
    /// <summary>
    /// Created by Monalisa Nayak on 23-12-2016 for Add more option in product and capacity
    /// </summary>
    /// <returns></returns>
    private DataTable CreateDataTable()
    {
        DataTable Data_table = new DataTable();
        DataColumn Data_Coloumn = new DataColumn();
        Data_Coloumn = new System.Data.DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "Product";
        Data_table.Columns.Add(Data_Coloumn);
        Data_Coloumn = new DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "Capacity";
        Data_table.Columns.Add(Data_Coloumn);

        Data_Coloumn = new DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "Delete";
        Data_table.Columns.Add(Data_Coloumn);

        Data_Coloumn = new DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.Int32");
        Data_Coloumn.ColumnName = "slno";
        Data_table.Columns.Add(Data_Coloumn);

        return Data_table;

    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = null;
        try
        {
            dt = CreateDataTable();
            ImageButton imgbtn = (ImageButton)sender;
            Label lblSlno = (Label)imgbtn.FindControl("lblSlno");
            //lbDirectors.Text = hdnDirectors.Value.TrimEnd('~');
            //lbBusiness.Text = hdnBusiness.Value.TrimEnd('~');
            for (int i = 0; i <= grdAddMore.Rows.Count - 1; i++)
            {
                DataRow dr = dt.NewRow();
                TextBox Product = (TextBox)grdAddMore.Rows[i].FindControl("txtProduct");
                TextBox Capacity = (TextBox)grdAddMore.Rows[i].FindControl("txtCapacity");
                dr["Product"] = Product.Text;
                dr["Capacity"] = Capacity.Text;
                dt.Rows.Add(dr);
            }
            if (dt.Rows.Count > 0)
            {
                dt.Rows.Remove(dt.Rows[int.Parse(lblSlno.Text) - 1]);
                grdAddMore.DataSource = dt;
                grdAddMore.DataBind();
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Agenda");
        }
        finally
        {
            dt = null;
        }
    }

    private void ShowGrid()
    {
        objams = new AMS();
        List<AMS> lstLoc = new List<AMS>();
        objams.CategoryId = 1;
        objams.DistrictId = 0;
        objams.ProjectLocation = "";
        lstLoc.Add(objams);
        Grdlocation.DataSource = lstLoc;
        Grdlocation.DataBind();
    }

    protected void Grdlocation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            var DdlDistrict = (DropDownList)e.Row.FindControl("DdlDistrict");
            dt = new DataTable();
            dt = AMServices.FillDistrict();
            DdlDistrict.DataSource = dt;
            DdlDistrict.DataTextField = "vchDistrictName";
            DdlDistrict.DataValueField = "intDistrictId";
            DdlDistrict.DataBind();
            DdlDistrict.Items.Insert(0, new ListItem("--Select--", "0"));

            ImageButton img = (ImageButton)e.Row.FindControl("imgbtnDelete1");
            //DropDownList Ddldist = (DropDownList)e.Row.FindControl("DdlDistrict");
            TextBox txtLocation = (TextBox)e.Row.FindControl("txtLocation");
            Button BtnAddMore = (Button)e.Row.FindControl("ButtonAdd");

            string dist = (e.Row.FindControl("lblDistrict") as Label).Text;  // Added by Surya
            DdlDistrict.Items.FindByValue(dist).Selected = true;            // Added by Surya

            if (DdlDistrict.SelectedValue == "0") //&& txtLocation.Text == ""
            {
                img.Visible = false;
                BtnAddMore.Visible = true;
            }
            //else if (LCount == 0)
            //{
            //    img.Visible = false;
            //    BtnAddMore.Visible = true;
            //} 
            else
            {
                img.Visible = true;
                BtnAddMore.Visible = false;
            }
        }
    }

    protected void grdAddMore_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton img = (ImageButton)e.Row.FindControl("imgbtnDelete");
            TextBox txtProduct = (TextBox)e.Row.FindControl("txtProduct");
            Button BtnAddMore = (Button)e.Row.FindControl("BtnAddMore");
            if (txtProduct.Text == "")
            {
                img.Visible = false;
                BtnAddMore.Visible = true;
            }
            //else if (PCount == 0)
            //{
            //    img.Visible = false;
            //    BtnAddMore.Visible = true;
            //}  
            else
            {
                img.Visible = true;
                BtnAddMore.Visible = false;
            }
        }


    }

    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();
    }

    private List<AMS> ConvertToList()
    {
        List<AMS> lstLocAdd = new List<AMS>();
        int count = Grdlocation.Rows.Count;
        foreach (GridViewRow gr in Grdlocation.Rows)
        {
            int index = gr.RowIndex;
            DataTable dt = new DataTable();
            if (gr.RowType == DataControlRowType.DataRow)
            {
                objams = new AMS();
                objams.CategoryId = count + 1;
                objams.DistrictId = Convert.ToInt32(((DropDownList)gr.FindControl("DdlDistrict")).SelectedValue);
                objams.ProjectLocation = ((TextBox)gr.FindControl("txtLocation")).Text;
                lstLocAdd.Add(objams);

            }
        }
        return lstLocAdd;
    }

    private void AddNewRowToGrid()
    {
        List<AMS> lstLoc = new List<AMS>();
        try
        {
            if (Grdlocation.Rows.Count > 0)
            {
                lstLoc = ConvertToList();
            }
            var dupes = lstLoc.GroupBy(x => new { x.DistrictId, x.ProjectLocation }).Where(x => x.Skip(1).Any()).ToArray();
            if (dupes.Any())
            {
                ScriptManager.RegisterStartupScript(UpdatePanelloc, this.GetType(), "", "alert('Duplicate Project Location Details.');", true);
                DropDownList box1 = (DropDownList)Grdlocation.Rows[Grdlocation.Rows.Count - 1].Cells[1].FindControl("DdlDistrict");
                TextBox box2 = (TextBox)Grdlocation.Rows[Grdlocation.Rows.Count - 1].Cells[2].FindControl("txtLocation");
                box1.SelectedValue = "0";
                box2.Text = "";
                return;
            }
            objams = new AMS();
            objams.DistrictId = 0;
            objams.ProjectLocation = "";
            lstLoc.Add(objams);
            LCount = lstLoc.Count;
            Grdlocation.DataSource = lstLoc;
            Grdlocation.DataBind();
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Agenda");
        }
        SetPreviousData(lstLoc);
    }

    private void SetPreviousData(List<AMS> lstLoc)
    {
        int rowIndex = 0;
        if (lstLoc.Count > 0)
        {
            for (int i = 0; i < lstLoc.Count; i++)
            {
                DropDownList box1 = (DropDownList)Grdlocation.Rows[rowIndex].Cells[1].FindControl("DdlDistrict");
                TextBox box2 = (TextBox)Grdlocation.Rows[rowIndex].Cells[2].FindControl("txtLocation");
                box1.SelectedValue = lstLoc[i].DistrictId.ToString();
                box2.Text = lstLoc[i].ProjectLocation.ToString();

                ImageButton img = (ImageButton)Grdlocation.Rows[rowIndex].Cells[3].FindControl("imgbtnDelete1");
                Button BtnAddMore = (Button)Grdlocation.Rows[rowIndex].Cells[3].FindControl("ButtonAdd");

                if (box1.SelectedValue == "0") //&& txtLocation.Text == ""
                {
                    img.Visible = false;
                    BtnAddMore.Visible = true;
                }
                //else if (LCount == 0)
                //{
                //    img.Visible = false;
                //    BtnAddMore.Visible = true;
                //}
                else
                {
                    img.Visible = true;
                    BtnAddMore.Visible = false;
                }

                rowIndex++;
            }
        }
    }

    //TO SHOW PRODUCT AND CAPACITY.
    private void ShowProductGrid()
    {
        objams = new AMS();
        List<AMS> lstProduct = new List<AMS>();
        objams.Product = "";
        objams.Capacity = "";
        lstProduct.Add(objams);
        grdAddMore.DataSource = lstProduct;
        grdAddMore.DataBind();
    }

    protected void BtnAddMore_Click(object sender, EventArgs e)
    {
        AddNewRowToProductGrid();
    }

    private List<AMS> ConvertToProductList()
    {
        List<AMS> lstProdAdd = new List<AMS>();
        int count = grdAddMore.Rows.Count;
        foreach (GridViewRow gr in grdAddMore.Rows)
        {
            int index = gr.RowIndex;
            DataTable dt = new DataTable();
            if (gr.RowType == DataControlRowType.DataRow)
            {
                objams = new AMS();
                objams.ID = count + 1;
                objams.Product = ((TextBox)gr.FindControl("txtProduct")).Text;
                objams.Capacity = ((TextBox)gr.FindControl("txtCapacity")).Text;
                lstProdAdd.Add(objams);

            }
        }
        return lstProdAdd;
    }

    private void AddNewRowToProductGrid()
    {
        List<AMS> lstProduct1 = new List<AMS>();
        try
        {
            if (grdAddMore.Rows.Count > 0)
            {
                lstProduct1 = ConvertToProductList();
            }
            var dupes = lstProduct1.GroupBy(x => new { x.Product, x.Capacity }).Where(x => x.Skip(1).Any()).ToArray();
            if (dupes.Any())
            {
                ScriptManager.RegisterStartupScript(UpdPnlProductDtls, this.GetType(), "", "alert('Duplicate Product and Capacity Details.');", true);
                TextBox product = (TextBox)grdAddMore.Rows[grdAddMore.Rows.Count - 1].Cells[1].FindControl("txtProduct");
                TextBox Capacity = (TextBox)grdAddMore.Rows[grdAddMore.Rows.Count - 1].Cells[2].FindControl("txtCapacity");
                product.Text = "";
                Capacity.Text = "";
                return;
            }
            objams = new AMS();
            objams.Product = "";
            objams.Capacity = "";
            lstProduct1.Add(objams);
            PCount = lstProduct1.Count;
            grdAddMore.DataSource = lstProduct1;
            grdAddMore.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }
        SetPreviousDtls(lstProduct1);
    }

    private void SetPreviousDtls(List<AMS> lstProduct1)
    {
        int rowIndex = 0;
        if (lstProduct1.Count > 0)
        {
            for (int i = 0; i < lstProduct1.Count; i++)
            {
                TextBox product = (TextBox)grdAddMore.Rows[rowIndex].Cells[1].FindControl("txtProduct");
                TextBox Capacity = (TextBox)grdAddMore.Rows[rowIndex].Cells[2].FindControl("txtCapacity");
                product.Text = lstProduct1[i].Product.ToString();
                Capacity.Text = lstProduct1[i].Capacity.ToString();
                rowIndex++;
            }
        }
    }

    private DataTable CreateDataTable1()
    {
        DataTable Data_table = new DataTable();
        DataColumn Data_Coloumn = new DataColumn();
        Data_Coloumn = new System.Data.DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "DistrictId";
        Data_table.Columns.Add(Data_Coloumn);
        Data_Coloumn = new DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "ProjectLocation";
        Data_table.Columns.Add(Data_Coloumn);

        Data_Coloumn = new DataColumn();
        Data_Coloumn.DataType = Type.GetType("System.String");
        Data_Coloumn.ColumnName = "Delete";
        Data_table.Columns.Add(Data_Coloumn);

        return Data_table;

    }

    protected void imgbtnDelete1_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dt = null;
        try
        {
            dt = CreateDataTable1();
            ImageButton imgbtn = (ImageButton)sender;
            Label lblSlno = (Label)imgbtn.FindControl("lblSlno");
            for (int i = 0; i <= Grdlocation.Rows.Count - 1; i++)
            {
                DataRow dr = dt.NewRow();
                DropDownList District = (DropDownList)Grdlocation.Rows[i].FindControl("DdlDistrict");
                TextBox Location = (TextBox)Grdlocation.Rows[i].FindControl("txtLocation");
                dr["DistrictId"] = District.Text;
                dr["ProjectLocation"] = Location.Text;
                dt.Rows.Add(dr);
            }
            if (dt.Rows.Count > 0)
            {
                dt.Rows.Remove(dt.Rows[int.Parse(lblSlno.Text) - 1]);
                Grdlocation.DataSource = dt;
                Grdlocation.DataBind();
                if (Grdlocation.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DropDownList district = (DropDownList)Grdlocation.Rows[i].FindControl("DdlDistrict");
                        district.SelectedValue = dt.Rows[i]["DistrictId"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Util.LogError(ex, "Agenda");
        }
        finally
        {
            dt = null;
        }
    }

}


