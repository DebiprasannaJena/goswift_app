using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;

public partial class SWP_Configuration_Configuration : System.Web.UI.Page
{
    DataTable dtable;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindGrid();
            TextBox textBox = new TextBox();

           // textBox.TextChanged += new EventHandler(txtNameLike_TextChanged);
        }

    }

    private void BindGrid()
    {
        //try
        //{
        ds = new DataSet();
        dtable = new DataTable();
        DataRow dtRow;
        DataColumn formname = new DataColumn("formname", Type.GetType("System.String"));
        //DataColumn ServiceSubSector = new DataColumn("ServiceSubSector", Type.GetType("System.String"));
        //DataColumn Department = new DataColumn("Department", Type.GetType("System.String"));
        //DataColumn ServiceName = new DataColumn("ServiceName", Type.GetType("System.String"));
        //DataColumn Description = new DataColumn("Description", Type.GetType("System.String"));
        //DataColumn Amount = new DataColumn("Amount", Type.GetType("System.String"));
        //DataColumn Action = new DataColumn("Action", Type.GetType("System.String"));

        dtable.Columns.Add(formname);
        //dtable.Columns.Add(ServiceSubSector);
        //dtable.Columns.Add(Department);
        //dtable.Columns.Add(ServiceName);
        //dtable.Columns.Add(Description);
        //dtable.Columns.Add(Amount);
        //dtable.Columns.Add(Action);


        dtRow = dtable.NewRow();
        dtRow[formname] = "Label 1";
        //dtRow[ServiceSubSector] = "Automobile";
        //dtRow[Department] = "Automobile";
        //dtRow[ServiceName] = "Service 1";
        //dtRow[Description] = "Aluminium Industry";
        //dtRow[Amount] = "51 crore";
        //dtRow[Action] = "Approved";
        dtable.Rows.Add(dtRow);

        //dtRow = dtable.NewRow();
        //dtRow[ServiceSector] = "Cement, Lime and Plaster";
        //dtRow[ServiceSubSector] = "Cement, Lime and Plaster";
        //dtRow[Department] = "Environment";
        //dtRow[ServiceName] = "Service 2";
        //dtRow[Description] = "Cement Industry";
        //dtRow[Amount] = "11 crore";
        //dtRow[Action] = "Approved";
        //dtable.Rows.Add(dtRow);

        //dtRow = dtable.NewRow();
        //dtRow[ServiceSector] = "Chemicals and Chemical products";
        //dtRow[ServiceSubSector] = "Basic chemicals";
        //dtRow[Department] = "Automobile";
        //dtRow[ServiceName] = "Service 3";
        //dtRow[Description] = "Chemical Industry";
        //dtRow[Amount] = "21 crore";
        //dtRow[Action] = "Approved";
        //dtable.Rows.Add(dtRow);

        //ds.Tables.Add(dtable);
        ViewState["dtstored"] = dtable;
        gvService.DataSource = dtable;// ds.Tables[0];
        gvService.DataBind();
    }

    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dt2 = new DataTable();
            dt2 = (DataTable)ViewState["dtstored"];
            DataRow dr = dt2.NewRow();
            dr["formname"] = "Level " + (dt2.Rows.Count + 1).ToString();
            dt2.Rows.Add(dr);
            ViewState["dtstored"] = dt2;
            gvService.DataSource = dt2;// ds.Tables[0];
            gvService.DataBind();
        }
        catch (Exception)
        {

        }
    }
    protected void ddlName_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList list = (DropDownList)sender;
        string value = list.SelectedValue;
    }

    protected void gvService_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox myTextBox = (TextBox)(e.Row.Cells[1].FindControl("txtForward"));
            if (myTextBox.Text == "")
            {
                //TextBox1.Text = value;
            }
        }
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("https://www.google.co.in");
    }

    public class Details
    {
        public int IntID { get; set; }
        public string vchName { get; set; }
    }

    [WebMethod]
    public static Details[] GetName(ListBox Controller)
    {
        DataTable Objdt = new DataTable();
        List<Details> details = new List<Details>();

        Objdt = (DataTable)Controller.DataSource;
        foreach (DataRow dtrow in Objdt.Rows)
        {
            Details User = new Details();
            User.IntID = Convert.ToInt32(dtrow["USERID"].ToString());
            User.vchName = dtrow["USERNAME"].ToString();
            details.Add(User);
        }
        return details.ToArray();
    }



    [System.Web.Services.WebMethod()]
    public static string GetUsers(string searchText)
    {
        string strUserIdNames = "--Select--,0,";
        if (searchText != "0")
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add(new DataColumn("Column1", typeof(string)));
                //for (int i = 0; i < lstLeft.Items.Count; i++)
                //{

                //    dr = dt.NewRow();

                //    //dr["Column1"] = lstLeft.Items[i].ToString();
                //    dt.Rows.Add(dr);

                //}


                //DataTable dt = default(DataTable);
                //dt = MeetingServiceV3.FillUsersLikeName(searchText);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    //strUserIdNames += dt.Rows[i][1].ToString() + "," + dt.Rows[i][0].ToString() + ",";
                    strUserIdNames += dt.Rows[i][2].ToString() + "," + dt.Rows[i][0].ToString() + ",";
                }
                return strUserIdNames.Trim(',');
            }
            else
            {
                return strUserIdNames.Trim(',');
            }
        }
        else
        {
            return strUserIdNames.Trim(',');
        }
    }
}