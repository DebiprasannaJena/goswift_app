using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Master_ViewIncentiveScheme : System.Web.UI.Page
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
        DataColumn SchemeCode = new DataColumn("SchemeCode", Type.GetType("System.String"));
        DataColumn SchemeName = new DataColumn("SchemeName", Type.GetType("System.String"));
        DataColumn Description = new DataColumn("Description", Type.GetType("System.String"));
        DataColumn Action = new DataColumn("Action", Type.GetType("System.String"));

        dtable.Columns.Add(SchemeCode);
        dtable.Columns.Add(SchemeName);
        dtable.Columns.Add(Description);
        dtable.Columns.Add(Action);


        dtRow = dtable.NewRow();
        dtRow[SchemeCode] = "SI-001";
        dtRow[SchemeName] = "Scheme1";
        dtRow[Description] = "Scheme Description One";
        dtable.Rows.Add(dtRow);

        dtRow = dtable.NewRow();
        dtRow[SchemeCode] = "SI-002";
        dtRow[SchemeName] = "Scheme2";
        dtRow[Description] = "Scheme Description Two";
        dtable.Rows.Add(dtRow);

        dtRow = dtable.NewRow();
        dtRow[SchemeCode] = "SI-003";
        dtRow[SchemeName] = "Scheme3";
        dtRow[Description] = "Scheme Description Three";
        dtable.Rows.Add(dtRow);
        ds.Tables.Add(dtable);
        gvSector.DataSource = ds.Tables[0];
        gvSector.DataBind();



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
        Response.Redirect("AddIncentiveScheme.aspx");
    }
}