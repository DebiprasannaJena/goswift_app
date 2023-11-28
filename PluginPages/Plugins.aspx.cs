using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;


public partial class Plugins : System.Web.UI.Page
{
    DataTable dtable;
    static string connectionString = ConfigurationManager.ConnectionStrings["AdminAppConnectionProd"].ConnectionString.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FirstGridViewRow();
        }
    }
    private void FirstGridViewRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;

        dt.Columns.Add(new DataColumn("NatureofDemand", typeof(string)));
        dt.Columns.Add(new DataColumn("Number", typeof(string)));
        dt.Columns.Add(new DataColumn("ConnectedLoad", typeof(string)));
        dt.Columns.Add(new DataColumn("TotalConnectedLoad", typeof(string)));
        dt.Columns.Add(new DataColumn("Remark", typeof(string)));
        dr = dt.NewRow();

        dr["NatureofDemand"] = string.Empty;
        dr["Number"] = string.Empty;
        dr["ConnectedLoad"] = string.Empty;
        dr["TotalConnectedLoad"] = string.Empty;
        dr["Remark"] = string.Empty;


        dt.Rows.Add(dr);
        ViewState["CurrentTable"] = dt;

        grvAddmore.DataSource = dt;
        grvAddmore.DataBind();


    }
    private void AddNewRow()
    {
        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {

                    TextBox txtNature =
                      (TextBox)grvAddmore.Rows[rowIndex].Cells[0].FindControl("txtNature");
                    TextBox txtNumber =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[1].FindControl("txtNumber");
                    TextBox txtConnLoad =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[2].FindControl("txtConnLoad");
                    TextBox txtTotalConnLoad =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[3].FindControl("txtTotalConnLoad");
                    TextBox txtRemark =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[4].FindControl("txtRemark");
                    drCurrentRow = dtCurrentTable.NewRow();


                    dtCurrentTable.Rows[i - 1]["NatureofDemand"] = txtNature.Text;
                    dtCurrentTable.Rows[i - 1]["Number"] = txtNumber.Text;
                    dtCurrentTable.Rows[i - 1]["ConnectedLoad"] = txtConnLoad.Text;
                    dtCurrentTable.Rows[i - 1]["TotalConnectedLoad"] = txtTotalConnLoad.Text;
                    dtCurrentTable.Rows[i - 1]["Remark"] = txtRemark.Text;

                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);

                ViewState["CurrentTable"] = dtCurrentTable;

                grvAddmore.DataSource = dtCurrentTable;
                grvAddmore.DataBind();
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        SetPreviousData();
    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox txtNature =
                       (TextBox)grvAddmore.Rows[rowIndex].Cells[0].FindControl("txtNature");
                    TextBox txtNumber =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[1].FindControl("txtNumber");
                    TextBox txtConnLoad =
                      (TextBox)grvAddmore.Rows[rowIndex].Cells[2].FindControl("txtConnLoad");
                    TextBox txtTotalConnLoad =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[3].FindControl("txtTotalConnLoad");

                    TextBox txtRemark =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[4].FindControl("txtRemark");

                    txtNature.Text = dt.Rows[i]["NatureofDemand"].ToString();

                    txtNumber.Text = dt.Rows[i]["Number"].ToString();
                    txtConnLoad.Text = dt.Rows[i]["ConnectedLoad"].ToString();
                    txtTotalConnLoad.Text = dt.Rows[i]["TotalConnectedLoad"].ToString();
                    txtRemark.Text = dt.Rows[i]["Remark"].ToString();


                    rowIndex++;
                }
            }
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRow();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        DataTable DynamicForm = new DataTable();
        DynamicForm.TableName = "MyTable";
        //DynamicForm.Columns.Add(new DataColumn("INT_ITEMID"));
        DynamicForm.Columns.Add(new DataColumn("INT_APPLICATIONID"));
        DynamicForm.Columns.Add(new DataColumn("VCH_NATUREOFDEMAND"));
        DynamicForm.Columns.Add(new DataColumn("INT_NUMBER"));
        DynamicForm.Columns.Add(new DataColumn("INT_CONNECTEDLOADEACH"));
        DynamicForm.Columns.Add(new DataColumn("INT_TOTALLOAD"));
        DynamicForm.Columns.Add(new DataColumn("VCH_REMARK"));
        DynamicForm.Columns.Add(new DataColumn("INT_DELETED_FLAG"));
        foreach (GridViewRow gv in grvAddmore.Rows)
        {
            string rating = string.Empty;
            TextBox txtNature = gv.FindControl("txtNature") as TextBox;
            TextBox txtNumber = gv.FindControl("txtNumber") as TextBox;
            TextBox txtConnLoad = gv.FindControl("txtConnLoad") as TextBox;
            TextBox txtTotalConnLoad = gv.FindControl("txtTotalConnLoad") as TextBox;
            TextBox txtRemark = gv.FindControl("txtRemark") as TextBox;
         

            DataRow dr = DynamicForm.NewRow();
            //dr["INT_ITEMID"] = 1;
            dr["INT_APPLICATIONID"] = 1;
            dr["VCH_NATUREOFDEMAND"] = txtNature.Text.ToString();
            dr["INT_NUMBER"] = Convert.ToInt32(txtNumber.Text.ToString());
            dr["INT_CONNECTEDLOADEACH"] = Convert.ToInt32(txtTotalConnLoad.Text.ToString());
            dr["INT_TOTALLOAD"] = Convert.ToInt32(txtConnLoad.Text.ToString());
            dr["VCH_REMARK"] = txtRemark.Text.ToString();
            dr["INT_DELETED_FLAG"] = 0;
            DynamicForm.Rows.Add(dr);
        }

        
        string xmltable = GetSTRXMLResult(DynamicForm);
        SqlConnection con = new SqlConnection(connectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("USP_GENERAL_ELECTRICALINFO", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@P_XMLTABLE", xmltable);
        int status = cmd.ExecuteNonQuery();
        con.Close();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Saved Successfully.');", true);
    }
    public static string GetSTRXMLResult(DataTable dtTable)
    {
        string strXMLResult = string.Empty;
        if (dtTable.Rows.Count > 0)
        {
            StringWriter sw = new StringWriter();
            dtTable.WriteXml(sw);
            strXMLResult = sw.ToString();
            sw.Close();
            sw.Dispose();
        }

        return strXMLResult;
    }
    protected void grvAddmore_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];

            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);

                ViewState["CurrentTable"] = dt;
                grvAddmore.DataSource = dt;
                grvAddmore.DataBind();


            }
            SetPreviousData();
        }
    }
    private void SetRowData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox txtNature =
                       (TextBox)grvAddmore.Rows[rowIndex].Cells[0].FindControl("txtNature");
                    TextBox txtNumber =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[1].FindControl("txtNumber");
                    TextBox txtConnLoad =
                      (TextBox)grvAddmore.Rows[rowIndex].Cells[2].FindControl("txtConnLoad");
                    TextBox txtTotalConnLoad =
                     (TextBox)grvAddmore.Rows[rowIndex].Cells[3].FindControl("txtTotalConnLoad");

                    TextBox txtRemark =
                    (TextBox)grvAddmore.Rows[rowIndex].Cells[4].FindControl("txtRemark");

                    txtNature.Text = dt.Rows[i]["NatureofDemand"].ToString();

                    txtNumber.Text = dt.Rows[i]["Number"].ToString();
                    txtConnLoad.Text = dt.Rows[i]["ConnectedLoad"].ToString();
                    txtTotalConnLoad.Text = dt.Rows[i]["TotalConnectedLoad"].ToString();
                    txtRemark.Text = dt.Rows[i]["Remark"].ToString();


                    rowIndex++;
                }
            }
        }
        //SetPreviousData();
    }
    public string PushData()
    {

        return "";
    }
}