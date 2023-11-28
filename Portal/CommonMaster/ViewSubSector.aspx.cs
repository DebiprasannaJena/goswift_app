using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Master_ViewSubSector : System.Web.UI.Page
{
    DataTable dtable;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {

    }
    private void BindGrid()
    {
        //try
        //{
        ds = new DataSet();
        dtable = new DataTable();
        DataRow dtRow;
        DataColumn SubSectorCode = new DataColumn("SubSectorCode", Type.GetType("System.String"));
        DataColumn SubSectorName = new DataColumn("SubSectorName", Type.GetType("System.String"));
        DataColumn SectorName = new DataColumn("SectorName", Type.GetType("System.String"));
        DataColumn Description = new DataColumn("Description", Type.GetType("System.String"));
        DataColumn Action = new DataColumn("Action", Type.GetType("System.String"));

        dtable.Columns.Add(SubSectorCode);
        dtable.Columns.Add(SubSectorName);
        dtable.Columns.Add(SectorName);
        dtable.Columns.Add(Description);
        dtable.Columns.Add(Action);


        dtRow = dtable.NewRow();
        dtRow[SubSectorCode] = "AI-001-001";
        dtRow[SubSectorName] = "Basic Aluminium";
        dtRow[SectorName] = "Aluminium Industry";
        dtRow[Description] = "Industry deals with aluminium.";
        dtable.Rows.Add(dtRow);

        dtRow = dtable.NewRow();
        dtRow[SubSectorCode] = "EE-005-001";
        dtRow[SubSectorName] = "Batteries and accumulators";
        dtRow[SectorName] = "Electrical Equipment";
        dtRow[Description] = "Deals with electrical equipments.";
        dtable.Rows.Add(dtRow);

        dtRow = dtable.NewRow();
        dtRow[SubSectorCode] = "CE-987-004";
        dtRow[SubSectorName] = "Computers and Peripheral equipment";
        dtRow[SectorName] = "Computer, Electronic and Optical Products";
        dtRow[Description] = "Deals with electronic products.";
        dtable.Rows.Add(dtRow);
        ds.Tables.Add(dtable);
        gvSubSector.DataSource = ds.Tables[0];
        gvSubSector.DataBind();



        //}
        //catch (Exception ex)
        //{

        //}
        //finally
        //{
        //    dtable = null;
        //}
    }
    protected void lbtnAction_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddSubSector.aspx");
    }
}