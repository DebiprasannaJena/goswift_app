using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class SingleWindow_AgendaPrint : System.Web.UI.Page
{
    public string DecisionText { get; set; }
    Double x = 0;
    Double y = 0;
    #region "Page Load"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(Session["UserId"] as string))
        {
            Response.Redirect("../Login.aspx");
        }
        else
        {

            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    GetDetails();
                }
            }
        }
    }

    #endregion

    #region "Get Details"

    private void GetDetails()
    {
        AMS objams = new AMS();
        DataTable ObjDt = new DataTable();
        try
        {
            objams = new AMS();
            objams.ProjectId = Convert.ToInt32(Request.QueryString["ID"]);
            objams.Action = "V";
            DataSet ds = new DataSet();
            ds = AMServices.ViewMom(objams);

            if (ds.Tables[0].Rows.Count > 0)
            {

                fillProjdetails(ds);
                fillProposaldetails(ds);
                fillPromoterdetails(ds);
                fillProjInfo(ds);
                fillFinancialdetails(ds);
                FillSLFCMember(ds);
                FillProjectCostDtls(ds);
                FillFinancingDtls(ds);
                FillDistrictLocation(ds);
                FillMaterialSource(ds);

                try
                {
                    if (ds.Tables[5].Rows.Count > 0)
                    {
                        rptCapacity.DataSource = ds.Tables[5];
                        rptCapacity.DataBind();
                      
                    }
                
                }
                catch (Exception m) { }
            }
        }
        catch (Exception m) { Response.Write(m.Message); }
        finally { objams = null; }
    }

    #endregion


    #region for fetch data

    public void fillProjdetails(DataSet ds)
    {
        try
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblProjTitle.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJECT_TITLE"]);
                lblSector.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHSECTOR"]);
                lblProjNm.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJCT_NAME"]);
                lblApplDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["DTMAPPLICATION_EBIZ"]).ToString("dd-MMM-yyyy");
                //lblDistrict.Text = Convert.ToString(ds.Tables[0].Rows[0]["vchDistrictName"]) + ",";
                //lblLocation.Text = Convert.ToString(ds.Tables[0].Rows[0]["VCHPROJCT_LOCATION"]);
               
                lblCategory.Text = Convert.ToString(ds.Tables[0].Rows[0]["CATEGORY"].ToString());
                lblNew.Text = ds.Tables[0].Rows[0]["PROJECTTYPE"].ToString();
            }
        }
        catch (Exception m) { }

    }

    public void fillProposaldetails(DataSet ds)
    {
        try
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                RptrProposal.DataSource = ds.Tables[1];
                RptrProposal.DataBind();
            }
        }
        catch (Exception m) { }
    }

    public void fillPromoterdetails(DataSet ds)
    {
        try
        {
            if (ds.Tables[2].Rows.Count > 0)
            {
                DataTable dtDirector = null;
                var rows = ds.Tables[2].AsEnumerable()
                    .Where(x => ((int)x["INTTYPE"]) == 1);

                if (rows.Any())
                {
                    dtDirector = rows.CopyToDataTable();
                    RptrPromoterDirectors.DataSource = dtDirector;
                    RptrPromoterDirectors.DataBind();

                }

                DataTable dtBusiness = null;
                var rows1 = ds.Tables[2].AsEnumerable()
                    .Where(x => ((int)x["INTTYPE"]) == 2);
                if (rows1.Any())
                {
                    dtBusiness = rows1.CopyToDataTable();
                    RptrPromoterBusiness.DataSource = dtBusiness;
                    RptrPromoterBusiness.DataBind();

                }

            }

        }
        catch (Exception m) { }
    }

    public void fillProjInfo(DataSet ds)
    {
        try
        {
            if (ds.Tables[3].Rows.Count > 0)
            {

               lblFinDesc.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchFinanceDescription"].ToString());
                if (lblFinDesc.Text.Length > 1)
                    trFin.Visible = true;
                else
                    trFin.Visible = false;
                lblLand.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchLand"].ToString());
                lblWater.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchWater"].ToString());
                lblPower.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchPower"].ToString());

                if ((Convert.ToString(ds.Tables[3].Rows[0]["intPowerSource"].ToString()) == "1"))
                    lblCPP.Text = "Source : CPP";
                else if ((Convert.ToString(ds.Tables[3].Rows[0]["intPowerSource"].ToString()) == "2"))
                {
                    lblCPP.Text = "Source : GRID";
                }
                else if ((Convert.ToString(ds.Tables[3].Rows[0]["intPowerSource"].ToString()) == "3"))
                {
                    lblCPP.Text = "Source : CPP & GRID";
                }


                //lblRawmaterial.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchRawMaterial"]);
                lblImple.Text = Convert.ToString(ds.Tables[3].Rows[0]["vchImplementPeriod"]);
                lblDirect.Text = Convert.ToDecimal(ds.Tables[3].Rows[0]["intEmployement"]).ToString("N2").TrimEnd('0').TrimEnd('.') + " Numbers";
                lblContra.Text = Convert.ToDecimal(ds.Tables[3].Rows[0]["intContractual"]).ToString("N2").TrimEnd('0').TrimEnd('.') + " Numbers";
                lblEmTotal.Text = Convert.ToDecimal(Convert.ToInt32(ds.Tables[3].Rows[0]["intEmployement"]) + Convert.ToInt32(ds.Tables[3].Rows[0]["intContractual"])).ToString("N2").TrimEnd('0').TrimEnd('.') + " Numbers";                

            }
        }
        catch (Exception m) { }

    }

    public void fillFinancialdetails(DataSet ds)
    {
        try
        {
            List<Finance> objFinance = new List<Finance>();
            Finance objFin = new Finance();

            if (ds.Tables[4].Rows.Count > 0)
            {
                objFinance = new List<Finance>();
                foreach (DataRow dr in ds.Tables[4].Rows)
                {
                    objFin = new Finance();
                    objFin.FinanceId = Convert.ToInt32(dr[0]);
                    objFin.ComapnyName = Convert.ToString(dr[1]);
                    objFin.Particulars = Convert.ToString(dr[2]);
                    objFin.FinYear1 = Convert.ToDecimal(dr[3]).ToString("N2");
                    objFin.FinYear2 = Convert.ToDecimal(dr[4]).ToString("N2");
                    objFin.FinYear3 = Convert.ToDecimal(dr[5]).ToString("N2");
                    objFin.FinDoc = Convert.ToString(dr[6]);

                    objFinance.Add(objFin);
                }

                GrdFinanace.DataSource = objFinance;

                GrdFinanace.Columns[2].HeaderText = ds.Tables[4].Columns[3].ColumnName;
                GrdFinanace.Columns[3].HeaderText = ds.Tables[4].Columns[4].ColumnName;
                GrdFinanace.Columns[4].HeaderText = ds.Tables[4].Columns[5].ColumnName;
                GrdFinanace.DataBind();
            }

        }
        catch (Exception m) { }

    }

    public void FillSLFCMember(DataSet ds)
    {
        try
        {
            if (ds.Tables[6].Rows.Count >= 0)
            {
                RptrComments.DataSource = ds.Tables[6];
                RptrComments.DataBind();
            }

        }
        catch (Exception m) { Response.Write(m.Message); }
    }

    public class Finance
    {
        public int SlNo { get; set; }
        public int KeyId { get; set; }
        public int FinanceId { get; set; }
        public string ComapnyName { get; set; }
        public string Particulars { get; set; }
        public string FinYear1 { get; set; }
        public string FinYear2 { get; set; }
        public string FinYear3 { get; set; }
        public string FinDoc { get; set; }
    }

    protected void OnDataBound(object sender, EventArgs e)
    {
        int[] a = new int[2] { 0, 1 };
        for (int i = GrdFinanace.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = GrdFinanace.Rows[i];
            GridViewRow previousRow = GrdFinanace.Rows[i - 1];
            foreach (var j in a)
            {
                if (row.Cells[j].Text == previousRow.Cells[j].Text)
                {
                    if (previousRow.Cells[j].RowSpan == 0)
                    {
                        if (row.Cells[j].RowSpan == 0)
                        {
                            previousRow.Cells[j].RowSpan += 2;
                        }
                        else
                        {
                            previousRow.Cells[j].RowSpan = row.Cells[j].RowSpan + 1;
                        }
                        if (j == 1)
                        {
                            if (row.Cells[0].RowSpan == 0)
                            {
                                previousRow.Cells[0].RowSpan += 2;
                            }
                            else
                            {
                                previousRow.Cells[0].RowSpan = row.Cells[0].RowSpan + 1;
                            }
                            row.Cells[0].Visible = false;
                        }

                        row.Cells[j].Visible = false;
                    }
                }
            }
        }
    }
   
    /// <summary>
    /// Created by Monalisa Nayak on 27-12-2016 to bind Project Cost details
    /// </summary>
    /// <param name="ds"></param>
    public void FillProjectCostDtls(DataSet ds)
    {
        try
        {
            if (ds.Tables[7].Rows.Count >= 0)
            {
                GrdProjectCostDtls.DataSource = ds.Tables[7];
                GrdProjectCostDtls.DataBind();
                GrdProjectCostDtls.FooterRow.Cells[0].Text = "Total";
                var total = GrdProjectCostDtls.FooterRow.FindControl("lblGrandTotal") as Label;
                total.Text = x.ToString("N2");
                lblProjCost.Text = x.ToString("N2");
            }
        }
        catch (Exception m) { Response.Write(m.Message); }
    }
 
    #endregion
    protected void GrdProjectCostDtls_RowDataBound1(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGrandTotal = (Label)e.Row.FindControl("lblGrandTotal");
            Label lblGrand = (Label)e.Row.FindControl("lblCost");
            x = x + Convert.ToDouble(lblGrand.Text);
        }
    }
    public void FillFinancingDtls(DataSet ds)
    {
        if (ds.Tables[9].Rows.Count > 0)
        {
            GrdFinDtls.DataSource = ds.Tables[9];
            GrdFinDtls.DataBind();
            GrdFinDtls.FooterRow.Cells[0].Text = "Total";
            var total = GrdFinDtls.FooterRow.FindControl("lblGTotal") as Label;
            total.Text = y.ToString("N2");
            //lblProjCost.Text = x.ToString("N2");
        }
    }
    protected void GrdFinDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGTotal = (Label)e.Row.FindControl("lblGTotal");
            Label lblGrand = (Label)e.Row.FindControl("lblAmount");
            y = y + Convert.ToDouble(lblGrand.Text);

        }
    }
    public void FillDistrictLocation(DataSet ds)
    {
        try
        {
            if (ds.Tables[10].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[10].Rows.Count; i++)
                {
                    Label label = new Label();
                    string District = ds.Tables[10].Rows[i]["District"].ToString();
                    string Loc = ds.Tables[10].Rows[i]["Location"].ToString();
                    string Com = Loc + "," + District + "<br/>";
                    label.Text = Com;
                    placeholder.Controls.Add(label);
                } 
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void FillMaterialSource(DataSet ds)
    {
        try
        {
            if (ds.Tables[11].Rows.Count > 0)
            {
                GrdSource.DataSource = ds.Tables[11];
                GrdSource.DataBind();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
}