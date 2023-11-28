using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
public partial class Master_ViewIncentive : System.Web.UI.Page
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
        DataColumn PolicyCode = new DataColumn("PolicyCode", Type.GetType("System.String"));
        DataColumn PolicyName = new DataColumn("PolicyName", Type.GetType("System.String"));
        DataColumn Description = new DataColumn("Description", Type.GetType("System.String"));
        DataColumn Action = new DataColumn("Action", Type.GetType("System.String"));

        dtable.Columns.Add(PolicyCode);
        dtable.Columns.Add(PolicyName);
        dtable.Columns.Add(Description);
        dtable.Columns.Add(Action);


        dtRow = dtable.NewRow();
        dtRow[PolicyCode] = "PI-001";
        dtRow[PolicyName] = "Policy1";
        dtRow[Description] = "Policy Description One";
        dtable.Rows.Add(dtRow);

        dtRow = dtable.NewRow();
        dtRow[PolicyCode] = "PI-002";
        dtRow[PolicyName] = "Policy1";
        dtRow[Description] = "Policy Description Two";
        dtable.Rows.Add(dtRow);

        dtRow = dtable.NewRow();
        dtRow[PolicyCode] = "PI-003";
        dtRow[PolicyName] = "Policy1";
        dtRow[Description] = "Policy Description Three";
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
        Response.Redirect("AddIncentivePolicy.aspx");
    }
}