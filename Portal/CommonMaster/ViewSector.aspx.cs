using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLogicLayer.MasterSector;

public partial class Master_ViewSector : System.Web.UI.Page
{
    DataTable dtable;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
 BindDdl();
        BindGridview();
        }
        
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
         
        MasterSectorBusinesslayer objService = new MasterSectorBusinesslayer();

        List<EntityLayer.Mastersector.Gridviewgrd> objProperty = new List<EntityLayer.Mastersector.Gridviewgrd>();
        EntityLayer.Mastersector.Gridviewgrd objinput = new EntityLayer.Mastersector.Gridviewgrd();
        try
        {

            objinput.strAction = "W";
            objinput.SectorCode =ddlPolicyReferences.SelectedIndex==0?0: Convert.ToInt32(ddlPolicyReferences.SelectedItem.ToString());
            objinput.SectorPriority = (chkSector.Checked) ? 1 : 0;
            objinput.strSectorName = txtSectorName.Text.Trim();
           
            
            objProperty = objService.BindDropdowngrd(objinput).ToList();
            

            gvSector.DataSource = objProperty;
            gvSector.DataBind();
            

        }
        catch (Exception)
        {

        }
    
    }
    public void BindDdl()
    {
        MasterSectorBusinesslayer objService = new MasterSectorBusinesslayer();

        List<EntityLayer.Mastersector.MasterGrid> objProperty = new List<EntityLayer.Mastersector.MasterGrid>();
        EntityLayer.Mastersector.MasterGrid objinput = new EntityLayer.Mastersector.MasterGrid();
        try
        {

            objinput.strAction = "V";
            objProperty = objService.BindDropdown(objinput).ToList();

            ddlPolicyReferences.DataTextField = "SectorCode";
            ddlPolicyReferences.DataValueField = "SectorId";
            ddlPolicyReferences.DataSource = objProperty;
            ddlPolicyReferences.DataBind();
            ddlPolicyReferences.Items.Insert(0, new ListItem("-Select-", "0"));

        }
        catch (Exception)
        {

        }
    }
    public void BindGridview()
    {
        MasterSectorBusinesslayer objService = new MasterSectorBusinesslayer();

        List<EntityLayer.Mastersector.Gridviewgrd> objProperty = new List<EntityLayer.Mastersector.Gridviewgrd>();
        EntityLayer.Mastersector.Gridviewgrd objinput = new EntityLayer.Mastersector.Gridviewgrd();
        try
        {

            objinput.strAction = "V";
            objProperty = objService.BindDropdowngrd(objinput).ToList();


            gvSector.DataSource = objProperty;
            gvSector.DataBind();
            

        }
        catch (Exception)
        {

        }
    }
    //private void BindGrid()
    //{
    //    //try
    //    //{
    //    ds = new DataSet();
    //    dtable = new DataTable();
    //    DataRow dtRow;
    //    DataColumn SectorCode = new DataColumn("SectorCode", Type.GetType("System.String"));
    //    DataColumn SectorName = new DataColumn("SectorName", Type.GetType("System.String"));
    //    DataColumn Description = new DataColumn("Description", Type.GetType("System.String"));
    //    DataColumn Action = new DataColumn("Action", Type.GetType("System.String"));

    //    dtable.Columns.Add(SectorCode);
    //    dtable.Columns.Add(SectorName);
    //    dtable.Columns.Add(Description);
    //    dtable.Columns.Add(Action);


    //    dtRow = dtable.NewRow();
    //    dtRow[SectorCode] = "AI-001";
    //    dtRow[SectorName] = "Aluminium Industry";
    //    dtRow[Description] = "Industry deals with aluminium.";
    //    dtable.Rows.Add(dtRow);

    //    dtRow = dtable.NewRow();
    //    dtRow[SectorCode] = "EE-005";
    //    dtRow[SectorName] = "Electrical Equipment";
    //    dtRow[Description] = "Deals with electrical equipments.";
    //    dtable.Rows.Add(dtRow);

    //    dtRow = dtable.NewRow();
    //    dtRow[SectorCode] = "CE-987";
    //    dtRow[SectorName] = "Computer, Electronic and Optical Products";
    //    dtRow[Description] = "Deals with electronic products.";
    //    dtable.Rows.Add(dtRow);
    //    ds.Tables.Add(dtable);
    //    gvSector.DataSource = ds.Tables[0];
    //    gvSector.DataBind();



    //    //}
    //    //catch (Exception ex)
    //    //{

    //    //}
    //    //finally
    //    //{
    //    //    dtable = null;
    //    //}
    //}

    protected void lbtnAction_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddSector.aspx");
    }
    protected void gvSector_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            string s = gvSector.DataKeys[e.NewEditIndex].Value.ToString();
             Response.Redirect("AddSector.aspx?id="+s);
        }
        catch (Exception ex)
        {
            
            throw ex;
        }
    }
}